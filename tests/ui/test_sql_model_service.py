import json
import re
from pathlib import Path

import pytest

from vg2c_ui.domain.semantic_models import (
    JoinPatch,
    PredicatePatch,
    SelectionPatch,
    SqlEditableModel,
)
from vg2c_ui.services.sql_entity_resolver import SqlEntityResolver
from vg2c_ui.services.sql_model import SqlEditError, SqlModelService

FIXTURE = Path(__file__).parents[1] / "fixtures" / "sql_semantic_parity.v1.json"


def _snake_case(value: str) -> str:
    return re.sub(r"(?<!^)(?=[A-Z])", "_", value).lower()


def _snake_keys(value):
    if isinstance(value, dict):
        return {_snake_case(key): _snake_keys(item) for key, item in value.items()}
    if isinstance(value, list):
        return [_snake_keys(item) for item in value]
    return value


def _projection(model):
    return {
        "selections": [
            {"id": item.id, "expression": item.expression, "alias": item.alias}
            for item in model.selections
        ],
        "filters": [
            {
                "id": item.id,
                "left": item.left,
                "operator": item.operator,
                "right": item.right,
                "connector": item.connector,
            }
            for item in model.filters
        ],
        "joins": [
            {
                "id": item.id,
                "joinType": item.join_type,
                "source": item.source,
                "predicates": [
                    {
                        "id": predicate.id,
                        "left": predicate.left,
                        "operator": predicate.operator,
                        "right": predicate.right,
                        "connector": predicate.connector,
                    }
                    for predicate in item.predicates
                ],
            }
            for item in model.joins
        ],
        "sources": [
            {"id": item.id, "expression": item.expression, "kind": item.kind}
            for item in model.sources
        ],
        "capabilities": {
            "selected": model.capabilities.selected,
            "filters": model.capabilities.filters,
            "joins": model.capabilities.joins,
            "rawSql": model.capabilities.raw_sql,
        },
        "readOnlyReason": model.read_only_reason,
    }


def _run_transform(service, case):
    sql = case["sql"]
    args = case["args"]
    operation = case["operation"]
    if operation == "addSelection":
        return service.add_selection(sql, args[0])
    if operation == "updateSelection":
        return service.update_selection(sql, args[0], SelectionPatch(**args[1]))
    if operation == "removeSelection":
        return service.remove_selection(sql, args[0])
    if operation == "reorderSelection":
        return service.reorder_selection(sql, args[0], args[1])
    if operation == "addFilter":
        return service.add_filter(sql, PredicatePatch(**args[0]))
    if operation == "updateFilter":
        return service.update_filter(sql, args[0], PredicatePatch(**args[1]))
    if operation == "removeFilter":
        return service.remove_filter(sql, args[0])
    if operation == "addJoin":
        values = args[0]
        predicate_values = {
            key: values[key] for key in ("left", "operator", "right") if key in values
        }
        return service.add_join(
            sql,
            JoinPatch(join_type=values["joinType"], source=values["source"]),
            PredicatePatch(**predicate_values),
        )
    if operation == "updateJoinType":
        return service.update_join(sql, args[0], JoinPatch(join_type=args[1]))
    if operation == "updateJoinSource":
        return service.update_join(sql, args[0], JoinPatch(source=args[1]))
    if operation == "updateJoinPredicate":
        return service.update_join_predicate(sql, args[1], PredicatePatch(**args[2]))
    if operation == "removeJoin":
        return service.remove_join(sql, args[0])
    if operation == "updateSource":
        return service.update_source(sql, args[0], args[1])
    raise AssertionError(f"Unhandled fixture operation {operation}")


def test_backend_parser_matches_session_1_golden_fixture():
    service = SqlModelService()
    fixture = json.loads(FIXTURE.read_text(encoding="utf-8"))

    for case in fixture["parser_cases"]:
        expected = SqlEditableModel.model_validate(_snake_keys(case["expected"]))
        assert service.parse(case["sql"]) == expected, case["name"]


def test_backend_transforms_match_session_1_golden_fixture():
    service = SqlModelService()
    fixture = json.loads(FIXTURE.read_text(encoding="utf-8"))

    for case in fixture["transform_cases"]:
        result = _run_transform(service, case)
        assert result.sql == case["expected"]["sql"], case["name"]
        assert _projection(result.model) == case["expected"]["model"], case["name"]


@pytest.mark.parametrize(
    ("sql", "reason"),
    [
        ("", "SQL is empty."),
        (
            "WITH c AS (SELECT a FROM foo) SELECT a FROM c",
            "CTEs are preserved as raw SQL until a CTE-aware structural editor is available.",
        ),
        (
            "SELECT a FROM foo INTERSECT SELECT a FROM bar",
            "UNION, INTERSECT, and EXCEPT queries are preserved as raw SQL.",
        ),
        (
            "SELECT a FROM foo; SELECT b FROM bar",
            (
                "Multiple SELECT statements are preserved raw because the editable target "
                "is ambiguous."
            ),
        ),
    ],
)
def test_unsupported_statements_remain_read_only(sql, reason):
    model = SqlModelService().parse(sql)
    assert model.read_only_reason == reason
    assert not model.capabilities.selected


def test_comments_make_only_the_affected_rows_read_only():
    service = SqlModelService()
    selection = service.parse("SELECT a, b /* keep */ + c FROM foo")
    predicate = service.parse("SELECT a FROM foo WHERE a /* keep */ = 1")
    source = service.parse("SELECT a FROM foo /* keep */ f")

    assert selection.selections[0].editable
    assert not selection.selections[1].editable
    assert not predicate.filters[0].editable
    assert not source.sources[0].editable


def test_remaining_typescript_transforms_have_backend_parity():
    service = SqlModelService()
    sql = "SELECT a, b, c FROM foo"
    moved = service.move_selection(sql, "selection-1", 1)
    assert moved.sql == "SELECT a, c, b FROM foo"

    join_sql = "SELECT a FROM x LEFT JOIN y ON x.a = y.a AND x.b = y.b"
    removed = service.remove_join_predicate(join_sql, "join-0-predicate-0")
    assert removed.sql == "SELECT a FROM x LEFT JOIN y ON  x.b = y.b"


def test_patch_omission_and_explicit_null_are_distinct():
    service = SqlModelService()
    selection_sql = "SELECT a AS original FROM foo"

    preserved = service.update_selection(
        selection_sql, "selection-0", SelectionPatch(expression="b")
    )
    cleared = service.update_selection(
        selection_sql, "selection-0", SelectionPatch(alias=None)
    )

    assert preserved.sql == "SELECT b AS original FROM foo"
    assert cleared.sql == "SELECT a FROM foo"
    assert "alias" not in SelectionPatch().model_fields_set
    assert "alias" in SelectionPatch(alias=None).model_fields_set

    filter_sql = "SELECT a FROM foo WHERE a = 1 OR b = 2"
    connector_preserved = service.update_filter(
        filter_sql, "filter-1", PredicatePatch(right="3")
    )
    connector_cleared = service.update_filter(
        filter_sql, "filter-1", PredicatePatch(connector=None)
    )

    assert connector_preserved.sql == "SELECT a FROM foo WHERE a = 1 OR b = 3"
    assert connector_cleared.sql == "SELECT a FROM foo WHERE a = 1 AND b = 2"
    assert "connector" not in PredicatePatch().model_fields_set
    assert "connector" in PredicatePatch(connector=None).model_fields_set


def test_transform_validation_preserves_read_only_guards():
    service = SqlModelService()
    sql = "SELECT a, b /* keep */ + c FROM foo"

    with pytest.raises(SqlEditError, match="comments"):
        service.update_selection(sql, "selection-1", SelectionPatch(expression="c"))


def test_sql_model_mutations_reject_unresolved_stable_refs():
    service = SqlModelService()
    resolver = SqlEntityResolver()
    sql = "SELECT customer_id FROM customer"
    ref = resolver.make_ref(
        service.parse(sql),
        document_id="doc",
        step_id="step-a",
        sql_parameter_id="sql-a",
        entity_kind="selection",
        parsed_id="selection-0",
        document_revision=1,
        output_hash="output-1",
    )

    with pytest.raises(TypeError, match="parser-local entity IDs"):
        service.update_selection(sql, ref, SelectionPatch(expression="customer_number"))

    assert service.parse(sql).selections[0].expression == "customer_id"
