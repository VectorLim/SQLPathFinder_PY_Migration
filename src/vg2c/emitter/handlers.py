from __future__ import annotations

from vg2c.dispatch.models import DispatchedBlock
from vg2c.emitter.macro_subst import MacroSubstituter
from vg2c.emitter.models import EmitContext
from vg2c.emitter.utility_shapes import classify_utility
from vg2c.frontend.models import Kind
from vg2c.resolver.models import ResolvedBlock, RowsInFile, StartMacro

__all__ = ["create_handlers"]


def _safe_string_literal(text: str) -> str:
    """Escape text for a triple-quoted Python string."""
    # For now, simple approach: replace backslashes and ensure no triple quotes
    text = text.replace('"""', r"\" \" \"")
    return text


def _emit_mars_read(
    ctx: EmitContext, block: ResolvedBlock, dispatched: DispatchedBlock
) -> tuple[str, str]:
    """Emit OracleReader for MARS."""
    assert dispatched is not None
    target = dispatched.reader_target
    record = (
        f'("{target.record_name}", "{target.record_version}")'
        if target.record_name
        else "None"
    )
    sql_literal = f'"""{_safe_string_literal(dispatched.rewritten_sql)}"""'

    func_name = f"step_{block.parsed.index:04d}_mars_read"
    func_code = f"""\
def {func_name}(ctx):
    reader = ctx.reader_mars(
        database="{target.database_arg}",
        node="{target.node}",
        record={record},
        instance="{target.instance or ''}",
    )
    result = reader.read(sql={sql_literal})
    ctx.csv_io.write("step_{block.parsed.index:04d}.csv", result)
"""

    call_site = f"{func_name}(ctx)"
    return func_code, call_site


def _emit_oasys_read(
    ctx: EmitContext, block: ResolvedBlock, dispatched: DispatchedBlock
) -> tuple[str, str]:
    """Emit OracleReader for OASYS."""
    assert dispatched is not None
    target = dispatched.reader_target
    record = (
        f'("{target.record_name}", "{target.record_version}")'
        if target.record_name
        else "None"
    )
    sql_literal = f'"""{_safe_string_literal(dispatched.rewritten_sql)}"""'

    func_name = f"step_{block.parsed.index:04d}_oasys_read"
    func_code = f"""\
def {func_name}(ctx):
    reader = ctx.reader_oasys(
        database="{target.database_arg}",
        node="{target.node}",
        record={record},
        instance="{target.instance or ''}",
    )
    result = reader.read(sql={sql_literal})
    ctx.csv_io.write("step_{block.parsed.index:04d}.csv", result)
"""

    call_site = f"{func_name}(ctx)"
    return func_code, call_site


def _emit_aries_read(
    ctx: EmitContext, block: ResolvedBlock, dispatched: DispatchedBlock
) -> tuple[str, str]:
    """Emit OracleReader for ARIES."""
    assert dispatched is not None
    target = dispatched.reader_target
    record = (
        f'("{target.record_name}", "{target.record_version}")'
        if target.record_name
        else "None"
    )
    sql_literal = f'"""{_safe_string_literal(dispatched.rewritten_sql)}"""'

    func_name = f"step_{block.parsed.index:04d}_aries_read"
    func_code = f"""\
def {func_name}(ctx):
    reader = ctx.reader_aries(
        database="{target.database_arg}",
        node="{target.node}",
        record={record},
        instance="{target.instance or ''}",
    )
    result = reader.read(sql={sql_literal})
    ctx.csv_io.write("step_{block.parsed.index:04d}.csv", result)
"""

    call_site = f"{func_name}(ctx)"
    return func_code, call_site


def _emit_sqlite_query(
    ctx: EmitContext, block: ResolvedBlock, dispatched: DispatchedBlock
) -> tuple[str, str]:
    """Emit SQLite query runner."""
    assert dispatched is not None
    sql_literal = f'"""{_safe_string_literal(dispatched.rewritten_sql)}"""'

    # Extract input CSVs from /TABLE= options
    inputs = []
    for key, value in block.resolved_options.pairs:
        if key == "TABLE":
            for table_name in value.split(","):
                table_name = table_name.strip()
                if table_name:
                    inputs.append(f'"{table_name}"')

    inputs_str = "[" + ", ".join(inputs) + "]" if inputs else "[]"

    func_name = f"step_{block.parsed.index:04d}_sqlite_query"
    output_name = f"step_{block.parsed.index:04d}.csv"

    func_code = f"""\
def {func_name}(ctx):
    ctx.sqlite_engine.run_join(
        sql={sql_literal},
        inputs={inputs_str},
        output="{output_name}",
    )
"""

    call_site = f"{func_name}(ctx)"
    return func_code, call_site


def _emit_write_file(
    ctx: EmitContext, block: ResolvedBlock, dispatched: DispatchedBlock | None
) -> tuple[str, str]:
    """Emit write_file call."""
    # Get the file path from /WRITE-FILE option
    path = block.resolved_options.lookup.get("WRITE-FILE", "output.txt")
    template_literal = f'"""{_safe_string_literal(block.resolved_body)}"""'

    func_name = f"step_{block.parsed.index:04d}_write_file"
    func_code = f"""\
def {func_name}(ctx):
    ctx.write_file(path="{path}", template={template_literal})
"""

    call_site = f"{func_name}(ctx)"
    return func_code, call_site


def _emit_utility(
    ctx: EmitContext, block: ResolvedBlock, dispatched: DispatchedBlock | None
) -> tuple[str, str]:
    """Emit utility call based on utilities string."""
    utilities_str = block.resolved_options.lookup.get("UTILITIES", "")
    shape_info = classify_utility(utilities_str)

    func_name = f"step_{block.parsed.index:04d}_utility"

    if shape_info.shape == "run-python-script":
        func_code = f'def {func_name}(ctx):\n    ctx.external.run([{", ".join(repr(t) for t in shape_info.argv)}])\n'
    elif shape_info.shape == "email":
        func_code = (
            f'def {func_name}(ctx):\n    ctx.mail.send(to="", subject="", body="")\n'
        )
    elif shape_info.shape in ("robocopy", "spf-copy"):
        func_code = f'def {func_name}(ctx):\n    ctx.fs_ops.copy(src="", dst="")\n'
    elif shape_info.shape == "spf-delete":
        func_code = f"def {func_name}(ctx):\n    ctx.fs_ops.delete(paths=[])\n"
    elif shape_info.shape == "bat-file" or shape_info.shape == "exe-direct":
        func_code = f'def {func_name}(ctx):\n    ctx.external.run([{", ".join(repr(t) for t in shape_info.argv)}])\n'
    else:
        func_code = f"def {func_name}(ctx):\n    pass  # TODO: unhandled utility shape={shape_info.shape}\n"

    call_site = f"{func_name}(ctx)"
    return func_code, call_site


def _emit_html_report(
    ctx: EmitContext, block: ResolvedBlock, dispatched: DispatchedBlock | None
) -> tuple[str, str]:
    """Emit HTML report (as a comment — not translated)."""
    func_name = f"step_{block.parsed.index:04d}_html_report"
    func_code = f"def {func_name}(ctx):\n    pass  # HTML report not translated\n"
    call_site = f"{func_name}(ctx)"
    return func_code, call_site


def _emit_unknown(
    ctx: EmitContext, block: ResolvedBlock, dispatched: DispatchedBlock | None
) -> tuple[str, str]:
    """Emit unknown block as TODO."""
    func_name = f"step_{block.parsed.index:04d}_unknown"
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
