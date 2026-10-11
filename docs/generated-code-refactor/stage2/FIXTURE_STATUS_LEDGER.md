# Stage 2 fixture status ledger — initial baseline

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
| `tests/fixtures/script_short.txt` | compiles | **new P2 runtime test added; CI not yet verified** | fully offline SQLite; synthetic `ww_yield.csv` |
| `tests/fixtures/sql_script.txt` | compiles | not executed | MARS/OASYS Oracle, downstream SQLite |
| `tests/fixtures/tcb_yield.txt` | expected unsupported active SORT | not executed | explicit unsupported report option; also Oracle |
| `tests/fixtures/test.txt` | compiles | not executed | ARIES/MARS Oracle, downstream SQLite |
| `tests/fixtures/test_long.txt` | compiles | not executed | ARIES Oracle, downstream SQLite |
| `tests/fixtures/vietnam_test_python.txt` | compiles | not executed | MARS Oracle and Windows Python script launcher |
| `ICMPCS.txt` (root) | independent smoke not covered | not executed | Oracle, external utilities, mail; generated-file smoke elsewhere |
| `CSR_IAM_v2.txt` (root) | independent smoke not covered | not executed | Oracle, external utilities, mail |

## Related but distinct references

- `tests/fixtures/reflow.py` is a legacy Python artifact (with merge-marker
  text), not a VG2 script or execution oracle.
- `tests/fixtures/crosstab_parity/basic.csv` and
  `expected_basic.csv` are pivot inputs/expectations, not complete VG2 jobs.
- `scripthost-utilities-decompiled/` holds the original implementation. Source
  review is labelled **SOURCE-DERIVED** unless original engine execution occurs.
- `tests/deployment/verify_linux_wheel.py` verifies an installed wheel using a
  **synthetic** translated job; it does not previously execute the real fixture.

## Next verification steps and evidence rules

1. Record the actual CI results of the three P2 `script_short.txt` execution
   cases; promote only that fixture to `offline verified` once they pass.
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
