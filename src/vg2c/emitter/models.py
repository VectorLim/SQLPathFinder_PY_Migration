from __future__ import annotations

from dataclasses import dataclass, field
from typing import Any

from vg2c.frontend.models import Diagnostic

__all__ = ["EmitContext", "EmittedFunction", "EmittedScript", "IndentWriter"]


@dataclass
class EmittedFunction:
    """One helper function emitted for a step in the VG2 script."""

    name: str
    source: str
    call_site: str


@dataclass
class EmitContext:
    """Mutable state during emission."""

    indent_depth: int = 0
    imports: set[str] = field(default_factory=set)
    dispatch_map: dict[int, Any] = field(
        default_factory=dict
    )  # block_index -> DispatchedBlock
    registry: Any | None = None  # HandlerRegistry instance
    needs_reader: bool = False  # set by reader handlers to inject reader snippet
    needed_utilities: set[str] = field(default_factory=set)  # utility keys to embed

    def add_import(self, module: str, name: str | None = None) -> None:
        """Register an import statement.

        Args:
            module: The module name (e.g., 'pathlib' or 'datasyncx')
            name: Optional name to import. If None, does `import module`.
                  If provided, does `from module import name`.
        """
        if name:
            self.imports.add(f"from {module} import {name}")
        else:
            self.imports.add(f"import {module}")


@dataclass(frozen=True, slots=True)
class EmittedScript:
    """Stage 5 final output."""

    source: str
    imports: tuple[str, ...]
    diagnostics: tuple[Diagnostic, ...]


class IndentWriter:
    """Simple indentation helper for code generation."""

    def __init__(self, indent_step: int = 4):
        self.lines: list[str] = []
        self.indent_depth: int = 0
        self.indent_step = indent_step

    def push_indent(self) -> None:
        self.indent_depth += 1

    def pop_indent(self) -> None:
        self.indent_depth = max(0, self.indent_depth - 1)

    def write(self, line: str) -> None:
        """Write a single line with current indentation."""
        if line.strip():
            self.lines.append(" " * (self.indent_depth * self.indent_step) + line)
        else:
            self.lines.append("")

    def write_block(self, lines: str) -> None:
        """Write multiple lines."""
        for line in lines.split("\n"):
            self.write(line)

    def source(self) -> str:
        """Get the full source text."""
        return "\n".join(self.lines)
