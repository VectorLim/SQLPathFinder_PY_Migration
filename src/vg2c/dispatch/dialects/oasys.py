from __future__ import annotations

from vg2c.dispatch.base import DialectHandler
from vg2c.dispatch.models import ReaderSpec
from vg2c.kind import Kind


class OasysDialect(DialectHandler):
    """Handler for Oracle OASYS dialect."""

    reader = ReaderSpec(module="datasyncx", name="OracleReader")
    reader_kwargs = {"database": "OASYS"}
    kind = Kind.SQL_QUERY

    _OASYS_PLACEHOLDER = "@OASYSSCHEMA@"

    @classmethod
    def matches_signals(cls, node: str, engine: str, oledb: str) -> bool:
        node_u = node.upper()
        engine_u = engine.upper()
        oledb_u = oledb.upper()
        return (engine_u == "VA" or oledb_u == "SQLPLUS") and "OASYS" in node_u

    @classmethod
    def substitute(cls, body: str) -> str:
        return body.replace(cls._OASYS_PLACEHOLDER, "")
