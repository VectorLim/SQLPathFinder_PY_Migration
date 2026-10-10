# Session 03 revised crosstab — source-grounded decisions and residual implementation plan

**Repository/branch:** `VectorLim/SQLPathFinder_PY_Migration` / `refactor/generated-code/implementation` only.
**Revision status:** **WIP: source-grounded and focused-tested, but not exact original-engine parity.**
**Starting full SHA:** `3e9def7a1b066e5007c9284510b242cbf611c7e2`.
**Original tested Session 03 SHA:** `e1ac1185dfc27e617d6a6e884ea270b8635d28bd`.
**No merge into main; Session 04 not started.**

This document reconciles the user-approved Option C revision plan with actual
code, testing, and the audited original decompiled implementation. The original
prompt/plan were provided out-of-band. Evidence strength matters: passing
source-derived fixtures is not the same as executing ScriptHost.

## 1. Core design and ownership

A normal query emits one editable Python call, for example:

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

Only `pivot_columns`/`pivot_values` are required for normal pivots. Extra
arguments only appear when source options require them; SQL remains a separate
editable file. `JobRuntime.sql -> execute_sql/run_query ->
_CrosstabUtility.apply -> _CsvIO.write` retains one canonical pivot algorithm.
The runtime infers grouping fields *after* the SQL query returns. It excludes
the pivot-header and value fields from the actual result-column sequence. It
never serializes 28 inferred keys or emits new `.crosstab.json` assets.

Keep existing `job.sql(..., crosstab=job.table_spec(...))` readable for
deployed projects; its user-authored `row_keys` have different explicit
grouping semantics and remain explicitly applied. Mixed old/new settings
raise instead of arbitrarily taking precedence. Keep `.header.json` and
`JobRuntime.table_spec()`; validate edited assets at consumption. Do not
turn SQL `/HEADERS` into HTML column labels.

Distinct runtime metadata flow:

```text
Query A SQL -> query-result pivot -> pivoted CSV
                           |-> optional CTARRAY <instance>_<alias>.ini
Query B editable SQL with CrossTab->[[...]]
           -> read exactly that workdir's header INI
           -> expand SQL expressions -> SQL execution
```

No ambient registry/CWD and no duplicate pivot implementation. Retain the
older SQLite schema-based token expansion as a separate *legacy* compatibility
path, not as evidence of ScriptHost CTARRAY equivalence.

## 2. Audited original call chain

All pointers are to `scripthost-utilities-decompiled/SPSQL3_py/SPFLib/`
at the original checked-out source. File/line references are source-audit
locations, not claims of proprietary execution.

1. `SPFSQL3.py:1609-1614`: `CTVAL` and `CTVALUE` both assign CTVal;
   `CTHEADER`, `CTROW` are separate.
2. `SPFSQL3.py:2442-2447`: QType=3 when CTVal, CTHeader, CTRow are
   non-None and STACK doesn't supersede; empty option handling needs
   additional exact-parser verification.
3. `SPFSQL3.py:2492-2504,2583-2587`: execute SQL first, pivot
   intermediate query result only for QType=3.
4. `SPFSQL3.py:2604-2611`: normal `pivotTable()` does **not** forward
   CTRow as group keys to `utils.PivotTable`; Hadoop `pivotTable4`
   differs and is out of scope.
5. `SPFUtilities/utils.py:4070-4088`: `getMyRowListFrmSourceFile()`
   subtracts pivot header/value identities from CSV headers.
6. `utils.py:4088-4218,4239-4278`: reads CSV values as strings,
   derives uppercase, sorted pivot header names, supports multiple
   value columns, `:M=`, `PIVOTDOT`, legacy sanitized headers,
   and optional `SORT`.
7. `utils.py:4280-4512`: pandas read with `dtype=object`,
   `na_filter=False`, physical duplicated-index keep=first/last
   before unstack; chunk outputs reconciled by `combine_first()`.
8. `utils.py:4514-4549`: `Save_CT_Header` writes
   `<instance>_<alias>.ini`; `SPFSQL3.py:4509-4777` expands
   later SQL `CrossTab->[[...]]` tokens with Y/N/A alias modes
   and optional `|<>|` SQL expression.
9. `SPFSQL3.py:4554,4666` splits CTARRAY metadata on TAB, while
   `utils.py:4214-4218` writes using target CSV delimiter, so
   `.csv` target can trigger a source-consumer delimiter mismatch.

The old source has defects (e.g. comparing list length to a `set` in
header de-duplication). Preserve externally observable policy only
where evidenced; do not copy broken conditionals uncritically.

## 3. Behavioral inventory (evidence status)

| Behavior | Audited original | Current revision / gate | Evidence level |
|---|---|---|---|
| CTVAL/CTVALUE | Both accepted, SPFSQL3:1609 | Both emitted, test parametrized | SOURCE-DERIVED |
| QType /CTROW | Required gate, not runtime row list | Emit only when parsed CTROW present, no pivot_rows | SOURCE-DERIVED; blank ambiguity WIP |
| STACK precedence | STACK may win over pivot | Extractor checks presence | SOURCE-DERIVED; extra gate fixtures WIP |
| Runtime row keys | All query-result CSV fields except pivot/value | Infer at runtime preserving column position | SOURCE-DERIVED |
| SQL first | Intermediate result materialized | Query execute precedes pivot | SOURCE-DERIVED; exact serialization WIP |
| Legacy row keys | Old vg2c crosstab explicit group list | Dedicated adapter keeps them | CURRENT-VG2C-ONLY |
| Default first/last | Positional index dedup | Physical `drop_duplicates`, first/last | SOURCE-DERIVED for single in-memory batch |
| Cross-chunk first/last | `combine_first` per chunk | Global dedup, no source-compatible chunk strategy yet | **UNVERIFIED/WIP** |
| Missing/null | `na_filter=False`, `:M=` | Coerce normal intermediate values to strings; missing filling | SOURCE-DERIVED; writer specifics WIP |
| Multi-value | CTVal comma list, @/. labels | One or many pivot values, header formats | SOURCE-DERIVED |
| Header case/order | Uppercase, sorted, optional sanitization | Deterministic uppercase header policy | SOURCE-DERIVED; collisions WIP |
| Multi CTHEADER | Partial split but single usecols lookup | Explicit diagnostic | UNVERIFIED, not implemented |
| Sort | ASC/DESC/DESC-1 | Narrow list parser, stable sort & numeric shadow | SOURCE-DERIVED; complete original syntax WIP |
| Empty/header-only | Original zero-byte vs no data distinction | New/legacy modes have explicit schema paths | UNVERIFIED exact outputs |
| CTARRAY persistence | Numbered alias INI in CWD | Scoped workdir INI, TAB-separated | INTENTIONAL-CHANGE; needs differential |
| SQL CrossTab token | Read INI, Y/N/A and optional SQL function placeholders | Scoped expansion for numeric tokens | SOURCE-DERIVED subset; expressions WIP |
| Old alias token | Existing vg2c introspects table aliases | Preserved in distinct fallback | CURRENT-VG2C-ONLY |
| SQL /HEADERS | Actual output CSV/schema | Existing header-json by-name behavior retained | CURRENT-VG2C-ONLY |
| SQLite identifiers | Quote CREATE/DROP, INSERT inconsistent | INSERT now shares `_quote_identifier` | Verified by focused regression |
| Repeated result labels | Positional results preserved | Keep; later CSV reload diagnoses collisions | CURRENT-VG2C-ONLY |
| Editing contract | Emitter metadata references arguments | New pivot argument editing/ranges tested | VG2C integration test |
| Python runtime | Needs no VG2 original | Generated project executed in offline Linux CI | VG2C integration test |
| Oracle/Hadoop/production | Different execution branches | Not executed or supported by this revision | UNVERIFIED/out of scope |

**No row marked SOURCE-DERIVED is certified exact original executable parity.**
A true differential requires independently executing a compatible original
isolated method or proprietary test oracle; neither was used here.

## 4. Implementation checkpoints

- Phase 0: GitHub API verified exact implementation branch HEAD and ancestry.
  GitHub Actions clean checkout used because local Git DNS blocks checkout;
  local dirty-tree verification unavailable.
- Phase 1: Parser/QType -> SQL result -> PivotTable -> pivotDF -> CTARRAY ->
  SubStitute_CT audited, and source/exception notes recorded above.
- Phase 2: Independently authored goldens and executable test corpus under
  `tests/fixtures/crosstab_parity/` and
  `tests/runtime/test_crosstab_script_host_parity.py`.
- Phase 3: Canonical runtime pivot and compatibility adapter implemented.
  **WIP** for chunk merging, unknown original data-type anomalies.
- Phase 4: Concise emitter/API and editability tests; no new pivot JSON.
- Phase 5: Old JSON asset loading retained; enhanced diagnostics,
  SQLite INSERT quoting repair, duplicate schema contract kept.
- Phase 6: File-backed CTARRAY and downstream numeric header SQL token
  support implemented, including Y/N/A and optional expression. **WIP**
  for full source syntax, cross-backend quoting, delimiter differences.
- Phase 7: Source/workflow checks run in clean CI, performance diagnostic
  logged; no original ScriptHost oracle; six inherited full-suite failures.
  The revision is **WIP**, not an acceptance-certified source-parity release.

## 5. Must-complete before claiming ScriptHost parity

1. Build a bounded independently executed original `PivotTable`/`pivotDF`
   differential on synthetic text with pinned older pandas (if runnable)
   and compare byte output, original csv/tab split, and 50k-row chunk
   boundaries for first/last.
2. Resolve `/CTROW` blank vs missing, duplicate `CTVAL` aliases,
   multi-value/missing-value edge cases, header collisions, zero-byte/
   header-only output behavior from executable reference.
3. Reconcile actual source CTARRAY filename casing, alias/instance naming,
   newline/delimiter behavior and optional aggregate expressions in
   SQLite versus Oracle. Add separate unsupported-engine diagnostics.
4. Add full writable/editable semantic parameters for any future
   verified advanced options; preserve current `EmittedScript.assets`,
   `StepEmission`, source offsets, and legacy project data.
5. Re-run full suite, classify all failures exactly; no UI/JMP changes
   as part of Session 03, and don't treat six inherited failures as green.
6. Keep the shared branch serial: Session 04 must not start until a
   deliberate handoff accepts these bounded WIP items.

## 6. Scope, security and precedence

No production/network-share ICMPCS execution; reference ICMPCS was read
only. No changes to UI, frontend, HTML/CSS, Hadoop/HPC, JMP/JSL, AED,
main, or planning branches. No secrets or production sample rows in
fixtures. Workdir and assets remain explicit. Any unexpected shared
branch movement must stop work rather than override another writer.

## Corrective pass — mixed-case identities and pre-revision JSON compatibility

**Checkpoint predecessor:** `1ae547e4b6f8cc70e875a2881dc5f4482c06f43b`. Test-first regression checkpoint: `25ebfa84aa1b1decb6956b8a2580cfe32d34d7a6`.
The clean CI on regression-only code reproduced 18 new corrective failures (24 total, including six inherited) before runtime modification. Corrective implementation checkpoint: `fbff792ee4cf3a70e51e2c5e687455ea999ac3d5`. The source-only runtime correction was deliberately confined to `runtime/crosstab.py` and the focused workflow includes the new regression file.

### What the original source actually does

- `SPFUtilities/utils.py:4137-4218` uppercases *output labels*, computes an uppercased/sorted header shell and contains the defective `len(list) != set(list)` condition. The deduplication block is therefore entered regardless of duplicate status.
- `utils.py:4304-4346` uppercases source pivot *values* **conditionally** when `foudnDuplicatesInPivotHeaders` was set after composing the uppercase grouping+header list. Case variants `a` and `A` can generate the same output label without setting this flag (the shell has already been deduplicated). Original normal-query behavior for these inputs is therefore not a reliable unconditional case-insensitive aggregation contract.
- The Option C correction **intentionally normalizes the pivot-header identity to uppercase before physical FIRST/LAST deduplication and unstack**, avoiding duplicate output names and data loss. This repairs an apparent source defect; the affected behavior is labeled **INTENTIONAL-CHANGE / SOURCE-DERIVED**, not independently proven byte-for-byte original parity.
- The pre-revision `src/vg2c/runtime/crosstab.py` at `3e9def7a1b066e5007c9284510b242cbf611c7e2` is a distinct historical API contract: it does `groupby([...], dropna=False)[value_key].first().unstack(..., fill_value="")`, **first non-null** (not physical first), filters null/empty pivot headers before grouping, returns requested `row_keys` unchanged for empty/nonpivotable data, and lowercases nonempty output column names. These behaviors remain mandatory for old `crosstab={...}` or JSON loading.

### Old vs corrected behavior

| Input or invariant | Old Option C revision | Corrected Option C | Legacy JSON after correction |
|---|---|---|---|
| Same lot with `a` then `A` | Two internal labels both become `A` and raise collision | One normalized `A`; FIRST chooses first physical row and LAST chooses last | Historical grouping and lowercase header policy retained |
| `Voltage` / `VOLTAGE` | Potential duplicate-normalized-header exception | One `VOLTAGE` identity before unstack | Historical behavior retained |
| First reading null, second non-null | Physical first produces empty | Remains physical-first (normal-mode contract) | **First non-null** selected as in old vg2c |
| Pivot header null or empty | Converted to text/unknown in revised pivot | Remains current normal-mode contract | **Filtered out**, including all-empty/header-only result |
| Empty DataFrame or explicit `row_keys=[]` | Empty keys could raise | Normal mode unchanged | Original empty output schema / keys accepted |
| Columns/order | Normal uppercase, optional DOT lower and multi-value @ or . | Unchanged except resolving case-variant labels | Original lowercase and groupby sorting/order |
| New emitted project | `pivot_columns`, `pivot_values`, no JSON | **Unchanged** | Prior `crosstab=job.table_spec(...)` still supported |

The subsystem still uses one `_CrosstabUtility` and common schema resolution; the historical pandas grouping operation is a compact internal `_apply_legacy` branch, not a second public pivot engine, strategy hierarchy or framework. No new generated kwargs or assets.

### Additional corrective goldens and decisions

New `tests/runtime/test_crosstab_corrective_pass.py` covers mixed-case a/A, Voltage/VOLTAGE, FIRST/LAST selection, multiple-value @ and . names, sanitized Unicode/punctuation and row-key collision, original legacy first-non-null/null handling, blank/null pivot-header filtering, empty row keys/schema, case-insensitive source field lookup, lowercased output/order, JSON read-after-edit and mixed old/new error. The deterministic 50,000-record chunk-boundary test independently reconstructs *documented pandas primitives* with two chunks, not the proprietary engine. FIRST returns the earlier row in both paths. For LAST, original-style `combine_first` retains the first chunk's non-null value whereas global positional LAST retains the second chunk's last record.

**Cross-chunk policy remains WIP.** Reproducing the original chunk reconciliation algorithm completely would enlarge this correction and potentially reproduce additional historical defects; no streaming framework was added. Tests explicitly demonstrate the discrepancy without pretending equivalent behavior. Original proprietary execution, Oracle/other backends, complex CrossTab expressions, exact CTARRAY filename/delimiter and other previously documented corner cases remain UNVERIFIED.

No unrelated UI/JMP/AED/HTML files were modified and there is no new crosstab JSON. Semantic editing source ranges, dynamic SQL token flow, quoted SQLite INSERT, `/HEADERS`, joins, binds and isolated workdir behavior are protected by the existing focused and full suites. See the revision handoff for exact current CI results and final SHA (final handoff commit cannot contain its own hash).
