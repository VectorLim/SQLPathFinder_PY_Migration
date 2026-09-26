from __future__ import annotations

import importlib
import subprocess
import sys
import threading
from concurrent.futures import ThreadPoolExecutor
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



def test_original_runtime_repeats_local_work_same_process(tmp_path) -> None:
    first = tmp_path / "first.txt"
    second = tmp_path / "second.txt"

    _run_original_runtime(
        tmp_path,
        _task("/WRITE-FILE=Y", f"/CSV={first}", command="first<EOF>"),
        "same-process-first",
    )
    _run_original_runtime(
        tmp_path,
        _task("/WRITE-FILE=Y", f"/CSV={second}", command="second<EOF>"),
        "same-process-second",
    )

    assert first.read_text(encoding="utf-8-sig") == "first"
    assert second.read_text(encoding="utf-8-sig") == "second"


def test_spfglobals_command_state_is_shared_across_threads(tmp_path) -> None:
    runtime_module = _runtime_module()
    first_dir = tmp_path / "thread-a"
    second_dir = tmp_path / "thread-b"
    first_dir.mkdir()
    second_dir.mkdir()
    first_configured = threading.Event()
    second_configured = threading.Event()

    def first_thread() -> str:
        manager = runtime_module.SPFManager()
        manager.gCommandLineArguments = [
            "SPFSQL3.py",
            f"/MYLOCAL={first_dir}",
            f"/EXEDIR={first_dir}",
        ]
        first_configured.set()
        assert second_configured.wait(timeout=5)
        return manager.gMyLocal

    def second_thread() -> str:
        assert first_configured.wait(timeout=5)
        manager = runtime_module.SPFManager()
        manager.gCommandLineArguments = [
            "SPFSQL3.py",
            f"/MYLOCAL={second_dir}",
            f"/EXEDIR={second_dir}",
        ]
        second_configured.set()
        return manager.gMyLocal

    with ThreadPoolExecutor(max_workers=2) as pool:
        first_future = pool.submit(first_thread)
        second_future = pool.submit(second_thread)
        first_seen = first_future.result(timeout=10)
        second_seen = second_future.result(timeout=10)

    # SPFGlobals stores command-line/runtime state at class/process scope. Once the
    # second job overwrites that state, the first manager observes the second job's
    # local directory too. This is evidence for process isolation, not a desired
    # same-process concurrency guarantee.
    assert first_seen == str(second_dir)
    assert second_seen == str(second_dir)


def _isolated_process_code(tmp_path: Path, output: Path, value: str, instance: str) -> str:
    return f"""
import sys
sys.path.insert(0, {str(SCRIPT_HOST)!r})
from SPFLib.SPFSQL3 import SPFManager

manager = SPFManager()
manager.gCommandLineArguments = [
    "SPFSQL3.py",
    "/MYLOCAL={tmp_path}",
    "/EXEDIR={tmp_path}",
    "/SPFINSTANCE={instance}",
]
manager.MySPFSQLFileData = {(
        "<OPTIONS>\n"
        "/WRITE-FILE=Y\n"
        f"/CSV={output}\n"
        "</OPTIONS>\n"
        f"{value}<EOF>"
    )!r}
if manager.Run_SPFSQL() is not True:
    raise SystemExit(2)
"""


def test_original_runtime_concurrent_subprocesses_are_isolated(tmp_path) -> None:
    first_dir = tmp_path / "process-a"
    second_dir = tmp_path / "process-b"
    first_dir.mkdir()
    second_dir.mkdir()
    first_output = first_dir / "result.txt"
    second_output = second_dir / "result.txt"

    first = subprocess.Popen(
        [
            sys.executable,
            "-c",
            _isolated_process_code(first_dir, first_output, "process-a", "process-a"),
        ],
        stdout=subprocess.PIPE,
        stderr=subprocess.PIPE,
        text=True,
    )
    second = subprocess.Popen(
        [
            sys.executable,
            "-c",
            _isolated_process_code(second_dir, second_output, "process-b", "process-b"),
        ],
        stdout=subprocess.PIPE,
        stderr=subprocess.PIPE,
        text=True,
    )
    first_stdout, first_stderr = first.communicate(timeout=30)
    second_stdout, second_stderr = second.communicate(timeout=30)

    assert first.returncode == 0, first_stdout + first_stderr
    assert second.returncode == 0, second_stdout + second_stderr
    assert first_output.read_text(encoding="utf-8-sig") == "process-a"
    assert second_output.read_text(encoding="utf-8-sig") == "process-b"
