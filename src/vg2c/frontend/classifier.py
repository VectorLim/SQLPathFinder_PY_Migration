from __future__ import annotations

from vg2c.frontend.models import (
    BlockOptions,
    ClassifiedBlock,
    ParsedBlock,
)
from vg2c.kind import Kind
from vg2c.logger import Logger
from vg2c.utilities import EmitterUtility, ensure_utility_checks_loaded

log = Logger.getLogger("vg2c.frontend.classifier")


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
                    parsed=block, kind=Kind.UNKNOWN
                )
            )
            loc = f"{block.span.file or '<input>'}:{block.span.start_line}:1"
            log.warning(
                f"[unknown-kind] {loc} (block {block.index}): "
                "Block did not match any known Stage 1 classification rule."
            )
            continue

        classified.append(ClassifiedBlock(parsed=block, kind=result))

    return classified


def _classify_one(options: BlockOptions) -> Kind | None:
    for utility_cls in EmitterUtility.iter_checks():
        outcome = utility_cls.check(options)
        if outcome is not None:
            return outcome
    return None
