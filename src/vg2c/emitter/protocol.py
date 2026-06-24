from __future__ import annotations

from typing import Any, Protocol

from vg2c.frontend.models import Kind
from vg2c.resolver.models import ResolvedBlock

__all__ = ["Handler", "HandlerRegistry"]


class Handler(Protocol):
    """Interface for per-Kind block emission.

    Implementations must be callable objects that emit a Python function for one block.
    """

    def emit(
        self, ctx: Any, block: ResolvedBlock, dispatched: Any | None
    ) -> tuple[str, str]:
        """Emit Python code for one block.

        Args:
            ctx: EmitContext (mutable state)
            block: ResolvedBlock (the source)
            dispatched: DispatchedBlock | None (Stage 4 metadata, None for non-SQL blocks)

        Returns:
            (function_source, call_site_line) where:
            - function_source is the full function definition
            - call_site_line is the one-liner to invoke it from run()
        """
        ...


class HandlerRegistry:
    """Manages per-Kind handler registration and lookup."""

    def __init__(self):
        self._handlers: dict[Kind, Handler] = {}

    def register(self, kind: Kind, handler: Handler) -> None:
        """Register a handler for a Kind."""
        self._handlers[kind] = handler

    def get(self, kind: Kind) -> Handler | None:
        """Look up a handler by Kind."""
        return self._handlers.get(kind)

    def __contains__(self, kind: Kind) -> bool:
        return kind in self._handlers
