from __future__ import annotations

from fastapi import APIRouter, HTTPException, Request

from vg2c.editing import ChangeValidationError
from vg2c_ui.api.models import (
    ChangeBatch,
    ChangePreviewView,
    ChangeResultView,
    DocumentSnapshot,
    ReorderRequest,
)
from vg2c_ui.services.document_store import DocumentStore, RevisionConflict, get_document_store

router = APIRouter(prefix="/api/changes", tags=["changes"])


def _store(request: Request) -> DocumentStore:
    return get_document_store(request)


def _validation_error(exc: ChangeValidationError) -> HTTPException:
    return HTTPException(
        status_code=422,
        detail=[
            {
                "level": issue.level,
                "code": issue.code,
                "message": issue.message,
                "binding_id": issue.binding_id,
            }
            for issue in exc.issues
        ],
    )


@router.post("/preview", response_model=ChangePreviewView)
def preview_changes(payload: ChangeBatch, request: Request) -> ChangePreviewView:
    try:
        return _store(request).preview(payload)
    except RevisionConflict as exc:
        raise HTTPException(status_code=409, detail=str(exc)) from exc
    except (OSError, ValueError) as exc:
        raise HTTPException(status_code=400, detail=str(exc)) from exc


@router.post("/save", response_model=ChangeResultView)
def save_changes(payload: ChangeBatch, request: Request) -> ChangeResultView:
    try:
        return _store(request).save(payload)
    except RevisionConflict as exc:
        raise HTTPException(status_code=409, detail=str(exc)) from exc
    except ChangeValidationError as exc:
        raise _validation_error(exc) from exc
    except (OSError, ValueError) as exc:
        raise HTTPException(status_code=400, detail=str(exc)) from exc


@router.post("/generate", response_model=ChangeResultView)
def generate_output(payload: DocumentSnapshot, request: Request) -> ChangeResultView:
    try:
        return _store(request).generate(payload)
    except RevisionConflict as exc:
        raise HTTPException(status_code=409, detail=str(exc)) from exc
    except ChangeValidationError as exc:
        raise _validation_error(exc) from exc
    except (OSError, ValueError) as exc:
        raise HTTPException(status_code=400, detail=str(exc)) from exc


@router.post("/reorder", response_model=ChangeResultView)
def reorder_execution(payload: ReorderRequest, request: Request) -> ChangeResultView:
    try:
        return _store(request).reorder(payload)
    except RevisionConflict as exc:
        raise HTTPException(status_code=409, detail=str(exc)) from exc
    except ChangeValidationError as exc:
        raise _validation_error(exc) from exc
    except (OSError, ValueError) as exc:
        raise HTTPException(status_code=400, detail=str(exc)) from exc


__all__ = ["router"]
