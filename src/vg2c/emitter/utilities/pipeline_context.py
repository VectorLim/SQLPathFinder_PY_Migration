"""PipelineContext - runtime context and block emitter."""

from __future__ import annotations

import re
from typing import Any, ContextManager

from vg2c.dispatch.models import DispatchedBlock
from vg2c.emitter.codegen import (
    CallSpec,
    FunctionDef,
    PyExpr,
    declared_headers,
    emit_call,
    python_literal_for_option,
    register_call_embed,
    strip_quotes,
)
from vg2c.emitter.models import EmitContext
from vg2c.emitter.readers import ReaderRuntime
from vg2c.emitter.utilities._base import UtilitySpec
from vg2c.emitter.utilities._registry import (
    classify_utility_command,
    register_utility,
)
from vg2c.emitter.utilities.crosstab import CrosstabUtility
from vg2c.emitter.utilities.csv_io import CsvIO
from vg2c.emitter.utilities.external import ExternalProcess
from vg2c.emitter.utilities.fs_ops import FileSystemOps
from vg2c.emitter.utilities.macro_state import MacroState
from vg2c.emitter.utilities.mail import MailService
from vg2c.emitter.utilities.sql_macros import SqlMacros
from vg2c.emitter.utilities.sqlite_engine import SqliteEngine
from vg2c.frontend.models import Kind
from vg2c.resolver.models import ResolvedBlock

_SQL_MACRO_TOKEN_RE = re.compile(r"@@SQLMACRO:(\d+)@@")


@register_utility
class PipelineContext(UtilitySpec):
    """Single runtime context object for generated scripts."""

    utility_name = "ctx"
    utility_imports = ("from typing import Any, ContextManager",)
    utility_dependencies = (
        "macro",
        "csv_io",
        "sqlite_engine",
        "sql_macros",
        "fs_ops",
        "mail",
        "external",
        "crosstab",
        "reader_runtime",
    )
    utility_embed_exclude_methods = (
        "emit",
        "_resolve_output_path",
        "_crosstab_kwarg",
        "_table_inputs",
        "_sql_macro_call",
        "_sql_to_python_expr",
        "_build_run_query_call",
        "_emit_sql_query",
        "_emit_sqlite_query",
        "_emit_write_file",
        "_emit_utility",
        "_emit_html_report",
        "_emit_unknown",
    )

    @classmethod
    def emit(
        cls,
        ctx: EmitContext,
        block: ResolvedBlock,
        dispatched: DispatchedBlock | None,
    ) -> tuple[str, str]:
        if block.kind is Kind.SQL_QUERY:
            return cls._emit_sql_query(ctx, block, dispatched)
        if block.kind is Kind.SQLITE_QUERY:
            return cls._emit_sqlite_query(ctx, block, dispatched)
        if block.kind is Kind.WRITE_FILE:
            return cls._emit_write_file(ctx, block)
        if block.kind is Kind.UTILITY:
            return cls._emit_utility(ctx, block)
        if block.kind is Kind.HTML_REPORT:
            return cls._emit_html_report(block)
        return cls._emit_unknown(block)

    @staticmethod
    def _resolve_output_path(block: ResolvedBlock, fallback_ext: str) -> str:
        csv_value = block.resolved_options.lookup.get("CSV")
        if csv_value:
            return strip_quotes(csv_value)

        write_file_value = block.resolved_options.lookup.get("WRITE-FILE")
        if write_file_value:
            candidate = strip_quotes(write_file_value)
            if candidate.upper() not in {"Y", "N"}:
                return candidate

        return f"step_{block.parsed.index:04d}.{fallback_ext}"

    @staticmethod
    def _crosstab_kwarg(block: ResolvedBlock) -> tuple[PyExpr | None, bool]:
        ctrow = strip_quotes(block.resolved_options.lookup.get("CTROW", ""))
        ctheader = strip_quotes(block.resolved_options.lookup.get("CTHEADER", ""))
        ctvalue = strip_quotes(block.resolved_options.lookup.get("CTVALUE", ""))
        if not (ctrow and ctheader and ctvalue):
            return None, False
        row_keys = [c.strip() for c in ctrow.split(",") if c.strip()]
        crosstab = PyExpr.dict_of(
            {
                "row_keys": PyExpr.literal(row_keys),
                "header_key": PyExpr.literal(ctheader),
                "value_key": PyExpr.literal(ctvalue),
            }
        )
        return crosstab, True

    @staticmethod
    def _table_inputs(block: ResolvedBlock) -> list[PyExpr]:
        inputs: list[PyExpr] = []
        for key, value in block.resolved_options.pairs:
            if key != "TABLE":
                continue
            for table_name in value.split(","):
                table_name = table_name.strip()
                if table_name:
                    inputs.append(python_literal_for_option(table_name))
        return inputs

    @classmethod
    def _sql_macro_call(cls, block: ResolvedBlock, call_index: int) -> PyExpr:
        if call_index < 0 or call_index >= len(block.sql_macro_calls):
            return PyExpr.literal(f"@@SQLMACRO:{call_index}@@")
        call = block.sql_macro_calls[call_index]
        spec = emit_call(
            SqlMacros.sql_get_csv_list,
            python_literal_for_option(call.csv_path),
            PyExpr.literal(call.column_ref),
            PyExpr.literal(call.lead_in),
        )
        return PyExpr.raw(spec.render())

    @classmethod
    def _sql_to_python_expr(cls, sql: str, block: ResolvedBlock) -> PyExpr:
        if "@@SQLMACRO:" not in sql:
            return PyExpr.multiline_string(sql)

        parts: list[PyExpr] = []
        cursor = 0
        for match in _SQL_MACRO_TOKEN_RE.finditer(sql):
            literal = sql[cursor : match.start()]
            if literal:
                parts.append(PyExpr.multiline_string(literal))
            parts.append(cls._sql_macro_call(block, int(match.group(1))))
            cursor = match.end()

        tail = sql[cursor:]
        if tail:
            parts.append(PyExpr.multiline_string(tail))

        if not parts:
            return PyExpr.multiline_string(sql)
        return PyExpr.concat(parts)

    @staticmethod
    def _build_run_query_call(
        *,
        sql: PyExpr,
        output: PyExpr,
        source_type: PyExpr,
        inputs: PyExpr | None,
        header: list[str] | None,
        crosstab: PyExpr | None,
    ) -> CallSpec:
        kwargs: dict[str, PyExpr] = {
            "sql": sql,
            "output": output,
            "source_type": source_type,
        }
        if inputs is not None:
            kwargs["inputs"] = inputs
        if header:
            kwargs["header"] = PyExpr.literal(header)
        if crosstab is not None:
            kwargs["crosstab"] = crosstab
        return emit_call(PipelineContext.run_query, **kwargs)

    @classmethod
    def _emit_sql_query(
        cls,
        ctx: EmitContext,
        block: ResolvedBlock,
        dispatched: DispatchedBlock | None,
    ) -> tuple[str, str]:
        if dispatched is None:
            raise ValueError("SQL query emission requires dispatch metadata")

        db_by_dialect = {
            "oracle_mars": ("MARS", "mars_read"),
            "oracle_oasys": ("OASYS", "oasys_read"),
            "oracle_aries": ("ARIES", "aries_read"),
        }
        db_type, suffix = db_by_dialect.get(
            dispatched.dialect,
            (dispatched.reader_target.database_arg or "MARS", "sql_query"),
        )

        crosstab, has_crosstab = cls._crosstab_kwarg(block)
        header = declared_headers(block) if not has_crosstab else None

        spec = cls._build_run_query_call(
            sql=cls._sql_to_python_expr(dispatched.rewritten_sql, block),
            output=PyExpr.literal(cls._resolve_output_path(block, "csv")),
            source_type=PyExpr.literal(db_type),
            inputs=None,
            header=header,
            crosstab=crosstab,
        )
        register_call_embed(ctx, spec)
        fdef = FunctionDef.from_call(
            FunctionDef.name_for(block, suffix),
            spec,
            multiline=True,
        )
        return fdef.source, fdef.call_site

    @classmethod
    def _emit_sqlite_query(
        cls,
        ctx: EmitContext,
        block: ResolvedBlock,
        dispatched: DispatchedBlock | None,
    ) -> tuple[str, str]:
        if dispatched is None:
            raise ValueError("SQLite query emission requires dispatch metadata")

        crosstab, has_crosstab = cls._crosstab_kwarg(block)
        header = declared_headers(block) if not has_crosstab else None

        inputs = cls._table_inputs(block)
        spec = cls._build_run_query_call(
            sql=cls._sql_to_python_expr(dispatched.rewritten_sql, block),
            output=PyExpr.literal(cls._resolve_output_path(block, "csv")),
            source_type=PyExpr.literal("sqlite"),
            inputs=PyExpr.list_of(inputs),
            header=header,
            crosstab=crosstab,
        )
        register_call_embed(ctx, spec)

        fdef = FunctionDef.from_call(
            FunctionDef.name_for(block, "sqlite_query"),
            spec,
            multiline=True,
        )
        return fdef.source, fdef.call_site

    @classmethod
    def _emit_write_file(
        cls,
        ctx: EmitContext,
        block: ResolvedBlock,
    ) -> tuple[str, str]:
        spec = emit_call(
            PipelineContext.write_file,
            path=python_literal_for_option(cls._resolve_output_path(block, "txt")),
            template=PyExpr.literal(block.resolved_body),
        )
        register_call_embed(ctx, spec)
        fdef = FunctionDef.from_call(FunctionDef.name_for(block, "write_file"), spec)
        return fdef.source, fdef.call_site

    @classmethod
    def _emit_utility(
        cls,
        ctx: EmitContext,
        block: ResolvedBlock,
    ) -> tuple[str, str]:
        utilities_str = block.resolved_options.lookup.get("UTILITIES", "")
        match = classify_utility_command(utilities_str)
        if match.utility_cls is None:
            fdef = FunctionDef.from_body(
                FunctionDef.name_for(block, "utility"),
                [f"pass  # TODO: utility shape not translated: {match.shape}"],
            )
            return fdef.source, fdef.call_site

        return match.utility_cls.emit(ctx, block, match)

    @staticmethod
    def _emit_html_report(block: ResolvedBlock) -> tuple[str, str]:
        fdef = FunctionDef.from_body(
            FunctionDef.name_for(block, "html_report"),
            ["pass  # HTML report not translated"],
        )
        return fdef.source, fdef.call_site

    @staticmethod
    def _emit_unknown(block: ResolvedBlock) -> tuple[str, str]:
        fdef = FunctionDef.from_body(
            FunctionDef.name_for(block, "unknown"),
            [f"pass  # TODO: unhandled kind={block.kind}"],
        )
        return fdef.source, fdef.call_site

    def __init__(self) -> None:
        self.macro = MacroState()
        self.csv_io = CsvIO()
        self.sqlite_engine = SqliteEngine()
        self.sql_macros = SqlMacros()
        self.fs_ops = FileSystemOps()
        self.mail = MailService()
        self.external = ExternalProcess()
        self.reader_runtime = ReaderRuntime()
        self.crosstab = CrosstabUtility()

    def macro_scope(self, row: dict[str, str] | None = None) -> ContextManager[None]:
        return self.macro.scope(row=row)

    def write_file(
        self,
        path: str,
        template: str,
        vars: dict[str, str] | None = None,
    ) -> None:
        self.macro.write_file(path, template, vars=vars)

    def read(self, sql: str, db_type: str):
        return self.reader_runtime.read(
            sql=sql, db_type=db_type, macro_state=self.macro
        )

    def run_query(
        self,
        sql: str,
        output: str,
        source_type: str,
        inputs: list[str] | None = None,
        header: list[str] | None = None,
        crosstab: dict | None = None,
    ):
        sql = self.macro.substitute_sql(sql)

        if source_type.lower() == "sqlite":
            result = self.sqlite_engine.execute(sql, inputs or [])
        else:
            result = self.reader_runtime.read(
                sql=sql, db_type=source_type, macro_state=None
            )

        if crosstab:
            result = self.crosstab.apply(
                result,
                row_keys=crosstab["row_keys"],
                header_key=crosstab["header_key"],
                value_key=crosstab["value_key"],
            )

        self.csv_io.write(output, result, header=header)

    def eval_condition(self, lhs: str, op: str, rhs: str, *args: Any) -> bool:
        return self.macro.eval_condition(lhs, op, rhs)
