from __future__ import annotations

from importlib import import_module
from typing import Any

from vg2c_new.utilities.base import Utility
from vg2c_new.utilities.email import EmailUtility
from vg2c_new.utilities.excel import ImportExcelUtility, LoadExcelUtility, XlsToCsvUtility
from vg2c_new.utilities.file_values import (
    AgeOfFileUtility,
    DateOfFileUtility,
    FileCompareUtility,
    GetFilesUtility,
    RowsInFileUtility,
    UpdateTimeUtility,
    ValueInFileUtility,
)
from vg2c_new.utilities.files import (
    AppendFileUtility,
    CopyFileUtility,
    DeleteFileUtility,
    DistributeUtility,
    RenameFileUtility,
    RoboCopyUtility,
    SetFileReadOnlyUtility,
    UnzipUtility,
    WaitFileUtility,
    WaitIntervalUtility,
    WriteFileUtility,
    ZipUtility,
)
from vg2c_new.utilities.misc import (
    CsvToHtmlUtility,
    CsvToXmlUtility,
    EchoUtility,
    StackDataUtility,
    XmlToCsvUtility,
)
from vg2c_new.utilities.process import (
    InlineRUtility,
    PyScriptUtility,
    RunPythonUtility,
    RunRUtility,
)
from vg2c_new.utilities.query import GetSiteTimeUtility, OracleQueryUtility, PlatformGapUtility
from vg2c_new.utilities.report import (
    HtmlDeferUtility,
    HtmlDeleteUtility,
    HtmlGnuPlotUtility,
    HtmlJsUtility,
    HtmlLayoutUtility,
    HtmlPyPlotUtility,
    HtmlRPlotUtility,
    HtmlRunUtility,
    HtmlTabMenuLayoutUtility,
)
from vg2c_new.utilities.smart_append import SmartAppendUtility
from vg2c_new.utilities.sqlite import SqliteDeleteUtility, SqliteLoadUtility, SqliteQueryUtility
from vg2c_new.utilities.web import GetWebTextUtility


def build_runtime_utilities() -> dict[str, Utility]:
    """Compose direct-runtime dependencies into the existing plain target->Utility dictionary."""
    mars = _construct(("datasyncx.readers.mars_reader", "MarsReader"), ("datasyncx", "MarsReader"))
    aries = _construct(
        ("datasyncx.readers.aries_reader", "AriesReader"), ("datasyncx", "AriesReader")
    )
    oracle_cls = _find_attribute(
        ("datasyncx.readers.oracle_reader", "OracleReader"),
        ("datasyncx", "OracleReader"),
    )
    oasys = _construct_type(oracle_cls, database="OASYS")
    generic_oracle = None
    send_msg = _find_attribute(
        ("datasyncx", "send_msg"),
        ("datasyncx.email", "send_msg"),
        ("datasyncx.mail", "send_msg"),
    )
    query_oracle = OracleQueryUtility(
        mars_reader=mars,
        aries_reader=aries,
        oasys_reader=oasys,
        oracle_reader=generic_oracle,
    )
    site_time = GetSiteTimeUtility(
        mars_reader=mars,
        aries_reader=aries,
        oasys_reader=oasys,
        oracle_reader=generic_oracle,
    )
    html_tab_menu = HtmlTabMenuLayoutUtility()
    html_js = HtmlJsUtility()
    html_pyplot = HtmlPyPlotUtility()
    html_gnuplot = HtmlGnuPlotUtility()
    html_rplot = HtmlRPlotUtility()
    utilities: dict[str, Utility] = {
        "append_file": AppendFileUtility(),
        "copy_file": CopyFileUtility(),
        "csv_to_html": CsvToHtmlUtility(),
        "csv_to_xml": CsvToXmlUtility(),
        "delete_file": DeleteFileUtility(),
        "email": EmailUtility(send_msg),
        "import_excel": ImportExcelUtility(),
        "load_excel": LoadExcelUtility(),
        "xls_to_csv": XlsToCsvUtility(),
        "set_file_read_only": SetFileReadOnlyUtility(),
        "run_python": RunPythonUtility(),
        "run_r": RunRUtility(),
        "rscript": InlineRUtility(),
        "robocopy": RoboCopyUtility(),
        "smart_append": SmartAppendUtility(),
        "rename_file": RenameFileUtility(),
        "sqlite_delete": SqliteDeleteUtility(),
        "sqlite_load": SqliteLoadUtility(),
        "unzip": UnzipUtility(),
        "distribute": DistributeUtility(),
        "wait_interval": WaitIntervalUtility(),
        "wait_file": WaitFileUtility(),
        "get_web_text": GetWebTextUtility(),
        "echo": EchoUtility(),
        "xml_to_csv": XmlToCsvUtility(),
        "zip": ZipUtility(),
        "age_of_file": AgeOfFileUtility(),
        "date_of_file": DateOfFileUtility(),
        "get_site_time": site_time,
        "rows_in_file": RowsInFileUtility(),
        "update_time": UpdateTimeUtility(),
        "update_time_file": UpdateTimeUtility(),
        "value_in_file": ValueInFileUtility(),
        "get_files": GetFilesUtility(),
        "file_compare": FileCompareUtility(),
        "pyscript": PyScriptUtility(),
        "write_file": WriteFileUtility(),
        "report.html_run": HtmlRunUtility(),
        "report.html_defer": HtmlDeferUtility(),
        "report.html_layout": HtmlLayoutUtility(),
        "report.html_tab_layout": html_tab_menu,
        "report.html_menu_layout": html_tab_menu,
        "report.delete": HtmlDeleteUtility(),
        "report.js_show": html_js,
        "report.js_defer": html_js,
        "report.pyplot": html_pyplot,
        "report.pyplot_show": html_pyplot,
        "report.gnuplot": html_gnuplot,
        "report.gnuplot_show": html_gnuplot,
        "report.rplot": html_rplot,
        "report.rplot_show": html_rplot,
        "query.sqlite": SqliteQueryUtility(),
        "query.oracle": query_oracle,
        "stack_data": StackDataUtility(),
    }
    gaps = {
        "get_aqua": "no approved portable AQUA integration is present in the repository",
        "get_tca": "no approved portable TCA integration is present in the repository",
        "hadoop_delete": "DataSyncX Hadoop mutation API is not evidenced by the pinned integration contract",
        "hadoop_load": "DataSyncX Hadoop load API is not evidenced by the pinned integration contract",
        "prompt_input": "interactive prompting is intentionally excluded from the direct runtime",
        "load_jmp": "JMP/COM execution is Windows-only and intentionally retired",
        "publish_sharepoint": "no approved portable SharePoint write integration is present",
        "tdx_to_csv": "TDX conversion has no approved portable backend",
        "get_help_csv": "the current help-query metadata source has not been exposed as a portable API",
        "join_by_name": "current VA_Join_By_Name semantics have no characterized portable contract yet",
        "mongo_export": "DataSyncX Mongo export API is not evidenced by the pinned integration contract",
        "mongo_import": "DataSyncX Mongo import API is not evidenced by the pinned integration contract",
        "mongo_extract": "DataSyncX Mongo extract API is not evidenced by the pinned integration contract",
        "verify_role": "portable identity/role provider is not defined for the direct runtime",
        "get_site_time_file": "legacy .spf$data persistence has no direct-runtime instance identity",
        "sharepoint_delete": "no approved portable SharePoint delete integration is present",
        "encrypt_text": "legacy ScriptHost encryption format has no approved replacement security contract",
        "encrypt_spfsql": "legacy encrypted VG2 file format is intentionally not restored",
        "encrypt_config": "legacy encrypted config format is intentionally not restored",
        "jsl": "JMP/JSL/COM execution is Windows-only and intentionally retired",
        "cb_acs": "current portable CB_ACS integration is not present",
        "prompt_jobid": "legacy UI job-id prompting is intentionally retired",
        "noop": "historical DUMMY compatibility target is intentionally retired",
        "shell": "generic DOS/shell fallback is intentionally retired",
        "va": "historical VA wrapper execution is intentionally retired",
    }
    query_gaps = {
        "query.cbsql_oracle": "CBSQL",
        "query.uber": "Uber",
        "query.sqlserver": "SQL Server",
        "query.hadoop": "Hadoop",
        "query.cb": "CB",
        "query.teradata": "Teradata",
        "query.text": "legacy Text/JET SQL",
        "query.mongo": "Mongo",
        "query.impala": "Impala",
        "query.msolap": "MSOLAP",
        "query.mysql": "MySQL",
        "query.saphana": "SAP HANA",
        "query.denodo": "Denodo",
        "query.pyscript": "PyScriptDriver",
        "query.pyscript_import": "PyScriptDriverImport",
        "query.duckdb": "DuckDB",
        "query.snowflake": "Snowflake",
        "query.postgresql": "PostgreSQL",
    }
    for target, system in query_gaps.items():
        gaps[target] = (
            f"no approved modern {system} reader/output contract is evidenced by "
            "the pinned project dependencies"
        )
    utilities.update(
        {target: PlatformGapUtility(target, reason) for target, reason in gaps.items()}
    )
    return utilities


def _construct(*locations: tuple[str, str]) -> Any | None:
    return _construct_type(_find_attribute(*locations))


def _construct_type(cls: Any | None, **kwargs: Any) -> Any | None:
    if cls is None:
        return None
    return cls(**kwargs)


def _find_attribute(*locations: tuple[str, str]) -> Any | None:
    for module_name, attribute in locations:
        try:
            module = import_module(module_name)
        except ImportError:
            continue
        value = getattr(module, attribute, None)
        if value is not None:
            return value
    return None
