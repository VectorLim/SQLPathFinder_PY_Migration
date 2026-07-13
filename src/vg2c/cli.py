"""vg2c CLI — translate VG2 scripts to Python.

Usage:
    vg2c <input> [<output>] [--strict]
"""

from __future__ import annotations

import argparse
import sys
from pathlib import Path

from vg2c.dataflow import analyze
from vg2c.dispatch import dispatch
from vg2c.emitter import emit
from vg2c.frontend import classify, parse
from vg2c.frontend.models import Diagnostic
from vg2c.resolver import resolve



def _format_diagnostic(diag: Diagnostic, source: Path | None) -> str:
    loc = str(source) if source else "<input>"
    if diag.span is not None:
        loc = f"{loc}:{diag.span.start_line}:1"
    code = f"[{diag.code}] " if diag.code else ""
    return f"{diag.severity.upper()} {code}{loc}: {diag.message}"


def cmd_translate(args: argparse.Namespace) -> int:
    input_path = Path(args.input)
    if not input_path.exists():
        print(f"ERROR: input file not found: {input_path}", file=sys.stderr)
        return 1

    text = input_path.read_text(encoding="utf-8", errors="replace")

    parsed, pdiag = parse(text, source=input_path)
    classified, cdiag = classify(parsed)
    resolved = resolve(classified, diagnostics=[*pdiag, *cdiag])
    analyzed = analyze(resolved)
    dispatched = dispatch(analyzed)
    emitted = emit(dispatched)

    # Print diagnostics to stderr, sorted by severity then block order
    severity_order = {"error": 0, "warning": 1, "info": 2}
    sorted_diags = sorted(
        emitted.diagnostics,
        key=lambda d: (severity_order.get(d.severity, 9), d.block_index or 0),
    )
    for diag in sorted_diags:
        print(_format_diagnostic(diag, input_path), file=sys.stderr)

    # Write output
    if args.output:
        out_path = Path(args.output)
        if len(out_path.parts) == 1:
            out_path = input_path.parent / out_path
    else:
        out_path = input_path.with_suffix(".py")

    out_path.parent.mkdir(parents=True, exist_ok=True)
    out_path.write_text(emitted.source, encoding="utf-8")

    # --strict: exit 1 if any error-severity diagnostics
    if args.strict and any(d.severity == "error" for d in emitted.diagnostics):
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
