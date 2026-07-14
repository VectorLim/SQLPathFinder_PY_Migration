from __future__ import annotations

import pytest

from vg2c.dispatch.base import DialectHandler
from vg2c.dispatch.dialects.aries import AriesDialect
from vg2c.dispatch.dialects.mars import MarsDialect
from vg2c.dispatch.dialects.oasys import OasysDialect
from vg2c.dispatch.dialects.sqlite import SqliteDialect, SqliteReader
from vg2c.kind import Kind


@pytest.mark.parametrize(
    "kind, expected",
    [
        (Kind.SQL_QUERY, None),
        (Kind.SQLITE_QUERY, SqliteReader),
    ],
)
def test_resolve_reader_cls_sql_bearing(kind: Kind, expected: type | None) -> None:
    assert DialectHandler.resolve_reader_cls(kind) is expected


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
def test_resolve_reader_cls_non_sql_returns_none(kind: Kind) -> None:
    assert DialectHandler.resolve_reader_cls(kind) is None


def test_resolve_reader_cls_for_sqlite_kind() -> None:
    assert DialectHandler.resolve_reader_cls(Kind.SQLITE_QUERY) is SqliteReader


def test_sql_bearing_kinds_coverage() -> None:
    sql_bearing_kinds = DialectHandler.sql_bearing_kinds()
    assert Kind.SQL_QUERY in sql_bearing_kinds
    assert Kind.SQLITE_QUERY in sql_bearing_kinds
    assert Kind.WRITE_FILE not in sql_bearing_kinds


# --- derive_from_signals fallback ---


def test_derive_from_signals_sqlite_by_engine() -> None:
    assert (
        DialectHandler.derive_reader_cls_from_signals(
            node=".\\", engine="SQLite", oledb="SQLite"
        )
        is SqliteReader
    )


def test_derive_from_signals_no_signals_returns_none() -> None:
    assert (
        DialectHandler.derive_reader_cls_from_signals(node="", engine="", oledb="")
        is None
    )
