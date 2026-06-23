from __future__ import annotations

import argparse
import dataclasses
import json
import logging
from collections.abc import Sequence
from pathlib import Path

from vg2c.frontend.parser import parse_vg2


def _configure_logging() -> None:
    """Configure CLI logging output to stderr at INFO level."""
    root = logging.getLogger()
    if root.handlers:
        return
    handler = logging.StreamHandler()
    handler.setFormatter(logging.Formatter("%(levelname)s: %(message)s"))
    root.addHandler(handler)
    root.setLevel(logging.INFO)


def _run_parse_command(path: str, as_json: bool) -> int:
    """Execute the parse subcommand and render output to stdout."""
    try:
        blocks = parse_vg2(Path(path))
    except (FileNotFoundError, UnicodeDecodeError):
        return 1

    if as_json:
        print(json.dumps([dataclasses.asdict(block) for block in blocks], indent=2))
        return 0

    print(f"{'#':>3}  {'lines':<12}  {'top option keys':<40}  {'body chars':>10}")
    for block in blocks:
        keys = ", ".join(list(block.options.keys())[:4])
        span = f"{block.span.start_line}-{block.span.end_line}"
        print(f"{block.index:>3}  {span:<12}  {keys:<40}  {len(block.body):>10}")
    return 0


def _build_parser() -> argparse.ArgumentParser:
    """Create the top-level argument parser for vg2c CLI."""
    parser = argparse.ArgumentParser(prog="vg2c")
    subparsers = parser.add_subparsers(dest="command", required=True)

    parse_parser = subparsers.add_parser("parse", help="Parse a VG2 file")
    parse_parser.add_argument("path", type=str)
    parse_parser.add_argument("--json", action="store_true", dest="as_json")

    return parser


def main(argv: Sequence[str] | None = None) -> int:
    """Run the vg2c command-line interface."""
    _configure_logging()
    parser = _build_parser()
    args = parser.parse_args(argv)

    if args.command == "parse":
        return _run_parse_command(path=args.path, as_json=args.as_json)
    return 1


if __name__ == "__main__":
    raise SystemExit(main())
