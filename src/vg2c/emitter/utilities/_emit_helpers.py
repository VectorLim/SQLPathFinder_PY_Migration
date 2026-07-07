from __future__ import annotations

import re
from typing import Any

from vg2c.emitter.utilities._base import UtilitySpec
from vg2c.emitter.utilities._emit_types import RawExpr
from vg2c.frontend.models import Kind

__all__ = [
    "NAMED_PLACEHOLDER_RE",
    "_emit_step_source",
    "_render_value",
    "_step_name",
    "emit_block",
    "macro_token_to_python_expr",
    "placeholders_to_python_expr",
    "render_method_call",
]

PLACEHOLDER_RE = re.compile(r"<<<([^>]+)>>>|<<>>")
NAMED_PLACEHOLDER_RE = re.compile(r"<<<([^>]+)>>>")


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


def render_method_call(
    ctx: Any,
    utility_name: str,
    method_name: str,
    *,
    args: tuple[Any, ...] = (),
    kwargs: dict[str, Any] | None = None,
) -> str:
    receiver = "ctx" if utility_name == "ctx" else f"ctx.{utility_name}"
    parts: list[str] = [_render_value(arg) for arg in args]
    for key, value in (kwargs or {}).items():
        parts.append(f"{key}={_render_value(value)}")
    return f"{receiver}.{method_name}({', '.join(parts)})"


def _step_name(block, suffix: str) -> str:
    return f"step_{block.parsed.index:04d}_{suffix}"


def _emit_step_source(name: str, body_lines: list[str]) -> tuple[str, str]:
    lines = [f"def {name}(ctx) -> None:"]
    if body_lines:
        lines.extend([f"    {line}" for line in body_lines])
    else:
        lines.append("    pass")
    return "\n".join(lines), f"{name}(ctx)"


def emit_block(ctx: Any, block: Any, dispatched: Any) -> tuple[str, str]:
    handler_cls = UtilitySpec._kind_handlers.get(block.kind)
    if handler_cls is not None:
        emitted = handler_cls.emit_block(ctx, block, dispatched)
        if emitted is not None:
            return emitted

    if block.kind is Kind.UTILITY:
        return _emit_step_source(
            _step_name(block, "utility"),
            ["pass  # TODO: utility command not classified"],
        )
    if block.kind is Kind.HTML_REPORT:
        return _emit_step_source(
            _step_name(block, "html_report"),
            ["pass  # HTML report not translated"],
        )
    return _emit_step_source(
        _step_name(block, "unknown"),
        [f"pass  # TODO: unhandled kind={block.kind}"],
    )
