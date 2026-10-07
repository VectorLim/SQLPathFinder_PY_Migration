"""Parser/classifier/scope cases adapted from main's focused compiler tests."""

from pathlib import Path

import pytest

from scripthost_portable.runtime import _spf_manager_type
from scripthost_portable.task_introspection import inspect_task
from vg2c import CompileError
from vg2c.frontend import classify, parse
from vg2c.kind import Kind
from vg2c.operands import IfThen, StartMacro, utility_arguments
from vg2c.resolver import resolve


def block(options, body=""):
    return "<OPTIONS>\n" + options + "\n</OPTIONS>\n" + body


def job(*blocks):
    return "\n<---- New Query ---->\n".join(blocks)


def test_parser_preserves_order_body_and_source_spans():
    text = "\ufeff" + job(block("/B=2\n/A=1", "left"), block("/CSV=x.csv", "right\n"))
    blocks = parse(text.replace("\n", "\r\n").encode("utf-8"), Path("job.txt"))
    assert blocks[0].options.pairs == (("B", "2"), ("A", "1"))
    assert blocks[0].body == "left\n"
    assert blocks[1].body == "right\n"
    assert blocks[0].span.file == Path("job.txt")
    assert blocks[0].span.start_line == 1
    assert blocks[1].span.start_line == 7


def test_parser_inline_options_and_whitespace_separators():
    text = "/ENGINE=SQLite\n/CSV=x.csv\nSELECT 1\n   <----   New Query   ---->   \n"
    blocks = parse(text + block("/REPORT=HTML-DELETE", "N/A"))
    assert len(blocks) == 2
    assert blocks[0].body == "SELECT 1\n"
    assert classify(blocks)[0].kind is Kind.SQLITE_QUERY


def test_parser_preserves_source_body_final_newline():
    assert parse(block("/A=1", "payload\n"))[0].body == "payload\n"
    assert parse(block("/A=1", "payload"))[0].body == "payload"


@pytest.mark.parametrize(
    "text,code",
    [
        ("<OPTIONS>\n/A=1", "options-structure"),
        ("</OPTIONS>\nbody", "options-structure"),
        (block("/A=1\n/A=2"), "duplicate-option"),
        (block("/A=1\nnot an option"), "malformed-option"),
        ("lost work\n" + block("/A=1"), "options-prefix"),
        (block("/A=1", block("/B=2")), "options-structure"),
        (b"\xff", "encoding"),
    ],
)
def test_parser_rejects_lossy_recovery(text, code):
    with pytest.raises(CompileError) as error:
        parse(text, Path("broken.txt"))
    assert error.value.code == code
    assert "broken.txt:1:1" in str(error.value)


@pytest.mark.parametrize(
    "options,kind",
    [
        ("/ENGINE=VA\n/OLEDB=SQLPlus", Kind.SQL_QUERY),
        ("/OLEDB=SQLite", Kind.SQLITE_QUERY),
        ("/REPORT=HTML-DEFER", Kind.HTML_REPORT),
        ('/UTILITIES={START-MACRO} "config.csv" "N"', Kind.MACRO_CONTROL),
        ('/UTILITIES={ROWS-IN-FILE} "rows.csv" "COUNT" "N"', Kind.ROWS_IN_FILE),
        ('/UTILITIES={AED} "rows.csv"', Kind.AED),
    ],
)
def test_scoped_classification(options, kind):
    if kind in {Kind.SQL_QUERY, Kind.SQLITE_QUERY}:
        body = "SELECT 1"
    elif kind is Kind.HTML_REPORT:
        body = "TYPE<\\>HTML<\\>"
    else:
        body = ""
    assert classify(parse(block(options, body)))[0].kind is kind


def test_empty_query_is_not_classified_as_a_supported_query():
    with pytest.raises(CompileError):
        classify(parse(block("/ENGINE=SQLite\n/OLEDB=SQLite\n/CSV=result.csv")))


def test_original_task_descriptors_are_inspected_without_execution(monkeypatch):
    _spf_manager_type()
    from SPFLib.SPFSQL3 import SPFTaskBase

    def forbidden_execute(*args, **kwargs):
        pytest.fail("task inspection must not execute a task")

    monkeypatch.setattr(SPFTaskBase, "execute", forbidden_execute)
    raw_blocks = [
        block('/UTILITIES={START-MACRO} "config.csv" "N"'),
        block('/UTILITIES={IF-THEN} "COUNT" "GT" "0"'),
        block("/UTILITIES={END-IF}"),
        block("/UTILITIES={END-MACRO}"),
        block(
            "/NODE=KM.MARS\n/UN=\n/PW=\n/OLEDB=SQLPlus\n/ENGINE=VA\n"
            "/WORKDIR=.\\\n/CSV=result.csv\n/TABLE=source",
            "SELECT 1",
        ),
        block("/REPORT=HTML-DEFER\n/ID=RPT1\n/INSTANCE=RPT1", "TYPE<\\>HTML<\\>"),
    ]
    expected = [
        ("{START-MACRO}", "StartMacroTask", True, False, 0),
        ("{IF-THEN}", "IfThenTask", True, False, 0),
        ("{END-IF}", "EndIfTask", False, True, -1),
        ("{END-MACRO}", "EndMacroTask", False, True, 0),
        ("NQ_ORACLE_TASK", "nqOracleTask", False, False, 0),
        ("HTML-DEFER", "HTMLDeferTask", False, False, 0),
    ]
    for index, (raw, values) in enumerate(zip(raw_blocks, expected)):
        descriptor = inspect_task(raw, index)
        assert (
            descriptor.task_type,
            descriptor.class_name,
            descriptor.is_control_start,
            descriptor.is_control_end,
            descriptor.nest_level,
        ) == values
        assert descriptor.block_index == index


def test_original_node_routing_precedes_compiler_backend_support(tmp_path):
    raw = block(
        "/NODE=prefix@uber@suffix\n/UN=\n/PW=\n/OLEDB=SQLPlus\n/ENGINE=VA\n"
        "/CSV=result.csv\n/TABLE=source",
        "SELECT 1",
    )
    descriptor = inspect_task(raw, 0)
    assert (descriptor.task_type, descriptor.class_name) == ("NQ_UBER_Task", "nqUberTask")
    with pytest.raises(CompileError) as error:
        (tmp_path / "uber.txt").write_text(raw, encoding="utf-8")
        from vg2c import compile_document

        compile_document(tmp_path / "uber.txt")
    assert error.value.code == "unsupported-engine"


@pytest.mark.parametrize(
    "options",
    [
        "/ENGINE=VA\n/OLEDB=SQLite",
        "/ENGINE=Hadoop",
        "/ENGINE=",
        "/OLEDB=",
        "/ENGINE=SQLite\n/OLEDB=",
        "/REPORT=PDF",
        '/UTILITIES={RUN-LOOP} "a.csv"',
        "/UTILITIES={ELSE}",
        "/WRITE-FILE=Y",
    ],
)
def test_unsupported_classification_fails(options):
    with pytest.raises(CompileError):
        classify(parse(block(options)))


def test_scope_tree_preserves_macro_if_leaves_and_boundaries():
    blocks = classify(
        parse(
            job(
                block('/UTILITIES={START-MACRO} "config.csv" "Y"'),
                block('/UTILITIES={IF-THEN} "COUNT" "GT" "0" "" "" "" ""'),
                block('/UTILITIES={AED} "candidates.csv"'),
                block("/UTILITIES={END-IF}"),
                block("/UTILITIES={END-MACRO}"),
                block('/UTILITIES={ROWS-IN-FILE} "results.csv" "COUNT"'),
            )
        )
    )
    root = resolve(blocks).scope_tree
    assert [n.kind for n in root.children] == ["macro", "leaf"]
    macro = root.children[0]
    assert (macro.start_index, macro.end_index) == (0, 4)
    assert macro.control_payload == StartMacro("config.csv", True)
    condition = macro.children[0]
    assert condition.control_payload == IfThen("COUNT", "GT", "0")
    assert condition.children[0].block_index == 2
    assert (
        len(
            {
                root.scope_id,
                macro.scope_id,
                condition.scope_id,
                condition.children[0].scope_id,
                root.children[1].scope_id,
            }
        )
        == 5
    )


@pytest.mark.parametrize(
    "utilities,code",
    [
        (["{END-IF}"], "control-structure"),
        (["{END-MACRO}"], "control-structure"),
        (['{START-MACRO} "x.csv"', '{AED} "x.csv"'], "unclosed-control"),
        (['{IF-THEN} "COUNT" "GT" "0"', '{AED} "x.csv"', "{END-MACRO}"], "control-structure"),
        (['{START-MACRO} "x.csv"', '{START-MACRO} "y.csv"'], "macro-scope"),
        (['{START-MACRO} "x.csv"', "{END-MACRO}"], "empty-scope"),
        (['{IF-THEN} "A" "GT" "0" "AND" "B" "LT" "2"'], "condition-arguments"),
        (['{IF-THEN} "A" "BOGUS" "0"'], "condition-arguments"),
        (['{START-MACRO} "x.csv" "maybe"'], "macro-arguments"),
        (
            ['{START-MACRO} "x.csv"', '{AED} "x.csv"', "{END-MACRO}", '{AED} "<<<PATH>>>"'],
            "macro-lifetime",
        ),
        (
            ['{START-MACRO} "x.csv"', '{AED} "x.csv"', "{END-MACRO}", '{START-MACRO} "y.csv"'],
            "macro-scope",
        ),
        (['{AED} "<<<>>>"'], "positional-macro"),
    ],
)
def test_invalid_scopes_and_arguments_fail(utilities, code):
    with pytest.raises(CompileError) as error:
        resolve(classify(parse(job(*(block("/UTILITIES=" + v) for v in utilities)))))
    assert error.value.code == code


def test_utility_arguments_use_original_csv_quoting():
    value = '{ROWS-IN-FILE} "C:\\folder with spaces\\a""b.csv" "COUNT" "N" ""'
    parsed = classify(parse(block("/UTILITIES=" + value)))[0]
    assert utility_arguments(parsed) == ['C:\\folder with spaces\\a"b.csv', "COUNT", "N", ""]
    malformed = classify(parse(block('/UTILITIES={AED} "unfinished')))[0]
    with pytest.raises(CompileError, match="utility-arguments"):
        utility_arguments(malformed)
