"""End-to-end runtime test: translate script_short.txt and exec() the result.

This is the "generated code actually runs" proof for Stage 7.
"""

from __future__ import annotations

import csv
from pathlib import Path

import vg2c_runtime

from vg2c.dataflow import analyze
from vg2c.dispatch import dispatch
from vg2c.dispatch.models import DispatchConfig
from vg2c.emitter import emit
from vg2c.frontend import classify, parse
from vg2c.resolver import resolve
from vg2c_runtime.context import PipelineContext

FIXTURES = Path(__file__).parent.parent / "fixtures"


def _run_full_pipeline(fixture_name: str) -> str:
    text = (FIXTURES / fixture_name).read_text(encoding="utf-8", errors="replace")
    p, pd = parse(text, source=FIXTURES / fixture_name)
    c, cd = classify(p)
    r = resolve(c, diagnostics=[*pd, *cd])
    a = analyze(r)
    d = dispatch(a, config=DispatchConfig(oasys_schema="SCHEMA"))
    e = emit(d)
    return e.source


def test_e2e_script_short(tmp_path, monkeypatch):
    """Translate script_short.txt, exec it, assert output CSV exists."""
    source = _run_full_pipeline("actual_script.txt")

    # script_short.txt has:  /TABLE=ww_yield.csv  /CSV=owner.csv  (SQLite query)
    # We need a ww_yield.csv in the working directory.
    input_csv = tmp_path / "ww_yield.csv"
    input_csv.write_text("owner\nAlice\nBob\nAlice\n", encoding="utf-8")

    monkeypatch.chdir(tmp_path)

    # Build a fresh context and patch vg2c_runtime singleton used by emitted import.
    old_ctx = vg2c_runtime.ctx
    vg2c_runtime.ctx = PipelineContext()
    try:
        ns: dict[str, object] = {}
        exec(source, ns)  # noqa: S102
        run_fn = ns["run"]
        assert callable(run_fn)
        run_fn()
    finally:
        vg2c_runtime.ctx = old_ctx

    output_csv = tmp_path / "owner.csv"
    assert output_csv.exists(), "output CSV owner.csv must be created"
    with output_csv.open(newline="", encoding="utf-8") as fh:
        rows = list(csv.DictReader(fh))
    # DISTINCT owner — expect Alice and Bob (deduped)
    owners = {r["owner"] for r in rows}
    assert "Alice" in owners
    assert "Bob" in owners
