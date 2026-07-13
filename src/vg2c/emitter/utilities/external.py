"""ExternalProcess - run system commands via subprocess."""

from __future__ import annotations

import os
import subprocess
from pathlib import Path

from vg2c.emitter.models import EmitContext
from vg2c.emitter.utilities._base import CheckedUtilitySpec
from vg2c.emitter.utilities._emit_helpers import (
    RawExpr,
    option_to_python_expr,
    render_method_call,
    split_utility_command,
)
from vg2c.kind import Kind


class ExternalProcess(CheckedUtilitySpec):
    """Execute generic shell command or script block."""

    utility_name = "external"
    handles = (Kind.EXTERNAL_RUN,)

    @staticmethod
    def check(options) -> tuple[Kind, str] | None:
        text = options.lookup.get("UTILITIES", "").strip()
        if not text:
            return None
        argv = split_utility_command(text)
        if not argv:
            return None
        basename = argv[0].split("/")[-1].split("\\")[-1].lower()
        if "run_python_script" in basename or basename.endswith((".bat", ".exe")):
            return Kind.EXTERNAL_RUN, "/UTILITIES command maps to external run"
        return None

    @staticmethod
    def _utility_argv(block) -> list[str]:
        text = block.resolved_options.lookup.get("UTILITIES", "").strip()
        return split_utility_command(text)

    @classmethod
    @EmitContext.step_emitter
    def emit_block(cls, block) -> list[str] | None:
        argv = cls._utility_argv(block)
        if not argv:
            return ["pass  # TODO: empty external utility command"]

        basename = argv[0].split("/")[-1].split("\\")[-1].lower()
        if "run_python_script" in basename:
            return ["pass  # Python script embedded directly, external run omitted"]

        stmt = cls._emit_run(argv)
        return [stmt]

    @staticmethod
    def _emit_run(argv: list[str]) -> str:
        expr_items = [option_to_python_expr(token) for token in argv]
        argv_expr = RawExpr("[" + ", ".join(expr_items) + "]")
        return render_method_call(
            "external",
            "run",
            kwargs={"argv": argv_expr},
        )

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
