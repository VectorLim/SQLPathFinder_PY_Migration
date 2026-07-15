"""End-to-end pipeline smoke test over every fixture script.

For each ``tests/fixtures/*.txt`` we run the full pipeline
(parse → classify → resolve → analyze → dispatch → emit) and assert the
emitted source compiles as valid Python.
"""

from __future__ import annotations

from pathlib import Path

import pytest

from vg2c.dataflow import analyze
from vg2c.dispatch import dispatch
from vg2c.emitter import emit
from vg2c.frontend import classify, parse
from vg2c.resolver import resolve

FIXTURES = Path(__file__).parent.parent / "fixtures"
FIXTURE_FILES = sorted(FIXTURES.glob("*.txt"))


@pytest.mark.parametrize("fixture", FIXTURE_FILES, ids=[f.name for f in FIXTURE_FILES])
def test_pipeline_emits_compilable_source(fixture: Path) -> None:
    text = fixture.read_text(encoding="utf-8", errors="replace")

    parsed = parse(text, source=fixture)
    classified = classify(parsed)
    resolved = resolve(classified)
    analyzed = analyze(resolved)
    dispatched = dispatch(analyzed)
    emitted = emit(dispatched)

    source = emitted.source
    assert source and "def run(" in source, f"empty/invalid emit for {fixture.name}"

    compile(source, filename=f"<{fixture.name}>", mode="exec")
