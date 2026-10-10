# Session 03 handoff — canonical ScriptHost pivot (Option C)

**Status:** Canonical pivot refactor and source-grounded regressions implemented; **WIP for exact original-engine parity**.  
**Repository:** `VectorLim/SQLPathFinder_PY_Migration`  
**Branch:** `refactor/generated-code/implementation` only  
**Exact beginning of semantic-authority cleanup:** `32f4fd4d420bf8c69aac32d95ee4b5b27bc7a39f`  
**Implementation checkpoint:** `968463135ddbee7178d10000b6e8d6c3e7b4c396`  
**Older vg2c compatibility oracle (historical only):** `3e9def7a1b066e5007c9284510b242cbf611c7e2`  
**Final tested implementation SHA:** Provided in agent's final response; this file cannot self-reference.

## 1. Semantic authority and result

The **original Python ScriptHost** is the semantic authority. Old vg2c `groupby.first()` and filtering of blank/null headers were vg2c-only deviations and are **no longer requirements**. There is exactly one pivot implementation for both input forms; the old `.crosstab.json` is only an optional configuration-format adapter.

**Original source inspected** (relative to `scripthost-utilities-decompiled/SPSQL3_py/SPFLib/`):

| Original | Source-observable contract |
|---|---|
| `SPFSQL3.py:1609-1614,2442-2447` | `CTVAL/CTVALUE` accepted; QType classification uses CTROW without implying grouping |
| `SPFSQL3.py:2492-2504,2583-2611` | Query result materialized first; normal pivot dispatch doesn't pass CTROW as row keys |
| `SPFUtilities/utils.py:4070-4088` | Grouping = all intermediate SQL result CSV headers except pivot and value columns, matched case-insensitively |
| `utils.py:4118-4122` | Chunk defaults 50k in ScriptHost-entry, 1m otherwise, optional source override |
| `utils.py:4137-4218,4239-4278` | CSV strings, missing values, uppercase and sorted pivot labels, `_UNKNOWN_`, multi-value @/. naming |
| `utils.py:4304-4389,4408-4482` | Positional FIRST/LAST within each chunk; `combine_first()` reconciliation favors earlier chunk's nonmissing cells |
| `utils.py:4514-4549`; `SPFSQL3.py:4509-4777` | CTARRAY numbered INI writes and downstream `CrossTab->[[...]]` SQL expansion |

Source analysis is **not** an executed original-engine differential. Distinguish source-verified code flow, source-derived test expectations, deliberate bug fixes and unverified details; see the revised decisions document.

## 2. Changes in this pass

| File | Change |
|---|---|
| `src/vg2c/runtime/crosstab.py` | Removed `_apply_legacy()` and `groupby.first()`; inferred row identifiers for all modes; old `row_keys` must match inferred; per-50k-chunk FIRST/LAST plus `combine_first` between chunks; one authoritative `unstack` |
| `src/vg2c/runtime/query.py` | Reject unknown old JSON keys rather than silently discarding computational options; preserve old/new API conflict guard and read-after-edit |
| `tests/runtime/test_crosstab_corrective_pass.py` | Replace vg2c-only expectations with ScriptHost-source regression tests: physical first when first is NULL, blank `_UNKNOWN_`, uppercase output, mismatched row-key diagnostics, JSON read-after-edit, FIRST/LAST cross-chunk semantics, empty-fill across chunk boundaries, CTARRAY downstream, header normalization |
| `tests/runtime/test_crosstab_script_host_parity.py` | Change outdated old JSON lowercase CSV assertion to original-source uppercase output |
| `tests/runtime/test_table_semantics_session03.py` | Update prior legacy-only column-name assertions to canonical source uppercase policy |
| `docs/generated-code-refactor/03_CROSSTAB_REVISION_DECISIONS.md` | Rewrite authority, evidence tiers, parity matrix and WIP caveats; supersede old legacy-preservation requirements |
| `docs/generated-code-refactor/handoffs/SESSION_03_REVISION_HANDOFF.md` | This final handoff and verification record |

**Known inputs still accepted:** `crosstab=job.table_spec("...crosstab.json")` with fields `row_keys`, `header_key`, `value_key`. The list of row keys is now a case-insensitive *schema assertion*, **not an override** of inferred row identifiers. A mismatched list (including an empty list where row fields exist), missing input field or unsupported extra option receives an actionable error. JSON is reread every run. The old lowercase header/first-nonnull/blank-filter semantics have been **intentionally removed**.

**Normal Option C emission remains:**

```python
job.sql(
    "sql/query_016_configsets.sql",
    reader=SqliteReader(),
    output="configsets.csv",
    inputs=["ICMPCS_config.csv"],
    pivot_columns="parameter",
    pivot_values="value",
)
```

No new `.crosstab.json` asset and no generated `pivot_rows`. SQL remains independently editable. Existing `table_spec()` support for long `/HEADERS` JSON, `EmittedScript.assets`, `StepEmission`, `EmittedParameter`, source offsets, workdir isolation, SQLite joins/binds/identifiers, SQL_Get_CSV_List, external reader routing and downstream CTARRAY SQL remain under existing suites.

## 3. Old vg2c versus ScriptHost-grounded fixtures

| Fixture | Old vg2c | New canonical result | Evidence |
|---|---|---|---|
| `001,A,NULL` then `001,A,later` | `later` (first non-null) | Blank FIRST result | ScriptHost positional duplicates + `na_filter=False`; source-derived |
| `metric=NULL` or `metric=""` | Rows filtered | Pivot header `_UNKNOWN_` | `utils.py:4205,4368`; source-derived |
| `row_keys=[]` or incomplete | Empty output / altered grouping | Explicit validation error | Source inference `utils.py:4070-4088`; tested |
| `metric="a"` and `"A"` | Potential internal collision | Normalize to one `A`, choose positional FIRST/LAST | Intentional repair of conditional uppercase source defect |
| `row_keys=["lot"]` JSON, nonempty | Lowercase `lot,a` | Source-style uppercase `LOT,A` | Source-derived |
| Source row across 50k boundary with LAST | Global LAST chose later chunk | Earlier chunk nonmissing cell wins | `utils.py:4408-4427`; source-derived chunk reconstruction |
| CTARRAY header list | Original delimiter can mismatch downstream split | Workdir-scoped TAB file and downstream SQL expansion | Intentional repair / subset integration verified |

Historical tests were **updated** because they encoded old vg2c quirks, not because code regressions were permitted. All unrelated passing assertions were retained.

## 4. Verification

**Clean Linux implementation checkpoint:** `968463135ddbee7178d10000b6e8d6c3e7b4c396`  
**CI:** https://github.com/VectorLim/SQLPathFinder_PY_Migration/actions/runs/38071387334  
**Python/pandas:** 3.12 / 3.0.3  
**Results:** compile passed, focused Ruff passed, **135 focused tests passed; full suite 605 passed, six inherited UI/JMP failed**. No unexpected new failures. This is **not** a green full suite.

Exact commands, including checks for emitted executable Python and downstream SQL in focused cases:

```sh
python -m compileall -q src
ruff check --select F821,F823 src/vg2c/runtime/csv_io.py src/vg2c/runtime/crosstab.py
PYTHONPATH=src:. python -m pytest -q tests/runtime/test_table_semantics_session03.py tests/runtime/test_crosstab_script_host_parity.py tests/runtime/test_crosstab_corrective_pass.py tests/runtime/test_csv_io.py tests/runtime/test_direct_runtime.py tests/runtime/test_e2e_fixtures.py tests/runtime/test_job_runtime.py tests/emitter/test_generated_project.py tests/emitter/test_sqlite_table_bindings.py
PYTHONPATH=src:. python -m pytest -q
```

Known inherited six (unchanged; out of scope):

1. `tests/ui/test_document_store.py::test_shared_global_edits_persist_across_steps`
2. `tests/ui/test_document_store.py::test_reorder_persists_execution_order_and_generation_state`
3. `tests/ui/test_document_store.py::test_html_preview_is_safe_exact_approximate_and_path_bounded`
4. `tests/ui/test_html_preview.py::test_html_preview_replays_safely_and_does_not_write_outputs` (JMP/JSL)
5. `tests/ui/test_workspace_sessions.py::test_sql_column_choices_read_uploaded_server_csv_headers`
6. `tests/ui/test_workspace_sessions.py::test_file_backed_sql_filter_uses_workspace_choices_through_save_and_generate`

Synthetic checkpoint diagnostics, not engine parity/performance evidence: 10k pivot 0.086s / 1.6 MiB, 75k pivot 0.569s / 11.9 MiB. No production sample/credential access or original proprietary engine execution. Remote commits and ancestry verified with GitHub API and clean GitHub Actions checkout; local git working-tree state unavailable in this session.

## 5. Remaining WIP and Session 04 gate

- **Chunk variant:** 50k modeled as ScriptHost-entry default. Original standalone default 1m and per-script override not modeled by public configuration. Original multi-frame reconciliation/control flow, especially 3+ chunks and output byte order, must be independently differential tested. The incremental `combine_first` matches the central source precedence rule without copying fragile original loops.
- **Bug-fix divergence:** unconditional pivot-identity uppercase and collision error are deliberate correctness fixes, not original defect replication.
- **Header/missing formatting:** `:M=`, non-ASCII/sanitized header collisions, blank/zero-byte inputs, multi-value col prefix nuances, `PIVOTDOT` and writer formatting need real engine diff.
- **CTARRAY:** numeric alias INI and Y/N/A/placeholder subset tested; TAB normalization intentional; full syntax and Oracle/alternate backend quoting remains unverified.
- **Other:** `/CTROW` missing vs blank, duplicated CTVAL, STACK gate edge cases, unsupported multi CTHEADER, advanced sorts/Hadoop outside normal QType parity.

Do **not** restart a competing legacy pivot mode to satisfy archived tests. Do not alter frontend/UI, AED, HTML, Hadoop/JMP/HPC, original ScriptHost, or other branches. No merge into main, no force update, and **do not start Session 04 automatically**. Before follow-on work fetch and compare the final remote SHA (published with final response).
