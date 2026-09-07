from pathlib import Path
from typing import Literal

import pytest

from vg2c import compile_document
from vg2c.emitter.models import (
    ArtifactRole,
    CodeExpr,
    build_step_emission,
    emittable,
    finalize_steps,
)
from vg2c.utilities import ensure_utility_checks_loaded
from vg2c.utilities._base import UtilitySpec
from vg2c.utilities.pipeline_context import PipelineContext
from vg2c.utilities.wait_file import WaitFile

FIXTURES = Path(__file__).parents[1] / "fixtures"


class MetadataProbeUtility(UtilitySpec):
    """Test-only utility proving registry-to-editor metadata needs no UI catalog."""

    utility_name = "test_metadata_probe"

    @emittable(
        capabilities=("probe-capability",),
        parameter_capabilities={"source": ("browse-path",)},
        artifact_roles={
            "source": ArtifactRole("input"),
            "output": ArtifactRole("output"),
        },
        supported_mutations=("set-parameter", "probe-mutation"),
    )
    def transform(
        self,
        source: str,
        output: str,
        mode: Literal["fast", "safe"] = "safe",
        retries: int = 1,
    ) -> None:
        pass


def test_parameterized_emittable_metadata_is_stripped_from_embedded_source():
    source = PipelineContext.get_source()

    assert "@emittable" not in source
    assert "parameter_capabilities=" not in source
    compile(source, "<pipeline-context>", "exec")


def test_emitter_records_owned_invocation_parameters_and_spans(tmp_path):
    source = tmp_path / "script.txt"
    source.write_text((FIXTURES / "script_short.txt").read_text(encoding="utf-8"))

    result = compile_document(source)

    assert len(result.emitted.steps) == 1
    step = result.emitted.steps[0]
    assert step.block_index == 0
    invocation = next(item for item in step.invocations if item.operation.id == "ctx.run_query")
    parameters = {item.name: item for item in invocation.parameters}

    assert parameters["output"].value == "owner.csv"
    assert parameters["output"].artifact_role is not None
    assert parameters["output"].artifact_role.direction == "output"
    assert parameters["inputs"].value == ["ww_yield.csv"]
    assert parameters["inputs"].artifact_role is not None
    assert parameters["inputs"].artifact_role.direction == "input"
    assert parameters["reader"].editable is False

    for parameter in invocation.parameters:
        span = parameter.source_range
        assert result.emitted.source[span.start_offset : span.end_offset] == parameter.source


def test_parameter_identity_ignores_unrelated_call_order():
    ensure_utility_checks_loaded()
    target = PipelineContext.write_file.render("a.txt", "body")
    unrelated = WaitFile.poll.render("flag.txt", 10)

    base = build_step_emission(
        function_name="step_0001_example",
        block_index=1,
        functional_kind="EXAMPLE",
        body_lines=[target],
    )
    with_unrelated = build_step_emission(
        function_name="step_0001_example",
        block_index=1,
        functional_kind="EXAMPLE",
        body_lines=[unrelated, target],
    )

    base_step = finalize_steps(base.source, [base])[0]
    changed_step = finalize_steps(with_unrelated.source, [with_unrelated])[0]
    base_parameter_ids = {
        parameter.name: parameter.id
        for invocation in base_step.invocations
        if invocation.operation.id == "ctx.write_file"
        for parameter in invocation.parameters
    }
    changed_parameter_ids = {
        parameter.name: parameter.id
        for invocation in changed_step.invocations
        if invocation.operation.id == "ctx.write_file"
        for parameter in invocation.parameters
    }

    assert base_parameter_ids == changed_parameter_ids
    assert all(
        identifier.startswith("block-1:ctx.write_file:default:")
        for identifier in base_parameter_ids.values()
    )


def test_same_operation_requires_distinct_semantic_keys_and_is_order_stable():
    first = PipelineContext.write_file.render("a.txt", "first").with_key("primary")
    second = PipelineContext.write_file.render("b.txt", "second").with_key("secondary")

    forward = build_step_emission(
        function_name="step_0002_example",
        block_index=2,
        functional_kind="EXAMPLE",
        body_lines=[first, second],
    )
    reversed_step = build_step_emission(
        function_name="step_0002_example",
        block_index=2,
        functional_kind="EXAMPLE",
        body_lines=[second, first],
    )

    forward_ids = {item.id for item in finalize_steps(forward.source, [forward])[0].invocations}
    reverse_ids = {
        item.id for item in finalize_steps(reversed_step.source, [reversed_step])[0].invocations
    }
    assert (
        forward_ids
        == reverse_ids
        == {
            "block-2:ctx.write_file:primary",
            "block-2:ctx.write_file:secondary",
        }
    )

    ambiguous = build_step_emission(
        function_name="step_0002_example",
        block_index=2,
        functional_kind="EXAMPLE",
        body_lines=[
            PipelineContext.write_file.render("a.txt", "first"),
            PipelineContext.write_file.render("b.txt", "second"),
        ],
    )
    with pytest.raises(ValueError, match="ambiguous emitted invocation identity"):
        finalize_steps(ambiguous.source, [ambiguous])


def test_code_expr_is_explicit_and_never_reparsed_for_editability():
    literal = PipelineContext.write_file.render("a.txt", "body")
    literal_step = build_step_emission(
        function_name="step_0007_literal",
        block_index=7,
        functional_kind="TEST",
        body_lines=[literal],
    )
    literal_parameter = finalize_steps(literal_step.source, [literal_step])[0].parameters[0]
    assert literal_parameter.value == "a.txt"
    assert literal_parameter.editable is True

    dynamic = PipelineContext.write_file.render(CodeExpr("ctx.macro.named('PATH')"), "body")
    dynamic_step = build_step_emission(
        function_name="step_0008_dynamic",
        block_index=8,
        functional_kind="TEST",
        body_lines=[dynamic],
    )
    dynamic_parameter = finalize_steps(dynamic_step.source, [dynamic_step])[0].parameters[0]
    assert dynamic_parameter.source == "ctx.macro.named('PATH')"
    assert dynamic_parameter.editable is False
    assert dynamic_parameter.read_only_reason == "Dynamic Python expressions are read-only"


def test_utility_catalog_is_derived_from_registered_emittable_methods():
    ensure_utility_checks_loaded()
    definitions = {item.id: item for item in PipelineContext.operation_definitions()}

    run_query = definitions["ctx.run_query"]
    assert [parameter.name for parameter in run_query.parameters][:4] == [
        "sql",
        "output",
        "reader",
        "inputs",
    ]
    assert run_query.artifact_role("output").direction == "output"
    assert run_query.capabilities_for_parameter("sql") == ("structured-sql",)

    write_file = definitions["ctx.write_file"]
    assert write_file.capabilities == ()
    assert write_file.parameter_capabilities == ()
    assert write_file.artifact_roles == ()
    assert write_file.supported_mutations == ("set-parameter",)


def test_new_ordinary_utility_flows_from_registry_to_generic_parameter_metadata():
    definition = UtilitySpec.operation_definition("test_metadata_probe", "transform")
    assert definition is not None
    assert definition.parameter("mode").choices == ("fast", "safe")
    assert definition.parameter("retries").default == 1
    assert definition.artifact_role("source") == ArtifactRole("input")
    assert definition.artifact_role("output") == ArtifactRole("output")
    assert definition.capabilities_for_parameter("source") == ("browse-path",)
    assert definition.capabilities == ("probe-capability",)
    assert definition.supported_mutations == ("set-parameter", "probe-mutation")

    rendered = MetadataProbeUtility.transform.render(
        "input.csv",
        "output.csv",
        "fast",
        3,
    )
    step = build_step_emission(
        function_name="step_0009_probe",
        block_index=9,
        functional_kind="TEST",
        body_lines=[rendered],
    )
    invocation = finalize_steps(step.source, [step])[0].invocations[0]
    parameters = {item.name: item for item in invocation.parameters}

    assert invocation.operation == definition
    assert parameters["source"].editor_type == "string"
    assert parameters["source"].artifact_role == ArtifactRole("input")
    assert parameters["output"].artifact_role == ArtifactRole("output")
    assert parameters["mode"].definition.choices == ("fast", "safe")
    assert parameters["retries"].editor_type == "integer"
