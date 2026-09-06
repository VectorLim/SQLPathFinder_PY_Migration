from __future__ import annotations

from collections.abc import Iterable, Mapping
from dataclasses import dataclass
from typing import Any

from vg2c.compilation import CompilationResult
from vg2c.editing import ParameterChange
from vg2c.emitter.models import EmittedInvocation, EmittedParameter
from vg2c.sql_editor.models import SqlActionName, SqlEditableModel, SqlEditError
from vg2c.sql_editor.parser import parse_sql
from vg2c.sql_editor.transform import (
    add_filter,
    add_join,
    add_selection,
    move_selection,
    remove_filter,
    remove_join,
    remove_join_predicate,
    remove_selection,
    reorder_selection,
    update_filter,
    update_join_predicate,
    update_join_source,
    update_join_type,
    update_selection,
    update_source,
)


@dataclass(frozen=True, slots=True)
class SqlAction:
    parameter_id: str
    action: SqlActionName
    arguments: Mapping[str, Any]


def parameter_capabilities(
    invocation: EmittedInvocation, parameter: EmittedParameter
) -> tuple[str, ...]:
    """Return editor capabilities declared by the owning utility parameter."""
    return invocation.operation.capabilities_for_parameter(parameter.name)


def structured_sql_model(
    result: CompilationResult,
    parameter_id: str,
    changes: Iterable[ParameterChange] = (),
) -> SqlEditableModel:
    _, parameter = _structured_parameter(result, parameter_id)
    value = _effective_parameter_value(parameter, changes)
    if not isinstance(value, str):
        raise SqlEditError("Structured SQL requires an editable string parameter.")
    return parse_sql(value)


def apply_sql_action(
    result: CompilationResult,
    action: SqlAction,
    changes: Iterable[ParameterChange] = (),
) -> ParameterChange:
    _, parameter = _structured_parameter(result, action.parameter_id)
    sql = _effective_parameter_value(parameter, changes)
    if not isinstance(sql, str):
        raise SqlEditError("Structured SQL requires an editable string parameter.")

    args = dict(action.arguments)
    name = action.action
    if name == "add-selection":
        transformed = add_selection(sql, _require(args, "expression"))
    elif name == "update-selection":
        selection_id = _require(args, "selection_id")
        kwargs: dict[str, Any] = {}
        if "expression" in args:
            kwargs["expression"] = args["expression"]
        if "alias" in args:
            kwargs["alias"] = args["alias"]
        transformed = update_selection(sql, selection_id, **kwargs)
    elif name == "remove-selection":
        transformed = remove_selection(sql, _require(args, "selection_id"))
    elif name == "move-selection":
        direction = int(_require(args, "direction"))
        if direction not in {-1, 1}:
            raise SqlEditError("Selection direction must be -1 or 1.")
        transformed = move_selection(sql, _require(args, "selection_id"), direction)
    elif name == "reorder-selection":
        transformed = reorder_selection(
            sql,
            _require(args, "selection_id"),
            int(_require(args, "target_index")),
        )
    elif name == "add-filter":
        transformed = add_filter(
            sql,
            left=_require(args, "left"),
            operator=_require(args, "operator"),
            right=_require(args, "right"),
            connector=args.get("connector", "AND"),
        )
    elif name == "update-filter":
        transformed = update_filter(
            sql,
            _require(args, "filter_id"),
            **{key: args[key] for key in ("left", "operator", "right", "connector") if key in args},
        )
    elif name == "remove-filter":
        transformed = remove_filter(sql, _require(args, "filter_id"))
    elif name == "add-join":
        transformed = add_join(
            sql,
            join_type=_require(args, "join_type"),
            source_expression=_require(args, "source"),
            left=_require(args, "left"),
            right=_require(args, "right"),
            operator=args.get("operator", "="),
        )
    elif name == "update-join-type":
        transformed = update_join_type(sql, _require(args, "join_id"), _require(args, "join_type"))
    elif name == "update-join-source":
        transformed = update_join_source(sql, _require(args, "join_id"), _require(args, "source"))
    elif name == "update-join-predicate":
        transformed = update_join_predicate(
            sql,
            _require(args, "join_id"),
            _require(args, "predicate_id"),
            **{key: args[key] for key in ("left", "operator", "right") if key in args},
        )
    elif name == "remove-join-predicate":
        transformed = remove_join_predicate(
            sql, _require(args, "join_id"), _require(args, "predicate_id")
        )
    elif name == "remove-join":
        transformed = remove_join(sql, _require(args, "join_id"))
    elif name == "update-source":
        transformed = update_source(sql, _require(args, "source_id"), _require(args, "source"))
    else:
        raise SqlEditError(f"Unsupported structured SQL action: {name}")

    return ParameterChange(parameter_id=parameter.id, value=transformed.sql)


def _structured_parameter(
    result: CompilationResult, parameter_id: str
) -> tuple[EmittedInvocation, EmittedParameter]:
    for step in result.emitted.steps:
        for invocation in step.invocations:
            for parameter in invocation.parameters:
                if parameter.id != parameter_id:
                    continue
                if "structured-sql" not in parameter_capabilities(invocation, parameter):
                    raise SqlEditError(
                        "This utility parameter does not expose structured SQL editing."
                    )
                if not parameter.editable:
                    raise SqlEditError(parameter.read_only_reason or "SQL parameter is read-only.")
                return invocation, parameter
    raise SqlEditError("SQL parameter no longer exists.")


def _effective_parameter_value(
    parameter: EmittedParameter, changes: Iterable[ParameterChange]
) -> Any:
    value = parameter.value
    for change in changes:
        if change.parameter_id == parameter.id:
            value = change.value
    return value


def _require(arguments: Mapping[str, Any], key: str) -> Any:
    if key not in arguments:
        raise SqlEditError(f"Missing SQL action argument: {key}")
    return arguments[key]


__all__ = [
    "SqlAction",
    "SqlActionName",
    "apply_sql_action",
    "parameter_capabilities",
    "structured_sql_model",
]
