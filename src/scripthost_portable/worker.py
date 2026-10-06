from __future__ import annotations

import argparse
import contextlib
import io
import json
import os
import subprocess
import sys
from dataclasses import asdict, dataclass
from pathlib import Path
from typing import Any

from .query_transport import (
    QueryConfigurationError,
    QueryExecutionError,
    QueryTransportUnavailable,
    UnsupportedQueryBackend,
)
from .runtime import PortableScriptHostRuntime

_RESULT_MARKER = "__SCRIPTHOST_JOB_RESULT__="
_CONFIG_REFERENCE_ENV = "SCRIPTHOST_DATASYNCX_CONFIG_REFERENCE"


@dataclass(frozen=True, slots=True)
class ScriptHostJob:
    working_directory: str
    script_path: str | None = None
    script_text: str | None = None
    execution_options: tuple[str, ...] = ()
    transport_config_reference: str | None = None

    def validate(self) -> None:
        if (self.script_path is None) == (self.script_text is None):
            raise ValueError("Exactly one of script_path or script_text must be supplied.")
        if not self.working_directory:
            raise ValueError("working_directory must be supplied.")


@dataclass(frozen=True, slots=True)
class ScriptHostJobResult:
    success: bool
    error_category: str | None
    message: str
    exit_code: int | None
    child_pid: int | None
    generated_outputs: tuple[str, ...] = ()
    stdout: str = ""
    stderr: str = ""


def run_job(job: ScriptHostJob, *, timeout: float | None = None) -> ScriptHostJobResult:
    """Execute exactly one ScriptHost job in one fresh child process."""
    job.validate()
    request = asdict(job)
    env = dict(os.environ)
    try:
        completed = subprocess.run(
            [sys.executable, "-m", "scripthost_portable.worker", "--child"],
            input=json.dumps(request),
            text=True,
            capture_output=True,
            env=env,
            timeout=timeout,
            check=False,
        )
    except subprocess.TimeoutExpired as exc:
        return ScriptHostJobResult(
            success=False,
            error_category="child_timeout",
            message=f"ScriptHost child exceeded timeout of {timeout} seconds.",
            exit_code=None,
            child_pid=None,
            stdout=_to_text(exc.stdout),
            stderr=_to_text(exc.stderr),
        )

    payload = _extract_result(completed.stdout)
    if payload is None:
        return ScriptHostJobResult(
            success=False,
            error_category="child_crash",
            message=(
                "ScriptHost child exited without a structured result "
                f"(exit code {completed.returncode})."
            ),
            exit_code=completed.returncode,
            child_pid=None,
            stdout=completed.stdout,
            stderr=completed.stderr,
        )

    result = _result_from_payload(payload)
    if completed.stderr:
        result = ScriptHostJobResult(
            **{**asdict(result), "stderr": result.stderr + completed.stderr}
        )
    return result


def _run_child(job: ScriptHostJob) -> ScriptHostJobResult:
    job.validate()
    workdir = Path(job.working_directory).resolve(strict=False)
    workdir.mkdir(parents=True, exist_ok=True)
    before = _snapshot(workdir)
    stdout = io.StringIO()
    stderr = io.StringIO()

    if job.transport_config_reference:
        os.environ[_CONFIG_REFERENCE_ENV] = job.transport_config_reference

    try:
        with contextlib.redirect_stdout(stdout), contextlib.redirect_stderr(stderr):
            from .aed_api import prepare_job

            script_text = job.script_text
            if script_text is None:
                script_text = Path(job.script_path or "").read_text(encoding="utf-8-sig")
            script_text = prepare_job(workdir, script_text, job.script_path)
            runtime = PortableScriptHostRuntime()
            succeeded = runtime.run_text(
                script_text,
                workdir,
                execution_options=job.execution_options,
            )
        if not succeeded:
            return _failure(
                "script_host_error",
                "Original ScriptHost runtime returned an unsuccessful status.",
                workdir,
                before,
                stdout,
                stderr,
            )
        return ScriptHostJobResult(
            success=True,
            error_category=None,
            message="ScriptHost job completed.",
            exit_code=0,
            child_pid=os.getpid(),
            generated_outputs=_changed_paths(workdir, before),
            stdout=stdout.getvalue(),
            stderr=stderr.getvalue(),
        )
    except QueryTransportUnavailable as exc:
        category = "transport_unavailable"
        return _failure(category, str(exc), workdir, before, stdout, stderr)
    except QueryConfigurationError as exc:
        return _failure(
            "datasyncx_configuration_failure",
            str(exc),
            workdir,
            before,
            stdout,
            stderr,
        )
    except UnsupportedQueryBackend as exc:
        return _failure("unsupported_legacy_integration", str(exc), workdir, before, stdout, stderr)
    except QueryExecutionError as exc:
        return _failure("query_execution_failure", str(exc), workdir, before, stdout, stderr)
    except Exception as exc:
        return _failure(
            "script_host_error",
            f"{type(exc).__name__}: {exc}",
            workdir,
            before,
            stdout,
            stderr,
        )


def _failure(
    category: str,
    message: str,
    workdir: Path,
    before: dict[str, tuple[int, int]],
    stdout: io.StringIO,
    stderr: io.StringIO,
) -> ScriptHostJobResult:
    return ScriptHostJobResult(
        success=False,
        error_category=category,
        message=message,
        exit_code=1,
        child_pid=os.getpid(),
        generated_outputs=_changed_paths(workdir, before),
        stdout=stdout.getvalue(),
        stderr=stderr.getvalue(),
    )


def _snapshot(root: Path) -> dict[str, tuple[int, int]]:
    snapshot: dict[str, tuple[int, int]] = {}
    for path in root.rglob("*"):
        if path.is_file():
            stat = path.stat()
            snapshot[str(path.relative_to(root))] = (stat.st_mtime_ns, stat.st_size)
    return snapshot


def _changed_paths(root: Path, before: dict[str, tuple[int, int]]) -> tuple[str, ...]:
    after = _snapshot(root)
    return tuple(sorted(name for name, state in after.items() if before.get(name) != state))


def _extract_result(stdout: str) -> dict[str, Any] | None:
    for line in reversed(stdout.splitlines()):
        if line.startswith(_RESULT_MARKER):
            try:
                payload = json.loads(line[len(_RESULT_MARKER) :])
            except json.JSONDecodeError:
                return None
            return payload if isinstance(payload, dict) else None
    return None


def _result_from_payload(payload: dict[str, Any]) -> ScriptHostJobResult:
    return ScriptHostJobResult(
        success=bool(payload.get("success")),
        error_category=payload.get("error_category"),
        message=str(payload.get("message", "")),
        exit_code=payload.get("exit_code"),
        child_pid=payload.get("child_pid"),
        generated_outputs=tuple(payload.get("generated_outputs", ())),
        stdout=str(payload.get("stdout", "")),
        stderr=str(payload.get("stderr", "")),
    )


def _to_text(value: str | bytes | None) -> str:
    if value is None:
        return ""
    return value.decode(errors="replace") if isinstance(value, bytes) else value


def main(argv: list[str] | None = None) -> int:
    parser = argparse.ArgumentParser(description="One-job-per-process ScriptHost worker")
    parser.add_argument("--child", action="store_true")
    args = parser.parse_args(argv)
    if not args.child:
        parser.error("worker module is an internal child-process entrypoint")

    try:
        raw = json.load(sys.stdin)
        raw["execution_options"] = tuple(raw.get("execution_options", ()))
        job = ScriptHostJob(**raw)
        result = _run_child(job)
    except Exception as exc:
        result = ScriptHostJobResult(
            success=False,
            error_category="child_protocol_error",
            message=f"{type(exc).__name__}: {exc}",
            exit_code=2,
            child_pid=os.getpid(),
        )
    print(_RESULT_MARKER + json.dumps(asdict(result), sort_keys=True))
    return 0


if __name__ == "__main__":
    raise SystemExit(main())
