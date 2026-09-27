from __future__ import annotations

import os
import subprocess
from concurrent.futures import ThreadPoolExecutor
from pathlib import Path

from scripthost_portable import QueryConfigurationError, use_reader_factory
from scripthost_portable.worker import ScriptHostJob, _run_child, run_job


def block(*options: str, body: str = "") -> str:
    option_text = "\n".join(options)
    return f"<OPTIONS>\n{option_text}\n</OPTIONS>\n{body}"


def _write_script(output: Path, value: str) -> str:
    return block("/WRITE-FILE=Y", f"/CSV={output}", body=value)


def test_worker_uses_one_fresh_process_per_job(tmp_path: Path) -> None:
    first_dir = tmp_path / "first"
    second_dir = tmp_path / "second"
    first = run_job(
        ScriptHostJob(
            working_directory=str(first_dir),
            script_text=_write_script(first_dir / "result.txt", "first"),
        ),
        timeout=30,
    )
    second = run_job(
        ScriptHostJob(
            working_directory=str(second_dir),
            script_text=_write_script(second_dir / "result.txt", "second"),
        ),
        timeout=30,
    )

    assert first.success, first
    assert second.success, second
    assert first.child_pid is not None
    assert second.child_pid is not None
    assert first.child_pid != second.child_pid
    assert "result.txt" in first.generated_outputs
    assert "result.txt" in second.generated_outputs
    assert (first_dir / "result.txt").read_text(encoding="utf-8") == "first"
    assert (second_dir / "result.txt").read_text(encoding="utf-8") == "second"


def test_parallel_workers_do_not_share_cwd_or_outputs(tmp_path: Path) -> None:
    jobs = []
    for label in ("alpha", "beta"):
        root = tmp_path / label
        jobs.append(
            ScriptHostJob(
                working_directory=str(root),
                script_text=_write_script(root / "result.txt", label),
            )
        )

    parent_cwd = Path.cwd()
    with ThreadPoolExecutor(max_workers=2) as executor:
        results = list(executor.map(lambda job: run_job(job, timeout=30), jobs))

    assert all(result.success for result in results), results
    assert len({result.child_pid for result in results}) == 2
    assert Path.cwd() == parent_cwd
    assert (tmp_path / "alpha" / "result.txt").read_text(encoding="utf-8") == "alpha"
    assert (tmp_path / "beta" / "result.txt").read_text(encoding="utf-8") == "beta"


def test_worker_child_environment_changes_do_not_leak_to_parent(tmp_path: Path) -> None:
    variable = "SESSION_26A_CHILD_ONLY"
    os.environ.pop(variable, None)
    root = tmp_path / "env"
    root.mkdir()
    source = root / "rows.csv"
    source.write_text("id\n1\n", encoding="utf-8")
    text = block(f'/UTILITIES={{ROWS-IN-FILE}} "{source}" "{variable}" "N"')

    result = run_job(ScriptHostJob(working_directory=str(root), script_text=text), timeout=30)

    assert result.success, result
    assert variable not in os.environ


def test_worker_surfaces_datasyncx_configuration_failure(tmp_path: Path) -> None:
    class BrokenFactory:
        def reader_for(self, backend: str, node: str):
            raise QueryConfigurationError(f"bad configuration for {backend}:{node}")

    root = tmp_path / "config-failure"
    text = block(
        "/NODE=KM.MARS",
        "/UN=//",
        "/PW=",
        "/OLEDB=SQLPlus",
        "/ENGINE=VA",
        "/WORKDIR=.\\",
        "/T=",
        "/CSV=ignored.tab",
        "/HEADERS=x",
        body="/*BEGIN SQL*/ SELECT 1 AS x FROM dual /*END SQL*/",
    )

    with use_reader_factory(BrokenFactory()):
        result = _run_child(ScriptHostJob(working_directory=str(root), script_text=text))

    assert not result.success
    assert result.error_category == "datasyncx_configuration_failure"
    assert "bad configuration" in result.message


def test_worker_surfaces_structured_script_failure(tmp_path: Path) -> None:
    root = tmp_path / "failure"
    text = block(
        "/NODE=KM.UNKNOWN",
        "/UN=//",
        "/PW=",
        "/OLEDB=SQLPlus",
        "/ENGINE=VA",
        "/WORKDIR=.\\",
        "/T=",
        "/CSV=ignored.tab",
        "/HEADERS=x",
        body="/*BEGIN SQL*/ SELECT 1 AS x FROM dual /*END SQL*/",
    )

    result = run_job(ScriptHostJob(working_directory=str(root), script_text=text), timeout=30)

    assert not result.success
    assert result.error_category == "unsupported_legacy_integration"
    assert "KM.UNKNOWN" in result.message
    assert result.child_pid is not None


def test_worker_surfaces_timeout(monkeypatch, tmp_path: Path) -> None:
    def timeout(*args, **kwargs):
        del args, kwargs
        raise subprocess.TimeoutExpired(cmd=["python"], timeout=0.01)

    monkeypatch.setattr(subprocess, "run", timeout)
    result = run_job(
        ScriptHostJob(
            working_directory=str(tmp_path),
            script_text=_write_script(tmp_path / "never.txt", "never"),
        ),
        timeout=0.01,
    )

    assert not result.success
    assert result.error_category == "child_timeout"
    assert "0.01" in result.message


def test_worker_surfaces_child_crash(monkeypatch, tmp_path: Path) -> None:
    def crashed(*args, **kwargs):
        del args, kwargs
        return subprocess.CompletedProcess(
            args=["python"], returncode=9, stdout="partial output", stderr="boom"
        )

    monkeypatch.setattr(subprocess, "run", crashed)
    result = run_job(
        ScriptHostJob(
            working_directory=str(tmp_path),
            script_text=_write_script(tmp_path / "never.txt", "never"),
        ),
        timeout=30,
    )

    assert not result.success
    assert result.error_category == "child_crash"
    assert result.exit_code == 9
    assert result.stdout == "partial output"
    assert result.stderr == "boom"
