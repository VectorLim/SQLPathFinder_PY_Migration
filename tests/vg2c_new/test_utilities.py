from __future__ import annotations

import sqlite3
from pathlib import Path
from unittest.mock import Mock

import pytest

from vg2c_new.model import Command, CommandKind, SourceSpan
from vg2c_new.runtime import RuntimeState
from vg2c_new.utilities.csv import CsvUtility
from vg2c_new.utilities.email import EmailUtility
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


def test_email_reference(tmp_path: Path) -> None:
    runtime = state(tmp_path)
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
