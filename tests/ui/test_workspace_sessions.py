from __future__ import annotations

import asyncio
from io import BytesIO
from pathlib import Path

import pytest
from fastapi.testclient import TestClient
from starlette.datastructures import UploadFile

from vg2c_ui.api.models import (
    ChangeBatch,
    DocumentSnapshot,
    SemanticChangeRequest,
    SqlActionRequest,
    SqlModelRequest,
    WorkspaceDocumentRequest,
    WorkspaceProjectionRequest,
)
from vg2c.sql_editor import SqlEditError
from vg2c_ui.services.document_store import DocumentStore
from vg2c_ui.app import create_app
from vg2c_ui.services.workspaces import WorkspaceManager, WorkspacePathError

FIXTURES = Path(__file__).parents[1] / "fixtures"


def test_http_sessions_cannot_see_each_others_uploaded_server_files(tmp_path: Path):
    app = create_app(tmp_path)
    first = TestClient(app)
    second = TestClient(app)
    upload = first.post(
        "/api/workspace/files",
        files={"files": ("private.csv", b"id\n1\n", "text/csv")},
        data={"paths": "private.csv"},
    )
    assert upload.status_code == 200
    assert [item["path"] for item in first.get("/api/workspace/files").json()] == [
        "inputs/private.csv"
    ]
    assert second.get("/api/workspace/files").json() == []
    assert second.get("/api/workspace/download/inputs/private.csv").status_code == 404


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
    editable = next(
        binding
        for operation in document.semantic_operations
        for binding in operation.bindings
        if binding.editable and binding.value_schema and binding.value_schema.kind == "string"
    )
    preview = store.preview(ChangeBatch(
        **document.model_dump(),
        changes=[SemanticChangeRequest(binding_id=editable.id, value="server-only-value")],
    ))
    assert preview.valid
    assert "+++ generated/inputs/script_short.py" in preview.diff
    assert str(tmp_path) not in preview.diff


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


def test_file_backed_sql_filter_uses_workspace_choices_through_save_and_generate(tmp_path: Path):
    manager = WorkspaceManager(tmp_path / "workspaces")
    workspace, _ = manager.resolve_or_create(None)
    source = workspace.root / "inputs" / "filter.txt"
    source.write_text(
        "<OPTIONS>\n/OLEDB=SQLite\n/CSV=out.csv\n</OPTIONS>\n"
        "SELECT * FROM lots t WHERE t.lot IN "
        "SQL_Get_CSV_List('old.csv->500', lot, 't.lot In')\n"
        "<---- New Query ---->\n",
        encoding="utf-8",
    )
    (workspace.root / "inputs" / "new.csv").write_text("lot\nA\n", encoding="utf-8")
    store = DocumentStore(
        workspace.root,
        inventory_paths=lambda: (
            item.path for item in manager.list_files(workspace) if item.role == "data"
        ),
    )
    document = store.translate("inputs/filter.txt", "generated/filter.py").view
    sql = next(
        binding for operation in document.semantic_operations for binding in operation.bindings
        if "structured-sql" in binding.capabilities
    )
    model = store.inspect_sql(SqlModelRequest(**document.model_dump(), binding_id=sql.id))
    assert len(model.file_lists) == 1
    assert "inputs/new.csv" in model.file_lists[0].choices
    assert model.file_lists[0].path == "old.csv"
    with pytest.raises(SqlEditError, match="not available"):
        store.apply_sql_action(SqlActionRequest(
            **document.model_dump(), binding_id=sql.id, action="update-file-list",
            arguments={"file_list_id": model.file_lists[0].id, "path": "future.csv"},
        ))
    action = store.apply_sql_action(SqlActionRequest(
        **document.model_dump(), binding_id=sql.id, action="update-file-list",
        arguments={"file_list_id": model.file_lists[0].id, "path": "inputs/new.csv"},
    ))
    assert action.change.binding_id == model.file_lists[0].id
    saved = store.save(ChangeBatch(
        **document.model_dump(), changes=[action.change]
    )).document
    assert saved.generation_state == "stale"
    file_effect = next(
        effect for effect in saved.effects if "sql-get-csv-list" in effect.id
    )
    assert file_effect.inputs[0].path == "inputs/new.csv"
    reopened = store.open_document(source, saved.output_path).view
    inspected = store.inspect_sql(SqlModelRequest(**reopened.model_dump(), binding_id=sql.id))
    assert inspected.file_lists[0].path == "inputs/new.csv"
    generated = store.generate(DocumentSnapshot.model_validate(reopened.model_dump())).document
    assert generated.generation_state == "current"
    assert "inputs/new.csv" in Path(generated.output_path).read_text(encoding="utf-8")


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
    for client_path in ("C:/Users/alice/report.csv", "D:\\Data\\report.csv"):
        with pytest.raises(WorkspacePathError):
            manager.resolve_file(workspace, client_path)


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



def test_projected_file_choices_follow_draft_execution_order(tmp_path: Path):
    workspace = tmp_path / "workspace"
    (workspace / "inputs").mkdir(parents=True)
    source = workspace / "inputs" / "flow.txt"
    source.write_text(
        "<OPTIONS>\n/OLEDB=SQLite\n/CSV=first.csv\n</OPTIONS>\n"
        "SELECT 1 AS value\n<---- New Query ---->\n"
        "<OPTIONS>\n/OLEDB=SQLite\n/CSV=query.csv\n"
        "/TABLE=first.csv:input_table\n</OPTIONS>\n"
        "SELECT * FROM input_table\n<---- New Query ---->\n"
        "<OPTIONS>\n/OLEDB=SQLite\n/CSV=later.csv\n</OPTIONS>\n"
        "SELECT 2 AS value\n<---- New Query ---->\n",
        encoding="utf-8",
    )
    store = DocumentStore(workspace, inventory_paths=lambda: ())
    document = store.translate(
        "inputs/flow.txt", "generated/flow.py"
    ).view

    queries = [
        operation
        for operation in document.semantic_operations
        if operation.kind == "ctx.run_query"
    ]
    assert len(queries) == 3
    first, query, later = queries
    query_input = next(
        binding for binding in query.bindings
        if "file-input" in binding.capabilities
    )
    assert "generated/first.csv" in query_input.file_choices
    assert "generated/later.csv" not in query_input.file_choices

    first_output = next(
        binding for binding in first.bindings
        if "file-output" in binding.capabilities
    )
    snapshot = DocumentSnapshot.model_validate(document.model_dump())
    projected = store.project_workspace(
        WorkspaceProjectionRequest(
            documents=[
                WorkspaceDocumentRequest(
                    document_id=document.id,
                    **snapshot.model_dump(),
                    changes=[
                        SemanticChangeRequest(
                            binding_id=first_output.id,
                            value="renamed.csv",
                        )
                    ],
                )
            ]
        )
    )
    choices = projected.documents[0].file_choices[query.id]
    assert "generated/renamed.csv" in choices
    assert "generated/first.csv" not in choices
    assert "generated/later.csv" not in choices
