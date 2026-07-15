from __future__ import annotations

import re
from pathlib import Path

from vg2c.logger import Logger
from vg2c.frontend.models import BlockOptions, ParsedBlock, SourceSpan

log = Logger.getLogger("vg2c.frontend.parser")

SEPARATOR_RE = re.compile(
    r"^[ \t]*<----[ \t]*New Query[ \t]*---->[ \t]*$", re.MULTILINE
)
OPEN_OPTIONS_RE = re.compile(r"^[ \t]*<OPTIONS>[ \t]*$", re.MULTILINE)
CLOSE_OPTIONS_RE = re.compile(r"^[ \t]*</OPTIONS>[ \t]*$", re.MULTILINE)
OPTION_LINE_RE = re.compile(r"^/([A-Z][A-Z0-9_\-]*)=(.*)$", re.IGNORECASE)


def _log_msg(
    code: str,
    message: str,
    block_index: int | None = None,
    span: SourceSpan | None = None,
) -> str:
    if span is not None and span.file is not None:
        loc = f"{span.file}:{span.start_line}:1"
    elif span is not None:
        loc = f"<input>:{span.start_line}:1"
    else:
        loc = "<input>"

    block_info = f" (block {block_index})" if block_index is not None else ""
    return f"[{code}] {loc}{block_info}: {message}"


def parse(text: str | bytes, source: Path | None = None) -> list[ParsedBlock]:
    normalized = _normalize_input(text)

    blocks: list[ParsedBlock] = []
    for segment, start_line, end_line in _split_segments(normalized):
        span = SourceSpan(file=source, start_line=start_line, end_line=end_line)
        if segment.strip() == "":
            log.warning(
                _log_msg(
                    code="empty-block",
                    message="Ignored an empty block between query separators.",
                    span=span,
                )
            )
            continue

        block_index = len(blocks)
        options_region, body_region = _extract_options_and_body(
            segment=segment,
            block_index=block_index,
            span=span,
        )
        options = _parse_options(
            options_region=options_region,
            block_index=block_index,
            span=span,
        )

        blocks.append(
            ParsedBlock(
                index=block_index,
                options=options,
                body=_trim_outer_blank_line(body_region),
                raw=segment,
                span=span,
            )
        )

    return blocks


def _normalize_input(text: str | bytes) -> str:
    normalized = (
        text.decode("utf-8", errors="replace") if isinstance(text, bytes) else text
    )

    if normalized.startswith("\ufeff"):
        normalized = normalized[1:]

    return normalized.replace("\r\n", "\n").replace("\r", "\n")


def _split_segments(text: str) -> list[tuple[str, int, int]]:
    segments: list[tuple[str, int, int]] = []
    cursor = 0
    line_no = 1

    for match in SEPARATOR_RE.finditer(text):
        segment = text[cursor : match.start()]
        start_line = line_no
        end_line = start_line + segment.count("\n")
        segments.append((segment, start_line, end_line))

        consumed = text[match.start() : match.end()]
        line_no = end_line + consumed.count("\n")
        cursor = match.end()
        if cursor < len(text) and text[cursor] == "\n":
            cursor += 1
            line_no += 1

    segment = text[cursor:]
    start_line = line_no
    end_line = start_line + segment.count("\n")
    segments.append((segment, start_line, end_line))
    return segments


def _extract_options_and_body(
    segment: str,
    block_index: int,
    span: SourceSpan,
) -> tuple[str, str]:
    open_match = OPEN_OPTIONS_RE.search(segment)
    if open_match:
        close_match = CLOSE_OPTIONS_RE.search(segment, open_match.end())
        if close_match:
            return (
                segment[open_match.end() : close_match.start()],
                segment[close_match.end() :],
            )

        log.error(
            _log_msg(
                code="unclosed-options",
                message="Found <OPTIONS> without a matching </OPTIONS>.",
                block_index=block_index,
                span=span,
            )
        )
        return segment[open_match.end() :], ""

    log.info(
        _log_msg(
            code="inline-options",
            message="Parsed block using inline option lines (no <OPTIONS> markers).",
            block_index=block_index,
            span=span,
        )
    )
    return _split_inline_options(segment)


def _split_inline_options(segment: str) -> tuple[str, str]:
    lines = segment.splitlines(keepends=True)
    option_lines: list[str] = []
    body_lines: list[str] = []
    in_options = True

    for line in lines:
        if in_options and line.startswith("/"):
            option_lines.append(line)
            continue

        in_options = False
        body_lines.append(line)

    return "".join(option_lines), "".join(body_lines)


def _parse_options(
    options_region: str,
    block_index: int,
    span: SourceSpan,
) -> BlockOptions:
    pairs: list[tuple[str, str]] = []
    seen_keys: set[str] = set()
    duplicate_reported: set[str] = set()

    for raw_line in options_region.split("\n"):
        line = raw_line.rstrip("\r").rstrip()
        if line.strip() == "":
            continue

        option_match = OPTION_LINE_RE.match(line)
        if not option_match:
            log.warning(
                _log_msg(
                    code="malformed-option-line",
                    message=f"Ignored malformed option line: {line}",
                    block_index=block_index,
                    span=span,
                )
            )
            continue

        key = option_match.group(1).upper()
        value = option_match.group(2)
        pairs.append((key, value))

        if key in seen_keys and key not in duplicate_reported:
            log.info(
                _log_msg(
                    code="duplicate-option-key",
                    message=f"Option key /{key} appears more than once in this block.",
                    block_index=block_index,
                    span=span,
                )
            )
            duplicate_reported.add(key)
        seen_keys.add(key)

    return BlockOptions.from_pairs(pairs)


def _trim_outer_blank_line(text: str) -> str:
    if text.startswith("\n"):
        text = text[1:]
    if text.endswith("\n"):
        text = text[:-1]
    return text
