"""Checkout launcher: one request, one isolated original ScriptHost job."""

from __future__ import annotations

import argparse
import json
from dataclasses import asdict
from pathlib import Path

from .worker import ScriptHostJob, run_job


def main() -> int:
    parser = argparse.ArgumentParser(description=__doc__)
    parser.add_argument("script", type=Path)
    parser.add_argument("--workdir", type=Path, required=True)
    parser.add_argument("--timeout", type=float, default=None)
    args = parser.parse_args()
    path = str(args.script.resolve())
    is_python = args.script.suffix.lower() == ".py"
    result = run_job(
        ScriptHostJob(
            python_path=path if is_python else None,
            script_path=None if is_python else path,
            working_directory=str(args.workdir.resolve()),
        ),
        timeout=args.timeout,
    )
    print(json.dumps(asdict(result)))
    return 0 if result.success else 1


if __name__ == "__main__":
    raise SystemExit(main())
