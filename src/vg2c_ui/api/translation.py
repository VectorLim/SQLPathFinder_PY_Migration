from __future__ import annotations

from pathlib import Path

from fastapi import APIRouter, Request

from vg2c_ui.api.models import (
    BatchTranslationRequest,
    BatchTranslationResponse,
    DiagnosticView,
    DocumentView,
)
from vg2c_ui.services.document_store import DocumentStore

router = APIRouter(prefix="/api/translations", tags=["translations"])


@router.post("/batch", response_model=BatchTranslationResponse)
def translate_batch(
    payload: BatchTranslationRequest, request: Request
) -> BatchTranslationResponse:
    store: DocumentStore = request.app.state.document_store
    documents: list[DocumentView] = []
    diagnostics: list[DiagnosticView] = []
    for source_path in payload.source_paths:
        try:
            output_path = (
                str(Path(payload.out_dir) / Path(source_path).with_suffix(".py").name)
                if payload.out_dir
                else None
            )
            documents.append(store.translate(source_path, output_path).view)
        except (OSError, ValueError) as exc:
            diagnostics.append(
                DiagnosticView(
                    level="error",
                    code="translation-failed",
                    message=f"{source_path}: {exc}",
                )
            )
    return BatchTranslationResponse(documents=documents, diagnostics=diagnostics)


__all__ = ["router"]
