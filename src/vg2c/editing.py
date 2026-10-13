from __future__ import annotations

import ast
import difflib
from collections.abc import Iterable
from typing import Any

from vg2c.compilation import CompilationResult
from vg2c.editing_models import (
    ChangePreview,
    ChangeProjection,
    ChangeValidationError,
    SemanticChange,
    ValidationIssue,
)
from vg2c.emitter.models import EmittedParameter
from vg2c.project_paths import project_main_path
from vg2c.semantics import _build_semantics, condition_symbol_token
from vg2c._editing_projection import (
    _project_control_replacements,
    _project_embedded_python,
    _project_omitted_parameters,
)
from vg2c._editing_validation import (
    _binding_from_parameter,
    _serialize_parameter,
    _validate_binding,
    _validate_changed_controls,
    _validate_parameter,
)

def validate_changes(
    result: CompilationResult, changes: Iterable[SemanticChange]
) -> tuple[ValidationIssue, ...]:
    return project_changes(result, changes).issues


def project_changes(
    result: CompilationResult, changes: Iterable[SemanticChange]
) -> ChangeProjection:
    """Project semantic binding intent onto canonical emitted source without writing files."""
    requested = tuple(changes)
    operations, bindings, semantic_symbols = _build_semantics(result)
    semantic_bindings = {binding.id: binding for binding in bindings}
    symbols = {symbol.id: symbol for symbol in semantic_symbols}
    requested_by_id = {change.binding_id: change for change in requested}

    emitted_bindings: dict[str, list[EmittedParameter]] = {}
    for step in result.emitted.steps:
        for parameter in step.parameters:
            emitted_bindings.setdefault(parameter.id, []).append(parameter)

    issues: list[ValidationIssue] = []
    replacements: dict[tuple[int, int], str] = {}
    accepted: list[SemanticChange] = []
    effective_values: dict[str, Any] = {}
    seen: dict[str, str] = {}

    for change in requested:
        binding_id = change.binding_id
        if binding_id in seen:
            if binding_id.startswith("global:"):
                if seen[binding_id] == repr((change.reset, change.value, change.symbol_id)):
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

        seen[binding_id] = repr((change.reset, change.value, change.symbol_id))
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

        if change.symbol_id is not None:
            if change.reset or change.value is not None:
                issues.append(ValidationIssue(
                    code="ambiguous-symbol-change",
                    message="Choose a literal value or a symbol, not both.",
                    binding_id=binding_id,
                ))
                continue
            symbol = symbols.get(change.symbol_id)
            if symbol is None or symbol.kind == "unresolved":
                issues.append(ValidationIssue(
                    code="unknown-symbol",
                    message="Selected symbol is not available in this document.",
                    binding_id=binding_id,
                ))
                continue
            if binding.source_kind != "condition" or binding.name not in {
                "lhs", "rhs", "lhs2", "rhs2"
            }:
                issues.append(ValidationIssue(
                    code="ineligible-symbol",
                    message="This binding cannot use the selected symbol.",
                    binding_id=binding_id,
                ))
                continue
            operator_name = "op2" if binding.name.endswith("2") else "op"
            operator_id = f"{binding.owner_operation_id}:{operator_name}"
            operator_change = requested_by_id.get(operator_id)
            operator_binding = semantic_bindings.get(operator_id)
            operator = (
                operator_change.value
                if operator_change and not operator_change.reset
                else operator_binding.value if operator_binding else None
            )
            try:
                value = condition_symbol_token(symbol, operator)
            except ValueError as exc:
                issues.append(ValidationIssue(
                    code="ineligible-symbol", message=str(exc), binding_id=binding_id
                ))
                continue
        else:
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
        effective_values[binding_id] = value
        if binding.source_kind == "parameter":
            for parameter in parameters:
                if parameter.source_range is not None:
                    span = (
                        parameter.source_range.start_offset,
                        parameter.source_range.end_offset,
                    )
                    replacements[span] = _serialize_parameter(parameter, value)
        elif binding.source_kind == "rows-in-file" and binding.source_range is not None:
            span = (binding.source_range.start_offset, binding.source_range.end_offset)
            replacements[span] = repr(value)
        elif binding.source_kind == "sql-file-list" and binding.source_range is not None:
            span = (binding.source_range.start_offset, binding.source_range.end_offset)
            replacements[span] = repr(value)

    accepted_values = effective_values
    for operation in operations:
        if operation.kind != "condition":
            continue
        by_name = {binding.name: binding for binding in operation.bindings}
        for operator_name, operand_names in (("op", ("lhs", "rhs")), ("op2", ("lhs2", "rhs2"))):
            operator_binding = by_name.get(operator_name)
            if operator_binding is None or operator_binding.id not in accepted_values:
                continue
            for operand_name in operand_names:
                operand = by_name.get(operand_name)
                if operand is None or operand.id in accepted_values or operand.symbol_id is None:
                    continue
                symbol = symbols.get(operand.symbol_id)
                if symbol is not None and symbol.kind != "unresolved":
                    accepted_values[operand.id] = condition_symbol_token(
                        symbol, accepted_values[operator_binding.id]
                    )
    _validate_changed_controls(result, operations, accepted_values, issues)
    if issues:
        return ChangeProjection(
            source=result.emitted.source,
            values=tuple(accepted),
            issues=tuple(issues),
            effective_values=accepted_values,
        )

    _project_control_replacements(result, operations, accepted_values, replacements)
    _project_embedded_python(result, operations, accepted_values, replacements)
    _project_omitted_parameters(
        result,
        emitted_bindings,
        bindings,
        accepted_values,
        replacements,
    )

    candidate = result.emitted.source
    for (start, end), replacement in sorted(replacements.items(), reverse=True):
        candidate = f"{candidate[:start]}{replacement}{candidate[end:]}"

    try:
        tree = ast.parse(candidate, filename=str(project_main_path(result.input_path)))
        compile(tree, str(project_main_path(result.input_path)), "exec")
    except SyntaxError as exc:
        issues.append(ValidationIssue(code="invalid-python", message=str(exc)))

    return ChangeProjection(
        source=candidate,
        values=tuple(accepted),
        issues=tuple(issues),
        effective_values=accepted_values,
    )


def preview_changes(
    result: CompilationResult, changes: Iterable[SemanticChange]
) -> ChangePreview:
    projection = project_changes(result, changes)
    diff = "".join(
        difflib.unified_diff(
            result.emitted.source.splitlines(keepends=True),
            projection.source.splitlines(keepends=True),
            fromfile=str(project_main_path(result.input_path)),
            tofile=str(project_main_path(result.input_path)),
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

__all__ = [
    "ChangePreview",
    "ChangeProjection",
    "ChangeValidationError",
    "SemanticChange",
    "ValidationIssue",
    "apply_changes",
    "preview_changes",
    "project_changes",
    "validate_changes",
]
