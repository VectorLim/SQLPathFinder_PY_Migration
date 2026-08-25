from vg2c_ui.domain.models import (
    ParameterDescriptor,
    SourceSpan,
    StepNode,
    UtilityDescriptor,
    WorkflowDocument,
)
from vg2c_ui.domain.semantic_models import EffectiveDocumentInput, PendingEdit
from vg2c_ui.services.effective_document import EffectiveDocumentService


def _parameter(parameter_id, name, value, editor_type="string"):
    return ParameterDescriptor(
        id=parameter_id,
        name=name,
        source="fixture",
        value=value,
        editor_type=editor_type,
        editable=True,
    )


def _document():
    utility = UtilityDescriptor(
        name="sql",
        class_name="Sql",
        module="fixture",
        title="SQL",
        description="fixture",
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
        parameters=[
            _parameter("query-sql", "sql", "SELECT a FROM foo", "multiline"),
            _parameter("query-inputs", "inputs", ["old-in.csv"], "list"),
            _parameter("query-output", "output", "old-out.csv"),
        ],
        csv_inputs=["old-in.csv"],
        csv_outputs=["old-out.csv"],
        read_only=False,
        utility=utility,
    )
    return WorkflowDocument(
        id="doc",
        source_path="workflow.txt",
        output_path="workflow.py",
        source_hash="source",
        output_hash="output",
        revision=2,
        steps=[step],
        scopes=[],
        artifacts=[],
        diagnostics=[],
    )


def test_build_applies_pending_values_without_mutating_base_document():
    base = _document()
    effective = EffectiveDocumentService().build(
        EffectiveDocumentInput(
            base_document=base,
            pending_edits=[
                PendingEdit(
                    document_id=base.id,
                    step_id="query",
                    parameter_id="query-sql",
                    value="SELECT b FROM foo",
                ),
                PendingEdit(
                    document_id=base.id,
                    step_id="query",
                    parameter_id="query-inputs",
                    value=[" new-in.csv ", "second.csv"],
                ),
                PendingEdit(
                    document_id=base.id,
                    step_id="query",
                    parameter_id="query-output",
                    value=" new-out.csv ",
                ),
            ],
        )
    )

    values = {parameter.id: parameter.value for parameter in effective.steps[0].parameters}
    assert values["query-sql"] == "SELECT b FROM foo"
    assert values["query-inputs"] == [" new-in.csv ", "second.csv"]
    assert values["query-output"] == " new-out.csv "
    assert effective.steps[0].csv_inputs == ["new-in.csv", "second.csv"]
    assert effective.steps[0].csv_outputs == ["new-out.csv"]

    base_values = {parameter.id: parameter.value for parameter in base.steps[0].parameters}
    assert base_values["query-sql"] == "SELECT a FROM foo"
    assert base.steps[0].csv_inputs == ["old-in.csv"]
    assert base.steps[0].csv_outputs == ["old-out.csv"]


def test_explicit_null_is_an_effective_value_but_invalid_file_values_fall_back():
    base = _document()
    effective = EffectiveDocumentService().build(
        EffectiveDocumentInput(
            base_document=base,
            pending_edits=[
                PendingEdit(
                    document_id=base.id,
                    step_id="query",
                    parameter_id="query-sql",
                    value=None,
                ),
                PendingEdit(
                    document_id=base.id,
                    step_id="query",
                    parameter_id="query-output",
                    value=None,
                ),
            ],
        )
    )

    values = {parameter.id: parameter.value for parameter in effective.steps[0].parameters}
    assert values["query-sql"] is None
    assert values["query-output"] is None
    assert effective.steps[0].csv_outputs == ["old-out.csv"]


def test_empty_input_list_is_preserved_as_an_explicit_effective_value():
    base = _document()
    effective = EffectiveDocumentService().build(
        EffectiveDocumentInput(
            base_document=base,
            pending_edits=[
                PendingEdit(
                    document_id=base.id,
                    step_id="query",
                    parameter_id="query-inputs",
                    value=[],
                )
            ],
        )
    )
    assert effective.steps[0].csv_inputs == []
