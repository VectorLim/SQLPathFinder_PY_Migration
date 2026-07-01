from __future__ import annotations

import re

from vg2c.dispatch.models import DispatchedProgram
from vg2c.emitter.codegen import (
    CTX_VAR,
    FunctionDef,
    PyExpr,
    emit_call,
    register_call_embed,
)
from vg2c.emitter.handlers import create_handlers
from vg2c.emitter.macro import (
    NAMED_PLACEHOLDER_RE,
    macro_token_to_python_expr,
)
from vg2c.emitter.models import EmitContext, IndentWriter
from vg2c.emitter.utilities import CsvIO, MacroState, PipelineContext
from vg2c.emitter.utilities_embed import register_utility_emission
from vg2c.frontend.models import Diagnostic, Kind
from vg2c.resolver.models import (
    IfThen,
    ResolvedBlock,
    RowsInFile,
    RunLoop,
    ScopeNode,
    StartMacro,
)

__all__ = ["walk_and_emit"]

_OPERATOR_TABLE = {
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
    value = operand.strip()

    if not value:
        return _int_expr("0") if numeric else repr("")

    if value.startswith("VAR(") and value.endswith(")"):
        base = macro_token_to_python_expr(value[4:-1].strip())
        return _int_expr(base) if numeric else base

    if NAMED_PLACEHOLDER_RE.fullmatch(value):
        base = macro_token_to_python_expr(value)
        return _int_expr(base) if numeric else base

    if allow_bare_macro and _BARE_IDENT_RE.match(value):
        base = macro_token_to_python_expr(value)
        return _int_expr(base) if numeric else base

    if numeric:
        return _int_expr(repr(value))
    return repr(value)


def _build_condition_expr(payload: IfThen) -> str:
    """Build a Python boolean expression from IfThen payload.

    Handles:
    - lhs op rhs [conj lhs2 op2 rhs2]
    - VAR(name) unwrapped to ctx.macro.named("NAME")
    - Operator code mapping (EQS -> ==, etc.)
    """

    op_symbol, op_type = _OPERATOR_TABLE.get(payload.op, ("==", "string"))
    numeric = op_type == "numeric"

    lhs = _operand_expr(payload.lhs, numeric=numeric, allow_bare_macro=numeric)
    rhs = _operand_expr(payload.rhs, numeric=numeric, allow_bare_macro=numeric)

    expr = f"{lhs} {op_symbol} {rhs}"

    if payload.conj and payload.lhs2 and payload.op2 and payload.rhs2:
        op2_symbol, op2_type = _OPERATOR_TABLE.get(payload.op2, ("==", "string"))
        numeric2 = op2_type == "numeric"
        lhs2 = _operand_expr(payload.lhs2, numeric=numeric2, allow_bare_macro=numeric2)
        rhs2 = _operand_expr(payload.rhs2, numeric=numeric2, allow_bare_macro=numeric2)
        conj_op = " and " if payload.conj.upper() == "AND" else " or "
        expr += f"{conj_op}{lhs2} {op2_symbol} {rhs2}"

    return expr


def walk_and_emit(
    dispatched: DispatchedProgram,
    ctx: EmitContext,
) -> tuple[list[str], str, tuple[Diagnostic, ...]]:
    """Walk scope tree and emit Python code.

    Returns:
        (functions_list, run_body_source, diagnostics) where functions_list is
        the emitted helper function definitions and run_body_source is the main
        run() body.
    """
    handlers = create_handlers()
    block_by_index = {b.parsed.index: b for b in dispatched.analyzed.resolved.blocks}

    functions: list[str] = []
    diagnostics: list[Diagnostic] = []
    writer = IndentWriter()

    _walk_scope(
        dispatched.analyzed.resolved.scope_tree,
        dispatched,
        block_by_index,
        handlers,
        ctx,
        writer,
        functions,
        diagnostics,
    )

    return functions, writer.source(), tuple(diagnostics)


def _walk_scope(
    node: ScopeNode,
    dispatched: DispatchedProgram,
    block_by_index: dict[int, ResolvedBlock],
    handlers: dict,
    ctx: EmitContext,
    writer: IndentWriter,
    functions: list[str],
    diagnostics: list[Diagnostic],
) -> None:
    """Recursively walk scope tree and emit code."""
    if node.kind == "program":
        # Top level: walk children
        for child in node.children:
            _walk_scope(
                child,
                dispatched,
                block_by_index,
                handlers,
                ctx,
                writer,
                functions,
                diagnostics,
            )

    elif node.kind == "macro":
        # {START-MACRO}: row-iter macros emit a for-loop; static-vars emit a with-block
        payload = node.control_payload
        if isinstance(payload, StartMacro):
            row_iter = bool(payload.csv_path)
            if row_iter:
                register_utility_emission(ctx, "csv_io", "macro")
                iter_call = emit_call(CsvIO.iter, PyExpr.literal(payload.csv_path))
                scope_call = emit_call(
                    PipelineContext.macro_scope, PyExpr.raw("__row")
                )
                writer.write(f"for __row in {iter_call.render()}:")
                writer.push_indent()
                writer.write(f"with {scope_call.render()}:")
                writer.push_indent()
            else:
                scope_call = emit_call(PipelineContext.macro_scope)
                writer.write(f"with {scope_call.render()}:")
                writer.push_indent()
            for child in node.children:
                _walk_scope(
                    child,
                    dispatched,
                    block_by_index,
                    handlers,
                    ctx,
                    writer,
                    functions,
                    diagnostics,
                )
            if row_iter:
                writer.pop_indent()
            writer.pop_indent()

    elif node.kind == "loop":
        # {RUN-LOOP}: emit a chunked for-loop over the input CSV.
        payload = node.control_payload
        if isinstance(payload, RunLoop):
            register_utility_emission(ctx, "csv_io", "macro")
            chunks_call = emit_call(
                CsvIO.iter_chunks,
                PyExpr.literal(payload.input_csv_path),
                PyExpr.literal(payload.chunk_csv_path),
                PyExpr.literal(int(payload.chunk_size)),
            )
            writer.write(f"for __chunk_path in {chunks_call.render()}:")
            writer.push_indent()
            for child in node.children:
                _walk_scope(
                    child,
                    dispatched,
                    block_by_index,
                    handlers,
                    ctx,
                    writer,
                    functions,
                    diagnostics,
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
                    child,
                    dispatched,
                    block_by_index,
                    handlers,
                    ctx,
                    writer,
                    functions,
                    diagnostics,
                )
        writer.pop_indent()

        if else_branch:
            writer.write("else:")
            writer.push_indent()
            for child in else_branch.children:
                _walk_scope(
                    child,
                    dispatched,
                    block_by_index,
                    handlers,
                    ctx,
                    writer,
                    functions,
                    diagnostics,
                )
            writer.pop_indent()

    elif node.kind in ("if-branch", "else-branch"):
        # These are containers; walk their children without emitting anything
        for child in node.children:
            _walk_scope(
                child,
                dispatched,
                block_by_index,
                handlers,
                ctx,
                writer,
                functions,
                diagnostics,
            )

    elif node.kind == "leaf":
        # A content block: dispatch to handler
        block_index = node.block_index
        if block_index is None:
            return

        block = block_by_index.get(block_index)
        if block is None:
            return

        if block.kind is Kind.MACRO_CONTROL:
            payload = block.control_payload
            if isinstance(payload, RowsInFile):
                register_utility_emission(ctx, "csv_io", "macro")
                func_name = f"step_{block.parsed.index:04d}_rows_in_file"
                row_count_call = emit_call(
                    CsvIO.row_count, PyExpr.literal(payload.csv_path)
                )
                set_named_call = emit_call(
                    MacroState.set_named,
                    PyExpr.literal(payload.var_name.upper()),
                    PyExpr.raw(f"str({row_count_call.render()})"),
                )
                fdef = FunctionDef.from_call(func_name, set_named_call)
                functions.append(fdef.source)
                writer.write(fdef.call_site)
            return

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
        except Exception as exc:
            diagnostics.append(
                Diagnostic(
                    severity="error",
                    code="emit-handler-failed",
                    message=f"Handler failed for block {block.parsed.index}: {exc}",
                    block_index=block.parsed.index,
                    span=block.parsed.span,
                )
            )
