"""Macro scope payloads: ``{START-MACRO}`` / ``{END-MACRO}``."""

from __future__ import annotations

from collections.abc import Callable
from dataclasses import dataclass
from typing import TYPE_CHECKING

from vg2c.frontend.models import ClassifiedBlock
from vg2c.logger import Logger
from vg2c.operands.base import (
    ParseChildrenFn,
    ScopeIdSource,
    ScopeNode,
    _quoted_args,
    build_delimited_scope,
    emit_children,
)

if TYPE_CHECKING:
    from vg2c.emitter.indent_writer import IndentWriter

log = Logger.getLogger("vg2c.operands.macro")


@dataclass(frozen=True, slots=True)
class StartMacro:
    csv_path: str
    prompt_off: bool

    # ------------------------------------------------------------------
    # Scope building
    # ------------------------------------------------------------------

    @classmethod
    def from_block(cls, block: ClassifiedBlock) -> StartMacro:
        """Parse a {START-MACRO} block into a StartMacro payload."""
        args = _quoted_args(block.options.lookup.get("UTILITIES", ""))
        csv_path = args[0] if args else ""
        prompt_flag = args[1] if len(args) > 1 else "N"
        return cls(csv_path=csv_path, prompt_off=prompt_flag.upper() == "Y")

    def build_scope(
        self,
        blocks: list[ClassifiedBlock],
        start_i: int,
        state: ScopeIdSource,
        parse_children: ParseChildrenFn,
    ) -> tuple[ScopeNode, int]:
        """Build a 'macro' ScopeNode, consuming blocks until {END-MACRO}."""
        return build_delimited_scope(
            self,
            blocks,
            start_i,
            state,
            parse_children,
            kind="macro",
            opening_token="START-MACRO",
            closing_token="END-MACRO",
            log_error=log.error,
        )

    def render_header(self, scope_id: int, *, macros: str = "macros") -> str:
        if not self.csv_path:
            return f"with {macros}.scope():"
        from vg2c.utilities._emit_helpers import native_macro_expression

        path = native_macro_expression(self.csv_path, macros)
        return f"macro_row_{scope_id} = job.read_macro_row({path})"

    def emit_scope(
        self,
        writer: IndentWriter,
        walk: Callable[[ScopeNode, str], None],
        node: ScopeNode,
        *,
        macros: str,
        imports: set[str],
        runtime_imports: set[str],
    ) -> None:
        writer.write(self.render_header(node.scope_id, macros=macros))
        if not self.csv_path:
            writer.push_indent()
            emit_children(writer, walk, node.children, macros)
            writer.pop_indent()
            return
        writer.write(f"if macro_row_{node.scope_id} is not None:")
        writer.push_indent()
        writer.write(f"with {macros}.scope(macro_row_{node.scope_id}):")
        writer.push_indent()
        emit_children(writer, walk, node.children, macros)
        writer.pop_indent()
        writer.pop_indent()


@dataclass(frozen=True, slots=True)
class EndMacro:
    pass
