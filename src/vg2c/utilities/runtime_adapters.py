"""Compiler metadata for runtime-backed utility implementations."""

from __future__ import annotations

from vg2c.runtime.crosstab import _CrosstabUtility as RuntimeCrosstabUtility
from vg2c.runtime.duckdb_reader import _DuckDbReader as RuntimeDuckDbReader
from vg2c.runtime.oracle_client import _OracleClient as RuntimeOracleClient
from vg2c.runtime.sqlite_reader import _SqliteReader as RuntimeSqliteReader
from vg2c.utilities._base import UtilitySpec


class CrosstabUtility(RuntimeCrosstabUtility, UtilitySpec):
    utility_name = "crosstab"


class DuckDbReader(RuntimeDuckDbReader, UtilitySpec):
    utility_name = "duckdb_reader"


class OracleClient(RuntimeOracleClient, UtilitySpec):
    utility_name = "oracle_client"


class SqliteReader(RuntimeSqliteReader, UtilitySpec):
    utility_name = "sqlite_reader"
