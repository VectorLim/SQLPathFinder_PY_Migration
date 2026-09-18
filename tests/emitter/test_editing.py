import ast
from dataclasses import replace
from pathlib import Path

import pytest

from vg2c import compile_document
from vg2c.editing import (
    ChangeValidationError,
    ParameterChange,
    apply_changes,
    preview_changes,
    project_changes,
)
from vg2c.utility_metadata import ValueSchema

FIXTURES = Path(__file__).parents[1] / "fixtures"


def _parameter(result, name: str):
    return next(
        parameter
        for step in result.emitted.steps
        for parameter in step.parameters
        if parameter.name == name
    )


def test_parameter_change_projects_from_compiler_manifest(tmp_path):
    source = tmp_path / "script.txt"
    source.write_text((FIXTURES / "script_short.txt").read_text(encoding="utf-8"))
    result = compile_document(source)
    output = _parameter(result, "output")

    projection = project_changes(
        result,
        [ParameterChange(parameter_id=output.id, value="renamed.csv")],
    )

    assert projection.valid
    assert "renamed.csv" in projection.source
    assert "renamed.csv" not in result.emitted.source


def test_preview_is_side_effect_free_and_reports_diff(tmp_path):
    source = tmp_path / "script.txt"
    source.write_text((FIXTURES / "script_short.txt").read_text(encoding="utf-8"))
    result = compile_document(source)
    output = _parameter(result, "output")

    preview = preview_changes(
        result,
        [ParameterChange(parameter_id=output.id, value="renamed.csv")],
    )

    assert preview.valid
    assert "-" in preview.diff and "+" in preview.diff
    assert result.emitted.source == result.emitted.source


def test_invalid_change_is_rejected_by_core(tmp_path):
    source = tmp_path / "script.txt"
    source.write_text((FIXTURES / "script_short.txt").read_text(encoding="utf-8"))
    result = compile_document(source)
    output = _parameter(result, "output")

    projection = project_changes(
        result,
        [ParameterChange(parameter_id=output.id, value=123)],
    )

    assert not projection.valid
    assert projection.issues[0].code == "invalid-type"
    with pytest.raises(ChangeValidationError):
        apply_changes(result, [ParameterChange(parameter_id=output.id, value=123)])


def test_omitted_default_and_supplied_value_rebuild_one_call(tmp_path):
    source = tmp_path / "script.txt"
    source.write_text((FIXTURES / "script_short.txt").read_text(encoding="utf-8"))
    result = compile_document(source)
    node = _parameter(result, "node")
    output = _parameter(result, "output")
    assert node.source_range is None and node.value is None
    projection = project_changes(
        result,
        [ParameterChange(node.id, "TEST"), ParameterChange(output.id, "changed.csv")],
    )
    assert projection.valid
    tree = ast.parse(projection.source)
    step = next(
        item
        for item in tree.body
        if isinstance(item, ast.FunctionDef)
        and item.name == result.emitted.steps[0].function_name
    )
    call = next(
        item
        for item in ast.walk(step)
        if isinstance(item, ast.Call)
        and isinstance(item.func, ast.Attribute)
        and item.func.attr == "run_query"
    )
    keywords = {item.arg: item.value for item in call.keywords}
    assert ast.literal_eval(keywords["node"]) == "TEST"
    assert ast.literal_eval(keywords["output"]) == "changed.csv"
    assert isinstance(keywords["reader"], ast.Call)
    assert project_changes(result, []).source == result.emitted.source
    assert not project_changes(result, [ParameterChange(node.id, True)]).valid


def test_shared_value_must_satisfy_every_binding(tmp_path):
    source = tmp_path / "script.txt"
    source.write_text((FIXTURES / "script_short.txt").read_text(encoding="utf-8"))
    result = compile_document(source)
    step = result.emitted.steps[0]
    invocation = step.invocations[0]
    output = _parameter(result, "output")
    restricted = replace(
        output,
        definition=replace(
            output.definition, schema=ValueSchema("string", choices=("only.csv",))
        ),
    )
    invocation = replace(invocation, parameters=(restricted, output))
    result = replace(
        result,
        emitted=replace(
            result.emitted, steps=(replace(step, invocations=(invocation,)),)
        ),
    )
    assert not project_changes(result, [ParameterChange(output.id, "other.csv")]).valid


def test_table_binding_json_edits_preserve_runtime_tuple_shape(tmp_path):
    source = tmp_path / "binding.txt"
    source.write_text(
        "<OPTIONS>\n/OLEDB=SQLite\n/TABLE=input.csv:records\n/CSV=out.csv\n</OPTIONS>\nSELECT 1\n"
    )
    result = compile_document(source)
    parameter = _parameter(result, "inputs")
    assert parameter.editable and parameter.value == [("input.csv", "records")]
    projected = project_changes(
        result, [ParameterChange(parameter.id, [["changed.csv", "renamed"]])]
    )
    assert projected.valid and "[('changed.csv', 'renamed')]" in projected.source
    assert not project_changes(
        result, [ParameterChange(parameter.id, [["changed.csv"]])]
    ).valid
