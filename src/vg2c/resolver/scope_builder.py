from __future__ import annotations

import re
from dataclasses import dataclass

from vg2c.frontend.models import ClassifiedBlock, Diagnostic
from vg2c.kind import Kind
from vg2c.resolver.models import (
    Else,
    EndIf,
    EndLoop,
    EndMacro,
    IfThen,
    RowsInFile,
    RunLoop,
    ScopeNode,
    StartMacro,
)

TOKEN_RE = re.compile(r"^\s*\{([A-Z\-]+)\}")

# Tokens that own a child scope — delegate to payload.build_scope.
_SCOPE_TOKENS: dict[str, type] = {
    "START-MACRO": StartMacro,
    "RUN-LOOP": RunLoop,
    "IF-THEN": IfThen,
}

# Orphan closer tokens: valid as stop-tokens inside a scope, but an error at
# the top level.  Maps token string → (diagnostic code, message, payload type).
_ORPHAN_TOKENS: dict[str, tuple[str, str, type]] = {
    "END-MACRO": ("orphan-end-macro", "Found {END-MACRO} without a matching opener.", EndMacro),
    "END-LOOP": ("orphan-end-loop", "Found {END-LOOP} without a matching {RUN-LOOP}.", EndLoop),
    "END-IF": ("orphan-end-if", "Found {END-IF} without a matching opener.", EndIf),
    "ELSE": ("orphan-else", "Found {ELSE} without a matching {IF-THEN}.", Else),
}


@dataclass
class _ScopeBuilderState:
    next_scope_id: int = 1

    def new_scope_id(self) -> int:
        value = self.next_scope_id
        self.next_scope_id += 1
        return value


def build_scope_tree(
    blocks: list[ClassifiedBlock],
) -> tuple[ScopeNode, list[Diagnostic]]:
    diagnostics: list[Diagnostic] = []
    state = _ScopeBuilderState()

    children, _, _ = _parse_children(
        blocks=blocks,
        start=0,
        stop_tokens=None,
        state=state,
        diagnostics=diagnostics,
    )

    root_end = blocks[-1].index if blocks else -1
    root = ScopeNode(
        scope_id=0,
        kind="program",
        start_index=0,
        end_index=root_end,
        children=tuple(children),
        block_index=None,
        control_payload=None,
    )
    return root, diagnostics


def _parse_children(
    blocks: list[ClassifiedBlock],
    start: int,
    stop_tokens: set[str] | None,
    state: _ScopeBuilderState,
    diagnostics: list[Diagnostic],
) -> tuple[list[ScopeNode], int, str | None]:
    children: list[ScopeNode] = []
    i = start
    while i < len(blocks):
        block = blocks[i]
        token = _control_token(block)

        if stop_tokens and token in stop_tokens:
            return children, i, token

        if token in _SCOPE_TOKENS:
            payload = _SCOPE_TOKENS[token].from_block(block)
            subtree, next_i = payload.build_scope(
                blocks, i, state, diagnostics, _parse_children
            )
            children.append(subtree)
            i = next_i
            continue

        if token == "ROWS-IN-FILE":
            children.append(
                _leaf_node(state, block.index, RowsInFile.from_block(block))
            )
            i += 1
            continue

        if token in _ORPHAN_TOKENS:
            code, message, payload_cls = _ORPHAN_TOKENS[token]
            diagnostics.append(
                Diagnostic(
                    severity="error",
                    code=code,
                    message=message,
                    block_index=block.index,
                    span=block.span,
                )
            )
            children.append(_leaf_node(state, block.index, payload_cls()))
            i += 1
            continue

        if token is not None:
            diagnostics.append(
                Diagnostic(
                    severity="warning",
                    code="unknown-macro-control",
                    message=f"Unknown macro control token {{{token}}}; treated as leaf.",
                    block_index=block.index,
                    span=block.span,
                )
            )
            children.append(_leaf_node(state, block.index))
            i += 1
            continue

        children.append(_leaf_node(state, block.index))
        i += 1

    return children, i, None


def _control_token(block: ClassifiedBlock) -> str | None:
    if block.kind is not Kind.MACRO_CONTROL:
        return None
    utilities = block.options.lookup.get("UTILITIES", "")
    match = TOKEN_RE.match(utilities)
    return match.group(1) if match else None


def _leaf_node(
    state: _ScopeBuilderState,
    block_index: int,
    control_payload: object | None = None,
) -> ScopeNode:
    return ScopeNode(
        scope_id=state.new_scope_id(),
        kind="leaf",
        start_index=block_index,
        end_index=block_index,
        children=(),
        block_index=block_index,
        control_payload=control_payload,
    )
