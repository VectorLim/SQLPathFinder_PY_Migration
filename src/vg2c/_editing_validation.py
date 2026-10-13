"""Validation and serialization helpers for semantic editing."""

from __future__ import annotations

from pathlib import PurePosixPath
from typing import Any

from vg2c.compilation import CompilationResult
from vg2c.editing_models import ValidationIssue
from vg2c.emitter.models import EmittedParameter
from vg2c.semantic_models import EditableBinding, WorkflowOperation
from vg2c.semantics import _build_semantics

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
    projected_operations, _, _ = _build_semantics(result, values)
    for operation in projected_operations:
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
        if operation.kind == "macro-loop":
            binding = operation.bindings[0]
            if bool(values.get(binding.id, binding.value)) != bool(binding.default):
                issues.append(ValidationIssue(code="macro-shape-change", message="Changing between static and CSV macro scopes requires regeneration from source.", binding_id=binding.id))


def _validate_binding(
    binding: EditableBinding, value: Any
) -> ValidationIssue | None:
    if not binding.editable:
        return ValidationIssue(
            code="read-only-binding",
            message=binding.read_only_reason or "Binding is read-only.",
            binding_id=binding.id,
        )
    if binding.source_kind == "sql-file-list" and isinstance(value, str):
        normalized = value.replace("\\", "/")
        path = PurePosixPath(normalized)
        if (not normalized or path.is_absolute() or ".." in path.parts
            or "'" in normalized or '"' in normalized
            or (len(normalized) >= 2 and normalized[0].isalpha() and normalized[1] == ":")):
            return ValidationIssue(
                code="invalid-workspace-path",
                message="File-list paths must be relative to the server workspace.",
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
