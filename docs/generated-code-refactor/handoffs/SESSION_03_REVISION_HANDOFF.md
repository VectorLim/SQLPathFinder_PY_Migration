# Session 03 revision handoff — ScriptHost-grounded crosstab (Option C)

**Status: WIP — not certified as fully ScriptHost-compatible.** Focused tests pass, and there are no new unexpected full-suite failures. The six inherited UI/JMP failures remain.

**Repository:** VectorLim/SQLPathFinder_PY_Migration  
**Only branch:** refactor/generated-code/implementation  
**Immutable revision start:** 3e9def7a1b066e5007c9284510b242cbf611c7e2  
**Original Session 03 implementation checkpoint:** e1ac1185dfc27e617d6a6e884ea270b8635d28bd  
**First revision commit:** c30c6e34458f7b2b7c1f14bc5aea0806fbe08b2c  
**First corrected/tested commit:** 952689c32fd52a528da472521ff6c1f4739cb64f  
**Expanded corpus and benchmark tested checkpoint:** f53b67e2556dd73f1e4e52cebc79c98e5b69ed5b  
**Exact final handoff commit:** agent final response; a committed file cannot self-reference its own SHA.

See docs/generated-code-refactor/03_CROSSTAB_REVISION_DECISIONS.md for the full source-line audit and behavior matrix.

## 1. Original source audit

Inspect locations in scripthost-utilities-decompiled/SPSQL3_py/SPFLib/:

| Original source | Actual inspected behavior | Revised contract / evidence |
|---|---|---|
| SPFSQL3.py:1609-1614 | CTVAL and CTVALUE both accepted | Compiler supports both; SOURCE-DERIVED |
| SPFSQL3.py:2442-2447 | QType requires CTVal, CTHeader, CTRow non-None; STACK wins | CTROW classification gate, not grouping; SOURCE-DERIVED |
| SPFSQL3.py:2492-2504,2583-2587 | SQL result written before pivot | Group by actual post-query result schema; SOURCE-DERIVED |
| SPFSQL3.py:2604-2611 | Normal pivotTable does not forward CTRow | No pivot_rows emitted; SOURCE-DERIVED |
| SPFUtilities/utils.py:4070-4088 | Row keys are all intermediate CSV columns other than header/value | Runtime inference; SOURCE-DERIVED |
| utils.py:4137-4218,4239-4278 | Multiple value names, uppercase sorted labels, missing value modifier, legacy header formatting, DOT option | Explicit optional parameters; SOURCE-DERIVED |
| utils.py:4280-4512 | Per-chunk positional first/last before unstack; then combine_first merges chunks | Global physical de-dup tested; cross-chunk fidelity WIP |
| utils.py:4514-4549; SPFSQL3.py:4509-4777 | CTARRAY writes numbered alias INI; later CrossTab expands Y/N/A and optional expression placeholder | Scoped INI metadata, separate SQL substitution; subset SOURCE-DERIVED |

Original source was inspected via GitHub API, not executed as the proprietary engine. Hadoop pivotTable4 passes CTROW and was explicitly left out of normal-query implementation. The original header-list writer uses target delimiter (utils.py:4214-4218) but its downstream consumer splits tabs (SPFSQL3.py:4554,4666); we intentionally write tabs for a functional downstream bridge, not original bytes.

## 2. Implementation ownership and changed files

| Path | Why |
|---|---|
| src/vg2c/utilities/_emit_helpers.py | Source-backed option aliases and QType detection; concise pivot arguments |
| src/vg2c/emitter/project.py | New Option C SQL calls; no newly generated crosstab JSON; preserve header JSON |
| src/vg2c/utilities/sqlite_engine.py | Consistent utility emitter argument forwarding |
| src/vg2c/utilities/pipeline_context.py | Emittable signature/semantic-editor metadata for new pivot arguments |
| src/vg2c/runtime/job.py | JobRuntime.sql concise args and backward-compatible JSON reloading/validation |
| src/vg2c/runtime/query.py | One pivot dispatch, SQL order, scoped metadata, explicit readers/binds and validation |
| src/vg2c/runtime/crosstab.py | Canonical pandas pivot with inferred rows; duplicate/missing/header/sort and dynamic token helpers |
| src/vg2c/runtime/sqlite_reader.py | Reuse safe quoted identifier for SQLite INSERT |
| tests/runtime/test_table_semantics_session03.py | Update previous new-JSON emitter expectation to real Option C editability |
| tests/runtime/test_crosstab_script_host_parity.py | Golden, aliases, legacy JSON, metadata, CTARRAY and workdir isolation regressions |
| tests/fixtures/crosstab_parity/README.md and two CSV files | Independently authored golden provenance and source-derived CSV bytes |
| .github/workflows/session03-validation.yml | Focused regression suite and synthetic join/pivot profiling |
| docs/generated-code-refactor/03_CROSSTAB_REVISION_DECISIONS.md | Semantic matrix, risks, follow-up acceptance |
| docs/generated-code-refactor/handoffs/SESSION_03_REVISION_HANDOFF.md | This handoff |

No UI/frontend, CSS/HTML, Hadoop/HPC/JMP/JSL, unrelated AED, original ScriptHost files, production ICMPCS, main or planning branch changes.

## 3. Public generated code and compatibility

Before:

    job.sql('sql/query_000_out.sql', reader=SqliteReader(), output='out.csv',
            crosstab=job.table_spec('sql/query_000_out.crosstab.json'))

After, verified from clean Actions:

    job.sql('sql/query_000_out.sql', reader=SqliteReader(), output='out.csv',
            inputs=[], pivot_columns='metric', pivot_values='value')

The sanitized 28-key source now emits a 130-character call and ZERO new .crosstab.json assets. Pivot keys derive from query-result columns in runtime, not compile-time inferred schema. The user edits Python pivot_columns/pivot_values and the separate .sql asset independently.

Existing job.sql signature retains reader, output, inputs, header, crosstab, node, params. Extra optional arguments: pivot_columns=None; pivot_values=None (str or list); pivot_duplicate='first'; pivot_missing=''; pivot_dot=False; pivot_sort=None; pivot_header_ref=None; pivot_legacy_headers=False. New options emitted only when input options require them.

Old crosstab=job.table_spec('*.crosstab.json') remains runnable, with its original explicit row_keys grouping choice. Old table_spec still rereads edited JSON; long .header.json assets still work. The old explicit row_keys list is not converted into inferred keys. Mixing new/old pivot APIs is rejected. Invalid JSON includes file-path context; header and legacy crosstab structures are validated. Legacy empty result retains user-specified row-field case.

The pandas pivot is implemented once, in runtime/crosstab.py. JobRuntime is a thin per-run facade with isolated workdir and project asset root. Native Python control flow and emitted source offsets are preserved. Query SQL, SQL_Get_CSV_List, external reader node, bind params, SQLite CSV joins, CSV /HEADERS projection and HTML presentation remain distinct mechanisms.

CTARRAY flow: A pivots SQL result and writes <instance>_<alias>.ini within its workdir; B reads it at SQL substitution time to expand CrossTab expressions. Basic numeric reference modes Y/N/A, a SQL function |<>| placeholder, and separate workdirs are regression tested. Old SQLite alias-table introspection token expansion is retained only as a backward-compatible separate fallback.

## 4. Test evidence / exact commands

**Latest source CI checkpoint:** f53b67e2556dd73f1e4e52cebc79c98e5b69ed5b  
**Actions:** https://github.com/VectorLim/SQLPathFinder_PY_Migration/actions/runs/38068009824  
**Runner:** Linux, Python 3.12.15, pandas 3.0.3.

    python -m compileall -q src
    ruff check --select F821,F823 src/vg2c/runtime/csv_io.py src/vg2c/runtime/crosstab.py
    PYTHONPATH=src:. python -m pytest -q tests/runtime/test_table_semantics_session03.py tests/runtime/test_crosstab_script_host_parity.py tests/runtime/test_csv_io.py tests/runtime/test_direct_runtime.py tests/runtime/test_e2e_fixtures.py tests/runtime/test_job_runtime.py tests/emitter/test_generated_project.py tests/emitter/test_sqlite_table_bindings.py
    PYTHONPATH=src:. python -m pytest -q

Results: **compile passed, focused Ruff passed, 107 focused passed; full suite 577 passed and six failed.** Exact same six inherited failure IDs as original Session 03 (historical run 38061593400: 560 passed, six failed). This is a failed full suite, NOT green. No tests skipped or assertions weakened to hide regressions.

First revision c30c6e... CI had two extra failures: legacy empty crosstab capitalization and source-derived test's wrong downstream SQLite column-case expectation. Both fixed at 952689c... (103 focused passed; 573 passed and six inherited failures), then expanded source fixture tests at f53b67... (107 focused passed; 577 passed, six inherited failures).

Six inherited failures, not fixed in revision:
1. tests/ui/test_document_store.py::test_shared_global_edits_persist_across_steps
2. tests/ui/test_document_store.py::test_reorder_persists_execution_order_and_generation_state
3. tests/ui/test_document_store.py::test_html_preview_is_safe_exact_approximate_and_path_bounded
4. tests/ui/test_html_preview.py::test_html_preview_replays_safely_and_does_not_write_outputs (JMP/JSL)
5. tests/ui/test_workspace_sessions.py::test_sql_column_choices_read_uploaded_server_csv_headers
6. tests/ui/test_workspace_sessions.py::test_file_backed_sql_filter_uses_workspace_choices_through_save_and_generate

Synthetic Linux runner diagnostics, not production performance evidence:
- SQLite join 10,000 rows: 0.406 s, tracemalloc 3.3 MiB; 75,000: 3.212 s, 24.6 MiB.
- Pandas pivot 10,000 rows: 0.078 s, 1.2 MiB; 75,000: 0.548 s, 8.5 MiB.

The generated Python was executed without VG2 source or original ScriptHost import. Golden fixture expected text was authored independently of the new function, with SOURCE-DERIVED label. Test covers semantic editing through project_changes with correct binding source ranges, emitted Python read-after-edit, legacy table_spec JSON read-after-edit, and per-workdir isolation.

## 5. Remaining parity blockers and concrete reproductions

**HIGH: cross-chunk first/last.** Original utils.py:4280-4512 de-duplicates within chunks, then merges repeated group indices using combine_first. New code globally drop_duplicates, without a source-matching chunk boundary policy. Repro: source row 0 is lot=001,metric=A,value=first, and row 50000 is lot=001,metric=A,value=last; use PIVOT_FUNCTION=LAST and a 50,000-row original chunk size. Original first chunk's nonempty value may win at combine_first while new global last selects last. Requires independently executing/reconstructing original code and byte-comparing results before declaring parity.

**HIGH: CTARRAY/SQL dynamic header list.** Original .csv versus .tab delimiter behavior conflicts; this revision intentionally writes tabs. More elaborate original CrossTab optional flags/expression syntax, alias cases, quoting for non-SQLite readers and header file encoding/creation-time failure semantics remain UNVERIFIED. Y/N/A and basic expression subset is SOURCE-DERIVED/tested, not original executable parity.

**MEDIUM/HIGH: data corners.** Original handling of empty vs missing CTROW, duplicate CTVAL spelling precedence, zero-byte/header-only data, blank pivot labels, unknown/NaN values, output header collisions/legacy sanitization, PIVOTDOT details, multi-column CTHEADER, DESC-1 types, multi-batch output/ordering and external Oracle results needs differential fixtures. Multi-column CTHEADER explicitly rejects, rather than silently implementing guesswork.

**VERIFICATION:** Proprietary ScriptHost was never executed or compared with production output. There is no full exact ScriptHost parity claim; WIP acceptance gate remains open.

Source files were read from remote. Local Git DNS blocked clone/dirty state inspection. Every remote update used a single-parent commit and non-force ref update with exact expected SHA, checked remote compare first. Clean Linux Actions checked out each pushed checkpoint SHA. No merge/rebase/force push or shared-branch overwrite.

## 6. Handoff and Session 04 constraints

- Preserve concise pivot args, no new crosstab JSON, long SQL /HEADERS JSON, preexisting legacy crosstab JSON and user-edited SQL assets.
- Keep source-edit binding metadata, EmittedScript.assets, StepEmission, runtime macros, JobRuntime isolation, reader/node/binds and file roots.
- Keep CTARRAY output metadata separate from data pivot and HTML; do not treat SQL /HEADERS as HTML display labels.
- Keep six inherited UI/JMP failures and all above parity gaps explicitly WIP; never claim exact original semantics without an authoritative differential.
- Do not start Session 04 or merge to main during this revision. Next agent must fetch exact final handoff SHA in the final response, verify remote equality and stop on competing branch movement.

Security and data review: synthetic fixtures only, no production customer/site inputs, no credentials, no original proprietary code modified, no network shares or production ICMPCS scripts executed.
