"""Promote only operation/date predicates to ordinary editable constants."""

from __future__ import annotations

import re

from vg2c.emitter.literals import escape_string, string_literal
from vg2c.sql_lexer import lex_sql

_DURATION = re.compile(r"(?:TRUNC\s*\(\s*SYSDATE\s*\)|SYSDATE)\s*-\s*\d+", re.I)


def render_sql(sql: str, constants: dict[str, str]) -> str:
    if '"""' in sql:
        return string_literal(sql)
    tokens, error = lex_sql(sql)
    # Legacy SQL_Get_CSV_List wrappers can be unbalanced until runtime preprocessing.
    # An advisory setting scan must not validate or rewrite that legacy syntax.
    if error and "parenthes" not in error:
        return string_literal(sql)
    tokens = [t for t in tokens if t.kind not in {"comment", "whitespace"}]
    replacements = []
    for i, token in enumerate(tokens[:-2]):
        column = token.text.strip('"`[]').upper()
        operator, operand = tokens[i + 1 : i + 3]
        if column == "OPERATION" and operator.text == "=" and operand.kind == "string":
            value = operand.text[1:-1].replace("''", "'")
            if "<<<" not in value:
                name = _constant_name("OPERATION", value, constants)
                expression = "{" + name + '.replace("\'", "\'\'")' + "}"
                replacements.append((operand.start + 1, operand.end - 1, expression))
        elif column == "OUT_DATE" and operator.text == ">=":
            match = _DURATION.match(sql, operand.start)
            if match is None:
                continue
            if not any(t.end == match.end() for t in tokens[i + 2 :]):
                continue
            # Never replace a prefix of a longer arithmetic expression or comment.
            following = next((t for t in tokens[i + 2 :] if t.start >= match.end()), None)
            if following and following.upper not in {"AND", "OR", ")", ";", "GROUP", "ORDER"}:
                continue
            name = _constant_name("DURATION", match.group(), constants)
            replacements.append((match.start(), match.end(), "{" + name + "}"))
    if not replacements:
        return string_literal(sql)
    pieces, cursor = [], 0
    for start, end, expression in sorted(replacements):
        pieces.append(escape_string(sql[cursor:start]).replace("{", "{{").replace("}", "}}"))
        pieces.append(expression)
        cursor = end
    pieces.append(escape_string(sql[cursor:]).replace("{", "{{").replace("}", "}}"))
    return 'f"""' + "".join(pieces) + '"""'


def _constant_name(base: str, value: str, constants: dict[str, str]) -> str:
    name, suffix = base, 2
    while name in constants and constants[name] != value:
        name = f"{base}_{suffix}"
        suffix += 1
    constants[name] = value
    return name
