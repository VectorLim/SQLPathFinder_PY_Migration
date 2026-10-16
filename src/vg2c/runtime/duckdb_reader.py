"""DuckDbReader - run local SQL queries over CSV, TSV, and Parquet inputs."""

from __future__ import annotations

import re

import duckdb
import pandas as pd

from vg2c.runtime.values import resolve_path


class _DuckDbReader:
    """Run DuckDB SQL using explicit local file bindings."""

    _MARKER = re.compile(r"<DDBTable>([^<]+)</DDBTable>", re.IGNORECASE)

    @staticmethod
    def _relation(path) -> str:
        literal = str(path).replace("'", "''")
        suffix = path.suffix.lower()
        if suffix == ".tab":
            return f"read_csv_auto('{literal}', delim='\\t', sample_size=-1)"
        if suffix == ".csv":
            return f"read_csv_auto('{literal}', sample_size=-1)"
        if suffix == ".parquet":
            return f"read_parquet('{literal}')"
        raise ValueError(f"DuckDB input has unsupported extension: {path}")

    def execute(
        self, sql: str, inputs: list[str | tuple[str, str]], *, params=None, workdir=None
    ) -> pd.DataFrame:
        relations = {}
        for input_spec in inputs:
            if not isinstance(input_spec, tuple) or len(input_spec) != 2:
                raise ValueError("DuckDB inputs require explicit <file>:<table> bindings")
            source, table_name = input_spec
            key = table_name.casefold() if isinstance(table_name, str) else ""
            if not key or key in relations:
                raise ValueError(f"DuckDB input has invalid or duplicate table name: {table_name!r}")
            relations[key] = self._relation(resolve_path(source, workdir=workdir))

        if "<DDBTABLE" in sql.upper() or "</DDBTABLE" in sql.upper():
            def replace(match):
                table_name = match.group(1).strip().casefold()
                if table_name not in relations:
                    raise ValueError(f"DuckDB query references unknown DDBTable marker: {table_name}")
                return relations[table_name]
            sql = self._MARKER.sub(replace, sql)
            if "<DDBTABLE" in sql.upper() or "</DDBTABLE" in sql.upper():
                raise ValueError("DuckDB query has malformed DDBTable marker")

        try:
            with duckdb.connect(":memory:") as conn:
                return conn.execute(sql, params or {}).df()
        except duckdb.Error as exc:
            raise RuntimeError(f"DuckDB error in execute: {exc}\nSQL:\n{sql}") from exc
