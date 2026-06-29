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

_ARIES_NOTE = (
    "oracle_aries dialect encountered; ARIES classification rule has no dedicated test "
    "fixture (see Stage 1 aries-rule-untested). Results are speculative."
)


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
    _aries_warned = False

    for block in analyzed.resolved.blocks:
        # --- Step 1: resolve handler ---
        handler = get_handler_for_kind(block.kind)
        is_fallback = False

        if handler is None and block.kind is Kind.UNKNOWN:
            opts = block.resolved_options.lookup
            handler = derive_handler_from_signals(
                node=opts.get("NODE", ""),
                engine=opts.get("ENGINE", ""),
                oledb=opts.get("OLEDB", ""),
            )
            is_fallback = handler is not None

        if handler is None:
            continue  # Non-SQL block; no DispatchedBlock emitted

        # --- Step 2: one-shot ARIES note ---
        if handler.dialect == "oracle_aries" and not _aries_warned:
            _aries_warned = True
            diagnostics.append(
                Diagnostic(
                    severity="info",
                    code="dispatch-aries-rule-untested",
                    message=_ARIES_NOTE,
                    block_index=block.parsed.index,
                    span=block.parsed.span,
                )
            )

        # --- Step 3: unknown-dialect fallback note ---
        if is_fallback:
            diagnostics.append(
                Diagnostic(
                    severity="info",
                    code="dispatch-unknown-dialect",
                    message=(
                        f"Block {block.parsed.index} has Kind.UNKNOWN; "
                        f"dialect derived from option signals as {handler.dialect!r}."
                    ),
                    block_index=block.parsed.index,
                    span=block.parsed.span,
                )
            )

        # --- Step 4: schema substitution ---
        rewritten_sql, schema_diags = handler.substitute(
            body=block.resolved_body,
            config=config,
            span=block.parsed.span,
            block_index=block.parsed.index,
        )
        diagnostics.extend(schema_diags)

        # --- Step 5: cross-dialect mismatch check ---
        for other_handler in HANDLERS.values():
            if other_handler is handler:
                continue
            if other_handler.has_own_placeholders(block.resolved_body):
                diagnostics.append(
                    Diagnostic(
                        severity="warning",
                        code="dispatch-placeholder-dialect-mismatch",
                        message=(
                            f"{other_handler.schema_placeholder} schema placeholder found in "
                            f"{handler.dialect} block; expected {other_handler.dialect} context."
                        ),
                        block_index=block.parsed.index,
                        span=block.parsed.span,
                    )
                )

        # --- Step 6: reader target ---
        reader_target, target_diags = handler.build_reader_target(block)
        diagnostics.extend(target_diags)

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
