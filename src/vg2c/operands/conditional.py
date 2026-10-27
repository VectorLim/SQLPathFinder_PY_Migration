"""Conditional flow payloads: ``{IF-THEN}`` / ``{ELSE}`` / ``{END-IF}``."""

from __future__ import annotations

from collections import Counter
from collections.abc import Callable
from dataclasses import dataclass
from typing import TYPE_CHECKING

from vg2c.frontend.models import ClassifiedBlock
from vg2c.logger import Logger
from vg2c.operands.base import (
    _OPERATOR_TABLE,
    ParseChildrenFn,
    ScopeIdSource,
    ScopeNode,
    _operand_expr,
    _operand_macro,
    _quoted_args,
    emit_children,
)

if TYPE_CHECKING:
    from vg2c.emitter.indent_writer import IndentWriter

log = Logger.getLogger("vg2c.operands.conditional")


@dataclass(frozen=True, slots=True)
class IfThen:
    lhs: str
    op: str
    rhs: str
    conj: str | None
    lhs2: str | None
    op2: str | None
    rhs2: str | None
    prompt_text: str
    # (macro, csv path) of a ROWS-IN-FILE folded into this condition.
    row_count: tuple[str, str] | None = None

    # ------------------------------------------------------------------
    # Option / expression parsing
    # ------------------------------------------------------------------

    @classmethod
    def from_block(cls, block: ClassifiedBlock) -> IfThen:
        """Parse an {IF-THEN} block into an IfThen payload."""
        args = _quoted_args(block.options.lookup.get("UTILITIES", ""))
        lhs = args[0] if args else ""
        op = args[1].upper() if len(args) > 1 else "EQS"
        rhs = args[2] if len(args) > 2 else ""
        conj = args[3].upper() if len(args) > 3 and args[3] else None
        lhs2 = args[4] if len(args) > 4 and args[4] else None
        op2 = args[5].upper() if len(args) > 5 and args[5] else None
        rhs2 = args[6] if len(args) > 6 and args[6] else None
        return cls(
            lhs=lhs,
            op=op,
            rhs=rhs,
            conj=conj,
            lhs2=lhs2,
            op2=op2,
            rhs2=rhs2,
            prompt_text=block.options.lookup.get("PROMPT-TEXT", ""),
        )

    def build_scope(
        self,
        blocks: list[ClassifiedBlock],
        start_i: int,
        state: ScopeIdSource,
        parse_children: ParseChildrenFn,
    ) -> tuple[ScopeNode, int]:
        """Build an 'if' ScopeNode with if-branch and optional else-branch children."""
        start_block = blocks[start_i]

        if_children, i, token = parse_children(
            blocks, start_i + 1, {"ELSE", "END-IF"}, state
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
                blocks, i + 1, {"END-IF"}, state
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
                loc = f"{start_block.span.file or '<input>'}:{start_block.span.start_line}:1"
                log.error(
                    f"[unclosed-if] {loc} (block {start_block.index}): "
                    "Found {IF-THEN} without a matching {END-IF}; implicitly closed at EOF."
                )
                next_i = j
        elif token == "END-IF":
            end_index = blocks[i].index
            next_i = i + 1
        else:
            loc = (
                f"{start_block.span.file or '<input>'}:{start_block.span.start_line}:1"
            )
            log.error(
                f"[unclosed-if] {loc} (block {start_block.index}): "
                "Found {IF-THEN} without a matching {END-IF}; implicitly closed at EOF."
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

    def _clauses(self):
        """Yield (python_operator, numeric, lhs, rhs) for each comparison."""
        yield (*_OPERATOR_TABLE.get(self.op, ("==", "string")), self.lhs, self.rhs)
        if self.conj and self.lhs2 and self.op2 and self.rhs2:
            yield (
                *_OPERATOR_TABLE.get(self.op2, ("==", "string")),
                self.lhs2,
                self.rhs2,
            )

    def _reads(self):
        """Yield (clause_index, numeric, macro) for every operand that reads a macro."""
        for index, (_, op_type, *operands) in enumerate(self._clauses()):
            numeric = op_type == "numeric"
            for operand in operands:
                if name := _operand_macro(operand, numeric):
                    yield index, numeric, name

    def macro_reads(self) -> set[str]:
        return {name for *_, name in self._reads()}

    def inlinable_macros(self) -> set[str]:
        """Macros read exactly once, numerically, in the always-evaluated first comparison."""
        reads = list(self._reads())
        counts = Counter(name for *_, name in reads)
        return {
            name
            for index, numeric, name in reads
            if index == 0 and numeric and counts[name] == 1
        }

    def _build_condition_expr(self) -> str:
        """Build a Python boolean expression from this IfThen payload."""
        from vg2c.utilities.rows_in_file import RowsInFile

        inline = {}
        if self.row_count is not None:
            target, path = self.row_count
            inline[target] = RowsInFile.call(path)
        parts = []
        for symbol, op_type, lhs, rhs in self._clauses():
            numeric = op_type == "numeric"
            parts.append(
                f"{_operand_expr(lhs, numeric, inline)} {symbol} "
                f"{_operand_expr(rhs, numeric, inline)}"
            )
        conj_op = " and " if (self.conj or "").upper() == "AND" else " or "
        return conj_op.join(parts)

    def render_header(self, scope_id: int, *, macros: str = "macros") -> str:
        """Render the direct Python condition used by the native emitter."""
        from vg2c.utilities._emit_helpers import native_expression

        return "if " + native_expression(self._build_condition_expr(), macros) + ":"

    def emit_scope(
        self,
        writer: IndentWriter,
        walk: Callable[[ScopeNode, str], None],
        node: ScopeNode,
        *,
        macros: str,
        imports: set[str],
        runtime_imports: set[str],
        header_override: str | None = None,
    ) -> None:
        """Emit an if/else block from this IfThen payload."""
        if_branch: ScopeNode | None = None
        else_branch: ScopeNode | None = None
        for child in node.children:
            if child.kind == "if-branch":
                if_branch = child
            elif child.kind == "else-branch":
                else_branch = child

        writer.write(
            header_override or self.render_header(node.scope_id, macros=macros)
        )
        writer.push_indent()
        emit_children(
            writer,
            walk,
            if_branch.children if if_branch else (),
            macros,
        )
        writer.pop_indent()

        if else_branch:
            writer.write("else:")
            writer.push_indent()
            emit_children(writer, walk, else_branch.children, macros)
            writer.pop_indent()


@dataclass(frozen=True, slots=True)
class Else:
    pass


@dataclass(frozen=True, slots=True)
class EndIf:
    pass
