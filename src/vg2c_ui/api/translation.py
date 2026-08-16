from __future__ import annotations

from fastapi import APIRouter, Request
from pydantic import BaseModel, Field

from vg2c_ui.domain.models import Diagnostic, WorkflowDocument
from vg2c_ui.services.document_store import DocumentStore

router = APIRouter(prefix="/api/translations", tags=["translations"])


class BatchTranslationRequest(BaseModel):
    source_paths: list[str] = Field(min_length=1, max_length=100)
    out_dir: str | None = None


class BatchTranslationResponse(BaseModel):
    documents: list[WorkflowDocument]
    diagnostics: list[Diagnostic]


@router.post("/batch", response_model=BatchTranslationResponse)
def translate_batch(
    payload: BatchTranslationRequest, request: Request
) -> BatchTranslationResponse:
    store: DocumentStore = request.app.state.document_store
    documents: list[WorkflowDocument] = []
    diagnostics: list[Diagnostic] = []
    for source_path in payload.source_paths:
        try:
            documents.append(store.translate_document(source_path, payload.out_dir))
        except (OSError, ValueError) as exc:
            diagnostics.append(
                Diagnostic(
                    level="error",
                    code="translation-failed",
                    message=f"{source_path}: {exc}",
                )
            )
    return BatchTranslationResponse(documents=documents, diagnostics=diagnostics)


__all__ = ["router"]
