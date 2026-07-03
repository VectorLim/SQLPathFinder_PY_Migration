"""SqliteEngine - execute SQL joins over CSV inputs."""

from __future__ import annotations

import csv
import re
import sqlite3
from pathlib import Path
from typing import Callable

import pandas as pd

from vg2c.emitter.utilities._base import UtilitySpec
from vg2c.emitter.utilities._registry import register_utility


@register_utility
class SqliteEngine(UtilitySpec):
    """Run SQL joins over CSV files using in-memory SQLite."""

    utility_name = "sqlite_engine"
    utility_imports = (
        "import csv",
        "import re",
        "import sqlite3",
        "from pathlib import Path",
        "from typing import Callable",
        "import pandas as pd",
    )

    CROSSTAB_RE = re.compile(
        r"(?:,CrossTab->\[\[\s*([A-Za-z_][A-Za-z0-9_]*)\s*,\s*([^;\]]+)\s*;\s*:([YyNn])\s*\]\])",
        re.IGNORECASE,
    )
    STMT_SPLIT_RE = re.compile(
        r"(?:'[^']*'|\"[^\"]*\"|\[[^\]]*\]|`[^`]*`|[^;])+",
        re.DOTALL,
    )

    @staticmethod
    def _extract_selected_columns_by_alias(sql: str) -> dict[str, set[str]]:
        by_alias: dict[str, set[str]] = {}
        match = re.search(
            r"\bSELECT\b(?P<select_part>.*?)\bFROM\b",
            sql,
            flags=re.IGNORECASE | re.DOTALL,
        )
        if not match:
            return by_alias

        select_part = match.group("select_part")
        col_ref_re = re.compile(
            r"\b([A-Za-z_][A-Za-z0-9_]*)\s*\.\s*(?:\[([^\]]+)\]|\"([^\"]+)\"|([A-Za-z_][A-Za-z0-9_]*))"
        )
        for col_match in col_ref_re.finditer(select_part):
            alias = col_match.group(1).lower()
            col_name = col_match.group(2) or col_match.group(3) or col_match.group(4)
            if not col_name:
                continue
            by_alias.setdefault(alias, set()).add(col_name.lower())

        return by_alias

    @classmethod
    def _substitute_crosstab(
        cls,
        sql: str,
        alias_columns_lookup: Callable[[str], list[str]] | None = None,
    ) -> str:
        if alias_columns_lookup is None or "CrossTab->[[" not in sql:
            return sql

        selected_by_alias = cls._extract_selected_columns_by_alias(sql)

        def _replace(match: re.Match[str]) -> str:
            alias = match.group(1)
            mode = match.group(3).upper()
            all_cols = alias_columns_lookup(alias)
            selected = selected_by_alias.get(alias.lower(), set())
            dynamic_cols = [c for c in all_cols if c.lower() not in selected]

            if not dynamic_cols:
                return ""

            if mode == "N":
                return "," + ",".join(dynamic_cols)

            return "," + "\n         ,".join(
                f"{alias}.[{c}] AS [{c}]" for c in dynamic_cols
            )

        return cls.CROSSTAB_RE.sub(_replace, sql)

    @staticmethod
    def _load_csv_as_table(conn: sqlite3.Connection, csv_path: str) -> str:
        path = Path(csv_path)
        table_name = path.stem

        with path.open(newline="", encoding="utf-8", errors="replace") as fh:
            reader = csv.DictReader(fh)
            rows = list(reader)

        if reader.fieldnames is None:
            conn.execute(f'DROP TABLE IF EXISTS "{table_name}"')
            conn.execute(f'CREATE TABLE "{table_name}" ("_empty" TEXT)')
            return table_name

        cols = list(reader.fieldnames)
        if not cols:
            conn.execute(f'DROP TABLE IF EXISTS "{table_name}"')
            conn.execute(f'CREATE TABLE "{table_name}" ("_empty" TEXT)')
            return table_name

        col_defs = ", ".join(f'"{c}" TEXT' for c in cols)
        conn.execute(f'DROP TABLE IF EXISTS "{table_name}"')
        conn.execute(f'CREATE TABLE "{table_name}" ({col_defs})')

        header_str = [str(c) for c in cols]
        filtered_rows = [
            row for row in rows if [str(row.get(c, "")) for c in cols] != header_str
        ]

        if filtered_rows:
            placeholders = ", ".join("?" for _ in cols)
            conn.executemany(
                f'INSERT INTO "{table_name}" VALUES ({placeholders})',
                [[row.get(c, "") for c in cols] for row in filtered_rows],
            )

        return table_name

    @classmethod
    def _split_statements(cls, sql: str) -> list[str]:
        return [
            match.group(0).strip()
            for match in cls.STMT_SPLIT_RE.finditer(sql)
            if match.group(0).strip()
        ]

    def execute(self, sql: str, inputs: list[str]) -> pd.DataFrame:
        conn = sqlite3.connect(":memory:")
        conn.row_factory = sqlite3.Row

        for csv_path in inputs:
            self._load_csv_as_table(conn, csv_path)

        stmts = self._split_statements(sql)
        if not stmts:
            conn.close()
            return pd.DataFrame()

        for stmt in stmts[:-1]:
            try:
                conn.execute(stmt)
            except sqlite3.Error:
                pass

        final_stmt = stmts[-1]

        alias_to_table: dict[str, str] = {}
        alias_map_re = re.compile(
            r"\b(?:FROM|JOIN)\s+(?:\[([^\]]+)\]|\"([^\"]+)\"|([A-Za-z_][A-Za-z0-9_]*))\s+([A-Za-z_][A-Za-z0-9_]*)\b",
            re.IGNORECASE,
        )
        for match in alias_map_re.finditer(final_stmt):
            table_name = match.group(1) or match.group(2) or match.group(3)
            alias = match.group(4)
            if table_name and alias:
                alias_to_table[alias.lower()] = table_name

        def _lookup_alias_columns(alias: str) -> list[str]:
            table_name = alias_to_table.get(alias.lower())
            if not table_name:
                return []
            pragma_rows = conn.execute(f'PRAGMA table_info("{table_name}")').fetchall()
            return [str(row[1]) for row in pragma_rows if len(row) > 1]

        final_stmt = self._substitute_crosstab(
            final_stmt,
            alias_columns_lookup=_lookup_alias_columns,
        )

        try:
            cursor = conn.execute(final_stmt)
            rows = cursor.fetchall()
            col_names = [d[0] for d in cursor.description] if cursor.description else []
        except sqlite3.Error as exc:
            conn.close()
            raise RuntimeError(
                f"SQLite error in execute: {exc}\nSQL:\n{final_stmt}"
            ) from exc

        conn.close()

        if not rows or not col_names:
            return pd.DataFrame()

        data = [{col_names[i]: row[i] for i in range(len(col_names))} for row in rows]
        return pd.DataFrame(data)
