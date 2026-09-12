"""Compiler-side workflow parsing and expression construction."""

from __future__ import annotations

import re
import shlex
from dataclasses import dataclass
from typing import Any

from vg2c.emitter.models import CodeExpr
from vg2c.kind import Kind
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


@dataclass(frozen=True, slots=True)
class SqlGetCsvListCall:
    """A well-formed SQL_Get_CSV_List call found in compiler input."""

    start: int
    end: int
    csv_path: str
    column_ref: int | str
    lead_in: str
    needs_closing_paren: bool


_CALL_RE = re.compile(r"\bSQL_Get_CSV_List\s*\(", re.IGNORECASE)
_CALL_SITE_WRAP_RE = re.compile(r"\(\s*[A-Za-z_][\w.\[\]@]*\s+In\s*$", re.IGNORECASE)


def scan_sql_get_csv_list_calls(body: str) -> list[SqlGetCsvListCall]:
    calls: list[SqlGetCsvListCall] = []
    cursor = 0
    while True:
        match = _CALL_RE.search(body, cursor)
        if match is None:
            break
        open_paren = body.find("(", match.start())
        close_paren = _find_matching_paren(body, open_paren)
        if close_paren == -1:
            break
        args = _split_args(body[open_paren + 1 : close_paren])
        next_cursor = close_paren + 1
        if len(args) == 3:
            calls.append(
                SqlGetCsvListCall(
                    start=match.start(),
                    end=next_cursor,
                    csv_path=strip_quotes(args[0]),
                    column_ref=_parse_column_ref(args[1]),
                    lead_in=strip_quotes(args[2]),
                    needs_closing_paren=bool(_CALL_SITE_WRAP_RE.search(body[: match.start()])),
                )
            )
        cursor = next_cursor
    return calls


def _find_matching_paren(text: str, open_idx: int) -> int:
    depth = 0
    in_single = False
    in_double = False
    for i in range(open_idx, len(text)):
        ch = text[i]
        prev = text[i - 1] if i > 0 else ""
        if ch == "'" and prev != "\\" and not in_double:
            in_single = not in_single
        elif ch == '"' and prev != "\\" and not in_single:
            in_double = not in_double
        elif not in_single and not in_double:
            if ch == "(":
                depth += 1
            elif ch == ")":
                depth -= 1
                if depth == 0:
                    return i
    return -1


def _split_args(args_text: str) -> list[str]:
    args: list[str] = []
    current: list[str] = []
    depth = 0
    in_single = False
    in_double = False
    for i, ch in enumerate(args_text):
        prev = args_text[i - 1] if i > 0 else ""
        if ch == "'" and prev != "\\" and not in_double:
            in_single = not in_single
            current.append(ch)
            continue
        if ch == '"' and prev != "\\" and not in_single:
            in_double = not in_double
            current.append(ch)
            continue
        if not in_single and not in_double:
            if ch == "(":
                depth += 1
            elif ch == ")" and depth > 0:
                depth -= 1
            elif ch == "," and depth == 0:
                args.append("".join(current).strip())
                current = []
                continue
        current.append(ch)
    if current:
        args.append("".join(current).strip())
    return args


def _parse_column_ref(raw: str) -> int | str:
    value = strip_quotes(raw)
    return int(value) if value.isdigit() else value


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
    if MacroState.PLACEHOLDER_RE.search(text):
        return CodeExpr(source)
    return CodeExpr(source, text)


def list_code_expr(values: list[str]) -> CodeExpr:
    items = [to_code_expr(value) for value in values]
    source = "[" + ", ".join(item.source for item in items) + "]"
    if all(item.has_value for item in items):
        return CodeExpr(source, [item.value for item in items])
    return CodeExpr(source)
