from __future__ import annotations

from vg2c.dispatch.base import DialectHandler
from vg2c.dispatch.models import DispatchConfig
from vg2c.dispatch.registry import register
from vg2c.frontend.models import Diagnostic, Kind, SourceSpan


@register
class AriesDialect(DialectHandler):
    """Handler for Oracle ARIES dialect."""

    dialect = "oracle_aries"
    kind = Kind.ARIES_READ
    reader_class_hint = "OracleReader"
    database_arg = "ARIES"

    @classmethod
    def matches_signals(cls, node: str, engine: str, oledb: str) -> bool:
        node_u = node.upper()
        engine_u = engine.upper()
        oledb_u = oledb.upper()
        return (engine_u == "VA" or oledb_u == "SQLPLUS") and "ARIES" in node_u

    @classmethod
    def substitute(
        cls,
        body: str,
        config: DispatchConfig | None,
        span: SourceSpan | None,
        block_index: int,
    ) -> tuple[str, list[Diagnostic]]:
        # No schema substitution for ARIES
        return body, []
