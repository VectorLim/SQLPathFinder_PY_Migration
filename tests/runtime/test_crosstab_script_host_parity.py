"""Source-derived normal-query pivot regression fixtures.

Expected values come from SPFSQL3.py normal QType dispatch and
SPFUtilities/utils.py PivotTable/pivotDF, not an executed proprietary oracle.
"""
from __future__ import annotations

import json

import pandas as pd
import pytest

from vg2c import compile_document, translate
from vg2c.runtime import JobRuntime, SqliteReader, execute_sql
from vg2c.runtime.crosstab import _CrosstabUtility


def _script(tmp_path, option_value="CTVALUE", ctrow="unused"):
    path = tmp_path / "source.txt"
    opts = ["/ENGINE=SQLite", "/CSV=out.csv", "/CTHEADER=metric",
            f"/{option_value}=reading"]
    if ctrow is not None:
        opts.append(f"/CTROW={ctrow}")
    query = "SELECT '001' AS lot, 'one' AS extra, 'A' AS metric, 'x' AS reading"
    path.write_text("<OPTIONS>\n" + "\n".join(opts) + "\n</OPTIONS>\n" + query)
    return path


@pytest.mark.parametrize("value_alias", ["CTVAL", "CTVALUE"])
def test_ctvalue_alias_and_ctrow_is_classification_only(tmp_path, value_alias):
    source = _script(tmp_path, value_alias)
    emitted = compile_document(source).emitted
    assert "pivot_columns='metric'" in emitted.source.lower()
    assert "pivot_values='reading'" in emitted.source.lower()
    assert "pivot_rows=" not in emitted.source
    assert not [name for name, _ in emitted.assets if name.endswith(".crosstab.json")]
    main = translate(source)
    ns = {"__file__": str(main), "__name__": "translated_job"}
    exec(compile(main.read_text(), str(main), "exec"), ns)
    ns["run"](workdir=tmp_path / "work")
    assert (tmp_path / "work" / "out.csv").read_text() == "LOT,EXTRA,A\n001,one,x\n"


def test_missing_ctrow_does_not_emit_pivot(tmp_path):
    source = _script(tmp_path, ctrow=None)
    assert "pivot_columns=" not in compile_document(source).emitted.source


def test_runtime_row_fields_come_from_query_output(tmp_path):
    sql = tmp_path / "pivot.sql"
    sql.write_text(
        "SELECT '001' AS lot, 'north' AS extra, 'A' AS metric, '1' AS reading "
        "UNION ALL SELECT '001', 'north', 'B', '2' "
        "UNION ALL SELECT '002', 'south', 'B', '3'"
    )
    execute_sql(sql, reader=SqliteReader(), workdir=tmp_path, output="out.csv",
                pivot_columns="metric", pivot_values="reading")
    assert (tmp_path / "out.csv").read_text() == (
        "LOT,EXTRA,A,B\n001,north,1,2\n002,south,,3\n"
    )


def test_pivot_first_last_are_physical_rows_not_nonnull_first():
    df = pd.DataFrame({
        "lot": ["001", "001", "002"],
        "metric": ["A", "A", "B"],
        "reading": [None, "later", "z"],
    })
    u = _CrosstabUtility()
    first = u.apply(df, pivot_columns="metric", pivot_values="reading")
    last = u.apply(df, pivot_columns="metric", pivot_values="reading",
                   duplicate="last")
    assert first.loc[first["LOT"] == "001", "A"].iloc[0] == ""
    assert last.loc[last["LOT"] == "001", "A"].iloc[0] == "later"


def test_multiple_value_headers_and_pivotdot():
    df = pd.DataFrame({
        "lot": ["001", "001"], "metric": ["B", "A"],
        "reading": ["2", "1"], "limit": ["20", "10"],
    })
    u = _CrosstabUtility()
    result = u.apply(df, pivot_columns="metric", pivot_values=["reading", "limit"])
    assert result.columns.tolist() == [
        "LOT", "LIMIT@A", "LIMIT@B", "READING@A", "READING@B"
    ]
    assert result.iloc[0].tolist() == ["001", "10", "20", "1", "2"]
    dotted = u.apply(df, pivot_columns="metric", pivot_values=["reading", "limit"],
                     dot=True)
    assert dotted.columns.tolist() == [
        "lot", "limit.a", "limit.b", "reading.a", "reading.b"
    ]


def test_pivot_preserves_literals_and_missing_value(tmp_path):
    source = pd.DataFrame({
        "id": ["001", "001"], "metric": ["A", "B"], "value": ["nan", ""],
    })
    frame = _CrosstabUtility().apply(source, pivot_columns="metric",
                                    pivot_values="value", missing="N/A")
    assert frame.loc[0, "ID"] == "001"
    assert frame.loc[0, "A"] == "nan"
    assert frame.loc[0, "B"] == ""


def test_ctarray_then_downstream_sql_expands_previous_headers(tmp_path):
    a = tmp_path / "a.sql"
    b = tmp_path / "b.sql"
    a.write_text(
        "SELECT '001' AS lot, 'x' AS metric, '3' AS value "
        "UNION ALL SELECT '001', 'y', '4'"
    )
    execute_sql(a, reader=SqliteReader(), workdir=tmp_path, output="wide.csv",
                pivot_columns="metric", pivot_values="value",
                pivot_header_ref="a0,4253")
    assert (tmp_path / "4253_A0.ini").read_text() == "X\tY"
    b.write_text(
        "SELECT a.lot, CrossTab->[[a0,4253;:Y]] "
        "FROM wide a0 JOIN wide a ON a0.lot = a.lot"
    )
    execute_sql(b, reader=SqliteReader(), workdir=tmp_path, output="selected.csv",
                inputs=["wide.csv"])
    assert (tmp_path / "selected.csv").read_text().splitlines()[0] == "LOT,X,Y"


def test_ctarray_expression_mode_a_and_missing_file(tmp_path):
    sql = tmp_path / "exp.sql"
    sql.write_text("SELECT CrossTab->[[a0,42;sum(|<>|):A]] FROM wide a0")
    (tmp_path / "wide.csv").write_text("X\n1\n")
    with pytest.raises((ValueError, FileNotFoundError), match="42|header|ini"):
        execute_sql(sql, reader=SqliteReader(), workdir=tmp_path,
                    output="out.csv", inputs=["wide.csv"])
    (tmp_path / "42_A0.ini").write_text("X")
    execute_sql(sql, reader=SqliteReader(), workdir=tmp_path,
                output="out.csv", inputs=["wide.csv"])
    assert (tmp_path / "out.csv").read_text().splitlines() == ["sum([X])", "1"]


def test_legacy_json_configuration_is_reloaded(tmp_path):
    root = tmp_path / "assets"
    root.mkdir()
    (root / "query.sql").write_text(
        "SELECT '001' AS lot, 'A' AS metric, 'first' AS reading"
    )
    opt = root / "legacy.crosstab.json"
    opt.write_text(json.dumps({
        "row_keys": ["lot"], "header_key": "metric", "value_key": "reading",
    }))
    job = JobRuntime(root, tmp_path / "run")
    job.sql("query.sql", reader=SqliteReader(), output="out.csv",
            crosstab=job.table_spec("legacy.crosstab.json"))
    assert (tmp_path / "run" / "out.csv").read_text() == "lot,a\n001,first\n"
    opt.write_text(json.dumps({"row_keys": ["missing"], "header_key": "metric",
                               "value_key": "reading"}))
    with pytest.raises(ValueError, match="missing"):
        job.sql("query.sql", reader=SqliteReader(), output="out.csv",
                crosstab=job.table_spec("legacy.crosstab.json"))


def test_legacy_json_shape_and_mixed_api_are_rejected(tmp_path):
    root = tmp_path / "assets"
    root.mkdir()
    (root / "query.sql").write_text("SELECT 1")
    (root / "bad.json").write_text('{"row_keys": ["x"], "value_key": 3}')
    job = JobRuntime(root, tmp_path / "work")
    with pytest.raises(ValueError, match="header_key|crosstab"):
        job.sql("query.sql", reader=SqliteReader(), output="x.csv",
                crosstab=job.table_spec("bad.json"))
    with pytest.raises(ValueError, match="both|mixed|crosstab"):
        job.sql("query.sql", reader=SqliteReader(), output="x.csv",
                pivot_columns="x", pivot_values="y",
                crosstab={"row_keys": ["k"], "header_key": "x", "value_key": "y"})


def test_many_runtime_keys_no_crosstab_json(tmp_path):
    keys = [f"k{i}" for i in range(28)]
    sql = "SELECT " + ", ".join(f"'{i}' AS {k}" for i, k in enumerate(keys))
    sql += ", 'A' AS metric, 'v' AS reading"
    path = tmp_path / "many.txt"
    path.write_text("<OPTIONS>\n/ENGINE=SQLite\n/CSV=wide.csv\n"
                    "/CTROW=unused\n/CTHEADER=metric\n/CTVAL=reading\n"
                    "</OPTIONS>\n" + sql)
    emitted = compile_document(path).emitted
    assert "pivot_columns=" in emitted.source
    assert not [name for name, _ in emitted.assets if name.endswith(".crosstab.json")]
    main = translate(path)
    ns = {"__file__": str(main), "__name__": "translated_job"}
    exec(compile(main.read_text(), str(main), "exec"), ns)
    ns["run"](workdir=tmp_path / "work")
    assert (tmp_path / "work" / "wide.csv").read_text().splitlines()[0] == (
        ",".join(k.upper() for k in keys) + ",A"
    )


def test_pivot_editing_binding_has_correct_range(tmp_path):
    from vg2c.editing import SemanticChange, project_changes

    source = _script(tmp_path)
    compiled = compile_document(source)
    invocation = next(
        item for step in compiled.emitted.steps for item in step.invocations
        if item.operation.id == "ctx.run_query"
    )
    bound = {p.name: p for p in invocation.parameters}
    for name in ("pivot_columns", "pivot_values"):
        p = bound[name]
        assert p.editable and p.source_range is not None
        span = p.source_range
        assert compiled.emitted.source[span.start_offset:span.end_offset] == p.source
    edited = project_changes(
        compiled, [SemanticChange(bound["pivot_columns"].id, "category")]
    )
    assert edited.valid
    assert "pivot_columns='category'" in edited.source
    assert "pivot_values='reading'" in edited.source
