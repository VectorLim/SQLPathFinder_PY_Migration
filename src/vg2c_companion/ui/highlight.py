"""Lexical highlighting for generated code and asset panes."""

from __future__ import annotations

import keyword
import re
from pathlib import Path

TAG_COLOURS = (
    ("keyword", "#005cc5"),
    ("string", "#a31515"),
    ("comment", "#6a737d"),
    ("markup", "#795da3"),
    ("number", "#008080"),
)


def mode_for_name(name: str) -> str:
    suffix = Path(name).suffix.lower()
    return {
        ".py": "python",
        ".sql": "sql",
        ".html": "html",
        ".htm": "html",
        ".css": "css",
    }.get(suffix, "text")


def highlight_ranges(text: str, mode: str = "python") -> list[tuple[str, int, int]]:
    """Return simple lexical tags for a generated code or asset pane."""
    tags: list[tuple[str, int, int]] = []
    if mode == "python":
        import tokenize
        from io import StringIO

        line_starts = [0]
        for index, character in enumerate(text):
            if character == "\n":
                line_starts.append(index + 1)
        try:
            for token in tokenize.generate_tokens(StringIO(text).readline):
                start = (
                    line_starts[min(token.start[0] - 1, len(line_starts) - 1)]
                    + token.start[1]
                )
                end = (
                    line_starts[min(token.end[0] - 1, len(line_starts) - 1)]
                    + token.end[1]
                )
                if token.type == tokenize.NAME and keyword.iskeyword(token.string):
                    tags.append(("keyword", start, end))
                elif token.type == tokenize.STRING:
                    tags.append(("string", start, end))
                elif token.type == tokenize.COMMENT:
                    tags.append(("comment", start, end))
        except (IndentationError, tokenize.TokenError):
            pass
    else:
        pattern = re.compile(
            r"--[^\n]*|/\*.*?\*/|<!--[\s\S]*?-->|#[^\n]*|"
            r"'(?:''|[^'])*'|\"(?:\"\"|[^\"])*\"",
            re.MULTILINE,
        )
        for match in pattern.finditer(text):
            value = match.group(0)
            tag = "comment" if value.startswith(("--", "/*", "<!--")) else "string"
            tags.append((tag, match.start(), match.end()))
        if mode == "sql":
            for match in re.finditer(
                r"\b(?:SELECT|FROM|WHERE|JOIN|LEFT|RIGHT|INNER|OUTER|ON|GROUP|ORDER|BY|AS|INSERT|UPDATE|DELETE|CREATE|DROP|AND|OR|NOT|NULL)\b",
                text,
                re.IGNORECASE,
            ):
                tags.append(("keyword", match.start(), match.end()))
        elif mode == "html":
            for match in re.finditer(r"<!--[\s\S]*?-->|</?[A-Za-z][^>]*>", text):
                if not match.group(0).startswith("<!--"):
                    tags.append(("markup", match.start(), match.end()))
        elif mode == "css":
            for match in re.finditer(
                r"#[0-9A-Fa-f]{3,8}\b|[-A-Za-z_][\w-]*(?=\s*:)", text
            ):
                tags.append(
                    (
                        "number" if match.group(0).startswith("#") else "keyword",
                        match.start(),
                        match.end(),
                    )
                )
    return tags
