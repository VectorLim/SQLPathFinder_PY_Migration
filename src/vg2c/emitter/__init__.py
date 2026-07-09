from __future__ import annotations

from vg2c.dispatch.models import DispatchedProgram
from vg2c.emitter.models import EmitContext, EmittedScript, IndentWriter
from vg2c.emitter.utilities import assemble_all_utilities
from vg2c.emitter.walker import walk_and_emit
from vg2c.frontend.models import Diagnostic


def emit(dispatched: DispatchedProgram) -> EmittedScript:
    """Stage 5 entry point: emit a Python script from DispatchedProgram.

    Args:
        dispatched: Output from Stage 4.

    Returns:
        An EmittedScript containing the generated Python source and merged diagnostics.
    """
    ctx = EmitContext()
    ctx.dispatch_map = {db.index: db for db in dispatched.dispatched}

    # Walk the scope tree and emit code
    functions, run_body, walker_diags = walk_and_emit(dispatched, ctx)

    # Assemble embedded utility classes.
    utility_imports, utility_sources = assemble_all_utilities()

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
    
    # Run filter post-processing comments
    source = post_process_comments(source, dispatched, script_writer.step_lines)

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


def post_process_comments(
    source: str,
    dispatched: DispatchedProgram,
    step_lines: dict[str, int]
) -> str:
    # Find all steps that have filters
    steps_with_filters = []
    for db in dispatched.dispatched:
        if db.sql_filters:
            steps_with_filters.append(db)
            
    if not steps_with_filters:
        return source
        
    # We will prepend comments.
    # To keep line numbers in the comments accurate, we need to sort steps by their original line number
    steps_with_filters.sort(key=lambda db: step_lines.get(db.step_name, 0))
    
    # Prepend comment block:
    # 1 line for header
    # len(steps_with_filters) lines for the details
    # 1 line for blank line separator
    num_comment_lines = len(steps_with_filters) + 2
    
    comment_lines = ["# SQL statements containing filters:"]
    for db in steps_with_filters:
        orig_line = step_lines.get(db.step_name, 1)
        final_line = orig_line + num_comment_lines
        
        # Merge all attributes from all filters in this block
        attrs = sorted(list(set(attr for f in db.sql_filters for attr in f.attributes)))
        attrs_str = ", ".join(attrs)
        comment_lines.append(f"# - {db.step_name} (Line {final_line}): filters on {attrs_str}")
        
    comments_block = "\n".join(comment_lines) + "\n\n"
    return comments_block + source
