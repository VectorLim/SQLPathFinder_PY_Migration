from __future__ import annotations

import re
from collections.abc import Mapping
from typing import Any

from vg2c.sql_editor.models import SqlEditError, SqlTransformResult
from vg2c.sql_editor.operations.base import SqlOperation
from vg2c.sql_editor.parser import parse_sql


class AddSelectionOperation(SqlOperation):
    name = "add-selection"

    def _transform(
        self,
        source: str,
        arguments: Mapping[str, Any],
    ) -> SqlTransformResult:
        model = parse_sql(source)
        if not model.capabilities.selected or not model.select_list_span:
            raise SqlEditError("SELECT list is not structurally editable.")

        expression = self.require(arguments, "expression").strip()
        if not expression:
            raise SqlEditError("Selected expression cannot be empty.")

        current = source[model.select_list_span.start:model.select_list_span.end]
        replacement = f"{current}, {expression}" if model.selections else expression
        return self.finish(
            self.replace_span(
                source,
                model.select_list_span.start,
                model.select_list_span.end,
                replacement,
            ),
            "selected",
        )


class UpdateSelectionOperation(SqlOperation):
    name = "update-selection"

    def _transform(
        self,
        source: str,
        arguments: Mapping[str, Any],
    ) -> SqlTransformResult:
        model = parse_sql(source)
        selection_id = self.require(arguments, "selection_id")
        selection = next(
            (item for item in model.selections if item.id == selection_id),
            None,
        )
        if selection is None or not selection.editable:
            raise SqlEditError(
                selection.read_only_reason if selection else "Selection is not editable."
            )

        expression = arguments.get("expression")
        next_expression = (
            selection.expression if expression is None else expression
        ).strip()
        if not next_expression:
            raise SqlEditError("Selected expression cannot be empty.")

        next_alias = (
            selection.alias
            if "alias" not in arguments
            else _clean_alias(arguments["alias"])
        )
        replacement = (
            f"{next_expression} AS {next_alias}" if next_alias else next_expression
        )
        return self.finish(
            self.replace_span(
                source,
                selection.span.start,
                selection.span.end,
                replacement,
            ),
            "selected",
        )


class RemoveSelectionOperation(SqlOperation):
    name = "remove-selection"

    def _transform(
        self,
        source: str,
        arguments: Mapping[str, Any],
    ) -> SqlTransformResult:
        model = parse_sql(source)
        if len(model.selections) <= 1:
            raise SqlEditError(
                "A SELECT query must keep at least one selected expression."
            )

        selection_id = self.require(arguments, "selection_id")
        index = next(
            (
                index
                for index, item in enumerate(model.selections)
                if item.id == selection_id
            ),
            -1,
        )
        selection = model.selections[index] if index >= 0 else None
        if selection is None or not selection.editable:
            raise SqlEditError(
                selection.read_only_reason
                if selection
                else "Selection is not removable."
            )

        start = selection.span.start
        end = selection.span.end
        if index < len(model.selections) - 1:
            end = model.selections[index + 1].span.start
        else:
            start = model.selections[index - 1].span.end

        return self.finish(self.replace_span(source, start, end, ""), "selected")


class ReorderSelectionOperation(SqlOperation):
    name = "reorder-selection"

    def _transform(
        self,
        source: str,
        arguments: Mapping[str, Any],
    ) -> SqlTransformResult:
        model = parse_sql(source)
        if not model.select_list_span:
            raise SqlEditError("SELECT list is not structurally editable.")

        selection_id = self.require(arguments, "selection_id")
        target_index = int(self.require(arguments, "target_index"))
        index = next(
            (
                index
                for index, item in enumerate(model.selections)
                if item.id == selection_id
            ),
            -1,
        )
        if index < 0 or target_index < 0 or target_index >= len(model.selections):
            raise SqlEditError("Selection reorder target is unavailable.")
        if index == target_index:
            return SqlTransformResult(source, model)
        if (
            not model.selections[index].editable
            or not model.selections[target_index].editable
        ):
            raise SqlEditError("Read-only selections cannot be reordered.")

        rows = [
            source[item.span.start:item.span.end]
            for item in model.selections
        ]
        moved = rows.pop(index)
        rows.insert(target_index, moved)
        return self.finish(
            self.replace_span(
                source,
                model.select_list_span.start,
                model.select_list_span.end,
                _selection_separator(source, model).join(rows),
            ),
            "selected",
        )


def _clean_alias(value: str | None | object) -> str | None:
    if value is None or value is ...:
        return None
    alias = str(value).strip()
    if not alias:
        return None

    plain = re.compile(r"^[A-Za-z_][A-Za-z0-9_$#]*$")
    quoted = re.compile(r'^(?:"(?:[^"]|"")+"|\x60[^\x60]+\x60|\[[^\]]+\])$')
    if not plain.match(alias) and not quoted.match(alias):
        raise SqlEditError(
            "Alias must be an identifier; quote aliases that contain spaces or punctuation."
        )
    return alias


def _selection_separator(source: str, model) -> str:
    if len(model.selections) > 1:
        separator = source[
            model.selections[0].span.end:model.selections[1].span.start
        ]
        if "," in separator:
            return separator
    return ", "
