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
    assert uploaded.role == "source"
    assert uploaded.translatable is True
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

    files = {item.path: item for item in manager.list_files(first)}
    assert files["inputs/script_short.txt"].role == "source"
    assert files["inputs/script_short.txt"].translatable is True
    assert files["generated/inputs/script_short.py"].role == "generated"
    assert files["generated/inputs/script_short.py"].translatable is False


def test_workspace_file_classification_is_backend_owned(tmp_path: Path):
    manager = WorkspaceManager(tmp_path / "workspaces")
    workspace, _ = manager.resolve_or_create(None)
    (workspace.root / "inputs" / "table.csv").write_text("a,b\n1,2\n", encoding="utf-8")
    (workspace.root / "inputs" / "nested").mkdir()
    (workspace.root / "inputs" / "nested" / "other.txt").write_text("script", encoding="utf-8")
    (workspace.root / "generated" / "report.csv").write_text("a\n1\n", encoding="utf-8")

    files = {item.path: item for item in manager.list_files(workspace)}
    assert files["inputs/table.csv"].role == "data"
    assert files["inputs/table.csv"].translatable is False
    assert files["inputs/nested/other.txt"].role == "source"
    assert files["inputs/nested/other.txt"].translatable is True
    assert files["generated/report.csv"].role == "generated"
    assert files["generated/report.csv"].translatable is False


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


def test_workspace_accepts_image_attachments_inside_inputs(tmp_path: Path):
    manager = WorkspaceManager(tmp_path / "workspaces")
    workspace, _ = manager.resolve_or_create(None)
    saved = asyncio.run(
        manager.save_upload(
            workspace,
            UploadFile(filename="chart.png", file=BytesIO(b"\x89PNG\r\n\x1a\n")),
            "attachments/chart.png",
        )
    )
    assert saved.path == "inputs/attachments/chart.png"
    assert saved.role == "data"
    assert manager.resolve_file(workspace, saved.path).is_file()
