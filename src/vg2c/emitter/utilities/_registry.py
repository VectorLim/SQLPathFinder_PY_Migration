"""Registry for embeddable utility classes.

Registration is class-attribute driven. Every utility must inherit
``UtilitySpec`` and declare ``utility_name`` and metadata attributes.
"""

from __future__ import annotations

import inspect
import re
from dataclasses import dataclass

from vg2c.emitter.utilities._base import UtilitySpec

__all__ = [
    "UTILITIES",
    "UTILITY_IMPORTS",
    "UTILITY_DEPENDENCIES",
    "CLASS_TO_UTILITY_NAME",
    "UtilityCommandMatch",
    "classify_utility_command",
    "require_utility",
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

# Reverse map for class-typed utilities, used by emit_call to derive the
# ``ctx.<name>`` receiver from an unbound-method reference.
CLASS_TO_UTILITY_NAME: dict[type, str] = {}


@dataclass(frozen=True, slots=True)
class UtilityCommandMatch:
    shape: str
    argv: tuple[str, ...]
    utility_cls: type[UtilitySpec] | None


def register_utility(cls: type[UtilitySpec]) -> type[UtilitySpec]:
    """Register one utility class from its declared metadata."""
    if not inspect.isclass(cls) or not issubclass(cls, UtilitySpec):
        raise TypeError("register_utility expects a UtilitySpec subclass")

    name = cls.utility_name.strip()
    if not name:
        raise ValueError(f"{cls.__name__}: utility_name must be non-empty")
    if name in UTILITIES:
        raise ValueError(f"duplicate utility_name: {name}")

    imports = tuple(cls.utility_imports)
    deps = tuple(cls.utility_dependencies)

    UTILITIES[name] = cls
    UTILITY_IMPORTS[name] = imports
    UTILITY_DEPENDENCIES[name] = deps
    CLASS_TO_UTILITY_NAME[cls] = name
    setattr(cls, "__vg2c_registered_name__", name)
    return cls


def classify_utility_command(utilities_string: str) -> UtilityCommandMatch:
    text = utilities_string.strip()
    if not text:
        return UtilityCommandMatch(shape="unknown", argv=(), utility_cls=None)

    argv = tuple(text.split())
    if not argv:
        return UtilityCommandMatch(shape="unknown", argv=(), utility_cls=None)

    first = argv[0]
    basename = first.split("/")[-1].split("\\")[-1].lower()

    for cls in UTILITIES.values():
        for shape, markers in cls.utility_command_contains:
            if any(marker in basename for marker in markers):
                return UtilityCommandMatch(shape=shape, argv=argv, utility_cls=cls)
        for shape, suffixes in cls.utility_command_suffixes:
            if any(basename.endswith(suffix) for suffix in suffixes):
                return UtilityCommandMatch(shape=shape, argv=argv, utility_cls=cls)

    return UtilityCommandMatch(shape="unknown", argv=argv, utility_cls=None)


def require_utility(ctx, *names: str) -> None:
    for name in names:
        if name not in UTILITIES:
            raise KeyError(f"Unknown utility: {name}")
        if name in ctx.needed_utilities:
            continue
        ctx.needed_utilities.add(name)
        for dep in UTILITY_DEPENDENCIES.get(name, ()):
            require_utility(ctx, dep)


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
    return _strip_embed_artifacts(
        source,
        cls.__name__,
        set(cls.utility_embed_exclude_methods),
    )


_CLASS_SIG_RE = re.compile(r"^(\s*class\s+\w+)\(.*\):\s*$")


def _strip_embed_artifacts(
    source: str,
    class_name: str,
    excluded_methods: set[str],
) -> str:
    lines = source.split("\n")

    while lines and lines[0].lstrip().startswith("@"):
        lines.pop(0)

    if not lines:
        return ""

    lines[0] = _CLASS_SIG_RE.sub(r"\1:", lines[0])
    lines[0] = lines[0].replace("(UtilitySpec):", ":")
    lines[0] = lines[0].replace(f"({class_name}, UtilitySpec):", f"({class_name}):")

    cleaned: list[str] = []
    i = 0
    while i < len(lines):
        line = lines[i]
        stripped = line.lstrip()
        indent = len(line) - len(stripped)

        if indent == 4:
            method_name = _method_name(stripped)
            if method_name in excluded_methods:
                i = _skip_block(lines, i, indent)
                continue

        if indent == 4 and stripped.startswith("@"):
            j = i
            while j < len(lines):
                candidate = lines[j]
                c_stripped = candidate.lstrip()
                c_indent = len(candidate) - len(c_stripped)
                if c_indent != 4 or not c_stripped.startswith("@"):
                    break
                j += 1
            if j < len(lines):
                next_line = lines[j]
                n_stripped = next_line.lstrip()
                n_indent = len(next_line) - len(n_stripped)
                method_name = _method_name(n_stripped) if n_indent == 4 else None
                if method_name in excluded_methods:
                    i = _skip_block(lines, j, n_indent)
                    continue

        cleaned.append(line)
        i += 1

    return "\n".join(cleaned).rstrip()


def _skip_block(lines: list[str], index: int, indent: int) -> int:
    i = index + 1
    while i < len(lines):
        stripped = lines[i].lstrip()
        current_indent = len(lines[i]) - len(stripped)
        if stripped and current_indent <= indent:
            break
        i += 1
    return i


def _method_name(stripped: str) -> str | None:
    if stripped.startswith("def ") and "(" in stripped:
        return stripped[4 : stripped.index("(")].strip()
    return None
