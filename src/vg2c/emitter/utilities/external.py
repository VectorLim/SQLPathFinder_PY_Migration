"""ExternalProcess — run system commands via subprocess."""

from __future__ import annotations

import subprocess
from pathlib import Path

from vg2c.emitter.codegen import (
    FunctionDef,
    PyExpr,
    emit_call,
    python_literal_for_option,
    register_call_embed,
)
from vg2c.emitter.utilities._base import UtilitySpec
from vg2c.emitter.utilities._registry import register_utility


@register_utility
class ExternalProcess(UtilitySpec):
    """Thin wrapper around subprocess.run."""

    utility_name = "external"
    utility_imports = (
        "import subprocess",
        "from pathlib import Path",
    )
    utility_command_contains = (("run-python-script", ("run_python_script",)),)
    utility_command_suffixes = (
        ("bat-file", (".bat",)),
        ("exe-direct", (".exe",)),
    )

    @classmethod
    def emit(
        cls,
        ctx,
        block,
        dispatched,
    ) -> tuple[str, str]:
        argv = dispatched.argv
        call = emit_call(
            ExternalProcess.run,
            PyExpr.list_of([python_literal_for_option(token) for token in argv]),
        )
        register_call_embed(ctx, call)
        func_name = FunctionDef.name_for(block, "utility")
        fdef = FunctionDef.from_call(func_name, call)
        return fdef.source, fdef.call_site

    def run(
        self,
        argv: list[str],
        cwd: str | Path | None = None,
        env: dict | None = None,
        check: bool = False,
    ) -> int:
        result = subprocess.run(
            argv,
            cwd=str(cwd) if cwd else None,
            env=env,
            check=check,
        )
        return result.returncode
