import pytest

from vg2c.emitter import STEPS_END, STEPS_START, WORKFLOW_END, WORKFLOW_START
from vg2c_ui.services.python_document import (
    InvalidGeneratedDocument,
    parse_generated_python,
)


def _document(step_body: str) -> str:
    return (
        f"{STEPS_START}\n"
        "def step_0001_external(ctx) -> None:\n"
        f"    {step_body}\n"
        f"{STEPS_END}\n"
        f"{WORKFLOW_START}\n"
        "def run() -> None:\n"
        "    pass\n"
        f"{WORKFLOW_END}\n"
    )


def test_parameter_spans_support_unicode_multiline_positional_and_keyword_values():
    source = _document("ctx.external.run(['héllo', '世界'], cwd='first\\nsecond')")

    parsed = parse_generated_python(source)
    parameters = parsed.steps["step_0001_external"].parameters

    assert [item.descriptor.id.rsplit(":", 1)[-1] for item in parameters] == [
        "0",
        "cwd",
    ]
    assert parameters[0].descriptor.editor_type == "list"
    assert parameters[1].descriptor.editor_type == "multiline"
    assert source[parameters[0].start_offset : parameters[0].end_offset] == "['héllo', '世界']"
    assert source[parameters[1].start_offset : parameters[1].end_offset] == "'first\\nsecond'"


def test_duplicate_generated_markers_are_rejected():
    source = _document("ctx.external.run(['ok'])")

    with pytest.raises(InvalidGeneratedDocument, match="exactly one"):
        parse_generated_python(f"{STEPS_START}\n{source}")
