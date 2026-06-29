from __future__ import annotations

import re
from collections import defaultdict

from vg2c.frontend.models import Diagnostic, Kind, SourceSpan
from vg2c.resolver.macro_resolver import normalize_csv_path
from vg2c.resolver.models import ResolvedBlock
from vg2c.resolver.sql_macros import HANDLERS, MacroParseError

_SQL_CALL_RE = re.compile(r"\b(SQL_[A-Za-z0-9_]+)\s*\(")
_SCANNED_KINDS = {Kind.SQL_QUERY, Kind.SQLITE_QUERY}


def expand_sql_macros(
    blocks: list[ResolvedBlock],
    csv_producers: dict[str, int],
    csv_consumers: dict[str, tuple[int, ...]],
) -> tuple[list[ResolvedBlock], dict[str, tuple[int, ...]], list[Diagnostic]]:
    diagnostics: list[Diagnostic] = []
    updated_blocks: list[ResolvedBlock] = []
    merged_consumers: dict[str, set[int]] = defaultdict(set)
    for path, items in csv_consumers.items():
        merged_consumers[path].update(items)

    for block in blocks:
        if block.kind not in _SCANNED_KINDS:
            updated_blocks.append(block)
            continue

        rewritten_body, calls, local_diags = _expand_body(
            body=block.resolved_body,
            span=block.parsed.span,
            block_index=block.parsed.index,
            csv_producers=csv_producers,
            merged_consumers=merged_consumers,
        )
        diagnostics.extend(local_diags)
        updated_blocks.append(
            ResolvedBlock(
                parsed=block.parsed,
                kind=block.kind,
                resolved_options=block.resolved_options,
                resolved_body=rewritten_body,
                sql_macro_calls=tuple(calls),
                runtime_macro_refs=block.runtime_macro_refs,
                control_payload=block.control_payload,
                scope_id=block.scope_id,
            )
        )

    normalized_consumers = {k: tuple(sorted(v)) for k, v in merged_consumers.items()}
    return updated_blocks, normalized_consumers, diagnostics


def _expand_body(
    body: str,
    span: SourceSpan,
    block_index: int,
    csv_producers: dict[str, int],
    merged_consumers: dict[str, set[int]],
) -> tuple[str, list, list[Diagnostic]]:
    diagnostics: list[Diagnostic] = []
    calls: list = []
    result_parts: list[str] = []
    cursor = 0

    while True:
        match = _next_sql_call(body, cursor)
        if not match:
            result_parts.append(body[cursor:])
            break

        result_parts.append(body[cursor : match.start])
        name = match.name
        call_text = body[match.start : match.end]

        handler = HANDLERS.get(name)
        if handler is None:
            diagnostics.append(
                _diag(
                    "info",
                    "unknown-sql-macro",
                    f"Left SQL macro {name} unchanged.",
                    block_index,
                    span,
                )
            )
            result_parts.append(call_text)
            cursor = match.end
            continue

        args = _split_args(match.args_text)
        placeholder = f"@@SQLMACRO:{len(calls)}@@"
        outcome = handler.build_call(args, placeholder, span, body[: match.start])

        if isinstance(outcome, MacroParseError):
            diagnostics.append(
                _diag(
                    "warning",
                    "sql-macro-parse-failed",
                    f"{outcome.message}; left call unchanged.",
                    block_index,
                    span,
                )
            )
            result_parts.append(call_text)
            cursor = match.end
            continue

        calls.append(outcome.call)
        result_parts.append(placeholder)
        result_parts.append(outcome.appended_text)

        for csv_path_raw in outcome.call.consumed_csv_paths():
            normalized_csv = normalize_csv_path(csv_path_raw)
            merged_consumers[normalized_csv].add(block_index)
            if normalized_csv not in csv_producers:
                diagnostics.append(
                    _diag(
                        "info",
                        "sql-macro-csv-unknown-producer",
                        f"No known producer found for SQL macro CSV path {csv_path_raw}.",
                        block_index,
                        span,
                    )
                )
        cursor = match.end

    return "".join(result_parts), calls, diagnostics


class _SqlCallMatch:
    def __init__(self, name: str, start: int, end: int, args_text: str) -> None:
        self.name = name
        self.start = start
        self.end = end
        self.args_text = args_text


def _next_sql_call(body: str, start: int) -> _SqlCallMatch | None:
    for match in _SQL_CALL_RE.finditer(body, start):
        open_paren = body.find("(", match.start())
        if open_paren == -1:
            continue
        close_paren = _find_matching_paren(body, open_paren)
        if close_paren == -1:
            continue
        return _SqlCallMatch(
            name=match.group(1),
            start=match.start(),
            end=close_paren + 1,
            args_text=body[open_paren + 1 : close_paren],
        )
    return None


def _find_matching_paren(text: str, open_idx: int) -> int:
    depth = 0
    in_single = False
    in_double = False
    i = open_idx
    while i < len(text):
        ch = text[i]
        prev = text[i - 1] if i > 0 else ""
        if ch == "'" and prev != "\\" and not in_double:
            in_single = not in_single
        elif ch == '"' and prev != "\\" and not in_single:
            in_double = not in_double
        elif not in_single and not in_double:
            if ch == "(":
                depth += 1
            elif ch == ")":
                depth -= 1
                if depth == 0:
                    return i
        i += 1
    return -1


def _split_args(args_text: str) -> list[str]:
    args: list[str] = []
    current: list[str] = []
    depth = 0
    in_single = False
    in_double = False

    for i, ch in enumerate(args_text):
        prev = args_text[i - 1] if i > 0 else ""
        if ch == "'" and prev != "\\" and not in_double:
            in_single = not in_single
            current.append(ch)
            continue
        if ch == '"' and prev != "\\" and not in_single:
            in_double = not in_double
            current.append(ch)
            continue

        if not in_single and not in_double:
            if ch == "(":
                depth += 1
            elif ch == ")" and depth > 0:
                depth -= 1
            elif ch == "," and depth == 0:
                args.append("".join(current).strip())
                current = []
                continue

        current.append(ch)

    if current:
        args.append("".join(current).strip())
    return args


def _diag(
    severity: str, code: str, message: str, block_index: int, span: SourceSpan
) -> Diagnostic:
    return Diagnostic(
        severity=severity,  # type: ignore[arg-type]
        code=code,
        message=message,
        block_index=block_index,
        span=span,
    )
