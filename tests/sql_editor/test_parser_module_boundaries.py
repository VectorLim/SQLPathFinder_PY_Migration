"""Cross-module contracts for the structured SQL parser refactor."""

from vg2c.sql_editor.parser import parse_sql


def test_between_keeps_internal_and_inside_one_predicate():
    model = parse_sql("SELECT a FROM t WHERE x BETWEEN 1 AND 3 AND y = 4")
    assert [item.raw for item in model.filters] == ["x BETWEEN 1 AND 3", "y = 4"]
    assert model.filters[1].connector == "AND"


def test_or_join_predicates_remain_read_only():
    model = parse_sql("SELECT a FROM t JOIN u ON t.id=u.id OR t.x=u.x")
    assert len(model.joins[0].predicates) == 2
    assert all(not item.editable for item in model.joins[0].predicates)


def test_join_source_comment_does_not_hide_safe_on_predicate():
    model = parse_sql("SELECT a FROM t JOIN /*keep*/ u ON t.id=u.id")
    assert not model.joins[0].editable_source
    assert model.joins[0].predicates[0].editable


def test_natural_join_keeps_type_and_source_read_only():
    model = parse_sql("SELECT a FROM t NATURAL JOIN u")
    assert model.joins[0].join_type == "NATURAL"
    assert not model.joins[0].editable_type
    assert not model.joins[0].editable_source
