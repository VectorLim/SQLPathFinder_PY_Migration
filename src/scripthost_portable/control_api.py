"""Python scopes over the original ScriptHost controllers.

Each scope builds the original controller with a DeferredChildTask child and drives its
executeSteps(). Every slot the controller yields is one Python body: a branch or one loop
iteration. Statements in that body get the slot's substitutions and child-error policy.
"""

from __future__ import annotations

from .deferred_task import DeferredChildTask, MacroLayer, legacy
from .hpc_api import RemoteScope
from .runtime import _session
from .task_inputs import Options, utility_input

__all__ = ["controls"]


class _Control:
    _end_route: str | None = None

    def __init__(self, route: str, arguments, options: Options, end_options: Options):
        self._input = utility_input(route, arguments, options)
        self._end_input = (
            utility_input(self._end_route, (), end_options) if self._end_route else None
        )
        self._session = self._controller = self._steps = self._slot = None
        self._started = False

    def _children(self) -> list:
        self._body = DeferredChildTask()
        return [self._body]

    def __enter__(self):
        self._session = _session()
        return self

    def _start(self) -> None:
        self._started = True
        session = self._session
        self._controller = session.task(self._input)
        self._controller.childTasksList = self._children()
        session.prepare(self._controller)
        self._steps = self._controller.executeSteps()
        self._resume()

    def _resume(self, error: Exception | None = None) -> None:
        """Continue the controller after the current body (raising 'error' there); open its next body."""
        session = self._session
        if self._slot is not None:
            session.bodies.pop()
            self._slot = None
        try:
            slot = next(self._steps) if error is None else self._steps.throw(error)
        except StopIteration:
            return
        layers = session.layers
        if isinstance(self._controller, legacy.StartMacroTask):
            # A nested START-MACRO substitutes its body with all tables at once, so outer macro layers stop here.
            layers = tuple(
                layer for layer in layers if not isinstance(layer, MacroLayer)
            )
        session.bodies.append((layers + tuple(slot.layers), slot.errorHandler))
        self._slot = slot

    def _close(self) -> None:
        if self._slot is not None:
            self._session.bodies.pop()
            self._slot = None
        if self._steps is not None:
            self._steps.close()

    def _finish(self, error: Exception | None) -> None:
        """The with-block ended inside a body: let the controller complete or handle the body's error."""
        self._resume(error)
        while self._slot is not None:
            self._resume()

    def __exit__(self, exc_type, error, traceback) -> bool:
        if error is not None and not isinstance(error, Exception):
            self._close()
            return False
        try:
            if not self._started and error is None:
                self._start()
            if self._slot is not None:
                self._finish(error)
            elif error is not None:
                raise error
        except Exception as failure:
            self._close()
            self._session.fail(failure)
        if self._end_input is not None:
            self._session.run(self._end_input)
        return True


class _Condition(_Control):
    """IF-THEN scope; ``matched`` evaluates the original condition."""

    _end_route = "{END-IF}"

    @property
    def matched(self) -> bool:
        if not self._started:
            self._start()
        return self._slot is not None and self._slot is self._body


class _ConditionWithElse(_Condition):
    """IF-THEN with ELSE; the original IfThenTask runs the ELSE and END-IF children itself."""

    _end_route = None

    def __init__(
        self, arguments, options: Options, else_options: Options, end_options: Options
    ):
        super().__init__("{IF-THEN}", arguments, options, None)
        self._else_input = utility_input("{ELSE}", (), else_options)
        self._end_if_input = utility_input("{END-IF}", (), end_options)

    def _children(self) -> list:
        else_task = self._session.task(self._else_input)
        else_task.childTasksList = [DeferredChildTask()]
        return [*super()._children(), else_task, self._session.task(self._end_if_input)]


class _Macro(_Control):
    """START-MACRO scope; ``active`` is False when the original task skips its steps."""

    _end_route = "{END-MACRO}"

    @property
    def active(self) -> bool:
        if not self._started:
            self._start()
        return self._slot is not None


class _Loop(_Control):
    """FOR/SITE/RUN loop; iterate it and run each body inside ``with iteration:``."""

    _end_route = "{END-LOOP}"

    def __iter__(self):
        if not self._started:
            self._start()
        while self._slot is not None:
            iteration = _Iteration(self)
            yield iteration
            if not iteration.advanced:
                self._resume()

    def _finish(self, error: Exception | None) -> None:
        # Left the loop early (break, return, or an error outside 'with iteration').
        self._close()
        if error is not None:
            raise error


class _Iteration:
    """One loop body; an error raised in it goes to the loop controller like a failing child task."""

    def __init__(self, loop: _Loop):
        self._loop = loop
        self.advanced = False

    def __enter__(self):
        return self

    def __exit__(self, exc_type, error, traceback) -> bool:
        if error is None or not isinstance(error, Exception):
            return False
        self.advanced = True
        self._loop._resume(error)
        return True


class _Controls:
    """Arguments are the original /UTILITIES arguments; options are the task's other /OPTIONS."""

    def if_then(
        self, *arguments, options: Options = None, end_options: Options = None
    ) -> _Condition:
        return _Condition("{IF-THEN}", arguments, options, end_options)

    def if_else(
        self,
        *arguments,
        options: Options = None,
        else_options: Options = None,
        end_options: Options = None,
    ) -> _ConditionWithElse:
        return _ConditionWithElse(arguments, options, else_options, end_options)

    def macro(
        self, *arguments, options: Options = None, end_options: Options = None
    ) -> _Macro:
        return _Macro("{START-MACRO}", arguments, options, end_options)

    def for_loop(
        self, *arguments, options: Options = None, end_options: Options = None
    ) -> _Loop:
        return _Loop("{FOR-LOOP}", arguments, options, end_options)

    def site_loop(
        self, *arguments, options: Options = None, end_options: Options = None
    ) -> _Loop:
        return _Loop("{SITE-LOOP}", arguments, options, end_options)

    def run_loop(
        self, *arguments, options: Options = None, end_options: Options = None
    ) -> _Loop:
        return _Loop("{RUN-LOOP}", arguments, options, end_options)

    def hpc(
        self, *arguments, options: Options = None, end_options: Options = None
    ) -> RemoteScope:
        """BEGIN-HPC: statements on the returned scope are declared for remote execution."""
        return RemoteScope(arguments, options, end_options)


controls = _Controls()
