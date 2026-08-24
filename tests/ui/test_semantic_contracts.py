import json

import pytest

from vg2c_ui.domain.models import (
    ParameterDescriptor,
    SourceSpan,
    StepNode,
    UtilityDescriptor,
    WorkflowDocument,
)
from vg2c_ui.domain.semantic_models import (
    ClientWorkingState,
    OpenDocumentState,
    PendingEdit,
    SqlEntityRef,
    WorkflowEntityRef,
)
from vg2c_ui.services.working_state import (
    WorkingStateConflict,
    prepare_effective_document_input,
    validate_entity_ref_version,
)


def document() -> WorkflowDocument:
    parameter = ParameterDescriptor(
        id="query-sql",
        name="sql",
        source="fixture",
        value="SELECT a FROM foo",
        editor_type="multiline",
        editable=True,
    )
    step = StepNode(
        id="query",
        function_name="query",
        block_index=0,
        source_span=SourceSpan(start_line=1, end_line=1),
        functional_kind="SQL_QUERY",
        display_label="Query",
        icon_key="sql",
        description="",
        parameters=[parameter],
        read_only=False,
        utility=UtilityDescriptor(
            name="sql", class_name="Sql", module="fixture", title="SQL", description="fixture"
        ),
    )
    return WorkflowDocument(
        id="doc-1",
        source_path="workflow.txt",
        output_path="workflow.py",
        source_hash="source-1",
        output_hash="output-1",
        revision=4,
        steps=[step],
        scopes=[],
        artifacts=[],
        diagnostics=[],
    )


def round_trip(model):
    payload = json.loads(model.model_dump_json())
    return type(model).model_validate(payload)


def test_entity_refs_and_working_state_round_trip():
    entity = WorkflowEntityRef(
        document_id="doc-1",
        entity_kind="parameter",
        entity_id="query-sql",
        step_id="query",
        document_revision=4,
        output_hash="output-1",
    )
    sql_entity = SqlEntityRef(
        document_id="doc-1",
        step_id="query",
        sql_parameter_id="query-sql",
        entity_kind="filter",
        parsed_id="filter-0",
        fingerprint="a = 1",
        ordinal_hint=0,
        document_revision=4,
        output_hash="output-1",
    )
    working = ClientWorkingState(
        active_document_id="doc-1",
        selected_item_id="query",
        open_documents=[
            OpenDocumentState(
                document_id="doc-1", source_hash="source-1", output_hash="output-1", revision=4
            )
        ],
        pending_edits=[
            PendingEdit(
                document_id="doc-1",
                step_id="query",
                parameter_id="query-sql",
                value="SELECT b FROM foo",
            )
        ],
    )

    assert round_trip(entity) == entity
    assert round_trip(sql_entity) == sql_entity
    assert round_trip(working) == working
    assert entity.schema_version == sql_entity.schema_version == working.schema_version == 1
    assert ClientWorkingState.model_json_schema()["properties"]["schema_version"]["const"] == 1


def test_prepares_only_validated_effective_pending_edits():
    base = document()
    state = ClientWorkingState(
        open_documents=[
            OpenDocumentState(
                document_id=base.id,
                source_hash=base.source_hash,
                output_hash=base.output_hash,
                revision=base.revision,
            )
        ],
        pending_edits=[
            PendingEdit(
                document_id=base.id,
                step_id="query",
                parameter_id="query-sql",
                value="SELECT b FROM foo",
            ),
            PendingEdit(
                document_id="another-doc",
                step_id="other",
                parameter_id="other-value",
                value="ignored",
            ),
        ],
    )

    inputs = prepare_effective_document_input(base, state)

    assert round_trip(inputs) == inputs
    assert inputs.base_document == base
    assert [edit.parameter_id for edit in inputs.pending_edits] == ["query-sql"]
    assert inputs.pending_edits[0].value == "SELECT b FROM foo"


@pytest.mark.parametrize(
    ("field", "value", "code"),
    [
        ("source_hash", "old-source", "stale-source-hash"),
        ("output_hash", "old-output", "stale-output-hash"),
        ("revision", 3, "stale-revision"),
    ],
)
def test_rejects_stale_document_version(field, value, code):
    base = document()
    snapshot = OpenDocumentState(
        document_id=base.id,
        source_hash=base.source_hash,
        output_hash=base.output_hash,
        revision=base.revision,
    )
    state = ClientWorkingState(open_documents=[snapshot.model_copy(update={field: value})])

    with pytest.raises(WorkingStateConflict) as error:
        prepare_effective_document_input(base, state)

    assert error.value.code == code


def test_rejects_unknown_or_duplicate_pending_edit_targets():
    base = document()
    open_document = OpenDocumentState(
        document_id=base.id,
        source_hash=base.source_hash,
        output_hash=base.output_hash,
        revision=base.revision,
    )

    with pytest.raises(WorkingStateConflict, match="step") as unknown_step:
        prepare_effective_document_input(
            base,
            ClientWorkingState(
                open_documents=[open_document],
                pending_edits=[
                    PendingEdit(
                        document_id=base.id,
                        step_id="gone",
                        parameter_id="x",
                        value=1,
                    )
                ],
            ),
        )
    assert unknown_step.value.code == "unknown-step"

    edit = PendingEdit(document_id=base.id, step_id="query", parameter_id="query-sql", value="x")
    with pytest.raises(WorkingStateConflict) as duplicate:
        prepare_effective_document_input(
            base, ClientWorkingState(open_documents=[open_document], pending_edits=[edit, edit])
        )
    assert duplicate.value.code == "duplicate-pending-edit"


def test_entity_reference_version_validation():
    base = document()
    ref = SqlEntityRef(
        document_id=base.id,
        step_id="query",
        sql_parameter_id="query-sql",
        entity_kind="selection",
        parsed_id="selection-0",
        fingerprint="a",
        ordinal_hint=0,
        document_revision=base.revision,
        output_hash=base.output_hash,
    )

    validate_entity_ref_version(ref, base)

    with pytest.raises(WorkingStateConflict) as error:
        validate_entity_ref_version(ref.model_copy(update={"output_hash": "stale"}), base)
    assert error.value.code == "stale-output-hash"
