"""Compiler-side workflow parsing and expression construction."""

from __future__ import annotations

import re
import shlex
from typing import Any

from vg2c.emitter.models import CodeExpr
from vg2c.kind import Kind
from vg2c.runtime.sql_text import SqlGetCsvListCall, scan_sql_get_csv_list_calls
from vg2c.utilities._runtime_helpers import normalize_macro_name, strip_quotes

__all__ = [
    "parse_table_binding",
    "resolve_output_path",
    "scan_sql_get_csv_list_calls",
    "extract_crosstab_options",
    "split_utility_command",
    "to_code_expr",
    "list_code_expr",
    "placeholders_to_python_expr",
]

_TABLE_BINDING_RE = re.compile(r"^(?P<path>.+\.[^\\/:]+):(?P<table>[A-Za-z_][A-Za-z0-9_]*)$")


def parse_table_binding(value: str) -> tuple[str, str | None]:
    """Return a /TABLE path and its optional SQLite table name."""
    value = strip_quotes(value)
    match = _TABLE_BINDING_RE.match(value)
    if match:
        return match.group("path"), match.group("table")
    return value, None


def split_utility_command(text: str) -> list[str]:
    text = text.strip()
    if not text:
        return []

    lexer = shlex.shlex(text, posix=False)
    lexer.whitespace_split = True
    lexer.commenters = ""
    return list(lexer)


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


def extract_crosstab_options(block: Any) -> dict[str, Any] | None:
    ctrow = strip_quotes(block.resolved_options.lookup.get("CTROW", ""))
    ctheader = strip_quotes(block.resolved_options.lookup.get("CTHEADER", ""))
    ctvalue = strip_quotes(block.resolved_options.lookup.get("CTVALUE", ""))
    if not (ctrow and ctheader and ctvalue):
        return None
    return {
        "row_keys": [c.strip() for c in ctrow.split(",") if c.strip()],
        "header_key": ctheader,
        "value_key": ctvalue,
    }


def placeholders_to_python_expr(text: str) -> str:
    from vg2c.utilities.macro_state import MacroState

    if not text:
        return repr("")
    parts: list[str] = []
    cursor = 0
    for match in MacroState.PLACEHOLDER_RE.finditer(text):
        literal = text[cursor : match.start()]
        if literal:
            parts.append(repr(literal))
        named = match.group(1)
        if named is not None:
            parts.append(MacroState.named.render(normalize_macro_name(named)))
        else:
            parts.append(MacroState.positional.render())
        cursor = match.end()
    tail = text[cursor:]
    if tail:
        parts.append(repr(tail))
    if not parts:
        return repr(text)
    return parts[0] if len(parts) == 1 else " + ".join(parts)


def to_code_expr(value: str | None) -> CodeExpr:
    from vg2c.utilities.macro_state import MacroState

    if value is None:
        return CodeExpr("None")
    text = strip_quotes(value)
    source = placeholders_to_python_expr(text)
    symbol_names = tuple(
        dict.fromkeys(
            normalize_macro_name(match.group(1))
            for match in MacroState.PLACEHOLDER_RE.finditer(text)
            if match.group(1) is not None
        )
    )
    if MacroState.PLACEHOLDER_RE.search(text):
        return CodeExpr(source, symbol_names=symbol_names)
    return CodeExpr(source, text)


def list_code_expr(values: list[str]) -> CodeExpr:
    items = [to_code_expr(value) for value in values]
    source = "[" + ", ".join(item.source for item in items) + "]"
    symbol_names = tuple(
        dict.fromkeys(name for item in items for name in item.symbol_names)
    )
    if all(item.has_value for item in items):
        return CodeExpr(source, [item.value for item in items], symbol_names=symbol_names)
    return CodeExpr(source, symbol_names=symbol_names)
