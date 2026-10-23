"""Project individual compiler blocks and controls into semantic operations."""

from __future__ import annotations

from collections import defaultdict
from typing import TYPE_CHECKING, Any, Literal

from vg2c.emitter.models import SourceRange
from vg2c.kind import Kind
from vg2c.operands import ForLoop, IfThen, RunLoop, ScopeNode, SiteLoop, StartMacro
from vg2c.operands.base import _OPERATOR_TABLE
from vg2c.semantic.models import EditableBinding, WorkflowOperation
from vg2c.semantic.sql_bindings import sql_file_list_bindings
from vg2c.utilities._emit_helpers import resolve_output_path, split_utility_command
from vg2c.utilities._runtime_helpers import strip_quotes
from vg2c.utility_metadata import FileEffectDefinition, ValueSchema

if TYPE_CHECKING:
    from vg2c.compilation import CompilationResult


def project_control_operation(
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
            file_effects=(),
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
            file_effects=(FileEffectDefinition(
                "macro-input", "read", inputs=("csv_path",),
                reason="Macro rows are loaded from this file.",
            ),) if binding.value else (),
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
            file_effects=(FileEffectDefinition(
                "chunks", "transform", inputs=("input_csv_path",),
                outputs=("chunk_csv_path",), in_loop=True,
                reason="Each iteration reads the input and materializes the current chunk file.",
            ),),
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
            branch=branch, source_span=block.span, bindings=(binding,), block_index=block.index,
            source_range=source_range, file_effects=())

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
        file_effects=(FileEffectDefinition(
            "unknown", "unknown",
            reason="No declared file effects are available for this operation.",
        ),),
    )


def project_leaf_operations(
    result: CompilationResult,
    block,
    parent_operation_id: str | None,
    branch: Literal["true", "false"] | None,
    values: dict[str, Any],
) -> list[WorkflowOperation]:
    step = result.emitted.step_for_block(block.index)

    submission = next(
        (item for item in result.resolved.csr_submissions if item.block_index == block.index),
        None,
    )
    if submission is not None:
        producer = next(
            item for item in result.resolved.blocks if item.index == submission.producer_index
        )
        operation_id = f"block-{block.index}:csr-submission"
        bindings = tuple(
            EditableBinding(
                id=f"{operation_id}:{name}", owner_operation_id=operation_id,
                name=name, display_label=label, schema=ValueSchema("string", path=True),
                value=value, default=value, editable=False,
                read_only_reason="Edit the generated Python or regenerate from the VG2 source.",
                capabilities=(capability,), source_kind="csr",
            )
            for name, label, value, capability in (
                ("input_file", "CSR requests", resolve_output_path(producer), "file-input"),
                ("output_file", "AED results", "AED_results.csv", "file-output"),
            )
        )
        return [WorkflowOperation(
            id=operation_id, kind="csr-submission", display_name="CSR → AED",
            parent_operation_id=parent_operation_id, branch=branch,
            source_span=block.span, summary=f"{resolve_output_path(producer)} → AED_results.csv",
            bindings=bindings, capabilities=("file-input", "file-output"),
            block_index=block.index, source_range=step.source_range if step else None,
            file_effects=(
                FileEffectDefinition(
                    "read", "read", inputs=("input_file",), input_base="script-directory",
                    reason="Read CSR requests; write results when an action is attempted.",
                ),
                FileEffectDefinition(
                    "write", "write", outputs=("output_file",), conditional=True,
                    reason="Read CSR requests; write results when an action is attempted.",
                ),
            ),
        )]

    if block.kind is Kind.ROWS_IN_FILE:
        return [_rows_in_file_operation(result, block, step, parent_operation_id, branch, values)]

    if block.kind is Kind.PYTHON_EMBED:
        operation_id = f"block-{block.index}:embedded-python"
        binding_id = f"{operation_id}:source"
        return [
            WorkflowOperation(
                id=operation_id,
                kind="embedded-python",
                display_name="Embedded Python",
                parent_operation_id=parent_operation_id,
                branch=branch,
                source_span=block.span,
                bindings=(
                    EditableBinding(
                        id=binding_id,
                        owner_operation_id=operation_id,
                        name="source",
                        display_label="Python source",
                        schema=ValueSchema("string"),
                        value=values.get(binding_id, block.resolved_body),
                        default=block.resolved_body,
                        capabilities=("embedded-python",),
                        source_kind="embedded-python",
                    ),
                ),
                capabilities=("embedded-python",),
                block_index=block.index,
                source_range=step.source_range if step else None,
                file_effects=(FileEffectDefinition(
                    "unknown", "unknown",
                    reason="Embedded Python may perform arbitrary file operations.",
                ),),
            )
        ]

    operations: list[WorkflowOperation] = []
    if step is not None:
        for invocation in step.invocations:
            definition = invocation.operation
            if definition.visibility == "internal":
                continue
            bindings = []
            for parameter in invocation.parameters:
                definition_parameter = parameter.definition
                if (
                    definition_parameter is not None
                    and definition_parameter.visibility == "internal"
                ):
                    continue
                binding_value = values.get(parameter.id, parameter.value)
                capabilities = list(
                    definition_parameter.capabilities if definition_parameter else ()
                )
                if parameter.editor_type == "multiline":
                    capabilities.append("multiline")
                if parameter.artifact_role is not None:
                    capabilities.append(f"file-{parameter.artifact_role.direction}")
                bindings.append(
                    EditableBinding(
                        id=parameter.id,
                        owner_operation_id=invocation.id,
                        name=parameter.name,
                        display_label=(
                            definition_parameter.display_label
                            if definition_parameter else parameter.name.replace("_", " ").title()
                        ),
                        schema=definition_parameter.schema if definition_parameter else None,
                        value=binding_value,
                        default=parameter.value,
                        required=definition_parameter.required if definition_parameter else True,
                        visibility=(
                            definition_parameter.visibility if definition_parameter else "normal"
                        ),
                        capabilities=tuple(dict.fromkeys(capabilities)),
                        validation_state="valid" if parameter.editable else "warning",
                        resettable=(
                            parameter.source_range is None
                            or not parameter.id.startswith("global:")
                        ),
                        editable=parameter.editable,
                        read_only_reason=parameter.read_only_reason,
                        source_range=parameter.source_range,
                        source_kind="parameter",
                    )
                )
            if definition.id == "ctx.run_query":
                bindings.extend(
                    sql_file_list_bindings(block, invocation, values)
                )
            operations.append(
                WorkflowOperation(
                    id=invocation.id,
                    kind=definition.id,
                    display_name=definition.display_name,
                    parent_operation_id=parent_operation_id,
                    branch=branch,
                    source_span=block.span,
                    summary=_utility_summary(definition.summary_template, bindings),
                    bindings=tuple(bindings),
                    capabilities=(
                        (*definition.capabilities, "html-preview")
                        if definition.id in {"ctx.write_file", "fs_ops.write_file"}
                        and any(
                            item.name == "path"
                            and isinstance(item.value, str)
                            and item.value.lower().endswith((".html", ".htm"))
                            for item in bindings
                        )
                        else definition.capabilities
                    ),
                    visibility=definition.visibility,
                    block_index=block.index,
                    source_range=invocation.source_range,
                    file_effects=definition.file_effects,
                )
            )

    if operations:
        return operations

    return [
        WorkflowOperation(
            id=f"block-{block.index}:unsupported",
            kind="unsupported",
            display_name="Unsupported / Unknown",
            parent_operation_id=parent_operation_id,
            branch=branch,
            source_span=block.span,
            validation_state="unsupported",
            block_index=block.index,
            source_range=step.source_range if step else None,
            file_effects=(FileEffectDefinition(
                "unknown", "unknown",
                reason="No declared file effects are available for this operation.",
            ),),
        )
    ]


def _rows_in_file_operation(
    result: CompilationResult,
    block,
    step,
    parent_operation_id: str | None,
    branch: Literal["true", "false"] | None,
    values: dict[str, Any],
) -> WorkflowOperation:
    operation_id = f"block-{block.index}:rows-in-file"
    argv = split_utility_command(block.resolved_options.lookup.get("UTILITIES", ""))
    path = strip_quotes(argv[1]) if len(argv) > 1 else ""
    target = strip_quotes(argv[2]).upper() if len(argv) > 2 else ""

    target_parameter = None
    if step is not None:
        target_parameter = next(
            (
                parameter
                for invocation in step.invocations
                for parameter in invocation.parameters
                if invocation.operation.id == "macro.set_named" and parameter.name == "name"
            ),
            None,
        )
    target_id = target_parameter.id if target_parameter is not None else f"{operation_id}:target"
    path_id = f"{operation_id}:path"
    path_range = _row_count_path_range(result.emitted.source, step.source_range if step else None, path)
    bindings = (
        EditableBinding(
            id=path_id,
            owner_operation_id=operation_id,
            name="path",
            display_label="Input file",
            schema=ValueSchema("string", path=True),
            value=values.get(path_id, path),
            default=path,
            capabilities=("file-input",),
            source_range=path_range,
            source_kind="rows-in-file",
        ),
        EditableBinding(
            id=target_id,
            owner_operation_id=operation_id,
            name="target",
            display_label="Result variable",
            schema=ValueSchema("string"),
            value=values.get(target_id, target),
            default=target,
            capabilities=("symbol-definition",),
            source_range=target_parameter.source_range if target_parameter else None,
            source_kind="parameter" if target_parameter else "rows-in-file",
        ),
    )
    return WorkflowOperation(
        id=operation_id,
        kind="check-row-count",
        display_name="Check Row Count",
        parent_operation_id=parent_operation_id,
        branch=branch,
        source_span=block.span,
        summary=f"{_summary_value(bindings[0].value)} → {_summary_value(bindings[1].value)}",
        bindings=bindings,
        capabilities=("file-input", "symbol-definition"),
        block_index=block.index,
        source_range=step.source_range if step else None,
        file_effects=(FileEffectDefinition(
            "row-count", "observe", inputs=("path",),
            reason="Row count observes this input file and stores the count in a symbol.",
        ),),
    )


def _summary_value(value: Any) -> str:
    if value is None:
        return ""
    if isinstance(value, (list, tuple)):
        text = ", ".join(str(item) for item in value)
    else:
        text = str(value)
    text = " ".join(text.split())
    return text if len(text) <= 80 else text[:77] + "…"


def _utility_summary(template: str | None, bindings: list[EditableBinding]) -> str:
    by_name = {binding.name: _summary_value(binding.value) for binding in bindings}
    if template:
        return template.format_map(defaultdict(str, by_name))
    file_binding = next(
        (
            binding
            for binding in bindings
            if any(cap.startswith("file-") for cap in binding.capabilities)
        ),
        None,
    )
    if file_binding is None:
        return ""
    return f"{file_binding.display_label}: {_summary_value(file_binding.value)}"


def _condition_summary(bindings: list[EditableBinding]) -> str:
    by_name = {binding.name: binding.value for binding in bindings}

    def clause(left: str, op: str, right: str) -> str:
        if not by_name.get(left) or not by_name.get(op):
            return ""
        operator = _OPERATOR_TABLE.get(str(by_name[op]), (str(by_name[op]), "string"))[0]
        return f"{_summary_value(by_name[left])} {operator} {_summary_value(by_name.get(right))}"

    first = clause("lhs", "op", "rhs")
    second = clause("lhs2", "op2", "rhs2")
    return f"{first} {by_name['conj']} {second}" if second and by_name.get("conj") else first


def _row_count_path_range(
    source: str, step_range: SourceRange | None, path: str
) -> SourceRange | None:
    if step_range is None:
        return None
    segment = source[step_range.start_offset : step_range.end_offset]
    marker = "row_count("
    call = segment.find(marker)
    if call < 0:
        return None
    literal = repr(path)
    start = segment.find(literal, call + len(marker))
    if start < 0:
        return None
    absolute = step_range.start_offset + start
    return SourceRange(absolute, absolute + len(literal))


def _control_operation_id(node: ScopeNode) -> str:
    suffix = {"if": "condition", "macro": "macro", "loop": "loop"}.get(node.kind, node.kind)
    return f"block-{node.start_index}:control:{suffix}"
