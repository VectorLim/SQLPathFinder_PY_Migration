"""Base classes and shared utilities for SQL macro handlers.

To add support for a new SQL macro (e.g. SQL_Time_Range):
1. Create a new file sql_time_range.py in this package.
2. Subclass SqlMacroHandler, set name = "SQL_Time_Range".
3. Implement build_call() to parse arguments and return MacroExpansion or MacroParseError.
4. In sql_macros/__init__.py, import your handler class and add an instance to the HANDLERS tuple.

No edits to dataflow/sql_macro_expander.py are required.
"""

from __future__ import annotations

from abc import ABC, abstractmethod
from dataclasses import dataclass

from vg2c.frontend.models import SourceSpan
from vg2c.resolver.models import SqlMacroCall


@dataclass(frozen=True, slots=True)
class MacroExpansion:
    """Successful parse result from a SQL macro handler."""

    call: SqlMacroCall
    appended_text: str = ""


@dataclass(frozen=True, slots=True)
class MacroParseError:
    """Parse failure from a SQL macro handler."""

    message: str


class SqlMacroHandler(ABC):
    """Base class for SQL macro handlers.

    Each handler is responsible for parsing SQL macro call arguments into a
    structured SqlMacroCall.
    """

    name: str

    @abstractmethod
    def build_call(
        self,
        args: list[str],
        span: SourceSpan,
        before_text: str,
    ) -> MacroExpansion | MacroParseError:
        """Parse macro arguments and return expansion metadata.

        Args:
            args: List of argument strings extracted from the SQL call.
            span: Source location of the containing block.
            before_text: Text preceding the macro call site (for context-sensitive parsing).

        Returns:
            MacroExpansion on success, MacroParseError on failure.
        """
        ...


def unquote_arg(value: str) -> str:
    """Remove surrounding quotes from a SQL argument if present."""
    stripped = value.strip()
    if len(stripped) >= 2 and stripped[0] == stripped[-1] and stripped[0] in {'"', "'"}:
        return stripped[1:-1]
    return stripped


def parse_column_ref(raw: str) -> int | str:
    """Parse a column reference as either an integer index or a string name."""
    value = unquote_arg(raw)
    return int(value) if value.isdigit() else value
