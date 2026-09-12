"""Compile-local naming and reuse for utility-selected literal values."""

from __future__ import annotations

import re

from vg2c.emitter.models import CodeExpr, EmittedParameter, SourceRange, _value_metadata

GlobalValues = dict[str, dict[str, object]]


def render_script_settings(settings: tuple[tuple[str, object, str], ...]) -> str:
    """Render input-agnostic runtime settings in the generated-script header."""
    if not settings:
        return ""

    lines = [
        "# ---------------------------------------------------------------------------",
        "# VG2C generated-script settings",
        "# Edit these values to tune runtime behavior for this generated script.",
        "# ---------------------------------------------------------------------------",
    ]
    for name, default, description in settings:
        lines.append(f"# {description}")
        lines.append(f"{name} = {default!r}")
    lines.append("# ---------------------------------------------------------------------------")
    return "\n".join(lines)


def global_key(key: str) -> str:
    name = re.sub(r"[^A-Z0-9_]", "_", key.upper()).strip("_") or "VALUE"
    return f"VALUE_{name}" if name[0].isdigit() else name


def resolve_globals(
    candidates: dict[str, object],
    collected: GlobalValues,
    block_index: int,
    reserved: set[str] | frozenset[str] = frozenset(),
) -> dict[str, CodeExpr]:
    resolved: dict[str, CodeExpr] = {}
    occupied = {name for group in collected.values() for name in group} | reserved
    for key, value in candidates.items():
        group = collected.setdefault(key, {})
        # repr keeps scalar/list types distinct (e.g. True, 1, and '1').
        name = next((name for name, old in group.items() if repr(old) == repr(value)), None)
        if name is None:
            base = global_key(key)
            name = base
            if name in occupied:
                name = f"STEP_{block_index:04d}_{base}"
            suffix = 2
            prefix = name
            while name in occupied:
                name = f"{prefix}_{suffix}"
                suffix += 1
            group[name] = value
            occupied.add(name)
        resolved[key] = CodeExpr(name, value, global_names=(name,))
    return resolved


def render_globals(
    collected: GlobalValues, used: set[str], offset: int
) -> tuple[str, dict[str, EmittedParameter]]:
    lines: list[str] = []
    parameters: dict[str, EmittedParameter] = {}
    for group in collected.values():
        for name, value in group.items():
            if name not in used:
                continue
            literal = repr(value)
            prefix = f"{name} = "
            start = offset + len(prefix)
            parameters[name] = EmittedParameter(
                id=f"global:{name}",
                name=name,
                position=None,
                source=literal,
                definition=None,
                artifact_role=None,
                source_range=SourceRange(start, start + len(literal)),
                **_value_metadata(value),
            )
            lines.append(prefix + literal)
            offset += len(prefix) + len(literal) + 1
    return "\n".join(lines), parameters
