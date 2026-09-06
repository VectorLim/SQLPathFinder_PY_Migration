from __future__ import annotations

from fastapi import APIRouter, HTTPException, Request

from vg2c.sql_editor import SqlEditError
from vg2c_ui.api.models import SqlActionRequest, SqlActionResponse, SqlModelRequest, SqlModelView
from vg2c_ui.services.document_store import DocumentStore

router = APIRouter(prefix="/api/sql", tags=["sql"])


def _store(request: Request) -> DocumentStore:
    return request.app.state.document_store


@router.post("/inspect", response_model=SqlModelView)
def inspect_sql(payload: SqlModelRequest, request: Request) -> SqlModelView:
    try:
        return _store(request).inspect_sql(payload)
    except (SqlEditError, OSError, ValueError) as exc:
        raise HTTPException(status_code=400, detail=str(exc)) from exc


@router.post("/apply-action", response_model=SqlActionResponse)
def apply_sql_action(payload: SqlActionRequest, request: Request) -> SqlActionResponse:
    try:
        return _store(request).apply_sql_action(payload)
    except (SqlEditError, OSError, ValueError) as exc:
        raise HTTPException(status_code=400, detail=str(exc)) from exc


__all__ = ["router"]
