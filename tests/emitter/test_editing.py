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
