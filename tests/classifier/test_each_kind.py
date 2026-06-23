from __future__ import annotations

from tests.classifier.conftest import make_block
from vg2c.classifier import Kind, classify_block


def test_classify_sql_fetch_mars() -> None:
    block = make_block(
        {
            "ENGINE": "VA",
            "NODE": "KM.[A15_PROD_21.].MARS",
            "CSV": "output.csv",
            "HEADERS": "col1,col2",
            "RECORD": "F_Calendar@1.0.0.0",
        },
        body="SELECT * FROM @[]@F_Calendar",
    )
    result = classify_block(block)
    assert result.kind == Kind.SQL_FETCH


def test_classify_sqlite_join() -> None:
    block = make_block(
        {
            "ENGINE": "SQLite",
            "CSV": "output.csv",
            "TABLE": "table1.csv,table2.csv",
            "HEADERS": "col1,col2",
        },
        body="SELECT * FROM table1",
    )
    result = classify_block(block)
    assert result.kind == Kind.SQLITE_JOIN


def test_classify_write_file() -> None:
    block = make_block(
        {
            "WRITE-FILE": "Y",
            "CSV": "script.py",
        },
        body="import pandas as pd\nprint('hello')",
    )
    result = classify_block(block)
    assert result.kind == Kind.WRITE_FILE


def test_classify_run_python() -> None:
    block = make_block(
        {
            "UTILITIES": '@EXEDIR@\\Run_Python_Script.va "script.py" "" "N" "server" "Python-v3"',
            "WORKDIR": ".\\",
        }
    )
    result = classify_block(block)
    assert result.kind == Kind.RUN_PYTHON


def test_classify_copy() -> None:
    block = make_block(
        {
            "UTILITIES": "Copy.va source.csv dest.csv N",
        }
    )
    result = classify_block(block)
    assert result.kind == Kind.COPY


def test_classify_rename() -> None:
    block = make_block(
        {
            "UTILITIES": "Rename.va old.csv new.csv",
        }
    )
    result = classify_block(block)
    assert result.kind == Kind.RENAME


def test_classify_delete() -> None:
    block = make_block(
        {
            "UTILITIES": "Delete.va file.csv N",
        }
    )
    result = classify_block(block)
    assert result.kind == Kind.DELETE_FILE


def test_classify_email() -> None:
    block = make_block(
        {
            "UTILITIES": 'Email.va "file.csv" token "Subject" "body.txt" "user@example.com"',
        }
    )
    result = classify_block(block)
    assert result.kind == Kind.EMAIL


def test_classify_generic_utility() -> None:
    block = make_block(
        {
            "UTILITIES": "CustomScript.va arg1 arg2",
        }
    )
    result = classify_block(block)
    assert result.kind == Kind.RUN_UTILITY


def test_classify_if_then() -> None:
    block = make_block(
        {
            "UTILITIES": '{IF-THEN} "var1" "==" "value1"',
        }
    )
    result = classify_block(block)
    assert result.kind == Kind.IF_OPEN


def test_classify_if_else() -> None:
    block = make_block(
        {
            "UTILITIES": "{IF-ELSE}",
        }
    )
    result = classify_block(block)
    assert result.kind == Kind.IF_ELSE


def test_classify_end_if() -> None:
    block = make_block(
        {
            "UTILITIES": "{END-IF}",
        }
    )
    result = classify_block(block)
    assert result.kind == Kind.IF_CLOSE


def test_classify_start_macro() -> None:
    block = make_block(
        {
            "UTILITIES": '{START-MACRO} "driver.csv" "N"',
        }
    )
    result = classify_block(block)
    assert result.kind == Kind.MACRO_OPEN


def test_classify_end_macro() -> None:
    block = make_block(
        {
            "UTILITIES": "{END-MACRO}",
        }
    )
    result = classify_block(block)
    assert result.kind == Kind.MACRO_CLOSE


def test_classify_for_loop() -> None:
    block = make_block(
        {
            "UTILITIES": 'FOR-LOOP "data.csv" "site"',
        }
    )
    result = classify_block(block)
    assert result.kind == Kind.LOOP_OPEN


def test_classify_end_loop() -> None:
    block = make_block(
        {
            "UTILITIES": "{END-LOOP}",
        }
    )
    result = classify_block(block)
    assert result.kind == Kind.LOOP_CLOSE


def test_classify_block_group_open() -> None:
    block = make_block(
        {
            "UTILITIES": "{BEGIN-BLOCK-GROUP}",
        }
    )
    result = classify_block(block)
    assert result.kind == Kind.BLOCK_GROUP_OPEN


def test_classify_block_group_close() -> None:
    block = make_block(
        {
            "UTILITIES": "{END-BLOCK-GROUP}",
        }
    )
    result = classify_block(block)
    assert result.kind == Kind.BLOCK_GROUP_CLOSE


def test_classify_html_report() -> None:
    block = make_block(
        {
            "REPORT": "HTML-RUN",
            "INSTANCE": "12345",
        },
        body="<html>report content</html>",
    )
    result = classify_block(block)
    assert result.kind == Kind.HTML_REPORT


def test_classify_unknown() -> None:
    block = make_block(
        {
            "SOME-UNKNOWN-OPTION": "value",
        }
    )
    result = classify_block(block)
    assert result.kind == Kind.UNKNOWN
