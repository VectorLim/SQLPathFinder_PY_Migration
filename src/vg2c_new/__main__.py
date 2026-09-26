from __future__ import annotations

import argparse
from pathlib import Path

from vg2c_new.parser import parse_file
from vg2c_new.runtime import Interpreter, RuntimeState
from vg2c_new.utilities.files import DeleteFileUtility, WriteFileUtility


def main() -> int:
    parser = argparse.ArgumentParser(description="Execute a VG2 script directly without generated Python.")
    parser.add_argument("script", type=Path)
    args = parser.parse_args()
    script = args.script.resolve()
    commands = parse_file(script)
    interpreter = Interpreter(
        {
            "write_file": WriteFileUtility(),
            "delete_file": DeleteFileUtility(),
        }
    )
    interpreter.execute(commands, RuntimeState(working_directory=script.parent))
    return 0


if __name__ == "__main__":
    raise SystemExit(main())
