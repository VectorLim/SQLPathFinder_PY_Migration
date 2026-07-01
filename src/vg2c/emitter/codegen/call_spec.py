"""``CallSpec`` — a deterministic renderer for one Python call expression.

The ``receiver`` field is constructed by :func:`emit_call` (which reads
:func:`register_utility` metadata). Callers never type the receiver
string by hand. ``embed_key`` propagates the registered utility name so
the caller can register the embed dependency on its :class:`EmitContext`.
"""

from __future__ import annotations

from dataclasses import dataclass, field

from vg2c.emitter.codegen.expr import PyExpr

__all__ = ["CallSpec"]


@dataclass(frozen=True, slots=True)
class CallSpec:
    """One Python call: ``[receiver.]method(*args, **kwargs)``."""

    method: str
    receiver: str | None = None
    args: tuple[PyExpr, ...] = ()
    kwargs: dict[str, PyExpr] = field(default_factory=dict)
    embed_key: str | None = None
    """Registered ``@register_utility`` name of the target (for embed registration)."""

    def render(self) -> str:
        """Return the call as a single Python expression."""
        prefix = f"{self.receiver}.{self.method}" if self.receiver else self.method
        rendered_args = [a.source for a in self.args]
        rendered_kwargs = [f"{k}={v.source}" for k, v in self.kwargs.items()]
        return f"{prefix}({', '.join(rendered_args + rendered_kwargs)})"

    def __str__(self) -> str:
        return self.render()
