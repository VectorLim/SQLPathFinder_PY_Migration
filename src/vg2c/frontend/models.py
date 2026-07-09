from __future__ import annotations

from dataclasses import dataclass, fields
from enum import Enum
from pathlib import Path
from types import MappingProxyType
from typing import Iterable, Literal, Mapping


def copy_dataclass_fields(source: object, target: object, base: type) -> None:
    for field in fields(base):
        object.__setattr__(target, field.name, getattr(source, field.name))



class Kind(str, Enum):
    SQL_QUERY = "SQL_QUERY"
    SQLITE_QUERY = "SQLITE_QUERY"
    WRITE_FILE = "WRITE_FILE"
    FS_COPY = "FS_COPY"
    FS_DELETE = "FS_DELETE"
    EXTERNAL_RUN = "EXTERNAL_RUN"
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
class ClassifiedBlock(ParsedBlock):
    kind: Kind
    reason: str

    def __init__(self, parsed: ParsedBlock, kind: Kind, reason: str) -> None:
        copy_dataclass_fields(parsed, self, ParsedBlock)
        object.__setattr__(self, "kind", kind)
        object.__setattr__(self, "reason", reason)



@dataclass(frozen=True, slots=True)
class Diagnostic:
    severity: Literal["info", "warning", "error"]
    code: str
    message: str
    block_index: int | None = None
    span: SourceSpan | None = None
    hint: str | None = None
