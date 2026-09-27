from __future__ import annotations

import stat
from pathlib import Path
from typing import TYPE_CHECKING

from vg2c_new.paths import resolve_path, working_directory_for
from vg2c_new.utilities.base import Utility

if TYPE_CHECKING:
    from vg2c_new.model import Command
    from vg2c_new.runtime import RuntimeState


class SetFileReadOnlyUtility(Utility):
    def apply(self, command: Command, state: RuntimeState) -> None:
        """Amended portable port of ScriptHost SetFileROTask.

        Source: SPSQL3_py/SPFLib/SPFSQL3.py :: SetFileROTask.
        Reference source/commit: vendored ScriptHost at 8ddd5e6463b43834d769057be48041ec657f0f9d.
        Port mode: AMENDED PORT.
        Preserved: READONLY/READWRITE intent.
        Amendments: chmod replaces Windows attributes.
        Intentionally discarded: HIDDEN/SYSTEM.
        """
        args = [state.substitute(v) for v in command.arguments]
        if len(args) < 2:
            raise ValueError("SetFileRO requires file and access mode.")
        path = _path(command, state, args[0])
        mode = args[1].upper()
        if "HIDDEN" in mode or "SYSTEM" in mode:
            raise RuntimeError(
                "HIDDEN/SYSTEM are Windows-only attributes and intentionally unsupported."
            )
        current = path.stat().st_mode
        if "READWRITE" in mode:
            path.chmod(current | stat.S_IWUSR)
        elif "READONLY" in mode:
            path.chmod(current & ~stat.S_IWUSR & ~stat.S_IWGRP & ~stat.S_IWOTH)
        else:
            raise ValueError(f"Unsupported file access mode {args[1]!r}.")


def _path(command: Command, state: RuntimeState, value: str) -> Path:
    return resolve_path(
        state.substitute(value), state, base=working_directory_for(command.option("WORKDIR"), state)
    )
