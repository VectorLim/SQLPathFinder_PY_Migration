from __future__ import annotations

import importlib
import subprocess
import sys
import threading
import time
from concurrent.futures import ThreadPoolExecutor
from pathlib import Path

import pytest

from vg2c_new.parser import parse as parse_vg2c_new
from vg2c_new.runtime import Interpreter as NewInterpreter
from vg2c_new.runtime import RuntimeState as NewRuntimeState
from vg2c_new.utilities.files import WriteFileUtility as NewWriteFileUtility

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
    payload = (
        "<OPTIONS>\n"
        "/WRITE-FILE=Y\n"
        f"/CSV={output}\n"
        "</OPTIONS>\n"
        f"{value}<EOF>"
    )
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
manager.MySPFSQLFileData = {payload!r}
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



def _representative_script(root: Path) -> str:
    return DELIMITER.join(
        [
            _task(
                "/WRITE-FILE=Y",
                f"/CSV={root / 'initial.txt'}",
                command="initial<EOF>",
            ),
            _task(f'/UTILITIES={{START-MACRO}} "{root / "macro.csv"}"'),
            _task(
                "/WRITE-FILE=Y",
                f"/CSV={root / 'macro.txt'}",
                command="<<<value>>><EOF>",
            ),
            _task("/UTILITIES={END-MACRO}"),
            _task('/UTILITIES={IF-THEN} "VAR(1)" "EQ" "1"'),
            _task(
                "/WRITE-FILE=Y",
                f"/CSV={root / 'if-true.txt'}",
                command="if-branch<EOF>",
            ),
            _task("/UTILITIES={ELSE}"),
            _task(
                "/WRITE-FILE=Y",
                f"/CSV={root / 'if-false.txt'}",
                command="else-branch<EOF>",
            ),
            _task("/UTILITIES={END-IF}"),
            _task('/UTILITIES={FOR-LOOP} "0" "2" "1" "T" "N"'),
            _task(
                "/WRITE-FILE=Y",
                f"/CSV={root}/loop-<<<spf-loop-ctr-T-int>>>.txt",
                command="loop-<<<spf-loop-ctr-T-int>>><EOF>",
            ),
            _task("/UTILITIES={END-LOOP}"),
        ]
    )


def _representative_outputs(root: Path) -> dict[str, str | None]:
    names = [
        "initial.txt",
        "macro.txt",
        "if-true.txt",
        "if-false.txt",
        "loop-0.txt",
        "loop-1.txt",
        "loop-2.txt",
    ]
    return {
        name: (
            (root / name).read_text(encoding="utf-8-sig")
            if (root / name).exists()
            else None
        )
        for name in names
    }


def test_original_runtime_matches_vg2c_new_on_representative_portable_slice(tmp_path) -> None:
    original_root = tmp_path / "original"
    new_root = tmp_path / "vg2c-new"
    original_root.mkdir()
    new_root.mkdir()
    for root in (original_root, new_root):
        (root / "macro.csv").write_text("value\nmacro-expanded\n", encoding="utf-8")

    _run_original_runtime(
        original_root,
        _representative_script(original_root),
        "portable-original-parity",
    )

    commands = parse_vg2c_new(_representative_script(new_root))
    NewInterpreter({"write_file": NewWriteFileUtility()}).execute(
        commands,
        NewRuntimeState(new_root),
    )

    assert _representative_outputs(original_root) == _representative_outputs(new_root)
    assert _representative_outputs(original_root) == {
        "initial.txt": "initial",
        "macro.txt": "macro-expanded",
        "if-true.txt": "if-branch",
        "if-false.txt": None,
        "loop-0.txt": "loop-0",
        "loop-1.txt": "loop-1",
        "loop-2.txt": "loop-2",
    }



def test_spfglobals_instance_cache_survives_command_argument_reset(tmp_path) -> None:
    runtime_module = _runtime_module()
    first = runtime_module.SPFManager()
    first.gSPFInstance = "first-job-instance"

    second = runtime_module.SPFManager()
    second.gCommandLineArguments = [
        "SPFSQL3.py",
        f"/MYLOCAL={tmp_path}",
        f"/EXEDIR={tmp_path}",
        "/SPFINSTANCE=second-job-instance",
    ]

    # __reInitStaticPropsDueToCmdUpdate resets many fields, but not __gSPFInstance.
    # A long-lived worker therefore does not provide complete job isolation merely
    # by replacing gCommandLineArguments between jobs.
    assert second.gSPFInstance == "first-job-instance"


def test_original_runtime_retains_historical_for_loop_branch_that_vg2c_new_rejects(
    tmp_path,
) -> None:
    original_output = tmp_path / "historical-loop-0.txt"
    original_script = DELIMITER.join(
        [
            _task('/UTILITIES={FOR-LOOP} "0" "2" "1" "H" "N" "Y"'),
            _task(
                "/WRITE-FILE=Y",
                f"/CSV={tmp_path}/historical-loop-<<<spf-loop-ctr-H-int>>>.txt",
                command="historical-<<<spf-loop-ctr-H-int>>><EOF>",
            ),
            _task("/UTILITIES={END-LOOP}"),
        ]
    )

    _run_original_runtime(tmp_path, original_script, "historical-loop-original")
    assert original_output.read_text(encoding="utf-8-sig") == "historical-0"
    assert not (tmp_path / "historical-loop-1.txt").exists()

    new_script = DELIMITER.join(
        [
            _task('/UTILITIES={FOR-LOOP} "0" "2" "1" "H" "N" "Y"'),
            _task(
                "/WRITE-FILE=Y",
                f"/CSV={tmp_path}/new-historical-<<<spf-loop-ctr-H-int>>>.txt",
                command="historical-<<<spf-loop-ctr-H-int>>><EOF>",
            ),
            _task("/UTILITIES={END-LOOP}"),
        ]
    )
    commands = parse_vg2c_new(new_script)
    with pytest.raises(RuntimeError, match="Historical .* version selection"):
        NewInterpreter({"write_file": NewWriteFileUtility()}).execute(
            commands,
            NewRuntimeState(tmp_path),
        )



def test_missing_legacy_database_transport_fails_only_when_invoked() -> None:
    runtime_module = _runtime_module()

    assert runtime_module.SPFManager is not None
    assert runtime_module.dbDrivers is None
    with pytest.raises(RuntimeError, match="database-driver integration is unavailable"):
        runtime_module.dbDriverBase()


def test_original_runtime_executes_site_loop_on_linux(tmp_path) -> None:
    script = DELIMITER.join(
        [
            _task('/UTILITIES={SITE-LOOP} "KM.MARS,PG.MARS"'),
            _task(
                "/WRITE-FILE=Y",
                f"/CSV={tmp_path}/site-<<<spf-site>>>.txt",
                command="<<<spf-site>>><EOF>",
            ),
            _task("/UTILITIES={END-LOOP}"),
        ]
    )

    _run_original_runtime(tmp_path, script, "portable-original-site-loop")

    assert (tmp_path / "site-KM.MARS.txt").read_text(encoding="utf-8-sig") == "KM.MARS"
    assert (tmp_path / "site-PG.MARS.txt").read_text(encoding="utf-8-sig") == "PG.MARS"


def test_original_runtime_executes_run_loop_on_linux(tmp_path) -> None:
    input_path = tmp_path / "run-loop-input.csv"
    output_path = tmp_path / "run-loop-chunk.csv"
    marker_path = tmp_path / "run-loop-marker.txt"
    input_path.write_text("id,value\n1,a\n2,b\n3,c\n", encoding="utf-8")
    script = DELIMITER.join(
        [
            _task(f'/UTILITIES={{RUN-LOOP}} "{input_path}" "{output_path}" "2" "N"'),
            _task(
                "/WRITE-FILE=Y",
                f"/CSV={marker_path}",
                command="child-ran<EOF>",
            ),
            _task("/UTILITIES={END-LOOP}"),
        ]
    )

    _run_original_runtime(tmp_path, script, "portable-original-run-loop")

    assert marker_path.read_text(encoding="utf-8-sig") == "child-ran"
    assert output_path.read_text(encoding="utf-8-sig").replace("\r\n", "\n") == "id,value\n3,c\n"


def test_fresh_process_job_overhead_is_bounded_on_linux(tmp_path) -> None:
    durations: list[float] = []
    for index in range(3):
        job_dir = tmp_path / f"startup-{index}"
        job_dir.mkdir()
        output = job_dir / "result.txt"
        started = time.perf_counter()
        completed = subprocess.run(
            [
                sys.executable,
                "-c",
                _isolated_process_code(job_dir, output, f"job-{index}", f"startup-{index}"),
            ],
            capture_output=True,
            text=True,
            timeout=30,
            check=False,
        )
        durations.append(time.perf_counter() - started)
        assert completed.returncode == 0, completed.stdout + completed.stderr
        assert output.read_text(encoding="utf-8-sig") == f"job-{index}"

    # This is deliberately a generous feasibility ceiling rather than a
    # production SLA. The GitHub Actions --durations output records the actual
    # observed cost for the architecture assessment.
    assert sum(durations) < 15.0



def test_windows_identity_path_fails_only_when_invoked_on_linux() -> None:
    runtime_module = _runtime_module()
    manager = runtime_module.SPFManager()

    assert runtime_module.SPFManager is not None
    with pytest.raises(RuntimeError, match="Windows identity integration is unavailable"):
        _ = manager.gUN



def test_actual_script_fixture_parses_through_both_runtime_resolvers(tmp_path) -> None:
    fixture = Path(__file__).resolve().parents[1] / "fixtures" / "actual_script.txt"
    text = fixture.read_text(encoding="utf-8-sig")

    runtime_module = _runtime_module()
    manager = runtime_module.SPFManager()
    manager.gCommandLineArguments = [
        "SPFSQL3.py",
        f"/MYLOCAL={tmp_path}",
        f"/EXEDIR={tmp_path}",
        "/SPFINSTANCE=actual-script-parse",
    ]
    segments = text.split(manager.SQLFILE_DELIM)
    original_tasks = manager.Process_Query(
        0,
        len(segments),
        segments,
        manager.gMyLocal,
        manager.gMyEXEDir,
        None,
        len(segments),
        manager.gRNStr,
        manager.TMP_F_NAME,
        None,
        False,
    )
    modern_commands = parse_vg2c_new(text, source=fixture)

    assert len(original_tasks) > 10
    assert len(modern_commands) > 10
    assert [type(task).__name__ for task in original_tasks[:3]] == [
        "HTMLRunTask",
        "HTMLLayoutTask",
        "HTMLDeleteTask",
    ]
    assert [command.utility_type for command in modern_commands[:3]] == [
        "report.html_run",
        "report.html_layout",
        "report.delete",
    ]
