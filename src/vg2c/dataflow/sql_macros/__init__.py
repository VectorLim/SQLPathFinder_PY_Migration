"""SQL macro handler registry for dataflow-owned SQL macro expansion."""

from __future__ import annotations

from vg2c.dataflow.sql_macros.base import (
    MacroExpansion,
    MacroParseError,
    SqlMacroHandler,
)
from vg2c.dataflow.sql_macros.sql_get_csv_list import SqlGetCsvListHandler

HANDLERS: dict[str, SqlMacroHandler] = {h.name: h for h in (SqlGetCsvListHandler(),)}

__all__ = [
    "HANDLERS",
    "MacroExpansion",
    "MacroParseError",
    "SqlMacroHandler",
]
