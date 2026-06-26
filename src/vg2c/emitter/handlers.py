from __future__ import annotations

import re

from vg2c.dispatch.models import DispatchedBlock
from vg2c.emitter.models import EmitContext
from vg2c.emitter.readers import register_reader_emission
from vg2c.emitter.utility_shapes import classify_utility
from vg2c.frontend.models import Kind
from vg2c.resolver.models import ResolvedBlock

__all__ = ["create_handlers"]

_PLACEHOLDER_RE = re.compile(r"<<<([^>]+)>>>|<<>>")
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


def _macro_name(raw: str) -> str:
    name = raw.strip()
    if name.startswith("<<<") and name.endswith(">>>"):
        name = name[3:-3]
    return name.strip().upper()


def _value_to_python_expr(value: str | None) -> str:
    if value is None:
        return "None"

    text = _strip_quotes(value)
    if not text:
        return repr("")

    parts: list[str] = []
    cursor = 0

    for match in _PLACEHOLDER_RE.finditer(text):
        literal = text[cursor : match.start()]
        if literal:
            parts.append(repr(literal))

        named = match.group(1)
        if named is not None:
            parts.append(f'ctx.macro.named("{_macro_name(named)}")')
        else:
            parts.append("ctx.macro.positional()")

        cursor = match.end()

    tail = text[cursor:]
    if tail:
        parts.append(repr(tail))

    if not parts:
        return repr(text)
    if len(parts) == 1:
        return parts[0]
    return " + ".join(parts)


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


def _sql_macro_expr(block: ResolvedBlock, call_index: int) -> str:
    if call_index < 0 or call_index >= len(block.sql_macro_calls):
        return repr(f"@@SQLMACRO:{call_index}@@")

    call = block.sql_macro_calls[call_index]
    path_expr = _value_to_python_expr(call.csv_path)
    col_expr = repr(call.column_ref)
    lead_expr = repr(call.lead_in)
    return f"ctx.sql_macros.sql_get_csv_list({path_expr}, {col_expr}, {lead_expr})"


def _sql_to_python_expr(sql: str, block: ResolvedBlock) -> str:
    if "@@SQLMACRO:" not in sql:
        return _python_multiline_literal(sql)

    parts: list[str] = []
    cursor = 0
    for match in _SQL_MACRO_TOKEN_RE.finditer(sql):
        literal = sql[cursor : match.start()]
        if literal:
            parts.append(_python_multiline_literal(literal))

        parts.append(_sql_macro_expr(block, int(match.group(1))))
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

    sql_expr = _sql_to_python_expr(dispatched.rewritten_sql, block)
    output_expr = repr(_resolve_output_path(block, "csv"))

    func_name = _function_name(block, suffix)
    func_code = f"""\
def {func_name}(ctx):
    result = read(sql={sql_expr}, db_type={repr(db_type)}, macro_state=ctx.macro)
    ctx.csv_io.write({output_expr}, result)
"""
    return func_code, f"{func_name}(ctx)"


def _emit_mars_read(
    ctx: EmitContext, block: ResolvedBlock, dispatched: DispatchedBlock
) -> tuple[str, str]:
    """Emit MarsReader read step."""
    assert dispatched is not None
    return _emit_reader(ctx, block, dispatched, "MARS", "mars_read")


def _emit_oasys_read(
    ctx: EmitContext, block: ResolvedBlock, dispatched: DispatchedBlock
) -> tuple[str, str]:
    """Emit OracleReader read step for OASYS."""
    assert dispatched is not None
    return _emit_reader(ctx, block, dispatched, "OASYS", "oasys_read")


def _emit_aries_read(
    ctx: EmitContext, block: ResolvedBlock, dispatched: DispatchedBlock
) -> tuple[str, str]:
    """Emit AriesReader read step."""
    assert dispatched is not None
    return _emit_reader(ctx, block, dispatched, "ARIES", "aries_read")


def _emit_sqlite_query(
    ctx: EmitContext, block: ResolvedBlock, dispatched: DispatchedBlock
) -> tuple[str, str]:
    """Emit SQLite query runner."""
    assert dispatched is not None
    sql_expr = _sql_to_python_expr(dispatched.rewritten_sql, block)

    # Extract input CSVs from /TABLE= options
    inputs = []
    for key, value in block.resolved_options.pairs:
        if key == "TABLE":
            for table_name in value.split(","):
                table_name = table_name.strip()
                if table_name:
                    inputs.append(_value_to_python_expr(table_name))

    inputs_str = "[" + ", ".join(inputs) + "]" if inputs else "[]"

    func_name = _function_name(block, "sqlite_query")
    output_name = _resolve_output_path(block, "csv")

    func_code = f"""\
def {func_name}(ctx):
    ctx.sqlite_engine.run_join(
        sql={sql_expr},
        inputs={inputs_str},
        output={repr(output_name)},
    )
"""

    call_site = f"{func_name}(ctx)"
    return func_code, call_site


def _emit_write_file(
    ctx: EmitContext, block: ResolvedBlock, dispatched: DispatchedBlock | None
) -> tuple[str, str]:
    """Emit write_file call."""
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
        func_code = f"def {func_name}(ctx):\n    ctx.external.run({argv_expr})\n"
    elif shape_info.shape == "email":
        # TODO: SQLPathFinder_Email.va argv positions not yet standardised.
        # Emit a stub + diagnostic until a real fixture pins the positions.
        func_code = f"def {func_name}(ctx):\n    pass  # TODO: email utility — argv positions unresolved\n"
    elif shape_info.shape in ("robocopy", "spf-copy"):
        # Typical: robocopy <src> <dst> [flags...]
        src_expr = (
            _value_to_python_expr(shape_info.argv[1])
            if len(shape_info.argv) > 1
            else repr("")
        )
        dst_expr = (
            _value_to_python_expr(shape_info.argv[2])
            if len(shape_info.argv) > 2
            else repr("")
        )
        func_code = f"def {func_name}(ctx):\n    ctx.fs_ops.copy(src={src_expr}, dst={dst_expr})\n"
    elif shape_info.shape == "spf-delete":
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
        Kind.MARS_READ: _emit_mars_read,
        Kind.OASYS_READ: _emit_oasys_read,
        Kind.ARIES_READ: _emit_aries_read,
        Kind.SQLITE_QUERY: _emit_sqlite_query,
        Kind.WRITE_FILE: _emit_write_file,
        Kind.UTILITY: _emit_utility,
        Kind.HTML_REPORT: _emit_html_report,
        Kind.UNKNOWN: _emit_unknown,
    }
