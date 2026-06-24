from __future__ import annotations

import re
from collections import defaultdict
from pathlib import PurePosixPath
from types import MappingProxyType

from vg2c.frontend.models import (
    BlockOptions,
    ClassifiedBlock,
    Diagnostic,
    Kind,
    SourceSpan,
)
from vg2c.resolver.models import (
    Else,
    EndIf,
    EndMacro,
    IfThen,
    MacroControlPayload,
    ResolvedBlock,
    RowsInFile,
    RuntimeMacroRef,
    ScopeNode,
    StartMacro,
)

TOKEN_RE = re.compile(r"^\s*\{([A-Z\-]+)\}")
QUOTED_RE = re.compile(r'"([^"]*)"')
NAMED_PLACEHOLDER_RE = re.compile(r"<<<([^<>]*)>>>")


def resolve_macros(
    blocks: list[ClassifiedBlock],
    scope_tree: ScopeNode,
) -> tuple[
    list[ResolvedBlock],
    dict[str, int],
    dict[str, tuple[int, ...]],
    list[Diagnostic],
]:
    diagnostics: list[Diagnostic] = []
    block_by_index = {b.parsed.index: b for b in blocks}

    scope_for_block = _build_scope_lookup(scope_tree, set(block_by_index.keys()))
    payload_by_index = {
        block.parsed.index: _parse_control_payload(block, diagnostics)
        for block in blocks
        if block.kind is Kind.MACRO_CONTROL
    }

    runtime_refs_by_block, local_diags = _collect_runtime_refs(
        blocks, scope_tree, payload_by_index
    )
    diagnostics.extend(local_diags)

    csv_producers = _collect_csv_producers(blocks)
    csv_consumers = _collect_csv_consumers(blocks, payload_by_index)
    for path, consumers in csv_consumers.items():
        if path not in csv_producers:
            for block_index in consumers:
                diagnostics.append(
                    Diagnostic(
                        severity="info",
                        code="unknown-csv-producer",
                        message=f"No known producer found for CSV consumer path {path}.",
                        block_index=block_index,
                        span=block_by_index[block_index].parsed.span,
                    )
                )

    resolved: list[ResolvedBlock] = []
    for block in blocks:
        payload = payload_by_index.get(block.parsed.index)
        resolved.append(
            ResolvedBlock(
                parsed=block.parsed,
                kind=block.kind,
                resolved_options=block.parsed.options,
                resolved_body=block.parsed.body,
                sql_macro_calls=(),
                runtime_macro_refs=tuple(
                    runtime_refs_by_block.get(block.parsed.index, [])
                ),
                control_payload=payload,
                scope_id=scope_for_block.get(block.parsed.index, 0),
            )
        )

    return resolved, csv_producers, csv_consumers, diagnostics


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


def _collect_runtime_refs(
    blocks: list[ClassifiedBlock],
    scope_tree: ScopeNode,
    payload_by_index: dict[int, MacroControlPayload | None],
) -> tuple[dict[int, list[RuntimeMacroRef]], list[Diagnostic]]:
    diagnostics: list[Diagnostic] = []
    refs: dict[int, list[RuntimeMacroRef]] = defaultdict(list)
    block_by_index = {b.parsed.index: b for b in blocks}
    mutable_frames: list[_MutableFrame] = [
        _MutableFrame(frame_id=0, kind="static-vars")
    ]

    def walk(node: ScopeNode) -> None:
        pushed = False
        if node.kind == "macro" and isinstance(node.control_payload, StartMacro):
            frame_id = node.scope_id
            mutable_frames.append(_MutableFrame(frame_id=frame_id, kind="row-iter"))
            pushed = True

        for child in node.children:
            walk(child)

        if node.kind == "leaf" and node.block_index is not None:
            block = block_by_index[node.block_index]
            payload = payload_by_index.get(node.block_index)
            if isinstance(payload, RowsInFile):
                mutable_frames[-1].named_vars.add(payload.var_name.upper())

            _scan_block_placeholders(
                block, mutable_frames, refs[node.block_index], diagnostics
            )

        if pushed:
            mutable_frames.pop()

    walk(scope_tree)
    return refs, diagnostics


class _MutableFrame:
    def __init__(self, frame_id: int, kind: str) -> None:
        self.frame_id = frame_id
        self.kind = kind
        self.named_vars: set[str] = set()
        self.positional_cursor = 0


def _scan_block_placeholders(
    block: ClassifiedBlock,
    frames: list[_MutableFrame],
    out_refs: list[RuntimeMacroRef],
    diagnostics: list[Diagnostic],
) -> None:
    for key, value in block.parsed.options.pairs:
        location = "utility-string" if key == "UTILITIES" else "option-value"
        _scan_text(
            text=value,
            span=block.parsed.span,
            frames=frames,
            out_refs=out_refs,
            diagnostics=diagnostics,
            block_index=block.parsed.index,
            location=location,
            option_key=key,
        )

    _scan_text(
        text=block.parsed.body,
        span=block.parsed.span,
        frames=frames,
        out_refs=out_refs,
        diagnostics=diagnostics,
        block_index=block.parsed.index,
        location="body",
        option_key=None,
    )


def _scan_text(
    text: str,
    span: SourceSpan,
    frames: list[_MutableFrame],
    out_refs: list[RuntimeMacroRef],
    diagnostics: list[Diagnostic],
    block_index: int,
    location: str,
    option_key: str | None,
) -> None:
    for match in NAMED_PLACEHOLDER_RE.finditer(text):
        raw_name = match.group(1)
        name = raw_name.strip().upper()
        if name == "":
            diagnostics.append(
                Diagnostic(
                    severity="warning",
                    code="empty-macro-name",
                    message="Encountered empty placeholder <<<>>>.",
                    block_index=block_index,
                    span=span,
                )
            )
            continue

        frame_id = _resolve_named_frame(name, frames)
        if frame_id == -1:
            diagnostics.append(
                Diagnostic(
                    severity="warning",
                    code="unbound-macro-var",
                    message=f"Macro variable {name} is not bound in current scope.",
                    block_index=block_index,
                    span=span,
                )
            )

        out_refs.append(
            RuntimeMacroRef(
                name=name,
                frame_id=frame_id,
                location=location,  # type: ignore[arg-type]
                option_key=option_key,
                source_span=span,
            )
        )

    positional_count = text.count("<<>>")
    for _ in range(positional_count):
        frame_id = _resolve_positional_frame(frames)
        if frame_id == -1:
            diagnostics.append(
                Diagnostic(
                    severity="warning",
                    code="unbound-macro-var",
                    message="Positional placeholder <<>> is not bound in current scope.",
                    block_index=block_index,
                    span=span,
                )
            )
        out_refs.append(
            RuntimeMacroRef(
                name="__POSITIONAL__",
                frame_id=frame_id,
                location=location,  # type: ignore[arg-type]
                option_key=option_key,
                source_span=span,
            )
        )


def _resolve_named_frame(name: str, frames: list[_MutableFrame]) -> int:
    for frame in reversed(frames):
        if frame.kind == "row-iter":
            return frame.frame_id
        if name in frame.named_vars:
            return frame.frame_id
    return -1


def _resolve_positional_frame(frames: list[_MutableFrame]) -> int:
    for frame in reversed(frames):
        if frame.kind in {"row-iter", "static-vars"}:
            frame.positional_cursor += 1
            return frame.frame_id
    return -1


def _parse_control_payload(
    block: ClassifiedBlock,
    diagnostics: list[Diagnostic],
) -> MacroControlPayload | None:
    if block.kind is not Kind.MACRO_CONTROL:
        return None

    utilities = block.parsed.options.lookup.get("UTILITIES", "")
    token_match = TOKEN_RE.match(utilities)
    if not token_match:
        diagnostics.append(
            Diagnostic(
                severity="warning",
                code="unknown-macro-control",
                message="Macro control block has no recognized token.",
                block_index=block.parsed.index,
                span=block.parsed.span,
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

    diagnostics.append(
        Diagnostic(
            severity="warning",
            code="unknown-macro-control",
            message=f"Unknown macro control token {{{token}}}.",
            block_index=block.parsed.index,
            span=block.parsed.span,
        )
    )
    return None


def _collect_csv_producers(blocks: list[ClassifiedBlock]) -> dict[str, int]:
    producers: dict[str, int] = {}
    for block in blocks:
        csv_value = block.parsed.options.lookup.get("CSV")
        if not csv_value:
            continue
        normalized = _normalize_csv_path(csv_value)
        producers.setdefault(normalized, block.parsed.index)
    return producers


def _collect_csv_consumers(
    blocks: list[ClassifiedBlock],
    payload_by_index: dict[int, MacroControlPayload | None],
) -> dict[str, tuple[int, ...]]:
    consumers: dict[str, list[int]] = defaultdict(list)
    for block in blocks:
        for key, value in block.parsed.options.pairs:
            if key == "TABLE":
                table_items = [
                    part.strip() for part in value.split(",") if part.strip()
                ]
                for table_item in table_items:
                    consumers[_normalize_csv_path(table_item)].append(
                        block.parsed.index
                    )

        payload = payload_by_index.get(block.parsed.index)
        if isinstance(payload, (StartMacro, RowsInFile)) and payload.csv_path:
            consumers[_normalize_csv_path(payload.csv_path)].append(block.parsed.index)

    return {k: tuple(v) for k, v in consumers.items()}


def _normalize_csv_path(value: str) -> str:
    normalized = value.strip().strip('"').replace("\\", "/")
    while normalized.startswith("./"):
        normalized = normalized[2:]
    if normalized.startswith(".") and len(normalized) > 1 and normalized[1] == "/":
        normalized = normalized[2:]
    return str(PurePosixPath(normalized)).lower()


def normalize_csv_path(value: str) -> str:
    return _normalize_csv_path(value)
