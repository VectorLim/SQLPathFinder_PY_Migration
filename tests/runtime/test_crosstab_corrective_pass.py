"""ScriptHost-grounded crosstab regression and old-JSON adapter tests.

SPFUtilities/utils.py:4070-4088, 4118-4122, 4137-4218, 4330-4361,
4408-4427. The old vg2c behavior is not a semantic authority.
Normalization of mixed-case pivot *identities* remains an intentional repair
of an observable source bug (not a tested proprietary-engine oracle).
"""
from __future__ import annotations

import json

import pandas as pd
import pytest

from vg2c.runtime import JobRuntime, SqliteReader
from vg2c.runtime.crosstab import _CrosstabUtility as CrosstabUtility


def legacy(df, row_keys=None):
    return CrosstabUtility().apply(
        df, row_keys=["lot"] if row_keys is None else row_keys,
        header_key="metric", value_key="reading",
    )


def normal(df, **options):
    return CrosstabUtility().apply(
        df, pivot_columns="metric", pivot_values="reading", **options,
    )


@pytest.mark.parametrize("raw", [("a", "A"), ("Voltage", "VOLTAGE"), ("vOlTaGe", "VOLTAGE")])
@pytest.mark.parametrize("keep,expected", [("first", "first"), ("last", "second")])
def test_normal_casefolds_pivot_identity_before_physical_duplicate_selection(raw, keep, expected):
    lower, upper = raw
    df = pd.DataFrame({
        "lot": ["001", "001"], "metric": [lower, upper],
        "reading": ["first", "second"],
    })
    result = normal(df, duplicate=keep)
    assert result.columns.tolist() == ["LOT", upper.upper()]
    assert result.iloc[0].tolist() == ["001", expected]


@pytest.mark.parametrize("dot,expected_cols", [
    (False, ["LOT", "LIMIT@A", "READING@A"]),
    (True, ["lot", "limit.a", "reading.a"]),
])
@pytest.mark.parametrize("keep,expected", [
    ("first", ["001", "10", "one"]),
    ("last", ["001", "20", "two"]),
])
def test_normal_mixed_case_multi_values(dot, expected_cols, keep, expected):
    df = pd.DataFrame({
        "lot": ["001", "001"], "metric": ["a", "A"],
        "reading": ["one", "two"], "limit": ["10", "20"],
    })
    result = CrosstabUtility().apply(
        df, pivot_columns="metric", pivot_values=["reading", "limit"],
        duplicate=keep, dot=dot,
    )
    assert result.columns.tolist() == expected_cols
    assert result.iloc[0].tolist() == expected


def test_normal_collision_with_row_identity_never_silently_discarded():
    df = pd.DataFrame({
        "Lot": ["001"], "Metric": ["lot"], "Reading": ["ambiguous"],
    })
    with pytest.raises(ValueError, match="collide"):
        normal(df)


def test_normal_legacy_sanitizer_and_special_pivot_values():
    df = pd.DataFrame({
        "lot": ["001"] * 3,
        "metric": ["with space", "under_score", "UNICØDE!"],
        "reading": ["s", "u", "i"],
    })
    result = normal(df, legacy_headers=True)
    assert result.columns.tolist() == [
        "LOT", "UNDER_SCORE", "UNICØDE#", "WITH_SPACE",
    ]
    assert result.iloc[0].tolist() == ["001", "u", "i", "s"]


def test_normal_casefold_does_not_change_noncolliding_uppercase_headers():
    df = pd.DataFrame({
        "lot": ["001", "001"], "metric": ["A", "B"],
        "reading": ["one", "two"],
    })
    assert normal(df).columns.tolist() == ["LOT", "A", "B"]
    assert normal(df).iloc[0].tolist() == ["001", "one", "two"]



@pytest.mark.parametrize("values", [
    [None, "later"], ["early", None], [None, None],
    [float("nan"), "later"], ["", "later"],
])
def test_deprecated_json_shape_uses_script_host_physical_first(values):
    """Old vg2c groupby.first() was first-non-null; ScriptHost drops physical duplicates."""
    df = pd.DataFrame({
        "lot": ["001", "001"], "metric": ["A", "A"], "reading": values,
    })
    current = normal(df)
    adapter = legacy(df)
    assert adapter.equals(current)
    assert adapter.columns.tolist() == ["LOT", "A"]
    assert adapter.iloc[0]["A"] == ("" if pd.isna(values[0]) else values[0])


@pytest.mark.parametrize("headers", [
    [None, "", "A"], ["", "", None], [float("nan"), "A", "A"],
])
def test_old_json_shape_keeps_script_host_blank_pivot_identity(headers):
    """na_filter=False produces an empty string and _UNKNOWN_ in ScriptHost."""
    df = pd.DataFrame({
        "lot": ["001", "001", "001"], "metric": headers,
        "reading": ["null", "empty", "good"],
    })
    adapter = legacy(df)
    assert adapter.equals(normal(df))
    assert "_UNKNOWN_" in adapter.columns
    expected = ["LOT", "A", "_UNKNOWN_"] if "A" in adapter.columns else ["LOT", "_UNKNOWN_"]
    assert adapter.columns.tolist() == expected


def test_old_json_empty_frames_and_row_key_mismatch_are_explicit():
    empty = pd.DataFrame(columns=["LOT", "Metric", "Reading"])
    assert legacy(empty, ["LOT"]).columns.tolist() == ["LOT"]
    sample = pd.DataFrame({"lot": ["001"], "metric": ["A"], "reading": ["x"]})
    with pytest.raises(ValueError, match="row_keys do not match|Update the JSON"):
        legacy(sample, [])
    with pytest.raises(ValueError, match="row_keys do not match|Update the JSON"):
        CrosstabUtility().apply(
            pd.DataFrame({"lot": ["001"], "extra": ["x"],
                          "metric": ["A"], "reading": ["v"]}),
            row_keys=["lot"], header_key="metric", value_key="reading",
        )


def test_deprecated_adapter_casefolded_fields_and_script_host_order():
    df = pd.DataFrame({
        "Lot": ["002", "001", "001", "002"],
        "METRIC": ["z", "B", "A", "A"],
        "Reading": ["zval", "bval", "aval", "aval2"],
    })
    result = legacy(df)
    assert result.equals(normal(df))
    assert result.columns.tolist() == ["LOT", "A", "B", "Z"]
    assert result["LOT"].tolist() == ["001", "002"]
    assert result.iloc[0].tolist() == ["001", "aval", "bval", ""]


def test_old_json_is_reread_after_edit_but_is_not_a_second_pivot_engine(tmp_path):
    root = tmp_path / "assets"
    root.mkdir()
    (root / "query.sql").write_text(
        "SELECT '001' AS lot, 'A' AS metric, NULL AS reading "
        "UNION ALL SELECT '001', 'A', 'later'"
    )
    cfg = root / "sql" / "query.crosstab.json"
    cfg.parent.mkdir()
    cfg.write_text(json.dumps({
        "row_keys": ["lot"], "header_key": "metric", "value_key": "reading",
    }))
    job = JobRuntime(root, tmp_path / "run")
    job.sql("query.sql", reader=SqliteReader(), output="out.csv",
            crosstab=job.table_spec("sql/query.crosstab.json"))
    assert (tmp_path / "run" / "out.csv").read_text() == "LOT,A\n001,\n"
    # JSON is still an editable on-disk input; case spelling doesn't change meaning.
    cfg.write_text(json.dumps({
        "row_keys": ["LOT"], "header_key": "METRIC", "value_key": "READING",
    }, indent=2))
    job.sql("query.sql", reader=SqliteReader(), output="out.csv",
            crosstab=job.table_spec("sql/query.crosstab.json"))
    assert (tmp_path / "run" / "out.csv").read_text() == "LOT,A\n001,\n"
    cfg.write_text(json.dumps({
        "row_keys": [], "header_key": "metric", "value_key": "reading",
    }))
    with pytest.raises(ValueError, match="row_keys do not match"):
        job.sql("query.sql", reader=SqliteReader(), output="out.csv",
                crosstab=job.table_spec("sql/query.crosstab.json"))
    with pytest.raises(ValueError, match="both|mixed|legacy"):
        job.sql("query.sql", reader=SqliteReader(), output="out.csv",
                crosstab=job.table_spec("sql/query.crosstab.json"),
                pivot_columns="metric", pivot_values="reading")


@pytest.mark.parametrize("keep", ["first", "last"])
def test_chunk_boundary_preserves_original_chunk_priority(keep):
    """Source: per-chunk FIRST/LAST then combine_first() retaining earlier chunk.

    This reconstructs the original pandas behavior, not an engine differential.
    ScriptHost-entry's default chunk size is 50k; standalone default is 1m.
    """
    chunk_size = CrosstabUtility._CHUNK_ROWS
    df = pd.DataFrame({
        "lot": ["001", *[f"{i:06d}" for i in range(1, chunk_size)], "001"],
        "metric": ["A"] * (chunk_size + 1),
        "reading": ["first", *(["filler"] * (chunk_size - 1)), "last"],
    })
    source_chunks = []
    for piece in (df.iloc[:chunk_size], df.iloc[chunk_size:]):
        indexed = piece.set_index(["lot", "metric"])[["reading"]]
        indexed = indexed[~indexed.index.duplicated(keep=keep)]
        source_chunks.append(indexed.unstack("metric"))
    source_result = source_chunks[0].combine_first(source_chunks[1])
    assert source_result.loc["001", ("reading", "A")] == "first"
    pivoted = normal(df, duplicate=keep)
    assert pivoted.loc[pivoted["LOT"] == "001", "A"].iloc[0] == "first"


def test_chunk_boundary_fills_earlier_missing_pivot_cells():
    size = CrosstabUtility._CHUNK_ROWS
    source = pd.DataFrame({
        "lot": ["001", *[f"{i:06d}" for i in range(1, size)], "001"],
        "metric": ["A", *(["A"] * (size-1)), "B"],
        "reading": ["early", *(["filler"] * (size-1)), "later"],
    })
    pivoted = normal(source, duplicate="last")
    assert pivoted.columns.tolist() == ["LOT", "A", "B"]
    assert pivoted.loc[pivoted["LOT"] == "001", ["A", "B"]].iloc[0].tolist() == ["early", "later"]


def test_source_empty_header_becomes_unknown_and_downstream_ctarray(tmp_path):
    sql = tmp_path / "source.sql"
    sql.write_text(
        "SELECT '001' AS lot, '' AS metric, 'empty' AS reading "
        "UNION ALL SELECT '001', 'a', 'first' "
        "UNION ALL SELECT '001', 'A', 'second'"
    )
    from vg2c.runtime import execute_sql
    execute_sql(sql, reader=SqliteReader(), workdir=tmp_path,
                output="pivot.csv", pivot_columns="metric", pivot_values="reading",
                pivot_header_ref="a0,4253")
    assert (tmp_path / "4253_A0.ini").read_text() == "A\t_UNKNOWN_"
    assert (tmp_path / "pivot.csv").read_text() == "LOT,A,_UNKNOWN_\n001,first,empty\n"
    follow = tmp_path / "follow.sql"
    follow.write_text("SELECT a.LOT, CrossTab->[[a0,4253;:Y]] FROM pivot a0 JOIN pivot a ON a0.LOT = a.LOT")
    execute_sql(follow, reader=SqliteReader(), workdir=tmp_path,
                output="downstream.csv", inputs=["pivot.csv"])
    assert (tmp_path / "downstream.csv").read_text().splitlines()[0] == "LOT,A,_UNKNOWN_"

def test_old_json_unknown_fields_are_rejected_not_ignored(tmp_path):
    root = tmp_path / "assets"
    root.mkdir()
    (root / "query.sql").write_text(
        "SELECT '001' AS lot, 'A' AS metric, 'value' AS reading"
    )
    path = root / "pivot.crosstab.json"
    path.write_text(json.dumps({
        "row_keys": ["lot"], "header_key": "metric", "value_key": "reading",
        "pivot_function": "last",
    }))
    job = JobRuntime(root, tmp_path / "work")
    with pytest.raises(ValueError, match="Unsupported legacy crosstab configuration fields"):
        job.sql("query.sql", reader=SqliteReader(), output="out.csv",
                crosstab=job.table_spec("pivot.crosstab.json"))
