from __future__ import annotations

from dataclasses import dataclass, replace

from vg2c.sql_editor.models import (
    SqlEditableModel,
    SqlEditCapabilities,
    SqlJoin,
    SqlLogicalConnector,
    SqlPredicate,
    SqlSelection,
    SqlSource,
    SqlSpan,
)

SET_OPERATORS = {"UNION", "INTERSECT", "EXCEPT"}
CLAUSE_WORDS = {
    "WHERE",
    "GROUP",
    "HAVING",
    "ORDER",
    "LIMIT",
    "OFFSET",
    "FETCH",
    "QUALIFY",
    "WINDOW",
}
SIMPLE_JOIN_TYPES = {"INNER", "LEFT", "RIGHT", "FULL", "CROSS"}


@dataclass(frozen=True, slots=True)
class _Token:
    kind: str
    text: str
    upper: str
    start: int
    end: int
    depth: int


@dataclass(frozen=True, slots=True)
class _Clauses:
    select: _Token
    from_: _Token | None
    where: _Token | None
    group: _Token | None
    having: _Token | None
    order: _Token | None
    limit: _Token | None
    offset: _Token | None
    fetch: _Token | None
    qualify: _Token | None
    window: _Token | None

    def values(self) -> tuple[_Token | None, ...]:
        return (
            self.select,
            self.from_,
            self.where,
            self.group,
            self.having,
            self.order,
            self.limit,
            self.offset,
            self.fetch,
            self.qualify,
            self.window,
        )


def parse_sql(source: str) -> SqlEditableModel:
    full_span = _trimmed_span(source, SqlSpan(0, len(source)))
    initial = _empty_model(source, full_span)
    if full_span.start >= full_span.end:
        return _replace_model(initial, read_only_reason="SQL is empty.")

    tokens, error = _lex_sql(source)
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
    tokens: list[_Token], full_span: SqlSpan
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
    tokens: list[_Token],
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


def _parse_selection(source: str, tokens: list[_Token], span: SqlSpan, index: int) -> SqlSelection:
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
    raw = source[span.start : span.end]
    editable = bool(expression) and not has_comment
    return SqlSelection(
        id=f"selection-{index}",
        expression=expression,
        alias=alias,
        raw=raw,
        editable=editable,
        read_only_reason=(
            "Selections containing comments are preserved raw."
            if has_comment
            else None
            if editable
            else "Selection is not safely editable."
        ),
        span=span,
    )


def _parse_where(
    source: str,
    tokens: list[_Token],
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


def _parse_from_and_joins(
    source: str,
    tokens: list[_Token],
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


def _parse_sources(source: str, tokens: list[_Token], span: SqlSpan) -> list[SqlSource]:
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


def _parse_predicate_chain(
    source: str, tokens: list[_Token], span: SqlSpan, prefix: str
) -> list[SqlPredicate]:
    significant = [
        token
        for token in tokens
        if not _is_trivia(token) and token.start >= span.start and token.end <= span.end
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

        candidate = _trimmed_span(source, SqlSpan(segment_start, token.start))
        if candidate.start < candidate.end:
            pieces.append((candidate, connector, connector_span))
        connector = token.upper  # type: ignore[assignment]
        connector_span = SqlSpan(token.start, token.end)
        segment_start = token.end
        between_pending = False

    tail = _trimmed_span(source, SqlSpan(segment_start, span.end))
    if tail.start < tail.end:
        pieces.append((tail, connector, connector_span))

    return [
        _parse_predicate(source, tokens, piece, f"{prefix}-{index}")
        for index, piece in enumerate(pieces)
    ]


def _parse_predicate(
    source: str,
    tokens: list[_Token],
    piece: tuple[SqlSpan, SqlLogicalConnector | None, SqlSpan | None],
    id_: str,
) -> SqlPredicate:
    span, connector, connector_span = piece
    local = [
        token
        for token in tokens
        if not _is_trivia(token) and token.start >= span.start and token.end <= span.end
    ]
    raw = source[span.start : span.end]
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
    left_span = _trimmed_span(source, SqlSpan(span.start, op_start))
    right_span = _trimmed_span(source, SqlSpan(op_end, span.end))
    left = source[left_span.start : left_span.end]
    right = source[right_span.start : right_span.end]
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


def _find_predicate_operator(
    tokens: list[_Token],
) -> tuple[int, int, str] | None:
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


def _locate_clauses(significant: list[_Token]) -> _Clauses:
    top = [token for token in significant if token.depth == 0]
    select = next(token for token in top if token.upper == "SELECT")
    after = [token for token in top if token.start > select.start]

    def find(name: str) -> _Token | None:
        return next((token for token in after if token.upper == name), None)

    return _Clauses(
        select=select,
        from_=find("FROM"),
        where=find("WHERE"),
        group=find("GROUP"),
        having=find("HAVING"),
        order=find("ORDER"),
        limit=find("LIMIT"),
        offset=find("OFFSET"),
        fetch=find("FETCH"),
        qualify=find("QUALIFY"),
        window=find("WINDOW"),
    )


def _first_clause_start_after(position: int, clauses: _Clauses, fallback: int) -> int:
    starts = sorted(
        token.start
        for token in clauses.values()
        if token is not None and token.start > position and token.upper != "SELECT"
    )
    return starts[0] if starts else fallback


def _next_clause_start(position: int, clauses: _Clauses, fallback: int) -> int:
    starts = sorted(
        token.start
        for token in clauses.values()
        if token is not None and token.start > position and token.upper in CLAUSE_WORDS
    )
    return starts[0] if starts else fallback


def _split_by_top_level_comma(tokens: list[_Token], span: SqlSpan) -> list[SqlSpan]:
    commas = [
        token
        for token in tokens
        if token.depth == 0
        and token.text == ","
        and token.start >= span.start
        and token.end <= span.end
    ]
    spans: list[SqlSpan] = []
    start = span.start
    for comma in commas:
        spans.append(SqlSpan(start, comma.start))
        start = comma.end
    spans.append(SqlSpan(start, span.end))
    return [
        item
        for item in (_trimmed_span_from_tokens(tokens, span) for span in spans)
        if item.start < item.end
    ]


def _trimmed_span_from_tokens(tokens: list[_Token], span: SqlSpan) -> SqlSpan:
    significant = [
        token
        for token in tokens
        if not _is_trivia(token) and token.start >= span.start and token.end <= span.end
    ]
    if not significant:
        return SqlSpan(span.end, span.end)
    return SqlSpan(significant[0].start, significant[-1].end)


def _join_type_start(tokens: list[_Token], join_token: _Token) -> int:
    index = tokens.index(join_token)
    cursor = index - 1
    start = join_token.start
    if cursor >= 0 and tokens[cursor].upper == "OUTER":
        start = tokens[cursor].start
        cursor -= 1
    if cursor >= 0 and tokens[cursor].upper in SIMPLE_JOIN_TYPES:
        start = tokens[cursor].start
        cursor -= 1
    if cursor >= 0 and tokens[cursor].upper == "NATURAL":
        start = tokens[cursor].start
    return start


def _normalize_join_type(raw: str) -> str:
    words = [word for word in raw.upper().replace("JOIN", " ").split() if word != "OUTER"]
    return " ".join(words) if words else "INNER"


def _lex_sql(source: str) -> tuple[list[_Token], str | None]:
    tokens: list[_Token] = []
    index = 0
    depth = 0
    while index < len(source):
        start = index
        character = source[index]

        if character.isspace():
            index += 1
            while index < len(source) and source[index].isspace():
                index += 1
            tokens.append(_token("whitespace", source, start, index, depth))
            continue

        if source.startswith("--", index):
            index += 2
            while index < len(source) and source[index] != "\n":
                index += 1
            tokens.append(_token("comment", source, start, index, depth))
            continue

        if source.startswith("/*", index):
            close = source.find("*/", index + 2)
            if close < 0:
                return tokens, "SQL contains an unterminated block comment."
            index = close + 2
            tokens.append(_token("comment", source, start, index, depth))
            continue

        if character == "'":
            index += 1
            closed = False
            while index < len(source):
                if source[index] == "'":
                    if index + 1 < len(source) and source[index + 1] == "'":
                        index += 2
                        continue
                    index += 1
                    closed = True
                    break
                index += 1
            if not closed:
                return tokens, "SQL contains an unterminated string literal."
            tokens.append(_token("string", source, start, index, depth))
            continue

        if character in {'"', "`", "["}:
            close_character = "]" if character == "[" else character
            index += 1
            closed = False
            while index < len(source):
                if source[index] == close_character:
                    if (
                        character != "["
                        and index + 1 < len(source)
                        and source[index + 1] == close_character
                    ):
                        index += 2
                        continue
                    if character == "[" and index + 1 < len(source) and source[index + 1] == "]":
                        index += 2
                        continue
                    index += 1
                    closed = True
                    break
                index += 1
            if not closed:
                return tokens, "SQL contains an unterminated quoted identifier."
            tokens.append(_token("quoted", source, start, index, depth))
            continue

        if character.isalpha() or character in "_$#@":
            index += 1
            while index < len(source) and (source[index].isalnum() or source[index] in "_$#@"):
                index += 1
            tokens.append(_token("word", source, start, index, depth))
            continue

        if character.isdigit():
            index += 1
            while index < len(source) and (source[index].isdigit() or source[index] in ".eE+-"):
                index += 1
            tokens.append(_token("number", source, start, index, depth))
            continue

        two = source[index : index + 2]
        if two in {"<=", ">=", "<>", "!=", "||", "::", "->"}:
            index += 2
            tokens.append(_token("operator", source, start, index, depth))
            continue

        if character == "(":
            tokens.append(_token("symbol", source, start, start + 1, depth))
            depth += 1
            index += 1
            continue

        if character == ")":
            depth -= 1
            if depth < 0:
                return tokens, "SQL contains an unmatched closing parenthesis."
            tokens.append(_token("symbol", source, start, start + 1, depth))
            index += 1
            continue

        kind = "operator" if character in "=<>+-*/%" else "symbol"
        index += 1
        tokens.append(_token(kind, source, start, index, depth))

    if depth != 0:
        return tokens, "SQL contains unmatched parentheses."
    return tokens, None


def _token(kind: str, source: str, start: int, end: int, depth: int) -> _Token:
    text = source[start:end]
    return _Token(kind, text, text.upper(), start, end, depth)


def _trimmed_span(source: str, span: SqlSpan) -> SqlSpan:
    start = max(0, span.start)
    end = min(len(source), span.end)
    while start < end and source[start].isspace():
        start += 1
    while end > start and source[end - 1].isspace():
        end -= 1
    return SqlSpan(start, end)


def _is_trivia(token: _Token) -> bool:
    return token.kind in {"whitespace", "comment"}


def _overlaps(token: _Token, span: SqlSpan) -> bool:
    return token.end > span.start and token.start < span.end


def _is_identifier_token(token: _Token) -> bool:
    return token.kind in {"word", "quoted"}


__all__ = ["parse_sql"]
