from __future__ import annotations

import json
import re
from collections import Counter
from dataclasses import dataclass
from typing import Any

from vg2c_ui.domain.semantic_models import (
    SqlEditableModel,
    SqlEntityKind,
    SqlEntityRef,
    SqlEntityResolution,
    SqlJoin,
    SqlPredicate,
    SqlSelection,
    SqlSource,
)


@dataclass(frozen=True, slots=True)
class _EntityCandidate:
    ordinal: int
    entity: Any
    fingerprint: str
    exact_safe: bool = True


class SqlEntityResolver:
    """Resolve revision-safe structured SQL references without positional guessing."""

    def resolve(
        self,
        model: SqlEditableModel,
        ref: SqlEntityRef,
        *,
        document_id: str,
        step_id: str,
        sql_parameter_id: str,
        document_revision: int,
        output_hash: str,
    ) -> SqlEntityResolution:
        if ref.document_id != document_id:
            return SqlEntityResolution(
                status="stale", reason="SQL entity reference belongs to a different document."
            )
        if ref.step_id != step_id:
            return SqlEntityResolution(
                status="stale", reason="SQL entity reference belongs to a different step."
            )
        if ref.sql_parameter_id != sql_parameter_id:
            return SqlEntityResolution(
                status="stale",
                reason="SQL entity reference belongs to a different SQL parameter.",
            )
        if ref.document_revision != document_revision:
            return SqlEntityResolution(
                status="stale", reason="SQL entity reference revision is stale."
            )
        if ref.output_hash != output_hash:
            return SqlEntityResolution(
                status="stale", reason="SQL entity reference output hash is stale."
            )

        candidates = _entities(model, ref.entity_kind)
        target_fingerprint = _normalize(ref.fingerprint)
        exact = next((item for item in candidates if item.entity.id == ref.parsed_id), None)
        if exact and exact.exact_safe and exact.fingerprint == target_fingerprint:
            return SqlEntityResolution(
                status="resolved",
                ref=_refreshed_ref(
                    ref,
                    exact,
                    document_revision=document_revision,
                    output_hash=output_hash,
                ),
            )

        matches = [item for item in candidates if item.fingerprint == target_fingerprint]
        if not matches:
            return SqlEntityResolution(
                status="not_found",
                reason="No current SQL entity matches the captured semantic fingerprint.",
            )
        if len(matches) > 1:
            return SqlEntityResolution(
                status="ambiguous",
                reason=(
                    f"{len(matches)} current SQL entities match the captured semantic fingerprint; "
                    "the ordinal hint is not used to guess between duplicates."
                ),
            )
        return SqlEntityResolution(
            status="resolved",
            ref=_refreshed_ref(
                ref,
                matches[0],
                document_revision=document_revision,
                output_hash=output_hash,
            ),
        )

    def make_ref(
        self,
        model: SqlEditableModel,
        *,
        document_id: str,
        step_id: str,
        sql_parameter_id: str,
        entity_kind: SqlEntityKind,
        parsed_id: str,
        document_revision: int,
        output_hash: str,
    ) -> SqlEntityRef:
        candidates = _entities(model, entity_kind)
        match = next((item for item in candidates if item.entity.id == parsed_id), None)
        if match is None:
            raise ValueError(f"SQL entity {parsed_id!r} was not found.")
        return SqlEntityRef(
            document_id=document_id,
            step_id=step_id,
            sql_parameter_id=sql_parameter_id,
            entity_kind=entity_kind,
            parsed_id=match.entity.id,
            fingerprint=match.fingerprint,
            ordinal_hint=match.ordinal,
            document_revision=document_revision,
            output_hash=output_hash,
        )


def _entities(model: SqlEditableModel, kind: SqlEntityKind) -> list[_EntityCandidate]:
    if kind == "selection":
        return [
            _EntityCandidate(index, selection, _selection_fingerprint(selection))
            for index, selection in enumerate(model.selections)
        ]
    if kind == "filter":
        return [
            _EntityCandidate(index, predicate, _predicate_fingerprint(predicate))
            for index, predicate in enumerate(model.filters)
        ]
    if kind == "join":
        return [
            _EntityCandidate(index, join, _join_fingerprint(join))
            for index, join in enumerate(model.joins)
        ]

    parent_bases = [_join_parent_base(join) for join in model.joins]
    parent_counts = Counter(parent_bases)
    parents = {join.id: join for join in model.joins}

    if kind == "source":
        candidates: list[_EntityCandidate] = []
        for index, source in enumerate(model.sources):
            if source.kind == "from":
                candidates.append(
                    _EntityCandidate(index, source, _source_fingerprint(source, None, 1))
                )
                continue
            parent = parents.get(source.join_id)
            base = _join_parent_base(parent) if parent else "<missing-parent-join>"
            count = parent_counts.get(base, 1)
            candidates.append(
                _EntityCandidate(
                    index,
                    source,
                    _source_fingerprint(source, parent, count),
                    exact_safe=count == 1,
                )
            )
        return candidates

    candidates = []
    ordinal = 0
    for join in model.joins:
        base = _join_parent_base(join)
        count = parent_counts[base]
        parent_context = _join_parent_context(base, count)
        for predicate in join.predicates:
            candidates.append(
                _EntityCandidate(
                    ordinal,
                    predicate,
                    _join_predicate_fingerprint(predicate, parent_context),
                    exact_safe=count == 1,
                )
            )
            ordinal += 1
    return candidates


def _refreshed_ref(
    ref: SqlEntityRef,
    candidate: _EntityCandidate,
    *,
    document_revision: int,
    output_hash: str,
) -> SqlEntityRef:
    return ref.model_copy(
        update={
            "parsed_id": candidate.entity.id,
            "fingerprint": candidate.fingerprint,
            "ordinal_hint": candidate.ordinal,
            "document_revision": document_revision,
            "output_hash": output_hash,
        }
    )


def _selection_fingerprint(selection: SqlSelection) -> str:
    text = selection.expression
    if selection.alias:
        text = f"{text} AS {selection.alias}"
    return _normalize(text)


def _predicate_fingerprint(predicate: SqlPredicate) -> str:
    return _normalize(f"{predicate.left} {predicate.operator} {predicate.right}")


def _source_fingerprint(
    source: SqlSource, parent_join: SqlJoin | None, parent_count: int
) -> str:
    if source.kind == "from":
        return _contextual_fingerprint("source", "from", source.expression)
    if parent_join is None:
        return _contextual_fingerprint(
            "source", "join", source.expression, "<missing-parent-join>"
        )
    parent_context = _join_parent_context(_join_parent_base(parent_join), parent_count)
    return _contextual_fingerprint("source", "join", source.expression, parent_context)


def _join_predicate_fingerprint(predicate: SqlPredicate, parent_context: str) -> str:
    return _contextual_fingerprint(
        "join_predicate", parent_context, _predicate_fingerprint(predicate)
    )


def _join_parent_base(join: SqlJoin) -> str:
    return _contextual_fingerprint("join_parent", join.join_type, join.source)


def _join_parent_context(base: str, sibling_count: int) -> str:
    return _contextual_fingerprint("join_parent_context", base, str(sibling_count))


def _join_fingerprint(join: SqlJoin) -> str:
    text = f"{join.join_type} JOIN {join.source}"
    if join.predicates:
        predicate_text = []
        for index, predicate in enumerate(join.predicates):
            prefix = "" if index == 0 else f"{predicate.connector or 'AND'} "
            predicate_text.append(
                f"{prefix}{predicate.left} {predicate.operator} {predicate.right}"
            )
        text = f"{text} ON {' '.join(predicate_text)}"
    return _normalize(text)


def _contextual_fingerprint(kind: str, *parts: str) -> str:
    return json.dumps(
        [kind, *(_normalize(part) for part in parts)],
        separators=(",", ":"),
    )


def _normalize(value: str) -> str:
    return re.sub(r"\s+", " ", value.strip())
