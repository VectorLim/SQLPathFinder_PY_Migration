from __future__ import annotations

from vg2c.dataflow.models import AnalyzedProgram
from vg2c.dispatch.dialect import (
    SQL_BEARING_KINDS,
    derive_from_signals,
    resolve_dialect,
)
from vg2c.dispatch.dispatcher import build_target
from vg2c.dispatch.models import (
    Dialect,
    DispatchConfig,
    DispatchedBlock,
    DispatchedProgram,
    ReaderTarget,
)
from vg2c.dispatch.schema import substitute
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
        # --- Step 1: resolve dialect ---
        dialect = resolve_dialect(block.kind)
        is_fallback = False

        if dialect is None and block.kind is Kind.UNKNOWN:
            opts = block.resolved_options.lookup
            dialect = derive_from_signals(
                node=opts.get("NODE", ""),
                engine=opts.get("ENGINE", ""),
                oledb=opts.get("OLEDB", ""),
            )
            is_fallback = dialect is not None

        if dialect is None:
            continue  # Non-SQL block; no DispatchedBlock emitted

        # --- Step 2: one-shot ARIES note ---
        if dialect == "oracle_aries" and not _aries_warned:
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
                        f"dialect derived from option signals as {dialect!r}."
                    ),
                    block_index=block.parsed.index,
                    span=block.parsed.span,
                )
            )

        # --- Step 4: schema substitution ---
        rewritten_sql, schema_diags = substitute(
            body=block.resolved_body,
            dialect=dialect,
            config=config,
            span=block.parsed.span,
            block_index=block.parsed.index,
        )
        diagnostics.extend(schema_diags)

        # --- Step 5: reader target ---
        reader_target, target_diags = build_target(block, dialect)
        diagnostics.extend(target_diags)

        dispatched.append(
            DispatchedBlock(
                block_index=block.parsed.index,
                dialect=dialect,
                reader_target=reader_target,
                rewritten_sql=rewritten_sql,
            )
        )

    return DispatchedProgram(
        analyzed=analyzed,
        dispatched=tuple(dispatched),
        diagnostics=tuple(diagnostics),
    )
