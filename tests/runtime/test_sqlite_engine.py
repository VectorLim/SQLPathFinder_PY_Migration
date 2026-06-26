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
