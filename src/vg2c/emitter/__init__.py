import inspect

from vg2c import kind as kind_module
from vg2c.dispatch.models import DispatchedBlock, DispatchedProgram
from vg2c.emitter.indent_writer import IndentWriter
from vg2c.emitter.models import EmittedScript
from vg2c.emitter.walker import walk_and_emit
from vg2c.logger import Logger
from vg2c.utilities import assemble_all_utilities
from vg2c.utilities._base import UtilitySpec

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


def _reader_import_or_root(reader_cls: type) -> tuple[str | None, str | None]:
    """Resolve how a dispatched block's reader class should reach the generated script.

    Third-party readers (e.g. datasyncx) get a plain import line. Project-local
    readers (e.g. SqliteReader) must be registered ``UtilitySpec``s so they are
    embedded like every other local class -- never live-imported from ``vg2c``.
    """
    if reader_cls.__module__.startswith("vg2c."):
        if not issubclass(reader_cls, UtilitySpec):
            raise ValueError(
                f"{reader_cls.__module__}.{reader_cls.__name__} is a project-local "
                "reader class but is not a registered UtilitySpec, so it cannot be "
                "embedded without leaking a vg2c import into the generated script."
            )
        return None, reader_cls.utility_name
    return f"from {reader_cls.__module__} import {reader_cls.__name__}", None


def _resolve_reader_imports_and_roots(
    dispatched: tuple[DispatchedBlock, ...],
) -> tuple[set[str], set[str]]:
    """Return (import_lines, forced_utility_names) for the reader classes actually used."""
    reader_imports: set[str] = set()
    forced_utility_names: set[str] = set()
    for reader_cls in {block.reader_cls for block in dispatched}:
        imp, forced_name = _reader_import_or_root(reader_cls)
        if imp is not None:
            reader_imports.add(imp)
        if forced_name is not None:
            forced_utility_names.add(forced_name)
    return reader_imports, forced_utility_names


def emit(dispatched: DispatchedProgram) -> EmittedScript:
    """Stage 5 entry point: emit a Python script from DispatchedProgram.

    Args:
        dispatched: Output from Stage 4.

    Returns:
        An EmittedScript containing the generated Python source.
    """
    # Reader classes actually used by this workflow (Oracle/Aries/Mars/SQLite/...)
    # drive both the reader import lines and which reader utilities get embedded --
    # no per-dialect special casing.
    reader_imports, forced_utility_names = _resolve_reader_imports_and_roots(
        dispatched.dispatched
    )
    required_kinds = frozenset(b.kind for b in dispatched.analyzed.resolved.blocks)

    # Load/register utility classes first so UtilitySpec dispatch handlers exist
    # before walk_and_emit() visits leaf blocks. Only utilities reachable from the
    # workflow's actual block Kinds (plus always-included roots) are embedded.
    utility_imports, utility_sources = assemble_all_utilities(
        required_kinds=required_kinds,
        extra_root_names=frozenset(forced_utility_names),
    )
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
    imports.update(reader_imports)

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
    script_writer.write("OracleClient.configure()")
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
