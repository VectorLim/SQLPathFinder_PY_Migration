"""Execute a safe, original-source segment from an external-dependent VG2 fixture.

This is intentionally PARTIAL coverage of vietnam_test_python.txt. Its first
/WRITE-FILE=Y block is local; later MARS/network/Windows operations are not run.
"""
from __future__ import annotations

import runpy
from pathlib import Path

from vg2c import translate


FIXTURE = Path(__file__).resolve().parents[1] / "fixtures" / "vietnam_test_python.txt"
SEPARATOR = "<---- New Query ---->"


def test_vietnam_fixture_original_local_write_block_runs_without_external_services(tmp_path):
    original = FIXTURE.read_text(encoding="utf-8", errors="replace")
    first_block = original.split(SEPARATOR, maxsplit=1)[0]
    assert first_block.startswith("<OPTIONS>\n/WRITE-FILE=Y\n")
    assert "/CSV=path.csv" in first_block
    assert "</OPTIONS>" in first_block
    # Preserve source payload without trimming leading or trailing whitespace.
    original_payload = first_block.split("</OPTIONS>", maxsplit=1)[1]
    assert original_payload.startswith("\n")

    source = tmp_path / "isolated_original_block.txt"
    source.write_text(first_block, encoding="utf-8")
    main = translate(source, out_dir=tmp_path / "generated")
    workdir = tmp_path / "work"
    workdir.mkdir()
    namespace = runpy.run_path(str(main), run_name="safe_original_fixture_block")
    namespace["run"](workdir=workdir)

    written = workdir / "path.csv"
    assert written.is_file()
    # Compare actual original block data, including Windows path characters.
    actual = written.read_text(encoding="utf-8")
    # The VG2 parser removes exactly one structural newline at each edge of\n    # the OPTIONS body. The second leading newline belongs to the data.\n    assert original_payload.startswith("\\n\\n")\n    assert original_payload.endswith("\\n")\n    assert actual == original_payload[1:-1]\n    assert actual.startswith("\\n")\n    assert not actual.endswith("\\n")\n    assert len(actual.strip().splitlines()) == 4
