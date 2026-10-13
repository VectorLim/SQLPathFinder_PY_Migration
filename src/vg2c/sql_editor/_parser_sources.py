"""FROM/JOIN parsing for the structured SQL editor."""

from __future__ import annotations

from vg2c.sql_editor.models import SqlJoin, SqlPredicate, SqlSource, SqlSpan
from vg2c.sql_lexer import SqlToken
from vg2c.sql_editor._parser_core import (
    _Clauses,
    _is_trivia,
    _join_type_start,
    _next_clause_start,
    _normalize_join_type,
    _overlaps,
    _split_by_top_level_comma,
    _trimmed_span,
)
from vg2c.sql_editor._parser_predicates import _parse_predicate_chain

def _parse_from_and_joins(
    source: str,
    tokens: list[SqlToken],
    clauses: _Clauses,
    statement_end: int,
) -> tuple[list[SqlJoin], list[SqlSource], SqlSpan | None, bool, str | None]:
    if clauses.from_ is None:
        return [], [], None, True, None

    end = _next_clause_start(clauses.from_.start, clauses, statement_end)
    span = _trimmed_span(source, SqlSpan(clauses.from_.end, end))
    if span.start >= span.end:
        return [], [], span, False, "FROM clause is empty."

    significant = [
        token
        for token in tokens
        if not _is_trivia(token)
        and token.depth == 0
        and token.start >= span.start
        and token.end <= span.end
    ]
    join_tokens = [token for token in significant if token.upper == "JOIN"]
    join_starts = [_join_type_start(significant, token) for token in join_tokens]
    base_end = join_starts[0] if join_starts else span.end
    base_source_span = _trimmed_span(source, SqlSpan(span.start, base_end))
    sources = _parse_sources(source, tokens, base_source_span)
    if not join_tokens:
        return [], sources, span, True, None

    joins: list[SqlJoin] = []
    for index, join_token in enumerate(join_tokens):
        start = join_starts[index]
        join_end = join_starts[index + 1] if index + 1 < len(join_starts) else span.end
        interior = [
            token
            for token in significant
            if token.start >= join_token.end and token.start < join_end
        ]
        condition = next((token for token in interior if token.upper in {"ON", "USING"}), None)
        source_span = _trimmed_span(
            source, SqlSpan(join_token.end, condition.start if condition else join_end)
        )
        source_text = source[source_span.start : source_span.end]
        source_trimmed = source_text.strip()
        source_has_comment = any(
            token.kind == "comment" and _overlaps(token, source_span) for token in tokens
        )
        source_subquery = source_trimmed.startswith("(")
        type_text = source[start : join_token.end]
        normalized_type = _normalize_join_type(type_text)
        natural = "NATURAL" in type_text.upper().split()
        using = condition is not None and condition.upper == "USING"

        predicates: list[SqlPredicate] = []
        if condition is not None and condition.upper == "ON":
            predicate_span = _trimmed_span(source, SqlSpan(condition.end, join_end))
            predicates = _parse_predicate_chain(
                source, tokens, predicate_span, f"join-{index}-predicate"
            )
            if any(predicate.connector == "OR" for predicate in predicates):
                predicates = [
                    SqlPredicate(
                        id=p.id,
                        left=p.left,
                        operator=p.operator,
                        right=p.right,
                        connector=p.connector,
                        raw=p.raw,
                        editable=False,
                        read_only_reason="OR-based join conditions are preserved raw.",
                        span=p.span,
                        connector_span=p.connector_span,
                    )
                    for p in predicates
                ]

        join_id = f"join-{index}"
        read_only_reason = (
            "NATURAL JOIN is preserved raw."
            if natural
            else "USING join keys are preserved raw; the join type remains editable."
            if using
            else "Subquery join sources are preserved raw."
            if source_subquery
            else "Join sources containing comments are preserved raw."
            if source_has_comment
            else None
        )
        joins.append(
            SqlJoin(
                id=join_id,
                join_type=normalized_type,
                source=source_text,
                predicates=tuple(predicates),
                editable_type=not natural,
                editable_source=(
                    bool(source_trimmed)
                    and not natural
                    and not source_has_comment
                    and not source_subquery
                ),
                read_only_reason=read_only_reason,
                span=_trimmed_span(source, SqlSpan(start, join_end)),
                type_span=_trimmed_span(source, SqlSpan(start, join_token.end)),
                source_span=source_span,
            )
        )
        sources.append(
            SqlSource(
                id=f"source-join-{index}",
                expression=source_trimmed,
                kind="join",
                editable=(
                    bool(source_trimmed)
                    and not natural
                    and not source_has_comment
                    and not source_subquery
                ),
                read_only_reason=(
                    "NATURAL JOIN source is preserved raw."
                    if natural
                    else "Subquery join sources are preserved raw."
                    if source_subquery
                    else "Join sources containing comments are preserved raw."
                    if source_has_comment
                    else None
                ),
                span=source_span,
                join_id=join_id,
            )
        )

    return joins, sources, span, True, None


def _parse_sources(source: str, tokens: list[SqlToken], span: SqlSpan) -> list[SqlSource]:
    if span.start >= span.end:
        return []
    result: list[SqlSource] = []
    for index, source_span in enumerate(_split_by_top_level_comma(tokens, span)):
        expression = source[source_span.start : source_span.end].strip()
        contains_comment = any(
            token.kind == "comment" and _overlaps(token, source_span) for token in tokens
        )
        subquery = expression.startswith("(")
        editable = bool(expression) and not contains_comment and not subquery
        result.append(
            SqlSource(
                id=f"source-from-{index}",
                expression=expression,
                kind="from",
                editable=editable,
                read_only_reason=(
                    "Sources containing comments are preserved raw."
                    if contains_comment
                    else "Subquery sources are preserved raw."
                    if subquery
                    else None
                    if editable
                    else "Source is not safely editable."
                ),
                span=source_span,
                join_id=None,
            )
        )
    return result
