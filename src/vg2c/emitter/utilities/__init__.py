"""Embeddable utility classes for generated scripts.

All concrete ``UtilitySpec`` subclasses are auto-registered via
``UtilitySpec.__init_subclass__`` and emitted into generated scripts.
"""

from __future__ import annotations

import ast
import inspect
import sys
from pathlib import Path

from vg2c.emitter.utilities._base import UtilitySpec
from vg2c.emitter.utilities._emit_helpers import emit_block
from vg2c.emitter.utilities._topo_sort import topological_sort
from vg2c.emitter.utilities.crosstab import CrosstabUtility
from vg2c.emitter.utilities.csv_io import CsvIO
from vg2c.emitter.utilities.external import ExternalProcess
from vg2c.emitter.utilities.fs_ops import FileSystemOps
from vg2c.emitter.utilities.macro_state import MacroState
from vg2c.emitter.utilities.mail import MailService
from vg2c.emitter.utilities.pipeline_context import PipelineContext
from vg2c.emitter.utilities.sql_macros import SqlMacros
from vg2c.emitter.utilities.sqlite_engine import SqliteEngine
from vg2c.emitter.readers import ReaderRuntime


def _alias_text(alias: ast.alias) -> str:
    if alias.asname:
        return f"{alias.name} as {alias.asname}"
    return alias.name


def _scan_imports_and_dependencies(
    file_path: Path,
    *,
    current_name: str,
    module_to_name: dict[str, str],
) -> tuple[set[str], set[str]]:
    source = file_path.read_text(encoding="utf-8", errors="replace")
    tree = ast.parse(source, filename=str(file_path))

    external_imports: set[str] = set()
    dependencies: set[str] = set()

    for node in tree.body:
        if isinstance(node, ast.Import):
            for alias in node.names:
                dep_name = module_to_name.get(alias.name)
                if dep_name is not None and dep_name != current_name:
                    dependencies.add(dep_name)
                    continue

                if alias.name.startswith("vg2c."):
                    continue

                external_imports.add(f"import {_alias_text(alias)}")

        elif isinstance(node, ast.ImportFrom):
            module = node.module or ""

            if node.level == 0:
                dep_name = module_to_name.get(module)
                if dep_name is not None and dep_name != current_name:
                    dependencies.add(dep_name)

            if node.level != 0:
                continue
            if not module or module.startswith("vg2c."):
                continue

            names = ", ".join(sorted(_alias_text(alias) for alias in node.names))
            external_imports.add(f"from {module} import {names}")

    return external_imports, dependencies


def _import_root(line: str) -> str:
    if line.startswith("import "):
        mod = line[len("import ") :].split(" as ", 1)[0]
    else:
        mod = line.split()[1]
    return mod.split(".", 1)[0]


def _render_grouped_imports(import_lines: set[str]) -> list[str]:
    stdlib: list[str] = []
    third_party: list[str] = []
    local: list[str] = []

    for line in sorted(import_lines):
        root = _import_root(line)
        if root == "vg2c":
            local.append(line)
        elif root == "__future__" or root in sys.stdlib_module_names:
            stdlib.append(line)
        else:
            third_party.append(line)

    grouped = [sorted(stdlib), sorted(third_party), sorted(local)]

    rendered: list[str] = []
    for group in grouped:
        if not group:
            continue
        if rendered:
            rendered.append("")
        rendered.extend(group)

    return rendered


def assemble_all_utilities() -> tuple[list[str], list[str]]:
    utilities = dict(UtilitySpec._registry)
    module_to_name = {cls.__module__: name for name, cls in utilities.items()}

    dependency_edges: dict[str, set[str]] = {name: set() for name in utilities}
    external_imports: set[str] = set()

    for name, cls in utilities.items():
        file_path = Path(inspect.getfile(cls))
        imports, deps = _scan_imports_and_dependencies(
            file_path,
            current_name=name,
            module_to_name=module_to_name,
        )
        dependency_edges[name].update(dep for dep in deps if dep in utilities)
        external_imports.update(imports)

    ordered_names = topological_sort(utilities, dependency_edges)
    sources = [utilities[name].get_source() for name in ordered_names]
    imports = _render_grouped_imports(external_imports)

    return imports, sources


__all__ = [
    "assemble_all_utilities",
    "emit_block",
    "CrosstabUtility",
    "CsvIO",
    "ExternalProcess",
    "FileSystemOps",
    "MacroState",
    "MailService",
    "PipelineContext",
    "ReaderRuntime",
    "SqlMacros",
    "SqliteEngine",
]
