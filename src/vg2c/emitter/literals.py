"""Readable Python strings that preserve SQL/report content exactly."""

from __future__ import annotations


def escape_string(text: str, quote: str = '"') -> str:
    escaped = text.replace("\\", "\\\\").replace("\r", "\\r").replace("\0", "\\x00")
    if escaped.endswith(quote):
        escaped = escaped[:-1] + "\\" + quote
    return escaped


def string_literal(text: str) -> str:
    if "\n" not in text:
        return repr(text)
    if (
        "\\" in text
        and '"""' not in text
        and not text.endswith(("\\", '"'))
        and not {"\r", "\0"} & set(text)
    ):
        return 'r"""' + text + '"""'
    quote = '"' if '"""' not in text else "'"
    if quote * 3 in text:
        return repr(text)
    return quote * 3 + escape_string(text, quote) + quote * 3


def py_string(text: str) -> str:
    """Readable literal: double quotes, a raw string for Windows paths, triple quotes for multiline text."""
    if "\n" in text:
        return string_literal(text)
    literal = repr(text)
    if '"' in text or not text.isprintable():
        return literal
    if "\\" in text and not text.endswith("\\") and "'" not in text:
        return f'r"{text}"'
    return '"' + literal[1:-1].replace("\\'", "'") + '"'
