"""Embeddable utility classes for generated scripts.

All concrete ``UtilitySpec`` subclasses are auto-registered via
``UtilitySpec.__init_subclass__`` and emitted into generated scripts.
"""

from __future__ import annotations

import ast
import importlib
import inspect
import logging
import sys
from dataclasses import dataclass, field
from pathlib import Path

from vg2c.kind import Kind
from vg2c.utilities._base import EmitterUtility, UtilitySpec
from vg2c.utilities._topo_sort import topological_sort

# Concrete utility classes are imported lazily in assemble_all_utilities()
# to avoid circular imports (utilities→emitter→dispatch→dataflow loop).
# Only base classes are imported here.

_CONCRETE_UTILS_LOADED = False
log = logging.getLogger("vg2c.utilities")


def ensure_utility_checks_loaded() -> None:
    """Import concrete utility modules once to register check/emit handlers."""

    global _CONCRETE_UTILS_LOADED
    if _CONCRETE_UTILS_LOADED:
        return

    from vg2c.logger import Logger  # noqa: F401
    from vg2c.utilities.crosstab import CrosstabUtility  # noqa: F401
    from vg2c.utilities.csv_io import CsvIO  # noqa: F401
    from vg2c.utilities.html_report import HtmlReport  # noqa: F401
    from vg2c.utilities.python_embed import PythonEmbed  # noqa: F401
    from vg2c.utilities.fs_ops import FileSystemOps  # noqa: F401
    from vg2c.utilities.rows_in_file import RowsInFile  # noqa: F401
    from vg2c.utilities.macro_state import MacroState  # noqa: F401
    from vg2c.utilities.external import ExternalProcess  # noqa: F401
    from vg2c.utilities.sqlite_engine import SqliteEngine  # noqa: F401
    from vg2c.utilities.generic import UnknownUtility  # noqa: F401
    from vg2c.utilities.mail import MailService  # noqa: F401
    from vg2c.utilities.pipeline_context import PipelineContext  # noqa: F401
    from vg2c.utilities.wait_file import WaitFile  # noqa: F401

    # SqliteReader is a plain project-local class, not a Kind handler -- it must be
    # a registered UtilitySpec so it gets embedded (not live-imported) like every
    # other project-local class, matching Aries/Mars/Oracle reader parity.
    from vg2c.dispatch.dialects.sqlite import SqliteReader  # noqa: F401

    _CONCRETE_UTILS_LOADED = True
    log.debug("Loaded concrete utility modules for check/emit registration.")


def _alias_text(alias: ast.alias) -> str:
    if alias.asname:
        return f"{alias.name} as {alias.asname}"
    return alias.name


@dataclass
class UtilityDependencyInfo:
    """Structured module -> imports/dependencies mapping for one source file."""

    external_imports: set[str] = field(default_factory=set)
    dependencies: set[str] = field(default_factory=set)
    helper_modules: set[str] = field(default_factory=set)
    cleaned_source: str = ""


def _find_promotable_function_imports(
    tree: ast.Module,
) -> list[ast.Import | ast.ImportFrom]:
    """Return import statements sitting directly inside a function/method body.

    Imports nested inside a ``try``/``except`` are left untouched -- those are
    almost always conditional/optional imports (e.g. an ``ImportError`` fallback)
    where promoting or removing them would change behavior.
    """
    promotable: list[ast.Import | ast.ImportFrom] = []

    def walk_body(stmts: list[ast.stmt]) -> None:
        for stmt in stmts:
            if isinstance(stmt, (ast.Import, ast.ImportFrom)):
                promotable.append(stmt)
            elif isinstance(stmt, ast.Try):
                continue
            elif isinstance(stmt, (ast.If, ast.For, ast.While, ast.With)):
                walk_body(stmt.body)
                walk_body(list(getattr(stmt, "orelse", ())))

    for node in ast.walk(tree):
        if isinstance(node, (ast.FunctionDef, ast.AsyncFunctionDef)):
            walk_body(node.body)

    return promotable


def _strip_import_lines(source: str, nodes: list[ast.Import | ast.ImportFrom]) -> str:
    """Replace each import statement's source lines with a no-op ``pass``."""
    lines = source.splitlines(keepends=True)
    for node in nodes:
        start_idx = node.lineno - 1
        end_idx = (node.end_lineno or node.lineno) - 1
        lines[start_idx] = f"{' ' * node.col_offset}pass\n"
        for idx in range(start_idx + 1, end_idx + 1):
            lines[idx] = "\n"
    return "".join(lines)


def _classify_import(
    node: ast.Import | ast.ImportFrom,
    *,
    current_name: str,
    module_to_name: dict[str, str],
    info: UtilityDependencyInfo,
) -> None:
    if isinstance(node, ast.Import):
        for alias in node.names:
            dep_name = module_to_name.get(alias.name)
            if dep_name is not None and dep_name != current_name:
                info.dependencies.add(dep_name)
                continue

            if alias.name.startswith("vg2c."):
                if alias.name.startswith("vg2c.utilities._"):
                    info.helper_modules.add(alias.name)
                continue

            info.external_imports.add(f"import {_alias_text(alias)}")
        return

    module = node.module or ""

    if node.level == 0:
        dep_name = module_to_name.get(module)
        if dep_name is not None and dep_name != current_name:
            info.dependencies.add(dep_name)

    if module.startswith("vg2c.utilities._"):
        info.helper_modules.add(module)

    if node.level != 0:
        return
    if not module or module.startswith("vg2c."):
        return

    names = ", ".join(sorted(_alias_text(alias) for alias in node.names))
    info.external_imports.add(f"from {module} import {names}")


def _scan_imports_and_dependencies(
    file_path: Path,
    *,
    current_name: str,
    module_to_name: dict[str, str],
) -> UtilityDependencyInfo:
    source = file_path.read_text(encoding="utf-8", errors="replace")
    tree = ast.parse(source, filename=str(file_path))

    info = UtilityDependencyInfo()

    for node in tree.body:
        if isinstance(node, (ast.Import, ast.ImportFrom)):
            _classify_import(
                node,
                current_name=current_name,
                module_to_name=module_to_name,
                info=info,
            )

    # Imports nested inside methods/functions are resolved the same way as
    # top-level ones (promoted to module-level imports / local dependencies /
    # helper-module references) and then stripped from the embedded source --
    # they must never survive verbatim inside an embedded method body.
    promotable = _find_promotable_function_imports(tree)
    for node in promotable:
        _classify_import(
            node, current_name=current_name, module_to_name=module_to_name, info=info
        )

    info.cleaned_source = (
        _strip_import_lines(source, promotable) if promotable else source
    )
    return info


def _extract_module_body(source: str) -> str:
    tree = ast.parse(source)

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


def assemble_all_utilities(
    required_kinds: frozenset[Kind] | None = None,
    extra_root_names: frozenset[str] = frozenset(),
) -> tuple[list[str], list[str]]:
    """Assemble utilities required for code emission.

    Lazy-imports concrete utilities to avoid circular dependencies.
    Called after emitter.models is available.

    Args:
        required_kinds: the set of block ``Kind``s actually present in the
            workflow being compiled. When ``None`` (default), every registered
            utility is embedded -- preserves prior behavior for callers that
            don't have dispatch metadata available. When given, only utilities
            reachable from the fixed ``always_include`` roots (PipelineContext,
            Logger) plus whichever utility ``UtilitySpec.for_kind()`` maps each
            kind to are embedded, following the existing dependency graph.
        extra_root_names: additional ``utility_name``s to force-include, e.g. a
            project-local reader class (such as SqliteReader) referenced by a
            dispatched block's ``reader_cls``.
    """
    ensure_utility_checks_loaded()

    utilities = dict(UtilitySpec._registry)
    module_to_name = {cls.__module__: name for name, cls in utilities.items()}

    dependency_edges: dict[str, set[str]] = {name: set() for name in utilities}
    per_utility_imports: dict[str, set[str]] = {}
    per_utility_helpers: dict[str, set[str]] = {}
    cleaned_sources: dict[str, str] = {}

    for name, cls in utilities.items():
        file_path = Path(inspect.getfile(cls))
        info = _scan_imports_and_dependencies(
            file_path,
            current_name=name,
            module_to_name=module_to_name,
        )
        dependency_edges[name].update(
            dep for dep in info.dependencies if dep in utilities
        )
        per_utility_imports[name] = info.external_imports
        per_utility_helpers[name] = info.helper_modules
        cleaned_sources[name] = info.cleaned_source

    if required_kinds is not None:
        root_names = {
            name
            for name, cls in utilities.items()
            if getattr(cls, "always_include", False)
        }
        for kind in required_kinds:
            handler = UtilitySpec.for_kind(kind)
            if handler is not None:
                root_names.add(handler.utility_name)
        root_names |= {name for name in extra_root_names if name in utilities}

        reachable: set[str] = set()
        stack = list(root_names)
        while stack:
            current = stack.pop()
            if current in reachable or current not in utilities:
                continue
            reachable.add(current)
            stack.extend(dependency_edges.get(current, ()))

        utilities = {name: cls for name, cls in utilities.items() if name in reachable}
        dependency_edges = {
            name: {dep for dep in deps if dep in utilities}
            for name, deps in dependency_edges.items()
            if name in utilities
        }

    external_imports: set[str] = set()
    helper_modules_used: set[str] = set()
    for name in utilities:
        external_imports.update(per_utility_imports[name])
        helper_modules_used.update(per_utility_helpers[name])

    helper_edges: dict[str, set[str]] = {name: set() for name in helper_modules_used}
    helper_imports: set[str] = set()
    helper_bodies: dict[str, str] = {}

    for helper_module in helper_modules_used:
        helper_path = _resolve_helper_file(helper_module)
        info = _scan_imports_and_dependencies(
            helper_path,
            current_name=helper_module,
            module_to_name={},
        )
        helper_imports.update(info.external_imports)
        helper_edges[helper_module].update(
            mod for mod in info.helper_modules if mod in helper_modules_used
        )
        helper_bodies[helper_module] = _extract_module_body(info.cleaned_source)

    helper_order = topological_sort(
        {name: object() for name in helper_modules_used},
        helper_edges,
    )
    helper_sources = [
        helper_bodies[name] for name in helper_order if helper_bodies[name]
    ]

    ordered_names = topological_sort(utilities, dependency_edges)
    if "logger" in ordered_names:
        ordered_names = [
            "logger",
            *(name for name in ordered_names if name != "logger"),
        ]
    elif required_kinds is None:
        log.warning(
            "Logger utility not registered; generated script will not embed Logger class."
        )

    sources = helper_sources + [
        utilities[name].get_source(source_override=cleaned_sources.get(name))
        for name in ordered_names
    ]
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
