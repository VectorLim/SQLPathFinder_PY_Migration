"""Stage 2 source-derived filesystem behavior checks.

Original ScriptHost: SPFLib/SPFUtilities/utils.py, SPFCopy(), around line 7807:
ordinary COPY/shutil.copy2 operates on files. Recursive directory copying is
an explicit opt-in extension in vg2c.runtime.files.copy_file(recurse=True).
These tests require no original Windows runtime or external services.
"""
from __future__ import annotations

import pytest

from vg2c.runtime import JobRuntime
from vg2c.runtime.files import copy_file


def test_nonrecursive_copy_does_not_silently_copy_a_directory(tmp_path):
    source = tmp_path / "source"
    source.mkdir()
    (source / "child.txt").write_text("payload", encoding="utf-8")
    with pytest.raises(IsADirectoryError, match="recurse=True"):
        copy_file("source", "destination", workdir=tmp_path)
    assert not (tmp_path / "destination").exists()


def test_explicit_recursive_copy_preserves_nested_files(tmp_path):
    source = tmp_path / "source"
    nested = source / "nested"
    nested.mkdir(parents=True)
    (nested / "payload.txt").write_text("payload", encoding="utf-8")
    job = JobRuntime(tmp_path, tmp_path)
    job.copy_file("source", "destination", recurse=True)
    assert (tmp_path / "destination" / "nested" / "payload.txt").read_text(
        encoding="utf-8"
    ) == "payload"


def test_default_file_copy_still_works(tmp_path):
    (tmp_path / "original.txt").write_text("file", encoding="utf-8")
    copy_file("original.txt", "copied.txt", workdir=tmp_path)
    assert (tmp_path / "copied.txt").read_text(encoding="utf-8") == "file"
