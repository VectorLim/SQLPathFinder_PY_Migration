"""Loop payloads: ``{RUN-LOOP}`` / ``{END-LOOP}``."""

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

log = Logger.getLogger("vg2c.operands.loop")


@dataclass(frozen=True, slots=True)
class RunLoop:
    input_csv_path: str
    chunk_csv_path: str
    chunk_size: int
    prompt_off: bool

    # ------------------------------------------------------------------
    # Scope building
    # ------------------------------------------------------------------

    @classmethod
    def from_block(cls, block: ClassifiedBlock) -> RunLoop:
        """Parse a {RUN-LOOP} block into a RunLoop payload."""
        args = _quoted_args(block.options.lookup.get("UTILITIES", ""))
        input_csv = args[0] if args else ""
        chunk_csv = args[1] if len(args) > 1 else ""
        chunk_sz = int(args[2]) if len(args) > 2 and args[2].isdigit() else 0
        prompt_flag = args[3] if len(args) > 3 else "N"
        return cls(
            input_csv_path=input_csv,
            chunk_csv_path=chunk_csv,
            chunk_size=chunk_sz,
            prompt_off=prompt_flag.upper() == "Y",
        )

    def build_scope(
        self,
        blocks: list[ClassifiedBlock],
        start_i: int,
        state: ScopeIdSource,
        parse_children: ParseChildrenFn,
    ) -> tuple[ScopeNode, int]:
        """Build a 'loop' ScopeNode, consuming blocks until {END-LOOP}."""
        return build_delimited_scope(
            self,
            blocks,
            start_i,
            state,
            parse_children,
            kind="loop",
            opening_token="RUN-LOOP",
            closing_token="END-LOOP",
            log_error=log.error,
        )

    # ------------------------------------------------------------------
    # Emission
    # ------------------------------------------------------------------

    def render_header(self, scope_id: int, *, macros: str = "macros") -> str:
        from vg2c.utilities._emit_helpers import native_macro_expression

        input_path = native_macro_expression(self.input_csv_path, macros)
        chunk_path = native_macro_expression(self.chunk_csv_path, macros)
        return (
            f"with closing(job.csv_chunks({input_path}, {chunk_path}, "
            f"{self.chunk_size})) as chunks_{scope_id}:"
        )

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
        """Emit a chunked for-loop over the input CSV."""
        imports.add("from contextlib import closing")
        writer.write(self.render_header(node.scope_id, macros=macros))
        writer.push_indent()
        writer.write(f"for chunk_{node.scope_id} in chunks_{node.scope_id}:")
        writer.push_indent()
        writer.write(f"with {macros}.scope():")
        writer.push_indent()
        trapped = self.prompt_off
        if trapped:
            writer.write("try:")
            writer.push_indent()
        emit_children(writer, walk, node.children, macros)
        if trapped:
            writer.pop_indent()
            writer.write("except Exception:")
            writer.push_indent()
            writer.write("continue")
            writer.pop_indent()
        writer.pop_indent()
        writer.pop_indent()
        writer.pop_indent()


@dataclass(frozen=True, slots=True)
class EndLoop:
    pass


@dataclass(frozen=True, slots=True)
class ForLoop:
    args: tuple[str, ...]
    build_scope = RunLoop.build_scope

    @classmethod
    def from_block(cls, block):
        args = tuple(_quoted_args(block.options.lookup.get("UTILITIES", "")))
        if len(args) < 4 or len(args) > 6:
            raise ValueError(f"{block.span.file}:{block.span.start_line}: FOR requires 4-6 arguments")
        return cls(args)

    def render_header(self, scope_id: int, *, macros: str = "macros") -> str:
        from vg2c.utilities._emit_helpers import native_macro_expression

        expr = ", ".join(native_macro_expression(arg, macros) for arg in self.args)
        return f"for loop_values_{scope_id} in for_values({expr}):"

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
        runtime_imports.add("for_values")
        writer.write(self.render_header(node.scope_id, macros=macros))
        writer.push_indent()
        writer.write(f"with {macros}.scope(loop_values_{node.scope_id}):")
        writer.push_indent()
        emit_children(writer, walk, node.children, macros)
        writer.pop_indent()
        writer.pop_indent()


@dataclass(frozen=True, slots=True)
class SiteLoop:
    nodes: str
    build_scope = RunLoop.build_scope

    @classmethod
    def from_block(cls, block):
        args = _quoted_args(block.options.lookup.get("UTILITIES", ""))
        if len(args) != 1 or not args[0]:
            raise ValueError(f"{block.span.file}:{block.span.start_line}: SITE requires a node list")
        return cls(args[0])

    def render_header(self, scope_id: int, *, macros: str = "macros") -> str:
        from vg2c.utilities._emit_helpers import native_macro_expression

        nodes = native_macro_expression(self.nodes, macros)
        return f"for loop_values_{scope_id} in site_values({nodes}):"

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
        runtime_imports.add("site_values")
        writer.write(self.render_header(node.scope_id, macros=macros))
        writer.push_indent()
        writer.write(f"with {macros}.scope(loop_values_{node.scope_id}):")
        writer.push_indent()
        writer.write("try:")
        writer.push_indent()
        emit_children(writer, walk, node.children, macros)
        writer.pop_indent()
        writer.write("except Exception:")
        writer.push_indent()
        writer.write("break")
        writer.pop_indent()
        writer.pop_indent()
        writer.pop_indent()
