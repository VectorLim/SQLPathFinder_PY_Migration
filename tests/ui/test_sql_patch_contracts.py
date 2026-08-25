import pytest
from pydantic import BaseModel, ValidationError

from vg2c_ui.domain.semantic_models import JoinPatch, PredicatePatch, SelectionPatch


@pytest.mark.parametrize(
    ("patch_type", "field"),
    [
        (SelectionPatch, "expression"),
        (PredicatePatch, "left"),
        (PredicatePatch, "operator"),
        (PredicatePatch, "right"),
        (JoinPatch, "join_type"),
        (JoinPatch, "source"),
    ],
)
def test_non_clearable_patch_fields_reject_explicit_null(
    patch_type: type[BaseModel], field: str
):
    with pytest.raises(ValidationError):
        patch_type.model_validate({field: None})

    schema = patch_type.model_json_schema()["properties"][field]
    assert schema.get("type") == "string"
    assert "anyOf" not in schema
    assert "default" not in schema


def test_clearable_patch_fields_keep_explicit_null_meaning():
    alias = SelectionPatch(alias=None)
    connector = PredicatePatch(connector=None)

    assert alias.alias is None
    assert connector.connector is None
    assert "alias" in alias.model_fields_set
    assert "connector" in connector.model_fields_set
    assert alias.model_dump() == {"alias": None}
    assert connector.model_dump() == {"connector": None}


def test_omitted_patch_fields_serialize_as_omitted():
    selection = SelectionPatch()
    predicate = PredicatePatch()
    join = JoinPatch()

    assert selection.model_fields_set == set()
    assert predicate.model_fields_set == set()
    assert join.model_fields_set == set()
    assert selection.model_dump() == {}
    assert predicate.model_dump() == {}
    assert join.model_dump() == {}
    assert selection.model_dump_json() == "{}"
    assert predicate.model_dump_json() == "{}"
    assert join.model_dump_json() == "{}"


def test_supplied_patch_fields_round_trip_without_materializing_omitted_fields():
    selection = SelectionPatch(expression="customer_id")
    predicate = PredicatePatch(left="a.id", right="b.id")
    join = JoinPatch(source="customer c")

    assert selection.model_dump() == {"expression": "customer_id"}
    assert predicate.model_dump() == {"left": "a.id", "right": "b.id"}
    assert join.model_dump() == {"source": "customer c"}

    assert SelectionPatch.model_validate_json(selection.model_dump_json()).model_dump() == {
        "expression": "customer_id"
    }
    assert PredicatePatch.model_validate_json(predicate.model_dump_json()).model_dump() == {
        "left": "a.id",
        "right": "b.id",
    }
    assert JoinPatch.model_validate_json(join.model_dump_json()).model_dump() == {
        "source": "customer c"
    }


def test_patch_serialization_remains_sparse_when_nested_in_api_models():
    class PatchEnvelope(BaseModel):
        patch: SelectionPatch

    omitted = PatchEnvelope(patch=SelectionPatch())
    supplied = PatchEnvelope(patch=SelectionPatch(alias=None))

    assert omitted.model_dump() == {"patch": {}}
    assert omitted.model_dump_json() == '{"patch":{}}'
    assert supplied.model_dump() == {"patch": {"alias": None}}
