from __future__ import annotations

from dataclasses import dataclass
from enum import StrEnum
from pathlib import Path


class CommandKind(StrEnum):
    """Execution shape of a resolved VG2 command."""

    UTILITY = "utility"
    QUERY = "query"
    IF = "if"
    MACRO = "macro"
    FOR_LOOP = "for_loop"
    SITE_LOOP = "site_loop"
    RUN_LOOP = "run_loop"
    SCOPE = "scope"
    MARKER = "marker"


@dataclass(frozen=True, slots=True)
class SourceSpan:
    file: Path | None
    start_line: int
    end_line: int

    @property
    def location(self) -> str:
        return f"{self.file or '<input>'}:{self.start_line}:1"


@dataclass(frozen=True, slots=True)
class Command:
    """Authoritative immutable command node used by the direct runtime."""

    index: int
    kind: CommandKind
    command_type: str
    utility_type: str | None
    options: tuple[tuple[str, str], ...]
    body: str
    raw: str
    arguments: tuple[str, ...]
    span: SourceSpan
    children: tuple[Command, ...] = ()
    else_children: tuple[Command, ...] = ()

    def option(self, name: str, default: str | None = None) -> str | None:
        key = name.removeprefix("/").upper()
        for option_name, value in reversed(self.options):
            if option_name == key:
                return value
        return default
