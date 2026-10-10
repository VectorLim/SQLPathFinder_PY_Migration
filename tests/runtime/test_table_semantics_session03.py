"""Session 03: file-backed SQL/table semantics and generated editability."""

from __future__ import annotations

import json

import pandas as pd
import pytest

from vg2c import compile_document, translate
from vg2c.runtime import JobRuntime, SqliteReader, execute_sql
from vg2c.runtime.csv_io import _CsvIO as CsvIO
from vg2c.runtime.crosstab import _CrosstabUtility as CrosstabUtility


def test_sql_duplicate_projection_preserves_positional_values(tmp_path):
    frame = SqliteReader().execute("SELECT '001' AS code, '002' AS code", [])
    assert frame.columns.tolist() == ["code", "code"]
    assert frame.iloc[0].tolist() == ["001", "002"]
    CsvIO().write(str(tmp_path / "result.csv"), frame)
    assert (tmp_path / "result.csv").read_text() == "code,code\n001,002\n"


@pytest.mark.parametrize("columns", ["ID,id", "ID,ID", "ID,"])
def test_sqlite_input_rejects_ambiguous_schema(tmp_path, columns):
    path = tmp_path / "data.csv"
    path.write_text(columns + "\nA,B\n")
    with pytest.raises(ValueError, match="empty or duplicate column"):
        SqliteReader().execute("SELECT * FROM data", [str(path)])


def test_dataframe_projection_diagnoses_case_collisions(tmp_path):
    out = str(tmp_path / "out.csv")
    with pytest.raises(ValueError, match="Ambiguous"):
        CsvIO().write(out, pd.DataFrame({"ID": ["A"], "id": ["B"]}), header=["id"])
    with pytest.raises(ValueError, match="collide"):
        CsvIO().write(out, pd.DataFrame({"ID": ["A"]}), header=["ID", "id"])


def test_query_output_renamed_case_can_be_read_downstream(tmp_path):
    data = tmp_path / "source.csv"
    data.write_text('id,label\n001,"a,b"\n002,x\n')
    initial = tmp_path / "initial.sql"
    initial.write_text("SELECT id AS code, label FROM T0")
    execute_sql(initial, reader=SqliteReader(), inputs=[("source.csv", "T0")],
                output="renamed.csv", header=["CODE", "LABEL"], workdir=tmp_path)
    assert (tmp_path / "renamed.csv").read_text() == 'CODE,LABEL\n001,"a,b"\n002,x\n'
    downstream = tmp_path / "downstream.sql"
    downstream.write_text("SELECT CODE, LABEL FROM renamed WHERE CODE = :id")
    execute_sql(downstream, reader=SqliteReader(), inputs=["renamed.csv"],
                output="final.csv", workdir=tmp_path, params={"id": "001"})
    assert (tmp_path / "final.csv").read_text() == 'CODE,LABEL\n001,"a,b"\n'


def test_legacy_unmatched_header_remains_blank_by_name(tmp_path):
    CsvIO().write(str(tmp_path / "out.csv"), pd.DataFrame({"a": ["one"]}),
                  header=["a", "unmatched"])
    assert (tmp_path / "out.csv").read_text() == "a,unmatched\none,\n"


def test_crosstab_first_duplicate_and_string_identifier():
    rows = pd.DataFrame({
        "Lot": ["001", "001", "002"], "Metric": ["A", "A", "B"],
        "Value": ["first", "second", "third"],
    })
    result = CrosstabUtility().apply(rows, row_keys=["lot"], header_key="metric",
                                    value_key="value")
    assert result["lot"].tolist() == ["001", "002"]
    assert result.loc[0, "a"] == "first"
    assert result.loc[1, "b"] == "third"


def test_crosstab_diagnoses_ambiguous_schema():
    source = pd.DataFrame({"ID": ["1"], "id": ["2"], "metric": ["A"], "value": ["3"]})
    with pytest.raises(ValueError, match="Ambiguous"):
        CrosstabUtility().apply(source, row_keys=["id"], header_key="metric",
                                value_key="value")


def test_large_table_options_are_editable_assets(tmp_path):
    names = [f"k{i}" for i in range(8)]
    source = tmp_path / "pivot.txt"
    opts = "/CTROW=" + ",".join(names) + "\n/CTHEADER=metric\n/CTVALUE=value"
    query = "SELECT " + ", ".join(f"'{i}' AS {name}" for i, name in enumerate(names))
    query += ", 'A' AS metric, 'first' AS value"
    source.write_text("<OPTIONS>\n/ENGINE=SQLite\n/CSV=result.csv\n" + opts
                      + "\n</OPTIONS>\n" + query + "\n")
    compiled = compile_document(source)
    assert "crosstab=job.table_spec(" in compiled.emitted.source
    paths = dict(compiled.emitted.assets)
    spec_name = next(name for name in paths if name.endswith(".crosstab.json"))
    assert json.loads(paths[spec_name])["row_keys"] == names
    main = translate(source)
    namespace = {"__file__": str(main), "__name__": "translated_job"}
    exec(compile(main.read_text(), str(main), "exec"), namespace)
    namespace["run"](workdir=tmp_path / "work")
    assert (tmp_path / "work" / "result.csv").read_text().splitlines()[0] == ",".join(names) + ",a"
    spec_path = main.parent / spec_name
    edited = json.loads(spec_path.read_text())
    edited["row_keys"] = list(reversed(names))
    spec_path.write_text(json.dumps(edited))
    namespace["run"](workdir=tmp_path / "work")
    assert (tmp_path / "work" / "result.csv").read_text().splitlines()[0] == ",".join(reversed(names)) + ",a"


def test_large_explicit_header_spec_remains_by_name(tmp_path):
    names = [f"c{i}" for i in range(8)]
    source = tmp_path / "headers.txt"
    source.write_text("<OPTIONS>\n/ENGINE=SQLite\n/CSV=result.csv\n/HEADERS="
                      + ",".join(names) + "\n</OPTIONS>\nSELECT "
                      + ", ".join(f"'{i}' AS {name}" for i, name in enumerate(names)))
    generated = compile_document(source).emitted
    assert "header=job.table_spec(" in generated.source
    path, text = next((name, value) for name, value in generated.assets if name.endswith(".header.json"))
    assert json.loads(text) == names


def test_multi_alias_join_and_dynamic_sql_projection(tmp_path):
    (tmp_path / "left.csv").write_text("lot,code\n001,ABC\n002,DEF\n")
    (tmp_path / "right.csv").write_text("lot,quantity\n001,5\n")
    sql = ("SELECT a.lot, CrossTab->[[a,ignored;:Y]], b.quantity "
           "FROM [T0] a LEFT JOIN [T1] b ON a.lot = b.lot")
    frame = SqliteReader().execute(
        sql, [("left.csv", "T0"), ("right.csv", "T1")], workdir=tmp_path
    )
    assert frame.columns.tolist() == ["lot", "code", "quantity"]
    assert frame.iloc[0].tolist() == ["001", "ABC", "5"]
    assert frame.iloc[1]["lot"] == "002"


def test_empty_crosstab_keeps_ordered_row_schema():
    frame = pd.DataFrame(columns=["ID", "Category", "Measure"])
    result = CrosstabUtility().apply(frame, row_keys=["ID"],
                                    header_key="Category", value_key="Measure")
    assert result.columns.tolist() == ["ID"]
    assert result.empty


def test_query_sql_get_csv_list_quotes_and_deduplicates(tmp_path):
    (tmp_path / "items.csv").write_text("lot\n001\nO'Neil\n001\n")
    sql = tmp_path / "query.sql"
    sql.write_text("SELECT '001' AS lot WHERE '001' IN "
                   "SQL_Get_CSV_List('items.csv', 1, 'lot IN')")
    execute_sql(sql, reader=SqliteReader(), output="matched.csv",
                workdir=tmp_path)
    assert (tmp_path / "matched.csv").read_text() == "lot\n001\n"


def test_quoted_sqlite_identifiers_are_not_truncated(tmp_path):
    path = tmp_path / "strange.csv"
    path.write_text('weird"name,lot\nvalue,001\n')
    result = SqliteReader().execute(
        'SELECT "weird""name", lot FROM strange', [str(path)]
    )
    assert result.columns.tolist() == ['weird"name', "lot"]
    assert result.iloc[0].tolist() == ["value", "001"]
