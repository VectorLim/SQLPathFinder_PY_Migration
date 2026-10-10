"""Per-run runtime ownership and generated-call contracts."""

import ast
from concurrent.futures import ThreadPoolExecutor

import pytest

from vg2c import compile_document, translate
from vg2c.runtime import JobRuntime, SqliteReader


def _block(options, body=""):
    return "<OPTIONS>\n" + options + "\n</OPTIONS>\n" + body + "\n<---- New Query ---->\n"


def test_isolated_jobs_have_independent_values_macros_and_output_roots(tmp_path):
    assets = tmp_path / "assets"
    assets.mkdir()
    roots = [tmp_path / "one", tmp_path / "two"]
    for root in roots:
        root.mkdir()

    def execute(pair):
        root, label = pair
        job = JobRuntime(assets, root, values={"CL_LABEL": label},
                         environ={"EXAMPLE": label}, initial_macros={"LABEL": label})
        with job.macros.scope({"LABEL": label.lower()}):
            job.write_file("nested/out.txt", "<<<LABEL>>>/<<<CL_LABEL>>>/<<<%EXAMPLE%>>>")
        job.write_file("final.txt", "<<<LABEL>>>")
        return job

    with ThreadPoolExecutor(max_workers=2) as pool:
        jobs = list(pool.map(execute, zip(roots, ["ALPHA", "BETA"])))
    assert (roots[0] / "nested/out.txt").read_text() == "alpha/ALPHA/ALPHA"
    assert (roots[1] / "nested/out.txt").read_text() == "beta/BETA/BETA"
    assert (roots[0] / "final.txt").read_text() == "ALPHA"
    assert (roots[1] / "final.txt").read_text() == "BETA"
    assert jobs[0].values is not jobs[1].values
    assert jobs[0].macros is not jobs[1].macros


def test_sql_and_html_assets_reread_from_project_not_output(tmp_path, monkeypatch):
    project = tmp_path / "project"
    (project / "sql").mkdir(parents=True)
    (project / "html").mkdir()
    (project / "sql/q.sql").write_text("select 1 as ID;\n")
    (project / "html/page.html").write_text("<html><head></head><body>Initial</body></html>")
    output = tmp_path / "output"
    elsewhere = tmp_path / "elsewhere"
    elsewhere.mkdir()
    monkeypatch.chdir(elsewhere)

    job = JobRuntime(project, output)
    job.sql("sql/q.sql", reader=SqliteReader(), output="data/result.csv")
    assert (output / "data/result.csv").read_text() == "ID\n1\n"
    (project / "sql/q.sql").write_text("select 2 as ID;\n")
    job.sql("sql/q.sql", reader=SqliteReader(), output="data/result.csv")
    assert (output / "data/result.csv").read_text() == "ID\n2\n"
    job.html("html/page.html", output="result.html")
    assert "Initial" in (output / "result.html").read_text()
    (project / "html/page.html").write_text("<html><head></head><body>Edited</body></html>")
    job.html("html/page.html", output="result.html")
    assert "Edited" in (output / "result.html").read_text()
    assert not (elsewhere / "result.html").exists()


def test_asset_root_guard_and_first_row_macro_snapshot(tmp_path):
    project = tmp_path / "project"
    project.mkdir()
    work = tmp_path / "work"
    work.mkdir()
    (work / "input.csv").write_text("LABEL\nfresh\nignored\n")
    job = JobRuntime(project, work)
    row = job.read_macro_row("input.csv")
    with job.macros.scope(row):
        assert job.macros["LABEL"] == "fresh"
    with pytest.raises(ValueError, match="Asset path"):
        job.asset_path("../outside.sql")
    with pytest.raises(FileNotFoundError):
        job.sql("sql/missing.sql", reader=SqliteReader(), output="out.csv")


def test_emitted_calls_avoid_context_kwargs_and_preserve_metadata(tmp_path):
    source = tmp_path / "sample.txt"
    source.write_text(
        _block('/ENGINE=SQLite\n/CSV=rows.csv', "select 3 as ID;")
        + _block('/UTILITIES={ROWS-IN-FILE} "rows.csv" "COUNT" "N"')
        + _block('/UTILITIES={IF-THEN} "COUNT" "GT" "0" "" "" "" ""')
        + _block('/WRITE-FILE=Y\n/CSV=label.txt', '<<<COUNT>>>')
        + _block('/UTILITIES={END-IF}')
    )
    result = compile_document(source).emitted
    tree = ast.parse(result.source)
    assert "job = JobRuntime(BASE_DIR, workdir)" in result.source
    assert "job.sql(" in result.source
    assert "job.write_file(" in result.source
    assert "job.row_count(" in result.source
    assert "workdir=workdir" not in result.source
    assert "values=job_values" not in result.source
    for step in result.steps:
        assert result.source[step.source_range.start_offset:step.source_range.end_offset] == step.source
        for invocation in step.invocations:
            assert result.source[invocation.source_range.start_offset:invocation.source_range.end_offset]
            for param in invocation.parameters:
                if param.source_range:
                    assert result.source[param.source_range.start_offset:param.source_range.end_offset] == param.source
    assert any(isinstance(node, ast.If) for node in ast.walk(tree))
    main = translate(source)
    work = tmp_path / "override"
    work.mkdir()
    namespace = {"__file__": str(main), "__name__": "generated"}
    exec(main.read_text(), namespace)
    namespace["run"](work)
    assert (work / "label.txt").read_text() == "1"
