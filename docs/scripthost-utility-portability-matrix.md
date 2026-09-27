# ScriptHost utility portability matrix

## Authority and evidence

Starting checkout: `fab2c5f9583177f706be41a9eb07881cbe0d25c3`, parent `c7e2d44`,
query-worker ancestor `2063649`; clean working tree. Windows baseline: 89 passed,
2 Linux-specific characterization failures. No live queries were repeated in this audit.

Original SPFManager -> GetQuery -> task -> SPFGlobals/Utilities remains authoritative.
Production jobs use `scripthost_portable.worker.run_job`: one fresh child per job.
No production import of the RETIRED compiler/editor/reference packages is needed.

**Pre-deletion Linux gate:** commit `5ccab6f`, [Ubuntu CI run 36315746227](https://github.com/VectorLim/SQLPathFinder_PY_Migration/actions/runs/36315746227):
119 tests passed, format, compile, lint and process-isolation benchmark passed.
Docker Desktop's API was inaccessible locally; a CLI restart did not resolve access.
Linux evidence comes from real Ubuntu worker execution, not Windows platform simulation.

Test names below refer to `tests/scripthost_portable/test_utility_contracts.py` unless stated.
Original task classes are in `SPFLib/SPFSQL3.py`; Utilities methods are in
`SPFLib/SPFUtilities/utils.py`. Generated fixtures are real temporary CSV/TAB/XML,
ZIP, XLSX, Python and R files executed through original VG2 tasks. These are bounded
capability proofs, not blanket equivalence for every historical option.

## Portability decisions before deletion

| Capability | Original class / method | Linux status and actual blocker | Amendment | Duplicate decision | Executed fixture / test |
|---|---|---|---|---|---|
| WRITE-FILE | WriteFileTask.executeTaskCommand | Pass unchanged; original preserves separator newlines and stops at EOF | None | Delete WriteFileUtility | write_rows_value_and_metadata; original_runtime_vertical_slice |
| ROWS-IN-FILE | RowsInFileTask -> getRowCountFromFile | Pass unchanged, counts original blank record | None | Delete RowsInFileUtility | write_rows_value_and_metadata; original_runtime_vertical_slice |
| VALUE-IN-FILE | ValueInFileTask | Pass unchanged, EMPTY/ERROR sentinel behavior | None | Delete ValueInFileUtility | value_empty_and_missing; write_rows_value_and_metadata |
| AGE-OF-FILE | AgeOfFileTask | Pass unchanged | None | Delete AgeOfFileUtility | write_rows_value_and_metadata, timestamped file |
| DATE-OF-FILE | DateOfFileTask -> GetFileDate | Pass unchanged | None | Delete DateOfFileUtility | write_rows_value_and_metadata |
| GET-FILES | GetFilesTask.getFilesInfoFromFolderGlob | Failed on backslash glob construction; now passes recursive listing | Use native separator/path joining | Delete GetFilesUtility | get_files, nested directory |
| FILE-COMPARE | FileCompareTask | Equal-file comparison passes; optional email notification remains transport-bound | None for comparison | Delete FileCompareUtility; keep notification limitation explicit | file_compare |
| CSVTOHTML | CSVToHTMLTask | Pass unchanged; raw HTML cell content is original behavior | None; do not adopt port escaping | Delete CsvToHtmlUtility | csv_html_xml, markup cells |
| CSVTOXML | CSVToXMLTask | Pass unchanged; Main/Item format and dot for empty value | None | Delete CsvToXmlUtility | csv_html_xml |
| XMLTOCSV | XMLToCSVTask | Windows spfExcelUtility.exe unavailable | Bounded Main/Item conversion at executable call boundary; other XML shapes rejected | Delete XmlToCsvUtility; Linux roundtrip passed at 081b611 (run 36315838567); no general Excel XML parity claim | xml_to_csv_original_roundtrip; xml_converter_rejects_unproven_shape |
| STACK-DATA | StackDataTask.execute | Pass unchanged, union and numeric sorting | None | Delete StackDataUtility | stack_data, two files with different columns |
| SMART-APPEND V4 | SmartAppendTask.smartAppend4_file_pandas -> File_Lock_Move | Algorithm passes; Windows move prevented final replacement | shutil.move at original move boundary | Delete SmartAppendUtility | smart_append_v4, deletion/update/schema-union fixture |
| ZIP | SPFZipTask -> ZipFiles2/ZipFolder2 | Python zipfile/shutil already present; file archive passes | None | Delete ZipUtility | zip_original |
| UNZIP | SPFUNZipTask -> UnzipFile | Windows unzip.exe unavailable | Plain zipfile operation; preserve/flatten paths; validate all members before extraction | Delete UnzipUtility; gate passed | unzip_original_task Y/N; unzip_rejects_escape_before_extracting |
| WAIT-FILE | WaitFileTask -> WaitFile | Pass unchanged; original sleeps after each failed poll | None | Delete WaitFileUtility | wait_existing_and_zero_interval; wait_poll_count_original (clock patched only) |
| WAIT-INTERVAL | WaitIntervalTask -> WaitInterval | Pass unchanged | None | Delete WaitIntervalUtility | wait_existing_and_zero_interval |
| Python | RunPythonScriptTask -> Run_Python | Local execution passes using existing PYTHON3 INI setting | None | Delete RunPythonUtility | python_original_launch(False), real interpreter/argv |
| PyScript | PyScriptTask -> Run_Python | Pass including appended SPFLOGLEVEL argument | None | Delete PyScriptUtility | python_original_launch(True) |
| R file / inline | RunRFileTask/RunRScriptTask -> Run_R | Existing local RTERM launch works with Linux R; inline temporary path used .\ | Normalize only inline temporary directory | Delete RunRUtility/InlineRUtility | r_original_local_interpreter(False/True), real R |
| Get-Web-Text V2 | GetWebTextTask -> SPFWebCopyPyReqs | SSPI/Windows certificate dependency; public HTTP now passes | Windows keeps SSPI; POSIX requests uses verified system CA, no implicit integrated credentials | Delete public-web duplicate; authenticated/SharePoint paths remain unproven | web_v2_original_local_http, real localhost server and 404/no-abort |
| SPFCopy / distribute | SPFCopyTask -> SPFDistribute -> GetFilePattern/SPFCopy | COPY command and path separators blocked Linux | Reuse original token selection; plain glob/shutil copy | Delete CopyFileUtility/DistributeUtility | copy_original_distribution_tokens, newest file and wildcard |
| RoboCopy | RoboCopyTask -> SPFRoboCopy | ROBOCOPY executable unavailable | Plain operation for /S /E /MOV /NP /IS; original task keeps defaults/open/missing-file/pass-code handling; other switches reject | Delete RoboCopyUtility for characterized subset | robocopy_original_task, recursive move/empty directory |
| Excel V2 LOAD/IMPORT | LoadExcelTask/ImportExcelTask -> LoadExcel2 | spfExcelUtility.exe unavailable | openpyxl operation inside original helper; .xlsx, CSV lists/sheets; explicit errors for VBA and binary formats | Delete LoadExcelUtility/ImportExcelUtility for supported subset | excel_load_import_original_tasks, template and added sheet |
| XLSToCSV | XLSToCSVTask | Existing pandas/openpyxl branch passes | None for tested .xlsx path | Delete XlsToCsvUtility; newline-replacement and legacy .xls need separate evidence | xlsx_to_csv_original |
| Append / rename | AppendFileTask / SPFRenameTask | Pass unchanged | None | Delete AppendFileUtility/RenameFileUtility | append_rename_delete |
| Delete | SPFDeleteTask -> SPFDelete | Windows DEL silently did nothing | Native file-only deletion after original argument parsing; directory contents are nonrecursive | Delete DeleteFileUtility | append_rename_delete; report cleanup also now removes temp INI |
| Set read-only | SetFileROTask | Linux read-only/read-write fixture passes | POSIX chmod replaces only win32 attributes | Delete files.py | readonly_original_task; Ubuntu 87d151e |
| Email | EmailTask -> SPFEmail/GetEmailAdss | SMTP helper unavailable; Outlook/identity/role lookups platform-bound. No send_msg implementation found in installed DataSyncX source | No guessed provider or recipient-policy rewrite | UNRESOLVED; reference deleted | email_original_task_keeps_role_and_recipient_policy captures task/helper boundary only; no email sent |
| GET-SITE-TIME | GetSiteTimeTask -> nqOracleTask.Do_Get_Time | GMT and database time pass; transport previously wrote only files | Send memory-bound query results into original MemTable.LoadDF | Delete GetSiteTimeUtility | gmt_update_time_original; test_scripthost_query_transport original_get_site_time_then_update |
| UPDATE-TIME / UPDATE-TIME-FILE | UpdateTimeTask/UpdateTimeFileTask -> Do_Update_Time/Do_Update_Time_File | Original offsets and persisted .spf$data pass across fresh workers with explicit INSTANCE | Native current-directory path for .spf$data | Delete file_values.py | time_file_persistence_original; gmt_update_time_original |
| SQLite query/load/delete | nqSQLiteTask / SQLiteLoadTask / SQLiteDeleteTask -> MemTable | Query pipeline and load/delete pass; uppercasing source paths prevented Linux imports | Preserve case when deduplicating POSIX input paths | Delete sqlite.py; original edge fixtures pass | sqlite_load_delete_original; sqlite_original_udfs; sqlite_original_reference_edges; real_22844_mars_aries_and_sqlite_progress_through_original_lifecycle |
| Oracle query | nqOracleTask -> PortableOracleConnection | Existing original transport suite passes; Windows live evidence predates audit | Preserve original gLoadToMemTable result destination | Delete query.py, including unused PlatformGapUtility | test_scripthost_query_transport; test_scripthost_worker |
| Echo / generic DOS/VA helpers | EchoTask/DOSCmdTask/vaTask -> Run | Literal ECHO passes after removal of COMSPEC call on POSIX; arbitrary Windows shell syntax remains unsupported | Console output for literal ECHO; reject shell operators | Delete misc.py; no generic shell translation | echo_original; source trace of generic helpers |
| Parser / controllers | SPFManager.GetQuery; StartMacroTask, IfThenTask, ForLoopTask, SiteLoopTask, RunLoopTask | Existing original control-flow, HPC, 22844 task-tree and worker suites pass | None | Delete parallel parser/manifest and Interpreter; retain only email reference dependencies | test_original_scripthost_runtime vertical_slice, site_loop, run_loop_final_chunk, local_hpc_scope, real_22844_builds_original_task_tree |
| CSV support | Utilities.GetFileDLM/getRowCountFromFile; NormalQueryTaskBase.Process_Get_CSV_List | Original delimiters, rows and real query expansion pass; apostrophe escaping and deduplication now have a dedicated original fixture | None | Delete csv.py; quote/dedup Linux fixture passes | write_rows_value_and_metadata; test_scripthost_query_transport original_csv_list_quotes_and_deduplicates |
| Reports | Original HTML* task hierarchy | Existing characterization passes with original data after shared file-case/delete fixes | No report rewrite | Separate next phase | test_original_scripthost_runtime report fixtures |

## Observed semantic differences to preserve

- WRITE-FILE retains newlines counted by original ROWS-IN-FILE.
- Original CSVTOHTML does not HTML-escape cell contents.
- Original SMART-APPEND V4 orders the new-file columns first and leaves absent
  columns empty in the exercised branch, even when MyDefNew is supplied. The port
  instead used old-file column order and inserted the supplied default.
- WAIT-FILE with three failed polls sleeps three times, not twice.
- PyScript appends /SPFLOGLEVEL; Python file-mode raw arguments are split by the
  original launch logic. The port's argv handling differs.
- Original UNZIP requires an existing destination folder. Its portable helper must
  not bypass that task-level validation.
- The two baseline Windows failures were platform-characterization assertions, not
  query transport regressions. Windows SQLite labels retain mixed case; Linux
  labels remain uppercase. Shared portable deletion legitimately changes the
  old Linux report cleanup limitation without altering report algorithms. Preserving
  input path case also restores report table data.

## Final convergence status

The migration/reference package and compiler/editor are RETIRED and removed.
Linux email delivery is UNRESOLVED at original EmailTask/SPFEmail; recipient and
role restrictions remain authoritative. No alternate email implementation is retained.
Original report rendering is certified separately through isolated worker jobs.

## Verified cleanup

Pre-deletion gates: Ubuntu 5ccab6f (119 passes), 081b611 (122 passes), ba82dad
(103 passes after early duplicate removals) and
[b1502e3, run 36317277087](https://github.com/VectorLim/SQLPathFinder_PY_Migration/actions/runs/36317277087)
(105 passes including the final SQLite/CSV reference edge fixtures). Each successful
gate also passed format, compilation, lint and process-isolation checks.

Removed eleven duplicate utility modules: composition, excel, file_values, files,
misc, process, query, smart_append, web, sqlite and csv. Also removed the parallel
parser/manifest, Interpreter and compare_vars implementation, plus port-only parser
and interpreter tests. Original tests live in tests/scripthost_portable and do not
import reference runtime semantics. An import guard enforces that separation.

All remaining email reference code and its model/state/path/protocol dependencies
are removed. Production remains
VG2 -> fresh child -> original SPFManager/GetQuery/task/SPFGlobals.

Original SQLite regex search returns matched text (or NULL), not a boolean. Regex
arguments are pattern-first with integer flags; LOB writes decode base64;
SPFPrepLikeValue prepares SQL LIKE patterns rather than translating shell wildcards.
These original contracts were preserved without changing the UDF implementations.

Shared path-case and delete fixes also restore original report data and temporary-file
cleanup. No report algorithm was rewritten. Arbitrary Windows DOS/VA helpers,
remote interpreter services, integrated web auth, Outlook/AD and richer Excel/XML
modes remain outside the demonstrated Linux subset. Their success is not implied
by an original task returning without an exception.
