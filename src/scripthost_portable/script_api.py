"""Python API for ScriptHost jobs: one run() with inline task calls and native control scopes.

``script`` runs one original task per call, routed by the original ScriptHost router.
``controls`` opens IF/ELSE, START-MACRO, FOR/SITE/RUN loop and BEGIN-HPC scopes. Macro and
loop substitutions apply to a call when it is reached, so text in a branch that never runs
is never checked. The launcher (worker) binds the job; scripts only define run().
"""

from __future__ import annotations

from .control_api import controls
from .runtime import _session
from .task_inputs import Options, TaskInput, option_pairs, utility_input

__all__ = ["controls", "script"]


class _Script:
    """Run one original ScriptHost task; the original router picks the task class."""

    def invoke(self, *, options: Options = None, command: str = "") -> None:
        """Task selected by its options (queries, reports, WRITE-FILE, ...)."""
        _session().run(TaskInput(option_pairs(options), command))

    def utility(
        self,
        name: str,
        *arguments,
        options: Options = None,
        external: bool = False,
        command: str = "",
    ) -> None:
        """/UTILITIES route: ``name`` is braced ({NAME}) unless ``external`` keeps an alias as written."""
        route = name if external else "{" + name + "}"
        _session().run(utility_input(route, arguments, options, command))

    def command(self, command: str, *, options: Options = None) -> None:
        """External (DOS) command line, kept exactly as written."""
        _session().run(TaskInput((("UTILITIES", command), *option_pairs(options))))


script = _Script()
