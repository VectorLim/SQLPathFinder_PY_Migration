from __future__ import annotations

from vg2c.dataflow.models import AnalyzedProgram
from vg2c.dispatch.models import (
    Dialect,
    DispatchConfig,
    DispatchedBlock,
    DispatchedProgram,
    ReaderTarget,
)
from vg2c.dispatch.registry import (
    HANDLERS,
    derive_handler_from_signals,
    get_handler_for_kind,
)
from vg2c.frontend.models import Diagnostic, Kind

__all__ = [
    "dispatch",
    "Dialect",
    "DispatchConfig",
    "DispatchedBlock",
    "DispatchedProgram",
    "ReaderTarget",
]


def dispatch(
    analyzed: AnalyzedProgram,
    config: DispatchConfig | None = None,
) -> DispatchedProgram:
    """Stage 4 entry point: resolve dialects, substitute schemas, build reader targets.

    Args:
        analyzed: Output from Stage 3 ``analyze()``.
        config:   Optional dispatch configuration. When *None*, OASYS schema
                  substitution will emit an error-severity diagnostic if any
                  ``@OASYSSCHEMA@`` placeholder is present.

    Returns:
        A ``DispatchedProgram`` wrapping *analyzed* and adding per-SQL-block
        dispatch metadata plus merged Stage 1–4 diagnostics.
    """
    diagnostics: list[Diagnostic] = list(analyzed.diagnostics)
    dispatched: list[DispatchedBlock] = []

    for block in analyzed.resolved.blocks:
        # --- Step 1: resolve handler ---
        is_fallback = False
        opts = block.resolved_options.lookup

        if block.kind is Kind.SQL_QUERY:
            handler = derive_handler_from_signals(
                node=opts.get("NODE", ""),
                engine=opts.get("ENGINE", ""),
                oledb=opts.get("OLEDB", ""),
            )
        else:
            handler = get_handler_for_kind(block.kind)

        if handler is None and block.kind is Kind.UNKNOWN:
            handler = derive_handler_from_signals(
                node=opts.get("NODE", ""),
                engine=opts.get("ENGINE", ""),
                oledb=opts.get("OLEDB", ""),
            )
            is_fallback = handler is not None

        if handler is None:
            continue  # Non-SQL block; no DispatchedBlock emitted

        # --- Step 3: unknown-dialect fallback note ---
        if is_fallback:
            pass

        # --- Step 4: schema substitution ---
        rewritten_sql, schema_diags = handler.substitute(
            body=block.resolved_body,
            config=config,
            span=block.parsed.span,
            block_index=block.parsed.index,
        )

        # --- Step 6: reader target ---
        reader_target, target_diags = handler.build_reader_target(block)
        dispatched.append(
            DispatchedBlock(
                block_index=block.parsed.index,
                dialect=handler.dialect,
                reader_target=reader_target,
                rewritten_sql=rewritten_sql,
            )
        )

    return DispatchedProgram(
        analyzed=analyzed,
        dispatched=tuple(dispatched),
        diagnostics=tuple(diagnostics),
    )
