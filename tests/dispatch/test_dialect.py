from __future__ import annotations

import pytest

from vg2c.dispatch.base import DialectHandler
from vg2c.dispatch.models import ReaderSpec
from vg2c.kind import Kind


@pytest.mark.parametrize(
    "kind, expected",
    [
        (Kind.SQL_QUERY, None),
        (
            Kind.SQLITE_QUERY,
            ReaderSpec(
                module="vg2c.utilities.sqlite_reader",
                name="SqliteReader",
                utility_name="sqlite_reader",
            ),
        ),
    ],
)
def test_resolve_reader_sql_bearing(kind: Kind, expected: ReaderSpec | None) -> None:
    assert DialectHandler.resolve_reader(kind) == expected


@pytest.mark.parametrize(
    "kind",
    [
        Kind.WRITE_FILE,
        Kind.EMAIL,
        Kind.MACRO_CONTROL,
        Kind.HTML_REPORT,
        Kind.UNKNOWN,
    ],
)
def test_resolve_reader_non_sql_returns_none(kind: Kind) -> None:
    assert DialectHandler.resolve_reader(kind) is None


def test_sql_bearing_kinds_coverage() -> None:
    sql_bearing_kinds = DialectHandler.sql_bearing_kinds()
    assert Kind.SQL_QUERY in sql_bearing_kinds
    assert Kind.SQLITE_QUERY in sql_bearing_kinds
    assert Kind.WRITE_FILE not in sql_bearing_kinds


def test_derive_from_signals_sqlite_by_engine() -> None:
    reader = DialectHandler.derive_reader_from_signals(
        node=".\\", engine="SQLite", oledb="SQLite"
    )
    assert reader is not None
    assert reader.name == "SqliteReader"
    assert reader.utility_name == "sqlite_reader"


def test_derive_from_signals_no_signals_returns_none() -> None:
    assert DialectHandler.derive_reader_from_signals(node="", engine="", oledb="") is None
