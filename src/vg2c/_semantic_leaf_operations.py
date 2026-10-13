"""Projection of leaf blocks and emitted calls into semantic operations."""

from __future__ import annotations

import ast
from collections import defaultdict
from typing import TYPE_CHECKING, Any, Literal

from vg2c.emitter.models import SourceRange
from vg2c.kind import Kind
from vg2c.operands.base import _OPERATOR_TABLE
from vg2c.semantic_models import EditableBinding, WorkflowOperation
from vg2c.utilities._emit_helpers import scan_sql_get_csv_list_calls, split_utility_command
from vg2c.utilities._runtime_helpers import strip_quotes
from vg2c.utility_metadata import ValueSchema

if TYPE_CHECKING:
    from vg2c.compilation import CompilationResult

def _leaf_operations(
    result: CompilationResult,
    block,
    parent_operation_id: str | None,
    branch: Literal["true", "false"] | None,
    values: dict[str, Any],
) -> list[WorkflowOperation]:
    step = result.emitted.step_for_block(block.index)

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
                    _sql_file_list_bindings(block, invocation, values)
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
        )
    ]


def _sql_file_list_bindings(block, invocation, values: dict[str, Any]) -> list[EditableBinding]:
    """Bind proven source calls to their emitted Python path literals."""
    calls = scan_sql_get_csv_list_calls(block.resolved_body)
    sql = next((item for item in invocation.parameters if item.name == "sql"), None)
    if not calls or sql is None or sql.source_range is None:
        return []
    try:
        expression = ast.parse(sql.source, mode="eval")
    except SyntaxError:
        return []
    generated = sorted(
        (
            node for node in ast.walk(expression)
            if isinstance(node, ast.Call)
            and isinstance(node.func, ast.Attribute)
            and ast.unparse(node.func) == "ctx.csv_io.sql_get_csv_list"
            and node.args
            and isinstance(node.args[0], ast.Constant)
            and isinstance(node.args[0].value, str)
        ),
        key=lambda node: (node.lineno, node.col_offset),
    )
    if len(generated) != len(calls):
        return []
    def normalize(path: str) -> str:
        return path.replace("\\", "/").removeprefix("./")

    bindings = []
    for index, (call, emitted_call) in enumerate(zip(calls, generated, strict=True)):
        if "'" in call.csv_path or '"' in call.csv_path:
            return []
        literal = emitted_call.args[0]
        generated_path = literal.value
        if normalize(generated_path) != normalize(call.source_path):
            return []
        start = _ast_offset(sql.source, literal.lineno, literal.col_offset)
        end = _ast_offset(sql.source, literal.end_lineno, literal.end_col_offset)
        source_range = SourceRange(
            sql.source_range.start_offset + start,
            sql.source_range.start_offset + end,
        )
        binding_id = f"{invocation.id}:sql-file-list:{index}"
        bindings.append(EditableBinding(
            id=binding_id,
            owner_operation_id=invocation.id,
            name=f"sql_file_list_{index + 1}",
            display_label=f"File list {index + 1}",
            schema=ValueSchema("string", path=True),
            value=values.get(binding_id, call.source_path),
            default=call.source_path,
            visibility="internal",
            capabilities=("file-input", "sql-file-list"),
            source_range=source_range,
            source_kind="sql-file-list",
        ))
    return bindings


def _ast_offset(source: str, line_number: int, byte_column: int) -> int:
    lines = source.splitlines(keepends=True)
    return sum(map(len, lines[:line_number - 1])) + len(
        lines[line_number - 1].encode("utf-8")[:byte_column].decode("utf-8")
    )


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
