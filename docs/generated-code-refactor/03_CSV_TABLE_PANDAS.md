# Session 03 — CSV/table semantics, minimal configuration and selective pandas

**Status:** Plan only. **Parent:** tested commit from Session 02; do not begin from planning-time SHA. **Next:** 04 consumes this session's final emitter/runtime table API.

## Objective / boundary

Remove redundant user-visible CSV/table boilerplate **only** where semantics are derivable, make schema-transforming options clear, and consolidate CSV/data processing sensibly. Preserve existing SQL and SQLite behavior including joins, special SQL token rewrites, DataFrame crosstab behavior, and downstream CSV schema. Avoid a pandas rewrite for its own sake. No CSS/template generation changes (Session 04).

## Source and tests to inspect

- `src/vg2c/emitter/project.py`: `leaf` SQL call construction ~282–329; `html()` / HTML-DEFER `csv_report` ~355–380 (inspect semantics but leave rendering cleanup for Session 04).
- `src/vg2c/utilities/sqlite_engine.py`: `_extract_table_inputs`, `_extract_header`, SQL source; `src/vg2c/utilities/_emit_helpers.py`: `extract_crosstab_options`, `parse_table_binding`; `src/vg2c/utilities/pipeline_context.py` for pre-direct behavior **as reference only**.
- `src/vg2c/runtime/query.py`: `execute_sql`, `run_query`, parameters and `CsvIo.write`; `src/vg2c/runtime/sqlite_reader.py`: SQL queries and CSV-as-table loading; `src/vg2c/runtime/csv_io.py`: `_CsvIO.write`, CSV dialect, header selection and `to_csv`; `src/vg2c/runtime/crosstab.py`: `_CrosstabUtility.apply`, `substitute_sql`; `runtime/sql_text.py`: `SQL_Get_CSV_List` scanning.
- `ICMPCS/main.py`: `query_016_configsets.sql` giant `crosstab={...}`, `query_046_IPM_Data.sql` giant `header=[...]`, `query_047_DATA.sql` one-entry header, deferred report columns/headers/alignment. Read the corresponding `ICMPCS/sql/*.sql` and CSV output samples; do not run network/environment-specific script in real mode.
- Test files `tests/runtime/{test_csv_io,test_direct_runtime,test_macro_state,test_e2e_fixtures,test_write_file_and_readers}.py`, `tests/emitter/{test_generated_project,test_sqlite_table_bindings,test_symbol_resolution,test_semantic_contracts}.py`, `tests/sql_editor/test_operations.py` for SQL syntax tests. Baseline entire suite.

## Important existing semantics and hazards

`pandas==3.0.3` is already required in `pyproject.toml`; `_SqliteReader.execute` returns DataFrames; `_CrosstabUtility.apply` pivots with `groupby(...).unstack(...)`; `_CsvIO.write` already calls `.to_csv` for DataFrames. Joins are executed by SQLite, not pandas, and may use alias-bound multiple CSV sources with actual SQL semantics. CrossTab also has a distinct `CrossTab->[[...]]` **SQL expansion**, not just a pivot. Neither can be dropped because the output column list looks long.

`/HEADERS` from SQL utilities flows through `SqliteEngine._extract_header` into query output; `_CsvIO.write` currently uses a case-insensitive **reindex by requested header names**, then rewrites column labels. This is not automatically the same as positional renaming, and can insert empty output columns if source names differ. Decide source-correct behavior from ScriptHost and current tests before simplifying; never accidentally rewrite schema. HTML-DEFER `COLUMN-DATA` is an explicit projection/order contract; `COLUMN-HEADERS` are display strings with independent escaping; `COLUMN-ALIGNMENT` is presentation. In the inspected `ICMPCS` report, all three are present and 26 distinct report slots appear in the HTML shell.

## Contract decision table — complete before implementation

| Setting | Potential effect | Safe omission test | Owner if retained |
|---|---|---|---|
| `/TABLE` / table alias bindings | Source tables / names in SQLite SQL | Never infer if aliases or paths matter; keep when referenced | Runtime SQL API or sidecar manifest only for large immutable specs |
| `/CSV` SQL output path | Downstream file identity | Do not omit unless exact deterministic naming contract | Business call output |
| `/HEADERS` in SQL | Output CSV schema, order, possible renaming | Omit only if equal to actual resulting DataFrame column identity/order according to tested original behavior | SQL output schema operation, not HTML display |
| `COLUMN-DATA` in HTML-DEFER | Projection and display column order | Omit only if it enumerates **all** actual CSV columns in exactly source order and lacks transformations; runtime must re-read CSV safely | Report/table definition |
| `COLUMN-HEADERS` in HTML-DEFER | Display label; may differ from CSV schema | Omit only if equivalent to tested default labels; never replace downstream column names | Report/table definition |
| `COLUMN-ALIGNMENT` | HTML cell presentation | Omit if all entries truly equal current default and no runtime substitution; else CSS/report spec | CSS/report layer |
| `/CTROW`, `/CTHEADER`, `/CTVALUE` | Actual pivot shape and aggregations | Normally required, cannot derive pivot grouping from arbitrary input file | Query/table operation or compact referenced transformation spec |
| `CrossTab->[[...]]` SQL token | Dynamic SQL projection | Never treat as cosmetic; keep existing substitution behavior | SQLite query helper |
| `SQL_Get_CSV_List` | Dynamic SQL `IN` list and file read | Not plain string replacement; keep guarded scanner | Runtime SQL/text helpers |

Do not automatically load CSV columns to infer the schema at **compile** time: runtime CSV might not exist yet, may be rewritten by prior steps, or have conditional shape. If inference is proven safe, perform it at **runtime** with documented deterministic ordering and strict duplicate behavior. Prefer clear arguments for genuinely meaningful edits instead of opaque sidecars. Large static mandatory crosstab specs may be emitted to a readable adjacent small JSON/TOML file **only if** it improves editability and the runtime avoids per-call lengthy wiring; otherwise keep the explicit SQL operation parameters with clean formatting. Avoid a new general config framework.

## Recommended approach

1. **Do not reimplement SQL joins with pandas.** SQLite already expresses arbitrary `JOIN`, `WHERE`, projection and aggregation. Preserve SQL file as user's editable source of truth.
2. Keep one canonical tabular implementation using DataFrame where it simplifies output/pivot; preserve CSV parsing rules that support legacy delimiter/row semantics. Avoid `pd.read_csv` if it changes literal string IDs, leading zeros, blanks, integer conversion, repeated header handling, case folding or duplicate fields. If using pandas, explicitly test `dtype`, `na_filter`, encoding, and row order.
3. Introduce a *small* report/SQL output schema descriptor **only when needed** and colocate it with its operation; no generic table registry. The runtime must not confuse schema renaming with label formatting.
4. Keep column/row order deterministic. Validate SQL output headers against actual columns, with exact policy (position versus by-name) dictated by differential tests; annotate mismatches with source-located errors, not quiet empty columns unless original contract demands it.
5. Keep `job.sql("sql/...", reader=..., output=..., inputs=..., crosstab=...)` calls lean; if a static crosstab mapping is genuinely large, use a readable asset referenced once and expose that reference in the job. User can edit that asset independently. **Never** omit crosstab row key semantics just to reduce LOC.
6. Session 04 handles final HTML report `columns/headers/alignment` externalization; in this session, settle and test data ownership to avoid a later incompatible report schema.

## Ordered implementation

1. Verify Session 02 commit and statuses; rerun focused SQL/CSV and entire baseline suites. Capture byte-level and parsed-frame results from source fixtures in controlled temp dirs.
2. Trace each option from original VG2/ScriptHost into current compiler and runtime (add source citations to code comments **only when lifting or altering logic**, with exact origin file/lines actually verified); populate decision table per supported option.
3. Add tests for emitted SQL output schema and downstream queries: renamed headers, unchanged headers, positional vs by-name disagreements, duplicate names, case collisions, empty/missing names and output paths. Use a real SQLite query reading the produced CSV.
4. Add tests for crosstab (row key order, pivot column order, nulls, duplicates, numeric/string values, empty frame), special SQL token expansion, joins, multi-source aliases, SQL `IN` expansion, query binds and separators.
5. Simplify existing `runtime/csv_io.py`, `runtime/crosstab.py` and/or `runtime/query.py` *only* at verified duplication points; keep backend SQL query executor and current DataFrame use. No new heavy package.
6. Update `utilities/sqlite_engine.py`, `utilities/_emit_helpers.py`, and SQL paths in `emitter/project.py` only where a safe static default or compact editable manifest has been proven. Preserve output invocation metadata, SQL asset read-after-edit and no-source-VG2 execution.
7. Benchmark representative medium/large synthetic CSV workloads and check memory; do not change behavior to win microbenchmarks. If new pandas usage increases memory or changes schema unexpectedly, retain previous path.
8. Run full tests including tests from Sessions 01–02; record semantic decision matrix, changed files, generated excerpts, new options, and commit SHA for 04.

## Negative cases / acceptance

- A query relying on a renamed CSV column continues to resolve it correctly, both before and after a subsequent query.
- No cross-join equivalence approximations; table aliases bind to correct input paths even when names repeat.
- Leading-zero identifiers remain strings where legacy CSV needs strings; null/NaN/empty handling, quoting, unicode and line endings stay correct.
- Header duplicates and collision policy is deliberate, tested and surfaced; no silent pandas name mangling without equivalence evidence.
- Crosstab requirements remain discoverable in the generated job or a single clearly referenced editable asset, not lost.
- Dynamic CSV columns are never incorrectly assumed from a compile-time snapshot.
- `CrossTab->[[...]]`, `SQL_Get_CSV_List`, backslash path literals and other authored SQL remain correct on supported platforms.
- Main Python readability improves with no increase in unsupported behavior; full suite remains green.

## Explicit out-of-scope and handoff

No generic DAG/frame engine, pandas-only query backend, UI schema controls, SQL editor frontend changes, JDBC/driver expansion, HTML styles or unsupported ScriptHost features. Session 04 receives exact table/report data schema contract, any sidecar file names/format, tests and emitted call examples, parent SHA and any unresolved behavior *with a concrete failing fixture and exact reason*. Do not pass architectural ambiguity to Session 05.