from __future__ import annotations

from vg2c.dispatch.models import DispatchedProgram
from vg2c.emitter.handlers import create_handlers
from vg2c.emitter.models import EmitContext, IndentWriter
from vg2c.frontend.models import Kind
from vg2c.resolver.models import IfThen, ResolvedBlock, ScopeNode, StartMacro

__all__ = ["walk_and_emit"]

_OPERATOR_MAP = {
    "EQS": "==",
    "NES": "!=",
    "LE": "<=",
    "LT": "<",
    "GE": ">=",
    "GT": ">",
    "EQ": "==",
    "NE": "!=",
}


def _build_condition_expr(payload: IfThen) -> str:
    """Build a Python boolean expression from IfThen payload.

    Handles:
    - lhs op rhs [conj lhs2 op2 rhs2]
    - VAR(name) unwrapped to ctx.macro.named("NAME")
    - Operator code mapping (EQS -> ==, etc.)
    """

    def unwrap_operand(operand: str) -> str:
        """Convert VAR(X) to ctx.macro.named('X'), or keep literal."""
        if operand.startswith("VAR(") and operand.endswith(")"):
            var_name = operand[4:-1].strip()
            return f'ctx.macro.named("{var_name.upper()}")'
        return f'"{operand}"' if operand else '""'

    lhs = unwrap_operand(payload.lhs)
    op = _OPERATOR_MAP.get(payload.op, "==")
    rhs = unwrap_operand(payload.rhs)

    expr = f"{lhs} {op} {rhs}"

    if payload.conj and payload.lhs2 and payload.op2 and payload.rhs2:
        lhs2 = unwrap_operand(payload.lhs2)
        op2 = _OPERATOR_MAP.get(payload.op2, "==")
        rhs2 = unwrap_operand(payload.rhs2)
        conj_op = " and " if payload.conj.upper() == "AND" else " or "
        expr += f" {conj_op} {lhs2} {op2} {rhs2}"

    return expr


def walk_and_emit(
    dispatched: DispatchedProgram,
    ctx: EmitContext,
) -> tuple[list[str], str]:
    """Walk scope tree and emit Python code.

    Returns:
        (functions_list, run_body_source) where functions_list is the emitted
        helper function definitions and run_body_source is the main run() body.
    """
    handlers = create_handlers()
    block_by_index = {b.parsed.index: b for b in dispatched.analyzed.resolved.blocks}

    functions: list[str] = []
    writer = IndentWriter()

    _walk_scope(
        dispatched.analyzed.resolved.scope_tree,
        dispatched,
        block_by_index,
        handlers,
        ctx,
        writer,
        functions,
    )

    return functions, writer.source()


def _walk_scope(
    node: ScopeNode,
    dispatched: DispatchedProgram,
    block_by_index: dict[int, ResolvedBlock],
    handlers: dict,
    ctx: EmitContext,
    writer: IndentWriter,
    functions: list[str],
) -> None:
    """Recursively walk scope tree and emit code."""
    if node.kind == "program":
        # Top level: walk children
        for child in node.children:
            _walk_scope(
                child, dispatched, block_by_index, handlers, ctx, writer, functions
            )

    elif node.kind == "macro":
        # {START-MACRO}: emit for/with wrapper
        payload = node.control_payload
        if isinstance(payload, StartMacro):
            writer.write(
                f'with ctx.macro_scope("{payload.csv_path}", row_iter={bool(payload.csv_path)}):'
            )
            writer.push_indent()
            for child in node.children:
                _walk_scope(
                    child, dispatched, block_by_index, handlers, ctx, writer, functions
                )
            writer.pop_indent()

    elif node.kind == "if":
        # {IF-THEN}: emit if/else wrapper
        if_branch = None
        else_branch = None
        for child in node.children:
            if child.kind == "if-branch":
                if_branch = child
            elif child.kind == "else-branch":
                else_branch = child

        # Extract the condition from the IfThen payload
        condition_expr = "True"  # Default fallback
        if node.control_payload and isinstance(node.control_payload, IfThen):
            condition_expr = _build_condition_expr(node.control_payload)

        writer.write(f"if {condition_expr}:")
        writer.push_indent()
        if if_branch:
            for child in if_branch.children:
                _walk_scope(
                    child, dispatched, block_by_index, handlers, ctx, writer, functions
                )
        writer.pop_indent()

        if else_branch:
            writer.write("else:")
            writer.push_indent()
            for child in else_branch.children:
                _walk_scope(
                    child, dispatched, block_by_index, handlers, ctx, writer, functions
                )
            writer.pop_indent()

    elif node.kind in ("if-branch", "else-branch"):
        # These are containers; walk their children without emitting anything
        for child in node.children:
            _walk_scope(
                child, dispatched, block_by_index, handlers, ctx, writer, functions
            )

    elif node.kind == "leaf":
        # A content block: dispatch to handler
        block_index = node.block_index
        if block_index is None:
            return

        block = block_by_index.get(block_index)
        if block is None or block.kind is Kind.MACRO_CONTROL:
            return  # Skip macro control blocks

        # Look up handler
        handler = handlers.get(block.kind)
        if handler is None:
            return

        # Find dispatch metadata
        dispatched_block = None
        for db in dispatched.dispatched:
            if db.block_index == block_index:
                dispatched_block = db
                break

        # Emit the function
        try:
            func_code, call_site = handler(ctx, block, dispatched_block)
            functions.append(func_code)
            writer.write(call_site)
        except Exception:
            # Silently skip on error; could emit diagnostic
            pass
