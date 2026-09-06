"""CLI tests for the interactive batch translator."""

from __future__ import annotations

import os
import shutil
import subprocess
import sys
from pathlib import Path

ROOT = Path(__file__).resolve().parents[2]
FIXTURES = ROOT / "tests" / "fixtures"


def _run_cli(
    args: list[str], *, selection: str = "1\n", cwd: Path | None = None
) -> subprocess.CompletedProcess[str]:
    cmd = [sys.executable, "-m", "vg2c.cli", *args]
    env = os.environ.copy()
    existing = env.get("PYTHONPATH", "")
    base_paths = [str(ROOT / "src"), str(ROOT)]
    if existing:
        base_paths.append(existing)
    env["PYTHONPATH"] = os.pathsep.join(base_paths)
    return subprocess.run(
        cmd,
        cwd=cwd or ROOT,
        capture_output=True,
        text=True,
        input=selection,
        env=env,
    )


def _copy_script(directory: Path, name: str = "script_short.txt") -> Path:
    directory.mkdir(parents=True, exist_ok=True)
    target = directory / name
    shutil.copy(FIXTURES / "script_short.txt", target)
    return target


def test_translate_selected_file_to_output_directory(tmp_path):
    input_dir = tmp_path / "input"
    output_dir = tmp_path / "output"
    _copy_script(input_dir)

    proc = _run_cli([str(input_dir), str(output_dir)])

    assert proc.returncode == 0, proc.stderr
    out_file = output_dir / "script_short.py"
    assert out_file.exists()
    text = out_file.read_text(encoding="utf-8")
    assert "def run() -> None:" in text
    assert "ctx = PipelineContext()" in text


def test_translate_defaults_output_to_input_directory(tmp_path):
    input_dir = tmp_path / "input"
    _copy_script(input_dir)

    proc = _run_cli([str(input_dir)])

    assert proc.returncode == 0, proc.stderr
    assert (input_dir / "script_short.py").exists()


def test_translate_all_selected_files(tmp_path):
    input_dir = tmp_path / "input"
    output_dir = tmp_path / "output"
    _copy_script(input_dir, "a.txt")
    _copy_script(input_dir, "b.txt")

    proc = _run_cli([str(input_dir), str(output_dir)], selection="*\n")

    assert proc.returncode == 0, proc.stderr
    assert (output_dir / "a.py").exists()
    assert (output_dir / "b.py").exists()


def test_missing_input_directory_returns_error(tmp_path):
    missing = tmp_path / "missing"
    proc = _run_cli([str(missing)])

    assert proc.returncode == 1
    assert "input directory" in proc.stderr.lower()
    assert "not found" in proc.stderr.lower()


def test_empty_input_directory_exits_cleanly(tmp_path):
    input_dir = tmp_path / "empty"
    input_dir.mkdir()

    proc = _run_cli([str(input_dir)])

    assert proc.returncode == 0
    assert "No .txt files found" in proc.stdout


def test_help_documents_batch_directory_interface():
    proc = _run_cli(["--help"], selection="")

    assert proc.returncode == 0
    assert "vg2c [input_dir] [output_dir]" in proc.stdout
    assert "translate .txt files" in proc.stdout
