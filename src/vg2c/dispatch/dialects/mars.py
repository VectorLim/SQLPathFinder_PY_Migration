from __future__ import annotations

import re

from vg2c.dispatch.base import DialectHandler
from vg2c.dispatch.models import ReaderSpec
from vg2c.kind import Kind


class MarsDialect(DialectHandler):
    """Handler for Oracle MARS dialect."""

    reader = ReaderSpec(module="datasyncx", name="MarsReader")
    kind = Kind.SQL_QUERY

    _MARS_MISSING_DOT_PATTERN = re.compile(r"@\[\]@(?=[A-Za-z_])")

    @classmethod
    def matches_signals(cls, node: str, engine: str, oledb: str) -> bool:
        node_u = node.upper()
        engine_u = engine.upper()
        oledb_u = oledb.upper()
        return (engine_u == "VA" or oledb_u == "SQLPLUS") and (
            node_u.endswith(".MARS") or "<<<MARS>>>" in node_u
        )

    @classmethod
    def substitute(cls, body: str) -> str:
        return cls._MARS_MISSING_DOT_PATTERN.sub("@[]@.", body)
