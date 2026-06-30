"""Utilities embedding logic for emitted scripts."""

from __future__ import annotations

import inspect
import re
from pathlib import Path

from vg2c.emitter.models import EmitContext
from vg2c.emitter.utilities import UTILITIES, UTILITY_IMPORTS

__all__ = ["register_utility_emission", "assemble_utility_snippets"]


# Dependency map: utility_name -> set of other utilities it needs
_UTILITY_DEPENDENCIES: dict[str, set[str]] = {
    "ctx": {"macro", "csv_io", "sqlite_engine", "sql_macros", "fs_ops", "mail", "external"},
}


def register_utility_emission(ctx: EmitContext, *names: str) -> None:
    """Mark one or more utilities as needed in the emitted script.

    Automatically resolves transitive dependencies (e.g., registering "ctx"
    also registers all utilities it instantiates).
    """
    for name in names:
        if name in ctx.needed_utilities:
            continue
        ctx.needed_utilities.add(name)
        # Pull in dependencies
        for dep in _UTILITY_DEPENDENCIES.get(name, set()):
            register_utility_emission(ctx, dep)


def assemble_utility_snippets(ctx: EmitContext) -> tuple[list[str], list[str]]:
    """Assemble source code for all needed utilities.

    Returns:
        (extra_imports, source_blocks): import statements and class/function sources.
            Sources are in dependency order (ctx last).
    """
    if not ctx.needed_utilities:
        return ([], [])

    # Resolve order: emit dependencies first, ctx last
    ordered_keys: list[str] = []
    seen: set[str] = set()

    def _visit(key: str) -> None:
        if key in seen:
            return
        seen.add(key)
        for dep in _UTILITY_DEPENDENCIES.get(key, set()):
            _visit(dep)
        ordered_keys.append(key)

    for key in sorted(ctx.needed_utilities):
        _visit(key)

    extra_imports: list[str] = []
    source_blocks: list[str] = []

    for key in ordered_keys:
        obj = UTILITIES[key]
        imports = UTILITY_IMPORTS[key]

        # Collect imports
        extra_imports.extend(imports)

        # Get source: read the entire file the utility is defined in
        # (works better than inspect.getsource for utilities with helper functions)
        module = inspect.getmodule(obj)
        if module and hasattr(module, "__file__") and module.__file__:
            module_path = Path(module.__file__)
            raw_source = module_path.read_text(encoding="utf-8")
        else:
            # Fallback to inspect.getsource if module file is not available
            raw_source = inspect.getsource(obj)

        # Strip the @register_utility decorator and import of _registry
        cleaned_source = _strip_registration_artifacts(raw_source)

        source_blocks.append(cleaned_source)

    return (extra_imports, source_blocks)


def _strip_registration_artifacts(source: str) -> str:
    """Remove @register_utility decorator, _registry import, and __future__ imports from source."""
    lines = source.split("\n")
    cleaned: list[str] = []

    i = 0
    while i < len(lines):
        line = lines[i]

        # Skip __future__ imports (will cause SyntaxError if not at file top)
        if line.strip().startswith("from __future__ import"):
            i += 1
            continue

        # Skip import of _registry
        if "from vg2c.emitter.utilities._registry import" in line:
            i += 1
            continue

        # Skip @register_utility decorator (may span multiple lines)
        if line.strip().startswith("@register_utility"):
            # Skip all decorator lines including the closing standalone ')'
            while i < len(lines):
                current_line = lines[i]
                i += 1
                # Stop after we find a line that is just a closing paren (possibly indented)
                if current_line.strip() == ")":
                    break
            continue

        cleaned.append(line)
        i += 1

    return "\n".join(cleaned)
