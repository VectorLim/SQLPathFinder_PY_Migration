"""vg2c CLI — translate VG2 scripts to Python.

Usage:
    vg2c <input> [<output>] [--strict]
"""

from __future__ import annotations

import argparse
import logging
import sys
from pathlib import Path

from vg2c.logger import Logger
from vg2c.dataflow import analyze
from vg2c.dispatch import dispatch
from vg2c.emitter import emit
from vg2c.frontend import classify, parse
from vg2c.resolver import resolve


class ErrorDetectingHandler(logging.Handler):
    def __init__(self) -> None:
        super().__init__()
        self.has_errors = False

    def emit(self, record: logging.LogRecord) -> None:
        if record.levelno >= logging.ERROR:
            self.has_errors = True


def cmd_translate(args: argparse.Namespace) -> int:
    # Initialize basic logging
    Logger.basicConfig(level=Logger.INFO)

    # Attach error detector if strict mode is active
    error_detector = ErrorDetectingHandler()
    vg2c_logger = Logger.getLogger()
    if args.strict:
        vg2c_logger.addHandler(error_detector)

    input_path = Path(args.input)
    if not input_path.exists():
        print(f"ERROR: input file not found: {input_path}", file=sys.stderr)
        return 1

    text = input_path.read_text(encoding="utf-8", errors="replace")

    try:
        parsed = parse(text, source=input_path)
        classified = classify(parsed)
        resolved = resolve(classified)
        analyzed = analyze(resolved)
        dispatched = dispatch(analyzed)
        emitted = emit(dispatched)
    finally:
        if args.strict:
            vg2c_logger.removeHandler(error_detector)

    # Write output
    if args.output:
        out_path = Path(args.output)
        if len(out_path.parts) == 1:
            out_path = input_path.parent / out_path
    else:
        out_path = input_path.with_suffix(".py")

    out_path.parent.mkdir(parents=True, exist_ok=True)
    out_path.write_text(emitted.source, encoding="utf-8")

    # --strict: exit 1 if any error-severity logs occurred
    if args.strict and error_detector.has_errors:
        return 1
    return 0


def build_parser() -> argparse.ArgumentParser:
    parser = argparse.ArgumentParser(
        prog="vg2c",
        description="Translate VG2 pipeline scripts to Python.",
    )
    parser.add_argument("input", help="Path to the VG2 source file.")
    parser.add_argument(
        "output",
        nargs="?",
        help="Path to the output Python file. If omitted or specified as a bare name, it will follow the directory of the input file.",
    )
    parser.add_argument(
        "--strict",
        action="store_true",
        help="Exit 1 if any error diagnostic is emitted.",
    )

    return parser


def main() -> None:
    parser = build_parser()
    args = parser.parse_args()
    sys.exit(cmd_translate(args))


if __name__ == "__main__":
    main()
