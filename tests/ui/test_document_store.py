import json
from pathlib import Path
from shutil import copyfile
from types import SimpleNamespace

import pytest

from vg2c_ui.api.models import (
    BatchTranslationRequest,
    ChangeBatch,
    DocumentSnapshot,
    DocumentView,
    HtmlPreviewRequest,
    ReorderRequest,
    SemanticChangeRequest,
    SqlModelRequest,
    WorkspaceDocumentRequest,
    WorkspaceProjectionRequest,
)
from vg2c_ui.api.translation import translate_batch
from vg2c_ui.app import create_app
from vg2c_ui.services.document_store import (
    DocumentStore,
    PathOutsideWorkspace,
    RevisionConflict,
)
from vg2c_ui.services.sidecar import SIDECAR_VERSION, read_sidecar, sidecar_path

FIXTURES = Path(__file__).parents[1] / "fixtures"


def _copy_fixture(tmp_path: Path) -> Path:
    source = tmp_path / "script_short.txt"
    copyfile(FIXTURES / "script_short.txt", source)
    return source


def _editable_string(document: DocumentView):
    return next(
        parameter
        for step in document.steps
        for operation in step.operations
        for parameter in operation.parameters
        if parameter.editable and parameter.editor_type == "string"
    )


def _batch(document: DocumentView, parameter_id: str, value: str) -> ChangeBatch:
    return ChangeBatch(
        **document.model_dump(),
        changes=[SemanticChangeRequest(binding_id=parameter_id, value=value)],
    )


def test_shared_global_edits_persist_across_steps(tmp_path):
    source = tmp_path / "shared.txt"
    source.write_text(
        (
            "<OPTIONS>\n/OLEDB=SQLite\n/CSV=out.csv\n</OPTIONS>\nSELECT * FROM t WHERE lot = '1'\n"
            "<---- New Query ---->\n"
        )
        * 2,
        encoding="utf-8",
    )
    store = DocumentStore(tmp_path)
    document = store.translate(str(source)).view
    globals = [
        p
        for step in document.steps
        for operation in step.operations
        for p in operation.parameters
        if p.name == "LOT"
    ]
    assert len(globals) == 2 and globals[0].id == globals[1].id
    batch = _batch(document, globals[0].id, "2")
    assert store.preview(batch).valid
    store.save(batch)
    reopened = store.open_document(source).view
    assert reopened.read_only_reason is None
    assert [
        p.value
        for step in reopened.steps
        for operation in step.operations
        for p in operation.parameters
        if p.name == "LOT"
    ] == [
        "2",
        "2",
    ]
    lot = next(symbol for symbol in reopened.symbols if symbol.display_name == "LOT")
    assert lot.value == "2"
    assert lot.value_binding_id == globals[0].id
    conflicting = _batch(reopened, globals[0].id, "3")
    conflicting.changes.append(
        SemanticChangeRequest(binding_id=globals[1].id, value="4")
    )
    preview = store.preview(conflicting)
    assert not preview.valid
    assert preview.issues[0].code == "conflicting-global-change"


def test_api_exposes_only_current_transport_routes(tmp_path):
    paths = set(create_app(tmp_path).openapi()["paths"])
    assert "/api/documents/open" in paths
    assert "/api/documents/preview-csv" not in paths
    assert "/api/documents/preview-html" in paths
    assert "/api/translations/batch" in paths
    assert "/api/changes/preview" in paths
    assert "/api/changes/save" in paths
    assert "/api/changes/generate" in paths
    assert "/api/changes/apply" not in paths
    assert "/api/workspace/project" in paths
    assert "/api/sql/inspect" in paths
    assert "/api/sql/apply-action" in paths
    assert not any(path.startswith("/api/commands") for path in paths)


def test_translation_starts_without_editor_sidecar(tmp_path):
    source = _copy_fixture(tmp_path)
    opened = DocumentStore(tmp_path).translate(str(source))
    assert read_sidecar(Path(opened.view.output_path)) is None


def test_batch_translation_resolves_out_dir_to_output_files(tmp_path):
    source = _copy_fixture(tmp_path)
    store = DocumentStore(tmp_path)
    request = SimpleNamespace(
        app=SimpleNamespace(state=SimpleNamespace(document_store=store))
    )
    response = translate_batch(
        BatchTranslationRequest(
            source_paths=[source.name],
            out_dir="generated",
        ),
        request,
    )
    assert not response.diagnostics
    assert response.documents[0].output_path == str(
        (tmp_path / "generated" / "script_short.py").resolve()
    )


def test_store_rejects_paths_outside_workspace(tmp_path):
    outside = tmp_path.parent / "outside.txt"
    with pytest.raises(PathOutsideWorkspace):
        DocumentStore(tmp_path).open_document(str(outside))


def test_parameter_change_uses_core_preview_save_and_reopens_with_effective_value(
    tmp_path,
):
    source = _copy_fixture(tmp_path)
    store = DocumentStore(tmp_path)
    document = store.translate(str(source)).view
    parameter = _editable_string(document)
    batch = _batch(document, parameter.id, "edited by script editor")

    before = Path(document.output_path).read_bytes()
    preview = store.preview(batch)
    assert preview.valid
    assert "edited by script editor" in preview.diff
    assert Path(document.output_path).read_bytes() == before

    applied = store.save(batch)
    assert applied.document.revision != document.revision
    reopened = store.open_document(source, document.output_path).view
    reopened_parameter = next(
        item
        for step in reopened.steps
        for operation in step.operations
        for item in operation.parameters
        if item.id == parameter.id
    )
    assert reopened_parameter.value == "edited by script editor"
    with pytest.raises(RevisionConflict):
        store.preview(batch)


def test_save_persists_only_validated_changes_in_v4_sidecar(tmp_path):
    source = _copy_fixture(tmp_path)
    store = DocumentStore(tmp_path)
    document = store.translate(str(source)).view
    parameter = _editable_string(document)
    applied = store.save(_batch(document, parameter.id, "persisted edit"))

    sidecar = read_sidecar(Path(applied.document.output_path))
    assert sidecar is not None
    assert sidecar.schema_version == SIDECAR_VERSION == 4
    assert sidecar.source_hash == applied.document.source_hash
    assert sidecar.last_generated_hash == applied.document.output_hash
    assert [(item.binding_id, item.value) for item in sidecar.value_changes] == [
        (parameter.id, "persisted edit")
    ]
    assert "steps" not in sidecar.model_dump()
    assert "source_path" not in sidecar.model_dump()
    assert "output_path" not in sidecar.model_dump()


def test_v2_sidecar_reopens_and_next_save_upgrades_to_v4(tmp_path):
    source = _copy_fixture(tmp_path)
    store = DocumentStore(tmp_path)
    document = store.translate(str(source)).view
    parameter = _editable_string(document)
    saved = store.save(_batch(document, parameter.id, "legacy edit")).document
    applied = store.generate(DocumentSnapshot.model_validate(saved.model_dump())).document
    output = Path(applied.output_path)
    path = sidecar_path(output)
    current = json.loads(path.read_text(encoding="utf-8"))
    path.write_text(
        json.dumps(
            {
                "schema_version": 2,
                "source_hash": current["source_hash"],
                "output_hash": current["last_generated_hash"],
                "changes": [
                    {"parameter_id": parameter.id, "value": "legacy edit"}
                ],
            },
            indent=2,
        )
        + "\n",
        encoding="utf-8",
    )

    legacy = read_sidecar(output)
    assert legacy is not None
    assert legacy.schema_version == 4 and legacy.legacy_version == 2
    assert [(item.binding_id, item.value) for item in legacy.value_changes] == [
        (parameter.id, "legacy edit")
    ]

    reopened = store.open_document(source, output).view
    assert reopened.read_only_reason is None, [
        (item.code, item.message) for item in reopened.diagnostics
    ]
    reopened_parameter = next(
        item
        for step in reopened.steps
        for operation in step.operations
        for item in operation.parameters
        if item.id == parameter.id
    )
    assert reopened_parameter.value == "legacy edit"

    upgraded = store.save(_batch(reopened, parameter.id, "upgraded edit")).document
    payload = json.loads(sidecar_path(Path(upgraded.output_path)).read_text(encoding="utf-8"))
    assert payload["schema_version"] == 4
    assert payload["value_changes"] == [
        {"binding_id": parameter.id, "value": "upgraded edit", "symbol_id": None}
    ]


def test_v3_sidecar_migrates_only_on_explicit_save(tmp_path):
    source = _copy_fixture(tmp_path)
    store = DocumentStore(tmp_path)
    original = store.translate(str(source)).view
    parameter = _editable_string(original)
    saved = store.save(_batch(original, parameter.id, "legacy v3")).document
    applied = store.generate(DocumentSnapshot.model_validate(saved.model_dump())).document
    output = Path(applied.output_path)
    path = sidecar_path(output)
    path.write_text(
        json.dumps({
            "schema_version": 3,
            "source_hash": applied.source_hash,
            "output_hash": applied.output_hash,
            "changes": [{"binding_id": parameter.id, "value": "legacy v3"}],
        }) + "\n",
        encoding="utf-8",
    )
    before = path.read_bytes()
    reopened = store.open_document(source, output).view
    assert reopened.read_only_reason is None
    assert path.read_bytes() == before
    store.save(_batch(reopened, parameter.id, "saved v4"))
    assert json.loads(path.read_text(encoding="utf-8"))["schema_version"] == 4


def test_save_keeps_output_stale_and_generate_uses_only_saved_state(tmp_path):
    source = _copy_fixture(tmp_path)
    store = DocumentStore(tmp_path)
    original = store.translate(str(source)).view
    output = Path(original.output_path)
    before = output.read_bytes()
    target = next(
        binding
        for operation in original.semantic_operations
        for binding in operation.bindings
        if binding.name == "output"
    )
    saved = store.save(_batch(original, target.id, "saved.csv")).document
    assert output.read_bytes() == before
    assert saved.read_only_reason is None
    assert saved.generation_state == "stale"
    assert store._generation_state(output, "different", read_sidecar(output)) == "stale"
    assert saved.effects[0].outputs[0].path == "saved.csv"

    sql = next(
        binding
        for operation in saved.semantic_operations
        for binding in operation.bindings
        if "structured-sql" in binding.capabilities
    )
    model = store.inspect_sql(SqlModelRequest(**saved.model_dump(), binding_id=sql.id))
    assert model.selections
    store.preview(_batch(saved, target.id, "another.csv"))
    workspace = store.project_workspace(
        WorkspaceProjectionRequest(documents=[
            WorkspaceDocumentRequest(document_id=saved.id, **saved.model_dump())
        ])
    )
    assert workspace.documents[0].effects[0].outputs[0].path == "saved.csv"

    persisted = read_sidecar(output)
    generated = store.generate(DocumentSnapshot.model_validate(saved.model_dump())).document
    assert output.read_bytes() != before
    assert generated.read_only_reason is None
    assert generated.generation_state == "current"
    assert read_sidecar(output).value_changes == persisted.value_changes
    assert read_sidecar(output).last_generated_hash == generated.output_hash


def test_missing_generated_output_remains_editable_after_save(tmp_path):
    source = _copy_fixture(tmp_path)
    store = DocumentStore(tmp_path)
    original = store.translate(str(source)).view
    parameter = _editable_string(original)
    saved = store.save(_batch(original, parameter.id, "saved while missing")).document
    Path(saved.output_path).unlink()
    reopened = store.open_document(source).view
    assert reopened.read_only_reason is None
    assert reopened.output_hash == ""
    assert reopened.generation_state == "missing"
    assert store.preview(_batch(reopened, parameter.id, "new draft")).valid
    sql = next(
        binding
        for operation in reopened.semantic_operations
        for binding in operation.bindings
        if "structured-sql" in binding.capabilities
    )
    assert store.inspect_sql(SqlModelRequest(**reopened.model_dump(), binding_id=sql.id))
    generated = store.generate(DocumentSnapshot.model_validate(reopened.model_dump())).document
    assert generated.read_only_reason is None and Path(generated.output_path).exists()
    assert generated.generation_state == "current"


def test_external_python_change_becomes_read_only_without_semantic_reparse(tmp_path):
    source = _copy_fixture(tmp_path)
    store = DocumentStore(tmp_path)
    document = store.translate(str(source)).view
    output = Path(document.output_path)
    output.write_text(
        output.read_text(encoding="utf-8") + "\n# external edit\n",
        encoding="utf-8",
    )
    reopened = store.open_document(source, output).view
    assert reopened.read_only_reason is not None
    assert reopened.read_only_reason
    assert all(step.read_only for step in reopened.steps)


def test_revision_is_an_opaque_json_string(tmp_path):
    source = _copy_fixture(tmp_path)
    document = DocumentStore(tmp_path).translate(str(source)).view
    assert isinstance(document.revision, str)
    assert (
        DocumentView.model_validate_json(document.model_dump_json()).revision
        == document.revision
    )


def test_workspace_projection_preserves_step_references(tmp_path):
    source = _copy_fixture(tmp_path)
    store = DocumentStore(tmp_path)
    document = store.translate(str(source)).view
    projected = store.project_workspace(
        WorkspaceProjectionRequest(
            documents=[
                WorkspaceDocumentRequest(
                    document_id=document.id,
                    **document.model_dump(),
                ),
            ]
        )
    )
    assert projected.documents[0].artifacts
    operation_ids = {
        operation.id for step in document.steps for operation in step.operations
    }
    assert all(effect.operation_id in operation_ids for effect in document.effects)
    step_ids = {step.id for step in document.steps}
    for artifact in projected.documents[0].artifacts:
        assert set(artifact.producer_step_ids + artifact.consumer_step_ids) <= step_ids


def test_saved_output_change_reopens_with_effective_artifacts(tmp_path):
    source = tmp_path / "query.txt"
    source.write_text(
        "<OPTIONS>\n/OLEDB=SQLite\n/CSV=out.csv\n</OPTIONS>\n"
        "SELECT 1 AS value\n<---- New Query ---->\n",
        encoding="utf-8",
    )
    store = DocumentStore(tmp_path)
    document = store.translate(str(source)).view
    output = next(
        parameter
        for step in document.steps
        for operation in step.operations
        for parameter in operation.parameters
        if parameter.name == "output"
    )
    reopened = store.save(_batch(document, output.id, "changed.csv")).document
    assert [artifact.path for artifact in reopened.artifacts] == ["changed.csv"]
    assert reopened.effects[0].outputs[0].path == "changed.csv"
    operation_ids = {
        operation.id for step in reopened.steps for operation in step.operations
    }
    assert reopened.effects[0].operation_id in operation_ids


def test_workspace_effects_match_reopened_effects_after_default_edit(tmp_path):
    source = _copy_fixture(tmp_path)
    store = DocumentStore(tmp_path)
    document = store.translate(str(source)).view
    parameter = next(
        item
        for step in document.steps
        for operation in step.operations
        for item in operation.parameters
        if item.name == "inputs"
    )
    request = WorkspaceDocumentRequest(
        document_id=document.id,
        **document.model_dump(),
        changes=[SemanticChangeRequest(binding_id=parameter.id, value=["new.csv"])],
    )
    projected = store.project_workspace(WorkspaceProjectionRequest(documents=[request]))
    reopened = store.save(_batch(document, parameter.id, ["new.csv"])).document
    assert projected.documents[0].effects == reopened.effects
    assert [item.path for item in reopened.effects[0].inputs] == ["new.csv"]


def test_reset_removes_saved_override_and_restores_omitted_default(tmp_path):
    source = _copy_fixture(tmp_path)
    store = DocumentStore(tmp_path)
    original = store.translate(str(source)).view
    node = next(
        item
        for step in original.steps
        for operation in step.operations
        for item in operation.parameters
        if item.name == "node"
    )
    assert node.omitted and node.generated_value is None
    saved = store.save(_batch(original, node.id, "TEST")).document
    saved = store.generate(DocumentSnapshot.model_validate(saved.model_dump())).document
    reset = _batch(saved, node.id, None)
    reset.changes[0].reset = True
    assert store.preview(reset).valid
    restored = store.save(reset).document
    restored = store.generate(DocumentSnapshot.model_validate(restored.model_dump())).document
    restored_node = next(
        item
        for step in restored.steps
        for operation in step.operations
        for item in operation.parameters
        if item.id == node.id
    )
    assert restored_node.value is None and not restored_node.overridden
    assert (
        Path(restored.output_path).read_text(encoding="utf-8").find("node='TEST'") == -1
    )
    assert read_sidecar(Path(restored.output_path)).value_changes == []


def test_invalid_sidecar_is_visible_and_does_not_modify_files(tmp_path):
    from vg2c_ui.services.sidecar import sidecar_path

    source = _copy_fixture(tmp_path)
    store = DocumentStore(tmp_path)
    document = store.translate(str(source)).view
    output = Path(document.output_path)
    before = output.read_bytes()
    sidecar = sidecar_path(output)
    sidecar.write_text("not json", encoding="utf-8")
    reopened = store.open_document(str(source)).view
    assert reopened.read_only_reason is not None
    assert any(
        item.code == "saved-changes-unreconciled" for item in reopened.diagnostics
    )
    assert output.read_bytes() == before and sidecar.read_text() == "not json"


def test_failed_sidecar_save_rolls_back_output(tmp_path, monkeypatch):
    import vg2c_ui.services.document_store as module

    source = _copy_fixture(tmp_path)
    store = DocumentStore(tmp_path)
    document = store.translate(str(source)).view
    before = Path(document.output_path).read_bytes()

    def fail_save(*args, **kwargs):
        raise OSError("simulated sidecar failure")

    monkeypatch.setattr(module, "write_sidecar", fail_save)
    parameter = _editable_string(document)
    with pytest.raises(OSError, match="simulated"):
        store.save(_batch(document, parameter.id, "changed"))
    assert Path(document.output_path).read_bytes() == before
    assert store.open_document(str(source)).view.read_only_reason is None


@pytest.mark.parametrize(
    "field", ["source_hash", "output_hash", "revision", "compiler_hash"]
)
def test_read_projections_reject_stale_document_identity(tmp_path, field):
    source = _copy_fixture(tmp_path)
    store = DocumentStore(tmp_path)
    document = store.translate(str(source)).view
    payload = document.model_dump() | {field: "stale"}
    sql = next(
        parameter
        for step in document.steps
        for operation in step.operations
        for parameter in operation.parameters
        if "structured-sql" in parameter.capabilities
    )
    with pytest.raises(RevisionConflict):
        store.inspect_sql(SqlModelRequest(**payload, binding_id=sql.id))
    with pytest.raises(RevisionConflict):
        store.project_workspace(
            WorkspaceProjectionRequest(
                documents=[
                    WorkspaceDocumentRequest(**payload, document_id=document.id),
                ]
            )
        )


def test_normal_document_view_never_exposes_generated_python(tmp_path):
    source = _copy_fixture(tmp_path)
    document = DocumentStore(tmp_path).translate(str(source)).view

    assert all(step.raw_code is None for step in document.steps)


def test_condition_semantic_edit_persists_through_reopen(tmp_path):
    source = tmp_path / "condition.txt"
    source.write_text(
        """<OPTIONS>
/UTILITIES={IF-THEN} "left" "EQS" "right" "" "" "" ""
</OPTIONS>
<---- New Query ---->
<OPTIONS>
/WRITE-FILE=Y
/CSV=inside.txt
</OPTIONS>
inside
<---- New Query ---->
<OPTIONS>
/UTILITIES={END-IF}
</OPTIONS>
<---- New Query ---->
""",
        encoding="utf-8",
    )
    store = DocumentStore(tmp_path)
    document = store.translate(str(source)).view
    condition = next(op for op in document.semantic_operations if op.kind == "condition")
    operator = next(binding for binding in condition.bindings if binding.name == "op")

    saved = store.save(
        ChangeBatch(
            **document.model_dump(),
            changes=[SemanticChangeRequest(binding_id=operator.id, value="NES")],
        )
    ).document
    applied = store.generate(DocumentSnapshot.model_validate(saved.model_dump())).document
    reopened = store.open_document(source, applied.output_path).view
    reopened_condition = next(
        op for op in reopened.semantic_operations if op.id == condition.id
    )
    reopened_operator = next(
        binding for binding in reopened_condition.bindings if binding.name == "op"
    )

    assert reopened.read_only_reason is None
    assert reopened_operator.value == "NES"
    assert " != " in Path(reopened.output_path).read_text(encoding="utf-8")


def test_symbol_selection_persists_as_identity_and_core_renders_condition(tmp_path):
    source = tmp_path / "symbols.txt"
    source.write_text(
        '<OPTIONS>\n/UTILITIES={ROWS-IN-FILE} "input.csv" "COUNT" "N"\n</OPTIONS>\n'
        '<---- New Query ---->\n'
        '<OPTIONS>\n/UTILITIES={IF-THEN} "left" "EQS" "right" "" "" "" ""\n</OPTIONS>\n'
        '<---- New Query ---->\n'
        '<OPTIONS>\n/WRITE-FILE=Y\n/CSV=inside.txt\n</OPTIONS>\ninside\n'
        '<---- New Query ---->\n'
        '<OPTIONS>\n/UTILITIES={END-IF}\n</OPTIONS>\n'
        '<---- New Query ---->\n',
        encoding="utf-8",
    )
    store = DocumentStore(tmp_path)
    document = store.translate(str(source)).view
    condition = next(op for op in document.semantic_operations if op.kind == "condition")
    right = next(binding for binding in condition.bindings if binding.name == "rhs")
    count = next(symbol for symbol in document.symbols if symbol.display_name == "COUNT")
    saved = store.save(ChangeBatch(
        **document.model_dump(),
        changes=[SemanticChangeRequest(binding_id=right.id, symbol_id=count.id)],
    )).document

    sidecar = read_sidecar(Path(saved.output_path))
    assert sidecar is not None
    assert sidecar.value_changes[0].symbol_id == count.id
    assert sidecar.value_changes[0].value is None
    reopened = store.open_document(source, saved.output_path).view
    selected = next(
        binding for op in reopened.semantic_operations if op.id == condition.id
        for binding in op.bindings if binding.id == right.id
    )
    assert selected.symbol_id == count.id
    assert selected.value is None
    generated = store.generate(DocumentSnapshot.model_validate(reopened.model_dump())).document
    assert "ctx.macro.named('COUNT')" in Path(generated.output_path).read_text(encoding="utf-8")


def test_reorder_persists_execution_order_and_generation_state(tmp_path):
    source = tmp_path / "reorder.txt"
    source.write_text(
        "<OPTIONS>\n/WRITE-FILE=Y\n/CSV=first.txt\n</OPTIONS>\nfirst\n"
        "<---- New Query ---->\n"
        "<OPTIONS>\n/WRITE-FILE=Y\n/CSV=second.txt\n</OPTIONS>\nsecond\n"
        "<---- New Query ---->\n",
        encoding="utf-8",
    )
    store = DocumentStore(tmp_path)
    original = store.translate(str(source)).view
    first, second = original.semantic_operations
    assert first.scope_id is not None and second.scope_id is not None
    assert second.scope_id in first.reorder_targets

    saved = store.reorder(ReorderRequest(
        **original.model_dump(),
        source_scope_id=first.scope_id,
        target_scope_id=second.scope_id,
    )).document
    assert saved.generation_state == "stale"
    assert [op.id for op in saved.semantic_operations] == [second.id, first.id]
    sidecar = read_sidecar(Path(saved.output_path))
    assert sidecar is not None and len(sidecar.order_changes) == 1

    reopened = store.open_document(source, saved.output_path).view
    assert [op.id for op in reopened.semantic_operations] == [second.id, first.id]
    generated = store.generate(DocumentSnapshot.model_validate(reopened.model_dump())).document
    assert generated.generation_state == "current"
    run_body = Path(generated.output_path).read_text(encoding="utf-8").split(
        "def run() -> None:", 1
    )[1]
    assert run_body.index("step_0001") < run_body.index("step_0000")


def test_html_preview_is_safe_exact_approximate_and_path_bounded(tmp_path):
    source = tmp_path / "report.txt"
    source.write_text(
        "<OPTIONS>\n"
        "/REPORT=HTML-LAYOUT\n"
        "/INSTANCE=101\n"
        "</OPTIONS>\n"
        ":FILE:preview.html\n"
        ":TITLE:Preview\n"
        "<h1>Hello Preview</h1>\n"
        "<---- New Query ---->\n",
        encoding="utf-8",
    )
    store = DocumentStore(tmp_path)
    document = store.translate(str(source)).view
    operation = next(
        item
        for item in document.semantic_operations
        if "html-preview" in item.capabilities
    )
    template = next(binding for binding in operation.bindings if binding.name == "template")

    exact = store.preview_html(
        HtmlPreviewRequest(
            **document.model_dump(),
            operation_id=operation.id,
            changes=[],
        )
    )
    assert exact.state == "exact"
    assert "<h1>Hello Preview</h1>" in exact.html
    assert exact.output_path == "preview.html"
    assert not (tmp_path / "preview.html").exists()

    approximate = store.preview_html(
        HtmlPreviewRequest(
            **document.model_dump(),
            operation_id=operation.id,
            changes=[
                SemanticChangeRequest(
                    binding_id=template.id,
                    value=":FILE:VAR(REPORT).html\n<h1>Runtime Preview</h1>\n",
                )
            ],
        )
    )
    assert approximate.state == "approximate"
    assert "runtime values" in (approximate.message or "").lower()
    assert not (tmp_path / "VAR(REPORT).html").exists()

    escaped_name = f"preview-escape-{tmp_path.name}.html"
    escaped_path = tmp_path.parent / escaped_name
    assert not escaped_path.exists()
    escaped = store.preview_html(
        HtmlPreviewRequest(
            **document.model_dump(),
            operation_id=operation.id,
            changes=[
                SemanticChangeRequest(
                    binding_id=template.id,
                    value=f":FILE:../{escaped_name}\n<h1>Escape</h1>\n",
                )
            ],
        )
    )
    assert escaped.state == "error"
    assert "workspace" in (escaped.message or "").lower()
    assert not escaped_path.exists()
