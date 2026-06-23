from __future__ import annotations

import argparse
import dataclasses
import json
import logging
from collections.abc import Sequence
from pathlib import Path

from vg2c.classifier import Kind, classify_all
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


def _run_classify_command(path: str, as_json: bool, report: bool, strict: bool) -> int:
    """Execute the classify subcommand and render output to stdout."""
    try:
        blocks = parse_vg2(Path(path))
    except (FileNotFoundError, UnicodeDecodeError):
        return 1

    classification = classify_all(blocks)

    if as_json:
        print(json.dumps(dataclasses.asdict(classification), indent=2))
        return 0

    if report:
        # Sort UNKNOWN first, then by index
        sorted_blocks = sorted(
            classification.blocks,
            key=lambda cb: (cb.kind != Kind.UNKNOWN, cb.parsed.index),
        )
        print(f"{'#':>3}  {'lines':<12}  {'kind':<20}  {'role':<8}  {'reason':<50}")
        for cb in sorted_blocks:
            span = f"{cb.parsed.span.start_line}-{cb.parsed.span.end_line}"
            reason_short = cb.reason[:47] + "..." if len(cb.reason) > 50 else cb.reason
            print(
                f"{cb.parsed.index:>3}  {span:<12}  "
                f"{cb.kind.value:<20}  {cb.role.value:<8}  {reason_short:<50}"
            )
        return 1 if (strict and classification.diagnostics) else 0

    # Default: kind-count summary
    from collections import Counter

    kind_counts = Counter(cb.kind for cb in classification.blocks)
    for kind in sorted(kind_counts.keys(), key=lambda k: k.value):
        print(f"{kind.value}: {kind_counts[kind]}")

    return 1 if (strict and classification.diagnostics) else 0


def _build_parser() -> argparse.ArgumentParser:
    """Create the top-level argument parser for vg2c CLI."""
    parser = argparse.ArgumentParser(prog="vg2c")
    subparsers = parser.add_subparsers(dest="command", required=True)

    parse_parser = subparsers.add_parser("parse", help="Parse a VG2 file")
    parse_parser.add_argument("path", type=str)
    parse_parser.add_argument("--json", action="store_true", dest="as_json")

    classify_parser = subparsers.add_parser("classify", help="Classify VG2 blocks")
    classify_parser.add_argument("path", type=str)
    classify_parser.add_argument("--json", action="store_true", dest="as_json")
    classify_parser.add_argument("--report", action="store_true")
    classify_parser.add_argument("--strict", action="store_true")

    return parser


def main(argv: Sequence[str] | None = None) -> int:
    """Run the vg2c command-line interface."""
    _configure_logging()
    parser = _build_parser()
    args = parser.parse_args(argv)

    if args.command == "parse":
        return _run_parse_command(path=args.path, as_json=args.as_json)
    elif args.command == "classify":
        return _run_classify_command(
            path=args.path,
            as_json=args.as_json,
            report=args.report,
            strict=args.strict,
        )
    return 1


if __name__ == "__main__":
    raise SystemExit(main())
