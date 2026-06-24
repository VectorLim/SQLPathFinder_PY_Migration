# Stage 3 Plan — Dataflow Analyzer

Audience: the coding agent (or human) about to implement Stage 3.
Source of truth for prior stages: [progress/stage1_parser_classifier.md](progress/stage1_parser_classifier.md), [progress/stage2_resolver_macro_sql.md](progress/stage2_resolver_macro_sql.md).
Scope: take `ResolvedProgram` (Stage 2) and produce an `AnalyzedProgram` carrying a per-path dataflow graph, plus targeted diagnostics about ordering, scope, overwrites, and orphans.

Anchors carried forward (unchanged): deterministic, diagnostics-first, no AI, no code emission, no file I/O, no SQL parsing, minimal new components.

---

## Step 1 — What Stage 2 Already Provides / What Is Missing

### Stage 3 can rely on (verified from `src/vg2c/resolver/`)
- `ResolvedProgram.blocks` — source-ordered, dense indices, each with `scope_id`.
- `ResolvedProgram.scope_tree` — `program / macro / if / if-branch / else-branch / leaf` nodes with `start_index`, `end_index`, and `scope_id`.
- `ResolvedProgram.csv_producers: Mapping[str, int]` — single producer per normalised path (first writer wins via `setdefault`).
- `ResolvedProgram.csv_consumers: Mapping[str, tuple[int, ...]]` — every consumer index per normalised path. Covers `/TABLE=`, `{START-MACRO}` csv, `{ROWS-IN-FILE}` csv, and `SqlMacroCall.csv_path`.
- Normalised path form: lowercase, forward-slashed, leading `./` stripped (`_normalize_csv_path`). Stage 3 uses the same helper.
- Stage 2's `unknown-csv-producer` warning is already emitted for any consumer path missing from `csv_producers`.

### Gaps Stage 3 must close
- **No multi-producer record.** `csv_producers` is single-valued. Multiple producers for the same path (legitimate in mutually exclusive branches, accidental otherwise) are silently collapsed.
- **No ordering check.** A consumer with index < producer index is not flagged.
- **No scope-awareness.** Producer inside an `{IF-THEN}` branch, consumer after `{END-IF}` is not detected. Producer inside `{START-MACRO}` loop, consumer outside the loop is not detected.
- **No external producer hypothesis.** Files created by `UTILITY` blocks (RoboCopy.va, getcsrsu.bat, setsiteparam.exe, etc.) are uniformly flagged as `unknown-csv-producer` regardless of whether a plausible upstream utility ran.
- **No "unused output" signal.** A `/CSV=` produced and never consumed is silent.
- **No producer-kind taxonomy.** The Emitter (Stage 4) will need to know whether the producer is a `WRITE_FILE`, a DB read, a SQLite query, or presumed-external — Stage 2 doesn't tag this.

These are the things Stage 3 adds. Nothing else.

---

## Step 2 — Real Pitfalls (grounded in `actual_script.txt` + `sql_script.txt`)

### High risk

**H1. Scope-crossing CSV usage inside mutually exclusive branches.**
Observed pattern: `actual_script.txt` has `HIST.csv` written by `WRITE_FILE` at line 583 (inside the `{IF-THEN} "HIST" "LE" "0"` true-branch dummy fallback) and *also* by an outer flow continuing into the `{ELSE}` branch. Both paths target the same name; only one runs at runtime. Treating them as duplicate producers and emitting a warning would be wrong. **Branch-exclusive multi-production is legitimate; only same-branch overwrite is a smell.**

**H2. Consumer outside the producing scope.**
If `WRITE_FILE` `/CSV=foo.csv` lives inside `{IF-THEN} ... {END-IF}` and a later block consumes `foo.csv` outside the IF, the file may not exist at runtime. This is a real correctness risk (e.g. emitter would generate `pd.read_csv('foo.csv')` against a missing file).

**H3. External-utility-produced CSVs.**
- `ctime.csv` (consumed by `{START-MACRO}` at line 207) — produced by an earlier utility (`setsiteparam.exe`) with no `/CSV=` declaration.
- `HIST.txt` (consumed by `{ROWS-IN-FILE}` at line 547) — produced by a preceding `RoboCopy.va` utility.
- `ICMPCS_config.csv` — externally produced, consumed by `{ROWS-IN-FILE}` at line 219.

Flat `unknown-csv-producer` is too noisy because it fires for every external-utility output. Stage 3 needs a **heuristic** to soften the diagnostic when a UTILITY block plausibly produced the file (preceding in the same scope or an ancestor scope, with no intervening `{END-MACRO}`/`{END-IF}` that would have flushed it out of scope).

### Medium risk

**M1. Producer-as-consumer chains.**
Some SQL_FETCH blocks read `/TABLE=` AND write `/CSV=` (rare but present in SQLite combine blocks: `actual_script.txt` line ~1344 — `/TABLE=yeuchuan_SQL_15507.tab,yeuchuan_a1_15507.tab` and `/CSV=...`). Stage 3 must allow a single block to appear on both sides of an edge without paradox.

**M2. Multiple consumers, source order matters.**
`SQL_Get_CSV_List(".\yeuchuan_a0_15507.tab", lot, ...)` and `SQL_Get_CSV_List(".\yeuchuan_a0_15507.tab", operation, ...)` appear on the same line in `actual_script.txt:960-963`. Both reference the same CSV. Stage 2 already records both consumer entries; Stage 3 must preserve that and not deduplicate by block index.

**M3. Comma-separated `/TABLE=` values.**
`/TABLE=yeuchuan_SQL_15507.tab,yeuchuan_a1_15507.tab` is a single option holding two consumed CSVs. **Stage 2's `_collect_csv_consumers` currently records the whole string as one path** (see `macro_resolver.py:368-370`). This is a latent Stage 2 bug that Stage 3 surfaces — Stage 3 must split on commas when reading the `/TABLE=` option value during its own consumer enrichment, OR Stage 2 must be patched. **Recommendation: patch Stage 2's `_collect_csv_consumers` as a Stage 3 prerequisite (single-line change), do not paper over it in Stage 3.** Flagged as a Stage 2 fix in the implementation issues file.

### Low risk

**L1. Cycle detection.** VG2 is strictly sequential by source order. A cycle (consumer-before-producer-before-consumer for the same CSV) is impossible by construction. **Skip cycle detection entirely.** What Stage 3 *does* detect is "consumer index < producer index" — but for the single-producer-per-path case, with `setdefault` semantics, that should be flagged as a warning, not as a cycle.

**L2. Path collisions across normalised forms.** `.\foo.csv` and `foo.csv` already normalise to the same key via Stage 2's helper. Trust it.

**L3. Inline Python / bat output detection.** A WRITE_FILE block with body `pd.read_csv('foo'); df.to_csv('bar.csv')` produces `bar.csv` at runtime. Detecting this requires parsing Python/bat bodies, which Stage 2 explicitly excluded. **Stage 3 stays out of body parsing.** Any CSV produced by an inline script is treated as "external-presumed" via the UTILITY-precedence heuristic, with an `info` diagnostic.

---

## Step 3 — Component Architecture

One new component. One new package. No registries, no protocols.

### `DataflowAnalyzer` (`src/vg2c/analyzer/dataflow.py`)
- **Responsibility:** one pass over `ResolvedProgram` that builds a per-path edge list (producer block index → consumer block indices), classifies each producer's kind, attaches the deepest enclosing `scope_id` to each producer/consumer, computes scope-relationship flags per edge, and emits dataflow diagnostics.
- **In → Out:** `ResolvedProgram` → `AnalyzedProgram`.
- **Why it exists:** Stage 2's `csv_producers`/`csv_consumers` are flat maps with no kind, scope, or ordering context. Without Stage 3, the Emitter would re-derive all of this (and get it wrong) every time it generated a step.

### `ScopeRelations` (small internal helper in the same module)
- **Responsibility:** answer two questions cheaply: `is_ancestor(scope_a, scope_b)` and `lca(scope_a, scope_b)` over the `ScopeTree`.
- **Implementation:** precompute `parent_of: dict[int, int | None]` and `depth_of: dict[int, int]` in one tree walk; classic two-pointer LCA.
- **Why it exists:** scope-crossing detection needs many ancestor/LCA queries; precomputing once is O(N), the alternative is repeated tree walks.

**Not introduced** (and why):
- No standalone `Graph` class with vertices/edges — a `tuple[DataflowEdge, ...]` keyed by `csv_path` is enough.
- No topological-sort utility — VG2 is already source-ordered; Stage 3 verifies that order, doesn't reconstruct it.
- No producer/consumer "registries" beyond what's already in `ResolvedProgram` — Stage 3 reads them, augments them once, returns one merged object.

---

## Step 4 — Data Model Evolution

All new types in `src/vg2c/analyzer/models.py`. Frozen dataclasses, slots, no logic.

### `ProducerKind` (`Literal`)
- `"write-file"` — `Kind.WRITE_FILE`
- `"db-read"` — `Kind.MARS_READ | Kind.OASYS_READ | Kind.ARIES_READ`
- `"sqlite-query"` — `Kind.SQLITE_QUERY`
- `"external-presumed"` — `Kind.UTILITY` block with a plausible filename signature (heuristic, see §H3)
- `"unknown"` — fallback

### `ConsumerKind` (`Literal`)
- `"table"` — `/TABLE=` reference
- `"start-macro"` — `{START-MACRO}` csv_path
- `"rows-in-file"` — `{ROWS-IN-FILE}` csv_path
- `"sql-macro"` — `SqlMacroCall.csv_path`

### `ProducerRecord`
- `block_index: int`
- `csv_path: str` (normalised)
- `scope_id: int`
- `producer_kind: ProducerKind`
- `is_conditional: bool` — true if any ancestor scope is `if-branch` or `else-branch`
- `is_in_loop: bool` — true if any ancestor scope is `macro` (row-iter)

### `ConsumerRecord`
- `block_index: int`
- `csv_path: str`
- `scope_id: int`
- `consumer_kind: ConsumerKind`

### `DataflowEdge`
- `csv_path: str`
- `producer: ProducerRecord | None` — `None` for unresolved consumers
- `consumer: ConsumerRecord`
- `scope_relation: Literal["same-scope", "consumer-deeper", "producer-deeper-loop", "producer-in-other-branch", "no-producer"]`
- `order_ok: bool` — `consumer.block_index > producer.block_index` (true when `producer is None` by convention; Stage 3 separately flags the missing producer)

> **Why not extend `ResolvedBlock`?** `ResolvedBlock` is frozen; rebuilding the tuple to inject one annotation per block adds churn for no gain. Side-tables on `AnalyzedProgram` are accessed by index when the Emitter wants them.

### `AnalyzedProgram`
- `resolved: ResolvedProgram` — pass-through reference
- `producers: tuple[ProducerRecord, ...]` — all producers (multiple per path allowed)
- `producers_by_path: Mapping[str, tuple[ProducerRecord, ...]]` — convenience index
- `consumers: tuple[ConsumerRecord, ...]`
- `edges: tuple[DataflowEdge, ...]` — one per (consumer, chosen producer) pair
- `unused_producers: tuple[ProducerRecord, ...]` — producers with no consumer for that path
- `diagnostics: tuple[Diagnostic, ...]` — Stage 1 + 2 + 3 merged

### New diagnostic codes
- `dataflow-order-violation` (warning) — `consumer.block_index < producer.block_index`.
- `dataflow-overwrite-same-scope` (info) — two producers for same path with overlapping (non-branch-exclusive) scopes.
- `dataflow-branch-exclusive-producers` (info, optional) — two producers in mutually exclusive `if-branch`/`else-branch` siblings; this is informational, not a warning, because it's idiomatic.
- `dataflow-scope-crossing-branch` (warning) — producer inside an `if-branch`/`else-branch`, consumer outside that branch's subtree.
- `dataflow-scope-crossing-loop` (info) — producer inside a `macro` loop, consumer outside (last-write-wins semantics; usually intentional).
- `dataflow-likely-external-producer` (info) — consumer has no `/CSV=` producer, but a `UTILITY` block precedes it in the same scope chain. Pairs with (does **not** replace) Stage 2's `unknown-csv-producer`.
- `dataflow-unused-output` (info) — `/CSV=` written, never consumed by any structural reference.

> **Pushback on cycle detection from the prompt.** VG2 is sequential; cycles are structurally impossible. The prompt's "cyclical dependencies (if possible)" should be answered with: **not possible in v1**, no code, no test. If a future syntax allows loops with back-edges, revisit.

---

## Step 5 — Pipeline Integration

```
[Stage 1]                [Stage 2]                  [Stage 3]                       [Stage 4 future]
parse + classify   →     resolve              →     analyze                    →    emit
list[ClassifiedBlock]    ResolvedProgram            AnalyzedProgram                 Python source
                         (+ csv_producers,          (+ producers w/ kind+scope,
                          csv_consumers,             edges, scope_relation,
                          scope_tree)                ordered/conditional flags,
                                                    dataflow diagnostics)
```

**Stage 3 consumes from Stage 2:** the whole `ResolvedProgram` object. No re-parse. No re-walk of blocks beyond the analyzer's single pass.

**Stage 4 (Emitter) will rely on Stage 3 for:**
- Per-edge `scope_relation` to decide where to declare CSV-bearing variables (inside `with` blocks vs above them).
- `producer_kind` to choose the right code template (write-file → `Path.write_text` / inline literal; db-read → `Task` + `Reader`; sqlite-query → runtime `sqlite_engine.run_join`).
- `unused_producers` to suppress unused-variable noise in generated Python.
- `dataflow-*` diagnostics surfaced via the CLI's eventual `--strict` mode.

**Pre-Stage-3 patch required:** fix Stage 2's `_collect_csv_consumers` to split comma-separated `/TABLE=` values. Trivial one-liner; without it, Stage 3 will mis-link `actual_script.txt` line ~1344's multi-table consumer. Document this in `progress/stage3_implementation_issues.md` when raised.

---

## Step 6 — Testing Strategy

Layered, tight. Reuse Stage 1+2 fixtures; no new fixture files.

### Unit tests — `tests/analyzer/test_producers.py`
- A `WRITE_FILE` block with `/CSV=foo.csv` → one `ProducerRecord` with `producer_kind="write-file"`.
- A `MARS_READ` block with `/CSV=bar.csv` → `producer_kind="db-read"`.
- A `SQLITE_QUERY` with `/CSV=baz.csv` and a `/TABLE=` → recorded as both producer (of baz) and consumer (of the table).
- A `UTILITY` block whose `/UTILITIES=` mentions `*.csv` literal preceding an unresolved consumer in the same scope → `producer_kind="external-presumed"` attached to the utility, `dataflow-likely-external-producer` info diagnostic emitted.
- Two `WRITE_FILE` blocks producing the same path in the same scope → two `ProducerRecord` entries; `dataflow-overwrite-same-scope` info diagnostic.
- Two `WRITE_FILE` producers in `if-branch` vs `else-branch` siblings → two records; `dataflow-branch-exclusive-producers` info, no warning.

### Unit tests — `tests/analyzer/test_edges.py`
- Producer-before-consumer in the same scope → edge with `scope_relation="same-scope"`, `order_ok=True`, no diagnostic.
- Producer-after-consumer (synthetic) → `order_ok=False`, `dataflow-order-violation` warning.
- Producer inside `if-branch`, consumer outside the `if` subtree → `scope_relation="producer-in-other-branch"`, `dataflow-scope-crossing-branch` warning.
- Producer inside `macro` loop, consumer outside the loop → `scope_relation="producer-deeper-loop"`, `dataflow-scope-crossing-loop` info.
- Consumer with no producer entry → edge with `producer=None`, `scope_relation="no-producer"`. Pairs with Stage 2's existing `unknown-csv-producer`.

### Unit tests — `tests/analyzer/test_unused.py`
- A `/CSV=` produced but referenced by no `/TABLE=`, no `{START-MACRO}`, no `{ROWS-IN-FILE}`, no `SqlMacroCall` → present in `unused_producers`; `dataflow-unused-output` info.
- An "intermediate" CSV (consumed by exactly one block and never again) → still considered used; no diagnostic.

### Edge case tests
- Same CSV name produced and re-produced across a `macro` scope boundary → exactly one producer per `ProducerRecord`, both tagged `is_in_loop` per their scope.
- Producer-as-consumer (single block consuming `/TABLE=a` and producing `/CSV=b`) → appears in `producers` and `consumers` lists; no self-edge synthesized.
- Comma-split `/TABLE=a.tab,b.tab` (after Stage 2 patch) → two consumer records, two edges if producers exist.
- `SqlMacroCall.csv_path` matches a normalised `WRITE_FILE` `/CSV=` value → edge created with `consumer_kind="sql-macro"`.

### End-to-end fixture tests — `tests/analyzer/test_fixtures.py`
Parameterised over the five fixtures. For each:
- Pipeline `parse → classify → resolve → analyze` runs without crashing.
- `AnalyzedProgram.edges` is non-empty for fixtures with cross-block CSVs (`script_another`, `sql_script`, `script_from_vietnam`, `actual_script`); may be empty for `script_short` (single block).
- No `error`-severity diagnostics on the four clean fixtures.

Per-fixture spot checks (presence-based):

- **`script_another.txt`**: `calendar_ref.csv` has at least one producer (`db-read`) and at least one consumer (`sql-macro` or implicit — but the consumer is in inline Python, so likely **none structurally**; this fixture should produce a `dataflow-unused-output` info). This validates the "structural-only" boundary holds.
- **`sql_script.txt`**: `yeuchuan_a0_29397.tab` (lower-cased) has one `db-read` producer (the MARS block) and one `sql-macro` consumer (the OASYS block's `SQL_Get_CSV_List`). Edge `scope_relation="same-scope"`, `order_ok=True`.
- **`actual_script.txt`** (the demanding one):
  - At least one `dataflow-scope-crossing-branch` warning — `HIST.csv` written in `if-branch` and the analyzer notes that downstream consumers depend on it being created in either branch.
  - At least one `dataflow-likely-external-producer` info — for `ctime.csv` or `ICMPCS_config.csv`, paired with the existing Stage 2 `unknown-csv-producer`.
  - At least two `branch-exclusive-producers` info entries — fixture has multiple "dummy file in one branch, real file in the other" patterns.
  - Every `sql-macro` consumer has a non-`None` producer where the path matches an earlier `/CSV=` block (i.e., the four `SQL_Get_CSV_List` calls in `actual_script` are correctly linked).

### What is deliberately not tested
- Exact diagnostic counts (fixtures grow; counts brittle). Test for presence and severity bucket.
- Cycle detection (impossible by construction).
- Inline Python body output (out of scope).
- Performance (whole pipeline should still be sub-second; if it isn't, the impl is wrong).

---

## Step 7 — Non-Goals for Stage 3

- **No View Registry expansion.** SPF logical view (`F_*`/`P_*`) lowering remains future work.
- **No Python code generation.** Stage 4.
- **No file I/O.** Analyzer never opens a CSV, never resolves a Windows path, never touches `pathlib` beyond `PurePosixPath` (already used by Stage 2's normaliser).
- **No re-resolution of macros.** Trust Stage 2's `RuntimeMacroRef` results.
- **No SQL AST parsing.** Trust Stage 2's `SqlMacroCall.csv_path` and `column_ref`.
- **No DataSyncX integration.**
- **No agentic AI.**
- **No Stage 2 redefinition.** The single recommended patch (comma-split `/TABLE=`) is a targeted one-liner, not a Stage 2 redesign.
- **No new `Kind` values.** Producer/consumer kinds live in Stage 3's enum; Stage 1's `Kind` is untouched.
- **No CLI surfacing of new diagnostics.** Stage 3 returns them in the merged tuple; how a caller presents them is a later concern.

---

## Step 8 — Simplicity Check

### Intentionally not implemented
- Generic graph algorithms (BFS/DFS/toposort/cycle detection). The structure is already linear and labelled; the only graph operations Stage 3 performs are `edges-by-path` lookups and per-edge scope-relation classification.
- A `Validator` stage. Stage 3 emits diagnostics; gating is a CLI concern. Splitting validation out would be premature.
- Body parsing for inline-script-produced files. Treated as external-presumed; one diagnostic, no parser.
- Cycle detection. Structurally impossible.
- Editable producer/consumer tables — `AnalyzedProgram` is immutable like its predecessors.

### Most likely future complexity
- **External-producer heuristic.** "UTILITY block precedes consumer in same scope" is the cheap version. It will misfire on scripts where the producer is two scope levels up, or where multiple utilities ran. **Resist the urge to make it smarter in v1.** When a real fixture proves the heuristic wrong, add one more rule, not a framework.
- **Scope-relation taxonomy growth.** Today: five values. If a future construct (e.g. nested loop with `continue`-like semantics) needs another, add it; do not pre-invent.
- **Multi-producer reconciliation when emitting.** Stage 4's job, not Stage 3's. Stage 3 hands the emitter all producers; the emitter chooses how to render them.

### Minimum viable Stage 3 (first commit path)
1. Patch `src/vg2c/resolver/macro_resolver.py::_collect_csv_consumers` to split `/TABLE=` on commas. Add one unit test.
2. Create `src/vg2c/analyzer/models.py` with `ProducerKind`, `ConsumerKind`, `ProducerRecord`, `ConsumerRecord`, `DataflowEdge`, `AnalyzedProgram`. ~80 lines, no logic.
3. Create `src/vg2c/analyzer/dataflow.py` with `analyze(resolved: ResolvedProgram) -> AnalyzedProgram` plus the private `ScopeRelations` helper. ~150 lines.
4. Create `src/vg2c/analyzer/__init__.py` exporting `analyze` and the public types.
5. Write the unit and end-to-end tests from §6.
6. Run `pytest`; iterate until green; commit.

Estimated effort: smaller than Stage 2. Most of the logic is mechanical bookkeeping; the only non-trivial parts are the scope-relation classifier (~30 lines using `ScopeRelations`) and the external-producer heuristic (~20 lines).

---

*End of Stage 3 plan. Implementation proceeds only after this plan is approved.*
