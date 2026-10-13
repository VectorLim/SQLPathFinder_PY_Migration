"""Projection of native control scopes into semantic operations."""

from __future__ import annotations

from typing import TYPE_CHECKING, Any, Literal

from vg2c.emitter.models import SourceRange
from vg2c.operands import IfThen, RunLoop, ForLoop, SiteLoop, ScopeNode, StartMacro
from vg2c.operands.base import _OPERATOR_TABLE
from vg2c.semantic_models import EditableBinding, WorkflowOperation
from vg2c._semantic_leaf_operations import _condition_summary, _summary_value
from vg2c.utility_metadata import ValueSchema

if TYPE_CHECKING:
    from vg2c.compilation import CompilationResult

def _control_operation(
    result: CompilationResult,
    node: ScopeNode,
    block,
    parent_operation_id: str | None,
    branch: Literal["true", "false"] | None,
    values: dict[str, Any],
    source_range: SourceRange | None,
) -> WorkflowOperation:
    payload = node.control_payload
    operation_id = _control_operation_id(node)

    if isinstance(payload, IfThen):
        names = ("lhs", "op", "rhs", "conj", "lhs2", "op2", "rhs2")
        labels = {
            "lhs": "Left value",
            "op": "Operator",
            "rhs": "Comparison value",
            "conj": "Connector",
            "lhs2": "Second left value",
            "op2": "Second operator",
            "rhs2": "Second comparison value",
        }
        bindings = []
        for name in names:
            binding_id = f"{operation_id}:{name}"
            value = values.get(binding_id, getattr(payload, name))
            if name in {"op", "op2"}:
                schema = ValueSchema(
                    "string",
                    nullable=name == "op2",
                    choices=tuple(_OPERATOR_TABLE),
                )
            elif name == "conj":
                schema = ValueSchema("string", nullable=True, choices=("AND", "OR"))
            else:
                schema = ValueSchema("string", nullable=name.endswith("2"))
            capability = (
                "condition-operator"
                if name in {"op", "op2"}
                else "condition-connector"
                if name == "conj"
                else "symbol-or-literal"
            )
            bindings.append(
                EditableBinding(
                    id=binding_id,
                    owner_operation_id=operation_id,
                    name=name,
                    display_label=labels[name],
                    schema=schema,
                    value=value,
                    default=getattr(payload, name),
                    required=name in {"lhs", "op", "rhs"},
                    capabilities=(capability,),
                    source_kind="condition",
                )
            )
        return WorkflowOperation(
            id=operation_id,
            kind="condition",
            display_name="IF",
            parent_operation_id=parent_operation_id,
            branch=branch,
            source_span=block.span,
            summary=_condition_summary(bindings),
            bindings=tuple(bindings),
            capabilities=("condition-editor",),
            block_index=block.index,
            source_range=source_range,
        )

    if isinstance(payload, StartMacro):
        binding_id = f"{operation_id}:csv_path"
        binding = EditableBinding(
            id=binding_id,
            owner_operation_id=operation_id,
            name="csv_path",
            display_label="Input file",
            schema=ValueSchema("string", path=True),
            value=values.get(binding_id, payload.csv_path),
            default=payload.csv_path,
            capabilities=("file-input",),
            source_kind="macro",
        )
        return WorkflowOperation(
            id=operation_id,
            kind="macro-loop",
            display_name="Use First Macro Row",
            parent_operation_id=parent_operation_id,
            branch=branch,
            source_span=block.span,
            summary=_summary_value(binding.value),
            bindings=(binding,),
            capabilities=("file-input",),
            block_index=block.index,
            source_range=source_range,
        )

    if isinstance(payload, RunLoop):
        specs = (
            ("input_csv_path", "Input file", ValueSchema("string", path=True), ("file-input",)),
            ("chunk_csv_path", "Chunk file", ValueSchema("string", path=True), ("file-output",)),
            ("chunk_size", "Chunk size", ValueSchema("integer"), ()),
        )
        bindings = tuple(
            EditableBinding(
                id=f"{operation_id}:{name}",
                owner_operation_id=operation_id,
                name=name,
                display_label=label,
                schema=schema,
                value=values.get(f"{operation_id}:{name}", getattr(payload, name)),
                default=getattr(payload, name),
                capabilities=capabilities,
                source_kind="loop",
            )
            for name, label, schema, capabilities in specs
        )
        return WorkflowOperation(
            id=operation_id,
            kind="chunk-loop",
            display_name="For Each Chunk",
            parent_operation_id=parent_operation_id,
            branch=branch,
            source_span=block.span,
            summary=f"{_summary_value(bindings[0].value)} · {bindings[2].value} rows",
            bindings=bindings,
            capabilities=("file-input", "file-output"),
            block_index=block.index,
            source_range=source_range,
        )

    if isinstance(payload, (ForLoop, SiteLoop)):
        name = "args" if isinstance(payload, ForLoop) else "nodes"
        value = list(payload.args) if isinstance(payload, ForLoop) else payload.nodes
        binding = EditableBinding(
            id=f"{operation_id}:{name}", owner_operation_id=operation_id, name=name, display_label="Loop values",
            schema=ValueSchema("list", items=ValueSchema("string")) if isinstance(payload, ForLoop) else ValueSchema("string"),
            value=value, default=value, editable=False,
            read_only_reason="Regenerate from source to change the native FOR/SITE loop contract.", source_kind="loop")
        return WorkflowOperation(
            id=operation_id, kind="for-loop" if isinstance(payload, ForLoop) else "site-loop",
            display_name="FOR" if isinstance(payload, ForLoop) else "SITE", parent_operation_id=parent_operation_id,
            branch=branch, source_span=block.span, bindings=(binding,), block_index=block.index, source_range=source_range)

    return WorkflowOperation(
        id=operation_id,
        kind="unsupported",
        display_name="Unsupported / Unknown",
        parent_operation_id=parent_operation_id,
        branch=branch,
        source_span=block.span,
        validation_state="unsupported",
        block_index=block.index,
        source_range=source_range,
    )

def _control_operation_id(node: ScopeNode) -> str:
    suffix = {"if": "condition", "macro": "macro", "loop": "loop"}.get(node.kind, node.kind)
    return f"block-{node.start_index}:control:{suffix}"


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
