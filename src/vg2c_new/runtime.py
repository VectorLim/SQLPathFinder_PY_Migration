from __future__ import annotations

import os
import re
from collections.abc import Iterator, Mapping
from contextlib import contextmanager
from dataclasses import dataclass, field
from pathlib import Path

_ENV_TOKEN_RE = re.compile(r"<<<%([^%]+)%>>>", re.IGNORECASE)
_MACRO_TOKEN_RE = re.compile(r"<<<(.*?)>>>", re.IGNORECASE | re.MULTILINE)


@dataclass(slots=True)
class RuntimeState:
    """Reference state retained only for the unresolved email migration fixture."""

    working_directory: Path
    globals: dict[str, str] = field(default_factory=dict)
    environment: Mapping[str, str] = field(default_factory=lambda: dict(os.environ))
    _frames: list[dict[str, str]] = field(default_factory=list, init=False, repr=False)

    def __post_init__(self) -> None:
        self.working_directory = Path(self.working_directory).resolve(strict=False)
        self.globals = {key.upper(): str(value) for key, value in self.globals.items()}

    def set_global(self, name: str, value: object) -> None:
        self.globals[name.upper()] = str(value)

    def environment_value(self, name: str) -> str | None:
        if name in self.environment:
            return self.environment[name]
        upper = name.upper()
        for key, value in self.environment.items():
            if key.upper() == upper:
                return value
        return None

    def lookup(self, name: str, default: str | None = None) -> str | None:
        key = name.upper()
        for frame in reversed(self._frames):
            if key in frame:
                return frame[key]
        if key in self.globals:
            return self.globals[key]
        env = self.environment_value(name)
        return default if env is None else env

    def push_frame(self, values: Mapping[str, object]) -> None:
        self._frames.append({key.upper(): str(value) for key, value in values.items()})

    def pop_frame(self) -> None:
        if not self._frames:
            raise RuntimeError("RuntimeState frame stack is empty.")
        self._frames.pop()

    @contextmanager
    def frame(self, values: Mapping[str, object]) -> Iterator[None]:
        self.push_frame(values)
        try:
            yield
        finally:
            self.pop_frame()

    def substitute(self, value: str) -> str:
        """Amended port of ScriptHost Substitute_Env/Substitute_Macro.

        Original: SPSQL3_py/SPFLib/SPFUtilities/utils.py :: Utilities.Substitute_Env
        and Utilities.Substitute_Macro.
        Reference commit: 8ddd5e6463b43834d769057be48041ec657f0f9d.
        Port mode: AMENDED_PORT.
        Preserved: <<<%ENV%>>> syntax, case-insensitive <<<macro>>> lookup,
        unresolved ordinary macro errors, and reserved spf token behavior.
        Amendments: resolves RuntimeState frames/globals instead of MemTable/global
        singleton state; loop/site tokens use the same frame lookup.
        Discarded: mutable MemTable traversal, SPFGlobals, Python-2 branches.
        """

        def replace_env(match: re.Match[str]) -> str:
            name = match.group(1)
            resolved = self.environment_value(name)
            if not resolved:
                raise RuntimeError(f"Environment variable {match.group(0)} not found")
            return resolved

        out = _ENV_TOKEN_RE.sub(replace_env, value)

        def replace_macro(match: re.Match[str]) -> str:
            token = match.group(1).strip()
            full = match.group(0)
            if token.lower() == "spf_datetime":
                return full
            resolved = self.lookup(token)
            if resolved is not None:
                return resolved
            upper = token.upper()
            if (
                upper.startswith("SPF-")
                or upper.startswith("SPF$")
                or upper.startswith("%")
                or upper.startswith("!")
            ):
                return full
            raise RuntimeError(f"Macro variable {full} not found")

        return _MACRO_TOKEN_RE.sub(replace_macro, out)
