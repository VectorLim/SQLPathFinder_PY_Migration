from __future__ import annotations

from fastapi import APIRouter, HTTPException, Request
from pydantic import BaseModel

from vg2c_ui.domain.models import (
    CommandBatch,
    CommandPreview,
    CommandResult,
    CsvPreview,
    StepNode,
    WorkflowDocument,
)
from vg2c_ui.services.command_service import CommandValidationError, DocumentConflict
from vg2c_ui.services.document_store import DocumentStore

router = APIRouter(prefix="/api/commands", tags=["commands"])


class DocumentReference(BaseModel):
    source_path: str
    output_path: str | None = None


class StepReference(DocumentReference):
    step_id: str


class CsvPreviewRequest(BaseModel):
    source_path: str
    csv_path: str


def _store(request: Request) -> DocumentStore:
    return request.app.state.document_store


@router.post("/get-workflow", response_model=WorkflowDocument)
def get_workflow(payload: DocumentReference, request: Request) -> WorkflowDocument:
    try:
        return _store(request).open_document(payload.source_path, payload.output_path)
    except (OSError, ValueError) as exc:
        raise HTTPException(status_code=400, detail=str(exc)) from exc


@router.post("/get-step", response_model=StepNode)
def get_step(payload: StepReference, request: Request) -> StepNode:
    document = get_workflow(payload, request)
    step = next((item for item in document.steps if item.id == payload.step_id), None)
    if step is None:
        raise HTTPException(status_code=404, detail="step not found")
    return step


@router.post("/validate-changes", response_model=CommandPreview)
@router.post("/preview-diff", response_model=CommandPreview)
@router.post("/set-parameter", response_model=CommandPreview)
def preview_commands(payload: CommandBatch, request: Request) -> CommandPreview:
    try:
        return _store(request).preview_commands(payload)
    except DocumentConflict as exc:
        raise HTTPException(status_code=409, detail=str(exc)) from exc
    except (OSError, ValueError) as exc:
        raise HTTPException(status_code=400, detail=str(exc)) from exc


@router.post("/apply-changes", response_model=CommandResult)
def apply_commands(payload: CommandBatch, request: Request) -> CommandResult:
    try:
        return _store(request).apply_commands(payload)
    except DocumentConflict as exc:
        raise HTTPException(status_code=409, detail=str(exc)) from exc
    except CommandValidationError as exc:
        raise HTTPException(
            status_code=422,
            detail=[issue.model_dump() for issue in exc.issues],
        ) from exc
    except (OSError, ValueError) as exc:
        raise HTTPException(status_code=400, detail=str(exc)) from exc


@router.post("/preview-csv", response_model=CsvPreview)
def preview_csv(payload: CsvPreviewRequest, request: Request) -> CsvPreview:
    try:
        return _store(request).preview_csv(payload.source_path, payload.csv_path)
    except FileNotFoundError as exc:
        raise HTTPException(status_code=404, detail=str(exc)) from exc
    except (OSError, ValueError) as exc:
        raise HTTPException(status_code=400, detail=str(exc)) from exc


__all__ = ["router"]
