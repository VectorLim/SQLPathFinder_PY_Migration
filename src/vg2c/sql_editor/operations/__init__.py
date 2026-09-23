from __future__ import annotations

from vg2c.sql_editor.models import SqlActionName, SqlEditError
from vg2c.sql_editor.operations.base import (
    FILTER_OPERATORS,
    JOIN_TYPES,
    SqlOperation,
)
from vg2c.sql_editor.operations.filter import (
    AddFilterOperation,
    RemoveFilterOperation,
    UpdateFilterOperation,
)
from vg2c.sql_editor.operations.join import (
    AddJoinOperation,
    RemoveJoinOperation,
    RemoveJoinPredicateOperation,
    UpdateJoinPredicateOperation,
    UpdateJoinSourceOperation,
    UpdateJoinTypeOperation,
)
from vg2c.sql_editor.operations.selection import (
    AddSelectionOperation,
    RemoveSelectionOperation,
    ReorderSelectionOperation,
    UpdateSelectionOperation,
)
from vg2c.sql_editor.operations.source import UpdateSourceOperation

_SQL_OPERATIONS: dict[SqlActionName, SqlOperation] = {
    operation.name: operation
    for operation in (
        AddSelectionOperation(),
        UpdateSelectionOperation(),
        RemoveSelectionOperation(),
        ReorderSelectionOperation(),
        AddFilterOperation(),
        UpdateFilterOperation(),
        RemoveFilterOperation(),
        AddJoinOperation(),
        UpdateJoinTypeOperation(),
        UpdateJoinSourceOperation(),
        UpdateJoinPredicateOperation(),
        RemoveJoinPredicateOperation(),
        RemoveJoinOperation(),
        UpdateSourceOperation(),
    )
}


def get_sql_operation(name: SqlActionName) -> SqlOperation:
    try:
        return _SQL_OPERATIONS[name]
    except KeyError as exc:
        raise SqlEditError(f"Unsupported structured SQL action: {name}") from exc


__all__ = [
    "FILTER_OPERATORS",
    "JOIN_TYPES",
    "SqlOperation",
    "get_sql_operation",
]
