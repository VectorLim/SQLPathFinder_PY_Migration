from pathlib import Path

from vg2c_ui.api.contracts import render_typescript_contracts

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
