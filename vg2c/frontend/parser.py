from __future__ import annotations

from pathlib import Path

from vg2c.frontend.options import parse_options
from vg2c.frontend.reader import normalize_collapsed_lines, read_vg2
from vg2c.frontend.splitter import split_blocks
from vg2c.model import ParsedBlock


def parse_vg2(path: str | Path) -> list[ParsedBlock]:
    """Parse a VG2 file into structured blocks."""
    p = Path(path)
    text = normalize_collapsed_lines(read_vg2(p))
    raw_blocks = split_blocks(text, file=str(p))
    return [
        ParsedBlock(
            index=rb.index,
            span=rb.span,
            options=parse_options(rb.header_text),
            body=rb.body_text,
            raw=rb.raw_text,
        )
        for rb in raw_blocks
    ]
