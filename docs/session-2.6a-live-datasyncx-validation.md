# Historical DataSyncX production validation evidence

This records earlier live validation, not the current architecture or completion
status. Current execution and cutover limits are in
[the final convergence handoff](final-runtime-convergence.md).

## Recorded execution

Environment: Windows 11, Python 3.14.6, DataSyncX 1.1.6, original ScriptHost
SPFManager/task lifecycle. The historical harness used the in-process entry inside
its own process; the current harness calls isolated `worker.run_job` directly.

The portable-transport override run returned MARS 3 rows, ARIES 7,580 rows and
original SQLite 1,114 rows. A preceding run and repeat/concurrency checks are in
[the archived evidence JSON](session-2.6a-live-datasyncx-evidence.json). Counts vary
because the real 22844 query uses a moving time window.

Before live DataSyncX calls, the historical observer asserted that original
ScriptHost expanded schema/SQL_Get_CSV_List tokens and that every distinct MARS lot
appeared in ARIES SQL. It recorded digests rather than business values.

Header labels, null versus literal dot, UTF-8/no-BOM, empty-result header fallback,
append without repeated headers and real Oracle exception propagation were checked.
Original source says cursor labels win for populated results. No live comparison
against the compiled historical driver was available in the Python 3.14 environment.

## Contract retained

- Public exports: MarsReader, AriesReader, OracleReader.
- MARS/ARIES default constructors; OracleReader(database="OASYS").
- read(site=..., query=...) returns pandas.DataFrame.
- DataSyncX owns authentication/configuration; installed configuration uses .env and
  environment. SCRIPTHOST_DATASYNCX_CONFIG_REFERENCE is diagnostic metadata only.
- SCRIPTHOST_FORCE_PORTABLE_QUERY_TRANSPORT=1 selects portable transport explicitly;
  other values preserve historical driver selection.

## Limits

Linux corporate authentication and live OASYS remain UNCERTIFIED. Explain-plan
execution is unavailable in portable transport. Historical compiled-driver null-byte
parity remains UNCERTIFIED. These records do not certify every original integration.

Current reproduction from the checkout, with DataSyncX/network access configured:

```sh
python scripts/architecture/validate_live_datasyncx.py --output data/new-live-run
```

Use a new output directory. It contains corporate output rows and logs and stays
local under ignored data/. Only sanitized evidence should be committed.
