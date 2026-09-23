from __future__ import annotations

import ast
import difflib
from collections.abc import Iterable
from dataclasses import dataclass, replace
from typing import Any, Literal

from vg2c.compilation import CompilationResult
from vg2c.emitter.models import (
    CodeExpr,
    EmittableOperation,
    EmittedParameter,
    build_step_emission,
)
from vg2c.operands import IfThen, RunLoop, StartMacro
from vg2c.semantics import EditableBinding, WorkflowOperation, build_semantic_model


@dataclass(frozen=True, slots=True, init=False)
class SemanticChange:
    binding_id: str
    value: Any = None
    reset: bool = False

    def __init__(
        self,
        binding_id: str | None = None,
        value: Any = None,
        reset: bool = False,
        *,
        parameter_id: str | None = None,
    ) -> None:
        identity = binding_id if binding_id is not None else parameter_id
        if not identity:
            raise ValueError("A binding_id is required.")
        object.__setattr__(self, "binding_id", identity)
        object.__setattr__(self, "value", value)
        object.__setattr__(self, "reset", reset)

    @property
    def parameter_id(self) -> str:
        """Compatibility alias for pre-v5 callers."""
        return self.binding_id


# Compatibility name retained while API/sidecars migrate to binding terminology.
ParameterChange = SemanticChange


@dataclass(frozen=True, slots=True)
class ValidationIssue:
    code: str
    message: str
    binding_id: str | None = None
    level: Literal["warning", "error"] = "error"

    @property
    def parameter_id(self) -> str | None:
        """Compatibility alias for pre-v5 callers."""
        return self.binding_id


@dataclass(frozen=True, slots=True)
class ChangeProjection:
    source: str
    values: tuple[SemanticChange, ...]
    issues: tuple[ValidationIssue, ...]

    @property
    def valid(self) -> bool:
        return not any(issue.level == "error" for issue in self.issues)


@dataclass(frozen=True, slots=True)
class ChangePreview:
    projection: ChangeProjection
    diff: str

    @property
    def valid(self) -> bool:
        return self.projection.valid


class ChangeValidationError(ValueError):
    def __init__(self, issues: tuple[ValidationIssue, ...]) -> None:
        super().__init__("change validation failed")
        self.issues = issues


def validate_changes(
    result: CompilationResult, changes: Iterable[SemanticChange]
) -> tuple[ValidationIssue, ...]:
    return project_changes(result, changes).issues


def project_changes(
    result: CompilationResult, changes: Iterable[SemanticChange]
) -> ChangeProjection:
    """Project semantic binding intent onto canonical emitted source without writing files."""
    requested = tuple(changes)
    model = build_semantic_model(result)
    semantic_bindings = {binding.id: binding for binding in model.bindings}

    emitted_bindings: dict[str, list[EmittedParameter]] = {}
    for step in result.emitted.steps:
        for parameter in step.parameters:
            emitted_bindings.setdefault(parameter.id, []).append(parameter)

    issues: list[ValidationIssue] = []
    replacements: dict[tuple[int, int], str] = {}
    accepted: list[SemanticChange] = []
    seen: dict[str, str] = {}

    for change in requested:
        binding_id = change.binding_id
        if binding_id in seen:
            if binding_id.startswith("global:"):
                if seen[binding_id] == repr((change.reset, change.value)):
                    continue
                issues.append(
                    ValidationIssue(
                        code="conflicting-global-change",
                        message="Operations sharing a global must use the same value.",
                        binding_id=binding_id,
                    )
                )
                continue
            issues.append(
                ValidationIssue(
                    code="duplicate-change",
                    message="Binding is edited more than once.",
                    binding_id=binding_id,
                )
            )
            continue

        seen[binding_id] = repr((change.reset, change.value))
        binding = semantic_bindings.get(binding_id)
        parameters = emitted_bindings.get(binding_id, [])

        # Global parameters can be referenced only from internal invocations and therefore
        # may not be present in the normal semantic binding projection.
        if binding is None and parameters:
            binding = _binding_from_parameter(parameters[0])

        if binding is None:
            issues.append(
                ValidationIssue(
                    code="unknown-binding",
                    message="Binding no longer exists in the compiler semantic model.",
                    binding_id=binding_id,
                )
            )
            continue

        value = binding.value if change.reset else change.value
        if binding.source_kind == "parameter":
            binding_issues = [
                issue
                for parameter in parameters
                if (issue := _validate_parameter(parameter, value)) is not None
            ]
            if not parameters and binding.editable:
                binding_issues = [_validate_binding(binding, value)]
                binding_issues = [item for item in binding_issues if item is not None]
        else:
            issue = _validate_binding(binding, value)
            binding_issues = [issue] if issue else []

        if binding_issues:
            issues.extend(binding_issues)
            continue
        if change.reset:
            continue

        accepted.append(change)
        if binding.source_kind == "parameter":
            for parameter in parameters:
                if parameter.source_range is not None:
                    span = (
                        parameter.source_range.start_offset,
                        parameter.source_range.end_offset,
                    )
                    replacements[span] = _serialize_parameter(parameter, change.value)
        elif binding.source_kind == "rows-in-file" and binding.source_range is not None:
            span = (binding.source_range.start_offset, binding.source_range.end_offset)
            replacements[span] = repr(change.value)

    accepted_values = {change.binding_id: change.value for change in accepted}
    _validate_changed_controls(result, model.operations, accepted_values, issues)
    if issues:
        return ChangeProjection(
            source=result.emitted.source,
            values=tuple(accepted),
            issues=tuple(issues),
        )

    _project_control_replacements(result, model.operations, accepted_values, replacements)
    _project_embedded_python(result, model.operations, accepted_values, replacements)
    _project_omitted_parameters(
        result,
        emitted_bindings,
        accepted_values,
        replacements,
    )

    candidate = result.emitted.source
    for (start, end), replacement in sorted(replacements.items(), reverse=True):
        candidate = f"{candidate[:start]}{replacement}{candidate[end:]}"

    try:
        tree = ast.parse(candidate, filename=str(result.input_path.with_suffix(".py")))
        compile(tree, str(result.input_path.with_suffix(".py")), "exec")
    except SyntaxError as exc:
        issues.append(ValidationIssue(code="invalid-python", message=str(exc)))

    return ChangeProjection(
        source=candidate,
        values=tuple(accepted),
        issues=tuple(issues),
    )


def preview_changes(
    result: CompilationResult, changes: Iterable[SemanticChange]
) -> ChangePreview:
    projection = project_changes(result, changes)
    diff = "".join(
        difflib.unified_diff(
            result.emitted.source.splitlines(keepends=True),
            projection.source.splitlines(keepends=True),
            fromfile=str(result.input_path.with_suffix(".py")),
            tofile=str(result.input_path.with_suffix(".py")),
        )
    )
    return ChangePreview(projection=projection, diff=diff)


def apply_changes(
    result: CompilationResult, changes: Iterable[SemanticChange]
) -> ChangeProjection:
    """Return the validated candidate source; persistence deliberately lives elsewhere."""
    projection = project_changes(result, changes)
    if not projection.valid:
        raise ChangeValidationError(projection.issues)
    return projection


def _project_omitted_parameters(
    result: CompilationResult,
    emitted_bindings: dict[str, list[EmittedParameter]],
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
                value = (
                    CodeExpr(_serialize_parameter(parameter, parameter_values[parameter.id]))
                    if parameter.id in parameter_values
                    and not parameter.id.startswith("global:")
                    else CodeExpr(argument.source)
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
            replacements[(span.start_offset, span.end_offset)] = str(
                EmittableOperation.render_method_call(
                    invocation.operation, args=tuple(args), kwargs=kwargs
                )
            )


def _project_control_replacements(
    result: CompilationResult,
    operations: tuple[WorkflowOperation, ...],
    values: dict[str, Any],
    replacements: dict[tuple[int, int], str],
) -> None:
    blocks = {block.index: block for block in result.resolved.blocks}
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
        ] = payload.render_header()


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
        rebuilt = build_step_emission(
            function_name=step.function_name,
            block_index=step.block_index,
            functional_kind=step.functional_kind,
            body_lines=[source] if source.strip() else [],
        ).source
        replacements[
            (operation.source_range.start_offset, operation.source_range.end_offset)
        ] = rebuilt


def _validate_changed_controls(
    result: CompilationResult,
    operations: tuple[WorkflowOperation, ...],
    values: dict[str, Any],
    issues: list[ValidationIssue],
) -> None:
    touched = {
        operation.id
        for operation in operations
        if any(binding.id in values for binding in operation.bindings)
    }
    if not touched:
        return
    projected = build_semantic_model(result, values)
    for operation in projected.operations:
        if operation.id not in touched:
            continue
        if operation.kind == "condition":
            by_name = {binding.name: binding for binding in operation.bindings}
            second = [by_name[name].value for name in ("conj", "lhs2", "op2", "rhs2")]
            has_second = any(value not in {None, ""} for value in second)
            complete_second = all(value not in {None, ""} for value in second)
            if has_second and not complete_second:
                issues.append(
                    ValidationIssue(
                        code="incomplete-condition",
                        message="A second condition requires connector, left value, operator, and comparison value.",
                        binding_id=next(
                            (
                                binding.id
                                for binding in operation.bindings
                                if binding.id in values
                            ),
                            None,
                        ),
                    )
                )
            for binding in operation.bindings:
                if binding.validation_state == "unresolved":
                    issues.append(
                        ValidationIssue(
                            code="unresolved-symbol",
                            message=f"{binding.value!r} does not resolve to a known macro or symbol.",
                            binding_id=binding.id,
                        )
                    )


def _validate_binding(
    binding: EditableBinding, value: Any
) -> ValidationIssue | None:
    if not binding.editable:
        return ValidationIssue(
            code="read-only-binding",
            message=binding.read_only_reason or "Binding is read-only.",
            binding_id=binding.id,
        )
    schema = binding.schema
    if schema is not None and schema.kind != "dynamic" and not schema.accepts(value):
        return ValidationIssue(
            code="invalid-type",
            message=f"Value does not satisfy the {schema.kind} binding schema.",
            binding_id=binding.id,
        )
    return None


def _validate_parameter(
    parameter: EmittedParameter, value: Any
) -> ValidationIssue | None:
    if not parameter.editable:
        return ValidationIssue(
            code="read-only-binding",
            message=parameter.read_only_reason or "Binding is read-only.",
            binding_id=parameter.id,
        )

    schema = parameter.definition.schema if parameter.definition else None
    if schema is not None and schema.kind != "dynamic":
        return None if schema.accepts(value) else _type_issue(parameter)

    expected: type[Any]
    if parameter.editor_type in {"string", "multiline"}:
        expected = str
    elif parameter.editor_type == "integer":
        if isinstance(value, bool) or not isinstance(value, int):
            return _type_issue(parameter)
        expected = int
    elif parameter.editor_type == "number":
        if isinstance(value, bool) or not isinstance(value, (int, float)):
            return _type_issue(parameter)
        expected = (int, float)  # type: ignore[assignment]
    elif parameter.editor_type == "boolean":
        expected = bool
    elif parameter.editor_type == "list":
        expected = list
    elif parameter.editor_type == "object":
        expected = dict
    else:
        return ValidationIssue(
            code="unsupported-editor",
            message="This binding cannot be edited safely.",
            binding_id=parameter.id,
        )

    if not isinstance(value, expected):
        return _type_issue(parameter)
    if isinstance(value, list) and not all(
        item is None or isinstance(item, (str, int, float, bool, list, tuple, dict))
        for item in value
    ):
        return ValidationIssue(
            code="invalid-list",
            message="List value contains an unsupported item.",
            binding_id=parameter.id,
        )

    choices = (
        parameter.definition.schema.choices
        if parameter.definition and parameter.definition.schema
        else ()
    )
    if choices and value not in choices:
        return ValidationIssue(
            code="invalid-choice",
            message=f"Value must be one of {list(choices)!r}.",
            binding_id=parameter.id,
        )
    return None


def _binding_from_parameter(parameter: EmittedParameter) -> EditableBinding:
    definition = parameter.definition
    return EditableBinding(
        id=parameter.id,
        owner_operation_id="",
        name=parameter.name,
        display_label=parameter.name.replace("_", " ").title(),
        schema=definition.schema if definition else None,
        value=parameter.value,
        default=definition.default if definition else None,
        required=definition.required if definition else True,
        editable=parameter.editable,
        read_only_reason=parameter.read_only_reason,
        source_range=parameter.source_range,
    )


def _type_issue(parameter: EmittedParameter) -> ValidationIssue:
    return ValidationIssue(
        code="invalid-type",
        message=f"Expected a {parameter.editor_type} value.",
        binding_id=parameter.id,
    )


def _serialize_parameter(parameter: EmittedParameter, value: Any) -> str:
    schema = parameter.definition.schema if parameter.definition else None
    return repr(schema.python_value(value) if schema else value)


__all__ = [
    "ChangePreview",
    "ChangeProjection",
    "ChangeValidationError",
    "ParameterChange",
    "SemanticChange",
    "ValidationIssue",
    "apply_changes",
    "preview_changes",
    "project_changes",
    "validate_changes",
]
