from __future__ import annotations

from vg2c.frontend.models import ClassifiedBlock, Diagnostic
from vg2c.resolver.macro_resolver import resolve_macros
from vg2c.resolver.models import (
    MacroControlPayload,
    MacroFrame,
    ResolvedBlock,
    ResolvedProgram,
    RuntimeMacroRef,
    ScopeNode,
    SqlMacroCall,
)
from vg2c.resolver.scope_builder import build_scope_tree
from vg2c.resolver.sql_macro_expander import expand_sql_macros


def resolve(
    blocks: list[ClassifiedBlock],
    diagnostics: list[Diagnostic] | None = None,
) -> ResolvedProgram:
    merged_diagnostics: list[Diagnostic] = list(diagnostics or [])

    scope_tree, scope_diags = build_scope_tree(blocks)
    merged_diagnostics.extend(scope_diags)

    resolved_blocks, csv_producers, csv_consumers, macro_diags = resolve_macros(
        blocks, scope_tree
    )
    merged_diagnostics.extend(macro_diags)

    resolved_blocks, csv_consumers, sql_diags = expand_sql_macros(
        blocks=resolved_blocks,
        csv_producers=csv_producers,
        csv_consumers=csv_consumers,
    )
    merged_diagnostics.extend(sql_diags)

    return ResolvedProgram(
        blocks=tuple(resolved_blocks),
        scope_tree=scope_tree,
        csv_producers=csv_producers,
        csv_consumers=csv_consumers,
        diagnostics=tuple(merged_diagnostics),
    )


__all__ = [
    "MacroControlPayload",
    "MacroFrame",
    "ResolvedBlock",
    "ResolvedProgram",
    "RuntimeMacroRef",
    "ScopeNode",
    "SqlMacroCall",
    "build_scope_tree",
    "expand_sql_macros",
    "resolve",
    "resolve_macros",
]
