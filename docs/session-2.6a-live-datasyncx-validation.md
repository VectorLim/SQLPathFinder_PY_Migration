# Session 2.6A live DataSyncX validation

## Decision

**Linux production query transport gate: NOT CLEARED.** Real Windows DataSyncX
MARS → ARIES → original SQLite execution succeeded. Linux authentication and
historical compiled-driver null/dot byte parity remain unverified. No main merge,
report rewrite, or original ScriptHost lifecycle change was made.

Base: `direct-runtime/session-2.6a-query-transport-worker`, exactly
`2063649ff5316dc0745a90a3f1ce158327fdaf60`. Validation branch:
`direct-runtime/session-2.6a-live-validation`.

## Environment and installed API inspection

Inspected installed source and imported signatures before editing code:
Windows 11, Python 3.14.6, project `.venv`, DataSyncX 1.1.6.
System Python 3.13 has no DataSyncX. WSL lists no distributions; Docker's Linux
daemon pipe is unavailable. No Linux execution is claimed.

Canonical imports are `datasyncx.MarsReader`, `AriesReader`, `OracleReader`.
Implementation modules are `datasyncx.readers.mars_reader`, `aries_reader`, and
`oracle_reader`. `DataSyncX` is a deprecated compatibility shim.

MARS/ARIES constructors: `(*, username=None, password=None, db_account=False)`.
Oracle constructor: `(*, sites=None, username=None, password=None, database=None,
node=None)`. All reads: `(self, site, query, **kwargs) -> pandas.DataFrame`.
MARS accepts `factory` through kwargs; ARIES validates nonempty site/query.
OASYS constructor is supported; live OASYS access was not tested.

DataSyncX returns pandas frames and uppercases column labels in `get_df_using_sql`.
It performs additional schema/site/timezone transformations internally. The
fixture reaches it after ScriptHost has expanded schema and CSV-list tokens.

Authentication/configuration:

- Default constructors use OS authentication on this Windows host; both queries
  succeeded without passing credentials. No credential values were read or saved.
- `resolve_password` uses explicit password, then uppercase username lookup in
  environment/`.env`, otherwise raises `MissingCredentialError` (a ValueError).
  Windows installed-package configuration resolves to the project `.env`;
  Linux resolves to `/app/.env`. Import can create a missing `.env`.
- DataSyncX initializes the thick Oracle client. Missing client DPI-1047 becomes
  RuntimeError; Oracle connection errors otherwise propagate.
- On POSIX, default MARS/ARIES authentication calls `kinit_user`, defaults to
  `GAR/msoaatm`, and needs configuration plus Kerberos tooling. This was inspected,
  not executed. Nonzero kinit exit is logged by DataSyncX rather than raised there.
- `get_df_using_sql` retries any Exception three total attempts, two delays of
  15–16 seconds, using the same connection. Connect itself is outside that retry.
  Two retry messages were observed for the invalid-column probe.
- Real invalid SQL surfaced `oracledb.exceptions.DatabaseError` (ORA-00904),
  preserved as `QueryExecutionError.__cause__`. Empty ARIES site raised ValueError;
  a deliberately nonexistent credential key raised MissingCredentialError.
- Reader objects provide no connection close method; package source does not
  explicitly close the Oracle connection in this read path. Fresh process exit
  is still the resource/isolation boundary. No DataSyncX package changes were made.

## Live evidence and transport identity

Run `scripts/architecture/validate_live_datasyncx.py --output <new-local-dir>`
with `PYTHONPATH=src` and the DataSyncX-enabled Python. Run each job as a separate
process. Output includes corporate row data and should remain local.

The harness sets the mapped `dbDriverCxOracle` to None only inside a patch context,
calls the original `nqOracleTask.OpenConnection`, and asserts the returned type is
exactly `PortableOracleConnection`. Its observation wrapper delegates to real
DataSyncX readers and records class, signatures, site, SQL digest, columns, row
count, and exceptions. It never replaces the query result with fake data.
Compiled-driver availability was false on Python 3.14; explicit forcing prevents
silent fallback to a compiled driver on other Windows Python versions too.

Recorded in `session-2.6a-live-datasyncx-evidence.json` (no business row values):

| Step | Rows | Result |
|---|---:|---|
| Real 22844 MARS | 4 | `KM.[A15_PROD_21.].MARS`, site `KM`, schema expanded |
| Real 22844 ARIES | 10,286 | `KM.ARIES`, site `KM`, CSV list expanded from real MARS file |
| Original SQLite | 1,786 | `XRAY_results.csv`, 534,179 bytes in observed run |

Sequential runs 01/02 and concurrent runs a/b all completed these three steps.
Run 02 PID 32300; concurrent PIDs 8184 and 15164. Each child used distinct local
output directories and reader instances; no same-process concurrent runtime was
used. Existing worker tests for fresh PIDs, parent environment/cwd, globals and
sibling output isolation passed. Concurrent live harness runs test independent
OS processes, not the `run_job` API; that API's isolation remains deterministic
test evidence. Raw logs and outputs are under ignored `data/live-datasyncx-*`.

## Headers and output parity

For **both real queries**, actual returned labels in order equal the literal
fixture `/HEADERS` list uppercased, and equal the generated TAB header exactly.
The evidence JSON records all four comparisons: literal fixture headers, actual
DataSyncX labels, generated headers, and the expected uppercase cursor labels
(derived by uppercasing the fixture list).

Historical expected behavior is source-backed, not a compiled-driver golden-file
claim: `SPFSQL3.py` changelog 2.0.0.6 explicitly says `writeCursorToFile` ignores
MyHeaders and takes cursor headers. Oracle's unquoted aliases in these queries
produce uppercase cursor labels. Therefore overriding populated results with
`MyHeaders` would be an unjustified compatibility change. No override was added.
The regression now deliberately supplies DIFFERENT_HEADER against ACTUAL_LABEL.
The live dual probe likewise supplies DIFFERENT,HEADERS,HERE and retains the
actual labels NULL_VALUE, DOT_VALUE, UNICODE_VALUE.

Real empty dual result: DataSyncX returns zero rows; transport writes no file.
Original ScriptHost with `/HEADERS=fallback_name` and SQL alias `actual_label`
writes `FALLBACK_NAME`, confirming the lifecycle's empty fallback owns `/HEADERS`.

Live null/unicode/append probes show SQL NULL as an empty field, literal `.` as
`.`, `é中` surviving UTF-8 round-trip, no BOM, CRLF on Windows, and one header over
two execute calls (`FirstConnect=True` then False). Deterministic tests also cover
no-header mode and empty append. The fixture contains no nulls in these observed
MARS/ARIES results, so dedicated dual probes were necessary.

Historical dot-sentinel null serialization is **NOT TESTABLE** here: bundled
drivers exist only in the archived distribution for Python 3.9/3.11/3.13; the
DataSyncX environment is 3.14 and no working historical driver/golden output was
available. Do not infer historic null-byte parity from the successful SQLite run.
SQLite output on this Windows host retains the fixture's mixed/lowercase labels;
the existing Linux test expects uppercase. This is outside the transport boundary.

## Provisional assumption disposition

| Handoff assumption | Classification | Evidence / limit |
|---|---|---|
| Primary top-level imports | CONFIRMED | Imported installed 1.1.6 classes |
| OracleReader(database="OASYS") construction | CONFIRMED | Installed constructor; live OASYS NOT TESTABLE |
| read(site=..., query=...) | CONFIRMED | Real MARS/ARIES calls |
| Leading site token KM | CONFIRMED | Captured KM for both successful readers |
| Alternate readers/mars, aries, oracle import paths | ADJUSTED | Replaced guesses with actual *_reader modules |
| No-argument MARS/ARIES construction | CONFIRMED | Live default OS-auth readers |
| pandas / to_pandas / convertible result | CONFIRMED | Actual pandas.DataFrame; alternative shapes NOT TESTABLE against this package |
| DataSyncX owns auth/config/retry, not legacy arguments | CONFIRMED | Installed source and real retries; Linux auth NOT TESTABLE |
| Construction failure vs unavailable transport | CONFIRMED | MissingCredentialError and existing factory regression; read ORA cause retained |
| Config reference consumed by DataSyncX | ADJUSTED | Reference remains metadata only; installed package uses .env/environment, not SCRIPTHOST_DATASYNCX_CONFIG_REFERENCE |
| Explain plan unsupported | CONFIRMED | Explicit UnsupportedQueryBackend remains; no emulation |
| TAB delimiter and UTF-8 without BOM | CONFIRMED | Real files and Unicode dual probe |
| pandas null emitted empty | CONFIRMED | Live dual probe; legacy dot equivalence NOT TESTABLE |
| No repeated append header | CONFIRMED | Real repeated execute probe |
| Empty-result fallback in ScriptHost | CONFIRMED | Real zero-row query, divergent alias/header |
| Historical populated /HEADERS override | ADJUSTED | Source says cursor labels win; tests now use mismatched labels |

## Changes, checks, and remaining gates

Only transport import fallback paths and explanatory diagnostics/comments changed
in production code. Added an opt-in live harness and two focused output tests.
Original ScriptHost files and report code were untouched.

Baseline: 79 passed, 2 failed (81 tests). After adjustments: **81 passed, 2 failed
(83 tests)** using `python -m pytest tests/vg2c_new -q`. Identical failures:

- `test_original_report_defer_layout_delete_lifecycle_characterization_on_linux`:
  Windows actually renders `80%`, whereas Linux characterization expects absence.
- `test_real_22844_mars_aries_and_sqlite_progress_through_original_lifecycle`:
  downstream Windows SQLite header case differs from the Linux expectation.

Ruff lint and format checks passed for the full direct-runtime scope plus harness;
compileall passed. No Linux test run is claimed. No tests were skipped or weakened
to manufacture a green result.

To clear production: run this harness and the 83-test suite in the intended Linux
deployment with its actual Oracle client, TNS, /app/.env, and Kerberos identity;
resolve any platform differences; obtain historical null/dot output evidence.
OASYS, explicit legacy credentials/retry options, alternate result shapes, and
explain plans are not part of the successful MARS/ARIES live gate.
