from __future__ import annotations

import re
from collections import defaultdict

from vg2c.frontend.models import Diagnostic, Kind, SourceSpan
from vg2c.resolver.macro_resolver import normalize_csv_path
from vg2c.resolver.models import ResolvedBlock, SqlMacroCall

SQL_CALL_RE = re.compile(r"\b(SQL_[A-Za-z0-9_]+)\s*\(")

# Detects the `(<col> In ` wrap that some VG2 scripts put before
# SQL_Get_CSV_List(...) — an unmatched `(` that relies on the macro/expansion
# to close it. Anchored to the end of body[:call_start].
_CALL_SITE_WRAP_RE = re.compile(
    r"\(\s*[A-Za-z_][\w.\[\]@]*\s+In\s*$", re.IGNORECASE
)


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
        if block.kind not in {
            Kind.MARS_READ,
            Kind.OASYS_READ,
            Kind.ARIES_READ,
            Kind.SQLITE_QUERY,
        }:
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
) -> tuple[str, list[SqlMacroCall], list[Diagnostic]]:
    diagnostics: list[Diagnostic] = []
    calls: list[SqlMacroCall] = []
    result_parts: list[str] = []
    cursor = 0
    macro_index = 0

    while True:
        match = _next_sql_call(body, cursor)
        if not match:
            result_parts.append(body[cursor:])
            break

        result_parts.append(body[cursor : match.start])
        name = match.name
        call_text = body[match.start : match.end]

        if name != "SQL_Get_CSV_List":
            diagnostics.append(
                Diagnostic(
                    severity="info",
                    code="unknown-sql-macro",
                    message=f"Left SQL macro {name} unchanged.",
                    block_index=block_index,
                    span=span,
                )
            )
            result_parts.append(call_text)
            cursor = match.end
            continue

        args = _split_args(match.args_text)
        if len(args) != 3:
            diagnostics.append(
                Diagnostic(
                    severity="warning",
                    code="sql-macro-parse-failed",
                    message="Could not parse SQL_Get_CSV_List arguments; left call unchanged.",
                    block_index=block_index,
                    span=span,
                )
            )
            result_parts.append(call_text)
            cursor = match.end
            continue

        csv_path_raw = _unquote(args[0])
        column_raw = args[1].strip()
        lead_in = _unquote(args[2])
        column_ref: int | str = _parse_column_ref(column_raw)

        placeholder = f"@@SQLMACRO:{macro_index}@@"
        macro_index += 1
        calls.append(
            SqlMacroCall(
                name="SQL_Get_CSV_List",
                csv_path=csv_path_raw,
                column_ref=column_ref,
                lead_in=lead_in,
                placeholder=placeholder,
                source_span=span,
            )
        )
        result_parts.append(placeholder)
        if _CALL_SITE_WRAP_RE.search(body[: match.start]):
            # The call site looks like `(<col> In SQL_Get_CSV_List(...)` with an
            # unmatched leading `(`. Emit a trailing `)` so the rewritten body
            # stays balanced after the runtime macro produces its IN list.
            result_parts.append(")")

        normalized_csv = normalize_csv_path(csv_path_raw)
        merged_consumers[normalized_csv].add(block_index)
        if normalized_csv not in csv_producers:
            diagnostics.append(
                Diagnostic(
                    severity="info",
                    code="sql-macro-csv-unknown-producer",
                    message=f"No known producer found for SQL macro CSV path {csv_path_raw}.",
                    block_index=block_index,
                    span=span,
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
    pattern = re.compile(r"\b(SQL_[A-Za-z0-9_]+)\s*\(")
    for match in pattern.finditer(body, start):
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


def _unquote(value: str) -> str:
    stripped = value.strip()
    if len(stripped) >= 2 and stripped[0] == stripped[-1] and stripped[0] in {'"', "'"}:
        return stripped[1:-1]
    return stripped


def _parse_column_ref(raw: str) -> int | str:
    value = _unquote(raw)
    return int(value) if value.isdigit() else value
