"""Reader runtime injected into emitted pipeline scripts.

The code between the ``BEGIN`` / ``END`` sentinel comments below is read as
source text by :mod:`vg2c.emitter.readers` and embedded verbatim into every
generated VG2 script that performs a SQL read.

To support a new database type, add an entry to ``DATABASE_TYPE_MAP`` below
and add the matching ``datasyncx`` Reader import alongside the others below.
"""

from __future__ import annotations

# --- Embedded reader runtime ------------------------------------------------
import re

from datasyncx.readers import AriesReader, MarsReader, OracleReader

# DATABASE_TYPE_MAP is the single extension point for adding a new database
# type: map the /ENGINE= identifier used in the VG2 source to a datasyncx
# Reader subclass. ``read`` below dispatches to it.
DATABASE_TYPE_MAP = {
    "MARS": MarsReader,
    "OASYS": OracleReader,
    "ARIES": AriesReader,
}

_MACRO_PLACEHOLDER_RE = re.compile(r"<<<([^>]+)>>>")


def _substitute_sql_macros(sql, macro_state):
    if macro_state is None or "<<<" not in sql:
        return sql
    return _MACRO_PLACEHOLDER_RE.sub(
        lambda m: macro_state.named(m.group(1).strip().upper()),
        sql,
    )


def read(sql, db_type, macro_state=None):
    """Run *sql* against the Reader registered for *db_type*.

    ``macro_state`` (when given) is used to substitute ``<<<NAME>>>`` macro
    placeholders that survive into the SQL body. A fresh Reader instance is
    constructed per call.
    """
    sql = _substitute_sql_macros(sql, macro_state)
    if db_type not in DATABASE_TYPE_MAP:
        raise ValueError(f"Unsupported database type: {db_type!r}")
    return DATABASE_TYPE_MAP[db_type]().read(site="KM", query=sql)


# --- end embedded reader runtime --------------------------------------------
