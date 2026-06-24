# Stage 2 Plan — Resolver, Macro System, SQL Macro Expansion

Audience: the coding agent (or human) about to implement Stage 2.
Source of truth for Stage 1: [progress/stage1_parser_classifier.md](progress/stage1_parser_classifier.md).
Scope: turn `list[ClassifiedBlock]` into a `ResolvedProgram` with scope tree, resolved macros, and structured SQL macro calls. Nothing more.

Approved anchors (carried forward):
- Deterministic. No agentic AI.
- Diagnostics-first: collect, do not crash on a single bad block.
- Keep components minimal; every component must earn its slot.
- No code emission, no DataSyncX, no view expansion in Stage 2.

---

## Step 1 — What Stage 1 Guarantees / What It Does Not

### Stage 2 can safely assume (from the Stage 1 summary)
- **Block boundaries are stable** and dense-indexed 0..N-1 in source order.
- **Every block has a `Kind`**, one of: `MARS_READ`, `OASYS_READ`, `ARIES_READ`, `SQLITE_QUERY`, `WRITE_FILE`, `HTML_REPORT`, `UTILITY`, `MACRO_CONTROL`, `UNKNOWN`, `MALFORMED`.
- **`MACRO_CONTROL` is the *only* kind that holds `{...}` control tokens.** ScopeBuilder can iterate the list and decide on this single field.
- **Options are uppercase-canonicalised** with ordered `pairs` and last-write-wins `lookup`. `/UTILITIES`, `/CSV`, `/TABLE`, `/PROMPT-TEXT`, `/INSTANCE` are reliably accessible by key.
- **Bodies are byte-preserved** except for one leading + one trailing newline. SQL bodies still contain `/*BEGIN SQL*/` framing and `SQL_Get_CSV_List(...)` calls verbatim.
- **`SourceSpan` per block** is reliable for diagnostics.
- **Stage 1 diagnostics are already collected** in-band; Stage 2 must append, not replace.

### Stage 2 cannot assume (gaps to design around)
- **No subclassification within `UTILITY`** (Run_Python, Email, RoboCopy, raw `.bat` — all one kind). Stage 2 only needs the macro-control distinction Stage 1 already made; it should **not** further subclassify here either.
- **No SQL parse tree.** SQL bodies are opaque strings. `SQL_Get_CSV_List(...)` is found via text scan, not AST.
- **No CSV dependency graph yet.** Stage 1 records `/CSV=` (producer) and `/TABLE=` (consumer) as raw option values, but no producer→consumer linking has been done.
- **No typed `MACRO_CONTROL` payload.** The full utility string is in `options["UTILITIES"]`; Stage 2 must parse `{START-MACRO} "csv.csv" "N"` etc. itself.
- **No interpretation of `<<<NAME>>>` / `<<>>` placeholders anywhere.**
- **No path normalisation.** `.\foo.csv`, `foo.csv`, and `.\\foo.csv` all coexist in the wild — Stage 2 must canonicalise when matching producers to consumers.
- **`MALFORMED` is reserved but not currently emitted** (per Stage 1 summary). Stage 2 should still handle it defensively (skip + diagnostic).

---

## Step 2 — Real Pitfalls (grounded in `actual_script.txt` + `sql_script.txt`)

### High risk

**H1. Nested IF inside MACRO inside IF.**
Observed in `actual_script.txt`:
- outer `{IF-THEN} "CONFIG" "LE" "0"` (line 231) → `{ELSE}` (line 255) → many blocks → `{END-IF}` at line ~648.
- inside the ELSE branch: `{START-MACRO} "configsets.csv"` (line 415) … `{END-MACRO}` (line 626).
- inside that macro: two more `{IF-THEN}` / `{END-IF}` pairs (lines 427/470 and 481/524) plus a third (559/615).

Depth is up to 3 (IF > MACRO > IF). Naive position-based pairing breaks; a **stack-based pass** is required.

**H2. CSV dependency timing — producers may be external.**
- `{START-MACRO} "macrotmp.csv"` is preceded by a `WRITE_FILE` `/CSV=macrotmp.csv` — clean producer→consumer.
- `{START-MACRO} "ctime.csv"` (line 207) has **no visible producer** in the script; `ctime.csv` is created by `setsiteparam.exe` (an external `.exe` invoked via `/UTILITIES=`). The DataflowAnalyzer cannot prove this exists by static inspection. **Treat unknown producers as `info`, not `error`** — the script demonstrably works in production.

**H3. `SQL_Get_CSV_List` has two column-reference modes.**
- By column **name** (bare identifier): `SQL_Get_CSV_List(".\yeuchuan_a0_29397.tab", lot, "v1.lot In")` (`sql_script.txt:71`).
- By column **index** (quoted 1-based int): `SQL_Get_CSV_List(".\CSR_Server_OIS_Product_List.csv", "2", "p.prodgroup3 In")` (`actual_script.txt:762`).

A regex that captures one form silently misses the other. Stage 2 must accept both and tag the parsed call with `column_ref: str | int`.

**H4. Variable-producing macros are not the same as row-iterating macros.**
- `{START-MACRO} "configsets.csv"` → row-iterating frame; for each CSV row, every column becomes a named variable (`<<<STARTTS>>>`, `<<<UTC>>>`, etc.).
- `{ROWS-IN-FILE} "ICMPCS_config.csv" "CONFIG" "N"` → **non-iterating**; sets exactly one variable `CONFIG` to the row count of that file. There is no `{END-ROWS-IN-FILE}`; it is a one-shot statement, not a scope.
- `{IF-THEN}` condition operands sometimes wrap in `VAR(<<<CSRV>>>)` to force a variable lookup; sometimes use bare `"CONFIG"`.

Treating `{ROWS-IN-FILE}` as a scope opener would corrupt the scope tree. ScopeBuilder must distinguish.

### Medium risk

**M1. Path normalisation.** `.\macrotmp.csv` (consumer) vs `macrotmp.csv` (producer's `/CSV=` value) must match. Canonicalise to `PurePosixPath` form after stripping leading `.\` for matching purposes; preserve the original in the resolved node.

**M2. Macro variable case-folding.** Source CSV header is `Sfolder,underDEV,useCSR,useMMS`; placeholders are `<<<SFOLDER>>>`, `<<<UNDERDEV>>>`, `<<<USECSR>>>`, `<<<USEMMS>>>`. Resolution is **case-insensitive** on the name. Store names uppercase.

**M3. Macro scope leakage.** `<<<SFOLDER>>>` defined inside one `{START-MACRO}` must not be visible to a later sibling `{START-MACRO}`. Same name, two macros, different sources. Push/pop frames strictly on `{START-MACRO}` / `{END-MACRO}`.

**M4. Cross-block macro references inside SQL bodies.**
`actual_script.txt` SQLite block (line ~287+) references `<<<STARTTS>>>` etc. inside the SQL body. These placeholders are defined by an enclosing `{START-MACRO} "ctime.csv"` scope. The MacroResolver must walk SQL bodies (and WRITE_FILE bodies, and `/UTILITIES=` strings) for placeholders, not just option values.

### Low risk

**L1. Positional `<<>>` placeholders.** **Zero occurrences in any current fixture.** Design a cursor slot in the MacroFrame so this is not a future refactor, but **do not** invest in tests beyond one synthetic unit test. If a real fixture ever exercises it, expand then.

**L2. `MALFORMED` blocks.** Stage 1 reserves but does not emit this kind. Stage 2 skips them with a `warning` diagnostic.

**L3. SQL macro chunking.** `SQL_Get_CSV_List` in the original SPF emits chunked `IN (...) OR <leadin> IN (...)` output when lists exceed Oracle's 1000-element limit. **Stage 2 does not chunk** — it preserves the call as a structured node; the emitter (later stage) or its runtime helper decides chunking.

---

## Step 3 — Component Architecture

Three components, all in one package (`vg2c/resolver/`). They share one in-memory `ResolvedProgram`; each is a pure function over it.

### `ScopeBuilder`
- **Responsibility:** convert the flat `list[ClassifiedBlock]` into a `ScopeTree` by pairing `{START-MACRO}`/`{END-MACRO}` and `{IF-THEN}`/`{ELSE}`/`{END-IF}` using a parsing stack.
- **In → Out:** `list[ClassifiedBlock]` → `ScopeTree` (over the same block indices) + diagnostics.
- **Why it exists:** without correct nesting, MacroResolver can't bound variable lifetimes and the Emitter can't generate Python `with`/`if`/`for` blocks. Naive pairing fails on the nesting depth observed in `actual_script.txt`.

### `MacroResolver`
- **Responsibility:** parse `MACRO_CONTROL` block payloads into typed records (`{START-MACRO, csv_path, prompt_off}`, `{IF-THEN, lhs, op, rhs, conj, lhs2, op2, rhs2}`, `{ROWS-IN-FILE, file, var, …}`, `{ELSE}`, `{END-IF}`, `{END-MACRO}`), maintain a **MacroFrame stack** during a tree walk, and rewrite `<<<NAME>>>` placeholders found in option values, bodies, and utility strings to **resolved-symbolic** form.
- **In → Out:** `ScopeTree` + leaf `ClassifiedBlock`s → same tree with `ResolvedBlock` leaves whose options/body carry resolved placeholders (or tagged unresolved markers + diagnostics).
- **Why it exists:** placeholder resolution is scope-dependent and cannot be done block-locally. Row-iterating frames (`{START-MACRO} "foo.csv"`) cannot be expanded at compile time — values only exist at runtime — so the resolver leaves them as `RuntimeMacroRef(name, frame_id)` nodes. Static frames (no CSV) and `{ROWS-IN-FILE}`-assigned variables can be resolved fully at compile time **iff** the producing CSV is statically known (rare; treat as runtime by default).

> **Design call:** v1 resolves **structurally** but not **value-substantively**. The resolver tags every placeholder with the frame it belongs to and the row column to read at runtime; it does not pre-read CSVs. This avoids the trap of half-resolving values (some at compile, some at runtime) and keeps emitted Python self-documenting. The cost: every `<<<X>>>` becomes a runtime lookup. Worth it for simplicity and auditability.

### `SqlMacroExpander`
- **Responsibility:** scan SQL bodies of `MARS_READ`, `OASYS_READ`, `ARIES_READ`, and `SQLITE_QUERY` blocks for known SPF SQL macros (v1: `SQL_Get_CSV_List` only), parse each invocation into a typed `SqlMacroCall` record, and replace the call site with a tagged placeholder (`@@SQLMACRO:n@@`) the emitter will fill in.
- **In → Out:** `ResolvedBlock` with SQL body string → `ResolvedBlock` with rewritten SQL body + `sql_macro_calls: list[SqlMacroCall]` side table.
- **Why it exists:** SQL_Get_CSV_List output depends on a CSV that may only exist at runtime; we can't expand to literal SQL at compile time. Capturing it as structured data lets the validator check the dataflow (producer must exist somewhere) and the emitter generate a clean `helpers.sql_get_csv_list(...)` call.

> **Pushback on the prompt's "Read values… Convert into IN (...)":** the expander **must not** read CSVs or emit `IN (...)` strings at Stage 2. CSVs may not exist; emitting a literal `IN(...)` baked at compile time would freeze data values into the generated script and lose any later updates. Stage 2 captures the call shape; runtime helpers do the actual list construction.

### Why not split further (e.g. separate `DataflowAnalyzer`)
- Producer/consumer linking is two passes over the block list (~30 lines). Hoisting it to its own component buys nothing until a Validator stage needs it. **Inline it into MacroResolver** for v1, with a small `csv_producers: dict[str, int]` field on `ResolvedProgram` so a future Validator can read it without re-walking.

---

## Step 4 — Data Model Evolution

All new types in `vg2c/resolver/models.py`. All frozen dataclasses.

### `MacroFrame`
- `kind: Literal["row-iter", "if", "static-vars"]`
- `csv_path: str | None` — for row-iter frames
- `csv_headers: tuple[str, ...] | None` — populated **only if** the producer's `/HEADERS=` is statically known; else `None` and the runtime helper discovers headers at iteration time
- `named_vars: dict[str, str]` — for `{ROWS-IN-FILE}`-style assignments resolvable at compile time; **case-folded uppercase** keys
- `positional_cursor: int` — reserved, unused in fixtures (per L1)
- `source_span: SourceSpan` — for diagnostics

### `ScopeNode`
- `kind: Literal["program", "macro", "if-branch", "else-branch", "leaf"]`
- `start_index: int`, `end_index: int` — block indices spanned (inclusive)
- `children: tuple[ScopeNode, ...]`
- `block_index: int | None` — only for `leaf`
- `control_payload: MacroControlPayload | None` — typed parsed version of the `{...}` token; None for leaves

The tree's leaves index into the flat block list; non-leaf nodes own no block content directly. This keeps source order trivially recoverable (in-order traversal of leaves == original list).

### `MacroControlPayload` (sum type)
- `StartMacro(csv_path: str, prompt_off: bool)`
- `EndMacro()`
- `IfThen(lhs: str, op: str, rhs: str, conj: str | None, lhs2: str | None, op2: str | None, rhs2: str | None)`
- `Else()`
- `EndIf()`
- `RowsInFile(csv_path: str, var_name: str, prompt_off: bool)` — note: **not a scope opener**, ScopeBuilder treats it as a leaf with side effects

### `SqlMacroCall`
- `name: Literal["SQL_Get_CSV_List"]` (extensible; v1 supports one)
- `csv_path: str`
- `column_ref: int | str` — int for `"2"`-style, str for `lot`-style
- `lead_in: str` — the third argument (the SQL prefix the runtime repeats per chunk)
- `placeholder: str` — the `@@SQLMACRO:n@@` token inserted in the SQL body
- `source_span: SourceSpan` (line-anchored inside the body)

### `ResolvedBlock`
- `parsed: ParsedBlock` (passthrough)
- `kind: Kind` (passthrough)
- `resolved_options: BlockOptions` — placeholder-rewritten if statically resolvable, otherwise unchanged + diagnostic
- `resolved_body: str` — same treatment
- `sql_macro_calls: tuple[SqlMacroCall, ...]` — empty for non-SQL kinds
- `runtime_macro_refs: tuple[RuntimeMacroRef, ...]` — every `<<<NAME>>>` that requires runtime resolution, with its scope-frame id
- `control_payload: MacroControlPayload | None` — for `MACRO_CONTROL` blocks
- `scope_id: int` — id of the deepest enclosing scope (for emitter convenience)

### `RuntimeMacroRef`
- `name: str` (uppercase)
- `frame_id: int` — id of the resolving frame; `-1` if unresolved
- `location: Literal["option-value", "body", "utility-string"]`
- `option_key: str | None`
- `source_span: SourceSpan`

### `ResolvedProgram`
- `blocks: tuple[ResolvedBlock, ...]` — flat, source-ordered
- `scope_tree: ScopeNode` — root is `program`
- `csv_producers: Mapping[str, int]` — normalised csv path → producing block index
- `csv_consumers: Mapping[str, tuple[int, ...]]` — normalised csv path → consuming block indices
- `diagnostics: tuple[Diagnostic, ...]` — Stage 1 + Stage 2 merged

### Why Stage 1 structures are insufficient
- `ParsedBlock` / `ClassifiedBlock` are flat and untyped beyond `Kind`. The Emitter (Stage 3) cannot generate nested Python from a flat list, cannot know which `<<<X>>>` belongs to which loop, and cannot stub `SQL_Get_CSV_List` without structured call data.
- Stage 1's `BlockOptions` doesn't distinguish raw vs resolved values. Stage 2 needs to track that distinction so the Emitter can choose `"literal"` vs `ctx.macro.named("X")`.

---

## Step 5 — Pipeline Integration

```
[Stage 1]                [Stage 2]                          [Stage 3 future]
parse + classify   →     resolve                       →    emit
list[ClassifiedBlock]    ResolvedProgram                    Python source

                         + ScopeBuilder
                         + MacroResolver  (incl. dataflow links)
                         + SqlMacroExpander
```

**What Stage 2 consumes from Stage 1:**
- `list[ClassifiedBlock]`
- The existing `list[Diagnostic]` (appended to, not replaced).

**What Stage 3 (Emitter) will expect from Stage 2:**
- `ResolvedProgram.scope_tree` to drive nested Python (`with`, `if`, `for`) generation.
- `ResolvedProgram.blocks` in source order for per-block code generation.
- `ResolvedBlock.runtime_macro_refs` → emitted as `ctx.macro.named("X")` / `ctx.macro.positional(i)` calls.
- `ResolvedBlock.sql_macro_calls` → emitted as `helpers.sql_get_csv_list(ctx, path, col, lead_in)` calls; the placeholder in `resolved_body` tells the emitter where to splice the result.
- `ResolvedProgram.csv_producers/consumers` → Emitter uses producer block indices to confirm execution order, and to route a CSV from one helper's return value to the next helper's argument when feasible.
- A single merged `diagnostics` list with severities; the Emitter can choose to bail in `--strict` mode (out of scope for Stage 2).

---

## Step 6 — Testing Strategy

Layered, tight, no overfitting to exact counts.

### Unit tests — `tests/resolver/test_scope_builder.py`
- Empty program → root `program` node with no children.
- Single `{START-MACRO}/{END-MACRO}` pair around three leaves → one macro node with three leaf children.
- `{IF-THEN}/{ELSE}/{END-IF}` → one `if` node with two branch children (`if-branch`, `else-branch`).
- `{IF-THEN}/{END-IF}` (no ELSE) → one `if` node with a single `if-branch` child.
- Nested IF inside MACRO (the actual_script pattern) → tree depth ≥ 3, correct ordering.
- Unbalanced `{END-MACRO}` with no opener → `error` diagnostic `"orphan-end-macro"`, parsing continues.
- Unbalanced `{START-MACRO}` with no closer → `error` diagnostic `"unclosed-macro"`, implicit close at end of program.
- Mis-ordered `{ELSE}` (no preceding `{IF-THEN}` in the same parent) → `error` `"orphan-else"`.
- `{ROWS-IN-FILE}` token is NOT treated as a scope opener (it remains a leaf).

### Unit tests — `tests/resolver/test_macro_resolver.py`
- Named placeholder in option value resolves to a `RuntimeMacroRef` tagged with the enclosing frame id.
- Same placeholder name in two sibling macros → two distinct frame ids (scope isolation, M3).
- Case-insensitive resolution: `<<<sfolder>>>`, `<<<SFOLDER>>>`, `<<<SFolder>>>` all bind to frame's `SFOLDER`.
- `{ROWS-IN-FILE}` produces a `RowsInFile` payload but **does NOT** push a frame.
- Placeholder outside any frame → `warning` diagnostic `"unbound-macro-var"`, ref kept with `frame_id=-1`.
- One synthetic positional `<<>>` test (only) — verifies the cursor field exists and resolves.

### Unit tests — `tests/resolver/test_sql_macro_expander.py`
- Column-by-name form parses correctly (`SQL_Get_CSV_List(".\f.tab", lot, "v1.lot In")` → `column_ref="lot"`).
- Column-by-index form parses correctly (`SQL_Get_CSV_List(".\f.csv", "2", "p.prodgroup3 In")` → `column_ref=2`).
- Lead-in containing embedded quotes is preserved verbatim.
- Two calls in one SQL body → two `SqlMacroCall` entries, two distinct placeholders, both round-trip-replaceable.
- Unknown SQL macro name (e.g. `SQL_Time_Range(...)`) → left untouched + `info` diagnostic `"unknown-sql-macro"` (no crash).
- Producer CSV not found in `csv_producers` → `info` diagnostic `"sql-macro-csv-unknown-producer"`, call still recorded.

### Edge case tests
- Nested macros (depth ≥ 3) — covered above.
- Missing `{END-MACRO}` / `{END-IF}` — covered above.
- Empty placeholder `<<<>>>` → `warning` `"empty-macro-name"`, treated as literal.
- `<<<NAME` (unclosed) and `NAME>>>` (no opener) → preserved verbatim, no diagnostic (too noisy; these occur in HTML/JS bodies legitimately).
- `SQL_Get_CSV_List` with three arguments having quoted commas inside — pick the parsing approach (paren-balanced scan, not split-on-comma) and test it.
- `<<>>` followed by `<<>>` in the same block — cursor advances; both resolve.

### End-to-end fixture tests — `tests/resolver/test_fixtures.py`
Parameterised over the five Stage 1 fixtures. For each:
- `resolve(classify(parse(text)))` completes without raised exceptions.
- `ResolvedProgram.scope_tree` is well-formed: every `MACRO_CONTROL` block index appears as either a scope opener/closer in the tree or as a `RowsInFile` leaf.
- No `error`-severity diagnostics on `script_short.txt`, `script_another.txt`, `sql_script.txt`, `script_from_vietnam.txt`.

Per-fixture spot checks (presence-based, not count-based):

- **`script_short.txt`** — no scope nodes (flat); zero `runtime_macro_refs`.
- **`sql_script.txt`** — exactly one `SqlMacroCall` for `SQL_Get_CSV_List` with `csv_path` ending in `yeuchuan_a0_29397.tab` and `column_ref == "lot"`.
- **`script_another.txt`** / **`script_from_vietnam.txt`** — `csv_producers` includes `calendar_ref.csv` produced by a `WRITE_FILE` block whose index is less than the consumer's index.
- **`actual_script.txt`** — the demanding one:
  - At least 2 `macro` nodes and at least 5 `if` nodes (rough lower bounds from the grep above; if these tighten with future fixture growth, raise the bounds, don't lower).
  - Scope tree depth ≥ 3 somewhere (IF inside MACRO inside IF).
  - Every `<<<SFOLDER>>>`, `<<<UNDERDEV>>>`, `<<<USECSR>>>`, `<<<USEMMS>>>`, `<<<STARTTS>>>`, `<<<CSRPATH>>>`, `<<<MMSPATH>>>`, `<<<CSRV>>>`, `<<<MMSV>>>` placeholder in any body or option value has a non-(-1) `frame_id`.
  - `SQL_Get_CSV_List` calls detected at the SQL bodies that contain them (≥4, per grep); both column-by-name and column-by-index forms appear in the parsed payloads.
  - `ctime.csv` appears in `csv_consumers` without a matching `csv_producers` entry → exactly one `info` diagnostic (`"unknown-csv-producer"`) for it, **not** an error.

### What is deliberately not tested
- Performance (parsing+resolving all five fixtures should still be sub-second; if it isn't, the implementation is wrong).
- Round-trip equality on the original `raw` text (Stage 1 already covers that for the parser; Stage 2 doesn't touch it).
- Cross-stage emitter behaviour (Stage 3).

---

## Step 7 — Non-Goals for Stage 2

- **No Python code generation.** That is Stage 3.
- **No DataSyncX integration.** Stage 3, optionally Stage 4.
- **No View Registry expansion.** SPF logical-view (F_*, P_*) expansion stays as a later resolver pass when a YAML registry exists.
- **No utility execution mapping.** `Run_Python_Script.va`, `RoboCopy.va`, `SQLPathFinder_Email.va` remain `UTILITY` blocks with raw utility strings; the per-utility handler dispatch is Stage 3.
- **No SQL parsing.** SQL bodies are scanned for macro calls textually; no AST.
- **No CSV reading.** Producers/consumers are linked by path, never opened.
- **No agentic AI.** Same constraint as Stage 1.
- **No new `Kind`s.** If a `{NEW-TOKEN}` appears that ScopeBuilder cannot pair, it becomes a leaf with `warning` `"unknown-macro-control"`.
- **No `--strict` policy layer.** Stage 2 returns diagnostics; the caller (eventually the CLI) decides.

---

## Step 8 — Simplicity Check

### Intentionally not implemented
- Standalone `DataflowAnalyzer` (folded into MacroResolver as ~30 lines).
- Standalone `Validator` (kept for a later stage; v1 surfaces diagnostics but does no semantic gating).
- Pre-reading CSVs for value substitution (kept fully runtime; see the H2/H3/L3 rationale).
- SPF SQL macros beyond `SQL_Get_CSV_List` (others added on first sighting).
- Positional `<<>>` resolution beyond a scaffold + one unit test.

### Most likely future complexity
- **`SQL_Get_CSV_List` argument parsing.** The third argument is a free-form SQL prefix that can contain quotes, commas, and parens. Using `split(",")` will break. **Use a small paren-balanced character-by-character scanner** to extract three arguments; this is ~40 lines and avoids the regex trap entirely. It is the single most likely place for a Stage 2 regression.
- **Scope tree on malformed input.** Real scripts in flight may produce unbalanced control tokens. The stack-based pairing must be defensive (implicit close at EOF, orphan-end-token tolerated) and every recovery path needs a diagnostic.

### Minimal working Stage 2 (first commit path)
1. Create `src/vg2c/resolver/models.py` with `MacroFrame`, `ScopeNode`, `MacroControlPayload` variants, `SqlMacroCall`, `RuntimeMacroRef`, `ResolvedBlock`, `ResolvedProgram`. ~150 lines, no logic.
2. Create `src/vg2c/resolver/scope_builder.py` implementing the stack-based pairing pass. No macro/SQL handling here.
3. Create `src/vg2c/resolver/macro_resolver.py` implementing:
   (a) parse `MACRO_CONTROL` payloads (one function per token kind),
   (b) MacroFrame push/pop on tree walk,
   (c) `<<<NAME>>>` scan over options + bodies + utility strings,
   (d) inline producer/consumer CSV link.
4. Create `src/vg2c/resolver/sql_macro_expander.py` implementing the paren-balanced parser for `SQL_Get_CSV_List` and placeholder rewriting.
5. Create `src/vg2c/resolver/__init__.py` exporting `resolve(blocks) -> ResolvedProgram`.
6. Write the unit and end-to-end tests from Step 6.
7. Run `pytest`; iterate until green; commit.

Estimated effort: comparable to Stage 1, possibly slightly larger because the SQL macro parser earns ~50 lines and the tree walker ~60.

---

*End of Stage 2 plan. Implementation proceeds only after this plan is approved.*
