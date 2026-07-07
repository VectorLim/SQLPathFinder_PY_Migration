from __future__ import annotations

from vg2c.dispatch.base import DialectHandler
from vg2c.dispatch.models import DispatchConfig
from vg2c.frontend.models import Diagnostic, Kind, SourceSpan


class SqliteDialect(DialectHandler):
    """Handler for SQLite dialect."""

    dialect = "sqlite"
    kind = Kind.SQLITE_QUERY
    database_arg = None

    @classmethod
    def matches_signals(cls, node: str, engine: str, oledb: str) -> bool:
        return engine.upper() == "SQLITE" or oledb.upper() == "SQLITE"

    @classmethod
    def substitute(
        cls,
        body: str,
        config: DispatchConfig | None,
        span: SourceSpan | None,
        block_index: int,
    ) -> tuple[str, list[Diagnostic]]:
        # No schema substitution for SQLite
        return body, []
