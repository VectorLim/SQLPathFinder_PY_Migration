from __future__ import annotations

from collections.abc import Mapping
from dataclasses import dataclass
from typing import Literal

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
class ArtifactSummary:
    """File-level view derived directly from one analyzed program."""

    path: str
    producers: tuple[ProducerRecord, ...]
    consumers: tuple[ConsumerRecord, ...]
    conditional: bool
    in_loop: bool
    order_valid: bool

    @property
    def is_external_input(self) -> bool:
        return bool(self.consumers) and not self.producers

    @property
    def is_output(self) -> bool:
        return bool(self.producers)


@dataclass(frozen=True, slots=True)
class AnalyzedProgram:
    resolved: ResolvedProgram
    producers: tuple[ProducerRecord, ...]
    producers_by_path: Mapping[str, tuple[ProducerRecord, ...]]
    consumers: tuple[ConsumerRecord, ...]
    edges: tuple[DataflowEdge, ...]

    @property
    def artifacts(self) -> tuple[ArtifactSummary, ...]:
        """Aggregate artifact relationships without re-inferring producer/consumer semantics."""
        producers: dict[str, dict[tuple[int, str], ProducerRecord]] = {}
        consumers: dict[str, list[ConsumerRecord]] = {}
        order_by_path: dict[str, list[bool]] = {}

        for producer in self.producers:
            key = (producer.block_index, producer.csv_path)
            producers.setdefault(producer.csv_path, {})[key] = producer
        for consumer in self.consumers:
            consumers.setdefault(consumer.csv_path, []).append(consumer)
        for edge in self.edges:
            order_by_path.setdefault(edge.csv_path, []).append(edge.order_ok)
            if edge.producer is not None:
                producers.setdefault(edge.producer.csv_path, {})[
                    (edge.producer.block_index, edge.producer.csv_path)
                ] = edge.producer

        summaries: list[ArtifactSummary] = []
        for path in sorted({*producers, *consumers}):
            path_producers = tuple(
                sorted(producers.get(path, {}).values(), key=lambda item: item.block_index)
            )
            path_consumers = tuple(
                sorted(consumers.get(path, ()), key=lambda item: item.block_index)
            )
            order_values = order_by_path.get(path, ())
            summaries.append(
                ArtifactSummary(
                    path=path,
                    producers=path_producers,
                    consumers=path_consumers,
                    conditional=any(item.is_conditional for item in path_producers),
                    in_loop=any(item.is_in_loop for item in path_producers),
                    order_valid=all(order_values) if order_values else True,
                )
            )
        return tuple(summaries)
