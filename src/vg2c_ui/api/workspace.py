from __future__ import annotations

from fastapi import APIRouter, HTTPException, Request

from vg2c.editing import ChangeValidationError
from vg2c_ui.api.models import WorkspaceProjectionRequest, WorkspaceProjectionView
from vg2c_ui.services.document_store import DocumentStore

router = APIRouter(prefix="/api/workspace", tags=["workspace"])


@router.post("/project", response_model=WorkspaceProjectionView)
def project_workspace(
    payload: WorkspaceProjectionRequest, request: Request
) -> WorkspaceProjectionView:
    store: DocumentStore = request.app.state.document_store
    try:
        return store.project_workspace(payload)
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
