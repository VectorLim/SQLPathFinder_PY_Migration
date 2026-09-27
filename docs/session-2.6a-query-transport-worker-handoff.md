# Session 2.6A — Portable ScriptHost Query Transport and Worker Handoff

## Scope and starting point

Session 2.6A started from:

- branch: `direct-runtime/architecture-reassessment-portable-scripthost-v5`
- base commit: `5c0a5039a8135688c454bacfa86bcf163c60d7ef`

Implementation branch:

- `direct-runtime/session-2.6a-query-transport-worker`

The final commit is the branch HEAD after the session squash. Use `git rev-parse HEAD` after checkout; the handoff response for this session records the exact final SHA.

This session deliberately did **not** merge into `main`, delete `vg2c_new`, or resume the separate report rewrite.

## Architecture outcome

The original ScriptHost parser, task construction, control flow, query preprocessing, and query-task lifecycle remain authoritative:

```text
one isolated Linux child per job
        ↓
PortableScriptHostRuntime
        ↓
SPFManager.Run_SPFSQL
        ↓
SPFManager.Process_Query
        ↓
SPFManager.GetQuery
        ↓
original task hierarchy
        ↓
NormalQueryTaskBase.parseTaskCommand
        ↓
NormalQueryTaskBase.executeTaskCommand
        ↓
NormalQueryTaskBase.performCommand
        ↓
NormalQueryTaskBase.__ExecuteSQL__
        ↓
original schema/facility/CSV-list preprocessing
        ↓
nqOracleTask.OpenConnection
        ↓
PortableOracleConnection only when the compiled legacy dbDriver is unavailable
```

The new code does not parse SPFSQL, choose task types, substitute MARS/OASYS schema tokens, expand `SQL_Get_CSV_List`, or implement a parallel query semantic model. Those remain in the decompiled ScriptHost source.

The portable seam is intentionally below the mature query semantics: `PortableOracleConnection` satisfies the small connection surface that `nqOracleTask` already expects.

## Original ScriptHost behavior retained

The following original classes/methods remain semantic owners and are exercised by the Session 2.6A tests:

- `SPFManager.Run_SPFSQL`
- `SPFManager.Process_Query`
- `SPFManager.GetQuery`
- `NormalQueryTaskBase`
- `NormalQueryTaskBase.parseTaskCommand`
- `NormalQueryTaskBase.executeTaskCommand`
- `NormalQueryTaskBase.performCommand`
- `NormalQueryTaskBase.__ExecuteSQL__`
- original `Substitute_Schema`
- original `Substitute_Facility`
- original `Get_CSV_List`
- `nqOracleTask`
- original `nqSQLiteTask`

On a host where the mapped compiled legacy driver is present, `nqOracleTask.OpenConnection` still prefers that original driver. The portable connection is a fallback only when that mapped driver is unavailable.

## Portable database transport seam

Primary file:

- `src/scripthost_portable/query_transport.py`

### Routing

Current bounded node routing is:

- node containing `MARS` → backend `mars`
- node containing `ARIES` → backend `aries`
- node containing `OASYS` → backend `oasys`
- other Oracle-family nodes → explicit `UnsupportedQueryBackend`

For the real `22844.spfsql` fixture:

- `KM.[A15_PROD_21.].MARS` routes to MARS with site `KM`
- `KM.ARIES` routes to ARIES with site `KM`

The adapter receives SQL only after original ScriptHost preprocessing. In the deterministic fixture test:

- the MARS query no longer contains `@[]@` and contains `A15_PROD_21.F_LotHist`;
- the ARIES query no longer contains `SQL_Get_CSV_List` and contains the lot value read from the MARS-generated tab file.

### Output compatibility characterized in Session 2.6A

For the exercised query path the adapter writes:

- tab delimiter for `.tab`;
- UTF-8 without BOM;
- empty string for pandas null values;
- no repeated header on append.

When a query returns no rows, the adapter intentionally writes nothing so the existing ScriptHost empty-result/header fallback remains authoritative. The original fallback was observed to normalize `/HEADERS=a,b` to `A<TAB>B`.

This is not a claim of byte-for-byte parity for every historical dbDriver option. Live/historical validation should focus on any production options beyond this exercised path before broadening support.

## Minimal decompiled-source portability fixes

### `SPFLib/SPFSQL3.py`

1. When compiled `dbDrivers` is unavailable, a minimal local `NodesInfo` fallback provides only the attributes the original parser consumes: `node`, `un`, and `pw`.
2. The real fixture spelling `.\\file` is normalized to a POSIX relative path only on POSIX hosts at the existing CSV-list preprocessing boundary.
3. `nqOracleTask.OpenConnection` falls back to `PortableOracleConnection` only when the mapped legacy compiled driver class is absent.

### `SPFLib/SPFGlobals.py`

The default local directory suffix now uses `os.sep` instead of a hard-coded backslash. Windows behavior is unchanged; Linux no longer constructs paths such as `/tmp/job\\file`.

### `SPFLib/SPFUtilities/memtable.py`

The original `Get_CSV_List` SQL already calls `CharIndex_v2(...)`, but the decompiled Python SQLite connection did not register that function. Session 2.6A registers a compatible four-argument 1-based `CharIndex_v2` helper on the existing MemTable connection. The original CSV-list algorithm itself was not replaced.

## Real 22844.spfsql progress

The primary regression test reads the actual repository fixture:

- `scripthost-utilities-decompiled/22844.spfsql`

Using deterministic fake readers but the original ScriptHost parser/query tasks:

1. The real MARS node is parsed and executed through `nqOracleTask`.
2. Original MARS schema substitution runs before transport.
3. The MARS fake result produces `yeuchuan_a0_22844.tab`.
4. The real ARIES node is parsed and executed through `nqOracleTask`.
5. Original `SQL_Get_CSV_List(".\\yeuchuan_a0_22844.tab", lot_, ...)` reads the MARS output and expands the ARIES SQL.
6. The ARIES fake result produces `yeuchuan_a1_22844.tab`.
7. The following original SQLite node executes and produces `XRAY_results.csv` with the expected original uppercase schema.

The deterministic fake rows validate the MARS and ARIES transport/lifecycle boundary. The downstream SQLite result is header/schema-only in this fake-data run; Session 2.6A does not reinterpret that as a transport failure or rewrite the unrelated SQLite join behavior.

## One-job-per-process worker

Primary file:

- `src/scripthost_portable/worker.py`

Public orchestration objects:

- `ScriptHostJob`
- `ScriptHostJobResult`
- `run_job(job, timeout=...)`

Every `run_job` invocation starts a fresh Python child with:

```text
python -m scripthost_portable.worker --child
```

A child accepts exactly one JSON job, executes it, emits one structured result, and exits. This is the production boundary for mutable ScriptHost globals, environment mutation, and working-directory mutation.

Structured failure categories currently include:

- `transport_unavailable`
- `datasyncx_configuration_failure`
- `unsupported_legacy_integration`
- `query_execution_failure`
- `script_host_error`
- `child_timeout`
- `child_crash`
- `child_protocol_error`

Tests cover sequential fresh PIDs, concurrent subprocess jobs with isolated working directories/outputs, child-only environment mutation, DataSyncX construction/configuration failure, structured unsupported-query failure, timeout, and child crash.

## Deterministic fake-reader validation

Tests live in:

- `tests/vg2c_new/test_scripthost_query_transport.py`
- `tests/vg2c_new/test_scripthost_worker.py`

The fake-reader factory records backend, original node, derived site, and final preprocessed query. It does not emulate parser semantics.

Covered transport cases:

- explicit original `GetQuery` routing of the real MARS and ARIES blocks to `nqOracleTask`
- MARS
- ARIES
- OASYS
- unsupported Oracle-family node
- output encoding/delimiter/append behavior
- original OASYS token preprocessing before transport
- actual `22844.spfsql` MARS → ARIES handoff
- original empty-query header fallback

## Linux validation

The branch is included in `.github/workflows/direct-runtime-validation.yml`.

The workflow checks:

- original ScriptHost package import probe;
- Ruff format;
- compile of `src/vg2c_new` and `src/scripthost_portable`;
- explicit compile of the modified decompiled ScriptHost targets plus the existing portable targets;
- `pytest tests/vg2c_new -q`;
- the existing process-isolation benchmark;
- resolver-manifest status;
- Ruff lint.

Before the final squash, the complete workflow passed with:

- **81 tests passed**;
- Ruff format passed;
- compile checks passed;
- Ruff lint passed;
- process-isolation benchmark passed.

The final squashed commit must be validated by the same workflow before handoff is considered complete.

Observed nonfatal legacy noise in CI includes attempts to reach the internal SQLPathFinder logging service and a terminal-display warning when optional table formatting support is unavailable. These are outside the Session 2.6A database-transport seam and did not prevent the query lifecycle or tests from completing.

## DataSyncX status: not live-validated

**No live DataSyncX validation is claimed by this session.**

The public CI environment does not contain the private DataSyncX package. All real-query-path tests therefore use deterministic fake readers while preserving the original ScriptHost query lifecycle.

All guessed DataSyncX API knowledge is deliberately concentrated in:

- `src/scripthost_portable/query_transport.py::DataSyncXReaderFactory`

Repository-backed expectations (already used by the existing `vg2c` compiler/runtime, but still not live-validated against the private installed package in this web session) are:

1. The primary imports are `datasyncx.MarsReader`, `datasyncx.AriesReader`, and `datasyncx.OracleReader`.
2. OASYS construction is conceptually `OracleReader(database="OASYS")`.
3. Query execution is conceptually `reader.read(site=<site>, query=<sql>)`.
4. The site value is the literal leading site token such as `KM`.

Additional provisional fallbacks/assumptions that still require Intel-host validation are:

1. Reader classes may also be importable as:
   - `datasyncx.MarsReader`, `datasyncx.readers.MarsReader`, or `datasyncx.readers.mars.MarsReader`;
   - equivalent ARIES paths for `AriesReader`;
   - equivalent Oracle paths for `OracleReader`.
2. MARS and ARIES reader constructors take no arguments.
3. Results are a pandas DataFrame, expose `.to_pandas()`, or are pandas-convertible.
4. Authentication/configuration/retry are owned by DataSyncX or its environment rather than the legacy `/UN`, `/PW`, and retry arguments.
5. Reader construction/configuration failures can be separated from missing transport; runtime DataSyncX exception types during `.read(...)` still require live inspection before finer classification than `QueryExecutionError`.
6. `SCRIPTHOST_DATASYNCX_CONFIG_REFERENCE` is only a non-secret configuration reference carried into the worker; the adapter does not yet assume how a real DataSyncX build consumes it.
7. Explain-plan execution is not implemented in the portable seam.

These assumptions must be confirmed or adjusted on the Intel host. Do not spread DataSyncX-specific API changes into `SPFSQL3.py` or recreate query semantics outside ScriptHost.

## Exact local Intel validation target

The local Intel agent should inspect and, if necessary, modify only the DataSyncX-facing adapter first:

- `src/scripthost_portable/query_transport.py`
  - `DataSyncXReaderFactory.reader_for`
  - `DataSyncXReaderFactory._CLASS_CANDIDATES`
  - only if real result shape requires it, `_as_frame`

The original ScriptHost integration point to observe, not redesign, is:

- `scripthost-utilities-decompiled/SPSQL3_py/SPFLib/SPFSQL3.py`
  - `nqOracleTask.OpenConnection`
  - `NormalQueryTaskBase.__ExceuteSQLQuery__`

Use the existing deterministic tests as a regression safety net and add live tests separately rather than changing them into environment-dependent tests.

### Live validation sequence

1. Checkout `direct-runtime/session-2.6a-query-transport-worker` and verify the final session SHA.
2. Inspect the installed DataSyncX package/version before editing the adapter.
3. Record the actual reader import paths and constructor signatures.
4. Record the actual MARS, ARIES, and if available OASYS execution signatures.
5. Confirm the accepted site/node form.
6. Confirm return types, empty-result behavior, null handling, and exceptions.
7. Run the real `22844.spfsql` MARS node and compare:
   - final SQL after original preprocessing;
   - row/column shape;
   - `yeuchuan_a0_22844.tab` delimiter, encoding, headers, and representative values.
8. Run the real ARIES node using the MARS-produced tab file and compare:
   - `SQL_Get_CSV_List` expansion;
   - final SQL;
   - `yeuchuan_a1_22844.tab`.
9. If OASYS is available, run one original OASYS task and verify schema-token stripping plus reader routing.
10. Exercise at least two sequential `run_job` calls and two concurrent process-isolated `run_job` calls.
11. Re-run the full Linux test suite/format/lint checks after any adapter adjustment.
12. Report each provisional assumption above as **CONFIRMED**, **ADJUSTED**, or **NOT TESTABLE**.

Do not claim live parity merely because DataSyncX imports. Verify one real successful MARS/ARIES query path and resulting output artifacts.

## Files changed by Session 2.6A

Expected session-owned changes are limited to:

- `.github/workflows/direct-runtime-validation.yml`
- `docs/session-2.6a-query-transport-worker-handoff.md`
- `scripthost-utilities-decompiled/SPSQL3_py/SPFLib/SPFGlobals.py`
- `scripthost-utilities-decompiled/SPSQL3_py/SPFLib/SPFSQL3.py`
- `scripthost-utilities-decompiled/SPSQL3_py/SPFLib/SPFUtilities/memtable.py`
- `src/scripthost_portable/__init__.py`
- `src/scripthost_portable/query_transport.py`
- `src/scripthost_portable/runtime.py`
- `src/scripthost_portable/worker.py`
- `tests/vg2c_new/test_scripthost_query_transport.py`
- `tests/vg2c_new/test_scripthost_worker.py`

No `src/vg2c_new` implementation file is changed by this session.

## Report subsystem status

The report subsystem remains exactly the separate architecture follow-up identified before Session 2.6A. This session does not resume the report rewrite and does not change report ownership.

## Recommended next step

Run the focused Intel-side DataSyncX validation above. If the real API differs, adapt only `DataSyncXReaderFactory` (and the result normalization helper if necessary), re-run all Session 2.6A tests, and record the live MARS/ARIES evidence. Only after that evidence should a later session decide whether this portable original-ScriptHost runtime becomes the primary execution architecture.
