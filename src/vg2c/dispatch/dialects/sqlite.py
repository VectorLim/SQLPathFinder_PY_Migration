from __future__ import annotations

from vg2c.dispatch.base import DialectHandler
from vg2c.dispatch.models import ReaderSpec
from vg2c.kind import Kind


class SqliteDialect(DialectHandler):
    """Handler for SQLite dialect."""

    reader = ReaderSpec(
        module="vg2c.utilities.sqlite_reader",
        name="SqliteReader",
        utility_name="sqlite_reader",
    )
    kind = Kind.SQLITE_QUERY

    @classmethod
    def matches_signals(cls, node: str, engine: str, oledb: str) -> bool:
        return engine.upper() == "SQLITE" or oledb.upper() == "SQLITE"

    @classmethod
    def substitute(cls, body: str) -> str:
        return body
