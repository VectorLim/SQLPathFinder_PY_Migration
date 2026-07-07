from __future__ import annotations

import pytest

from vg2c.dispatch import get_datasyncx_reader_name
from vg2c.dispatch.base import DialectHandler
from vg2c.dispatch.dialects.aries import AriesDialect
from vg2c.dispatch.dialects.mars import MarsDialect
from vg2c.dispatch.dialects.oasys import OasysDialect
from vg2c.dispatch.dialects.sqlite import SqliteDialect
from vg2c.dispatch.models import Dialect
from vg2c.frontend.models import Kind


@pytest.mark.parametrize(
    "kind, expected",
    [
        (Kind.SQL_QUERY, None),
        (Kind.SQLITE_QUERY, "sqlite"),
    ],
)
def test_resolve_dialect_sql_bearing(kind: Kind, expected: Dialect | None) -> None:
    assert DialectHandler.resolve_dialect(kind) == expected


@pytest.mark.parametrize(
    "kind",
    [
        Kind.WRITE_FILE,
        Kind.UTILITY,
        Kind.MACRO_CONTROL,
        Kind.HTML_REPORT,
        Kind.UNKNOWN,
        Kind.MALFORMED,
    ],
)
def test_resolve_dialect_non_sql_returns_none(kind: Kind) -> None:
    assert DialectHandler.resolve_dialect(kind) is None


def test_dialect_handlers_auto_register() -> None:
    assert DialectHandler.for_dialect("oracle_aries") is AriesDialect
    assert DialectHandler.for_dialect("oracle_mars") is MarsDialect
    assert DialectHandler.for_dialect("oracle_oasys") is OasysDialect
    assert DialectHandler.for_dialect("sqlite") is SqliteDialect
    assert DialectHandler.for_kind(Kind.SQL_QUERY) is None
    assert DialectHandler.for_kind(Kind.SQLITE_QUERY) is SqliteDialect


def test_datasyncx_reader_metadata_matches_dispatch_helper() -> None:
    assert get_datasyncx_reader_name("MARS") == "MarsReader"
    assert get_datasyncx_reader_name("OASYS") == "OracleReader"
    assert get_datasyncx_reader_name("ARIES") == "AriesReader"
    assert get_datasyncx_reader_name("sqlite") is None


def test_sql_bearing_kinds_coverage() -> None:
    sql_bearing_kinds = DialectHandler.sql_bearing_kinds()
    assert Kind.SQL_QUERY in sql_bearing_kinds
    assert Kind.SQLITE_QUERY in sql_bearing_kinds
    assert Kind.WRITE_FILE not in sql_bearing_kinds


# --- derive_from_signals fallback ---


def test_derive_from_signals_oasys_by_node() -> None:
    assert (
        DialectHandler.derive_from_signals(
            node="KM.OASYS", engine="VA", oledb="SQLPlus"
        )
        == "oracle_oasys"
    )


def test_derive_from_signals_mars_by_node_suffix() -> None:
    assert (
        DialectHandler.derive_from_signals(
            node="KM.[A15_PROD_21.].MARS",
            engine="VA",
            oledb="SQLPlus",
        )
        == "oracle_mars"
    )


def test_derive_from_signals_mars_placeholder_node() -> None:
    assert (
        DialectHandler.derive_from_signals(
            node="<<<MARS>>>", engine="VA", oledb="SQLPlus"
        )
        == "oracle_mars"
    )


def test_derive_from_signals_aries_by_node() -> None:
    assert (
        DialectHandler.derive_from_signals(
            node="<<<ARIES>>>", engine="VA", oledb="SQLPlus"
        )
        == "oracle_aries"
    )


def test_derive_from_signals_sqlite_by_engine() -> None:
    assert (
        DialectHandler.derive_from_signals(
            node=".\\", engine="SQLite", oledb="SQLite"
        )
        == "sqlite"
    )


def test_derive_from_signals_no_signals_returns_none() -> None:
    assert DialectHandler.derive_from_signals(node="", engine="", oledb="") is None
