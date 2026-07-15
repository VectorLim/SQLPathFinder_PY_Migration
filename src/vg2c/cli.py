"""vg2c CLI — interactive batch translator.

Detects .txt files in the target directory (defaults to the current working
directory) and lets the user choose which ones to translate to Python.
"""

from __future__ import annotations

import importlib.util
import os
import subprocess
import sys
import traceback
from pathlib import Path

from vg2c.logger import Logger
from vg2c import translate

_log = Logger.getLogger(__name__)


def _pick_files(txt_files: list[Path]) -> list[Path]:
    """Prompt the user to select files by index. Returns selected paths."""
    print("\nDetected .txt files:")
    for i, p in enumerate(txt_files, 1):
        print(f"  [{i}] {p.name}")
    print()

    while True:
        raw = input(
            "Select files (* for all, or indexes separated by spaces): "
        ).strip()
        if not raw:
            print("  No selection entered. Please try again.")
            continue

        if raw == "*":
            return list(txt_files)

        tokens = raw.split()
        selected: list[Path] = []
        seen: set[int] = set()
        valid = True

        for token in tokens:
            if not token.isdigit():
                print(f"  Invalid input '{token}'. Use integers or '*'.")
                valid = False
                break
            idx = int(token)
            if idx < 1 or idx > len(txt_files):
                print(f"  Index {idx} is out of range (1–{len(txt_files)}).")
                valid = False
                break
            if idx in seen:
                print(f"  Duplicate index {idx} — each file may only be selected once.")
                valid = False
                break
            seen.add(idx)
            selected.append(txt_files[idx - 1])

        if valid and selected:
            return selected


_HELP = """\
vg2c — interactive batch translator

Usage:
  vg2c [input_dir] [output_dir]   translate .txt files
  vg2c --build                    compile vg2c into a standalone .exe
  vg2c --help                     show this message

Arguments:
  input_dir   Directory containing .txt source files.
              Relative paths are resolved from the current working directory.
              Defaults to the current working directory.
  output_dir  Directory for translated .py files.
              Relative paths are resolved from the current working directory.
              Defaults to the same directory as each source file.

Selection syntax:
  *        translate all files
  1        translate file at index 1
  1 3 5    translate files at indexes 1, 3, and 5

Output:
  A summary of successes and failures is printed at the end.

Build:
  --build installs PyInstaller if needed and produces dist/vg2c.exe.
  Run from the project root so PyInstaller can see the installed package.
"""


def cmd_build() -> None:
    """Compile vg2c into a standalone .exe using PyInstaller."""
    # Install PyInstaller on demand
    try:
        import PyInstaller  # noqa: F401  # type: ignore[import]
    except ImportError:
        print("PyInstaller not found — installing...")
        subprocess.check_call(
            [
                sys.executable, "-m", "pip", "install", "pyinstaller",
                "-i", "https://pypi.org/simple",
            ]
        )

    # Locate vg2c/cli.py via importlib so absolute path is always correct
    spec = importlib.util.find_spec("vg2c.cli")
    if spec is None or spec.origin is None:
        print("ERROR: could not locate vg2c.cli — is the package installed?", file=sys.stderr)
        sys.exit(1)

    cli_path = Path(spec.origin)
    print(f"Building from: {cli_path}")

    subprocess.check_call(
        [
            sys.executable, "-m", "PyInstaller",
            "--onefile",
            "--name", "vg2c",
            str(cli_path),
        ]
    )

    exe = Path("dist") / "vg2c.exe"
    print(f"\nBuild complete. Executable: {exe.resolve()}")


def main() -> None:
    if len(sys.argv) > 1 and sys.argv[1] in ("-h", "--help", "-help"):
        print(_HELP)
        sys.exit(0)

    if len(sys.argv) > 1 and sys.argv[1] == "--build":
        cmd_build()
        sys.exit(0)

    Logger.basicConfig(level=Logger.INFO)

    # Capture original cwd *before* any chdir so relative paths are
    # always resolved from where the user actually launched the tool.
    original_cwd = Path.cwd()

    # --- input directory ---
    if len(sys.argv) > 1:
        work_dir = Path(sys.argv[1])
        if not work_dir.is_absolute():
            work_dir = (original_cwd / work_dir).resolve()
        else:
            work_dir = work_dir.resolve()
        if not work_dir.is_dir():
            print(f"ERROR: input directory '{work_dir}' not found.", file=sys.stderr)
            sys.exit(1)
        os.chdir(work_dir)
    else:
        work_dir = original_cwd

    # --- output directory ---
    out_dir: Path | None = None
    if len(sys.argv) > 2:
        out_dir = Path(sys.argv[2])
        if not out_dir.is_absolute():
            out_dir = (original_cwd / out_dir).resolve()
        else:
            out_dir = out_dir.resolve()

    txt_files = sorted(work_dir.glob("*.txt"))

    if not txt_files:
        print(f"No .txt files found in {work_dir}")
        sys.exit(0)

    selected = _pick_files(txt_files)

    succeeded: list[str] = []
    failed: list[str] = []

    for path in selected:
        try:
            out = translate(path, out_dir)
            _log.info("OK  %s → %s", path.name, out)
            succeeded.append(path.name)
        except Exception:
            _log.error(
                "FAIL %s\n%s",
                path.name,
                traceback.format_exc().rstrip(),
            )
            failed.append(path.name)

    print("\n--- Summary ---")
    print(f"  Succeeded : {len(succeeded)}")
    print(f"  Failed    : {len(failed)}")
    if failed:
        for name in failed:
            print(f"    ✗ {name}")

    sys.exit(1 if failed else 0)


if __name__ == "__main__":
    main()
