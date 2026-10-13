"""Shared token/span helpers for the structured SQL parser.

Keep lexical mechanics here so parser.py can focus on statement/selection
orchestration while join and predicate modules share one implementation.
"""

from __future__ import annotations

from dataclasses import dataclass

from vg2c.sql_editor.models import SqlSpan
from vg2c.sql_lexer import SqlToken

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
class _Clauses:
    select: SqlToken
    from_: SqlToken | None
    where: SqlToken | None
    group: SqlToken | None
    having: SqlToken | None
    order: SqlToken | None
    limit: SqlToken | None
    offset: SqlToken | None
    fetch: SqlToken | None
    qualify: SqlToken | None
    window: SqlToken | None

    def values(self) -> tuple[SqlToken | None, ...]:
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

def unquote_identifier(value: str) -> str:
    if value.startswith("[") and value.endswith("]"):
        return value[1:-1].replace("]]", "]")
    if value.startswith(('"', "`")) and value.endswith(value[0]):
        return value[1:-1].replace(value[0] * 2, value[0])
    return value

def _locate_clauses(significant: list[SqlToken]) -> _Clauses:
    top = [token for token in significant if token.depth == 0]
    select = next(token for token in top if token.upper == "SELECT")
    after = [token for token in top if token.start > select.start]

    def find(name: str) -> SqlToken | None:
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


def _split_by_top_level_comma(tokens: list[SqlToken], span: SqlSpan) -> list[SqlSpan]:
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


def _trimmed_span_from_tokens(tokens: list[SqlToken], span: SqlSpan) -> SqlSpan:
    significant = [
        token
        for token in tokens
        if not _is_trivia(token) and token.start >= span.start and token.end <= span.end
    ]
    if not significant:
        return SqlSpan(span.end, span.end)
    return SqlSpan(significant[0].start, significant[-1].end)


def _join_type_start(tokens: list[SqlToken], join_token: SqlToken) -> int:
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


def _trimmed_span(source: str, span: SqlSpan) -> SqlSpan:
    start = max(0, span.start)
    end = min(len(source), span.end)
    while start < end and source[start].isspace():
        start += 1
    while end > start and source[end - 1].isspace():
        end -= 1
    return SqlSpan(start, end)


def _is_trivia(token: SqlToken) -> bool:
    return token.kind in {"whitespace", "comment"}


def _overlaps(token: SqlToken, span: SqlSpan) -> bool:
    return token.end > span.start and token.start < span.end


def _is_identifier_token(token: SqlToken) -> bool:
    return token.kind in {"word", "quoted"}
