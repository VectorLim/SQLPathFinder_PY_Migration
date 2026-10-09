"""Python scopes for the original controllers, read from the original task tree.

handleControlerTask returns a controller followed by its closing task as siblings, except
that IF-THEN with ELSE holds ``[then..., ELSE(else...), END-IF]`` as its own children.
Inside BEGIN-HPC the same scopes are declarations on the remote scope object.
"""

from __future__ import annotations

from typing import TYPE_CHECKING

from scripthost_portable.task_introspection import TaskDescriptor
from vg2c.emitter.literals import py_string
from vg2c.emitter.tasks import options_argument, without_utilities

if TYPE_CHECKING:
    from vg2c.emitter.walker import Emitter

_SCOPES = {  # class name -> (controls method, closing route, scope variable)
    "StartMacroTask": ("macro", "{END-MACRO}", "macro"),
    "ForLoopTask": ("for_loop", "{END-LOOP}", "loop"),
    "SiteLoopTask": ("site_loop", "{END-LOOP}", "loop"),
    "RunLoopTask": ("run_loop", "{END-LOOP}", "loop"),
    "BeginHPCTask": ("hpc", "{END-HPC}", "remote"),
}


def emit_control(emitter: Emitter, task: TaskDescriptor, closer: TaskDescriptor | None, target: str) -> bool:
    """Write one controller scope; return True when it consumed the following closing task."""
    if task.class_name == "IfThenTask":
        return _condition(emitter, task, closer, target)
    if task.class_name not in _SCOPES:
        emitter.fail("unsupported-control", f"No Python scope for {task.class_name} ({task.task_type}).", task)
    method, end_route, variable = _SCOPES[task.class_name]
    head = _arguments(emitter, task) + _end_options(emitter, task, closer, end_route)
    blocks = (task, closer)
    if method == "hpc":
        if target != "script":
            emitter.fail("nested-hpc", "BEGIN-HPC cannot run inside another BEGIN-HPC.", task)
        with emitter.scope(variable) as name:
            emitter.call("with controls.hpc", head, blocks, f" as {name}:")
            emitter.block(task.children, name)
    elif target != "script":
        emitter.call(f"with {target}.{method}", head, blocks, ":")
        emitter.block(task.children, target)
    elif method == "macro":
        with emitter.scope(variable) as name:
            emitter.call("with controls.macro", head, blocks, f" as {name}:")
            emitter.push()
            emitter.write(f"if {name}.active:")
            emitter.block(task.children, target)
            emitter.pop()
    else:
        with emitter.scope(variable) as name, emitter.scope("iteration") as iteration:
            emitter.call(f"with controls.{method}", head, blocks, f" as {name}:")
            emitter.push()
            emitter.write(f"for {iteration} in {name}:")
            emitter.push()
            emitter.write(f"with {iteration}:")
            emitter.block(task.children, target)
            emitter.pop()
            emitter.pop()
    return True


def _condition(emitter: Emitter, task: TaskDescriptor, closer: TaskDescriptor | None, target: str) -> bool:
    children = list(task.children)
    else_at = next((i for i, child in enumerate(children) if child.class_name == "ElseTask"), None)
    arguments = _arguments(emitter, task)
    if else_at is None:
        then, else_task, otherwise, end = children, None, (), closer
    else:
        then, else_task, rest = children[:else_at], children[else_at], children[else_at + 1:]
        if len(rest) != 1:
            emitter.fail("else-structure", "ELSE must be followed by exactly one END-IF.", else_task)
        _check_bare(emitter, else_task, "{ELSE}")
        otherwise, end = else_task.children, rest[0]
    end_options = _end_options(emitter, task, end, "{END-IF}")
    else_options = without_utilities(else_task) if else_task is not None else []

    if target != "script":
        emitter.call(f"with {target}.if_then", arguments + end_options, (task, end), ":")
        emitter.push()
        if else_task is None:
            emitter.body(then, target)
        else:
            emitter.statements(then, target)
            emitter.call(f"{target}.else_branch", options_argument(else_options), (else_task,))
            emitter.statements(otherwise, target)
        emitter.pop()
        return else_task is None

    with emitter.scope("condition") as name:
        if else_task is None:
            emitter.call("with controls.if_then", arguments + end_options, (task, end), f" as {name}:")
        else:
            head = arguments + options_argument(else_options, "else_options") + end_options
            emitter.call("with controls.if_else", head, (task, else_task, end), f" as {name}:")
        emitter.push()
        emitter.write(f"if {name}.matched:")
        emitter.block(then, target)
        if else_task is not None:
            emitter.write("else:")
            emitter.block(otherwise, target)
        emitter.pop()
    return else_task is None


def _arguments(emitter: Emitter, task: TaskDescriptor) -> list:
    utility = task.input.utility
    if utility is None:
        emitter.fail("control-arguments", "Controller arguments must each be one quoted value.", task)
    if task.input.command:
        emitter.fail("control-body", "A controller block cannot carry command text.", task)
    return [[py_string(argument)] for argument in utility[1:]] + options_argument(without_utilities(task))


def _check_bare(emitter: Emitter, task: TaskDescriptor, route: str) -> None:
    if task.input.utility != (route,) or task.input.command:
        emitter.fail("control-input", f"Expected {route} without arguments or command text.", task)


def _end_options(emitter: Emitter, task: TaskDescriptor, end: TaskDescriptor | None, route: str) -> list:
    if end is None or not end.is_control_end or (end.input.utility or ("",))[0] != route:
        emitter.fail("missing-end", f"{task.class_name} must be closed by {route}.", task)
    _check_bare(emitter, end, route)
    return options_argument(without_utilities(end), "end_options")
