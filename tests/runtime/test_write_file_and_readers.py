"""Unit tests for write_file and MockReader."""

from __future__ import annotations

from pathlib import Path

import pytest

from vg2c.emitter.macro import MacroState
from vg2c.emitter.readers import READER_SNIPPET
from vg2c.emitter.write_file import write_file

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


def test_reader_snippet_includes_runtime_imports():
    assert (
        "from datasyncx.readers import AriesReader, MarsReader, OracleReader"
        in READER_SNIPPET
    )
    assert "import re" in READER_SNIPPET
