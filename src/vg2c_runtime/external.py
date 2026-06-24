"""ExternalProcess — run system commands via subprocess."""

from __future__ import annotations

import subprocess
from pathlib import Path


class ExternalProcess:
    """Thin wrapper around subprocess.run."""

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
