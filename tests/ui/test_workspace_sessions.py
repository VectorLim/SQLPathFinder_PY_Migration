from __future__ import annotations

import asyncio
from io import BytesIO
from pathlib import Path

import pytest
from starlette.datastructures import UploadFile

from vg2c_ui.api.models import SqlActionRequest, SqlModelRequest
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


def test_document_file_choices_use_each_users_server_workspace(tmp_path: Path):
    manager = WorkspaceManager(tmp_path / "workspaces")
    first, _ = manager.resolve_or_create(None)
    second, _ = manager.resolve_or_create(None)
    _upload(manager, first)
    first_data = asyncio.run(
        manager.save_upload(
            first,
            UploadFile(filename="table.csv", file=BytesIO(b"name\nfirst\n")),
            "table.csv",
        )
    )
    asyncio.run(
        manager.save_upload(
            second,
            UploadFile(filename="private.csv", file=BytesIO(b"name\nsecond\n")),
            "private.csv",
        )
    )
    stale_output = first.root / "generated" / "inputs" / "owner.csv"
    stale_output.parent.mkdir(parents=True, exist_ok=True)
    stale_output.write_text("old output", encoding="utf-8")
    store = DocumentStore(
        first.root,
        expose_relative_paths=True,
        inventory_paths=lambda: (
            item.path
            for item in manager.list_files(first)
            if item.role != "source" and not item.path.endswith(".py")
        ),
    )
    document = store.translate(
        "inputs/script_short.txt", "generated/inputs/script_short.py"
    ).view
    inputs = next(
        binding
        for operation in document.semantic_operations
        for binding in operation.bindings
        if binding.name == "inputs"
    )
    assert first_data.path in inputs.file_choices
    assert "inputs/private.csv" not in inputs.file_choices
    assert "generated/inputs/owner.csv" not in inputs.file_choices
    assert str(tmp_path) not in document.model_dump_json()


def test_sql_column_choices_read_uploaded_server_csv_headers(tmp_path: Path):
    manager = WorkspaceManager(tmp_path / "workspaces")
    workspace, _ = manager.resolve_or_create(None)
    source = workspace.root / "inputs" / "script.txt"
    source.write_text(
        (FIXTURES / "script_short.txt")
        .read_text(encoding="utf-8")
        .replace("/TABLE=ww_yield.csv", "/TABLE=inputs/ww_yield.csv:ww_yield")
        .replace("/HEADERS=owner", "/HEADERS=output_only"),
        encoding="utf-8",
    )
    (workspace.root / "inputs" / "ww_yield.csv").write_text(
        "owner,amount\nalice,42\n", encoding="utf-8"
    )
    store = DocumentStore(
        workspace.root,
        inventory_paths=lambda: (
            item.path for item in manager.list_files(workspace) if item.role == "data"
        ),
    )
    document = store.translate("inputs/script.txt", "generated/script.py").view
    sql = next(
        binding
        for operation in document.semantic_operations
        for binding in operation.bindings
        if "structured-sql" in binding.capabilities
    )
    model = store.inspect_sql(SqlModelRequest(**document.model_dump(), binding_id=sql.id))
    assert [item.label for item in model.column_choices] == ["a0.owner", "a0.amount"]
    assert all("output_only" not in item.label for item in model.column_choices)
    change = store.apply_sql_action(
        SqlActionRequest(
            **document.model_dump(),
            binding_id=sql.id,
            action="add-selection",
            arguments={"column_choice_id": model.column_choices[1].id},
        )
    ).change
    assert '"a0"."amount"' in change.value


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
