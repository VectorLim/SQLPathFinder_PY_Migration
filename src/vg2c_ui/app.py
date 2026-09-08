from __future__ import annotations

from pathlib import Path

from fastapi import FastAPI
from fastapi.staticfiles import StaticFiles

from vg2c_ui.api.changes import router as changes_router
from vg2c_ui.api.documents import router as documents_router
from vg2c_ui.api.sql import router as sql_router
from vg2c_ui.api.translation import router as translation_router
from vg2c_ui.api.workspace import router as workspace_router
from vg2c_ui.services.document_store import DocumentStore


def create_app(workspace: Path | None = None) -> FastAPI:
    app = FastAPI(title="PYTHONPathFinder", version="2")
    app.state.document_store = DocumentStore(workspace or Path.cwd())
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
