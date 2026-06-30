from __future__ import annotations

import re
from dataclasses import dataclass

from vg2c.frontend.models import ClassifiedBlock, Diagnostic, Kind
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

    root_end = blocks[-1].parsed.index if blocks else -1
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

        if block.kind is Kind.MALFORMED:
            diagnostics.append(
                Diagnostic(
                    severity="warning",
                    code="malformed-block-skipped",
                    message="Resolver skipped a malformed block.",
                    block_index=block.parsed.index,
                    span=block.parsed.span,
                )
            )
            children.append(_leaf_node(state, block.parsed.index))
            i += 1
            continue

        if token == "START-MACRO":
            subtree, next_i = _parse_macro(blocks, i, state, diagnostics)
            children.append(subtree)
            i = next_i
            continue

        if token == "RUN-LOOP":
            subtree, next_i = _parse_loop(blocks, i, state, diagnostics)
            children.append(subtree)
            i = next_i
            continue

        if token == "IF-THEN":
            subtree, next_i = _parse_if(blocks, i, state, diagnostics)
            children.append(subtree)
            i = next_i
            continue

        if token == "END-MACRO":
            diagnostics.append(
                Diagnostic(
                    severity="error",
                    code="orphan-end-macro",
                    message="Found {END-MACRO} without a matching opener.",
                    block_index=block.parsed.index,
                    span=block.parsed.span,
                )
            )
            children.append(
                _leaf_node(state, block.parsed.index, control_payload=EndMacro())
            )
            i += 1
            continue

        if token == "END-LOOP":
            diagnostics.append(
                Diagnostic(
                    severity="error",
                    code="orphan-end-loop",
                    message="Found {END-LOOP} without a matching {RUN-LOOP}.",
                    block_index=block.parsed.index,
                    span=block.parsed.span,
                )
            )
            children.append(
                _leaf_node(state, block.parsed.index, control_payload=EndLoop())
            )
            i += 1
            continue

        if token == "END-IF":
            diagnostics.append(
                Diagnostic(
                    severity="error",
                    code="orphan-end-if",
                    message="Found {END-IF} without a matching opener.",
                    block_index=block.parsed.index,
                    span=block.parsed.span,
                )
            )
            children.append(
                _leaf_node(state, block.parsed.index, control_payload=EndIf())
            )
            i += 1
            continue

        if token == "ELSE":
            diagnostics.append(
                Diagnostic(
                    severity="error",
                    code="orphan-else",
                    message="Found {ELSE} without a matching {IF-THEN}.",
                    block_index=block.parsed.index,
                    span=block.parsed.span,
                )
            )
            children.append(
                _leaf_node(state, block.parsed.index, control_payload=Else())
            )
            i += 1
            continue

        if token == "ROWS-IN-FILE":
            payload = _parse_rows_in_file_payload(block)
            children.append(
                _leaf_node(state, block.parsed.index, control_payload=payload)
            )
            i += 1
            continue

        if token is not None:
            diagnostics.append(
                Diagnostic(
                    severity="warning",
                    code="unknown-macro-control",
                    message=f"Unknown macro control token {{{token}}}; treated as leaf.",
                    block_index=block.parsed.index,
                    span=block.parsed.span,
                )
            )
            children.append(_leaf_node(state, block.parsed.index))
            i += 1
            continue

        children.append(_leaf_node(state, block.parsed.index))
        i += 1

    return children, i, None


def _parse_macro(
    blocks: list[ClassifiedBlock],
    start_i: int,
    state: _ScopeBuilderState,
    diagnostics: list[Diagnostic],
) -> tuple[ScopeNode, int]:
    start_block = blocks[start_i]
    payload = _parse_start_macro_payload(start_block)
    children, i, end_token = _parse_children(
        blocks=blocks,
        start=start_i + 1,
        stop_tokens={"END-MACRO"},
        state=state,
        diagnostics=diagnostics,
    )

    if end_token != "END-MACRO":
        diagnostics.append(
            Diagnostic(
                severity="error",
                code="unclosed-macro",
                message="Found {START-MACRO} without a matching {END-MACRO}; implicitly closed at EOF.",
                block_index=start_block.parsed.index,
                span=start_block.parsed.span,
            )
        )
        end_index = blocks[-1].parsed.index if blocks else start_block.parsed.index
        return (
            ScopeNode(
                scope_id=state.new_scope_id(),
                kind="macro",
                start_index=start_block.parsed.index,
                end_index=end_index,
                children=tuple(children),
                block_index=None,
                control_payload=payload,
            ),
            i,
        )

    end_index = blocks[i].parsed.index
    return (
        ScopeNode(
            scope_id=state.new_scope_id(),
            kind="macro",
            start_index=start_block.parsed.index,
            end_index=end_index,
            children=tuple(children),
            block_index=None,
            control_payload=payload,
        ),
        i + 1,
    )


def _parse_loop(
    blocks: list[ClassifiedBlock],
    start_i: int,
    state: _ScopeBuilderState,
    diagnostics: list[Diagnostic],
) -> tuple[ScopeNode, int]:
    start_block = blocks[start_i]
    payload = _parse_run_loop_payload(start_block)
    children, i, end_token = _parse_children(
        blocks=blocks,
        start=start_i + 1,
        stop_tokens={"END-LOOP"},
        state=state,
        diagnostics=diagnostics,
    )

    if end_token != "END-LOOP":
        diagnostics.append(
            Diagnostic(
                severity="error",
                code="unclosed-loop",
                message="Found {RUN-LOOP} without a matching {END-LOOP}; implicitly closed at EOF.",
                block_index=start_block.parsed.index,
                span=start_block.parsed.span,
            )
        )
        end_index = blocks[-1].parsed.index if blocks else start_block.parsed.index
        return (
            ScopeNode(
                scope_id=state.new_scope_id(),
                kind="loop",
                start_index=start_block.parsed.index,
                end_index=end_index,
                children=tuple(children),
                block_index=None,
                control_payload=payload,
            ),
            i,
        )

    end_index = blocks[i].parsed.index
    return (
        ScopeNode(
            scope_id=state.new_scope_id(),
            kind="loop",
            start_index=start_block.parsed.index,
            end_index=end_index,
            children=tuple(children),
            block_index=None,
            control_payload=payload,
        ),
        i + 1,
    )


def _parse_if(
    blocks: list[ClassifiedBlock],
    start_i: int,
    state: _ScopeBuilderState,
    diagnostics: list[Diagnostic],
) -> tuple[ScopeNode, int]:
    start_block = blocks[start_i]
    payload = _parse_if_then_payload(start_block)

    if_children, i, token = _parse_children(
        blocks=blocks,
        start=start_i + 1,
        stop_tokens={"ELSE", "END-IF"},
        state=state,
        diagnostics=diagnostics,
    )

    branch_nodes: list[ScopeNode] = [
        ScopeNode(
            scope_id=state.new_scope_id(),
            kind="if-branch",
            start_index=start_block.parsed.index,
            end_index=(
                if_children[-1].end_index if if_children else start_block.parsed.index
            ),
            children=tuple(if_children),
            block_index=None,
            control_payload=None,
        )
    ]

    end_index = blocks[-1].parsed.index if blocks else start_block.parsed.index
    next_i = i

    if token == "ELSE":
        else_children, j, else_stop = _parse_children(
            blocks=blocks,
            start=i + 1,
            stop_tokens={"END-IF"},
            state=state,
            diagnostics=diagnostics,
        )
        branch_nodes.append(
            ScopeNode(
                scope_id=state.new_scope_id(),
                kind="else-branch",
                start_index=blocks[i].parsed.index,
                end_index=(
                    else_children[-1].end_index
                    if else_children
                    else blocks[i].parsed.index
                ),
                children=tuple(else_children),
                block_index=None,
                control_payload=Else(),
            )
        )
        if else_stop == "END-IF":
            end_index = blocks[j].parsed.index
            next_i = j + 1
        else:
            diagnostics.append(
                Diagnostic(
                    severity="error",
                    code="unclosed-if",
                    message="Found {IF-THEN} without a matching {END-IF}; implicitly closed at EOF.",
                    block_index=start_block.parsed.index,
                    span=start_block.parsed.span,
                )
            )
            next_i = j
    elif token == "END-IF":
        end_index = blocks[i].parsed.index
        next_i = i + 1
    else:
        diagnostics.append(
            Diagnostic(
                severity="error",
                code="unclosed-if",
                message="Found {IF-THEN} without a matching {END-IF}; implicitly closed at EOF.",
                block_index=start_block.parsed.index,
                span=start_block.parsed.span,
            )
        )

    return (
        ScopeNode(
            scope_id=state.new_scope_id(),
            kind="if",
            start_index=start_block.parsed.index,
            end_index=end_index,
            children=tuple(branch_nodes),
            block_index=None,
            control_payload=payload,
        ),
        next_i,
    )


def _control_token(block: ClassifiedBlock) -> str | None:
    if block.kind is not Kind.MACRO_CONTROL:
        return None
    utilities = block.parsed.options.lookup.get("UTILITIES", "")
    match = TOKEN_RE.match(utilities)
    return match.group(1) if match else None


def _quoted_args(value: str) -> list[str]:
    return re.findall(r'"([^"]*)"', value)


def _parse_start_macro_payload(block: ClassifiedBlock) -> StartMacro:
    args = _quoted_args(block.parsed.options.lookup.get("UTILITIES", ""))
    csv_path = args[0] if args else ""
    prompt_flag = args[1] if len(args) > 1 else "N"
    return StartMacro(csv_path=csv_path, prompt_off=prompt_flag.upper() == "Y")


def _parse_if_then_payload(block: ClassifiedBlock) -> IfThen:
    args = _quoted_args(block.parsed.options.lookup.get("UTILITIES", ""))
    padded = (args + ["", "", "", "", "", "", ""])[:7]
    conj = padded[3] or None
    lhs2 = padded[4] or None
    op2 = padded[5] or None
    rhs2 = padded[6] or None
    return IfThen(
        lhs=padded[0],
        op=padded[1],
        rhs=padded[2],
        conj=conj,
        lhs2=lhs2,
        op2=op2,
        rhs2=rhs2,
    )


def _parse_rows_in_file_payload(block: ClassifiedBlock) -> RowsInFile:
    args = _quoted_args(block.parsed.options.lookup.get("UTILITIES", ""))
    csv_path = args[0] if args else ""
    var_name = args[1] if len(args) > 1 else ""
    prompt_flag = args[2] if len(args) > 2 else "N"
    return RowsInFile(
        csv_path=csv_path,
        var_name=var_name,
        prompt_off=prompt_flag.upper() == "Y",
    )


def _parse_run_loop_payload(block: ClassifiedBlock) -> RunLoop:
    args = _quoted_args(block.parsed.options.lookup.get("UTILITIES", ""))
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
