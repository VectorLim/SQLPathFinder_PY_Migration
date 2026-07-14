"""Resolver operand payload package.

Each operand payload (``{START-MACRO}``, ``{IF-THEN}``, ``{RUN-LOOP}``,
``{ROWS-IN-FILE}`` …) lives in its own focused module. This package
re-exports every payload type plus the shared scope-tree primitives, and
defines the ``MacroControlPayload`` union used by ``ResolvedBlock`` and
``ScopeNode``.
"""

from __future__ import annotations

from vg2c.resolver.operands.base import (
    MacroFrame,
    ParseChildrenFn,
    ScopeIdSource,
    ScopeNode,
)
from vg2c.resolver.operands.conditional import Else, EndIf, IfThen
from vg2c.resolver.operands.file_ops import RowsInFile
from vg2c.resolver.operands.loop import EndLoop, RunLoop
from vg2c.resolver.operands.macro import EndMacro, StartMacro

MacroControlPayload = (
    StartMacro | EndMacro | IfThen | Else | EndIf | RowsInFile | RunLoop | EndLoop
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
    "RowsInFile",
    "RunLoop",
    "ScopeIdSource",
    "ScopeNode",
    "StartMacro",
]
