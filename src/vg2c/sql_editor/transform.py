from __future__ import annotations

import re
from typing import Literal

from vg2c.sql_editor.models import (
    SqlEditError,
    SqlLogicalConnector,
    SqlTransformResult,
)
from vg2c.sql_editor.parser import parse_sql

FILTER_OPERATORS = (
    "=", "!=", "<>", "<", "<=", ">", ">=", "LIKE", "NOT LIKE", "ILIKE",
    "IN", "NOT IN", "IS", "IS NOT",
)
JOIN_TYPES = ("INNER", "LEFT", "RIGHT", "FULL", "CROSS")


def add_filter(
    source: str,
    *,
    left: str,
    operator: str,
    right: str,
    connector: SqlLogicalConnector = "AND",
) -> SqlTransformResult:
    model = parse_sql(source)
    if not model.capabilities.filters:
        raise SqlEditError(
            model.read_only_reason or "Filters are not structurally editable."
        )
    left = left.strip()
    right = right.strip()
    operator = _normalize_operator(operator)
    if not left or not right:
        raise SqlEditError("Filter operands cannot be empty.")
    predicate = f"{left} {operator} {right}"
    if model.where_body_span:
        return _finish(
            _replace_span(
                source,
                model.where_body_span.end,
                model.where_body_span.end,
                f"\n{connector} {predicate}",
            ),
            "filters",
        )
    if not model.from_clause_span:
        raise SqlEditError(
            "A WHERE filter cannot be added because the FROM clause is unavailable."
        )
    return _finish(
        _replace_span(
            source,
            model.from_clause_span.end,
            model.from_clause_span.end,
            f"\nWHERE {predicate}",
        ),
        "filters",
    )


def add_join(
    source: str,
    *,
    join_type: str,
    source_expression: str,
    left: str,
    right: str,
    operator: str = "=",
) -> SqlTransformResult:
    model = parse_sql(source)
    if not model.capabilities.joins or not model.from_clause_span:
        raise SqlEditError(
            model.read_only_reason or "Joins are not structurally editable for this query."
        )
    join_type = join_type.strip().upper()
    if join_type not in JOIN_TYPES or join_type == "CROSS":
        raise SqlEditError("New keyed joins must use INNER, LEFT, RIGHT, or FULL.")
    source_expression = source_expression.strip()
    left = left.strip()
    right = right.strip()
    operator = _normalize_operator(operator)
    if not source_expression or not left or not right:
        raise SqlEditError("Join source and key expressions cannot be empty.")
    clause = (
        f"\n{join_type} JOIN {source_expression} ON {left} {operator} {right}"
    )
    return _finish(
        _replace_span(
            source, model.from_clause_span.end, model.from_clause_span.end, clause
        ),
        "joins",
    )


def add_selection(source: str, expression: str) -> SqlTransformResult:
    model = parse_sql(source)
    if not model.capabilities.selected or not model.select_list_span:
        raise SqlEditError("SELECT list is not structurally editable.")
    expression = expression.strip()
    if not expression:
        raise SqlEditError("Selected expression cannot be empty.")
    current = source[model.select_list_span.start:model.select_list_span.end]
    replacement = f"{current}, {expression}" if model.selections else expression
    return _finish(
        _replace_span(
            source,
            model.select_list_span.start,
            model.select_list_span.end,
            replacement,
        ),
        "selected",
    )


def update_source(source: str, source_id: str, value: str) -> SqlTransformResult:
    model = parse_sql(source)
    sql_source = next((item for item in model.sources if item.id == source_id), None)
    if sql_source is None or not sql_source.editable:
        raise SqlEditError(
            sql_source.read_only_reason if sql_source else "Source is not editable."
        )
    expression = value.strip()
    if not expression:
        raise SqlEditError("Source cannot be empty.")
    capability = "joins" if sql_source.kind == "join" else "selected"
    return _finish(
        _replace_span(source, sql_source.span.start, sql_source.span.end, expression),
        capability,
    )


def update_selection(
    source: str,
    selection_id: str,
    *,
    expression: str | None = None,
    alias: str | None | object = ...,
) -> SqlTransformResult:
    model = parse_sql(source)
    selection = next(
        (item for item in model.selections if item.id == selection_id), None
    )
    if selection is None or not selection.editable:
        raise SqlEditError(
            selection.read_only_reason if selection else "Selection is not editable."
        )
    next_expression = (
        selection.expression if expression is None else expression
    ).strip()
    if not next_expression:
        raise SqlEditError("Selected expression cannot be empty.")
    next_alias = selection.alias if alias is ... else _clean_alias(alias)
    replacement = (
        f"{next_expression} AS {next_alias}" if next_alias else next_expression
    )
    return _finish(
        _replace_span(source, selection.span.start, selection.span.end, replacement),
        "selected",
    )


def remove_selection(source: str, selection_id: str) -> SqlTransformResult:
    model = parse_sql(source)
    if len(model.selections) <= 1:
        raise SqlEditError("A SELECT query must keep at least one selected expression.")
    index = next(
        (index for index, item in enumerate(model.selections) if item.id == selection_id),
        -1,
    )
    selection = model.selections[index] if index >= 0 else None
    if selection is None or not selection.editable:
        raise SqlEditError(
            selection.read_only_reason if selection else "Selection is not removable."
        )
    start = selection.span.start
    end = selection.span.end
    if index < len(model.selections) - 1:
        end = model.selections[index + 1].span.start
    else:
        start = model.selections[index - 1].span.end
    return _finish(_replace_span(source, start, end, ""), "selected")


def move_selection(
    source: str, selection_id: str, direction: Literal[-1, 1]
) -> SqlTransformResult:
    model = parse_sql(source)
    if not model.select_list_span:
        raise SqlEditError("SELECT list is not structurally editable.")
    index = next(
        (index for index, item in enumerate(model.selections) if item.id == selection_id),
        -1,
    )
    target = index + direction
    if index < 0 or target < 0 or target >= len(model.selections):
        raise SqlEditError("Selection cannot move further.")
    if not model.selections[index].editable or not model.selections[target].editable:
        raise SqlEditError("Read-only selections cannot be reordered.")
    rows = [
        source[item.span.start:item.span.end] for item in model.selections
    ]
    rows[index], rows[target] = rows[target], rows[index]
    separator = _selection_separator(source, model)
    return _finish(
        _replace_span(
            source,
            model.select_list_span.start,
            model.select_list_span.end,
            separator.join(rows),
        ),
        "selected",
    )


def reorder_selection(
    source: str, selection_id: str, target_index: int
) -> SqlTransformResult:
    model = parse_sql(source)
    if not model.select_list_span:
        raise SqlEditError("SELECT list is not structurally editable.")
    index = next(
        (index for index, item in enumerate(model.selections) if item.id == selection_id),
        -1,
    )
    if (
        index < 0
        or target_index < 0
        or target_index >= len(model.selections)
    ):
        raise SqlEditError("Selection reorder target is unavailable.")
    if index == target_index:
        return SqlTransformResult(source, model)
    if (
        not model.selections[index].editable
        or not model.selections[target_index].editable
    ):
        raise SqlEditError("Read-only selections cannot be reordered.")
    rows = [
        source[item.span.start:item.span.end] for item in model.selections
    ]
    moved = rows.pop(index)
    rows.insert(target_index, moved)
    return _finish(
        _replace_span(
            source,
            model.select_list_span.start,
            model.select_list_span.end,
            _selection_separator(source, model).join(rows),
        ),
        "selected",
    )


def update_filter(
    source: str,
    filter_id: str,
    *,
    left: str | None = None,
    operator: str | None = None,
    right: str | None = None,
    connector: SqlLogicalConnector | None = None,
) -> SqlTransformResult:
    model = parse_sql(source)
    predicate = next((item for item in model.filters if item.id == filter_id), None)
    if predicate is None or not predicate.editable:
        raise SqlEditError(
            predicate.read_only_reason if predicate else "Filter is not editable."
        )
    left_value = (predicate.left if left is None else left).strip()
    right_value = (predicate.right if right is None else right).strip()
    operator_value = _normalize_operator(
        predicate.operator if operator is None else operator
    )
    if not left_value or not right_value:
        raise SqlEditError("Filter operands cannot be empty.")
    replacements = [
        (
            predicate.span.start,
            predicate.span.end,
            f"{left_value} {operator_value} {right_value}",
        )
    ]
    if connector and predicate.connector_span:
        replacements.append(
            (
                predicate.connector_span.start,
                predicate.connector_span.end,
                connector,
            )
        )
    return _finish(_apply_replacements(source, replacements), "filters")


def remove_filter(source: str, filter_id: str) -> SqlTransformResult:
    model = parse_sql(source)
    index = next(
        (index for index, item in enumerate(model.filters) if item.id == filter_id),
        -1,
    )
    predicate = model.filters[index] if index >= 0 else None
    if predicate is None or not predicate.editable:
        raise SqlEditError(
            predicate.read_only_reason if predicate else "Filter is not removable."
        )
    if len(model.filters) == 1:
        if not model.where_clause_span:
            raise SqlEditError("WHERE clause span is unavailable.")
        return _finish(
            _replace_span(
                source,
                model.where_clause_span.start,
                model.where_clause_span.end,
                "",
            ),
            "filters",
        )
    if index > 0 and predicate.connector_span:
        return _finish(
            _replace_span(
                source,
                predicate.connector_span.start,
                predicate.span.end,
                "",
            ),
            "filters",
        )
    next_predicate = model.filters[index + 1]
    if not next_predicate.connector_span:
        raise SqlEditError("Filter connector could not be isolated safely.")
    return _finish(
        _replace_span(
            source,
            predicate.span.start,
            next_predicate.connector_span.end,
            "",
        ),
        "filters",
    )


def update_join_type(
    source: str, join_id: str, join_type: str
) -> SqlTransformResult:
    model = parse_sql(source)
    join = next((item for item in model.joins if item.id == join_id), None)
    if join is None or not join.editable_type:
        raise SqlEditError(
            join.read_only_reason if join else "Join type is not editable."
        )
    normalized = join_type.strip().upper()
    if normalized not in JOIN_TYPES:
        raise SqlEditError("Unsupported join type.")
    if normalized == "CROSS" and (
        join.predicates
        or (join.read_only_reason and "USING" in join.read_only_reason)
    ):
        raise SqlEditError("CROSS JOIN cannot retain ON/USING join keys.")
    return _finish(
        _replace_span(
            source,
            join.type_span.start,
            join.type_span.end,
            f"{normalized} JOIN",
        ),
        "joins",
    )


def update_join_source(
    source: str, join_id: str, value: str
) -> SqlTransformResult:
    model = parse_sql(source)
    join = next((item for item in model.joins if item.id == join_id), None)
    if join is None or not join.editable_source:
        raise SqlEditError(
            join.read_only_reason if join else "Join source is not editable."
        )
    value = value.strip()
    if not value:
        raise SqlEditError("Join source cannot be empty.")
    return _finish(
        _replace_span(source, join.source_span.start, join.source_span.end, value),
        "joins",
    )


def update_join_predicate(
    source: str,
    join_id: str,
    predicate_id: str,
    *,
    left: str | None = None,
    operator: str | None = None,
    right: str | None = None,
) -> SqlTransformResult:
    model = parse_sql(source)
    join = next((item for item in model.joins if item.id == join_id), None)
    predicate = (
        next((item for item in join.predicates if item.id == predicate_id), None)
        if join
        else None
    )
    if predicate is None or not predicate.editable:
        raise SqlEditError(
            predicate.read_only_reason
            if predicate
            else "Join predicate is not editable."
        )
    left_value = (predicate.left if left is None else left).strip()
    right_value = (predicate.right if right is None else right).strip()
    operator_value = _normalize_operator(
        predicate.operator if operator is None else operator
    )
    if not left_value or not right_value:
        raise SqlEditError("Join key expressions cannot be empty.")
    return _finish(
        _replace_span(
            source,
            predicate.span.start,
            predicate.span.end,
            f"{left_value} {operator_value} {right_value}",
        ),
        "joins",
    )


def remove_join_predicate(
    source: str, join_id: str, predicate_id: str
) -> SqlTransformResult:
    model = parse_sql(source)
    join = next((item for item in model.joins if item.id == join_id), None)
    if join is None:
        raise SqlEditError("Join no longer exists.")
    if len(join.predicates) <= 1:
        raise SqlEditError("A join using ON must keep at least one predicate.")
    index = next(
        (
            index
            for index, item in enumerate(join.predicates)
            if item.id == predicate_id
        ),
        -1,
    )
    predicate = join.predicates[index] if index >= 0 else None
    if predicate is None or not predicate.editable:
        raise SqlEditError(
            predicate.read_only_reason
            if predicate
            else "Join predicate is not removable."
        )
    if index > 0 and predicate.connector_span:
        return _finish(
            _replace_span(
                source,
                predicate.connector_span.start,
                predicate.span.end,
                "",
            ),
            "joins",
        )
    next_predicate = join.predicates[index + 1]
    if not next_predicate.connector_span:
        raise SqlEditError("Join predicate connector could not be isolated safely.")
    return _finish(
        _replace_span(
            source,
            predicate.span.start,
            next_predicate.connector_span.end,
            "",
        ),
        "joins",
    )


def remove_join(source: str, join_id: str) -> SqlTransformResult:
    model = parse_sql(source)
    join = next((item for item in model.joins if item.id == join_id), None)
    if join is None or (
        join.read_only_reason and "NATURAL" in join.read_only_reason
    ):
        raise SqlEditError(
            join.read_only_reason if join else "Join is not removable."
        )
    return _finish(
        _replace_span(source, join.span.start, join.span.end, ""),
        "joins",
    )


def _clean_alias(value: str | None | object) -> str | None:
    if value is None or value is ...:
        return None
    alias = str(value).strip()
    if not alias:
        return None
    plain = re.compile(r"^[A-Za-z_][A-Za-z0-9_$#]*$")
    quoted = re.compile(r'^(?:"(?:[^"]|"")+"|`[^`]+`|\[[^\]]+\])$')
    if not plain.match(alias) and not quoted.match(alias):
        raise SqlEditError(
            "Alias must be an identifier; quote aliases that contain spaces or punctuation."
        )
    return alias


def _normalize_operator(value: str) -> str:
    normalized = " ".join(value.strip().upper().split())
    if normalized not in FILTER_OPERATORS:
        raise SqlEditError("Unsupported predicate operator.")
    return normalized


def _finish(
    sql: str, capability: Literal["selected", "filters", "joins"]
) -> SqlTransformResult:
    model = parse_sql(sql)
    if not getattr(model.capabilities, capability):
        raise SqlEditError(
            model.read_only_reason
            or f"Updated SQL can no longer be edited safely in {capability}."
        )
    return SqlTransformResult(sql, model)


def _replace_span(source: str, start: int, end: int, replacement: str) -> str:
    return f"{source[:start]}{replacement}{source[end:]}"


def _apply_replacements(
    source: str, replacements: list[tuple[int, int, str]]
) -> str:
    result = source
    for start, end, replacement in sorted(
        replacements, key=lambda item: item[0], reverse=True
    ):
        result = _replace_span(result, start, end, replacement)
    return result


def _selection_separator(source: str, model) -> str:
    if len(model.selections) > 1:
        separator = source[
            model.selections[0].span.end:model.selections[1].span.start
        ]
        if "," in separator:
            return separator
    return ", "


__all__ = [
    "FILTER_OPERATORS",
    "JOIN_TYPES",
    "add_filter",
    "add_join",
    "add_selection",
    "move_selection",
    "remove_filter",
    "remove_join",
    "remove_join_predicate",
    "remove_selection",
    "reorder_selection",
    "update_filter",
    "update_join_predicate",
    "update_join_source",
    "update_join_type",
    "update_selection",
    "update_source",
]
