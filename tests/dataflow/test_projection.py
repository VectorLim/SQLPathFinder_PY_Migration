from dataclasses import replace
from pathlib import Path
from typing import get_type_hints

from vg2c import compile_document
from vg2c.editing import SemanticChange
from vg2c.semantic.models import EditableBinding
from vg2c.utility_metadata import FileEffectDefinition, ValueSchema
from vg2c.workflow import (
    EffectiveDocument,
    WorkflowDocument,
    project_document,
    workspace_issues,
)

FIXTURES = Path(__file__).parents[1] / "fixtures"


def test_public_workspace_document_annotations_remain_resolvable():
    assert get_type_hints(WorkflowDocument)["workflow"] is EffectiveDocument


def test_new_operation_binds_declared_effects_without_a_kind_handler(tmp_path, monkeypatch):
    source = tmp_path / "new-operation.txt"
    source.write_text("<OPTIONS>\n/WRITE-FILE=Y\n/CSV=placeholder.txt\n</OPTIONS>\nbody")
    result = compile_document(source)
    original = project_document(result).operations[0]
    operation = replace(
        original, id="custom-operation", kind="custom-file-operation",
        bindings=tuple(EditableBinding(
            id=f"custom:{name}", owner_operation_id="custom-operation", name=name,
            display_label=name, schema=ValueSchema("string", path=True), value=value,
        ) for name, value in (("source", "input.csv"), ("destination", "result.csv"))),
        file_effects=(
            FileEffectDefinition("input", "read", inputs=("source",)),
            FileEffectDefinition(
                "result", "write", outputs=("destination",), conditional=True, in_loop=True,
            ),
        ),
    )
    monkeypatch.setattr("vg2c.workflow.project_semantics", lambda *args: ((operation,), (), ()))
    effects = project_document(result).effects
    assert [effect.id for effect in effects] == [
        "custom-operation:effect:input", "custom-operation:effect:result",
    ]
    assert effects[0].inputs[0].path == "input.csv"
    assert effects[1].outputs[0].path == "result.csv"
    assert effects[1].conditional and effects[1].in_loop
    operation = replace(operation, file_effects=None)
    unknown = project_document(result).effects
    assert len(unknown) == 1 and unknown[0].kind == "unknown"
    assert not unknown[0].inputs and not unknown[0].outputs


def test_scope_flags_follow_scope_ids_after_a_conditional(tmp_path):
    def block(options, body=""):
        return f"<OPTIONS>\n{options}\n</OPTIONS>\n{body}\n<---- New Query ---->\n"

    source = tmp_path / "scope-flags.txt"
    source.write_text(
        block('/UTILITIES={START-MACRO} "config.csv" "N"')
        + block('/UTILITIES={IF-THEN} "1" "EQ" "1" "" "" "" ""')
        + block('/WRITE-FILE=Y\n/CSV=inside.csv', "inside")
        + block('/UTILITIES={END-IF}')
        + block('/UTILITIES={END-MACRO}')
        + block('/WRITE-FILE=Y\n/CSV=outside.csv', "outside")
        + block('/UTILITIES=RoboCopy.va "outside.csv" "." "copies"')
    )
    effects = project_document(compile_document(source)).effects
    by_block = {effect.block_index: effect for effect in effects}
    assert by_block[0].in_loop
    inside, outside, copy = (by_block[index] for index in (2, 5, 6))
    assert inside.conditional
    assert not outside.conditional
    assert any(item.path == "outside.csv" and item.status == "guaranteed"
               for item in copy.available_before)


def test_html_layout_declares_its_parsed_output(tmp_path):
    source = tmp_path / "layout.txt"
    source.write_text("<OPTIONS>\n/REPORT=HTML-LAYOUT\n</OPTIONS>\n"
                      ":FILE:report.html\n:TITLE:Report\n<p>content</p>")
    effects = project_document(compile_document(source)).effects
    assert len(effects) == 1
    assert effects[0].kind == "write"
    assert effects[0].outputs[0].path == "report.html"


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
