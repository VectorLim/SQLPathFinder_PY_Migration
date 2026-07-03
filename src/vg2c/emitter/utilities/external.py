"""ExternalProcess — run system commands via subprocess."""

from __future__ import annotations

import subprocess
from pathlib import Path

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
