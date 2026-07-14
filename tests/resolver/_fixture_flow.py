from __future__ import annotations

import re
from pathlib import Path
from typing import Iterable

from vg2c.frontend import classify, parse
from vg2c.frontend.models import ClassifiedBlock
from vg2c.kind import Kind
from vg2c.resolver import resolve
from vg2c.resolver.models import ResolvedBlock, ResolvedProgram
from vg2c.operands import ScopeNode

TOKEN_RE = re.compile(r"^\s*\{([A-Z\-]+)\}")


def parse_classify_fixture(
    fixtures: Path,
    file_name: str,
) -> list[ClassifiedBlock]:
    path = fixtures / file_name
    text = path.read_text(encoding="utf-8", errors="replace")
    parsed = parse(text, source=path)
    classified = classify(parsed)
    return classified


def resolve_fixture(fixtures: Path, file_name: str) -> ResolvedProgram:
    classified = parse_classify_fixture(fixtures, file_name)
    return resolve(classified)


def all_scope_nodes(node: ScopeNode) -> Iterable[ScopeNode]:
    yield node
    for child in node.children:
        yield from all_scope_nodes(child)


def max_scope_depth(node: ScopeNode) -> int:
    if not node.children:
        return 1
    return 1 + max(max_scope_depth(child) for child in node.children)


def token_from_block(block: ResolvedBlock | ClassifiedBlock) -> str | None:
    if block.kind is not Kind.MACRO_CONTROL:
        return None
    utilities = block.options.lookup.get("UTILITIES", "")
    match = TOKEN_RE.match(utilities)
    return match.group(1) if match else None


def blocks_for_token(
    blocks: Iterable[ResolvedBlock], token: str
) -> list[ResolvedBlock]:
    return [block for block in blocks if token_from_block(block) == token]
