"""Unit tests for CsvIO."""

from __future__ import annotations

from pathlib import Path

import pytest

from vg2c.utilities.csv_io import CsvIO


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


def test_single_row_returns_only_data_row(tmp_path):
    csv_io = CsvIO()
    out = str(tmp_path / "one.csv")
    csv_io.write(out, [{"a": "1", "b": "2"}])

    row = csv_io.single_row(out)
    assert row == {"a": "1", "b": "2"}


def test_single_row_raises_on_zero_rows(tmp_path):
    csv_io = CsvIO()
    out = str(tmp_path / "zero.csv")
    csv_io.write(out, [], header=["a", "b"])

    with pytest.raises(ValueError, match="exactly 1 data row; found 0"):
        csv_io.single_row(out)


def test_single_row_raises_on_multiple_rows(tmp_path):
    csv_io = CsvIO()
    out = str(tmp_path / "many.csv")
    csv_io.write(out, [{"a": "1"}, {"a": "2"}])

    with pytest.raises(ValueError, match="exactly 1 data row; found >1"):
        csv_io.single_row(out)


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


def _write_csv(path: Path, rows: list[list]) -> None:
    import csv

    path.parent.mkdir(parents=True, exist_ok=True)
    with path.open("w", newline="", encoding="utf-8") as fh:
        writer = csv.writer(fh)
        writer.writerows(rows)


def test_int_column_ref(tmp_path):
    m = CsvIO()
    f = tmp_path / "lots.csv"
    _write_csv(f, [["lot"], ["L001"], ["L002"], ["L003"]])
    result = m.sql_get_csv_list(str(f), 1, "lot In")
    assert "L001" in result
    assert "L002" in result
    assert "L003" in result


def test_named_column_ref(tmp_path):
    m = CsvIO()
    f = tmp_path / "data.csv"
    _write_csv(f, [["lot", "op"], ["A", "100"], ["B", "200"]])
    result = m.sql_get_csv_list(str(f), "lot", "lot In")
    assert "'A'" in result
    assert "'B'" in result


def test_single_quote_escaping(tmp_path):
    m = CsvIO()
    f = tmp_path / "q.csv"
    _write_csv(f, [["val"], ["it's"]])
    result = m.sql_get_csv_list(str(f), 1, "v In")
    assert "it''s" in result


def test_deduplication(tmp_path):
    m = CsvIO()
    f = tmp_path / "dup.csv"
    _write_csv(f, [["v"], ["A"], ["A"], ["B"]])
    result = m.sql_get_csv_list(str(f), 1, "v In")
    assert result.count("'A'") == 1


def test_chunking_at_1000(tmp_path):
    """Values > 1000 should be chunked into multiple IN groups."""
    m = CsvIO()
    f = tmp_path / "big.csv"
    vals = [["v"]] + [[str(i)] for i in range(1001)]
    _write_csv(f, vals)
    result = m.sql_get_csv_list(str(f), 1, "v In")
    # Two IN groups separated by an OR + lead_in connector.
    assert "OR v In" in result
    assert result.count("(") == 2
    assert result.count(")") == 2
    # The macro itself emits balanced parens; the resolver appends a trailing
    # `)` when the call site has an unmatched `(<col> In ` wrap.
    assert result.endswith(")")


def test_balanced_output_when_unwrapped(tmp_path):
    """Output alone is balanced; call sites without a wrap stay valid."""
    m = CsvIO()
    f = tmp_path / "balanced.csv"
    _write_csv(f, [["v"], ["A"], ["B"]])
    result = m.sql_get_csv_list(str(f), 1, "v In")
    assert result.count("(") == result.count(")")


def test_empty_file_returns_no_values_sentinel(tmp_path):
    m = CsvIO()
    f = tmp_path / "empty.csv"
    _write_csv(f, [["col"]])
    result = m.sql_get_csv_list(str(f), 1, "c In")
    assert "__NO_VALUES__" in result
    # Sentinel is also balanced.
    assert result.count("(") == result.count(")")


def test_skips_row_that_duplicates_header(tmp_path):
    """When CSV data includes a row that duplicates header, it is skipped."""
    m = CsvIO()
    f = tmp_path / "dup_header.csv"
    # First data row duplicates the header
    _write_csv(f, [["col1", "col2"], ["col1", "col2"], ["val1", "val2"]])
    result = m.sql_get_csv_list(str(f), 1, "c In")
    # Should only extract "val1", not "col1" (which is in both header and duplicate row)
    assert "'val1'" in result
    assert result.count("'col1'") == 0  # Header value should not appear in result
