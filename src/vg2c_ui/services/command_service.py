from __future__ import annotations

import ast
import difflib
import hashlib
from collections.abc import Callable
from dataclasses import dataclass
from pathlib import Path
from typing import Any

from vg2c_ui.domain.models import (
    CommandBatch,
    CommandPreview,
    ValidationIssue,
    WorkflowDocument,
    WorkflowOverride,
    WorkflowSidecar,
)
from vg2c_ui.services.atomic_io import atomic_write_text
from vg2c_ui.services.python_document import parse_generated_python
from vg2c_ui.services.sidecar import write_sidecar


class DocumentConflict(RuntimeError):
    pass


class CommandValidationError(ValueError):
    def __init__(self, issues: list[ValidationIssue]) -> None:
        super().__init__("command validation failed")
        self.issues = issues


@dataclass(frozen=True, slots=True)
class PreparedCommands:
    document: WorkflowDocument
    output: Path
    original: str
    candidate: str
    overrides: list[WorkflowOverride]
    issues: list[ValidationIssue]

    @property
    def diff(self) -> str:
        return "".join(
            difflib.unified_diff(
                self.original.splitlines(keepends=True),
                self.candidate.splitlines(keepends=True),
                fromfile=self.document.output_path,
                tofile=self.document.output_path,
            )
        )


class CommandService:
    """Validates, previews, and atomically applies constrained UI commands."""

    def __init__(
        self,
        resolve: Callable[[str | Path], Path],
        open_document: Callable[[str | Path, str | Path | None], WorkflowDocument],
    ) -> None:
        self._resolve = resolve
        self._open_document = open_document

    def preview(self, batch: CommandBatch) -> CommandPreview:
        prepared = self._prepare(batch)
        return CommandPreview(
            valid=not any(issue.level == "error" for issue in prepared.issues),
            diff=prepared.diff,
            issues=prepared.issues,
        )

    def apply(self, batch: CommandBatch) -> tuple[WorkflowDocument, str]:
        prepared = self._prepare(batch)
        if any(issue.level == "error" for issue in prepared.issues):
            raise CommandValidationError(prepared.issues)
        atomic_write_text(prepared.output, prepared.candidate)
        revision = prepared.document.revision + 1
        output_hash = _sha256(prepared.candidate.encode("utf-8"))
        write_sidecar(
            prepared.output,
            WorkflowSidecar(
                source_hash=prepared.document.source_hash,
                output_hash=output_hash,
                revision=revision,
                layout=prepared.document.layout,
                overrides=prepared.overrides,
            ),
        )
        return (
            self._open_document(batch.source_path, batch.output_path),
            prepared.diff,
        )

    def _prepare(self, batch: CommandBatch) -> PreparedCommands:
        output = self._resolve(batch.output_path)
        current = self._open_document(batch.source_path, batch.output_path)
        if (
            current.source_hash != batch.source_hash
            or current.output_hash != batch.output_hash
            or current.revision != batch.revision
        ):
            raise DocumentConflict(
                "The source or generated Python changed. Reload before applying edits."
            )
        original = output.read_text(encoding="utf-8")
        parsed = parse_generated_python(original)
        steps = {step.id: step for step in current.steps}
        replacements: list[tuple[int, int, str]] = []
        issues: list[ValidationIssue] = []
        overrides = {
            (item.step_id, item.parameter_id): item for item in current.overrides
        }
        seen: set[tuple[str, str]] = set()

        for command in batch.commands:
            key = (command.step_id, command.parameter_id)
            if key in seen:
                issues.append(_issue(command, "duplicate-command", "Parameter is edited twice."))
                continue
            seen.add(key)
            step = steps.get(command.step_id)
            if step is None:
                issues.append(_issue(command, "unknown-step", "Step no longer exists."))
                continue
            parameter = parsed.parameter(step.function_name, command.parameter_id)
            descriptor = next(
                (item for item in step.parameters if item.id == command.parameter_id),
                None,
            )
            if parameter is None or descriptor is None:
                issues.append(
                    _issue(command, "unknown-parameter", "Parameter no longer exists.")
                )
                continue
            if step.read_only or not descriptor.editable:
                issues.append(
                    _issue(
                        command,
                        "read-only-parameter",
                        descriptor.read_only_reason or "Parameter is read-only.",
                    )
                )
                continue
            serialized = _serialize(descriptor.editor_type, command.value)
            if isinstance(serialized, ValidationIssue):
                serialized.step_id = command.step_id
                serialized.parameter_id = command.parameter_id
                issues.append(serialized)
                continue
            choices = descriptor.constraints.get("choices")
            if choices is not None and command.value not in choices:
                issues.append(
                    _issue(command, "invalid-choice", f"Value must be one of {choices!r}.")
                )
                continue
            replacements.append((parameter.start_offset, parameter.end_offset, serialized))
            overrides[key] = WorkflowOverride(
                step_id=command.step_id,
                parameter_id=command.parameter_id,
                value=command.value,
            )

        candidate = original
        for start, end, value in sorted(replacements, reverse=True):
            candidate = f"{candidate[:start]}{value}{candidate[end:]}"
        if not issues:
            try:
                tree = ast.parse(candidate, filename=str(output))
                compile(tree, str(output), "exec")
                parse_generated_python(candidate)
            except (SyntaxError, ValueError) as exc:
                issues.append(
                    ValidationIssue(code="invalid-python", message=str(exc))
                )
        return PreparedCommands(
            document=current,
            output=output,
            original=original,
            candidate=candidate,
            overrides=list(overrides.values()),
            issues=issues,
        )


def _serialize(editor_type: str, value: Any) -> str | ValidationIssue:
    expected: type[Any] | tuple[type[Any], ...]
    if editor_type in {"string", "multiline"}:
        expected = str
    elif editor_type == "integer":
        expected = int
        if isinstance(value, bool):
            return ValidationIssue(code="invalid-type", message="Expected an integer.")
    elif editor_type == "boolean":
        expected = bool
    elif editor_type == "list":
        expected = list
    else:
        return ValidationIssue(
            code="unsupported-editor", message="This parameter cannot be edited safely."
        )
    if not isinstance(value, expected):
        return ValidationIssue(
            code="invalid-type", message=f"Expected a {editor_type} value."
        )
    if isinstance(value, list) and not all(
        item is None or isinstance(item, (str, int, float, bool)) for item in value
    ):
        return ValidationIssue(
            code="invalid-list", message="Lists may only contain scalar JSON values."
        )
    return repr(value)


def _issue(command: Any, code: str, message: str) -> ValidationIssue:
    return ValidationIssue(
        code=code,
        message=message,
        step_id=command.step_id,
        parameter_id=command.parameter_id,
    )


def _sha256(value: bytes) -> str:
    return hashlib.sha256(value).hexdigest()


__all__ = [
    "CommandService",
    "CommandValidationError",
    "DocumentConflict",
]
