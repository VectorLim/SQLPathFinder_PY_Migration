from __future__ import annotations

import ast
import difflib
from collections.abc import Iterable
from dataclasses import dataclass
from typing import Any, Literal

from vg2c.compilation import CompilationResult
from vg2c.emitter.models import CodeExpr, EmittableOperation, EmittedParameter


@dataclass(frozen=True, slots=True)
class ParameterChange:
    parameter_id: str
    value: Any = None
    reset: bool = False


@dataclass(frozen=True, slots=True)
class ValidationIssue:
    code: str
    message: str
    parameter_id: str | None = None
    level: Literal["warning", "error"] = "error"


@dataclass(frozen=True, slots=True)
class ChangeProjection:
    source: str
    values: tuple[ParameterChange, ...]
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
    result: CompilationResult, changes: Iterable[ParameterChange]
) -> tuple[ValidationIssue, ...]:
    return project_changes(result, changes).issues


def project_changes(
    result: CompilationResult, changes: Iterable[ParameterChange]
) -> ChangeProjection:
    """Project parameter intent onto canonical emitted source without writing files."""
    requested = tuple(changes)
    bindings: dict[str, list[EmittedParameter]] = {}
    for step in result.emitted.steps:
        for parameter in step.parameters:
            bindings.setdefault(parameter.id, []).append(parameter)
    issues: list[ValidationIssue] = []
    replacements: dict[tuple[int, int], str] = {}
    accepted: list[ParameterChange] = []
    seen: dict[str, str] = {}

    for change in requested:
        if change.parameter_id in seen:
            if change.parameter_id.startswith("global:"):
                if seen[change.parameter_id] == repr((change.reset, change.value)):
                    continue
                issues.append(
                    ValidationIssue(
                        code="conflicting-global-change",
                        message="Steps sharing a global must use the same value.",
                        parameter_id=change.parameter_id,
                    )
                )
                continue
            issues.append(
                ValidationIssue(
                    code="duplicate-change",
                    message="Parameter is edited more than once.",
                    parameter_id=change.parameter_id,
                )
            )
            continue
        seen[change.parameter_id] = repr((change.reset, change.value))
        parameters = bindings.get(change.parameter_id, [])
        if not parameters:
            issues.append(
                ValidationIssue(
                    code="unknown-parameter",
                    message="Parameter no longer exists in the compiler output.",
                    parameter_id=change.parameter_id,
                )
            )
            continue
        binding_issues = [
            issue
            for parameter in parameters
            if (
                issue := _validate_value(
                    parameter, parameter.value if change.reset else change.value
                )
            )
            is not None
        ]
        if binding_issues:
            issues.extend(binding_issues)
            continue
        if change.reset:
            continue
        for parameter in parameters:
            if parameter.source_range is not None:
                span = (
                    parameter.source_range.start_offset,
                    parameter.source_range.end_offset,
                )
                replacements[span] = _serialize(parameter, change.value)
        accepted.append(change)

    values = {change.parameter_id: change.value for change in accepted}
    for step in result.emitted.steps:
        for invocation in step.invocations:
            omitted = [
                parameter
                for parameter in invocation.parameters
                if parameter.source_range is None and parameter.id in values
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
                    CodeExpr(_serialize(parameter, values[parameter.id]))
                    if parameter.id in values and not parameter.id.startswith("global:")
                    else CodeExpr(argument.source)
                )
                if argument.position is None:
                    kwargs[argument.name] = value
                else:
                    args.append(value)
            kwargs.update(
                (parameter.name, CodeExpr(_serialize(parameter, values[parameter.id])))
                for parameter in omitted
            )
            span = invocation.source_range
            replacements = {
                key: value
                for key, value in replacements.items()
                if not (span.start_offset <= key[0] and key[1] <= span.end_offset)
            }
            replacements[(span.start_offset, span.end_offset)] = str(
                EmittableOperation.render_method_call(
                    invocation.operation, args=tuple(args), kwargs=kwargs
                )
            )

    candidate = result.emitted.source
    for (start, end), replacement in sorted(replacements.items(), reverse=True):
        candidate = f"{candidate[:start]}{replacement}{candidate[end:]}"

    if not issues:
        try:
            tree = ast.parse(
                candidate, filename=str(result.input_path.with_suffix(".py"))
            )
            compile(tree, str(result.input_path.with_suffix(".py")), "exec")
        except SyntaxError as exc:
            issues.append(ValidationIssue(code="invalid-python", message=str(exc)))

    return ChangeProjection(
        source=candidate,
        values=tuple(accepted),
        issues=tuple(issues),
    )


def preview_changes(
    result: CompilationResult, changes: Iterable[ParameterChange]
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
    result: CompilationResult, changes: Iterable[ParameterChange]
) -> ChangeProjection:
    """Return the validated candidate source; persistence deliberately lives elsewhere."""
    projection = project_changes(result, changes)
    if not projection.valid:
        raise ChangeValidationError(projection.issues)
    return projection


def _validate_value(parameter: EmittedParameter, value: Any) -> ValidationIssue | None:
    if not parameter.editable:
        return ValidationIssue(
            code="read-only-parameter",
            message=parameter.read_only_reason or "Parameter is read-only.",
            parameter_id=parameter.id,
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
    elif parameter.editor_type == "boolean":
        expected = bool
    elif parameter.editor_type == "list":
        expected = list
    else:
        return ValidationIssue(
            code="unsupported-editor",
            message="This parameter cannot be edited safely.",
            parameter_id=parameter.id,
        )

    if not isinstance(value, expected):
        return _type_issue(parameter)
    if isinstance(value, list) and not all(
        item is None or isinstance(item, (str, int, float, bool)) for item in value
    ):
        return ValidationIssue(
            code="invalid-list",
            message="Lists may only contain scalar JSON values.",
            parameter_id=parameter.id,
        )

    choices = parameter.definition.choices if parameter.definition else ()
    if choices and value not in choices:
        return ValidationIssue(
            code="invalid-choice",
            message=f"Value must be one of {list(choices)!r}.",
            parameter_id=parameter.id,
        )
    return None


def _type_issue(parameter: EmittedParameter) -> ValidationIssue:
    return ValidationIssue(
        code="invalid-type",
        message=f"Expected a {parameter.editor_type} value.",
        parameter_id=parameter.id,
    )


def _serialize(parameter: EmittedParameter, value: Any) -> str:
    schema = parameter.definition.schema if parameter.definition else None
    return repr(schema.python_value(value) if schema else value)


__all__ = [
    "ChangePreview",
    "ChangeProjection",
    "ChangeValidationError",
    "ParameterChange",
    "ValidationIssue",
    "apply_changes",
    "preview_changes",
    "project_changes",
    "validate_changes",
]
