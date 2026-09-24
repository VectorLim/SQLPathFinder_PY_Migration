# Direct ScriptHost-compatible runtime: target architecture and migration plan

**Status:** architecture proposal only. Build an importable, independent `src/vg2c_new/` package; leave `vg2c` intact during comparison. The output is execution of `.txt` scripts in Linux Docker. Generated Python and the existing UI are not product requirements. This is based on static inspection of this checkout, extracted ScriptHost, and locally installed DataSyncX 1.1.6; no legacy script, live database, email, or Linux image was run in this planning pass.

## 1. Final architecture

~~~text
.txt script -> parse and bind -> one executable Command/task tree -> ordered Interpreter
                                                    |-> control nodes: macro, IF/ELSE, loops
                                                    `-> bound Utility.apply(command, state)
                                                                     |-> portable adapted ScriptHost algorithm
                                                                     |-> portable local implementation
                                                                     `-> DataSyncX query / mail
~~~

**One representation.** A `Command` is also the executable task node: it has a normalized command name/kind, original ordered options (including duplicates where relevant), raw body and utility argument text, source span, child commands, and for atomic commands a bound concrete `Utility` instance; an IF node also has its else children. This adopts ScriptHost's useful choice to construct a task tree during parsing. Typed command-specific values may be attached after validation, but there is no public `ParsedBlock -> ClassifiedBlock -> ResolvedBlock -> DispatchedBlock` chain and no second task model. Parse once into this authoritative tree. Bind the handler from the registry when the literal command name is known; never look it up again at execution. Keep raw values until execution where macro/global substitution depends on current frames. Resolve static command names, nesting, option syntax, and literal argument structure during parsing; resolve dynamic substitutions and paths at execution. A dynamic command name, if real scripts use one, needs a narrow explicit late-binding rule and a test. A malformed/unknown command raises a source-located error rather than silently becoming an external shell command.

**One execution path.** `Interpreter.run(tree, state)` visits children in source order, owns controller behavior and error policy, and calls each atomic node's already bound `utility.apply(command, state)`. Controllers are tree syntax, not utility wrappers around an interpreter. No runtime manager, generated step, or emitted-source metadata. A reusable `Utility(ABC)` requires `apply(command, state)`; concrete utilities receive only their needed external capability at construction or as one explicit argument. For example `MarsUtility(reader).apply(...)` routes prepared SQL and site to DataSyncX; `SmartAppendUtility(...).apply(...)` performs its local algorithm; `EmailUtility(send_msg).apply(...)` sends mail. The small explicit registry maps parsed names/aliases to concrete instances **at parse/bind time only**. No `_apply`, `render`, `emit_block`, reflection registration, or second string dispatch inside a known class.

**State and effects.** `RuntimeState` contains only per-run variable/macro frames, global values, demonstrated run settings, and working directory. The interpreter creates a fresh state per run. A single path resolver at the I/O edge maps allowed historic Windows/UNC paths to mounted Linux paths; it must reject unmapped drive/UNC paths explicitly. It is a path policy, not a bag of filesystem, mail, Oracle, and CSV services. Pass DataSyncX readers/mail, local SQLite connection or file access, and optional clock as explicit dependencies only to utilities needing them. Do not port `PipelineContext` or `SPFGlobals` wholesale. Preserve command source locations for diagnostics without preserving generated Python ranges.

**Minimal proposed package** (files are responsibilities, not a mandate to pre-create empty modules):

~~~text
src/vg2c_new/
  __init__.py           public run_file/run_text
  __main__.py           small CLI for .txt execution
  model.py              executable Command/task node, source span, run error
  parser.py             splitting, options, task selection/binding, nesting, arguments
  runtime.py            ordered interpreter, RuntimeState, controller evaluation
  paths.py              Windows/UNC-to-mounted-path policy
  utilities/
    base.py             Utility ABC and explicit registry
    queries.py          Mars/Aries/other approved DataSyncX utility classes
    sqlite.py           local SQLite utility
    files.py            one class per atomic file/CSV command
    smart_append.py     SmartAppendUtility
    reports.py          report utility only if required
    email.py            EmailUtility via DataSyncX
  legacy/               only extracted portable algorithms actually needed
    tokens.py           macro/global and SQL token rules, if extraction wins
    smart_append.py     local SmartAppend core, if extraction wins
~~~

Add a `legacy/` file only after a specific routine passes a dependency and parity check. Do not copy the entire `SPFLib` tree or create subpackages merely to mirror it. The distribution can retain the existing project name initially while exposing `vg2c_new`; choose CLI packaging at cutover. A separate UI is already available for viewing `.txt`, so no new frontend is planned.

## 2. Parser/resolver decision, from dependency tracing

| Concern | ScriptHost source and coupling | Choice for `vg2c_new` |
| --- | --- | --- |
| Split `.txt` | `SPFManager.Run_SPFSQL` splits `MySPFSQLFileData` on `SQLFILE_DELIM` (`SPFSQL3.py:500-550`); current `frontend/parser.py:36-113` handles line boundaries, BOM and spans. | Adapt the exact delimiter rule, retain current span/BOM handling where correct. Extract only pure splitting, not `SPFManager`. Differential-test whitespace/case/empty blocks. |
| Detect task/query | `GetQuery` (`SPFSQL3.py:790-1073`) has a large command map plus `/UTILITIES`, `/REPORT`, `/NODE`, `/UN`, `/OLEDB`, `/ENGINE`, `/TABLE` precedence and normal-query backend routing; then `getattr(sys.modules[__name__], ...)` constructs a task. | Extract the applicable decision table and alias/precedence rules into parser code. Construct a `Command` task node with its utility bound; never construct a legacy task. Reject Windows-only or unsupported backends explicitly; retain ScriptHost rules as coverage oracle. |
| Options | `SPFTaskBase.parseTaskOptions` (`SPFSQL3.py:1518-1824`) validates `<OPTIONS>` and reads options, then mutates many task/global fields and instantiates compiled `NodesInfo` for `/NODE`. Current `frontend/parser.py` parses options with less coupling. | Adapt the portable grammar and validation, not the mutating method. Preserve order/duplicates until the chosen precedence is known. Node normalization belongs at the DataSyncX query boundary and must be verified against real values. |
| Utility arguments | `SPFTaskBase.MyUtilities` (`SPFSQL3.py:1326-1368`) uses stdlib `csv.reader(delimiter=" ", skipinitialspace=True, quotechar='"')` with a malformed-quote fallback. Current `_emit_helpers.py` uses `shlex(posix=False)`. | Use and characterize ScriptHost's `csv.reader` rule for quoted paths/empty arguments. Keep one parser rule; copy fallback only if scripts/tests require it. Command-specific arity/defaults come from the relevant legacy task, e.g. SmartAppend at `SPFSQL3.py:14767`. |
| Nested controllers | `Process_Query`/`handleControlerTask` (`SPFSQL3.py:658-788`) recurse over mutable lists of executable `SPFTaskBase` objects using `isControlerStartTask`, `isControlerEndTask`, `nestLevel`, `childTasksList`. Current `resolver/scope_builder.py:59-150` and `operands/` build then emit separate scope objects. | Keep the **task-construction pattern**: parser creates bound `Command` task nodes and attaches children immediately. Adapt ScriptHost matching/nesting logic into this one tree; inspect mismatched/end markers and ELSE association. Do not retain legacy mutable task classes or a parallel `ScopeNode`. |
| Macro, IF, loop resolution | `StartMacroTask`, `RunLoopTask`, `ForLoopTask`, `SiteLoopTask`, `IfThenTask` (`SPFSQL3.py:12419-13319`) mix parsing, file/variable state, child execution and global access. `Utilities.Substitute_Macro` (`utils.py:1882`) is nested-table aware. | Extract narrow token matching, condition operators and argument defaults where portable; implement run-scoped frames and controller execution in interpreter. Keep substitutions at execution time. Legacy task classes cannot be the target command model. |
| `.spf` job list | `parse_RunMultipleQueriesInput` (`SPFSQL3.py:1079`) rewrites jobs into `.spfsql` text, accesses instance globals, logging, `parse_cmdParams`, file encoding. | Separate later input adapter only if `.spf` is in scope; do not make it a second parser or preempt `.txt` work. |

**Comparison and verdict.** ScriptHost's **task tree construction is reusable as a design** and fits the single-model target: bind a concrete utility while parsing and attach children as ScriptHost does. Directly reusing its executable task classes is much more complicated: `SPFManager` and `SPFTaskBase` inherit `Utilities -> SPFGlobals -> ScriptHost` (`SPFSQL3.py:275,1200`; `utils.py:306`; `SPFGlobals.py:62`). `SPFSQL3.py:273` imports compiled `dbDrivers`/`NodesInfo`; `SPFLib/__init__.py:45-95` invokes Windows filesystem encoding, pywin32 and pythonnet imports. `SPFTaskBase` has many mutable class/default fields, parses options in `execute()` (`SPFSQL3.py:1200-1324,1496`), and dispatches execution to subclass methods; its execution also deep-copies children (`SPFSQL3.py:1957`). Removing a few top-level imports would not remove these runtime dependencies. First try extracting the small pure task-selection and tree-construction routines; if a short portability patch makes **those isolated routines** directly reusable with bound new task nodes, take that option. If they still require the hierarchy, adapt their logic into `vg2c_new/parser.py`; do not maintain a patched full ScriptHost fork. Retaining the current parser verbatim would keep useful spans but its classifier imports `EmitterUtility` (`frontend/classifier.py:11,49`), and its scope builder depends on operand classes that emit Python. Reuse its proven lexical code selectively, not its pipeline. Choose each rule by differential fixtures, not by which implementation is already integrated.

## 3. Utility ownership and ScriptHost reuse

Every supported atomic command has exactly one concrete `Utility.apply`. Helpers may be shared for CSV, tokens, and paths, but a helper is not a second utility implementation. The runtime invokes a class once; that method can call a directly portable ScriptHost routine, an extracted routine in `legacy/`, or a local algorithm. Prefer direct import only if the module imports on Linux without Windows/runtime transitive dependencies and the function accepts explicit inputs; otherwise extract the narrow algorithm and test it against the reference. Do not write both an adapted ScriptHost and a parallel vg2c algorithm for the same command.

Likely extraction candidates: ScriptHost utility argument parsing, global/macro token matching (`utils.py:1563,1882`), `NormalQueryTaskBase` SQL token behavior (`SPFSQL3.py:2308,3007,4509`), controller operator/default rules, and SmartAppend update/delete/sort behavior (`SmartAppendTask` from `SPFSQL3.py:14396`). Each needs a dependency audit; SmartAppend is particularly broad and references globals, file utilities, and MemTable, whose constructor creates `Utilities()` and shared SQLite state (`memtable.py:52,77`). Prefer current portable `CsvIO`, `SqliteReader`, `FileSystemOps`, and existing SmartAppend pieces when they match observed behavior, with narrow ScriptHost semantics added where they do not. Legacy `SPFCopy` and `SPFDelete` have Python branches but also Windows utility behavior; adapt only needed local behavior. `SPFEmail` and query driver execution are **not** reuse candidates because DataSyncX owns those effects. HTML reporting may need a larger extraction and is gated by actual scripts. Reuse potential is therefore **high for rules/algorithms, low for whole utility classes**, subject to parity and distribution rights.

### DataSyncX boundary

- `MarsUtility` and `AriesUtility` use DataSyncX `MarsReader`/`AriesReader` with explicit site and SQL. Other approved internal query systems use an applicable DataSyncX reader (for example its exposed `OracleReader`) only after reader/node capability is verified. Legacy `nqOracleTask`/`nqUberTask` supply query interpretation and SQL transformations, never connections. No direct `cx_Oracle`, ODBC, .NET, `dbDrivers`, or hidden fallback.
- `EmailUtility` translates validated command arguments into DataSyncX `send_msg`. Use `email_on_exception` only for an opted-in run-level alert, not as a replacement for command email. The installed `send_msg` has a narrower shape than legacy email (notably BCC/multiple attachments and possible interactive defaults); fail clearly for unsupported options until an approved DataSyncX API covers them. Do not retain `utilities/mail.py` SMTP/keyring transport.
- Local SQLite and CSV are local file operations, not internal database readers. Preserve header-only zero-row SELECT output. DataSyncX reader/mail objects are injected into relevant concrete utilities; there is no all-purpose service container.
- Linux image work must replace the current UI-led `Dockerfile` entry point and resolve the existing missing `uv.lock` before a locked build. Validate DataSyncX's client/auth configuration in the actual image; a local import or mocked call is not live connectivity evidence.

## 4. Command support matrix (proposed scope, before implementation)

Legend: **P0** = target first parity gate; **P1** = implement if present in representative scripts; **Hold** = explicit unsupported decision until requirement/capability is shown. Current = current `vg2c` translator/runtime; ScriptHost = extracted reference. “Yes” under Windows means a legacy implementation/runtime path depends on Windows, not that the new command will.

| Command | Current | ScriptHost | New target | Reuse | Windows | DataSyncX | Support | Tests |
| --- | --- | --- | --- | --- | --- | --- | --- | --- |
| Mars SQL | SQL_QUERY + Mars dialect/DataSyncX | normal query routing/driver | MarsUtility | node/SQL rules | Yes, driver | Yes | P0 | site, SQL, CSV headers/rows, errors |
| Aries SQL | SQL_QUERY + Aries dialect/DataSyncX | normal query routing/driver | AriesUtility | node/SQL rules | Yes, driver | Yes | P0 | site, SQL, ordering, empty rows |
| Oasys/other internal Oracle | Oasys dialect/reader | Oracle/other task families | explicit supported DataSyncX utility | SQL transforms only | Yes, driver | Yes | P1, reader gate | node mapping, unsupported nodes, mocked/live read |
| SQLite SQL | SQLITE_QUERY/SqliteReader | nqSQLiteTask/MemTable | SqliteUtility | current portable core + select legacy rules | Legacy graph | No | P0 | multi-statement, table binding, empty headers |
| CSV output/input and SQL_Get_CSV_List | CsvIO/helper | file output/Get_CSV_List | CsvUtility or query helper at point of use | current CSV, legacy token rules | No for core | No | P0 | encoding, quoting, header/order, list expansion |
| WRITE-FILE | WriteFile/FileSystemOps | WriteFileTask | WriteFileUtility | current core, legacy no-strip rule | No for core | No | P0 | exact bytes, blank body, paths |
| FS copy/rename | FileSystemOps | SPFCopyTask/SPFRenameTask | CopyUtility/RenameUtility | local Python behavior | Legacy variants | No | P0 | files, directories, missing paths |
| FS delete | FileSystemOps | SPFDeleteTask | DeleteUtility | local Python behavior | Legacy variants | No | P0 | file/dir, missing/read-only policy |
| Wait file | WaitFile | WaitFileTask | WaitFileUtility | current/legacy timing rules | No for core | No | P1 | success, timeout, cancellation |
| Rows/value in file | RowsInFile | RowsInFileTask/ValueInFileTask | RowsInFileUtility; value if needed | legacy argument/column rules | No for core | No | P0 rows; P1 value | header, row count, column case |
| SmartAppend | SmartAppend append | SmartAppendTask v1/v3+ | SmartAppendUtility | extract update/delete/sort algorithm selectively | Legacy graph | No | P0 for used version | append, update/delete, schema, sort, missing file |
| CrossTab/pivot | CrosstabUtility | SubStitute_CT/PivotTable | query transform or CrossTabUtility if standalone | tested token/aggregation rules | No for core | No | P0 SQL token; P1 report pivot | aliases, values, duplicates, order |
| Macro scope/substitution | operand/MacroState | Start/EndMacro + Substitute_Macro | interpreter frame/control | token and nesting rules | Legacy graph | No | P0 | nested frames, missing token, files |
| IF/ELSE/END-IF | operand emitter | IfThen/Else/EndIf tasks | interpreter control | operator/default rules | Legacy graph | No | P0 | nesting, AND/OR, strings/numbers |
| FOR/SITE/RUN loop | operand loop subset | loop tasks | interpreter control | iteration/exit rules | Legacy graph | Sometimes for site data | P0 FOR/RUN; P1 SITE | nested iteration, stop/error, state reset |
| Email | custom SMTP/keyring | EmailTask/SPFEmail | EmailUtility | argument grammar only | Yes, transport | Yes, send_msg | P1 | mocked recipients/body/attachment/failures |
| HTML report | HtmlReport | HTML* and CSVToHTML tasks | ReportUtility only if needed | current or narrow legacy algorithm | Legacy assets | No | P1 | rendered file content, layout/assets |
| PYTHON_EMBED/{PYSCRIPT} | emitted Python | PyScriptTask | isolated explicit Python-script policy | grammar only | Mixed | No | Hold | security/runtime scope decision |
| External `.bat/.exe/.va`, DOS/JSL/R | ExternalProcess | many task classes | reject unless portable command approved | none | Yes | No | Hold | clear error with source location |
| Legacy extra utilities (Excel, ZIP, XML, web, Hadoop, Mongo, SharePoint, etc.) | absent/generic | dedicated task classes | add one utility per proven need | audit each narrow routine | Mixed/often yes | Internal access must use DataSyncX | Hold | inventory real scripts first |
| `.spf` multiple jobs | absent | parse_RunMultipleQueriesInput | optional input adapter to same parser | adapt text rewrite | Legacy graph | No | Hold | job order, comments, paths |

“Hold” never means silently pass through or shell out. Before coding, inventory actual `.txt` inputs and map every encountered `/UTILITIES`, query backend, and `/REPORT` to a row or an explicit new row. This matrix covers current `Kind` values (`src/vg2c/kind.py`) and relevant ScriptHost controller/utility families; ScriptHost's entire historical command catalogue is not presumed in scope.

## 5. Migration backwards from the target

1. **Define the parity contract.** Inventory representative scripts, versions/options and expected side effects. Capture current translator-generated execution results where it reliably runs, plus safe ScriptHost results where runnable. Record unsupported forms and rights to redistribute extracted code. Make the matrix above concrete before broad implementation. No production code changes in this phase.
2. **Build the isolated spine.** First prototype whether ScriptHost's task-selection/tree-building functions can be extracted to construct `vg2c_new.Command` nodes with a bound utility using only a small portability patch. Keep this only if no `SPFTaskBase`/global/driver dependency remains and it is simpler than adapting the rules. Create `vg2c_new` with one bound task tree, parser, interpreter, `Utility` ABC/registry and small state. Differential-test splitting, options, task classification, quoted arguments, nesting, spans and unknown-command errors. Run local commands without importing `vg2c`, `SPFLib`, Windows modules, or generated Python.
3. **Complete local semantic families.** Add macro/global substitution, IF and loops, CSV/SQLite, file commands, used SmartAppend version, and needed SQL token/CrossTab transforms. For each, choose one existing portable implementation or one extracted/adapted ScriptHost routine after a dependency audit; delete duplicate logic within the new package. Compare actual files, rows, headers, ordering and failures against the old executable oracle.
4. **Add DataSyncX effects.** Wire Mars/Aries and approved other readers, then mail, through explicit utilities. Verify exact SQL/site sent with mocks; independently run controlled read-only live database checks and mail configuration checks. Never deliver real email for a regression test. Unsupported mail/query capability is a deliberate error.
5. **Linux cutover.** Build a Docker image with no UI build/entry or Windows imports; settle DataSyncX Oracle/Kerberos/client requirements and mounted path mapping. Run local and controlled integration scripts inside it. Replace the entry point with `vg2c_new` only after the support matrix's required rows pass. Keep old translator isolated as a test oracle until cutover evidence is recorded.
6. **Delete old system by consumer reachability.** Once nothing required imports it, remove `vg2c_ui/`, compiler/emitter/embedding, generated-Python editing and metadata, `editing.py`, UI-only workflow/dataflow projection, `@emittable` infrastructure, `PipelineContext`, custom mail/keyring and direct database clients, old CLI and their tests. Retain only input SQL analysis or file-flow logic if a current non-UI consumer is demonstrated; otherwise delete it. Remove obsolete dependencies, scripts, Docker stages and documentation in the same cutover. Do not build a compatibility facade for generated `.py` APIs.

The current `compile_document` runs parse/classify/resolve/analyze/dispatch/emit (`src/vg2c/compilation.py:66-91`); `emitter/` and `embedding/` alone are about 2,707 lines in this checkout, with additional UI, editor, dispatch models and emission halves in utilities. Thus a **large majority of the current architecture is deletion candidate**, but exact net lines and safe removal depend on the consumer/import audit. Starting separately avoids spending work untangling those components merely to delete them.

## 6. Behavior gates and unresolved decisions

Compare the **same `.txt` input** under old generated execution and direct runtime where old execution is reliable. Normalize only nondeterministic timestamps/temp names. Compare file bytes or parsed CSV with explicit encoding, header/schema on zero rows, row order, SQL and site passed to DataSyncX, macro/global values, IF decisions, loop iteration counts, SmartAppend mutations, and error type/location/continue behavior. Use safe ScriptHost execution only if its environment permits it; static source is a weaker oracle. Run repeated jobs in one process to detect leaked state. Keep unit/mocked, local end-to-end, Linux container, and live read-only checks separate in reports. No emitted-source parity target.

Before implementation, user input would materially settle: (1) representative production-like `.txt` scripts and the required first-support rows/SmartAppend versions; (2) whether `.spf`, Python embed and legacy extra utilities are required at all; (3) redistribution permission for extracted decompiled routines; (4) Linux DataSyncX authentication and safe read-only endpoint, mail feature needs, and UNC mount mappings. The already stated UI retirement and no generated-Python compatibility decision need no reconfirmation.

**Final minimality check:** one bound executable command/task tree, one interpreter, one utility class and behavior per atomic command, one registry lookup per atomic node, small state, explicit capability injection, one path policy. No `PipelineContext`, second model hierarchy, forwarding manager, emitter, generic external fallback, Windows import, or unneeded `legacy/` module in `vg2c_new`.
