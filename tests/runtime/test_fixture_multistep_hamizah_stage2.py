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
    user_code = main.read_text(encoding="utf-8")
    assert "def run(workdir=None):" in user_code
    assert "job = JobRuntime.from_script(__file__, workdir)" in user_code
    assert "@job_entrypoint" not in user_code
    assert "job.sql(" in user_code
    assert "output='input.csv', inputs=['yeuchuan_a0_5547.tab', 'yeuchuan_a1_5547.tab']" in user_code
    assert "output='yeuchuan_SQL_5547.tab', inputs=['input.csv']" in user_code
    assert "header=" not in user_code
    assets = list((main.parent / "sql").glob("*.sql"))
    assert len(assets) == 2
    assert all(asset.read_text(encoding="utf-8").startswith("-- vg2c-output-headers: ")
               for asset in assets)
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


def test_consecutive_original_hamizah_sql_asset_edit_changes_rerun(tmp_path):
    main = _project(tmp_path)
    workdir = tmp_path / "work-edited"
    workdir.mkdir()
    _seed(workdir)
    _run(main, workdir)
    output = workdir / "yeuchuan_SQL_5547.tab"
    assert _read(output, "\t")[1] == ["ENTITY-A", "GROUP-A"]

    sql_assets = sorted((main.parent / "sql").glob("*.sql"))
    assert len(sql_assets) == 2
    downstream = [
        asset for asset in sql_assets
        if "[input] a0" in asset.read_text(encoding="utf-8")
    ]
    assert len(downstream) == 1
    source = downstream[0].read_text(encoding="utf-8")
    original = "a0.[entity] AS [entity]"
    assert original in source
    downstream[0].write_text(
        source.replace(original, "'PATCHED' AS [entity]"),
        encoding="utf-8",
    )

    _run(main, workdir)
    assert _read(output, "\t") == [
        ["entity", "prodgroup3"],
        ["PATCHED", "GROUP-A"],
    ]


def test_original_first_four_hamizah_blocks_run_with_mocked_mars_reader(tmp_path, monkeypatch):
    """Preserve all four VG2 bodies; only the external MARS results are simulated.

    A fake reader validates local cross-block SQL_Get_CSV_List expansion, not
    Oracle SQL execution or original ScriptHost engine parity.
    """
    import sys
    import types

    import pandas as pd

    blocks = FIXTURE.read_text(encoding="utf-8").split(SEPARATOR)
    assert len(blocks) >= 4
    assert "SQL_Get_CSV_List" in blocks[1]
    assert all("/ENGINE=SQLite" in block for block in blocks[2:4])
    source = tmp_path / "original_four_blocks.txt"
    source.write_text(SEPARATOR.join(blocks[:4]), encoding="utf-8")

    calls = []

    class OfflineReader:
        def __init__(self, *args, **kwargs):
            pass

        def read(self, *, site, query):
            calls.append((site, query))
            if len(calls) == 1:
                return pd.DataFrame({
                    "test_lot": ["TEST-1", "TEST-2"],
                    "from_to": ["F", "F"],
                    "tcb_lot": ["TCB-1", "TCB-2"],
                })
            if len(calls) == 2:
                return pd.DataFrame({
                    "lot_1": ["TCB-1", "OTHER"],
                    "entity": ["ENTITY-A", "ENTITY-X"],
                    "prodgroup3": ["GROUP-A", "GROUP-X"],
                })
            raise AssertionError("Unexpected external reader invocation")

    fake_datasyncx = types.ModuleType("datasyncx")
    fake_datasyncx.MarsReader = OfflineReader
    monkeypatch.setitem(sys.modules, "datasyncx", fake_datasyncx)

    main = translate(source, out_dir=tmp_path / "generated")
    workdir = tmp_path / "work-all-four"
    workdir.mkdir()
    _run(main, workdir)

    assert len(calls) == 2
    assert [site for site, _ in calls] == ["KM", "KM"]
    assert "SQL_Get_CSV_List" not in calls[1][1]
    assert "TCB-1" in calls[1][1] and "TCB-2" in calls[1][1]
    assert _read(workdir / "yeuchuan_a0_5547.tab", "\t") == [
        ["test_lot", "from_to", "tcb_lot"],
        ["TEST-1", "F", "TCB-1"],
        ["TEST-2", "F", "TCB-2"],
    ]
    assert _read(workdir / "yeuchuan_a1_5547.tab", "\t") == [
        ["lot_1", "entity", "prodgroup3"],
        ["TCB-1", "ENTITY-A", "GROUP-A"],
        ["OTHER", "ENTITY-X", "GROUP-X"],
    ]
    assert _read(workdir / "input.csv", ",") == [
        ["test_lot", "tcb_lot", "entity", "prodgroup3"],
        ["TEST-1", "TCB-1", "ENTITY-A", "GROUP-A"],
    ]
    assert _read(workdir / "yeuchuan_SQL_5547.tab", "\t") == [
        ["entity", "prodgroup3"],
        ["ENTITY-A", "GROUP-A"],
    ]
