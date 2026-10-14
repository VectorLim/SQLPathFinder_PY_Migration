"""Scope traversal that assembles semantic operations and symbols."""

from __future__ import annotations

from dataclasses import replace
from typing import TYPE_CHECKING, Any

from vg2c.semantic.models import EditableBinding, Symbol, WorkflowOperation
from vg2c.semantic.operations import project_control_operation, project_leaf_operations
from vg2c.semantic.symbols import (
    apply_condition_symbol_validation,
    build_symbols,
)
from vg2c.emitter.models import SourceRange
from vg2c.operands import ScopeNode

if TYPE_CHECKING:
    from vg2c.compilation import CompilationResult


def project_semantics(
    result: CompilationResult,
    values: dict[str, Any] | None = None,
) -> tuple[
    tuple[WorkflowOperation, ...],
    tuple[EditableBinding, ...],
    tuple[Symbol, ...],
]:
    """Build semantic primitives used by editing and the effective document projection."""
    values = values or {}
    block_by_index = {block.index: block for block in result.resolved.blocks}
    control_ranges = _control_source_ranges(result)

    operations: list[WorkflowOperation] = []
    handled_blocks: set[int] = set()

    def visit(node: ScopeNode, parent_operation: str | None = None, branch=None) -> None:
        next_parent = parent_operation
        next_branch = branch
        if node.kind in {"if", "macro", "loop"}:
            operation = project_control_operation(
                result,
                node,
                block_by_index[node.start_index],
                parent_operation,
                branch,
                values,
                control_ranges.get(node.scope_id),
            )
            operation = replace(
                operation,
                scope_id=node.scope_id,
                comments=_source_comments(block_by_index[node.start_index].raw),
            )
            operations.append(operation)
            handled_blocks.add(node.start_index)
            next_parent = operation.id
            next_branch = None
        elif node.kind == "if-branch":
            next_branch = "true"
        elif node.kind == "else-branch":
            next_branch = "false"
        elif node.kind == "leaf" and node.block_index is not None:
            block = block_by_index.get(node.block_index)
            if block is not None:
                leaf_ops = project_leaf_operations(
                    result,
                    block,
                    parent_operation,
                    branch,
                    values,
                )
                leaf_ops = [
                    replace(operation, scope_id=node.scope_id)
                    for operation in leaf_ops
                ]
                if leaf_ops:
                    leaf_ops[0] = replace(leaf_ops[0], comments=_source_comments(block.raw))
                operations.extend(leaf_ops)
                if leaf_ops:
                    handled_blocks.add(block.index)
        for child in node.children:
            visit(child, next_parent, next_branch)

    visit(result.resolved.scope_tree)

    # Orphan/unsupported source blocks can be absent from the structural walk.
    for block in result.resolved.blocks:
        if block.index in handled_blocks:
            continue
        orphan_ops = [
            replace(operation, scope_id=block.scope_id)
            for operation in project_leaf_operations(result, block, None, None, values)
        ]
        if orphan_ops:
            orphan_ops[0] = replace(orphan_ops[0], comments=_source_comments(block.raw))
        operations.extend(orphan_ops)

    bindings = tuple(binding for operation in operations for binding in operation.bindings)
    symbols = build_symbols(result, operations, values)
    operations = apply_condition_symbol_validation(operations, symbols)
    bindings = tuple(binding for operation in operations for binding in operation.bindings)
    return tuple(operations), bindings, symbols


def _source_comments(raw: str) -> tuple[str, ...]:
    """Attach only comments present in the original VG2 source block."""
    comments: list[str] = []
    for line in raw.splitlines():
        text = line.strip()
        if text.startswith("#"):
            comment = text[1:].strip()
        elif text.startswith("--"):
            comment = text[2:].strip()
        else:
            continue
        if comment:
            comments.append(comment)
    return tuple(comments)


def _control_source_ranges(result: CompilationResult) -> dict[int, SourceRange]:
    """Locate control headers in emitted workflow order without parsing generated Python."""
    source = result.emitted.source
    marker = source.find("# <vg2c:workflow:start>")
    cursor = marker if marker >= 0 else 0
    ranges: dict[int, SourceRange] = {}

    def visit(node: ScopeNode, macros="macros") -> None:
        nonlocal cursor
        if node.kind in {"if", "macro", "loop"} and node.control_payload is not None:
            header = node.control_payload.render_header(node.scope_id, macros=macros)
            start = source.find(header, cursor)
            if start >= 0:
                ranges[node.scope_id] = SourceRange(start, start + len(header))
                cursor = start + len(header)
        for child in node.children:
            visit(child, macros)

    visit(result.resolved.scope_tree)
    return ranges
