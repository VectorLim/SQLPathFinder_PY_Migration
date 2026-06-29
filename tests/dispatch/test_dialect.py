from __future__ import annotations

import pytest

from vg2c.dispatch.dialect import (
    SQL_BEARING_KINDS,
    derive_from_signals,
    resolve_dialect,
)
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
    assert resolve_dialect(kind) == expected


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
    assert resolve_dialect(kind) is None


def test_sql_bearing_kinds_coverage() -> None:
    assert Kind.SQL_QUERY in SQL_BEARING_KINDS
    assert Kind.SQLITE_QUERY in SQL_BEARING_KINDS
    assert Kind.WRITE_FILE not in SQL_BEARING_KINDS


# --- derive_from_signals fallback ---


def test_derive_from_signals_oasys_by_node() -> None:
    assert (
        derive_from_signals(node="KM.OASYS", engine="VA", oledb="SQLPlus")
        == "oracle_oasys"
    )


def test_derive_from_signals_mars_by_node_suffix() -> None:
    assert (
        derive_from_signals(node="KM.[A15_PROD_21.].MARS", engine="VA", oledb="SQLPlus")
        == "oracle_mars"
    )


def test_derive_from_signals_mars_placeholder_node() -> None:
    assert (
        derive_from_signals(node="<<<MARS>>>", engine="VA", oledb="SQLPlus")
        == "oracle_mars"
    )


def test_derive_from_signals_aries_by_node() -> None:
    assert (
        derive_from_signals(node="<<<ARIES>>>", engine="VA", oledb="SQLPlus")
        == "oracle_aries"
    )


def test_derive_from_signals_sqlite_by_engine() -> None:
    assert derive_from_signals(node=".\\", engine="SQLite", oledb="SQLite") == "sqlite"


def test_derive_from_signals_no_signals_returns_none() -> None:
    assert derive_from_signals(node="", engine="", oledb="") is None
