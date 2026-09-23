"""Conservative execution-tree reordering for independent file writes."""

from __future__ import annotations

from dataclasses import dataclass, replace
from typing import Iterable

from vg2c.compilation import CompilationResult
from vg2c.editing import SemanticChange
from vg2c.emitter import emit
from vg2c.operands import ScopeNode
from vg2c.workflow import project_document, _reorder_safe_outputs


@dataclass(frozen=True, slots=True)
class OrderChange:
    parent_scope_id: int
    child_scope_ids: tuple[int, ...]


class InvalidOrderChange(ValueError):
    pass


def _safe_leaf_ids(
    result: CompilationResult, changes: Iterable[SemanticChange]
) -> dict[int, set[str]]:
    document = project_document(result, changes)
    return _reorder_safe_outputs(
        result,
        document.operations,
        document.effects,
        result.input_path.with_suffix(".py"),
    )


def legal_reorder_targets(
    result: CompilationResult,
    changes: Iterable[SemanticChange] = (),
) -> dict[int, tuple[int, ...]]:
    """Return adjacent safe sibling targets from the effective document."""
    document = project_document(result, changes)
    return {
        operation.scope_id: operation.reorder_targets
        for operation in document.operations
        if operation.scope_id is not None and operation.reorder_targets
    }


def swap_adjacent(
    result: CompilationResult,
    source_scope_id: int,
    target_scope_id: int,
    changes: Iterable[SemanticChange] = (),
) -> OrderChange:
    targets = legal_reorder_targets(result, changes)
    if target_scope_id not in targets.get(source_scope_id, ()):
        raise InvalidOrderChange("These operations cannot be reordered safely.")
    for parent in _walk(result.resolved.scope_tree):
        ids = [child.scope_id for child in parent.children]
        if source_scope_id in ids and target_scope_id in ids:
            first, second = ids.index(source_scope_id), ids.index(target_scope_id)
            ids[first], ids[second] = ids[second], ids[first]
            return OrderChange(parent.scope_id, tuple(ids))
    raise InvalidOrderChange("Operations are not siblings.")


def apply_order_changes(
    result: CompilationResult,
    order_changes: Iterable[OrderChange],
    changes: Iterable[SemanticChange] = (),
) -> CompilationResult:
    """Validate saved permutations against source identities and re-emit the run body."""
    overrides = tuple(order_changes)
    if not overrides:
        return result
    if len({item.parent_scope_id for item in overrides}) != len(overrides):
        raise InvalidOrderChange("A scope has more than one saved order.")
    safe = _safe_leaf_ids(result, changes)
    by_parent = {item.parent_scope_id: item for item in overrides}
    seen: set[int] = set()

    def reorder(node: ScopeNode) -> ScopeNode:
        children = tuple(reorder(child) for child in node.children)
        change = by_parent.get(node.scope_id)
        if change is None:
            return replace(node, children=children)
        seen.add(node.scope_id)
        if node.kind not in {"program", "if-branch", "else-branch"}:
            raise InvalidOrderChange("Control and loop scopes cannot be reordered.")
        original = [child.scope_id for child in children]
        requested = change.child_scope_ids
        if len(requested) != len(original) or set(requested) != set(original):
            raise InvalidOrderChange("Saved order no longer matches the source scope.")
        position = {identity: index for index, identity in enumerate(original)}
        for index, identity in enumerate(requested):
            if identity not in safe and index != position[identity]:
                raise InvalidOrderChange("A fixed scope cannot move.")
        for left_index, left_id in enumerate(requested):
            for right_id in requested[left_index + 1:]:
                if position[left_id] < position[right_id]:
                    continue
                if left_id not in safe or right_id not in safe:
                    raise InvalidOrderChange("Reorder crosses a fixed scope.")
                if safe[left_id] & safe[right_id]:
                    raise InvalidOrderChange("Reorder would invert a file dependency.")
        child_by_id = {child.scope_id: child for child in children}
        return replace(node, children=tuple(child_by_id[identity] for identity in requested))

    tree = reorder(result.resolved.scope_tree)
    if seen != set(by_parent):
        raise InvalidOrderChange("Saved order refers to an unknown scope.")
    resolved = replace(result.resolved, scope_tree=tree)
    dispatched = replace(result.dispatched, resolved=resolved)
    return replace(result, dispatched=dispatched, emitted=emit(dispatched))


def _walk(node: ScopeNode):
    yield node
    for child in node.children:
        yield from _walk(child)


__all__ = [
    "OrderChange", "InvalidOrderChange", "apply_order_changes",
    "legal_reorder_targets", "swap_adjacent",
]
