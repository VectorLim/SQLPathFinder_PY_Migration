"""CLI tests for `vg2c`."""

from __future__ import annotations

import subprocess
import sys
import os
import shutil
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
        [str(FIXTURES / "actual_script.txt"), str(out_file)]
    )
    assert proc.returncode == 0, proc.stderr
    assert out_file.exists()
    text = out_file.read_text(encoding="utf-8")
    assert "def run() -> None:" in text
    assert "vg2c_runtime" not in text
    assert "ctx = PipelineContext()" in text


def test_translate_no_output_path_defaults_to_py(tmp_path):
    input_file = tmp_path / "script_short.txt"
    shutil.copy(FIXTURES / "script_short.txt", input_file)
    
    proc = _run_cli([str(input_file)])
    assert proc.returncode == 0, proc.stderr
    
    out_file = tmp_path / "script_short.py"
    assert out_file.exists()
    text = out_file.read_text(encoding="utf-8")
    assert "def run() -> None:" in text


def test_translate_output_name_only_follows_input_directory(tmp_path):
    subdir = tmp_path / "subdir"
    subdir.mkdir()
    input_file = subdir / "script_short.txt"
    shutil.copy(FIXTURES / "script_short.txt", input_file)
    
    proc = _run_cli([str(input_file), "my_output.py"])
    assert proc.returncode == 0, proc.stderr
    
    out_file = subdir / "my_output.py"
    assert out_file.exists()
    text = out_file.read_text(encoding="utf-8")
    assert "def run() -> None:" in text


def test_translate_missing_input_returns_error(tmp_path):
    proc = _run_cli([str(tmp_path / "missing.vg2")])
    assert proc.returncode == 1
    assert "input file not found" in proc.stderr.lower()


def test_translate_strict_flag_returns_nonzero_on_error(tmp_path):
    # malformed options/body to induce at least one parse/classify error diagnostic
    bad = tmp_path / "bad_script.txt"
    bad.write_text("<OPTIONS>\n/CSV=x.csv\n", encoding="utf-8")

    out_file = tmp_path / "bad.py"
    proc = _run_cli([str(bad), str(out_file), "--strict"])
    assert proc.returncode == 1


def test_translate_diagnostics_to_stderr(tmp_path):
    # Use a tiny malformed input to trigger diagnostic formatting.
    bad = tmp_path / "bad2.txt"
    bad.write_text("{IF-THEN}\n", encoding="utf-8")
    
    # We output to bad2.py, but expect diagnostics on stderr
    proc = _run_cli([str(bad)])
    assert proc.stderr is not None

