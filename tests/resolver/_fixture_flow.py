from __future__ import annotations

import re
from pathlib import Path
from typing import Iterable

from vg2c.frontend import classify, parse
from vg2c.frontend.models import ClassifiedBlock, Diagnostic
from vg2c.kind import Kind
from vg2c.resolver import resolve
from vg2c.resolver.models import ResolvedBlock, ResolvedProgram, ScopeNode

TOKEN_RE = re.compile(r"^\s*\{([A-Z\-]+)\}")


def parse_classify_fixture(
    fixtures: Path,
    file_name: str,
) -> tuple[list[ClassifiedBlock], list[Diagnostic]]:
    path = fixtures / file_name
    text = path.read_text(encoding="utf-8", errors="replace")
    parsed, parse_diags = parse(text, source=path)
    classified, classify_diags = classify(parsed)
    return classified, [*parse_diags, *classify_diags]


def resolve_fixture(fixtures: Path, file_name: str) -> ResolvedProgram:
    classified, upstream_diags = parse_classify_fixture(fixtures, file_name)
    return resolve(classified, diagnostics=upstream_diags)


def all_scope_nodes(node: ScopeNode) -> Iterable[ScopeNode]:
    yield node
    for child in node.children:
        yield from all_scope_nodes(child)


def max_scope_depth(node: ScopeNode) -> int:
    if not node.children:
        return 1
    return 1 + max(max_scope_depth(child) for child in node.children)


def diagnostics_by_code(
    diagnostics: Iterable[Diagnostic],
    code: str,
) -> list[Diagnostic]:
    return [diag for diag in diagnostics if diag.code == code]


def token_from_block(block: ResolvedBlock | ClassifiedBlock) -> str | None:
    if block.kind is not Kind.MACRO_CONTROL:
        return None
    utilities = block.options.lookup.get("UTILITIES", "")
    match = TOKEN_RE.match(utilities)
    return match.group(1) if match else None


def blocks_for_token(blocks: Iterable[ResolvedBlock], token: str) -> list[ResolvedBlock]:
    return [block for block in blocks if token_from_block(block) == token]
