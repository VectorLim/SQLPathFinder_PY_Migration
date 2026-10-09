"""Call arguments that reproduce one ordinary task's original input."""

from __future__ import annotations

from scripthost_portable.task_introspection import TaskDescriptor
from vg2c.emitter.literals import py_string

Argument = list[str]  # source lines of one call argument


def options_argument(pairs, name: str = "options") -> list[Argument]:
    """``name={...}``; a list of pairs when a token repeats. Empty when there are no options."""
    if not pairs:
        return []
    tokens = [token for token, _ in pairs]
    if len(set(tokens)) == len(tokens):
        items, brackets = [f"{py_string(k)}: {py_string(v)}" for k, v in pairs], "{}"
    else:
        items, brackets = [f"({py_string(k)}, {py_string(v)})" for k, v in pairs], "[]"
    flat = f"{name}={brackets[0]}{', '.join(items)}{brackets[1]}"
    if len(flat) <= 80:
        return [[flat]]
    return [[f"{name}={brackets[0]}", *(f"    {item}," for item in items), brackets[1]]]


def without_utilities(task: TaskDescriptor) -> list[tuple[str, str]]:
    return [pair for pair in task.input.options if pair[0] != "UTILITIES"]


def leaf_call(task: TaskDescriptor) -> tuple[str, list[Argument]]:
    """(method, arguments) of the script.* call for an ordinary task."""
    task_input = task.input
    others = without_utilities(task)
    values = [value for token, value in task_input.options if token == "UTILITIES"]
    command = (
        [[f"command={py_string(task_input.command)}"]] if task_input.command else []
    )
    if task.task_type == "DOSCmdTask" and len(values) == 1 and not command:
        return "command", [[py_string(values[0])], *options_argument(others)]
    if task_input.utility is not None:
        route, *arguments = task_input.utility
        braced = route.startswith("{") and route.endswith("}")
        positional = [
            [py_string(value)]
            for value in (route[1:-1] if braced else route, *arguments)
        ]
        external = [] if braced else [["external=True"]]
        return "utility", [*positional, *options_argument(others), *external, *command]
    return "invoke", [*options_argument(task_input.options), *command]
