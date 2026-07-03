"""Registry and direct-emission helpers for embeddable utility classes."""

from __future__ import annotations

import inspect
import re
from dataclasses import dataclass
from typing import Any, Callable

from vg2c.emitter.utilities._base import UtilityShape, UtilitySpec
from vg2c.frontend.models import Kind

__all__ = [
    "UTILITIES",
    "UTILITY_IMPORTS",
    "UTILITY_DEPENDENCIES",
    "CLASS_TO_UTILITY_NAME",
    "UtilityCommandMatch",
    "classify_utility_command",
    "mark_utility_used",
    "assemble_registered_utilities",
    "get_registered_source",
    "register_utility",
]

# Registry of utility classes keyed by name
UTILITIES: dict[str, type[UtilitySpec]] = {}

# Registry of imports for each utility
UTILITY_IMPORTS: dict[str, tuple[str, ...]] = {}

# Registry of dependencies for each utility
UTILITY_DEPENDENCIES: dict[str, tuple[str, ...]] = {}

# Registry of owner class by emitted block kind.
KIND_HANDLERS: dict[Kind, type[UtilitySpec]] = {}

# Reverse map for class-typed utilities.
CLASS_TO_UTILITY_NAME: dict[type, str] = {}


@dataclass(frozen=True, slots=True)
class UtilityCommandMatch:
    shape: UtilityShape | None
    argv: tuple[str, ...]
    utility_cls: type[UtilitySpec] | None


def register_utility(
    cls: type[UtilitySpec] | None = None,
    *,
    name: str | None = None,
    imports: tuple[str, ...] | None = None,
    depends_on: tuple[str, ...] | None = None,
    handles: Kind | tuple[Kind, ...] | None = None,
) -> type[UtilitySpec] | Callable[[type[UtilitySpec]], type[UtilitySpec]]:
    """Register one utility class from decorator args or class metadata."""

    def _register(target: type[UtilitySpec]) -> type[UtilitySpec]:
        if not inspect.isclass(target) or not issubclass(target, UtilitySpec):
            raise TypeError("register_utility expects a UtilitySpec subclass")

        reg_name = (name or target.utility_name).strip()
        if not reg_name:
            raise ValueError(f"{target.__name__}: utility_name must be non-empty")
        if reg_name in UTILITIES:
            raise ValueError(f"duplicate utility_name: {reg_name}")

        reg_imports = tuple(imports if imports is not None else target.utility_imports)
        reg_deps = tuple(
            depends_on if depends_on is not None else target.utility_dependencies
        )
        if handles is None:
            reg_handles = tuple(target.handles)
        elif isinstance(handles, tuple):
            reg_handles = handles
        else:
            reg_handles = (handles,)

        UTILITIES[reg_name] = target
        UTILITY_IMPORTS[reg_name] = reg_imports
        UTILITY_DEPENDENCIES[reg_name] = reg_deps
        CLASS_TO_UTILITY_NAME[target] = reg_name

        for handled_kind in reg_handles:
            owner = KIND_HANDLERS.get(handled_kind)
            if owner is not None:
                raise ValueError(
                    f"duplicate handler for {handled_kind}: {owner.__name__} and {target.__name__}"
                )
            KIND_HANDLERS[handled_kind] = target

        setattr(target, "__vg2c_registered_name__", reg_name)
        return target

    if cls is None:
        return _register
    return _register(cls)


def classify_utility_command(argv: list[str]) -> UtilityCommandMatch:
    argv_tuple = tuple(argv)
    if not argv:
        return UtilityCommandMatch(shape=None, argv=argv_tuple, utility_cls=None)

    first = argv[0]
    basename = first.split("/")[-1].split("\\")[-1].lower()

    for cls in UTILITIES.values():
        for shape in cls.utility_shapes:
            if shape.contains and any(marker in basename for marker in shape.contains):
                return UtilityCommandMatch(
                    shape=shape, argv=argv_tuple, utility_cls=cls
                )
            if shape.suffixes and any(
                basename.endswith(suffix) for suffix in shape.suffixes
            ):
                return UtilityCommandMatch(
                    shape=shape, argv=argv_tuple, utility_cls=cls
                )

    return UtilityCommandMatch(shape=None, argv=argv_tuple, utility_cls=None)


def mark_utility_used(ctx: Any, *names: str) -> None:
    for name in names:
        if name not in UTILITIES:
            raise KeyError(f"Unknown utility: {name}")
        if name in ctx.needed_utilities:
            continue
        ctx.needed_utilities.add(name)
        for dep in UTILITY_DEPENDENCIES.get(name, ()):
            mark_utility_used(ctx, dep)


def assemble_registered_utilities(ctx) -> tuple[list[str], list[str]]:
    if not ctx.needed_utilities:
        return ([], [])

    ordered_keys: list[str] = []
    seen: set[str] = set()

    def _visit(key: str) -> None:
        if key in seen:
            return
        seen.add(key)
        for dep in UTILITY_DEPENDENCIES.get(key, ()):  # already validated in register
            _visit(dep)
        ordered_keys.append(key)

    for key in sorted(ctx.needed_utilities):
        _visit(key)

    imports: list[str] = []
    sources: list[str] = []
    for key in ordered_keys:
        imports.extend(UTILITY_IMPORTS[key])
        sources.append(get_registered_source(key))
    return imports, sources


def get_registered_source(name: str) -> str:
    cls = UTILITIES[name]
    source = inspect.getsource(cls)
    return _strip_embed_artifacts(source, cls.__name__)


_CLASS_SIG_RE = re.compile(r"^(\s*class\s+\w+)\(.*\):\s*$")


def _strip_embed_artifacts(source: str, class_name: str) -> str:
    lines = source.split("\n")

    while lines and lines[0].lstrip().startswith("@"):
        lines.pop(0)

    if not lines:
        return ""

    lines[0] = _CLASS_SIG_RE.sub(r"\1:", lines[0])
    lines[0] = lines[0].replace("(UtilitySpec):", ":")
    lines[0] = lines[0].replace(f"({class_name}, UtilitySpec):", f"({class_name}):")

    return "\n".join(lines).rstrip()
