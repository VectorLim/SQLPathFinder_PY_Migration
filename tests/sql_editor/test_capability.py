from pathlib import Path

import pytest

from vg2c import compile_document
from vg2c.editing import ParameterChange, project_changes
from vg2c.sql_editor import (
    SqlAction,
    SqlEditError,
    apply_sql_action,
    structured_sql_model,
)
from vg2c.sql_editor.parser import parse_sql
from vg2c.sql_editor.schema import SqlTableSchema, with_input_schemas

FIXTURES = Path(__file__).parents[1] / "fixtures"


def _sql_parameter(result):
    return next(
        parameter
        for step in result.emitted.steps
        for invocation in step.invocations
        for parameter in invocation.parameters
        if parameter.definition and "structured-sql" in parameter.definition.capabilities
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


def test_sql_action_returns_normal_parameter_change_for_preview_apply_pipeline(
    tmp_path,
):
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


def test_reset_then_inspect_and_apply_sql_action(tmp_path):
    source = tmp_path / "script.txt"
    source.write_text((FIXTURES / "script_short.txt").read_text(encoding="utf-8"))
    result = compile_document(source)
    sql = _sql_parameter(result)
    reset = [ParameterChange(sql.id, None, reset=True)]
    model = structured_sql_model(result, sql.id, reset)
    assert model.source == sql.value
    change = apply_sql_action(
        result,
        SqlAction(sql.id, "add-selection", {"expression": "42 AS answer"}),
        reset,
    )
    assert "42 AS answer" in change.value
    assert project_changes(result, [change]).valid


def test_sqlite_columns_require_table_header_evidence_and_use_opaque_action_ids(tmp_path):
    source = tmp_path / "script.txt"
    source.write_text((FIXTURES / "script_short.txt").read_text(encoding="utf-8"))
    result = compile_document(source)
    sql = _sql_parameter(result)
    assert not structured_sql_model(result, sql.id, csv_header=lambda _: None).column_choices

    def header(path):
        return ("owner", "other") if path == "ww_yield.csv" else None

    model = structured_sql_model(result, sql.id, csv_header=header)
    assert [choice.label for choice in model.column_choices] == ["a0.owner", "a0.other"]
    choice = model.column_choices[1]
    change = apply_sql_action(
        result,
        SqlAction(sql.id, "add-selection", {"column_choice_id": choice.id}),
        csv_header=header,
    )
    assert '"a0"."other"' in change.value
    with pytest.raises(SqlEditError, match="no longer available"):
        apply_sql_action(
            result,
            SqlAction(sql.id, "add-selection", {"column_choice_id": "stale"}),
            csv_header=header,
        )


def test_duplicate_column_names_are_source_qualified_and_remote_schema_unknown(tmp_path):
    model = with_input_schemas(
        parse_sql("SELECT a.id FROM accounts a JOIN bills b ON a.id = b.id"),
        (SqlTableSchema("accounts", ("id", "name")), SqlTableSchema("bills", ("id", "total"))),
    )
    assert [choice.label for choice in model.column_choices] == [
        "a.id", "a.name", "b.id", "b.total"
    ]
    pending = with_input_schemas(
        parse_sql("SELECT a.id FROM accounts a"),
        (SqlTableSchema("accounts", ("id",)), SqlTableSchema("bills", ("id", "total"))),
    )
    assert [table.label for table in pending.table_choices] == ["bills"]
    assert [choice.label for choice in pending.column_choices] == [
        "a.id", "bills.id", "bills.total"
    ]
    source = tmp_path / "remote.txt"
    source.write_text(
        "<OPTIONS>\n/NODE=<<<MARS>>>\n/OLEDB=SQLPlus\n/ENGINE=VA\n"
        "/CSV=out.csv\n/TABLE=orders.csv:orders\n/HEADERS=id,amount\n</OPTIONS>\n"
        "SELECT o.id FROM orders o\n<---- New Query ---->\n",
        encoding="utf-8",
    )
    result = compile_document(source)
    sql = _sql_parameter(result)
    assert not structured_sql_model(
        result, sql.id, csv_header=lambda _: ("id", "amount")
    ).column_choices


def test_join_action_resolves_table_and_column_choice_ids_in_core(tmp_path):
    source = tmp_path / "join.txt"
    source.write_text(
        "<OPTIONS>\n/OLEDB=SQLite\n/CSV=out.csv\n"
        "/TABLE=a.csv:accounts\n/TABLE=b.csv:bills\n</OPTIONS>\n"
        "SELECT a.id FROM accounts a\n<---- New Query ---->\n",
        encoding="utf-8",
    )
    result = compile_document(source)
    sql = _sql_parameter(result)
    model = structured_sql_model(
        result, sql.id, csv_header=lambda _: ("id", "name")
    )
    assert len(model.table_choices) == 1
    left = next(item for item in model.column_choices if item.label == "a.id")
    right = next(item for item in model.column_choices if item.label == "bills.id")
    change = apply_sql_action(
        result,
        SqlAction(
            sql.id,
            "add-join",
            {
                "join_type": "INNER",
                "table_choice_id": model.table_choices[0].id,
                "left_choice_id": left.id,
                "right_choice_id": right.id,
            },
        ),
        csv_header=lambda _: ("id", "name"),
    )
    assert 'JOIN "bills" ON "a"."id" = "bills"."id"' in change.value
