from __future__ import annotations

from abc import ABC, abstractmethod
from collections.abc import Mapping
from typing import Any, ClassVar

from vg2c.sql_editor.models import SqlActionName, SqlEditError, SqlTransformResult
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


class SqlOperation(ABC):
    """One structured SQL text operation.

    Shared dispatch and required-argument behavior lives here; subclasses only
    describe the arguments and transform that are specific to one operation.
    """

    name: ClassVar[SqlActionName]

    def apply(
        self,
        source: str,
        arguments: Mapping[str, Any],
    ) -> SqlTransformResult:
        return self._transform(source, arguments)

    @abstractmethod
    def _transform(
        self,
        source: str,
        arguments: Mapping[str, Any],
    ) -> SqlTransformResult:
        raise NotImplementedError

    @staticmethod
    def require(arguments: Mapping[str, Any], key: str) -> Any:
        if key not in arguments:
            raise SqlEditError(f"Missing SQL action argument: {key}")
        return arguments[key]


class AddSelectionOperation(SqlOperation):
    name = "add-selection"

    def _transform(self, source: str, arguments: Mapping[str, Any]) -> SqlTransformResult:
        return add_selection(source, self.require(arguments, "expression"))


class UpdateSelectionOperation(SqlOperation):
    name = "update-selection"

    def _transform(self, source: str, arguments: Mapping[str, Any]) -> SqlTransformResult:
        kwargs: dict[str, Any] = {}
        if "expression" in arguments:
            kwargs["expression"] = arguments["expression"]
        if "alias" in arguments:
            kwargs["alias"] = arguments["alias"]
        return update_selection(
            source,
            self.require(arguments, "selection_id"),
            **kwargs,
        )


class RemoveSelectionOperation(SqlOperation):
    name = "remove-selection"

    def _transform(self, source: str, arguments: Mapping[str, Any]) -> SqlTransformResult:
        return remove_selection(source, self.require(arguments, "selection_id"))


class ReorderSelectionOperation(SqlOperation):
    name = "reorder-selection"

    def _transform(self, source: str, arguments: Mapping[str, Any]) -> SqlTransformResult:
        return reorder_selection(
            source,
            self.require(arguments, "selection_id"),
            int(self.require(arguments, "target_index")),
        )


class AddFilterOperation(SqlOperation):
    name = "add-filter"

    def _transform(self, source: str, arguments: Mapping[str, Any]) -> SqlTransformResult:
        return add_filter(
            source,
            left=self.require(arguments, "left"),
            operator=self.require(arguments, "operator"),
            right=self.require(arguments, "right"),
            connector=arguments.get("connector", "AND"),
        )


class UpdateFilterOperation(SqlOperation):
    name = "update-filter"

    def _transform(self, source: str, arguments: Mapping[str, Any]) -> SqlTransformResult:
        return update_filter(
            source,
            self.require(arguments, "filter_id"),
            **{
                key: arguments[key]
                for key in ("left", "operator", "right", "connector")
                if key in arguments
            },
        )


class RemoveFilterOperation(SqlOperation):
    name = "remove-filter"

    def _transform(self, source: str, arguments: Mapping[str, Any]) -> SqlTransformResult:
        return remove_filter(source, self.require(arguments, "filter_id"))


class AddJoinOperation(SqlOperation):
    name = "add-join"

    def _transform(self, source: str, arguments: Mapping[str, Any]) -> SqlTransformResult:
        return add_join(
            source,
            join_type=self.require(arguments, "join_type"),
            source_expression=self.require(arguments, "source"),
            left=self.require(arguments, "left"),
            right=self.require(arguments, "right"),
            operator=arguments.get("operator", "="),
        )


class UpdateJoinTypeOperation(SqlOperation):
    name = "update-join-type"

    def _transform(self, source: str, arguments: Mapping[str, Any]) -> SqlTransformResult:
        return update_join_type(
            source,
            self.require(arguments, "join_id"),
            self.require(arguments, "join_type"),
        )


class UpdateJoinSourceOperation(SqlOperation):
    name = "update-join-source"

    def _transform(self, source: str, arguments: Mapping[str, Any]) -> SqlTransformResult:
        return update_join_source(
            source,
            self.require(arguments, "join_id"),
            self.require(arguments, "source"),
        )


class UpdateJoinPredicateOperation(SqlOperation):
    name = "update-join-predicate"

    def _transform(self, source: str, arguments: Mapping[str, Any]) -> SqlTransformResult:
        return update_join_predicate(
            source,
            self.require(arguments, "join_id"),
            self.require(arguments, "predicate_id"),
            **{
                key: arguments[key]
                for key in ("left", "operator", "right")
                if key in arguments
            },
        )


class RemoveJoinPredicateOperation(SqlOperation):
    name = "remove-join-predicate"

    def _transform(self, source: str, arguments: Mapping[str, Any]) -> SqlTransformResult:
        return remove_join_predicate(
            source,
            self.require(arguments, "join_id"),
            self.require(arguments, "predicate_id"),
        )


class RemoveJoinOperation(SqlOperation):
    name = "remove-join"

    def _transform(self, source: str, arguments: Mapping[str, Any]) -> SqlTransformResult:
        return remove_join(source, self.require(arguments, "join_id"))


class UpdateSourceOperation(SqlOperation):
    name = "update-source"

    def _transform(self, source: str, arguments: Mapping[str, Any]) -> SqlTransformResult:
        return update_source(
            source,
            self.require(arguments, "source_id"),
            self.require(arguments, "source"),
        )


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


__all__ = ["SqlOperation", "get_sql_operation"]
