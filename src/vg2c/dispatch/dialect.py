from __future__ import annotations

from vg2c.dispatch.models import Dialect
from vg2c.frontend.models import Kind

_KIND_TO_DIALECT: dict[Kind, Dialect] = {
    Kind.MARS_READ: "oracle_mars",
    Kind.OASYS_READ: "oracle_oasys",
    Kind.ARIES_READ: "oracle_aries",
    Kind.SQLITE_QUERY: "sqlite",
}

SQL_BEARING_KINDS: frozenset[Kind] = frozenset(_KIND_TO_DIALECT.keys())


def resolve_dialect(kind: Kind) -> Dialect | None:
    """Primary dialect resolution by Kind. Returns None for non-SQL-bearing kinds."""
    return _KIND_TO_DIALECT.get(kind)


def derive_from_signals(node: str, engine: str, oledb: str) -> Dialect | None:
    """Fallback dialect derivation from raw option signals, for UNKNOWN blocks.

    Used only when Kind is UNKNOWN but options carry recognisable dialect markers.
    Returns None when no conclusive signal is present — caller must skip the block.
    """
    node_u = node.upper()
    engine_u = engine.upper()
    oledb_u = oledb.upper()

    if engine_u == "SQLITE" or oledb_u == "SQLITE":
        return "sqlite"

    if engine_u == "VA" or oledb_u == "SQLPLUS":
        if "OASYS" in node_u:
            return "oracle_oasys"
        if "ARIES" in node_u:
            return "oracle_aries"
        # MARS: plain .MARS suffix or <<<MARS>>> placeholder
        if node_u.endswith(".MARS") or "<<<MARS>>>" in node_u:
            return "oracle_mars"

    return None
