"""Compiler metadata for the single-sourced DuckDB runtime implementation."""

from __future__ import annotations

from vg2c.runtime.duckdb_reader import _DuckDbReader as RuntimeDuckDbReader
from vg2c.utilities._base import UtilitySpec


class DuckDbReader(RuntimeDuckDbReader, UtilitySpec):
    utility_name = "duckdb_reader"
