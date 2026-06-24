"""Unit tests for CsvIO."""

from __future__ import annotations

from pathlib import Path

import pytest

from vg2c_runtime.csv_io import CsvIO


def test_write_and_iter(tmp_path):
    csv_io = CsvIO()
    rows = [{"name": "Alice", "score": "90"}, {"name": "Bob", "score": "85"}]
    out = str(tmp_path / "out.csv")
    csv_io.write(out, rows)

    result = list(csv_io.iter(out))
    assert result == rows


def test_row_count(tmp_path):
    csv_io = CsvIO()
    rows = [{"a": str(i)} for i in range(5)]
    out = str(tmp_path / "data.csv")
    csv_io.write(out, rows)
    assert csv_io.row_count(out) == 5


def test_row_count_missing_file():
    csv_io = CsvIO()
    assert csv_io.row_count("nonexistent_file.csv") == 0


def test_write_empty_list(tmp_path):
    csv_io = CsvIO()
    out = str(tmp_path / "empty.csv")
    csv_io.write(out, [])
    assert Path(out).read_text() == ""


def test_write_raw_string(tmp_path):
    csv_io = CsvIO()
    out = str(tmp_path / "raw.txt")
    csv_io.write(out, "line1\nline2\n")
    assert Path(out).read_text(encoding="utf-8") == "line1\nline2\n"


def test_read_returns_path(tmp_path):
    csv_io = CsvIO()
    f = tmp_path / "f.csv"
    f.write_text("a\n1\n")
    result = csv_io.read(str(f))
    assert isinstance(result, Path)


def test_auto_mkdir(tmp_path):
    csv_io = CsvIO()
    out = str(tmp_path / "sub" / "dir" / "out.csv")
    csv_io.write(out, [{"x": "1"}])
    assert Path(out).exists()
