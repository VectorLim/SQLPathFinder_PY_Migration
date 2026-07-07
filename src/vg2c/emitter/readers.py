"""Reader runtime injected into emitted pipeline scripts.

To support a new database type, add an entry to ``DATABASE_TYPE_MAP`` in
``ReaderRuntime`` and add the matching ``datasyncx`` reader import.
"""

from __future__ import annotations

from vg2c.emitter.utilities._base import UtilitySpec


from datasyncx.readers import AriesReader, MarsReader, OracleReader


class ReaderRuntime(UtilitySpec):
    utility_name = "reader_runtime"
    DATABASE_TYPE_MAP = {
        "MARS": MarsReader,
        "OASYS": OracleReader,
        "ARIES": AriesReader,
    }

    def read(self, sql, db_type, macro_state=None):
        """Run *sql* against the Reader registered for *db_type*."""
        if macro_state is not None:
            sql = macro_state.substitute_sql(sql)
        if db_type not in self.DATABASE_TYPE_MAP:
            raise ValueError(f"Unsupported database type: {db_type!r}")
        result = self.DATABASE_TYPE_MAP[db_type]().read(site="KM", query=sql)
        result.columns = [col.lower() for col in result.columns]
        return result
