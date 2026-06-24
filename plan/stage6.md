# Stage 6 — Emitter Correctness

Stage 5's emitted Python is structurally valid (`ast.parse` passes) but **semantically dead** on `actual_script.txt`. The output exposes bugs invisible from static review of the code. Scope expands from "hardening" to "correctness"; *shape* stays the same: edits bounded to `src/vg2c/emitter/`, no new pipeline stage, no public model change, runtime helpers + CLI still deferred to Stage 7.

---

## Why the emitted script is wrong (verified findings)

| # | Defect | Evidence | Sev |
|---|---|---|---|
| **N1** | All SQL/reader handlers emit `output="step_NNNN.csv"` instead of the `/CSV=` value. WRITE_FILE handler pulls path from `/WRITE-FILE` (a Y/N flag) → `path="Y"`. | `src/vg2c/emitter/handlers.py`; `actual_script.txt:124` (`/WRITE-FILE=Y` + `/CSV=macrotmp.csv` adjacent); `actual_script.txt:806` (`/CSV=yeuchuan_a0_15507.tab` consumed downstream as `/TABLE=`) | **Critical** |
| **N2** | `{IF-THEN} "CONFIG" "LE" "0"` → emits `if "CONFIG" <= "0":` (string-vs-string). The bare LHS is a macro variable, not a literal. | `walker.py:unwrap_operand`; fixture lines 231, 380, 559, 789, 1243 | **Critical** |
| **N3** | `"VAR(<<<CSRV>>>)"` → `ctx.macro.named("<<<CSRV>>>")` (angle-brackets retained as part of the macro name). | Same; fixture lines 427, 481 | **Critical** |
| **N4** | `EQS`/`NES` (string ops) and `LE`/`LT`/`GE`/`GT`/`EQ`/`NE` (numeric ops) all map identically. Numeric ops need `int(...)` coercion on both sides. | `_OPERATOR_MAP` | High |
| **N5** | `{ROWS-IN-FILE} "ICMPCS_config.csv" "CONFIG" "N"` emits *nothing*. The variable definitions never appear, so even with N2 fixed the conditions can't work. | Walker skips all `Kind.MACRO_CONTROL` blocks uniformly; fixture lines 219, 368, 547, 777, 1231 | **Critical** |
| **N6** | `node="<<<MARS>>>"`, `record=("WIP_Lot_History_v2", "1.0.0.0")`, etc. — option-value macro refs emitted as literal Python strings. | SQL handlers; `MacroSubstituter.substitute` filters `if ref.location != "body"` | High |
| **N7** | `ctx.external.run(['setsiteparam.exe', 'KM', '<<<SFOLDER>>>', '<<<UNDERDEV>>>', ...])` — argv elements with macro refs stay literal. | `_emit_utility` exe-direct branch | High |
| **N8** | `/PROMPT-TEXT="Step 1-9. pass server time info..."` is ignored. Function names are `step_0009_utility`. | All handlers | Medium |
| **N9** | Mystery `2` between functions in pasted output. `IndentWriter` has no path that writes `2`. | Likely paste/render artifact — verify on real generated file | Low |

Carry-forward defects from the prior plan still apply: D1 escape (`\K`/`\A` → 9 SyntaxWarnings; `"""` mangling), D3 `@@SQLMACRO:n@@` never expanded, D4 walker swallows exceptions silently, D6 utility argv discarded for email/copy/delete shapes, D7 `instance=""` vs `None`, D11 tests assert structural shape only.

---

## Steps (10 groups, each independently landable)

Ordered by **impact**, not by file. The implementer can stop at any step and the emitter is better than before.

1. **Group A — Filename routing (N1).** Single helper `_resolve_output_path(block, default)`. All five handlers (4× SQL/reader + WRITE_FILE) read `/CSV=`. **Single biggest correctness fix in the whole stage.**
2. **Group D — ROWS-IN-FILE walker branch (N5).** Walker branches `MACRO_CONTROL` leaves by payload type; `RowsInFile` emits `ctx.macro.set_named("VAR", ctx.csv_io.row_count("path.csv"))`. Add `MacroState.set_named` to runtime stub.
3. **Group B (B4) + C — IF-THEN operand + operator typing (N2, N3, N4).** Rewrite `_build_condition_expr.unwrap_operand` with branches: `VAR(...)`, `<<<...>>>`, bare identifier, literal. Replace `_OPERATOR_MAP` with `_OPERATOR_TABLE` carrying `("==", "numeric"|"string")`; numeric ops coerce both sides to `int(...)`.
4. **Group B (B1) — Option-value substitution (N6, N7).** Drop the `location != "body"` filter in `MacroSubstituter`. New helper `_subst_value_into_python(value, refs)` returns a quoted literal, a bare `ctx.macro.named(...)` expression, or an f-string depending on placeholder shape. Wire into SQL handlers' `node=`/`record=`/`instance=` and utility argv.
5. **Group G — String literal escape (D1).** `_python_string_literal` using `repr` for short, raw triple-quoted `r"""..."""` for multi-line, fallback to `repr`-with-explicit-`\n` when body contains `"""`.
6. **Group B (B3) — `@@SQLMACRO:n@@` expansion (D3).** `_expand_sql_macros(sql, calls)` splices `ctx.sql_macros.sql_get_csv_list(path, column_ref, lead_in)` into an f-string composition. Left-to-right, deterministic.
7. **Group E — Utility argv plumbing (D6, N7).** All shapes consume `shape_info.argv` through Group B substitution. Email shape gets `# TODO: SQLPathFinder_Email.va argv positions are heuristic` comment + info diagnostic.
8. **Group H — Walker diagnostics (D4).** Thread `walker_diagnostics: list[Diagnostic]`; replace `except Exception: pass` with `emit-handler-failed` diagnostic carrying `block_index`/`span`. `walk_and_emit` returns `(functions, run_body, diagnostics)`.
9. **Group F — Function naming (N8).** Slugify `/PROMPT-TEXT`; prefix with `step_NNNN_` for uniqueness/source-order; cap at ~80 chars. Falls back to `step_NNNN_<kind>` when missing.
10. **Group I — Mystery `2` (N9).** Run the pipeline, write emitted source to disk, read back as raw bytes, search for `^2$` lines. If reproduced, trace. If not, paste artifact — document and move on.
11. **Group J — Doc rewrite.** Fix `progress/stage5_emitter.md` to match code (drop `database_arg="orasql"`, "macro substitution integration", "unit tests" claims).

---

## Architecture (bounded to `src/vg2c/emitter/`)

### Data model evolution
**None.** All groups operate on existing `ResolvedBlock` / `RuntimeMacroRef` / `DispatchedBlock` data already produced upstream. Runtime stub gains one new signature: `MacroState.set_named`.

### Pipeline integration
Unchanged externally. `emit(dispatched) -> EmittedScript` keeps the same shape.

### Relevant files
- `src/vg2c/emitter/handlers.py` — primary edits (Groups A, B1, B3, E, G, F).
- `src/vg2c/emitter/walker.py` — Groups B4, C, D, H.
- `src/vg2c/emitter/macro_subst.py` — drop location filter; reused by Group B1.
- `src/vg2c/emitter/__init__.py` — orchestrator update for Group H return shape.
- `src/vg2c_runtime/__init__.py` — add `MacroState.set_named` signature.
- `tests/emitter/` — nine new unit test files; one extended e2e file.
- `progress/stage5_emitter.md` — Group J doc rewrite.

---

## Verification

### Per-group unit tests (new files in `tests/emitter/`)

| File | Coverage |
|---|---|
| `test_output_path.py` | `_resolve_output_path` returns `/CSV=` value when present; falls back to `step_NNNN.csv` when absent; WRITE_FILE uses same helper. |
| `test_condition_builder.py` | All operand shapes: `VAR(<<<X>>>)`, `VAR(X)`, `<<<X>>>`, bare ident, quoted literal. Both op types (numeric/string) produce correct coercion. Compound AND/OR. Unknown operator → diagnostic + `==` fallback. |
| `test_value_subst.py` | `_subst_value_into_python("<<<MARS>>>", refs)` → bare `ctx.macro.named("MARS")` expression. `"MARS-<<<X>>>"` → f-string. Plain text → quoted literal. `<<>>` positional → `ctx.macro.positional()`. |
| `test_rows_in_file.py` | Walker emits `step_NNNN_rows_in_file` for `RowsInFile` payload, with `set_named` body and `row_count` call. Other MACRO_CONTROL payloads still skipped. |
| `test_utility_argv.py` | One positive per shape with non-empty argv including `<<<...>>>` placeholders. |
| `test_string_literal.py` | `\K`/`\A`, `"""`, mixed quotes, empty, multi-line. Zero `SyntaxWarning` after `compile(...)`. |
| `test_sql_macro_expansion.py` | Zero/one/two `@@SQLMACRO:n@@` tokens; ordering. |
| `test_walker_diagnostics.py` | Handler raise → `emit-handler-failed` with correct `block_index`/`span`. |
| `test_function_naming.py` | Slugifier produces valid Python identifiers; long inputs truncate; uniqueness via `step_NNNN_` prefix. |

### Extended `test_fixtures.py` for all five fixtures
- `compile(source, ..., "exec")` produces **zero `SyntaxWarning`** (`warnings.catch_warnings()`).
- No `<<<` substring outside triple-quoted bodies.
- No `@@SQLMACRO:` substring anywhere.
- No `emit-handler-failed` on clean fixtures.

### Cross-stage dataflow consistency assertion (drives Group A verification)
- Regex-extract all `output="..."`, `path="..."`, `ctx.csv_io.write("...", ...)` from emitted source → `produced_files`.
- Regex-extract all `inputs=[...]` from emitted source → `consumed_files`.
- Assert `consumed_files ⊆ produced_files ∪ {ROWS-IN-FILE inputs} ∪ {fixture seed files}`.

### `actual_script.txt`-specific assertions
- Emitted source contains `ctx.macro.set_named("CONFIG", ` and `ctx.macro.set_named("CONFIGSETS", `.
- IF condition uses `int(ctx.macro.named("CONFIG"))` for numeric op.
- `node=ctx.macro.named("MARS")` appears (not `node="<<<MARS>>>"`).
- Utility argv list contains `ctx.macro.named("SFOLDER")` (not `'<<<SFOLDER>>>'`).
- `output=` filenames include `macrotmp.csv`, `yeuchuan_a0_15507.tab`, `CSR_Server_OIS_Product_List.csv`.
- WRITE_FILE `path=` is never `"Y"`.
- `ctx.macro.named(...)` never receives an argument containing `<<<`.

### Not in test scope
- Runtime execution (Stage 7).
- Source snapshots.
- The `2` artifact — investigated manually in Group I, not via test.

---

## Pitfalls

### Critical
- **C1. Filename routing consistency.** Every producer (5 handler kinds) must use `/CSV=` so every consumer's `/TABLE=` resolves. Cross-stage dataflow test is the safety net.
- **C2. Bare-identifier IF-THEN heuristic risk.** A literal like `"FAIL"` in `EQS "VAR(<<<X>>>)" "FAIL"` is a real string. Rule: **only** treat bare LHS as macro variable when operator is **numeric**; for string ops, bare = literal unless `VAR(...)`/`<<<...>>>`-wrapped. This rules out false positives.

### High
- **H1.** Escape rewrite regressions — unit-test against `\K`, `\A`, `"""`, mixed quotes, empty, multi-line.
- **H2.** `@@SQLMACRO:n@@` ordering with two adjacent tokens in `actual_script.txt`.
- **H3.** Walker return-shape change ripples through the orchestrator atomically.
- **H4.** ROWS-IN-FILE coupling — verify Stage 2's `RowsInFile` payload field names (`csv_path`, `var_name`) before relying on them.

### Medium
- **M1.** Option-value substitution outputs vary by placeholder shape: bare expression vs f-string vs literal. Helper must handle all three.
- **M2.** Slugified function names can be long — cap at ~80 chars while keeping `step_NNNN_` prefix.
- **M3.** Email argv positions unstandardised — `# TODO` + info diagnostic, don't guess.

### Low
- **L1.** Dead `HandlerRegistry`/`EmittedFunction` types — leave alone.
- **L2.** `EmitContext` mutability fine for single-emit lifecycle.
- **L3.** The `2` artifact — verify with a real run; treat as paste artifact until proven otherwise.

---

## Non-goals

- No runtime helper implementation (Stage 7).
- No CLI (Stage 7+).
- No DataSyncX execution.
- No new pipeline stage.
- No new public model (one runtime stub signature only: `MacroState.set_named`).
- No re-arch of Stages 1–4.
- No agentic AI.
- No formatter dependency.
- No view expansion.
- No registry refactor.
- No dead-type removal.

---

## Simplicity check

### Intentionally not added
- Per-shape utility files. Single matcher + single emit branch keeps dispatch readable.
- A general AST builder. String emission with `IndentWriter` is sufficient.
- A handler registry refactor.
- A graph-based dataflow check at emit time — Stage 3 already produced the analysis; the test layer cross-checks emitted output against it without rebuilding the graph.

### Where future complexity may appear
- **Macro operand grammar**: if VG2 invents new operand wrappers beyond `VAR(...)` / `<<<...>>>` / bare, the unwrap table grows.
- **Operator table**: any new SPF operator needs a row + a type tag.
- **WRITE_FILE templates with mixed placeholder types**: today only `<<<...>>>` is observed. If `<<>>` positional appears, the helper already handles it.
- **Utility shapes**: as new `.va` scripts surface, the table grows.

---

## Decisions
- **Stage 6 scope grows from "hardening" to "correctness"** — without filename routing the emitted Python cannot run at all, so deferring further is meaningless.
- **Function naming pulled into scope.** Cheap, high readability gain; the slugifier is needed regardless (uniqueness via `step_NNNN_` prefix).
- **SQL bodies keep `<<<NAME>>>` literal** for runtime substitution in `Reader.read()`; option values and utility argv get substituted **at emit time** because they're already in Python-expression contexts.
- **Bare-identifier IF-THEN handling is operator-typed** to avoid false positives.

---

## Further considerations (decide before implementation)
1. **Function-name length cap** — Option A: hard cap at 80 chars (truncate slug, keep `step_NNNN_` prefix). Option B: no cap. **Recommend A**.
2. **Bare-identifier case** — `"Lots" "GT" "0"` (mixed case) — case-insensitive lookup; emit `ctx.macro.named("LOTS")` (uppercased) to match existing `unwrap_operand` upcase behaviour.
3. **The `2` lines** — Option A: investigate now via Group I. Option B: defer until reproduced. **Recommend A** — five minutes of work, eliminates a known unknown before Stage 7.
