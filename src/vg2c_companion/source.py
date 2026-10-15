"""Source snapshots, UTF-16 offsets, and bounded compiler source mapping."""

from __future__ import annotations

import re
from dataclasses import dataclass
from typing import Any


@dataclass(frozen=True, slots=True)
class SourceMapping:
    block_index: int | None
    generated_offset: int | None
    generated_range: tuple[int, int] | None
    asset_names: tuple[str, ...] = ()
    reason: str = ""


def map_designer_label(
    result: Any,
    label: str,
    *,
    observed_pid: int | None,
    compiled_pid: int | None,
    observed_title: str,
    compiled_title: str,
) -> SourceMapping:
    """Map only the verified single main-query designer label."""
    if label != "1. Main Query":
        return SourceMapping(None, None, None, reason="Designer label has no explicit mapping.")
    if not observed_pid or observed_pid != compiled_pid or observed_title != compiled_title:
        return SourceMapping(None, None, None, reason="Designer/process context does not match the preview.")
    steps = tuple(getattr(result.emitted, "steps", ()))
    query_steps = tuple(
        step for step in steps
        if str(getattr(step, "functional_kind", "")).lower() in {
            "sql_query", "sql-query", "sqlite_query", "sqlite-query"
        }
    )
    if len(query_steps) != 1:
        return SourceMapping(None, None, None, reason="Main-query mapping is ambiguous.")
    step = query_steps[0]
    return SourceMapping(
        block_index=step.block_index,
        generated_offset=step.source_range.start_offset,
        generated_range=(step.source_range.start_offset, step.source_range.end_offset),
        reason="Mapped the exact SQLPathFinder main-query label.",
    )


def utf16_to_python_offset(text: str, offset: int | None) -> int | None:
    """Convert a UI Automation UTF-16 code-unit offset to a Python offset."""
    if offset is None:
        return None
    if offset <= 0:
        return 0
    units = 0
    for index, character in enumerate(text):
        next_units = units + (2 if ord(character) > 0xFFFF else 1)
        if next_units > offset:
            return index
        units = next_units
        if units == offset:
            return index + 1
    return len(text)


def python_to_utf16_offset(text: str, offset: int | None) -> int | None:
    if offset is None:
        return None
    offset = max(0, min(offset, len(text)))
    return sum(2 if ord(character) > 0xFFFF else 1 for character in text[:offset])


def _line_at(text: str, offset: int) -> int:
    return text.count("\n", 0, max(0, min(offset, len(text)))) + 1


def _is_separator_line(text: str, line: int) -> bool:
    lines = text.splitlines()
    return 1 <= line <= len(lines) and bool(
        re.fullmatch(r"[ \t]*<----[ \t]*New Query[ \t]*---->[ \t]*", lines[line - 1])
    )


def map_source_offset(result: Any, source_text: str, source_offset: int) -> SourceMapping:
    """Map a source caret to the emitted step range owned by its block.

    The compiler intentionally exposes block line spans and emitted step ranges;
    this adapter does not infer mappings from generated text or labels.
    """
    source_offset = max(0, min(source_offset, len(source_text)))
    line = _line_at(source_text, source_offset)
    if _is_separator_line(source_text, line):
        return SourceMapping(None, None, None, reason="Query separator has no source block mapping.")
    # shortcut: compiler blocks expose line spans only; upgrade when character spans are public.
    blocks = tuple(getattr(result.dispatched.resolved, "blocks", ()))
    block = next(
        (
            item
            for item in blocks
            if item.span.start_line <= line <= max(item.span.end_line, item.span.start_line)
        ),
        None,
    )
    if block is None:
        return SourceMapping(None, None, None, reason="No compiler block covers this source line.")
    step = result.emitted.step_for_block(block.index)
    if step is None:
        return SourceMapping(block.index, None, None, reason="Compiler emitted no step for this block.")
    asset_names = tuple(name for name, _content in result.emitted.assets)
    owned: list[str] = []
    for parameter in step.parameters:
        if parameter.artifact_role is None or not isinstance(parameter.value, str):
            continue
        value_name = parameter.value.replace("\\", "/")
        if value_name in asset_names and value_name not in owned:
            owned.append(value_name)
    start, end = step.source_range.start_offset, step.source_range.end_offset
    return SourceMapping(
        block_index=block.index,
        generated_offset=start,
        generated_range=(start, end),
        asset_names=tuple(owned),
        reason=f"Mapped source line {line} to block {block.index}.",
    )

