from __future__ import annotations

import ast
import difflib
from collections.abc import Iterable
from dataclasses import dataclass
from typing import Any, Literal

from vg2c.compilation import CompilationResult
from vg2c.emitter.models import EmittedParameter


@dataclass(frozen=True, slots=True)
class ParameterChange:
    parameter_id: str
    value: Any


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
    parameters = {
        parameter.id: parameter
        for step in result.emitted.steps
        for parameter in step.parameters
    }
    issues: list[ValidationIssue] = []
    replacements: list[tuple[int, int, str]] = []
    accepted: list[ParameterChange] = []
    seen: set[str] = set()

    for change in requested:
        if change.parameter_id in seen:
            issues.append(
                ValidationIssue(
                    code="duplicate-change",
                    message="Parameter is edited more than once.",
                    parameter_id=change.parameter_id,
                )
            )
            continue
        seen.add(change.parameter_id)
        parameter = parameters.get(change.parameter_id)
        if parameter is None:
            issues.append(
                ValidationIssue(
                    code="unknown-parameter",
                    message="Parameter no longer exists in the compiler output.",
                    parameter_id=change.parameter_id,
                )
            )
            continue
        issue = _validate_value(parameter, change.value)
        if issue is not None:
            issues.append(issue)
            continue
        serialized = _serialize(parameter, change.value)
        replacements.append(
            (
                parameter.source_range.start_offset,
                parameter.source_range.end_offset,
                serialized,
            )
        )
        accepted.append(change)

    candidate = result.emitted.source
    for start, end, replacement in sorted(replacements, reverse=True):
        candidate = f"{candidate[:start]}{replacement}{candidate[end:]}"

    if not issues:
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


def _validate_value(
    parameter: EmittedParameter, value: Any
) -> ValidationIssue | None:
    if not parameter.editable:
        return ValidationIssue(
            code="read-only-parameter",
            message=parameter.read_only_reason or "Parameter is read-only.",
            parameter_id=parameter.id,
        )

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
    return repr(value)


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
