"""Unsupported blocks warn and skip without interrupting supported steps."""

from __future__ import annotations

import pytest

from tests.fixtures.generated_job import capture_jobs
from vg2c import compile_document, translate
from vg2c.emitter.models import BlockEmission
from vg2c.kind import Kind
from vg2c.utilities import ensure_utility_checks_loaded
from vg2c.utilities._base import UtilitySpec


def block(options, body=""):
    return f"<OPTIONS>\n{options}\n</OPTIONS>\n{body}\n<---- New Query ---->\n"


@pytest.mark.parametrize("options, reason", [
    ("/UTILITIES=not_a_supported_utility", "Unclassified utility"),
    ("/JSL=Y", "JMP/JSL"),
])
def test_unsupported_block_is_skipped_between_supported_steps(tmp_path, options, reason):
    source = tmp_path / "source.txt"
    source.write_text(
        block("/WRITE-FILE=Y\n/CSV=before.txt", "before")
        + block(options, "unsupported source must not execute")
        + block("/WRITE-FILE=Y\n/CSV=after.txt", "after"),
        encoding="utf-8",
    )
    result = compile_document(source)
    warnings = [item for item in result.diagnostics if item.code == "skipped-block"]
    assert len(warnings) == 1
    assert warnings[0].level == "warning"
    assert warnings[0].location.startswith(str(source))
    assert reason in warnings[0].message
    assert "unsupported source must not execute" not in result.emitted.source
    assert result.emitted.steps[1].functional_kind == "UNSUPPORTED"
    expected = (
        "job.load_file(path='before.txt', asset='files/before.txt')",
        "pass  # Skipped unsupported query block.",
        "job.load_file(path='after.txt', asset='files/after.txt')",
    )
    previous_end = 0
    for step, statement in zip(result.emitted.steps, expected, strict=True):
        span = step.source_range
        assert previous_end <= span.start_offset < span.end_offset
        assert result.emitted.source[span.start_offset:span.end_offset].strip() == statement
        previous_end = span.end_offset

    main = translate(source)
    namespace = {"__file__": str(main), "__name__": "generated_job"}
    exec(compile(main.read_text(), str(main), "exec"), namespace)
    capture_jobs(namespace)
    namespace["run"]()
    assert namespace["_job"].read_file("before.txt").strip() == "before"
    assert namespace["_job"].read_file("after.txt").strip() == "after"


@pytest.mark.parametrize("scope", ["", "if", "loop"])
def test_skipped_only_scope_remains_valid_python(tmp_path, scope):
    text = block("/JSL=Y", "unsupported source")
    if scope == "if":
        text = (
            block('/UTILITIES={IF-THEN} "x" "EQS" "x"')
            + text + block('/UTILITIES={ELSE}') + text
            + block('/UTILITIES={END-IF}')
        )
    elif scope == "loop":
        text = block('/UTILITIES={FOR-LOOP} "0" "1" "1" "A" "N"') + text
        text += block('/UTILITIES={END-LOOP}')
    source = tmp_path / "scope.txt"
    source.write_text(text, encoding="utf-8")
    result = compile_document(source)
    compile(result.emitted.source, str(source), "exec")
    assert "pass  # Skipped unsupported query block." in result.emitted.source


@pytest.mark.parametrize("missing_handler", [True, False])
def test_missing_handler_or_empty_emission_is_skipped(tmp_path, monkeypatch, missing_handler):
    ensure_utility_checks_loaded()
    if missing_handler:
        monkeypatch.delitem(UtilitySpec._emit_handlers, Kind.WRITE_FILE)
    else:
        handler = UtilitySpec._emit_handlers[Kind.WRITE_FILE]
        monkeypatch.setattr(handler, "emit_block", lambda *args, **kwargs: BlockEmission(lines=()))
    source = tmp_path / "source.txt"
    source.write_text(block("/WRITE-FILE=Y\n/CSV=ignored.txt", "ignored"))
    result = compile_document(source)
    assert any(item.code == "skipped-block" for item in result.diagnostics)
    assert result.emitted.steps[0].functional_kind == "UNSUPPORTED"
    assert not result.emitted.assets
    assert "job.write_file" not in result.emitted.source


def test_unknown_kind_has_no_noop_emission_handler():
    ensure_utility_checks_loaded()
    assert Kind.UNKNOWN not in UtilitySpec._emit_handlers
    assert UtilitySpec.for_name("utility") is None
