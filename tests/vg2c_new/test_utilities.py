from __future__ import annotations

import sqlite3
import zipfile
from pathlib import Path
from unittest.mock import Mock

import pandas as pd
import pytest

from vg2c_new.model import Command, CommandKind, SourceSpan
from vg2c_new.runtime import RuntimeState
from vg2c_new.utilities.csv import CsvUtility
from vg2c_new.utilities.email import EmailUtility
from vg2c_new.utilities.excel import ImportExcelUtility, LoadExcelUtility
from vg2c_new.utilities.file_values import (
    FileCompareUtility,
    GetFilesUtility,
    RowsInFileUtility,
    UpdateTimeUtility,
    ValueInFileUtility,
)
from vg2c_new.utilities.files import (
    AppendFileUtility,
    CopyFileUtility,
    RenameFileUtility,
    UnzipUtility,
    WaitFileUtility,
    ZipUtility,
)
from vg2c_new.utilities.process import PyScriptUtility, RunPythonUtility
from vg2c_new.utilities.query import GetSiteTimeUtility, OracleQueryUtility
from vg2c_new.utilities.smart_append import SmartAppendUtility
from vg2c_new.utilities.sqlite import SqliteLoadUtility, SqliteQueryUtility
from vg2c_new.utilities.web import GetWebTextUtility


def command(
    target: str,
    args: tuple[str, ...] = (),
    *,
    options: tuple[tuple[str, str], ...] = (),
    body: str = "",
) -> Command:
    kind = CommandKind.QUERY if target.startswith("query.") else CommandKind.UTILITY
    return Command(1, kind, "TEST", target, options, body, "", args, SourceSpan(None, 1, 1))


def state(tmp_path: Path) -> RuntimeState:
    return RuntimeState(tmp_path)


def test_csv_list_rows_and_values(tmp_path: Path) -> None:
    path = tmp_path / "x.csv"
    path.write_text("id,name\n1,O'Brien\n2,B\n1,O'Brien\n", encoding="utf-8")
    assert CsvUtility.row_count(path) == 3
    assert CsvUtility.sql_get_csv_list(path, "name", "name IN") == "('O''Brien', 'B')"
    runtime = state(tmp_path)
    RowsInFileUtility().apply(command("rows_in_file", ("x.csv", "ROWS", "N", "")), runtime)
    ValueInFileUtility().apply(command("value_in_file", ("x.csv", "name", "VALUE")), runtime)
    assert runtime.lookup("ROWS") == "3"
    assert runtime.lookup("VALUE") == "O'Brien"


def test_copy_rename_append_zip_and_unzip(tmp_path: Path) -> None:
    runtime = state(tmp_path)
    (tmp_path / "a.csv").write_text("id\n1\n", encoding="utf-8")
    (tmp_path / "b.csv").write_text("id\n2\n", encoding="utf-8")
    CopyFileUtility().apply(command("copy_file", ("a.csv", "copy.csv")), runtime)
    RenameFileUtility().apply(command("rename_file", ("copy.csv", "renamed.csv")), runtime)
    AppendFileUtility().apply(command("append_file", ("renamed.csv", "b.csv")), runtime)
    assert CsvUtility.row_count(tmp_path / "renamed.csv") == 2
    ZipUtility().apply(command("zip", ("renamed.csv", "archive.zip")), runtime)
    UnzipUtility().apply(command("unzip", ("archive.zip", "out")), runtime)
    assert (tmp_path / "out" / "renamed.csv").exists()


def test_unzip_blocks_zip_slip(tmp_path: Path) -> None:
    archive = tmp_path / "bad.zip"
    with zipfile.ZipFile(archive, "w") as handle:
        handle.writestr("../escape.txt", "bad")
    with pytest.raises(RuntimeError, match="Unsafe ZIP member path"):
        UnzipUtility().apply(command("unzip", ("bad.zip", "out")), state(tmp_path))


def test_wait_files_metadata_compare_and_update_time(tmp_path: Path) -> None:
    sleeps: list[int] = []
    WaitFileUtility(sleeper=sleeps.append, poll_seconds=3).apply(
        command("wait_file", ("missing.txt", "3")),
        state(tmp_path),
    )
    assert sleeps == [3, 3]
    (tmp_path / "a.txt").write_text("Hello 2026-01-01 01:02:03", encoding="utf-8")
    (tmp_path / "b.txt").write_text("hello 2026-09-02 03:04:05", encoding="utf-8")
    runtime = state(tmp_path)
    GetFilesUtility().apply(command("get_files", ("*.txt", "files.csv", "N")), runtime)
    assert set(CsvUtility.read_dataframe(tmp_path / "files.csv")["Filename"]) == {"a.txt", "b.txt"}
    FileCompareUtility().apply(
        command("file_compare", ("a.txt", "b.txt", "Y", "Y", "10", "N")),
        runtime,
    )
    runtime.set_global("SPF_SITE_TIME", "2026-09-26 12:00:00")
    UpdateTimeUtility().apply(command("update_time", ("time.csv",)), runtime)
    assert (
        CsvUtility.read_dataframe(tmp_path / "time.csv").loc[0, "Last_Date-15m"]
        == "2026-09-26 11:45:00"
    )


def test_smart_append_v4_semantics_and_old_version_rejection(tmp_path: Path) -> None:
    old = tmp_path / "old.csv"
    new = tmp_path / "new.csv"
    old.write_text(
        "ID,DATE,OLD\n1,2026-01-01 00:00:00,a\n2,2026-09-01 00:00:00,b\n",
        encoding="utf-8",
    )
    new.write_text(
        "ID,DATE,NEW\n2,2026-09-02 00:00:00,x\n3,2026-09-03 00:00:00,y\n",
        encoding="utf-8",
    )
    args = (
        "old.csv",
        "new.csv",
        "DATE",
        "2026-02-01 00:00:00",
        "ID",
        "N",
        "Y",
        "",
        "VERSION4",
        "",
        "missing",
        "",
        "Y",
    )
    SmartAppendUtility().apply(command("smart_append", args), state(tmp_path))
    frame = CsvUtility.read_dataframe(old)
    assert frame["ID"].tolist() == ["2", "3"]
    assert list(frame.columns) == ["ID", "DATE", "OLD", "NEW"]
    assert frame["OLD"].tolist() == ["missing", "missing"]
    with pytest.raises(RuntimeError, match="Historical SmartAppend"):
        SmartAppendUtility().apply(
            command("smart_append", (*args[:8], "VERSION3")),
            state(tmp_path),
        )


def test_sqlite_query_load_reserved_columns_and_visible_errors(tmp_path: Path) -> None:
    (tmp_path / "a.csv").write_text("id,name\n1,A\n2,B\n", encoding="utf-8")
    runtime = state(tmp_path)
    query = command(
        "query.sqlite",
        options=(("TABLE", "a.csv:t"), ("CSV", "out.csv")),
        body="CREATE INDEX ix ON t(id); SELECT name FROM t ORDER BY id;",
    )
    SqliteQueryUtility().apply(query, runtime)
    assert CsvUtility.read_dataframe(tmp_path / "out.csv")["name"].tolist() == ["A", "B"]
    with pytest.raises(sqlite3.Error):
        SqliteQueryUtility().apply(
            command("query.sqlite", options=(("TABLE", "a.csv:t"),), body="CREATE BOGUS THING;"),
            runtime,
        )
    (tmp_path / "bad.csv").write_text("rowid,x\n1,a\n", encoding="utf-8")
    with pytest.raises(ValueError, match="Reserved SQLite"):
        SqliteQueryUtility().apply(
            command("query.sqlite", options=(("TABLE", "bad.csv:t"),), body="select 1"),
            runtime,
        )
    SqliteLoadUtility().apply(
        command("sqlite_load", ("a.csv:t", "db.sqlite", "CREATE INDEX ix ON t(id);", "N")),
        runtime,
    )
    connection = sqlite3.connect(tmp_path / "db.sqlite")
    assert connection.execute("select name from t order by id").fetchall() == [("A",), ("B",)]
    connection.close()


def test_datasyncx_oracle_exact_calls_and_real_node_site(tmp_path: Path) -> None:
    mars = Mock()
    mars.read.return_value = pd.DataFrame({"x": [1]})
    aries = Mock()
    aries.read.return_value = pd.DataFrame({"x": [2]})
    oasys = Mock()
    oasys.read.return_value = pd.DataFrame({"x": [3]})
    utility = OracleQueryUtility(mars_reader=mars, aries_reader=aries, oasys_reader=oasys)
    utility.apply(
        command(
            "query.oracle",
            options=(("NODE", "KM.[A15_PROD_21.].MARS"), ("CSV", "m.csv")),
            body="select @[]@COL from dual",
        ),
        state(tmp_path),
    )
    mars.read.assert_called_once_with(site="KM", query="select @[]@.COL from dual")
    utility.apply(
        command("query.oracle", options=(("NODE", "PG.ARIES"),), body="select 2"),
        state(tmp_path),
    )
    aries.read.assert_called_once_with(site="PG", query="select 2")
    utility.apply(
        command(
            "query.oracle",
            options=(("NODE", "VN.OASYS"),),
            body="select @OASYSSCHEMA@T from dual",
        ),
        state(tmp_path),
    )
    oasys.read.assert_called_once_with(site="VN", query="select T from dual")


def test_get_site_time_web_and_email_injection(tmp_path: Path) -> None:
    mars = Mock()
    mars.read.return_value = pd.DataFrame({"last_update_date": ["2026-09-26 12:34:56"]})
    runtime = state(tmp_path)
    GetSiteTimeUtility(mars_reader=mars).apply(command("get_site_time", ("KM.MARS",)), runtime)
    assert runtime.lookup("SPF_SITE_TIME") == "2026-09-26 12:34:56"
    response = Mock(content=b"abc")
    response.raise_for_status.return_value = None
    session = Mock()
    session.get.return_value = response
    GetWebTextUtility(session).apply(
        command("get_web_text", ("https://x", "x.txt", "VERSION2", "N", "N", "4")),
        runtime,
    )
    assert (tmp_path / "x.txt").read_bytes() == b"abc"
    (tmp_path / "body.txt").write_text("hello", encoding="utf-8")
    send = Mock()
    EmailUtility(send).apply(
        command(
            "email",
            ("a.csv", "a@x;b@y", "sub", "body.txt", "c@x", "d@x", "", "N", "N"),
        ),
        runtime,
    )
    assert send.call_args.kwargs["to"] == ["a@x", "b@y"]
    assert send.call_args.kwargs["body"] == "hello"


def test_excel_and_python_portable_execution(tmp_path: Path) -> None:
    (tmp_path / "in.csv").write_text("id,x\n1,a\n", encoding="utf-8")
    runtime = state(tmp_path)
    LoadExcelUtility().apply(
        command("load_excel", ("in.csv", "out.xlsx", "Version 1", "N")),
        runtime,
    )
    ImportExcelUtility().apply(
        command(
            "import_excel",
            ("out.xlsx", "out2.xlsx", "in.csv", "Data", "", "", "Version 2", "N"),
        ),
        runtime,
    )
    frame = pd.read_excel(tmp_path / "out2.xlsx", sheet_name="Data", dtype=str)
    assert frame.loc[0, "x"] == "a"
    script = tmp_path / "script.py"
    script.write_text(
        "import pathlib,sys; pathlib.Path('args.txt').write_text('|'.join(sys.argv[1:]))",
        encoding="utf-8",
    )
    RunPythonUtility().apply(
        command("run_python", ("script.py", "--csv x.csv", "N", "host", "Python-v3")),
        runtime,
    )
    assert (tmp_path / "args.txt").read_text() == "--csv x.csv"
    PyScriptUtility().apply(command("pyscript", ("script.py", "a", "b c")), runtime)
    assert (tmp_path / "args.txt").read_text() == "a|b c"
    with pytest.raises(RuntimeError, match="Historical Python selector"):
        RunPythonUtility().apply(
            command("run_python", ("script.py", "", "N", "", "Python-v3-old")),
            runtime,
        )
