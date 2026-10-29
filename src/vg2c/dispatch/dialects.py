from __future__ import annotations

import re

from vg2c.dispatch.base import DialectHandler
from vg2c.dispatch.models import ReaderSpec
from vg2c.kind import Kind


def _uses_va_or_sqlplus(engine: str, oledb: str) -> bool:
    return engine.upper() == "VA" or oledb.upper() == "SQLPLUS"


class AriesDialect(DialectHandler):
    """Handler for Oracle ARIES dialect."""

    reader = ReaderSpec(module="datasyncx", name="AriesReader")
    kind = Kind.SQL_QUERY

    @classmethod
    def matches_signals(cls, node: str, engine: str, oledb: str) -> bool:
        return _uses_va_or_sqlplus(engine, oledb) and "ARIES" in node.upper()


class DuckDbDialect(DialectHandler):
    """Handler for local DuckDB queries."""

    reader = ReaderSpec(
        module="vg2c.utilities.runtime_adapters",
        name="DuckDbReader",
        utility_name="duckdb_reader",
    )
    kind = Kind.DUCKDB_QUERY

    @classmethod
    def matches_signals(cls, node: str, engine: str, oledb: str) -> bool:
        return engine.upper() == "DUCKDB" or oledb.upper() == "DUCKDB"


class MarsDialect(DialectHandler):
    """Handler for Oracle MARS dialect."""

    reader = ReaderSpec(module="datasyncx", name="MarsReader")
    kind = Kind.SQL_QUERY

    _MARS_MISSING_DOT_PATTERN = re.compile(r"@\[\]@(?=[A-Za-z_])")

    @classmethod
    def matches_signals(cls, node: str, engine: str, oledb: str) -> bool:
        node_u = node.upper()
        return _uses_va_or_sqlplus(engine, oledb) and (
            node_u.endswith(".MARS") or "<<<MARS>>>" in node_u
        )

    @classmethod
    def substitute(cls, body: str) -> str:
        return cls._MARS_MISSING_DOT_PATTERN.sub("@[]@.", body)


class OasysDialect(DialectHandler):
    """Handler for Oracle OASYS dialect."""

    reader = ReaderSpec(module="datasyncx", name="OracleReader")
    reader_kwargs = {"database": "OASYS"}
    kind = Kind.SQL_QUERY

    _OASYS_PLACEHOLDER = "@OASYSSCHEMA@"

    @classmethod
    def matches_signals(cls, node: str, engine: str, oledb: str) -> bool:
        return _uses_va_or_sqlplus(engine, oledb) and "OASYS" in node.upper()

    @classmethod
    def substitute(cls, body: str) -> str:
        return body.replace(cls._OASYS_PLACEHOLDER, "")


class SqliteDialect(DialectHandler):
    """Handler for SQLite dialect."""

    reader = ReaderSpec(
        module="vg2c.utilities.runtime_adapters",
        name="SqliteReader",
        utility_name="sqlite_reader",
    )
    kind = Kind.SQLITE_QUERY

    @classmethod
    def matches_signals(cls, node: str, engine: str, oledb: str) -> bool:
        return engine.upper() == "SQLITE" or oledb.upper() == "SQLITE"


__all__ = ["AriesDialect", "DuckDbDialect", "MarsDialect", "OasysDialect", "SqliteDialect"]