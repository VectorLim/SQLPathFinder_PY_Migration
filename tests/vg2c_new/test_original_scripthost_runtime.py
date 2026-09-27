from __future__ import annotations

import os
import subprocess
import sys
from concurrent.futures import ThreadPoolExecutor
from pathlib import Path

import pytest

from scripthost_portable import PortableScriptHostRuntime
from vg2c_new.parser import parse
from vg2c_new.runtime import Interpreter, RuntimeState
from vg2c_new.utilities.file_values import RowsInFileUtility
from vg2c_new.utilities.files import WriteFileUtility

DELIM = "<---- New Query ---->"


def block(*options: str, body: str = "") -> str:
    return f"<OPTIONS>\n{'\n'.join(options)}\n</OPTIONS>\n{body}"


def script(*blocks: str) -> str:
    return f"\n{DELIM}\n".join(blocks)


def vertical_slice(root: Path) -> str:
    root.mkdir(parents=True, exist_ok=True)
    macro = root / "macro.csv"
    macro.write_text("name,flag\nalpha,1\nbeta,0\n", encoding="utf-8")
    output_pattern = root / "out_<<<name>>>_<<<spf-loop-ctr-x-int>>>.csv"
    first_output = root / "out_alpha_0.csv"
    verified = root / "verified.txt"
    bad = root / "bad.txt"
    return script(
        block(
            "/WRITE-FILE=Y",
            f"/CSV={root / 'seed.csv'}",
            body="id,value\n1,a\n2,b\n<EOF>ignored",
        ),
        block(f'/UTILITIES={{START-MACRO}} "{macro}" "N"'),
        block('/UTILITIES={IF-THEN} "VAR(<<<flag>>>)" "GT" "0"'),
        block('/UTILITIES={FOR-LOOP} "0" "2" "1" "x" "N"'),
        block(
            "/WRITE-FILE=Y",
            f"/CSV={output_pattern}",
            body="name,loop\n<<<name>>>,<<<spf-loop-ctr-x-int>>>\n",
        ),
        block("/UTILITIES={END-LOOP}"),
        block("/UTILITIES={ELSE}"),
        block("/WRITE-FILE=Y", f"/CSV={bad}", body="wrong branch"),
        block("/UTILITIES={END-IF}"),
        block("/UTILITIES={END-MACRO}"),
        block(f'/UTILITIES={{ROWS-IN-FILE}} "{first_output}" "PORTABLE_SPFS_ROWS" "N"'),
        block('/UTILITIES={IF-THEN} "PORTABLE_SPFS_ROWS" "GT" "0"'),
        block("/WRITE-FILE=Y", f"/CSV={verified}", body="verified"),
        block("/UTILITIES={END-IF}"),
    )


def outputs(root: Path) -> dict[str, str]:
    names = ["seed.csv", "out_alpha_0.csv", "out_alpha_1.csv", "out_alpha_2.csv", "verified.txt"]
    return {name: (root / name).read_text(encoding="utf-8") for name in names}


def run_new(text: str, root: Path) -> RuntimeState:
    state = RuntimeState(root)
    Interpreter(
        {
            "write_file": WriteFileUtility(),
            "rows_in_file": RowsInFileUtility(),
        }
    ).execute(parse(text), state)
    return state


def test_original_runtime_vertical_slice_matches_vg2c_new(tmp_path: Path) -> None:
    original_dir = tmp_path / "original"
    new_dir = tmp_path / "new"
    original_text = vertical_slice(original_dir)
    new_text = vertical_slice(new_dir)

    try:
        assert PortableScriptHostRuntime().run_text(original_text, original_dir)
        state = run_new(new_text, new_dir)

        original_outputs = outputs(original_dir)
        new_outputs = outputs(new_dir)
        assert {name: value.rstrip("\n") for name, value in original_outputs.items()} == {
            name: value.rstrip("\n") for name, value in new_outputs.items()
        }
        # Real parity gap: ScriptHost keeps the block's separator newline while
        # vg2c_new trims the outer blank line before WRITE-FILE execution.
        assert original_outputs["out_alpha_0.csv"].endswith("\n\n")
        assert new_outputs["out_alpha_0.csv"].endswith("\n")
        assert original_outputs["verified.txt"] == "verified\n"
        assert new_outputs["verified.txt"] == "verified"
        assert not (original_dir / "bad.txt").exists()
        assert not (new_dir / "bad.txt").exists()
        assert os.environ["PORTABLE_SPFS_ROWS"] == "1"
        assert state.lookup("PORTABLE_SPFS_ROWS") == "1"
    finally:
        os.environ.pop("PORTABLE_SPFS_ROWS", None)


def test_original_runtime_task_tree_repeats_sequentially(tmp_path: Path) -> None:
    workdir = tmp_path / "repeat"
    text = vertical_slice(workdir)
    runtime = PortableScriptHostRuntime()
    try:
        assert runtime.run_text(text, workdir)
        first = outputs(workdir)
        assert runtime.run_text(text, workdir)
        assert outputs(workdir) == first
        assert not (workdir / "bad.txt").exists()
    finally:
        os.environ.pop("PORTABLE_SPFS_ROWS", None)


def test_original_runtime_process_environment_is_not_per_run_state(tmp_path: Path) -> None:
    os.environ.pop("PORTABLE_SPFS_LEAK", None)
    workdir = tmp_path / "environment-leak"
    source = workdir / "rows.csv"
    marker = workdir / "leaked.txt"
    workdir.mkdir(parents=True)
    source.write_text("id\n1\n", encoding="utf-8")
    set_env = script(block(f'/UTILITIES={{ROWS-IN-FILE}} "{source}" "PORTABLE_SPFS_LEAK" "N"'))
    observe_env = script(
        block('/UTILITIES={IF-THEN} "PORTABLE_SPFS_LEAK" "GT" "0"'),
        block("/WRITE-FILE=Y", f"/CSV={marker}", body="leaked"),
        block("/UTILITIES={END-IF}"),
    )

    runtime = PortableScriptHostRuntime()
    try:
        assert runtime.run_text(set_env, workdir)
        assert os.environ["PORTABLE_SPFS_LEAK"] == "1"
        assert runtime.run_text(observe_env, workdir)
        assert marker.read_text(encoding="utf-8") == "leaked\n"
    finally:
        os.environ.pop("PORTABLE_SPFS_LEAK", None)


def test_spfglobals_command_line_state_is_shared_between_threads() -> None:
    from SPFLib.SPFSQL3 import SPFManager

    import threading

    barrier = threading.Barrier(2)

    def worker(label: str) -> tuple[str, str]:
        manager = SPFManager()
        manager.gCommandLineArguments = [label]
        barrier.wait()
        return label, manager.gCommandLineArguments[0]

    with ThreadPoolExecutor(max_workers=2) as executor:
        observed = list(executor.map(worker, ("THREAD_A", "THREAD_B")))

    assert len({value for _, value in observed}) == 1
    assert any(expected != value for expected, value in observed)


def test_original_runtime_is_safe_when_concurrent_jobs_use_process_isolation(
    tmp_path: Path,
) -> None:
    repo_root = Path(__file__).resolve().parents[2]
    jobs: list[tuple[Path, Path]] = []
    for name in ("job-a", "job-b"):
        root = tmp_path / name
        vg2 = root / "job.spfsql"
        text = vertical_slice(root)
        vg2.write_text(text, encoding="utf-8")
        jobs.append((vg2, root))

    code = (
        "from pathlib import Path; "
        "from scripthost_portable import PortableScriptHostRuntime; "
        "import sys; "
        "raise SystemExit(0 if PortableScriptHostRuntime().run_file("
        "Path(sys.argv[1]), Path(sys.argv[2])) else 1)"
    )
    child_env = dict(os.environ)
    child_env.pop("PORTABLE_SPFS_ROWS", None)
    child_env["PYTHONPATH"] = os.pathsep.join(
        [
            str(repo_root / "src"),
            str(repo_root),
            child_env.get("PYTHONPATH", ""),
        ]
    )

    def run_job(job: tuple[Path, Path]) -> subprocess.CompletedProcess[str]:
        vg2, root = job
        return subprocess.run(
            [sys.executable, "-c", code, str(vg2), str(root)],
            cwd=repo_root,
            env=child_env,
            text=True,
            capture_output=True,
            timeout=30,
            check=False,
        )

    with ThreadPoolExecutor(max_workers=2) as executor:
        results = list(executor.map(run_job, jobs))

    assert [result.returncode for result in results] == [0, 0], [
        result.stdout + result.stderr for result in results
    ]
    for _, root in jobs:
        assert outputs(root)["verified.txt"] == "verified\n"
        assert not (root / "bad.txt").exists()
    assert "PORTABLE_SPFS_ROWS" not in os.environ


def test_windows_db_transport_fails_only_when_invoked() -> None:
    from SPFLib.SPFSQL3 import SPFManager, dbDriverBase

    assert SPFManager is not None
    with pytest.raises(RuntimeError, match="database transport is unavailable"):
        dbDriverBase()
