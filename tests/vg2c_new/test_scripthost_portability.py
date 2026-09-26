from __future__ import annotations

import importlib
import os
import sys
import zipfile
from pathlib import Path

import pytest

from SPSQL3_py.SPFLib.SPFUtilities.portable import (
    get_file_delimiter,
    zip_files,
    zip_folder,
)


@pytest.mark.skipif(os.name == "nt", reason="Linux portability assertion")
def test_portable_scripthost_imports_do_not_load_windows_runtime() -> None:
    importlib.import_module("SPSQL3_py.SPFLib")
    importlib.import_module("SPSQL3_py.SPFLib.SPFGlobals")
    importlib.import_module("SPSQL3_py.PyUtils")

    for name in ("win32api", "win32com", "pythoncom", "winreg", "clr"):
        assert name not in sys.modules


def test_report_modules_import_without_optional_plot_backends() -> None:
    importlib.import_module("SPSQL3_py.AutoComm_HTML_Report")
    importlib.import_module("SPSQL3_py.AutoComm_ChartData")
    importlib.import_module("SPSQL3_py.PyGraphingMethods")
    importlib.import_module("SPSQL3_py.PyPlot_Class")


def test_portable_delimiter_matches_current_scripthost_contract() -> None:
    assert get_file_delimiter("x.tab") == "\t"
    assert get_file_delimiter("x.hive-tab") == "\t"
    assert get_file_delimiter("x.asc") == "|"
    assert get_file_delimiter("x.plus") == "+"
    assert get_file_delimiter("x.csv") == ","
    assert get_file_delimiter("x.txt", mode="I") == ","
    assert get_file_delimiter("x.txt", mode="O") == ""
    with pytest.raises(ValueError, match="not supported"):
        get_file_delimiter("x.json")


def test_portable_zip_helpers_are_state_isolated(tmp_path: Path) -> None:
    one = tmp_path / "one.txt"
    two = tmp_path / "two.txt"
    one.write_text("one", encoding="utf-8")
    two.write_text("two", encoding="utf-8")

    first = zip_files([one], tmp_path / "first.zip", retain_relative_path=False)
    second = zip_files([two], tmp_path / "second.zip", retain_relative_path=False)

    with zipfile.ZipFile(first) as archive:
        assert archive.namelist() == ["one.txt"]
        assert archive.read("one.txt") == b"one"
    with zipfile.ZipFile(second) as archive:
        assert archive.namelist() == ["two.txt"]
        assert archive.read("two.txt") == b"two"

    folder = tmp_path / "folder"
    folder.mkdir()
    (folder / "nested.txt").write_text("nested", encoding="utf-8")
    folder_archive = zip_folder(folder, tmp_path / "folder.zip")
    with zipfile.ZipFile(folder_archive) as archive:
        assert archive.namelist() == ["nested.txt"]
