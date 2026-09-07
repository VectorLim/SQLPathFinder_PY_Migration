import pytest

from vg2c.sql_editor import (
    SqlEditError,
    add_filter,
    add_join,
    add_selection,
    move_selection,
    parse_sql,
    remove_filter,
    remove_join,
    remove_selection,
    reorder_selection,
    update_filter,
    update_join_predicate,
    update_join_source,
    update_join_type,
    update_selection,
    update_source,
)

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


def test_parser_keeps_complex_queries_read_only():
    assert parse_sql("WITH q AS (SELECT 1) SELECT * FROM q").read_only_reason
    assert parse_sql("SELECT a FROM t UNION SELECT a FROM u").read_only_reason
    assert parse_sql("SELECT a FROM t; SELECT b FROM u").read_only_reason
    assert not parse_sql("SELECT /* keep */ a FROM t").selections[0].editable


def test_selection_transforms_match_existing_editor_behavior():
    assert "a AS x, b, c" in add_selection(BASE, "c").sql
    assert "SELECT z AS zz, b" in update_selection(
        BASE, "selection-0", expression="z", alias="zz"
    ).sql
    assert remove_selection(BASE, "selection-0").sql.startswith("SELECT b FROM")
    assert move_selection(BASE, "selection-0", 1).sql.startswith("SELECT b, a AS x")
    assert reorder_selection(BASE, "selection-1", 0).sql.startswith("SELECT b, a AS x")
    with pytest.raises(SqlEditError):
        remove_selection("SELECT a FROM t", "selection-0")


def test_filter_transforms_match_existing_editor_behavior():
    added = add_filter(BASE, left="c", operator=">=", right="2").sql
    assert "AND c >= 2" in added
    assert "WHERE z = 1" in update_filter(BASE, "filter-0", left="z").sql
    removed = remove_filter(BASE, "filter-0").sql
    assert "a = 1" not in removed
    assert "b LIKE 'x%'" in removed


def test_join_transforms_match_existing_editor_behavior():
    assert "INNER JOIN table2 u" in update_join_type(BASE, "join-0", "INNER").sql
    assert "LEFT JOIN table3 v" in update_join_source(BASE, "join-0", "table3 v").sql
    assert "ON t.key = u.id" in update_join_predicate(
        BASE, "join-0", "join-0-predicate-0", left="t.key"
    ).sql
    assert "JOIN table2" not in remove_join(BASE, "join-0").sql
    assert "INNER JOIN extra e ON t.id = e.id" in add_join(
        "SELECT a FROM table1 t",
        join_type="INNER",
        source_expression="extra e",
        left="t.id",
        right="e.id",
    ).sql


def test_from_source_update_is_structural_not_string_replacement():
    result = update_source("SELECT a FROM table1 t", "source-from-0", "table2 x")
    assert result.sql == "SELECT a FROM table2 x"
