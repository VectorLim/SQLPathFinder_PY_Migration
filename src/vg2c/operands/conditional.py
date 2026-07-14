"""Conditional flow payloads: ``{IF-THEN}`` / ``{ELSE}`` / ``{END-IF}``."""

from __future__ import annotations

from dataclasses import dataclass
from typing import TYPE_CHECKING, Callable

from vg2c.frontend.models import ClassifiedBlock, Diagnostic

from vg2c.resolver.operands.base import (
    _OPERATOR_TABLE,
    ParseChildrenFn,
    ScopeIdSource,
    ScopeNode,
    _operand_expr,
    _quoted_args,
)

if TYPE_CHECKING:
    from vg2c.emitter.models import IndentWriter


@dataclass(frozen=True, slots=True)
class IfThen:
    lhs: str
    op: str
    rhs: str
    conj: str | None
    lhs2: str | None
    op2: str | None
    rhs2: str | None

    # ------------------------------------------------------------------
    # Scope building
    # ------------------------------------------------------------------

    @classmethod
    def from_block(cls, block: ClassifiedBlock) -> IfThen:
        """Parse an {IF-THEN} block into an IfThen payload."""
        args = _quoted_args(block.options.lookup.get("UTILITIES", ""))
        padded = (args + ["", "", "", "", "", "", ""])[:7]
        return cls(
            lhs=padded[0],
            op=padded[1],
            rhs=padded[2],
            conj=padded[3] or None,
            lhs2=padded[4] or None,
            op2=padded[5] or None,
            rhs2=padded[6] or None,
        )

    def build_scope(
        self,
        blocks: list[ClassifiedBlock],
        start_i: int,
        state: ScopeIdSource,
        diagnostics: list[Diagnostic],
        parse_children: ParseChildrenFn,
    ) -> tuple[ScopeNode, int]:
        """Build an 'if' ScopeNode with if-branch and optional else-branch children."""
        start_block = blocks[start_i]

        if_children, i, token = parse_children(
            blocks, start_i + 1, {"ELSE", "END-IF"}, state, diagnostics
        )

        branch_nodes: list[ScopeNode] = [
            ScopeNode(
                scope_id=state.new_scope_id(),
                kind="if-branch",
                start_index=start_block.index,
                end_index=(
                    if_children[-1].end_index if if_children else start_block.index
                ),
                children=tuple(if_children),
                block_index=None,
                control_payload=None,
            )
        ]

        end_index = blocks[-1].index if blocks else start_block.index
        next_i = i

        if token == "ELSE":
            else_children, j, else_stop = parse_children(
                blocks, i + 1, {"END-IF"}, state, diagnostics
            )
            branch_nodes.append(
                ScopeNode(
                    scope_id=state.new_scope_id(),
                    kind="else-branch",
                    start_index=blocks[i].index,
                    end_index=(
                        else_children[-1].end_index
                        if else_children
                        else blocks[i].index
                    ),
                    children=tuple(else_children),
                    block_index=None,
                    control_payload=Else(),
                )
            )
            if else_stop == "END-IF":
                end_index = blocks[j].index
                next_i = j + 1
            else:
                diagnostics.append(
                    Diagnostic(
                        severity="error",
                        code="unclosed-if",
                        message="Found {IF-THEN} without a matching {END-IF}; implicitly closed at EOF.",
                        block_index=start_block.index,
                        span=start_block.span,
                    )
                )
                next_i = j
        elif token == "END-IF":
            end_index = blocks[i].index
            next_i = i + 1
        else:
            diagnostics.append(
                Diagnostic(
                    severity="error",
                    code="unclosed-if",
                    message="Found {IF-THEN} without a matching {END-IF}; implicitly closed at EOF.",
                    block_index=start_block.index,
                    span=start_block.span,
                )
            )

        return (
            ScopeNode(
                scope_id=state.new_scope_id(),
                kind="if",
                start_index=start_block.index,
                end_index=end_index,
                children=tuple(branch_nodes),
                block_index=None,
                control_payload=self,
            ),
            next_i,
        )

    # ------------------------------------------------------------------
    # Emission
    # ------------------------------------------------------------------

    def _build_condition_expr(self) -> str:
        """Build a Python boolean expression from this IfThen payload."""
        op_symbol, op_type = _OPERATOR_TABLE.get(self.op, ("==", "string"))
        numeric = op_type == "numeric"

        lhs = _operand_expr(self.lhs, numeric, numeric)
        rhs = _operand_expr(self.rhs, numeric, numeric)
        expr = f"{lhs} {op_symbol} {rhs}"

        if self.conj and self.lhs2 and self.op2 and self.rhs2:
            op2_symbol, op2_type = _OPERATOR_TABLE.get(self.op2, ("==", "string"))
            numeric2 = op2_type == "numeric"
            lhs2 = _operand_expr(self.lhs2, numeric2, numeric2)
            rhs2 = _operand_expr(self.rhs2, numeric2, numeric2)
            conj_op = " and " if self.conj.upper() == "AND" else " or "
            expr += f"{conj_op}{lhs2} {op2_symbol} {rhs2}"

        return expr

    def emit_scope(
        self,
        writer: IndentWriter,
        walk: Callable[[ScopeNode], None],
        children: tuple[ScopeNode, ...],
    ) -> None:
        """Emit an if/else block from this IfThen payload."""
        if_branch: ScopeNode | None = None
        else_branch: ScopeNode | None = None
        for child in children:
            if child.kind == "if-branch":
                if_branch = child
            elif child.kind == "else-branch":
                else_branch = child

        condition_expr = self._build_condition_expr()

        writer.write(f"if {condition_expr}:")
        writer.push_indent()
        if if_branch:
            for child in if_branch.children:
                walk(child)
        writer.pop_indent()

        if else_branch:
            writer.write("else:")
            writer.push_indent()
            for child in else_branch.children:
                walk(child)
            writer.pop_indent()


@dataclass(frozen=True, slots=True)
class Else:
    pass


@dataclass(frozen=True, slots=True)
class EndIf:
    pass
