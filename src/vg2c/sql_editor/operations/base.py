from __future__ import annotations

from abc import ABC, abstractmethod
from collections.abc import Mapping
from typing import Any, ClassVar, Literal

from vg2c.sql_editor.models import SqlActionName, SqlEditError, SqlTransformResult
from vg2c.sql_editor.parser import parse_sql

FILTER_OPERATORS = (
    "=", "!=", "<>", "<", "<=", ">", ">=", "LIKE", "NOT LIKE", "ILIKE",
    "IN", "NOT IN", "IS", "IS NOT",
)
JOIN_TYPES = ("INNER", "LEFT", "RIGHT", "FULL", "CROSS")


class SqlOperation(ABC):
    """Base contract for one structured SQL edit operation."""

    name: ClassVar[SqlActionName]

    @abstractmethod
    def apply(
        self,
        source: str,
        arguments: Mapping[str, Any],
    ) -> SqlTransformResult:
        """Apply this operation to SQL source and return the validated result."""
        raise NotImplementedError

    @staticmethod
    def require(arguments: Mapping[str, Any], key: str) -> Any:
        if key not in arguments:
            raise SqlEditError(f"Missing SQL action argument: {key}")
        return arguments[key]

    @staticmethod
    def normalize_operator(value: str) -> str:
        normalized = " ".join(value.strip().upper().split())
        if normalized not in FILTER_OPERATORS:
            raise SqlEditError("Unsupported predicate operator.")
        return normalized

    @staticmethod
    def replace_span(source: str, start: int, end: int, replacement: str) -> str:
        return f"{source[:start]}{replacement}{source[end:]}"

    @classmethod
    def apply_replacements(
        cls,
        source: str,
        replacements: list[tuple[int, int, str]],
    ) -> str:
        result = source
        for start, end, replacement in sorted(
            replacements,
            key=lambda item: item[0],
            reverse=True,
        ):
            result = cls.replace_span(result, start, end, replacement)
        return result

    @staticmethod
    def finish(
        sql: str,
        capability: Literal["selected", "filters", "joins"],
    ) -> SqlTransformResult:
        model = parse_sql(sql)
        if not getattr(model.capabilities, capability):
            raise SqlEditError(
                model.read_only_reason
                or f"Updated SQL can no longer be edited safely in {capability}."
            )
        return SqlTransformResult(sql, model)
