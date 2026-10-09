"""Generated jobs run offline from editable assets and an explicit work root."""

import ast
from pathlib import Path
import shutil
import sys
from types import SimpleNamespace

import pandas as pd

import pytest

from vg2c import compile_document, translate


SQL = '<OPTIONS>\n/ENGINE=SQLite\n/CSV=result.csv\n</OPTIONS>\n-- preserved comment\nselect 1 as ID;\n'


def load_job(path):
    namespace = {"__file__": str(path), "__name__": "generated_job"}
    exec(compile(path.read_text(), str(path), "exec"), namespace)
    return namespace["run"]


def test_project_assets_move_and_reread(tmp_path, monkeypatch):
    source = tmp_path / "source.txt"
    source.write_text(SQL)
    main = translate(source)
    assert main == tmp_path / "source" / "main.py"
    generated = main.read_text()
    tree = ast.parse(generated)
    assert [node.name for node in tree.body if isinstance(node, ast.FunctionDef)] == ["run"]
    assert "ctx" not in generated
    assert "GetQuery" not in generated
    assert "class " not in generated
    assets = list(main.parent.glob("sql/*.sql"))
    assert len(assets) == 1
    assert "-- preserved comment" in assets[0].read_text()
    moved = tmp_path / "moved"
    shutil.move(main.parent, moved)
    source.unlink()
    other = tmp_path / "other"
    other.mkdir()
    monkeypatch.chdir(other)
    run = load_job(moved / "main.py")
    run()
    assert (moved / "output/result.csv").read_text() == "ID\n1\n"
    (moved / "sql" / assets[0].name).write_text("select 2 as ID;\n")
    run()
    assert (moved / "output/result.csv").read_text() == "ID\n2\n"
    run(workdir=other)
    assert (other / "result.csv").read_text() == "ID\n2\n"


def test_project_refuses_nonempty_without_writes(tmp_path):
    source = tmp_path / "a.b.txt"
    source.write_text(SQL)
    main = translate(source)
    sentinel = main.parent / "sentinel"
    sentinel.write_text("keep")
    original = main.read_bytes()
    with pytest.raises(FileExistsError):
        translate(source)
    assert main.read_bytes() == original
    assert sentinel.read_text() == "keep"
    collision = tmp_path / "a b.txt"
    collision.write_text(SQL)
    with pytest.raises(FileExistsError):
        translate(collision)


def test_sql_asset_preserves_body_comments_and_newlines(tmp_path):
    source = tmp_path / "exact.txt"
    body = b"\r\n-- comment ;\r\nselect 1;\r\n\r\n"
    source.write_bytes(b"<OPTIONS>\r\n/ENGINE=SQLite\r\n/CSV=out.csv\r\n</OPTIONS>\r\n" + body)
    main = translate(source)
    assert next(main.parent.glob("sql/*.sql")).read_bytes() == body


def test_direct_invocation_ranges(tmp_path):
    source = tmp_path / "source.txt"
    source.write_text(SQL)
    result = compile_document(source)
    emitted = result.emitted
    for step in emitted.steps:
        assert emitted.source[step.source_range.start_offset:step.source_range.end_offset] == step.source
        for invocation in step.invocations:
            snippet = emitted.source[invocation.source_range.start_offset:invocation.source_range.end_offset]
            assert snippet.startswith("execute_sql(")
            for parameter in invocation.parameters:
                if parameter.source_range:
                    assert emitted.source[parameter.source_range.start_offset:parameter.source_range.end_offset] == parameter.source


def block(options, body=""):
    return "<OPTIONS>\n" + options + "\n</OPTIONS>\n" + body + "\n<---- New Query ---->\n"


def test_local_html_delivery_options_have_source_diagnostics(tmp_path):
    source = tmp_path / "local.txt"
    source.write_text(block('/REPORT=HTML-LAYOUT\n/OUTLOOK=Y', ':FILE:out.html\n:SEC:Y\n<h1>Local</h1>'))
    result = compile_document(source)
    diagnostic = next(item for item in result.diagnostics if item.code == "local-html-only")
    assert diagnostic.location.startswith(str(source))
    assert "SEC, OUTLOOK" in diagnostic.message


def test_authored_python_top_level_return_has_explicit_diagnostic(tmp_path):
    source = tmp_path / "return.txt"
    source.write_text(block('/WRITE-FILE=Y\n/CSV=code.py', 'return'))
    with pytest.raises(ValueError, match="return/yield"):
        compile_document(source)


def test_aed_bootstrap_values_feed_native_macros_without_environment_mutation(tmp_path):
    source = tmp_path / "aed.txt"
    source.write_text(block('/WRITE-FILE=Y\n/CSV=node.txt', '<<<MARS>>>')
                      + block('/UTILITIES={AED} "candidates.csv"'))
    main = translate(source)
    namespace = {"__file__": str(main), "__name__": "job"}
    exec(main.read_text(), namespace)
    config = {"MARS": "PG.[A12_PROD_0.].MARS", "ENV_MODE": "test"}
    namespace["bootstrap_aed"] = lambda **kwargs: config.copy()
    calls = []
    namespace["process_candidates"] = lambda path, **kwargs: calls.append((path, kwargs))
    factory = object()
    namespace["run"](tmp_path / "work", aed_service_factory=factory)
    assert (tmp_path / "work/node.txt").read_text().strip() == config["MARS"]
    assert calls[0][0] == "candidates.csv"
    assert calls[0][1]["service_factory"] is factory
    assert config == {"MARS": "PG.[A12_PROD_0.].MARS", "ENV_MODE": "test"}


def test_native_site_routing_and_macro_scope(tmp_path, monkeypatch):
    calls = []
    class Reader:
        def read(self, *, site, query):
            calls.append((site, query.strip()))
            return pd.DataFrame({"VALUE": [site]})
    monkeypatch.setitem(sys.modules, "datasyncx", SimpleNamespace(MarsReader=Reader))
    source = tmp_path / "routing.txt"
    source.write_text(
        block('/UTILITIES={START-MACRO} "config.csv" "Y"')
        + block('/UTILITIES={SITE-LOOP} "PG.[A12_PROD_0.],KM.[A15_PROD_21.]"')
        + block('/ENGINE=VA\n/NODE=<<<SPF-SITE>>>.MARS\n/CSV=<<<SPF-SITE-FOR-FILE-NAME>>>.csv', "select '<<<LABEL>>>'")
        + block('/UTILITIES={END-LOOP}')
        + block('/UTILITIES={START-MACRO} "inner.csv" "Y"')
        + block('/ENGINE=SQLite\n/CSV=inner.csv', "select '<<<LABEL>>>' as VALUE")
        + block('/UTILITIES={END-MACRO}')
        + block('/ENGINE=SQLite\n/CSV=outer.csv', "select '<<<LABEL>>>' as VALUE")
        + block('/UTILITIES={END-MACRO}')
    )
    main = translate(source)
    work = tmp_path / "work"
    work.mkdir()
    (work / "config.csv").write_text("LABEL\nouter\nignored\n")
    (work / "inner.csv").write_text("LABEL\ninner\n")
    run = load_job(main)
    run(work)
    assert calls == [("PG", "select 'outer'"), ("KM", "select 'outer'")]
    assert (work / "outer.csv").read_text() == "VALUE\nouter\n"
    (work / "config.csv").write_text("LABEL\nfresh\n")
    run(work)
    assert calls[-2:] == [("PG", "select 'fresh'"), ("KM", "select 'fresh'")]
    assert (work / "outer.csv").read_text() == "VALUE\nfresh\n"
    tree = ast.parse(main.read_text())
    assert any(isinstance(node, ast.For) for node in ast.walk(tree))
    assert any(isinstance(node, ast.If) for node in ast.walk(tree))


def test_generated_html_reads_deferred_csv_and_edited_shell(tmp_path):
    source = tmp_path / "report.txt"
    delimiter = r"<\\>"
    options = "\n".join(delimiter.join(row) for row in [
        ["TYPE", "HTML"], ["INPUT-FILE", "data.csv"], ["OUTPUT-FILE", "fallback.html"],
        ["COLUMN-DATA", "", "value"], ["COLUMN-HEADERS", "", "Safe & Header"],
    ])
    source.write_text(block('/REPORT=HTML-DEFER\n/ID=REPORT1', options)
                      + block('/REPORT=HTML-LAYOUT', ':FILE:result.html\n<h1>Authored <em>markup</em></h1>\nHTM:REPORT1'))
    main = translate(source)
    work = tmp_path / "work"
    work.mkdir()
    (work / "data.csv").write_text('value\n"<script>&"\n')
    run = load_job(main)
    run(work)
    result = (work / "result.html").read_text()
    assert "<em>markup</em>" in result
    assert "Safe &amp; Header" in result
    assert "&lt;script&gt;&amp;" in result
    shell = next(main.parent.glob("html/*.html"))
    shell.write_text(shell.read_text().replace("Authored", "Edited"))
    (work / "data.csv").write_text("value\nfresh\n")
    run(work)
    assert "Edited" in (work / "result.html").read_text()
    assert "fresh" in (work / "result.html").read_text()
