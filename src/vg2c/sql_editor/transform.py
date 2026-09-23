from __future__ import annotations

from collections.abc import Mapping
from typing import Any

from vg2c.sql_editor.models import (
    SqlActionName,
    SqlLogicalConnector,
    SqlTransformResult,
)
from vg2c.sql_editor.operations import (
    FILTER_OPERATORS,
    JOIN_TYPES,
    get_sql_operation,
)


def _apply(
    name: SqlActionName,
    source: str,
    arguments: Mapping[str, Any],
) -> SqlTransformResult:
    return get_sql_operation(name).apply(source, arguments)


def add_filter(
    source: str,
    *,
    left: str,
    operator: str,
    right: str,
    connector: SqlLogicalConnector = "AND",
) -> SqlTransformResult:
    return _apply(
        "add-filter",
        source,
        {
            "left": left,
            "operator": operator,
            "right": right,
            "connector": connector,
        },
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
    return _apply(
        "add-join",
        source,
        {
            "join_type": join_type,
            "source": source_expression,
            "left": left,
            "right": right,
            "operator": operator,
        },
    )


def add_selection(source: str, expression: str) -> SqlTransformResult:
    return _apply("add-selection", source, {"expression": expression})


def update_source(
    source: str,
    source_id: str,
    value: str,
) -> SqlTransformResult:
    return _apply(
        "update-source",
        source,
        {"source_id": source_id, "source": value},
    )


def update_selection(
    source: str,
    selection_id: str,
    *,
    expression: str | None = None,
    alias: str | None | object = ...,
) -> SqlTransformResult:
    arguments: dict[str, Any] = {"selection_id": selection_id}
    if expression is not None:
        arguments["expression"] = expression
    if alias is not ...:
        arguments["alias"] = alias
    return _apply("update-selection", source, arguments)


def remove_selection(source: str, selection_id: str) -> SqlTransformResult:
    return _apply(
        "remove-selection",
        source,
        {"selection_id": selection_id},
    )


def reorder_selection(
    source: str,
    selection_id: str,
    target_index: int,
) -> SqlTransformResult:
    return _apply(
        "reorder-selection",
        source,
        {
            "selection_id": selection_id,
            "target_index": target_index,
        },
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
    return _apply(
        "update-filter",
        source,
        {
            "filter_id": filter_id,
            "left": left,
            "operator": operator,
            "right": right,
            "connector": connector,
        },
    )


def remove_filter(source: str, filter_id: str) -> SqlTransformResult:
    return _apply("remove-filter", source, {"filter_id": filter_id})


def update_join_type(
    source: str,
    join_id: str,
    join_type: str,
) -> SqlTransformResult:
    return _apply(
        "update-join-type",
        source,
        {"join_id": join_id, "join_type": join_type},
    )


def update_join_source(
    source: str,
    join_id: str,
    value: str,
) -> SqlTransformResult:
    return _apply(
        "update-join-source",
        source,
        {"join_id": join_id, "source": value},
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
    return _apply(
        "update-join-predicate",
        source,
        {
            "join_id": join_id,
            "predicate_id": predicate_id,
            "left": left,
            "operator": operator,
            "right": right,
        },
    )


def remove_join_predicate(
    source: str,
    join_id: str,
    predicate_id: str,
) -> SqlTransformResult:
    return _apply(
        "remove-join-predicate",
        source,
        {"join_id": join_id, "predicate_id": predicate_id},
    )


def remove_join(source: str, join_id: str) -> SqlTransformResult:
    return _apply("remove-join", source, {"join_id": join_id})


__all__ = [
    "FILTER_OPERATORS",
    "JOIN_TYPES",
    "add_filter",
    "add_join",
    "add_selection",
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
