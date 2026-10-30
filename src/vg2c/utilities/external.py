"""ExternalProcess - run system commands via subprocess."""

from __future__ import annotations

import os
import subprocess
from pathlib import Path

from vg2c.emitter.models import BlockEmission
from vg2c.emitter.operations import emittable
from vg2c.kind import Kind
from vg2c.utilities._base import EmitterUtility
from vg2c.utilities._emit_helpers import (
    list_code_expr,
    lower_native_call,
    split_utility_command,
)


class ExternalProcess(EmitterUtility):
    """Execute generic shell command or script block."""

    utility_name = "external"
    handles = (Kind.EXTERNAL_RUN,)
    # Generic .bat/.exe handling is a fallback after more specific utilities.
    check_priority = -100

    @staticmethod
    def check(options) -> Kind | None:
        text = options.lookup.get("UTILITIES", "").strip()
        if not text:
            return None
        argv = split_utility_command(text)
        if not argv:
            return None
        basename = argv[0].split("/")[-1].split("\\")[-1].lower()
        if "run_python_script" in basename or basename.endswith((".bat", ".exe")):
            return Kind.EXTERNAL_RUN
        return None

    @staticmethod
    def _utility_argv(block) -> list[str]:
        text = block.resolved_options.lookup.get("UTILITIES", "").strip()
        return split_utility_command(text)

    @classmethod
    def emit_block(cls, block, *, macros: str = "macros") -> BlockEmission:
        argv = cls._utility_argv(block)
        if not argv:
            raise ValueError("Unsupported utility operation")

        stmt = cls._emit_run(argv)
        call = lower_native_call(
            stmt, function="job.run_program", macros=macros
        )
        return BlockEmission((call,))

    @classmethod
    def _emit_run(cls, argv: list[str]) -> str:
        return cls.run.render(argv=list_code_expr(argv))

    @staticmethod
    def _resolve_exedir() -> str:
        """Return the SPF tools directory from env var VG2C_EXEDIR."""
        return os.environ.get("VG2C_EXEDIR", "")

    @staticmethod
    def _resolve_path(path: str) -> str:
        return os.path.normpath(path)

    @classmethod
    def _resolve_argv(cls, argv: list[str]) -> list[str]:
        """Substitute @EXEDIR@ tokens and normalise path-like arguments."""
        exedir = cls._resolve_exedir()
        return [
            cls._resolve_path(a) if os.sep in a else a
            for a in (arg.replace("@EXEDIR@", exedir) for arg in argv)
        ]

    @emittable
    def run(
        self,
        argv: list[str],
        cwd: str | Path | None = None,
        env: dict | None = None,
        check: bool = False,
    ) -> int:
        resolved = self._resolve_argv(argv)
        first = resolved[0] if resolved else ""
        use_shell = Path(first).suffix.lower() in {".bat", ".va", ".exe"}
        result = subprocess.run(
            resolved,
            cwd=str(cwd) if cwd else None,
            env=env,
            check=check,
            shell=use_shell,
        )
        return result.returncode
