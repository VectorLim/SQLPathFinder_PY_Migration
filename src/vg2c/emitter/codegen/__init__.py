"""Compile-time codegen primitives for the emitter.

Owns the canonical Python-source rendering vocabulary used by handlers
and the walker so they never type ``ctx.x.y(...)`` strings by hand.
"""

from __future__ import annotations

from vg2c.emitter.codegen.call_spec import CallSpec
from vg2c.emitter.codegen.constants import CTX_PARAM, CTX_VAR
from vg2c.emitter.codegen.emit_call import (
    EmitTargetError,
    emit_call,
    register_call_embed,
)
from vg2c.emitter.codegen.expr import PyExpr
from vg2c.emitter.codegen.function_def import FunctionDef
from vg2c.emitter.codegen.options import (
    declared_headers,
    python_literal_for_option,
    strip_quotes,
)

__all__ = [
    "CTX_VAR",
    "CTX_PARAM",
    "PyExpr",
    "CallSpec",
    "FunctionDef",
    "emit_call",
    "register_call_embed",
    "EmitTargetError",
    "strip_quotes",
    "python_literal_for_option",
    "declared_headers",
]
