from pathlib import Path

from vg2c.sql_editor.models import SqlEditableModel, SqlEditCapabilities, SqlSpan
from vg2c_ui.api.contracts import render_typescript_contracts
from vg2c_ui.api.serialization import sql_model_view

ROOT = Path(__file__).parents[2]


def test_generated_typescript_contracts_are_in_sync():
    target = ROOT / "src/vg2c_ui/frontend/src/contracts.generated.ts"
    assert target.read_text(encoding="utf-8") == render_typescript_contracts()


def test_contracts_are_transport_views_not_workflow_domain_copies():
    generated = render_typescript_contracts()
    assert "interface DocumentView" in generated
    assert "interface WorkflowDocument" not in generated
    assert "overrides:" not in generated
    assert "capabilities:" in generated
    assert "interface WorkspaceUploadPolicyView" in generated
    assert "allowed_upload_suffixes:" in generated


def test_sql_transport_exposes_backend_owned_before_and_after_slices():
    source = "-- setup\nSELECT 1\n-- cleanup"
    start = source.index("SELECT")
    end = start + len("SELECT 1")
    model = SqlEditableModel(
        source=source,
        statement_span=SqlSpan(start, end),
        selections=(),
        filters=(),
        joins=(),
        sources=(),
        capabilities=SqlEditCapabilities(
            selected=False,
            filters=False,
            joins=False,
        ),
        read_only_reason="Read only fixture",
        select_list_span=None,
        where_clause_span=None,
        where_body_span=None,
        from_clause_span=None,
    )

    view = sql_model_view(model)

    assert view.source[: view.statement_span.start] == "-- setup\n"
    assert view.source[view.statement_span.end :] == "\n-- cleanup"
