from __future__ import annotations

from pathlib import Path
import re
import shlex
from typing import Any

from vg2c.kind import Kind

__all__ = [
    "NAMED_PLACEHOLDER_RE",
    "PLACEHOLDER_RE",
    "normalize_macro_name",
    "option_to_python_expr",
    "placeholders_to_python_expr",
    "resolve_output_path",
    "resolve_path",
    "split_utility_command",
    "strip_quotes",
]

PLACEHOLDER_RE = re.compile(r"<<<([^>]+)>>>|<<>>")
NAMED_PLACEHOLDER_RE = re.compile(r"<<<([^>]+)>>>")


def strip_quotes(value: str) -> str:
    value = value.strip()
    if len(value) >= 2 and value[0] == value[-1] and value[0] in {'"', "'"}:
        return value[1:-1]
    return value


def split_utility_command(text: str) -> list[str]:
    text = text.strip()
    if not text:
        return []

    lexer = shlex.shlex(text, posix=False)
    lexer.whitespace_split = True
    lexer.commenters = ""
    return list(lexer)


def option_to_python_expr(value: str | None) -> str:
    if value is None:
        return "None"
    return placeholders_to_python_expr(strip_quotes(value))


def resolve_output_path(block: Any) -> str:
    csv_value = block.resolved_options.lookup.get("CSV")
    if csv_value:
        return strip_quotes(csv_value)

    write_file_value = block.resolved_options.lookup.get("WRITE-FILE")
    if write_file_value:
        candidate = strip_quotes(write_file_value)
        if candidate.upper() not in {"Y", "N"}:
            return candidate

    suffix = "txt" if block.kind in {Kind.WRITE_FILE, Kind.PYTHON_EMBED} else "csv"
    return f"step_{block.index:04d}.{suffix}"


def resolve_path(name: str | Path, *, for_write: bool = False) -> Path:
    path = Path(name)
    script_file = globals().get("__file__")
    if script_file and Path(script_file).name != "_emit_helpers.py":
        base_dir = Path(script_file).resolve().parent
    else:
        base_dir = Path.cwd()

    if path.is_absolute():
        if for_write:
            return path
        if path.exists():
            return path
        local_fallback = Path(path.name)
        if local_fallback.exists():
            return local_fallback
        return path

    base_path = base_dir / path
    if for_write:
        return base_path

    if path.exists():
        return path
    if base_path.exists():
        return base_path
    return base_path


def normalize_macro_name(raw: str) -> str:
    name = raw.strip()
    if name.startswith("<<<") and name.endswith(">>>"):
        name = name[3:-3]
    return name.strip().upper()


def placeholders_to_python_expr(text: str) -> str:
    if not text:
        return repr("")

    parts: list[str] = []
    cursor = 0

    for match in PLACEHOLDER_RE.finditer(text):
        literal = text[cursor : match.start()]
        if literal:
            parts.append(repr(literal))

        named = match.group(1)
        if named is not None:
            parts.append(f"ctx.macro.named({repr(normalize_macro_name(named))})")
        else:
            parts.append("ctx.macro.positional()")

        cursor = match.end()

    tail = text[cursor:]
    if tail:
        parts.append(repr(tail))

    if not parts:
        return repr(text)
    if len(parts) == 1:
        return parts[0]
    return " + ".join(parts)
