"""Execute two consecutive original, local SQLite steps from hamizah.txt.

The preceding MARS queries are not executed. Their original .tab output names
are populated with controlled local fixture rows. This tests the actual chain
from a join into a second select, without mocking a generated runtime call.
"""

from __future__ import annotations

import csv
import runpy
from pathlib import Path

import pytest

from vg2c import translate


FIXTURE = Path(__file__).resolve().parents[1] / "fixtures" / "hamizah.txt"
SEPARATOR = "<---- New Query ---->"


def _project(tmp_path):
    blocks = FIXTURE.read_text(encoding="utf-8").split(SEPARATOR)
    assert len(blocks) >= 4
    first, second = blocks[2:4]
    assert "/CSV=input.csv" in first
    assert "/TABLE=yeuchuan_a0_5547.tab,yeuchuan_a1_5547.tab" in first
    assert "INNER JOIN" in first.upper()
    assert "/CSV=yeuchuan_SQL_5547.tab" in second
    assert "/TABLE=input.csv" in second
    source = tmp_path / "original_consecutive_sqlite_blocks.txt"
    source.write_text(SEPARATOR.join([first, second]), encoding="utf-8")
    return translate(source, out_dir=tmp_path / "generated")


def _write_tab(path, rows):
    with path.open("w", encoding="utf-8", newline="") as stream:
        csv.writer(stream, delimiter="\t").writerows(rows)


def _read(path, delimiter):
    with path.open(encoding="utf-8", newline="") as stream:
        return list(csv.reader(stream, delimiter=delimiter))


def _seed(workdir):
    _write_tab(workdir / "yeuchuan_a0_5547.tab", [
        ["test_lot", "from_to", "tcb_lot"],
        ["TEST-1", "F", "TCB-1"],
        ["TEST-2", "F", "TCB-2"],
    ])
    _write_tab(workdir / "yeuchuan_a1_5547.tab", [
        ["lot_1", "entity", "prodgroup3"],
        ["TCB-1", "ENTITY-A", "GROUP-A"],
        ["OTHER", "ENTITY-X", "GROUP-X"],
    ])


def _run(main, workdir):
    runpy.run_path(str(main), run_name="isolated_hamizah_fixture")["run"](workdir=workdir)


def test_consecutive_original_hamizah_sqlite_steps_produce_downstream_relation(tmp_path):
    main = _project(tmp_path)
    workdir = tmp_path / "work"
    workdir.mkdir()
    _seed(workdir)

    _run(main, workdir)

    first = workdir / "input.csv"
    second = workdir / "yeuchuan_SQL_5547.tab"
    assert _read(first, ",") == [
        ["test_lot", "tcb_lot", "entity", "prodgroup3"],
        ["TEST-1", "TCB-1", "ENTITY-A", "GROUP-A"],
    ]
    assert second.read_text(encoding="utf-8").startswith("entity\tprodgroup3")
    assert _read(second, "\t") == [
        ["entity", "prodgroup3"],
        ["ENTITY-A", "GROUP-A"],
    ]


def test_consecutive_original_hamizah_sqlite_steps_fail_when_upstream_absent(tmp_path):
    main = _project(tmp_path)
    workdir = tmp_path / "empty"
    workdir.mkdir()
    with pytest.raises(FileNotFoundError):
        _run(main, workdir)
    assert not (workdir / "yeuchuan_SQL_5547.tab").exists()
