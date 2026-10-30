import pytest

from vg2c.emitter.models import EmittedParameter, SourceRange, StepEmission
from vg2c.emitter.source_map import finalize_steps


def test_finalize_steps_preserves_metadata_and_locates_repeated_fragments_in_order():
    source = "prefix\nfirst()\ngap\nagain()\ngap\nagain()\nsuffix"
    parameter = EmittedParameter("input.csv", artifact_role=True)
    emissions = [
        StepEmission("first", 2, "FIRST", "first()", (parameter,)),
        StepEmission("repeat_one", 4, "REPEAT", "again()", ()),
        StepEmission("repeat_two", 7, "REPEAT", "again()", ()),
    ]

    steps = finalize_steps(source, emissions)

    assert [step.block_index for step in steps] == [2, 4, 7]
    assert [step.functional_kind for step in steps] == ["FIRST", "REPEAT", "REPEAT"]
    assert [step.parameters for step in steps] == [(parameter,), (), ()]
    assert [step.source_range for step in steps] == [
        SourceRange(7, 14), SourceRange(19, 26), SourceRange(31, 38),
    ]
    for step, fragment in zip(steps, ("first()", "again()", "again()"), strict=True):
        span = step.source_range
        assert source[span.start_offset:span.end_offset] == fragment


@pytest.mark.parametrize("source", ["unrelated()", "again()"])
def test_finalize_steps_reports_missing_fragment_without_reusing_prior_match(source):
    emissions = [
        StepEmission("repeat_one", 0, "REPEAT", "again()", ()),
        StepEmission("repeat_two", 1, "REPEAT", "again()", ()),
    ]
    missing = "repeat_one" if source == "unrelated()" else "repeat_two"
    with pytest.raises(ValueError, match=f"emitted step not found in final source: {missing}"):
        finalize_steps(source, emissions)
