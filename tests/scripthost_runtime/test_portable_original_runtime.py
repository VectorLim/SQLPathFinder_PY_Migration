from __future__ import annotations

import importlib


def test_original_scripthost_runtime_imports_on_linux() -> None:
    globals_module = importlib.import_module("SPFLib.SPFGlobals")
    utils_module = importlib.import_module("SPFLib.SPFUtilities.utils")
    runtime_module = importlib.import_module("SPFLib.SPFSQL3")

    assert globals_module.SPFGlobals is not None
    assert utils_module.Utilities is not None
    assert runtime_module.SPFManager is not None
    assert runtime_module.SPFTaskBase is not None
