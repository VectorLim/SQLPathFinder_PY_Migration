from __future__ import annotations

from dataclasses import dataclass, field
from typing import Any

from vg2c.frontend.models import Diagnostic, Kind

__all__ = ["EmitContext", "EmittedScript", "IndentWriter"]


@dataclass
class EmitContext:
    """Mutable state during emission."""

    indent_depth: int = 0
    imports: set[str] = field(default_factory=set)
    dispatch_map: dict[int, Any] = field(
        default_factory=dict
    )  # block_index -> DispatchedBlock

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

    def render_method_call(
        self,
        utility_name: str,
        method_name: str,
        *,
        args: tuple[Any, ...] = (),
        kwargs: dict[str, Any] | None = None,
    ) -> str:
        """Render a Python method-call expression for the generated script.

        Args:
            utility_name: The utility sub-object to call on (e.g. ``'csv_io'``,
                ``'fs_ops'``).  Pass ``'ctx'`` to address the context object itself.
            method_name: The method to invoke on that object.
            args: Positional argument values (raw Python values or
                :class:`~vg2c.emitter.utilities._emit_helpers.RawExpr` instances).
            kwargs: Keyword argument values (same accepted types as *args*).

        Returns:
            A string such as ``"ctx.csv_io.iter('file.csv')"``.
        """
        from vg2c.emitter.utilities._emit_helpers import _render_value

        receiver = "ctx" if utility_name == "ctx" else f"ctx.{utility_name}"
        parts: list[str] = [_render_value(arg) for arg in args]
        for key, value in (kwargs or {}).items():
            parts.append(f"{key}={_render_value(value)}")
        return f"{receiver}.{method_name}({', '.join(parts)})"

    def emit_block(self, block: Any, dispatched: Any) -> tuple[str, str]:
        """Dispatch a resolved block to its registered handler.

        Tries the registered :class:`~vg2c.emitter.utilities._base.UtilitySpec`
        handler for the block kind first.  Falls back to a ``pass``-stub for
        unclassified utility commands, HTML reports, and any other unhandled kind.

        Returns:
            A ``(func_source, call_site)`` pair suitable for appending to the
            emitted function list and the run-body respectively.
        """
        from vg2c.emitter.utilities._base import UtilitySpec
        from vg2c.emitter.utilities._emit_helpers import _emit_step_source, _step_name

        handler_cls = UtilitySpec._kind_handlers.get(block.kind)
        if handler_cls is not None:
            emitted = handler_cls.emit_block(self, block, dispatched)
            if emitted is not None:
                return emitted

        kind_fallbacks = {
            Kind.UTILITY: ("utility", "pass  # TODO: utility command not classified"),
            Kind.HTML_REPORT: ("html_report", "pass  # HTML report not translated"),
        }
        suffix, default_stmt = kind_fallbacks.get(
            block.kind,
            ("unknown", f"pass  # TODO: unhandled kind={block.kind}"),
        )
        return _emit_step_source(_step_name(block, suffix), [default_stmt])


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
        self.step_lines: dict[str, int] = {}

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
        import re
        match = re.match(r'^\s*def\s+(step_\w+)\b', lines)
        if match:
            self.step_lines[match.group(1)] = len(self.lines) + 1
        for line in lines.split("\n"):
            self.write(line)

    def source(self) -> str:
        """Get the full source text."""
        return "\n".join(self.lines)
