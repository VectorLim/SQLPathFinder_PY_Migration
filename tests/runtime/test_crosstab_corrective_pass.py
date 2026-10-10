"""Corrective-pass regression tests.

Legacy expectations are from crosstab.py at 3e9def7a1b066e5007c9284510b242cbf611c7e2.
Normal-query cases trace SPFUtilities/utils.py:4137-4218, 4304-4360;
normalizing mixed-case *identities* is an intentional repair of an original
conditional-normalization defect, not proven byte-identical ScriptHost output.
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


@pytest.mark.parametrize("values,expected_legacy,expected_normal", [
    ([None, "later"], "later", ""),
    (["early", None], "early", "early"),
    ([None, None], None, ""),
    ([float("nan"), "later"], "later", ""),
])
def test_legacy_first_nonnull_vs_normal_physical_first(values, expected_legacy, expected_normal):
    df = pd.DataFrame({
        "lot": ["001", "001"], "metric": ["A", "A"], "reading": values,
    })
    old = legacy(df)
    current = normal(df)
    assert old.columns.tolist() == ["lot", "a"]
    value = old.iloc[0]["a"]
    if expected_legacy is None:
        assert pd.isna(value)
    else:
        assert value == expected_legacy
    assert current.iloc[0]["A"] == expected_normal


@pytest.mark.parametrize("headers", [
    [None, "", "A"], ["", "", None], [float("nan"), "A", "A"],
])
def test_legacy_filters_null_and_empty_pivot_headers(headers):
    df = pd.DataFrame({
        "lot": ["001", "001", "001"], "metric": headers,
        "reading": ["bad_null", "bad_empty", "good"],
    })
    result = legacy(df)
    remaining = [x for x in headers if pd.notna(x) and str(x) != ""]
    assert result.columns.tolist() == (["lot", *sorted({str(x).lower() for x in remaining})]
                                      if remaining else ["lot"])
    assert "nan" not in result.columns
    assert "" not in result.columns


def test_legacy_empty_rows_and_empty_keys_preserve_pre_revision_behavior():
    empty = pd.DataFrame(columns=["LOT", "Metric", "Reading"])
    assert legacy(empty, ["LOT"]).columns.tolist() == ["LOT"]
    assert legacy(pd.DataFrame({"lot": ["001"], "metric": ["A"],
                                "reading": ["x"]}), []).columns.tolist() == []
    no_rows = pd.DataFrame({
        "lot": ["001", "001"], "metric": [None, ""], "reading": ["n", "e"],
    })
    assert legacy(no_rows, ["lot"]).columns.tolist() == ["lot"]


def test_legacy_case_insensitive_fields_group_ids_and_header_order():
    df = pd.DataFrame({
        "Lot": ["002", "001", "001", "002"],
        "METRIC": ["z", "B", "A", "A"],
        "Reading": ["zval", "bval", "aval", "aval2"],
    })
    result = legacy(df)
    assert result.columns.tolist() == ["lot", "a", "b", "z"]
    assert result["lot"].tolist() == ["001", "002"]
    assert result.iloc[0].tolist() == ["001", "aval", "bval", ""]


def test_legacy_json_read_after_edit_keeps_nonnull_contract(tmp_path):
    root = tmp_path / "assets"
    root.mkdir()
    sql = root / "query.sql"
    sql.write_text(
        "SELECT '001' AS lot, 'A' AS metric, NULL AS reading "
        "UNION ALL SELECT '001', 'A', 'later'"
    )
    path = root / "sql" / "query.crosstab.json"
    path.parent.mkdir()
    path.write_text(json.dumps({
        "row_keys": ["lot"], "header_key": "metric", "value_key": "reading",
    }))
    job = JobRuntime(root, tmp_path / "work")
    job.sql("query.sql", reader=SqliteReader(), output="out.csv",
            crosstab=job.table_spec("sql/query.crosstab.json"))
    assert (tmp_path / "work" / "out.csv").read_text() == "lot,a\n001,later\n"
    path.write_text(json.dumps({
        "row_keys": ["lot"], "header_key": "metric", "value_key": "reading",
    }, indent=2))
    job.sql("query.sql", reader=SqliteReader(), output="out.csv",
            crosstab=job.table_spec("sql/query.crosstab.json"))
    assert (tmp_path / "work" / "out.csv").read_text() == "lot,a\n001,later\n"
    with pytest.raises(ValueError, match="both|mixed|legacy"):
        job.sql("query.sql", reader=SqliteReader(), output="out.csv",
                crosstab=job.table_spec("sql/query.crosstab.json"),
                pivot_columns="metric", pivot_values="reading")


def test_normal_chunk_boundary_reference_discrepancy_is_explicit():
    """Source-derived 50k boundary: combine_first can conflict with global LAST.

    This constructs the cross-chunk reconciliation using the same pandas
    primitives visible in original utils.py:4280-4512, NOT an executed engine.
    """
    chunk_size = 50000
    df = pd.DataFrame({
        "lot": ["001", *[f"{i:06d}" for i in range(1, chunk_size)], "001"],
        "metric": ["A"] * (chunk_size + 1),
        "reading": ["first", *(["filler"] * (chunk_size - 1)), "last"],
    })
    original_style = []
    for piece in (df.iloc[:chunk_size], df.iloc[chunk_size:]):
        indexed = piece.set_index(["lot", "metric"])[["reading"]]
        indexed = indexed[~indexed.index.duplicated(keep="last")]
        original_style.append(indexed.unstack("metric"))
    combined = original_style[0].combine_first(original_style[1])
    expected_from_original_primitives = combined.loc["001", ("reading", "A")]
    assert expected_from_original_primitives == "first"
    current_last = normal(df, duplicate="last")
    assert current_last.loc[current_last["LOT"] == "001", "A"].iloc[0] == "last"
    assert expected_from_original_primitives != "last"
