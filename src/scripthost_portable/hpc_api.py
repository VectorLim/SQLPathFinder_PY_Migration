"""BEGIN-HPC scope: Python declares the remote steps; the original BeginHPCTask runs them.

Statements inside the scope are recorded, not executed. On exit the recorded inputs are
built into original task objects by SPFManager.Process_Query, exactly as for a VG2 file,
and attached as the BeginHPCTask children. Its LOCAL or service execution is unchanged.
"""

from __future__ import annotations

from .runtime import _session
from .task_inputs import Options, TaskInput, option_pairs, utility_input


class _Block:
    """A remote controller: its header on entry and its closing task on exit."""

    def __init__(self, steps: list, header: TaskInput, closer: TaskInput):
        self._steps, self._header, self._closer = steps, header, closer

    def __enter__(self):
        self._steps.append(self._header)
        return self

    def __exit__(self, exc_type, error, traceback) -> None:
        self._steps.append(self._closer)


class RemoteScope:
    """Declarations for the remote job; mirrors script.* and controls.* without running them."""

    def __init__(self, arguments, options: Options, end_options: Options):
        self._header = utility_input("{BEGIN-HPC}", arguments, options)
        self._closer = utility_input("{END-HPC}", (), end_options)
        self._steps: list[TaskInput] = []

    def invoke(self, *, options: Options = None, command: str = "") -> None:
        self._steps.append(TaskInput(option_pairs(options), command))

    def utility(self, name: str, *arguments, options: Options = None, external: bool = False,
                command: str = "") -> None:
        self._steps.append(utility_input(name if external else "{" + name + "}", arguments, options, command))

    def command(self, command: str, *, options: Options = None) -> None:
        self._steps.append(TaskInput((("UTILITIES", command), *option_pairs(options))))

    def _block(self, route, arguments, options, end_route, end_options) -> _Block:
        return _Block(self._steps, utility_input(route, arguments, options), utility_input(end_route, (), end_options))

    def if_then(self, *arguments, options: Options = None, end_options: Options = None) -> _Block:
        return self._block("{IF-THEN}", arguments, options, "{END-IF}", end_options)

    def else_branch(self, *, options: Options = None) -> None:
        self._steps.append(utility_input("{ELSE}", (), options))

    def macro(self, *arguments, options: Options = None, end_options: Options = None) -> _Block:
        return self._block("{START-MACRO}", arguments, options, "{END-MACRO}", end_options)

    def for_loop(self, *arguments, options: Options = None, end_options: Options = None) -> _Block:
        return self._block("{FOR-LOOP}", arguments, options, "{END-LOOP}", end_options)

    def site_loop(self, *arguments, options: Options = None, end_options: Options = None) -> _Block:
        return self._block("{SITE-LOOP}", arguments, options, "{END-LOOP}", end_options)

    def run_loop(self, *arguments, options: Options = None, end_options: Options = None) -> _Block:
        return self._block("{RUN-LOOP}", arguments, options, "{END-LOOP}", end_options)

    def __enter__(self):
        self._session = _session()
        return self

    def __exit__(self, exc_type, error, traceback) -> bool:
        if error is not None:
            return False
        session = self._session
        try:
            task = session.task(self._header)
            manager = session.manager
            items = [manager.Substitute_Global_Var(step.encode()) for step in self._steps]
            task.childTasksList = manager.Process_Query(
                0, len(items), items, manager.gMyLocal, "", None, len(items), manager.gRNStr, "", "")
            session.prepare(task)
            task.execute()
        except Exception as failure:
            session.fail(failure)
        session.run(self._closer)
        return False
