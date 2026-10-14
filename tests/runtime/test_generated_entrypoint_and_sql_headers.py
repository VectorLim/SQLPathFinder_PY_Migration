"""Generated Python exposes setup and keeps SQL headers in editable assets."""

from __future__ import annotations

import inspect
import runpy
import shutil

import pytest

from vg2c import compile_document, translate
from vg2c.runtime import JobRuntime, SqliteReader, execute_sql


def _source(tmp_path, *, headers=None):
    options = "<OPTIONS>\n/ENGINE=SQLite\n/CSV=result.csv\n"
    if headers is not None:
        options += "/HEADERS=" + headers + "\n"
    source = tmp_path / "sample.txt"
    source.write_text(options + "</OPTIONS>\n"
                      + "SELECT '001' AS code, 'Example' AS label\n", encoding="utf-8")
    return source


def test_generated_run_is_parameterless_and_uses_relocated_project_output(tmp_path, monkeypatch):
    source = _source(tmp_path)
    main = translate(source)
    text = main.read_text(encoding="utf-8")
    assert "job_entrypoint" not in text
    assert "def run():" in text
    assert "job.sql(" in text
    assert "workdir = Path(workdir).resolve()" not in text
    assert "job = JobRuntime.from_script(__file__)" in text
    assert "project_dir =" not in text
    assert "workdir =" not in text
    assert "BASE_DIR =" not in text
    assert "WORK_DIR =" not in text
    assert "header=" not in text
    assert "inputs=[]" not in text  # Empty TABLE configuration is the runtime default.

    moved = tmp_path / "relocated"
    shutil.move(str(main.parent), str(moved))
    loaded = runpy.run_path(str(moved / "main.py"), run_name="translated")
    workdir = moved / "output"
    assert not inspect.signature(loaded["run"]).parameters
    monkeypatch.chdir(tmp_path)
    loaded["run"]()
    assert (workdir / "result.csv").read_text() == "code,label\n001,Example\n"
    assert not (tmp_path / "result.csv").exists()
    with pytest.raises(TypeError):
        loaded["run"](workdir=tmp_path / "external")
    with pytest.raises(TypeError):
        loaded["run"](tmp_path / "external")

    monkeypatch.chdir(tmp_path)
    loaded["run"]()
    assert (moved / "output" / "result.csv").read_text() == "code,label\n001,Example\n"


def test_generated_run_creates_fresh_state_each_time(tmp_path, monkeypatch):
    main = translate(_source(tmp_path))
    jobs = []

    class RecordingJob(JobRuntime):
        def __init__(self, **kwargs):
            super().__init__(**kwargs)
            jobs.append(self)

    monkeypatch.setattr("vg2c.runtime.JobRuntime", RecordingJob)
    run = runpy.run_path(str(main), run_name="generated")["run"]
    run()
    jobs[0].macros["PREVIOUS_RUN"] = "old"
    run()
    assert len(jobs) == 2
    assert jobs[0] is not jobs[1]
    assert jobs[1].macros["PREVIOUS_RUN"] is None
    assert jobs[0].values is not jobs[1].values
    assert jobs[0].reports is not jobs[1].reports


def test_explicit_legacy_header_is_stored_in_sql_not_generated_main(tmp_path):
    source = _source(tmp_path, headers="CODE,LABEL")
    emitted = compile_document(source).emitted
    assert "header=" not in emitted.source
    assert "job.table_spec(" not in emitted.source
    assert "job.sql(" in emitted.source
    path, sql = next((p, text) for p, text in emitted.assets if p.endswith(".sql"))
    assert sql.startswith('-- vg2c-output-headers: ["CODE", "LABEL"]\n')
    assert "SELECT '001' AS code" in sql
    assert not any(path.endswith(".header.json") for path, _ in emitted.assets)

    main = translate(source)
    run = runpy.run_path(str(main), run_name="generated")["run"]
    workdir = main.parent / "output"
    run()
    assert (workdir / "result.csv").read_text() == "CODE,LABEL\n001,Example\n"

    asset = main.parent / path
    asset.write_text(sql.replace(" AS code", " AS identifier"), encoding="utf-8")
    run()
    # Explicit /HEADERS is a name-based projection, not a positional rename.
    assert (workdir / "result.csv").read_text() == "CODE,LABEL\n,Example\n"
    asset.write_text(asset.read_text().split("\n", 1)[1], encoding="utf-8")
    run()
    assert (workdir / "result.csv").read_text() == "identifier,label\n001,Example\n"


@pytest.mark.parametrize("bad", [
    "-- vg2c-output-headers: {}\nSELECT 1 AS code",
    "-- vg2c-output-headers: [1]\nSELECT 1 AS code",
    "-- vg2c-output-headers: broken\nSELECT 1 AS code",
])
def test_bad_sql_header_directive_fails_visibly(tmp_path, bad):
    asset = tmp_path / "custom.sql"
    asset.write_text(bad, encoding="utf-8")
    with pytest.raises(ValueError, match="output-headers"):
        execute_sql(asset, reader=SqliteReader(), output="out.csv", workdir=tmp_path)
    assert not (tmp_path / "out.csv").exists()
