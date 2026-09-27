"""Boundary failures that must not be mistaken for successful job execution."""

from __future__ import annotations

import ast
from pathlib import Path

import pytest

from scripthost_portable import file_operations


def test_supported_runtime_has_no_reference_runtime_imports():
    root = Path(__file__).resolve().parents[2]
    sources = list((root / "src/scripthost_portable").glob("*.py"))
    sources += list((root / "scripthost-utilities-decompiled/SPSQL3_py/SPFLib").rglob("*.py"))
    for path in sources:
        tree = ast.parse(path.read_text(encoding="utf-8-sig"))
        for node in ast.walk(tree):
            if isinstance(node, ast.Import):
                assert all(not name.name.startswith("vg2c_new") for name in node.names), path
            elif isinstance(node, ast.ImportFrom):
                assert not (node.module or "").startswith("vg2c_new"), path


def test_robocopy_retries_io_and_rejects_unknown_switches(tmp_path, monkeypatch):
    source, target = tmp_path / "source", tmp_path / "target"
    source.mkdir()
    (source / "input.csv").write_text("data")
    copy = file_operations.shutil.copy2
    calls, sleeps = [], []

    def flaky(src, dst):
        calls.append(src)
        if len(calls) == 1:
            raise PermissionError("transient")
        return copy(src, dst)

    monkeypatch.setattr(file_operations.shutil, "copy2", flaky)
    monkeypatch.setattr(file_operations.time, "sleep", sleeps.append)
    assert file_operations.robocopy_files(str(source), str(target), ["*.csv"], 1, 2, []) == 1
    assert sleeps == [2]
    assert (target / "input.csv").read_text() == "data"
    assert file_operations.robocopy_files(str(source), str(target), ["*.csv"], 0, 0, []) == 0
    with pytest.raises(ValueError, match="Unsupported portable RoboCopy"):
        file_operations.robocopy_files(str(source), str(target), ["*"], 0, 0, ["/MIR"])
    assert (source / "input.csv").exists()


def test_copy_and_delete_dos_all_files_pattern_is_nonrecursive(tmp_path):
    source, target = tmp_path / "source", tmp_path / "target"
    source.mkdir()
    target.mkdir()
    (source / "extensionless").write_text("data")
    file_operations.copy_files(str(source / "*.*"), str(target))
    assert (target / "extensionless").read_text() == "data"
    (target / "nested").mkdir()
    (target / "nested" / "keep.txt").write_text("keep")
    file_operations.delete_files([str(target)], True)
    assert not (target / "extensionless").exists()
    assert (target / "nested" / "keep.txt").read_text() == "keep"
