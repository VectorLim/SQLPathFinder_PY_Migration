"""SQLite multi-statement failures must not silently become successful job output.

A failing setup statement cannot be ignored merely because a later SELECT succeeds.
These are runtime correctness tests, not an original ScriptHost engine differential.
"""

from __future__ import annotations

import pytest

from vg2c.runtime import JobRuntime, SqliteReader


@pytest.mark.parametrize(
    "setup",
    [
        "INSERT INTO missing_table VALUES ('lost')",
        "CREATE TABL broken (value TEXT)",
    ],
)
def test_preceding_sqlite_statement_failure_stops_job(tmp_path, setup):
    project = tmp_path / "project"
    work = tmp_path / "output"
    project.mkdir()
    work.mkdir()
    sql = project / "query.sql"
    sql.write_text(f"{setup}; SELECT 'success' AS status;", encoding="utf-8")

    job = JobRuntime(project, work)
    with pytest.raises(RuntimeError, match="SQLite error"):
        job.sql("query.sql", reader=SqliteReader(), output="result.csv")

    assert not (work / "result.csv").exists()


def test_valid_multi_statement_sqlite_setup_remains_supported(tmp_path):
    project = tmp_path / "project"
    project.mkdir()
    (project / "query.sql").write_text(
        "CREATE TABLE scratch (value TEXT); "
        "INSERT INTO scratch VALUES ('ok'); "
        "SELECT value FROM scratch;",
        encoding="utf-8",
    )
    job = JobRuntime(project, tmp_path / "work")
    job.sql("query.sql", reader=SqliteReader(), output="result.csv")
    assert (tmp_path / "work" / "result.csv").read_text(encoding="utf-8") == "value\\nok\\n".replace("\\n", "\n")
