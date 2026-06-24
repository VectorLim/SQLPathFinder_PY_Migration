# Stage 1 Plan — VG2 Parser + Block Classifier

Audience: the coding agent (or human) about to implement Stage 1.
Scope: turn raw VG2 text into an ordered list of classified blocks. Nothing more.

Architectural anchors already approved:
- Deterministic staged compiler. No agentic AI in v1.
- Strategy + decorator-driven handler registry (used **later**, not in Stage 1).
- Declarative YAML View Registry (used **later**, not in Stage 1).
- Diagnostics-first: collect, do not crash on malformed individual blocks.
- Keep Stage 1 small. Do not pre-build runtime helpers, IR, or resolver code here.

> **Note on prior artifacts.** The files under [tests/fixtures/classification/](tests/fixtures/classification/) show a previous design that extracted a typed `spec` per block (e.g. `csv_out`, `tables`, `headers`, `delete_patterns`). **Stage 1 deliberately does NOT extract typed specs.** It produces only `kind` + raw options dict + body text. Typed specs belong to the Resolver / handlers in a later stage. Treat those JSON files as informational, not as the target schema for Stage 1.

---

## 1. Stage 1 Output Model

Five data structures. All immutable (`@dataclass(frozen=True, slots=True)`). All in `models.py`.

### `Kind` (enum)
The smallest set that covers what we have actually observed in the fixtures, plus explicit slots for malformed/unknown.

| Member | Trigger summary |
|---|---|
| `MARS_READ` | `/NODE` ending in `.MARS` and `/ENGINE=VA` (real DB read) |
| `OASYS_READ` | `/NODE` containing `.OASYS` and `/ENGINE=VA` |
| `ARIES_READ` | `/NODE` containing `.ARIES` and `/ENGINE=VA` (speculative — no fixture yet; rule kept but flagged) |
| `SQLITE_QUERY` | `/OLEDB=SQLite` or `/ENGINE=SQLite` |
| `WRITE_FILE` | `/WRITE-FILE=Y` |
| `HTML_REPORT` | `/REPORT=HTML-*` |
| `UTILITY` | `/UTILITIES=` whose value is NOT a `{...}` control token |
| `MACRO_CONTROL` | `/UTILITIES=` whose value **starts** with `{` (e.g. `{START-MACRO}`, `{END-MACRO}`, `{IF-THEN}`, `{ELSE}`, `{END-IF}`, `{ROWS-IN-FILE}`) |
| `UNKNOWN` | Recognizable structure but no rule matched |
| `MALFORMED` | Parser could not extract usable options or body |

Why split `UTILITY` vs `MACRO_CONTROL`:
- The later `ScopeBuilder` needs to find control tokens cheaply without re-parsing the utility string. Tagging them at classification time is one cheap test that prevents a downstream re-scan.
- We do **NOT** further subclassify `UTILITY` in Stage 1 (no `RUN_PYTHON`, no `EMAIL`, no `COPY`, no `DELETE`). That fan-out is the handler stage's job. Leaving it at `UTILITY` here keeps the classifier table short and stable.

### `BlockOptions`
Wraps an ordered dict of slash-key options.

Fields:
- `pairs: tuple[tuple[str, str], ...]` — preserves source order and duplicates.
- `lookup: Mapping[str, str]` — last-write-wins view for convenient access.

Why both: duplicates exist in the wild (e.g. multiple `/TABLE=` lines on join blocks). Lookup is the common case; `pairs` is the source of truth for diagnostics and emission later. Keys stored **uppercase** (canonical) so callers don't have to normalize.

Not included yet: typed coercions (no `int`, no `bool`, no `list[str]` for comma-separated values). Those are stage-local concerns for resolvers/handlers.

### `SourceSpan`
Tracks where a block came from for diagnostics.

Fields:
- `file: Path | None` (None when parsing in-memory text)
- `start_line: int` (1-based, inclusive)
- `end_line: int` (1-based, inclusive)

Not included: column offsets, byte offsets. Line precision is sufficient for human diagnostics.

### `ParsedBlock`
The parser's output (one per block).

Fields:
- `index: int` — 0-based position in the file (source order is authoritative).
- `options: BlockOptions`
- `body: str` — verbatim body text, including blank lines and any `/*BEGIN SQL*/` / `/*END SQL*/` framing. The parser does **not** strip these markers; later stages can if they want.
- `raw: str` — the full original text of the block (options block + body), for diagnostics and round-trip debugging.
- `span: SourceSpan`

Not included: `kind`, `spec`, parsed SQL, parsed Python AST, normalized headers list. All of that lives downstream.

### `ClassifiedBlock`
The classifier's output. Light wrapper, not a subclass.

Fields:
- `parsed: ParsedBlock`
- `kind: Kind`
- `reason: str` — one-line explanation of which rule fired (e.g. `"OLEDB=SQLite"` or `"/NODE endswith .MARS and /ENGINE=VA"`). Useful in diagnostics and when debugging misclassification on a new fixture. Cheap.

Not included: `role` (leaf vs container — that's a Resolver concept), `spec` (handler concept), `engine` (already implicit in kind).

### `Diagnostic`
Fields:
- `severity: Literal["info", "warning", "error"]`
- `code: str` — short stable identifier (e.g. `"unclosed-options"`, `"unknown-kind"`). Stable codes matter for filtering / `--strict` later.
- `message: str`
- `block_index: int | None`
- `span: SourceSpan | None`
- `hint: str | None` — optional suggested fix

`Diagnostic` is plain data. **No exceptions raised** for per-block issues. Only an unreadable file (e.g. encoding error on the whole file) raises.

---

## 2. Parser Plan

Single file: `parser.py`. One public function: `parse(text: str, source: Path | None = None) -> tuple[list[ParsedBlock], list[Diagnostic]]`.

Strategy: small, linear, deterministic. No regex backtracking traps, no streaming, no tokenizer framework.

### 2.1 Normalization
- Read as UTF-8 with `errors="replace"`. VG2 files in fixtures are ASCII-clean but some contain non-ASCII paths/messages. `errors="replace"` emits a single `info` diagnostic (`"non-utf8-bytes-replaced"`) and continues.
- Strip a leading BOM if present.
- Normalize line endings to `\n` once at ingest. Track that the original used `\r\n` only if we need round-trip later — Stage 1 does not.
- Do **not** trim trailing whitespace from lines; SQL bodies and shell commands rely on exact body content.

### 2.2 Block splitting
- Split on `<---- New Query ---->` (treated as a fence, not part of either neighbor block).
- Regex used for splitting: `r"^[ \t]*<----[ \t]*New Query[ \t]*---->[ \t]*$"` with `re.MULTILINE`. Tolerates incidental whitespace; rejects in-line uses inside bodies (unlikely but cheap).
- Per the fixtures, the very first block may have no preceding separator. That is fine — the split yields it as block 0.
- An empty/whitespace-only segment between two separators emits a `warning` diagnostic (`"empty-block"`) and is skipped (no `ParsedBlock` produced). This keeps indices stable to non-empty blocks.

### 2.3 Options extraction (per block)
Two parsing modes, attempted in this order:

1. **Explicit form** — block text contains `<OPTIONS>` … `</OPTIONS>`.
   - Locate first `<OPTIONS>` and matching `</OPTIONS>` (line-anchored).
   - Everything between is the options region. Everything after `</OPTIONS>` is the body.
   - If `<OPTIONS>` is present without `</OPTIONS>`: emit `error` `"unclosed-options"`, treat the rest of the block as options (lossy but recoverable), emit empty body.

2. **Inline form** — no `<OPTIONS>` marker found.
   - Walk leading lines that start with `/`. As soon as a non-option line is hit, switch to body.
   - Emit an `info` diagnostic `"inline-options"` so we can spot how often this happens in real data.
   - **All current fixtures use the explicit form.** This branch is implemented but mostly exercised by synthetic tests; it exists to satisfy the prompt's robustness requirement and will earn its real keep when a new sample needs it.

### 2.4 Option-line parsing
For each line in the options region:
- Strip trailing `\r` and trailing whitespace; preserve leading whitespace **inside the value**, not before the slash.
- Skip blank lines silently.
- Must match `r"^/([A-Z][A-Z0-9_\-]*)=(.*)$"` (case-insensitive on the key; we canonicalize uppercase).
- Value is everything after the first `=` up to end of line. **No quote stripping, no escape handling.** VG2 doesn't escape; values like `setsiteparam.exe KM <<<SFOLDER>>> <<<UNDERDEV>>>` are kept verbatim.
- Lines that don't match emit `warning` `"malformed-option-line"` and are dropped (block still produced).

Duplicate keys: kept in `pairs`, last value wins in `lookup`. Duplicate emits `info` `"duplicate-option-key"` once per (block, key). This is informational because legitimate `/TABLE=` repetition exists.

### 2.5 Body preservation
- Body is everything from the line **after** `</OPTIONS>` (explicit form) or the first non-option line (inline form) to the end of the block segment.
- Trim **one** leading blank line if present (cosmetic; fixtures consistently have one) and one trailing blank line. Do not collapse interior blank lines.
- Empty body is valid (utility blocks routinely have no body).

### 2.6 Source spans
- Track line offsets during splitting. Each `ParsedBlock` carries the absolute `(start_line, end_line)` in the original file.

### 2.7 What the parser deliberately does NOT do
- No SQL parsing, no Python parsing, no CSV parsing of body content.
- No path normalization on option values.
- No macro placeholder resolution.
- No `<<<NAME>>>` / `<<>>` interpretation.
- No `@EXEDIR@` / `@[]@` substitution.
- No detection of `/*BEGIN SQL*/` framing (kept in body verbatim).

---

## 3. Classifier Plan

Single file: `classifier.py`. One public function: `classify(blocks: list[ParsedBlock]) -> tuple[list[ClassifiedBlock], list[Diagnostic]]`.

Implementation: an **ordered list of rules**, each a small function `(BlockOptions, body) -> Kind | None`. First match wins. Adding a new rule = appending one entry. No reflection, no decorator framework here — rules are too few and too interrelated to need one. (The decorator-based handler registry is for the *later* per-Kind translation stage.)

### 3.1 Rule order (first match wins)
1. **`/REPORT` starts with `HTML-`** → `HTML_REPORT`
   *Examples seen: `HTML-RUN`, `HTML-LAYOUT`, `HTML-DELETE`. We do not subclassify in Stage 1; the value is preserved in options.*
2. **`/WRITE-FILE=Y`** → `WRITE_FILE`
3. **`/UTILITIES` present and value starts with `{`** → `MACRO_CONTROL`
   *Strips leading whitespace before checking the brace. Tag set covers `{START-MACRO}`, `{END-MACRO}`, `{IF-THEN}`, `{ELSE}`, `{END-IF}`, `{ROWS-IN-FILE}` — and any new `{TOKEN}` we have not seen, which still classifies correctly without code change.*
4. **`/UTILITIES` present** (and rule 3 did not match) → `UTILITY`
5. **`/OLEDB=SQLite` or `/ENGINE=SQLite`** → `SQLITE_QUERY`
6. **`/NODE` value ends with `.MARS` (case-insensitive) and `/ENGINE=VA`** → `MARS_READ`
   *Pushback on the prompt's suggestion to use `/RECORD` as a MARS signal: `/RECORD` is present on every reader block regardless of dialect. The dialect signal is in `/NODE` (e.g. `KM.[A15_PROD_21.].MARS` vs `KM.OASYS`). `/RECORD` only tells you it's a real SPF read, which `/ENGINE=VA` already does.*
7. **`/NODE` value ends with `.OASYS` (case-insensitive) and `/ENGINE=VA`** → `OASYS_READ`
8. **`/NODE` value ends with `.ARIES` (case-insensitive) and `/ENGINE=VA`** → `ARIES_READ`
   *No ARIES sample in fixtures yet. Rule kept but tagged in the `reason` string so a misclassification is auditable. Emits an `info` `"aries-rule-untested"` the first time it fires per run.*
9. **Otherwise** → `UNKNOWN` + `warning` `"unknown-kind"`.

`MALFORMED` is **not** assigned by the classifier; only the parser produces it (when options or block structure cannot be recovered at all).

### 3.2 What the classifier deliberately does NOT do
- Does not subclassify `UTILITY` by which `.va` / `.bat` is invoked.
- Does not extract macro frame info (csv file, condition operands) — that's for the resolver.
- Does not validate semantic consistency (e.g. SQLite block referencing a non-existent CSV input) — that's Validator/DataflowAnalyzer territory.
- Does not look inside the body. Classification is options-only. (This is a deliberate constraint; if a future block kind requires body inspection, we'll add it as one rule and document the exception.)

---

## 4. Diagnostics Plan

Trimmed to the items that produce usable signal on the current fixtures. Anything speculative is excluded.

| Code | Severity | When |
|---|---|---|
| `non-utf8-bytes-replaced` | info | File contained non-UTF-8 bytes; replacement used |
| `empty-block` | warning | Whitespace-only segment between separators; block skipped |
| `unclosed-options` | error | `<OPTIONS>` present, no `</OPTIONS>` found |
| `inline-options` | info | Block parsed without `<OPTIONS>` markers |
| `malformed-option-line` | warning | Line in options region failed to match `/KEY=value` |
| `duplicate-option-key` | info | Same key appeared more than once in one block |
| `unknown-kind` | warning | Classifier had no matching rule |
| `aries-rule-untested` | info | ARIES rule fired (no fixture coverage yet) |

Excluded from Stage 1 (with reason):
- *"`/CSV` but no body"*: WRITE_FILE blocks legitimately have empty bodies in some cases; this check belongs to Validator with cross-block context.
- *"Suspicious inline option parsing"*: too vague to be actionable.
- *"Block too large / too small"*: arbitrary thresholds, no value.

Diagnostics flow from both parser and classifier into a single merged list returned alongside results. The CLI / test layer decides what to print or fail on.

---

## 5. Testing Plan

All under `tests/frontend/`. Pytest. No mocking — these are pure functions over text.

### 5.1 Unit tests — `tests/frontend/test_parser.py`
One assertion-tight test per behavior:

- Splits two blocks separated by `<---- New Query ---->`.
- Splits with leading/trailing whitespace on the separator line.
- Parses a clean `<OPTIONS>…</OPTIONS>` block; key order preserved.
- Parses inline options (no `<OPTIONS>` markers).
- Preserves SQL body byte-for-byte (including `/*BEGIN SQL*/` and `ORDER BY` indentation).
- Preserves Python body byte-for-byte (multiline, leading spaces matter).
- Preserves CSV body and HTML body byte-for-byte.
- Quoted utility argument values kept verbatim, including embedded `"` (no unescaping).
- Duplicate `/TABLE=` keys: both in `pairs`, last in `lookup`, one `duplicate-option-key` diagnostic emitted.
- Empty block between separators: zero blocks produced, one `empty-block` warning.
- Unclosed `<OPTIONS>` produces `unclosed-options` error and best-effort block.
- Source spans line up with the actual line numbers (test with a synthetic multi-block string).
- `<<<SFOLDER>>>` and `<<>>` in option values pass through verbatim (no interpretation).
- UNC path `\\AZATSHFS.intel.com\...` in option value passes through verbatim.

### 5.2 Unit tests — `tests/frontend/test_classifier.py`
One test per rule branch, plus the fall-through:

- `/REPORT=HTML-RUN` → `HTML_REPORT`; `/REPORT=HTML-LAYOUT` → same; `/REPORT=HTML-DELETE` → same.
- `/WRITE-FILE=Y` → `WRITE_FILE` (body is a `.py`, `.bat`, `.csv`, and `.htm` — all four should classify the same).
- `/UTILITIES={START-MACRO} "macrotmp.csv" "N"` → `MACRO_CONTROL`.
- `/UTILITIES={IF-THEN} …`, `{ELSE}`, `{END-IF}`, `{END-MACRO}`, `{ROWS-IN-FILE} …` → all `MACRO_CONTROL`.
- `/UTILITIES=@EXEDIR@\Run_Python_Script.va …` → `UTILITY` (and *not* `MACRO_CONTROL`).
- `/UTILITIES=getcsrsu.bat` → `UTILITY`.
- `/OLEDB=SQLite` (also `/ENGINE=SQLite`) → `SQLITE_QUERY`.
- `/NODE=KM.[A15_PROD_21.].MARS` + `/ENGINE=VA` → `MARS_READ`.
- `/NODE=KM.OASYS` + `/ENGINE=VA` → `OASYS_READ`.
- Block with no recognized signals → `UNKNOWN` + diagnostic.

### 5.3 End-to-end fixture tests — `tests/frontend/test_fixtures.py`
Parameterized over the five fixtures. For each:
- Parse + classify succeeds without raised exception.
- Returns ≥1 `ClassifiedBlock`.
- Block indices are dense and 0-based (`[b.parsed.index for b in result] == list(range(len(result)))`).
- No `error`-severity diagnostics on `script_short.txt`, `script_another.txt`, `sql_script.txt`, `script_from_vietnam.txt`. (Their content is clean; an error here means the parser regressed.)

Per-fixture sanity assertions (kind-presence, not exact counts):

- **`script_short.txt`**: at least one `SQLITE_QUERY`.
- **`script_another.txt`**: at least one `MARS_READ`, at least one `WRITE_FILE`, at least one `UTILITY`.
- **`sql_script.txt`**: at least one `MARS_READ`, at least one `OASYS_READ`, at least one `SQLITE_QUERY`.
- **`script_from_vietnam.txt`**: at least one `MARS_READ`, at least one `WRITE_FILE`, at least one `UTILITY`.
- **`actual_script.txt`** (the demanding one):
  - At least one `HTML_REPORT` (HTML-RUN, HTML-LAYOUT, and HTML-DELETE all present).
  - At least one `WRITE_FILE` whose `/CSV=` ends in `.bat`, one ending in `.csv`, one ending in `.htm`, one ending in `.py` (if present).
  - At least one `SQLITE_QUERY`.
  - At least one `MACRO_CONTROL` for each of: `{START-MACRO}`, `{END-MACRO}`, `{IF-THEN}`, `{ELSE}`, `{END-IF}`, `{ROWS-IN-FILE}`.
  - At least one `UTILITY` that mentions `Run_Python_Script.va` *or* `SQLPathFinder_Email.va` *or* `RoboCopy.va` (assert by substring on the `/UTILITIES=` option value).
  - **No** block reaches `UNKNOWN`. If this fails on a real-script update, the test message must show the offending block's `prompt-text` and `raw[:200]` so the gap is obvious.
  - Source order: `PROMPT-TEXT` values, when present and starting with `Step `, appear in monotonically non-decreasing source order (a soft assertion — VG2 step numbering is human-authored; we do not parse the dotted version).

No assertions on exact block counts. Fixtures may grow.

### 5.4 What we do **not** test in Stage 1
- Macro placeholder resolution (no resolver yet).
- View expansion (no registry yet).
- SQL semantics (out of scope).
- DataSyncX integration (out of scope).
- Performance (parsing all five fixtures should be sub-second on cold start; if it isn't, the implementation is wrong, no benchmark needed).

---

## 6. File Layout for Stage 1

```
src/
  vg2c/
    __init__.py
    frontend/
      __init__.py        # re-exports: parse, classify, Kind, ParsedBlock, ClassifiedBlock, Diagnostic
      models.py          # dataclasses + Kind enum (single file; ~150 lines)
      parser.py          # parse(text, source=None) -> (blocks, diagnostics)
      classifier.py      # classify(blocks) -> (classified, diagnostics) + rule table
tests/
  conftest.py            # (already exists; FIXTURES fixture)
  frontend/
    __init__.py
    test_parser.py
    test_classifier.py
    test_fixtures.py
```

That's it for Stage 1. No `cli.py`, no `runtime/`, no `resolver/`, no `handlers/`, no `view_registry/`. Those land when their stage starts.

`pyproject.toml` already declares the `vg2c` package; we just need to add `[tool.setuptools.packages.find] where = ["src"]` (or move to a flat layout if preferred) and ensure `tests/` discovers `src/`. The `pythonpath = ["."]` entry suggests a flat-layout preference; adopt whichever is simpler — I recommend `src/`-layout to keep the package importable but isolated from test discovery. **Decide once at the start of implementation and stay with it.**

---

## 7. Simplicity Check

**Intentionally NOT in Stage 1:**
- Handler registry / decorator-based plugin system. (Belongs to translation stage.)
- View Registry, dialect policy, schema-placeholder substitution. (Resolver stage.)
- MacroFrame stack, ChainMap of named vars, positional cursor. (Resolver stage.)
- Typed per-Kind `spec` extraction (despite the prior JSON fixtures showing it).
- SQL preprocessing (`SQL_Get_CSV_List` and friends).
- Any runtime helpers (`csv_io`, `paths`, `sqlite_engine`, `mail`, `fs_ops`).
- CLI binary. The parser/classifier are library functions; the CLI lands when there's a stage to invoke from it.
- `--strict` flag. Diagnostics are returned; how a caller reacts is the caller's job until there's a caller.

**Most likely over-engineering trap:**
The classifier rule list. It is tempting to subclassify `UTILITY` (Run_Python vs Email vs RoboCopy vs SPFDelete vs raw `.bat`) right now, because the patterns are visible in `actual_script.txt`. **Don't.** That subclassification is handler-stage knowledge; doing it here would (a) duplicate logic with the handlers, (b) couple the classifier to specific utility filenames, and (c) make adding a new utility a two-place edit forever. Leave `UTILITY` as a single kind in Stage 1.

The second-most-likely trap: inventing a `Spec` abstraction up-front because the prior JSON fixtures show one. Stage 1 ships `kind` + raw options + body, period. Specs come when the resolver needs them.

**Minimum useful implementation path for the first commit:**
1. Create `src/vg2c/frontend/models.py` with `Kind`, `BlockOptions`, `SourceSpan`, `ParsedBlock`, `ClassifiedBlock`, `Diagnostic`. ~100 lines, no logic.
2. Create `src/vg2c/frontend/parser.py` implementing the explicit-form path only (skip inline mode in this commit — all current fixtures are explicit). Emit `inline-options` diagnostic but do not yet implement the inline body extraction; that lands when a fixture needs it. Cover §2.1, §2.2, §2.3 (mode 1), §2.4, §2.5, §2.6.
3. Create `src/vg2c/frontend/classifier.py` with the rule list from §3.1.
4. Wire `__init__.py` re-exports.
5. Write `test_parser.py` and `test_classifier.py` unit tests (§5.1, §5.2).
6. Write `test_fixtures.py` with the per-fixture sanity assertions (§5.3).
7. Run `pytest`; iterate until green; commit.

This is one to two days of focused work. The inline-options path, the ARIES rule, and any diagnostic refinements come in follow-up commits driven by real failures, not speculation.

---

*End of Stage 1 plan. Step 2 — implementation — proceeds only after this plan is approved.*
