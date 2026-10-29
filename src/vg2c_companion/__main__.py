from __future__ import annotations

import argparse
from pathlib import Path

from .app import run


def main(argv: list[str] | None = None) -> None:
    parser = argparse.ArgumentParser(
        description="Choose an open SQLPathFinder document and update its Python preview."
    )
    parser.add_argument(
        "native_paths",
        nargs="*",
        type=Path,
        help="Optional native .VG2 paths for identification only.",
    )
    parser.add_argument(
        "--pid",
        type=int,
        help="Observe this SQLPathFinder PID after exact process-name validation.",
    )
    args = parser.parse_args(argv)
    for path in args.native_paths:
        if path.suffix.lower() != ".vg2":
            parser.error(f"Native context path must have .VG2 suffix: {path}")
    run(tuple(args.native_paths), args.pid)


if __name__ == "__main__":
    main()
