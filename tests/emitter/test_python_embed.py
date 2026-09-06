"""Unit tests for PythonEmbed utility."""

from __future__ import annotations

from vg2c.frontend.models import BlockOptions, ClassifiedBlock, ParsedBlock, SourceSpan
from vg2c.kind import Kind
from vg2c.resolver.models import ResolvedBlock
from vg2c.utilities._base import UtilitySpec


def _make_block(
    index: int, kind: Kind, body: str, csv: str | None = None
) -> ResolvedBlock:
    pairs = [("WRITE-FILE", "Y")]
    if csv:
        pairs.append(("CSV", csv))
    options = BlockOptions.from_pairs(pairs)
    parsed = ParsedBlock(
        index=index, options=options, body=body, raw="", span=SourceSpan(None, 1, 1)
    )
    classified = ClassifiedBlock(parsed, kind, "test")
    return ResolvedBlock(classified, options, body, None, 0)


def test_emit_block_embeds_python_body() -> None:
    code = "import os\nprint(os.getcwd())"
    block = _make_block(3, Kind.PYTHON_EMBED, code, csv="script.py")
    emission = UtilitySpec.dispatch_and_emit(block)

    assert "def step_0003_python_embed(ctx)" in emission.source
    assert "import os" in emission.source
    assert "print(os.getcwd())" in emission.source
    assert emission.call_site == "step_0003_python_embed(ctx)"


def test_emit_block_preserves_multiline_indentation() -> None:
    code = "for i in range(10):\n" "    print(i)\n" "    if i > 5:\n" "        break"
    block = _make_block(7, Kind.PYTHON_EMBED, code, csv="loop.py")
    emission = UtilitySpec.dispatch_and_emit(block)

    assert "    for i in range(10):" in emission.source
    assert "        print(i)" in emission.source
    assert "            break" in emission.source


def test_emit_block_empty_body() -> None:
    block = _make_block(1, Kind.PYTHON_EMBED, "", csv="empty.py")
    emission = UtilitySpec.dispatch_and_emit(block)

    assert "def step_0001_python_embed(ctx)" in emission.source
    assert emission.call_site == "step_0001_python_embed(ctx)"
