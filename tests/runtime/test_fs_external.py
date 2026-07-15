"""Unit tests for FileSystemOps and ExternalProcess."""

from __future__ import annotations

from pathlib import Path
import sys

import pytest

from vg2c.utilities.external import ExternalProcess
from vg2c.utilities.fs_ops import FileSystemOps

# --- FileSystemOps ---


def test_copy_file(tmp_path):
    src = tmp_path / "src.txt"
    src.write_text("hello")
    dst = tmp_path / "sub" / "dst.txt"
    FileSystemOps().copy(src, dst)
    assert dst.read_text() == "hello"


def test_rename(tmp_path):
    src = tmp_path / "old.txt"
    src.write_text("data")
    dst = tmp_path / "new.txt"
    FileSystemOps().rename(src, dst)
    assert dst.exists()
    assert not src.exists()


def test_delete_file(tmp_path):
    f = tmp_path / "del.txt"
    f.write_text("x")
    FileSystemOps().delete([f])
    assert not f.exists()


def test_delete_missing_file_no_error(tmp_path):
    FileSystemOps().delete([tmp_path / "nonexistent.txt"])  # should not raise


def test_delete_multiple(tmp_path):
    files = [tmp_path / f"f{i}.txt" for i in range(3)]
    for f in files:
        f.write_text("x")
    FileSystemOps().delete(files)
    assert not any(f.exists() for f in files)


# --- ExternalProcess ---


def test_run_returns_exit_code():
    proc = ExternalProcess()
    code = proc.run([sys.executable, "-c", "import sys; sys.exit(0)"])
    assert code == 0


def test_run_non_zero_exit():
    proc = ExternalProcess()
    code = proc.run([sys.executable, "-c", "import sys; sys.exit(42)"])
    assert code == 42
