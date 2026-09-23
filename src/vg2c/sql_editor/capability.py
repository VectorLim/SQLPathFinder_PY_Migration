from __future__ import annotations

from collections.abc import Callable, Iterable, Mapping
from dataclasses import dataclass
from pathlib import Path
from typing import Any

from vg2c.compilation import CompilationResult
from vg2c.editing import ParameterChange, project_changes
from vg2c.emitter.models import EmittedInvocation, EmittedParameter, EmittedStep
from vg2c.kind import Kind
from vg2c.sql_editor.models import SqlActionName, SqlEditableModel, SqlEditError
from vg2c.sql_editor.parser import parse_sql
from vg2c.sql_editor.schema import SqlTableSchema, with_input_schemas
from vg2c.sql_editor.transform import (
    add_filter,
    add_join,
    add_selection,
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


CsvHeaderReader = Callable[[str], tuple[str, ...] | None]


def structured_sql_model(
    result: CompilationResult,
    parameter_id: str,
    changes: Iterable[ParameterChange] = (),
    *,
    csv_header: CsvHeaderReader | None = None,
) -> SqlEditableModel:
    step, invocation, parameter = _structured_parameter(result, parameter_id)
    value = _effective_parameter_value(result, parameter, changes)
    if not isinstance(value, str):
        raise SqlEditError("Structured SQL requires an editable string parameter.")
    model = parse_sql(value)
    block = next((item for item in result.resolved.blocks if item.index == step.block_index), None)
    if block is None or block.kind is not Kind.SQLITE_QUERY or csv_header is None:
        return model
    inputs = next((item for item in invocation.parameters if item.name == "inputs"), None)
    if inputs is None:
        return model
    specs = _effective_parameter_value(result, inputs, changes)
    schemas: list[SqlTableSchema] = []
    for spec in specs if isinstance(specs, list) else ():
        if isinstance(spec, str):
            path, table_name = spec, Path(spec).stem
        elif (
            isinstance(spec, (tuple, list))
            and len(spec) == 2
            and all(isinstance(value, str) for value in spec)
        ):
            path, table_name = spec
        else:
            continue
        if columns := csv_header(path):
            schemas.append(SqlTableSchema(table_name, columns))
    return with_input_schemas(model, tuple(schemas))


def apply_sql_action(
    result: CompilationResult,
    action: SqlAction,
    changes: Iterable[ParameterChange] = (),
    *,
    csv_header: CsvHeaderReader | None = None,
) -> ParameterChange:
    _, _, parameter = _structured_parameter(result, action.parameter_id)
    model = structured_sql_model(
        result, action.parameter_id, changes, csv_header=csv_header
    )
    sql = model.source

    args = dict(action.arguments)
    if "table_choice_id" in args:
        if "source" in args:
            raise SqlEditError("Specify either table_choice_id or source.")
        table = next(
            (item for item in model.table_choices if item.id == args["table_choice_id"]),
            None,
        )
        if table is None:
            raise SqlEditError("SQL table choice is no longer available; refresh the editor.")
        args["source"] = table.expression
    for choice_key, target_key in (
        ("column_choice_id", "expression"),
        ("left_choice_id", "left"),
        ("right_choice_id", "right"),
    ):
        if choice_key not in args:
            continue
        if target_key in args:
            raise SqlEditError(f"Specify either {choice_key} or {target_key}.")
        choice = next(
            (item for item in model.column_choices if item.id == args[choice_key]),
            None,
        )
        if choice is None:
            raise SqlEditError("SQL column choice is no longer available; refresh the editor.")
        args[target_key] = choice.expression
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
            **{
                key: args[key]
                for key in ("left", "operator", "right", "connector")
                if key in args
            },
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
        transformed = update_join_type(
            sql, _require(args, "join_id"), _require(args, "join_type")
        )
    elif name == "update-join-source":
        transformed = update_join_source(
            sql, _require(args, "join_id"), _require(args, "source")
        )
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
        transformed = update_source(
            sql, _require(args, "source_id"), _require(args, "source")
        )
    else:
        raise SqlEditError(f"Unsupported structured SQL action: {name}")

    return ParameterChange(parameter_id=parameter.id, value=transformed.sql)


def _structured_parameter(
    result: CompilationResult, parameter_id: str
) -> tuple[EmittedStep, EmittedInvocation, EmittedParameter]:
    for step in result.emitted.steps:
        for invocation in step.invocations:
            for parameter in invocation.parameters:
                if parameter.id != parameter_id:
                    continue
                if (
                    not parameter.definition
                    or "structured-sql" not in parameter.definition.capabilities
                ):
                    raise SqlEditError(
                        "This utility parameter does not expose structured SQL editing."
                    )
                if not parameter.editable:
                    raise SqlEditError(
                        parameter.read_only_reason or "SQL parameter is read-only."
                    )
                return step, invocation, parameter
    raise SqlEditError("SQL parameter no longer exists.")


def _effective_parameter_value(
    result: CompilationResult,
    parameter: EmittedParameter,
    changes: Iterable[ParameterChange],
) -> Any:
    projection = project_changes(result, changes)
    if not projection.valid:
        raise SqlEditError("; ".join(issue.message for issue in projection.issues))
    return next(
        (
            change.value
            for change in projection.values
            if change.parameter_id == parameter.id
        ),
        parameter.value,
    )


def _require(arguments: Mapping[str, Any], key: str) -> Any:
    if key not in arguments:
        raise SqlEditError(f"Missing SQL action argument: {key}")
    return arguments[key]


__all__ = [
    "SqlAction",
    "SqlActionName",
    "apply_sql_action",
    "structured_sql_model",
]
