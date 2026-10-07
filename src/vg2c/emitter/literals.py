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
    quote = '"' if '"""' not in text else "'"
    if quote * 3 in text:
        return repr(text)
    return quote * 3 + escape_string(text, quote) + quote * 3
