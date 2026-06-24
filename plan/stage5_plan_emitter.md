# Stage 5 Plan — Python Emitter

Audience: the coding agent (or human) about to implement Stage 5.
Source-of-truth for prior stages: progress files for stages 1–3; Stage 4 has no progress file but the implementation in `src/vg2c/dispatch/` is verified clean (single `DispatchedProgram` wrapper with per-SQL-block `ReaderTarget` + `rewritten_sql`).

Scope: take `DispatchedProgram` (Stage 4) and produce a `EmittedScript` containing one human-readable Python file per VG2 source. **No execution. No live validation.** The emitted file references a `vg2c_runtime` helpers package whose implementation is Stage 6's concern.

Anchors carried forward (unchanged): deterministic, diagnostics-first, no AI, no file I/O at emit time, no SQL parsing, no mutation of prior-stage models.

---

## Step 1 — Re-Grounding

### Current pipeline reality
- **Stage 1** (`src/vg2c/frontend/`) — parser + classifier, 10-value `Kind`.
- **Stage 2** (`src/vg2c/resolver/`) — `ResolvedProgram` with `scope_tree`, per-block `RuntimeMacroRef[]`, `SqlMacroCall[]` (each with a `@@SQLMACRO:n@@` placeholder embedded in `resolved_body`), `MacroControlPayload` parsed.
- **Stage 3** (`src/vg2c/dataflow/`) — `AnalyzedProgram` with `producers`, `consumers`, scope-aware `DataflowEdge[]`, `unused_producers`.
- **Stage 4** (`src/vg2c/dispatch/`) — `DispatchedProgram` with `DispatchedBlock` per SQL-bearing block: `dialect`, `ReaderTarget(reader_class_hint, database_arg, record_name, record_version, node, instance)`, and `rewritten_sql` (post-`@OASYSSCHEMA@` substitution; `@[]@` preserved).

### What the Emitter can rely on
- Source order is authoritative (block indices are dense; `scope_tree` leaves are in order).
- `scope_tree` covers every block exactly once; `MACRO_CONTROL` blocks are *not* leaves under their own kind — they're consumed as scope-node boundaries.
- All `<<<NAME>>>` placeholders are already extracted into `RuntimeMacroRef[]` per block (with `frame_id`, `location`, `option_key`). Bodies/option-values still contain the literal `<<<NAME>>>` text — Stage 2 did not rewrite them in-place; the refs are a side-table.
- All `SQL_Get_CSV_List(...)` calls are extracted into `SqlMacroCall[]` and *have been replaced* in `resolved_body` / `rewritten_sql` with `@@SQLMACRO:n@@` tokens.
- Dialect, reader class, record identity, schema are pre-resolved by Stage 4.
- Per-CSV producer/consumer is known via Stage 3.

### What the Emitter must do itself
- **Macro placeholder substitution into Python expressions** — Stage 2 left the literal `<<<NAME>>>` in bodies; the emitter rewrites them to `ctx.macro.named("NAME")` calls (or string-interpolates if the location is a Python string literal).
- **`@@SQLMACRO:n@@` substitution** — replace each token with a runtime helper invocation that produces the chunked `IN (...)` SQL.
- **`/UTILITIES=` subclassification** — Stage 1 deliberately did not subclassify utilities; the emitter must finally do so (Run_Python_Script, SQLPathFinder_Email, RoboCopy, SPFDelete, SPFCopy, raw `.bat`, raw `.exe`). Each maps to a different runtime call shape.
- **IF/MACRO scope-to-Python lowering** — convert `IfThen` payload's operator codes (`EQS`, `NES`, `LE`, `GT`, …) to Python expressions; convert `StartMacro` (row-iter vs static) to `for ... in csv_io.iter(...)` + `with macro_scope_row(...)` or just `with macro_scope_static(...)`.

---

## Step 2 — Critical Review of Current State

### What the system can reliably do (Stages 1–4 in aggregate)
- Read any current fixture, classify every block, build a correct nested scope tree (verified on `actual_script.txt`'s depth-3 IF-in-MACRO-in-IF pattern), resolve macro placeholder structurally (frame IDs assigned), capture SQL macro calls as structured records, link CSV producers/consumers with scope-relation classification, resolve dialect + schema substitution + reader targets per SQL block.
- Surface diagnostics in-band across all four stages, with severity buckets used consistently.
- Pass 86+ tests, no errors on the four clean fixtures.

### What is still missing / weak (blockers for Stage 5)

| # | Gap | Severity | Stage 5 action |
|---|---|---|---|
| G1 | Utility blocks are uniformly `Kind.UTILITY`; the emitter must subclassify by inspecting `/UTILITIES=` value at emit time. The classifier was deliberately kept coarse. | High | Emitter introduces a small utility-shape matcher (table-driven, not per-utility files). |
| G2 | `<<<NAME>>>` placeholders are still literal text in `resolved_body` / option values. The `RuntimeMacroRef` side-table tags them but does not rewrite them. Emitter must perform the rewrite contextually (Python expr vs string literal vs SQL body). | High | One rewrite pass per emitted artifact, using the `RuntimeMacroRef` records as the authoritative locator. |
| G3 | `SqlMacroCall.column_ref` is `int | str` (index vs name). The emitter must produce a helper call that handles both. The Stage 6 runtime helper signature must accept both shapes. | Medium | Define the runtime helper signature here (Step 4 — Runtime API Contract). |
| G4 | `WRITE_FILE` bodies can contain placeholders (e.g. `CSRVerror.htm` body has `<<<SFOLDER>>>`). The emitter must either substitute at emit time (only if statically resolvable) or pass through a runtime helper that does the substitution from `ctx`. | Medium | Emit `ctx.write_file(path, _TEMPLATE_X, vars=...)` where the template is a literal triple-quoted string and the helper does the runtime interpolation. |
| G5 | HTML_REPORT blocks are explicitly out of scope. They must still appear in the emitted file as a comment block or no-op call, in source order — so the script structure mirrors the VG2. | Low | Emit a single-line `# Step X-Y: HTML report (not translated)` comment; no helper call. |
| G6 | `Kind.UNKNOWN` and `Kind.MALFORMED` blocks must emit something visible (TODO stub) so the engineer sees the gap. | Low | Emit `# TODO: unhandled kind=<...> at step X-Y` + a diagnostic. |
| G7 | Stage 3 produces `unused_producers` and other informational diagnostics. The emitter can use `unused_producers` to suppress variable-name churn but must not act on most diagnostics (those are caller concerns). | Low | Pass diagnostics through; do not gate emission. |

---

## Step 3 — Stage 5 Architecture

Five components in `src/vg2c/emitter/`. The pattern mirrors prior stages: small files, one responsibility each.

### `HandlerRegistry` (`src/vg2c/emitter/registry.py`)
- **Responsibility:** decorator-based registration of one handler class per `Kind`, queried by `kind: Kind -> Handler`.
- **In → Out:** `Kind` → `Handler` instance (looked up at emit time).
- **Why it exists:** adding a new block kind = new file + `@register(Kind.X)` line, no core edits. This is the extensibility hinge the whole architecture has been pointing at.

### `Handler` protocol (`src/vg2c/emitter/protocol.py`)
- **Responsibility:** uniform per-Kind interface. Single method `emit(ctx: EmitContext, block: ResolvedBlock, dispatched: DispatchedBlock | None) -> EmittedFunction`.
- **In → Out:** one block → one Python function source string + the call-site line that invokes it from `run()`.
- **Why it exists:** without a uniform protocol, the TreeWalker would special-case every kind. Strategy pattern.

### `TreeWalker` (`src/vg2c/emitter/walker.py`)
- **Responsibility:** recursively walk `ResolvedProgram.scope_tree`. For each leaf, dispatch to the registered handler. For each scope node (macro / if / if-branch / else-branch), emit the Python wrapper (`with`, `for`, `if`, `else`) at the correct indentation.
- **In → Out:** `ScopeTree` + `DispatchedProgram` → ordered list of `EmittedFunction` + the `run()` body string.
- **Why it exists:** the scope-tree-to-Python-structure mapping is one place, not duplicated per handler. Handlers stay block-local.

### `MacroSubstituter` (`src/vg2c/emitter/macro_subst.py`)
- **Responsibility:** given a text blob and the block's `RuntimeMacroRef[]`, rewrite each `<<<NAME>>>` / `<<>>` occurrence to a Python expression appropriate for the surrounding context. Three contexts:
  - **Python expression** (e.g. inside an `if` condition or function arg) → `ctx.macro.named("NAME")`.
  - **String literal in emitted Python** (e.g. inside an f-string) → `{ctx.macro.named("NAME")}`.
  - **Body text passed to a runtime helper** (e.g. a `.htm` body for `write_file`) → leave the `<<<NAME>>>` in place; the helper handles substitution.
- **In → Out:** `(text, refs, target_context)` → rewritten text.
- **Why it exists:** macro rewriting is the same logic everywhere; a handler-local implementation would duplicate it five times.

### `UtilityShapeMatcher` (`src/vg2c/emitter/utility_shapes.py`)
- **Responsibility:** classify a `/UTILITIES=` string into one of: `run-python-script`, `email`, `robocopy`, `spf-delete`, `spf-copy`, `bat-file`, `exe-direct`, `unknown`. Pure pattern matching against the leading argv tokens.
- **In → Out:** raw utilities string → utility shape + parsed argv.
- **Why it exists:** the Stage 1 plan explicitly deferred utility subclassification. This is where it lives, finally, and only at emit time.

### `Emitter` orchestrator (`src/vg2c/emitter/__init__.py`)
- **Responsibility:** `emit(dispatched: DispatchedProgram) -> EmittedScript`. Walk the tree, assemble imports, headers, helper functions, and the single `run()` entrypoint, format with `ruff format`-compatible output (no formatter dependency — emit clean indentation directly).
- **In → Out:** `DispatchedProgram` → `EmittedScript`.

### Data model — minimal additions in `src/vg2c/emitter/models.py`

- **`EmitContext`** — short-lived, mutable during emission. Holds: current indent depth, the imports set (handlers register their imports), the handler registry, the dispatch lookup table (`block_index -> DispatchedBlock`), the macro substituter.
- **`EmittedFunction`** — `name: str`, `source: str`, `call_site: str`. Each step in VG2 becomes one helper function; `run()` invokes them in order with the right wrappers.
- **`EmittedScript`** — `source: str` (the full file content), `imports: tuple[str, ...]`, `diagnostics: tuple[Diagnostic, ...]`.

> No mutation of `ResolvedBlock`, `AnalyzedProgram`, or `DispatchedProgram`. The emitter is a pure read-many → write-one pass.

### Runtime API contract (defined in Stage 5, implemented in Stage 6)

The emitter generates code that references this surface. Stage 5 will ship a stub module `src/vg2c_runtime/__init__.py` containing **only signatures + docstrings + `NotImplementedError`** so Stage 6 has a concrete target.

```text
ctx                            # PipelineContext singleton
ctx.macro.named(name) -> str
ctx.macro.positional() -> str
ctx.csv_io.iter(name) -> Iterator[Row]
ctx.csv_io.read(name) -> Path
ctx.csv_io.write(name, content)
ctx.sqlite_engine.run_join(sql, inputs, output)
ctx.sql_macros.sql_get_csv_list(path, column_ref, lead_in) -> str
ctx.fs_ops.copy(src, dst); ctx.fs_ops.rename(src, dst); ctx.fs_ops.delete(paths)
ctx.mail.send(to, subject, body, attachments)
ctx.external.run(argv, cwd=None, env=None)
ctx.write_file(path, template, vars=None)
macro_scope_static(ctx, **vars)  # context manager
macro_scope_row(ctx, row)        # context manager
```

This is the *minimum* surface the emitted code touches. Each name is a hard contract.

---

## Step 4 — Per-Kind Emission Map

| `Kind` | Emitted Python shape |
|---|---|
| `MARS_READ` | `OracleReader(database="MARS", node="...", record=("Name","1.0.0.0"))` + `Task` with the `rewritten_sql` (still contains `@[]@`, DataSyncX expands) → write `/CSV` |
| `OASYS_READ` | Same as MARS but `database="OASYS"`, `rewritten_sql` has `@OASYSSCHEMA@` already substituted |
| `ARIES_READ` | Same shape, `database="ARIES"` |
| `SQLITE_QUERY` | `ctx.sqlite_engine.run_join(sql=..., inputs=[...], output=...)` |
| `WRITE_FILE` | `ctx.write_file(path, template, vars=...)` where `template` is a triple-quoted string of the body |
| `UTILITY` | Dispatched via `UtilityShapeMatcher`: → `ctx.external.run(["py", "script.py"])` / `ctx.mail.send(...)` / `ctx.fs_ops.copy(...)` / `# TODO: unknown utility` |
| `HTML_REPORT` | Single-line comment only |
| `MACRO_CONTROL` | Consumed by `TreeWalker` as scope boundaries; **no per-block emission** |
| `UNKNOWN` / `MALFORMED` | `# TODO: unhandled kind=<...>` + diagnostic |

### Scope-node lowering

| `ScopeNode.kind` | Emitted Python |
|---|---|
| `program` | wraps everything in `def run() -> None:` |
| `macro` (StartMacro with csv_path) | `for row in ctx.csv_io.iter("...csv"):` followed by `with macro_scope_row(ctx, row):` |
| `macro` (StartMacro without csv) | `with macro_scope_static(ctx):` |
| `if` | `if <expr>:` / `else:` with branches as nested blocks |
| `if-branch`, `else-branch` | indent-only; their children are inline |
| `leaf` | call-site line for the corresponding handler-emitted function |

`IfThen` operator codes are mapped via a static table: `EQS` → `==`, `NES` → `!=`, `LE` → `<=`, `LT` → `<`, `GE` → `>=`, `GT` → `>`, `EQ` → `==` (numeric), `NE` → `!=`. `VAR(...)` operands are unwrapped to `ctx.macro.named("...")`. Unknown operator → diagnostic + emit literal Python comment.

---

## Step 5 — Pitfalls (Real, Ranked)

### High

**H1. Emitted code must be syntactically valid Python.**
The emitter's strongest hard contract. Every test must `ast.parse(emitted_source)` cleanly. A handler that accidentally emits unbalanced quotes or stray `<<<...>>>` literals will break the whole script.

**H2. Macro substitution context confusion.**
The same `<<<SFOLDER>>>` placeholder needs different rewrites depending on whether it sits in a Python expression, a Python string literal, or a body-template that the runtime handles. Getting this wrong produces either invalid Python or wrong runtime behaviour. The `MacroSubstituter` must take an explicit `target_context` argument every call site.

**H3. `IfThen` operator coverage.**
`actual_script.txt` uses `EQS`, `LE`, `NE`, with `AND` conjunctions and `VAR(...)` operand wrappers. Mis-mapping `EQS` to `=` (SQL-style) instead of `==` would silently produce assignment statements in Python — syntactically valid but semantically wrong. The mapping table must be complete and tested per operator.

### Medium

**M1. Utility shape false positives.**
`@EXEDIR@\Run_Python_Script.va "lich.py" "" "N" "atd_atm.hadoop" "Python-v3"` should be recognised, but a fixture variant like `Run_Python_Script.va` (no `@EXEDIR@\` prefix) might miss. The matcher must split argv first, then match by basename.

**M2. WRITE_FILE bodies that are valid Python or HTML.**
`script_another.txt` writes `lich.py` whose body IS Python code (later executed by `Run_Python_Script.va`). The emitter must treat this as opaque text — a triple-quoted raw string — not try to embed or "improve" it.

**M3. Multiple `@@SQLMACRO:n@@` placeholders in one SQL body.**
`actual_script.txt:960-963` has two on adjacent lines. The emitter must process them in order, replacing each with a distinct helper invocation; mixing the order would put the wrong lead-in in the wrong slot.

**M4. Scope tree contains MACRO_CONTROL leaves at boundaries.**
The Stage 2 scope tree includes the `{START-MACRO}`, `{END-MACRO}`, `{IF-THEN}`, `{ELSE}`, `{END-IF}` blocks as scope-defining nodes — they should NOT also be walked as leaf blocks emitting code. The walker must check `block.kind` and skip MACRO_CONTROL in the leaf dispatch.

### Low

**L1. `ROWS-IN-FILE` is a non-scope leaf.**
It's a `MACRO_CONTROL` block but acts as a side-effect leaf, not a scope opener. It maps to `<varname> = ctx.csv_io.row_count("...csv")`. Easily missed — explicit test.

**L2. CSV path normalisation in emission.**
The emitter must use the *original* `/CSV=` string (e.g. `macrotmp.csv`) when passing to `ctx.csv_io.write(...)`, not the normalised form (lowercase) Stage 3 uses internally for matching. Mixing the two would produce path mismatch at runtime.

**L3. Diagnostic blow-up from unknown utilities.**
If a fixture contains 20 unfamiliar `.va` scripts, emitting 20 `# TODO: unknown utility` comments + 20 diagnostics is correct but noisy. Acceptable in v1; revisit when a real script hits the threshold.

---

## Step 6 — Testing Strategy

### Hard contract: every emitted script must `ast.parse()` cleanly
This is the single strongest test across the whole suite. If any handler emits invalid Python on any fixture, the emitter is broken. All E2E tests assert this first.

### Unit tests — `tests/emitter/test_handlers.py`
One test per handler:
- `MARS_READ` emits a function that constructs `OracleReader(database="MARS", node="KM.[A15_PROD_21.].MARS", record=("Calendar", "1.0.0.0"))` and a `Task` calling `read` then writing `calendar_ref.csv`.
- `OASYS_READ` emits the OASYS-equivalent with `@OASYSSCHEMA@` already substituted in the SQL string.
- `SQLITE_QUERY` emits `ctx.sqlite_engine.run_join(...)` with the correct `inputs=[...]` from `/TABLE=`.
- `WRITE_FILE` (literal CSV body) emits `ctx.write_file("macrotmp.csv", _TEMPLATE_0)` with the template defined as a module-level triple-quoted string.
- `WRITE_FILE` with `<<<SFOLDER>>>` in body emits the same shape, passing `vars={"SFOLDER": ctx.macro.named("SFOLDER")}`.
- `UTILITY` (Run_Python_Script) → `ctx.external.run([...])`.
- `UTILITY` (SQLPathFinder_Email) → `ctx.mail.send(to=..., subject=..., body=..., attachments=[...])`.
- `UTILITY` (RoboCopy) → `ctx.fs_ops.copy(...)`.
- `UTILITY` (SPFDelete) → `ctx.fs_ops.delete([...])`.
- `UTILITY` (raw `.bat`) → `ctx.external.run(["getcsrsu.bat"])` + comment.
- `UTILITY` (raw `.exe` with `<<<...>>>` args) → `ctx.external.run([..., ctx.macro.named("SFOLDER"), ...])`.
- `UTILITY` (unrecognised) → comment stub + diagnostic.
- `HTML_REPORT` → single-line comment, no diagnostic.
- `UNKNOWN` → TODO stub + diagnostic.

### Unit tests — `tests/emitter/test_walker.py`
- Linear program (no scopes) → flat sequence of `step_*` calls inside `run()`.
- Single `{START-MACRO}` (row-iter, with csv) → `for row in ctx.csv_io.iter(...):` + `with macro_scope_row(...):` wrapper.
- Single `{START-MACRO}` (static, no csv body) → `with macro_scope_static(...):` wrapper.
- `{IF-THEN}/{END-IF}` (no ELSE) → `if <expr>:` block only.
- `{IF-THEN}/{ELSE}/{END-IF}` → `if/else` with both branches.
- Nested IF-in-MACRO-in-IF (synthesised from `actual_script.txt` patterns) → 3 levels of Python indentation, correct call-site placement.
- `MACRO_CONTROL` blocks are not present as standalone calls in the emitted body.

### Unit tests — `tests/emitter/test_macro_subst.py`
- `<<<SFOLDER>>>` in a Python-expression context → `ctx.macro.named("SFOLDER")`.
- `<<<SFOLDER>>>` in a Python f-string context → `{ctx.macro.named("SFOLDER")}`.
- `<<<SFOLDER>>>` in a body-template context → preserved literal `<<<SFOLDER>>>` (helper substitutes at runtime).
- `<<>>` (positional) → `ctx.macro.positional()`.
- Multiple placeholders in one string — all rewritten, none missed.
- Case folding: `<<<sfolder>>>` and `<<<SFolder>>>` both resolve to `ctx.macro.named("SFOLDER")`.

### Unit tests — `tests/emitter/test_utility_shapes.py`
Each utility shape with at least one positive and one negative case. Argv-split must handle quoted args with embedded spaces and UNC paths.

### Edge case tests
- `IfThen` with `VAR(<<<CSRV>>>)` operand → `ctx.macro.named("CSRV") == "FAIL" and ctx.macro.named("UNDERDEV") == "N"`.
- `ROWS-IN-FILE` → `CONFIG = ctx.csv_io.row_count("ICMPCS_config.csv")`.
- WRITE_FILE producing `lich.py` (body is Python) → emitted as triple-quoted raw string, not parsed.
- Two adjacent `@@SQLMACRO:0@@` and `@@SQLMACRO:1@@` in one SQL body → two separate helper calls in correct order.

### End-to-end fixture tests — `tests/emitter/test_fixtures.py`
Pipeline `parse → classify → resolve → analyze → dispatch → emit` over all five fixtures. For each:
- Runs without exception.
- `ast.parse(emitted.source)` succeeds.
- Emitted source contains a `def run() -> None:` and an `if __name__ == "__main__":` block.
- Emitted source contains expected function names derived from `/PROMPT-TEXT` (slug form), in source order.
- For `actual_script.txt`: emitted source's `def run()` body has at least one `for row in ctx.csv_io.iter(...)` and at least one `if ...:` and one `else:`.

### What is deliberately not tested
- That the emitted code *runs*. Stage 6 ships the runtime; this is Stage 6's E2E contract.
- That the SQL is "correct" against a live database.
- Formatter behaviour (we emit clean indentation directly; ruff-format applied externally if desired).
- Line-by-line snapshot equality. Snapshots break on every spacing tweak.

---

## Step 7 — Non-Goals for Stage 5

- **No execution of the emitted code.** Stage 6 ships the runtime.
- **No DataSyncX integration testing.** The emitter generates `OracleReader(...)` calls; whether they connect is Stage 6+ business.
- **No SQL validation.** Schema-substituted SQL is emitted as-is.
- **No agentic AI.** Constraint unchanged.
- **No re-walk of prior stages.** Trust `DispatchedProgram` end-to-end.
- **No mutation of `ResolvedBlock` / `AnalyzedProgram` / `DispatchedProgram`.**
- **No CLI.** The emitter is a library function; the CLI is Stage 7.
- **No formatter dependency.** Emit clean indentation manually; let the user run `ruff format` externally.
- **No imports beyond stdlib in the emitter itself.** Stage 5 has zero new runtime dependencies.
- **No View Expansion.** Same status as Stage 4.
- **No code golf / optimisation pass.** Human-readable beats clever.

---

## Step 8 — Simplicity Check

### Intentionally not implemented
- A general AST-builder. We emit strings via a thin `IndentWriter`; nothing fancier earns its keep at this scope.
- A formatter integration. `ruff format` is a one-line external call if needed.
- Per-utility *files* (one per shape). The `UtilityShapeMatcher` is a single ~60-line table-driven function — splitting it into eight files would obscure the dispatch logic.
- Auto-discovery of handlers via entry points / file scanning. Import-time decorator registration is enough.
- Runtime helper *implementation*. Stage 5 ships the *signature contract*; Stage 6 implements.
- Live-running E2E tests. The emitted code references `vg2c_runtime` which doesn't exist yet. `ast.parse()` is the structural contract; runtime contract is Stage 6.

### Most likely future complexity
- **Utility shape growth.** Each new `.va` script discovered in a real fixture needs a new entry. Keep them table-driven; do not branch on filename strings in handler code.
- **Macro substitution edge cases.** Triple-nested macros, conditionals that reference macros from two scopes up — the current `ctx.macro.named()` API handles them at runtime, but the substituter's context-detection (expr vs string vs template) will see new shapes.
- **HTML report blocks.** If they eventually need translation, that's a new handler — same Strategy + Registry hinge.

### Minimum viable Stage 5 (first commit path)
1. Create `src/vg2c/emitter/models.py` (`EmitContext`, `EmittedFunction`, `EmittedScript`). ~50 lines.
2. Create `src/vg2c/emitter/registry.py` (decorator + handler map). ~20 lines.
3. Create `src/vg2c/emitter/protocol.py` (`Handler` Protocol, `IndentWriter` helper). ~30 lines.
4. Create `src/vg2c/emitter/macro_subst.py`. ~50 lines.
5. Create `src/vg2c/emitter/utility_shapes.py` with the matcher table. ~80 lines.
6. Create one handler per Kind under `src/vg2c/emitter/handlers/`. ~30–60 lines each.
7. Create `src/vg2c/emitter/walker.py`. ~80 lines.
8. Create `src/vg2c/emitter/__init__.py` orchestrator with `emit(...)`. ~50 lines.
9. Create `src/vg2c_runtime/__init__.py` as a signature-only stub raising `NotImplementedError` everywhere. ~80 lines, no logic. This is the *contract* Stage 6 satisfies; the emitter imports it.
10. Write unit + edge + E2E tests per §6.
11. `pytest`; iterate until green.

Estimated effort: comparable to Stage 2. The handler files are mostly mechanical; the hard parts are `MacroSubstituter` (context-aware rewrite) and the `UtilityShapeMatcher` table.

---

*End of Stage 5 plan. Implementation proceeds only after this plan is approved.*
