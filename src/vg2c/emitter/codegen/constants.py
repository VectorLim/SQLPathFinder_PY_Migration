"""Single source of truth for the receiver variable name in emitted code."""

from __future__ import annotations

__all__ = ["CTX_VAR", "CTX_PARAM"]

CTX_VAR = "ctx"
"""Name of the runtime-context variable referenced in every emitted call."""

CTX_PARAM = "ctx"
"""Parameter name used by generated step functions: ``def step_x(ctx): ...``."""
