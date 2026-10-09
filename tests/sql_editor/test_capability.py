from pathlib import Path

import pytest

from vg2c import compile_document
from vg2c.editing import SemanticChange, project_changes
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
    assert not model.capabilities.selected
    assert "external editable asset" in model.read_only_reason
    assert model.selections
    assert model.sources


def test_sql_asset_edits_cannot_be_projected_into_python_offsets(tmp_path):
    source = tmp_path / "script.txt"
    source.write_text((FIXTURES / "script_short.txt").read_text())
    result = compile_document(source)
    sql = _sql_parameter(result)
    assert sql.source_range is None and not sql.editable
    path = next(p for p in result.emitted.steps[0].parameters if p.name == "path")
    assert "structured-sql" not in path.definition.capabilities
    with pytest.raises(SqlEditError, match="external editable asset"):
        apply_sql_action(result, SqlAction(sql.id, "add-selection", {"expression": "42 AS answer"}))
    projection = project_changes(result, [SemanticChange(sql.id, "SELECT 2")])
    assert not projection.valid and projection.source == result.emitted.source


def test_external_sql_reset_is_rejected_without_changing_assets(tmp_path):
    source = tmp_path / "script.txt"
    source.write_text((FIXTURES / "script_short.txt").read_text())
    result = compile_document(source)
    sql = _sql_parameter(result)
    assert structured_sql_model(result, sql.id).source == sql.value
    with pytest.raises(SqlEditError, match="external editable asset"):
        structured_sql_model(result, sql.id, [SemanticChange(sql.id, None, reset=True)])


def test_readonly_asset_inspection_does_not_offer_column_edit_actions(tmp_path):
    source = tmp_path / "script.txt"
    source.write_text((FIXTURES / "script_short.txt").read_text())
    result = compile_document(source)
    sql = _sql_parameter(result)
    model = structured_sql_model(result, sql.id, csv_header=lambda _: ("owner", "other"))
    assert model.selections and model.sources
    assert not model.column_choices and not model.capabilities.selected
    with pytest.raises(SqlEditError, match="external editable asset"):
        apply_sql_action(result, SqlAction(sql.id, "add-selection", {"column_choice_id": "stale"}))


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


def test_readonly_assets_reject_join_actions(tmp_path):
    source = tmp_path / "join.txt"
    source.write_text("<OPTIONS>\n/OLEDB=SQLite\n/CSV=out.csv\n/TABLE=a.csv:accounts\n</OPTIONS>\nSELECT a.id FROM accounts a\n")
    result = compile_document(source)
    sql = _sql_parameter(result)
    assert structured_sql_model(result, sql.id).sources[0].expression == "accounts a"
    with pytest.raises(SqlEditError, match="external editable asset"):
        apply_sql_action(result, SqlAction(sql.id, "add-join", {}))


def test_sql_csv_list_remains_in_external_asset_and_rejects_python_offset_edits(tmp_path):
    source = tmp_path / "file-list.txt"
    source.write_text("<OPTIONS>\n/OLEDB=SQLite\n/CSV=out.csv\n</OPTIONS>\nSELECT * FROM lots t WHERE t.lot IN SQL_Get_CSV_List('old.csv->500', lot, 't.lot In')\n")
    result = compile_document(source)
    sql = _sql_parameter(result)
    model = structured_sql_model(result, sql.id, file_choices=("new.csv",))
    assert "SQL_Get_CSV_List('old.csv->500'" in model.source
    assert not model.file_lists
    with pytest.raises(SqlEditError, match="external editable asset"):
        apply_sql_action(result, SqlAction(sql.id, "update-file-list", {"path": "new.csv"}), file_choices=("new.csv",))
    assert result.emitted.assets[0][1] == sql.value
