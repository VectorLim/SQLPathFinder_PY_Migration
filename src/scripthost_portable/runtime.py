from __future__ import annotations

import os
import runpy
import sys
from collections.abc import Callable, Sequence
from contextlib import contextmanager
from pathlib import Path

_SCRIPT_HOST_ROOT = Path(__file__).resolve().parent / "_vendor" / "SPSQL3_py"


def _spf_manager_type():
    root = str(_SCRIPT_HOST_ROOT)
    if root not in sys.path:
        sys.path.insert(0, root)
    from SPFLib.SPFSQL3 import SPFManager

    return SPFManager


@contextmanager
def _execution(working_directory: Path, execution_options: Sequence[str]):
    workdir = Path(working_directory).resolve(strict=False)
    workdir.mkdir(parents=True, exist_ok=True)
    manager = _spf_manager_type()()
    manager.gCommandLineArguments = [
        str(_SCRIPT_HOST_ROOT / "SPFSQL3.py"),
        f'/MYLOCAL="{workdir}"',
        "/EXECMODE=UT",
        *execution_options,
    ]
    # ScriptHost cwd, environment and globals require one job per child process.
    previous = Path.cwd()
    os.chdir(workdir)
    try:
        yield manager
    finally:
        os.chdir(previous)


class PortableScriptHostRuntime:
    """Entry into the original SPFManager/task runtime inside an isolated worker.

    The historical top-level entrypoint and SPFManager.main initialize
    Windows/service/network integrations that are not needed to assess the
    parser/controller/task engine. This entrypoint intentionally enters at
    Run_SPFSQL after supplying the same command-line-derived per-run state.
    All parsing, task construction, control flow and utility execution remain
    the original ScriptHost implementations.
    """

    def run_text(
        self,
        text: str,
        working_directory: Path,
        *,
        execution_options: Sequence[str] = (),
    ) -> bool:
        with _execution(working_directory, execution_options) as manager:
            manager.MySPFSQLFileData = text
            return bool(manager.Run_SPFSQL())

    def run_python(
        self,
        run: Callable[[], object],
        working_directory: Path,
        *,
        execution_options: Sequence[str] = (),
    ) -> bool:
        """Invoke plain run() inside the worker's hidden original runtime.

        Like run_text(), this in-process entry is not thread-safe or a job
        isolation mechanism. Use worker.run_job for fresh-process execution.
        """
        from .script_api import _bind, _check_unbound

        _check_unbound()
        with _execution(working_directory, execution_options) as manager, _bind(manager):
            try:
                run()
                return True
            finally:
                manager.Final_CleanUp(manager.TMP_F_NAME)

    def run_python_file(
        self,
        script: Path,
        working_directory: Path,
        *,
        execution_options: Sequence[str] = (),
    ) -> bool:
        path = Path(script).resolve(strict=True)

        def invoke():
            entrypoint = runpy.run_path(str(path)).get("run")
            if not callable(entrypoint):
                raise ValueError("Python ScriptHost job must define a callable run().")
            entrypoint()

        return self.run_python(invoke, working_directory, execution_options=execution_options)

    def run_file(
        self,
        script: Path,
        working_directory: Path | None = None,
        *,
        execution_options: Sequence[str] = (),
    ) -> bool:
        path = Path(script).resolve(strict=True)
        workdir = path.parent if working_directory is None else Path(working_directory)
        return self.run_text(
            path.read_text(encoding="utf-8-sig"),
            workdir,
            execution_options=execution_options,
        )
