from __future__ import annotations

import re
from dataclasses import dataclass
from typing import Literal

from vg2c_ui.domain.semantic_models import SqlSpan

TokenKind = Literal[
    "word", "number", "string", "quoted", "operator", "symbol", "whitespace", "comment"
]


@dataclass(frozen=True, slots=True)
class Token:
    kind: TokenKind
    text: str
    upper: str
    start: int
    end: int
    depth: int


def lex_sql(source: str) -> tuple[list[Token], str | None]:
    tokens: list[Token] = []
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
        if character in {'"', '`', '['}:
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
        if re.match(r"[A-Za-z_$#@]", character):
            index += 1
            while index < len(source) and re.match(r"[A-Za-z0-9_$#@]", source[index]):
                index += 1
            tokens.append(_token("word", source, start, index, depth))
            continue
        if character.isdigit():
            index += 1
            while index < len(source) and re.match(r"[\d.eE+-]", source[index]):
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
        kind: TokenKind = "operator" if character in "=<>+-*/%" else "symbol"
        index += 1
        tokens.append(_token(kind, source, start, index, depth))
    if depth != 0:
        return tokens, "SQL contains unmatched parentheses."
    return tokens, None


def _token(kind: TokenKind, source: str, start: int, end: int, depth: int) -> Token:
    text = source[start:end]
    return Token(kind=kind, text=text, upper=text.upper(), start=start, end=end, depth=depth)


def trimmed_span(source: str, span: SqlSpan) -> SqlSpan:
    start = max(0, span.start)
    end = min(len(source), span.end)
    while start < end and source[start].isspace():
        start += 1
    while end > start and source[end - 1].isspace():
        end -= 1
    return SqlSpan(start=start, end=end)


def is_trivia(token: Token) -> bool:
    return token.kind in {"whitespace", "comment"}


def overlaps(token: Token, span: SqlSpan) -> bool:
    return token.end > span.start and token.start < span.end


def is_identifier_token(token: Token) -> bool:
    return token.kind in {"word", "quoted"}
