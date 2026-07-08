"""Unit tests for MacroState.write_file and PipelineContext reader snippets."""

from __future__ import annotations

from pathlib import Path

import pytest

from vg2c.emitter.utilities.macro_state import MacroState
from vg2c.emitter.utilities.pipeline_context import PipelineContext

PIPELINE_CONTEXT_SNIPPET = PipelineContext.get_source()

# --- MacroState.write_file ---


def test_write_file_plain(tmp_path):
    out = str(tmp_path / "out.txt")
    m = MacroState()
    m.write_file(out, "hello world", vars=None)
    assert Path(out).read_text(encoding="utf-8") == "hello world"


def test_write_file_substitutes_from_vars(tmp_path):
    out = str(tmp_path / "out.txt")
    m = MacroState()
    m.write_file(out, "hi <<<NAME>>>", vars={"NAME": "Alice"})
    assert "Alice" in Path(out).read_text(encoding="utf-8")


def test_write_file_substitutes_from_macro_state(tmp_path):
    m = MacroState()
    m.set_named("GREETING", "Hello")
    out = str(tmp_path / "out.txt")
    m.write_file(out, "<<<GREETING>>> world", vars=None)
    assert Path(out).read_text(encoding="utf-8") == "Hello world"


def test_write_file_vars_take_priority_over_macro(tmp_path):
    m = MacroState()
    m.set_named("X", "from_macro")
    out = str(tmp_path / "out.txt")
    m.write_file(out, "<<<X>>>", vars={"X": "from_vars"})
    assert Path(out).read_text(encoding="utf-8") == "from_vars"


def test_write_file_auto_mkdir(tmp_path):
    out = str(tmp_path / "a" / "b" / "c.txt")
    m = MacroState()
    m.write_file(out, "x", vars=None)
    assert Path(out).exists()


def test_pipeline_context_snippet_includes_datasyncx_reader_logic():
    assert "class PipelineContext" in PIPELINE_CONTEXT_SNIPPET
    assert "def _read_datasyncx" in PIPELINE_CONTEXT_SNIPPET
    assert "reader" in PIPELINE_CONTEXT_SNIPPET


def test_run_query_reads_datasyncx_and_lowercases_columns():
    calls: list[tuple[str, str]] = []
    captured: dict[str, object] = {}

    class FakeResult:
        columns = ["COL_A", "COL_B"]

    class FakeReader:
        def read(self, site: str, query: str):
            calls.append((site, query))
            return FakeResult()

    class FakeMacro:
        def substitute_sql(self, sql: str) -> str:
            return sql.replace("<<<X>>>", "42")

    class FakeCsvIo:
        def write(self, output: str, result, header=None) -> None:
            captured["output"] = output
            captured["result"] = result
            captured["header"] = header

    ctx = object.__new__(PipelineContext)
    ctx.macro = FakeMacro()
    ctx.csv_io = FakeCsvIo()
    ctx.run_query(
        "select <<<X>>> as COL_A",
        "out.csv",
        FakeReader(),
        header=["col_a"],
    )

    assert calls == [("KM", "select 42 as COL_A")]
    assert captured["output"] == "out.csv"
    assert captured["header"] == ["col_a"]
    assert captured["result"].columns == ["col_a", "col_b"]


def test_run_query_requires_reader_behavior():
    class FakeMacro:
        def substitute_sql(self, sql: str) -> str:
            return sql

    ctx = object.__new__(PipelineContext)
    ctx.macro = FakeMacro()

    class NotAReader:
        pass

    with pytest.raises(AttributeError):
        ctx.run_query("select 1", "out.csv", NotAReader())
