"""SqliteEngine - execute SQL joins over CSV inputs."""

from __future__ import annotations

import re
from functools import partial

from vg2c.emitter.utilities._base import UtilitySpec
from vg2c.emitter.utilities.crosstab import CrosstabUtility
from vg2c.emitter.utilities._emit_helpers import (
    RawExpr,
    _emit_step_source,
    _step_name,
    option_to_python_expr,
    resolve_output_path,
    strip_quotes,
)
from vg2c.frontend.models import Kind


class SqliteEngine(UtilitySpec):
    """Emit query calls for external and SQLite readers."""

    utility_name = "sqlite_engine"
    handles = (Kind.SQL_QUERY, Kind.SQLITE_QUERY)

    _SQL_MACRO_TOKEN_RE = re.compile(r"@@SQLMACRO:(\d+)@@")

    @staticmethod
    def _extract_sql_text(block, dispatched) -> str | RawExpr:
        sql = (
            dispatched.rewritten_sql if dispatched is not None else block.resolved_body
        )
        if "@@SQLMACRO:" not in sql:
            return sql

        parts: list[str] = []
        cursor = 0
        for match in SqliteEngine._SQL_MACRO_TOKEN_RE.finditer(sql):
            literal = sql[cursor : match.start()]
            if literal:
                parts.append(repr(literal))

            call_index = int(match.group(1))
            if call_index < 0 or call_index >= len(block.sql_macro_calls):
                parts.append(repr(match.group(0)))
            else:
                call = block.sql_macro_calls[call_index]
                csv_path_expr = option_to_python_expr(call.csv_path)
                col_ref = repr(call.column_ref)
                lead_in = repr(call.lead_in)
                parts.append(
                    f"ctx.sql_macros.sql_get_csv_list({csv_path_expr}, {col_ref}, {lead_in})"
                )

            cursor = match.end()

        tail = sql[cursor:]
        if tail:
            parts.append(repr(tail))

        if not parts:
            return sql
        return RawExpr(" + ".join(parts))

    @staticmethod
    def _extract_table_inputs(block) -> list[str]:
        inputs: list[str] = []
        for key, value in block.resolved_options.pairs:
            if key != "TABLE":
                continue
            for table_name in value.split(","):
                table_name = strip_quotes(table_name.strip())
                if table_name:
                    inputs.append(table_name)
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
    def emit_block(cls, ctx, block, dispatched) -> tuple[str, str] | None:
        sqlite = block.kind is Kind.SQLITE_QUERY
        return cls._emit_sql(ctx, block, dispatched, sqlite=sqlite)

    @classmethod
    def _emit_sql(
        cls,
        ctx,
        block,
        dispatched,
        *,
        sqlite: bool,
    ) -> tuple[str, str]:
        if dispatched is None:
            raise ValueError("SQL emission requires dispatch metadata")

        sql = cls._extract_sql_text(block, dispatched)
        output = resolve_output_path(block)
        reader_cls = dispatched.reader_cls
        ctx.add_import(reader_cls.__module__, reader_cls.__name__)
        crosstab = CrosstabUtility.extract_options(block)
        header = None if crosstab else cls._extract_header(block)

        reader_kwargs_items = [f"{k}={repr(v)}" for k, v in dispatched.reader_kwargs.items()]
        inst_expr = f"{reader_cls.__name__}({', '.join(reader_kwargs_items)})"

        kwargs: dict[str, object] = {
            "sql": sql,
            "output": output,
            "reader": RawExpr(inst_expr),
        }
        if sqlite:
            kwargs["inputs"] = cls._extract_table_inputs(block)
        if header:
            kwargs["header"] = header
        if crosstab:
            kwargs["crosstab"] = crosstab

        stmt = ctx.render_method_call("ctx", "run_query", kwargs=kwargs)
        suffix = "sqlite_query" if sqlite else "sql_query"
        return _emit_step_source(_step_name(block, suffix), [stmt])
