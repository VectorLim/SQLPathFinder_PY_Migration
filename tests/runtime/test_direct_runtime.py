"""Offline checks for direct operations and the installed import boundary."""

import importlib
import json
import os
from pathlib import Path
import subprocess
import sys

import pandas as pd
import pytest

from vg2c.runtime import SqliteReader, execute_sql, read_macro_row


def test_runtime_import_excludes_compiler():
    code = """
import json, sys
import vg2c.runtime
print(json.dumps(sorted(sys.modules)))
"""
    result = subprocess.run([sys.executable, "-c", code], capture_output=True, text=True,
                            check=True, env={**os.environ, "PYTHONPATH": str(Path(__file__).parents[2] / "src")})
    modules = json.loads(result.stdout)
    forbidden = ("vg2c.emitter", "vg2c.frontend", "vg2c.resolver", "vg2c.compilation",
                 "vg2c.utility_metadata", "vg2c_ui", "scripthost_portable")
    assert not [name for name in modules if name.startswith(forbidden)]


def test_lazy_facade_preserves_compiler_exports():
    from vg2c import compile_document
    assert callable(compile_document)
    assert importlib.import_module("vg2c").CompilationResult


def test_sql_edits_binds_and_explicit_roots(tmp_path, monkeypatch):
    assets = tmp_path / "assets"
    assets.mkdir()
    workdir = tmp_path / "work"
    workdir.mkdir()
    (workdir / "data.csv").write_text("ID\n1\n2\n")
    sql = assets / "query.sql"
    sql.write_text("select ID from data where ID = :id;")
    monkeypatch.chdir(assets)
    execute_sql(sql, reader=SqliteReader(), inputs=["data.csv"], output="result.csv",
                workdir=workdir, params={"id": "2"})
    assert (workdir / "result.csv").read_text() == "ID\n2\n"
    sql.write_text("select 3 as ID;")
    execute_sql(sql, reader=SqliteReader(), output="result.csv", workdir=workdir)
    assert (workdir / "result.csv").read_text() == "ID\n3\n"
    assert not (assets / "result.csv").exists()


def test_two_nodes_route_independently(tmp_path):
    calls = []

    class Reader:
        def read(self, *, site, query):
            calls.append((site, query))
            return pd.DataFrame({"SITE": [site]})

    sql = tmp_path / "query.sql"
    sql.write_text("select '<<<label>>>'")
    reader = Reader()
    for site, node in [("PG", "PG.[A12_PROD_0.].MARS"), ("KM", "KM.[A15_PROD_21.].MARS")]:
        execute_sql(sql, reader=reader, node=node, output=f"{site}.csv", workdir=tmp_path,
                    macros={"label": site})
    assert calls == [("PG", "select 'PG'"), ("KM", "select 'KM'")]
    assert (tmp_path / "PG.csv").read_text() == "site\nPG\n"


def test_missing_node_and_unsupported_binds_never_execute(tmp_path):
    class Reader:
        def read(self, *, site, query):
            pytest.fail("reader must not run")

    sql = tmp_path / "query.sql"
    sql.write_text("select :id")
    for kwargs, message in [({}, "explicit node"), ({"params": {"id": 1}, "node": "PG"}, "does not support")]:
        with pytest.raises(ValueError, match=message):
            execute_sql(sql, reader=Reader(), output="out.csv", workdir=tmp_path, **kwargs)
    assert not (tmp_path / "out.csv").exists()


def test_multistatement_binds_rejected_before_reader(tmp_path):
    class Reader:
        def execute(self, sql, inputs, *, params=None):
            pytest.fail("reader must not run")

    sql = tmp_path / "query.sql"
    sql.write_text("create table unsafe(x); select :id;")
    with pytest.raises(ValueError, match="exactly one"):
        execute_sql(sql, reader=Reader(), params={"id": 1}, output="out.csv", workdir=tmp_path)


def test_sql_semicolons_in_comments_and_strings(tmp_path):
    sql = tmp_path / "query.sql"
    sql.write_text("-- comment ;\nselect ';' as value; -- trailing ;\n")
    execute_sql(sql, reader=SqliteReader(), output="out.csv", workdir=tmp_path, params={"unused": 1})
    assert (tmp_path / "out.csv").read_text() == "value\n;\n"


@pytest.mark.parametrize("body, expected", [("A\n", None), ("A\nfirst\nsecond\n", {"A": "first"}), ("A,B\nx,\n", {"A": "x", "B": ""})])
def test_start_macro_cardinality(tmp_path, body, expected):
    (tmp_path / "macro.csv").write_text(body)
    assert read_macro_row("macro.csv", workdir=tmp_path) == expected


def test_values_preserve_blanks_case_and_newlines(tmp_path):
    sql = tmp_path / "query.sql"
    sql.write_text("\nselect '<<<name>>>' as value;")
    execute_sql(sql, reader=SqliteReader(), output="out.csv", workdir=tmp_path, macros={"NAME": ""})
    assert (tmp_path / "out.csv").read_text() == 'value\n""\n'
    with pytest.raises(ValueError, match="query.sql.*Unknown value"):
        execute_sql(sql, reader=SqliteReader(), output="out.csv", workdir=tmp_path)


def test_sql_get_csv_list_uses_work_root_and_rereads(tmp_path, monkeypatch):
    assets = tmp_path / "assets"
    assets.mkdir()
    work = tmp_path / "work"
    work.mkdir()
    monkeypatch.chdir(assets)
    (work / "ids.csv").write_text("ID\n1\n")
    sql = assets / "query.sql"
    sql.write_text("select '1' as ID where '1' In SQL_Get_CSV_List('ids.csv',1,'ID In');")
    execute_sql(sql, reader=SqliteReader(), output="out.csv", workdir=work)
    assert (work / "out.csv").read_text() == "ID\n1\n"
    (work / "ids.csv").write_text("ID\n2\n")
    execute_sql(sql, reader=SqliteReader(), output="out.csv", workdir=work)
    assert (work / "out.csv").read_text() == "ID\n"
