"""Generated macro projects use one readable store and native control flow."""

import ast
import shutil
from types import SimpleNamespace

import pytest

from vg2c import compile_document, translate
from vg2c.emitter.models import CodeExpr
from vg2c.runtime import MacroStore
from vg2c.runtime.job import JobRuntime
from vg2c.utilities._emit_helpers import native_argument
from vg2c.utilities.csv_io import CsvIO
from vg2c.utilities.macro_control import MacroControl


def block(options, body=""):
    return "<OPTIONS>\n" + options + "\n</OPTIONS>\n" + body + "\n<---- New Query ---->\n"


def run_job(main):
    namespace = {"__file__": str(main), "__name__": "generated_job"}
    exec(compile(main.read_text(), str(main), "exec"), namespace)
    namespace["run"]()


def test_write_file_macro_arguments_strip_only_leading_whitespace(tmp_path, monkeypatch):
    source = tmp_path / "macro_command.txt"
    payload = "Sfolder,underDEV,useCSR,useMMS\nICMPCS_CWFNCO_CSR_IAM,N,Y,Y\n\n"
    source.write_text(
        block('/WRITE-FILE=Y\n/CSV=macrotmp.csv', "\n \t\n  " + payload)
        + block('/UTILITIES={START-MACRO} "macrotmp.csv" "N"')
        + block('/UTILITIES=setsiteparam.exe KM <<<SFOLDER>>> <<<UNDERDEV>>> '
                '<<<USECSR>>> <<<USEMMS>>>')
        + block('/UTILITIES={END-MACRO}')
    )
    calls = []

    def fake_run(argv, **kwargs):
        calls.append(argv)
        return SimpleNamespace(returncode=0)

    monkeypatch.setattr("vg2c.runtime.files.subprocess.run", fake_run)
    monkeypatch.setattr("vg2c.runtime.files.sys", SimpleNamespace(platform="win32"))
    main = translate(source)
    workdir = main.parent / "output"
    run_job(main)
    assert (workdir / "macrotmp.csv").read_text() == payload
    assert calls == [["setsiteparam.exe", "KM", "ICMPCS_CWFNCO_CSR_IAM", "N", "Y", "Y"]]


def test_scopes_and_independent_runs_and_empty_csv(tmp_path):
    source = tmp_path / "macros.txt"
    source.write_text(
        block('/UTILITIES={START-MACRO} "outer.csv" "N"')
        + block('/WRITE-FILE=Y\n/CSV=outer.txt', "<<<LABEL>>>")
        + block('/UTILITIES={START-MACRO} "inner.csv" "N"')
        + block('/WRITE-FILE=Y\n/CSV=inner.txt', "<<<LABEL>>>")
        + block('/UTILITIES={END-MACRO}')
        + block('/WRITE-FILE=Y\n/CSV=restored.txt', "<<<LABEL>>>")
        + block('/UTILITIES={END-MACRO}')
    )
    emitted = compile_document(source).emitted
    code = emitted.source
    tree = ast.parse(code)
    assert "macro_values_" not in code
    assert "macro_values =" not in code
    assert "job_entrypoint" not in code
    assert "job = JobRuntime.from_script(__file__)" in code
    assert "macros = job.macros" in code
    assert "with macros.scope(" in code
    assert any(isinstance(node, ast.With) for node in ast.walk(tree))
    assert any(isinstance(node, ast.If) for node in ast.walk(tree))
    for step in emitted.steps:
        assert code[step.source_range.start_offset:step.source_range.end_offset] == step.source
        for invocation in step.invocations:
            snippet = code[invocation.source_range.start_offset:invocation.source_range.end_offset]
            assert "(" in snippet and ")" in snippet
            for parameter in invocation.parameters:
                if parameter.source_range is not None:
                    assert code[parameter.source_range.start_offset:parameter.source_range.end_offset] == parameter.source

    main = translate(source)
    for label in ["first", "second"]:
        project = tmp_path / label
        shutil.copytree(main.parent, project)
        folder = project / "output"
        folder.mkdir()
        (folder / "outer.csv").write_text("LABEL\n" + label + "\nignored\n")
        (folder / "inner.csv").write_text("LABEL\ninner\n")
        run_job(project / "main.py")
        assert (folder / "outer.txt").read_text().strip() == label
        assert (folder / "inner.txt").read_text().strip() == "inner"
        assert (folder / "restored.txt").read_text().strip() == label

    empty = main.parent / "output"
    empty.mkdir()
    (empty / "outer.csv").write_text("LABEL\n")
    run_job(main)
    assert not (empty / "outer.txt").exists()
    assert not (empty / "inner.txt").exists()


def test_native_numeric_branch_and_editable_macro_assignment(tmp_path):
    source = tmp_path / "conditional.txt"
    source.write_text(
        block('/UTILITIES={ROWS-IN-FILE} "rows.csv" "COUNT" "N"')
        + block('/UTILITIES={IF-THEN} "COUNT" "GT" "0" "" "" "" ""')
        + block('/WRITE-FILE=Y\n/CSV=positive.txt', "<<<COUNT>>>")
        + block('/UTILITIES={ELSE}')
        + block('/WRITE-FILE=Y\n/CSV=empty.txt', "zero")
        + block('/UTILITIES={END-IF}')
    )
    emitted = compile_document(source).emitted
    code = emitted.source
    ast.parse(code)
    assert "macros['COUNT'] = str(job.row_count(" in code
    assert "int(macros['COUNT']) > int('0')" in code
    assert "macro_values_" not in code
    main = translate(source)
    folder = main.parent / "output"
    folder.mkdir()
    (folder / "rows.csv").write_text("ID\n1\n")
    run_job(main)
    assert (folder / "positive.txt").read_text().strip() == "1"


@pytest.mark.parametrize("value, expected", [
    ("EMAIL:<<<dEmail>>>", "EMAIL:recipient@example.com"),
    ("<<<first>>><<<SECOND>>>", "onetwo"),
    (["<<<FIRST>>>.csv", ("<<<SECOND>>>", "T0")], ["one.csv", ("two", "T0")]),
    ({"field": "<<<x > y>>>"}, {"field": "comparison"}),
    ("<<<line\nbreak>>>", "multiline"),
])
def test_macro_discovery_in_all_argument_values(value, expected):
    expression = native_argument(value, "macros")
    assert isinstance(expression, CodeExpr)
    assert not expression.has_value
    assert expression.symbol_names
    macros = MacroStore(initial={
        "dEmail": "recipient@example.com", "first": "one", "second": "two",
        "x > y": "comparison", "line\nbreak": "multiline",
    })
    assert eval(expression.source, {"macros": macros}) == expected
    if isinstance(value, str):
        assert macros.substitute(value) == expected
    assert native_argument("ordinary value", "macros") == "ordinary value"


def test_native_macro_assignment_tracks_nested_row_count_arguments():
    row_count = CsvIO.row_count.render("<<<ROW_FILE>>>")
    assignment = MacroControl.render_named_assignment(
        MacroControl.set_named.render("COUNT", CodeExpr(f"str({row_count})")),
        "macros",
    )

    assert str(assignment) == "macros['COUNT'] = str(job.row_count(macros['ROW_FILE']))"
    for argument in assignment.arguments:
        assert (
            str(assignment)[
                argument.source_range.start_offset : argument.source_range.end_offset
            ]
            == argument.source
        )


def test_generated_sql_and_html_options_discover_macros(tmp_path, monkeypatch):
    source = tmp_path / "options.txt"
    source.write_text(
        block('/UTILITIES={START-MACRO} "<<<CL_CONFIG>>>" "N"')
        + block('/ENGINE=SQLite\n/CSV=<<<RESULT>>>\n/TABLE=<<<INPUT>>>:T0',
                "select * from T0")
        + block('/REPORT=HTML-LAYOUT',
                ':FILE:EMAIL:<<<dEmail>>>\n:CSS:<<<STYLE>>>\n<p><<<FIRST>>></p>')
        + block('/UTILITIES={END-MACRO}')
    )
    emitted = compile_document(source).emitted
    code = emitted.source
    assert "job.read_macro_row(macros['CL_CONFIG'])" in code
    assert "output=macros['RESULT']" in code
    assert "inputs=[(macros['INPUT'], 'T0')]" in code
    assert "output='EMAIL:' + macros['DEMAIL']" in code
    assert "css_file=macros['STYLE']" in code
    sql_output = next(parameter for step in emitted.steps for parameter in step.parameters
                      if parameter.name == "output" and parameter.symbol_names == ("RESULT",))
    assert not sql_output.editable

    calls = []
    monkeypatch.setattr("vg2c.runtime.job.render_html", lambda path, **kwargs: calls.append(kwargs))
    main = translate(source)
    work = main.parent / "output"
    work.mkdir()
    (work / "config.csv").write_text(
        "RESULT,INPUT,dEmail,STYLE,FIRST\nresult.csv,data.csv,recipient@example.com,style.css,one\n"
    )
    (work / "data.csv").write_text("VALUE\nvalue\n")
    # CL_CONFIG is a per-run value rather than an environment variable.
    namespace = {"__file__": str(main), "__name__": "generated_job"}
    exec(compile(main.read_text(), str(main), "exec"), namespace)
    class ConfiguredJob(JobRuntime):
        def __init__(self, **kwargs):
            super().__init__(**kwargs, values={"CL_CONFIG": "config.csv"})

    namespace["JobRuntime"] = ConfiguredJob
    namespace["run"]()
    assert (work / "result.csv").read_text() == "VALUE\nvalue\n"
    assert calls[0]["output"] == "EMAIL:recipient@example.com"
    assert calls[0]["values"]["VALUE_1"] == "one"
