# Session 2.5A — ScriptHost Linux Portability + Direct-Reuse Audit

Base: `c51894176e89497c029036e825cd84bc0ecf1800`

This audit follows the reuse ladder:

`DIRECT IMPORT → DIRECT REUSE → THIN ADAPTER → AMEND ORIGINAL → PORT → REWRITE`.

The direct runtime remains `VG2 → Command → Interpreter → Utility.apply()`. It does not use
`SPFGlobals`, `PipelineContext`, SQLPFaaS, or a legacy service locator as execution state.

## Authoritative source

The checked-in `SPFLib/SPFSQL3.py` placeholder was zero bytes at the Session-2 base. The
authoritative decompiled source was recovered byte-for-byte from
`scripthost-utilities-decompiled/SPSQL3_py.zip`; it is ScriptHost Extract Engine 2.0.9.5
(22,388 lines).

## Import/dependency map

| Area | Classification | Audit result |
| --- | --- | --- |
| `SPFLib/__init__.py` stdlib, pandas/numpy, requests/dateutil | portable required | kept importable on Linux |
| bson/pymongo/chardet/tabulate/requests_kerberos/duckdb/pyarrow | portable optional | missing packages no longer block base package import |
| pywin32/COM/pythoncom/winreg/pythonnet/System.* | Windows-only, unrelated to portable target paths | guarded/lazy; no fake shims |
| `SPFGlobals` | legacy/global runtime | importable reference, rejected as direct-runtime state |
| `SPFUtilities.utils.Utilities` | mixed portable + Windows + singleton/global behavior | portable delimiter/ZIP algorithms extracted; giant object not used by the new runtime |
| `SPFUtilities.memtable.MemTable` | portable SQLite mixed with process-global class state | rejected for direct reuse |
| `PyUtils` | mostly portable; registry/Mongo integrations optional | registry and pymongo imports localized to the methods that need them |
| report/chart modules | portable Python with optional plotting backends | package-relative imports; plotting dependencies fail only when plotting is constructed |

## Direct-reuse decisions

| Runtime area | Latest ScriptHost authority | Decision | Why |
| --- | --- | --- | --- |
| CSV delimiter | `Utilities.GetFileDLM` | **DIRECT REUSE** via `SPFUtilities.portable.get_file_delimiter` | pure extension→delimiter rule; no global state needed |
| ZIP files/folders | `Utilities.ZipFiles2`, `Utilities.ZipFolder2`, `SPFZipTask` | **DIRECT REUSE / THIN ADAPTER** | current ScriptHost implementation is already Python `zipfile`/`shutil`; state/logging removed |
| UNZIP | `SPFUNZipTask` → `Utilities.UnzipFile` | retain portable port | latest source still invokes `unzip.exe`; new runtime also enforces zip-slip protection |

The historical `Utilities.GetFileDLM`, `ZipFiles2`, and `ZipFolder2` methods delegate to the
same stateless portable authority used by `vg2c_new`, so there is one maintained algorithm for
those semantics.

## Session-2 utility re-audit

| Module / utility | Authoritative ScriptHost path | Result | Reason |
| --- | --- | --- | --- |
| files: write | `WriteFileTask` | retain port | task wrapper/global console are larger than the pathlib operation |
| files: delete | `SPFDeleteTask`, `Utilities.SPFDelete` | retain amended port | legacy fallback uses BAT/Windows attributes |
| files: copy/distribute | `SPFCopyTask`, `Utilities.SPFCopy/SPFDistribute/GetFilePattern` | retain amended port | legacy current path still includes COMSPEC/COPY and global console/error state |
| files: rename | `SPFRenameTask` | retain amended port | legacy lock/move path is Windows-oriented |
| files: append | `AppendFileTask` / append helpers | retain amended port | source staging, encoding/global helpers and task state outweigh adapter |
| files: robocopy | `RoboCopyTask`, `Utilities.SPFRoboCopy` | retain rewrite | transport is intrinsically `robocopy.exe` |
| files: read-only | `SetFileROTask` | retain amended port | Win32 attributes are not portable; chmod owns READONLY/READWRITE |
| files: wait interval/file | `WaitIntervalTask`, `WaitFileTask` | retain tiny ports | direct task reuse would import task/global infrastructure for a sleep/poll loop |
| files: zip | `SPFZipTask` + `ZipFiles2/ZipFolder2` | **switch to direct reuse** | portable current implementation, stateless extraction is smaller |
| files: unzip | `SPFUNZipTask` | retain port | external Windows executable plus security hardening in new runtime |
| csv shared behavior | `Utilities.GetFileDLM/getRowCountFromFile`, value/file task paths | delimiter switched; other ports retained | row-count/value paths pull encoding/archive/MemTable helpers; current selective implementations are smaller |
| sqlite | `nqSQLiteTask`, `SQLiteLoadTask`, `SQLiteDeleteTask`, `MemTable` | retain selective port | `MemTable.myMemTable`, `myUtils`, table metadata and other class state are process-global |
| SmartAppend V4 | `SmartAppendTask.smartAppend4_file_pandas/process_csv/return_old_file` | retain amended port | V4 still depends on `SPFTaskBase`, WorkDir/RNStr, encoding globals, `SPFRoboCopy`, console/logger and mutable task fields |
| Excel | `XLSToCSVTask`, `LoadExcelTask`, `ImportExcelTask` | retain amended port | pandas/openpyxl paths exist but are embedded in task state and retain executable/COM fallbacks |
| web | `GetWebTextTask`, `Utilities.SPFWebCopyPyReqs` | retain rewrite | legacy current transport uses Windows negotiate auth/certificate-store behavior |
| process | `RunPythonScriptTask`, `RunRFileTask` | retain amended port | legacy executable selectors/HPC/ScriptHost transport are platform/runtime infrastructure |
| misc: echo | `EchoTask` | retain tiny port | adapter would be larger than behavior |
| misc: CSV/XML conversion | `CSVToHTMLTask`, `CSVToXMLTask`, `XMLToCSVTask` | retain ports | task-global wrappers; full report semantics intentionally deferred |
| misc: StackData | `StackDataTask.execute` | retain amended port | source creates shared `MemTable` and relies on `SPFTaskBase`; pandas-only port is smaller |
| query | `nqOracleTask`, site-time/query handlers | retain rewrite boundary | approved modern transport is DataSyncX; old DB stack is intentionally not reused |
| email | `EmailTask` / SMTP/Outlook helpers | retain rewrite boundary | approved modern transport is DataSyncX; old DB/mail/Outlook stack stays out |
| file-values/metadata | RowsInFile/ValueInFile/GetFiles/FileCompare/UpdateTime task paths | retain selective ports | original paths share task/global/MemTable helpers; new implementations are small and stateless |

## State/isolation finding

Mutable instance state was not rejected by itself. The rejection point is hidden shared state.
`SPFGlobals`, `Utilities.logger`, and `MemTable` use class/process-level state, while the
selected portable delimiter and ZIP helpers are pure/stateless. Repeated ZIP characterization
therefore exercises separate invocations without shared mutable state.

## Report portability prep

- `SPSQL3_py` is a normal package discovered by setuptools; no runtime `sys.path` patch is used.
- report imports are package-relative;
- `PyUtils` does not import Windows registry or pymongo until those specific integrations run;
- matplotlib/seaborn and plotly imports are optional at module import and raise an explicit
  runtime error when a plot object is constructed without its backend.

Session 2.5B still owns report command semantics and explicit cross-command report state.

## Remaining Windows-only blockers

The full `SPFSQL3.py` engine remains intentionally unsuitable as a Linux direct-runtime entry:
it imports the historical DB-driver graph and many task classes whose execution contracts assume
Win32/COM/.NET, ScriptHost services, or `SPFGlobals`. This does not block the portable modules
selected above and is not a reason to recreate that infrastructure.

## Session 2.5B starting point

Start from the final Session-2.5A commit on
`direct-runtime/session-2-utilities-integrations`. Reuse the now-packageable report modules
directly; add only explicit minimal report state required by the report commands. Do not route the
new runtime through `SPFGlobals` or the full `SPFSQL3.py` engine.
