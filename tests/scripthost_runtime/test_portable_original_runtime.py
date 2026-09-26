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


def test_original_runtime_executes_write_file_through_manager_pipeline(tmp_path) -> None:
    runtime_module = importlib.import_module("SPFLib.SPFSQL3")
    manager = runtime_module.SPFManager()
    output = tmp_path / "original-runtime.txt"

    manager.gCommandLineArguments = [
        "SPFSQL3.py",
        f"/MYLOCAL={tmp_path}",
        f"/EXEDIR={tmp_path}",
        "/SPFINSTANCE=portable-original-write",
    ]
    manager.MySPFSQLFileData = (
        "<OPTIONS>\n"
        "/WRITE-FILE=Y\n"
        f"/CSV={output}\n"
        "</OPTIONS>\n"
        "hello from original ScriptHost runtime<EOF>"
    )

    assert manager.Run_SPFSQL() is True
    assert output.read_text(encoding="utf-8-sig") == "hello from original ScriptHost runtime"
