"""SqlMacros — expand SQL_Get_CSV_List(...) into chunked IN-list clauses."""

from __future__ import annotations

import csv
from pathlib import Path


def _read_column(path: str, column_ref: int | str) -> list[str]:
    """Extract unique values from a column (1-based index or name), preserving order."""
    rows = []
    with Path(path).open(newline="", encoding="utf-8", errors="replace") as fh:
        reader = csv.reader(fh)
        header = next(reader, [])

        if isinstance(column_ref, int):
            # 1-based index
            idx = column_ref - 1
        else:
            col_lower = [h.lower() for h in header]
            try:
                idx = col_lower.index(column_ref.lower())
            except ValueError:
                return []

        seen: dict[str, None] = {}
        for row in reader:
            if idx < len(row):
                val = row[idx]
                if val not in seen:
                    seen[val] = None
                    rows.append(val)

    return rows


def _single_quote(value: str) -> str:
    return "'" + value.replace("'", "''") + "'"


class SqlMacros:
    """SQL macro expansion helpers."""

    def sql_get_csv_list(self, path: str, column_ref: int | str, lead_in: str) -> str:
        """Return chunked IN-list clause for use inside Oracle SQL.

        Oracle hard-limits IN lists to 1000 values. When there are more, the
        result is chunked: ``(v1..v1000) OR <lead_in> (v1001..)``.

        The result is a balanced parenthesized IN list. Any call-site wrapping
        ``(<col> In ...)`` paren is closed by the resolver (see
        :mod:`vg2c.resolver.sql_macro_expander`), not by this runtime macro.
        """
        values = _read_column(path, column_ref)
        if not values:
            return "('__NO_VALUES__')"

        chunk_size = 1000
        chunks = [values[i : i + chunk_size] for i in range(0, len(values), chunk_size)]
        parts = []
        for i, chunk in enumerate(chunks):
            quoted = ", ".join(_single_quote(v) for v in chunk)
            parts.append(f"({quoted})")
            if i < len(chunks) - 1:
                parts.append(f"\nOR {lead_in} ")

        return "".join(parts)
