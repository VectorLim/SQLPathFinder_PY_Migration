"""Options/body parsing from main, with strict failure instead of recovery."""

from __future__ import annotations

import re
from pathlib import Path

from vg2c.diagnostics import CompileError
from vg2c.frontend.models import BlockOptions, ParsedBlock, SourceSpan

SEPARATOR_RE = re.compile(r"^[ \t]*<----[ \t]*New Query[ \t]*---->[ \t]*$", re.MULTILINE)
OPEN_OPTIONS_RE = re.compile(r"^[ \t]*<OPTIONS>[ \t]*$", re.MULTILINE)
CLOSE_OPTIONS_RE = re.compile(r"^[ \t]*</OPTIONS>[ \t]*$", re.MULTILINE)
OPTION_LINE_RE = re.compile(r"^/([A-Z][A-Z0-9_\-]*)=(.*)$", re.IGNORECASE)


def parse(text: str | bytes, source: Path | None = None) -> list[ParsedBlock]:
    if isinstance(text, bytes):
        try:
            text = text.decode("utf-8-sig")
        except UnicodeDecodeError as error:
            raise CompileError(
                "encoding", "Input must be UTF-8.", SourceSpan(source, 1, 1), 0
            ) from error
    normalized = text.removeprefix("\ufeff").replace("\r\n", "\n").replace("\r", "\n")
    blocks = []
    for segment, start_line, end_line in _split_segments(normalized):
        # Empty separators occur in the real CSR input; they contain no operation.
        if not segment.strip():
            continue
        index = len(blocks)
        span = SourceSpan(source, start_line, end_line)
        options_region, body = _extract_options_and_body(segment, index, span)
        pairs = []
        seen = set()
        for line in options_region.splitlines():
            if not line.strip():
                continue
            match = OPTION_LINE_RE.fullmatch(line)
            if not match:
                raise CompileError(
                    "malformed-option", f"Invalid option line: {line!r}", span, index
                )
            key, value = match.group(1).upper(), match.group(2)
            if key in seen:
                raise CompileError(
                    "duplicate-option", f"Repeated /{key} is unsupported.", span, index
                )
            pairs.append((key, value))
            seen.add(key)
        blocks.append(ParsedBlock(index, BlockOptions.from_pairs(pairs), body, segment, span))
    return blocks


def _split_segments(text: str) -> list[tuple[str, int, int]]:
    segments = []
    cursor, line_no = 0, 1
    for match in SEPARATOR_RE.finditer(text):
        segment = text[cursor : match.start()]
        end_line = line_no + segment.count("\n")
        segments.append((segment, line_no, end_line))
        line_no = end_line
        cursor = match.end()
        if cursor < len(text) and text[cursor] == "\n":
            cursor += 1
            line_no += 1
    segment = text[cursor:]
    segments.append((segment, line_no, line_no + segment.count("\n")))
    return segments


def _extract_options_and_body(segment: str, index: int, span: SourceSpan) -> tuple[str, str]:
    opens = list(OPEN_OPTIONS_RE.finditer(segment))
    closes = list(CLOSE_OPTIONS_RE.finditer(segment))
    if opens or closes:
        if len(opens) != 1 or len(closes) != 1 or opens[0].end() > closes[0].start():
            raise CompileError(
                "options-structure", "Expected one matching <OPTIONS> pair.", span, index
            )
        if segment[: opens[0].start()].strip():
            raise CompileError(
                "options-prefix", "Content before <OPTIONS> is unsupported.", span, index
            )
        options = segment[opens[0].end() : closes[0].start()]
        body = segment[closes[0].end() :]
    else:
        lines = segment.lstrip("\n").splitlines(keepends=True)
        split = 0
        while split < len(lines) and lines[split].startswith("/"):
            split += 1
        options, body = "".join(lines[:split]), "".join(lines[split:])
    # Original task parsing removes leading whitespace but retains the ending.
    return options, body.lstrip()
