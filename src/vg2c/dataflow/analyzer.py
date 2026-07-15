from __future__ import annotations

import re
from collections import defaultdict
from pathlib import PurePosixPath
from types import MappingProxyType

from vg2c.dataflow.models import (
    AnalyzedProgram,
    ConsumerKind,
    ConsumerRecord,
    DataflowEdge,
    ProducerRecord,
    ScopeRelation,
)
from vg2c.kind import Kind
from vg2c.resolver.models import (
    ResolvedBlock,
    ResolvedProgram,
)
from vg2c.operands import (
    RowsInFile,
    RunLoop,
    ScopeNode,
    StartMacro,
)

_CSV_TOKEN_RE = re.compile(r"[A-Za-z0-9_./\\-]+\.(?:csv|tab|txt)", re.IGNORECASE)
_SQL_SCANNED_KINDS = {Kind.SQL_QUERY, Kind.SQLITE_QUERY}


def analyze(resolved: ResolvedProgram) -> AnalyzedProgram:
    # Lazy import to avoid circular dependency: dataflow→utilities→emitter→dispatch→dataflow
    from vg2c.utilities.csv_io import CsvIO

    calls_by_block: dict[int, tuple[CsvIO.SqlGetCsvListCall, ...]] = {}
    for block in resolved.blocks:
        if block.kind not in _SQL_SCANNED_KINDS:
            continue
        calls = tuple(CsvIO.scan_sql_get_csv_list_calls(block.resolved_body))
        if calls:
            calls_by_block[block.index] = calls

    blocks = list(resolved.blocks)
    scope_rel = _ScopeRelations(resolved.scope_tree)

    edges: list[DataflowEdge] = []
    matched_producer_keys: set[tuple[int, str]] = set()
    consumers = _collect_consumers(blocks, calls_by_block)

    explicit_producers = _collect_explicit_producers(blocks, scope_rel)
    producers_by_path = _index_by_path(explicit_producers)

    for consumer in consumers:
        producer = _choose_explicit_producer(consumer, producers_by_path)
        if producer is None:
            utility_candidates = _collect_external_utility_candidates(blocks, scope_rel)
            external = _choose_external_candidate(consumer, utility_candidates)
            if external is not None:
                producer = external

        scope_relation, order_ok = _classify_edge_relation(
            producer, consumer, scope_rel
        )
        edge = DataflowEdge(
            csv_path=consumer.csv_path,
            producer=producer,
            consumer=consumer,
            scope_relation=scope_relation,
            order_ok=order_ok,
        )
        edges.append(edge)

        if producer is not None:
            matched_producer_keys.add((producer.block_index, producer.csv_path))

    producers_map = {k: tuple(v) for k, v in producers_by_path.items()}
    return AnalyzedProgram(
        resolved=resolved,
        producers=tuple(explicit_producers),
        producers_by_path=MappingProxyType(producers_map),
        consumers=tuple(consumers),
        edges=tuple(edges),
    )


def _collect_explicit_producers(
    blocks: list[ResolvedBlock],
    scope_rel: "_ScopeRelations",
) -> list[ProducerRecord]:
    producers: list[ProducerRecord] = []
    for block in blocks:
        csv_value = block.resolved_options.lookup.get("CSV")
        if csv_value:
            csv_path = _normalize_csv_path(csv_value)
            producers.append(
                ProducerRecord(
                    block_index=block.index,
                    csv_path=csv_path,
                    scope_id=block.scope_id,
                    producer_kind=block.kind,
                    is_conditional=scope_rel.is_under_kind(
                        block.scope_id, {"if-branch", "else-branch"}
                    ),
                    is_in_loop=scope_rel.is_under_kind(
                        block.scope_id, {"macro", "loop"}
                    ),
                )
            )

        payload = block.control_payload
        if isinstance(payload, RunLoop) and payload.chunk_csv_path:
            producers.append(
                ProducerRecord(
                    block_index=block.index,
                    csv_path=_normalize_csv_path(payload.chunk_csv_path),
                    scope_id=block.scope_id,
                    producer_kind=block.kind,
                    is_conditional=scope_rel.is_under_kind(
                        block.scope_id, {"if-branch", "else-branch"}
                    ),
                    is_in_loop=True,
                )
            )
    return producers


def _collect_external_utility_candidates(
    blocks: list[ResolvedBlock],
    scope_rel: "_ScopeRelations",
) -> list[ProducerRecord]:
    candidates: list[ProducerRecord] = []
    for block in blocks:
        if not block.kind.is_external_utility:
            continue
        utilities = block.resolved_options.lookup.get("UTILITIES", "")
        for token in _CSV_TOKEN_RE.findall(utilities):
            candidates.append(
                ProducerRecord(
                    block_index=block.index,
                    csv_path=_normalize_csv_path(token),
                    scope_id=block.scope_id,
                    producer_kind=block.kind,
                    is_conditional=scope_rel.is_under_kind(
                        block.scope_id, {"if-branch", "else-branch"}
                    ),
                    is_in_loop=scope_rel.is_under_kind(
                        block.scope_id, {"macro", "loop"}
                    ),
                )
            )
    return candidates


def _collect_consumers(
    blocks: list[ResolvedBlock],
    calls_by_block: dict[int, tuple[CsvIO.SqlGetCsvListCall, ...]],
) -> list[ConsumerRecord]:
    consumers: list[ConsumerRecord] = []
    for block in blocks:
        for key, value in block.resolved_options.pairs:
            if key == "TABLE":
                table_items = [
                    item.strip() for item in value.split(",") if item.strip()
                ]
                for table_item in table_items:
                    consumers.append(
                        ConsumerRecord(
                            block_index=block.index,
                            csv_path=_normalize_csv_path(table_item),
                            scope_id=block.scope_id,
                            consumer_kind="table",
                        )
                    )

        payload = block.control_payload
        payload_csv_path = None
        consumer_kind = None
        if isinstance(payload, StartMacro) and payload.csv_path:
            payload_csv_path = payload.csv_path
            consumer_kind = "start-macro"
        elif isinstance(payload, RowsInFile) and payload.csv_path:
            payload_csv_path = payload.csv_path
            consumer_kind = "rows-in-file"
        elif isinstance(payload, RunLoop) and payload.input_csv_path:
            payload_csv_path = payload.input_csv_path
            consumer_kind = "run-loop"

        if payload_csv_path and consumer_kind:
            consumers.append(
                ConsumerRecord(
                    block_index=block.index,
                    csv_path=_normalize_csv_path(payload_csv_path),
                    scope_id=block.scope_id,
                    consumer_kind=consumer_kind,
                )
            )

        for call in calls_by_block.get(block.index, ()):
            consumers.append(
                ConsumerRecord(
                    block_index=block.index,
                    csv_path=_normalize_csv_path(call.csv_path),
                    scope_id=block.scope_id,
                    consumer_kind="sql-macro",
                )
            )
    return consumers


def _index_by_path(producers: list[ProducerRecord]) -> dict[str, list[ProducerRecord]]:
    by_path: dict[str, list[ProducerRecord]] = defaultdict(list)
    for producer in producers:
        by_path[producer.csv_path].append(producer)
    for items in by_path.values():
        items.sort(key=lambda p: p.block_index)
    return by_path


def _choose_explicit_producer(
    consumer: ConsumerRecord,
    producers_by_path: dict[str, list[ProducerRecord]],
) -> ProducerRecord | None:
    options = producers_by_path.get(consumer.csv_path, [])
    if not options:
        return None
    best: ProducerRecord | None = None
    for producer in options:
        if producer.block_index < consumer.block_index:
            best = producer
    return best if best is not None else options[0]


def _choose_external_candidate(
    consumer: ConsumerRecord,
    candidates: list[ProducerRecord],
) -> ProducerRecord | None:
    best: ProducerRecord | None = None
    for candidate in candidates:
        if candidate.csv_path != consumer.csv_path:
            continue
        if candidate.block_index >= consumer.block_index:
            continue
        best = candidate
    return best


def _classify_edge_relation(
    producer: ProducerRecord | None,
    consumer: ConsumerRecord,
    scope_rel: "_ScopeRelations",
) -> tuple[ScopeRelation, bool]:
    if producer is None:
        return "no-producer", True

    if producer.scope_id == consumer.scope_id:
        relation = "same-scope"
    elif scope_rel.is_ancestor(producer.scope_id, consumer.scope_id):
        relation = "consumer-deeper"
    elif scope_rel.is_ancestor(consumer.scope_id, producer.scope_id):
        relation = (
            "producer-deeper-loop"
            if producer.is_in_loop
            else "producer-in-other-branch"
        )
    else:
        relation = "producer-in-other-branch"

    return relation, consumer.block_index > producer.block_index


def _normalize_csv_path(value: str) -> str:
    normalized = value.strip().strip('"').replace("\\", "/")
    while normalized.startswith("./"):
        normalized = normalized[2:]
    if normalized.startswith(".") and len(normalized) > 1 and normalized[1] == "/":
        normalized = normalized[2:]
    return str(PurePosixPath(normalized)).lower()


class _ScopeRelations:
    def __init__(self, root: ScopeNode) -> None:
        self.parent_of: dict[int, int | None] = {}
        self.depth_of: dict[int, int] = {}
        self.kind_of: dict[int, str] = {}
        self._build(root)

    def _build(self, root: ScopeNode) -> None:
        stack: list[tuple[ScopeNode, int | None, int]] = [(root, None, 0)]
        while stack:
            node, parent, depth = stack.pop()
            self.parent_of[node.scope_id] = parent
            self.depth_of[node.scope_id] = depth
            self.kind_of[node.scope_id] = node.kind
            for child in reversed(node.children):
                stack.append((child, node.scope_id, depth + 1))

    def is_ancestor(self, ancestor: int, child: int) -> bool:
        cur = child
        while True:
            if cur == ancestor:
                return True
            parent = self.parent_of.get(cur)
            if parent is None:
                return False
            cur = parent

    def lca(self, a: int, b: int) -> int:
        da = self.depth_of.get(a, 0)
        db = self.depth_of.get(b, 0)
        x, y = a, b
        while da > db:
            x = self.parent_of.get(x) or 0
            da -= 1
        while db > da:
            y = self.parent_of.get(y) or 0
            db -= 1
        while x != y:
            x = self.parent_of.get(x) or 0
            y = self.parent_of.get(y) or 0
        return x

    def is_under_kind(self, scope_id: int, kinds: set[str]) -> bool:
        cur = scope_id
        while True:
            kind = self.kind_of.get(cur)
            if kind in kinds:
                return True
            parent = self.parent_of.get(cur)
            if parent is None:
                return False
            cur = parent

    def nearest_kind(self, scope_id: int, kinds: set[str]) -> int | None:
        cur = scope_id
        while True:
            kind = self.kind_of.get(cur)
            if kind in kinds:
                return cur
            parent = self.parent_of.get(cur)
            if parent is None:
                return None
            cur = parent
