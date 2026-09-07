from __future__ import annotations

from vg2c.frontend.models import ClassifiedBlock
from vg2c.operands import (
    MacroControlPayload,
    MacroFrame,
    ScopeNode,
)
from vg2c.resolver.macro_resolver import resolve_macros
from vg2c.resolver.models import (
    ResolvedBlock,
    ResolvedProgram,
)
from vg2c.resolver.scope_builder import build_scope_tree


def resolve(
    blocks: list[ClassifiedBlock],
) -> ResolvedProgram:
    scope_tree = build_scope_tree(blocks)
    resolved_blocks = resolve_macros(blocks, scope_tree)

    return ResolvedProgram(
        blocks=tuple(resolved_blocks),
        scope_tree=scope_tree,
    )


__all__ = [
    "MacroControlPayload",
    "MacroFrame",
    "ResolvedBlock",
    "ResolvedProgram",
    "ScopeNode",
    "build_scope_tree",
    "resolve",
    "resolve_macros",
]
