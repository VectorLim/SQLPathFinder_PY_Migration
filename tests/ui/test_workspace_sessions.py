from __future__ import annotations

import asyncio
from io import BytesIO
from pathlib import Path

import pytest
from starlette.datastructures import UploadFile

from vg2c_ui.services.document_store import DocumentStore
from vg2c_ui.services.workspaces import WorkspaceManager, WorkspacePathError

FIXTURES = Path(__file__).parents[1] / "fixtures"


def _upload(manager: WorkspaceManager, workspace, name: str = "script_short.txt"):
    upload = UploadFile(filename=name, file=BytesIO((FIXTURES / "script_short.txt").read_bytes()))
    return asyncio.run(manager.save_upload(workspace, upload, name))


def test_workspace_uploads_are_isolated_and_document_paths_are_relative(tmp_path: Path):
    manager = WorkspaceManager(tmp_path / "workspaces")
    first, created = manager.resolve_or_create(None)
    second, _ = manager.resolve_or_create(None)
    assert created and first.id != second.id

    uploaded = _upload(manager, first)
    assert uploaded.path == "inputs/script_short.txt"
    assert [item.path for item in manager.list_files(first)] == ["inputs/script_short.txt"]
    assert manager.list_files(second) == []

    store = DocumentStore(first.root, expose_relative_paths=True)
    document = store.translate("inputs/script_short.txt", "generated/inputs/script_short.py").view
    assert document.source_path == "inputs/script_short.txt"
    assert document.output_path == "generated/inputs/script_short.py"
    assert str(tmp_path) not in document.model_dump_json()
    assert manager.resolve_file(second, "inputs/script_short.txt") != manager.resolve_file(
        first, "inputs/script_short.txt"
    )


def test_upload_rejects_paths_outside_workspace_and_executables(tmp_path: Path):
    manager = WorkspaceManager(tmp_path / "workspaces")
    workspace, _ = manager.resolve_or_create(None)
    with pytest.raises(WorkspacePathError):
        asyncio.run(
            manager.save_upload(
                workspace, UploadFile(filename="bad.txt", file=BytesIO(b"content")), "../bad.txt"
            )
        )
    with pytest.raises(WorkspacePathError):
        asyncio.run(
            manager.save_upload(
                workspace, UploadFile(filename="bad.py", file=BytesIO(b"print('bad')")), "bad.py"
            )
        )
