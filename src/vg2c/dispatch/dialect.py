from __future__ import annotations

# Compatibility shim - delegates to registry
from vg2c.dispatch.registry import (
    SQL_BEARING_KINDS,
    derive_from_signals,
    resolve_dialect,
)

__all__ = [
    "SQL_BEARING_KINDS",
    "derive_from_signals",
    "resolve_dialect",
]
