"""Source-rewrite helpers for accepted semantic edits."""

from __future__ import annotations

from dataclasses import replace
from typing import Any

from vg2c.compilation import CompilationResult
from vg2c.emitter.models import CodeExpr, EmittedParameter
from vg2c.emitter.operations import EmittableOperation
from vg2c.operands import IfThen, RunLoop, StartMacro
from vg2c.semantic_models import EditableBinding, WorkflowOperation
from vg2c._editing_validation import _serialize_parameter

def _project_omitted_parameters(
    result: CompilationResult,
    emitted_bindings: dict[str, list[EmittedParameter]],
    semantic_bindings: tuple[EditableBinding, ...],
    values: dict[str, Any],
    replacements: dict[tuple[int, int], str],
) -> None:
    parameter_values = {
        binding_id: value
        for binding_id, value in values.items()
        if binding_id in emitted_bindings
    }
    for step in result.emitted.steps:
        for invocation in step.invocations:
            omitted = [
                parameter
                for parameter in invocation.parameters
                if parameter.source_range is None and parameter.id in parameter_values
            ]
            if not omitted:
                continue
            parameters_by_name = {
                parameter.name: parameter for parameter in invocation.parameters
            }
            args: list[Any] = []
            kwargs: dict[str, Any] = {}
            for argument in invocation.arguments:
                parameter = parameters_by_name[argument.name]
                source = argument.source
                if parameter.name == "sql" and parameter.source_range is not None:
                    nested = [
                        (
                            binding.source_range.start_offset - parameter.source_range.start_offset,
                            binding.source_range.end_offset - parameter.source_range.start_offset,
                            repr(values[binding.id]),
                        )
                        for binding in semantic_bindings
                        if binding.owner_operation_id == invocation.id
                        and binding.source_kind == "sql-file-list"
                        and binding.source_range is not None
                        and binding.id in values
                    ]
                    for start, end, replacement in sorted(nested, reverse=True):
                        source = source[:start] + replacement + source[end:]
                value = (
                    CodeExpr(_serialize_parameter(parameter, parameter_values[parameter.id]))
                    if parameter.id in parameter_values
                    and not parameter.id.startswith("global:")
                    else CodeExpr(source)
                )
                if argument.position is None:
                    kwargs[argument.name] = value
                else:
                    args.append(value)
            kwargs.update(
                (
                    parameter.name,
                    CodeExpr(_serialize_parameter(parameter, parameter_values[parameter.id])),
                )
                for parameter in omitted
            )
            span = invocation.source_range
            replacements_copy = {
                key: value
                for key, value in replacements.items()
                if not (span.start_offset <= key[0] and key[1] <= span.end_offset)
            }
            replacements.clear()
            replacements.update(replacements_copy)
            statement = ast.parse(result.emitted.source[span.start_offset:span.end_offset]).body[0]
            if invocation.operation.id == "macro.set_named" and isinstance(statement, ast.Assign):
                replacement = f"{ast.unparse(statement.targets[0].value)}[{args[0].source}] = {args[1].source}"
            else:
                replacement = str(EmittableOperation.render_method_call(
                    invocation.operation, function=ast.unparse(statement.value.func), args=tuple(args), kwargs=kwargs))
                if isinstance(statement, ast.Assign):
                    replacement = ast.unparse(statement.targets[0]) + " = " + replacement
            replacements[(span.start_offset, span.end_offset)] = replacement


def _project_control_replacements(
    result: CompilationResult,
    operations: tuple[WorkflowOperation, ...],
    values: dict[str, Any],
    replacements: dict[tuple[int, int], str],
) -> None:
    blocks = {block.index: block for block in result.resolved.blocks}
    contexts = {}
    def visit(node, macros="macros"):
        contexts[node.start_index] = (node.scope_id, macros)
        for child in node.children:
            visit(child, macros)
    visit(result.resolved.scope_tree)
    for operation in operations:
        if operation.kind not in {"condition", "macro-loop", "chunk-loop"}:
            continue
        changed = {
            binding.name: values[binding.id]
            for binding in operation.bindings
            if binding.id in values
        }
        if not changed or operation.source_range is None:
            continue
        payload = blocks[operation.block_index].control_payload
        if isinstance(payload, IfThen):
            payload = replace(payload, **changed)
        elif isinstance(payload, StartMacro):
            payload = replace(payload, **changed)
        elif isinstance(payload, RunLoop):
            payload = replace(payload, **changed)
        else:
            continue
        replacements[
            (operation.source_range.start_offset, operation.source_range.end_offset)
        ] = payload.render_header(
            contexts[operation.block_index][0],
            macros=contexts[operation.block_index][1],
        )


def _project_embedded_python(
    result: CompilationResult,
    operations: tuple[WorkflowOperation, ...],
    values: dict[str, Any],
    replacements: dict[tuple[int, int], str],
) -> None:
    steps = {step.block_index: step for step in result.emitted.steps}
    for operation in operations:
        if operation.kind != "embedded-python" or operation.source_range is None:
            continue
        binding = next((item for item in operation.bindings if item.name == "source"), None)
        if binding is None or binding.id not in values:
            continue
        source = values[binding.id]
        if not isinstance(source, str):
            continue
        step = steps.get(operation.block_index)
        if step is None:
            continue
        indent = step.source[:len(step.source) - len(step.source.lstrip())]
        rebuilt = "\n".join(indent + line if line.strip() else "" for line in (source or "pass").splitlines())
        replacements[
            (operation.source_range.start_offset, operation.source_range.end_offset)
        ] = rebuilt
