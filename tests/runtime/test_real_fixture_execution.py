"""Behavioral smoke tests execute an actual checked-in VG2 fixture, not synthetic VG2 input.

The owner query is a single local SQLite operation. No reader credentials, email,
network or external executables are needed. This tests generated asset plumbing
and runtime behavior that the emission-only fixture suite cannot verify.
"""

from __future__ import annotations

import csv
import runpy
import shutil
from pathlib import Path

import pytest

from vg2c import translate


SHORT = Path(__file__).parents[1] / "fixtures" / "script_short.txt"


def _project(tmp_path: Path) -> Path:
    source_dir = tmp_path / "fixture-source"
    source_dir.mkdir()
    source = source_dir / SHORT.name
    shutil.copyfile(SHORT, source)
    return translate(source, out_dir=tmp_path / "generated")


def _rows(path: Path) -> list[list[str]]:
    with path.open(encoding="utf-8", newline="") as stream:
        return list(csv.reader(stream))


def _run(main_py: Path) -> None:
    # run_name is deliberately not __main__: call the generated public run()
    # with its project-local output root, regardless of the process CWD.
    namespace = runpy.run_path(str(main_py), run_name="vg2c_fixture_test")
    namespace["run"]()


def test_script_short_real_fixture_executes_distinct_owner_query(tmp_path, monkeypatch):
    main_py = _project(tmp_path)
    workdir = main_py.parent / "output"
    workdir.mkdir()
    (workdir / "ww_yield.csv").write_text(
        'owner\nALICE\nBOB\nALICE\n"雪"\n', encoding="utf-8"
    )
    unrelated = tmp_path / "unrelated"
    unrelated.mkdir()
    monkeypatch.chdir(unrelated)

    _run(main_py)

    result = _rows(workdir / "owner.csv")
    assert result[0] == ["owner"]
    # DISTINCT has no ORDER BY in the actual fixture; assert the relation.
    assert sorted(result[1:]) == [["ALICE"], ["BOB"], ["雪"]]
    assert not (unrelated / "owner.csv").exists()


def test_script_short_real_fixture_sql_asset_is_editable(tmp_path):
    main_py = _project(tmp_path)
    sql_assets = list((main_py.parent / "sql").glob("*.sql"))
    assert len(sql_assets) == 1
    sql_assets[0].write_text("SELECT 'patched' AS [owner] FROM [ww_yield] a0\n", encoding="utf-8")
    workdir = main_py.parent / "output"
    workdir.mkdir()
    (workdir / "ww_yield.csv").write_text("owner\nALICE\n", encoding="utf-8")

    _run(main_py)

    assert _rows(workdir / "owner.csv") == [["owner"], ["patched"]]


def test_script_short_real_fixture_missing_csv_fails_loudly(tmp_path):
    main_py = _project(tmp_path)
    workdir = main_py.parent / "output"
    workdir.mkdir()

    with pytest.raises(FileNotFoundError):
        _run(main_py)

    assert not (workdir / "owner.csv").exists()
