from __future__ import annotations

import re

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
from vg2c.emitter.codegen.utility_shapes import build_shape_result
from vg2c.emitter.models import EmitContext
from vg2c.emitter.utilities import (
    CsvIO,
    ExternalProcess,
    FileSystemOps,
    MacroState,
    MailService,
    PipelineContext,
    SqlMacros,
    SqliteEngine,
    read,
)
from vg2c.emitter.utilities_embed import (
    register_reader_emission,
    register_utility_emission,
)
from vg2c.emitter.utility_shapes import classify_utility
from vg2c.frontend.models import Kind
from vg2c.resolver.models import ResolvedBlock

__all__ = ["create_handlers"]

_SQL_MACRO_TOKEN_RE = re.compile(r"@@SQLMACRO:(\d+)@@")


# ---------------------------------------------------------------------------
# Block-option extraction (compile-time only)
# ---------------------------------------------------------------------------


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


def _crosstab_kwarg(block: ResolvedBlock) -> tuple[PyExpr | None, bool]:
    """Return (crosstab PyExpr, has_crosstab). PyExpr is None when absent."""
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


# ---------------------------------------------------------------------------
# SQL body → PyExpr (handles @@SQLMACRO:N@@ placeholders)
# ---------------------------------------------------------------------------


def _sql_macro_call(block: ResolvedBlock, call_index: int) -> PyExpr:
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


def _sql_to_python_expr(sql: str, block: ResolvedBlock) -> PyExpr:
    if "@@SQLMACRO:" not in sql:
        return PyExpr.multiline_string(sql)

    parts: list[PyExpr] = []
    cursor = 0
    for match in _SQL_MACRO_TOKEN_RE.finditer(sql):
        literal = sql[cursor : match.start()]
        if literal:
            parts.append(PyExpr.multiline_string(literal))
        parts.append(_sql_macro_call(block, int(match.group(1))))
        cursor = match.end()

    tail = sql[cursor:]
    if tail:
        parts.append(PyExpr.multiline_string(tail))

    if not parts:
        return PyExpr.multiline_string(sql)
    return PyExpr.concat(parts)


# ---------------------------------------------------------------------------
# Per-Kind handlers
# ---------------------------------------------------------------------------


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


def _emit_sql_query(
    ctx: EmitContext, block: ResolvedBlock, dispatched: DispatchedBlock
) -> tuple[str, str]:
    """Emit Oracle reader step (MARS/OASYS/ARIES) via ``ctx.run_query``."""
    assert dispatched is not None
    register_reader_emission(ctx)

    db_by_dialect = {
        "oracle_mars": ("MARS", "mars_read"),
        "oracle_oasys": ("OASYS", "oasys_read"),
        "oracle_aries": ("ARIES", "aries_read"),
    }
    db_type, suffix = db_by_dialect.get(
        dispatched.dialect,
        (dispatched.reader_target.database_arg or "MARS", "sql_query"),
    )

    crosstab, has_crosstab = _crosstab_kwarg(block)
    if has_crosstab:
        register_utility_emission(ctx, "crosstab")
    header = declared_headers(block) if not has_crosstab else None

    spec = _build_run_query_call(
        sql=_sql_to_python_expr(dispatched.rewritten_sql, block),
        output=PyExpr.literal(_resolve_output_path(block, "csv")),
        source_type=PyExpr.literal(db_type),
        inputs=None,
        header=header,
        crosstab=crosstab,
    )
    register_call_embed(ctx, spec)
    register_utility_emission(ctx, "csv_io", "macro")

    fdef = FunctionDef.from_call(
        FunctionDef.name_for(block, suffix), spec, multiline=True
    )
    return fdef.source, fdef.call_site


def _emit_sqlite_query(
    ctx: EmitContext, block: ResolvedBlock, dispatched: DispatchedBlock
) -> tuple[str, str]:
    """Emit SQLite query step via ``ctx.run_query`` (source_type='sqlite')."""
    assert dispatched is not None
    register_reader_emission(ctx)
    register_utility_emission(ctx, "sqlite_engine", "csv_io", "macro")

    crosstab, has_crosstab = _crosstab_kwarg(block)
    if has_crosstab:
        register_utility_emission(ctx, "crosstab")
    header = declared_headers(block) if not has_crosstab else None

    inputs = _table_inputs(block)
    spec = _build_run_query_call(
        sql=_sql_to_python_expr(dispatched.rewritten_sql, block),
        output=PyExpr.literal(_resolve_output_path(block, "csv")),
        source_type=PyExpr.literal("sqlite"),
        inputs=PyExpr.list_of(inputs),
        header=header,
        crosstab=crosstab,
    )
    register_call_embed(ctx, spec)

    fdef = FunctionDef.from_call(
        FunctionDef.name_for(block, "sqlite_query"), spec, multiline=True
    )
    return fdef.source, fdef.call_site


def _emit_write_file(
    ctx: EmitContext, block: ResolvedBlock, dispatched: DispatchedBlock | None
) -> tuple[str, str]:
    register_utility_emission(ctx, "macro")
    spec = emit_call(
        PipelineContext.write_file,
        path=python_literal_for_option(_resolve_output_path(block, "txt")),
        template=PyExpr.literal(block.resolved_body),
    )
    register_call_embed(ctx, spec)
    fdef = FunctionDef.from_call(FunctionDef.name_for(block, "write_file"), spec)
    return fdef.source, fdef.call_site


def _emit_utility(
    ctx: EmitContext, block: ResolvedBlock, dispatched: DispatchedBlock | None
) -> tuple[str, str]:
    """Emit a utility call by dispatching on its classified shape."""
    utilities_str = block.resolved_options.lookup.get("UTILITIES", "")
    shape_info = classify_utility(utilities_str)
    result = build_shape_result(shape_info)

    func_name = FunctionDef.name_for(block, "utility")
    if result.call is not None:
        register_call_embed(ctx, result.call)
        fdef = FunctionDef.from_call(func_name, result.call)
    else:
        message = result.stub_message or f"unhandled utility shape={shape_info.shape}"
        fdef = FunctionDef.from_body(func_name, [f"pass  # TODO: {message}"])
    return fdef.source, fdef.call_site


def _emit_html_report(
    ctx: EmitContext, block: ResolvedBlock, dispatched: DispatchedBlock | None
) -> tuple[str, str]:
    fdef = FunctionDef.from_body(
        FunctionDef.name_for(block, "html_report"),
        ["pass  # HTML report not translated"],
    )
    return fdef.source, fdef.call_site


def _emit_unknown(
    ctx: EmitContext, block: ResolvedBlock, dispatched: DispatchedBlock | None
) -> tuple[str, str]:
    fdef = FunctionDef.from_body(
        FunctionDef.name_for(block, "unknown"),
        [f"pass  # TODO: unhandled kind={block.kind}"],
    )
    return fdef.source, fdef.call_site


def create_handlers() -> dict[Kind, callable]:
    return {
        Kind.SQL_QUERY: _emit_sql_query,
        Kind.SQLITE_QUERY: _emit_sqlite_query,
        Kind.WRITE_FILE: _emit_write_file,
        Kind.UTILITY: _emit_utility,
        Kind.HTML_REPORT: _emit_html_report,
        Kind.UNKNOWN: _emit_unknown,
    }


# Keep unused-import linters quiet: these symbols are referenced by tests
# and adjacent modules even if not directly used in this file.
_ = (CsvIO, ExternalProcess, FileSystemOps, MacroState, MailService, SqliteEngine, read)
