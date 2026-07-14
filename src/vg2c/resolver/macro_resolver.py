from __future__ import annotations

from vg2c.frontend.models import (
    ClassifiedBlock,
    Diagnostic,
)
from vg2c.resolver.models import ResolvedBlock
from vg2c.resolver.operands import MacroControlPayload, ScopeNode


def resolve_macros(
    blocks: list[ClassifiedBlock],
    scope_tree: ScopeNode,
) -> tuple[list[ResolvedBlock], list[Diagnostic]]:
    diagnostics: list[Diagnostic] = []
    indices = {block.index for block in blocks}

    scope_for_block = _build_scope_lookup(scope_tree, indices)
    payload_by_index = _build_payload_lookup(scope_tree, indices)

    resolved: list[ResolvedBlock] = []
    for block in blocks:
        payload = payload_by_index.get(block.index)
        resolved.append(
            ResolvedBlock(
                classified=block,
                resolved_options=block.options,
                resolved_body=block.body,
                sql_macro_calls=(),
                control_payload=payload,
                scope_id=scope_for_block.get(block.index, 0),
            )
        )

    return resolved, diagnostics


def _build_scope_lookup(scope_tree: ScopeNode, indices: set[int]) -> dict[int, int]:
    mapping: dict[int, int] = {idx: 0 for idx in indices}

    def visit(node: ScopeNode) -> None:
        if node.kind == "leaf" and node.block_index is not None:
            mapping[node.block_index] = node.scope_id
        for child in node.children:
            visit(child)

    visit(scope_tree)

    # Include control boundary indices that are not explicit leaves.
    for idx in indices:
        if mapping.get(idx, 0) != 0:
            continue
        mapping[idx] = _deepest_scope_containing(scope_tree, idx)
    return mapping


def _deepest_scope_containing(node: ScopeNode, idx: int, best: int = 0) -> int:
    if node.start_index <= idx <= node.end_index:
        best = node.scope_id
        for child in node.children:
            best = _deepest_scope_containing(child, idx, best)
    return best


def _build_payload_lookup(
    scope_tree: ScopeNode,
    indices: set[int],
) -> dict[int, MacroControlPayload | None]:
    mapping: dict[int, MacroControlPayload | None] = {}

    def visit(node: ScopeNode) -> None:
        # Leaf payloads include ROWS-IN-FILE and orphan control tokens.
        if (
            node.kind == "leaf"
            and node.block_index is not None
            and node.control_payload is not None
        ):
            if node.block_index in indices:
                mapping[node.block_index] = node.control_payload

        # Structural payloads belong to opener indices represented by the node.
        if (
            node.kind in {"macro", "loop", "if", "else-branch"}
            and node.control_payload is not None
            and node.start_index in indices
        ):
            mapping[node.start_index] = node.control_payload

        for child in node.children:
            visit(child)

    visit(scope_tree)
    return mapping
