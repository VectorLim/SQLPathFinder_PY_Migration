"""Main's source-range and invocation identity concepts, without editor machinery."""

from __future__ import annotations

from dataclasses import dataclass

from vg2c.frontend.models import SourceSpan


@dataclass(frozen=True, slots=True)
class SourceRange:
    start_offset: int
    end_offset: int


@dataclass(frozen=True, slots=True)
class EmittedParameter:
    id: str
    name: str
    position: int | None
    source: str
    value: object
    source_range: SourceRange


@dataclass(frozen=True, slots=True)
class EmittedInvocation:
    id: str
    operation: str
    source_range: SourceRange
    parameters: tuple[EmittedParameter, ...]


@dataclass(frozen=True, slots=True)
class EmittedBlock:
    block_index: int
    functional_kind: str
    source: str
    source_range: SourceRange
    invocations: tuple[EmittedInvocation, ...]
    input_span: SourceSpan


@dataclass(frozen=True, slots=True)
class EmittedScript:
    source: str
    imports: tuple[str, ...]
    blocks: tuple[EmittedBlock, ...]
