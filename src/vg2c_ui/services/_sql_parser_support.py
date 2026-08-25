from __future__ import annotations

import re
from dataclasses import dataclass

from vg2c_ui.domain.semantic_models import SqlSpan
from vg2c_ui.services._sql_lexer import Token, is_trivia

_CLAUSE_WORDS = frozenset(
    ("WHERE", "GROUP", "HAVING", "ORDER", "LIMIT", "OFFSET", "FETCH", "QUALIFY", "WINDOW")
)
_SIMPLE_JOIN_TYPES = frozenset(("INNER", "LEFT", "RIGHT", "FULL", "CROSS"))


@dataclass(frozen=True, slots=True)
class _Clauses:
    select: Token
    from_: Token | None
    where: Token | None
    group: Token | None
    having: Token | None
    order: Token | None
    limit: Token | None
    offset: Token | None
    fetch: Token | None
    qualify: Token | None
    window: Token | None

    def values(self) -> tuple[Token | None, ...]:
        return (
            self.select, self.from_, self.where, self.group, self.having, self.order,
            self.limit, self.offset, self.fetch, self.qualify, self.window,
        )


def _locate_clauses(significant: list[Token]) -> _Clauses:
    top = [token for token in significant if token.depth == 0]
    select = next(token for token in top if token.upper == "SELECT")
    after_select = [token for token in top if token.start > select.start]

    def find(name: str) -> Token | None:
        return next((token for token in after_select if token.upper == name), None)

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
        if token and token.start > position and token.upper != "SELECT"
    )
    return starts[0] if starts else fallback


def _next_clause_start(position: int, clauses: _Clauses, fallback: int) -> int:
    starts = sorted(
        token.start
        for token in clauses.values()
        if token and token.start > position and token.upper in _CLAUSE_WORDS
    )
    return starts[0] if starts else fallback


def _split_by_top_level_comma(tokens: list[Token], span: SqlSpan) -> list[SqlSpan]:
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
        spans.append(SqlSpan(start=start, end=comma.start))
        start = comma.end
    spans.append(SqlSpan(start=start, end=span.end))
    trimmed = [_trimmed_span_from_tokens(tokens, item) for item in spans]
    return [item for item in trimmed if item.start < item.end]


def _trimmed_span_from_tokens(tokens: list[Token], span: SqlSpan) -> SqlSpan:
    significant = [
        token
        for token in tokens
        if not is_trivia(token) and token.start >= span.start and token.end <= span.end
    ]
    if not significant:
        return SqlSpan(start=span.end, end=span.end)
    return SqlSpan(start=significant[0].start, end=significant[-1].end)


def _join_type_start(tokens: list[Token], join_token: Token) -> int:
    index = tokens.index(join_token)
    cursor = index - 1
    start = join_token.start
    if cursor >= 0 and tokens[cursor].upper == "OUTER":
        start = tokens[cursor].start
        cursor -= 1
    if cursor >= 0 and tokens[cursor].upper in _SIMPLE_JOIN_TYPES:
        start = tokens[cursor].start
        cursor -= 1
    if cursor >= 0 and tokens[cursor].upper == "NATURAL":
        start = tokens[cursor].start
    return start


def _normalize_join_type(raw: str) -> str:
    text = re.sub(r"\bJOIN\s*$", "", raw, flags=re.IGNORECASE)
    text = re.sub(r"\bOUTER\b", "", text, flags=re.IGNORECASE).strip().upper()
    return text or "INNER"
