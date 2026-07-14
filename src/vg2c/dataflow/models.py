from __future__ import annotations

from dataclasses import dataclass
from typing import Literal, Mapping

from vg2c.frontend.models import SourceSpan
from vg2c.kind import Kind
from vg2c.resolver.models import ResolvedProgram


@dataclass(frozen=True, slots=True)
class CSVGenerationCall:
    """Parsed SQL_Get_CSV_List call."""

    name: Literal["SQL_Get_CSV_List"]
    csv_path: str
    column_ref: int | str
    lead_in: str
    source_span: SourceSpan

    def consumed_csv_paths(self) -> tuple[str, ...]:
        """Return CSV paths consumed by this call."""
        return (self.csv_path,)


ConsumerKind = Literal["table", "start-macro", "rows-in-file", "sql-macro", "run-loop"]


ScopeRelation = Literal[
    "same-scope",
    "consumer-deeper",
    "producer-deeper-loop",
    "producer-in-other-branch",
    "no-producer",
]


@dataclass(frozen=True, slots=True)
class ProducerRecord:
    block_index: int
    csv_path: str
    scope_id: int
    producer_kind: Kind
    is_conditional: bool
    is_in_loop: bool


@dataclass(frozen=True, slots=True)
class ConsumerRecord:
    block_index: int
    csv_path: str
    scope_id: int
    consumer_kind: ConsumerKind


@dataclass(frozen=True, slots=True)
class DataflowEdge:
    csv_path: str
    producer: ProducerRecord | None
    consumer: ConsumerRecord
    scope_relation: ScopeRelation
    order_ok: bool


@dataclass(frozen=True, slots=True)
class AnalyzedProgram:
    resolved: ResolvedProgram
    producers: tuple[ProducerRecord, ...]
    producers_by_path: Mapping[str, tuple[ProducerRecord, ...]]
    consumers: tuple[ConsumerRecord, ...]
    edges: tuple[DataflowEdge, ...]
    csv_generation_calls_by_block: Mapping[int, tuple[CSVGenerationCall, ...]]

