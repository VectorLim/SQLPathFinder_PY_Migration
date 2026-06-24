from __future__ import annotations

from vg2c.frontend import Kind, classify
from vg2c.frontend.models import BlockOptions, ParsedBlock, SourceSpan


def _block(options: dict[str, str], body: str = "") -> ParsedBlock:
    return ParsedBlock(
        index=0,
        options=BlockOptions.from_pairs(options.items()),
        body=body,
        raw="",
        span=SourceSpan(file=None, start_line=1, end_line=1),
    )


def test_html_report_rule() -> None:
    for value in ("HTML-RUN", "HTML-LAYOUT", "HTML-DELETE"):
        classified, diagnostics = classify([_block({"REPORT": value})])
        assert classified[0].kind is Kind.HTML_REPORT
        assert not diagnostics


def test_write_file_rule_for_multiple_body_types() -> None:
    bodies = ["print('x')", "echo hi", "a,b\n1,2", "<html></html>"]
    for body in bodies:
        classified, diagnostics = classify(
            [_block({"WRITE-FILE": "Y", "CSV": "x"}, body=body)]
        )
        assert classified[0].kind is Kind.WRITE_FILE
        assert not diagnostics


def test_macro_control_rule_for_all_known_tokens() -> None:
    values = (
        '{START-MACRO} "macrotmp.csv" "N"',
        '{IF-THEN} "A" "LE" "0" "" "" "" ""',
        "{ELSE}",
        "{END-IF}",
        "{END-MACRO}",
        '{ROWS-IN-FILE} "x.csv" "COUNT" "N"',
    )
    for value in values:
        classified, diagnostics = classify([_block({"UTILITIES": value})])
        assert classified[0].kind is Kind.MACRO_CONTROL
        assert not diagnostics


def test_utility_rule_for_non_macro_values() -> None:
    values = (
        '@EXEDIR@\\Run_Python_Script.va "script.py" "" "N" "atd_atm.hadoop" "Python-v3"',
        "getcsrsu.bat",
    )
    for value in values:
        classified, diagnostics = classify([_block({"UTILITIES": value})])
        assert classified[0].kind is Kind.UTILITY
        assert not diagnostics


def test_sqlite_rule_for_oledb_or_engine() -> None:
    classified_oledb, _ = classify([_block({"OLEDB": "SQLite"})])
    classified_engine, _ = classify([_block({"ENGINE": "SQLite"})])

    assert classified_oledb[0].kind is Kind.SQLITE_QUERY
    assert classified_engine[0].kind is Kind.SQLITE_QUERY


def test_mars_rule() -> None:
    classified, diagnostics = classify(
        [_block({"NODE": "KM.[A15_PROD_21.].MARS", "ENGINE": "VA"})]
    )
    assert classified[0].kind is Kind.MARS_READ
    assert not diagnostics


def test_oasys_rule() -> None:
    classified, diagnostics = classify([_block({"NODE": "KM.OASYS", "ENGINE": "VA"})])
    assert classified[0].kind is Kind.OASYS_READ
    assert not diagnostics


def test_unknown_rule_emits_diagnostic() -> None:
    classified, diagnostics = classify([_block({"FOO": "BAR"})])

    assert classified[0].kind is Kind.UNKNOWN
    assert len(diagnostics) == 1
    assert diagnostics[0].code == "unknown-kind"
