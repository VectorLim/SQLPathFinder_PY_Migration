from pathlib import Path
from shutil import copyfile

import pytest

from vg2c_ui.app import create_app
from vg2c_ui.domain.models import CommandBatch, SetParameterCommand
from vg2c_ui.services.command_service import DocumentConflict
from vg2c_ui.services.document_store import DocumentStore, PathOutsideWorkspace
from vg2c_ui.services.sidecar import read_sidecar

FIXTURES = Path(__file__).parents[1] / "fixtures"


def test_app_exposes_script_editor_endpoints_without_layout_api(tmp_path):
    paths = set(create_app(tmp_path).openapi()["paths"])

    assert "/api/documents/open" in paths
    assert "/api/translations/batch" in paths
    assert "/api/commands/get-workflow" in paths
    assert "/api/commands/preview-diff" in paths
    assert "/api/commands/apply-changes" in paths
    assert "/api/commands/preview-csv" in paths
    assert "/api/documents/layout" not in paths


def test_translation_sidecar_contains_only_editor_state(tmp_path):
    source = tmp_path / "script_short.txt"
    copyfile(FIXTURES / "script_short.txt", source)
    store = DocumentStore(tmp_path)

    document = store.translate_document(source)
    output = source.with_suffix(".py")
    sidecar = read_sidecar(output)

    assert output.is_file()
    assert sidecar is not None
    assert sidecar.source_hash == document.source_hash
    assert sidecar.output_hash == document.output_hash
    assert "layout" not in sidecar.model_dump()


def test_store_rejects_paths_outside_workspace(tmp_path):
    store = DocumentStore(tmp_path)

    with pytest.raises(PathOutsideWorkspace):
        store.resolve(tmp_path.parent / "outside.txt")


def test_parameter_changes_are_previewed_then_applied_atomically(tmp_path):
    source = tmp_path / "script_short.txt"
    copyfile(FIXTURES / "script_short.txt", source)
    store = DocumentStore(tmp_path)
    document = store.translate_document(source)
    step, parameter = next(
        (step, parameter)
        for step in document.steps
        for parameter in step.parameters
        if parameter.editable and parameter.editor_type == "string"
    )
    before = Path(document.output_path).read_bytes()
    batch = CommandBatch(
        source_path=document.source_path,
        output_path=document.output_path,
        source_hash=document.source_hash,
        output_hash=document.output_hash,
        revision=document.revision,
        commands=[
            SetParameterCommand(
                step_id=step.id,
                parameter_id=parameter.id,
                value="edited by script editor",
            )
        ],
    )

    preview = store.preview_commands(batch)

    assert preview.valid
    assert "edited by script editor" in preview.diff
    assert Path(document.output_path).read_bytes() == before

    result = store.apply_commands(batch)

    assert result.document.revision == document.revision + 1
    assert result.document.output_hash != document.output_hash
    assert "edited by script editor" in Path(document.output_path).read_text(encoding="utf-8")
    reopened = store.open_document(source, document.output_path)
    reopened_parameter = next(
        item
        for reopened_step in reopened.steps
        for item in reopened_step.parameters
        if item.id == parameter.id
    )
    assert reopened_parameter.value == "edited by script editor"
    with pytest.raises(DocumentConflict):
        store.preview_commands(batch)


def test_csv_preview_is_bounded_and_workspace_confined(tmp_path):
    source = tmp_path / "source.txt"
    source.write_text("source", encoding="utf-8")
    csv_path = tmp_path / "sample.csv"
    csv_path.write_text("name,value\nalpha,1\nbeta,2\n", encoding="utf-8")
    store = DocumentStore(tmp_path)

    preview = store.preview_csv(str(source), "sample.csv")

    assert preview.columns == ["name", "value"]
    assert preview.rows == [["alpha", "1"], ["beta", "2"]]
    with pytest.raises(PathOutsideWorkspace):
        store.preview_csv(str(source), str(tmp_path.parent / "outside.csv"))
