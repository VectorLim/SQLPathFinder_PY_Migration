from __future__ import annotations

from pathlib import Path
from typing import TYPE_CHECKING

from vg2c_new.paths import resolve_path, working_directory_for
from vg2c_new.utilities.base import Utility

if TYPE_CHECKING:
    from vg2c_new.model import Command
    from vg2c_new.runtime import RuntimeState


class EchoUtility(Utility):
    def apply(self, command: Command, state: RuntimeState) -> None:
        print(" ".join(state.substitute(v) for v in command.arguments))


def _path(command: Command, state: RuntimeState, value: str) -> Path:
    return resolve_path(value, state, base=working_directory_for(command.option("WORKDIR"), state))
