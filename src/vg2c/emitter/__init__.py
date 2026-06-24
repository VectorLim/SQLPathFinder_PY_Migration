from __future__ import annotations

from vg2c.dispatch.models import DispatchedProgram
from vg2c.emitter.macro_subst import MacroSubstituter
from vg2c.emitter.models import EmitContext, EmittedScript, IndentWriter
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
    ctx.macro_subst = MacroSubstituter()
    ctx.dispatch_map = {db.block_index: db for db in dispatched.dispatched}

    # Add standard imports
    ctx.add_import("vg2c_runtime", "ctx as pipeline_ctx")

    # Walk the scope tree and emit code
    functions, run_body = walk_and_emit(dispatched, ctx)

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

    # Helper functions
    for func_code in functions:
        script_writer.write_block(func_code)
        script_writer.write("")

    # Main entry point
    script_writer.write("def run() -> None:")
    script_writer.push_indent()
    script_writer.write("ctx = pipeline_ctx")
    script_writer.write_block(run_body)
    script_writer.pop_indent()

    # CLI hook
    script_writer.write("")
    script_writer.write('if __name__ == "__main__":')
    script_writer.push_indent()
    script_writer.write("run()")
    script_writer.pop_indent()

    source = script_writer.source()

    # Validate syntax
    try:
        import ast

        ast.parse(source)
    except SyntaxError as e:
        # Emit diagnostic but include source anyway (with error marked)
        diags = list(dispatched.diagnostics)
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
        source=source, imports=tuple(ctx.imports), diagnostics=dispatched.diagnostics
    )
