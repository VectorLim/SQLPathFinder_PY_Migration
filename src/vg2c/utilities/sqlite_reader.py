"""Compiler metadata for the single-sourced runtime implementation."""

from __future__ import annotations
from vg2c.runtime.sqlite_reader import _SqliteReader as RuntimeSqliteReader
from vg2c.utilities._base import UtilitySpec


class SqliteReader(RuntimeSqliteReader, UtilitySpec):
    utility_name = "sqlite_reader"
