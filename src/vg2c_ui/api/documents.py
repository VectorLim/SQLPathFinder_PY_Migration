from __future__ import annotations

from fastapi import APIRouter, HTTPException, Request
from pydantic import BaseModel

from vg2c_ui.domain.models import WorkflowDocument, WorkflowLayout
from vg2c_ui.services.document_store import DocumentStore

router = APIRouter(prefix="/api/documents", tags=["documents"])


class OpenDocumentRequest(BaseModel):
    source_path: str
    output_path: str | None = None


class SaveLayoutRequest(BaseModel):
    source_path: str
    output_path: str
    source_hash: str
    output_hash: str
    revision: int
    layout: WorkflowLayout


def _store(request: Request) -> DocumentStore:
    return request.app.state.document_store


@router.post("/open", response_model=WorkflowDocument)
def open_document(payload: OpenDocumentRequest, request: Request) -> WorkflowDocument:
    try:
        return _store(request).open_document(payload.source_path, payload.output_path)
    except (OSError, ValueError) as exc:
        raise HTTPException(status_code=400, detail=str(exc)) from exc


@router.put("/layout", status_code=204)
def save_layout(payload: SaveLayoutRequest, request: Request) -> None:
    try:
        current = _store(request).open_document(payload.source_path, payload.output_path)
        if (
            current.source_hash != payload.source_hash
            or current.output_hash != payload.output_hash
            or current.revision != payload.revision
        ):
            raise HTTPException(status_code=409, detail="document revision changed")
        current.layout = payload.layout
        _store(request).save_layout(current)
    except HTTPException:
        raise
    except (OSError, ValueError) as exc:
        raise HTTPException(status_code=400, detail=str(exc)) from exc


__all__ = ["router"]
