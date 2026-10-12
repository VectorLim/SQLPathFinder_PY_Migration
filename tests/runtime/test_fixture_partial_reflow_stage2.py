"""Execute original final SQLite RIGHT JOIN blocks of two real reflow fixtures.

Upstream MARS and ARIES steps are out-of-scope: controlled, tab-delimited rows
stand in for their outputs. A fixture whose upstream services are unavailable is
PARTIALLY validated, never reported as a complete passing job or ScriptHost run.
"""
from __future__ import annotations

import csv
import runpy
from pathlib import Path

import pytest

from vg2c import translate


FIXTURES = Path(__file__).resolve().parents[1] / "fixtures"


@pytest.mark.parametrize(
    ("name", "suffix"),
    [("reflow.txt", "30714"), ("reflow_detection.txt", "6511")],
)
def test_original_reflow_sqlite_right_join_retains_unmatched_inspection_rows(
    tmp_path, name, suffix
):
    source_text = (FIXTURES / name).read_text(encoding="utf-8", errors="replace")
    local_block = source_text.split("<---- New Query ---->")[-1]
    assert "/ENGINE=SQLite" in local_block
    assert f"/TABLE=yeuchuan_a0_{suffix}.tab,yeuchuan_a1_{suffix}.tab" in local_block
    assert "RIGHT OUTER JOIN" in local_block.upper()

    source = tmp_path / "original_reflow_sqlite_block.txt"
    source.write_text(local_block, encoding="utf-8")
    main = translate(source, out_dir=tmp_path / "generated")
    workdir = tmp_path / "work"
    workdir.mkdir()

    upstream = {
        f"yeuchuan_a0_{suffix}.tab": [
            ["facility", "lot_", "owner", "out_date"],
            ["PG", "MATCH-LOT", "ENGINEER", "2026-01-02"],
        ],
        f"yeuchuan_a1_{suffix}.tab": [
            [
                "site_work_week", "prodgroup3", "lot", "visual_id",
                "image_name", "image_full_path_RAW", "rank_", "maxrank_",
                "filter_", "image_full_path", "numeric_value", "numeric_value_max",
            ],
            [
                "202601", "CHIP", "MATCH-LOT", "IMAGE-A", "matched.png",
                "/safe/fixture/matched.png", "1", "2", "0",
                "/safe/fixture/matched.png", "1.2", "1.8",
            ],
            [
                "202601", "CHIP", "UNMATCHED-LOT", "IMAGE-B", "unmatched.png",
                "/safe/fixture/unmatched.png", "1", "2", "1",
                "/safe/fixture/unmatched.png", "1.3", "1.9",
            ],
        ],
    }
    for file_name, rows in upstream.items():
        with (workdir / file_name).open("w", newline="", encoding="utf-8") as stream:
            csv.writer(stream, delimiter="\t").writerows(rows)

    namespace = runpy.run_path(str(main), run_name="original_reflow_sqlite_join")
    namespace["run"](workdir=workdir)

    output = workdir / "XRAY_results_batch.csv"
    assert output.is_file()
    with output.open(newline="", encoding="utf-8") as stream:
        reader = csv.DictReader(stream)
        assert reader.fieldnames == [
            "facility", "site_work_week", "prodgroup3", "lot", "owner",
            "out_date", "visual_id", "image_name", "image_full_path_RAW",
            "rank_", "maxrank_", "filter_", "image_full_path",
            "numeric_value", "numeric_value_max",
        ]
        rows = {row["lot"]: row for row in reader}
    assert set(rows) == {"MATCH-LOT", "UNMATCHED-LOT"}
    assert rows["MATCH-LOT"]["owner"] == "ENGINEER"
    assert rows["MATCH-LOT"]["visual_id"] == "IMAGE-A"
    assert rows["UNMATCHED-LOT"]["owner"] == ""
    assert rows["UNMATCHED-LOT"]["visual_id"] == "IMAGE-B"
