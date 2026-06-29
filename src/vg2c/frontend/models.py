from __future__ import annotations

from dataclasses import dataclass
from enum import Enum
from pathlib import Path
from types import MappingProxyType
from typing import Iterable, Literal, Mapping


class Kind(str, Enum):
    MARS_READ = "MARS_READ"
    OASYS_READ = "OASYS_READ"
    ARIES_READ = "ARIES_READ"
    SQLITE_QUERY = "SQLITE_QUERY"
    WRITE_FILE = "WRITE_FILE"
    HTML_REPORT = "HTML_REPORT"
    UTILITY = "UTILITY"
    MACRO_CONTROL = "MACRO_CONTROL"
    UNKNOWN = "UNKNOWN"
    MALFORMED = "MALFORMED"


@dataclass(frozen=True, slots=True)
class BlockOptions:
    pairs: tuple[tuple[str, str], ...]
    lookup: Mapping[str, str]

    @classmethod
    def from_pairs(cls, pairs: Iterable[tuple[str, str]]) -> "BlockOptions":
        ordered_pairs: tuple[tuple[str, str], ...] = tuple(
            (k.upper(), v) for k, v in pairs
        )
        lookup_dict: dict[str, str] = {}
        for key, value in ordered_pairs:
            lookup_dict[key] = value
        return cls(pairs=ordered_pairs, lookup=MappingProxyType(lookup_dict))


@dataclass(frozen=True, slots=True)
class SourceSpan:
    file: Path | None
    start_line: int
    end_line: int


@dataclass(frozen=True, slots=True)
class ParsedBlock:
    index: int
    options: BlockOptions
    body: str
    raw: str
    span: SourceSpan


@dataclass(frozen=True, slots=True)
class ClassifiedBlock:
    parsed: ParsedBlock
    kind: Kind
    reason: str


@dataclass(frozen=True, slots=True)
class Diagnostic:
    severity: Literal["info", "warning", "error"]
    code: str
    message: str
    block_index: int | None = None
    span: SourceSpan | None = None
    hint: str | None = None
