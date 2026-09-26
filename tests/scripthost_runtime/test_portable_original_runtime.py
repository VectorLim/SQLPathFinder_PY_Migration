from __future__ import annotations

import importlib
import sys
from pathlib import Path

SCRIPT_HOST = (
    Path(__file__).resolve().parents[2] / "scripthost-utilities-decompiled" / "SPSQL3_py"
)
DELIMITER = "\n<---- New Query ---->\n"


def _runtime_module():
    script_host = str(SCRIPT_HOST)
    if script_host not in sys.path:
        sys.path.insert(0, script_host)
    return importlib.import_module("SPFLib.SPFSQL3")


def _run_original_runtime(tmp_path: Path, spfsql: str, instance: str):
    runtime_module = _runtime_module()
    manager = runtime_module.SPFManager()
    manager.gCommandLineArguments = [
        "SPFSQL3.py",
        f"/MYLOCAL={tmp_path}",
        f"/EXEDIR={tmp_path}",
        f"/SPFINSTANCE={instance}",
    ]
    manager.MySPFSQLFileData = spfsql
    assert manager.Run_SPFSQL() is True
    return manager


def _task(*option_lines: str, command: str = "") -> str:
    options = "\n".join(option_lines)
    return f"<OPTIONS>\n{options}\n</OPTIONS>\n{command}"


def test_original_scripthost_runtime_imports_on_linux() -> None:
    runtime_module = _runtime_module()
    globals_module = importlib.import_module("SPFLib.SPFGlobals")
    utils_module = importlib.import_module("SPFLib.SPFUtilities.utils")

    assert globals_module.SPFGlobals is not None
    assert utils_module.Utilities is not None
    assert runtime_module.SPFManager is not None
    assert runtime_module.SPFTaskBase is not None


def test_original_runtime_executes_write_file_through_manager_pipeline(tmp_path) -> None:
    output = tmp_path / "original-runtime.txt"
    spfsql = _task(
        "/WRITE-FILE=Y",
        f"/CSV={output}",
        command="hello from original ScriptHost runtime<EOF>",
    )

    _run_original_runtime(tmp_path, spfsql, "portable-original-write")

    assert output.read_text(encoding="utf-8-sig") == "hello from original ScriptHost runtime"


def test_original_runtime_executes_representative_control_flow_on_linux(tmp_path) -> None:
    initial_output = tmp_path / "initial.txt"
    macro_output = tmp_path / "macro.txt"
    if_output = tmp_path / "if-true.txt"
    else_output = tmp_path / "if-false.txt"
    macro_file = tmp_path / "macro.csv"
    macro_file.write_text("value\nmacro-expanded\n", encoding="utf-8")

    tasks = [
        _task(
            "/WRITE-FILE=Y",
            f"/CSV={initial_output}",
            command="initial<EOF>",
        ),
        _task(f'/UTILITIES={{START-MACRO}} "{macro_file}"'),
        _task(
            "/WRITE-FILE=Y",
            f"/CSV={macro_output}",
            command="<<<value>>><EOF>",
        ),
        _task("/UTILITIES={END-MACRO}"),
        _task('/UTILITIES={IF-THEN} "VAR(1)" "EQ" "1"'),
        _task(
            "/WRITE-FILE=Y",
            f"/CSV={if_output}",
            command="if-branch<EOF>",
        ),
        _task("/UTILITIES={ELSE}"),
        _task(
            "/WRITE-FILE=Y",
            f"/CSV={else_output}",
            command="else-branch<EOF>",
        ),
        _task("/UTILITIES={END-IF}"),
        _task('/UTILITIES={FOR-LOOP} "0" "2" "1" "T" "N"'),
        _task(
            "/WRITE-FILE=Y",
            f"/CSV={tmp_path}/loop-<<<spf-loop-ctr-T-int>>>.txt",
            command="loop-<<<spf-loop-ctr-T-int>>><EOF>",
        ),
        _task("/UTILITIES={END-LOOP}"),
    ]

    _run_original_runtime(tmp_path, DELIMITER.join(tasks), "portable-original-control-flow")

    assert initial_output.read_text(encoding="utf-8-sig") == "initial"
    assert macro_output.read_text(encoding="utf-8-sig") == "macro-expanded"
    assert if_output.read_text(encoding="utf-8-sig") == "if-branch"
    assert not else_output.exists()
    assert [
        (tmp_path / f"loop-{index}.txt").read_text(encoding="utf-8-sig")
        for index in range(3)
    ] == ["loop-0", "loop-1", "loop-2"]
