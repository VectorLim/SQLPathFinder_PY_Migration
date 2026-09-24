from __future__ import annotations

import os
from pathlib import Path

from fastapi import FastAPI
from fastapi.staticfiles import StaticFiles

from vg2c_ui.api.changes import router as changes_router
from vg2c_ui.api.documents import router as documents_router
from vg2c_ui.api.sql import router as sql_router
from vg2c_ui.api.translation import router as translation_router
from vg2c_ui.api.workspace import router as workspace_router
from vg2c_ui.services.document_store import DocumentStore
from vg2c_ui.services.workspaces import DisabledExecutionService, WorkspaceManager


def create_app(data_dir: Path | None = None) -> FastAPI:
    app = FastAPI(title="PYTHONPathFinder", version="2")
    root = Path(data_dir or os.getenv("VG2C_DATA_DIR", "/data")).resolve()
    manager = WorkspaceManager(
        root / "workspaces",
        retention_seconds=int(os.getenv("VG2C_WORKSPACE_RETENTION_SECONDS", str(24 * 60 * 60))),
        cleanup_interval_seconds=int(os.getenv("VG2C_CLEANUP_INTERVAL_SECONDS", str(60 * 60))),
        max_upload_bytes=int(os.getenv("VG2C_MAX_UPLOAD_BYTES", str(100 * 1024 * 1024))),
        max_file_count=int(os.getenv("VG2C_MAX_FILE_COUNT", "100")),
        max_workspace_bytes=int(
            os.getenv("VG2C_MAX_WORKSPACE_BYTES", str(500 * 1024 * 1024))
        ),
    )
    app.state.workspace_manager = manager
    app.state.execution_service = DisabledExecutionService()
    @app.middleware("http")
    async def workspace_session(request, call_next):
        if not request.url.path.startswith("/api/") or request.url.path == "/api/health":
            return await call_next(request)
        workspace, _ = manager.resolve_or_create(request.cookies.get(manager.cookie_name))
        request.state.workspace = workspace
        request.state.document_store = DocumentStore(
            workspace.root,
            expose_relative_paths=True,
            inventory_paths=lambda: (
                item.path
                for item in manager.list_files(workspace)
                if item.role != "source" and not item.path.endswith(".py")
            ),
        )
        response = await call_next(request)
        # Renew the browser session on every workspace API call so cookie expiry
        # matches the workspace's inactivity-based retention policy.
        response.set_cookie(
            key=manager.cookie_name,
            value=workspace.id,
            max_age=manager.retention_seconds,
            httponly=True,
            samesite="lax",
            secure=os.getenv("VG2C_COOKIE_SECURE", "false").lower() == "true",
        )
        return response
    app.include_router(documents_router)
    app.include_router(translation_router)
    app.include_router(changes_router)
    app.include_router(workspace_router)
    app.include_router(sql_router)

    @app.get("/api/health")
    def health() -> dict[str, str]:
        return {"status": "ok"}

    static_dir = Path(__file__).with_name("static")
    if (static_dir / "index.html").is_file():
        app.mount("/", StaticFiles(directory=static_dir, html=True), name="frontend")
    return app


__all__ = ["create_app"]
