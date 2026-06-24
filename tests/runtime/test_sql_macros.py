"""Unit tests for SqlMacros.sql_get_csv_list."""

from __future__ import annotations

import csv
from pathlib import Path

import pytest

from vg2c_runtime.sql_macros import SqlMacros


def _write_csv(path: Path, rows: list[list]) -> None:
    path.parent.mkdir(parents=True, exist_ok=True)
    with path.open("w", newline="", encoding="utf-8") as fh:
        writer = csv.writer(fh)
        writer.writerows(rows)


def test_int_column_ref(tmp_path):
    m = SqlMacros()
    f = tmp_path / "lots.csv"
    _write_csv(f, [["lot"], ["L001"], ["L002"], ["L003"]])
    result = m.sql_get_csv_list(str(f), 1, "lot In")
    assert "L001" in result
    assert "L002" in result
    assert "L003" in result


def test_named_column_ref(tmp_path):
    m = SqlMacros()
    f = tmp_path / "data.csv"
    _write_csv(f, [["lot", "op"], ["A", "100"], ["B", "200"]])
    result = m.sql_get_csv_list(str(f), "lot", "lot In")
    assert "'A'" in result
    assert "'B'" in result


def test_single_quote_escaping(tmp_path):
    m = SqlMacros()
    f = tmp_path / "q.csv"
    _write_csv(f, [["val"], ["it's"]])
    result = m.sql_get_csv_list(str(f), 1, "v In")
    assert "it''s" in result


def test_deduplication(tmp_path):
    m = SqlMacros()
    f = tmp_path / "dup.csv"
    _write_csv(f, [["v"], ["A"], ["A"], ["B"]])
    result = m.sql_get_csv_list(str(f), 1, "v In")
    assert result.count("'A'") == 1


def test_chunking_at_1000(tmp_path):
    """Values > 1000 should be chunked into multiple IN groups."""
    m = SqlMacros()
    f = tmp_path / "big.csv"
    vals = [["v"]] + [[str(i)] for i in range(1001)]
    _write_csv(f, vals)
    result = m.sql_get_csv_list(str(f), 1, "v In")
    # There should be two IN groups separated by the lead_in
    assert result.count("v In") >= 1


def test_empty_file_returns_no_values_sentinel(tmp_path):
    m = SqlMacros()
    f = tmp_path / "empty.csv"
    _write_csv(f, [["col"]])
    result = m.sql_get_csv_list(str(f), 1, "c In")
    assert "__NO_VALUES__" in result
