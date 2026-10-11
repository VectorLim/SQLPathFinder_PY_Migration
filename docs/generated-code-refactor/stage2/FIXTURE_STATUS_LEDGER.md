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
| `tests/fixtures/actual_script.txt` | compiles | not executed | external utilities, Oracle and mail; unsafe to run unmodified |
| `tests/fixtures/aries_simple.txt` | compiles | not executed | ARIES Oracle reader |
| `tests/fixtures/hamizah.txt` | compiles | not executed | MARS/Oracle, downstream SQLite |
| `tests/fixtures/html_test.txt` | expected unsupported JMP/JSL | not executed | unsupported JMP/JSL; local HTML portions need separate oracle |
| `tests/fixtures/maxlidheight.txt` | compiles | not executed | external utilities, Oracle and mail |
| `tests/fixtures/new_icmpcs.txt` | compiles | not executed | external utilities, Oracle and mail |
| `tests/fixtures/new_stuff.txt` | compiles | not executed | Oracle plus downstream SQLite |
| `tests/fixtures/oasys.txt` | compiles | not executed | OASYS Oracle reader |
| `tests/fixtures/oracle_datasyncx_smoke.txt` | compiles | not executed | real Oracle/DataSyncX connection unavailable |
| `tests/fixtures/reflow.txt` | compiles | not executed | MARS/ARIES Oracle, downstream SQLite |
| `tests/fixtures/reflow_detection.txt` | compiles | not executed | MARS/ARIES Oracle, downstream SQLite |
| `tests/fixtures/script_another.txt` | compiles | not executed | MARS reader and Windows Run_Python_Script.va |
| `tests/fixtures/script_short.txt` | compiles | **offline verified: actual translated fixture runtime and installed wheel** | fully offline SQLite; synthetic `ww_yield.csv` |
| `tests/fixtures/sql_script.txt` | compiles | not executed | MARS/OASYS Oracle, downstream SQLite |
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
- **Actual full offline fixture:** `script_short.txt` produced a DISTINCT `owner.csv` relation from deterministic `ww_yield.csv`. SQL-asset editing and missing-input failure propagation passed. The relocated generated project also ran using the installed Linux wheel in [workflow 38151049513](https://github.com/VectorLim/SQLPathFinder_PY_Migration/actions/runs/38151049513).
- **Partial source-preserved fixture segments:** The original first WRITE-FILE block from `vietnam_test_python.txt` created `path.csv` and preserved the source body (including its leading blank line). The original first HTML-RUN CSS block from each root `ICMPCS.txt` and `CSR_IAM_v2.txt` created the expected stylesheet. They are **partial**, not complete fixture successes.
- **ScriptHost-derived P4 defect:** `SPFLib/SPFUtilities/utils.py::SPFCopy` (around line 7807) calls ordinary file-copy operations. In VG2C, `runtime/files.py::copy_file(..., recurse=False)` silently performed a recursive directory copy. [Red CI run 38150673222](https://github.com/VectorLim/SQLPathFinder_PY_Migration/actions/runs/38150673222) reproduced `DID NOT RAISE IsADirectoryError`; commit `76220d1b35fbb37fb610cf0625840b4deb72e55e` now requires opt-in `recurse=True`. File-copy and explicit recursive-copy tests also pass. This source check does **not** establish exact behavior for an absent external `SPFCopy.bat` implementation.
- **Core suite at checkpoint:** [workflow 38151049556](https://github.com/VectorLim/SQLPathFinder_PY_Migration/actions/runs/38151049556): `417 passed` runtime/emitter, `595 passed / 6 failed` full suite. The six failures are the same inherited UI/document-store/JMP tests; do not claim a green entire suite.
- **No future metadata implementation:** No runtime discovery API, metadata decorators, sidecars, manifests, descriptor schemas or metadata-specific tests were added.

### Phase status — truthful remaining scope

| Phase | Current verified work | Still required |
|---|---|---|
| P0 | Branch ancestry and CI baseline rechecked; initial source consumers inspected | Complete exhaustive dynamic/static reference map and locally reproducible checkout where possible |
| P1 | All 19 fixture scripts plus two root scripts individually logged | Enrich exact input/output and backend requirements for additional safe local segments |
| P2 | One complete offline fixture and three real source-preserved local segments executed | Further partial/whole feasible fixture verification; external integration remains blocked |
| P3 | Original ScriptHost sources examined for SQL/CSV/pivot/HTML/file cases; file-copy source traced | Complete source-derived oracle family coverage; original-engine runs unavailable |
| P4 | Unsupported mail command diagnostics and directory-copy opt-in correction tested | Red-test-first review of remaining confirmed defects only |
| P5 | Unreachable UNKNOWN no-op utility removed; incorrect embedding comments corrected; live UI/core boundaries identified | Full symbol-level consumer analysis, small measured consolidations where justified; do not retire supported UI endpoints without scope decision |
| P6 | Installed Linux wheel including a real fixture passed; current core tests green | Complete planned suite and fixture closure, full-suite inherited UI decisions, final measured architecture sign-off |

Do **not** promote source-derived, partial, stubbed or blocked fixture results to engine-verified parity. Stage 2 is **in progress**, not complete.
