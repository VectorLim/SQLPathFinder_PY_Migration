"""Reorder feasibility derived from scope structure and projected file effects."""

from __future__ import annotations

from pathlib import Path

from vg2c.compilation import CompilationResult
from vg2c.dataflow.file_effects import FileEffect, endpoint_keys
from vg2c.semantic_models import WorkflowOperation

def _reorder_safe_outputs(
    result: CompilationResult,
    operations: tuple[WorkflowOperation, ...],
    effects: tuple[FileEffect, ...],
    output_path: Path,
) -> dict[int, set[str]]:
    """Return independent leaf outputs that are safe candidates for reordering."""
    operations_by_block: dict[int, list[WorkflowOperation]] = {}
    for operation in operations:
        operations_by_block.setdefault(operation.block_index, []).append(operation)
    effects_by_operation: dict[str, list[FileEffect]] = {}
    for effect in effects:
        effects_by_operation.setdefault(effect.operation_id, []).append(effect)

    safe: dict[int, set[str]] = {}
    for node in _walk_scopes(result.resolved.scope_tree):
        if node.kind != "leaf" or node.block_index is None:
            continue
        node_operations = operations_by_block.get(node.block_index, [])
        if len(node_operations) != 1 or node_operations[0].kind != "ctx.write_file":
            continue
        node_effects = effects_by_operation.get(node_operations[0].id, [])
        if len(node_effects) != 1 or node_effects[0].kind != "write":
            continue
        effect = node_effects[0]
        if effect.inputs or not effect.outputs or any(
            endpoint.path is None or endpoint.status != "known"
            for endpoint in effect.outputs
        ):
            continue
        keys = {
            key
            for endpoint in effect.outputs
            for key in endpoint_keys(endpoint, output_path)
        }
        if keys:
            safe[node.scope_id] = keys
    return safe


def _legal_reorder_targets(
    result: CompilationResult,
    safe: dict[int, set[str]],
) -> dict[int, tuple[int, ...]]:
    """Compute adjacent reorder targets from already-projected safe outputs."""
    targets: dict[int, list[int]] = {}
    for parent in _walk_scopes(result.resolved.scope_tree):
        if parent.kind not in {"program", "if-branch", "else-branch"}:
            continue
        for left, right in zip(parent.children, parent.children[1:]):
            if left.scope_id not in safe or right.scope_id not in safe:
                continue
            if safe[left.scope_id] & safe[right.scope_id]:
                continue
            targets.setdefault(left.scope_id, []).append(right.scope_id)
            targets.setdefault(right.scope_id, []).append(left.scope_id)
    return {key: tuple(value) for key, value in targets.items()}


def _walk_scopes(node):
    yield node
    for child in node.children:
        yield from _walk_scopes(child)


def _scope_flags(result: CompilationResult) -> dict[int, tuple[bool, bool]]:
    flags: dict[int, tuple[bool, bool]] = {}

    def visit(node, conditional=False, in_loop=False):
        conditional = conditional or node.kind in {"if-branch", "else-branch"}
        in_loop = in_loop or node.kind in {"macro", "loop"}
        flags[node.scope_id] = conditional, in_loop
        for child in node.children:
            visit(child, conditional, in_loop)

    visit(result.resolved.scope_tree)
    return flags


def _block_scope_id(result: CompilationResult, block_index: int) -> int:
    block = next((item for item in result.resolved.blocks if item.index == block_index), None)
    return block.scope_id if block is not None else 0
