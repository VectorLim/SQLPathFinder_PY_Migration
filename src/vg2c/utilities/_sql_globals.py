"""Select literal SQL predicate operands without rewriting other SQL syntax."""

from __future__ import annotations

import re
from dataclasses import dataclass, replace

from vg2c.emitter.globals import global_key
from vg2c.sql_lexer import SqlToken, lex_sql
from vg2c.utilities._emit_helpers import scan_sql_get_csv_list_calls
from vg2c.utilities.sqlite_engine import SqliteEngine

_NUMBER = re.compile(SqliteEngine._SQL_NUMBER)
_CLAUSES = {
    "SELECT",
    "FROM",
    "JOIN",
    "GROUP",
    "HAVING",
    "ORDER",
    "LIMIT",
    "OFFSET",
    "FETCH",
    "QUALIFY",
    "WINDOW",
    "UNION",
    "INTERSECT",
    "EXCEPT",
    "RETURNING",
}
_STARTS = {"WHERE", "ON", "AND", "OR", "NOT", "("}
_ENDS = _CLAUSES | {
    "WHERE",
    "ON",
    "LEFT",
    "RIGHT",
    "FULL",
    "INNER",
    "CROSS",
    "NATURAL",
    "AND",
    "OR",
    ")",
    ";",
    "WHEN",
    "THEN",
    "ELSE",
    "END",
}


@dataclass(frozen=True)
class SqlGlobal:
    key: str
    value: object
    start: int
    end: int
    numeric: bool = False


def extract_sql_globals(sql: str) -> list[SqlGlobal]:
    # The emitter closes legacy CSV-list wrappers. Mask only their call spans
    # for lexing, retaining offsets into the original SQL for every candidate.
    lexical_sql = sql
    for call in reversed(scan_sql_get_csv_list_calls(sql)):
        marker = "NULL" + (")" if call.needs_closing_paren else "")
        marker = marker.ljust(call.end - call.start)
        lexical_sql = lexical_sql[: call.start] + marker + lexical_sql[call.end :]
    tokens, error = lex_sql(lexical_sql)
    if error:
        return []
    comments = [token for token in tokens if token.kind == "comment"]
    tokens = [token for token in tokens if token.kind not in {"comment", "whitespace"}]
    result: list[SqlGlobal] = []
    candidates: dict[str, object] = {}
    scopes = [False]
    case_depth = 0
    for index, token in enumerate(tokens):
        previous = tokens[index - 1].upper if index else ""
        if token.upper == "CASE":
            case_depth += 1
        elif token.upper == "END":
            case_depth = max(0, case_depth - 1)
        if token.text == "(":
            scopes.append(scopes[-1] and previous in _STARTS)
        elif token.text == ")":
            scopes.pop()
        elif token.upper in {"WHERE", "ON"}:
            scopes[-1] = True
        elif token.upper in _CLAUSES or token.text == ";":
            scopes[-1] = False
        elif (
            not case_depth
            and scopes[-1]
            and previous in _STARTS
            and token.kind in {"word", "quoted"}
        ):
            for candidate in _predicate(tokens, index):
                if any(c.start < candidate.end and c.end > candidate.start for c in comments):
                    continue
                base = global_key(candidate.key)
                key, suffix = base, 2
                while key in candidates and repr(candidates[key]) != repr(candidate.value):
                    key = f"{base}_{suffix}"
                    suffix += 1
                candidates[key] = candidate.value
                result.append(replace(candidate, key=key))
    return result


def _predicate(tokens: list[SqlToken], index: int) -> list[SqlGlobal]:
    column = tokens[index]
    cursor = index + 1
    while cursor + 1 < len(tokens) and tokens[cursor].text == ".":
        column = tokens[cursor + 1]
        if column.kind not in {"word", "quoted"}:
            return []
        cursor += 2
    key = column.text.strip('"`[]')
    if cursor >= len(tokens):
        return []
    operator = tokens[cursor].upper
    cursor += 1
    if operator == "NOT" and cursor < len(tokens):
        operator = f"NOT {tokens[cursor].upper}"
        cursor += 1
    if operator in {"IN", "NOT IN"}:
        if cursor >= len(tokens) or tokens[cursor].text != "(":
            return []
        cursor += 1
        start = cursor
        values: list[SqlGlobal] = []
        while cursor < len(tokens):
            parsed = _literal(tokens, cursor, key)
            if parsed is None:
                return []
            value, cursor = parsed
            values.append(value)
            if cursor < len(tokens) and tokens[cursor].text == ",":
                cursor += 1
                continue
            break
        if cursor >= len(tokens) or tokens[cursor].text != ")" or not _end(tokens, cursor + 1):
            return []
        # Mixed text and decimal-token lists cannot retain their types in a plain
        # editable Python list. Leave those uncommon operands in SQL verbatim.
        decimal = any(v.numeric and isinstance(v.value, str) for v in values)
        all_numeric = all(v.numeric for v in values)
        if decimal and not all_numeric:
            return []
        return [
            SqlGlobal(
                key,
                [v.value for v in values],
                tokens[start].start,
                values[-1].end,
                numeric=all_numeric,
            )
        ]
    if operator not in {"=", "!=", "<>", "<", "<=", ">", ">=", "BETWEEN", "NOT BETWEEN"}:
        return []
    suffix = "_MIN" if operator in {">", ">="} else "_MAX" if operator in {"<", "<="} else ""
    parsed = _literal(tokens, cursor, key + suffix)
    if parsed is None:
        return []
    value, cursor = parsed
    if operator in {"BETWEEN", "NOT BETWEEN"}:
        if cursor >= len(tokens) or tokens[cursor].upper != "AND":
            return []
        upper = _literal(tokens, cursor + 1, key + "_MAX")
        if upper is None or not _end(tokens, upper[1]):
            return []
        return [replace(value, key=key + "_MIN"), upper[0]]
    return [value] if _end(tokens, cursor) else []


def _end(tokens: list[SqlToken], index: int) -> bool:
    return index == len(tokens) or tokens[index].upper in _ENDS


def _literal(tokens: list[SqlToken], index: int, key: str) -> tuple[SqlGlobal, int] | None:
    if index >= len(tokens):
        return None
    token = tokens[index]
    if token.kind == "string":
        value = token.text[1:-1].replace("''", "'")
        if "<<<" in value:
            return None
        return SqlGlobal(key, value, token.start, token.end), index + 1
    end = index + 1
    if token.text in {"+", "-"} and end < len(tokens):
        end += 1
    if tokens[end - 1].text == "." and end < len(tokens):
        end += 1
    text = "".join(item.text for item in tokens[index:end])
    if not _NUMBER.fullmatch(text):
        return None
    value = int(text) if re.fullmatch(r"[+-]?\d+", text) else text
    return SqlGlobal(key, value, token.start, tokens[end - 1].end, numeric=True), end
