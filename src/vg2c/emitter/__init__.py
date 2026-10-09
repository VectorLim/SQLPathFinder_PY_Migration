from __future__ import annotations

from typing import TYPE_CHECKING

from vg2c.emitter.models import EmittedScript

if TYPE_CHECKING:
    from vg2c.dispatch.models import DispatchedBlock, DispatchedProgram, ReaderSpec

DEPENDENCIES_END = "# <vg2c:dependencies:end>"
STEPS_START = "# <vg2c:steps:start>"
STEPS_END = "# <vg2c:steps:end>"
WORKFLOW_START = "# <vg2c:workflow:start>"
WORKFLOW_END = "# <vg2c:workflow:end>"


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
    """Emit native Python and editable assets from the single existing scope tree."""
    from vg2c.emitter.project import emit_project

    return emit_project(dispatched)


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
