"""Generated Python should show operations, not job plumbing or duplicate SQL headers."""

from __future__ import annotations

import runpy
import shutil

import pytest

from vg2c import compile_document, translate
from vg2c.runtime import SqliteReader, execute_sql


def _source(tmp_path, *, headers=None):
    options = "<OPTIONS>\n/ENGINE=SQLite\n/CSV=result.csv\n"
    if headers is not None:
        options += "/HEADERS=" + headers + "\n"
    source = tmp_path / "sample.txt"
    source.write_text(options + "</OPTIONS>\n"
                      + "SELECT '001' AS code, 'Example' AS label\n", encoding="utf-8")
    return source


def test_generated_run_has_only_operations_and_preserves_workdir_override(tmp_path, monkeypatch):
    source = _source(tmp_path)
    main = translate(source)
    text = main.read_text(encoding="utf-8")
    assert "@job_entrypoint(__file__)" in text
    assert "def run(job):" in text
    assert "job.sql(" in text
    assert "workdir = Path(workdir).resolve()" not in text
    assert "job = JobRuntime(" not in text
    assert "BASE_DIR =" not in text
    assert "WORK_DIR =" not in text
    assert "header=" not in text
    assert "inputs=[]" not in text  # Empty TABLE configuration is the runtime default.

    moved = tmp_path / "relocated"
    shutil.move(str(main.parent), str(moved))
    loaded = runpy.run_path(str(moved / "main.py"), run_name="translated")
    workdir = tmp_path / "external"
    loaded["run"](workdir=workdir)
    assert (workdir / "result.csv").read_text() == "code,label\n001,Example\n"

    monkeypatch.chdir(tmp_path)
    loaded["run"]()
    assert (moved / "output" / "result.csv").read_text() == "code,label\n001,Example\n"


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
    workdir = tmp_path / "work"
    run(workdir=workdir)
    assert (workdir / "result.csv").read_text() == "CODE,LABEL\n001,Example\n"

    asset = main.parent / path
    asset.write_text(sql.replace(" AS code", " AS identifier"), encoding="utf-8")
    run(workdir=workdir)
    # Explicit /HEADERS is a name-based projection, not a positional rename.
    assert (workdir / "result.csv").read_text() == "CODE,LABEL\n,Example\n"
    asset.write_text(asset.read_text().split("\n", 1)[1], encoding="utf-8")
    run(workdir=workdir)
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
