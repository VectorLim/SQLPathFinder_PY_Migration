from __future__ import annotations

import importlib
import logging
import sys
from pathlib import Path

SCRIPT_HOST = Path(__file__).resolve().parents[2] / "scripthost-utilities-decompiled" / "SPSQL3_py"


def _with_scripthost_path() -> None:
    value = str(SCRIPT_HOST)
    if value not in sys.path:
        sys.path.insert(0, value)


def test_spflib_import_does_not_require_windows_runtime() -> None:
    _with_scripthost_path()
    module = importlib.import_module("SPFLib")

    assert module.isPYTHON2 is False
    if sys.platform != "win32":
        assert module.win32api is None
        assert module.win32com is None
        assert module.clr is None
        assert "win32api" not in sys.modules
        assert "win32com" not in sys.modules


def test_pyutils_portable_helpers_import_without_windows_registry() -> None:
    _with_scripthost_path()
    module = importlib.import_module("PyUtils")

    assert module.FixString("A B/C", logger=logging.getLogger(__name__)) == "a_b_c"
    if sys.platform != "win32":
        assert module.winreg is None


def test_pyutils_fresh_instances_do_not_share_argument_state() -> None:
    _with_scripthost_path()
    module = importlib.import_module("PyUtils")
    logger = logging.getLogger(__name__)

    first = module.BuildArgsClass({"value": "one"}, {"value": ""}, "none", "Y", "N", logger=logger)
    second = module.BuildArgsClass({"value": "two"}, {"value": ""}, "none", "Y", "N", logger=logger)

    assert first.arg_values == {"value": "one"}
    assert second.arg_values == {"value": "two"}
    first.arg_values["value"] = "changed"
    assert second.arg_values == {"value": "two"}
