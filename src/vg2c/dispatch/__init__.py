from __future__ import annotations

from vg2c.dataflow.models import AnalyzedProgram
from vg2c.dispatch import dialects as _dialects  # noqa: F401
from vg2c.dispatch.base import DialectHandler
from vg2c.dispatch.models import (
    DispatchedBlock,
    DispatchedProgram,
    ReaderTarget,
)
from vg2c.frontend.models import Diagnostic, Kind

__all__ = [
    "dispatch",
    "DispatchedBlock",
    "DispatchedProgram",
    "DialectHandler",
    "ReaderTarget",
]


def dispatch(
    analyzed: AnalyzedProgram,
) -> DispatchedProgram:
    """Stage 4 entry point: resolve dialects, substitute schemas, build reader targets.

    Args:
        analyzed: Output from Stage 3 ``analyze()``.

    Returns:
        A ``DispatchedProgram`` wrapping *analyzed* and adding per-SQL-block
        dispatch metadata plus merged Stage 1–4 diagnostics.
    """
    diagnostics: list[Diagnostic] = list(analyzed.diagnostics)
    dispatched: list[DispatchedBlock] = []

    for block in analyzed.resolved.blocks:
        # --- Step 1: resolve handler ---
        opts = block.resolved_options.lookup

        if block.kind is Kind.SQL_QUERY:
            handler = DialectHandler.derive_handler_from_signals(
                node=opts.get("NODE", ""),
                engine=opts.get("ENGINE", ""),
                oledb=opts.get("OLEDB", ""),
            )
        else:
            handler = DialectHandler.for_kind(block.kind)

        if handler is None and block.kind is Kind.UNKNOWN:
            handler = DialectHandler.derive_handler_from_signals(
                node=opts.get("NODE", ""),
                engine=opts.get("ENGINE", ""),
                oledb=opts.get("OLEDB", ""),
            )

        if handler is None:
            continue  # Non-SQL block; no DispatchedBlock emitted

        # --- Step 4: schema substitution ---
        rewritten_sql = handler.substitute(body=block.resolved_body)

        # --- Step 6: reader target ---
        reader_target, target_diags = handler.build_reader_target(block)
        diagnostics.extend(target_diags)
        dispatched.append(
            DispatchedBlock(
                block_index=block.parsed.index,
                reader_cls=handler.reader_cls,
                reader_kwargs=handler.reader_kwargs,
                reader_target=reader_target,
                rewritten_sql=rewritten_sql,
            )
        )

    return DispatchedProgram(
        analyzed=analyzed,
        dispatched=tuple(dispatched),
        diagnostics=tuple(diagnostics),
    )
