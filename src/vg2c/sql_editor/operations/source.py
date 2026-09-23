from __future__ import annotations

from collections.abc import Mapping
from typing import Any

from vg2c.sql_editor.models import SqlEditError, SqlTransformResult
from vg2c.sql_editor.operations.base import SqlOperation
from vg2c.sql_editor.parser import parse_sql


class UpdateSourceOperation(SqlOperation):
    name = "update-source"

    def _transform(
        self,
        source: str,
        arguments: Mapping[str, Any],
    ) -> SqlTransformResult:
        model = parse_sql(source)
        source_id = self.require(arguments, "source_id")
        sql_source = next(
            (item for item in model.sources if item.id == source_id),
            None,
        )
        if sql_source is None or not sql_source.editable:
            raise SqlEditError(
                sql_source.read_only_reason
                if sql_source
                else "Source is not editable."
            )

        expression = self.require(arguments, "source").strip()
        if not expression:
            raise SqlEditError("Source cannot be empty.")

        capability = "joins" if sql_source.kind == "join" else "selected"
        return self.finish(
            self.replace_span(
                source,
                sql_source.span.start,
                sql_source.span.end,
                expression,
            ),
            capability,
        )
