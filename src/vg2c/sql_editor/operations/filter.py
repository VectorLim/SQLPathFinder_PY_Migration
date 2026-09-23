from __future__ import annotations

from collections.abc import Mapping
from typing import Any

from vg2c.sql_editor.models import SqlEditError, SqlTransformResult
from vg2c.sql_editor.operations.base import SqlOperation
from vg2c.sql_editor.parser import parse_sql


class AddFilterOperation(SqlOperation):
    name = "add-filter"

    def apply(
        self,
        source: str,
        arguments: Mapping[str, Any],
    ) -> SqlTransformResult:
        model = parse_sql(source)
        if not model.capabilities.filters:
            raise SqlEditError(
                model.read_only_reason or "Filters are not structurally editable."
            )

        left = self.require(arguments, "left").strip()
        right = self.require(arguments, "right").strip()
        operator = self.normalize_operator(self.require(arguments, "operator"))
        connector = arguments.get("connector", "AND")
        if not left or not right:
            raise SqlEditError("Filter operands cannot be empty.")

        predicate = f"{left} {operator} {right}"
        if model.where_body_span:
            return self.finish(
                self.replace_span(
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
        return self.finish(
            self.replace_span(
                source,
                model.from_clause_span.end,
                model.from_clause_span.end,
                f"\nWHERE {predicate}",
            ),
            "filters",
        )


class UpdateFilterOperation(SqlOperation):
    name = "update-filter"

    def apply(
        self,
        source: str,
        arguments: Mapping[str, Any],
    ) -> SqlTransformResult:
        model = parse_sql(source)
        filter_id = self.require(arguments, "filter_id")
        predicate = next(
            (item for item in model.filters if item.id == filter_id),
            None,
        )
        if predicate is None or not predicate.editable:
            raise SqlEditError(
                predicate.read_only_reason if predicate else "Filter is not editable."
            )

        left = arguments.get("left")
        right = arguments.get("right")
        operator = arguments.get("operator")
        connector = arguments.get("connector")

        left_value = (predicate.left if left is None else left).strip()
        right_value = (predicate.right if right is None else right).strip()
        operator_value = self.normalize_operator(
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
        return self.finish(
            self.apply_replacements(source, replacements),
            "filters",
        )


class RemoveFilterOperation(SqlOperation):
    name = "remove-filter"

    def apply(
        self,
        source: str,
        arguments: Mapping[str, Any],
    ) -> SqlTransformResult:
        model = parse_sql(source)
        filter_id = self.require(arguments, "filter_id")
        index = next(
            (
                index
                for index, item in enumerate(model.filters)
                if item.id == filter_id
            ),
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
            return self.finish(
                self.replace_span(
                    source,
                    model.where_clause_span.start,
                    model.where_clause_span.end,
                    "",
                ),
                "filters",
            )

        if index > 0 and predicate.connector_span:
            return self.finish(
                self.replace_span(
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
        return self.finish(
            self.replace_span(
                source,
                predicate.span.start,
                next_predicate.connector_span.end,
                "",
            ),
            "filters",
        )
