# Session 03 handoff — CSV/table semantics and selective pandas

**Status:** WIP — focused checks green, six inherited UI/JMP failures still present.
**Repository:** VectorLim/SQLPathFinder_PY_Migration
**Shared branch:** refactor/generated-code/implementation (no merge)
**Exact Session 02 parent:** 8c0b25212ce4e28de2432a01b28deef25402307c
**Tested Session 03 implementation SHA before handoff:** e1ac1185dfc27e617d6a6e884ea270b8635d28bd
**Final handoff SHA:** In the agent's final response (cannot embed the SHA of this file's own commit).

## Verified start and environment

The previous Session 02 final response gives 8c0b25212ce4e28de2432a01b28deef25402307c. GitHub API verified exact remote match (ahead/behind 0/0), with 11 predecessor commits since Session 01 and correct ancestry. The committed master plan, Session 03 plan, README and Session 02 handoff were read. No merge, branch replacement, force push or destructive cleanup.

Local git fetch and checkout were blocked by DNS resolution for github.com. GitHub API verified the remote SHA and GitHub Actions checked out the exact committed code on a clean Python 3.12 Linux runner. **Local dirty-state verification and local pytest were not possible.**

Fresh pre-implementation full-suite baseline: **545 passed, 6 failed**, matching Session 02's inherited six. No original proprietary ScriptHost or private production ICMPCS differential was available.

## Files and contracts changed

- src/vg2c/runtime/job.py — job.table_spec(path) rereads a JSON list/dict from the bounded project assets root on each invocation. The existing job.sql(...) API still holds reader, node, inputs, bind params, output, header and crosstab explicitly; no ambient state.
- src/vg2c/emitter/project.py — _table_option externalizes an explicit header list of at least 8 names or an explicit crosstab with at least 8 row keys into adjacent sql/query_*.header.json or .crosstab.json. Short options remain inline. The generated call passes header=job.table_spec(...) or crosstab=job.table_spec(...). Project assets remain in EmittedScript.assets, preserving SQL source as independently editable .sql.
- src/vg2c/runtime/sqlite_reader.py — DataFrame construction now uses positional row values and full column-name list, preventing duplicate SELECT labels from silently overwriting earlier values. CSV table inputs reject duplicate/case-colliding/empty headers explicitly; SQLite identifier escaping centralized. SQLite remains the JOIN/SQL execution backend.
- src/vg2c/runtime/csv_io.py — explicit ambiguous source/requested column detection in DataFrame /HEADERS projection. **Legacy case-insensitive by-name projection and blank fields for unmatched requested names remain unchanged**, as existing tests require.
- src/vg2c/runtime/crosstab.py — validate unique pivot key identities, unambiguous casefolded input fields, required fields and unique final output schema. Existing pandas groupby/unstack/first values and CrossTab dynamic SQL substitution are retained.
- tests/runtime/test_table_semantics_session03.py — 15 deterministic new tests: duplicate projection preservation, case-colliding headers, quoted identifiers, downstream SQL on renamed-case CSV headers, leading-zero strings, CSV quoting, joins and aliases, special SQL token expansion, CSV-list expansion, crosstab/null/empty-key cases, large generated JSON and read-after-edit.
- .github/workflows/session03-validation.yml — pushed-commit Linux compile, targeted Ruff, focused/full pytest, generated-code source metrics and deterministic synthetic joins.

No UI/frontend, CSS, HTML report, AED, unsupported HPC/JMP/JSL, main, or planning branch modifications.

## Decision matrix

| Option | Classification / decision |
|---|---|
| /TABLE and aliases | Source identity: keep SQLite CSV/table bindings explicit |
| /CSV output | Downstream file identity: explicit workdir output |
| SQL /HEADERS | Actual CSV projection/order. Case-insensitive source-name matching, with requested output casing. Unknown names remain blank per tests, **not positional renames**. Long options may be moved into JSON |
| Missing /HEADERS | Use runtime result column names and order, including repeated SQL positions; never infer at compile time |
| /CTROW /CTHEADER /CTVALUE | Actual pandas pivot row/header/value identities; retain; long static keys may move to JSON |
| CrossTab->[[...]] | Dynamic SQL projection; preserve existing scanner and alias table-column lookup |
| SQL_Get_CSV_List | Dynamic source read, escaping, deduplication, chunking; retain existing dedicated parser |
| HTML COLUMN-DATA | CSV-to-report projection and display order; left unchanged for Session 04 |
| HTML COLUMN-HEADERS | Display labels, separate from CSV schema identity; Session 04 owns externalization |
| HTML COLUMN-ALIGNMENT | Presentation only; Session 04 CSS/report scope |

Diagnostic compatibility change: ambiguous duplicate/case-colliding SQL CSV columns fail explicitly rather than undergoing silent data loss. Source ScriptHost parity for this edge case remains unverified.

## Representative generated source and readability

Before (inline equivalent of Session 02's schema arguments):
    job.sql('sql/query_000_out.sql', reader=SqliteReader(), output='out.csv', inputs=[], crosstab={'row_keys': ['k0', ... 'k27'], 'header_key': 'metric', 'value_key': 'value'})

After (actual sanitized 28-key output printed in CI):
    job.sql('sql/query_000_out.sql', reader=SqliteReader(), output='out.csv', inputs=[], crosstab=job.table_spec('sql/query_000_out.crosstab.json'))

The complete old inline argument measured **255 characters** versus a compact editable asset call. The 28-key mapping occupies **371 bytes** in the adjacent JSON asset. This **moves** definition complexity from generated Python into a readable editable file; it does not eliminate it. Physical call-line count does not change. A regression test edits the JSON and re-runs the same emitted script without source VG2, confirming asset read-after-edit.

## Executed tests and performance

Clean GitHub Actions workflow run: https://github.com/VectorLim/SQLPathFinder_PY_Migration/actions/runs/38061593400, Python 3.12, installed pandas 3.0.3. Tested source SHA: e1ac1185dfc27e617d6a6e884ea270b8635d28bd.

1. python -m compileall -q src — passed.
2. ruff check --select F821,F823 src/vg2c/runtime/csv_io.py src/vg2c/runtime/crosstab.py — passed (not repository-wide Ruff cleanliness).
3. PYTHONPATH=src:. python -m pytest -q tests/runtime/test_table_semantics_session03.py tests/runtime/test_csv_io.py tests/runtime/test_direct_runtime.py tests/runtime/test_e2e_fixtures.py tests/runtime/test_job_runtime.py tests/emitter/test_generated_project.py tests/emitter/test_sqlite_table_bindings.py — **90 passed**.
4. PYTHONPATH=src:. python -m pytest -q — **560 passed, six failed**, exactly six inherited failures, compared to the 545/6 baseline. No new regression or weakened test assertion.
5. Deterministic synthetic two-file SQLite joins with six-digit literal string IDs, per-run diagnostic wall time and tracemalloc peak (no claimed speedup): 10,000 result rows **0.220 s, 3.3 MiB**; 75,000 rows **1.713 s, 24.6 MiB**. CI-runner-dependent, not production benchmark.

Inherited failures, explicitly out of scope:
1. tests/ui/test_document_store.py::test_shared_global_edits_persist_across_steps
2. tests/ui/test_document_store.py::test_reorder_persists_execution_order_and_generation_state
3. tests/ui/test_document_store.py::test_html_preview_is_safe_exact_approximate_and_path_bounded
4. tests/ui/test_html_preview.py::test_html_preview_replays_safely_and_does_not_write_outputs (JMP/JSL)
5. tests/ui/test_workspace_sessions.py::test_sql_column_choices_read_uploaded_server_csv_headers
6. tests/ui/test_workspace_sessions.py::test_file_backed_sql_filter_uses_workspace_choices_through_save_and_generate

## Session 04 requirements

Continue this **same implementation branch** at the exact final pushed handoff SHA from this session's final answer. Compare that full SHA with fetched origin HEAD before editing. Do not create a new branch, merge or rebase. Preserve JobRuntime, MacroStore, editable SQL/JSON asset paths, reader/node/bind semantics, source offsets and EmittedScript.assets. The SQL output /HEADERS option is *data/schema*, not an HTML column label. HTML COLUMN-DATA, COLUMN-HEADERS and COLUMN-ALIGNMENT remain separately owned by Session 04; no compile-time CSV schema inference. Session 04 must preserve generic runtime report rendering and avoid conflating display headers with CSV names.

Run focused/full suites and do not hide the six inherited failures. This session remains WIP until the shared full acceptance gate and proprietary ScriptHost differential are satisfied.
