from __future__ import annotations

from vg2c.frontend.models import ClassifiedBlock, Diagnostic
from vg2c.resolver.macro_resolver import resolve_macros
from vg2c.resolver.models import (
    ResolvedBlock,
    ResolvedProgram,
    SqlMacroCall,
)
from vg2c.resolver.operands import (
    MacroControlPayload,
    MacroFrame,
    ScopeNode,
)
from vg2c.resolver.scope_builder import build_scope_tree


def resolve(
    blocks: list[ClassifiedBlock],
    diagnostics: list[Diagnostic] | None = None,
) -> ResolvedProgram:
    merged_diagnostics: list[Diagnostic] = list(diagnostics or [])

    scope_tree, scope_diags = build_scope_tree(blocks)
    merged_diagnostics.extend(scope_diags)

    resolved_blocks, macro_diags = resolve_macros(blocks, scope_tree)
    merged_diagnostics.extend(macro_diags)

    return ResolvedProgram(
        blocks=tuple(resolved_blocks),
        scope_tree=scope_tree,
        diagnostics=tuple(merged_diagnostics),
    )


__all__ = [
    "MacroControlPayload",
    "MacroFrame",
    "ResolvedBlock",
    "ResolvedProgram",
    "ScopeNode",
    "SqlMacroCall",
    "build_scope_tree",
    "resolve",
    "resolve_macros",
]
