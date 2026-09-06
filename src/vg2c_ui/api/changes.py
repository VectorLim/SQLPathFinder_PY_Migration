from __future__ import annotations

from fastapi import APIRouter, HTTPException, Request

from vg2c.editing import ChangeValidationError
from vg2c_ui.api.models import ChangeBatch, ChangePreviewView, ChangeResultView
from vg2c_ui.services.document_store import DocumentStore, RevisionConflict

router = APIRouter(prefix="/api/changes", tags=["changes"])


def _store(request: Request) -> DocumentStore:
    return request.app.state.document_store


@router.post("/preview", response_model=ChangePreviewView)
def preview_changes(payload: ChangeBatch, request: Request) -> ChangePreviewView:
    try:
        return _store(request).preview(payload)
    except RevisionConflict as exc:
        raise HTTPException(status_code=409, detail=str(exc)) from exc
    except (OSError, ValueError) as exc:
        raise HTTPException(status_code=400, detail=str(exc)) from exc


@router.post("/apply", response_model=ChangeResultView)
def apply_changes(payload: ChangeBatch, request: Request) -> ChangeResultView:
    try:
        return _store(request).apply(payload)
    except RevisionConflict as exc:
        raise HTTPException(status_code=409, detail=str(exc)) from exc
    except ChangeValidationError as exc:
        raise HTTPException(
            status_code=422,
            detail=[
                {
                    "level": issue.level,
                    "code": issue.code,
                    "message": issue.message,
                    "parameter_id": issue.parameter_id,
                }
                for issue in exc.issues
            ],
        ) from exc
    except (OSError, ValueError) as exc:
        raise HTTPException(status_code=400, detail=str(exc)) from exc


__all__ = ["router"]
