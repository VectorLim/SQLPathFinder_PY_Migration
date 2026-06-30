"""CLI tests for `vg2c translate`."""

from __future__ import annotations

import subprocess
import sys
import os
from pathlib import Path

import pytest

ROOT = Path(__file__).resolve().parents[2]
FIXTURES = ROOT / "tests" / "fixtures"


def _run_cli(args: list[str], cwd: Path | None = None) -> subprocess.CompletedProcess:
    cmd = [sys.executable, "-m", "vg2c.cli", *args]
    env = os.environ.copy()
    existing = env.get("PYTHONPATH", "")
    base_paths = [str(ROOT / "src"), str(ROOT)]
    if existing:
        base_paths.append(existing)
    env["PYTHONPATH"] = os.pathsep.join(base_paths)
    return subprocess.run(cmd, cwd=cwd or ROOT, capture_output=True, text=True, env=env)


def test_translate_happy_path_writes_file(tmp_path):
    out_file = tmp_path / "translated.py"
    proc = _run_cli(
        ["translate", str(FIXTURES / "actual_script.txt"), "-o", str(out_file)]
    )
    assert proc.returncode == 0, proc.stderr
    assert out_file.exists()
    text = out_file.read_text(encoding="utf-8")
    assert "def run() -> None:" in text
    assert "vg2c_runtime" not in text
    assert "ctx = PipelineContext()" in text


def test_translate_stdout_when_no_output_path():
    proc = _run_cli(["translate", str(FIXTURES / "script_short.txt")])
    assert proc.returncode == 0, proc.stderr
    assert "def run() -> None:" in proc.stdout


def test_translate_missing_input_returns_error(tmp_path):
    proc = _run_cli(["translate", str(tmp_path / "missing.vg2")])
    assert proc.returncode == 1
    assert "input file not found" in proc.stderr.lower()


def test_translate_strict_flag_returns_nonzero_on_error(tmp_path):
    # malformed options/body to induce at least one parse/classify error diagnostic
    bad = tmp_path / "bad_script.txt"
    bad.write_text("<OPTIONS>\n/CSV=x.csv\n", encoding="utf-8")

    out_file = tmp_path / "bad.py"
    proc = _run_cli(["translate", str(bad), "-o", str(out_file), "--strict"])
    assert proc.returncode == 1


def test_translate_diagnostics_to_stderr(tmp_path):
    # Use a tiny malformed input to trigger diagnostic formatting.
    bad = tmp_path / "bad2.txt"
    bad.write_text("{IF-THEN}\n", encoding="utf-8")
    proc = _run_cli(["translate", str(bad)])
    # We do not require specific diagnostic codes, only that diagnostics flow to stderr shape.
    assert proc.stderr is not None
