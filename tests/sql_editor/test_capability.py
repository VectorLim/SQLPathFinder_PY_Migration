from pathlib import Path

from vg2c import compile_document
from vg2c.editing import project_changes
from vg2c.sql_editor import (
    SqlAction,
    apply_sql_action,
    parameter_capabilities,
    structured_sql_model,
)

FIXTURES = Path(__file__).parents[1] / "fixtures"


def _sql_parameter(result):
    return next(
        parameter
        for step in result.emitted.steps
        for invocation in step.invocations
        for parameter in invocation.parameters
        if "structured-sql" in parameter_capabilities(invocation, parameter)
    )


def test_compiler_capability_is_owned_by_actual_utility_parameter(tmp_path):
    source = tmp_path / "script.txt"
    source.write_text((FIXTURES / "script_short.txt").read_text(encoding="utf-8"))
    result = compile_document(source)
    sql = _sql_parameter(result)
    assert sql.name == "sql"
    model = structured_sql_model(result, sql.id)
    assert model.capabilities.selected
    assert model.selections
    assert model.sources


def test_sql_action_returns_normal_parameter_change_for_preview_apply_pipeline(tmp_path):
    source = tmp_path / "script.txt"
    source.write_text((FIXTURES / "script_short.txt").read_text(encoding="utf-8"))
    result = compile_document(source)
    sql = _sql_parameter(result)
    selection = structured_sql_model(result, sql.id).selections[0]
    change = apply_sql_action(
        result,
        SqlAction(
            parameter_id=sql.id,
            action="update-selection",
            arguments={
                "selection_id": selection.id,
                "expression": "a0.[owner] || '_x'",
            },
        ),
    )
    projection = project_changes(result, [change])
    assert projection.valid
    assert "owner] || '_x'" in change.value
    assert repr(change.value) in projection.source
