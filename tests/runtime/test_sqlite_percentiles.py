import pytest

from vg2c.runtime import SqliteReader


def test_sqlite_percentiles_are_continuous_grouped_aggregates():
    frame = SqliteReader().execute(
        """
        SELECT group_id, P25(value) AS p25, P75(value) AS p75
        FROM (
            SELECT 'four' AS group_id, 4 AS value UNION ALL
            SELECT 'four', 1 UNION ALL
            SELECT 'four', 3 UNION ALL
            SELECT 'four', 2 UNION ALL
            SELECT 'four', NULL UNION ALL
            SELECT 'two', CAST('10' AS TEXT) UNION ALL
            SELECT 'two', CAST('2' AS TEXT) UNION ALL
            SELECT 'one', 7 UNION ALL
            SELECT 'same', 5 UNION ALL
            SELECT 'same', 5
        )
        GROUP BY group_id
        ORDER BY group_id
        """,
        [],
    )

    assert frame.values.tolist() == [
        ["four", 1.75, 3.25],
        ["one", 7.0, 7.0],
        ["same", 5.0, 5.0],
        ["two", 4.0, 8.0],
    ]


def test_sqlite_percentiles_work_in_setup_statements_and_return_null_without_values():
    frame = SqliteReader().execute(
        """
        CREATE TABLE summary AS
        SELECT 'empty' AS group_id, p25(value) AS p25, p75(value) AS p75
        FROM (SELECT CAST(NULL AS REAL) AS value WHERE 0)
        UNION ALL
        SELECT 'nulls', p25(value), p75(value)
        FROM (SELECT NULL AS value UNION ALL SELECT NULL);
        SELECT p25, p75 FROM summary ORDER BY group_id
        """,
        [],
    )

    assert frame.shape == (2, 2)
    assert frame.isna().all().all()


def test_sqlite_percentiles_reject_non_numeric_values():
    with pytest.raises(RuntimeError, match="aggregate"):
        SqliteReader().execute("SELECT P25('not numeric')", [])
