from __future__ import annotations

from vg2c.classifier.model import (
    ClassificationReport,
    ClassifiedBlock,
    Diagnostic,
    Kind,
    Role,
    UnknownSpec,
)
from vg2c.classifier.rules import RULE_CHAIN
from vg2c.model import ParsedBlock


def classify_block(block: ParsedBlock) -> ClassifiedBlock:
    """Classify a single parsed block."""
    for rule in RULE_CHAIN:
        m = rule.match(block)
        if m is not None:
            return ClassifiedBlock(
                parsed=block,
                kind=m.kind,
                role=m.role,
                spec=m.spec,
                reason=m.reason,
            )

    spec = UnknownSpec(reason="no rule matched", options_seen=dict(block.options))
    return ClassifiedBlock(
        parsed=block,
        kind=Kind.UNKNOWN,
        role=Role.LEAF,
        spec=spec,
        reason="no rule matched",
    )


def classify_all(blocks: list[ParsedBlock]) -> ClassificationReport:
    """Classify all blocks in a script and generate diagnostics."""
    classified = [classify_block(b) for b in blocks]
    diagnostics = [
        Diagnostic(
            block_index=cb.parsed.index,
            span=cb.parsed.span,
            severity="warn",
            message=cb.reason,
        )
        for cb in classified
        if cb.kind == Kind.UNKNOWN
    ]
    return ClassificationReport(blocks=classified, diagnostics=diagnostics)
