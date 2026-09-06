from __future__ import annotations

from collections.abc import Iterable
from dataclasses import dataclass, replace
from pathlib import PurePosixPath
from typing import Any, Literal, TypeVar

from vg2c.compilation import CompilationResult
from vg2c.dataflow.analyzer import analyze_records
from vg2c.dataflow.models import AnalyzedProgram, ConsumerRecord, ProducerRecord
from vg2c.editing import ChangeValidationError, ParameterChange, project_changes


@dataclass(frozen=True, slots=True)
class ProjectedDocument:
    document_id: str
    result: CompilationResult
    changes: tuple[ParameterChange, ...]
    analyzed: AnalyzedProgram


@dataclass(frozen=True, slots=True)
class WorkspaceDependencyEdge:
    artifact: str
    producer_document_id: str
    producer_block_index: int
    consumer_document_id: str
    consumer_block_index: int


@dataclass(frozen=True, slots=True)
class WorkspaceDependencyIssue:
    code: Literal["BROKEN_DEPENDENCY", "DUPLICATE_OUTPUT"]
    document_id: str
    block_index: int
    artifact: str
    message: str
    related_document_id: str | None = None
    related_block_index: int | None = None


@dataclass(frozen=True, slots=True)
class WorkspaceProjection:
    documents: tuple[ProjectedDocument, ...]
    dependencies: tuple[WorkspaceDependencyEdge, ...]
    issues: tuple[WorkspaceDependencyIssue, ...]


_Record = TypeVar("_Record", ProducerRecord, ConsumerRecord)


def project_analysis(
    result: CompilationResult, changes: Iterable[ParameterChange]
) -> AnalyzedProgram:
    """Re-run normal dataflow rules after applying artifact-role changes to analyzed records."""
    requested = tuple(changes)
    projection = project_changes(result, requested)
    if not projection.valid:
        raise ChangeValidationError(projection.issues)

    values = {change.parameter_id: change.value for change in requested}
    producers = list(result.analyzed.producers)
    consumers = list(result.analyzed.consumers)
    explicit_keys = {(item.block_index, item.csv_path) for item in producers}
    external_candidates = _external_candidates(result.analyzed, explicit_keys)

    for step in result.emitted.steps:
        for parameter in step.parameters:
            role = parameter.artifact_role
            if role is None or parameter.id not in values:
                continue
            old_paths = _artifact_values(parameter.value)
            new_paths = _artifact_values(values[parameter.id])
            if role.direction == "output":
                producers = _replace_record_paths(
                    producers,
                    block_index=step.block_index,
                    old_paths=old_paths,
                    new_paths=new_paths,
                )
                external_candidates = _replace_record_paths(
                    external_candidates,
                    block_index=step.block_index,
                    old_paths=old_paths,
                    new_paths=new_paths,
                )
            else:
                consumers = _replace_record_paths(
                    consumers,
                    block_index=step.block_index,
                    old_paths=old_paths,
                    new_paths=new_paths,
                )

    return analyze_records(
        result.resolved,
        producers=producers,
        consumers=consumers,
        external_candidates=external_candidates,
    )


def project_workspace(
    documents: Iterable[tuple[str, CompilationResult, Iterable[ParameterChange]]],
) -> WorkspaceProjection:
    """Project every open document, including dirty inactive tabs."""
    projected: list[ProjectedDocument] = []
    for document_id, result, changes in documents:
        change_tuple = tuple(changes)
        projected.append(
            ProjectedDocument(
                document_id=document_id,
                result=result,
                changes=change_tuple,
                analyzed=project_analysis(result, change_tuple),
            )
        )

    baseline = _producer_index(
        (item.document_id, item.result.analyzed) for item in projected
    )
    current = _producer_index((item.document_id, item.analyzed) for item in projected)
    issues: list[WorkspaceDependencyIssue] = []

    for artifact, producers in current.items():
        if len(producers) <= 1:
            continue
        for document_id, block_index in producers:
            related = next((item for item in producers if item != (document_id, block_index)), None)
            issues.append(
                WorkspaceDependencyIssue(
                    code="DUPLICATE_OUTPUT",
                    document_id=document_id,
                    block_index=block_index,
                    artifact=artifact,
                    message=(
                        f"{artifact} is produced by {len(producers)} operations "
                        "across the open workspace."
                    ),
                    related_document_id=related[0] if related else None,
                    related_block_index=related[1] if related else None,
                )
            )

    dependencies: list[WorkspaceDependencyEdge] = []
    for document in projected:
        internal_paths = {
            artifact.path
            for artifact in document.analyzed.artifacts
            if artifact.producers
        }
        baseline_paths_by_block: dict[int, set[str]] = {}
        for consumer in document.result.analyzed.consumers:
            baseline_paths_by_block.setdefault(consumer.block_index, set()).add(consumer.csv_path)

        for consumer in document.analyzed.consumers:
            if consumer.csv_path in internal_paths:
                continue
            producers = [
                candidate
                for candidate in current.get(consumer.csv_path, ())
                if candidate[0] != document.document_id
            ]
            for producer_document_id, producer_block_index in producers:
                dependencies.append(
                    WorkspaceDependencyEdge(
                        artifact=consumer.csv_path,
                        producer_document_id=producer_document_id,
                        producer_block_index=producer_block_index,
                        consumer_document_id=document.document_id,
                        consumer_block_index=consumer.block_index,
                    )
                )
            if producers:
                continue

            baseline_paths = baseline_paths_by_block.get(consumer.block_index, set())
            old_path = next((path for path in baseline_paths if baseline.get(path)), None)
            if old_path is None:
                continue
            related = baseline[old_path][0]
            issues.append(
                WorkspaceDependencyIssue(
                    code="BROKEN_DEPENDENCY",
                    document_id=document.document_id,
                    block_index=consumer.block_index,
                    artifact=consumer.csv_path,
                    message=(
                        f"Missing input: {consumer.csv_path}. Its previously known "
                        "producer now emits a different artifact."
                    ),
                    related_document_id=related[0],
                    related_block_index=related[1],
                )
            )

    return WorkspaceProjection(
        tuple(projected),
        tuple(_dedupe_edges(dependencies)),
        tuple(_dedupe_issues(issues)),
    )


def _replace_record_paths(
    records: list[_Record],
    *,
    block_index: int,
    old_paths: list[str],
    new_paths: list[str],
) -> list[_Record]:
    """Replace only analyzed artifact records owned by the changed compiler block."""
    old = set(old_paths)
    if not old:
        return records
    matching = [
        record
        for record in records
        if record.block_index == block_index and record.csv_path in old
    ]
    if not matching:
        return records

    result = [
        record
        for record in records
        if not (record.block_index == block_index and record.csv_path in old)
    ]
    template = matching[0]
    result.extend(replace(template, csv_path=path) for path in new_paths)
    return result


def _external_candidates(
    analyzed: AnalyzedProgram, explicit_keys: set[tuple[int, str]]
) -> list[ProducerRecord]:
    candidates: dict[tuple[int, str], ProducerRecord] = {}
    for edge in analyzed.edges:
        producer = edge.producer
        if producer is None:
            continue
        key = (producer.block_index, producer.csv_path)
        if key not in explicit_keys:
            candidates[key] = producer
    return list(candidates.values())


def _artifact_values(value: Any) -> list[str]:
    if isinstance(value, str):
        return [_normalize(value)] if value.strip() else []
    if isinstance(value, list) and all(isinstance(item, str) for item in value):
        return [_normalize(item) for item in value if item.strip()]
    return []


def _normalize(value: str) -> str:
    normalized = value.strip().strip('"').strip("'").replace("\\", "/")
    while normalized.startswith("./"):
        normalized = normalized[2:]
    return str(PurePosixPath(normalized)).lower()


def _producer_index(
    documents: Iterable[tuple[str, AnalyzedProgram]],
) -> dict[str, list[tuple[str, int]]]:
    result: dict[str, list[tuple[str, int]]] = {}
    for document_id, analyzed in documents:
        for producer in _all_producers(analyzed):
            ref = (document_id, producer.block_index)
            values = result.setdefault(producer.csv_path, [])
            if ref not in values:
                values.append(ref)
    return result


def _all_producers(analyzed: AnalyzedProgram) -> tuple[ProducerRecord, ...]:
    result: dict[tuple[int, str], ProducerRecord] = {
        (producer.block_index, producer.csv_path): producer
        for producer in analyzed.producers
    }
    for edge in analyzed.edges:
        if edge.producer is not None:
            result[(edge.producer.block_index, edge.producer.csv_path)] = edge.producer
    return tuple(result.values())


def _dedupe_issues(
    issues: list[WorkspaceDependencyIssue],
) -> list[WorkspaceDependencyIssue]:
    seen: set[tuple[str, str, int, str]] = set()
    result: list[WorkspaceDependencyIssue] = []
    for issue in issues:
        key = (issue.code, issue.document_id, issue.block_index, issue.artifact)
        if key in seen:
            continue
        seen.add(key)
        result.append(issue)
    return result


def _dedupe_edges(
    edges: list[WorkspaceDependencyEdge],
) -> list[WorkspaceDependencyEdge]:
    seen: set[tuple[str, str, int, str, int]] = set()
    result: list[WorkspaceDependencyEdge] = []
    for edge in edges:
        key = (
            edge.artifact,
            edge.producer_document_id,
            edge.producer_block_index,
            edge.consumer_document_id,
            edge.consumer_block_index,
        )
        if key in seen:
            continue
        seen.add(key)
        result.append(edge)
    return result


__all__ = [
    "ProjectedDocument",
    "WorkspaceDependencyEdge",
    "WorkspaceDependencyIssue",
    "WorkspaceProjection",
    "project_analysis",
    "project_workspace",
]
