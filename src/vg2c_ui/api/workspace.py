from __future__ import annotations

import tempfile
import zipfile
from pathlib import Path

from fastapi import APIRouter, File, Form, HTTPException, Request, UploadFile
from fastapi.responses import FileResponse
from starlette.background import BackgroundTask

from vg2c.editing import ChangeValidationError
from vg2c_ui.api.models import (
    WorkspaceFileView,
    WorkspaceProjectionRequest,
    WorkspaceProjectionView,
    WorkspaceUploadPolicyView,
)
from vg2c_ui.services.document_store import (
    DocumentStore,
    RevisionConflict,
    get_document_store,
)
from vg2c_ui.services.workspaces import (
    WorkspacePathError,
    get_workspace,
    get_workspace_manager,
)

router = APIRouter(prefix="/api/workspace", tags=["workspace"])


@router.post("/project", response_model=WorkspaceProjectionView)
def project_workspace(
    payload: WorkspaceProjectionRequest, request: Request
) -> WorkspaceProjectionView:
    store: DocumentStore = get_document_store(request)
    try:
        return store.project_workspace(payload)
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
                    "binding_id": issue.binding_id,
                }
                for issue in exc.issues
            ],
        ) from exc
    except (OSError, ValueError) as exc:
        raise HTTPException(status_code=400, detail=str(exc)) from exc


@router.get("/policy", response_model=WorkspaceUploadPolicyView)
def workspace_upload_policy(request: Request) -> WorkspaceUploadPolicyView:
    manager = get_workspace_manager(request)
    return WorkspaceUploadPolicyView(
        allowed_upload_suffixes=sorted(manager.allowed_upload_suffixes),
        max_upload_bytes=manager.max_upload_bytes,
        max_file_count=manager.max_file_count,
        max_workspace_bytes=manager.max_workspace_bytes,
    )


@router.get("/files", response_model=list[WorkspaceFileView])
def list_workspace_files(request: Request) -> list[WorkspaceFileView]:
    manager = get_workspace_manager(request)
    workspace = get_workspace(request)
    return [_file_view(item) for item in manager.list_files(workspace)]


@router.post("/files", response_model=list[WorkspaceFileView])
async def upload_workspace_files(
    request: Request,
    files: list[UploadFile] = File(...),
    paths: list[str] = Form(...),
) -> list[WorkspaceFileView]:
    if len(files) != len(paths):
        raise HTTPException(
            status_code=400, detail="Each uploaded file must include a relative path."
        )
    manager = get_workspace_manager(request)
    workspace = get_workspace(request)
    saved: list[WorkspaceFileView] = []
    try:
        for upload, path in zip(files, paths, strict=True):
            item = await manager.save_upload(workspace, upload, path)
            saved.append(_file_view(item))
    except (OSError, WorkspacePathError) as exc:
        raise HTTPException(status_code=400, detail=str(exc)) from exc
    finally:
        for upload in files:
            await upload.close()
    return saved


@router.get("/download/{file_path:path}")
def download_workspace_file(file_path: str, request: Request) -> FileResponse:
    manager = get_workspace_manager(request)
    workspace = get_workspace(request)
    try:
        target = manager.resolve_file(workspace, file_path)
    except WorkspacePathError as exc:
        raise HTTPException(status_code=400, detail=str(exc)) from exc
    if not target.is_file() or target.is_symlink():
        raise HTTPException(status_code=404, detail="Workspace file not found.")
    manager.touch(workspace)
    return FileResponse(target, filename=target.name)


@router.get("/archive")
def download_workspace_archive(request: Request) -> FileResponse:
    manager = get_workspace_manager(request)
    workspace = get_workspace(request)
    handle = tempfile.NamedTemporaryFile(
        prefix="vg2c-workspace-", suffix=".zip", delete=False
    )
    archive_path = Path(handle.name)
    handle.close()
    try:
        with zipfile.ZipFile(
            archive_path, "w", compression=zipfile.ZIP_DEFLATED
        ) as archive:
            for item in manager.list_files(workspace):
                archive.write(
                    manager.resolve_file(workspace, item.path), arcname=item.path
                )
    except BaseException:
        archive_path.unlink(missing_ok=True)
        raise
    manager.touch(workspace)
    return FileResponse(
        archive_path,
        filename="vg2c-workspace.zip",
        media_type="application/zip",
        background=BackgroundTask(archive_path.unlink, missing_ok=True),
    )


__all__ = ["router"]


def _file_view(item) -> WorkspaceFileView:
    return WorkspaceFileView(
        path=item.path,
        size_bytes=item.size_bytes,
        modified_at=item.modified_at,
        role=item.role,
        translatable=item.translatable,
    )
