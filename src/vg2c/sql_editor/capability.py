from __future__ import annotations

from collections.abc import Callable, Iterable, Mapping
from dataclasses import dataclass, replace
from pathlib import Path
import re
from typing import Any

from vg2c.compilation import CompilationResult
from vg2c.editing import SemanticChange, project_changes
from vg2c.emitter.models import EmittedInvocation, EmittedParameter, EmittedStep
from vg2c.kind import Kind
from vg2c.semantics import build_semantic_model
from vg2c.sql_editor.models import (
    SqlActionName, SqlEditCapabilities, SqlEditableModel, SqlEditError, SqlFileList,
)
from vg2c.sql_editor.parser import parse_sql
from vg2c.sql_editor.schema import SqlTableSchema, with_input_schemas
from vg2c.sql_editor.operations import get_sql_operation
from vg2c.utilities._emit_helpers import scan_sql_get_csv_list_calls


@dataclass(frozen=True, slots=True)
class SqlAction:
    binding_id: str
    action: SqlActionName
    arguments: Mapping[str, Any]


CsvHeaderReader = Callable[[str], tuple[str, ...] | None]


def structured_sql_model(
    result: CompilationResult,
    binding_id: str,
    changes: Iterable[SemanticChange] = (),
    *,
    csv_header: CsvHeaderReader | None = None,
    file_choices: Iterable[str] = (),
) -> SqlEditableModel:
    changes = tuple(changes)
    step, invocation, parameter = _structured_parameter(
        result, binding_id, require_editable=False
    )
    if not parameter.editable:
        block = next(item for item in result.resolved.blocks if item.index == step.block_index)
        projection = project_changes(result, changes)
        if not projection.valid:
            raise SqlEditError("; ".join(issue.message for issue in projection.issues))
        semantic = build_semantic_model(result, projection.effective_values)
        bindings = {item.id: item for item in semantic.bindings}
        original = block.resolved_body
        calls = scan_sql_get_csv_list_calls(original)
        source = original
        lists: list[SqlFileList] = []
        choices = tuple(dict.fromkeys(file_choices))
        for index in reversed(range(len(calls))):
            call = calls[index]
            item = bindings.get(f"{invocation.id}:sql-file-list:{index}")
            if item is None:
                continue
            lists.append(SqlFileList(item.id, item.value, call.column_ref, call.lead_in, choices))
            source = _replace_file_call_path(source, call, item.value)
        parsed = parse_sql(source)
        return replace(
            parsed,
            file_lists=tuple(reversed(lists)),
            capabilities=SqlEditCapabilities(False, False, False),
            read_only_reason=(
                "SQL structure is read-only; file-list inputs can be changed."
                if lists else parameter.read_only_reason
            ),
        )
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
    changes: Iterable[SemanticChange] = (),
    *,
    csv_header: CsvHeaderReader | None = None,
    file_choices: Iterable[str] = (),
) -> SemanticChange:
    if action.action == "update-file-list":
        _, invocation, _ = _structured_parameter(
            result, action.binding_id, require_editable=False
        )
        model = structured_sql_model(
            result, action.binding_id, changes, file_choices=file_choices
        )
        target = next(
            (item for item in model.file_lists
             if item.id == action.arguments.get("file_list_id")),
            None,
        )
        if target is None or not target.id.startswith(f"{invocation.id}:sql-file-list:"):
            raise SqlEditError("File-backed filter is no longer available; refresh the editor.")
        path = action.arguments.get("path")
        if not isinstance(path, str) or path not in target.choices:
            raise SqlEditError("File choice is not available at this operation.")
        return SemanticChange(binding_id=target.id, value=path)
    _, _, parameter = _structured_parameter(result, action.binding_id)
    model = structured_sql_model(
        result, action.binding_id, changes, csv_header=csv_header
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
    transformed = get_sql_operation(action.action).apply(sql, args)

    return SemanticChange(binding_id=parameter.id, value=transformed.sql)


def _structured_parameter(
    result: CompilationResult, binding_id: str, *, require_editable: bool = True
) -> tuple[EmittedStep, EmittedInvocation, EmittedParameter]:
    for step in result.emitted.steps:
        for invocation in step.invocations:
            for parameter in invocation.parameters:
                if parameter.id != binding_id:
                    continue
                if (
                    not parameter.definition
                    or "structured-sql" not in parameter.definition.capabilities
                ):
                    raise SqlEditError(
                        "This utility parameter does not expose structured SQL editing."
                    )
                if require_editable and not parameter.editable:
                    raise SqlEditError(
                        parameter.read_only_reason or "SQL parameter is read-only."
                    )
                return step, invocation, parameter
    raise SqlEditError("SQL parameter no longer exists.")


def _replace_file_call_path(source: str, call, path: str) -> str:
    original = source[call.start:call.end]
    match = re.match(
        r"(SQL_Get_CSV_List\s*\(\s*)(['\"])(.*?)\2",
        original,
        re.IGNORECASE | re.DOTALL,
    )
    if match is None or not isinstance(path, str) or "'" in path or '"' in path:
        return source
    suffix = call.csv_path[len(call.source_path):]
    replacement = f"{match.group(1)}{match.group(2)}{path}{suffix}{match.group(2)}"
    updated = replacement + original[match.end():]
    return source[:call.start] + updated + source[call.end:]


def _effective_parameter_value(
    result: CompilationResult,
    parameter: EmittedParameter,
    changes: Iterable[SemanticChange],
) -> Any:
    projection = project_changes(result, changes)
    if not projection.valid:
        raise SqlEditError("; ".join(issue.message for issue in projection.issues))
    return next(
        (
            change.value
            for change in projection.values
            if change.binding_id == parameter.id
        ),
        parameter.value,
    )



__all__ = [
    "SqlAction",
    "SqlActionName",
    "apply_sql_action",
    "structured_sql_model",
]
