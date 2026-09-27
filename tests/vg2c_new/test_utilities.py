from __future__ import annotations

import sqlite3
from pathlib import Path
from unittest.mock import Mock

import pandas as pd
import pytest

from vg2c_new.model import Command, CommandKind, SourceSpan
from vg2c_new.runtime import RuntimeState
from vg2c_new.utilities.csv import CsvUtility
from vg2c_new.utilities.email import EmailUtility
from vg2c_new.utilities.file_values import UpdateTimeUtility
from vg2c_new.utilities.query import GetSiteTimeUtility, OracleQueryUtility
from vg2c_new.utilities.sqlite import SqliteLoadUtility, SqliteQueryUtility


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


def test_update_time_reference(tmp_path: Path) -> None:
    runtime = state(tmp_path)
    runtime.set_global("SPF_SITE_TIME", "2026-09-26 12:00:00")
    UpdateTimeUtility().apply(command("update_time", ("time.csv",)), runtime)
    assert (
        CsvUtility.read_dataframe(tmp_path / "time.csv").loc[0, "Last_Date-15m"]
        == "2026-09-26 11:45:00"
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


def test_get_site_time_and_email_reference(tmp_path: Path) -> None:
    mars = Mock()
    mars.read.return_value = pd.DataFrame({"last_update_date": ["2026-09-26 12:34:56"]})
    runtime = state(tmp_path)
    GetSiteTimeUtility(mars_reader=mars).apply(command("get_site_time", ("KM.MARS",)), runtime)
    assert runtime.lookup("SPF_SITE_TIME") == "2026-09-26 12:34:56"
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
