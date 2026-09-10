"""SqliteEngine - execute SQL joins over CSV inputs."""

from __future__ import annotations

import re

from vg2c.emitter.models import CodeExpr
from vg2c.kind import Kind
from vg2c.utilities._base import EmitterUtility
from vg2c.utilities._emit_helpers import (
    extract_crosstab_options,
    parse_table_binding,
    resolve_output_path,
    scan_sql_get_csv_list_calls,
    to_code_expr,
)
from vg2c.utilities._runtime_helpers import strip_quotes
from vg2c.utilities.crosstab import CrosstabUtility
from vg2c.utilities.csv_io import CsvIO


class SqliteEngine(EmitterUtility):
    """Emit query calls for external and SQLite readers."""

    utility_name = "sqlite_engine"
    handles = (Kind.SQL_QUERY, Kind.SQLITE_QUERY)
    _SQL_NUMBER = r"[+-]?(?:\d+(?:\.\d*)?|\.\d+)(?:[eE][+-]?\d+)?"

    @staticmethod
    def check(options) -> tuple[Kind, str] | None:
        if options.lookup.get("OLEDB", "").upper() == "SQLITE":
            return Kind.SQLITE_QUERY, "/OLEDB=SQLite"
        if options.lookup.get("ENGINE", "").upper() == "SQLITE":
            return Kind.SQLITE_QUERY, "/ENGINE=SQLite"

        node = options.lookup.get("NODE", "")
        engine = options.lookup.get("ENGINE", "")
        oledb = options.lookup.get("OLEDB", "")
        if engine.upper() not in {"VA"} and oledb.upper() not in {"SQLPLUS"}:
            return None

        if any(SqliteEngine._node_matches(node, token) for token in ("MARS", "OASYS", "ARIES")):
            return (
                Kind.SQL_QUERY,
                "/NODE indicates Oracle dialect and /ENGINE=VA or /OLEDB=SQLPlus",
            )
        return None

    @staticmethod
    def _node_matches(node_value: str, token: str) -> bool:
        node = node_value.upper().strip()
        return node.endswith(token) or node.endswith(f".{token}") or f"<<<{token}>>>" in node

    @staticmethod
    def _format_sql_literal(sql: str) -> str:
        escaped = sql.replace("\\", "\\\\").replace('"""', '\\"\\"\\"')
        return f'"""{escaped}"""'

    @staticmethod
    def _sql_source(block) -> str:
        sql = getattr(block, "rewritten_sql", None)
        return block.resolved_body if sql is None else sql

    @classmethod
    def extract_globals(cls, block) -> dict[str, object]:
        from vg2c.utilities._sql_globals import extract_sql_globals

        return {item.key: item.value for item in extract_sql_globals(cls._sql_source(block))}

    @classmethod
    def _extract_sql_text(cls, block, global_refs=None) -> CodeExpr:
        from vg2c.utilities._sql_globals import extract_sql_globals

        sql = cls._sql_source(block)
        calls = scan_sql_get_csv_list_calls(sql)
        replacements: list[tuple[int, int, str]] = []
        references: list[str] = []
        for item in extract_sql_globals(sql) if global_refs else ():
            if any(call.start < item.end and call.end > item.start for call in calls):
                continue
            reference = global_refs[item.key]
            replacements.append(
                (
                    item.start,
                    item.end,
                    f"SqliteEngine.global_sql({reference.source}, {item.numeric!r})",
                )
            )
            references.extend(reference.global_names)
        for call in calls:
            csv_path_expr = to_code_expr(call.csv_path)
            expr = CsvIO.sql_get_csv_list.render(csv_path_expr, call.column_ref, call.lead_in)
            if call.needs_closing_paren:
                expr += " + ')'"
            replacements.append((call.start, call.end, expr))
        if not replacements:
            return CodeExpr(cls._format_sql_literal(sql), sql)

        parts: list[str] = []
        cursor = 0
        for start, end, expression in sorted(replacements):
            literal = sql[cursor:start]
            if literal:
                parts.append(repr(literal))
            parts.append(expression)
            cursor = end
        if sql[cursor:]:
            parts.append(repr(sql[cursor:]))
        return CodeExpr(" + ".join(parts), global_names=tuple(dict.fromkeys(references)))

    @staticmethod
    def global_sql(value, numeric: bool = False) -> str:
        """Render editable operands with their original SQL literal category."""
        if isinstance(value, list):
            if not value:
                raise ValueError("SQL IN globals must contain at least one value")
            return ", ".join(SqliteEngine.global_sql(item, numeric) for item in value)
        if numeric or type(value) is int:
            text = str(value)
            if not re.fullmatch(SqliteEngine._SQL_NUMBER, text):
                raise ValueError(f"Invalid SQL numeric global: {value!r}")
            return text
        if not isinstance(value, str):
            raise ValueError(f"Expected a SQL string or number, got {value!r}")
        return "'" + value.replace("'", "''") + "'"

    @staticmethod
    def _extract_table_inputs(block) -> list[str | tuple[str, str]]:
        inputs: list[str | tuple[str, str]] = []
        for key, value in block.resolved_options.pairs:
            if key != "TABLE":
                continue
            for table_spec in value.split(","):
                path, table_name = parse_table_binding(table_spec.strip())
                if path:
                    inputs.append((path, table_name) if table_name else path)
        return inputs

    @staticmethod
    def _extract_header(block) -> list[str] | None:
        headers_value = block.resolved_options.lookup.get("HEADERS")
        if not headers_value:
            return None
        if CrosstabUtility.has_token(headers_value):
            return None
        stripped = strip_quotes(headers_value)
        parts = [p.strip() for p in stripped.split(",")]
        return [p for p in parts if p]

    @classmethod
    def emit_block(cls, block, *, global_refs=None) -> tuple[str, list[str]] | None:
        sqlite = block.kind is Kind.SQLITE_QUERY
        return cls._emit_sql(block, sqlite=sqlite, global_refs=global_refs)

    @classmethod
    def _emit_sql(
        cls,
        block,
        *,
        sqlite: bool,
        global_refs=None,
    ) -> tuple[str, list[str]]:
        sql = cls._extract_sql_text(block, global_refs)
        output = resolve_output_path(block)
        reader = getattr(block, "reader", None)
        if reader is None:
            raise ValueError("SQL emission requires dispatch metadata")
        crosstab = extract_crosstab_options(block)
        header = None if crosstab else cls._extract_header(block)

        reader_kwargs = getattr(block, "reader_kwargs", {})
        reader_kwargs_items = [f"{key}={value!r}" for key, value in reader_kwargs.items()]
        inst_expr = f"{reader.name}({', '.join(reader_kwargs_items)})"

        kwargs: dict[str, object] = {
            "sql": sql,
            "output": output,
            "reader": CodeExpr(inst_expr),
        }
        if sqlite:
            kwargs["inputs"] = cls._extract_table_inputs(block)
        if header:
            kwargs["header"] = header
        if crosstab:
            kwargs["crosstab"] = crosstab

        from vg2c.utilities.pipeline_context import PipelineContext

        stmt = PipelineContext.run_query.render(**kwargs)
        suffix = "sqlite_query" if sqlite else "sql_query"
        return suffix, [stmt]
