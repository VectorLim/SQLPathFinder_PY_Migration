from __future__ import annotations

from vg2c_ui.domain.semantic_models import SqlPredicate, SqlSpan
from vg2c_ui.services._sql_lexer import Token, is_trivia, trimmed_span


def _parse_predicate_chain(
    source: str, tokens: list[Token], span: SqlSpan, prefix: str
) -> list[SqlPredicate]:
    significant = [
        token
        for token in tokens
        if not is_trivia(token) and token.start >= span.start and token.end <= span.end
    ]
    pieces: list[tuple[SqlSpan, str | None, SqlSpan | None]] = []
    segment_start = span.start
    connector_for_segment = None
    connector_span_for_segment = None
    between_pending = False
    for token in significant:
        if token.depth != 0:
            continue
        if token.upper == "CASE":
            between_pending = False
        if token.upper == "BETWEEN":
            between_pending = True
            continue
        if token.upper == "AND" and between_pending:
            between_pending = False
            continue
        if token.upper not in {"AND", "OR"}:
            continue
        candidate = trimmed_span(source, SqlSpan(start=segment_start, end=token.start))
        if candidate.start < candidate.end:
            pieces.append((candidate, connector_for_segment, connector_span_for_segment))
        connector_for_segment = token.upper
        connector_span_for_segment = SqlSpan(start=token.start, end=token.end)
        segment_start = token.end
        between_pending = False
    tail = trimmed_span(source, SqlSpan(start=segment_start, end=span.end))
    if tail.start < tail.end:
        pieces.append((tail, connector_for_segment, connector_span_for_segment))
    return [
        _parse_predicate(source, tokens, piece, f"{prefix}-{index}")
        for index, piece in enumerate(pieces)
    ]


def _parse_predicate(
    source: str,
    tokens: list[Token],
    piece: tuple[SqlSpan, str | None, SqlSpan | None],
    predicate_id: str,
) -> SqlPredicate:
    span, connector, connector_span = piece
    local = [
        token
        for token in tokens
        if not is_trivia(token) and token.start >= span.start and token.end <= span.end
    ]
    raw = source[span.start : span.end]
    has_comment = any(
        token.kind == "comment" and token.start >= span.start and token.end <= span.end
        for token in tokens
    )
    top = [token for token in local if token.depth == 0]
    operator = _find_predicate_operator(top)
    if operator is None or has_comment or any(token.upper in {"CASE", "BETWEEN"} for token in top):
        return SqlPredicate(
            id=predicate_id,
            left=raw.strip(),
            operator="",
            right="",
            connector=connector,
            raw=raw,
            editable=False,
            read_only_reason=(
                "Predicates containing comments are preserved raw."
                if has_comment
                else "This predicate is too complex for safe row editing."
            ),
            span=span,
            connector_span=connector_span,
        )
    op_start, op_end, op_text = operator
    left_span = trimmed_span(source, SqlSpan(start=span.start, end=op_start))
    right_span = trimmed_span(source, SqlSpan(start=op_end, end=span.end))
    left = source[left_span.start : left_span.end]
    right = source[right_span.start : right_span.end]
    editable = bool(left.strip() and right.strip())
    return SqlPredicate(
        id=predicate_id,
        left=left,
        operator=op_text,
        right=right,
        connector=connector,
        raw=raw,
        editable=editable,
        read_only_reason=None if editable else "Predicate operands could not be isolated safely.",
        span=span,
        connector_span=connector_span,
    )


def _find_predicate_operator(tokens: list[Token]) -> tuple[int, int, str] | None:
    candidates: list[tuple[int, int, str]] = []
    for index, token in enumerate(tokens):
        if token.kind == "operator" and token.text in {"=", "!=", "<>", "<", "<=", ">", ">="}:
            candidates.append((token.start, token.end, token.text.upper()))
            continue
        if token.upper in {"LIKE", "ILIKE", "IN", "IS"}:
            previous = tokens[index - 1] if index > 0 else None
            following = tokens[index + 1] if index + 1 < len(tokens) else None
            if token.upper in {"LIKE", "ILIKE", "IN"}:
                if previous and previous.upper == "NOT":
                    candidates.append((previous.start, token.end, f"NOT {token.upper}"))
                else:
                    candidates.append((token.start, token.end, token.upper))
            elif following and following.upper == "NOT":
                candidates.append((token.start, following.end, "IS NOT"))
            else:
                candidates.append((token.start, token.end, "IS"))
    unique = [
        candidate
        for index, candidate in enumerate(candidates)
        if index == 0 or candidate[:2] != candidates[index - 1][:2]
    ]
    return unique[0] if len(unique) == 1 else None
