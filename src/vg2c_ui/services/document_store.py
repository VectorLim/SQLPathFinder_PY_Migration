from __future__ import annotations

import difflib
import hashlib
from collections.abc import Iterable
from dataclasses import dataclass
from functools import wraps
from pathlib import Path
from threading import RLock

from vg2c import CompilationResult, compile_document
from vg2c.editing import (
    ParameterChange,
    ValidationIssue,
    project_changes,
)
from vg2c.editing import (
    apply_changes as apply_parameter_changes,
)
from vg2c.sql_editor import SqlAction, apply_sql_action, structured_sql_model
from vg2c.workflow import (
    WorkflowDocument,
    project_workflow,
    workspace_issues,
    workspace_links,
)
from vg2c_ui.api.models import (
    ChangeBatch,
    ChangePreviewView,
    ChangeResultView,
    CsvPreviewView,
    CsvPreviewRequest,
    DependencyIssueView,
    DependencyLinkView,
    DocumentView,
    DocumentSnapshot,
    DiagnosticView,
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
from vg2c_ui.api.serialization import (
    artifact_views_for_effects,
    compiler_manifest_hash,
    document_view,
    effect_views,
    sql_model_view,
)
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


_STORE_LOCK = RLock()


def _serialized(method):
    @wraps(method)
    def guarded(*args, **kwargs):
        with _STORE_LOCK:
            return method(*args, **kwargs)

    return guarded


def get_document_store(request) -> "DocumentStore":
    """Return the caller-scoped store, retaining the legacy app store for tests."""
    state = getattr(request, "state", None)
    return getattr(state, "document_store", request.app.state.document_store)


@dataclass(frozen=True, slots=True)
class OpenedDocument:
    result: CompilationResult
    view: DocumentView


class DocumentStore:
    """Workspace-safe persistence boundary around compiler-owned semantics."""

    def __init__(self, workspace: Path, *, expose_relative_paths: bool = False):
        self.workspace = Path(workspace).resolve()
        self.expose_relative_paths = expose_relative_paths

    @_serialized
    def open_document(
        self, source_path: str, output_path: str | None = None
    ) -> OpenedDocument:
        source = self._resolve(source_path)
        output = (
            self._resolve(output_path) if output_path else source.with_suffix(".py")
        )
        result = compile_document(source)
        recovery_reason = None
        try:
            persisted = self._read_effective_changes(source, output)
        except InvalidSidecar as exc:
            persisted = []
            recovery_reason = str(exc)
        projected = project_changes(result, persisted)
        if not projected.valid:
            persisted = []
            recovery_reason = "Saved edits no longer match the compiler manifest. Original files are preserved."
        generated = projected.source if projected.valid else result.emitted.source
        synchronized = (
            recovery_reason is None
            and output.exists()
            and _hash_text(output.read_text(encoding="utf-8")) == _hash_text(generated)
        )
        read_only_reason = None
        if not synchronized:
            read_only_reason = (
                recovery_reason
                or "Generated output cannot be reconciled with compiler metadata; "
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
        if recovery_reason:
            view.diagnostics.append(
                DiagnosticView(
                    level="error",
                    code="saved-changes-unreconciled",
                    message=recovery_reason,
                )
            )
        return OpenedDocument(result=result, view=self._present_view(view))

    @_serialized
    def translate(
        self, source_path: str, output_path: str | None = None
    ) -> OpenedDocument:
        source = self._resolve(source_path)
        output = (
            self._resolve(output_path) if output_path else source.with_suffix(".py")
        )
        result = compile_document(source)
        atomic_write_text(output, result.emitted.source)
        self._clear_sidecar(output)
        return self.open_document(source_path, output_path)

    @_serialized
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

    @_serialized
    def apply(self, batch: ChangeBatch) -> ChangeResultView:
        source, output, result, persisted = self._load_for_change(batch)
        merged = _merge_changes(persisted, _changes(batch.changes))
        projected = apply_parameter_changes(result, merged)

        previous = output.read_text(encoding="utf-8")
        if batch.revision != self._revision(source, output):
            raise RevisionConflict("Document changed while validating edits.")
        try:
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
                        for item in projected.values
                    ],
                ),
            )
        except OSError:
            if output.exists() and _hash_text(
                output.read_text(encoding="utf-8")
            ) == _hash_text(projected.source):
                atomic_write_text(output, previous)
            raise
        opened = self.open_document(str(source), str(output))
        return ChangeResultView(document=opened.view)

    @_serialized
    def project_workspace(
        self, request: WorkspaceProjectionRequest
    ) -> WorkspaceProjectionView:
        workflows: list[WorkflowDocument] = []
        baseline: list[WorkflowDocument] = []
        for item in request.documents:
            source, output, result, persisted = self._load_for_change(item)
            merged = _merge_changes(persisted, _changes(item.changes))
            workflows.append(
                WorkflowDocument(
                    item.document_id,
                    output,
                    project_workflow(result, merged, output_path=output),
                )
            )
            baseline.append(
                WorkflowDocument(
                    item.document_id,
                    output,
                    project_workflow(result, persisted, output_path=output),
                )
            )

        projected_documents = tuple(
            ProjectedDocumentView(
                document_id=item.document_id,
                artifacts=artifact_views_for_effects(item.workflow.effects),
                effects=effect_views(item.workflow.effects),
            )
            for item in workflows
        )
        links = tuple(
            DependencyLinkView(
                artifact=item.artifact,
                producer_document_id=item.producer_document_id,
                producer_step_id=item.producer_step_id,
                consumer_document_id=item.consumer_document_id,
                consumer_step_id=item.consumer_step_id,
                producer_operation_id=item.producer_operation_id,
                consumer_operation_id=item.consumer_operation_id,
            )
            for item in workspace_links(workflows)
        )
        issues = tuple(
            DependencyIssueView.model_validate(item, from_attributes=True)
            for item in workspace_issues(workflows, baseline)
        )
        return WorkspaceProjectionView(
            documents=projected_documents,
            dependencies=links,
            issues=issues,
        )

    @_serialized
    def inspect_sql(self, request: SqlModelRequest) -> SqlModelView:
        source, output, result, persisted = self._load_for_change(request)
        merged = _merge_changes(persisted, _changes(request.changes))
        return sql_model_view(
            structured_sql_model(result, request.parameter_id, merged)
        )

    @_serialized
    def apply_sql_action(self, request: SqlActionRequest) -> SqlActionResponse:
        source, output, result, persisted = self._load_for_change(request)
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

    @_serialized
    def preview_csv(self, request: CsvPreviewRequest) -> CsvPreviewView:
        source, output, result, persisted = self._load_for_change(request)
        workflow = project_workflow(
            result,
            _merge_changes(persisted, _changes(request.changes)),
            output_path=output,
        )
        endpoint = next(
            (
                endpoint
                for effect in workflow.effects
                if effect.id == request.effect_id
                for endpoint in (*effect.inputs, *effect.outputs)
                if endpoint.id == request.endpoint_id
            ),
            None,
        )
        if endpoint is None or endpoint.path != request.expected_path:
            raise RevisionConflict(
                "File endpoint changed; refresh the file flow before previewing."
            )
        if not endpoint.path:
            raise ValueError("Dynamic file paths cannot be previewed.")
        candidate = Path(endpoint.path)
        if not candidate.is_absolute():
            if endpoint.path_base != "script-directory":
                raise ValueError(
                    "Preview unavailable: runtime working directory is unknown."
                )
            candidate = output.parent / candidate
        preview = read_csv_preview(self._resolve(str(candidate)))
        if self.expose_relative_paths:
            preview.path = self._relative_display(preview.path)
        return preview

    def _load_for_change(
        self, batch: DocumentSnapshot
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
        if batch.compiler_hash != compiler_manifest_hash(result):
            raise RevisionConflict(
                "Compiler manifest changed since the document was opened."
            )
        persisted = self._read_effective_changes(source, output)
        projected = project_changes(result, persisted)
        if (
            not projected.valid
            or not output.exists()
            or _hash_text(output.read_text(encoding="utf-8"))
            != _hash_text(projected.source)
        ):
            raise RevisionConflict(
                "Generated output is not synchronized; retranslate before editing."
            )
        return source, output, result, persisted

    def _read_effective_changes(
        self, source: Path, output: Path
    ) -> list[ParameterChange]:
        sidecar = read_sidecar(output)
        if sidecar is None:
            return []
        if sidecar.source_hash != _hash_file(source):
            raise InvalidSidecar(
                "Saved changes belong to a different source revision. Original files are preserved."
            )
        if not output.exists() or sidecar.output_hash != _hash_file(output):
            raise InvalidSidecar(
                "Saved changes and generated output do not match. Original files are preserved."
            )
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

    def _present_view(self, view: DocumentView) -> DocumentView:
        """Remove host paths from API responses for browser workspaces."""
        if not self.expose_relative_paths:
            return view
        steps = [
            step.model_copy(
                update={
                    "source_span": step.source_span.model_copy(
                        update={
                            "file": (
                                self._relative_display(step.source_span.file)
                                if step.source_span.file
                                else None
                            )
                        }
                    )
                }
            )
            for step in view.steps
        ]
        return view.model_copy(
            update={
                "id": self._relative_display(view.source_path),
                "source_path": self._relative_display(view.source_path),
                "output_path": self._relative_display(view.output_path),
                "steps": steps,
            }
        )

    def _relative_display(self, value: str) -> str:
        path = Path(value).resolve()
        if path != self.workspace and self.workspace not in path.parents:
            raise PathOutsideWorkspace("Cannot expose a path outside the workspace.")
        return path.relative_to(self.workspace).as_posix()

    def _revision(self, source: Path, output: Path) -> str:
        digest = hashlib.sha256()
        digest.update(_hash_file(source).encode())
        digest.update((_hash_file(output) if output.exists() else "").encode())
        persisted = sidecar_path(output)
        digest.update((_hash_file(persisted) if persisted.exists() else "").encode())
        return digest.hexdigest()

    def _clear_sidecar(self, output: Path) -> None:
        sidecar_path(output).unlink(missing_ok=True)


def _changes(items: Iterable[ParameterChangeRequest]) -> list[ParameterChange]:
    return [
        ParameterChange(
            parameter_id=item.parameter_id, value=item.value, reset=item.reset
        )
        for item in items
    ]


def _merge_changes(
    base: Iterable[ParameterChange],
    overrides: Iterable[ParameterChange],
) -> list[ParameterChange]:
    requested = list(overrides)
    overridden = {item.parameter_id for item in requested}
    # Preserve duplicate requests so core validation can reject conflicting shared edits.
    return [item for item in base if item.parameter_id not in overridden] + requested


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


__all__ = [
    "DocumentStore",
    "OpenedDocument",
    "PathOutsideWorkspace",
    "RevisionConflict",
    "get_document_store",
]
