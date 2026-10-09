"""Where each original block landed in the generated Python."""

from __future__ import annotations

from dataclasses import dataclass

from vg2c.frontend.models import SourceSpan


@dataclass(frozen=True, slots=True)
class SourceRange:
    start_offset: int
    end_offset: int


@dataclass(frozen=True, slots=True)
class EmittedBlock:
    """One original block and the Python call carrying it (a controller's call also carries its ELSE/END)."""

    block_index: int
    class_name: str
    source: str
    source_range: SourceRange
    input_span: SourceSpan


@dataclass(frozen=True, slots=True)
class EmittedScript:
    source: str
    imports: tuple[str, ...]
    blocks: tuple[EmittedBlock, ...]
