from __future__ import annotations

from dataclasses import dataclass
from typing import Literal

SqlLogicalConnector = Literal["AND", "OR"]
SqlActionName = Literal[
    "add-selection", "update-selection", "remove-selection", "move-selection",
    "reorder-selection", "add-filter", "update-filter", "remove-filter",
    "add-join", "update-join-type", "update-join-source",
    "update-join-predicate", "remove-join-predicate", "remove-join", "update-source",
]


@dataclass(frozen=True, slots=True)
class SqlSpan:
    start: int
    end: int


@dataclass(frozen=True, slots=True)
class SqlSelection:
    id: str
    expression: str
    alias: str | None
    raw: str
    editable: bool
    read_only_reason: str | None
    span: SqlSpan


@dataclass(frozen=True, slots=True)
class SqlSource:
    id: str
    expression: str
    kind: Literal["from", "join"]
    editable: bool
    read_only_reason: str | None
    span: SqlSpan
    join_id: str | None


@dataclass(frozen=True, slots=True)
class SqlPredicate:
    id: str
    left: str
    operator: str
    right: str
    connector: SqlLogicalConnector | None
    raw: str
    editable: bool
    read_only_reason: str | None
    span: SqlSpan
    connector_span: SqlSpan | None


@dataclass(frozen=True, slots=True)
class SqlJoin:
    id: str
    join_type: str
    source: str
    predicates: tuple[SqlPredicate, ...]
    editable_type: bool
    editable_source: bool
    read_only_reason: str | None
    span: SqlSpan
    type_span: SqlSpan
    source_span: SqlSpan


@dataclass(frozen=True, slots=True)
class SqlEditCapabilities:
    selected: bool
    filters: bool
    joins: bool
    raw_sql: bool = True


@dataclass(frozen=True, slots=True)
class SqlEditableModel:
    source: str
    statement_span: SqlSpan
    selections: tuple[SqlSelection, ...]
    filters: tuple[SqlPredicate, ...]
    joins: tuple[SqlJoin, ...]
    sources: tuple[SqlSource, ...]
    capabilities: SqlEditCapabilities
    read_only_reason: str | None
    select_list_span: SqlSpan | None
    where_clause_span: SqlSpan | None
    where_body_span: SqlSpan | None
    from_clause_span: SqlSpan | None


@dataclass(frozen=True, slots=True)
class SqlTransformResult:
    sql: str
    model: SqlEditableModel


class SqlEditError(ValueError):
    pass


__all__ = [
    "SqlActionName", "SqlEditCapabilities", "SqlEditError", "SqlEditableModel",
    "SqlJoin", "SqlLogicalConnector", "SqlPredicate", "SqlSelection", "SqlSource",
    "SqlSpan", "SqlTransformResult",
]
