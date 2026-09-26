from __future__ import annotations

import re
from pathlib import Path
from typing import TYPE_CHECKING

if TYPE_CHECKING:
    from vg2c_new.runtime import RuntimeState

_PERCENT_ENV_RE = re.compile(r"%([^%]+)%")
_WINDOWS_ABSOLUTE_RE = re.compile(r"^[A-Za-z]:[\\/]")


def resolve_path(value: str, state: RuntimeState, *, base: Path | None = None) -> Path:
    """Resolve a VG2 local path relative to RuntimeState.working_directory."""
    text = state.substitute(value).strip().strip('"')

    def expand_percent(match: re.Match[str]) -> str:
        resolved = state.environment_value(match.group(1))
        return match.group(0) if resolved is None else resolved

    text = _PERCENT_ENV_RE.sub(expand_percent, text)

    # Relative ScriptHost paths are Windows-authored. Treat separators as syntax so
    # the same scripts work on Linux. Keep drive/UNC paths intact; Session 1 does
    # not claim portable access to Windows-only locations.
    is_windows_absolute = bool(_WINDOWS_ABSOLUTE_RE.match(text))
    is_unc = text.startswith("\\\\")
    if not is_windows_absolute and not is_unc:
        text = text.replace("\\", "/")

    path = Path(text).expanduser()
    if not path.is_absolute() and not is_windows_absolute and not is_unc:
        path = (base or state.working_directory) / path
    return path.resolve(strict=False) if not (is_windows_absolute or is_unc) else path


def working_directory_for(command_workdir: str | None, state: RuntimeState) -> Path:
    if not command_workdir or command_workdir.strip() in {".", ".\\", "./"}:
        return state.working_directory
    return resolve_path(command_workdir, state)
