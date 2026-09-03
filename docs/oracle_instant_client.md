# DataSyncX Oracle Instant Client experiment

## Configuration

The generated workflow calls `OracleClient.configure()` before it creates a
`PipelineContext` or performs a DataSyncX read.  It is a no-op by default, so
the current full-client configuration remains the fallback.

To opt in on Windows for one PowerShell session:

```powershell
$env:DATASYNCX_ORACLE_CLIENT = 'instant'
$env:DATASYNCX_INSTANT_CLIENT_DIR = 'C:\oracle\instantclient_19_17'
$env:DATASYNCX_ORACLE_NET_CONFIG_DIR = 'C:\oracle\network\admin' # optional
.\.venv\Scripts\python.exe .\hamizah.py
```

The selected directory must contain `oci.dll`.  If no explicit directory is
set, the selector looks for an `instantclient*` directory containing `oci.dll`
on `PATH`.  In Instant Client mode it prepends that directory to the current
process `PATH`, removes `ORACLE_HOME` from that process, and optionally points
`TNS_ADMIN` at an explicit Oracle Net configuration directory.  It does not
persistently change user or machine environment variables.

After DataSyncX first opens (or attempts to open) an Oracle connection, the
terminal reports the loaded client once, for example:

```text

========================================================================
 Oracle client: 23.26.3.0.0 | mode=thick | source=Instant Client (C:\Oracle\instantclient_23_26)
========================================================================
```

## Revert

Remove `DATASYNCX_ORACLE_CLIENT` (or set it to `home`) and start a new Python
process.  No file or persistent environment change is required.  The original
environment and `.env` were backed up before this experiment under
`%LOCALAPPDATA%\SQLPathFinder_PY_Migration\oracle-client-backups`.

## Platform limitations

DataSyncX 1.1.6 calls `oracledb.init_oracle_client()` without `lib_dir`.
Windows supports that call through `PATH`, which this selector controls.  On
Linux, configure Instant Client with `ldconfig` (preferred) or
`LD_LIBRARY_PATH` before Python starts.  On macOS, or to use explicit
`lib_dir`/`config_dir` everywhere, DataSyncX needs an upstream change to pass
those arguments to `init_oracle_client()`.

Instant Client can use a separate `TNS_ADMIN` directory; it does not require a
full Oracle Home.  Features that need `sqlnet.ora`, wallets, NTS/Kerberos, or
other Oracle Net settings still require the corresponding configuration files
and compatible client/server policy.

## Validation on this workstation

On 2026-08-28, a read-only `MarsReader().read(site="KM", query="select 1 …
from dual")` probe returned one row with both configurations:

- Existing configuration: Oracle Client `11.2.0.4`.
- Instant Client configuration: Instant Client `19.17`, with `ORACLE_HOME`
  removed from the process and `TNS_ADMIN` pointed at the snapshot's copied
  `network-admin` directory.

Both runs emitted DataSyncX's existing pandas DBAPI warning; it is pre-existing
and does not indicate a client-loading failure.
