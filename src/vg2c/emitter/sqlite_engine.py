"""SqliteEngine — execute SQL joins over CSV inputs using in-memory SQLite."""

from __future__ import annotations

import csv
import re
import sqlite3
from pathlib import Path


def _load_csv_as_table(conn: sqlite3.Connection, csv_path: str) -> str:
    """Load a CSV file into a SQLite table; return the table name (file stem)."""
    path = Path(csv_path)
    # Strip both .tab and .csv suffixes to form the table name.
    stem = path.stem
    table_name = stem

    with path.open(newline="", encoding="utf-8", errors="replace") as fh:
        reader = csv.DictReader(fh)
        rows = list(reader)

    if not rows:
        conn.execute(f'CREATE TABLE IF NOT EXISTS "{table_name}" (_dummy TEXT)')
        return table_name

    cols = list(rows[0].keys())
    col_defs = ", ".join(f'"{c}" TEXT' for c in cols)
    conn.execute(f'DROP TABLE IF EXISTS "{table_name}"')
    conn.execute(f'CREATE TABLE "{table_name}" ({col_defs})')

    placeholders = ", ".join("?" for _ in cols)
    conn.executemany(
        f'INSERT INTO "{table_name}" VALUES ({placeholders})',
        [[r.get(c, "") for c in cols] for r in rows],
    )
    return table_name


# Split on ';' but protect content inside quotes/brackets.
_STMT_SPLIT_RE = re.compile(
    r"(?:'[^']*'|\"[^\"]*\"|\[[^\]]*\]|`[^`]*`|[^;])+",
    re.DOTALL,
)


def _split_statements(sql: str) -> list[str]:
    return [
        m.group(0).strip() for m in _STMT_SPLIT_RE.finditer(sql) if m.group(0).strip()
    ]


class SqliteEngine:
    """Run SQL joins over CSV files using an in-memory SQLite connection."""

    def run_join(self, sql: str, inputs: list[str], output: str) -> None:
        """
        1. Open in-memory SQLite connection.
        2. Load each *input* CSV as a table.
        3. Split *sql* on ';'; execute non-SELECT statements directly.
        4. Execute the final SELECT; write rows to *output* CSV.
        """
        conn = sqlite3.connect(":memory:")
        conn.row_factory = sqlite3.Row

        for csv_path in inputs:
            _load_csv_as_table(conn, csv_path)

        stmts = _split_statements(sql)
        if not stmts:
            conn.close()
            return

        # Execute all but the last as DDL/DML (CREATE INDEX etc.)
        for stmt in stmts[:-1]:
            try:
                conn.execute(stmt)
            except sqlite3.Error:
                pass  # best-effort; DDL errors (already exists etc.) are non-fatal

        # Run the final statement as a SELECT
        final_stmt = stmts[-1]
        try:
            cursor = conn.execute(final_stmt)
            rows = cursor.fetchall()
            col_names = [d[0] for d in cursor.description] if cursor.description else []
        except sqlite3.Error as exc:
            conn.close()
            raise RuntimeError(
                f"SQLite error in run_join: {exc}\nSQL:\n{final_stmt}"
            ) from exc

        conn.close()

        # Write output CSV
        output_path = Path(output)
        output_path.parent.mkdir(parents=True, exist_ok=True)
        with output_path.open("w", newline="", encoding="utf-8") as fh:
            writer = csv.writer(fh)
            if col_names:
                writer.writerow(col_names)
            for row in rows:
                writer.writerow(list(row))
