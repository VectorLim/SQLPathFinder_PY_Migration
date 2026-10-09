"""Job text as the original runtime reads it, and the source lines of each block."""

from __future__ import annotations

from pathlib import Path

from scripthost_portable.task_inputs import task_delimiter
from vg2c.diagnostics import CompileError
from vg2c.frontend.models import SourceSpan


def read_source(
    data: bytes, source: Path | None = None
) -> tuple[str, tuple[SourceSpan, ...]]:
    """Decoded text (UTF-8, universal newlines) and one span per delimiter-split block index."""
    try:
        text = data.decode("utf-8-sig")
    except UnicodeDecodeError as error:
        raise CompileError(
            "encoding", "Input must be UTF-8.", SourceSpan(source, 1, 1), 0
        ) from error
    text = text.replace("\r\n", "\n").replace("\r", "\n")
    spans, line = [], 1
    for segment in text.split(task_delimiter()):
        leading = len(segment) - len(segment.lstrip())
        end = line + segment.count("\n")
        spans.append(SourceSpan(source, line + segment.count("\n", 0, leading), end))
        line = end
    return text, tuple(spans)
