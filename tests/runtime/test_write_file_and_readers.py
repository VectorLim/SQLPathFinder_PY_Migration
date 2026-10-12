"""Direct runtime replaces the retired MacroState and PipelineContext engines."""

import pandas as pd
import pytest

from vg2c.runtime import JobRuntime, MacroStore


def test_macro_substitution_and_scopes_use_single_authority():
    store = MacroStore()
    store["USER"] = "Bob"
    assert store.substitute("hello <<<USER>>>") == "hello Bob"
    with store.scope({"USER": "Ada"}):
        assert store["user"] == "Ada"
    assert store["user"] == "Bob"
    with pytest.raises(ValueError, match="Unknown value"):
        store.substitute("<<<NOT_DEFINED>>>")


def test_direct_write_file_creates_parent_and_expands_macros(tmp_path):
    job = JobRuntime(tmp_path, tmp_path / "work", initial_macros={"USER": "Bob"})
    job.write_file("a/b/out.txt", "hello <<<USER>>>")
    assert (tmp_path / "work/a/b/out.txt").read_text() == "hello Bob"


def test_external_reader_routing_is_explicit_and_sql_is_external(tmp_path):
    class Reader:
        def __init__(self):
            self.requests = []
        def read(self, *, site, query):
            self.requests.append((site, query))
            return pd.DataFrame({"COL_A": ["42"]})

    asset = tmp_path / "sql/q.sql"
    asset.parent.mkdir()
    asset.write_text("select 42 as COL_A")
    reader = Reader()
    job = JobRuntime(tmp_path, tmp_path / "work")
    job.sql("sql/q.sql", reader=reader, output="out.csv", node="PG.MARS", header=["col_a"])
    assert reader.requests == [("PG", "select 42 as COL_A")]
    assert (tmp_path / "work/out.csv").read_text() == "col_a\n42\n"
    with pytest.raises(ValueError, match="explicit node/site"):
        job.sql("sql/q.sql", reader=reader, output="out.csv")
