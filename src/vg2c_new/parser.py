from __future__ import annotations

import csv
import re
from dataclasses import replace
from io import StringIO
from pathlib import Path

from vg2c_new.model import Command, CommandKind, SourceSpan

SCRIPTHOST_REFERENCE = "8ddd5e6463b43834d769057be48041ec657f0f9d"
SQLFILE_DELIM = "<---- New Query ---->"
_SEPARATOR_RE = re.compile(r"^[ \t]*<----[ \t]*New Query[ \t]*---->[ \t]*$", re.MULTILINE)
_OPTION_RE = re.compile(r"^/([^=\s]+)=(.*)$")


class Vg2ParseError(ValueError):
    pass


# alias, ScriptHost task, semantic runtime target, port mode, dependency, parity focus
# Seeded directly from SPFManager.GetQuery. Session 1 implements only the control
# targets plus WRITE-FILE and SPFDelete; the remaining rows are the Session-2 queue.
_RESOLVER_MANIFEST_BASE: tuple[tuple[str, str, str, str, str, str], ...] = (
    (
        r"@EXEDIR@\AppendFile.va",
        "AppendFileTask",
        "append_file",
        "DIRECT_PORT",
        "filesystem",
        "append semantics",
    ),
    (
        r"@EXEDIR@\SPFCopy.bat",
        "SPFCopyTask",
        "copy_file",
        "AMENDED_PORT",
        "filesystem",
        "copy/continue",
    ),
    (
        r"@EXEDIR@\CSVToHTML.va",
        "CSVToHTMLTask",
        "csv_to_html",
        "DIRECT_PORT",
        "csv/html",
        "format parity",
    ),
    (
        r"@EXEDIR@\CSVToXML.va",
        "CSVToXMLTask",
        "csv_to_xml",
        "DIRECT_PORT",
        "csv/xml",
        "format parity",
    ),
    (
        r"@EXEDIR@\SPFDelete.bat",
        "SPFDeleteTask",
        "delete_file",
        "AMENDED_PORT",
        "filesystem",
        "list/wildcard delete",
    ),
    (
        r"@EXEDIR@\SQLPathFinder_Email.va",
        "EmailTask",
        "email",
        "REWRITE",
        "DataSyncX",
        "mail contract",
    ),
    (
        r"@EXEDIR@\ImportExcel.va",
        "ImportExcelTask",
        "import_excel",
        "AMENDED_PORT",
        "excel",
        "sheet import",
    ),
    (
        r"@EXEDIR@\LoadExcel.va",
        "LoadExcelTask",
        "load_excel",
        "AMENDED_PORT",
        "excel",
        "sheet load",
    ),
    (
        r"@EXEDIR@\XLSToCSV.va",
        "XLSToCSVTask",
        "xls_to_csv",
        "AMENDED_PORT",
        "excel/csv",
        "conversion",
    ),
    (
        r"@EXEDIR@\SetFileRO.va",
        "SetFileROTask",
        "set_file_read_only",
        "AMENDED_PORT",
        "filesystem",
        "permissions",
    ),
    (
        r"@EXEDIR@\Get_Aqua.va",
        "GetAquaTask",
        "get_aqua",
        "REWRITE",
        "network",
        "current integration",
    ),
    (r"@EXEDIR@\Get_TCA.va", "GetTCATask", "get_tca", "REWRITE", "network", "current integration"),
    (
        r"U->Hadoop-Delete:Y@EXEDIR@\Hadoop-Delete.va",
        "HadoopDeleteTask",
        "hadoop_delete",
        "REWRITE",
        "external",
        "current backend",
    ),
    (
        r"U->Hadoop-Load:Y@EXEDIR@\Hadoop-Load.va",
        "HadoopLoadTask",
        "hadoop_load",
        "REWRITE",
        "external",
        "current backend",
    ),
    (
        r"U->Prompt-Input:Y@EXEDIR@\PROMPT-INPUT.va",
        "PromptInputTask",
        "prompt_input",
        "REWRITE",
        "interactive",
        "input behavior",
    ),
    (
        r"@EXEDIR@\LoadJMP.va",
        "LoadJMPTask",
        "load_jmp",
        "REWRITE",
        "JMP/Windows",
        "product decision",
    ),
    (
        r"@EXEDIR@\Run_Python_Script.va",
        "RunPythonScriptTask",
        "run_python",
        "AMENDED_PORT",
        "python",
        "process contract",
    ),
    (r"@EXEDIR@\Run_R_Script.va", "RunRFileTask", "run_r", "AMENDED_PORT", "R", "process contract"),
    (r"@EXEDIR@\RoboCopy.va", "RoboCopyTask", "robocopy", "REWRITE", "filesystem", "portable copy"),
    (
        r"@EXEDIR@\PublishToSP.va",
        "PublishToSPTask",
        "publish_sharepoint",
        "REWRITE",
        "SharePoint",
        "integration",
    ),
    (
        r"@EXEDIR@\SmartAppend.va",
        "SmartAppendTask",
        "smart_append",
        "AMENDED_PORT",
        "csv",
        "append parity",
    ),
    (
        r"@EXEDIR@\SPFRename.va",
        "SPFRenameTask",
        "rename_file",
        "AMENDED_PORT",
        "filesystem",
        "rename parity",
    ),
    (
        r"@EXEDIR@\SQLiteDelete.va",
        "SQLiteDeleteTask",
        "sqlite_delete",
        "DIRECT_PORT",
        "sqlite",
        "delete parity",
    ),
    (
        r"@EXEDIR@\SQLite-Load.va",
        "SQLiteLoadTask",
        "sqlite_load",
        "DIRECT_PORT",
        "sqlite",
        "load parity",
    ),
    (r"@EXEDIR@\TDXTOCSV.va", "TDXToCSVTask", "tdx_to_csv", "REWRITE", "tdx", "conversion"),
    (r"@EXEDIR@\SPFUNZIP.va", "SPFUNZipTask", "unzip", "DIRECT_PORT", "zipfile", "archive parity"),
    (
        r"@EXEDIR@\SPFDISTRIBUTE.VA",
        "SPFDistributeTask",
        "distribute",
        "AMENDED_PORT",
        "filesystem",
        "distribution",
    ),
    (
        r"@EXEDIR@\WaitInterval.va",
        "WaitIntervalTask",
        "wait_interval",
        "DIRECT_PORT",
        "time",
        "wait parity",
    ),
    (
        r"@EXEDIR@\WaitFile.va",
        "WaitFileTask",
        "wait_file",
        "AMENDED_PORT",
        "filesystem",
        "poll parity",
    ),
    (
        r"@EXEDIR@\Get_Web_Text.exe",
        "GetWebTextTask",
        "get_web_text",
        "REWRITE",
        "http",
        "http parity",
    ),
    (r"@Echo", "EchoTask", "echo", "DIRECT_PORT", "none", "text parity"),
    (r"@EXEDIR@\XMLToCSV.va", "XMLToCSVTask", "xml_to_csv", "DIRECT_PORT", "xml/csv", "conversion"),
    (r"@EXEDIR@\SPFZIP.va", "SPFZipTask", "zip", "DIRECT_REUSE", "ScriptHost portable", "archive parity"),
    (
        r"@EXEDIR@\GetHelpCSV.va",
        "GetHelpCSVTask",
        "get_help_csv",
        "AMENDED_PORT",
        "csv",
        "help output",
    ),
    (
        r"MONGODB-EXPORT.VA",
        "MongoExportTask",
        "mongo_export",
        "REWRITE",
        "DataSyncX",
        "mongo parity",
    ),
    (
        r"U->MongoDB-Import:Y@EXEDIR@\MongoDB-Import.va",
        "MongoImportTask",
        "mongo_import",
        "REWRITE",
        "DataSyncX",
        "mongo parity",
    ),
    (
        r"U->MongoDB-Extract:Y@EXEDIR@\MongoDB-Extract.va",
        "MongoExtractTask",
        "mongo_extract",
        "REWRITE",
        "DataSyncX",
        "mongo parity",
    ),
    (
        r"@EXEDIR@\VA_Join_By_Name.va",
        "SPFJoinTask",
        "join_by_name",
        "DIRECT_PORT",
        "csv",
        "join parity",
    ),
    ("{AGE-OF-FILE}", "AgeOfFileTask", "age_of_file", "DIRECT_PORT", "filesystem", "value parity"),
    ("{VERIFY-ROLE}", "VerifyRoleTask", "verify_role", "REWRITE", "identity", "product decision"),
    (
        "{DATE-OF-FILE}",
        "DateOfFileTask",
        "date_of_file",
        "DIRECT_PORT",
        "filesystem",
        "value parity",
    ),
    ("{GET-SITE-TIME}", "GetSiteTimeTask", "get_site_time", "AMENDED_PORT", "time", "site time"),
    (
        "{GET-SITE-TIME-FILE}",
        "GetSiteTimeTask",
        "get_site_time_file",
        "AMENDED_PORT",
        "time/filesystem",
        "site time file",
    ),
    ("{ROWS-IN-FILE}", "RowsInFileTask", "rows_in_file", "DIRECT_PORT", "filesystem", "row count"),
    ("{UPDATE-TIME}", "UpdateTimeTask", "update_time", "DIRECT_PORT", "time", "token parity"),
    (
        "{UPDATE-TIME-FILE}",
        "UpdateTimeFileTask",
        "update_time_file",
        "DIRECT_PORT",
        "time/filesystem",
        "file parity",
    ),
    (
        "{VALUE-IN-FILE}",
        "ValueInFileTask",
        "value_in_file",
        "DIRECT_PORT",
        "filesystem",
        "value parity",
    ),
    ("{GET-FILES}", "GetFilesTask", "get_files", "AMENDED_PORT", "filesystem", "glob parity"),
    (
        "{SHAREPOINT-DELETE}",
        "DeleteSPTask",
        "sharepoint_delete",
        "REWRITE",
        "SharePoint",
        "integration",
    ),
    (
        "{FILE-COMPARE}",
        "FileCompareTask",
        "file_compare",
        "DIRECT_PORT",
        "filesystem",
        "comparison",
    ),
    ("{PYSCRIPT}", "PyScriptTask", "pyscript", "AMENDED_PORT", "python", "embedded runtime"),
    ("{ENCRYPT-TEXT}", "EncryptTextTask", "encrypt_text", "REWRITE", "crypto", "security decision"),
    (
        "{ENCRYPT-SPFSQL-FILE}",
        "EncryptSPFSQLFileTask",
        "encrypt_spfsql",
        "REWRITE",
        "crypto",
        "security decision",
    ),
    (
        "{ENCRYPT-CONFIG-FILE}",
        "EncryptConfigFileTask",
        "encrypt_config",
        "REWRITE",
        "crypto",
        "security decision",
    ),
)

# Non-atomic GetQuery entries are kept in the same manifest so Session 2 can
# account for the complete resolver surface without introducing another registry.
_RESOLVER_MANIFEST_BASE += (
    ("{IF-THEN}", "IfThenTask", "if", "DIRECT_PORT", "runtime", "operators/else"),
    ("{ELSE}", "ElseTask", "else", "DIRECT_PORT", "runtime", "nesting"),
    ("{END-IF}", "EndIfTask", "end_if", "DROP_LEGACY", "runtime", "nesting"),
    (
        "{START-MACRO}",
        "StartMacroTask",
        "macro",
        "AMENDED_PORT",
        "csv/runtime",
        "first-row/nesting",
    ),
    ("{END-MACRO}", "EndMacroTask", "end_macro", "DROP_LEGACY", "runtime", "nesting"),
    ("{FOR-LOOP}", "ForLoopTask", "for_loop", "DIRECT_PORT", "runtime", "version-2 tokens"),
    ("{SITE-LOOP}", "SiteLoopTask", "site_loop", "DIRECT_PORT", "runtime", "site tokens/errors"),
    ("{RUN-LOOP}", "RunLoopTask", "run_loop", "AMENDED_PORT", "csv/runtime", "chunk/errors"),
    ("{END-LOOP}", "EndLoopTask", "end_loop", "DROP_LEGACY", "runtime", "nesting"),
    (
        "{BEGIN-HPC}",
        "BeginHPCTask",
        "hpc_scope",
        "FLATTEN_TRANSPORT",
        "runtime",
        "local child execution",
    ),
    ("{END-HPC}", "EndHPCTask", "end_hpc", "DROP_LEGACY", "runtime", "nesting"),
    ("JSL", "Run_JSLTask", "jsl", "REWRITE", "JMP/Windows", "product decision"),
    ("RSCRIPT", "RunRScriptTask", "rscript", "AMENDED_PORT", "R", "process contract"),
    ("CB_ACS", "CB_ACSTask", "cb_acs", "REWRITE", "external", "current integration"),
    ("WRITE-FILE", "WriteFileTask", "write_file", "DIRECT_PORT", "filesystem", "EOF/blank body"),
    ("PROMPT-JOBID", "PromptJobIDTask", "prompt_jobid", "DROP_LEGACY", "none", "metadata no-op"),
    ("HTML-RUN", "HTMLRunTask", "report.html_run", "AMENDED_PORT", "html", "report parity"),
    ("HTML-DEFER", "HTMLDeferTask", "report.html_defer", "AMENDED_PORT", "html", "report parity"),
    (
        "HTML-LAYOUT",
        "HTMLLayoutTask",
        "report.html_layout",
        "AMENDED_PORT",
        "html",
        "report parity",
    ),
    (
        "HTML-TAB-LAYOUT",
        "HTMLTaborMenuLayoutTask",
        "report.html_tab_layout",
        "AMENDED_PORT",
        "html",
        "report parity",
    ),
    (
        "HTML-MENU-LAYOUT",
        "HTMLTaborMenuLayoutTask",
        "report.html_menu_layout",
        "AMENDED_PORT",
        "html",
        "report parity",
    ),
    ("HTML-GNUPLOT", "HTMLGNUPlotTask", "report.gnuplot", "REWRITE", "plotting", "report parity"),
    (
        "HTML-GNUPLOT-SHOW",
        "HTMLGNUPlotTask",
        "report.gnuplot_show",
        "REWRITE",
        "plotting",
        "report parity",
    ),
    ("HTML-RPLOT", "HTMLRPlotTask", "report.rplot", "REWRITE", "R", "report parity"),
    ("HTML-RPLOT-SHOW", "HTMLRPlotTask", "report.rplot_show", "REWRITE", "R", "report parity"),
    (
        "HTML-DELETE",
        "HTMLDeleteTask",
        "report.delete",
        "AMENDED_PORT",
        "filesystem/html",
        "report parity",
    ),
    ("HTML-JS-SHOW", "HTMLJSTask", "report.js_show", "AMENDED_PORT", "html/js", "report parity"),
    ("HTML-JS-DEFER", "HTMLJSTask", "report.js_defer", "AMENDED_PORT", "html/js", "report parity"),
    ("HTML-PYPLOT", "HTMLPyPlotTask", "report.pyplot", "AMENDED_PORT", "plotting", "report parity"),
    (
        "HTML-PYPLOT-SHOW",
        "HTMLPyPlotTask",
        "report.pyplot_show",
        "AMENDED_PORT",
        "plotting",
        "report parity",
    ),
    (
        "NQ_CBSQL_ORACLE_Task",
        "nqCBSQLTask",
        "query.cbsql_oracle",
        "REWRITE",
        "DataSyncX",
        "routing/query parity",
    ),
    ("NQ_UBER_Task", "nqUberTask", "query.uber", "REWRITE", "DataSyncX", "routing/query parity"),
    (
        "NQ_SQLSERVER_TASK",
        "nqSQLServerTask",
        "query.sqlserver",
        "REWRITE",
        "DataSyncX",
        "routing/query parity",
    ),
    (
        "NQ_HADOOP_TASK",
        "nqHadoopTask",
        "query.hadoop",
        "REWRITE",
        "DataSyncX",
        "routing/query parity",
    ),
    (
        "NQ_SQLITE_TASK",
        "nqSQLiteTask",
        "query.sqlite",
        "AMENDED_PORT",
        "sqlite",
        "routing/query parity",
    ),
    ("NQ_CB_TASK", "nqCBTask", "query.cb", "REWRITE", "DataSyncX", "routing/query parity"),
    (
        "NQ_ORACLE_TASK",
        "nqOracleTask",
        "query.oracle",
        "REWRITE",
        "DataSyncX",
        "routing/query parity",
    ),
    (
        "NQ_TERADATA_TASK",
        "nqTeradataTask",
        "query.teradata",
        "REWRITE",
        "DataSyncX",
        "routing/query parity",
    ),
    (
        "NQ_TEXT_TASK",
        "nqTextTask",
        "query.text",
        "AMENDED_PORT",
        "filesystem",
        "routing/query parity",
    ),
    ("NQ_MONGO_TASK", "nqMongoTask", "query.mongo", "REWRITE", "DataSyncX", "routing/query parity"),
    (
        "NQ_HADOOP_IMPALA_TASK",
        "nqHadoopImpalaTask",
        "query.impala",
        "REWRITE",
        "DataSyncX",
        "routing/query parity",
    ),
    (
        "NQ_MSOLAP_TASK",
        "nqMSOLAPTask",
        "query.msolap",
        "REWRITE",
        "DataSyncX",
        "routing/query parity",
    ),
    ("NQ_MYSQL_TASK", "nqMySQLTask", "query.mysql", "REWRITE", "DataSyncX", "routing/query parity"),
    (
        "NQ_SAPHANA_TASK",
        "nqSAPHanaTask",
        "query.saphana",
        "REWRITE",
        "DataSyncX",
        "routing/query parity",
    ),
    (
        "NQ_DENODO_TASK",
        "nqDenodoTask",
        "query.denodo",
        "REWRITE",
        "DataSyncX",
        "routing/query parity",
    ),
    (
        "NQ_PYSCRIPTDRIVER_TASK",
        "nqPyScriptDriverTask",
        "query.pyscript",
        "AMENDED_PORT",
        "python",
        "routing/query parity",
    ),
    (
        "NQ_PYSCRIPTDRIVERIMPORT_TASK",
        "nqPyScriptDriverImportTask",
        "query.pyscript_import",
        "AMENDED_PORT",
        "python",
        "routing/query parity",
    ),
    (
        "NQ_DUCKDB_TASK",
        "nqDuckDBTask",
        "query.duckdb",
        "AMENDED_PORT",
        "duckdb",
        "routing/query parity",
    ),
    (
        "NQ_SNOWFLAKE_TASK",
        "nqSnowFlakeTask",
        "query.snowflake",
        "REWRITE",
        "DataSyncX",
        "routing/query parity",
    ),
    (
        "NQ_PostgreSQL_TASK",
        "nqPostgreSQLTask",
        "query.postgresql",
        "REWRITE",
        "DataSyncX",
        "routing/query parity",
    ),
    ("DOSCmdTask", "DOSCmdTask", "shell", "DROP_LEGACY", "shell", "must remain unsupported"),
    ("StackDataTask", "StackDataTask", "stack_data", "AMENDED_PORT", "csv", "stack parity"),
    ("va", "vaTask", "va", "DROP_LEGACY", "Windows", "historical wrapper"),
    ("DUMMY", "DummyPassThroughTask", "noop", "DROP_LEGACY", "none", "compatibility decision"),
)


_IMPLEMENTED_TARGETS = frozenset(
    {
        "append_file",
        "copy_file",
        "csv_to_html",
        "csv_to_xml",
        "delete_file",
        "email",
        "import_excel",
        "load_excel",
        "xls_to_csv",
        "set_file_read_only",
        "run_python",
        "run_r",
        "robocopy",
        "smart_append",
        "rename_file",
        "sqlite_delete",
        "sqlite_load",
        "unzip",
        "distribute",
        "wait_interval",
        "wait_file",
        "get_web_text",
        "echo",
        "xml_to_csv",
        "zip",
        "age_of_file",
        "date_of_file",
        "get_site_time",
        "rows_in_file",
        "update_time",
        "update_time_file",
        "value_in_file",
        "get_files",
        "file_compare",
        "pyscript",
        "if",
        "else",
        "end_if",
        "macro",
        "end_macro",
        "for_loop",
        "site_loop",
        "run_loop",
        "end_loop",
        "write_file",
        "rscript",
        "query.sqlite",
        "query.oracle",
        "stack_data",
    }
)
_FLATTENED_TARGETS = frozenset({"hpc_scope", "end_hpc"})
_RETIRED_TARGETS = frozenset(
    {
        "prompt_input",
        "load_jmp",
        "verify_role",
        "encrypt_text",
        "encrypt_spfsql",
        "encrypt_config",
        "jsl",
        "prompt_jobid",
        "shell",
        "va",
        "noop",
    }
)


def _manifest_status(target: str) -> str:
    if target in _IMPLEMENTED_TARGETS:
        return "implemented"
    if target in _FLATTENED_TARGETS:
        return "flattened obsolete transport"
    if target in _RETIRED_TARGETS:
        return "explicitly retired capability"
    return "documented current-platform gap"


RESOLVER_MANIFEST: tuple[tuple[str, str, str, str, str, str, str], ...] = tuple(
    (*row, _manifest_status(row[2])) for row in _RESOLVER_MANIFEST_BASE
)

_MANIFEST_BY_ALIAS = {row[0].upper(): row for row in RESOLVER_MANIFEST}

_CONTROL_TYPES: dict[str, tuple[CommandKind, str]] = {
    "{IF-THEN}": (CommandKind.IF, "if"),
    "{ELSE}": (CommandKind.MARKER, "else"),
    "{END-IF}": (CommandKind.MARKER, "end_if"),
    "{START-MACRO}": (CommandKind.MACRO, "macro"),
    "{END-MACRO}": (CommandKind.MARKER, "end_macro"),
    "{FOR-LOOP}": (CommandKind.FOR_LOOP, "for_loop"),
    "{SITE-LOOP}": (CommandKind.SITE_LOOP, "site_loop"),
    "{RUN-LOOP}": (CommandKind.RUN_LOOP, "run_loop"),
    "{END-LOOP}": (CommandKind.MARKER, "end_loop"),
    "{BEGIN-HPC}": (CommandKind.SCOPE, "hpc_scope"),
    "{END-HPC}": (CommandKind.MARKER, "end_hpc"),
}

_OPTION_TARGETS = {
    "CB_ACS": "cb_acs",
    "JSL": "jsl",
    "RSCRIPT": "rscript",
    "WRITE-FILE": "write_file",
    "PROMPT-JOBID": "prompt_jobid",
}

_REPORT_TARGETS = {
    "HTML-RUN": "report.html_run",
    "HTML-DEFER": "report.html_defer",
    "HTML-LAYOUT": "report.html_layout",
    "HTML-TAB-LAYOUT": "report.html_tab_layout",
    "HTML-MENU-LAYOUT": "report.html_menu_layout",
    "HTML-GNUPLOT": "report.gnuplot",
    "HTML-GNUPLOT-SHOW": "report.gnuplot_show",
    "HTML-RPLOT": "report.rplot",
    "HTML-RPLOT-SHOW": "report.rplot_show",
    "HTML-DELETE": "report.delete",
    "HTML-JS-SHOW": "report.js_show",
    "HTML-JS-DEFER": "report.js_defer",
    "HTML-PYPLOT": "report.pyplot",
    "HTML-PYPLOT-SHOW": "report.pyplot_show",
}


def parse(text: str | bytes, source: Path | None = None) -> tuple[Command, ...]:
    """Amended port of ScriptHost SPFManager parsing/resolution.

    Original: scripthost-utilities-decompiled/SPSQL3_py/SPFLib/SPFSQL3.py ::
    SPFManager.GetQuery / Process_Query / handleControlerTask and
    SPFTaskBase.parseTaskOptions / MyUtilities.
    Reference commit: 8ddd5e6463b43834d769057be48041ec657f0f9d.
    Port mode: AMENDED_PORT.
    Preserved: query delimiter, strict OPTIONS envelope, GetQuery precedence,
    quoted utility arguments, controller nesting, source ordering.
    Amendments: directly emits one immutable Command tree with source spans.
    Discarded: mutable task hierarchy, DOS fallback, generated-Python dispatch,
    Python-2 and historical transport behavior.
    """
    normalized = _normalize(text)
    flat = tuple(
        _parse_segment(index, segment, start_line, end_line, source)
        for index, (segment, start_line, end_line) in enumerate(_nonempty_segments(normalized))
    )
    commands, position, marker = _parse_sequence(flat, 0, frozenset())
    if marker is not None:
        command = flat[position]
        raise _error(command.span, f"Unexpected controller marker {marker!r}.")
    return tuple(commands)


def parse_file(path: Path) -> tuple[Command, ...]:
    path = Path(path)
    return parse(path.read_bytes(), source=path)


def parse_utility_arguments(value: str, span: SourceSpan | None = None) -> tuple[str, ...]:
    """Direct port of SPFTaskBase.MyUtilities tokenization.

    Original: SPFLib/SPFSQL3.py :: SPFTaskBase.MyUtilities.
    Reference commit: 8ddd5e6463b43834d769057be48041ec657f0f9d.
    Port mode: DIRECT_PORT.
    Preserved: csv.reader with space delimiter, skipinitialspace, double quotes.
    Amendments: immutable tuple and source-located parse failure.
    Discarded: cached mutable property and StringIO-position recovery branch.
    """
    try:
        reader = csv.reader(
            StringIO(value.strip()), delimiter=" ", skipinitialspace=True, quotechar='"'
        )
        return tuple(next(reader))
    except (csv.Error, StopIteration) as exc:
        location = span.location if span else "<input>"
        raise Vg2ParseError(f"{location}: Invalid /UTILITIES arguments: {exc}") from exc


def _normalize(text: str | bytes) -> str:
    value = text.decode("utf-8", errors="replace") if isinstance(text, bytes) else text
    if value.startswith("\ufeff"):
        value = value[1:]
    return value.replace("\r\n", "\n").replace("\r", "\n")


def _split_segments(text: str) -> list[tuple[str, int, int]]:
    segments: list[tuple[str, int, int]] = []
    cursor = 0
    line_no = 1
    for match in _SEPARATOR_RE.finditer(text):
        segment = text[cursor : match.start()]
        start = line_no
        end = start + segment.count("\n")
        segments.append((segment, start, end))
        consumed = text[match.start() : match.end()]
        line_no = end + consumed.count("\n")
        cursor = match.end()
        if cursor < len(text) and text[cursor] == "\n":
            cursor += 1
            line_no += 1
    segment = text[cursor:]
    start = line_no
    segments.append((segment, start, start + segment.count("\n")))
    return segments


def _nonempty_segments(text: str) -> list[tuple[str, int, int]]:
    return [segment for segment in _split_segments(text) if segment[0].strip()]


def _parse_segment(index: int, segment: str, start: int, end: int, source: Path | None) -> Command:
    span = SourceSpan(file=source, start_line=start, end_line=end)
    stripped = segment.lstrip()
    if not stripped.startswith("<OPTIONS>"):
        raise _error(span, "Missing <OPTIONS> token at start of query block.")
    close = stripped.find("</OPTIONS>")
    if close < 0:
        raise _error(span, "Missing </OPTIONS> terminator.")

    option_text = stripped[len("<OPTIONS>") : close]
    body = stripped[close + len("</OPTIONS>") :]
    if "<OPTIONS>" in body:
        raise _error(span, "Found <OPTIONS> in command body; query delimiter may be missing.")
    body = _trim_outer_blank_line(body)
    options = _parse_options(option_text, span)
    kind, command_type, utility_type, arguments = _resolve(options, body, span)
    return Command(
        index=index,
        kind=kind,
        command_type=command_type,
        utility_type=utility_type,
        options=options,
        body=body,
        raw=segment,
        arguments=arguments,
        span=span,
    )


def _parse_options(text: str, span: SourceSpan) -> tuple[tuple[str, str], ...]:
    pairs: list[tuple[str, str]] = []
    for line in text.split("\n"):
        line = line.strip()
        if not line:
            continue
        match = _OPTION_RE.match(line)
        if match is None:
            raise _error(span, f"Missing '=' in option line: {line}")
        pairs.append((match.group(1).upper(), match.group(2)))
    return tuple(pairs)


def _effective(options: tuple[tuple[str, str], ...]) -> dict[str, str]:
    result: dict[str, str] = {}
    for key, value in options:
        result[key] = value
    return result


def _resolve(
    options: tuple[tuple[str, str], ...], body: str, span: SourceSpan
) -> tuple[CommandKind, str, str | None, tuple[str, ...]]:
    effective = _effective(options)

    # GetQuery deliberately collapses to REPORT when REPORT coexists with other
    # routing options. A lone REPORT also resolves by its value.
    if "REPORT" in effective:
        report = effective["REPORT"].strip().upper()
        target = _REPORT_TARGETS.get(report)
        if target is None:
            raise _error(span, f"Unsupported /REPORT value {effective['REPORT']!r}.")
        return CommandKind.UTILITY, target, target, ()

    utility_value = effective.get("UTILITIES")
    if utility_value is not None:
        parsed = parse_utility_arguments(utility_value, span)
        if not parsed:
            raise _error(span, "/UTILITIES is empty.")
        alias = parsed[0]
        alias_key = alias.upper()
        if re.fullmatch(r"\{PYSCRIPT:.*\}", alias, flags=re.IGNORECASE):
            alias_key = "{PYSCRIPT}"

        control = _CONTROL_TYPES.get(alias_key)
        if control is not None:
            kind, command_type = control
            return kind, command_type, None, parsed[1:]

        manifest = _MANIFEST_BY_ALIAS.get(alias_key)
        if manifest is None:
            raise _error(
                span, f"Unsupported /UTILITIES command {alias!r}; shell fallback is disabled."
            )
        target = manifest[2]
        return CommandKind.UTILITY, target, target, parsed[1:]

    for option_name in ("CB_ACS", "JSL", "RSCRIPT", "WRITE-FILE", "PROMPT-JOBID"):
        if option_name in effective:
            target = _OPTION_TARGETS[option_name]
            return CommandKind.UTILITY, target, target, ()

    if all(name in effective for name in ("NODE", "UN", "OLEDB", "ENGINE")):
        target = _resolve_query(effective)
        return CommandKind.QUERY, target, target, ()

    if "TABLE" in effective and not body.strip():
        return CommandKind.UTILITY, "stack_data", "stack_data", ()

    raise _error(span, "Command type could not be determined from current GetQuery rules.")


def _resolve_query(options: dict[str, str]) -> str:
    site = options["NODE"]
    user = options["UN"]
    oledb = options["OLEDB"]
    engine = options["ENGINE"]
    if re.search(r"(^\$CB\$\@?)", user, re.IGNORECASE) or (
        re.search(r"\.yas", site, re.IGNORECASE) and not user.strip()
    ):
        return "query.cbsql_oracle"
    if (
        re.search(r"(xeus|\@uber\@|_yas_)", site, re.IGNORECASE)
        or re.search(r"^uber", engine, re.IGNORECASE)
        or re.search(r"^uber", oledb, re.IGNORECASE)
    ) and not re.search(r"SQLite", engine, re.IGNORECASE):
        return "query.uber"
    if re.search(r"\@SQL7\@\$NET\$S1\$\@", site, re.IGNORECASE) or oledb.upper() == "SQLSERVER":
        return "query.sqlserver"
    if oledb.upper() == "HADOOP":
        return "query.hadoop"
    if oledb.upper() == "HADOOPIMPALAODBC":
        return "query.impala"
    if oledb.upper() == "SNOWFLAKEODBC":
        return "query.snowflake"
    if oledb.upper() == "TERADATA" and engine.upper() == "VA":
        return "query.teradata"
    if engine.upper() == "SQLITE":
        return "query.sqlite"
    if engine.upper() == "CB":
        return "query.cb"
    if oledb.upper() == "TEXT":
        return "query.text"
    if re.match(r"MONGO", oledb, re.IGNORECASE) or re.search(r"\@MONGO\@", site, re.IGNORECASE):
        return "query.mongo"
    if re.match(r"MSOLAP", oledb, re.IGNORECASE):
        return "query.msolap"
    if re.match(r"MYSQL", oledb, re.IGNORECASE):
        return "query.mysql"
    if re.match(r"SAPHANAODBC", oledb, re.IGNORECASE):
        return "query.saphana"
    if re.match(r"DENODOODBC", oledb, re.IGNORECASE):
        return "query.denodo"
    if re.match(r"PYSCRIPTDRIVERIMPORT", oledb, re.IGNORECASE):
        return "query.pyscript_import"
    if re.match(r"PYSCRIPTDRIVER", oledb, re.IGNORECASE):
        return "query.pyscript"
    if re.match(r"DuckDB", oledb, re.IGNORECASE):
        return "query.duckdb"
    if re.match(r"POSTGRES", oledb, re.IGNORECASE):
        return "query.postgresql"
    return "query.oracle"


def _parse_sequence(
    flat: tuple[Command, ...], position: int, stop: frozenset[str]
) -> tuple[list[Command], int, str | None]:
    result: list[Command] = []
    while position < len(flat):
        command = flat[position]
        if command.kind is CommandKind.MARKER:
            if command.command_type in stop:
                return result, position, command.command_type
            raise _error(command.span, f"Unexpected controller marker {command.command_type!r}.")

        if command.kind is CommandKind.IF:
            children, position, marker = _parse_sequence(
                flat, position + 1, frozenset({"else", "end_if"})
            )
            if marker is None:
                raise _error(command.span, "Unclosed {IF-THEN}; expected {END-IF}.")
            else_children: list[Command] = []
            if marker == "else":
                else_children, position, marker = _parse_sequence(
                    flat, position + 1, frozenset({"end_if"})
                )
                if marker != "end_if":
                    raise _error(command.span, "Unclosed {ELSE}; expected {END-IF}.")
            result.append(
                replace(command, children=tuple(children), else_children=tuple(else_children))
            )
            position += 1
            continue

        expected_end = {
            CommandKind.MACRO: "end_macro",
            CommandKind.FOR_LOOP: "end_loop",
            CommandKind.SITE_LOOP: "end_loop",
            CommandKind.RUN_LOOP: "end_loop",
            CommandKind.SCOPE: "end_hpc",
        }.get(command.kind)
        if expected_end is not None:
            children, position, marker = _parse_sequence(
                flat, position + 1, frozenset({expected_end})
            )
            if marker != expected_end:
                raise _error(
                    command.span, f"Unclosed {command.command_type}; expected {expected_end}."
                )
            result.append(replace(command, children=tuple(children)))
            position += 1
            continue

        result.append(command)
        position += 1
    return result, position, None


def _trim_outer_blank_line(text: str) -> str:
    if text.startswith("\n"):
        text = text[1:]
    if text.endswith("\n"):
        text = text[:-1]
    return text


def _error(span: SourceSpan, message: str) -> Vg2ParseError:
    return Vg2ParseError(f"{span.location}: {message}")
