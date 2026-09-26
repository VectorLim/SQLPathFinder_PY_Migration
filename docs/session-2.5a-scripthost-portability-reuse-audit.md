# Session 2.5A ScriptHost portability and direct-reuse audit

Base: `c51894176e89497c029036e825cd84bc0ecf1800`

## Import/dependency map

| Dependency area | Classification | 2.5A action |
|---|---|---|
| stdlib, pandas/numpy, pathlib, dateutil | PORTABLE REQUIRED | retained eager |
| requests, urllib3, chardet, tabulate, bson/pymongo, duckdb/pyarrow | PORTABLE OPTIONAL | guarded so unrelated paths import without them |
| win32api/win32com/winreg/pythoncom/pywintypes | WINDOWS-ONLY BUT UNUSED BY PORTABLE TARGET PATHS | platform/lazy boundary; no fake shims |
| pythonnet `clr` and System.* | WINDOWS-ONLY BUT UNUSED BY PORTABLE TARGET PATHS | platform/lazy boundary |
| COM, BAT, robocopy.exe, helper EXEs, Outlook | WINDOWS-ONLY REQUIRED BY HISTORICAL TRANSPORT | not restored |
| SQLPFaaS/service globals and historical DB/mail transports | LEGACY/OBSOLETE | not restored |

`SPFLib/__init__.py` was changed from a host bootstrap into a granular shared-import boundary. It no longer imports `SPFUtilities` back into itself. `PyUtils.py` no longer changes `sys.path` merely to locate optional legacy integrations. Standalone report modules now prefer package-relative imports.

## Session 2 utility decisions

The classification below is the final implementation authority after attempting the reuse ladder. "RETAINED vg2c_new PORT" means the original latest algorithm was traced but direct invocation would still require more host/global compatibility code than the existing focused implementation.

| Utility | Final classification | Why |
|---|---|---|
| WriteFileUtility | RETAINED vg2c_new PORT | Original task is embedded in the monolithic task executor; current implementation is the small current write/substitution/EOF contract. |
| DeleteFileUtility | RETAINED vg2c_new PORT | Original delete helper carries DOS/attribute/error-routing behavior; pathlib implementation is smaller and portable. |
| CopyFileUtility | RETAINED vg2c_new PORT | Original distribution algorithm is coupled to Utilities/global console/error routing; current port preserves current pattern/token semantics without host reconstruction. |
| DistributeUtility | RETAINED vg2c_new PORT | Alias of the portable copy authority; importing historical distribution would restore obsolete transport. |
| RenameFileUtility | RETAINED vg2c_new PORT | Historical implementation includes Windows lock/move behavior; current pathlib replacement preserves current semantics. |
| AppendFileUtility | RETAINED vg2c_new PORT | Original append path includes network staging, codec/global retry state and Utilities coupling; current dataframe implementation is materially smaller. |
| RoboCopyUtility | REWRITE | Transport is intrinsically `robocopy.exe`/Windows; shutil implementation is the Linux authority. |
| SetFileReadOnlyUtility | REWRITE | Historical file attributes are Win32-specific; chmod is the supported portable contract. |
| WaitIntervalUtility | RETAINED vg2c_new PORT | Original task wrapper is larger than the one-line semantic operation and requires task-host construction. |
| WaitFileUtility | RETAINED vg2c_new PORT | Original task includes Windows session probing; bounded portable polling is simpler. |
| ZipUtility | REWRITE | Historical helper executable transport is obsolete; stdlib zipfile is authoritative on Linux. |
| UnzipUtility | REWRITE | Historical helper transport is obsolete; stdlib implementation also provides zip-slip protection. |
| CsvUtility | RETAINED vg2c_new PORT | Current CSV semantics are spread across Utilities/MemTable and encoding/global helpers; extracting them would create a compatibility layer larger than this focused module. |
| SqliteQueryUtility | RETAINED vg2c_new PORT | MemTable owns class-level connection/Utilities state; removing it cleanly would amount to redesigning MemTable. Current per-operation connection is safer. |
| SqliteLoadUtility | RETAINED vg2c_new PORT | Same MemTable shared-state boundary; current selective port preserves current load/index behavior. |
| SqliteDeleteUtility | RETAINED vg2c_new PORT | Direct MemTable reuse would reintroduce shared connection ownership for a trivial operation. |
| SmartAppendUtility | RETAINED vg2c_new PORT | V4 remains intertwined with Utilities encoding/copy/global error-routing helpers. Isolating only V4 requires more compatibility surface than the current latest-only implementation. |
| XlsToCsvUtility | REWRITE | Historical Excel transport depends on Windows Excel/helper infrastructure. |
| LoadExcelUtility | REWRITE | Current openpyxl/pandas implementation replaces helper EXE/COM transport while preserving latest Version 2 behavior. |
| ImportExcelUtility | REWRITE | Same obsolete Excel transport boundary. |
| GetWebTextUtility | RETAINED vg2c_new PORT | Original web helper mixes portable requests with Kerberos/SSPI/proxy/global behavior; current requests path is smaller and dependency-injected. |
| RunPythonUtility | RETAINED vg2c_new PORT | Original Run_Python includes ScriptHost interpreter selection/service process behavior; current subprocess call preserves latest Python-v3 semantics. |
| PyScriptUtility | RETAINED vg2c_new PORT | Original task wrapper adds host execution state without reusable business logic. |
| RunRUtility | REWRITE | Historical R installation/Windows environment transport is not portable authority. |
| InlineRUtility | REWRITE | Same process/runtime boundary. |
| EchoUtility | RETAINED vg2c_new PORT | Original task-host wrapper is larger than the current direct semantic operation. |
| CsvToHtmlUtility | RETAINED vg2c_new PORT | Full historical report stack is deferred to 2.5B; current non-report conversion remains isolated. |
| CsvToXmlUtility | RETAINED vg2c_new PORT | Original path is embedded in Utilities/task infrastructure; current conversion is smaller. |
| XmlToCsvUtility | RETAINED vg2c_new PORT | Same. |
| StackDataUtility | RETAINED vg2c_new PORT | Original StackTable depends on broad Utilities/report/encoding machinery; focused dataframe port is smaller. |
| OracleQueryUtility | THIN ADAPTER | VG2 command semantics are retained while approved DataSyncX readers own transport; old DB drivers are intentionally not authoritative. |
| GetSiteTimeUtility | THIN ADAPTER | DataSyncX MARS reader is the approved modern transport; adapter only maps command/state. |
| PlatformGapUtility | THIN ADAPTER | Integration boundary remains explicit rather than restoring legacy service lookup. |
| EmailUtility | THIN ADAPTER | Command semantics retained; injected approved sender owns transport. Historical SMTPAuth/Outlook paths are obsolete. |
| RowsInFileUtility | RETAINED vg2c_new PORT | Original row-count helpers depend on broad Utilities encoding/file-type state; focused implementation is smaller. |
| ValueInFileUtility | RETAINED vg2c_new PORT | Same. |
| AgeOfFileUtility | RETAINED vg2c_new PORT | Direct task reuse would require host/global time/error state for a small filesystem calculation. |
| DateOfFileUtility | RETAINED vg2c_new PORT | Same. |
| UpdateTimeUtility | RETAINED vg2c_new PORT | Current explicit RuntimeState value replaces implicit ScriptHost global state. |
| GetFilesUtility | RETAINED vg2c_new PORT | Original helper includes console/global behavior; current filesystem projection is focused. |
| FileCompareUtility | RETAINED vg2c_new PORT | Original comparison path carries host error/console state; current implementation preserves current comparison semantics without it. |

## Why no Session 2 algorithm was moved back wholesale

The clarified audit did **not** reject reuse merely because `SPFGlobals` or Windows imports existed. Those import barriers were removed first. After doing so, the remaining Session 2 candidates fall into two groups:

1. their useful algorithm is inseparable enough from the giant `Utilities` / task executor API that making the original callable would require a compatibility surface larger than the existing focused port; or
2. the original implementation's transport is exactly the obsolete Windows/DB/mail/process mechanism the direct runtime intentionally replaces.

Therefore retaining these focused ports is a result of the reuse attempt, not a categorical ban on ScriptHost source.

## Direct reuse prepared for Session 2.5B

The standalone report stack is a better direct-reuse candidate than the Session 2 command utilities. 2.5A therefore makes its shared foundations importable without Windows host initialization:

- `SPSQL3_py` is now an importable package.
- `PyUtils` portable helpers can import without `winreg` or permanent `sys.path` mutation.
- `PyGraphingMethods` prefers package-relative `PyUtils`.
- `AutoComm_HTML_Report` prefers package-relative `PyUtils` and `AutoComm_ChartData`.
- `PyPlot_Class` was already substantially standalone and requires no 2.5A source change.

Full report command semantics remain intentionally deferred to Session 2.5B.

## State isolation

Mutable per-instance state is accepted. Shared execution state remains disallowed. The new portability tests instantiate `BuildArgsClass` twice and prove mutation of one instance does not affect the other. MemTable is deliberately not adopted because its connection and Utilities references are class-level shared state.

## Remaining Windows-only blockers

Windows-only methods remain in the vendored source and may still require Win32/COM/.NET when explicitly invoked. This is intentional: 2.5A makes portable paths granular; it does not fake or emulate unsupported transports.

## Session 2.5B starting point

Start from the final commit of `direct-runtime/session-2.5a-portability-reuse-audit`. Reuse the now-package-importable report/plot modules first, and amend only the report-specific remaining dependencies needed by `02B_HTML_REPORT_RUNTIME.md`.
