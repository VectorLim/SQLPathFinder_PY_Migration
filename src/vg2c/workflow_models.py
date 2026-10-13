"""Data contracts for effective workflow projection and workspace linking."""

from __future__ import annotations

from dataclasses import dataclass
from pathlib import Path
from typing import Literal

from vg2c.dataflow.file_effects import FileEffect
from vg2c.semantic_models import OperationReference, Symbol, WorkflowOperation

@dataclass(frozen=True, slots=True)
class FileResource:
    id: str
    path: str | None
    status: Literal["workspace", "generated", "external", "missing", "dynamic", "possible"]
    producer_refs: tuple[OperationReference, ...] = ()
    consumer_refs: tuple[OperationReference, ...] = ()
    lifecycle_refs: tuple[OperationReference, ...] = ()


@dataclass(frozen=True, slots=True)
class DocumentDiagnostic:
    level: str
    code: str
    message: str
    location: str | None = None
    node_id: str | None = None


@dataclass(frozen=True, slots=True)
class EffectiveDocument:
    """Authoritative effective semantic document derived from one compilation result."""

    source: str
    effects: tuple[FileEffect, ...]
    operations: tuple[WorkflowOperation, ...] = ()
    symbols: tuple[Symbol, ...] = ()
    files: tuple[FileResource, ...] = ()
    diagnostics: tuple[DocumentDiagnostic, ...] = ()


@dataclass(frozen=True, slots=True)
class WorkflowDocument:
    document_id: str
    output_path: Path
    workflow: EffectiveDocument


@dataclass(frozen=True, slots=True)
class WorkflowLink:
    artifact: str
    producer_document_id: str
    producer_operation_id: str
    producer_step_id: str
    consumer_document_id: str
    consumer_operation_id: str
    consumer_step_id: str
    consumer_endpoint_id: str


@dataclass(frozen=True, slots=True)
class WorkflowIssue:
    code: Literal["BROKEN_DEPENDENCY", "DUPLICATE_OUTPUT"]
    document_id: str
    step_id: str
    artifact: str
    message: str
    related_document_id: str | None = None
    related_step_id: str | None = None
