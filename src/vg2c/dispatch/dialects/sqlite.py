from __future__ import annotations

from vg2c.dispatch.base import DialectHandler
from vg2c.kind import Kind
from vg2c.utilities.sqlite_reader import SqliteReader


class SqliteDialect(DialectHandler):
    """Handler for SQLite dialect."""

    reader_cls = SqliteReader
    kind = Kind.SQLITE_QUERY

    @classmethod
    def matches_signals(cls, node: str, engine: str, oledb: str) -> bool:
        return engine.upper() == "SQLITE" or oledb.upper() == "SQLITE"

    @classmethod
    def substitute(cls, body: str) -> str:
        # No schema substitution for SQLite
        return body
