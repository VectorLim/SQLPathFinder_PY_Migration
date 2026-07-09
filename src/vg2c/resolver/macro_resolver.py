from __future__ import annotations

import re

from vg2c.frontend.models import (
    ClassifiedBlock,
    Diagnostic,
)
from vg2c.kind import Kind
from vg2c.resolver.models import (
    Else,
    EndIf,
    EndLoop,
    EndMacro,
    IfThen,
    MacroControlPayload,
    ResolvedBlock,
    RowsInFile,
    RunLoop,
    ScopeNode,
    StartMacro,
)

TOKEN_RE = re.compile(r"^\s*\{([A-Z\-]+)\}")
QUOTED_RE = re.compile(r'"([^"]*)"')


def resolve_macros(
    blocks: list[ClassifiedBlock],
    scope_tree: ScopeNode,
) -> tuple[list[ResolvedBlock], list[Diagnostic]]:
    diagnostics: list[Diagnostic] = []

    scope_for_block = _build_scope_lookup(
        scope_tree, {block.index for block in blocks}
    )
    payload_by_index = {
        block.index: _parse_control_payload(block, diagnostics)
        for block in blocks
        if block.kind is Kind.MACRO_CONTROL
    }

    resolved: list[ResolvedBlock] = []
    for block in blocks:
        payload = payload_by_index.get(block.index)
        resolved.append(
            ResolvedBlock(
                classified=block,
                resolved_options=block.options,
                resolved_body=block.body,
                sql_macro_calls=(),
                control_payload=payload,
                scope_id=scope_for_block.get(block.index, 0),
            )
        )

    return resolved, diagnostics


def _build_scope_lookup(scope_tree: ScopeNode, indices: set[int]) -> dict[int, int]:
    mapping: dict[int, int] = {idx: 0 for idx in indices}

    def visit(node: ScopeNode, ancestors: list[ScopeNode]) -> None:
        next_ancestors = [*ancestors, node]
        if node.kind == "leaf" and node.block_index is not None:
            mapping[node.block_index] = node.scope_id
        for child in node.children:
            visit(child, next_ancestors)

    visit(scope_tree, [])

    # Include control boundary indices that are not explicit leaves.
    for idx in indices:
        if mapping.get(idx, 0) != 0:
            continue
        mapping[idx] = _deepest_scope_containing(scope_tree, idx)
    return mapping


def _deepest_scope_containing(node: ScopeNode, idx: int, best: int = 0) -> int:
    if node.start_index <= idx <= node.end_index:
        best = node.scope_id
        for child in node.children:
            best = _deepest_scope_containing(child, idx, best)
    return best


def _parse_control_payload(
    block: ClassifiedBlock,
    diagnostics: list[Diagnostic],
) -> MacroControlPayload | None:
    if block.kind is not Kind.MACRO_CONTROL:
        return None

    utilities = block.options.lookup.get("UTILITIES", "")
    token_match = TOKEN_RE.match(utilities)
    if not token_match:
        diagnostics.append(
            Diagnostic(
                severity="warning",
                code="unknown-macro-control",
                message="Macro control block has no recognized token.",
                block_index=block.index,
                span=block.span,
            )
        )
        return None

    token = token_match.group(1)
    args = QUOTED_RE.findall(utilities)
    if token == "START-MACRO":
        csv_path = args[0] if args else ""
        prompt_flag = args[1] if len(args) > 1 else "N"
        return StartMacro(csv_path=csv_path, prompt_off=prompt_flag.upper() == "Y")
    if token == "END-MACRO":
        return EndMacro()
    if token == "IF-THEN":
        padded = (args + ["", "", "", "", "", "", ""])[:7]
        return IfThen(
            lhs=padded[0],
            op=padded[1],
            rhs=padded[2],
            conj=padded[3] or None,
            lhs2=padded[4] or None,
            op2=padded[5] or None,
            rhs2=padded[6] or None,
        )
    if token == "ELSE":
        return Else()
    if token == "END-IF":
        return EndIf()
    if token == "ROWS-IN-FILE":
        csv_path = args[0] if args else ""
        var_name = args[1] if len(args) > 1 else ""
        prompt_flag = args[2] if len(args) > 2 else "N"
        return RowsInFile(
            csv_path=csv_path, var_name=var_name, prompt_off=prompt_flag.upper() == "Y"
        )
    if token == "RUN-LOOP":
        input_csv = args[0] if args else ""
        chunk_csv = args[1] if len(args) > 1 else ""
        chunk_size_raw = args[2] if len(args) > 2 else "0"
        prompt_flag = args[3] if len(args) > 3 else "N"
        try:
            chunk_size = int(chunk_size_raw)
        except ValueError:
            chunk_size = 0
        return RunLoop(
            input_csv_path=input_csv,
            chunk_csv_path=chunk_csv,
            chunk_size=chunk_size,
            prompt_off=prompt_flag.upper() == "Y",
        )
    if token == "END-LOOP":
        return EndLoop()

    diagnostics.append(
        Diagnostic(
            severity="warning",
            code="unknown-macro-control",
            message=f"Unknown macro control token {{{token}}}.",
            block_index=block.index,
            span=block.span,
        )
    )
    return None
