# Original ScriptHost capability status

Original SPFManager/GetQuery/task hierarchy/SPFGlobals is the sole semantic authority.
Production entry is `scripthost_portable.worker.run_job`, one fresh child per job.
Status applies only to the described, asserted subset. A successful return alone
is not evidence of correct output. No alternate report or utility runtime remains.

| Capability | Status | Evidence and limits |
|---|---|---|
| Parsing, macros, IF/ELSE, ForLoop, SiteLoop, RunLoop | SUPPORTED | Original task-tree characterization and worker output assertions in test_certification.py |
| WRITE-FILE, ROWS/VALUE/AGE/DATE-IN-FILE, GET-FILES | SUPPORTED | Original tasks, actual files and metadata; empty/missing sentinels retained |
| CSVTOHTML / CSVTOXML | SUPPORTED | Original markup semantics, Main/Item XML, dot for empty values |
| XMLTOCSV | SUPPORTED | Linux Main/Item roundtrip only; rejects other XML shapes |
| STACK-DATA and SMART-APPEND V4 | SUPPORTED | Union, sorting, updates/deletion/schema cases; original algorithm and column order |
| ZIP / UNZIP | SUPPORTED | Actual archives, preserved/flattened paths, traversal rejection |
| WAIT-FILE / WAIT-INTERVAL | SUPPORTED | Original polling/sleep count and existing-file behavior |
| SPFCopy/distribute, append/rename/delete, read-only | SUPPORTED | Native file operations after original task argument handling |
| RoboCopy subset | SUPPORTED | /S /E /MOV /NP /IS, retries; unknown switches rejected |
| Excel V2 LOAD/IMPORT, XLSToCSV | SUPPORTED | Characterized .xlsx/openpyxl subset; VBA/binary variants UNCERTIFIED |
| Python/PyScript, local R file/inline | SUPPORTED | Real local interpreters, argv and generated output assertions; remote execution UNCERTIFIED |
| Get-Web-Text V2 | SUPPORTED | Local HTTP/404 behavior; integrated authentication and SharePoint UNCERTIFIED |
| GET-SITE-TIME, UPDATE-TIME, persisted time | SUPPORTED | Memory query results and original persistence across workers |
| SQLite query/load/delete/UDFs | SUPPORTED | Original SQL, delimiters, regex and LOB contracts; case-preserving Linux file lookup |
| MARS/ARIES through DataSyncX | SUPPORTED | Public DataSyncX 1.1.6 readers; live Windows worker evidence and deterministic Linux boundary tests |
| OASYS | UNCERTIFIED | Public OracleReader constructor and original preprocessing tested; no live database certification |
| HTML-RUN/DEFER/LAYOUT/DELETE | SUPPORTED | Representative html_test/tcb_yield report slices, rendered data, sorting, CSS and cleanup; local destinations and batch mode |
| CleanDelimsCRLF boundary | SUPPORTED | Linux UTF-8 CSV/TAB subset: quoted fields, embedded comma/tab/newline/quote cleanup; no general executable equivalence claim |
| HTML-TAB/MENU, plotting, interactive viewer | UNCERTIFIED | Not required by the representative report slices |
| Linux EmailTask/SPFEmail delivery | SUPPORTED | DataSyncX SMTP (smtpauth.intel.com, `ATMANALYTIC`), sender atmanalytic@intel.com; `self` from `SCRIPTHOST_USER_EMAIL` (unset: skipped with warning); OnlyIntel retained; role verification UNRESOLVED (fails closed) |
| Generic Windows DOS/VA, Outlook/AD, remote interpreter services | UNCERTIFIED | Platform integrations outside the demonstrated subset |
| Legacy compiler/editor/reference runtime | RETIRED | vg2c, vg2c_ui, vg2c_new, their tests and alternate HTML/email implementations removed |

## Original behavior preserved

- WRITE-FILE and ROWS-IN-FILE retain original newline/blank-record behavior.
- CSVTOHTML keeps original raw cell markup.
- SmartAppend V4 uses original new-file column order and missing-column behavior.
- WAIT-FILE sleeps after each failed poll; PyScript appends SPFLOGLEVEL.
- UNZIP requires the destination expected by the original task.
- Nonempty query results use cursor labels; original empty-result headers remain.
- SQLite regex returns matched text or NULL, and LOB writes decode base64.
- Historical Windows database-driver selection remains; portable validation can set
  `SCRIPTHOST_FORCE_PORTABLE_QUERY_TRANSPORT=1`.

See [final convergence evidence](final-runtime-convergence.md) and the retained
[historical live validation](session-2.6a-live-datasyncx-validation.md).
