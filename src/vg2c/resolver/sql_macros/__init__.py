"""SQL macro handler registry.

To add a new macro handler:
1. Create a new module in this package (e.g. sql_foo.py).
2. Import the handler class below.
3. Add an instance to the HANDLERS tuple.
"""

from __future__ import annotations

from vg2c.resolver.sql_macros.base import (
    MacroExpansion,
    MacroParseError,
    SqlMacroHandler,
)
from vg2c.resolver.sql_macros.sql_get_csv_list import SqlGetCsvListHandler

HANDLERS: dict[str, SqlMacroHandler] = {h.name: h for h in (SqlGetCsvListHandler(),)}

__all__ = [
    "HANDLERS",
    "MacroExpansion",
    "MacroParseError",
    "SqlMacroHandler",
]
