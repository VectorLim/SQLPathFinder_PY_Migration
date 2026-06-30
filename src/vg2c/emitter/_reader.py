"""Reader runtime injected into emitted pipeline scripts.

The code between the ``BEGIN`` / ``END`` sentinel comments below is read as
source text by :mod:`vg2c.emitter.readers` and embedded verbatim into every
generated VG2 script that performs a SQL read.

The runtime relies on ``macro_state.substitute_sql(sql)`` (provided by
:class:`vg2c.emitter.macro.MacroState`) so SQL placeholder substitution
stays owned by the macro subsystem.

To support a new database type, add an entry to ``DATABASE_TYPE_MAP`` below
and add the matching ``datasyncx`` Reader import alongside the others below.
"""

from __future__ import annotations

# --- Embedded reader runtime ------------------------------------------------
from datasyncx.readers import AriesReader, MarsReader, OracleReader

# DATABASE_TYPE_MAP is the single extension point for adding a new database
# type: map the /ENGINE= identifier used in the VG2 source to a datasyncx
# Reader subclass. ``read`` below dispatches to it.
DATABASE_TYPE_MAP = {
    "MARS": MarsReader,
    "OASYS": OracleReader,
    "ARIES": AriesReader,
}


def read(sql, db_type, macro_state=None):
    """Run *sql* against the Reader registered for *db_type*.

    ``macro_state`` (when given) substitutes ``<<<NAME>>>`` macro
    placeholders that survive into the SQL body via its own
    ``substitute_sql`` helper.
    """
    if macro_state is not None:
        sql = macro_state.substitute_sql(sql)
    if db_type not in DATABASE_TYPE_MAP:
        raise ValueError(f"Unsupported database type: {db_type!r}")
    result = DATABASE_TYPE_MAP[db_type]().read(site="KM", query=sql)
    result.columns = [col.lower() for col in result.columns]

    return result


# --- end embedded reader runtime --------------------------------------------
