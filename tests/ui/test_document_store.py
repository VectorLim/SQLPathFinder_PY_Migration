from pathlib import Path
from shutil import copyfile
from types import SimpleNamespace

import pytest

from vg2c_ui.api.models import (
    BatchTranslationRequest,
    ChangeBatch,
    DocumentView,
    ParameterChangeRequest,
    TranslationFileRequest,
)
from vg2c_ui.api.translation import translate_batch
from vg2c_ui.app import create_app
from vg2c_ui.services.document_store import (
    DocumentStore,
    PathOutsideWorkspace,
    RevisionConflict,
)
from vg2c_ui.services.sidecar import SIDECAR_VERSION, read_sidecar

FIXTURES = Path(__file__).parents[1] / "fixtures"


def _copy_fixture(tmp_path: Path) -> Path:
    source = tmp_path / "script_short.txt"
    copyfile(FIXTURES / "script_short.txt", source)
    return source


def _editable_string(document: DocumentView):
    return next(
        parameter
        for step in document.steps
        for parameter in step.parameters
        if parameter.editable and parameter.editor_type == "string"
    )


def _batch(document: DocumentView, parameter_id: str, value: str) -> ChangeBatch:
    return ChangeBatch(
        source_path=document.source_path,
        output_path=document.output_path,
        source_hash=document.source_hash,
        output_hash=document.output_hash,
        revision=document.revision,
        changes=[ParameterChangeRequest(parameter_id=parameter_id, value=value)],
    )


def _request(store: DocumentStore):
    return SimpleNamespace(
        app=SimpleNamespace(state=SimpleNamespace(document_store=store))
    )


def test_api_exposes_only_current_transport_routes(tmp_path):
    paths = set(create_app(tmp_path).openapi()["paths"])
    assert "/api/documents/open" in paths
    assert "/api/documents/preview-csv" in paths
    assert "/api/translations/batch" in paths
    assert "/api/changes/preview" in paths
    assert "/api/changes/apply" in paths
    assert "/api/workspace/project" in paths
    assert "/api/sql/inspect" in paths
    assert "/api/sql/apply-action" in paths
    assert not any(path.startswith("/api/commands") for path in paths)


def test_translation_starts_without_editor_sidecar(tmp_path):
    source = _copy_fixture(tmp_path)
    opened = DocumentStore(tmp_path).translate(str(source))
    assert read_sidecar(Path(opened.view.output_path)) is None


def test_batch_translation_stages_browser_files_and_returns_per_file_results(tmp_path):
    content = (FIXTURES / "script_short.txt").read_text(encoding="utf-8")
    response = translate_batch(
        BatchTranslationRequest(
            files=[
                TranslationFileRequest(name="first.txt", content=content),
                TranslationFileRequest(name="second.vg2", content=content),
            ]
        ),
        _request(DocumentStore(tmp_path)),
    )

    assert [item.status for item in response.results] == ["success", "success"]
    assert [item.file_name for item in response.results] == ["first.txt", "second.vg2"]
    assert all(item.document is not None for item in response.results)
    for item in response.results:
        assert item.document is not None
        source = Path(item.document.source_path)
        assert source.is_file()
        assert ".vg2c-ui" in source.parts
        assert Path(item.document.output_path).is_file()


def test_batch_translation_failure_does_not_cancel_remaining_files(tmp_path):
    content = (FIXTURES / "script_short.txt").read_text(encoding="utf-8")
    response = translate_batch(
        BatchTranslationRequest(
            files=[
                TranslationFileRequest(name="before.txt", content=content),
                TranslationFileRequest(name="unsupported.csv", content=content),
                TranslationFileRequest(name="after.txt", content=content),
            ]
        ),
        _request(DocumentStore(tmp_path)),
    )

    assert [item.status for item in response.results] == ["success", "error", "success"]
    assert response.results[1].document is None
    assert response.results[1].diagnostics[0].code == "translation-failed"
    assert "Unsupported VG2 source file type" in response.results[1].diagnostics[0].message
    assert response.results[2].document is not None


def test_batch_translation_reports_multiple_failures_independently(tmp_path):
    response = translate_batch(
        BatchTranslationRequest(
            files=[
                TranslationFileRequest(name="one.csv", content="x"),
                TranslationFileRequest(name="two.json", content="x"),
            ]
        ),
        _request(DocumentStore(tmp_path)),
    )
    assert [item.status for item in response.results] == ["error", "error"]
    assert all(len(item.diagnostics) == 1 for item in response.results)


def test_store_rejects_paths_outside_workspace(tmp_path):
    outside = tmp_path.parent / "outside.txt"
    with pytest.raises(PathOutsideWorkspace):
        DocumentStore(tmp_path).open_document(str(outside))


def test_parameter_change_uses_core_preview_apply_and_reopens_with_effective_value(
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

    applied = store.apply(batch)
    assert applied.document.revision != document.revision
    reopened = store.open_document(source, document.output_path).view
    reopened_parameter = next(
        item
        for step in reopened.steps
        for item in step.parameters
        if item.id == parameter.id
    )
    assert reopened_parameter.value == "edited by script editor"
    with pytest.raises(RevisionConflict):
        store.preview(batch)


def test_apply_persists_only_validated_changes_in_v2_sidecar(tmp_path):
    source = _copy_fixture(tmp_path)
    store = DocumentStore(tmp_path)
    document = store.translate(str(source)).view
    parameter = _editable_string(document)
    applied = store.apply(_batch(document, parameter.id, "persisted edit"))

    sidecar = read_sidecar(Path(applied.document.output_path))
    assert sidecar is not None
    assert sidecar.schema_version == SIDECAR_VERSION == 2
    assert sidecar.source_hash == applied.document.source_hash
    assert sidecar.output_hash == applied.document.output_hash
    assert [(item.parameter_id, item.value) for item in sidecar.changes] == [
        (parameter.id, "persisted edit")
    ]
    assert "steps" not in sidecar.model_dump()
    assert "source_path" not in sidecar.model_dump()
    assert "output_path" not in sidecar.model_dump()


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
    assert not reopened.synchronized
    assert reopened.read_only_reason
    assert all(step.read_only for step in reopened.steps)
