from __future__ import annotations

from typing import Any

from vg2c.logger import Logger
from vg2c.dispatch.models import DispatchedProgram
from vg2c.emitter.indent_writer import IndentWriter
from vg2c.utilities._base import UtilitySpec
from vg2c.resolver.models import ResolvedBlock
from vg2c.operands import ScopeNode

__all__ = ["walk_and_emit"]

log = Logger.getLogger("vg2c.emitter.walker")


def walk_and_emit(
    dispatched: DispatchedProgram,
) -> tuple[list[str], str]:
    """Walk scope tree and emit Python code.

    Returns:
        (functions_list, run_body_source) where functions_list is
        the emitted helper function definitions, and run_body_source is the main
        run() body.
    """
    block_by_index = {b.index: b for b in dispatched.analyzed.resolved.blocks}
    dispatch_map = {db.index: db for db in dispatched.dispatched}

    functions: list[str] = []
    writer = IndentWriter()

    _walk_scope(
        dispatched.analyzed.resolved.scope_tree,
        dispatch_map,
        block_by_index,
        writer,
        functions,
    )

    return functions, writer.source()


def _walk_scope(
    node: ScopeNode,
    dispatch_map: dict[int, Any],
    block_by_index: dict[int, ResolvedBlock],
    writer: IndentWriter,
    functions: list[str],
) -> None:
    """Recursively walk scope tree and emit code.

    Leaf nodes are dispatched to UtilitySpec handlers; all other node kinds
    delegate structural emission to their control payload via ScopeNode.emit.
    """

    def walk(child: ScopeNode) -> None:
        _walk_scope(child, dispatch_map, block_by_index, writer, functions)

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
            loc = f"{block.span.file or '<input>'}:{block.span.start_line}:1"
            log.error(
                f"[emit-handler-failed] {loc} (block {block.index}): "
                f"Handler failed for block {block.index}: {exc}"
            )
    else:
        node.emit(writer, walk)
