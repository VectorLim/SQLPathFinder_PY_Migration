"""SqliteEngine — execute SQL joins over CSV inputs using in-memory SQLite."""

from __future__ import annotations

import csv
import re
import sqlite3
from pathlib import Path

from vg2c.emitter.macro import substitute_crosstab


def _load_csv_as_table(conn: sqlite3.Connection, csv_path: str) -> str:
    """Load a CSV file into a SQLite table; return the table name (file stem)."""
    path = Path(csv_path)
    # Strip both .tab and .csv suffixes to form the table name.
    stem = path.stem
    table_name = stem

    try:
        with path.open(newline="", encoding="utf-8", errors="replace") as fh:
            reader = csv.DictReader(fh)
            rows = list(reader)

            # Handle empty/no-header files gracefully
            if reader.fieldnames is None:
                # Create placeholder table with one column, no rows
                conn.execute(f'DROP TABLE IF EXISTS "{table_name}"')
                conn.execute(f'CREATE TABLE "{table_name}" ("_empty" TEXT)')
                return table_name

            cols = list(reader.fieldnames)
            if not cols:
                # Empty header - create placeholder table
                conn.execute(f'DROP TABLE IF EXISTS "{table_name}"')
                conn.execute(f'CREATE TABLE "{table_name}" ("_empty" TEXT)')
                return table_name

            col_defs = ", ".join(f'"{c}" TEXT' for c in cols)
            conn.execute(f'DROP TABLE IF EXISTS "{table_name}"')
            conn.execute(f'CREATE TABLE "{table_name}" ({col_defs})')

            # Drop rows that duplicate the header
            header_str = [str(c) for c in cols]
            filtered_rows = [
                r for r in rows if [str(r.get(c, "")) for c in cols] != header_str
            ]

            if filtered_rows:
                placeholders = ", ".join("?" for _ in cols)
                conn.executemany(
                    f'INSERT INTO "{table_name}" VALUES ({placeholders})',
                    [[r.get(c, "") for c in cols] for r in filtered_rows],
                )
            return table_name
    except Exception as exc:
        raise RuntimeError(f"Failed to load CSV {csv_path}: {exc}") from exc


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

    def run_join(
        self, sql: str, inputs: list[str], output: str, header: list[str] | None = None
    ) -> None:
        """
        1. Open in-memory SQLite connection.
        2. Load each *input* CSV as a table.
        3. Split *sql* on ';'; execute non-SELECT statements directly.
        4. Execute the final SELECT; write rows to *output* CSV.

        Args:
            sql: SQL query (may contain multiple statements separated by ';')
            inputs: List of CSV file paths to load as tables
            output: Output CSV file path
            header: Optional declared column list for output CSV.
                   When provided, output uses this header exactly.
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

        alias_to_table: dict[str, str] = {}
        alias_map_re = re.compile(
            r"\b(?:FROM|JOIN)\s+(?:\[([^\]]+)\]|\"([^\"]+)\"|([A-Za-z_][A-Za-z0-9_]*))\s+([A-Za-z_][A-Za-z0-9_]*)\b",
            re.IGNORECASE,
        )
        for m in alias_map_re.finditer(final_stmt):
            table_name = m.group(1) or m.group(2) or m.group(3)
            alias = m.group(4)
            if table_name and alias:
                alias_to_table[alias.lower()] = table_name

        def _lookup_alias_columns(alias: str) -> list[str]:
            table_name = alias_to_table.get(alias.lower())
            if not table_name:
                return []
            pragma_rows = conn.execute(f'PRAGMA table_info("{table_name}")').fetchall()
            return [str(r[1]) for r in pragma_rows if len(r) > 1]

        final_stmt = substitute_crosstab(
            final_stmt, alias_columns_lookup=_lookup_alias_columns
        )

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

        # Use declared header if provided, otherwise use query result columns
        output_header = header if header else col_names

        with output_path.open("w", newline="", encoding="utf-8") as fh:
            writer = csv.writer(fh)
            if output_header:
                writer.writerow(output_header)

            # When header is declared, project rows by column name
            if header and col_names:
                # Build mapping from col_names to values
                col_index = {name: idx for idx, name in enumerate(col_names)}
                header_str = [str(h) for h in header]

                for row in rows:
                    # Project row to declared header order
                    projected = [
                        row[col_index[h]] if h in col_index else "" for h in header
                    ]
                    # Skip rows that duplicate the header
                    if [str(v) for v in projected] != header_str:
                        writer.writerow(projected)
            else:
                # No declared header - write rows as-is
                for row in rows:
                    writer.writerow(list(row))
