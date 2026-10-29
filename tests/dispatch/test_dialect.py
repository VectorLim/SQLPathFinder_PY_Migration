from __future__ import annotations

import pytest

from vg2c.dispatch.base import DialectHandler
from vg2c.dispatch.dialects import (
    AriesDialect,
    DuckDbDialect,
    MarsDialect,
    OasysDialect,
    SqliteDialect,
)


@pytest.mark.parametrize(
    "node, engine, oledb, expected",
    [
        ("ARIES_PROD", "VA", "", AriesDialect),
        ("DUCKDB:data.tab:t0", "DuckDB", "DuckDB", DuckDbDialect),
        ("SITE.MARS", "", "SQLPLUS", MarsDialect),
        ("<<<MARS>>>", "VA", "", MarsDialect),
        ("OASYS_DB", "VA", "", OasysDialect),
        (".\\", "SQLite", "SQLite", SqliteDialect),
        ("", "", "", None),
        ("ARIES_PROD", "", "", None),
    ],
)
def test_signals_select_dialect(node, engine, oledb, expected) -> None:
    assert DialectHandler.derive_handler_from_signals(node, engine, oledb) is expected


def test_substitution_is_identity_unless_overridden() -> None:
    assert SqliteDialect.substitute("select @[]@x") == "select @[]@x"
    assert MarsDialect.substitute("from @[]@tbl") == "from @[]@.tbl"
    assert OasysDialect.substitute("from @OASYSSCHEMA@tbl") == "from tbl"


def test_local_readers_share_runtime_adapter_module() -> None:
    assert DuckDbDialect.reader.module == "vg2c.utilities.runtime_adapters"
    assert DuckDbDialect.reader.utility_name == "duckdb_reader"
    assert SqliteDialect.reader.module == "vg2c.utilities.runtime_adapters"
    assert SqliteDialect.reader.utility_name == "sqlite_reader"
