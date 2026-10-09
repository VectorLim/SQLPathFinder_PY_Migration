"""Structured input of one ScriptHost task, bridged to the original lexical codec."""

from __future__ import annotations

from collections.abc import Iterable, Mapping
from dataclasses import dataclass

from .runtime import _spf_manager_type

Options = Mapping[str, str] | Iterable[tuple[str, str]] | None


def _codec():
    _spf_manager_type()  # puts the vendored SPFLib on sys.path
    from SPFLib import SPFTaskInput

    return SPFTaskInput


def task_delimiter() -> str:
    """Text between task items in a ScriptHost job (SPFManager.SQLFILE_DELIM)."""
    return _codec().TASK_DELIMITER


def option_pairs(options: Options) -> tuple[tuple[str, str], ...]:
    """Ordered (token, value) pairs; a sequence of pairs keeps duplicate tokens."""
    items = options.items() if isinstance(options, Mapping) else options or ()
    return tuple((str(token), str(value)) for token, value in items)


def utility_input(route: str, arguments=(), options: Options = None, command: str = "") -> TaskInput:
    """Task input whose /UTILITIES value is the route followed by every argument quoted."""
    value = _codec().encode_utility(route, arguments)
    return TaskInput((("UTILITIES", value), *option_pairs(options)), command)


@dataclass(frozen=True, slots=True)
class TaskInput:
    """Ordered /OPTION pairs and command text, as SPFTaskBase.parseTaskOptions reads them."""

    options: tuple[tuple[str, str], ...]
    command: str = ""

    @classmethod
    def parse(cls, task_item: str) -> TaskInput:
        options, command = _codec().parse_task_item(task_item)
        return cls(tuple(options), command)

    def encode(self) -> str:
        return _codec().encode_task_item(self.options, self.command)

    @property
    def utility(self) -> tuple[str, ...] | None:
        """/UTILITIES route and arguments, when re-encoding them reproduces the original value."""
        values = [value for token, value in self.options if token == "UTILITIES"]
        parts = _codec().decode_utility(values[0]) if len(values) == 1 else None
        return None if parts is None else tuple(parts)
