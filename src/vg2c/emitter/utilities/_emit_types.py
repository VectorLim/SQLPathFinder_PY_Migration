from __future__ import annotations

from dataclasses import dataclass
from typing import Any

from vg2c.emitter.macro import placeholders_to_python_expr
from vg2c.frontend.models import Kind

__all__ = [
    "RawExpr",
    "option_to_python_expr",
    "resolve_output_path",
    "strip_quotes",
]


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
