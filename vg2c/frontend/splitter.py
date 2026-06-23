from __future__ import annotations

import re
from dataclasses import dataclass

from vg2c.model import SourceSpan

_INLINE_HEADER_RE = re.compile(r"^/[A-Z_][A-Z0-9_-]*=")
_DELIMITER = "<---- New Query ---->"


@dataclass(frozen=True)
class RawBlock:
    index: int
    span: SourceSpan
    header_text: str
    body_text: str
    raw_text: str


def split_blocks(text: str, file: str) -> list[RawBlock]:
    """Split normalized VG2 text into raw blocks with source spans."""
    lines = text.splitlines(keepends=True)
    blocks: list[RawBlock] = []
    i = 0

    def emit_block(
        start_idx: int,
        end_exclusive: int,
        header_text: str,
        body_start_idx: int,
        body_end_exclusive: int,
    ) -> None:
        body_text = "".join(lines[body_start_idx:body_end_exclusive])
        raw_text = "".join(lines[start_idx:end_exclusive])
        if not header_text and not body_text:
            return

        blocks.append(
            RawBlock(
                index=len(blocks),
                span=SourceSpan(file=file, start_line=start_idx + 1, end_line=end_exclusive),
                header_text=header_text,
                body_text=body_text,
                raw_text=raw_text,
            )
        )

    while i < len(lines):
        while i < len(lines) and lines[i].strip() == "":
            i += 1
        if i >= len(lines):
            break

        block_start = i
        header_text = ""

        stripped = lines[i].strip()
        if stripped.startswith("<OPTIONS>"):
            header_lines = [lines[i]]
            i += 1
            while i < len(lines):
                header_lines.append(lines[i])
                if "</OPTIONS>" in lines[i]:
                    i += 1
                    break
                i += 1
            header_text = "".join(header_lines)
            body_start = i
        elif _INLINE_HEADER_RE.match(stripped):
            header_text = lines[i]
            i += 1
            body_start = i
        else:
            body_start = i

        while i < len(lines):
            if lines[i].strip() == _DELIMITER:
                emit_block(
                    start_idx=block_start,
                    end_exclusive=i,
                    header_text=header_text,
                    body_start_idx=body_start,
                    body_end_exclusive=i,
                )
                i += 1
                break
            i += 1
        else:
            emit_block(
                start_idx=block_start,
                end_exclusive=len(lines),
                header_text=header_text,
                body_start_idx=body_start,
                body_end_exclusive=len(lines),
            )
            break

    return blocks
