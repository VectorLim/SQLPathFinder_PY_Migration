"""Predicate parsing for WHERE clauses and JOIN conditions."""

from __future__ import annotations

from vg2c.sql_editor._parser_support import (
    Clauses,
    is_trivia,
    next_clause_start,
    trimmed_span,
)
from vg2c.sql_editor.models import SqlLogicalConnector, SqlPredicate, SqlSpan
from vg2c.sql_lexer import SqlToken


def parse_where(
    source: str,
    tokens: list[SqlToken],
    clauses: Clauses,
    statement_end: int,
) -> tuple[list[SqlPredicate], SqlSpan | None, SqlSpan | None, bool, str | None]:
    if clauses.where is None:
        return [], None, None, True, None
    end = next_clause_start(clauses.where.start, clauses, statement_end)
    body_span = trimmed_span(source, SqlSpan(clauses.where.end, end))
    clause_span = trimmed_span(source, SqlSpan(clauses.where.start, end))
    if body_span.start >= body_span.end:
        return [], clause_span, body_span, False, "WHERE clause is empty."
    return (
        parse_predicate_chain(source, tokens, body_span, "filter"),
        clause_span,
        body_span,
        True,
        None,
    )


def parse_predicate_chain(
    source: str, tokens: list[SqlToken], span: SqlSpan, prefix: str
) -> list[SqlPredicate]:
    significant = [
        token
        for token in tokens
        if not is_trivia(token) and token.start >= span.start and token.end <= span.end
    ]
    pieces: list[tuple[SqlSpan, SqlLogicalConnector | None, SqlSpan | None]] = []
    segment_start = span.start
    connector: SqlLogicalConnector | None = None
    connector_span: SqlSpan | None = None
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

        candidate = trimmed_span(source, SqlSpan(segment_start, token.start))
        if candidate.start < candidate.end:
            pieces.append((candidate, connector, connector_span))
        connector = token.upper  # type: ignore[assignment]
        connector_span = SqlSpan(token.start, token.end)
        segment_start = token.end
        between_pending = False

    tail = trimmed_span(source, SqlSpan(segment_start, span.end))
    if tail.start < tail.end:
        pieces.append((tail, connector, connector_span))

    return [
        _parse_predicate(source, tokens, piece, f"{prefix}-{index}")
        for index, piece in enumerate(pieces)
    ]


def _parse_predicate(
    source: str,
    tokens: list[SqlToken],
    piece: tuple[SqlSpan, SqlLogicalConnector | None, SqlSpan | None],
    id_: str,
) -> SqlPredicate:
    span, connector, connector_span = piece
    local = [
        token
        for token in tokens
        if not is_trivia(token) and token.start >= span.start and token.end <= span.end
    ]
    raw = source[span.start:span.end]
    has_comment = any(
        token.kind == "comment" and token.start >= span.start and token.end <= span.end
        for token in tokens
    )
    top = [token for token in local if token.depth == 0]
    operator = _find_predicate_operator(top)
    too_complex = any(token.upper in {"CASE", "BETWEEN"} for token in top)
    if operator is None or has_comment or too_complex:
        return SqlPredicate(
            id=id_,
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
    left_span = trimmed_span(source, SqlSpan(span.start, op_start))
    right_span = trimmed_span(source, SqlSpan(op_end, span.end))
    left = source[left_span.start:left_span.end]
    right = source[right_span.start:right_span.end]
    editable = bool(left.strip() and right.strip())
    return SqlPredicate(
        id=id_,
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


def _find_predicate_operator(tokens: list[SqlToken]) -> tuple[int, int, str] | None:
    candidates: list[tuple[int, int, str]] = []
    for index, token in enumerate(tokens):
        if token.kind == "operator" and token.text in {"=", "!=", "<>", "<", "<=", ">", ">="}:
            candidates.append((token.start, token.end, token.text.upper()))
            continue
        if token.upper in {"LIKE", "ILIKE", "IN"}:
            previous = tokens[index - 1] if index > 0 else None
            if previous and previous.upper == "NOT":
                candidates.append((previous.start, token.end, f"NOT {token.upper}"))
            else:
                candidates.append((token.start, token.end, token.upper))
        elif token.upper == "IS":
            next_token = tokens[index + 1] if index + 1 < len(tokens) else None
            if next_token and next_token.upper == "NOT":
                candidates.append((token.start, next_token.end, "IS NOT"))
            else:
                candidates.append((token.start, token.end, "IS"))

    unique: list[tuple[int, int, str]] = []
    for candidate in candidates:
        if not unique or candidate[:2] != unique[-1][:2]:
            unique.append(candidate)
    return unique[0] if len(unique) == 1 else None
