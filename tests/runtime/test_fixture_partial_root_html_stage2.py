"""Execute original, isolated HTML-RUN CSS blocks from both root VG2 fixtures.

These are PARTIAL and source-derived checks, not full ICMPCS/CSR job validation.
All later network, credential, executable and mail steps are excluded.
"""
from __future__ import annotations

import runpy
from pathlib import Path

import pytest

from vg2c import translate


ROOT = Path(__file__).resolve().parents[2]
SEPARATOR = "<---- New Query ---->"


@pytest.mark.parametrize("fixture", ["ICMPCS.txt", "CSR_IAM_v2.txt"])
def test_original_root_fixture_first_css_block_runs_offline(tmp_path, fixture):
    source_text = (ROOT / fixture).read_text(encoding="utf-8", errors="replace")
    sections = source_text.split(SEPARATOR)
    assert len(sections) > 2
    first_block = sections[1]
    assert "/REPORT=HTML-RUN" in first_block
    assert "TYPE<\\\\>CSS" in first_block
    assert "CSS<\\\\>sqlpathfinder_style_1.css" in first_block

    source = tmp_path / "local_first_block.txt"
    source.write_text(first_block, encoding="utf-8")
    main = translate(source, out_dir=tmp_path / "generated")
    workdir = tmp_path / "work"
    workdir.mkdir()
    runpy.run_path(str(main), run_name="isolated_original_html_run")["run"](workdir=workdir)

    stylesheet = workdir / "sqlpathfinder_style_1.css"
    assert stylesheet.is_file(), sorted(workdir.iterdir())
    css = stylesheet.read_text(encoding="utf-8")
    assert "background-color" in css
    assert "font-family" in css
    assert not (workdir / "owner.csv").exists()
