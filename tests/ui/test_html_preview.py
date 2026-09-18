from __future__ import annotations

from pathlib import Path

from vg2c_ui.api.models import HtmlPreviewRequest
from vg2c_ui.services.document_store import DocumentStore

FIXTURES = Path(__file__).parents[1] / "fixtures"


def test_html_preview_replays_safely_and_does_not_write_outputs(tmp_path: Path):
    source = tmp_path / "html_test.txt"
    source.write_bytes((FIXTURES / "html_test.txt").read_bytes())
    store = DocumentStore(tmp_path)
    opened = store.translate(str(source), str(tmp_path / "html_test.py"))
    view = opened.view
    operation = next(
        item for item in view.semantic_operations if item.kind == "html_report.layout"
    )

    request = HtmlPreviewRequest(
        schema_version=5,
        source_path=view.source_path,
        output_path=view.output_path,
        source_hash=view.source_hash,
        output_hash=view.output_hash,
        revision=view.revision,
        compiler_hash=view.compiler_hash,
        operation_id=operation.id,
        changes=[],
    )
    before = {path.relative_to(tmp_path) for path in tmp_path.rglob("*") if path.is_file()}
    preview = store.preview_html(request)
    after = {path.relative_to(tmp_path) for path in tmp_path.rglob("*") if path.is_file()}

    assert preview.state == "error"
    assert preview.message and "Missing preview input" in preview.message
    assert before == after
    assert not any(path.suffix.lower() in {".htm", ".html", ".css"} for path in after)
