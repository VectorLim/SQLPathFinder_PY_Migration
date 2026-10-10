# SQLPathFinder generated-code refactor — master plan

**Status:** Planning published; Session 01 already started on its original branch. Sessions 02–05 run sequentially on one shared implementation branch. No merge authorized.
**Repository:** `VectorLim/SQLPathFinder_PY_Migration`  
**Verified branch:** `html-and-sql-rework`  
**Immutable inspection baseline:** `afb701c2013537ffbf3c1e1b86e147ad0c900d1d`  
**Verified `main` HEAD:** `e0737d90a164148a3f51320688d8ff4e2900a5e3`  
**Ancestry:** branch is one commit ahead of `main`, zero behind, with `main` as merge base (GitHub compare API, 2026-10-10).  
**Inspection limitations:** GitHub remote source inspected read-only through the connected GitHub integration. No local checkout was available; container cannot resolve github.com for `git clone`. Therefore local dirty/clean state and executable test baseline **cannot be verified in this planning session**. Start Session 1 with a real checkout, explicit `git status`, `git fetch`, and baseline tests. Do not imply tests have run.

## 1. Objective and non-negotiables

Make translated `main.py` ordinary, readable Python primarily describing the job's control flow. Preserve supported semantics for VG2 macros and scopes, SQL readers and bindings, file outputs, data transformations, HTML rendering, exceptions, and Linux paths. Runtime library `vg2c` is installed in the target container; generated code must need neither the VG2 source nor ScriptHost. The UI/frontend, HPC, JMP/JSL, unproven ScriptHost-wide feature expansion, and unrelated AED deployment work are out of scope. Existing unsupported behavior remains explicitly diagnosed, not silently treated as supported.

This is a **sequential five-session implementation with two branches total**: Session 01 retains its already-started `refactor/generated-code/s01-macros`; Session 02 creates `refactor/generated-code/implementation` at the exact completed and tested Session 01 SHA, and Sessions 03–05 continue that **same implementation branch**. Agents run as separate sessions, never concurrently editing the branch. The planning branch `html-and-sql-rework` remains documentation-only; no merge into `main` or planning branches is authorized.

## 2. Inspected architecture: facts, not assumptions

Compiler: `src/vg2c/compilation.py` performs parse → classify → resolve → dispatch → emit. `src/vg2c/emitter/project.py` (~485 lines) is the **current direct-project emitter** and traverses scope nodes, emitting Python while building SQL/HTML assets and invocation metadata. `src/vg2c/operands/{macro,conditional,loop}.py` and `src/vg2c/utilities/{sqlite_engine,html_report,_emit_helpers}.py` provide control/utility semantics and metadata. `src/vg2c/project_paths.py`, `src/vg2c/__init__.py`, and `src/vg2c/cli.py` write output projects.

Generated/runtime: `src/vg2c/runtime/__init__.py` exports direct runtime functions. `runtime/values.py` owns snapshot globals, token substitution and first-row macro CSV reading. `runtime/query.py` executes standalone `.sql` on `SqliteReader`/external reader and performs substitution, crosstab, header output. `runtime/csv_io.py` uses pandas for DataFrame CSV writes; `runtime/sqlite_reader.py` loads CSV into SQLite and returns pandas DataFrames; `runtime/crosstab.py` uses pandas `groupby(...).unstack(...)` and rewrites specialized `CrossTab->[[...]]` SQL tokens. `runtime/html.py` renders editable HTML shells and CSVReport rows, dynamically reads optional stylesheet files and copies them beside generated HTML. `runtime/html_format.py` creates CSS from native styles and formats cells. `utilities/macro_state.py` retains an older frame-stack class with **different** substitution semantics and compiler metadata usages; do not equate it to `runtime/values.py` without parity tests.

**Concrete generated case:** `ICMPCS/main.py` on this branch is 749 physical lines (748 content lines), and contains 48 literal `workdir=workdir`, 51 `values=job_values`, 57 occurrences of numbered macro-map names, 33 `substitute(...)` calls, and nine `styles[...]` assignments comprising long CSS declarations; it also has bulky `crosstab={...}` and `header=[...]` definitions. `ICMPCS/sql/*.sql` and `ICMPCS/html/*.html` already exist. `ICMPCS/html/report_055.html` still contains embedded `<style>` text and 26 numbered report header slots. These are baseline *readability indicators*, not correctness measures. This sample also references environment-specific resources and should not be executed unmocked.

**Critical factual corrections to proposed solutions:** (1) pandas is already a declared dependency (`pyproject.toml`: `pandas==3.0.3`) and used for the relevant tabular work; prefer targeted consolidation. (2) CSS external references are already supported in `runtime/html.py`; the missing work is primarily generated style ownership and asset generation. (3) `runtime/query.py` distinguishes SQL text, reader/site selection, CSV-table inputs, SQL binds and crosstab output; do not collapse them. (4) `runtime/values.substitute` errors on unknown ordinary macro tokens but preserves certain reserved tokens; older `MacroState.named` returns `""` for missing names and its substitution strips a leading newline. These cannot be unified by a blind class rename.

## 3. Ownership contract for the refactor

| Responsibility | Authoritative owner | Explicitly not owned by |
|---|---|---|
| Parse VG2 syntax, classify utilities, resolve source locations, model control scopes | Existing compiler, dispatch, operands | Generated main / runtime |
| Choose runtime API and generate ordinary Python AST-equivalent statements; construct static assets and editable invocation metadata | `emitter/project.py` and existing metadata contracts | Runtime library |
| Per-run working root, project asset root, environment snapshots, configured execution reader | One **instance-scoped** lightweight `JobRuntime` under `vg2c.runtime` | Process global state / CWD |
| Macro frame stack, mutable macro values, named substitutions, source-compatible missing/reserved-token rules | One runtime `MacroStore`, reusing existing `runtime/values.py` rules | Emitter-specific duplicate substitution engine |
| CSV/SQL execution, SQLite table binding, reader routing, special SQL tokens, crosstab pivot, CSV output | Runtime query/csv/sqlite/crosstab modules | Generated main |
| Static SQL, HTML, CSS and optionally compact report definitions | Generated project assets; runtime reads the current file at use | Long Python literals / inline style setup |
| HTML data escaping, raw authored HTML preservation, style linking/copying/embedding, report templates | Runtime HTML/rendering | Emitter-generated HTML processing loops |

**Target shape (illustrative, not frozen signatures):**

```python
from pathlib import Path
from vg2c.runtime import JobRuntime
BASE_DIR = Path(__file__).resolve().parent

def run(workdir=BASE_DIR / "output"):
    job = JobRuntime(assets_root=BASE_DIR, workdir=workdir)
    macros = job.macros
    if (row := job.read_macro_row("configsets.csv")) is not None:
        with macros.scope(row):
            if macros["UNDERDEV"] == "N":
                job.sql("sql/query_046_IPM_Data.sql", reader="sqlite",
                        inputs=["PARMI_IPM_RAW.csv"], output="IPM_Data.csv")
            if job.row_count("IPM_Data.csv") > 0:
                job.html("html/report_055.html", output="result.html")

if __name__ == "__main__":
    run()
```

Use runtime operations named clearly (`job.sql`, `job.html`, `job.write_file`, etc.). The chosen actual API may keep `reader=SqliteReader()` / reader factory for backend-specific options rather than use a string. Avoid gratuitous magic defaults. The explicit first-row guard is purposeful: Python context managers cannot skip a `with` body when CSV has no data. Native `if`/`for`/`else` must remain recognizable, not converted to opaque step objects. `macros.scope` pushes/pops one frame; no per-scope shadow dictionaries in generated code.

## 4. Decisions and rejected alternatives

1. **Scoped macro object:** prefer a single per-job mutable MacroStore with stack behavior, case-normalized dictionary-like reads/writes and `.scope(row)`; reuse current substitution rules. Reject module-global macros, scattered copies `macro_values_39`, or a replacement of missing-token semantics with Python's normal `dict.get` defaults. The current `MacroState` is a reuse candidate, **not** automatically the new authority: compare its `substitute`/`named` against `runtime.values.substitute`, including reserved tokens, leading newlines, scopes, positional reads and errors, before migrating.
2. **One job runtime object:** prefer one initialized instance holding `assets_root`, `workdir`, fresh `values`, `macros`, report state, then thin *meaningful* methods for common operations. Reuse existing direct functions; no service registry, DI container, ambient contextvars, CWD mutation or API wrapper per operation. Public stand-alone direct functions may be retained only for concrete existing users/tests; reconsider unused duplicates at final cleanup.
3. **Data identity before brevity:** keep source SQL column names/aliases and output schema semantics authoritative. Only infer column sets when the operation *actually means all CSV columns in source order*. `SQL /HEADERS` affects the resulting CSV/schema; `HTML-DEFER /COLUMN-HEADERS` affects display labels, not downstream field names. Never equate them. Crosstab row/header/value keys select data transformation and cannot be dropped just because long. Store unavoidable large static specifications adjacent to the relevant SQL/report asset, with a clear edit contract; avoid merely hiding a required contract in an opaque registry.
4. **Pandas selectively:** retain SQLite for actual SQL query execution and joins. Preserve DataFrame-based crosstab and CSV output where correct, improve isolated helpers where semantics benefit, compare outputs against original behavior before any replacement. Do not use `pandas.merge()` to replace arbitrary SQL semantics.
5. **External assets:** preserve already-working SQL/HTML externalization, move static CSS to `.css` assets, preserve CSS embedding when `CSSEMBED` or report delivery requires it. If style directives vary dynamically, emit appropriate per-state assets or retain a small explicit runtime style update; never silently flatten conditional styles. Template read-after-edit and safe escaping remain required.
6. **No false compatibility:** the older `PipelineContext`, `MacroState`, utility emission functions and API metadata are partly referenced by compiler/editor/test code; before deletion, enumerate actual callers. Delete truly redundant paths once evidence shows no supported external consumer, rather than adding pass-through compatibility wrappers. Test invocation-source offsets after any emitter change.

## 5. Session graph / file ownership

| Session | Primary responsibility | Main files (illustrative) | Depends on | Gate |
|---|---|---|---|---|
| **01** Macro state | Runtime MacroStore, scope and substitution; replace numbered maps in generated code | `runtime/values.py`, new/minimal macro module, `emitter/project.py` macro/control fragments, macro tests | Baseline commit | Macro parity and nested control tests pass |
| **02** Job runtime | Establish one per-run context; simplify runtime calls and explicit root resolution | `runtime/__init__.py`, thin context module, `runtime/{query,files,controls,html}.py` as needed, `emitter/project.py` | Commit from 01 | All emitted operations work with no repeated trio of kwargs |
| **03** CSV/table/pandas | Audit table option semantics, shorten safe declarations, maintain SQLite/joins/crosstab behavior | `runtime/{csv_io,crosstab,sqlite_reader,query}.py`, `utilities/{sqlite_engine,_emit_helpers}.py`, `emitter/project.py` | Commit from 02 | Schema, pivot, null/duplicate/ordering goldens pass |
| **04** SQL/HTML/CSS assets | Emit static styles and report assets, improve template structure; retain existing SQL external files | `runtime/{html,html_format}.py`, `utilities/html_report.py`, `emitter/project.py`, relevant tests | Commit from 03 | HTML/style/editability/relocation/Linux paths pass |
| **05** Convergence | Re-run all regressions, audit and remove dead paths, readibility and installed Linux probe | All touched modules + tests/docs, only proven removals | Commit from 04 | End-to-end approval checklist passes |

These sessions must be **serial**, not parallel; the emitter is a shared hot spot. Each agent writes `docs/generated-code-refactor/handoffs/SESSION_0N_HANDOFF.md` with its full parent SHA, test commands/results, changed files and relevant contracts, then reports the **final pushed commit SHA in its final message**, since a tracked handoff cannot include its own resulting SHA. Before coding, the next agent verifies that exact final SHA against the remote implementation-branch HEAD and reads the committed handoff. **Branch flow:** `refactor/generated-code/s01-macros` (01 only) → `refactor/generated-code/implementation` (02 → 03 → 04 → 05, one branch, distinct commits/handoffs). Session 02 reviews and copies only revised Markdown docs/prompts from `origin/html-and-sql-rework` into its implementation branch; do not merge that planning branch or disturb Session 01's changes. Never reset/force-push or overwrite a dirty tree. See [agent prompts](agent-prompts/README.md).

## 6. Shared baseline and regression harness (all sessions)

**At implementation start:** verify `git remote -v`, `git fetch --all --prune`, `git branch -vv`, `git rev-parse HEAD`, `git status --short`, `git merge-base main HEAD`; explicitly report mismatch/dirty tree and preserve unrelated edits. Install project deps with the repo-supported workflow; execute `python -m pytest -q` (or `uv run pytest -q`), `python -m compileall -q src`, and existing Ruff checks if configured. Save baseline results and representational output artifacts in temporary test directories, not committed golden samples containing secrets.

**Behavioral differential harness:** create synthetic but representative VG2 inputs from `tests/fixtures`, including `script_short.txt`, `actual_script.txt`, `sql_script.txt`, report fixtures, plus sanitized, portable cases adapted from `ICMPCS`. Before/after compile to separate directories and execute with the same environment, frozen time/global snapshot, mock external readers/services where necessary, and controlled temporary workdir. Compare output bytes where behavior is specified (SQL text assets, CSV quoting/order/line endings, HTML whitespace only if contract demands), parsed table values and column identity, HTML DOM/text and CSS rules, output paths, side effects and thrown exception types. Always re-load edited SQL/HTML/CSS assets between job runs.

**Required test matrix:** macro named read/write/override/restore; reserved globals and `%ENV%`, unresolved regular macro failures, blank/None, nested and zero-row START-MACRO; native `if`/`else`, numeric comparisons, `FOR`/`SITE`/`RUN` loops and exception behavior; fresh per-run state and concurrent independent jobs; working root vs asset root and absolute/relative/Windows-style paths; SQL reader/node selection, binds and multi-statement guard, `SQL_Get_CSV_List`, CSV headers/renames consumed by later SQL, table aliases and multi-source joins; crosstab pivot and special `CrossTab` SQL syntax; duplicate keys/columns, null/type/case/order/BOM/quoting; deferred HTML reports, escaped data, CSS linked/copied or embedded, conditional style changes, malformed slots; source/editor invocation offsets; direct Python executable without source VG2; installed-wheel import without compiler dependencies; Linux runtime path and filesystem behavior. Ensure tests test both emitted source shape *and* runtime results.

**Existing baseline tests to start with:** `tests/emitter/test_generated_project.py`, `tests/runtime/{test_direct_runtime,test_direct_html,test_control_parity,test_macro_state,test_csv_io,test_e2e_fixtures,test_placeholder_substitution}.py`, `tests/emitter/{test_no_vg2c_leak,test_semantic_contracts,test_emission_metadata,test_editing}.py`; then entire suite. Existing tests may assert old textual spellings: replace with invariant/equivalence assertions rather than weakening behavioral expectations. Unsupported HPC/JMP/JSL tests can remain explicitly `xfail`/diagnostic where already unsupported; do not expand scope.

**Readability scorecard:** establish baseline from `ICMPCS/main.py`: 749 physical lines, 48 `workdir=workdir`, 51 `values=job_values`, 57 numbered macro-name occurrences, 33 `substitute(` calls, 9 style assignment sites. Record equivalent generated output after every session. Aim for zero repeated value/workdir/macro keyword trio in business operations, zero copied numbered macro dictionaries, zero generated CSS-property list blocks, with a substantial reduction in main-file LOC. Track total runtime LOC too: reject a decrease in generated LOC if bought by more duplicate/helper LOC without clarity or justified semantics. No rigid line-count threshold overrides correctness.

## 7. Execution, publishing and approval workflow

**Planning published; workflow updated after Session 01 started on 2026-10-10.** Leave the Session 01 branch, prompt, and running agent untouched. Session 02 creates one shared implementation branch from Session 01's verified result, importing only updated planning Markdown from `html-and-sql-rework` after review. Sessions 03–05 continue that same branch, gated by handoffs and exact pushed SHAs. Never merge into a long-lived branch without explicit approval; the final session verifies earlier architecture and regression tests instead of redesigning them.

## 8. Iterative plan review (planning pass)

- **Pass 1 — feasibility:** original idea of adopting pandas universally rejected because pandas already exists and SQLite semantics matter; current CSS already has a runtime file mechanism; direct runtime remains the required deployment API.
- **Pass 2 — correctness:** distinguished two macro substitution implementations and first-row skip rules; differentiated query output `/HEADERS` from HTML display labels; preserved `CSSEMBED` and dynamic style behavior.
- **Pass 3 — maintainability:** collapsed candidate context manager/service hierarchies to a single per-job object; rejected parallel sessions because all change the same emitter; kept existing `@emittable` metadata/source offset contract and removed premature deletion assumptions.
- **Pass 4 — scope:** excluded UI, unvalidated ScriptHost utilities, HPC/JMP and AED operational expansion; kept explicit environment-independent tests, Linux paths, external file edits and unsupported-feature diagnostics.

**Publication readiness:** Architecture and session plans are approved for publishing; local checkout/status/test re-verification is still mandatory before any implementation. See the five numbered session documents for concrete steps, files and gates.

## 9. Document index

- [01 — Macro simplification](01_MACRO_SIMPLIFICATION.md)
- [02 — Runtime context](02_RUNTIME_CONTEXT.md)
- [03 — CSV, table and pandas](03_CSV_TABLE_PANDAS.md)
- [04 — HTML, CSS and SQL output](04_HTML_CSS_SQL_OUTPUT.md)
- [05 — Final integration](05_FINAL_INTEGRATION.md)
- [Session agent prompts and shared implementation branch workflow](agent-prompts/README.md)