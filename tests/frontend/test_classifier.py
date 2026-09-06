from __future__ import annotations

import logging

from vg2c.frontend import classify
from vg2c.frontend.models import BlockOptions, ParsedBlock, SourceSpan
from vg2c.kind import Kind


def _block(options: dict[str, str], body: str = "") -> ParsedBlock:
    return ParsedBlock(
        index=0,
        options=BlockOptions.from_pairs(options.items()),
        body=body,
        raw="",
        span=SourceSpan(file=None, start_line=1, end_line=1),
    )


def test_html_report_classification() -> None:
    for value in ("HTML-RUN", "HTML-LAYOUT", "HTML-DELETE"):
        classified = classify([_block({"REPORT": value})])
        assert classified[0].kind is Kind.HTML_REPORT


def test_python_embed_vs_write_file_precedence() -> None:
    # WRITE-FILE=Y and CSV ending in .py -> PYTHON_EMBED
    py_block = _block({"WRITE-FILE": "Y", "CSV": "my_script.py"})
    classified_py = classify([py_block])
    assert classified_py[0].kind is Kind.PYTHON_EMBED

    # WRITE-FILE=Y and CSV not ending in .py -> WRITE_FILE
    txt_block = _block({"WRITE-FILE": "Y", "CSV": "data.txt"})
    classified_txt = classify([txt_block])
    assert classified_txt[0].kind is Kind.WRITE_FILE


def test_macro_control_precedence() -> None:
    # UTILITIES starting with { -> MACRO_CONTROL
    macro_block = _block({"UTILITIES": "{START-MACRO} my_macro.csv"})
    classified = classify([macro_block])
    assert classified[0].kind is Kind.MACRO_CONTROL


def test_utility_command_mapping() -> None:
    # External runs: Run_Python_Script.va, *.bat, *.exe (without starting with {)
    external_cases = (
        '@EXEDIR@\\Run_Python_Script.va "script.py"',
        "getcsrsu.bat",
        "setsiteparam.exe KM",
    )
    for cmd in external_cases:
        classified = classify([_block({"UTILITIES": cmd})])
        assert classified[0].kind is Kind.EXTERNAL_RUN

    # File System Copy: robocopy, spfcopy, spfrename
    copy_cases = (
        '@EXEDIR@\\RoboCopy.va "a.txt"',
        '@EXEDIR@\\SPFCopy.bat "a.txt"',
        '@EXEDIR@\\SPFRename.va "a.txt" "b.txt"',
    )
    for cmd in copy_cases:
        classified = classify([_block({"UTILITIES": cmd})])
        assert classified[0].kind is Kind.FS_COPY

    # File System Delete: spfdelete
    classified = classify([_block({"UTILITIES": '@EXEDIR@\\SPFDelete.bat "a.txt"'})])
    assert classified[0].kind is Kind.FS_DELETE

    # Email: sqlpathfinder_email
    classified = classify([_block({"UTILITIES": '@EXEDIR@\\SQLPathFinder_Email.va "to" "sub"'})])
    assert classified[0].kind is Kind.EMAIL


def test_sqlite_detection() -> None:
    for key in ("OLEDB", "ENGINE"):
        classified = classify([_block({key: "SQLite"})])
        assert classified[0].kind is Kind.SQLITE_QUERY


def test_oracle_sql_detection() -> None:
    oracle_nodes = (
        "KM.[A15_PROD_21.].MARS",
        "KM.OASYS",
        "KM.ARIES",
        "KM.<<<MARS>>>",
        "KM.<<<OASYS>>>",
        "KM.<<<ARIES>>>",
        "OASYS",
        "my_prefix.ARIES",
    )
    for node in oracle_nodes:
        classified = classify([_block({"NODE": node, "ENGINE": "VA"})])
        assert classified[0].kind is Kind.SQL_QUERY

        classified = classify([_block({"NODE": node, "OLEDB": "SQLPlus"})])
        assert classified[0].kind is Kind.SQL_QUERY


def test_unknown_fallback_and_diagnostic(caplog) -> None:
    with caplog.at_level(logging.WARNING):
        classified = classify([_block({"FOO": "BAR"})])
        assert classified[0].kind is Kind.UNKNOWN
        assert len(caplog.records) == 1
        assert "unknown-kind" in caplog.text

    caplog.clear()
    with caplog.at_level(logging.WARNING):
        classified = classify([_block({"UTILITIES": "unrecognized_utility_cmd.sh"})])
        assert classified[0].kind is Kind.UNKNOWN
        assert len(caplog.records) == 1
        assert "unknown-kind" in caplog.text
