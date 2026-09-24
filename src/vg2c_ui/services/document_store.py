from __future__ import annotations

import csv
import difflib
import hashlib
from collections.abc import Callable, Iterable
from dataclasses import dataclass
from functools import wraps
from pathlib import Path
from threading import RLock

from vg2c import CompilationResult, compile_document
from vg2c.dataflow.file_effects import FileEffect
from vg2c.editing import (
    ChangeProjection,
    ChangeValidationError,
    SemanticChange,
    ValidationIssue,
    apply_changes,
    project_changes,
)
from vg2c.reorder import OrderChange, apply_order_changes, swap_adjacent
from vg2c.sql_editor import SqlAction, apply_sql_action, structured_sql_model
from vg2c.workflow import (
    WorkflowDocument,
    project_document,
    workspace_issues,
    workspace_links,
)
from vg2c_ui.api.models import (
    ArtifactView,
    ChangeBatch,
    ChangePreviewView,
    ChangeResultView,
    DependencyIssueView,
    DependencyLinkView,
    DiagnosticView,
    DocumentSnapshot,
    DocumentView,
    HtmlPreviewRequest,
    HtmlPreviewView,
    ProjectedDocumentView,
    ReorderRequest,
    SemanticChangeRequest,
    SqlActionRequest,
    SqlActionResponse,
    SqlModelRequest,
    SqlModelView,
    ValidationIssueView,
    WorkspaceProjectionRequest,
    WorkspaceProjectionView,
)
from vg2c_ui.api.serialization import (
    compiler_manifest_hash,
    document_view,
    effect_views,
    sql_model_view,
)
from vg2c_ui.services.atomic_io import atomic_write_text
from vg2c_ui.services.file_choices import file_choices_by_operation
from vg2c_ui.services.html_preview import preview_html_report
from vg2c_ui.services.sidecar import (
    EditorSidecar,
    InvalidSidecar,
    SavedOrderChange,
    SavedSemanticChange,
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


def get_document_store(request) -> DocumentStore:
    """Return the caller's server workspace store."""
    state = getattr(request, "state", None)
    store = getattr(state, "document_store", None)
    if store is None:
        raise RuntimeError("Workspace session is unavailable.")
    return store


@dataclass(frozen=True, slots=True)
class OpenedDocument:
    result: CompilationResult
    view: DocumentView


class DocumentStore:
    """Workspace-safe persistence boundary around compiler-owned semantics."""

    def __init__(
        self,
        workspace: Path,
        *,
        expose_relative_paths: bool = False,
        inventory_paths: Callable[[], Iterable[str]] | None = None,
    ):
        self.workspace = Path(workspace).resolve()
        self.expose_relative_paths = expose_relative_paths
        self.inventory_paths = inventory_paths or (lambda: ())

    @_serialized
    def open_document(
        self, source_path: str, output_path: str | None = None
    ) -> OpenedDocument:
        source = self._resolve(source_path)
        output = (
            self._resolve(output_path) if output_path else source.with_suffix(".py")
        )
        result = compile_document(source)
        base_compiler_hash = compiler_manifest_hash(result)
        recovery_reason = None
        generation_state = "missing"
        try:
            sidecar = read_sidecar(output)
            persisted = self._read_effective_changes(source, output, sidecar)
            result = apply_order_changes(result, _saved_orders(sidecar), persisted)
            try:
                effective = project_document(result, persisted, output_path=output)
            except ChangeValidationError as exc:
                raise InvalidSidecar(
                    "Saved edits no longer match the compiler manifest. "
                    "Original files are preserved."
                ) from exc
            generation_state = self._generation_state(
                output, effective.source, sidecar
            )
        except (InvalidSidecar, ValueError) as exc:
            persisted = []
            recovery_reason = str(exc)
            result = compile_document(source)
            effective = project_document(result, persisted, output_path=output)
        read_only_reason = None
        if recovery_reason:
            read_only_reason = (
                recovery_reason
                or "Generated output cannot be reconciled with compiler metadata; "
                "retranslate before editing."
            )
        file_choices = file_choices_by_operation(
            effective.effects,
            self.inventory_paths(),
            output_path=output,
            workspace_root=self.workspace,
        )
        view = document_view(
            result,
            effective,
            output_path=output,
            revision=self._revision(source, output),
            source_hash=_hash_file(source),
            output_hash=_hash_file(output) if output.exists() else "",
            file_choices=file_choices,
            read_only_reason=read_only_reason,
            generation_state=generation_state,
            compiler_hash=base_compiler_hash,
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
        display_path = (
            self._relative_display(str(output))
            if self.expose_relative_paths
            else str(output)
        )
        return ChangePreviewView(
            valid=projected.valid,
            diff="".join(
                difflib.unified_diff(
                    previous.splitlines(keepends=True),
                    projected.source.splitlines(keepends=True),
                    fromfile=display_path,
                    tofile=display_path,
                )
            ),
            issues=tuple(_issue_view(issue) for issue in projected.issues),
        )

    @_serialized
    def preview_html(self, request: HtmlPreviewRequest) -> HtmlPreviewView:
        source, output, result, persisted = self._load_for_change(request)
        merged = _merge_changes(persisted, _changes(request.changes))
        try:
            effective = project_document(result, merged, output_path=output)
        except ChangeValidationError:
            return HtmlPreviewView(
                state="error",
                html="",
                message="HTML preview is unavailable until configuration errors are resolved.",
            )

        preview = preview_html_report(
            effective,
            target_operation_id=request.operation_id,
            resolve_path=self._resolve,
        )
        return HtmlPreviewView(
            state=preview.state,
            html=preview.html,
            output_path=(
                self._relative_display(str(preview.output_path))
                if preview.output_path is not None
                else None
            ),
            message=preview.message,
        )

    @_serialized
    def save(self, batch: ChangeBatch) -> ChangeResultView:
        source, output, result, persisted = self._load_for_change(batch)
        merged = _merge_changes(persisted, _changes(batch.changes))
        sidecar = read_sidecar(output)
        orders = _saved_orders(sidecar)
        if orders:
            result = apply_order_changes(compile_document(source), orders, merged)
        projected = apply_changes(result, merged)
        if batch.revision != self._revision(source, output):
            raise RevisionConflict("Document changed while validating edits.")
        provenance = (
            sidecar.last_generated_hash
            if sidecar is not None
            else _hash_file(output) if output.exists() else None
        )
        write_sidecar(output, _saved_sidecar(source, projected.values, provenance, orders))
        return ChangeResultView(document=self.open_document(str(source), str(output)).view)

    @_serialized
    def generate(self, snapshot: DocumentSnapshot) -> ChangeResultView:
        source, output, result, persisted = self._load_for_change(snapshot)
        projected = apply_changes(result, persisted)
        if snapshot.revision != self._revision(source, output):
            raise RevisionConflict("Document changed while generating output.")
        self._write_generated_state(source, output, projected, _saved_orders(read_sidecar(output)))
        return ChangeResultView(document=self.open_document(str(source), str(output)).view)

    @_serialized
    def reorder(self, request: ReorderRequest) -> ChangeResultView:
        source, output, result, persisted = self._load_for_change(request)
        next_order = swap_adjacent(
            result, request.source_scope_id, request.target_scope_id, persisted
        )
        sidecar = read_sidecar(output)
        orders = [
            item for item in _saved_orders(sidecar)
            if item.parent_scope_id != next_order.parent_scope_id
        ] + [next_order]
        candidate = apply_order_changes(compile_document(source), orders, persisted)
        apply_changes(candidate, persisted)
        if request.revision != self._revision(source, output):
            raise RevisionConflict("Document changed while reordering operations.")
        provenance = (
            sidecar.last_generated_hash if sidecar is not None
            else _hash_file(output) if output.exists() else None
        )
        write_sidecar(output, _saved_sidecar(source, persisted, provenance, orders))
        return ChangeResultView(document=self.open_document(str(source), str(output)).view)

    def _write_generated_state(
        self, source: Path, output: Path, projected: ChangeProjection,
        orders: Iterable[OrderChange] = (),
    ) -> None:
        previous = output.read_text(encoding="utf-8") if output.exists() else None
        try:
            atomic_write_text(output, projected.source)
            write_sidecar(
                output,
                _saved_sidecar(source, projected.values, _hash_file(output), orders),
            )
        except OSError:
            if output.exists() and _hash_text(
                output.read_text(encoding="utf-8")
            ) == _hash_text(projected.source):
                if previous is None:
                    output.unlink()
                else:
                    atomic_write_text(output, previous)
            raise

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
                    project_document(result, merged, output_path=output),
                )
            )
            baseline.append(
                WorkflowDocument(
                    item.document_id,
                    output,
                    project_document(result, persisted, output_path=output),
                )
            )

        inventory_paths = self.inventory_paths()
        projected_documents = tuple(
            ProjectedDocumentView(
                document_id=item.document_id,
                artifacts=_workspace_artifact_views(item.workflow.effects),
                effects=effect_views(item.workflow.effects),
                file_choices=file_choices_by_operation(
                    item.workflow.effects,
                    inventory_paths,
                    output_path=item.output_path,
                    workspace_root=self.workspace,
                ),
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
        file_choices, csv_header = self._sql_context(result, merged, request.binding_id, output)
        return sql_model_view(
            structured_sql_model(
                result,
                request.binding_id,
                merged,
                csv_header=csv_header,
                file_choices=file_choices,
            )
        )

    @_serialized
    def apply_sql_action(self, request: SqlActionRequest) -> SqlActionResponse:
        source, output, result, persisted = self._load_for_change(request)
        merged = _merge_changes(persisted, _changes(request.changes))
        file_choices, csv_header = self._sql_context(result, merged, request.binding_id, output)
        change = apply_sql_action(
            result,
            SqlAction(
                binding_id=request.binding_id,
                action=request.action,
                arguments=request.arguments,
            ),
            merged,
            csv_header=csv_header,
            file_choices=file_choices,
        )
        next_changes = _merge_changes(merged, [change])
        next_file_choices, next_csv_header = self._sql_context(
            result, next_changes, request.binding_id, output
        )
        return SqlActionResponse(
            change=SemanticChangeRequest(
                binding_id=change.binding_id,
                value=change.value,
            ),
            model=sql_model_view(
                structured_sql_model(
                    result,
                    request.binding_id,
                    next_changes,
                    csv_header=next_csv_header,
                    file_choices=next_file_choices,
                )
            ),
        )

    def _sql_context(
        self, result: CompilationResult, changes: list[SemanticChange],
        binding_id: str, output: Path,
    ):
        workflow = project_document(result, changes, output_path=output)
        binding = next(
            (
                binding
                for operation in workflow.operations
                for binding in operation.bindings
                if binding.id == binding_id
            ),
            None,
        )
        if binding is None:
            return (), lambda path: None
        choices = file_choices_by_operation(
            workflow.effects,
            self.inventory_paths(),
            output_path=output,
            workspace_root=self.workspace,
        )
        file_choices = tuple(choices.get(binding.owner_operation_id, ()))
        allowed = set(file_choices)

        def read_header(path: str) -> tuple[str, ...] | None:
            try:
                candidate = self._resolve(path)
            except PathOutsideWorkspace:
                return None
            if (
                candidate.relative_to(self.workspace).as_posix() not in allowed
                or not candidate.is_file()
            ):
                return None
            with candidate.open(
                newline="", encoding="utf-8-sig", errors="replace"
            ) as handle:
                row = next(csv.reader(handle), None)
            return tuple(row) if row else None

        return file_choices, read_header

    def _load_for_change(
        self, batch: DocumentSnapshot
    ) -> tuple[Path, Path, CompilationResult, list[SemanticChange]]:
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
        sidecar = read_sidecar(output)
        persisted = self._read_effective_changes(source, output, sidecar)
        result = apply_order_changes(result, _saved_orders(sidecar), persisted)
        projected = project_changes(result, persisted)
        if not projected.valid:
            raise RevisionConflict(
                "Saved edits no longer match the compiler manifest."
            )
        try:
            self._generation_state(output, projected.source, sidecar)
        except InvalidSidecar as exc:
            raise RevisionConflict(str(exc)) from exc
        return source, output, result, persisted

    def _read_effective_changes(
        self, source: Path, output: Path, sidecar: EditorSidecar | None
    ) -> list[SemanticChange]:
        if sidecar is None:
            return []
        if sidecar.source_hash != _hash_file(source):
            raise InvalidSidecar(
                "Saved changes belong to a different source revision. Original files are preserved."
            )
        if sidecar.legacy_version is not None and (
            not output.exists() or sidecar.last_generated_hash != _hash_file(output)
        ):
            raise InvalidSidecar(
                "Saved changes and generated output do not match. Original files are preserved."
            )
        return [
            SemanticChange(
                binding_id=item.binding_id,
                value=item.value,
                symbol_id=item.symbol_id,
            )
            for item in sidecar.value_changes
        ]

    @staticmethod
    def _generation_state(
        output: Path, projected_source: str, sidecar: EditorSidecar | None
    ) -> str:
        if not output.exists():
            return "missing"
        actual = _hash_file(output)
        if actual == _hash_text(projected_source):
            return "current"
        if sidecar is not None and actual == sidecar.last_generated_hash:
            return "stale"
        raise InvalidSidecar(
            "Generated output was modified outside the editor. Original files are preserved."
        )

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
        semantic_operations = [
            operation.model_copy(
                update={
                    "source_span": operation.source_span.model_copy(
                        update={
                            "file": (
                                self._relative_display(operation.source_span.file)
                                if operation.source_span.file
                                else None
                            )
                        }
                    )
                }
            )
            for operation in view.semantic_operations
        ]
        return view.model_copy(
            update={
                "id": self._relative_display(view.source_path),
                "source_path": self._relative_display(view.source_path),
                "output_path": self._relative_display(view.output_path),
                "semantic_operations": semantic_operations,
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


def _changes(
    items: Iterable[SemanticChangeRequest],
) -> list[SemanticChange]:
    return [
        SemanticChange(
            binding_id=item.binding_id,
            value=item.value,
            symbol_id=item.symbol_id,
            reset=item.reset,
        )
        for item in items
    ]


def _merge_changes(
    base: Iterable[SemanticChange],
    overrides: Iterable[SemanticChange],
) -> list[SemanticChange]:
    requested = list(overrides)
    overridden = {item.binding_id for item in requested}
    # Preserve duplicate requests so core validation can reject conflicting shared edits.
    return [item for item in base if item.binding_id not in overridden] + requested


def _saved_sidecar(
    source: Path, changes: Iterable[SemanticChange], provenance: str | None,
    orders: Iterable[OrderChange] = (),
) -> EditorSidecar:
    return EditorSidecar(
        source_hash=_hash_file(source),
        last_generated_hash=provenance,
        value_changes=[
            SavedSemanticChange(
                binding_id=item.binding_id,
                value=item.value,
                symbol_id=item.symbol_id,
            )
            for item in changes
        ],
        order_changes=[
            SavedOrderChange(
                parent_scope_id=item.parent_scope_id,
                child_scope_ids=list(item.child_scope_ids),
            )
            for item in orders
        ],
    )


def _saved_orders(sidecar: EditorSidecar | None) -> tuple[OrderChange, ...]:
    return tuple(
        OrderChange(item.parent_scope_id, tuple(item.child_scope_ids))
        for item in sidecar.order_changes
    ) if sidecar is not None else ()


def _workspace_artifact_views(effects: Iterable[FileEffect]) -> list[ArtifactView]:
    """Build workspace-only artifact summaries outside compiler and transport layers."""
    artifacts: dict[str, ArtifactView] = {}
    for effect in effects:
        for endpoint in (*effect.inputs, *effect.outputs):
            if not endpoint.path:
                continue
            artifact = artifacts.setdefault(
                endpoint.path,
                ArtifactView(
                    id=endpoint.path,
                    path=endpoint.path,
                    label=Path(endpoint.path).name,
                ),
            )
            references = (
                artifact.producer_step_ids
                if endpoint.phase == "next"
                else artifact.consumer_step_ids
            )
            if effect.step_id not in references:
                references.append(effect.step_id)
            artifact.conditional |= effect.conditional
            artifact.in_loop |= effect.in_loop
            artifact.order_valid &= endpoint.status != "missing"

    for artifact in artifacts.values():
        artifact.is_output = bool(artifact.producer_step_ids)
        artifact.is_external_input = (
            bool(artifact.consumer_step_ids) and not artifact.is_output
        )
    return sorted(artifacts.values(), key=lambda item: item.path)


def _issue_view(issue: ValidationIssue) -> ValidationIssueView:
    return ValidationIssueView(
        level=issue.level,
        code=issue.code,
        message=issue.message,
        binding_id=issue.binding_id,
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
