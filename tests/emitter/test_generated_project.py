"""Generated jobs run offline from editable assets and an explicit work root."""

import ast
from pathlib import Path
import shutil
import sys
from types import SimpleNamespace

import pandas as pd

import pytest

from tests.fixtures.generated_job import capture_jobs
from vg2c import compile_document, translate

SQL = "<OPTIONS>\n/ENGINE=SQLite\n/CSV=result.csv\n</OPTIONS>\n-- preserved comment\nselect 1 as ID;\n"


def load_job(path):
    namespace = {"__file__": str(path), "__name__": "generated_job"}
    exec(compile(path.read_text(), str(path), "exec"), namespace)
    capture_jobs(namespace)

    def run():
        namespace["run"]()
        return namespace["_job"]

    return run


@pytest.mark.parametrize(
    "command",
    [
        'RoboCopy.va "HIST.txt" "source" "dest"',
        'SPFCopy.bat "source/HIST.txt" "dest" "N"',
    ],
)
def test_generated_copy_imports_path_and_executes(tmp_path, command):
    source = tmp_path / "copy.txt"
    source.write_text(f"<OPTIONS>\n/UTILITIES={command}\n</OPTIONS>\n")
    main = translate(source)
    workdir = main.parent / "output"
    (workdir / "source").mkdir(parents=True)
    (workdir / "dest").mkdir()
    (workdir / "source/HIST.txt").write_text("LOT\nexample\n")
    load_job(main)()
    assert (workdir / "dest/HIST.txt").read_text() == "LOT\nexample\n"


def test_project_assets_move_and_reread(tmp_path, monkeypatch):
    source = tmp_path / "source.txt"
    source.write_text(SQL)
    main = translate(source)
    assert main == tmp_path / "source" / "main.py"
    generated = main.read_text()
    tree = ast.parse(generated)
    assert [node.name for node in tree.body if isinstance(node, ast.FunctionDef)] == [
        "run"
    ]
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
    with pytest.raises(TypeError):
        run(workdir=other)
    assert not (other / "result.csv").exists()


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
    source.write_bytes(
        b"<OPTIONS>\r\n/ENGINE=SQLite\r\n/CSV=out.csv\r\n</OPTIONS>\r\n" + body
    )
    main = translate(source)
    assert next(main.parent.glob("sql/*.sql")).read_bytes() == body


def test_direct_invocation_ranges(tmp_path):
    source = tmp_path / "source.txt"
    source.write_text(SQL)
    result = compile_document(source)
    emitted = result.emitted
    for step in emitted.steps:
        assert (
            emitted.source[
                step.source_range.start_offset : step.source_range.end_offset
            ]
            == step.source
        )
        for invocation in step.invocations:
            snippet = emitted.source[
                invocation.source_range.start_offset : invocation.source_range.end_offset
            ]
            assert snippet.startswith("job.sql(")
            for parameter in invocation.parameters:
                if parameter.source_range:
                    assert (
                        emitted.source[
                            parameter.source_range.start_offset : parameter.source_range.end_offset
                        ]
                        == parameter.source
                    )


def block(options, body=""):
    return (
        "<OPTIONS>\n" + options + "\n</OPTIONS>\n" + body + "\n<---- New Query ---->\n"
    )


def test_local_html_delivery_options_have_source_diagnostics(tmp_path):
    source = tmp_path / "local.txt"
    source.write_text(
        block(
            "/REPORT=HTML-LAYOUT\n/OUTLOOK=Y", ":FILE:out.html\n:SEC:Y\n<h1>Local</h1>"
        )
    )
    result = compile_document(source)
    diagnostic = next(
        item for item in result.diagnostics if item.code == "local-html-only"
    )
    assert diagnostic.location.startswith(str(source))
    assert "SEC, OUTLOOK" in diagnostic.message


def test_authored_python_top_level_return_has_explicit_diagnostic(tmp_path):
    source = tmp_path / "return.txt"
    source.write_text(block("/WRITE-FILE=Y\n/CSV=code.py", "return"))
    with pytest.raises(ValueError, match="return/yield"):
        compile_document(source)


def test_retired_aed_directive_is_skipped_as_unsupported(tmp_path):
    source = tmp_path / "aed.txt"
    source.write_text(block('/UTILITIES={AED} "candidates.csv"'))
    result = compile_document(source)
    assert any(item.code == "skipped-block" for item in result.diagnostics)
    assert "csr_submission" not in result.emitted.source
    assert translate(source).is_file()


@pytest.mark.parametrize("lots", [["example"], []])
def test_generated_backend_nodes_expand_config_macros(tmp_path, monkeypatch, lots):
    calls = []

    class Reader:
        def read(self, *, site, query):
            calls.append((site, query.strip()))
            return pd.DataFrame({"LOT": lots})

    monkeypatch.setitem(
        sys.modules, "datasyncx", SimpleNamespace(MarsReader=Reader, AriesReader=Reader)
    )
    source = tmp_path / "routing.txt"
    source.write_text(
        block('/UTILITIES={START-MACRO} "configsets.csv" "N"')
        + block("/ENGINE=VA\n/NODE=<<<MARS>>>\n/CSV=mars.tab", "select 'mars'")
        + block("/ENGINE=VA\n/NODE=<<<ARIES>>>\n/CSV=aries.tab", "select 'aries'")
        + block("/UTILITIES={END-MACRO}")
    )
    main = translate(source)
    generated = main.read_text()
    assert "node=macros['MARS']" in generated
    assert "node=macros['ARIES']" in generated
    assert "macros.substitute(" not in generated
    work = main.parent / "output"
    work.mkdir()
    (work / "configsets.csv").write_text(
        "MARS,ARIES\nKM.[A15_PROD_21.].MARS,KM.ARIES\n"
    )
    load_job(main)()
    assert calls == [("KM", "select 'mars'"), ("KM", "select 'aries'")]
    expected = "lot\n" + "".join(f"{lot}\n" for lot in lots)
    assert (work / "mars.tab").read_text() == expected
    assert (work / "aries.tab").read_text() == expected


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
        + block('/UTILITIES={SITE-LOOP} "<<<NODES>>>"')
        + block(
            "/ENGINE=VA\n/NODE=<<<SPF-SITE>>>.MARS\n/CSV=<<<SPF-SITE-FOR-FILE-NAME>>>.csv",
            "select '<<<LABEL>>>'",
        )
        + block("/UTILITIES={END-LOOP}")
        + block('/UTILITIES={START-MACRO} "inner.csv" "Y"')
        + block("/ENGINE=SQLite\n/CSV=inner.csv", "select '<<<LABEL>>>' as VALUE")
        + block("/UTILITIES={END-MACRO}")
        + block("/ENGINE=SQLite\n/CSV=outer.csv", "select '<<<LABEL>>>' as VALUE")
        + block("/UTILITIES={END-MACRO}")
    )
    main = translate(source)
    generated = main.read_text()
    assert "node=macros['SPF-SITE'] + '.MARS'" in generated
    assert "macros.substitute(" not in generated
    tree = ast.parse(generated)
    assert any(
        isinstance(child, ast.Break)
        for handler in ast.walk(tree)
        if isinstance(handler, ast.ExceptHandler)
        for child in ast.walk(handler)
    )
    work = main.parent / "output"
    work.mkdir()
    (work / "config.csv").write_text(
        'LABEL,NODES\nouter,"PG.[A12_PROD_0.],KM.[A15_PROD_21.]"\nignored,\n'
    )
    (work / "inner.csv").write_text("LABEL\ninner\n")
    run = load_job(main)
    run()
    assert calls == [("PG", "select 'outer'"), ("KM", "select 'outer'")]
    assert (work / "outer.csv").read_text() == "VALUE\nouter\n"
    (work / "config.csv").write_text(
        'LABEL,NODES\nfresh,"PG.[A12_PROD_0.],KM.[A15_PROD_21.]"\n'
    )
    run()
    assert calls[-2:] == [("PG", "select 'fresh'"), ("KM", "select 'fresh'")]
    assert (work / "outer.csv").read_text() == "VALUE\nfresh\n"
    tree = ast.parse(main.read_text())
    assert any(isinstance(node, ast.For) for node in ast.walk(tree))
    assert any(isinstance(node, ast.If) for node in ast.walk(tree))


def test_generated_run_loop_closes_chunks_and_continues_on_prompt_off(tmp_path):
    source = tmp_path / "chunks.txt"
    source.write_text(
        block('/UTILITIES={RUN-LOOP} "input.csv" "chunk.csv" "2" "Y"')
        + block("/UTILITIES={END-LOOP}")
    )

    generated = compile_document(source).emitted.source
    tree = ast.parse(generated)

    assert "from contextlib import closing" in generated
    assert "with closing(job.csv_chunks('input.csv', 'chunk.csv', 2))" in generated
    assert "with macros.scope():" in generated
    assert any(
        isinstance(child, ast.Continue)
        for handler in ast.walk(tree)
        if isinstance(handler, ast.ExceptHandler)
        for child in ast.walk(handler)
    )
    assert "pass" in generated


def test_generated_run_loop_batches_query_and_append_in_order(tmp_path, monkeypatch):
    from vg2c.runtime import JobRuntime

    source = tmp_path / "unit_level_data.txt"
    source.write_text(
        block('/UTILITIES=@EXEDIR@\\SPFDelete.bat "data.csv" "N"')
        + block(
            '/UTILITIES={RUN-LOOP} "cam_lot_list.csv" "temp_cam_lot_list.csv" "10" "N" "0"'
        )
        + block(
            "/ENGINE=SQLite\n/CSV=xdata.csv\n/TABLE=temp_cam_lot_list.csv",
            "SELECT lot FROM temp_cam_lot_list",
        )
        + block('/UTILITIES=@EXEDIR@\\SmartAppend.va "data.csv" "xdata.csv"')
        + block("/UTILITIES={END-LOOP}")
    )
    main = translate(source)
    work = main.parent / "output"
    work.mkdir()
    (work / "cam_lot_list.csv").write_text(
        "lot\n" + "".join(f"LOT{i}\n" for i in range(23))
    )
    (work / "data.csv").write_text("lot\nstale\n")
    batches = []
    original_sql = JobRuntime.sql

    def sql(job, *args, **kwargs):
        batches.append(
            pd.read_csv(job.workdir / "temp_cam_lot_list.csv")["lot"].tolist()
        )
        return original_sql(job, *args, **kwargs)

    monkeypatch.setattr(JobRuntime, "sql", sql)
    load_job(main)()

    assert [len(batch) for batch in batches] == [10, 10, 3]
    assert pd.read_csv(work / "data.csv")["lot"].tolist() == [
        f"LOT{i}" for i in range(23)
    ]
    assert not (work / "temp_cam_lot_list.csv").exists()


def test_loop_html_and_file_macro_arguments_use_brackets(tmp_path):
    source = tmp_path / "macros.txt"
    source.write_text(
        block('/UTILITIES={START-MACRO} "config.csv" "N"')
        + block('/UTILITIES={FOR-LOOP} "0" "<<<END>>>" "1" "A" "N"')
        + block("/WRITE-FILE=Y\n/CSV=loop_<<<SPF-LOOP-CTR-A-INT>>>.txt", "<<<LABEL>>>")
        + block("/UTILITIES={END-LOOP}")
        + block("/REPORT=HTML-LAYOUT", ":FILE:result.html\n<p><<<LABEL>>></p>")
        + block("/WRITE-FILE=Y\n/CSV=data/<<<CANDIDATE>>>", "<<<LABEL>>>")
        + block("/UTILITIES={END-MACRO}")
    )
    main = translate(source)
    code = main.read_text()
    assert "for_values('0', macros['END'], '1', 'A', 'N')" in code
    assert "'VALUE_1': macros['LABEL']" in code
    assert "'data/' + macros['CANDIDATE']" in code
    assert "macros.substitute(" not in code
    work = main.parent / "output"
    work.mkdir()
    (work / "config.csv").write_text("END,LABEL,CANDIDATE\n2,<Safe &>,candidates.csv\n")
    job = load_job(main)()
    assert job.read_file("data/candidates.csv").strip() == "<Safe &>"
    assert [
        path.name for path in sorted(job.files) if path.name.startswith("loop_")
    ] == [
        "loop_0.txt",
        "loop_1.txt",
        "loop_2.txt",
    ]
    assert "&lt;Safe &amp;&gt;" in (work / "result.html").read_text()


def test_generated_html_reads_deferred_csv_and_edited_shell(tmp_path):
    source = tmp_path / "report.txt"
    delimiter = r"<\\>"
    options = "\n".join(
        delimiter.join(row)
        for row in [
            ["TYPE", "HTML"],
            ["INPUT-FILE", "data.csv"],
            ["OUTPUT-FILE", "fallback.html"],
            ["COLUMN-DATA", "", "value"],
            ["COLUMN-HEADERS", "", "Safe & Header"],
        ]
    )
    source.write_text(
        block("/REPORT=HTML-DEFER\n/ID=REPORT1", options)
        + block(
            "/REPORT=HTML-LAYOUT",
            ":FILE:result.html\n<h1>Authored <em>markup</em></h1>\nHTM:REPORT1",
        )
    )
    main = translate(source)
    work = main.parent / "output"
    work.mkdir()
    (work / "data.csv").write_text('value\n"<script>&"\n')
    run = load_job(main)
    run()
    result = (work / "result.html").read_text()
    assert "<em>markup</em>" in result
    assert "Safe &amp; Header" in result
    assert "&lt;script&gt;&amp;" in result
    shell = next(main.parent.glob("html/*.html"))
    shell.write_text(shell.read_text().replace("Authored", "Edited"))
    (work / "data.csv").write_text("value\nfresh\n")
    run()
    assert "Edited" in (work / "result.html").read_text()
    assert "fresh" in (work / "result.html").read_text()
