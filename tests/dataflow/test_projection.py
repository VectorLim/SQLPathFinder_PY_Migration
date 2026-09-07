from pathlib import Path

from vg2c import compile_document
from vg2c.dataflow.projection import project_analysis, project_workspace
from vg2c.editing import ParameterChange

FIXTURES = Path(__file__).parents[1] / "fixtures"


def _artifact_parameter(result, direction: str):
    return next(
        parameter
        for step in result.emitted.steps
        for parameter in step.parameters
        if parameter.artifact_role is not None
        and parameter.artifact_role.direction == direction
    )


def test_artifact_draft_reuses_analyzer_without_rewriting_resolver_state(tmp_path):
    source = tmp_path / "script.txt"
    source.write_text((FIXTURES / "script_short.txt").read_text(encoding="utf-8"))
    result = compile_document(source)
    output = _artifact_parameter(result, "output")

    projected = project_analysis(
        result,
        [ParameterChange(parameter_id=output.id, value="renamed.csv")],
    )

    assert projected.resolved is result.resolved
    assert {item.csv_path for item in result.analyzed.producers} == {"owner.csv"}
    assert {item.csv_path for item in projected.producers} == {"renamed.csv"}


def test_artifact_input_draft_projects_consumer_records(tmp_path):
    source = tmp_path / "script.txt"
    source.write_text((FIXTURES / "script_short.txt").read_text(encoding="utf-8"))
    result = compile_document(source)
    input_parameter = _artifact_parameter(result, "input")

    projected = project_analysis(
        result,
        [ParameterChange(parameter_id=input_parameter.id, value=["renamed-input.csv"])],
    )

    assert projected.resolved is result.resolved
    assert "ww_yield.csv" in {item.csv_path for item in result.analyzed.consumers}
    assert "renamed-input.csv" in {item.csv_path for item in projected.consumers}
    assert "ww_yield.csv" not in {item.csv_path for item in projected.consumers}


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

    baseline = project_workspace(
        [
            ("producer", producer, []),
            ("consumer", consumer, []),
        ]
    )
    assert not [issue for issue in baseline.issues if issue.code == "BROKEN_DEPENDENCY"]

    dirty = project_workspace(
        [
            (
                "producer",
                producer,
                [ParameterChange(parameter_id=producer_output.id, value="renamed.csv")],
            ),
            ("consumer", consumer, []),
        ]
    )

    broken = [issue for issue in dirty.issues if issue.code == "BROKEN_DEPENDENCY"]
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

    projected = project_workspace(
        [
            ("first", first, []),
            (
                "second",
                second,
                [ParameterChange(parameter_id=second_output.id, value="owner.csv")],
            ),
        ]
    )

    duplicates = [issue for issue in projected.issues if issue.code == "DUPLICATE_OUTPUT"]
    assert {issue.document_id for issue in duplicates} == {"first", "second"}
