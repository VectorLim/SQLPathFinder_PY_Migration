from __future__ import annotations

from vg2c import logger
from vg2c.utilities import EmitterUtility, ensure_utility_checks_loaded
from vg2c.frontend.models import (
    BlockOptions,
    ClassifiedBlock,
    ParsedBlock,
)
from vg2c.kind import Kind

log = logger.getLogger("vg2c.frontend.classifier")


def classify(
    blocks: list[ParsedBlock],
) -> list[ClassifiedBlock]:
    ensure_utility_checks_loaded()
    classified: list[ClassifiedBlock] = []

    for block in blocks:
        result = _classify_one(block.options)
        if result is None:
            classified.append(
                ClassifiedBlock(
                    parsed=block, kind=Kind.UNKNOWN, reason="no rule matched"
                )
            )
            loc = f"{block.span.file or '<input>'}:{block.span.start_line}:1"
            log.warning(
                f"[unknown-kind] {loc} (block {block.index}): "
                "Block did not match any known Stage 1 classification rule."
            )
            continue

        kind, reason = result
        classified.append(ClassifiedBlock(parsed=block, kind=kind, reason=reason))

    return classified


def _classify_one(options: BlockOptions) -> tuple[Kind, str] | None:
    for utility_cls in EmitterUtility.iter_checks():
        outcome = utility_cls.check(options)
        if outcome is not None:
            return outcome
    return None
