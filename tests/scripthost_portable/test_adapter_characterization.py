"""Characterize the original task semantics retained by thin adapters."""

import importlib
import os
from contextlib import closing
from types import SimpleNamespace
from unittest.mock import Mock

import pytest

from scripthost_portable.runtime import _spf_manager_type


@pytest.mark.parametrize("increment", ["50", "1000", "no", "0"])
def test_incremental_path_and_legacy_range(increment):
    _spf_manager_type()
    from SPFLib.SPFSQL3 import NormalQueryTaskBase

    class Task(NormalQueryTaskBase):
        logger = Mock()
        OLEDBopt = "ORACLE"
        SQLEngine = "MICROSOFT"
        TmpNode = False

    task = object.__new__(Task)
    result = task.Prep_Inc_Process(
        r".\folder\input.csv->" + increment, 999, True, "ignored", "SQLite", "ignored", False
    )
    count = int(increment) if increment.isdigit() else -1
    path = "./folder/input.csv" if os.sep == "/" else r".\folder\input.csv"
    # The legacy range expression accepts 1000. This cleanup must preserve it.
    assert result == (path, count >= 0, count)
    if count >= 0:
        with pytest.raises(Exception, match="only allowed to specify one incremental"):
            task.Prep_Inc_Process("input.csv->" + increment, -1, False, "", "", "", True)


def test_character_index_is_registered_before_attach_sql():
    _spf_manager_type()
    from SPFLib.SPFUtilities.memtable import MemTable

    with closing(MemTable().getStandaloneCon(
        ":memory:", "CREATE TABLE positions AS SELECT CharIndex_v2('a', 'banana', 1, 2) AS position"
    )) as connection:
        assert connection.execute("SELECT position FROM positions").fetchone() == (4,)
        assert connection.execute("SELECT SPFPrepLikeValue('ABC')").fetchone() == ("ABC%",)


@pytest.mark.skipif(os.name == "nt", reason="POSIX command replacement")
def test_file_move_retains_retry_count_and_error(tmp_path, monkeypatch):
    _spf_manager_type()
    import SPFLib.SPFUtilities.utils as legacy

    utility = legacy.Utilities()
    sleeps = []
    monkeypatch.setattr(legacy.time, "sleep", sleeps.append)
    monkeypatch.setattr(utility, "FileIsLocked", lambda path: False)
    with pytest.raises(Exception, match="Even after 5 retries"):
        utility.File_Lock_Move(str(tmp_path / "missing.csv"), str(tmp_path / "target.csv"))
    assert sleeps == [30] * 5
    assert not (tmp_path / "target.csv").exists()


@pytest.mark.skipif(os.name == "nt", reason="POSIX file-pattern separator")
def test_file_pattern_retains_newest_folder_selection(tmp_path):
    _spf_manager_type()
    from SPFLib.SPFUtilities.utils import Utilities

    old, new = tmp_path / "old", tmp_path / "new"
    old.mkdir()
    new.mkdir()
    os.utime(old, (1700000000, 1700000000))
    os.utime(new, (1700000100, 1700000100))
    (new / "data.csv").write_text("ID\n1\n")
    pattern = tmp_path / "<folder-DateLastModified>" / "data.csv"
    assert Utilities().GetFilePattern(str(pattern)) == str(new / "data.csv")


@pytest.mark.parametrize("module_name,class_name,method,arguments", [
    ("utilities", "Utilities", "SPFDelete", ("file.csv",)),
    ("utilities", "Utilities", "ConvertDLM", ("in.csv", "out.csv", "converter.exe")),
    ("utilities", "Utilities", "IntelWW", (object(),)),
    ("utilities", "Utilities", "SPFEmail", tuple(range(11))),
    ("utilities", "Utilities", "UnzipFile", ("in.zip", "out", False)),
    ("utilities", "Utilities", "SPFCopy", ("in.csv", "out.csv")),
    ("utilities", "Utilities", "LoadExcel2", ("LOAD", False, "in.csv", "out.xlsx")),
    ("echo", "EchoTask", "executeTaskCommand", ()),
    ("readonly", "SetFileROTask", "executeTaskCommand", ()),
    ("xml_to_csv", "XMLToCSVTask", "Run", ("spfExcelUtility.exe", [])),
])
def test_windows_uses_original_method(monkeypatch, module_name, class_name, method, arguments):
    _spf_manager_type()
    module = importlib.import_module("scripthost_portable.overrides." + module_name)
    portable = getattr(module, class_name)
    original = portable.__bases__[0]
    calls = []
    sentinel = object()

    def inherited(self, *args, **kwargs):
        calls.append((args, kwargs))
        return sentinel

    monkeypatch.setattr(original, method, inherited)
    # Replace only the legacy module's dependency, not Python's global os.name.
    monkeypatch.setattr(module.legacy, "os", SimpleNamespace(name="nt"))
    assert getattr(object.__new__(portable), method)(*arguments) is sentinel
    assert len(calls) == 1
    assert calls[0][0][:len(arguments)] == arguments


def test_missing_windows_smtp_does_not_fall_back_to_outlook(monkeypatch):
    _spf_manager_type()
    from scripthost_portable.overrides.utilities import Utilities, legacy

    class Email(Utilities):
        logger = Mock()
        gUserPrincipal = "sender@example.test"
        SHisSHEntry = False

        def Substitute_Std_Tokens(self, value, mode):
            return value

        def GetEmailAdss(self, *args):
            return ["person@example.test"], 1, "N"

    email = Email()
    outlook = Mock()
    monkeypatch.setattr(email, "ol_email", outlook)
    monkeypatch.setattr(legacy, "SPFSMTPAuthEmail", None)
    monkeypatch.setattr(legacy, "os", SimpleNamespace(**{**vars(os), "name": "nt"}))
    with pytest.raises(RuntimeError, match="SMTP transport is unavailable"):
        email.SPFEmail("N", "", "person@example.test", "subject", "", "", "", "", False, False)
    outlook.assert_not_called()
