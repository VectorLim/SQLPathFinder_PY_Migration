"""Compile-time failures with the original block's source location."""

from __future__ import annotations

from typing import TYPE_CHECKING

if TYPE_CHECKING:
    from vg2c.frontend.models import SourceSpan


class CompileError(ValueError):
    def __init__(self, code: str, message: str, span: SourceSpan, block_index: int):
        self.code = code
        self.span = span
        self.block_index = block_index
        location = f"{span.file or '<input>'}:{span.start_line}:1"
        super().__init__(f"[{code}] {location} (block {block_index}): {message}")
