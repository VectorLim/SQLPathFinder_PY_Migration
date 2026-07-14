"""Macro scope payloads: ``{START-MACRO}`` / ``{END-MACRO}``."""

from __future__ import annotations

from dataclasses import dataclass
from typing import TYPE_CHECKING, Callable

from vg2c.frontend.models import ClassifiedBlock, Diagnostic

from vg2c.operands.base import (
    ParseChildrenFn,
    ScopeIdSource,
    ScopeNode,
    _quoted_args,
)

if TYPE_CHECKING:
    from vg2c.emitter.models import IndentWriter


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
        diagnostics: list[Diagnostic],
        parse_children: ParseChildrenFn,
    ) -> tuple[ScopeNode, int]:
        """Build a 'macro' ScopeNode, consuming blocks until {END-MACRO}."""
        start_block = blocks[start_i]
        children, i, end_token = parse_children(
            blocks, start_i + 1, {"END-MACRO"}, state, diagnostics
        )

        if end_token != "END-MACRO":
            diagnostics.append(
                Diagnostic(
                    severity="error",
                    code="unclosed-macro",
                    message="Found {START-MACRO} without a matching {END-MACRO}; implicitly closed at EOF.",
                    block_index=start_block.index,
                    span=start_block.span,
                )
            )
            end_index = blocks[-1].index if blocks else start_block.index
            return (
                ScopeNode(
                    scope_id=state.new_scope_id(),
                    kind="macro",
                    start_index=start_block.index,
                    end_index=end_index,
                    children=tuple(children),
                    block_index=None,
                    control_payload=self,
                ),
                i,
            )

        end_index = blocks[i].index
        return (
            ScopeNode(
                scope_id=state.new_scope_id(),
                kind="macro",
                start_index=start_block.index,
                end_index=end_index,
                children=tuple(children),
                block_index=None,
                control_payload=self,
            ),
            i + 1,
        )

    # ------------------------------------------------------------------
    # Emission
    # ------------------------------------------------------------------

    def emit_scope(
        self,
        writer: IndentWriter,
        walk: Callable[[ScopeNode], None],
        children: tuple[ScopeNode, ...],
    ) -> None:
        """Emit a row-iterating macro (for-loop + scope) or a static-vars macro (with-block)."""
        from vg2c.emitter.utilities.csv_io import CsvIO
        from vg2c.emitter.utilities.macro_state import MacroState

        row_iter = bool(self.csv_path)
        if row_iter:
            iter_call = CsvIO.iter.render(repr(self.csv_path))
            scope_call = MacroState.scope.render("__row")
            writer.write(f"for __row in {iter_call}:")
            writer.push_indent()
            writer.write(f"with {scope_call}:")
            writer.push_indent()
        else:
            scope_call = MacroState.scope.render()
            writer.write(f"with {scope_call}:")
            writer.push_indent()

        for child in children:
            walk(child)

        if row_iter:
            writer.pop_indent()
        writer.pop_indent()


@dataclass(frozen=True, slots=True)
class EndMacro:
    pass
