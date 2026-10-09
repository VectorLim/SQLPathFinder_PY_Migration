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


class _Session:
    """One running Python job: the original manager and the controller bodies now executing."""

    def __init__(self, manager):
        self.manager = manager
        # (substitution layers, error handler) of each open controller body, innermost last
        self.bodies = []

    @property
    def layers(self) -> tuple:
        return self.bodies[-1][0] if self.bodies else ()

    def task(self, task_input):
        """Original task object for one statement, routed as Process_Query routes a block."""
        manager = self.manager
        text = manager.Substitute_Global_Var(task_input.encode())
        task = manager.GetQuery(manager.gMyLocal, text, 0, False)
        task.RNStr = manager.gRNStr
        return task

    def prepare(self, task) -> None:
        """Apply the substitutions the enclosing controllers apply to their children."""
        for layer in self.layers:
            layer([task])

    def fail(self, error: Exception) -> None:
        """Enclosing body's error policy: return to continue with the next statement, else raise."""
        if not self.bodies:
            self.manager.handleRootTaskError(error)
        elif self.bodies[-1][1] is None:
            raise error
        else:
            self.bodies[-1][1](error)

    def run(self, task_input) -> None:
        """Execute one statement through the original router, task and error policy."""
        try:
            task = self.task(task_input)
            self.prepare(task)
            task.execute()
        except Exception as error:
            self.fail(error)


# ponytail: process-global binding; use the existing fresh-child worker per job.
_current_session: _Session | None = None


def _session() -> _Session:
    if _current_session is None:
        raise RuntimeError(
            "Script API requires a job invoked through the ScriptHost runtime."
        )
    return _current_session


@contextmanager
def _bind(manager):
    global _current_session
    if _current_session is not None:
        raise RuntimeError(
            "A Python ScriptHost job is already running in this process."
        )
    _current_session = _Session(manager)
    try:
        yield
    finally:
        _current_session = None


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
        if _current_session is not None:
            raise RuntimeError(
                "A Python ScriptHost job is already running in this process."
            )
        with (
            _execution(working_directory, execution_options) as manager,
            _bind(manager),
        ):
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

        return self.run_python(
            invoke, working_directory, execution_options=execution_options
        )

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
