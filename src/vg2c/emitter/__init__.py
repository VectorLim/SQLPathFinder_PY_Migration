import inspect

from vg2c import kind as kind_module
from vg2c.dispatch.models import DispatchedProgram
from vg2c.emitter.indent_writer import IndentWriter
from vg2c.emitter.models import EmittedScript
from vg2c.emitter.walker import walk_and_emit
from vg2c.logger import Logger
from vg2c.utilities import assemble_all_utilities

log = Logger.getLogger("vg2c.emitter")

DEPENDENCIES_END = "# <vg2c:dependencies:end>"
STEPS_START = "# <vg2c:steps:start>"
STEPS_END = "# <vg2c:steps:end>"
WORKFLOW_START = "# <vg2c:workflow:start>"
WORKFLOW_END = "# <vg2c:workflow:end>"


def _first_literal_site(dispatched: DispatchedProgram) -> str:
    """Return the first block's literal /NODE site (e.g. "KM"), or "" if none is known."""
    for block in dispatched.dispatched:
        if block.reader_target.site:
            return block.reader_target.site
    return ""


def emit(dispatched: DispatchedProgram) -> EmittedScript:
    """Stage 5 entry point: emit a Python script from DispatchedProgram.

    Args:
        dispatched: Output from Stage 4.

    Returns:
        An EmittedScript containing the generated Python source.
    """
    # Load/register utility classes first so UtilitySpec dispatch handlers exist
    # before walk_and_emit() visits leaf blocks.
    utility_imports, utility_sources = assemble_all_utilities()
    log.debug(
        "Assembled %d utility sources and %d utility imports.",
        len(utility_sources),
        len(utility_imports),
    )

    # Walk the scope tree and emit code.
    functions, run_body = walk_and_emit(dispatched)
    log.debug("Walker emitted %d helper functions.", len(functions))

    # Get kind.py source (strip all imports)
    kind_source = "\n".join(
        line
        for line in inspect.getsource(kind_module).splitlines()
        if not line.startswith(("import ", "from "))
    )

    imports = set(utility_imports)
    imports.add("from enum import Enum")
    imports.add("from datasyncx.readers.aries_reader import AriesReader")
    imports.add("from datasyncx.readers.mars_reader import MarsReader")
    imports.add("from vg2c.dispatch.dialects.sqlite import SqliteReader")

    # Assemble the final script
    script_writer = IndentWriter()

    # Header
    script_writer.write("# Auto-generated Python script from VG2")
    script_writer.write('"""Pipeline implementation."""')
    script_writer.write("")

    # Imports
    for imp in sorted(imports):
        script_writer.write(imp)
    script_writer.write("")

    # Embedded kind module (near top so utilities can reference Kind)
    script_writer.write_block(kind_source)
    script_writer.write("")

    # Embedded utilities
    for utility_source in utility_sources:
        script_writer.write_block(utility_source)
        script_writer.write("")

    # Helper functions
    script_writer.write(DEPENDENCIES_END)
    script_writer.write(STEPS_START)
    for func_code in functions:
        script_writer.write_block(func_code)
        script_writer.write("")
    script_writer.write(STEPS_END)
    script_writer.write("")

    # Main entry point
    script_writer.write(WORKFLOW_START)
    script_writer.write("def run() -> None:")
    script_writer.push_indent()
    script_writer.write("ctx = PipelineContext()")
    default_site = _first_literal_site(dispatched)
    if default_site:
        script_writer.write(f'ctx.macro.set_named("NODE", {default_site!r})')
    script_writer.write_block(run_body)
    script_writer.pop_indent()
    script_writer.write(WORKFLOW_END)

    # CLI hook
    script_writer.write("")
    script_writer.write('if __name__ == "__main__":')
    script_writer.push_indent()
    script_writer.write("run()")
    script_writer.pop_indent()

    source = script_writer.source()

    # Run filter post-processing comments
    source = post_process_comments(source, dispatched, script_writer.step_lines)

    # Validate syntax
    try:
        import ast

        ast.parse(source)
    except SyntaxError as e:
        log.error(
            f"[emit-syntax-error] <generated_script>:{e.lineno}:1: "
            f"Generated script has syntax error at line {e.lineno}: {e.msg}"
        )
        return EmittedScript(source=source, imports=tuple(imports))

    return EmittedScript(source=source, imports=tuple(imports))


def post_process_comments(
    source: str, dispatched: DispatchedProgram, step_lines: dict[str, int]
) -> str:
    # Find all steps that have filters
    steps_with_filters = []
    for db in dispatched.dispatched:
        if db.sql_filters:
            steps_with_filters.append(db)

    if not steps_with_filters:
        return source

    # We will prepend comments.
    # Sort by original line so prepended comment offsets stay accurate.
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
        comment_lines.append(
            f"# - {db.step_name} (Line {final_line}): filters on {attrs_str}"
        )

    comments_block = "\n".join(comment_lines) + "\n\n"
    return comments_block + source
