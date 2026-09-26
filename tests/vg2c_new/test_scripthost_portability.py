from __future__ import annotations

import importlib
import logging
import sys
from pathlib import Path

import pytest

SCRIPTHOST_PARENT = Path(__file__).resolve().parents[2] / "scripthost-utilities-decompiled"


def _import(name: str):
    path = str(SCRIPTHOST_PARENT)
    if path not in sys.path:
        sys.path.insert(0, path)
    return importlib.import_module(name)


def test_scripthost_core_imports_without_windows_runtime() -> None:
    spflib = _import("SPSQL3_py.SPFLib")
    _import("SPSQL3_py.SPFLib.SPFGlobals")
    utilities = _import("SPSQL3_py.SPFLib.SPFUtilities.utils")
    memtable = _import("SPSQL3_py.SPFLib.SPFUtilities.memtable")

    assert utilities.Utilities.__mro__[1].__name__ == "SPFGlobals"
    assert memtable.MemTable.myMemTable is None

    if sys.platform != "win32":
        assert spflib.WINDOWS_RUNTIME_AVAILABLE is False
        for module_name in ("clr", "win32api", "win32com", "win32security", "winreg"):
            assert module_name not in sys.modules
        with pytest.raises(RuntimeError, match="requires the legacy Windows ScriptHost runtime"):
            spflib.require_windows_runtime("legacy COM operation")


def test_pyutils_portable_helpers_use_fresh_state() -> None:
    pyutils = _import("SPSQL3_py.PyUtils")
    logger = logging.getLogger("test_pyutils_portable_helpers")

    first = pyutils.BuildArgs({"value": "first"}, {"value": ""}, logger=logger)
    second = pyutils.BuildArgs({"value": "second"}, {"value": ""}, logger=logger)

    assert first == {"value": "first"}
    assert second == {"value": "second"}
    assert first is not second
    assert pyutils.FixString("A report name!", logger=logger) == "a_report_name_"


def test_report_modules_import_from_package_on_linux() -> None:
    html_report = _import("SPSQL3_py.AutoComm_HTML_Report")
    chart_data = _import("SPSQL3_py.AutoComm_ChartData")
    graphing = _import("SPSQL3_py.PyGraphingMethods")
    plotly_module = _import("SPSQL3_py.PyPlot_Class")

    assert hasattr(html_report, "AC_HTML_Report")
    assert hasattr(chart_data, "AC_Chart_Data")
    assert hasattr(graphing, "Basic_Charts")
    assert hasattr(plotly_module, "SPFPlotly")
