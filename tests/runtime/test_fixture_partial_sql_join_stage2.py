"""Exercise the original final SQLite/LEFT JOIN block of sql_script.txt offline.

The two preceding MARS/OASYS steps require external services and are NOT run.
Source SQL, option headers, and input table names are kept from the real VG2
fixture. Fake tab-separated upstream results are stored only in tmp_path.
"""
from __future__ import annotations

import csv
import runpy
from pathlib import Path

from vg2c import translate


FIXTURE = Path(__file__).resolve().parents[1] / "fixtures" / "sql_script.txt"
SEPARATOR = "<---- New Query ---->"


def test_original_sql_script_local_sqlite_join_with_tab_inputs(tmp_path):
    text = FIXTURE.read_text(encoding="utf-8", errors="replace")
    final_block = text.split(SEPARATOR)[-1]
    assert "/ENGINE=SQLite" in final_block
    assert "/TABLE=yeuchuan_a0_29397.tab,yeuchuan_a1_29397.tab" in final_block
    assert "LEFT OUTER JOIN" in final_block.upper()

    source = tmp_path / "original_sqlite_block.txt"
    source.write_text(final_block, encoding="utf-8")
    main = translate(source, out_dir=tmp_path / "generated")
    workdir = main.parent / "output"
    workdir.mkdir()

    sources = {
        "yeuchuan_a0_29397.tab": [
            ["prodgroup3", "operation", "lot", "prevout_date"],
            ["BDR", "1225", "LOT-A", "2026-01-02"],
            ["BDR", "1225", "LOT-B", "2026-01-03"],
        ],
        "yeuchuan_a1_29397.tab": [
            ["spc_lot", "monitor_set_name"],
            ["LOT-A", "V_PRE_NDLE_OFF"],
        ],
    }
    for name, rows in sources.items():
        with (workdir / name).open("w", encoding="utf-8", newline="") as output:
            csv.writer(output, delimiter="\t").writerows(rows)

    namespace = runpy.run_path(str(main), run_name="isolated_original_sqlite_join")
    namespace["run"]()
    output = workdir / "out_26585.tab"
    assert output.is_file()
    with output.open(encoding="utf-8", newline="") as stream:
        header_line = stream.readline()
        stream.seek(0)
        assert "\t" in header_line
        assert "," not in header_line
        reader = csv.DictReader(stream, delimiter="\t")
        assert reader.fieldnames == [
            "prodgroup3", "operation", "lot",
            "spc_lot", "monitor_set_name", "prevout_date",
        ]
        result = {row["lot"]: row for row in reader}
    assert set(result) == {"LOT-A", "LOT-B"}
    assert result["LOT-A"]["spc_lot"] == "LOT-A"
    assert result["LOT-A"]["monitor_set_name"] == "V_PRE_NDLE_OFF"
    assert result["LOT-B"]["spc_lot"] in ("", None)
    assert result["LOT-B"]["prevout_date"] == "2026-01-03"
