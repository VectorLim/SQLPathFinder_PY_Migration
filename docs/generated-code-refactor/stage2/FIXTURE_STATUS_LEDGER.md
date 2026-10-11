# Stage 2 fixture status ledger — executed evidence checkpoint

**Authority:** original Python ScriptHost source. **Ref:** `b8fc52ff41569129959a37487f5417b0fd339527`.
This is a tracking baseline, not a claim of engine-level parity. `tests/runtime/test_e2e_fixtures.py`
previously establishes parse/classify/resolve/dispatch/emit and Python `compile()` only.
It is **not** evidence that translated projects executed. CI baseline: 16 such
successful compilations; three explicit unsupported cases.

**Status dimensions:** `compile` = expected result of checked-in fixture smoke;
`runtime` = actual translated-project execution evidence, if present;
`dependency` = observed facility preventing safe complete execution.
A service stub is not equivalent to real Oracle/Windows behavior.

| Script | Compile baseline | Runtime baseline | Feasibility / dependency |
|---|---|---|---|
| `tests/fixtures/TimeDelta.txt` | expected unsupported JMP/JSL | not executed | Oracle, external utility and unsupported JMP/JSL |
| `tests/fixtures/actual_script.txt` | compiles | **first original HTML-RUN CSS block executed (PARTIAL)** | remaining external utilities, Oracle and mail blocked |
| `tests/fixtures/aries_simple.txt` | compiles | not executed | ARIES Oracle reader |
| `tests/fixtures/hamizah.txt` | compiles | **original first four blocks executed as one chain (PARTIAL: first two MARS reads mocked, next two SQLite blocks real); independently, the last two local blocks executed with synthetic `.tab` inputs** | original Oracle/MARS engine and later source steps remain blocked |
| `tests/fixtures/html_test.txt` | expected unsupported JMP/JSL | not executed | unsupported JMP/JSL; local HTML portions need separate oracle |
| `tests/fixtures/maxlidheight.txt` | compiles | **first original HTML-RUN CSS block executed (PARTIAL)** | remaining external utilities, Oracle and mail blocked |
| `tests/fixtures/new_icmpcs.txt` | compiles | **first original HTML-RUN CSS block executed (PARTIAL)** | remaining external utilities, Oracle and mail blocked |
| `tests/fixtures/new_stuff.txt` | compiles | not executed | Oracle plus downstream SQLite |
| `tests/fixtures/oasys.txt` | compiles | not executed | OASYS Oracle reader |
| `tests/fixtures/oracle_datasyncx_smoke.txt` | compiles | not executed | real Oracle/DataSyncX connection unavailable |
| `tests/fixtures/reflow.txt` | compiles | **original final SQLite RIGHT JOIN executed (PARTIAL)** | upstream MARS/ARIES Oracle unavailable; controlled tab-delimited inputs substituted |
| `tests/fixtures/reflow_detection.txt` | compiles | **original final SQLite RIGHT JOIN executed (PARTIAL)** | upstream MARS/ARIES Oracle unavailable; controlled tab-delimited inputs substituted |
| `tests/fixtures/script_another.txt` | compiles | not executed | MARS reader and Windows Run_Python_Script.va |
| `tests/fixtures/script_short.txt` | compiles | **offline verified: actual translated fixture runtime and installed wheel** | fully offline SQLite; synthetic `ww_yield.csv` |
| `tests/fixtures/sql_script.txt` | compiles | **third original SQLite LEFT JOIN block executed (PARTIAL)** | two upstream MARS/OASYS Oracle queries unavailable; deterministic tab-delimited input substituted |
| `tests/fixtures/tcb_yield.txt` | expected unsupported active SORT | not executed | explicit unsupported report option; also Oracle |
| `tests/fixtures/test.txt` | compiles | not executed | ARIES/MARS Oracle, downstream SQLite |
| `tests/fixtures/test_long.txt` | compiles | not executed | ARIES Oracle, downstream SQLite |
| `tests/fixtures/vietnam_test_python.txt` | compiles | **first original WRITE-FILE block executed and checked (PARTIAL)** | remaining MARS/Windows launcher blocked; no full-fixture verification |
| `ICMPCS.txt` (root) | independent smoke not covered | **first original HTML-RUN CSS block executed (PARTIAL)** | remaining Oracle, external utilities, mail blocked |
| `CSR_IAM_v2.txt` (root) | independent smoke not covered | **first original HTML-RUN CSS block executed (PARTIAL)** | remaining Oracle, external utilities, mail blocked |

## Related but distinct references

- `tests/fixtures/reflow.py` is a legacy Python artifact (with merge-marker
  text), not a VG2 script or execution oracle.
- `tests/fixtures/crosstab_parity/basic.csv` and
  `expected_basic.csv` are pivot inputs/expectations, not complete VG2 jobs.
- `scripthost-utilities-decompiled/` holds the original implementation. Source
  review is labelled **SOURCE-DERIVED** unless original engine execution occurs.
- `tests/deployment/verify_linux_wheel.py` now verifies an installed wheel using both the original **synthetic** translated job and the actual relocated `script_short.txt` fixture with synthetic input.

## Next verification steps and evidence rules

1. [Completed] `script_short.txt` actual translated runtime cases passed, including editable SQL and missing-input failure; Linux installed-wheel execution passed. Do not call this original-engine parity.
2. For mixed Oracle/SQLite scripts, identify safe independent SQLite/report
   segments and generate deterministic local inputs only after checking that
   those segments contain no external side effects. Mark partial execution as
   **partial**, not complete.
3. Record fixture-level `emit` failures separately from external runtime
   blockers. Never silently skip them or count mocks as real integration.
4. Map affected rules to original `SPFSQL3.py` and `SPFUtilities` source.
   Note intentional fixes and unknown oracles explicitly.
5. Keep comprehensive test command exit statuses and inherited UI/JMP failures
   in the separate Stage 2 handoff.

No future metadata schema, decorator, API or registry is implemented here.

## Verified Stage 2 checkpoint (11 October 2026)

- **Source baseline:** `b8fc52ff41569129959a37487f5417b0fd339527`; subsequent Stage 2 commits are on `refactor/generated-code/implementation`.
- **Actual full offline fixture:** `script_short.txt` produced a DISTINCT `owner.csv` relation from deterministic `ww_yield.csv`. SQL-asset editing and missing-input failure propagation passed. The relocated generated project also ran using the installed Linux wheel in [workflow 38152518801](https://github.com/VectorLim/SQLPathFinder_PY_Migration/actions/runs/38152518801).
- **Partial source-preserved fixture segments (nine total):** The original first WRITE-FILE block from `vietnam_test_python.txt` created `path.csv` and preserved the source body (including its leading blank line). The original first HTML-RUN CSS block from each root `ICMPCS.txt` and `CSR_IAM_v2.txt`, and from `actual_script.txt`, `maxlidheight.txt` and `new_icmpcs.txt`, created the expected stylesheet. The original third SQLite block from `sql_script.txt` also executed a LEFT JOIN on synthetic tab-separated upstream inputs, retaining matches and unmatched rows. The original final SQLite RIGHT OUTER JOIN blocks of `reflow.txt` and `reflow_detection.txt` also ran with deterministic tab-separated source rows, retaining both joined and unmatched inspection records. All nine segments are **partial**, not complete fixture successes.
- **ScriptHost-derived P4 defect:** `SPFLib/SPFUtilities/utils.py::SPFCopy` (around line 7807) calls ordinary file-copy operations. In VG2C, `runtime/files.py::copy_file(..., recurse=False)` silently performed a recursive directory copy. [Red CI run 38150673222](https://github.com/VectorLim/SQLPathFinder_PY_Migration/actions/runs/38150673222) reproduced `DID NOT RAISE IsADirectoryError`; commit `76220d1b35fbb37fb610cf0625840b4deb72e55e` now requires opt-in `recurse=True`. File-copy and explicit recursive-copy tests also pass. This source check does **not** establish exact behavior for an absent external `SPFCopy.bat` implementation.

- **ScriptHost-derived P4 delimiter defects:** Original `SPFLib/SPFUtilities/utils.py::GetFileDLM` (~478–515) chooses `.tab`/`.hive-tab`/`.hive-sequence` = tab, `.asc` = pipe, `.plus` = plus, and CSV = comma. Original `SPFLib/SPFUtilities/memtable.py` (~1146) consults it for SQLite table input. The old runtime SQLite importer always parsed commas, breaking original `sql_script.txt` with `no such column: spc_lot` in [red CI 38151588876](https://github.com/VectorLim/SQLPathFinder_PY_Migration/actions/runs/38151588876). Corrected in `runtime/sqlite_reader.py` via the existing `_CsvIO._delimiter_for` (`e5febf40c7c5ad7322c553bc3188d8975e26f0ba`). Additional red [38151810986](https://github.com/VectorLim/SQLPathFinder_PY_Migration/actions/runs/38151810986) found 4 tab/pipe CSV writer failures, and [38152219324](https://github.com/VectorLim/SQLPathFinder_PY_Migration/actions/runs/38152219324) found 3 hive/plus extension failures. Corrected with one shared extension helper in `runtime/csv_io.py` (`0d90a9a505606c90f6fe5e78a9e3b2352bf08331`, `77e5e5ed72fb9f935701743ee7f69941b734bb79`); chunk tests include embedded commas/tabs. The full original `sql_script.txt` still needs unavailable Oracle services.
- **P5 ownership audit:** `ARCHITECTURE_OWNERSHIP_REVIEW.md` records actual core/compiler/editor dependency direction, consumers, smallest decisions, verified deletions and why existing `utility_metadata.py` cannot simply be removed. This does **not** implement future metadata.
- **Core suite at checkpoint:** [workflow 38152518914](https://github.com/VectorLim/SQLPathFinder_PY_Migration/actions/runs/38152518914): `432 passed` runtime/emitter, `610 passed / 6 failed` full suite. The six failures are the same inherited UI/document-store/JMP tests; do not claim a green entire suite.
- **No future metadata implementation:** No runtime discovery API, metadata decorators, sidecars, manifests, descriptor schemas or metadata-specific tests were added.

### Phase status — truthful remaining scope

| Phase | Current verified work | Still required |
|---|---|---|
| P0 | Branch ancestry and CI baseline rechecked; initial source consumers inspected | Complete exhaustive dynamic/static reference map and locally reproducible checkout where possible |
| P1 | All 19 fixture scripts plus two root scripts individually logged | Enrich exact input/output and backend requirements for additional safe local segments |
| P2 | One complete offline fixture and nine real source-preserved local segments executed | Further partial/whole feasible fixture verification; external integration remains blocked |
| P3 | Original ScriptHost sources examined for SQL/CSV/pivot/HTML/file cases; file-copy source traced | Complete source-derived oracle family coverage; original-engine runs unavailable |
| P4 | Unsupported mail command diagnostics, directory-copy opt-in and ScriptHost-derived delimiter fixes tested | Red-test-first review of remaining confirmed defects only |
| P5 | Unreachable UNKNOWN no-op utility removed, comments corrected; source-consumer boundary review committed | Full symbol-level consumer analysis, small measured consolidations where justified; do not retire supported UI endpoints without scope decision |
| P6 | Installed Linux wheel including a real fixture passed; 432 core tests green | Complete planned suite and fixture closure, full-suite inherited UI decisions, final measured architecture sign-off |

Do **not** promote source-derived, partial, stubbed or blocked fixture results to engine-verified parity. Stage 2 is **in progress**, not complete.

### Remaining gating work at this checkpoint

- Complete the P0 symbol-level reverse consumer inventory before any additional P5 removal; do not mistake compiler-only descriptions and source diagnostics for an unimplemented future optional runtime metadata layer.
- Continue isolated, source-preserved execution for any other safe downstream SQLite/HTML/WRITE-FILE blocks. The original compiled-only coverage (16 supported and 3 expected-unsupported) remains distinct from behavioral execution evidence.
- Real Oracle/MARS/ARIES/OASYS, corporate network files, Windows utilities, live SMTP and the original ScriptHost engine were **not** accessed. No live-engine parity or production integration is claimed.
- Do not make CI appear green by dropping inherited UI tests. The six failures are in the editor/UI surface; investigate editor retirement versus preservation as an isolated P5 decision with consumer proofs and regression gates.
- Retain independent installed-wheel runtime execution, full fixture-status coverage, and source-derived error/format semantics when deciding further changes.


## Continuation evidence — 12 October 2026

- **WRITE-FILE test:** test_fixture_partial_local_write_stage2.py compares the complete output with the original payload after one outer structural newline is removed on each side by frontend/parser.py::_trim_outer_blank_line. The remaining leading blank line is asserted. This is an observed translator/parser framing rule, not an independently executed ScriptHost oracle. Previously .strip() erased that evidence.
- **Strict .tab output test:** test_fixture_partial_sql_join_stage2.py now explicitly requires literal tabs and reads with tab delimiter, rejecting comma fallback. ScriptHost GetFileDLM is the source-derived authority.
- **New meaningful local chain (PARTIAL):** test_fixture_multistep_hamizah_stage2.py executes the original consecutive third and fourth SQLite blocks from hamizah.txt. Synthetic upstream yeuchuan_a0_5547.tab and yeuchuan_a1_5547.tab feed the original INNER JOIN into input.csv; the next original SQLite SELECT consumes input.csv to produce yeuchuan_SQL_5547.tab. Assertions cover headers, complete row lists, required tab delimiter, missing-input FileNotFoundError and changes after editing the second generated SQL asset and rerunning. The first MARS stages and remaining external stages are not run. These two local blocks join the previous nine partial source blocks: eleven independently/local-executed partial blocks. Separately, the four-block chain exercises two additional source-preserved MARS blocks via controlled fake reader and the same two real SQLite blocks. This yields thirteen distinct source-preserved partial blocks; the additional MARS execution is **mocked**, not Oracle parity.
- **Verified implementation CI:** [workflow 38182606214](https://github.com/VectorLim/SQLPathFinder_PY_Migration/actions/runs/38182606214) reported 435 passed runtime/emitter; 613 passed / six inherited UI/JMP failures in the full suite. [Wheel workflow 38182606154](https://github.com/VectorLim/SQLPathFinder_PY_Migration/actions/runs/38182606154) passed. Later audit/demonstration workflow results are recorded in the continuation handoff.
- **Other concurrent change:** f8d1414eb24feedc6503ee69c30206257296b4cb switched unmatched filesystem command emission from silent pass to explicit ValueError. This is fail-visible behavior, not original-engine verification.

Fixture execution, compilation-only smoke, original-engine parity and external integration remain distinct evidence categories. Future optional runtime metadata remains out of scope.


### Four-block fixture expansion and cross-platform path correction

- Source-preserved `tests/fixtures/hamizah.txt` blocks 1–4 now execute as one chained generated project in `test_original_first_four_hamizah_blocks_run_with_mocked_mars_reader`. Blocks 1 and 2 invoke a **fake, in-process DataSyncX MarsReader** (no network or real SQL execution) and write original-name tab-delimited outputs; block 2 performs real local `SQL_Get_CSV_List` substitution from block 1, checked against its actual SQL. Original SQLite blocks 3 and 4 execute with the real runtime and produce asserted `input.csv` then `yeuchuan_SQL_5547.tab`. This is PARTIAL mocked-external integration, not ScriptHost parity or complete script execution.
- Initial four-block execution exposed a real Linux error: source text `SQL_Get_CSV_List(".\\yeuchuan_a0_5547.tab", ...)` attempted to open a literal backslash filename. The direct red regression in `test_file_delimiter_parity_stage2.py` models nested relative Windows separators, and `runtime/csv_io.py::_read_column` now interprets those relative path separators via `PureWindowsPath` before resolving under the workdir. Drive-absolute and UNC inputs are rejected instead of silently remapped. The original source uses Windows-relative paths, but no original engine execution was performed. This correction is restricted to the CSV-list column reader.
- [Green core/full CI 38183242029](https://github.com/VectorLim/SQLPathFinder_PY_Migration/actions/runs/38183242029) at `f605423d0e3bd1365667f13e76b89c525f2b9152`: 437 runtime/emitter passed (one warning); full suite 615 passed, the exact same six UI/JMP failures. [Installed Linux-wheel 38183242073](https://github.com/VectorLim/SQLPathFinder_PY_Migration/actions/runs/38183242073) succeeded. Compiler tests, fixture execution and Linux packaging remain distinct from original-engine semantics.

- **Final guarded regression checkpoint:** [CI 38183396305](https://github.com/VectorLim/SQLPathFinder_PY_Migration/actions/runs/38183396305) after the two additional Windows drive/UNC rejection cases: **439 runtime/emitter passed (one warning), 617 full-suite passed with the unchanged six inherited UI/JMP failures**. [Installed wheel 38183396317](https://github.com/VectorLim/SQLPathFinder_PY_Migration/actions/runs/38183396317) passed. Earlier 437/615 results above document the preceding code checkpoint, not the latest guarded case.
