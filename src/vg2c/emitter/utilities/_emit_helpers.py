from __future__ import annotations

from dataclasses import dataclass
import re
from typing import Any

from vg2c.frontend.models import Kind

__all__ = [
    "NAMED_PLACEHOLDER_RE",
    "RawExpr",
    "_emit_step_source",
    "_render_value",
    "_step_name",
    "macro_token_to_python_expr",
    "option_to_python_expr",
    "placeholders_to_python_expr",
    "resolve_output_path",
    "strip_quotes",
]

PLACEHOLDER_RE = re.compile(r"<<<([^>]+)>>>|<<>>")
NAMED_PLACEHOLDER_RE = re.compile(r"<<<([^>]+)>>>")


@dataclass(frozen=True, slots=True)
class RawExpr:
    source: str


def strip_quotes(value: str) -> str:
    value = value.strip()
    if len(value) >= 2 and value[0] == value[-1] and value[0] in {'"', "'"}:
        return value[1:-1]
    return value


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

    suffix = "txt" if block.kind is Kind.WRITE_FILE else "csv"
    return f"step_{block.parsed.index:04d}.{suffix}"


def _normalize_macro_name(raw: str) -> str:
    name = raw.strip()
    if name.startswith("<<<") and name.endswith(">>>"):
        name = name[3:-3]
    return name.strip().upper()


def macro_token_to_python_expr(raw: str) -> str:
    return f'ctx.macro.named("{_normalize_macro_name(raw)}")'


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
            parts.append(macro_token_to_python_expr(named))
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


def _render_value(value: Any) -> str:
    if isinstance(value, RawExpr):
        return value.source
    return repr(value)


def _step_name(block: Any, suffix: str) -> str:
    return f"step_{block.parsed.index:04d}_{suffix}"


def _emit_step_source(name: str, body_lines: list[str]) -> tuple[str, str]:
    lines = [f"def {name}(ctx) -> None:"]
    if body_lines:
        lines.extend([f"    {line}" for line in body_lines])
    else:
        lines.append("    pass")
    return "\n".join(lines), f"{name}(ctx)"
