"""Shared SQL tokens with exact source spans; no query rewriting."""

from __future__ import annotations

from dataclasses import dataclass


@dataclass(frozen=True, slots=True)
class SqlToken:
    kind: str
    text: str
    upper: str
    start: int
    end: int
    depth: int


def lex_sql(source: str) -> tuple[list[SqlToken], str | None]:
    tokens: list[SqlToken] = []
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


def _token(kind: str, source: str, start: int, end: int, depth: int) -> SqlToken:
    text = source[start:end]
    return SqlToken(kind, text, text.upper(), start, end, depth)
