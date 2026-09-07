from pathlib import Path

from vg2c import compile_document
from vg2c_ui.domain.models import WorkflowSidecar
from vg2c_ui.services.sidecar import read_sidecar, write_sidecar
from vg2c_ui.services.workflow_builder import build_workflow

FIXTURES = Path(__file__).parents[1] / "fixtures"


def test_workflow_serialization_is_stable_and_includes_editor_metadata(tmp_path):
    result = compile_document(FIXTURES / "new_icmpcs.txt")

    first = build_workflow(result, tmp_path / "new_icmpcs.py", result.generated_python)
    second = build_workflow(result, tmp_path / "new_icmpcs.py", result.generated_python)

    assert first.model_dump_json() == second.model_dump_json()
    assert first.steps
    assert {scope.node_kind for scope in first.scopes} >= {"if", "branch", "loop"}
    assert first.artifacts
    assert any(artifact.producer_step_ids for artifact in first.artifacts)
    assert any(artifact.consumer_step_ids for artifact in first.artifacts)
    assert all(step.description for step in first.steps)
    assert all(step.utility.class_name for step in first.steps)
    assert all(step.utility.module.startswith("vg2c.") for step in first.steps)
    fields = type(first).model_fields
    assert "layout" not in fields
    assert "control_edges" not in fields
    assert "data_edges" not in fields


def test_python_embed_is_explicitly_read_only(tmp_path):
    result = compile_document(FIXTURES / "script_another.txt")
    workflow = build_workflow(result, tmp_path / "script_another.py", result.generated_python)

    embedded = [step for step in workflow.steps if step.functional_kind == "PYTHON_EMBED"]
    assert embedded
    assert all(step.validation_state == "unsupported" for step in embedded)
    assert all(parameter.editable is False for step in embedded for parameter in step.parameters)


def test_sidecar_round_trip_contains_no_presentation_layout(tmp_path):
    output = tmp_path / "workflow.py"
    sidecar = WorkflowSidecar(
        source_hash="source",
        output_hash="output",
        revision=3,
    )

    written = write_sidecar(output, sidecar)

    assert written == tmp_path / "workflow.vg2c-ui.json"
    assert read_sidecar(output) == sidecar
    assert "layout" not in sidecar.model_dump()
