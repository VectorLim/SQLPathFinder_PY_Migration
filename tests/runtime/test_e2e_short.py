"""End-to-end runtime test: translate script_short.txt and exec() the result.

This is the "generated code actually runs" proof for Stage 7.
"""

from __future__ import annotations

import csv
from pathlib import Path

from vg2c.dataflow import analyze
from vg2c.dispatch import dispatch
from vg2c.emitter import emit
from vg2c.frontend import classify, parse
from vg2c.resolver import resolve

FIXTURES = Path(__file__).parent.parent / "fixtures"


def _run_full_pipeline(fixture_name: str) -> str:
    text = (FIXTURES / fixture_name).read_text(encoding="utf-8", errors="replace")
    p, pd = parse(text, source=FIXTURES / fixture_name)
    c, cd = classify(p)
    r = resolve(c, diagnostics=[*pd, *cd])
    a = analyze(r)
    d = dispatch(a)
    e = emit(d)
    return e.source


def test_e2e_script_short(tmp_path, monkeypatch):
    """Translate script_short.txt, exec it, assert output CSV exists."""
    source = _run_full_pipeline("maxlidheight.txt")
    (Path.cwd() / "generated_script.py").write_text(source, encoding="utf-8")

    monkeypatch.chdir(tmp_path)

    # Execute the generated script (it's self-contained now, no patching needed)
    ns: dict[str, object] = {}
    exec(source, ns)  # noqa: S102
    run_fn = ns["run"]
    assert callable(run_fn)
    run_fn()
