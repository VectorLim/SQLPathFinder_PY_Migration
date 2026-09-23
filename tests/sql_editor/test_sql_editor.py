import pytest

from vg2c.sql_editor.models import SqlEditError
from vg2c.sql_editor.operations.filter import (
    AddFilterOperation,
    RemoveFilterOperation,
    UpdateFilterOperation,
)
from vg2c.sql_editor.operations.join import (
    AddJoinOperation,
    RemoveJoinOperation,
    UpdateJoinPredicateOperation,
    UpdateJoinSourceOperation,
    UpdateJoinTypeOperation,
)
from vg2c.sql_editor.operations.selection import (
    AddSelectionOperation,
    RemoveSelectionOperation,
    ReorderSelectionOperation,
    UpdateSelectionOperation,
)
from vg2c.sql_editor.operations.source import UpdateSourceOperation
from vg2c.sql_editor.parser import parse_sql

BASE = (
    "SELECT a AS x, b FROM table1 t "
    "LEFT JOIN table2 u ON t.id = u.id "
    "WHERE a = 1 AND b LIKE 'x%' ORDER BY a"
)


def test_parser_preserves_structured_sql_shape():
    model = parse_sql(BASE)

    assert model.capabilities.selected
    assert model.capabilities.filters
    assert model.capabilities.joins
    assert [(item.expression, item.alias) for item in model.selections] == [
        ("a", "x"),
        ("b", None),
    ]
    assert [item.expression for item in model.sources] == ["table1 t", "table2 u"]
    assert model.joins[0].join_type == "LEFT"
    assert (model.joins[0].predicates[0].left, model.joins[0].predicates[0].right) == (
        "t.id",
        "u.id",
    )
    assert [item.connector for item in model.filters] == [None, "AND"]


def test_column_labels_use_only_proven_identifiers_or_aliases():
    model = parse_sql(
        'SELECT t.[order id], "gross", COUNT(*) AS total, a + b '
        'FROM table1 t'
    )
    assert [item.display_label for item in model.selections] == [
        "order id", "gross", "total", "Expression 4"
    ]
    assert [item.editable for item in model.selections] == [True, True, True, False]
    assert "preserved as raw SQL" in model.selections[-1].read_only_reason
    assert model.selections[-1].raw == "a + b"


def test_parser_keeps_complex_queries_read_only():
    assert parse_sql("WITH q AS (SELECT 1) SELECT * FROM q").read_only_reason
    assert parse_sql("SELECT a FROM t UNION SELECT a FROM u").read_only_reason
    assert parse_sql("SELECT a FROM t; SELECT b FROM u").read_only_reason
    assert not parse_sql("SELECT /* keep */ a FROM t").selections[0].editable


def test_selection_operations_match_existing_editor_behavior():
    assert "a AS x, b, c" in AddSelectionOperation().apply(
        BASE,
        {"expression": "c"},
    ).sql
    assert "SELECT z AS zz, b" in UpdateSelectionOperation().apply(
        BASE,
        {"selection_id": "selection-0", "expression": "z", "alias": "zz"},
    ).sql
    assert RemoveSelectionOperation().apply(
        BASE,
        {"selection_id": "selection-0"},
    ).sql.startswith("SELECT b FROM")
    assert ReorderSelectionOperation().apply(
        BASE,
        {"selection_id": "selection-1", "target_index": 0},
    ).sql.startswith("SELECT b, a AS x")
    with pytest.raises(SqlEditError):
        RemoveSelectionOperation().apply(
            "SELECT a FROM t",
            {"selection_id": "selection-0"},
        )


def test_filter_operations_match_existing_editor_behavior():
    added = AddFilterOperation().apply(
        BASE,
        {"left": "c", "operator": ">=", "right": "2"},
    ).sql
    assert "AND c >= 2" in added
    assert "WHERE z = 1" in UpdateFilterOperation().apply(
        BASE,
        {"filter_id": "filter-0", "left": "z"},
    ).sql
    removed = RemoveFilterOperation().apply(
        BASE,
        {"filter_id": "filter-0"},
    ).sql
    assert "a = 1" not in removed
    assert "b LIKE 'x%'" in removed


def test_join_operations_match_existing_editor_behavior():
    assert "INNER JOIN table2 u" in UpdateJoinTypeOperation().apply(
        BASE,
        {"join_id": "join-0", "join_type": "INNER"},
    ).sql
    assert "LEFT JOIN table3 v" in UpdateJoinSourceOperation().apply(
        BASE,
        {"join_id": "join-0", "source": "table3 v"},
    ).sql
    assert "ON t.key = u.id" in UpdateJoinPredicateOperation().apply(
        BASE,
        {
            "join_id": "join-0",
            "predicate_id": "join-0-predicate-0",
            "left": "t.key",
        },
    ).sql
    assert "JOIN table2" not in RemoveJoinOperation().apply(
        BASE,
        {"join_id": "join-0"},
    ).sql
    assert "INNER JOIN extra e ON t.id = e.id" in AddJoinOperation().apply(
        "SELECT a FROM table1 t",
        {
            "join_type": "INNER",
            "source": "extra e",
            "left": "t.id",
            "right": "e.id",
        },
    ).sql


def test_from_source_update_is_structural_not_string_replacement():
    result = UpdateSourceOperation().apply(
        "SELECT a FROM table1 t",
        {"source_id": "source-from-0", "source": "table2 x"},
    )
    assert result.sql == "SELECT a FROM table2 x"
