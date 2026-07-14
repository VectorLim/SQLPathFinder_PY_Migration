from __future__ import annotations

from dataclasses import dataclass
from typing import Literal, Mapping

from vg2c.kind import Kind
from vg2c.resolver.models import ResolvedProgram


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

