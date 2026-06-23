from __future__ import annotations

from dataclasses import dataclass


@dataclass(frozen=True)
class SourceSpan:
    file: str
    start_line: int
    end_line: int


@dataclass(frozen=True)
class ParsedBlock:
    index: int
    span: SourceSpan
    options: dict[str, str]
    body: str
    raw: str
