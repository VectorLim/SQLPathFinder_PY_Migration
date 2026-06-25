"""Readers — Reader ABC, MockReader, and lazily-imported OracleReader."""

from __future__ import annotations

import re
from abc import ABC, abstractmethod
from typing import Any

_PLACEHOLDER_RE = re.compile(r"<<<([^>]+)>>>")


def _substitute_macros(sql: str, macro_state) -> str:
    if macro_state is None or "<<<" not in sql:
        return sql
    return _PLACEHOLDER_RE.sub(
        lambda m: macro_state.named(m.group(1).strip().upper()),
        sql,
    )


class Reader(ABC):
    """Abstract base for all database readers."""

    @abstractmethod
    def read(self, sql: str) -> list[dict[str, Any]]:
        """Execute *sql* and return rows as a list of dicts."""


class MockReader(Reader):
    """Canned-data reader for unit / e2e tests.

    ``canned`` maps a SQL string (or substring prefix) to a list of row dicts.
    Falls back to an empty list when no key matches.
    """

    def __init__(self, canned: dict[str, list[dict[str, Any]]] | None = None) -> None:
        self._canned: dict[str, list[dict[str, Any]]] = canned or {}

    def read(self, sql: str) -> list[dict[str, Any]]:
        sql_stripped = sql.strip()
        # Exact match first
        if sql_stripped in self._canned:
            return list(self._canned[sql_stripped])
        # Substring / prefix match
        for key, rows in self._canned.items():
            if key in sql_stripped:
                return list(rows)
        return []


class OracleReader(Reader):
    """DataSyncX-backed reader. Import of datasyncx is deferred to avoid hard dep."""

    def __init__(
        self,
        database: str,
        node: str = "",
        record: tuple[str, str] | None = None,
        instance: str | None = None,
        macro_state=None,
    ) -> None:
        self._database = database
        self._node = node
        self._record = record
        self._instance = instance
        self._macro_state = macro_state
        # Lazy import — fail clearly if datasyncx isn't installed
        try:
            import datasyncx  # type: ignore[import]  # noqa: F401

            self._datasyncx = datasyncx
        except ImportError as exc:
            raise RuntimeError(
                "DataSyncX is not installed. Install it or use MockReader for testing.\n"
                "  pip install datasyncx"
            ) from exc

    def read(self, sql: str) -> list[dict[str, Any]]:
        sql = _substitute_macros(sql, self._macro_state)
        # Delegate to DataSyncX reader API
        reader = self._datasyncx.OracleReader(
            database=self._database,
            node=self._node,
            record=self._record,
            instance=self._instance,
        )
        return reader.read(sql)
