"""Behavior at the original SQLite algorithm's portable boundaries."""

import csv
import os
from pathlib import Path

import pytest

from scripthost_portable.runtime import _spf_manager_type


@pytest.fixture
def table(tmp_path, monkeypatch):
    _spf_manager_type()
    from SPFLib.SPFUtilities.memtable import MemTable

    monkeypatch.chdir(tmp_path)
    return MemTable()


def query(table, **options):
    arguments = dict(
        Site1=".\\", Command1="SELECT * FROM Input", MyTables="MyInput.csv:Input",
        WorkDir=".\\", OutExcel="result.csv", FinalRow=0, MyLocal="Y",
        MyText="QUIET", PreProcCSV=False, ll_QuoteCSV=True, ll_NoHdrs=False,
    )
    arguments.update(options)
    return table.Run_SQLite(**arguments)


def rows(path="result.csv"):
    with open(path, encoding="utf-8-sig", newline="") as stream:
        return list(csv.reader(stream))


@pytest.mark.skipif(os.name == "nt", reason="Requires a real POSIX filesystem")
def test_mixed_case_imports_keep_first_seen_order_and_current_workdir(table, monkeypatch):
    Path("MyInput.csv").write_text('id,name\n1,"A, ""quoted"""\n', encoding="utf-8")
    Path("NextInput.csv").write_text("id,name\n2,B\n", encoding="utf-8")
    assert not Path("MYINPUT.CSV").exists()
    imported = []
    original = table.LoadFromFile

    def load(source, name, *args, **kwargs):
        imported.append((Path(source), name))
        return original(source, name, *args, **kwargs)

    monkeypatch.setattr(table, "LoadFromFile", load)
    count = query(
        table, MyTables="NextInput.csv:Second,MyInput.csv:First,NextInput.csv:Second",
        Command1="SELECT a.name AS label, b.name AS other FROM First a CROSS JOIN Second b",
    )
    assert count == 1
    assert imported == [(Path.cwd() / "NextInput.csv", "Second"),
                        (Path.cwd() / "MyInput.csv", "First")]
    assert rows() == [["label", "other"], ['A, "quoted"', "B"]]


@pytest.mark.skipif(os.name != "nt", reason="Requires the real Windows import branch")
def test_windows_imports_keep_archived_uppercase_set_behavior(table, monkeypatch):
    Path("MyInput.csv").write_text("id,name\n1,A\n", encoding="utf-8")
    Path("NextInput.csv").write_text("id,name\n2,B\n", encoding="utf-8")
    pairs = ["NextInput.csv:Second", "MyInput.csv:First", "NEXTINPUT.CSV:SECOND"]
    imported = []
    original = table.LoadFromFile

    def load(source, name, *args, **kwargs):
        imported.append((Path(source).name, name))
        return original(source, name, *args, **kwargs)

    monkeypatch.setattr(table, "LoadFromFile", load)
    assert query(table, MyTables=",".join(pairs),
                 Command1="SELECT name FROM First UNION ALL SELECT name FROM Second") == 2
    assert imported == [tuple(pair.split(":")) for pair in list(set(p.upper() for p in pairs))]
    assert rows() == [["name"], ["A"], ["B"]]


@pytest.mark.skipif(os.name == "nt", reason="Runs the real POSIX CSV preprocessor")
@pytest.mark.parametrize("invalid_sql", [False, True])
def test_preprocessed_temp_file_is_local_and_cleaned_on_success_or_error(
    table, monkeypatch, invalid_sql
):
    Path("MyInput.csv").write_text('id,name\n1,"a,b\nline"\n', encoding="utf-8")
    converted = []
    original = table.myUtils.ConvertDLM

    def convert(source, destination, *args, **kwargs):
        result = original(source, destination, *args, **kwargs)
        path = Path(destination)
        assert path.is_file()
        assert path.resolve().parent == Path.cwd()
        assert "\\" not in str(destination)
        assert path.read_text(encoding="utf-8") == "id,name\n1,a;b line\n"
        converted.append(path)
        return result

    monkeypatch.setattr(table.myUtils, "ConvertDLM", convert)
    if invalid_sql:
        with pytest.raises(Exception, match="syntax"):
            query(table, PreProcCSV=True, Command1="CREATE BOGUS THING")
    else:
        assert query(table, PreProcCSV=True) == 1
        assert rows() == [["id", "name"], ["1", "a;b line"]]
    assert len(converted) == 1
    assert all(not path.exists() for path in converted)
    assert not list(Path.cwd().glob("*.tmp"))
    assert not list(Path.cwd().glob("*.sql"))
    assert not any(path.name.startswith(".\\") for path in Path.cwd().iterdir())


def test_empty_result_keeps_headers_and_append_keeps_rows(table):
    Path("MyInput.csv").write_text("id,name\n1,A\n", encoding="utf-8")
    assert query(table, Command1="SELECT name FROM Input WHERE 0") == 0
    assert rows() == [["name"]]
    assert query(table, Command1="SELECT name FROM Input", myAppend=True) == 1
    assert rows() == [["name"], ["A"]]
    assert query(table, Command1="SELECT name FROM Input", myAppend=True) == 1
    assert rows() == [["name"], ["A"], ["A"]]
