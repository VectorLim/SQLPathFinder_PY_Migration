from __future__ import annotations

from fastapi import APIRouter, HTTPException, Request

from vg2c_ui.api.models import (
    CsvPreviewRequest,
    CsvPreviewView,
    HtmlPreviewRequest,
    HtmlPreviewView,
    DocumentReference,
    DocumentView,
)
from vg2c_ui.services.document_store import (
    DocumentStore,
    RevisionConflict,
    get_document_store,
)

router = APIRouter(prefix="/api/documents", tags=["documents"])


def _store(request: Request) -> DocumentStore:
    return get_document_store(request)


@router.post("/open", response_model=DocumentView)
def open_document(payload: DocumentReference, request: Request) -> DocumentView:
    try:
        return (
            _store(request).open_document(payload.source_path, payload.output_path).view
        )
    except (OSError, ValueError) as exc:
        raise HTTPException(status_code=400, detail=str(exc)) from exc


@router.post("/preview-html", response_model=HtmlPreviewView)
def preview_html(payload: HtmlPreviewRequest, request: Request) -> HtmlPreviewView:
    return _store(request).preview_html(payload)


@router.post("/preview-csv", response_model=CsvPreviewView)
def preview_csv(payload: CsvPreviewRequest, request: Request) -> CsvPreviewView:
    try:
        return _store(request).preview_csv(payload)
    except RevisionConflict as exc:
        raise HTTPException(status_code=409, detail=str(exc)) from exc
    except FileNotFoundError as exc:
        raise HTTPException(status_code=404, detail=str(exc)) from exc
    except (OSError, ValueError) as exc:
        raise HTTPException(status_code=400, detail=str(exc)) from exc


__all__ = ["router"]
