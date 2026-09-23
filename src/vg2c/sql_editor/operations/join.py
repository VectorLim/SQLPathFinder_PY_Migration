from __future__ import annotations

from collections.abc import Mapping
from typing import Any

from vg2c.sql_editor.models import SqlEditError, SqlTransformResult
from vg2c.sql_editor.operations.base import JOIN_TYPES, SqlOperation
from vg2c.sql_editor.parser import parse_sql


class AddJoinOperation(SqlOperation):
    name = "add-join"

    def _transform(
        self,
        source: str,
        arguments: Mapping[str, Any],
    ) -> SqlTransformResult:
        model = parse_sql(source)
        if not model.capabilities.joins or not model.from_clause_span:
            raise SqlEditError(
                model.read_only_reason
                or "Joins are not structurally editable for this query."
            )

        join_type = self.require(arguments, "join_type").strip().upper()
        if join_type not in JOIN_TYPES or join_type == "CROSS":
            raise SqlEditError(
                "New keyed joins must use INNER, LEFT, RIGHT, or FULL."
            )

        source_expression = self.require(arguments, "source").strip()
        left = self.require(arguments, "left").strip()
        right = self.require(arguments, "right").strip()
        operator = self.normalize_operator(arguments.get("operator", "="))
        if not source_expression or not left or not right:
            raise SqlEditError(
                "Join source and key expressions cannot be empty."
            )

        clause = (
            f"\n{join_type} JOIN {source_expression} "
            f"ON {left} {operator} {right}"
        )
        return self.finish(
            self.replace_span(
                source,
                model.from_clause_span.end,
                model.from_clause_span.end,
                clause,
            ),
            "joins",
        )


class UpdateJoinTypeOperation(SqlOperation):
    name = "update-join-type"

    def _transform(
        self,
        source: str,
        arguments: Mapping[str, Any],
    ) -> SqlTransformResult:
        model = parse_sql(source)
        join_id = self.require(arguments, "join_id")
        join = next((item for item in model.joins if item.id == join_id), None)
        if join is None or not join.editable_type:
            raise SqlEditError(
                join.read_only_reason if join else "Join type is not editable."
            )

        normalized = self.require(arguments, "join_type").strip().upper()
        if normalized not in JOIN_TYPES:
            raise SqlEditError("Unsupported join type.")
        if normalized == "CROSS" and (
            join.predicates
            or (join.read_only_reason and "USING" in join.read_only_reason)
        ):
            raise SqlEditError("CROSS JOIN cannot retain ON/USING join keys.")

        return self.finish(
            self.replace_span(
                source,
                join.type_span.start,
                join.type_span.end,
                f"{normalized} JOIN",
            ),
            "joins",
        )


class UpdateJoinSourceOperation(SqlOperation):
    name = "update-join-source"

    def _transform(
        self,
        source: str,
        arguments: Mapping[str, Any],
    ) -> SqlTransformResult:
        model = parse_sql(source)
        join_id = self.require(arguments, "join_id")
        join = next((item for item in model.joins if item.id == join_id), None)
        if join is None or not join.editable_source:
            raise SqlEditError(
                join.read_only_reason if join else "Join source is not editable."
            )

        value = self.require(arguments, "source").strip()
        if not value:
            raise SqlEditError("Join source cannot be empty.")

        return self.finish(
            self.replace_span(
                source,
                join.source_span.start,
                join.source_span.end,
                value,
            ),
            "joins",
        )


class UpdateJoinPredicateOperation(SqlOperation):
    name = "update-join-predicate"

    def _transform(
        self,
        source: str,
        arguments: Mapping[str, Any],
    ) -> SqlTransformResult:
        model = parse_sql(source)
        join_id = self.require(arguments, "join_id")
        predicate_id = self.require(arguments, "predicate_id")
        join = next((item for item in model.joins if item.id == join_id), None)
        predicate = (
            next(
                (item for item in join.predicates if item.id == predicate_id),
                None,
            )
            if join
            else None
        )
        if predicate is None or not predicate.editable:
            raise SqlEditError(
                predicate.read_only_reason
                if predicate
                else "Join predicate is not editable."
            )

        left = arguments.get("left")
        operator = arguments.get("operator")
        right = arguments.get("right")
        left_value = (predicate.left if left is None else left).strip()
        right_value = (predicate.right if right is None else right).strip()
        operator_value = self.normalize_operator(
            predicate.operator if operator is None else operator
        )
        if not left_value or not right_value:
            raise SqlEditError("Join key expressions cannot be empty.")

        return self.finish(
            self.replace_span(
                source,
                predicate.span.start,
                predicate.span.end,
                f"{left_value} {operator_value} {right_value}",
            ),
            "joins",
        )


class RemoveJoinPredicateOperation(SqlOperation):
    name = "remove-join-predicate"

    def _transform(
        self,
        source: str,
        arguments: Mapping[str, Any],
    ) -> SqlTransformResult:
        model = parse_sql(source)
        join_id = self.require(arguments, "join_id")
        predicate_id = self.require(arguments, "predicate_id")
        join = next((item for item in model.joins if item.id == join_id), None)
        if join is None:
            raise SqlEditError("Join no longer exists.")
        if len(join.predicates) <= 1:
            raise SqlEditError(
                "A join using ON must keep at least one predicate."
            )

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
            return self.finish(
                self.replace_span(
                    source,
                    predicate.connector_span.start,
                    predicate.span.end,
                    "",
                ),
                "joins",
            )

        next_predicate = join.predicates[index + 1]
        if not next_predicate.connector_span:
            raise SqlEditError(
                "Join predicate connector could not be isolated safely."
            )
        return self.finish(
            self.replace_span(
                source,
                predicate.span.start,
                next_predicate.connector_span.end,
                "",
            ),
            "joins",
        )


class RemoveJoinOperation(SqlOperation):
    name = "remove-join"

    def _transform(
        self,
        source: str,
        arguments: Mapping[str, Any],
    ) -> SqlTransformResult:
        model = parse_sql(source)
        join_id = self.require(arguments, "join_id")
        join = next((item for item in model.joins if item.id == join_id), None)
        if join is None or (
            join.read_only_reason and "NATURAL" in join.read_only_reason
        ):
            raise SqlEditError(
                join.read_only_reason if join else "Join is not removable."
            )

        return self.finish(
            self.replace_span(source, join.span.start, join.span.end, ""),
            "joins",
        )
