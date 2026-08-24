from __future__ import annotations

from vg2c_ui.domain.models import WorkflowDocument
from vg2c_ui.domain.semantic_models import (
    ClientWorkingState,
    EffectiveDocumentInput,
    SqlEntityRef,
    WorkflowEntityRef,
)


class WorkingStateConflict(ValueError):
    def __init__(self, code: str, message: str) -> None:
        super().__init__(message)
        self.code = code


def prepare_effective_document_input(
    document: WorkflowDocument,
    working_state: ClientWorkingState,
) -> EffectiveDocumentInput:
    """Validate a client's document snapshot, then return only its pending overlays."""

    snapshots = [item for item in working_state.open_documents if item.document_id == document.id]
    if not snapshots:
        raise WorkingStateConflict(
            "document-not-open",
            "Document is not present in the client working state.",
        )
    if len(snapshots) > 1:
        raise WorkingStateConflict(
            "duplicate-document-state",
            "Document appears more than once in the client working state.",
        )
    snapshot = snapshots[0]
    if snapshot.source_hash != document.source_hash:
        raise WorkingStateConflict(
            "stale-source-hash",
            "Source hash is stale; reload the document.",
        )
    if snapshot.output_hash != document.output_hash:
        raise WorkingStateConflict(
            "stale-output-hash",
            "Output hash is stale; reload the document.",
        )
    if snapshot.revision != document.revision:
        raise WorkingStateConflict(
            "stale-revision",
            "Document revision is stale; reload the document.",
        )

    steps = {step.id: step for step in document.steps}
    edits = [item for item in working_state.pending_edits if item.document_id == document.id]
    seen: set[tuple[str, str]] = set()
    for edit in edits:
        key = (edit.step_id, edit.parameter_id)
        if key in seen:
            raise WorkingStateConflict(
                "duplicate-pending-edit",
                "A parameter appears more than once in pending edits.",
            )
        seen.add(key)
        step = steps.get(edit.step_id)
        if step is None:
            raise WorkingStateConflict(
                "unknown-step",
                "Pending edit refers to a step that no longer exists.",
            )
        if not any(parameter.id == edit.parameter_id for parameter in step.parameters):
            raise WorkingStateConflict(
                "unknown-parameter",
                "Pending edit refers to a parameter that no longer exists.",
            )

    return EffectiveDocumentInput(base_document=document, pending_edits=edits)


def validate_entity_ref_version(
    ref: WorkflowEntityRef | SqlEntityRef,
    document: WorkflowDocument,
) -> None:
    """Reject entity references captured against a different document version."""

    if ref.document_id != document.id:
        raise WorkingStateConflict(
            "wrong-document",
            "Entity reference belongs to a different document.",
        )
    if ref.output_hash != document.output_hash:
        raise WorkingStateConflict(
            "stale-output-hash",
            "Entity reference output hash is stale.",
        )
    if ref.document_revision != document.revision:
        raise WorkingStateConflict(
            "stale-revision",
            "Entity reference revision is stale.",
        )
