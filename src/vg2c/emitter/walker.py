from __future__ import annotations

from typing import Any

from vg2c.dispatch.models import DispatchedProgram
from vg2c.emitter.models import IndentWriter
from vg2c.emitter.utilities._base import UtilitySpec
from vg2c.frontend.models import Diagnostic
from vg2c.resolver.models import ResolvedBlock
from vg2c.operands import ScopeNode

__all__ = ["walk_and_emit"]


def walk_and_emit(
    dispatched: DispatchedProgram,
) -> tuple[list[str], str, tuple[Diagnostic, ...]]:
    """Walk scope tree and emit Python code.

    Returns:
        (functions_list, run_body_source, diagnostics) where functions_list is
        the emitted helper function definitions, and run_body_source is the main
        run() body.
    """
    block_by_index = {b.index: b for b in dispatched.analyzed.resolved.blocks}
    dispatch_map = {db.index: db for db in dispatched.dispatched}

    functions: list[str] = []
    diagnostics: list[Diagnostic] = []
    writer = IndentWriter()

    _walk_scope(
        dispatched.analyzed.resolved.scope_tree,
        dispatch_map,
        block_by_index,
        writer,
        functions,
        diagnostics,
    )

    return functions, writer.source(), tuple(diagnostics)


def _walk_scope(
    node: ScopeNode,
    dispatch_map: dict[int, Any],
    block_by_index: dict[int, ResolvedBlock],
    writer: IndentWriter,
    functions: list[str],
    diagnostics: list[Diagnostic],
) -> None:
    """Recursively walk scope tree and emit code.

    Leaf nodes are dispatched to UtilitySpec handlers; all other node kinds
    delegate structural emission to their control payload via ScopeNode.emit.
    """

    def walk(child: ScopeNode) -> None:
        _walk_scope(child, dispatch_map, block_by_index, writer, functions, diagnostics)

    if node.kind == "leaf":
        block_index = node.block_index
        if block_index is None:
            return

        block = dispatch_map.get(block_index) or block_by_index.get(block_index)
        if block is None:
            return

        try:
            func_code, call_site = UtilitySpec.dispatch_and_emit(block)
            functions.append(func_code)
            writer.write(call_site)
        except Exception as exc:
            diagnostics.append(
                Diagnostic(
                    severity="error",
                    code="emit-handler-failed",
                    message=f"Handler failed for block {block.index}: {exc}",
                    block_index=block.index,
                    span=block.span,
                )
            )
    else:
        node.emit(writer, walk)
