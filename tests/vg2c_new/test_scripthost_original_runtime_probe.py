from __future__ import annotations

import importlib
import os
import sys
import zipfile
from pathlib import Path

SCRIPT_HOST_ROOT = Path(__file__).resolve().parents[2] / "scripthost-utilities-decompiled"
EXTRACTED = SCRIPT_HOST_ROOT / "SPSQL3_py"
ARCHIVE = SCRIPT_HOST_ROOT / "SPSQL3_py.zip"


def _with_extracted_runtime() -> None:
    value = str(EXTRACTED)
    if value not in sys.path:
        sys.path.insert(0, value)


def test_original_archive_runtime_inventory() -> None:
    with zipfile.ZipFile(ARCHIVE) as archive:
        names = archive.namelist()

    dbdriver_entries = [name for name in names if "dbdriver" in name.lower()]
    print("dbdriver entries:", dbdriver_entries)
    print("archive python files:", sum(name.lower().endswith(".py") for name in names))
    assert names


def test_spfglobals_imports_on_linux() -> None:
    _with_extracted_runtime()
    module = importlib.import_module("SPFLib.SPFGlobals")
    assert module.SPFGlobals is not None


def test_original_spfmanager_imports_on_linux() -> None:
    _with_extracted_runtime()
    module = importlib.import_module("SPFLib.SPFSQL3")
    assert module.SPFManager is not None


DELIM = "<---- New Query ---->"


def _block(*options: str, body: str = "") -> str:
    return "<OPTIONS>\n" + "\n".join(options) + "\n</OPTIONS>\n" + body


def _script(*blocks: str) -> str:
    return ("\n" + DELIM + "\n").join(blocks)


def _representative_script() -> str:
    return _script(
        _block("/WRITE-FILE=Y", "/CSV=seed.txt", body="seed<EOF>ignored"),
        _block('/UTILITIES={START-MACRO} "macro.csv" "N"'),
        _block('/UTILITIES={IF-THEN} "VAR(<<<flag>>>)" "GT" "0"'),
        _block('/UTILITIES={FOR-LOOP} "0" "2" "1" "x" "N"'),
        _block(
            "/WRITE-FILE=Y",
            "/CSV=out_<<<name>>>_<<<spf-loop-ctr-x-int>>>.txt",
            body="<<<name>>>:<<<spf-loop-ctr-x-int>>>",
        ),
        _block("/UTILITIES={END-LOOP}"),
        _block("/UTILITIES={ELSE}"),
        _block("/WRITE-FILE=Y", "/CSV=bad.txt", body="wrong branch"),
        _block("/UTILITIES={END-IF}"),
        _block("/UTILITIES={END-MACRO}"),
    )


def test_original_run_spfsql_vertical_slice_on_linux(tmp_path, monkeypatch) -> None:
    _with_extracted_runtime()
    module = importlib.import_module("SPFLib.SPFSQL3")
    monkeypatch.chdir(tmp_path)
    (tmp_path / "macro.csv").write_text("name,flag\nalpha,1\nbeta,0\n", encoding="utf-8")

    manager = module.SPFManager()
    manager.gCommandLineArguments = [
        "scripthost-probe",
        f"/MYLOCAL={tmp_path}",
        f"/EXEDIR={tmp_path}",
    ]
    manager.MySPFSQLFileData = _representative_script()

    assert manager.Run_SPFSQL() is True
    assert not (tmp_path / "bad.txt").exists()
    assert [
        (tmp_path / f"out_alpha_{index}.txt").read_text(encoding="utf-8") for index in range(3)
    ] == ["alpha:0", "alpha:1", "alpha:2"]
