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
    ProducerKind,
    ProducerRecord,
    ScopeRelation,
)
from vg2c.frontend.models import Diagnostic, Kind
from vg2c.resolver.models import ResolvedProgram, RowsInFile, ScopeNode, StartMacro

_CSV_TOKEN_RE = re.compile(r"[A-Za-z0-9_./\\-]+\.(?:csv|tab|txt)", re.IGNORECASE)


def analyze(resolved: ResolvedProgram) -> AnalyzedProgram:
    diagnostics: list[Diagnostic] = list(resolved.diagnostics)
    blocks = list(resolved.blocks)
    block_by_index = {block.parsed.index: block for block in blocks}
    scope_rel = _ScopeRelations(resolved.scope_tree)

    explicit_producers = _collect_explicit_producers(blocks, scope_rel)
    utility_candidates = _collect_external_utility_candidates(blocks, scope_rel)
    consumers = _collect_consumers(blocks)

    producers_by_path = _index_by_path(explicit_producers)
    _emit_multi_producer_diagnostics(
        producers_by_path, scope_rel, diagnostics, block_by_index
    )

    edges: list[DataflowEdge] = []
    matched_producer_keys: set[tuple[int, str]] = set()

    for consumer in consumers:
        producer = _choose_explicit_producer(consumer, producers_by_path)
        if producer is None:
            external = _choose_external_candidate(consumer, utility_candidates)
            if external is not None:
                producer = external
                diagnostics.append(
                    Diagnostic(
                        severity="info",
                        code="dataflow-likely-external-producer",
                        message=(
                            f"Consumer path {consumer.csv_path} likely produced by a preceding utility block."
                        ),
                        block_index=consumer.block_index,
                        span=block_by_index[consumer.block_index].parsed.span,
                    )
                )

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
            if not order_ok:
                diagnostics.append(
                    Diagnostic(
                        severity="warning",
                        code="dataflow-order-violation",
                        message=(
                            f"Consumer index {consumer.block_index} appears before or at producer index {producer.block_index} "
                            f"for {consumer.csv_path}."
                        ),
                        block_index=consumer.block_index,
                        span=block_by_index[consumer.block_index].parsed.span,
                    )
                )

            if producer.is_conditional and not scope_rel.is_ancestor(
                producer.scope_id, consumer.scope_id
            ):
                diagnostics.append(
                    Diagnostic(
                        severity="warning",
                        code="dataflow-scope-crossing-branch",
                        message=(
                            f"Consumer {consumer.csv_path} is outside producer conditional branch scope."
                        ),
                        block_index=consumer.block_index,
                        span=block_by_index[consumer.block_index].parsed.span,
                    )
                )
            elif producer.is_in_loop and not scope_rel.is_ancestor(
                producer.scope_id, consumer.scope_id
            ):
                diagnostics.append(
                    Diagnostic(
                        severity="info",
                        code="dataflow-scope-crossing-loop",
                        message=(
                            f"Consumer {consumer.csv_path} is outside producer macro-loop scope."
                        ),
                        block_index=consumer.block_index,
                        span=block_by_index[consumer.block_index].parsed.span,
                    )
                )

    unused = tuple(
        producer
        for producer in explicit_producers
        if (producer.block_index, producer.csv_path) not in matched_producer_keys
    )
    for producer in unused:
        diagnostics.append(
            Diagnostic(
                severity="info",
                code="dataflow-unused-output",
                message=f"Produced CSV {producer.csv_path} has no structural consumer.",
                block_index=producer.block_index,
                span=block_by_index[producer.block_index].parsed.span,
            )
        )

    producers_map = {k: tuple(v) for k, v in producers_by_path.items()}
    return AnalyzedProgram(
        resolved=resolved,
        producers=tuple(explicit_producers),
        producers_by_path=MappingProxyType(producers_map),
        consumers=tuple(consumers),
        edges=tuple(edges),
        unused_producers=unused,
        diagnostics=tuple(diagnostics),
    )


def _collect_explicit_producers(
    blocks,
    scope_rel: "_ScopeRelations",
) -> list[ProducerRecord]:
    producers: list[ProducerRecord] = []
    for block in blocks:
        csv_value = block.resolved_options.lookup.get("CSV")
        if not csv_value:
            continue
        csv_path = _normalize_csv_path(csv_value)
        producers.append(
            ProducerRecord(
                block_index=block.parsed.index,
                csv_path=csv_path,
                scope_id=block.scope_id,
                producer_kind=_producer_kind_for_block(block.kind),
                is_conditional=scope_rel.is_under_kind(
                    block.scope_id, {"if-branch", "else-branch"}
                ),
                is_in_loop=scope_rel.is_under_kind(block.scope_id, {"macro"}),
            )
        )
    return producers


def _collect_external_utility_candidates(
    blocks,
    scope_rel: "_ScopeRelations",
) -> list[ProducerRecord]:
    candidates: list[ProducerRecord] = []
    for block in blocks:
        if block.kind is not Kind.UTILITY:
            continue
        utilities = block.resolved_options.lookup.get("UTILITIES", "")
        for token in _CSV_TOKEN_RE.findall(utilities):
            candidates.append(
                ProducerRecord(
                    block_index=block.parsed.index,
                    csv_path=_normalize_csv_path(token),
                    scope_id=block.scope_id,
                    producer_kind="external-presumed",
                    is_conditional=scope_rel.is_under_kind(
                        block.scope_id, {"if-branch", "else-branch"}
                    ),
                    is_in_loop=scope_rel.is_under_kind(block.scope_id, {"macro"}),
                )
            )
    return candidates


def _collect_consumers(blocks) -> list[ConsumerRecord]:
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
                            block_index=block.parsed.index,
                            csv_path=_normalize_csv_path(table_item),
                            scope_id=block.scope_id,
                            consumer_kind="table",
                        )
                    )

        payload = block.control_payload
        if isinstance(payload, StartMacro) and payload.csv_path:
            consumers.append(
                ConsumerRecord(
                    block_index=block.parsed.index,
                    csv_path=_normalize_csv_path(payload.csv_path),
                    scope_id=block.scope_id,
                    consumer_kind="start-macro",
                )
            )
        elif isinstance(payload, RowsInFile) and payload.csv_path:
            consumers.append(
                ConsumerRecord(
                    block_index=block.parsed.index,
                    csv_path=_normalize_csv_path(payload.csv_path),
                    scope_id=block.scope_id,
                    consumer_kind="rows-in-file",
                )
            )

        for call in block.sql_macro_calls:
            consumers.append(
                ConsumerRecord(
                    block_index=block.parsed.index,
                    csv_path=_normalize_csv_path(call.csv_path),
                    scope_id=block.scope_id,
                    consumer_kind="sql-macro",
                )
            )
    return consumers


def _producer_kind_for_block(kind: Kind) -> ProducerKind:
    if kind is Kind.WRITE_FILE:
        return "write-file"
    if kind in {Kind.MARS_READ, Kind.OASYS_READ, Kind.ARIES_READ}:
        return "db-read"
    if kind is Kind.SQLITE_QUERY:
        return "sqlite-query"
    return "unknown"


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


def _emit_multi_producer_diagnostics(
    producers_by_path: dict[str, list[ProducerRecord]],
    scope_rel: "_ScopeRelations",
    diagnostics: list[Diagnostic],
    block_by_index,
) -> None:
    for path, producers in producers_by_path.items():
        if len(producers) < 2:
            continue
        for i in range(len(producers)):
            for j in range(i + 1, len(producers)):
                left = producers[i]
                right = producers[j]
                if _are_branch_exclusive(left.scope_id, right.scope_id, scope_rel):
                    diagnostics.append(
                        Diagnostic(
                            severity="info",
                            code="dataflow-branch-exclusive-producers",
                            message=f"CSV {path} has branch-exclusive producers.",
                            block_index=right.block_index,
                            span=block_by_index[right.block_index].parsed.span,
                        )
                    )
                else:
                    diagnostics.append(
                        Diagnostic(
                            severity="info",
                            code="dataflow-overwrite-same-scope",
                            message=f"CSV {path} is produced multiple times in overlapping scopes.",
                            block_index=right.block_index,
                            span=block_by_index[right.block_index].parsed.span,
                        )
                    )


def _are_branch_exclusive(
    scope_a: int, scope_b: int, scope_rel: "_ScopeRelations"
) -> bool:
    branch_a = scope_rel.nearest_kind(scope_a, {"if-branch", "else-branch"})
    branch_b = scope_rel.nearest_kind(scope_b, {"if-branch", "else-branch"})
    if branch_a is None or branch_b is None or branch_a == branch_b:
        return False
    parent_a = scope_rel.parent_of.get(branch_a)
    parent_b = scope_rel.parent_of.get(branch_b)
    if parent_a is None or parent_b is None:
        return False
    if parent_a != parent_b:
        return False
    return scope_rel.kind_of.get(branch_a) != scope_rel.kind_of.get(branch_b)


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
