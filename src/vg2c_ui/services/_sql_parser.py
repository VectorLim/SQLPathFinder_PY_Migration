from __future__ import annotations

import re

from vg2c_ui.domain.semantic_models import (
    SqlEditCapabilities,
    SqlEditableModel,
    SqlJoin,
    SqlPredicate,
    SqlSelection,
    SqlSource,
    SqlSpan,
)
from vg2c_ui.services._sql_lexer import (
    Token,
    is_identifier_token,
    is_trivia,
    lex_sql,
    overlaps,
    trimmed_span,
)
from vg2c_ui.services._sql_parser_support import (
    _Clauses,
    _first_clause_start_after,
    _join_type_start,
    _locate_clauses,
    _next_clause_start,
    _normalize_join_type,
    _split_by_top_level_comma,
    _trimmed_span_from_tokens,
)
from vg2c_ui.services._sql_predicates import _parse_predicate_chain

_SET_OPERATORS = frozenset(("UNION", "INTERSECT", "EXCEPT"))


def parse_sql(source: str) -> SqlEditableModel:
    full_span = trimmed_span(source, SqlSpan(start=0, end=len(source)))
    initial = _empty_model(source, full_span)
    if full_span.start >= full_span.end:
        return initial.model_copy(update={"read_only_reason": "SQL is empty."})

    tokens, error = lex_sql(source)
    if error:
        return initial.model_copy(update={"read_only_reason": error})
    statement_span, reason = _choose_editable_statement(tokens, full_span)
    if statement_span is None:
        return initial.model_copy(
            update={
                "read_only_reason": reason
                or "Only SELECT statements are structurally editable."
            }
        )
    empty = _empty_model(source, statement_span)
    significant = [
        token for token in tokens if not is_trivia(token) and overlaps(token, statement_span)
    ]
    if not significant:
        return empty.model_copy(update={"read_only_reason": "SQL is empty."})

    first = significant[0]
    if first.upper == "WITH":
        return empty.model_copy(
            update={
                "read_only_reason": (
                    "CTEs are preserved as raw SQL until a CTE-aware structural "
                    "editor is available."
                )
            }
        )
    if first.upper != "SELECT":
        return empty.model_copy(
            update={"read_only_reason": "Only SELECT statements are structurally editable."}
        )
    if any(token.depth == 0 and token.upper in _SET_OPERATORS for token in significant):
        return empty.model_copy(
            update={
                "read_only_reason": "UNION, INTERSECT, and EXCEPT queries are preserved as raw SQL."
            }
        )

    clauses = _locate_clauses(significant)
    selections, select_span, selected_capable, select_reason = _parse_selections(
        source, tokens, clauses, statement_span.end
    )
    joins, sources, from_span, joins_capable, from_reason = _parse_from_and_joins(
        source, tokens, clauses, statement_span.end
    )
    filters, where_clause_span, where_body_span, filters_capable, where_reason = _parse_where(
        source, tokens, clauses, statement_span.end
    )
    reasons = [item for item in (select_reason, from_reason, where_reason) if item]
    return SqlEditableModel(
        source=source,
        statement_span=statement_span,
        selections=selections,
        filters=filters,
        joins=joins,
        sources=sources,
        capabilities=SqlEditCapabilities(
            selected=selected_capable,
            filters=filters_capable,
            joins=joins_capable,
            raw_sql=True,
        ),
        read_only_reason=" ".join(reasons) if len(reasons) == 3 else None,
        select_list_span=select_span,
        where_clause_span=where_clause_span,
        where_body_span=where_body_span,
        from_clause_span=from_span,
    )


def _choose_editable_statement(
    tokens: list[Token], full_span: SqlSpan
) -> tuple[SqlSpan | None, str | None]:
    semicolons = [
        token
        for token in tokens
        if token.depth == 0 and token.text == ";" and overlaps(token, full_span)
    ]
    spans: list[SqlSpan] = []
    start = full_span.start
    for semicolon in semicolons:
        span = _trimmed_span_from_tokens(tokens, SqlSpan(start=start, end=semicolon.start))
        if span.start < span.end:
            spans.append(span)
        start = semicolon.end
    tail = _trimmed_span_from_tokens(tokens, SqlSpan(start=start, end=full_span.end))
    if tail.start < tail.end:
        spans.append(tail)
    if not spans:
        return None, "SQL is empty."

    first_tokens = [
        next(
            (
                token
                for token in tokens
                if not is_trivia(token)
                and token.start >= span.start
                and token.end <= span.end
            ),
            None,
        )
        for span in spans
    ]
    select_spans = [
        span
        for span, token in zip(spans, first_tokens, strict=True)
        if token and token.upper == "SELECT"
    ]
    if len(select_spans) == 1:
        return select_spans[0], None
    if len(select_spans) > 1:
        return (
            None,
            (
                "Multiple SELECT statements are preserved raw because the editable target "
                "is ambiguous."
            ),
        )
    if len(spans) == 1 and first_tokens[0] and first_tokens[0].upper == "WITH":
        return spans[0], None
    return None, "No unambiguous SELECT statement was found for structured editing."


def _empty_model(source: str, statement_span: SqlSpan) -> SqlEditableModel:
    return SqlEditableModel(
        source=source,
        statement_span=statement_span,
        selections=[],
        filters=[],
        joins=[],
        sources=[],
        capabilities=SqlEditCapabilities(selected=False, filters=False, joins=False, raw_sql=True),
        read_only_reason=None,
        select_list_span=None,
        where_clause_span=None,
        where_body_span=None,
        from_clause_span=None,
    )


def _parse_selections(
    source: str, tokens: list[Token], clauses: _Clauses, statement_end: int
) -> tuple[list[SqlSelection], SqlSpan | None, bool, str | None]:
    top = [token for token in tokens if not is_trivia(token) and token.depth == 0]
    select_index = top.index(clauses.select)
    if select_index + 1 >= len(top):
        return [], None, False, "SELECT list is missing."
    start_token = top[select_index + 1]
    if start_token.upper == "TOP":
        return (
            [],
            None,
            False,
            "SELECT TOP syntax is preserved raw until its modifier can be isolated safely.",
        )
    if start_token.upper in {"DISTINCT", "ALL"}:
        next_token = top[select_index + 2] if select_index + 2 < len(top) else None
        if next_token is None or (start_token.upper == "DISTINCT" and next_token.upper == "ON"):
            return [], None, False, "This SELECT modifier is preserved raw."
        start_token = next_token

    end = clauses.from_.start if clauses.from_ else _first_clause_start_after(
        clauses.select.end, clauses, statement_end
    )
    if start_token.start >= end:
        return [], None, False, "SELECT list is empty."
    span = trimmed_span(source, SqlSpan(start=start_token.start, end=end))
    item_spans = _split_by_top_level_comma(tokens, span)
    selections = [
        _parse_selection(source, tokens, item_span, index)
        for index, item_span in enumerate(item_spans)
    ]
    return (
        selections,
        span,
        bool(selections),
        None if selections else "No safely isolated SELECT items were found.",
    )


def _parse_selection(
    source: str, tokens: list[Token], span: SqlSpan, index: int
) -> SqlSelection:
    local = [
        token
        for token in tokens
        if not is_trivia(token) and token.start >= span.start and token.end <= span.end
    ]
    comments = any(
        token.kind == "comment" and token.start >= span.start and token.end <= span.end
        for token in tokens
    )
    expression_span = span
    alias = None
    as_candidates = [token for token in local if token.depth == 0 and token.upper == "AS"]
    as_token = as_candidates[-1] if as_candidates else None
    if as_token:
        after = [token for token in local if token.start >= as_token.end]
        if len(after) == 1 and is_identifier_token(after[0]):
            expression_span = trimmed_span(source, SqlSpan(start=span.start, end=as_token.start))
            alias = source[after[0].start : after[0].end]
    expression = source[expression_span.start : expression_span.end].strip()
    raw = source[span.start : span.end]
    editable = bool(expression) and not comments
    return SqlSelection(
        id=f"selection-{index}",
        expression=expression,
        alias=alias,
        raw=raw,
        editable=editable,
        read_only_reason=(
            "Selections containing comments are preserved raw."
            if comments
            else None if editable else "Selection is not safely editable."
        ),
        span=span,
    )


def _parse_where(
    source: str, tokens: list[Token], clauses: _Clauses, statement_end: int
) -> tuple[list[SqlPredicate], SqlSpan | None, SqlSpan | None, bool, str | None]:
    if clauses.where is None:
        return [], None, None, True, None
    end = _next_clause_start(clauses.where.start, clauses, statement_end)
    body_span = trimmed_span(source, SqlSpan(start=clauses.where.end, end=end))
    clause_span = trimmed_span(source, SqlSpan(start=clauses.where.start, end=end))
    if body_span.start >= body_span.end:
        return [], clause_span, body_span, False, "WHERE clause is empty."
    filters = _parse_predicate_chain(source, tokens, body_span, "filter")
    return filters, clause_span, body_span, True, None


def _parse_from_and_joins(
    source: str, tokens: list[Token], clauses: _Clauses, statement_end: int
) -> tuple[list[SqlJoin], list[SqlSource], SqlSpan | None, bool, str | None]:
    if clauses.from_ is None:
        return [], [], None, True, None
    end = _next_clause_start(clauses.from_.start, clauses, statement_end)
    span = trimmed_span(source, SqlSpan(start=clauses.from_.end, end=end))
    if span.start >= span.end:
        return [], [], span, False, "FROM clause is empty."
    significant = [
        token
        for token in tokens
        if not is_trivia(token)
        and token.depth == 0
        and token.start >= span.start
        and token.end <= span.end
    ]
    join_tokens = [token for token in significant if token.upper == "JOIN"]
    join_starts = [_join_type_start(significant, token) for token in join_tokens]
    base_source_span = trimmed_span(
        source, SqlSpan(start=span.start, end=join_starts[0] if join_starts else span.end)
    )
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
        condition_token = next(
            (token for token in interior if token.upper in {"ON", "USING"}), None
        )
        source_span = trimmed_span(
            source,
            SqlSpan(
                start=join_token.end,
                end=condition_token.start if condition_token else join_end,
            ),
        )
        source_text = source[source_span.start : source_span.end]
        source_trimmed = source_text.strip()
        source_contains_comment = any(
            token.kind == "comment" and overlaps(token, source_span) for token in tokens
        )
        source_subquery = source_trimmed.startswith("(")
        type_text = source[start : join_token.end]
        normalized_type = _normalize_join_type(type_text)
        natural = bool(re.search(r"\bNATURAL\b", type_text, re.IGNORECASE))
        using = bool(condition_token and condition_token.upper == "USING")
        predicates: list[SqlPredicate] = []
        if condition_token and condition_token.upper == "ON":
            predicate_span = trimmed_span(
                source, SqlSpan(start=condition_token.end, end=join_end)
            )
            predicates = _parse_predicate_chain(
                source, tokens, predicate_span, f"join-{index}-predicate"
            )
            if any(predicate.connector == "OR" for predicate in predicates):
                predicates = [
                    predicate.model_copy(
                        update={
                            "editable": False,
                            "read_only_reason": "OR-based join conditions are preserved raw.",
                        }
                    )
                    for predicate in predicates
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
            if source_contains_comment
            else None
        )
        joins.append(
            SqlJoin(
                id=join_id,
                join_type=normalized_type,
                source=source_text,
                predicates=predicates,
                editable_type=not natural,
                editable_source=bool(source_trimmed)
                and not natural
                and not source_contains_comment
                and not source_subquery,
                read_only_reason=read_only_reason,
                span=trimmed_span(source, SqlSpan(start=start, end=join_end)),
                type_span=trimmed_span(source, SqlSpan(start=start, end=join_token.end)),
                source_span=source_span,
            )
        )
        sources.append(
            SqlSource(
                id=f"source-join-{index}",
                expression=source_trimmed,
                kind="join",
                editable=bool(source_trimmed)
                and not natural
                and not source_contains_comment
                and not source_subquery,
                read_only_reason=(
                    "NATURAL JOIN source is preserved raw."
                    if natural
                    else "Subquery join sources are preserved raw."
                    if source_subquery
                    else "Join sources containing comments are preserved raw."
                    if source_contains_comment
                    else None
                ),
                span=source_span,
                join_id=join_id,
            )
        )
    return joins, sources, span, True, None


def _parse_sources(source: str, tokens: list[Token], span: SqlSpan) -> list[SqlSource]:
    if span.start >= span.end:
        return []
    result: list[SqlSource] = []
    for index, source_span in enumerate(_split_by_top_level_comma(tokens, span)):
        expression = source[source_span.start : source_span.end].strip()
        contains_comment = any(
            token.kind == "comment" and overlaps(token, source_span) for token in tokens
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
                    else None if editable else "Source is not safely editable."
                ),
                span=source_span,
                join_id=None,
            )
        )
    return result
