from __future__ import annotations

import difflib
import hashlib
from collections.abc import Iterable
from dataclasses import dataclass
from pathlib import Path

from vg2c import CompilationResult, compile_document
from vg2c.dataflow.projection import project_workspace
from vg2c.editing import (
    ParameterChange,
    ValidationIssue,
    project_changes,
)
from vg2c.editing import (
    apply_changes as apply_parameter_changes,
)
from vg2c.sql_editor import SqlAction, apply_sql_action, structured_sql_model
from vg2c_ui.api.models import (
    ChangeBatch,
    ChangePreviewView,
    ChangeResultView,
    CsvPreviewView,
    DependencyIssueView,
    DependencyLinkView,
    DocumentView,
    ParameterChangeRequest,
    ProjectedDocumentView,
    SqlActionRequest,
    SqlActionResponse,
    SqlModelRequest,
    SqlModelView,
    ValidationIssueView,
    WorkspaceProjectionRequest,
    WorkspaceProjectionView,
)
from vg2c_ui.api.serialization import artifact_views_for_analysis, document_view, sql_model_view
from vg2c_ui.services.atomic_io import atomic_write_text
from vg2c_ui.services.csv_preview import read_csv_preview
from vg2c_ui.services.sidecar import (
    EditorSidecar,
    InvalidSidecar,
    SavedParameterChange,
    read_sidecar,
    sidecar_path,
    write_sidecar,
)


class PathOutsideWorkspace(ValueError):
    pass


class RevisionConflict(RuntimeError):
    pass


@dataclass(frozen=True, slots=True)
class OpenedDocument:
    result: CompilationResult
    view: DocumentView


class DocumentStore:
    """Workspace-safe persistence boundary around compiler-owned semantics."""

    def __init__(self, workspace: Path):
        self.workspace = Path(workspace).resolve()

    def open_document(
        self, source_path: str, output_path: str | None = None
    ) -> OpenedDocument:
        source = self._resolve(source_path)
        output = self._resolve(output_path) if output_path else source.with_suffix(".py")
        result = compile_document(source)
        persisted = self._read_effective_changes(source, output)
        projected = project_changes(result, persisted)
        generated = projected.source if projected.valid else result.emitted.source
        synchronized = output.exists() and _hash_text(
            output.read_text(encoding="utf-8")
        ) == _hash_text(generated)
        read_only_reason = None
        if not synchronized:
            read_only_reason = (
                "Generated output cannot be reconciled with compiler metadata; "
                "retranslate before editing."
            )
        view = document_view(
            result,
            output_path=output,
            revision=self._revision(source, output),
            source_hash=_hash_file(source),
            output_hash=_hash_file(output) if output.exists() else "",
            saved_changes=persisted,
            synchronized=synchronized,
            read_only_reason=read_only_reason,
        )
        return OpenedDocument(result=result, view=view)

    def translate(
        self, source_path: str, output_path: str | None = None
    ) -> OpenedDocument:
        source = self._resolve(source_path)
        output = self._resolve(output_path) if output_path else source.with_suffix(".py")
        result = compile_document(source)
        atomic_write_text(output, result.emitted.source)
        self._clear_sidecar(output)
        return self.open_document(str(source), str(output))

    def preview(self, batch: ChangeBatch) -> ChangePreviewView:
        source, output, result, persisted = self._load_for_change(batch)
        merged = _merge_changes(persisted, _changes(batch.changes))
        projected = project_changes(result, merged)
        previous = (
            output.read_text(encoding="utf-8")
            if output.exists()
            else result.emitted.source
        )
        return ChangePreviewView(
            valid=projected.valid,
            diff="".join(
                difflib.unified_diff(
                    previous.splitlines(keepends=True),
                    projected.source.splitlines(keepends=True),
                    fromfile=str(output),
                    tofile=str(output),
                )
            ),
            issues=tuple(_issue_view(issue) for issue in projected.issues),
        )

    def apply(self, batch: ChangeBatch) -> ChangeResultView:
        source, output, result, persisted = self._load_for_change(batch)
        merged = _merge_changes(persisted, _changes(batch.changes))
        projected = apply_parameter_changes(result, merged)

        atomic_write_text(output, projected.source)
        write_sidecar(
            output,
            EditorSidecar(
                source_hash=_hash_file(source),
                output_hash=_hash_file(output),
                changes=[
                    SavedParameterChange(
                        parameter_id=item.parameter_id,
                        value=item.value,
                    )
                    for item in merged
                ],
            ),
        )
        opened = self.open_document(str(source), str(output))
        return ChangeResultView(document=opened.view)

    def project_workspace(
        self, request: WorkspaceProjectionRequest
    ) -> WorkspaceProjectionView:
        documents: list[tuple[str, CompilationResult, tuple[ParameterChange, ...]]] = []
        results_by_id: dict[str, CompilationResult] = {}
        for item in request.documents:
            source = self._resolve(item.source_path)
            output = self._resolve(item.output_path)
            result = compile_document(source)
            persisted = self._read_effective_changes(source, output)
            merged = _merge_changes(persisted, _changes(item.changes))
            documents.append((item.document_id, result, tuple(merged)))
            results_by_id[item.document_id] = result

        projection = project_workspace(documents)
        projected_documents = tuple(
            ProjectedDocumentView(
                document_id=item.document_id,
                artifacts=artifact_views_for_analysis(item.analyzed),
            )
            for item in projection.documents
        )
        links = tuple(
            DependencyLinkView(
                artifact=item.artifact,
                producer_document_id=item.producer_document_id,
                producer_step_id=_step_id(
                    results_by_id[item.producer_document_id],
                    item.producer_block_index,
                ),
                consumer_document_id=item.consumer_document_id,
                consumer_step_id=_step_id(
                    results_by_id[item.consumer_document_id],
                    item.consumer_block_index,
                ),
            )
            for item in projection.dependencies
        )
        issues = tuple(
            DependencyIssueView(
                code=item.code,
                document_id=item.document_id,
                step_id=_step_id(
                    results_by_id[item.document_id],
                    item.block_index,
                ),
                artifact=item.artifact,
                message=item.message,
                related_document_id=item.related_document_id,
                related_step_id=(
                    _step_id(
                        results_by_id[item.related_document_id],
                        item.related_block_index,
                    )
                    if item.related_document_id is not None
                    and item.related_block_index is not None
                    else None
                ),
            )
            for item in projection.issues
        )
        return WorkspaceProjectionView(
            documents=projected_documents,
            dependencies=links,
            issues=issues,
        )

    def inspect_sql(self, request: SqlModelRequest) -> SqlModelView:
        source = self._resolve(request.source_path)
        output = self._resolve(request.output_path)
        result = compile_document(source)
        persisted = self._read_effective_changes(source, output)
        merged = _merge_changes(persisted, _changes(request.changes))
        return sql_model_view(structured_sql_model(result, request.parameter_id, merged))

    def apply_sql_action(self, request: SqlActionRequest) -> SqlActionResponse:
        source = self._resolve(request.source_path)
        output = self._resolve(request.output_path)
        result = compile_document(source)
        persisted = self._read_effective_changes(source, output)
        merged = _merge_changes(persisted, _changes(request.changes))
        change = apply_sql_action(
            result,
            SqlAction(
                parameter_id=request.parameter_id,
                action=request.action,
                arguments=request.arguments,
            ),
            merged,
        )
        next_changes = _merge_changes(merged, [change])
        return SqlActionResponse(
            change=ParameterChangeRequest(
                parameter_id=change.parameter_id,
                value=change.value,
            ),
            model=sql_model_view(
                structured_sql_model(result, request.parameter_id, next_changes)
            ),
        )

    def preview_csv(self, source_path: str, csv_path: str) -> CsvPreviewView:
        source = self._resolve(source_path)
        csv_file = self._resolve_relative_to_source(source, csv_path)
        return read_csv_preview(csv_file)

    def _load_for_change(
        self, batch: ChangeBatch
    ) -> tuple[Path, Path, CompilationResult, list[ParameterChange]]:
        source = self._resolve(batch.source_path)
        output = self._resolve(batch.output_path)
        if batch.source_hash != _hash_file(source):
            raise RevisionConflict("Source file changed since the document was opened.")
        current_output_hash = _hash_file(output) if output.exists() else ""
        if batch.output_hash != current_output_hash:
            raise RevisionConflict(
                "Generated file changed since the document was opened."
            )
        if batch.revision != self._revision(source, output):
            raise RevisionConflict(
                "Document revision changed since the document was opened."
            )
        result = compile_document(source)
        return source, output, result, self._read_effective_changes(source, output)

    def _read_effective_changes(
        self, source: Path, output: Path
    ) -> list[ParameterChange]:
        try:
            sidecar = read_sidecar(output)
        except InvalidSidecar:
            return []
        if sidecar is None:
            return []
        if sidecar.source_hash != _hash_file(source):
            return []
        if not output.exists() or sidecar.output_hash != _hash_file(output):
            return []
        return [
            ParameterChange(
                parameter_id=item.parameter_id,
                value=item.value,
            )
            for item in sidecar.changes
        ]

    def _resolve(self, value: str | None) -> Path:
        if value is None:
            raise ValueError("A path is required.")
        candidate = Path(value)
        if not candidate.is_absolute():
            candidate = self.workspace / candidate
        resolved = candidate.resolve()
        if resolved != self.workspace and self.workspace not in resolved.parents:
            raise PathOutsideWorkspace(f"Path is outside workspace: {value}")
        return resolved

    def _resolve_relative_to_source(self, source: Path, value: str) -> Path:
        candidate = Path(value)
        if not candidate.is_absolute():
            candidate = source.parent / candidate
        return self._resolve(str(candidate))

    def _revision(self, source: Path, output: Path) -> int:
        digest = hashlib.sha256()
        digest.update(_hash_file(source).encode())
        digest.update((_hash_file(output) if output.exists() else "").encode())
        persisted = sidecar_path(output)
        digest.update((_hash_file(persisted) if persisted.exists() else "").encode())
        return int.from_bytes(digest.digest()[:8], "big", signed=False)

    def _clear_sidecar(self, output: Path) -> None:
        sidecar_path(output).unlink(missing_ok=True)


def _changes(items: Iterable[ParameterChangeRequest]) -> list[ParameterChange]:
    return [
        ParameterChange(parameter_id=item.parameter_id, value=item.value)
        for item in items
    ]


def _merge_changes(
    base: Iterable[ParameterChange],
    overrides: Iterable[ParameterChange],
) -> list[ParameterChange]:
    result: dict[str, ParameterChange] = {item.parameter_id: item for item in base}
    for item in overrides:
        result[item.parameter_id] = item
    return list(result.values())


def _issue_view(issue: ValidationIssue) -> ValidationIssueView:
    return ValidationIssueView(
        level=issue.level,
        code=issue.code,
        message=issue.message,
        parameter_id=issue.parameter_id,
    )


def _hash_text(value: str) -> str:
    return hashlib.sha256(value.encode("utf-8")).hexdigest()


def _hash_file(path: Path) -> str:
    digest = hashlib.sha256()
    with path.open("rb") as handle:
        for chunk in iter(lambda: handle.read(1024 * 1024), b""):
            digest.update(chunk)
    return digest.hexdigest()


def _step_id(result: CompilationResult, block_index: int) -> str:
    step = result.emitted.step_for_block(block_index)
    return step.function_name if step is not None else f"block-{block_index}"


__all__ = [
    "DocumentStore",
    "OpenedDocument",
    "PathOutsideWorkspace",
    "RevisionConflict",
]
