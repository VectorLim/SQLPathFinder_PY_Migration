from __future__ import annotations

import importlib
import os
from contextlib import closing

from scripthost_portable.runtime import _spf_manager_type


def test_runtime_routes_existing_classes_to_portable_subclasses(monkeypatch):
    manager_type = _spf_manager_type()
    classes = {
        "globals": ("SPFLib.SPFGlobals", "SPFGlobals"),
        "utilities": ("SPFLib.SPFUtilities.utils", "Utilities"),
        "memtable": ("SPFLib.SPFUtilities.memtable", "MemTable"),
        "normal_query": ("SPFLib.SPFSQL3", "NormalQueryTaskBase"),
        "oracle": ("SPFLib.SPFSQL3", "nqOracleTask"),
        "site_time": ("SPFLib.SPFSQL3", "GetSiteTimeTask"),
        "update_time": ("SPFLib.SPFSQL3", "UpdateTimeFileTask"),
        "smart_append": ("SPFLib.SPFSQL3", "SmartAppendTask"),
        "readonly": ("SPFLib.SPFSQL3", "SetFileROTask"),
        "xml_to_csv": ("SPFLib.SPFSQL3", "XMLToCSVTask"),
        "echo": ("SPFLib.SPFSQL3", "EchoTask"),
        "get_files": ("SPFLib.SPFSQL3", "GetFilesTask"),
    }
    for filename, (module_name, class_name) in classes.items():
        exported = getattr(importlib.import_module(module_name), class_name)
        portable = getattr(
            importlib.import_module(f"scripthost_portable.overrides.{filename}"), class_name
        )
        base = portable.__bases__[0]
        assert exported is portable
        assert base.__name__ == portable.__name__ == class_name
        assert base.__module__ == module_name

    from SPFLib.SPFGlobals import SPFGlobals
    from SPFLib.SPFSQL3 import NormalQueryTaskBase, nqOracleTask
    from SPFLib.SPFUtilities.utils import Utilities

    # Further subclassing must not recurse in a legacy constructor or lose
    # the original property storage through Python's private name mangling.
    class DerivedUtilities(Utilities):
        pass

    monkeypatch.setattr(SPFGlobals, "_SPFGlobals__gLocalDir", None)
    utility = DerivedUtilities()
    assert utility.gLocalDir == os.path.abspath(os.curdir) + os.sep
    assert isinstance(manager_type(), Utilities)
    assert issubclass(nqOracleTask, NormalQueryTaskBase)


def test_inherited_memtable_registers_character_index_function():
    _spf_manager_type()
    from SPFLib.SPFUtilities.memtable import MemTable

    with closing(MemTable().getStandaloneCon(":memory:")) as connection:
        result = connection.execute(
            "SELECT CharIndex_v2('a', 'banana', 1, 2), "
            "CharIndex_v2('x', 'banana', 1, 1), "
            "CharIndex_v2(NULL, 'banana', 1, 1)"
        ).fetchone()
    assert result == (4, 0, 0)
