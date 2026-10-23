from __future__ import annotations

from collections.abc import Iterable
from dataclasses import replace
from pathlib import Path

from vg2c.compilation import CompilationResult
from vg2c.dataflow.file_effects import (
    FileEffect,
    FileEndpoint,
    order_file_effects,
)
from vg2c.dataflow.projection import project_file_effects
from vg2c.editing import (
    ChangeValidationError,
    SemanticChange,
    project_changes,
)
from vg2c.project_paths import project_main_path
from vg2c.reorder_analysis import reorder_targets, safe_leaf_outputs
from vg2c.semantic.models import (
    OperationReference,
)
from vg2c.semantic.projection import project_semantics
from vg2c.workflow_models import (
    DocumentDiagnostic,
    EffectiveDocument,
    FileResource,
    WorkflowDocument,
    WorkflowIssue,
    WorkflowLink,
)
from vg2c.workspace import workspace_issues, workspace_links


def project_document(
    result: CompilationResult,
    changes: Iterable[SemanticChange] = (),
    *,
    output_path: Path | None = None,
) -> EffectiveDocument:
    """Build the one effective semantic document for downstream consumers."""
    requested = tuple(changes)
    if requested:
        projection = project_changes(result, requested)
        if not projection.valid:
            raise ChangeValidationError(projection.issues)
        source = projection.source
        values = projection.effective_values
    else:
        source = result.emitted.source
        values = {}

    operations, _, symbols = project_semantics(result, values)
    effects = project_file_effects(result, operations, values)
    ordered = order_file_effects(
        effects,
        result.resolved.scope_tree,
        output_path or project_main_path(result.input_path),
    )
    output = output_path or project_main_path(result.input_path)
    safe_outputs = safe_leaf_outputs(result, operations, ordered, output)
    targets = reorder_targets(result, safe_outputs)
    operations = tuple(
        replace(
            operation,
            reorder_targets=targets.get(operation.scope_id, ()),
        )
        for operation in operations
    )
    return EffectiveDocument(
        source=source,
        operations=operations,
        symbols=symbols,
        effects=ordered,
        files=file_resources(ordered),
        diagnostics=_document_diagnostics(result, ordered),
    )


def _document_diagnostics(
    result: CompilationResult, effects: tuple[FileEffect, ...]
) -> tuple[DocumentDiagnostic, ...]:
    diagnostics = [
        DocumentDiagnostic(
            item.level,
            item.code,
            item.message,
            item.location,
        )
        for item in result.diagnostics
    ]
    for effect in effects:
        for endpoint in effect.inputs:
            if endpoint.status not in {"external", "missing"}:
                continue
            diagnostics.append(
                DocumentDiagnostic(
                    level="warning" if endpoint.status == "missing" else "info",
                    code=(
                        "file-state-missing"
                        if endpoint.status == "missing"
                        else "external-file-input"
                    ),
                    message=(
                        f"{endpoint.path}: unavailable after deletion/move."
                        if endpoint.status == "missing"
                        else f"{endpoint.path}: external input."
                    ),
                    node_id=effect.operation_id,
                )
            )
    return tuple(diagnostics)


def file_resources(effects: Iterable[FileEffect]) -> tuple[FileResource, ...]:
    grouped: dict[str, list[tuple[FileEffect, FileEndpoint]]] = {}
    for effect in effects:
        for endpoint in (*effect.inputs, *effect.outputs):
            if endpoint.file_resource_id is None:
                raise ValueError("File resource identity must be assigned by order_file_effects.")
            key = endpoint.file_resource_id
            grouped.setdefault(key, []).append((effect, endpoint))

    resources: list[FileResource] = []
    for resource_id, items in sorted(grouped.items()):
        known_path = next((endpoint.path for _, endpoint in items if endpoint.path), None)
        producers = tuple(
            dict.fromkeys(
                OperationReference(effect.operation_id, endpoint.binding_id)
                for effect, endpoint in items
                if endpoint.phase == "next"
            )
        )
        consumers = tuple(
            dict.fromkeys(
                OperationReference(effect.operation_id, endpoint.binding_id)
                for effect, endpoint in items
                if endpoint.phase != "next"
            )
        )
        lifecycle = tuple(
            dict.fromkeys(
                OperationReference(effect.operation_id, endpoint.binding_id)
                for effect, endpoint in items
                if effect.kind in {"copy", "move", "append", "delete"}
            )
        )
        statuses = {endpoint.status for _, endpoint in items}
        if known_path is None:
            status = "dynamic"
        elif "missing" in statuses:
            status = "missing"
        elif "possible" in statuses:
            status = "possible"
        elif producers:
            status = "generated"
        elif "external" in statuses:
            status = "external"
        else:
            status = "workspace"
        resources.append(
            FileResource(
                id=resource_id,
                path=known_path,
                status=status,
                producer_refs=producers,
                consumer_refs=consumers,
                lifecycle_refs=lifecycle,
            )
        )
    return tuple(resources)


__all__ = [
    "FileResource",
    "WorkflowDocument",
    "WorkflowIssue",
    "WorkflowLink",
    "DocumentDiagnostic",
    "EffectiveDocument",
    "file_resources",
    "project_document",
    "workspace_issues",
    "workspace_links",
]
