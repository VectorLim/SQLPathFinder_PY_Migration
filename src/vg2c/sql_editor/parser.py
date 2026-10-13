from __future__ import annotations

from dataclasses import replace

from vg2c.sql_editor.models import (
    SqlEditableModel,
    SqlEditCapabilities,
    SqlPredicate,
    SqlSelection,
    SqlSpan,
)
from vg2c.sql_lexer import SqlToken, lex_sql
from vg2c.sql_editor._parser_core import (
    SET_OPERATORS,
    _Clauses,
    _first_clause_start_after,
    _is_identifier_token,
    _is_trivia,
    _locate_clauses,
    _next_clause_start,
    _overlaps,
    _split_by_top_level_comma,
    _trimmed_span,
    _trimmed_span_from_tokens,
    unquote_identifier,
)
from vg2c.sql_editor._parser_predicates import _parse_predicate_chain
from vg2c.sql_editor._parser_sources import _parse_from_and_joins



def parse_sql(source: str) -> SqlEditableModel:
    full_span = _trimmed_span(source, SqlSpan(0, len(source)))
    initial = _empty_model(source, full_span)
    if full_span.start >= full_span.end:
        return _replace_model(initial, read_only_reason="SQL is empty.")

    tokens, error = lex_sql(source)
    if error:
        return _replace_model(initial, read_only_reason=error)

    statement_span, reason = _choose_editable_statement(tokens, full_span)
    if statement_span is None:
        return _replace_model(
            initial,
            read_only_reason=reason or "Only SELECT statements are structurally editable.",
        )

    empty = _empty_model(source, statement_span)
    significant = [
        token for token in tokens if not _is_trivia(token) and _overlaps(token, statement_span)
    ]
    if not significant:
        return _replace_model(empty, read_only_reason="SQL is empty.")

    first = significant[0]
    if first.upper == "WITH":
        return _replace_model(
            empty,
            read_only_reason=(
                "CTEs are preserved as raw SQL until a CTE-aware structural editor is available."
            ),
        )
    if first.upper != "SELECT":
        return _replace_model(
            empty, read_only_reason="Only SELECT statements are structurally editable."
        )
    if any(token.depth == 0 and token.upper in SET_OPERATORS for token in significant):
        return _replace_model(
            empty,
            read_only_reason=("UNION, INTERSECT, and EXCEPT queries are preserved as raw SQL."),
        )

    clauses = _locate_clauses(significant)
    selections, select_span, selected_capable, selected_reason = _parse_selections(
        source, tokens, clauses, statement_span.end
    )
    joins, sources, from_span, joins_capable, joins_reason = _parse_from_and_joins(
        source, tokens, clauses, statement_span.end
    )
    filters, where_clause_span, where_body_span, filters_capable, filters_reason = _parse_where(
        source, tokens, clauses, statement_span.end
    )
    reasons = [item for item in (selected_reason, joins_reason, filters_reason) if item]

    return SqlEditableModel(
        source=source,
        statement_span=statement_span,
        selections=tuple(selections),
        filters=tuple(filters),
        joins=tuple(joins),
        sources=tuple(sources),
        capabilities=SqlEditCapabilities(
            selected=selected_capable,
            filters=filters_capable,
            joins=joins_capable,
        ),
        read_only_reason=" ".join(reasons) if len(reasons) == 3 else None,
        select_list_span=select_span,
        where_clause_span=where_clause_span,
        where_body_span=where_body_span,
        from_clause_span=from_span,
    )


def _replace_model(model: SqlEditableModel, *, read_only_reason: str | None) -> SqlEditableModel:
    return SqlEditableModel(
        source=model.source,
        statement_span=model.statement_span,
        selections=model.selections,
        filters=model.filters,
        joins=model.joins,
        sources=model.sources,
        capabilities=model.capabilities,
        read_only_reason=read_only_reason,
        select_list_span=model.select_list_span,
        where_clause_span=model.where_clause_span,
        where_body_span=model.where_body_span,
        from_clause_span=model.from_clause_span,
    )


def _choose_editable_statement(
    tokens: list[SqlToken], full_span: SqlSpan
) -> tuple[SqlSpan | None, str | None]:
    semicolons = [
        token
        for token in tokens
        if token.depth == 0 and token.text == ";" and _overlaps(token, full_span)
    ]
    spans: list[SqlSpan] = []
    start = full_span.start
    for semicolon in semicolons:
        span = _trimmed_span_from_tokens(tokens, SqlSpan(start, semicolon.start))
        if span.start < span.end:
            spans.append(span)
        start = semicolon.end
    tail = _trimmed_span_from_tokens(tokens, SqlSpan(start, full_span.end))
    if tail.start < tail.end:
        spans.append(tail)
    if not spans:
        return None, "SQL is empty."

    first_tokens = [
        next(
            (
                token
                for token in tokens
                if not _is_trivia(token) and token.start >= span.start and token.end <= span.end
            ),
            None,
        )
        for span in spans
    ]
    select_spans = [
        span
        for span, token in zip(spans, first_tokens, strict=True)
        if token is not None and token.upper == "SELECT"
    ]
    if len(select_spans) == 1:
        return select_spans[0], None
    if len(select_spans) > 1:
        return (
            None,
            "Multiple SELECT statements are preserved raw because the editable "
            "target is ambiguous.",
        )
    if len(spans) == 1 and first_tokens[0] and first_tokens[0].upper == "WITH":
        return spans[0], None
    return None, "No unambiguous SELECT statement was found for structured editing."


def _empty_model(source: str, statement_span: SqlSpan) -> SqlEditableModel:
    return SqlEditableModel(
        source=source,
        statement_span=statement_span,
        selections=(),
        filters=(),
        joins=(),
        sources=(),
        capabilities=SqlEditCapabilities(False, False, False),
        read_only_reason=None,
        select_list_span=None,
        where_clause_span=None,
        where_body_span=None,
        from_clause_span=None,
    )


def _parse_selections(
    source: str,
    tokens: list[SqlToken],
    clauses: _Clauses,
    statement_end: int,
) -> tuple[list[SqlSelection], SqlSpan | None, bool, str | None]:
    top = [token for token in tokens if not _is_trivia(token) and token.depth == 0]
    select_index = top.index(clauses.select)
    if select_index + 1 >= len(top):
        return [], None, False, "SELECT list is missing."

    start_token = top[select_index + 1]
    list_prefix_end = clauses.select.end
    if start_token.upper == "TOP":
        return (
            [],
            None,
            False,
            "SELECT TOP syntax is preserved raw until its modifier can be isolated safely.",
        )
    if start_token.upper in {"DISTINCT", "ALL"}:
        list_prefix_end = start_token.end
        next_index = select_index + 2
        next_token = top[next_index] if next_index < len(top) else None
        if next_token is None or (start_token.upper == "DISTINCT" and next_token.upper == "ON"):
            return [], None, False, "This SELECT modifier is preserved raw."
        start_token = next_token

    end = (
        clauses.from_.start
        if clauses.from_
        else _first_clause_start_after(clauses.select.end, clauses, statement_end)
    )
    if start_token.start >= end:
        return [], None, False, "SELECT list is empty."

    span = _trimmed_span(source, SqlSpan(start_token.start, end))
    item_spans = _split_by_top_level_comma(tokens, span)
    selections = [
        _parse_selection(source, tokens, item_span, index)
        for index, item_span in enumerate(item_spans)
    ]
    has_comment = any(
        token.kind == "comment" and token.start >= list_prefix_end and token.end <= end
        for token in tokens
    )
    if has_comment:
        selections = [
            replace(
                item,
                editable=False,
                read_only_reason="SELECT lists containing comments are preserved raw.",
            )
            for item in selections
        ]
        return selections, span, False, "SELECT lists containing comments are preserved raw."

    return (
        selections,
        span,
        bool(selections),
        None if selections else "No safely isolated SELECT items were found.",
    )


def _parse_selection(
    source: str, tokens: list[SqlToken], span: SqlSpan, index: int
) -> SqlSelection:
    local = [
        token
        for token in tokens
        if not _is_trivia(token) and token.start >= span.start and token.end <= span.end
    ]
    has_comment = any(
        token.kind == "comment" and token.start >= span.start and token.end <= span.end
        for token in tokens
    )
    expression_span = span
    alias: str | None = None
    as_candidates = [token for token in local if token.depth == 0 and token.upper == "AS"]
    if as_candidates:
        as_token = as_candidates[-1]
        after = [token for token in local if token.start >= as_token.end]
        if len(after) == 1 and _is_identifier_token(after[0]):
            expression_span = _trimmed_span(source, SqlSpan(span.start, as_token.start))
            alias = source[after[0].start : after[0].end]

    expression = source[expression_span.start : expression_span.end].strip()
    expression_tokens = [
        token for token in local if token.end <= expression_span.end
    ]
    simple_label = _simple_column_label(expression_tokens)
    display_label = (
        unquote_identifier(alias)
        if alias is not None
        else simple_label or f"Expression {index + 1}"
    )
    raw = source[span.start : span.end]
    complex_unaliased = simple_label is None and alias is None
    editable = bool(expression) and not has_comment and not complex_unaliased
    return SqlSelection(
        id=f"selection-{index}",
        expression=expression,
        alias=alias,
        display_label=display_label,
        raw=raw,
        editable=editable,
        read_only_reason=(
            "Selections containing comments are preserved raw."
            if has_comment
            else "Complex expressions without an alias are preserved as raw SQL."
            if complex_unaliased
            else None
            if editable
            else "Selection is not safely editable."
        ),
        span=span,
    )


def _simple_column_label(tokens: list[SqlToken]) -> str | None:
    if not tokens or len(tokens) % 2 != 1:
        return None
    if any(
        not _is_identifier_token(token) if index % 2 == 0 else token.text != "."
        for index, token in enumerate(tokens)
    ):
        return None
    return unquote_identifier(tokens[-1].text)




def _parse_where(
    source: str,
    tokens: list[SqlToken],
    clauses: _Clauses,
    statement_end: int,
) -> tuple[list[SqlPredicate], SqlSpan | None, SqlSpan | None, bool, str | None]:
    if clauses.where is None:
        return [], None, None, True, None
    end = _next_clause_start(clauses.where.start, clauses, statement_end)
    body_span = _trimmed_span(source, SqlSpan(clauses.where.end, end))
    clause_span = _trimmed_span(source, SqlSpan(clauses.where.start, end))
    if body_span.start >= body_span.end:
        return [], clause_span, body_span, False, "WHERE clause is empty."
    return (
        _parse_predicate_chain(source, tokens, body_span, "filter"),
        clause_span,
        body_span,
        True,
        None,
    )








__all__ = ["parse_sql"]
