from __future__ import annotations

from vg2c.frontend.models import BlockOptions, ClassifiedBlock, ParsedBlock, SourceSpan
from vg2c.kind import Kind
from vg2c.resolver.models import ResolvedBlock
from vg2c.utilities import assemble_utilities, ensure_utility_checks_loaded
from vg2c.utilities._base import UtilitySpec


def test_required_external_imports_are_deduplicated():
    embedded = assemble_utilities(
        step_emissions=(),
        workflow_source=(
            "def run(ctx):\n    ctx.csv_io.iter('a.csv')\n    ctx.csv_io.row_count('a.csv')"
        ),
        reader_names=set(),
    )
    assert len(embedded.imports) == len(set(embedded.imports))
    assert not any("vg2c" in line for line in embedded.imports)
    assert "import csv" in embedded.imports
    assert "import pandas" in embedded.imports


def _make_utility_block(
    index: int, utilities: str, body: str = "", kind: Kind = Kind.EMAIL
) -> ResolvedBlock:
    options = BlockOptions.from_pairs([("UTILITIES", utilities)])
    parsed = ParsedBlock(
        index=index,
        options=options,
        body=body,
        raw="",
        span=SourceSpan(None, 1, 1),
    )
    classified = ClassifiedBlock(parsed, kind, "test")
    return ResolvedBlock(classified, options, body, None, 0)


def test_emit_block_routes_email_utility_before_generic_fallback() -> None:
    block = _make_utility_block(
        8,
        '@EXEDIR@\\SQLPathFinder_Email.va "report.csv" "self" "Subject" '
        '"body.txt" "user@example.com" "" "" "N" "N"',
    )

    ensure_utility_checks_loaded()
    emission = UtilitySpec.dispatch_and_emit(block)

    assert "def step_0008_email(ctx)" in emission.source
    expected = (
        "ctx.email.send(to='user@example.com', subject='Subject', body='body.txt', "
        "attachments=['report.csv'])"
    )
    assert expected in emission.source
    assert emission.call_site == "step_0008_email(ctx)"


def test_emit_block_keeps_unknown_utility_fallback() -> None:
    block = _make_utility_block(9, '@EXEDIR@\\SomeOtherUtility.va "x"', kind=Kind.UNKNOWN)

    ensure_utility_checks_loaded()
    emission = UtilitySpec.dispatch_and_emit(block)

    assert "utility command not classified" in emission.source
    assert emission.call_site == "step_0009_utility(ctx)"
