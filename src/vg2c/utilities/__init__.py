"""Embeddable utility classes for generated scripts.

All concrete ``UtilitySpec`` subclasses are auto-registered via
``UtilitySpec.__init_subclass__`` and emitted into generated scripts.
"""

from __future__ import annotations

import ast
import importlib
import inspect
import sys
from pathlib import Path

from vg2c.utilities._base import EmitterUtility, UtilitySpec
from vg2c.utilities._topo_sort import topological_sort

# Concrete utility classes are imported lazily in assemble_all_utilities()
# to avoid circular imports (utilities→emitter→dispatch→dataflow loop).
# Only base classes are imported here.

_CONCRETE_UTILS_LOADED = False


def ensure_utility_checks_loaded() -> None:
    """Import concrete utility modules once to register check/emit handlers."""

    global _CONCRETE_UTILS_LOADED
    if _CONCRETE_UTILS_LOADED:
        return

    from vg2c.utilities.crosstab import CrosstabUtility  # noqa: F401
    from vg2c.utilities.csv_io import CsvIO  # noqa: F401
    from vg2c.utilities.html_report import HtmlReport  # noqa: F401
    from vg2c.utilities.python_embed import PythonEmbed  # noqa: F401
    from vg2c.utilities.fs_ops import FileSystemOps  # noqa: F401
    from vg2c.utilities.macro_state import MacroState  # noqa: F401
    from vg2c.utilities.external import ExternalProcess  # noqa: F401
    from vg2c.utilities.sqlite_engine import SqliteEngine  # noqa: F401
    from vg2c.utilities.generic import UnknownUtility  # noqa: F401
    from vg2c.utilities.mail import MailService  # noqa: F401
    from vg2c.utilities.pipeline_context import PipelineContext  # noqa: F401
    from vg2c.utilities.wait_file import WaitFile  # noqa: F401

    _CONCRETE_UTILS_LOADED = True


def _alias_text(alias: ast.alias) -> str:
    if alias.asname:
        return f"{alias.name} as {alias.asname}"
    return alias.name


def _scan_imports_and_dependencies(
    file_path: Path,
    *,
    current_name: str,
    module_to_name: dict[str, str],
) -> tuple[set[str], set[str], set[str]]:
    source = file_path.read_text(encoding="utf-8", errors="replace")
    tree = ast.parse(source, filename=str(file_path))

    external_imports: set[str] = set()
    dependencies: set[str] = set()
    helper_modules: set[str] = set()

    for node in tree.body:
        if isinstance(node, ast.Import):
            for alias in node.names:
                dep_name = module_to_name.get(alias.name)
                if dep_name is not None and dep_name != current_name:
                    dependencies.add(dep_name)
                    continue

                if alias.name.startswith("vg2c."):
                    if alias.name.startswith("vg2c.utilities._"):
                        helper_modules.add(alias.name)
                    continue

                external_imports.add(f"import {_alias_text(alias)}")

        elif isinstance(node, ast.ImportFrom):
            module = node.module or ""

            if node.level == 0:
                dep_name = module_to_name.get(module)
                if dep_name is not None and dep_name != current_name:
                    dependencies.add(dep_name)

            if module.startswith("vg2c.utilities._"):
                helper_modules.add(module)

            if node.level != 0:
                continue
            if not module or module.startswith("vg2c."):
                continue

            names = ", ".join(sorted(_alias_text(alias) for alias in node.names))
            external_imports.add(f"from {module} import {names}")

    return external_imports, dependencies, helper_modules


def _extract_module_body(file_path: Path) -> str:
    source = file_path.read_text(encoding="utf-8", errors="replace")
    tree = ast.parse(source, filename=str(file_path))

    chunks: list[str] = []
    for node in tree.body:
        if isinstance(node, ast.Assign):
            target_names = {
                target.id for target in node.targets if isinstance(target, ast.Name)
            }
            if "__all__" in target_names:
                continue

        if isinstance(node, (ast.Assign, ast.AnnAssign, ast.ClassDef, ast.FunctionDef)):
            segment = ast.get_source_segment(source, node)
            if segment:
                chunks.append(segment.rstrip())

    return "\n\n".join(chunks).rstrip()


def _resolve_helper_file(module_name: str) -> Path:
    module = importlib.import_module(module_name)
    return Path(inspect.getfile(module))


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
    """Assemble all registered utilities for code emission.

    Lazy-imports concrete utilities to avoid circular dependencies.
    Called after emitter.models is available.
    """
    ensure_utility_checks_loaded()

    utilities = dict(UtilitySpec._registry)
    module_to_name = {cls.__module__: name for name, cls in utilities.items()}

    dependency_edges: dict[str, set[str]] = {name: set() for name in utilities}
    external_imports: set[str] = set()
    helper_modules_used: set[str] = set()

    for name, cls in utilities.items():
        file_path = Path(inspect.getfile(cls))
        imports, deps, helper_modules = _scan_imports_and_dependencies(
            file_path,
            current_name=name,
            module_to_name=module_to_name,
        )
        dependency_edges[name].update(dep for dep in deps if dep in utilities)
        external_imports.update(imports)
        helper_modules_used.update(helper_modules)

    helper_edges: dict[str, set[str]] = {name: set() for name in helper_modules_used}
    helper_imports: set[str] = set()
    helper_bodies: dict[str, str] = {}

    for helper_module in helper_modules_used:
        helper_path = _resolve_helper_file(helper_module)
        imports, _, nested_helpers = _scan_imports_and_dependencies(
            helper_path,
            current_name=helper_module,
            module_to_name={},
        )
        helper_imports.update(imports)
        helper_edges[helper_module].update(
            mod for mod in nested_helpers if mod in helper_modules_used
        )
        helper_bodies[helper_module] = _extract_module_body(helper_path)

    helper_order = topological_sort(
        {name: object() for name in helper_modules_used},
        helper_edges,
    )
    helper_sources = [
        helper_bodies[name] for name in helper_order if helper_bodies[name]
    ]

    ordered_names = topological_sort(utilities, dependency_edges)
    sources = helper_sources + [utilities[name].get_source() for name in ordered_names]
    external_imports.update(helper_imports)
    imports = _render_grouped_imports(external_imports)

    return imports, sources


__all__ = [
    "ensure_utility_checks_loaded",
    "assemble_all_utilities",
    "EmitterUtility",
    "CrosstabUtility",
    "CsvIO",
    "ExternalProcess",
    "FileSystemOps",
    "UnknownUtility",
    "HtmlReport",
    "MacroState",
    "MailService",
    "PipelineContext",
    "SqliteEngine",
    "PythonEmbed",
    "WaitFile",
]
