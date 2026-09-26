from __future__ import annotations

import importlib
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
