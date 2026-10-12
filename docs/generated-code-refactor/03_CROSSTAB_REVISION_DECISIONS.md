# Session 03 crosstab — ScriptHost semantic authority

**Repository:** `VectorLim/SQLPathFinder_PY_Migration`  
**Only implementation branch:** `refactor/generated-code/implementation`  
**Current pass starts at:** `32f4fd4d420bf8c69aac32d95ee4b5b27bc7a39f`  
**Implementation checkpoint:** `968463135ddbee7178d10000b6e8d6c3e7b4c396`  
**Original pre-Option-C source for historical comparison:** `3e9def7a1b066e5007c9284510b242cbf611c7e2`  
**Parity status:** **WIP; source-audited and tested, not independently executed against proprietary ScriptHost.**

## 1. Authority and design

The original **Python ScriptHost** is the sole authority for expected VG2 crosstab semantics. An old vg2c implementation is *not* a competing behavioral contract. Preserve previously generated JSON **input formats** only when they map unambiguously to ScriptHost semantics; reject misleading or contradictory old options with an actionable diagnostic.

Normal generated Python remains concise; no row-key lists, Python pandas internals or new `.crosstab.json` assets are emitted:

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

The SQL is edited separately. `JobRuntime.sql -> run_query -> _CrosstabUtility.apply -> CsvIO.write` owns one pivot algorithm. `/CTROW` participates in original QType=3 classification but does **not** supply emitted row identifiers for normal SQL. Row identifiers are inferred from actual SQL result fields excluding CTHEADER and CTVAL.

For an older generated project, `crosstab=job.table_spec("...crosstab.json")` still loads and rereads the JSON. The known `row_keys`, `header_key`, `value_key` shape is accepted. `row_keys` is now a **schema assertion**, not a grouping override: it must match the inferred post-query grouping names and order (case-insensitive), otherwise the runtime raises an error explaining how to update the JSON or switch to Option C. Unsupported JSON fields and mixed old/new pivot APIs are rejected. No public migration parameters or legacy engine were added.

## 2. Original source citations and evidence tiers

Source root: `scripthost-utilities-decompiled/SPSQL3_py/SPFLib/`.

**Confirmed by inspected original source (not proprietary execution):**

- `SPFSQL3.py:1609-1614`: `CTVAL` and `CTVALUE` parsing; `CTHEADER`/`CTROW` parsed separately.
- `SPFSQL3.py:2442-2447,2492-2504,2583-2587,2604-2611`: QType pivot classification, SQL before pivot, and normal-query dispatch without explicit CTROW grouping. Hadoop/alternate pivot route is not equivalent.
- `SPFUtilities/utils.py:4070-4088`: grouping keys = intermediate CSV headers excluding pivot/value fields, matching column names case-insensitively.
- `utils.py:4118-4122`: ScriptHost-entry default pivot read chunk size **50,000**, standalone/non-SH default **1,000,000**; explicit original `pivotReadChunkSize` may override. The present vg2c API has no equivalent runtime-mode or override signal and uses 50,000; standalone/overridden chunk parity is **WIP**.
- `utils.py:4137-4218,4239-4278`: intermediate CSV values, value-column lists, sorted uppercase pivot headers, blank names `_UNKNOWN_`, sanitization, `PIVOTDOT`, `:M=` and sort option handling.
- `utils.py:4304-4389`: chunk-local positional index duplicate selection (FIRST/LAST), followed by `unstack()`.
- `utils.py:4408-4482`: overlap is reconciled using `combine_first()`, which keeps an earlier chunk's non-null cell even for LAST duplicates across chunk boundaries.
- `utils.py:4514-4549` and `SPFSQL3.py:4509-4777`: CTARRAY creates numbered alias INI metadata, consumed by downstream SQL `CrossTab->[[...]]` substitution.

**Source-derived and covered by clean Linux regression tests, not original differential:** runtime inferred keys, FIRST/LAST physical selection inside 50,000-row chunks, `combine_first` for overlapping pivots, fill of disjoint pivot columns, uppercase output ordering, multiple values with @ or ., `_UNKNOWN_` for blank pivot identities, CTARRAY file and downstream SQLite expansion, case-insensitive field matching, and JSON input adaptation.

**Intentional ScriptHost bug fixes / divergences:**

1. `utils.py:4199-4212` contains `len(pivotedHeaderNamesList) != set(pivotedHeaderNamesList)`, comparing an integer to a set; the condition does not correctly test duplicate labels. `utils.py:4304-4346` uppercases source pivot identities only when another collision flag is set. For `a` and `A`, two distinct pivot indices can be emitted with the same uppercase output name. vg2c **always uppercases pivot identities before physical FIRST/LAST selection** and raises on still-ambiguous sanitized/output collisions. This is a documented modern correctness repair, not claimed exact original bug reproduction.
2. Original CTARRAY writes its list using the target CSV delimiter while a downstream consumer expects TAB (`utils.py:4214-4218`, `SPFSQL3.py:4554,4666`). vg2c writes TAB-delimited, workdir-scoped metadata to keep the documented downstream SQL pattern functional and isolated.
3. Original headers may silently collapse in some collision branches. vg2c rejects ambiguous row/output identifiers rather than silently lose data. Exact exceptional-case equivalence remains unverified.

**Optional old-vg2c format compatibility:** `JobRuntime.table_spec()`, JSON shape reloading, case-insensitive legacy field names, numeric CTARRAY tokens and separately existing alias-schema fallback. These interfaces are **not** licenses to preserve old computational semantics.

**Removed vg2c-only behavior:** `_apply_legacy()`, `groupby(...).first()` (first non-null), old filtering of null/blank pivot header records, lowercase legacy output headers, empty `row_keys=[]` silently disabling pivot, explicit user row keys changing grouping, and alternative legacy-only output ordering. Historical assertions enforcing these were intentionally updated to source-backed results.

## 3. Semantic comparison

| Input / policy | Pre-Option-C vg2c | Current ScriptHost-grounded vg2c | Confidence |
|---|---|---|---|
| Duplicate A: first value NULL, next later | `later` via first non-null | Empty first physical value (`na_filter=False` semantics) | SOURCE-DERIVED / tested |
| Pivot header NULL or empty | Drop affected rows | Materialize blank string and `_UNKNOWN_` header | SOURCE-DERIVED / tested |
| Explicit JSON `row_keys` smaller/reordered | Changes selected grouping | Reject mismatch; infer grouping from result | SOURCE-DERIVED / tested |
| Output header `a` from JSON | Lowercase | Uppercase `A` | SOURCE-DERIVED / tested |
| Pivot values `a`, `A` | Separate/conditional quirks | Single `A` identity, physical FIRST/LAST | INTENTIONAL BUG FIX / tested |
| Duplicate row/pivot straddles SH 50k boundary, LAST | vg2c global LAST chose later row | Earlier chunk non-null wins by `combine_first` | SOURCE-DERIVED; exact original multi-chunk executor WIP |
| New generated SQL call | Legacy JSON asset | Concise `pivot_columns/pivot_values` | CODE-GENERATION TESTED |
| Old JSON schema with unknown keys | Inconsistent handling | Explicit diagnostic | INPUT VALIDATION / tested |
| CTARRAY mixed case / blank header downstream | No complete source parity | Sorted header INI, numeric token expansion | SOURCE-DERIVED / tested subset |

## 4. Remaining parity limitations

- **No independent proprietary engine execution.** Old source inspection and independently authored fixtures do not prove byte-identical behavior.
- **Cross-chunk:** 50k models ScriptHost-entry defaults, not standalone 1m or custom chunk size. The source has a more elaborate multi-DataFrame merging/writing loop; additional 3+ chunk and original-engine byte-level comparisons remain WIP, including output ordering and memory behavior. The implementation uses incremental `combine_first` rather than reproducing questionable original control flow.
- **Null/missing:** Source read uses `na_filter=False`; pandas/SQLite output string conversion, `:M=` and writer formatting need additional original differential, especially external-backend NULL representation.
- **CTARRAY:** Alias casing, delimiters, Y/N/A and basic `|<>|` expressions have source-derived integration tests; full dynamic SQL token syntax, advanced expressions, metadata encoding, and cross-backend behavior remain WIP.
- **Header/collisions:** uppercase normalization is an intentional fix. Unusual whitespace, invalid/sanitized labels, `PIVOTDOT` details and multi-value header collision cases require independent engine comparisons. Multiple CTHEADER entries remain explicitly unsupported.
- **Classification and SQL:** missing-versus-blank CTROW, duplicated CTVAL options, STACK precedence edge cases, external Oracle quoting, advanced sort syntax and zero-byte/header-only input differences are not certified.

## 5. Verification and Session 04 constraints

**Final tested semantic-authority checkpoint:** `5323b232ba887b552bfa1cc30f050eaec8537649`  
**CI:** https://github.com/VectorLim/SQLPathFinder_PY_Migration/actions/runs/38071668466  
**On clean Linux, Python 3.12 / pandas 3.0.3:** compile and focused Ruff passed, **136 focused passed; 606 full-suite passed, six inherited UI/JMP failed** (zero new failures). The workflow also executes generated Python and benchmarks 10k/75k row pivots and SQLite joins. The full workflow is **failed**, not green.

Exact commands:

```sh
python -m compileall -q src
ruff check --select F821,F823 src/vg2c/runtime/csv_io.py src/vg2c/runtime/crosstab.py
PYTHONPATH=src:. python -m pytest -q tests/runtime/test_table_semantics_session03.py tests/runtime/test_crosstab_script_host_parity.py tests/runtime/test_crosstab_corrective_pass.py tests/runtime/test_csv_io.py tests/runtime/test_direct_runtime.py tests/runtime/test_e2e_fixtures.py tests/runtime/test_job_runtime.py tests/emitter/test_generated_project.py tests/emitter/test_sqlite_table_bindings.py
PYTHONPATH=src:. python -m pytest -q
```

Do not change `EmittedScript.assets`, `StepEmission`, `EmittedParameter`, semantic editing ranges, editable SQL assets, workdir isolation, SQLite joins/bind parameters, quoted identifiers, duplicate CSV header diagnostics or `/HEADERS` behavior without their focused tests. Keep original source unmodified. No Session 04, main merge or unrelated UI/AED/HTML/HPC/JMP work in this pass. Original historical docs on the prior handoff commits remain in Git history; **this document supersedes their legacy-only requirements**. The final handoff commit SHA is provided in the agent's final response, as a commit cannot contain its own SHA.
