from __future__ import annotations

import re
from typing import Literal

from vg2c_ui.domain.semantic_models import (
    JoinPatch,
    PredicatePatch,
    SelectionPatch,
    SqlEditableModel,
    SqlPredicate,
    SqlTransformResult,
)
from vg2c_ui.services._sql_parser import parse_sql

FILTER_OPERATORS = frozenset(
    (
        "=", "!=", "<>", "<", "<=", ">", ">=", "LIKE", "NOT LIKE",
        "ILIKE", "IN", "NOT IN", "IS", "IS NOT",
    )
)
JOIN_TYPES = frozenset(("INNER", "LEFT", "RIGHT", "FULL", "CROSS"))


class SqlEditError(ValueError):
    pass


class SqlModelService:
    """Authoritative backend structured-SQL parser and deterministic editor."""

    def parse(self, sql_text: str) -> SqlEditableModel:
        return parse_sql(sql_text)

    def add_selection(self, sql_text: str, expression: str) -> SqlTransformResult:
        model = self.parse(sql_text)
        if not model.capabilities.selected or model.select_list_span is None:
            raise SqlEditError("SELECT list is not structurally editable.")
        value = expression.strip()
        if not value:
            raise SqlEditError("Selected expression cannot be empty.")
        current = sql_text[model.select_list_span.start : model.select_list_span.end]
        replacement = f"{current}, {value}" if model.selections else value
        return self._finish(
            _replace_span(
                sql_text, model.select_list_span.start, model.select_list_span.end, replacement
            ),
            "selected",
        )

    def update_selection(
        self, sql_text: str, parsed_id: str, patch: SelectionPatch
    ) -> SqlTransformResult:
        parsed_id = _target_id(parsed_id)
        model = self.parse(sql_text)
        selection = next((item for item in model.selections if item.id == parsed_id), None)
        if selection is None or not selection.editable:
            reason = selection.read_only_reason if selection else "Selection is not editable."
            raise SqlEditError(reason)
        expression = _patch_string(patch, "expression", selection.expression).strip()
        if not expression:
            raise SqlEditError("Selected expression cannot be empty.")
        alias = selection.alias
        if "alias" in patch.model_fields_set:
            alias = _clean_alias(patch.alias)
        replacement = f"{expression}{f' AS {alias}' if alias else ''}"
        return self._finish(
            _replace_span(sql_text, selection.span.start, selection.span.end, replacement),
            "selected",
        )

    def remove_selection(self, sql_text: str, parsed_id: str) -> SqlTransformResult:
        parsed_id = _target_id(parsed_id)
        model = self.parse(sql_text)
        if len(model.selections) <= 1:
            raise SqlEditError("A SELECT query must keep at least one selected expression.")
        index = next((i for i, item in enumerate(model.selections) if item.id == parsed_id), -1)
        selection = model.selections[index] if index >= 0 else None
        if selection is None or not selection.editable:
            reason = selection.read_only_reason if selection else "Selection is not removable."
            raise SqlEditError(reason)
        start, end = selection.span.start, selection.span.end
        if index < len(model.selections) - 1:
            end = model.selections[index + 1].span.start
        else:
            start = model.selections[index - 1].span.end
        return self._finish(_replace_span(sql_text, start, end, ""), "selected")

    def move_selection(
        self, sql_text: str, parsed_id: str, direction: Literal[-1, 1]
    ) -> SqlTransformResult:
        parsed_id = _target_id(parsed_id)
        if direction not in {-1, 1}:
            raise SqlEditError("Selection move direction must be -1 or 1.")
        model = self.parse(sql_text)
        if model.select_list_span is None:
            raise SqlEditError("SELECT list is not structurally editable.")
        index = next(
            (i for i, item in enumerate(model.selections) if item.id == parsed_id), -1
        )
        target = index + direction
        if index < 0 or target < 0 or target >= len(model.selections):
            raise SqlEditError("Selection cannot move further.")
        if not model.selections[index].editable or not model.selections[target].editable:
            raise SqlEditError("Read-only selections cannot be reordered.")
        return self.reorder_selection(sql_text, parsed_id, target)

    def reorder_selection(
        self, sql_text: str, parsed_id: str, target_index: int
    ) -> SqlTransformResult:
        parsed_id = _target_id(parsed_id)
        model = self.parse(sql_text)
        if model.select_list_span is None:
            raise SqlEditError("SELECT list is not structurally editable.")
        index = next((i for i, item in enumerate(model.selections) if item.id == parsed_id), -1)
        if index < 0 or target_index < 0 or target_index >= len(model.selections):
            raise SqlEditError("Selection reorder target is unavailable.")
        if index == target_index:
            return SqlTransformResult(sql=sql_text, model=model)
        if not model.selections[index].editable or not model.selections[target_index].editable:
            raise SqlEditError("Read-only selections cannot be reordered.")
        rows = [sql_text[item.span.start : item.span.end] for item in model.selections]
        moved = rows.pop(index)
        rows.insert(target_index, moved)
        separator = ", "
        if len(model.selections) > 1:
            observed = sql_text[model.selections[0].span.end : model.selections[1].span.start]
            if "," in observed:
                separator = observed
        replacement = separator.join(rows)
        return self._finish(
            _replace_span(
                sql_text, model.select_list_span.start, model.select_list_span.end, replacement
            ),
            "selected",
        )

    def add_filter(self, sql_text: str, patch: PredicatePatch) -> SqlTransformResult:
        model = self.parse(sql_text)
        if not model.capabilities.filters:
            raise SqlEditError(model.read_only_reason or "Filters are not structurally editable.")
        left = _patch_string(patch, "left", "").strip()
        right = _patch_string(patch, "right", "").strip()
        operator_value = _patch_string(patch, "operator", "")
        if not operator_value:
            raise SqlEditError("Filter operator is required.")
        operator = _normalize_operator(operator_value)
        if not left or not right:
            raise SqlEditError("Filter operands cannot be empty.")
        predicate = f"{left} {operator} {right}"
        if model.where_body_span is not None:
            connector = patch.connector or "AND"
            sql = _replace_span(
                sql_text,
                model.where_body_span.end,
                model.where_body_span.end,
                f"\n{connector} {predicate}",
            )
            return self._finish(sql, "filters")
        if model.from_clause_span is None:
            raise SqlEditError(
                "A WHERE filter cannot be added because the FROM clause is unavailable."
            )
        sql = _replace_span(
            sql_text,
            model.from_clause_span.end,
            model.from_clause_span.end,
            f"\nWHERE {predicate}",
        )
        return self._finish(sql, "filters")

    def update_filter(
        self, sql_text: str, parsed_id: str, patch: PredicatePatch
    ) -> SqlTransformResult:
        parsed_id = _target_id(parsed_id)
        model = self.parse(sql_text)
        predicate = next((item for item in model.filters if item.id == parsed_id), None)
        if predicate is None or not predicate.editable:
            reason = predicate.read_only_reason if predicate else "Filter is not editable."
            raise SqlEditError(reason)
        left = _patch_string(patch, "left", predicate.left).strip()
        right = _patch_string(patch, "right", predicate.right).strip()
        operator = _normalize_operator(_patch_string(patch, "operator", predicate.operator))
        if not left or not right:
            raise SqlEditError("Filter operands cannot be empty.")
        replacements: list[tuple[int, int, str]] = [
            (predicate.span.start, predicate.span.end, f"{left} {operator} {right}")
        ]
        _append_connector_replacement(replacements, predicate, patch)
        return self._finish(_apply_replacements(sql_text, replacements), "filters")

    def remove_filter(self, sql_text: str, parsed_id: str) -> SqlTransformResult:
        parsed_id = _target_id(parsed_id)
        model = self.parse(sql_text)
        index = next((i for i, item in enumerate(model.filters) if item.id == parsed_id), -1)
        predicate = model.filters[index] if index >= 0 else None
        if predicate is None or not predicate.editable:
            reason = predicate.read_only_reason if predicate else "Filter is not removable."
            raise SqlEditError(reason)
        if len(model.filters) == 1:
            if model.where_clause_span is None:
                raise SqlEditError("WHERE clause span is unavailable.")
            return self._finish(
                _replace_span(
                    sql_text, model.where_clause_span.start, model.where_clause_span.end, ""
                ),
                "filters",
            )
        if index > 0 and predicate.connector_span is not None:
            return self._finish(
                _replace_span(sql_text, predicate.connector_span.start, predicate.span.end, ""),
                "filters",
            )
        following = model.filters[index + 1]
        if following.connector_span is None:
            raise SqlEditError("Filter connector could not be isolated safely.")
        return self._finish(
            _replace_span(sql_text, predicate.span.start, following.connector_span.end, ""),
            "filters",
        )

    def add_join(
        self, sql_text: str, patch: JoinPatch, predicate: PredicatePatch
    ) -> SqlTransformResult:
        model = self.parse(sql_text)
        if not model.capabilities.joins or model.from_clause_span is None:
            raise SqlEditError(
                model.read_only_reason or "Joins are not structurally editable for this query."
            )
        join_type = _patch_string(patch, "join_type", "").strip().upper()
        if join_type not in JOIN_TYPES or join_type == "CROSS":
            raise SqlEditError("New keyed joins must use INNER, LEFT, RIGHT, or FULL.")
        join_source = _patch_string(patch, "source", "").strip()
        left = _patch_string(predicate, "left", "").strip()
        right = _patch_string(predicate, "right", "").strip()
        operator = _normalize_operator(_patch_string(predicate, "operator", "="))
        if not join_source or not left or not right:
            raise SqlEditError("Join source and key expressions cannot be empty.")
        clause = f"\n{join_type} JOIN {join_source} ON {left} {operator} {right}"
        return self._finish(
            _replace_span(sql_text, model.from_clause_span.end, model.from_clause_span.end, clause),
            "joins",
        )

    def update_join(
        self, sql_text: str, parsed_id: str, patch: JoinPatch
    ) -> SqlTransformResult:
        parsed_id = _target_id(parsed_id)
        model = self.parse(sql_text)
        join = next((item for item in model.joins if item.id == parsed_id), None)
        if join is None:
            raise SqlEditError("Join no longer exists.")
        replacements: list[tuple[int, int, str]] = []
        if "join_type" in patch.model_fields_set:
            if not join.editable_type:
                raise SqlEditError(join.read_only_reason or "Join type is not editable.")
            normalized = _patch_string(patch, "join_type", "").strip().upper()
            if normalized not in JOIN_TYPES:
                raise SqlEditError("Unsupported join type.")
            if normalized == "CROSS" and (
                join.predicates or (join.read_only_reason and "USING" in join.read_only_reason)
            ):
                raise SqlEditError("CROSS JOIN cannot retain ON/USING join keys.")
            replacements.append((join.type_span.start, join.type_span.end, f"{normalized} JOIN"))
        if "source" in patch.model_fields_set:
            if not join.editable_source:
                raise SqlEditError(join.read_only_reason or "Join source is not editable.")
            source_value = _patch_string(patch, "source", "").strip()
            if not source_value:
                raise SqlEditError("Join source cannot be empty.")
            replacements.append((join.source_span.start, join.source_span.end, source_value))
        if not replacements:
            return SqlTransformResult(sql=sql_text, model=model)
        return self._finish(_apply_replacements(sql_text, replacements), "joins")

    def update_join_predicate(
        self, sql_text: str, parsed_id: str, patch: PredicatePatch
    ) -> SqlTransformResult:
        parsed_id = _target_id(parsed_id)
        model = self.parse(sql_text)
        predicate = _join_predicate(model, parsed_id)
        if predicate is None or not predicate.editable:
            raise SqlEditError(
                predicate.read_only_reason if predicate else "Join predicate is not editable."
            )
        left = _patch_string(patch, "left", predicate.left).strip()
        right = _patch_string(patch, "right", predicate.right).strip()
        operator = _normalize_operator(_patch_string(patch, "operator", predicate.operator))
        if not left or not right:
            raise SqlEditError("Join key expressions cannot be empty.")
        replacements: list[tuple[int, int, str]] = [
            (predicate.span.start, predicate.span.end, f"{left} {operator} {right}")
        ]
        _append_connector_replacement(replacements, predicate, patch)
        return self._finish(_apply_replacements(sql_text, replacements), "joins")

    def remove_join_predicate(self, sql_text: str, parsed_id: str) -> SqlTransformResult:
        parsed_id = _target_id(parsed_id)
        model = self.parse(sql_text)
        join = next(
            (item for item in model.joins if any(p.id == parsed_id for p in item.predicates)),
            None,
        )
        if join is None:
            raise SqlEditError("Join no longer exists.")
        if len(join.predicates) <= 1:
            raise SqlEditError("A join using ON must keep at least one predicate.")
        index = next((i for i, item in enumerate(join.predicates) if item.id == parsed_id), -1)
        predicate = join.predicates[index] if index >= 0 else None
        if predicate is None or not predicate.editable:
            raise SqlEditError(
                predicate.read_only_reason if predicate else "Join predicate is not removable."
            )
        if index > 0 and predicate.connector_span is not None:
            return self._finish(
                _replace_span(sql_text, predicate.connector_span.start, predicate.span.end, ""),
                "joins",
            )
        following = join.predicates[index + 1]
        if following.connector_span is None:
            raise SqlEditError("Join predicate connector could not be isolated safely.")
        return self._finish(
            _replace_span(sql_text, predicate.span.start, following.connector_span.end, ""),
            "joins",
        )

    def remove_join(self, sql_text: str, parsed_id: str) -> SqlTransformResult:
        parsed_id = _target_id(parsed_id)
        model = self.parse(sql_text)
        join = next((item for item in model.joins if item.id == parsed_id), None)
        if join is None or (join.read_only_reason and "NATURAL" in join.read_only_reason):
            raise SqlEditError(join.read_only_reason if join else "Join is not removable.")
        return self._finish(_replace_span(sql_text, join.span.start, join.span.end, ""), "joins")

    def update_source(
        self, sql_text: str, parsed_id: str, source: str
    ) -> SqlTransformResult:
        parsed_id = _target_id(parsed_id)
        model = self.parse(sql_text)
        sql_source = next((item for item in model.sources if item.id == parsed_id), None)
        if sql_source is None or not sql_source.editable:
            reason = sql_source.read_only_reason if sql_source else "Source is not editable."
            raise SqlEditError(reason)
        expression = source.strip()
        if not expression:
            raise SqlEditError("Source cannot be empty.")
        capability: Literal["selected", "joins"] = (
            "joins" if sql_source.kind == "join" else "selected"
        )
        return self._finish(
            _replace_span(sql_text, sql_source.span.start, sql_source.span.end, expression),
            capability,
        )

    def _finish(
        self, sql: str, capability: Literal["selected", "filters", "joins"]
    ) -> SqlTransformResult:
        model = self.parse(sql)
        if not getattr(model.capabilities, capability):
            raise SqlEditError(
                model.read_only_reason
                or f"Updated SQL can no longer be edited safely in {capability}."
            )
        return SqlTransformResult(sql=sql, model=model)


def _target_id(value: str) -> str:
    if not isinstance(value, str):
        raise TypeError(
            "SqlModelService mutation targets must be parser-local entity IDs; "
            "resolve SqlEntityRef before mutation."
        )
    if not value:
        raise SqlEditError("SQL entity ID cannot be empty.")
    return value


def _patch_string(
    patch: SelectionPatch | PredicatePatch | JoinPatch,
    field: str,
    default: str,
) -> str:
    if field not in patch.model_fields_set:
        return default
    value = getattr(patch, field)
    if not isinstance(value, str):
        raise TypeError(f"Patch field {field!r} must be a string when supplied.")
    return value


def _join_predicate(model: SqlEditableModel, parsed_id: str) -> SqlPredicate | None:
    return next(
        (
            predicate
            for join in model.joins
            for predicate in join.predicates
            if predicate.id == parsed_id
        ),
        None,
    )


def _append_connector_replacement(
    replacements: list[tuple[int, int, str]], predicate: SqlPredicate, patch: PredicatePatch
) -> None:
    if "connector" not in patch.model_fields_set:
        return
    if predicate.connector_span is None:
        if patch.connector is not None:
            raise SqlEditError("The first predicate cannot have a leading connector.")
        return
    # Clearing a non-leading connector means reverting it to the implicit/default AND.
    replacement = patch.connector or "AND"
    replacements.append((predicate.connector_span.start, predicate.connector_span.end, replacement))


def _clean_alias(value: str | None) -> str | None:
    if value is None:
        return None
    alias = value.strip()
    if not alias:
        return None
    plain = re.compile(r"^[A-Za-z_][A-Za-z0-9_$#]*$")
    quoted = re.compile(r'^("(?:[^"]|"")+"|`[^`]+`|\[[^\]]+\])$')
    if not plain.match(alias) and not quoted.match(alias):
        raise SqlEditError(
            "Alias must be an identifier; quote aliases that contain spaces or punctuation."
        )
    return alias


def _normalize_operator(value: str) -> str:
    normalized = " ".join(value.strip().split()).upper()
    if normalized not in FILTER_OPERATORS:
        raise SqlEditError("Unsupported predicate operator.")
    return normalized


def _replace_span(source: str, start: int, end: int, replacement: str) -> str:
    return f"{source[:start]}{replacement}{source[end:]}"


def _apply_replacements(source: str, replacements: list[tuple[int, int, str]]) -> str:
    result = source
    for start, end, replacement in sorted(replacements, key=lambda item: item[0], reverse=True):
        result = _replace_span(result, start, end, replacement)
    return result
