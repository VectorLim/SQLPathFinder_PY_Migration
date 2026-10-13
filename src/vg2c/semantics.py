from __future__ import annotations

from dataclasses import replace
from typing import TYPE_CHECKING, Any

from vg2c.operands import ScopeNode
from vg2c.operands.base import _OPERATOR_TABLE
from vg2c.semantic_models import (
    BindingVisibility,
    EditableBinding,
    OperationReference,
    Symbol,
    SymbolKind,
    SymbolReference,
    SymbolValueState,
    ValidationState,
    WorkflowOperation,
)
from vg2c._semantic_controls import (
    _control_operation,
    _control_operation_id,
    _control_source_ranges,
)
from vg2c._semantic_leaf_operations import (
    _ast_offset,
    _condition_summary,
    _leaf_operations,
    _row_count_path_range,
    _rows_in_file_operation,
    _source_comments,
    _sql_file_list_bindings,
    _summary_value,
    _utility_summary,
)
from vg2c._semantic_symbols import (
    _apply_condition_symbol_validation,
    _build_symbols,
    _csv_headers,
    _symbolic_operand,
)

if TYPE_CHECKING:
    from vg2c.compilation import CompilationResult

def condition_symbol_token(symbol: Symbol, operator: str | None) -> str:
    """Resolve an eligible symbol for the compiler's condition operand syntax."""
    if symbol.kind not in {"macro", "macro-row"}:
        raise ValueError(f"{symbol.display_name} is not a condition symbol.")
    operand_type = _OPERATOR_TABLE.get((operator or "").upper(), ("", "string"))[1]
    return symbol.display_name if operand_type == "numeric" else f"VAR({symbol.display_name})"


CONDITION_OPERATORS = tuple(
    (code, symbol, operand_type)
    for code, (symbol, operand_type) in _OPERATOR_TABLE.items()
)


def _build_semantics(
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
            operation = _control_operation(
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
                leaf_ops = _leaf_operations(
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
            for operation in _leaf_operations(result, block, None, None, values)
        ]
        if orphan_ops:
            orphan_ops[0] = replace(orphan_ops[0], comments=_source_comments(block.raw))
        operations.extend(orphan_ops)

    bindings = tuple(binding for operation in operations for binding in operation.bindings)
    symbols = _build_symbols(result, operations, values)
    operations = _apply_condition_symbol_validation(operations, symbols)
    bindings = tuple(binding for operation in operations for binding in operation.bindings)
    return tuple(operations), bindings, symbols












def _scope_indexes(root: ScopeNode) -> tuple[dict[int, int | None], dict[int, ScopeNode]]:
    parent: dict[int, int | None] = {}
    nodes: dict[int, ScopeNode] = {}
    stack = [(root, None)]
    while stack:
        node, parent_id = stack.pop()
        parent[node.scope_id] = parent_id
        nodes[node.scope_id] = node
        for child in reversed(node.children):
            stack.append((child, node.scope_id))
    return parent, nodes


__all__ = [
    "BindingVisibility",
    "CONDITION_OPERATORS",
    "EditableBinding",
    "OperationReference",
    "Symbol",
    "SymbolReference",
    "ValidationState",
    "WorkflowOperation",
]
