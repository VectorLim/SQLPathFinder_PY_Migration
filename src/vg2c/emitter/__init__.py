from __future__ import annotations

from vg2c.dispatch.models import DispatchedProgram
from vg2c.emitter.models import EmitContext, EmittedScript, IndentWriter
from vg2c.emitter.readers import READER_SNIPPET
from vg2c.emitter.utilities_embed import (
    assemble_utility_snippets,
    register_utility_emission,
)
from vg2c.emitter.walker import walk_and_emit
from vg2c.frontend.models import Diagnostic

__all__ = ["emit"]


def emit(dispatched: DispatchedProgram) -> EmittedScript:
    """Stage 5 entry point: emit a Python script from DispatchedProgram.

    Args:
        dispatched: Output from Stage 4.

    Returns:
        An EmittedScript containing the generated Python source and merged diagnostics.
    """
    ctx = EmitContext()
    ctx.dispatch_map = {db.block_index: db for db in dispatched.dispatched}

    # Walk the scope tree and emit code
    functions, run_body, walker_diags = walk_and_emit(dispatched, ctx)

    # Always include ctx (PipelineContext) in the emitted script
    register_utility_emission(ctx, "ctx")

    # Assemble embedded utilities
    utility_imports, utility_sources = assemble_utility_snippets(ctx)

    # Merge utility imports into ctx.imports
    ctx.imports.update(utility_imports)

    # Assemble the final script
    script_writer = IndentWriter()

    # Header
    script_writer.write("# Auto-generated Python script from VG2")
    script_writer.write('"""Pipeline implementation."""')
    script_writer.write("")

    # Imports
    for imp in sorted(ctx.imports):
        script_writer.write(imp)
    script_writer.write("")

    # Embedded utilities
    for utility_source in utility_sources:
        script_writer.write_block(utility_source)
        script_writer.write("")

    # Embedded reader runtime (only when a reader handler asked for it)
    if ctx.needs_reader:
        script_writer.write_block(READER_SNIPPET)
        script_writer.write("")

    # Helper functions
    for func_code in functions:
        script_writer.write_block(func_code)
        script_writer.write("")

    # Main entry point
    script_writer.write("def run() -> None:")
    script_writer.push_indent()
    script_writer.write("ctx = PipelineContext()")
    script_writer.write_block(run_body)
    script_writer.pop_indent()

    # CLI hook
    script_writer.write("")
    script_writer.write('if __name__ == "__main__":')
    script_writer.push_indent()
    script_writer.write("run()")
    script_writer.pop_indent()

    source = script_writer.source()
    merged_diags = [*dispatched.diagnostics, *walker_diags]

    # Validate syntax
    try:
        import ast

        ast.parse(source)
    except SyntaxError as e:
        # Emit diagnostic but include source anyway (with error marked)
        diags = list(merged_diags)
        diags.append(
            Diagnostic(
                severity="error",
                code="emit-syntax-error",
                message=f"Generated script has syntax error at line {e.lineno}: {e.msg}",
            )
        )
        return EmittedScript(
            source=source, imports=tuple(ctx.imports), diagnostics=tuple(diags)
        )

    return EmittedScript(
        source=source, imports=tuple(ctx.imports), diagnostics=tuple(merged_diags)
    )
