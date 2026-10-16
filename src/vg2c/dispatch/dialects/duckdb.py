from __future__ import annotations

from vg2c.dispatch.base import DialectHandler
from vg2c.dispatch.models import ReaderSpec
from vg2c.kind import Kind


class DuckDbDialect(DialectHandler):
    """Handler for local DuckDB queries."""

    reader = ReaderSpec(
        module="vg2c.utilities.duckdb_reader",
        name="DuckDbReader",
        utility_name="duckdb_reader",
    )
    kind = Kind.DUCKDB_QUERY

    @classmethod
    def matches_signals(cls, node: str, engine: str, oledb: str) -> bool:
        return engine.upper() == "DUCKDB" or oledb.upper() == "DUCKDB"

    @classmethod
    def substitute(cls, body: str) -> str:
        return body
