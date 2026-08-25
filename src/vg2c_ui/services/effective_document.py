from __future__ import annotations

from vg2c_ui.domain.models import StepNode, WorkflowDocument
from vg2c_ui.domain.semantic_models import EffectiveDocumentInput

_SQL_KINDS = frozenset(("SQL_QUERY", "SQLITE_QUERY"))


class EffectiveDocumentService:
    """Build a non-persistent workflow view with pending parameter edits applied."""

    def build(self, inputs: EffectiveDocumentInput) -> WorkflowDocument:
        document = inputs.base_document.model_copy(deep=True)
        steps = {step.id: step for step in document.steps}
        for edit in inputs.pending_edits:
            step = steps.get(edit.step_id)
            if step is None:
                raise ValueError(f"Pending edit refers to unknown step {edit.step_id!r}.")
            parameter = next(
                (item for item in step.parameters if item.id == edit.parameter_id), None
            )
            if parameter is None:
                raise ValueError(
                    f"Pending edit refers to unknown parameter {edit.parameter_id!r}."
                )
            parameter.value = edit.value

        for step in document.steps:
            if step.functional_kind in _SQL_KINDS:
                _refresh_sql_files(step)
        return document


def _refresh_sql_files(step: StepNode) -> None:
    output = _parameter_value(step, "output")
    if isinstance(output, str) and output.strip():
        step.csv_outputs = [output.strip()]

    inputs = _parameter_value(step, "inputs")
    if isinstance(inputs, list) and all(isinstance(item, str) for item in inputs):
        step.csv_inputs = [item.strip() for item in inputs if item.strip()]


def _parameter_value(step: StepNode, name: str):
    parameter = next(
        (item for item in step.parameters if item.name.lower() == name), None
    )
    return parameter.value if parameter is not None else None
