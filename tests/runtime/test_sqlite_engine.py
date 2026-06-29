"""Unit tests for SqliteEngine."""

from __future__ import annotations

import csv
from pathlib import Path

import pytest

from vg2c.emitter.sqlite_engine import SqliteEngine


def _write_csv(path: Path, rows: list[dict]) -> None:
    path.parent.mkdir(parents=True, exist_ok=True)
    with path.open("w", newline="", encoding="utf-8") as fh:
        if rows:
            writer = csv.DictWriter(fh, fieldnames=list(rows[0].keys()))
            writer.writeheader()
            writer.writerows(rows)


def test_simple_select(tmp_path):
    engine = SqliteEngine()
    inp = tmp_path / "data.csv"
    _write_csv(inp, [{"name": "Alice", "score": "90"}, {"name": "Bob", "score": "85"}])

    out = str(tmp_path / "result.csv")
    engine.run_join(
        sql="SELECT name, score FROM [data]",
        inputs=[str(inp)],
        output=out,
    )
    rows = list(csv.DictReader(Path(out).open(newline="", encoding="utf-8")))
    assert len(rows) == 2
    names = {r["name"] for r in rows}
    assert names == {"Alice", "Bob"}


def test_multi_statement_drop_create_select(tmp_path):
    """DROP INDEX / CREATE INDEX / SELECT should all work without error."""
    engine = SqliteEngine()
    inp = tmp_path / "items.csv"
    _write_csv(inp, [{"id": "1", "val": "a"}, {"id": "2", "val": "b"}])

    sql = """
DROP INDEX IF EXISTS idx_val;
CREATE INDEX IF NOT EXISTS idx_val ON [items] (val);
SELECT id, val FROM [items] WHERE val = 'a'
"""
    out = str(tmp_path / "out.csv")
    engine.run_join(sql=sql, inputs=[str(inp)], output=out)
    rows = list(csv.DictReader(Path(out).open(newline="", encoding="utf-8")))
    assert len(rows) == 1
    assert rows[0]["val"] == "a"


def test_output_file_created(tmp_path):
    engine = SqliteEngine()
    inp = tmp_path / "t.csv"
    _write_csv(inp, [{"x": "1"}])
    out = str(tmp_path / "sub" / "out.csv")
    engine.run_join(sql="SELECT x FROM [t]", inputs=[str(inp)], output=out)
    assert Path(out).exists()


def test_join_two_tables(tmp_path):
    engine = SqliteEngine()
    a = tmp_path / "a.csv"
    b = tmp_path / "b.csv"
    _write_csv(a, [{"id": "1", "name": "Alice"}, {"id": "2", "name": "Bob"}])
    _write_csv(b, [{"id": "1", "score": "90"}])
    out = str(tmp_path / "joined.csv")
    engine.run_join(
        sql="SELECT a.name, b.score FROM [a] a LEFT JOIN [b] b ON a.id = b.id WHERE b.score IS NOT NULL",
        inputs=[str(a), str(b)],
        output=out,
    )
    rows = list(csv.DictReader(Path(out).open(newline="", encoding="utf-8")))
    assert len(rows) == 1
    assert rows[0]["name"] == "Alice"


def test_crosstab_macro_expansion_in_sqlite_join(tmp_path):
    engine = SqliteEngine()
    a0 = tmp_path / "a0.csv"
    _write_csv(
        a0,
        [
            {
                "facility": "KM",
                "lot": "L1",
                "SUBPLANEANGLEX": "1.25",
                "SUBPLANEANGLEY": "-0.75",
            }
        ],
    )

    out = str(tmp_path / "out_crosstab.csv")
    engine.run_join(
        sql=(
            "SELECT a0.[facility] AS [facility], a0.[lot] AS [lot], "
            "CrossTab->[[a0,15507;:Y]] "
            "FROM [a0] a0"
        ),
        inputs=[str(a0)],
        output=out,
    )

    rows = list(csv.DictReader(Path(out).open(newline="", encoding="utf-8")))
    assert len(rows) == 1
    assert rows[0]["facility"] == "KM"
    assert rows[0]["lot"] == "L1"
    assert rows[0]["SUBPLANEANGLEX"] == "1.25"
    assert rows[0]["SUBPLANEANGLEY"] == "-0.75"


def test_run_join_uses_declared_header_for_output(tmp_path):
    """When header is provided, output uses that exact column list and order."""
    engine = SqliteEngine()
    inp = tmp_path / "data.csv"
    _write_csv(inp, [{"b": "2", "a": "1", "c": "3"}])

    out = str(tmp_path / "result.csv")
    engine.run_join(
        sql="SELECT a, b, c FROM [data]",
        inputs=[str(inp)],
        output=out,
        header=["a", "b", "c"],
    )

    lines = Path(out).read_text(encoding="utf-8").splitlines()
    assert lines[0] == "a,b,c"
    assert lines[1] == "1,2,3"


def test_run_join_zero_rows_with_header_still_writes_header(tmp_path):
    """When query returns zero rows but header is declared, output contains header."""
    engine = SqliteEngine()
    inp = tmp_path / "data.csv"
    _write_csv(inp, [{"x": "1"}])

    out = str(tmp_path / "empty_result.csv")
    engine.run_join(
        sql="SELECT x FROM [data] WHERE x = '999'",
        inputs=[str(inp)],
        output=out,
        header=["x"],
    )

    content = Path(out).read_text(encoding="utf-8")
    assert content == "x\n"


def test_load_csv_as_table_empty_file_does_not_crash(tmp_path):
    """Empty CSV files are loaded as empty placeholder tables without crashing."""
    import sqlite3

    engine = SqliteEngine()
    empty_csv = tmp_path / "empty.csv"
    empty_csv.write_text("", encoding="utf-8")

    out = str(tmp_path / "result.csv")
    # Should not crash; creates placeholder table
    engine.run_join(
        sql="SELECT * FROM [empty]",
        inputs=[str(empty_csv)],
        output=out,
    )

    lines = Path(out).read_text(encoding="utf-8").splitlines()
    # Placeholder table has one column, zero rows
    assert len(lines) == 1  # just header
    assert lines[0] == "_empty"


def test_load_csv_as_table_skips_duplicated_header_row(tmp_path):
    """When CSV data includes a row that duplicates the header, it is skipped."""
    inp = tmp_path / "dup_header.csv"
    # Write CSV manually with header row duplicated in data
    inp.write_text("col1,col2\ncol1,col2\nval1,val2\n", encoding="utf-8")

    engine = SqliteEngine()
    out = str(tmp_path / "result.csv")
    engine.run_join(
        sql="SELECT col1, col2 FROM [dup_header]",
        inputs=[str(inp)],
        output=out,
    )

    lines = Path(out).read_text(encoding="utf-8").splitlines()
    assert len(lines) == 2  # header + 1 data row (duplicate header row dropped)
    assert lines[0] == "col1,col2"
    assert lines[1] == "val1,val2"


def test_run_join_declared_header_projects_missing_columns_as_empty(tmp_path):
    """When declared header includes columns not in result, fill with empty."""
    engine = SqliteEngine()
    inp = tmp_path / "data.csv"
    _write_csv(inp, [{"a": "1", "b": "2"}])

    out = str(tmp_path / "result.csv")
    engine.run_join(
        sql="SELECT a, b FROM [data]",
        inputs=[str(inp)],
        output=out,
        header=["a", "b", "c", "d"],
    )

    lines = Path(out).read_text(encoding="utf-8").splitlines()
    assert lines[0] == "a,b,c,d"
    assert lines[1] == "1,2,,"
