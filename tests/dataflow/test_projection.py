from pathlib import Path

from vg2c import compile_document
from vg2c.editing import SemanticChange
from vg2c.workflow import (
    WorkflowDocument,
    project_document,
    workspace_issues,
)

FIXTURES = Path(__file__).parents[1] / "fixtures"


def _artifact_parameter(result, direction: str):
    return next(
        parameter
        for step in result.emitted.steps
        for parameter in step.parameters
        if parameter.artifact_role is not None
        and parameter.artifact_role.direction == direction
    )


def _paths(workflow, phase: str) -> set[str]:
    return {
        endpoint.path
        for effect in workflow.effects
        for endpoint in (effect.outputs if phase == "output" else effect.inputs)
        if endpoint.path
    }


def test_artifact_output_draft_projects_semantic_file_effects(tmp_path):
    source = tmp_path / "script.txt"
    source.write_text((FIXTURES / "script_short.txt").read_text(encoding="utf-8"))
    result = compile_document(source)
    output = _artifact_parameter(result, "output")

    baseline = project_document(result)
    projected = project_document(
        result,
        [SemanticChange(binding_id=output.id, value="renamed.csv")],
    )

    assert _paths(baseline, "output") == {"owner.csv"}
    assert _paths(projected, "output") == {"renamed.csv"}


def test_artifact_input_draft_projects_semantic_file_effects(tmp_path):
    source = tmp_path / "script.txt"
    source.write_text((FIXTURES / "script_short.txt").read_text(encoding="utf-8"))
    result = compile_document(source)
    input_parameter = _artifact_parameter(result, "input")

    projected = project_document(
        result,
        [SemanticChange(binding_id=input_parameter.id, value=["renamed-input.csv"])],
    )

    assert "renamed-input.csv" in _paths(projected, "input")
    assert "ww_yield.csv" not in _paths(projected, "input")


def test_workspace_projection_includes_inactive_dirty_producer(tmp_path):
    template = (FIXTURES / "script_short.txt").read_text(encoding="utf-8")
    producer_source = tmp_path / "producer.txt"
    consumer_source = tmp_path / "consumer.txt"
    producer_source.write_text(template, encoding="utf-8")
    consumer_source.write_text(
        template.replace("/CSV=owner.csv", "/CSV=final.csv").replace(
            "/TABLE=ww_yield.csv", "/TABLE=owner.csv"
        ),
        encoding="utf-8",
    )
    producer = compile_document(producer_source)
    consumer = compile_document(consumer_source)
    producer_output = _artifact_parameter(producer, "output")
    baseline = (
        WorkflowDocument("producer", producer_source.with_suffix(".py"), project_document(producer)),
        WorkflowDocument("consumer", consumer_source.with_suffix(".py"), project_document(consumer)),
    )

    dirty = (
        WorkflowDocument(
            "producer",
            producer_source.with_suffix(".py"),
            project_document(
                producer,
                [SemanticChange(binding_id=producer_output.id, value="renamed.csv")],
            ),
        ),
        baseline[1],
    )
    broken = [issue for issue in workspace_issues(dirty, baseline) if issue.code == "BROKEN_DEPENDENCY"]
    assert len(broken) == 1
    assert broken[0].document_id == "consumer"
    assert broken[0].artifact == "owner.csv"


def test_workspace_projection_detects_duplicate_effective_outputs(tmp_path):
    template = (FIXTURES / "script_short.txt").read_text(encoding="utf-8")
    first_source = tmp_path / "first.txt"
    second_source = tmp_path / "second.txt"
    first_source.write_text(template, encoding="utf-8")
    second_source.write_text(
        template.replace("/CSV=owner.csv", "/CSV=other.csv"),
        encoding="utf-8",
    )
    first = compile_document(first_source)
    second = compile_document(second_source)
    second_output = _artifact_parameter(second, "output")
    baseline = (
        WorkflowDocument("first", first_source.with_suffix(".py"), project_document(first)),
        WorkflowDocument("second", second_source.with_suffix(".py"), project_document(second)),
    )
    projected = (
        baseline[0],
        WorkflowDocument(
            "second",
            second_source.with_suffix(".py"),
            project_document(
                second,
                [SemanticChange(binding_id=second_output.id, value="owner.csv")],
            ),
        ),
    )

    duplicates = [issue for issue in workspace_issues(projected, baseline) if issue.code == "DUPLICATE_OUTPUT"]
    assert {issue.document_id for issue in duplicates} == {"first", "second"}
