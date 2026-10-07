# ScriptHost inheritance overrides

The source baseline is `scripthost-utilities-decompiled/SPSQL3_py.zip`.
`original/SPSQL3_py` already contains portability edits and is not the pristine
comparison source. Legacy modules define their original classes and then
re-export the compatibility subclasses. Keep class names unchanged for private
attribute storage, and use zero-argument `super()` in re-exported base classes.

Original ScriptHost owns validation, query parsing, task execution, state,
status handling, and retries. Overrides access its module globals through
`legacy`; they replace unavailable operations or normalize inputs.

## Utilities

| Original method | Current adaptation | Reason |
| --- | --- | --- |
| `SPFRoboCopy` | Inherited; `_run_robocopy` replaces command execution | Windows ROBOCOPY is unavailable; original checks and exit-code interpretation remain authoritative |
| `File_Lock_Move` | Inherited; `_move_file` replaces command execution | COMSPEC move is unavailable; original locking/retries remain authoritative |
| `setEnv` | Normalize case aliases, then `super()` | POSIX environment names are case sensitive |
| `unzipString` | Existing implementation retained | UTF-8-first decoding differs from the detector; this is a semantic exception awaiting an encoding decision |
| `SPFDelete` | Linux-only file operation; Windows `super()` | Windows DEL is unavailable; preserve existing CSV/token parsing and messages |
| `ConvertDLM` | Linux helper; Windows `super()` | CleanDelimsCRLF.exe is unavailable |
| `IntelWW` | Linux numeric January 1 calculation; Windows `super()` | The legacy Windows English locale is unavailable on Linux |
| `Run_R` | Normalize current-directory argument, then `super()` | The literal `.\` is not the POSIX current directory |
| `SPFWebCopyPyReqs` | Inherited; `_web_auth_type` and `_web_verify` replace dependencies | SSPI and Windows certificate-store integration are unavailable; Linux retains system CA verification |
| `SPFEmail` | Existing Linux behavior only; Windows `super()` | AD identity, verifyrole.exe, Outlook, and native SMTP are unavailable; retain role rejection, configured self identity, DataSyncX SMTP, fixed sender, recipient filtering, and BCC privacy |
| `UnzipFile` | Linux helper; Windows `super()` | Replace the external Windows unzip operation |
| `GetFilePattern` | Inherited; `_file_pattern_separator` supplies separator | Windows path splitting cannot interpret POSIX roots |
| `SPFCopy` | Linux helper; Windows `super()` | COMSPEC COPY is unavailable |
| `LoadExcel2` | Existing Linux .xlsx LOAD/IMPORT branch; Windows `super()` | Excel executable/COM integration is unavailable; VBA and binary workbooks remain unsupported |

The command hooks in the vendor preserve the original Windows command,
arguments, and flags. Web hooks retain the archived SSPI import position and
PEM/auth evaluation order. The existing missing-SMTP guard is retained in the
original SMTP branch; it prevents fallback when that optional dependency is
absent. No Linux implementation is placed in the vendor.

## Other overrides

| Class/member | Current adaptation and reason |
| --- | --- |
| `SPFGlobals.gIsSvc` | Cache false when .NET service contexts are unavailable; otherwise original property |
| `SPFGlobals.gLocalDir` | Initialize POSIX separator, then original property/logging |
| `MemTable.Run_SQLite` | Large override retained pending the explicit architecture decision; differences are work-directory spelling, case/order of CSV import pairs, and temporary CSV path spelling |
| `MemTable.getStandaloneCon` | Original connection/UDF initialization, then add missing `CharIndex_v2` before attachment SQL |
| `MemTable.sqliteCharIndex_v2` | Added UDF required by original CSV-list SQL generation |
| `NormalQueryTaskBase.Prep_Inc_Process` | Normalize leading `.\`, then original incremental semantics, including legacy range behavior |
| `NormalQueryTaskBase.SubStitute_CT` | Pre-existing CrossTab filename-case normalization retained; outside this cleanup |
| `nqOracleTask.OpenConnection` | Original compiled driver when available; DataSyncX driver boundary when absent or explicitly selected |
| `GetSiteTimeTask.SetIni` | Normalize persisted current-directory INI path; original execution inherited |
| `UpdateTimeFileTask.GetIni` | Normalize persisted INI path; original execution inherited |
| `SmartAppendTask.Do_Update_Time_File` | Normalize INI input; original append/time algorithm inherited |
| `SetFileROTask.executeTaskCommand` | Linux chmod branch; original Windows attributes through `super()` |
| `XMLToCSVTask.Run` | Replace only the generated XMLTOCSV executable command; original validation, deletion, completion, and cleanup inherited |
| `EchoTask.executeTaskCommand` | Linux console echo only, with shell syntax rejection; original Windows execution through `super()` |
| `GetFilesTask._directory_glob`, `_glob_parts` | POSIX path construction; original scanning, metadata, and error handling inherited |

`_UnavailableLegacyDBDriverBase` and `NodesInfo` remain minimal fallback shapes
when compiled classes cannot be imported. They are not alternate query parsers.

The driver contract owns CSV writing, headers, append behavior, delimiter, and
row counts. `query_transport.py` implements that missing driver contract;
moving it to query tasks would change ownership. All six `file_operations.py`
functions remain supported replacements, exercised by runtime paths/tests.

The runtime, Python facade, worker, and launcher retain their existing roles.
Process isolation is required because the original runtime mutates cwd,
environment, and shared state. AED orchestration and migration-tool package
boundaries are documented in `docs/scripthost-portability-cleanup.md`; no package
move is part of this cleanup.

## Verification

`tests/scripthost_portable/test_adapter_characterization.py` records incremental
paths/range/errors, UDF registration before attachment SQL, move retries/errors,
newest-folder selection, Windows delegation, and the missing-SMTP guard.
Existing utility integration tests execute actual original tasks and compare
file output, state, and transport behavior. See the cleanup report for baseline
and final results and platform limitations.
