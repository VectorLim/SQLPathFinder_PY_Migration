"""Test that MARS placeholders are correctly handled during translation and runtime."""

from __future__ import annotations

from pathlib import Path
from vg2c.dataflow import analyze
from vg2c.dispatch import dispatch
from vg2c.dispatch.models import DispatchConfig
from vg2c.emitter import emit
from vg2c.frontend import classify, parse
from vg2c.resolver import resolve

FIXTURES = Path(__file__).parent.parent / "fixtures"


def test_mars_placeholder_in_emitted_sql():
    """Verify @[]@.F_* placeholders are present in emitted SQL and will be substituted."""
    text = (FIXTURES / "actual_script.txt").read_text(
        encoding="utf-8", errors="replace"
    )
    p, pd = parse(text, source=FIXTURES / "actual_script.txt")
    c, cd = classify(p)
    r = resolve(c, diagnostics=[*pd, *cd])
    a = analyze(r)
    d = dispatch(a, config=DispatchConfig(oasys_schema="SCHEMA"))
    e = emit(d)

    source = e.source

    # Verify that the emitted code has @[]@. placeholders (with the dot)
    assert (
        "@[]@." in source
    ), "Expected @[]@. placeholders to be normalized with dots in emitted SQL"

    # Verify that @[]@F_ (without dot) is NOT in the emitted SQL
    # (it should have been normalized to @[]@.F_)
    assert (
        "@[]@F_" not in source
    ), "Found @[]@F_ without dot - should be normalized to @[]@.F_"

    assert "def _read_datasyncx" in source
    assert "DATASYNCX_READER_MAP" not in source
    assert "reader_cls=MarsReader" in source
