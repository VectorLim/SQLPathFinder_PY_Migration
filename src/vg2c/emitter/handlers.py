from __future__ import annotations

import re

from vg2c.dispatch.models import DispatchedBlock
from vg2c.emitter.macro import placeholders_to_python_expr
from vg2c.emitter.models import EmitContext
from vg2c.emitter.utilities_embed import (
    register_reader_emission,
    register_utility_emission,
)
from vg2c.emitter.utility_shapes import classify_utility
from vg2c.frontend.models import Kind
from vg2c.resolver.models import ResolvedBlock

__all__ = ["create_handlers"]

_SQL_MACRO_TOKEN_RE = re.compile(r"@@SQLMACRO:(\d+)@@")
_SLUG_RE = re.compile(r"[^a-z0-9]+")


def _python_multiline_literal(text: str) -> str:
    """Emit readable Python string literals for multiline SQL blocks."""
    if "\n" not in text:
        return repr(text)
    # Keep SQL readable in emitted script while still producing valid Python.
    escaped = text.replace('"""', '\\"\\"\\"')
    return f'"""{escaped}"""'


def _strip_quotes(value: str) -> str:
    value = value.strip()
    if len(value) >= 2 and value[0] == value[-1] and value[0] in {'"', "'"}:
        return value[1:-1]
    return value


def _declared_headers(block: ResolvedBlock) -> list[str] | None:
    """Extract declared headers from /HEADERS option.

    Returns None if:
    - /HEADERS is not present
    - /HEADERS contains CrossTab->[[...]] (dynamic columns)

    Otherwise returns a list of stripped header names.
    """
    headers_value = block.resolved_options.lookup.get("HEADERS")
    if not headers_value:
        return None

    # Skip if this is a crosstab block (dynamic columns)
    if "CrossTab->[[" in headers_value:
        return None

    # Split, strip quotes and whitespace, filter empties
    stripped = _strip_quotes(headers_value)
    parts = [p.strip() for p in stripped.split(",")]
    return [p for p in parts if p]


def _value_to_python_expr(value: str | None) -> str:
    if value is None:
        return "None"
    return placeholders_to_python_expr(_strip_quotes(value))


def _resolve_output_path(block: ResolvedBlock, fallback_ext: str) -> str:
    csv_value = block.resolved_options.lookup.get("CSV")
    if csv_value:
        return _strip_quotes(csv_value)

    write_file_value = block.resolved_options.lookup.get("WRITE-FILE")
    if write_file_value:
        candidate = _strip_quotes(write_file_value)
        if candidate.upper() not in {"Y", "N"}:
            return candidate

    return f"step_{block.parsed.index:04d}.{fallback_ext}"


def _function_name(block: ResolvedBlock, suffix: str) -> str:
    prompt_text = _strip_quotes(block.resolved_options.lookup.get("PROMPT-TEXT", ""))
    slug = _SLUG_RE.sub("_", prompt_text.lower()).strip("_")
    base = slug or suffix

    prefix = f"step_{block.parsed.index:04d}_"
    max_total = 80
    keep = max(8, max_total - len(prefix))
    base = base[:keep].strip("_") or suffix
    return prefix + base


def _sql_macro_expr(ctx: EmitContext, block: ResolvedBlock, call_index: int) -> str:
    if call_index < 0 or call_index >= len(block.sql_macro_calls):
        return repr(f"@@SQLMACRO:{call_index}@@")

    call = block.sql_macro_calls[call_index]
    path_expr = _value_to_python_expr(call.csv_path)
    col_expr = repr(call.column_ref)
    lead_expr = repr(call.lead_in)
    register_utility_emission(ctx, "sql_macros")
    return f"ctx.sql_macros.sql_get_csv_list({path_expr}, {col_expr}, {lead_expr})"


def _sql_to_python_expr(ctx: EmitContext, sql: str, block: ResolvedBlock) -> str:
    if "@@SQLMACRO:" not in sql:
        return _python_multiline_literal(sql)

    parts: list[str] = []
    cursor = 0
    for match in _SQL_MACRO_TOKEN_RE.finditer(sql):
        literal = sql[cursor : match.start()]
        if literal:
            parts.append(_python_multiline_literal(literal))

        parts.append(_sql_macro_expr(ctx, block, int(match.group(1))))
        cursor = match.end()

    tail = sql[cursor:]
    if tail:
        parts.append(_python_multiline_literal(tail))

    if not parts:
        return _python_multiline_literal(sql)
    if len(parts) == 1:
        return parts[0]
    return " + ".join(parts)


def _emit_reader(
    ctx: EmitContext,
    block: ResolvedBlock,
    dispatched: DispatchedBlock,
    db_type: str,
    suffix: str,
) -> tuple[str, str]:
    register_reader_emission(ctx)
    register_utility_emission(ctx, "ctx", "csv_io", "macro")

    sql_expr = _sql_to_python_expr(ctx, dispatched.rewritten_sql, block)
    output_expr = repr(_resolve_output_path(block, "csv"))

    # Extract crosstab parameters
    ctrow = _strip_quotes(block.resolved_options.lookup.get("CTROW", ""))
    ctheader = _strip_quotes(block.resolved_options.lookup.get("CTHEADER", ""))
    ctvalue = _strip_quotes(block.resolved_options.lookup.get("CTVALUE", ""))
    has_crosstab = bool(ctrow and ctheader and ctvalue)

    # Extract declared headers (skip for crosstab - dynamic columns)
    declared_hdrs = _declared_headers(block) if not has_crosstab else None
    header_arg = f", header={declared_hdrs!r}" if declared_hdrs else ""

    func_name = _function_name(block, suffix)

    if has_crosstab:
        register_utility_emission(ctx, "crosstab")
        row_keys = [c.strip() for c in ctrow.split(",") if c.strip()]
        crosstab_arg = f", crosstab={{'row_keys': {row_keys!r}, 'header_key': {ctheader!r}, 'value_key': {ctvalue!r}}}"
    else:
        crosstab_arg = ""

    func_code = f"""\
def {func_name}(ctx):
    ctx.run_query(
        sql={sql_expr},
        output={output_expr},
        source_type={repr(db_type)}{header_arg}{crosstab_arg},
    )
"""
    return func_code, f"{func_name}(ctx)"


def _emit_sql_query(
    ctx: EmitContext, block: ResolvedBlock, dispatched: DispatchedBlock
) -> tuple[str, str]:
    """Emit OracleReader read step for signal-resolved Oracle dialects."""
    assert dispatched is not None
    db_by_dialect = {
        "oracle_mars": ("MARS", "mars_read"),
        "oracle_oasys": ("OASYS", "oasys_read"),
        "oracle_aries": ("ARIES", "aries_read"),
    }
    db_type, suffix = db_by_dialect.get(
        dispatched.dialect,
        (dispatched.reader_target.database_arg or "MARS", "sql_query"),
    )
    return _emit_reader(ctx, block, dispatched, db_type, suffix)


def _emit_sqlite_query(
    ctx: EmitContext, block: ResolvedBlock, dispatched: DispatchedBlock
) -> tuple[str, str]:
    """Emit SQLite query runner using unified run_query interface."""
    assert dispatched is not None
    register_reader_emission(ctx)
    register_utility_emission(ctx, "ctx", "sqlite_engine", "csv_io", "macro")

    sql_expr = _sql_to_python_expr(ctx, dispatched.rewritten_sql, block)
    output_expr = repr(_resolve_output_path(block, "csv"))

    # Extract input CSVs from /TABLE= options
    inputs = []
    for key, value in block.resolved_options.pairs:
        if key == "TABLE":
            for table_name in value.split(","):
                table_name = table_name.strip()
                if table_name:
                    inputs.append(_value_to_python_expr(table_name))

    inputs_str = "[" + ", ".join(inputs) + "]" if inputs else "[]"

    # Extract crosstab parameters
    ctrow = _strip_quotes(block.resolved_options.lookup.get("CTROW", ""))
    ctheader = _strip_quotes(block.resolved_options.lookup.get("CTHEADER", ""))
    ctvalue = _strip_quotes(block.resolved_options.lookup.get("CTVALUE", ""))
    has_crosstab = bool(ctrow and ctheader and ctvalue)

    # Extract declared headers (skip for crosstab - dynamic columns)
    declared_hdrs = _declared_headers(block) if not has_crosstab else None
    header_arg = f", header={declared_hdrs!r}" if declared_hdrs else ""

    func_name = _function_name(block, "sqlite_query")

    if has_crosstab:
        register_utility_emission(ctx, "crosstab")
        row_keys = [c.strip() for c in ctrow.split(",") if c.strip()]
        crosstab_arg = f", crosstab={{'row_keys': {row_keys!r}, 'header_key': {ctheader!r}, 'value_key': {ctvalue!r}}}"
    else:
        crosstab_arg = ""

    func_code = f"""\
def {func_name}(ctx):
    ctx.run_query(
        sql={sql_expr},
        output={output_expr},
        source_type='sqlite',
        inputs={inputs_str}{header_arg}{crosstab_arg},
    )
"""

    call_site = f"{func_name}(ctx)"
    return func_code, call_site


def _emit_write_file(
    ctx: EmitContext, block: ResolvedBlock, dispatched: DispatchedBlock | None
) -> tuple[str, str]:
    """Emit write_file call."""
    register_utility_emission(ctx, "macro")
    path_expr = _value_to_python_expr(_resolve_output_path(block, "txt"))
    template_expr = repr(block.resolved_body)

    func_name = _function_name(block, "write_file")
    func_code = f"""\
def {func_name}(ctx):
    ctx.write_file(path={path_expr}, template={template_expr})
"""

    call_site = f"{func_name}(ctx)"
    return func_code, call_site


def _emit_utility(
    ctx: EmitContext, block: ResolvedBlock, dispatched: DispatchedBlock | None
) -> tuple[str, str]:
    """Emit utility call based on utilities string."""
    utilities_str = block.resolved_options.lookup.get("UTILITIES", "")
    shape_info = classify_utility(utilities_str)
    argv_expr = "[" + ", ".join(_value_to_python_expr(t) for t in shape_info.argv) + "]"
    argv_tail_expr = (
        "[" + ", ".join(_value_to_python_expr(t) for t in shape_info.argv[1:]) + "]"
        if len(shape_info.argv) > 1
        else "[]"
    )

    func_name = _function_name(block, "utility")

    if shape_info.shape == "run-python-script":
        register_utility_emission(ctx, "external")
        func_code = f"def {func_name}(ctx):\n    ctx.external.run({argv_expr})\n"
    elif shape_info.shape == "email":
        # TODO: SQLPathFinder_Email.va argv positions not yet standardised.
        # Emit a stub + diagnostic until a real fixture pins the positions.
        func_code = f"def {func_name}(ctx):\n    pass  # TODO: email utility — argv positions unresolved\n"
    elif shape_info.shape in ("robocopy", "spf-copy"):
        register_utility_emission(ctx, "fs_ops")
        # RoboCopy.va argv: <filename> <source_dir> <dest_dir> [flags...]
        # argv[0]=exe, argv[1]=filename, argv[2]=source_dir, argv[3]=dest_dir
        # src = source_dir\filename  (remote → local)
        # dst = dest_dir (shutil.copy2 handles placing the file there)
        filename_expr = (
            _value_to_python_expr(shape_info.argv[1])
            if len(shape_info.argv) > 1
            else repr("")
        )
        src_dir_expr = (
            _value_to_python_expr(shape_info.argv[2])
            if len(shape_info.argv) > 2
            else repr("")
        )
        dst_expr = (
            _value_to_python_expr(shape_info.argv[3])
            if len(shape_info.argv) > 3
            else repr(".")
        )
        src_expr = f"os.path.join({src_dir_expr}, {filename_expr})"
        func_code = f"def {func_name}(ctx):\n    ctx.fs_ops.copy(src={src_expr}, dst={dst_expr})\n"
    elif shape_info.shape == "spf-delete":
        register_utility_emission(ctx, "fs_ops")
        # SPFDelete arg[1] is a comma-joined path list; split into individual paths
        paths_raw = shape_info.argv[1] if len(shape_info.argv) > 1 else ""
        paths_items = [p.strip() for p in paths_raw.split(",") if p.strip()]
        paths_expr = (
            "[" + ", ".join(_value_to_python_expr(p) for p in paths_items) + "]"
        )
        func_code = (
            f"def {func_name}(ctx):\n    ctx.fs_ops.delete(paths={paths_expr})\n"
        )
    elif shape_info.shape == "bat-file" or shape_info.shape == "exe-direct":
        register_utility_emission(ctx, "external")
        func_code = f"def {func_name}(ctx):\n    ctx.external.run({argv_expr})\n"
    else:
        func_code = f"def {func_name}(ctx):\n    pass  # TODO: unhandled utility shape={shape_info.shape}\n"

    call_site = f"{func_name}(ctx)"
    return func_code, call_site


def _emit_html_report(
    ctx: EmitContext, block: ResolvedBlock, dispatched: DispatchedBlock | None
) -> tuple[str, str]:
    """Emit HTML report (as a comment — not translated)."""
    func_name = _function_name(block, "html_report")
    func_code = f"def {func_name}(ctx):\n    pass  # HTML report not translated\n"
    call_site = f"{func_name}(ctx)"
    return func_code, call_site


def _emit_unknown(
    ctx: EmitContext, block: ResolvedBlock, dispatched: DispatchedBlock | None
) -> tuple[str, str]:
    """Emit unknown block as TODO."""
    func_name = _function_name(block, "unknown")
    func_code = (
        f"def {func_name}(ctx):\n    pass  # TODO: unhandled kind={block.kind}\n"
    )
    call_site = f"{func_name}(ctx)"
    return func_code, call_site


def create_handlers() -> dict[Kind, callable]:
    """Create a mapping of Kind -> handler function."""
    return {
        Kind.SQL_QUERY: _emit_sql_query,
        Kind.SQLITE_QUERY: _emit_sqlite_query,
        Kind.WRITE_FILE: _emit_write_file,
        Kind.UTILITY: _emit_utility,
        Kind.HTML_REPORT: _emit_html_report,
        Kind.UNKNOWN: _emit_unknown,
    }
