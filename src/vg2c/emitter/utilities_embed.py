"""Embedding logic for emitted scripts."""

from __future__ import annotations

import inspect
from pathlib import Path

from vg2c.emitter.models import EmitContext
from vg2c.emitter.utilities import UTILITIES, UTILITY_IMPORTS

__all__ = [
    "READER_RUNTIME_KEY",
    "READER_SNIPPET",
    "register_embed_emission",
    "register_utility_emission",
    "register_reader_emission",
    "assemble_embed_snippets",
    "assemble_utility_snippets",
]


# Dependency map: embed_name -> set of other embeds it needs.
_EMBED_DEPENDENCIES: dict[str, set[str]] = {
    "ctx": {
        "macro",
        "csv_io",
        "sqlite_engine",
        "sql_macros",
        "fs_ops",
        "mail",
        "external",
    },
}

READER_RUNTIME_KEY = "reader_runtime"


def register_embed_emission(ctx: EmitContext, *names: str) -> None:
    """Mark one or more embeds as needed in the emitted script."""
    for name in names:
        if name in ctx.needed_embeds:
            continue
        ctx.needed_embeds.add(name)
        for dep in _EMBED_DEPENDENCIES.get(name, set()):
            register_embed_emission(ctx, dep)


def register_utility_emission(ctx: EmitContext, *names: str) -> None:
    """Back-compat wrapper for utility embed registration."""
    register_embed_emission(ctx, *names)


def register_reader_emission(ctx: EmitContext) -> None:
    """Mark the emitted script as needing the embedded reader runtime."""
    register_embed_emission(ctx, READER_RUNTIME_KEY)


def assemble_embed_snippets(ctx: EmitContext) -> tuple[list[str], list[str]]:
    """Assemble source code for all needed embeds."""
    if not ctx.needed_embeds:
        return ([], [])

    ordered_keys: list[str] = []
    seen: set[str] = set()

    def _visit(key: str) -> None:
        if key in seen:
            return
        seen.add(key)
        for dep in _EMBED_DEPENDENCIES.get(key, set()):
            _visit(dep)
        ordered_keys.append(key)

    for key in sorted(ctx.needed_embeds):
        _visit(key)

    extra_imports: list[str] = []
    source_blocks: list[str] = []

    for key in ordered_keys:
        imports, source = _resolve_embed_source(key)
        extra_imports.extend(imports)
        source_blocks.append(source)

    return (extra_imports, source_blocks)


def assemble_utility_snippets(ctx: EmitContext) -> tuple[list[str], list[str]]:
    """Back-compat wrapper for prior utility-only API name."""
    return assemble_embed_snippets(ctx)


def _resolve_embed_source(key: str) -> tuple[tuple[str, ...], str]:
    if key in UTILITIES:
        obj = UTILITIES[key]
        imports = UTILITY_IMPORTS[key]
        module = inspect.getmodule(obj)
        if module and hasattr(module, "__file__") and module.__file__:
            module_path = Path(module.__file__)
            raw_source = module_path.read_text(encoding="utf-8")
        else:
            raw_source = inspect.getsource(obj)
        return imports, _strip_embed_artifacts(raw_source)

    raise KeyError(f"Unknown embed key: {key}")


def _strip_embed_artifacts(source: str) -> str:
    """Remove utility registration and non-embeddable import artifacts."""
    lines = source.split("\n")
    cleaned: list[str] = []

    i = 0
    while i < len(lines):
        line = lines[i]

        if line.strip().startswith("from __future__ import"):
            i += 1
            continue

        if "from vg2c.emitter.utilities._registry import" in line:
            i += 1
            continue

        if line.strip().startswith("@register_utility"):
            stripped = line.strip()
            i += 1
            # Single-line decorator: @register_utility("reader_runtime")
            if ")" in stripped and not stripped.endswith("("):
                continue
            # Multi-line decorator block: consume until a standalone closing paren.
            while i < len(lines):
                current_line = lines[i]
                i += 1
                if current_line.strip() == ")":
                    break
            continue

        cleaned.append(line)
        i += 1

    return "\n".join(cleaned)


READER_SNIPPET = _resolve_embed_source(READER_RUNTIME_KEY)[1]
