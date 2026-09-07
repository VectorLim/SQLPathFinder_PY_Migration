from __future__ import annotations

from fastapi import APIRouter, HTTPException, Request
from pydantic import BaseModel

from vg2c_ui.domain.models import WorkflowDocument
from vg2c_ui.services.document_store import DocumentStore

router = APIRouter(prefix="/api/documents", tags=["documents"])


class OpenDocumentRequest(BaseModel):
    source_path: str
    output_path: str | None = None


def _store(request: Request) -> DocumentStore:
    return request.app.state.document_store


@router.post("/open", response_model=WorkflowDocument)
def open_document(payload: OpenDocumentRequest, request: Request) -> WorkflowDocument:
    """Open a translated document without carrying diagram presentation state."""
    try:
        return _store(request).open_document(payload.source_path, payload.output_path)
    except (OSError, ValueError) as exc:
        raise HTTPException(status_code=400, detail=str(exc)) from exc


__all__ = ["router"]
