"""Unit tests for MacroState.write_file and PipelineContext reader snippets."""

from __future__ import annotations

from pathlib import Path

import pytest

from vg2c.utilities.macro_state import MacroState
from vg2c.utilities.pipeline_context import PipelineContext

PIPELINE_CONTEXT_SNIPPET = PipelineContext.get_source()

# --- MacroState.substitute and FileSystemOps/PipelineContext write_file ---


def test_substitute_plain():
    m = MacroState()
    assert m.substitute("hello world", vars=None) == "hello world"


def test_substitute_substitutes_from_vars():
    m = MacroState()
    assert m.substitute("hi <<<NAME>>>", vars={"NAME": "Alice"}) == "hi Alice"


def test_substitute_substitutes_from_macro_state():
    m = MacroState()
    m.set_named("GREETING", "Hello")
    assert m.substitute("<<<GREETING>>> world", vars=None) == "Hello world"


def test_substitute_vars_take_priority_over_macro():
    m = MacroState()
    m.set_named("X", "from_macro")
    assert m.substitute("<<<X>>>", vars={"X": "from_vars"}) == "from_vars"


def test_fs_ops_write_file_auto_mkdir(tmp_path):
    from vg2c.utilities.fs_ops import FileSystemOps

    out = str(tmp_path / "a" / "b" / "c.txt")
    fs = FileSystemOps()
    fs.write_file(out, "x")
    assert Path(out).exists()
    assert Path(out).read_text(encoding="utf-8") == "x"


def test_pipeline_context_write_file(tmp_path):
    ctx = PipelineContext()
    out = str(tmp_path / "ctx_out.txt")
    ctx.macro.set_named("USER", "Bob")
    ctx.write_file(out, "hello <<<USER>>>")
    assert Path(out).read_text(encoding="utf-8") == "hello Bob"


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
        def substitute(self, sql: str, vars: dict[str, str] | None = None) -> str:
            return sql.replace("<<<X>>>", "42")

        def named(self, name: str) -> str:
            return ""

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


def test_run_query_explicit_node_overrides_everything():
    calls: list[tuple[str, str]] = []

    class FakeResult:
        columns = ["COL_A"]

    class FakeReader:
        def read(self, site: str, query: str):
            calls.append((site, query))
            return FakeResult()

    class FakeMacro:
        def substitute(self, sql: str, vars: dict[str, str] | None = None) -> str:
            return sql

        def named(self, name: str) -> str:
            return "PG"  # script default; explicit kwarg below must win

    class FakeCsvIo:
        def write(self, output: str, result, header=None) -> None:
            pass

    ctx = object.__new__(PipelineContext)
    ctx.macro = FakeMacro()
    ctx.csv_io = FakeCsvIo()
    ctx.run_query("select 1", "out.csv", FakeReader(), node="KM")

    assert calls == [("KM", "select 1")]


def test_run_query_uses_macro_node_when_no_explicit_override():
    calls: list[tuple[str, str]] = []

    class FakeResult:
        columns = ["COL_A"]

    class FakeReader:
        def read(self, site: str, query: str):
            calls.append((site, query))
            return FakeResult()

    class FakeMacro:
        def substitute(self, sql: str, vars: dict[str, str] | None = None) -> str:
            return sql

        def named(self, name: str) -> str:
            assert name == "NODE"
            return "PG"

    class FakeCsvIo:
        def write(self, output: str, result, header=None) -> None:
            pass

    ctx = object.__new__(PipelineContext)
    ctx.macro = FakeMacro()
    ctx.csv_io = FakeCsvIo()
    ctx.run_query("select 1", "out.csv", FakeReader())

    assert calls == [("PG", "select 1")]


def test_run_query_falls_back_to_env_var_default(monkeypatch):
    calls: list[tuple[str, str]] = []

    class FakeResult:
        columns = ["COL_A"]

    class FakeReader:
        def read(self, site: str, query: str):
            calls.append((site, query))
            return FakeResult()

    class FakeMacro:
        def substitute(self, sql: str, vars: dict[str, str] | None = None) -> str:
            return sql

        def named(self, name: str) -> str:
            return ""

    class FakeCsvIo:
        def write(self, output: str, result, header=None) -> None:
            pass

    monkeypatch.setenv("VG2C_DEFAULT_NODE", "XX")

    ctx = object.__new__(PipelineContext)
    ctx.macro = FakeMacro()
    ctx.csv_io = FakeCsvIo()
    ctx.run_query("select 1", "out.csv", FakeReader())

    assert calls == [("XX", "select 1")]


def test_run_query_requires_reader_behavior():
    class FakeMacro:
        def substitute(self, sql: str, vars: dict[str, str] | None = None) -> str:
            return sql

        def named(self, name: str) -> str:
            return ""

    ctx = object.__new__(PipelineContext)
    ctx.macro = FakeMacro()

    class NotAReader:
        pass

    with pytest.raises(AttributeError):
        ctx.run_query("select 1", "out.csv", NotAReader())
