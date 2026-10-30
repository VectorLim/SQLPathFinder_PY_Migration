"""Execute isolated original HTML-RUN CSS blocks from four actual VG2 fixtures.

These are PARTIAL source-preserved checks, not full job or ScriptHost parity tests.
All later network, credential, executable and mail steps are excluded.
"""
from __future__ import annotations

import runpy
from pathlib import Path

import pytest

from vg2c import translate


ROOT = Path(__file__).resolve().parents[2]
SEPARATOR = "<---- New Query ---->"


@pytest.mark.parametrize(
    "fixture",
    [
        "CSR_IAM_v2.txt",
        "tests/fixtures/actual_script.txt",
        "tests/fixtures/maxlidheight.txt",
        "tests/fixtures/new_icmpcs.txt",
    ],
)
def test_original_fixture_first_css_block_runs_offline(tmp_path, fixture):
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
    workdir = main.parent / "output"
    workdir.mkdir()
    runpy.run_path(str(main), run_name="isolated_original_html_run")["run"]()

    stylesheet = workdir / "sqlpathfinder_style_1.css"
    assert stylesheet.is_file(), sorted(workdir.iterdir())
    css = stylesheet.read_text(encoding="utf-8")
    assert "background-color" in css
    assert "font-family" in css
    assert not (workdir / "owner.csv").exists()


def test_original_html_run_css_asset_is_editable_and_rerun_publishes_changes(tmp_path):
    """Original HTML-RUN CSS is a project asset, not hardcoded in main.py."""
    fixture = (ROOT / "tests/fixtures/actual_script.txt").read_text(
        encoding="utf-8", errors="replace"
    )
    block = fixture.split(SEPARATOR)[1]
    assert "/REPORT=HTML-RUN" in block
    assert "TYPE<\\\\>CSS" in block
    source = tmp_path / "original_css_block.txt"
    source.write_text(block, encoding="utf-8")
    main = translate(source, out_dir=tmp_path / "generated")
    css_assets = sorted((main.parent / "styles").glob("report_*.css"))
    assert len(css_assets) == 1
    workdir = main.parent / "output"
    workdir.mkdir()
    run = runpy.run_path(str(main), run_name="isolated_css_edit")["run"]
    run()
    output = workdir / "sqlpathfinder_style_1.css"
    initial = output.read_text(encoding="utf-8")
    assert initial == css_assets[0].read_text(encoding="utf-8")
    assert "background-color" in initial

    marker = "/* stage2 fixture asset edit */\n"
    css_assets[0].write_text(initial + marker, encoding="utf-8")
    run()
    assert output.read_text(encoding="utf-8") == initial + marker
    assert output.read_text(encoding="utf-8") != initial
