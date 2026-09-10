from __future__ import annotations

import ast
from dataclasses import replace
from typing import TYPE_CHECKING

from vg2c.emitter.globals import GlobalValues, render_globals
from vg2c.emitter.indent_writer import IndentWriter
from vg2c.emitter.models import EmittedScript, SourceRange, finalize_steps

if TYPE_CHECKING:
    from vg2c.dispatch.models import DispatchedBlock, DispatchedProgram, ReaderSpec

DEPENDENCIES_END = "# <vg2c:dependencies:end>"
STEPS_START = "# <vg2c:steps:start>"
STEPS_END = "# <vg2c:steps:end>"
WORKFLOW_START = "# <vg2c:workflow:start>"
WORKFLOW_END = "# <vg2c:workflow:end>"


def _first_literal_site(dispatched: DispatchedProgram) -> str:
    for block in dispatched.dispatched:
        if block.reader_target.site:
            return block.reader_target.site
    return ""


def _reader_import_or_root(reader: ReaderSpec) -> tuple[str | None, str | None]:
    if reader.utility_name is not None:
        return None, reader.utility_name
    if reader.module.startswith("vg2c."):
        raise ValueError(
            f"{reader.module}.{reader.name} is project-local but has no utility_name, "
            "so emitting it would leak a vg2c import into the generated script."
        )
    return f"from {reader.module} import {reader.name}", None


def _resolve_reader_imports_and_roots(
    dispatched: tuple[DispatchedBlock, ...],
) -> tuple[set[str], set[str]]:
    reader_imports: set[str] = set()
    forced_utility_names: set[str] = set()
    for reader in {block.reader for block in dispatched}:
        imp, forced_name = _reader_import_or_root(reader)
        if imp is not None:
            reader_imports.add(imp)
        if forced_name is not None:
            forced_utility_names.add(forced_name)
    return reader_imports, forced_utility_names


def emit(dispatched: DispatchedProgram) -> EmittedScript:
    """Stage 5: emit Python and the edit/semantic manifest at the same time."""
    from vg2c.emitter.walker import walk_and_emit
    from vg2c.logger import Logger
    from vg2c.utilities import assemble_utilities
    from vg2c.utilities._symbol_index import bound_names

    log = Logger.getLogger("vg2c.emitter")
    reader_imports, forced_utility_names = _resolve_reader_imports_and_roots(dispatched.dispatched)
    collected: GlobalValues = {}
    step_emissions, run_body = walk_and_emit(dispatched, collected=collected)
    default_site = _first_literal_site(dispatched)
    setup = ["Logger.basicConfig(level=Logger.INFO)", "OracleClient.configure()"]
    if default_site:
        run_body = f'ctx.macro.set_named("NODE", {default_site!r})\n' + run_body
    workflow_source = "def run(ctx):\n" + "\n".join(
        "    " + line for line in [*setup, *run_body.splitlines()]
    )
    embedded = assemble_utilities(
        step_emissions=step_emissions,
        workflow_source=workflow_source,
        reader_names=forced_utility_names,
        reader_imports=reader_imports,
    )
    imports = set(embedded.imports)
    reserved = bound_names(ast.parse("\n".join([*imports, *embedded.sources])).body).names
    if reserved.intersection(name for group in collected.values() for name in group):
        # Only names change; runtime dependencies and workflow call sites stay identical.
        collected.clear()
        step_emissions, _ = walk_and_emit(dispatched, collected=collected, reserved=reserved)
    used_globals = {
        name
        for step in step_emissions
        for invocation in step.invocations
        for argument in invocation.arguments
        for name in argument.global_names
    }

    script_writer = IndentWriter()
    script_writer.write("# Auto-generated Python script from VG2")
    script_writer.write('"""Pipeline implementation."""')
    script_writer.write("")

    for imp in sorted(imports):
        script_writer.write(imp)
    script_writer.write("")

    globals_source, global_parameters = render_globals(
        collected, used_globals, len(script_writer.source()) + 1
    )
    if globals_source:
        script_writer.write_block(globals_source)
        script_writer.write("")

    for utility_source in embedded.sources:
        script_writer.write_block(utility_source)
        script_writer.write("")

    script_writer.write(DEPENDENCIES_END)
    script_writer.write(STEPS_START)
    for step in step_emissions:
        script_writer.write_block(step.source)
        script_writer.write("")
    script_writer.write(STEPS_END)
    script_writer.write("")

    script_writer.write(WORKFLOW_START)
    script_writer.write("def run() -> None:")
    script_writer.push_indent()
    for line in setup:
        script_writer.write(line)
    script_writer.write(f"ctx = {embedded.context_expression}")
    script_writer.write_block(run_body)
    script_writer.pop_indent()
    script_writer.write(WORKFLOW_END)

    script_writer.write("")
    script_writer.write('if __name__ == "__main__":')
    script_writer.push_indent()
    script_writer.write("run()")
    script_writer.pop_indent()

    # SQL-filter comments are part of normal final assembly. The first writer
    # gives us the original step line numbers; the final writer owns the exact
    # source that is parsed, returned, and used to finalize emitted spans.
    body_source = script_writer.source()
    comment_lines = _sql_filter_comment_lines(dispatched, script_writer.step_lines)
    if comment_lines:
        final_writer = IndentWriter()
        for line in comment_lines:
            final_writer.write(line)
        final_writer.write("")
        final_writer.write_block(body_source)
        source = final_writer.source()
    else:
        source = body_source

    try:
        ast.parse(source)
    except SyntaxError as exc:
        log.error(
            f"[emit-syntax-error] <generated_script>:{exc.lineno}:1: "
            f"Generated script has syntax error at line {exc.lineno}: {exc.msg}"
        )

    prefix_length = len(source) - len(body_source)
    global_parameters = {
        name: replace(
            parameter,
            source_range=SourceRange(
                parameter.source_range.start_offset + prefix_length,
                parameter.source_range.end_offset + prefix_length,
            ),
        )
        for name, parameter in global_parameters.items()
    }
    steps = finalize_steps(source, step_emissions, global_parameters)
    return EmittedScript(source=source, imports=tuple(sorted(imports)), steps=steps)


def _sql_filter_comment_lines(
    dispatched: DispatchedProgram, step_lines: dict[str, int]
) -> tuple[str, ...]:
    steps_with_filters = [db for db in dispatched.dispatched if db.sql_filters]
    if not steps_with_filters:
        return ()

    steps_with_filters.sort(key=lambda db: step_lines.get(db.step_name, 0))
    num_comment_lines = len(steps_with_filters) + 2
    comment_lines = ["# SQL statements containing filters:"]
    for db in steps_with_filters:
        orig_line = step_lines.get(db.step_name, 1)
        final_line = orig_line + num_comment_lines
        attrs = sorted({attr for item in db.sql_filters for attr in item.attributes})
        comment_lines.append(
            f"# - {db.step_name} (Line {final_line}): filters on {', '.join(attrs)}"
        )

    return tuple(comment_lines)
