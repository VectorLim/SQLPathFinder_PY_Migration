from __future__ import annotations

import re

_KEY_RE = re.compile(r"/[A-Z_][A-Z0-9_-]*=")


def parse_options(header: str) -> dict[str, str]:
    """Parse VG2 option header text into key-value pairs."""
    text = header.strip()
    if text.startswith("<OPTIONS>"):
        text = text[len("<OPTIONS>") :]
    if text.endswith("</OPTIONS>"):
        text = text[: -len("</OPTIONS>")]

    boundaries: list[tuple[int, int, str]] = []
    for match in _KEY_RE.finditer(text):
        start = match.start()
        if start != 0 and not text[start - 1].isspace():
            continue
        token = match.group()
        key = token[1:-1]
        boundaries.append((start, match.end(), key))

    options: dict[str, str] = {}
    for idx, (_, value_start, key) in enumerate(boundaries):
        next_start = boundaries[idx + 1][0] if idx + 1 < len(boundaries) else len(text)
        value = text[value_start:next_start].strip()
        options[key] = value

    return options
