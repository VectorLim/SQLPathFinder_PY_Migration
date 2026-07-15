"""Resolver operand payload package.

Each operand payload (``{START-MACRO}``, ``{IF-THEN}``, ``{RUN-LOOP}`` …)
lives in its own focused module. This package re-exports every payload type
plus the shared scope-tree primitives, and defines the
``MacroControlPayload`` union used by ``ResolvedBlock`` and ``ScopeNode``.
"""

from __future__ import annotations

from vg2c.operands.base import (
    MacroFrame,
    ParseChildrenFn,
    ScopeIdSource,
    ScopeNode,
)
from vg2c.operands.conditional import Else, EndIf, IfThen
from vg2c.operands.loop import EndLoop, RunLoop
from vg2c.operands.macro import EndMacro, StartMacro

MacroControlPayload = (
    StartMacro | EndMacro | IfThen | Else | EndIf | RunLoop | EndLoop
)

__all__ = [
    "Else",
    "EndIf",
    "EndLoop",
    "EndMacro",
    "IfThen",
    "MacroControlPayload",
    "MacroFrame",
    "ParseChildrenFn",
    "RunLoop",
    "ScopeIdSource",
    "ScopeNode",
    "StartMacro",
]
