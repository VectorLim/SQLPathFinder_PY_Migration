from __future__ import annotations

import sys
from pathlib import Path

_SCRIPT_HOST_ROOT = (
    Path(__file__).resolve().parents[2]
    / "scripthost-utilities-decompiled"
    / "SPSQL3_py"
)


def _spf_manager_type():
    root = str(_SCRIPT_HOST_ROOT)
    if root not in sys.path:
        sys.path.insert(0, root)
    from SPFLib.SPFSQL3 import SPFManager

    return SPFManager


class PortableScriptHostRuntime:
    """Minimal Linux facade around the original SPFManager/task runtime.

    The historical top-level entrypoint and SPFManager.main initialize
    Windows/service/network integrations that are not needed to assess the
    parser/controller/task engine. This facade intentionally enters at
    Run_SPFSQL after supplying the same command-line-derived per-run state.
    All parsing, task construction, control flow and utility execution remain
    the original ScriptHost implementations.
    """

    def run_text(self, text: str, working_directory: Path) -> bool:
        workdir = Path(working_directory).resolve(strict=False)
        workdir.mkdir(parents=True, exist_ok=True)

        manager = _spf_manager_type()()
        manager.gCommandLineArguments = [
            str(_SCRIPT_HOST_ROOT / "SPFSQL3.py"),
            f'/MYLOCAL="{workdir}"',
            "/EXECMODE=UT",
        ]
        manager.MySPFSQLFileData = text
        return bool(manager.Run_SPFSQL())

    def run_file(self, script: Path, working_directory: Path | None = None) -> bool:
        path = Path(script).resolve(strict=True)
        workdir = path.parent if working_directory is None else Path(working_directory)
        return self.run_text(path.read_text(encoding="utf-8-sig"), workdir)
