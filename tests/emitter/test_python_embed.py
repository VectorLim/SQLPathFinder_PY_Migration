"""Unit tests for PythonEmbed utility."""

from __future__ import annotations

from vg2c.emitter.utilities.python_embed import PythonEmbed
from vg2c.emitter.utilities._emit_helpers import _step_name
from vg2c.frontend.models import BlockOptions, ClassifiedBlock, ParsedBlock, SourceSpan
from vg2c.kind import Kind
from vg2c.resolver.models import ResolvedBlock


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
    return ResolvedBlock(classified, options, body, (), None, 0)


def test_emit_block_embeds_python_body() -> None:
    code = "import os\nprint(os.getcwd())"
    block = _make_block(3, Kind.PYTHON_EMBED, code, csv="script.py")
    result = PythonEmbed.emit_block(block)

    assert result is not None
    func_source, call_site = result
    assert "def step_0003_python_embed(ctx)" in func_source
    assert "import os" in func_source
    assert "print(os.getcwd())" in func_source
    assert call_site == "step_0003_python_embed(ctx)"


def test_emit_block_preserves_multiline_indentation() -> None:
    code = (
        "for i in range(10):\n"
        "    print(i)\n"
        "    if i > 5:\n"
        "        break"
    )
    block = _make_block(7, Kind.PYTHON_EMBED, code, csv="loop.py")
    result = PythonEmbed.emit_block(block)

    assert result is not None
    func_source, _ = result
    # Body lines should appear indented under the def
    assert "    for i in range(10):" in func_source
    assert "        print(i)" in func_source
    assert "            break" in func_source


def test_emit_block_empty_body() -> None:
    block = _make_block(1, Kind.PYTHON_EMBED, "", csv="empty.py")
    result = PythonEmbed.emit_block(block)

    assert result is not None
    func_source, call_site = result
    assert "def step_0001_python_embed(ctx)" in func_source
    assert call_site == "step_0001_python_embed(ctx)"
