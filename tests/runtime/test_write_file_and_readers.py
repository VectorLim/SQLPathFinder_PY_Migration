"""Unit tests for write_file and MockReader."""

from __future__ import annotations

from pathlib import Path

import pytest

from vg2c_runtime.macro import MacroState
from vg2c_runtime.readers import MockReader
from vg2c_runtime.write_file import write_file

# --- write_file ---


def test_write_file_plain(tmp_path):
    out = str(tmp_path / "out.txt")
    write_file(out, "hello world", vars=None, macro_state=None)
    assert Path(out).read_text(encoding="utf-8") == "hello world"


def test_write_file_substitutes_from_vars(tmp_path):
    out = str(tmp_path / "out.txt")
    write_file(out, "hi <<<NAME>>>", vars={"NAME": "Alice"}, macro_state=None)
    assert "Alice" in Path(out).read_text(encoding="utf-8")


def test_write_file_substitutes_from_macro_state(tmp_path):
    m = MacroState()
    m.set_named("GREETING", "Hello")
    out = str(tmp_path / "out.txt")
    write_file(out, "<<<GREETING>>> world", vars=None, macro_state=m)
    assert Path(out).read_text(encoding="utf-8") == "Hello world"


def test_write_file_vars_take_priority_over_macro(tmp_path):
    m = MacroState()
    m.set_named("X", "from_macro")
    out = str(tmp_path / "out.txt")
    write_file(out, "<<<X>>>", vars={"X": "from_vars"}, macro_state=m)
    assert Path(out).read_text(encoding="utf-8") == "from_vars"


def test_write_file_auto_mkdir(tmp_path):
    out = str(tmp_path / "a" / "b" / "c.txt")
    write_file(out, "x", vars=None, macro_state=None)
    assert Path(out).exists()


# --- MockReader ---


def test_mock_reader_exact_match():
    r = MockReader({"SELECT 1": [{"val": "1"}]})
    rows = r.read("SELECT 1")
    assert rows == [{"val": "1"}]


def test_mock_reader_substring_match():
    r = MockReader({"FROM my_table": [{"id": "42"}]})
    rows = r.read("SELECT * FROM my_table WHERE x = 1")
    assert rows == [{"id": "42"}]


def test_mock_reader_no_match_returns_empty():
    r = MockReader({})
    rows = r.read("SELECT anything")
    assert rows == []


def test_mock_reader_returns_copies():
    canned = [{"a": "1"}]
    r = MockReader({"key": canned})
    result = r.read("key")
    result.append({"extra": "row"})
    assert len(r.read("key")) == 1  # original not mutated
