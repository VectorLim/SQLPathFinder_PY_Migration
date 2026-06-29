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


def test_write_empty_with_header_writes_header_only(tmp_path):
    """When header is provided and content is empty, write header-only CSV."""
    csv_io = CsvIO()
    out = str(tmp_path / "header_only.csv")
    csv_io.write(out, [], header=["col1", "col2", "col3"])
    content = Path(out).read_text(encoding="utf-8")
    assert content == "col1,col2,col3\n"


def test_write_dicts_reorders_to_header(tmp_path):
    """When header is provided for dicts, output columns match header order."""
    csv_io = CsvIO()
    rows = [{"b": "2", "a": "1", "c": "3"}, {"b": "5", "a": "4", "c": "6"}]
    out = str(tmp_path / "reordered.csv")
    csv_io.write(out, rows, header=["a", "b", "c"])

    lines = Path(out).read_text(encoding="utf-8").splitlines()
    assert lines[0] == "a,b,c"
    assert lines[1] == "1,2,3"
    assert lines[2] == "4,5,6"


def test_write_dicts_fills_missing_columns_with_empty(tmp_path):
    """When declared header includes columns not in dict, fill with empty string."""
    csv_io = CsvIO()
    rows = [{"a": "1", "b": "2"}]
    out = str(tmp_path / "missing.csv")
    csv_io.write(out, rows, header=["a", "b", "c", "d"])

    lines = Path(out).read_text(encoding="utf-8").splitlines()
    assert lines[0] == "a,b,c,d"
    assert lines[1] == "1,2,,"


def test_write_lists_with_header_drops_duplicate_header_row(tmp_path):
    """When header is provided and data includes a row equal to header, drop it."""
    csv_io = CsvIO()
    # First row duplicates the header
    rows = [["col1", "col2"], ["val1", "val2"], ["val3", "val4"]]
    out = str(tmp_path / "dedup.csv")
    csv_io.write(out, rows, header=["col1", "col2"])

    lines = Path(out).read_text(encoding="utf-8").splitlines()
    assert len(lines) == 3  # header + 2 data rows
    assert lines[0] == "col1,col2"
    assert lines[1] == "val1,val2"
    assert lines[2] == "val3,val4"


def test_write_dataframe_reindexed_to_header(tmp_path):
    """When header is provided for DataFrame, reindex columns to match."""
    import pandas as pd

    csv_io = CsvIO()
    df = pd.DataFrame({"b": [2, 5], "a": [1, 4], "c": [3, 6]})
    out = str(tmp_path / "df_reindexed.csv")
    csv_io.write(out, df, header=["a", "b", "c"])

    lines = Path(out).read_text(encoding="utf-8").splitlines()
    assert lines[0] == "a,b,c"
    assert lines[1] == "1,2,3"
    assert lines[2] == "4,5,6"


def test_write_dataframe_fills_missing_columns(tmp_path):
    """When declared header includes columns not in DataFrame, fill with empty."""
    import pandas as pd

    csv_io = CsvIO()
    df = pd.DataFrame({"a": [1], "b": [2]})
    out = str(tmp_path / "df_missing.csv")
    csv_io.write(out, df, header=["a", "b", "c"])

    lines = Path(out).read_text(encoding="utf-8").splitlines()
    assert lines[0] == "a,b,c"
    assert lines[1] == "1,2,"
    assert Path(out).exists()
