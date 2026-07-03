from __future__ import annotations

from typing import Any

from vg2c.emitter.semtypes import Argv, RawExpr
from vg2c.emitter.utilities._registry import (
    KIND_HANDLERS,
    classify_utility_command,
    mark_utility_used,
)
from vg2c.frontend.models import Kind

__all__ = [
    "_emit_step_source",
    "_render_value",
    "_step_name",
    "emit_block",
    "render_method_call",
]


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
    mark_utility_used(ctx, utility_name)
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


def _emit_utility(ctx: Any, block: Any) -> tuple[str, str]:
    argv = Argv.extract(block, None)
    match = classify_utility_command(argv)
    stmt = None
    if match.shape is not None and match.shape.emit is not None:
        stmt = match.shape.emit(ctx, list(match.argv))

    shape_name = match.shape.name if match.shape is not None else "unknown"
    if stmt is None:
        return _emit_step_source(
            _step_name(block, "utility"),
            [f"pass  # TODO: utility shape not translated: {shape_name}"],
        )
    return _emit_step_source(_step_name(block, "utility"), [stmt])


def emit_block(ctx: Any, block: Any, dispatched: Any) -> tuple[str, str]:
    handler_cls = KIND_HANDLERS.get(block.kind)
    if handler_cls is not None:
        emitted = handler_cls.emit_block(ctx, block, dispatched)
        if emitted is not None:
            return emitted

    if block.kind is Kind.UTILITY:
        return _emit_utility(ctx, block)
    if block.kind is Kind.HTML_REPORT:
        return _emit_step_source(
            _step_name(block, "html_report"),
            ["pass  # HTML report not translated"],
        )
    return _emit_step_source(
        _step_name(block, "unknown"),
        [f"pass  # TODO: unhandled kind={block.kind}"],
    )
