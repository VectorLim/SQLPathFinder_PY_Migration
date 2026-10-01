"""Boundary failures that must not be mistaken for successful job execution."""

from __future__ import annotations

import ast
import os
from pathlib import Path

import pytest

from scripthost_portable import file_operations


def test_supported_runtime_has_no_reference_runtime_imports():
    root = Path(__file__).resolve().parents[2]
    sources = list((root / "src/scripthost_portable").glob("*.py"))
    sources += list((root / "src/scripthost_portable/_vendor/SPSQL3_py").rglob("*.py"))
    for path in sources:
        tree = ast.parse(path.read_text(encoding="utf-8-sig"))
        for node in ast.walk(tree):
            if isinstance(node, ast.Import):
                assert all(
                    name.name.split(".")[0] not in {"vg2c", "vg2c_new", "vg2c_ui"}
                    for name in node.names
                ), path
            elif isinstance(node, ast.ImportFrom):
                assert (node.module or "").split(".")[0] not in {"vg2c", "vg2c_new", "vg2c_ui"}, (
                    path
                )


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


def test_worker_runs_with_all_retired_imports_blocked(tmp_path, monkeypatch):
    from scripthost_portable.worker import ScriptHostJob, run_job

    guard = tmp_path / "guard"
    guard.mkdir()
    (guard / "sitecustomize.py").write_text(
        "import sys\n"
        "class RetiredImportGuard:\n"
        "    def find_spec(self, fullname, path=None, target=None):\n"
        "        if fullname.split('.')[0] in {'vg2c', 'vg2c_new', 'vg2c_ui'}:\n"
        "            raise AssertionError('Retired runtime imported: ' + fullname)\n"
        "sys.meta_path.insert(0, RetiredImportGuard())\n"
    )
    monkeypatch.setenv("PYTHONPATH", str(guard) + os.pathsep + os.environ.get("PYTHONPATH", ""))
    result = run_job(
        ScriptHostJob(
            working_directory=str(tmp_path),
            script_text="<OPTIONS>\n/WRITE-FILE=Y\n/CSV=independent.txt\n</OPTIONS>\noriginal ScriptHost",
        ),
        timeout=30,
    )
    assert result.success, result
    assert (tmp_path / "independent.txt").read_text() == "original ScriptHost"


@pytest.mark.parametrize("delimiter", [",", "\t"])
def test_report_preprocessing_preserves_records_and_cleans_embedded_text(tmp_path, delimiter):
    import csv

    source = tmp_path / "input.csv"
    target = tmp_path / "clean.tmp"
    with source.open("w", encoding="utf-8-sig", newline="") as stream:
        writer = csv.writer(stream, delimiter=delimiter)
        writer.writerow(["name", "detail"])
        writer.writerow(["é", 'one,two\tthree\nfour"five'])
    file_operations.clean_delimited_file(str(source), str(target), delimiter)
    assert target.read_text(encoding="utf-8").splitlines() == [
        delimiter.join(["name", "detail"]),
        delimiter.join(["é", "one;two three fourfive"]),
    ]


def test_retired_package_trees_are_absent():
    root = Path(__file__).resolve().parents[2]
    for package in ("vg2c", "vg2c_new", "vg2c_ui"):
        assert not (root / "src" / package).exists()
