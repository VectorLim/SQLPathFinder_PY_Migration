# Session 2.5A — ScriptHost Linux portability and direct-reuse audit

Base: `direct-runtime/session-2-utilities-integrations@c51894176e89497c029036e825cd84bc0ecf1800`

This review treats the vendored ScriptHost source as the semantic authority, but not as an
architecture that must be preserved. Direct reuse wins only when the reusable path stays smaller
than the existing `vg2c_new` port and does not restore global runtime state or obsolete transport.

## Import/dependency map

| Area | Classification | Session 2.5A result |
| --- | --- | --- |
| Python stdlib, pandas/numpy, requests | Portable required | Kept available from the ScriptHost package boundary. |
| chardet, tabulate, pymongo, duckdb/pyarrow, requests-kerberos | Portable optional | No longer required merely to import `SPFLib`; optional imports are isolated. |
| Win32, COM, CLR/System, registry | Windows-only | No longer imported by `SPFLib` on Linux. Windows-only legacy paths remain unsupported rather than shimmed. |
| Dask/Rich progress support | Portable optional | Dask's unused global import was removed; Rich is imported only around its progress implementation. |
| legacy SMTP/DB driver | Legacy/transport-specific | No longer imported by the giant Utilities module at module import time; the mail driver is scoped to legacy `SPFEmail`. |
| `SPFGlobals` | Legacy execution state | Kept only for the historical source; not adopted as direct-runtime state. |
| `MemTable` singleton connection | Legacy shared state | Import path cleaned up, but not reused by the direct runtime. |
| standalone AutoComm/report/plot modules | Portable report code | Package-relative imports fixed and portable dependencies declared for Session 2.5B. |

## Reuse findings

### SmartAppend V4

Authoritative source:
`SPFLib/SPFSQL3.py::SmartAppendTask`, especially
`smartAppend4_file_pandas`, `process_csv`, `parseDelCriteria`, `validateHDRS`, and
`GetUnionOfCols`.

Direct reuse was rejected after structural characterization. `SmartAppendTask` inherits
`SPFTaskBase` and has substantial class-level task state. The current V4 path reaches task/global
properties such as `gMySPFJobDT`, `gMySPFGWDT`, `gMyLocal`, `gSPFInstance`, logging/console
helpers, encoding helpers, `SPFRoboCopy`, and `MemTable`. `validateHDRS` and
`GetUnionOfCols` explicitly use `MemTable`. Only `process_csv` is effectively isolated.

Decision: retain `vg2c_new.utilities.smart_append.SmartAppendUtility`. Adapting the original V4
class would require rebuilding more task/global infrastructure than the existing amended port.

### MemTable / SQLite

Authoritative source:
`SPFLib/SPFUtilities/memtable.py::MemTable` (`LoadFromFile`, `Run_SQLite`,
`Create_SQL_In_Like_List`, `getStandaloneCon`).

`MemTable` has process-wide class state including `myMemTable`, `myUtils`, `logger`,
`tbl_ColNames_Dict`, and writer flags. Construction also creates `Utilities`, whose class inherits
`SPFGlobals`.

Decision: retain the per-operation stdlib `sqlite3` implementation in
`vg2c_new.utilities.sqlite`. Reusing `MemTable` would violate the no shared execution-state
boundary.

## Session 2 utility decisions

| Module | Authoritative ScriptHost/current source | Decision | Reason |
| --- | --- | --- | --- |
| `files` | `WriteFileTask`, `SPFDeleteTask`, `SPFCopyTask`, `SPFRenameTask`, `AppendFileTask`, `RoboCopyTask`, `SetFileROTask`, `Wait*Task`, `SPFZipTask`, `SPFUNZipTask`; Utilities file helpers | Retain port/rewrite | Original task wrappers depend on `SPFTaskBase`; robocopy/attributes/legacy transports are Windows-specific. The pathlib/shutil implementation is smaller than an adapter around the task runtime. |
| `csv` | `Utilities.GetFileDLM`, `Utilities.getRowCountFromFile`, `ValueInFileTask`; current `CsvIO` for SQL list behavior | Retain stateless port | `Utilities` inherits `SPFGlobals`; current CSV helper has no hidden state and already isolates portable semantics. |
| `sqlite` | `MemTable`, SQLite task classes | Retain port | Shared class connection and `Utilities` coupling would recreate process-global state. |
| `smart_append` | `SmartAppendTask` V4 | Retain amended port | V4 still reaches task globals, MemTable, logging/console, encoding and staging helpers; adapter would be larger than the port. |
| `excel` | `LoadExcelTask` / `ImportExcelTask` / `Utilities.LoadExcel2` | Retain rewrite | Latest legacy transport is helper-exe/COM oriented. Portable pandas/openpyxl is the correct transport boundary. |
| `web` | `GetWebTextTask` / `Utilities.SPFWebCopyPyReqs` | Retain amended port | Requests semantics are portable, but the original path is coupled to Windows/enterprise authentication and `Utilities`; the injected requests session is the thinner boundary. |
| `process` | `RunPython*Task`, `PyScriptTask`, `Utilities.Run_Python`, R task helpers | Retain amended port | Original code carries historical interpreter/app-server/platform selectors. Direct `subprocess` with current interpreter/Rscript preserves current semantics without compatibility history. |
| `misc` | CSV/HTML/XML and StackData task classes | Retain port | Simple task algorithms are smaller as direct stateless code; StackData's original path reaches MemTable/UI/task state. |
| `query` | Oracle/GetSiteTime task paths plus current query routing | Retain DataSyncX adapter | Legacy DB/.NET/SQLPFaaS transports are explicitly out of scope. DataSyncX is the approved modern boundary. |
| `email` | `EmailTask` / `Utilities.SPFEmail` | Retain DataSyncX adapter | Legacy SMTP/SMTPAuth/Outlook transport is intentionally not restored. |

No Session 2 utility was switched merely to increase a "direct reuse" count. For every candidate,
the reusable ScriptHost object either owns broader global/task state or embeds an obsolete transport.
Keeping the existing port is less code and has clearer ownership.

**Duplicate Session 2 algorithm LOC deleted: 0.** This is intentional: no duplicate passed the
direct-reuse acceptance test. Deleting a port would have required adding a larger compatibility
adapter or restoring legacy runtime/transport infrastructure. The code reduction in this phase is
instead at the import boundary: unused/global platform imports and eager legacy dependencies were
removed or localized at their point of use.

## Directly reusable source prepared for Session 2.5B

The following report-side source is now structured for normal package import:

- `SPSQL3_py.PyUtils` — Windows registry and Mongo dependencies are lazy; portable helpers such as
  `BuildArgs` and `FixString` can be imported without Win32.
- `SPSQL3_py.PyGraphingMethods` — package-relative `PyUtils` import.
- `SPSQL3_py.AutoComm_ChartData` — package-relative graph/helper imports.
- `SPSQL3_py.AutoComm_HTML_Report` — package-relative helper/chart imports.
- `SPSQL3_py.PyPlot_Class` — already portable once Plotly is installed.

Full report command/state semantics remain Session 2.5B work.

## Full task-module boundary

After the package-relative import fix, the Linux audit can import `SPFLib`, `SPFGlobals`,
`SPFUtilities.utils`, and `SPFUtilities.memtable` successfully. Importing the full
`SPFSQL3` monolith then stops at its eager `dbDrivers` import. That package is not present in
the vendored decompiled source tree, and its historical role is the legacy database/service
transport that this migration explicitly does not reconstruct.

This is therefore a deliberate boundary rather than a portability TODO for Session 2.5A:
portable reusable modules are importable; the monolithic legacy task engine remains dependent on
unvendored/obsolete transport infrastructure.

## State-isolation conclusion

Fresh-instance reuse does not make `Utilities`, `SmartAppendTask`, or `MemTable` invocation-local:
the first two inherit or reach class/global ScriptHost state, and `MemTable` owns class-level
connections/state directly. The direct runtime therefore continues to keep its own explicit
`RuntimeState` and per-operation resources.

## Test intent

Session 2.5A adds Linux checks for:

- importing portable ScriptHost core without importing Win32/COM/CLR;
- clear failure helper for explicitly Windows-only operations;
- fresh-state behavior of portable report helpers;
- package imports of the prepared report modules;
- all existing Session 1/2 runtime characterization unchanged.

Validated on Ubuntu 24.04 / Python 3.12 with 52 tests passing. The only warning is an existing
`datetime.utcnow()` deprecation inside historical `SPFGlobals`; it does not affect runtime
behavior or the direct-runtime state model.
