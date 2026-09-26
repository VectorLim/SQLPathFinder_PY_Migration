from __future__ import annotations

import argparse
import os
import sys
from pathlib import Path

REPO_ROOT = Path(__file__).resolve().parents[1]
SCRIPT_HOST_ROOT = REPO_ROOT / "scripthost-utilities-decompiled" / "SPSQL3_py"


def run_script(script_path: Path, working_directory: Path) -> bool:
    sys.path.insert(0, str(SCRIPT_HOST_ROOT))
    from SPFLib.SPFSQL3 import SPFManager

    script_path = script_path.resolve()
    working_directory = working_directory.resolve()
    working_directory.mkdir(parents=True, exist_ok=True)
    os.chdir(working_directory)

    manager = SPFManager()
    manager.gCommandLineArguments = [
        "scripthost-portable-runner",
        f"/MYLOCAL={working_directory}",
        f"/EXEDIR={working_directory}",
    ]
    manager.MySPFSQLFileData = script_path.read_text(encoding="utf-8")
    return bool(manager.Run_SPFSQL())


def main() -> int:
    parser = argparse.ArgumentParser(
        description="Prototype one-process-per-run Linux host for the original ScriptHost runtime."
    )
    parser.add_argument("script", type=Path)
    parser.add_argument("--workdir", type=Path, required=True)
    args = parser.parse_args()
    return 0 if run_script(args.script, args.workdir) else 1


if __name__ == "__main__":
    raise SystemExit(main())
