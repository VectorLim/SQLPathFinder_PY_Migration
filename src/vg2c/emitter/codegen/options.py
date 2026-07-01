"""Block-option helpers shared by handlers.

These functions parse the small set of ``/KEY=VALUE`` options that
handlers need when turning a ``ResolvedBlock`` into ``CallSpec`` kwargs.
"""

from __future__ import annotations

from vg2c.emitter.codegen.expr import PyExpr
from vg2c.emitter.macro import placeholders_to_python_expr

__all__ = [
    "strip_quotes",
    "python_literal_for_option",
    "declared_headers",
]


def strip_quotes(value: str) -> str:
    value = value.strip()
    if len(value) >= 2 and value[0] == value[-1] and value[0] in {'"', "'"}:
        return value[1:-1]
    return value


def python_literal_for_option(value: str | None) -> PyExpr:
    """Translate a raw option value into a Python expression.

    ``<<<NAME>>>`` placeholders become ``ctx.macro.named("NAME")`` calls;
    everything else becomes a string literal.
    """
    if value is None:
        return PyExpr.literal(None)
    return PyExpr.raw(placeholders_to_python_expr(strip_quotes(value)))


def declared_headers(block) -> list[str] | None:
    """Return the declared ``/HEADERS`` columns, or ``None`` if dynamic/absent.

    Returns ``None`` when:
    - ``/HEADERS`` is absent;
    - the headers carry a ``CrossTab->[[...]]`` placeholder (dynamic columns).
    """
    headers_value = block.resolved_options.lookup.get("HEADERS")
    if not headers_value:
        return None
    if "CrossTab->[[" in headers_value:
        return None
    stripped = strip_quotes(headers_value)
    parts = [p.strip() for p in stripped.split(",")]
    return [p for p in parts if p]
