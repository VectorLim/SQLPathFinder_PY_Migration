from __future__ import annotations

from vg2c.emitter.utilities import EmitterUtility
from vg2c.frontend.models import (
    BlockOptions,
    ClassifiedBlock,
    Diagnostic,
    ParsedBlock,
)
from vg2c.kind import Kind


def classify(
    blocks: list[ParsedBlock],
) -> tuple[list[ClassifiedBlock], list[Diagnostic]]:
    diagnostics: list[Diagnostic] = []
    classified: list[ClassifiedBlock] = []

    for block in blocks:
        result = _classify_one(block.options)
        if result is None:
            classified.append(
                ClassifiedBlock(
                    parsed=block, kind=Kind.UNKNOWN, reason="no rule matched"
                )
            )
            diagnostics.append(
                Diagnostic(
                    severity="warning",
                    code="unknown-kind",
                    message="Block did not match any known Stage 1 classification rule.",
                    block_index=block.index,
                    span=block.span,
                )
            )
            continue

        kind, reason = result
        classified.append(ClassifiedBlock(parsed=block, kind=kind, reason=reason))

    return classified, diagnostics


def _classify_one(options: BlockOptions) -> tuple[Kind, str] | None:
    for utility_cls in EmitterUtility.iter_checks():
        outcome = utility_cls.check(options)
        if outcome is not None:
            return outcome
    return None
