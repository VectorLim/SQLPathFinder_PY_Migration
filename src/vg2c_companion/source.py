"""Source snapshots, UTF-16 offsets, and bounded compiler source mapping."""

from __future__ import annotations

import re
import tokenize
from ast import literal_eval
from dataclasses import dataclass
from io import StringIO
from typing import Any


@dataclass(frozen=True, slots=True)
class SourceMapping:
    block_index: int | None
    generated_offset: int | None
    generated_range: tuple[int, int] | None
    asset_names: tuple[str, ...] = ()
    reason: str = ""


@dataclass(frozen=True, slots=True)
class GeneratedAssetLink:
    """An emitted literal whose declared artifact exists in this preview."""

    name: str
    source_range: tuple[int, int]


_STEP_ID = r"[0-9]+(?:[.-][0-9A-Za-z]+)*"


def designer_document(title: str) -> str:
    """Use the displayed document and level; SQLPF can retain stale Pre/Post title text."""
    match = re.search(r" - \[(.+\.vg2(?:\s+\(L[0-9]+\))?)\]$", title, re.IGNORECASE)
    return match.group(1).casefold() if match else title


def is_prepost_child(title: str, parent_title: str) -> bool:
    if "pre/post query" not in title.casefold():
        return False
    level = re.search(r"\(L([0-9]+)\)\]$", title, re.IGNORECASE)
    parent_level = re.search(r"\(L([0-9]+)\)\]$", parent_title, re.IGNORECASE)
    if level and parent_level:
        return int(level.group(1)) > int(parent_level.group(1))
    return designer_document(title) != designer_document(parent_title)


def _prompt_step_id(block: Any) -> str | None:
    match = re.match(
        r"^Step\s+(" + _STEP_ID + r")\.(?:\s|$)",
        str(block.resolved_options.lookup.get("PROMPT-TEXT", "")),
    )
    return match.group(1) if match else None


def generated_asset_links(result: Any) -> tuple[GeneratedAssetLink, ...]:
    """Return generated string literals that name an emitted asset exactly."""
    source = result.emitted.source
    asset_names = {name for name, _content in result.emitted.assets if isinstance(name, str)}
    line_starts = [0]
    for index, character in enumerate(source):
        if character == "\n":
            line_starts.append(index + 1)
    links: list[GeneratedAssetLink] = []
    try:
        tokens = tokenize.generate_tokens(StringIO(source).readline)
        for item in tokens:
            if item.type != tokenize.STRING:
                continue
            try:
                value = literal_eval(item.string)
            except (SyntaxError, TypeError, ValueError):
                continue
            if not isinstance(value, str) or value not in asset_names:
                continue
            start = line_starts[item.start[0] - 1] + item.start[1]
            end = line_starts[item.end[0] - 1] + item.end[1]
            links.append(GeneratedAssetLink(value, (start, end)))
    except (IndentationError, tokenize.TokenError):
        pass
    return tuple(links)


def map_generated_offset_to_asset(result: Any, offset: int) -> str | None:
    """Return one emitted asset for a clicked generated-source offset, if unambiguous."""
    matches = tuple(
        link.name for link in generated_asset_links(result)
        if link.source_range[0] <= offset < link.source_range[1]
    )
    return matches[0] if len(matches) == 1 else None


def map_designer_label(
    result: Any,
    label: str,
    *,
    observed_pid: int | None,
    compiled_pid: int | None,
    observed_title: str,
    compiled_title: str,
) -> SourceMapping:
    """Map a designer's own qualified step ID through compiler prompt metadata."""
    if not observed_pid or observed_pid != compiled_pid or designer_document(observed_title) != designer_document(compiled_title):
        return SourceMapping(None, None, None, reason="Designer/process context does not match the preview.")
    match = re.match(r"^(" + _STEP_ID + r")\.\s+", label)
    blocks = tuple(getattr(result.dispatched.resolved, "blocks", ()))
    is_main_query = bool(match and label[match.end():].strip().lower() == "main query")
    is_prepost_query = bool(
        match and re.match(r"pre/post\s+query(?:\s|$)", label[match.end():].strip(), re.IGNORECASE)
    )
    if is_prepost_query:
        root_id = match.group(1)
        query_name = re.search(r"\{([^{}]+)\}", label[match.end():])
        opened_query = bool(query_name and re.search(
            r"\(Pre/Post Query - .* - " + re.escape(query_name.group(1)) + r" - [0-9]+\) - \[",
            observed_title, re.IGNORECASE,
        ))
        # A separately captured child has local step numbers, while its tree retains the parent's ID.
        members = blocks if opened_query else tuple(
            block for block in blocks
            if (prompt_id := _prompt_step_id(block))
            and (prompt_id == root_id or prompt_id.startswith(root_id + "-"))
        )
        if not members:
            return SourceMapping(None, None, None, reason=f"Pre/Post Query {root_id} has no emitted prompt-mapped steps.")
        return _mapping_for_blocks(result, members, f"Mapped Pre/Post Query {root_id} and its nested steps.")
    if is_main_query:
        root_id = match.group(1)
        query_blocks = tuple(
            block for block in blocks
            if (prompt_id := _prompt_step_id(block))
            and (prompt_id == root_id or prompt_id.startswith(root_id + "."))
            and (step := result.emitted.step_for_block(block.index)) is not None
            and str(getattr(step, "functional_kind", "")).lower() in {
                "sql_query", "sql-query", "sqlite_query", "sqlite-query", "duckdb_query", "duckdb-query",
            }
        )
        if len(query_blocks) == 1:
            return _mapping_for_block(result, query_blocks[0], f"Mapped main query {root_id}.")
        if query_blocks:
            return SourceMapping(None, None, None, reason="Main-query prompt mapping is ambiguous.")
        if any(block.resolved_options.lookup.get("PROMPT-TEXT") for block in blocks):
            return SourceMapping(None, None, None, reason="Main query has no emitted prompt-mapped query step.")
    if match:
        step_id = match.group(1)
        matches = tuple(
            block for block in blocks
            if _prompt_step_id(block) == step_id
        )
        if len(matches) == 1:
            return _mapping_for_block(result, matches[0], f"Mapped designer step {step_id}.")
        if matches:
            return SourceMapping(None, None, None, reason="Designer step prompt mapping is ambiguous.")
        if label != "1. Main Query":
            return SourceMapping(None, None, None, reason=f"No compiler prompt matches designer step {step_id}.")
    if label != "1. Main Query":
        return SourceMapping(None, None, None, reason="Designer label has no qualified step ID.")
    steps = tuple(getattr(result.emitted, "steps", ()))
    query_steps = tuple(
        step for step in steps
        if str(getattr(step, "functional_kind", "")).lower() in {
            "sql_query", "sql-query", "sqlite_query", "sqlite-query",
            "duckdb_query", "duckdb-query",
        }
    )
    if len(query_steps) != 1:
        return SourceMapping(None, None, None, reason="Main-query mapping is ambiguous.")
    step = query_steps[0]
    return _mapping_for_step(result, step, "Mapped the exact SQLPathFinder main-query label.")


def _mapping_for_block(result: Any, block: Any, reason: str) -> SourceMapping:
    step = result.emitted.step_for_block(block.index)
    if step is None:
        scope = next((
            scope for scope in getattr(result.emitted, "scopes", ())
            if block.index in (scope.start_index, scope.end_index)
        ), None)
        if scope is not None:
            start, end = scope.source_range.start_offset, scope.source_range.end_offset
            return SourceMapping(block.index, start, (start, end), reason=reason)
        return SourceMapping(block.index, None, None, reason="Compiler emitted no step for this block.")
    return _mapping_for_step(result, step, reason)


def _mapping_for_blocks(result: Any, blocks: tuple[Any, ...], reason: str) -> SourceMapping:
    mappings = tuple(_mapping_for_block(result, block, reason) for block in blocks)
    if any(mapping.generated_range is None for mapping in mappings):
        return SourceMapping(None, None, None, reason="Compiler emitted no step for part of this Pre/Post Query.")
    ranges = tuple(mapping.generated_range for mapping in mappings)
    owned: list[str] = []
    for mapping in mappings:
        for name in mapping.asset_names:
            if name not in owned:
                owned.append(name)
    return SourceMapping(
        block_index=None,
        generated_offset=min(start for start, _end in ranges),
        generated_range=(min(start for start, _end in ranges), max(end for _start, end in ranges)),
        asset_names=tuple(owned),
        reason=reason,
    )


def _mapping_for_step(result: Any, step: Any, reason: str) -> SourceMapping:
    asset_names = tuple(name for name, _content in result.emitted.assets)
    owned: list[str] = []
    for parameter in step.parameters:
        value = parameter.value
        if parameter.artifact_role is None or not isinstance(value, str):
            continue
        value_name = value.replace("\\", "/")
        if value_name in asset_names and value_name not in owned:
            owned.append(value_name)
    start, end = step.source_range.start_offset, step.source_range.end_offset
    return SourceMapping(
        block_index=step.block_index,
        generated_offset=start,
        generated_range=(start, end),
        asset_names=tuple(owned),
        reason=reason,
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
    return _mapping_for_block(result, block, f"Mapped source line {line} to block {block.index}.")

