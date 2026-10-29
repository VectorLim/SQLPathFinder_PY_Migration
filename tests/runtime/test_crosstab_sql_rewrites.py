"""Preserve legacy CrossTab SQL projection substitution coverage."""
import pytest

from vg2c.runtime import SqliteReader
from vg2c.runtime.query import run_query
from vg2c.utilities.runtime_adapters import CrosstabUtility


def test_substitute_sql_expands_crosstab_projection():
    sql = "SELECT a0.[facility], CrossTab->[[a0,15507;:Y]] FROM [t] a0"
    out = CrosstabUtility.substitute_sql(
        sql,
        alias_columns_lookup=lambda alias: [
            "facility",
            "SUBPLANEANGLEX",
            "SUBPLANEANGLEY",
        ],
    )
    assert "CrossTab->" not in out
    assert "a0.[SUBPLANEANGLEX] AS [SUBPLANEANGLEX]" in out
    assert "a0.[SUBPLANEANGLEY] AS [SUBPLANEANGLEY]" in out


def test_CrosstabUtility_substitute_sql_header_mode_n():
    sql = "CrossTab->[[a0,15507;:N]]"
    out = CrosstabUtility.substitute_sql(
        sql,
        alias_columns_lookup=lambda alias: ["SUBPLANEANGLEX", "SUBPLANEANGLEY"],
    )
    assert out == "SUBPLANEANGLEX,SUBPLANEANGLEY"


@pytest.mark.parametrize("projection", [
    "{ct}, a0.LOT",
    "a0.LOT, {ct}",
    "a0.LOT,\n {ct}, a0.OPERATION",
    "{ct}, {ct}, a0.LOT",
    "a0.LOT, {ct}, {ct}",
    "a0.LOT, {ct}, {ct}, a0.OPERATION",
])
def test_empty_header_list_removes_one_separator_per_projection(tmp_path, projection):
    CrosstabUtility.write_header_list(tmp_path, "a0,17680", [])
    (tmp_path / "wide.csv").write_text("LOT,OPERATION\n001,2303\n")
    sql = "SELECT " + projection.format(ct="CrossTab->[[a0,17680;:Y]]") + " FROM wide a0"
    result = run_query(sql, reader=SqliteReader(), workdir=tmp_path,
                       output="out.csv", inputs=["wide.csv"])
    expected = ["LOT", "OPERATION"] if "OPERATION" in projection else ["LOT"]
    assert result.columns.tolist() == expected
    assert result.iloc[0].tolist() == ["001", "2303"][:len(expected)]


@pytest.mark.parametrize("projection,expected", [
    ("{ct}, a0.LOT", ["X", "Y", "LOT"]),
    ("a0.LOT, {ct}", ["LOT", "X", "Y"]),
    ("a0.LOT, {ct}, a0.OPERATION", ["LOT", "X", "Y", "OPERATION"]),
])
def test_nonempty_header_list_preserves_projection_separators(tmp_path, projection, expected):
    CrosstabUtility.write_header_list(tmp_path, "a0,17680", ["X", "Y"])
    (tmp_path / "wide.csv").write_text("LOT,OPERATION,X,Y\n001,2303,1,2\n")
    sql = "SELECT " + projection.format(ct="CrossTab->[[a0,17680;:Y]]") + " FROM wide a0"
    result = run_query(sql, reader=SqliteReader(), workdir=tmp_path,
                       output="out.csv", inputs=["wide.csv"])
    assert result.columns.tolist() == expected
    assert len(result) == 1


def test_empty_header_list_still_validates_expression(tmp_path):
    CrosstabUtility.write_header_list(tmp_path, "a0,17680", [])
    with pytest.raises(ValueError, match="must include"):
        CrosstabUtility.substitute_header_lists("CrossTab->[[a0,17680;sum(x):A]]", tmp_path)


@pytest.mark.parametrize("left_rows", ["", "001,2303\n"])
def test_zero_row_pivot_then_left_join_preserves_static_schema(tmp_path, left_rows):
    run_query(
        "SELECT '001' AS lot, '2303' AS operation, 'A' AS parameter, "
        "'1' AS numeric_value WHERE 0",
        reader=SqliteReader(), workdir=tmp_path, output="wide.tab",
        pivot_columns="parameter", pivot_values="numeric_value",
        pivot_header_ref="a0,17680",
    )
    assert (tmp_path / "17680_A0.ini").read_text() == ""
    assert (tmp_path / "wide.tab").read_text() == "LOT\tOPERATION\n"
    (tmp_path / "lots.csv").write_text("lot_1,operation_1\n" + left_rows)
    result = run_query(
        "CREATE INDEX IdxA0 ON wide (LOT, OPERATION); "
        "SELECT a1.lot_1, a0.LOT, CrossTab->[[a0,17680;:Y]], a1.operation_1 "
        "FROM lots a1 LEFT JOIN wide a0 "
        "ON a1.lot_1 = a0.LOT AND a1.operation_1 = a0.OPERATION",
        reader=SqliteReader(), workdir=tmp_path, output="joined.csv",
        inputs=["lots.csv", "wide.tab"],
    )
    assert len(result) == bool(left_rows)
    assert (tmp_path / "joined.csv").read_text() == (
        "lot_1,LOT,operation_1\n" + ("001,,2303\n" if left_rows else "")
    )


