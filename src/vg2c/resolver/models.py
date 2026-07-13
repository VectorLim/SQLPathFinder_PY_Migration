from __future__ import annotations

import re
from dataclasses import dataclass
from typing import TYPE_CHECKING, Callable, Literal, Mapping

from vg2c.frontend.models import (
    BlockOptions,
    ClassifiedBlock,
    Diagnostic,
    ParsedBlock,
    SourceSpan,
    copy_dataclass_fields,
)
from vg2c.kind import Kind

if TYPE_CHECKING:
    from vg2c.emitter.models import IndentWriter

# ---------------------------------------------------------------------------
# Condition-expression helpers (moved from emitter/walker.py)
# ---------------------------------------------------------------------------

_OPERATOR_TABLE: dict[str, tuple[str, str]] = {
    "EQS": ("==", "string"),
    "NES": ("!=", "string"),
    "LE": ("<=", "numeric"),
    "LT": ("<", "numeric"),
    "GE": (">=", "numeric"),
    "GT": (">", "numeric"),
    "EQ": ("==", "numeric"),
    "NE": ("!=", "numeric"),
}

_BARE_IDENT_RE = re.compile(r"^[A-Za-z_][A-Za-z0-9_]*$")


def _int_expr(expr: str) -> str:
    return f"int({expr})"


def _operand_expr(operand: str, numeric: bool, allow_bare_macro: bool) -> str:
    """Render a single condition operand as a Python expression string."""
    # Local import to avoid circular dependency at module load time.
    from vg2c.emitter.utilities._emit_helpers import normalize_macro_name
    from vg2c.emitter.utilities.macro_state import MacroState

    value = operand.strip()

    if not value:
        return _int_expr("0") if numeric else repr("")

    if value.startswith("VAR(") and value.endswith(")"):
        base = MacroState.named.render(repr(normalize_macro_name(value[4:-1].strip())))
        return _int_expr(base) if numeric else base

    if MacroState.NAMED_PLACEHOLDER_RE.fullmatch(value):
        base = MacroState.named.render(repr(normalize_macro_name(value)))
        return _int_expr(base) if numeric else base

    if allow_bare_macro and _BARE_IDENT_RE.match(value):
        base = MacroState.named.render(repr(normalize_macro_name(value)))
        return _int_expr(base) if numeric else base

    if numeric:
        return _int_expr(repr(value))
    return repr(value)


# ---------------------------------------------------------------------------
# Scope-building helpers (shared with scope_builder.py)
# ---------------------------------------------------------------------------

def _quoted_args(value: str) -> list[str]:
    """Extract double-quoted argument strings from a UTILITIES option value."""
    return re.findall(r'"([^"]*)"', value)


class ScopeIdSource:
    """Protocol-style duck type expected by build_scope methods.

    scope_builder._ScopeBuilderState satisfies this interface.
    """

    def new_scope_id(self) -> int:  # pragma: no cover
        raise NotImplementedError


# Type alias for the recursive parse_children callable injected into build_scope.
# Defined as a plain alias so it can be used at runtime as well as for type hints.
ParseChildrenFn = Callable[
    [
        "list[ClassifiedBlock]",   # blocks
        int,                       # start index
        "set[str] | None",         # stop_tokens
        ScopeIdSource,             # state
        "list[Diagnostic]",        # diagnostics
    ],
    "tuple[list[ScopeNode], int, str | None]",
]


# ---------------------------------------------------------------------------
# Macro control payload types
# ---------------------------------------------------------------------------


@dataclass(frozen=True, slots=True)
class MacroFrame:
    frame_id: int
    kind: Literal["row-iter", "if", "static-vars"]
    csv_path: str | None
    csv_headers: tuple[str, ...] | None
    named_vars: Mapping[str, str]
    positional_cursor: int
    source_span: SourceSpan


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


@dataclass(frozen=True, slots=True)
class RowsInFile:
    csv_path: str
    var_name: str
    prompt_off: bool

    @classmethod
    def from_block(cls, block: ClassifiedBlock) -> RowsInFile:
        """Parse a {ROWS-IN-FILE} block into a RowsInFile payload."""
        args = _quoted_args(block.options.lookup.get("UTILITIES", ""))
        csv_path = args[0] if args else ""
        var_name = args[1] if len(args) > 1 else ""
        prompt_flag = args[2] if len(args) > 2 else "N"
        return cls(
            csv_path=csv_path,
            var_name=var_name,
            prompt_off=prompt_flag.upper() == "Y",
        )


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
        chunk_size_raw = args[2] if len(args) > 2 else "0"
        prompt_flag = args[3] if len(args) > 3 else "N"
        try:
            chunk_size = int(chunk_size_raw)
        except ValueError:
            chunk_size = 0
        return cls(
            input_csv_path=input_csv,
            chunk_csv_path=chunk_csv,
            chunk_size=chunk_size,
            prompt_off=prompt_flag.upper() == "Y",
        )

    def build_scope(
        self,
        blocks: list[ClassifiedBlock],
        start_i: int,
        state: ScopeIdSource,
        diagnostics: list[Diagnostic],
        parse_children: ParseChildrenFn,
    ) -> tuple[ScopeNode, int]:
        """Build a 'loop' ScopeNode, consuming blocks until {END-LOOP}."""
        start_block = blocks[start_i]
        children, i, end_token = parse_children(
            blocks, start_i + 1, {"END-LOOP"}, state, diagnostics
        )

        if end_token != "END-LOOP":
            diagnostics.append(
                Diagnostic(
                    severity="error",
                    code="unclosed-loop",
                    message="Found {RUN-LOOP} without a matching {END-LOOP}; implicitly closed at EOF.",
                    block_index=start_block.index,
                    span=start_block.span,
                )
            )
            end_index = blocks[-1].index if blocks else start_block.index
            return (
                ScopeNode(
                    scope_id=state.new_scope_id(),
                    kind="loop",
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
                kind="loop",
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
        """Emit a chunked for-loop over the input CSV."""
        from vg2c.emitter.utilities.csv_io import CsvIO

        chunks_call = CsvIO.iter_chunks.render(
            repr(self.input_csv_path),
            repr(self.chunk_csv_path),
            int(self.chunk_size),
        )
        writer.write(f"for __chunk_path in {chunks_call}:")
        writer.push_indent()
        for child in children:
            walk(child)
        writer.pop_indent()


@dataclass(frozen=True, slots=True)
class EndLoop:
    pass


MacroControlPayload = (
    StartMacro | EndMacro | IfThen | Else | EndIf | RowsInFile | RunLoop | EndLoop
)


@dataclass(frozen=True, slots=True)
class ScopeNode:
    scope_id: int
    kind: Literal["program", "macro", "loop", "if", "if-branch", "else-branch", "leaf"]
    start_index: int
    end_index: int
    children: tuple["ScopeNode", ...]
    block_index: int | None
    control_payload: MacroControlPayload | None

    def emit(
        self,
        writer: IndentWriter,
        walk: Callable[[ScopeNode], None],
    ) -> None:
        """Delegate structural emission to the control payload, or walk children transparently.

        Leaf nodes are intentionally excluded — the walker handles them via
        UtilitySpec dispatch, which is not a payload concern.
        """
        if self.control_payload is not None and hasattr(
            self.control_payload, "emit_scope"
        ):
            self.control_payload.emit_scope(writer, walk, self.children)  # type: ignore[union-attr]
        else:
            # Transparent nodes: program, if-branch, else-branch
            for child in self.children:
                walk(child)


@dataclass(frozen=True, slots=True)
class SqlGetCsvListCall:
    """Parsed SQL_Get_CSV_List macro call."""

    name: Literal["SQL_Get_CSV_List"]
    csv_path: str
    column_ref: int | str
    lead_in: str
    source_span: SourceSpan

    def consumed_csv_paths(self) -> tuple[str, ...]:
        """Return CSV paths consumed by this call."""
        return (self.csv_path,)


# Union grows as more macros are added (e.g. SqlGetCsvListCall | SqlTimeRangeCall)
SqlMacroCall = SqlGetCsvListCall


@dataclass(frozen=True, slots=True)
class ResolvedBlock(ClassifiedBlock):
    resolved_options: BlockOptions
    resolved_body: str
    sql_macro_calls: tuple[SqlMacroCall, ...]
    control_payload: MacroControlPayload | None
    scope_id: int

    def __init__(
        self,
        classified: ClassifiedBlock,
        resolved_options: BlockOptions,
        resolved_body: str,
        sql_macro_calls: tuple[SqlMacroCall, ...],
        control_payload: MacroControlPayload | None,
        scope_id: int,
    ) -> None:
        copy_dataclass_fields(classified, self, ClassifiedBlock)

        object.__setattr__(self, "resolved_options", resolved_options)
        object.__setattr__(self, "resolved_body", resolved_body)
        object.__setattr__(self, "sql_macro_calls", sql_macro_calls)
        object.__setattr__(self, "control_payload", control_payload)
        object.__setattr__(self, "scope_id", scope_id)


@dataclass(frozen=True, slots=True)
class ResolvedProgram:
    blocks: tuple[ResolvedBlock, ...]
    scope_tree: ScopeNode
    diagnostics: tuple[Diagnostic, ...]
