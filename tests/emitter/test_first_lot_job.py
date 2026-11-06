import runpy
import sqlite3
from pathlib import Path
from unittest.mock import Mock

import pandas as pd
import pytest

from vg2c.runtime import JobRuntime
from vg2c.runtime.query import resolve_reader

PROJECT = Path(__file__).resolve().parents[2] / "first_lot_after_ept_lots"
INTERVENTION_COLUMNS = [
    "facility",
    "parent_entity",
    "ept_entity",
    "statehist_id",
    "ept_state",
    "event_start",
    "event_end",
    "event_end_key",
]
HISTORY_COLUMNS = [
    "facility",
    "parent_entity",
    "lot",
    "operation",
    "first_lot_timestamp",
    "lot_timestamp_key",
]
OUTPUT_COLUMNS = INTERVENTION_COLUMNS[:-1] + HISTORY_COLUMNS[2:-1]


def intervention(statehist_id, end, facility="KM", entity="JDC1"):
    return [
        facility,
        entity,
        "DGX1",
        statehist_id,
        "PM",
        "2026-11-03 09:00:00",
        "2026-11-03 10:00:00",
        end,
    ]


def history_row(lot, timestamp, facility="KM", entity="JDC1"):
    return [facility, entity, lot, "1000", "2026-11-03 10:00:00", timestamp]


def _offline_job(monkeypatch, tmp_path, reader):
    job = JobRuntime(
        PROJECT,
        tmp_path,
        site="KM",
        environ={},
        test_mode_excluded={"email", "mms"},
    )
    aed = Mock()
    monkeypatch.setattr("vg2c.runtime.job.AEDClient", lambda *args, **kwargs: aed)
    monkeypatch.setattr(JobRuntime, "from_script", lambda *args, **kwargs: job)
    monkeypatch.setattr(
        "vg2c.runtime.query.resolve_reader",
        lambda node, **kwargs: reader if node else resolve_reader(),
    )
    return aed


def test_first_lot_job_two_bounded_calls(monkeypatch, tmp_path):
    interventions = pd.DataFrame(
        [intervention(1, "20261103100000500000000")],
        columns=INTERVENTION_COLUMNS,
    )
    history = pd.DataFrame(
        [
            history_row("EARLY", "20261103100000000000000"),
            history_row("FIRST", "20261103100001000000000"),
        ],
        columns=HISTORY_COLUMNS,
    )
    calls = []

    class Reader:
        def read(self, site, query):
            calls.append((site, query))
            return (interventions if len(calls) == 1 else history).copy()

    aed = _offline_job(monkeypatch, tmp_path, Reader())
    monkeypatch.chdir(tmp_path)
    runpy.run_path(str(PROJECT / "main.py"), run_name="__main__")

    assert len(calls) == 2
    aed.lot_status.assert_called_once_with("FIRST")
    assert [site for site, query in calls] == ["KM", "KM"]
    assert "F_ENTITYLOTHIST" not in calls[0][1]
    assert "event_end >= event_start" in calls[0][1]
    assert "event_end IS NOT NULL" not in calls[0][1]
    assert "F_EPT_" not in calls[1][1]
    normalized = " ".join(calls[1][1].split())
    assert "(history.facility IN ('KM'))" in normalized
    assert "(history.entity IN ('JDC1'))" in normalized
    assert "TO_DATE('20261103100000', 'YYYYMMDDHH24MISS')" in calls[1][1]
    assert "SQL_Get_CSV_List" not in calls[1][1]
    assert "<<<" not in calls[1][1]
    with sqlite3.connect(":memory:") as connection:
        connection.create_function(
            "NVL", 2, lambda value, default: default if value is None else value
        )
        connection.create_function("TO_DATE", 2, lambda value, date_format: value)
        connection.execute(
            "CREATE TABLE F_ENTITYLOTHIST "
            "(facility, entity, unique_flag, history_deleted_flag, txn_date)"
        )
        connection.executemany(
            "INSERT INTO F_ENTITYLOTHIST VALUES (?, ?, ?, ?, ?)",
            [
                ("KM", "JDC1", "Y", None, "20261103100001"),
                ("OTHER", "JDC1", "Y", None, "20261103100001"),
                ("KM", "JDC2", "Y", None, "20261103100001"),
                ("KM", "JDC1", "N", None, "20261103100001"),
                ("KM", "JDC1", "Y", "Y", "20261103100001"),
                ("KM", "JDC1", "Y", None, "20261103095959"),
            ],
        )
        predicate = calls[1][1].partition("WHERE")[2]
        rows = connection.execute(
            "SELECT facility, entity FROM F_ENTITYLOTHIST history WHERE " + predicate
        ).fetchall()
        assert rows == [("KM", "JDC1")]
    assert (tmp_path / "ept_interventions.csv").is_file()
    assert (tmp_path / "lot_history.csv").is_file()
    output = pd.read_csv(tmp_path / "first_lot_after_ept_lots.csv")
    assert output["lot"].tolist() == ["FIRST"]


def test_matching_preserves_ties_precision_and_facility(tmp_path):
    interventions = pd.DataFrame(
        [
            intervention(1, "20261103100000500000000"),
            intervention(2, "20261103100001000000000"),
            intervention(3, "20261103110000000000000"),
            intervention(4, "20261103100000500000000", facility="OTHER"),
            intervention(5, "20261103100000500000000", entity="JDC2"),
            intervention(6, "20261103100000500000000", facility=None),
            intervention(7, None),
            intervention(10, "20261103100001000000000"),
            intervention(20, "20261103100000999999999"),
        ],
        columns=INTERVENTION_COLUMNS,
    )
    history = pd.DataFrame(
        [
            history_row("EARLY", "20261103100000499999999"),
            history_row("TIE_A", "20261103100001000000000"),
            history_row("TIE_B", "20261103100001000000000"),
            history_row("TIE_A", "20261103100001000000000"),
            history_row("LATER", "20261103100002000000000"),
            history_row("OTHER", "20261103100000500000000", facility="OTHER"),
            history_row("NULL", "20261103100000500000000", facility=None),
        ],
        columns=HISTORY_COLUMNS,
    )
    interventions.to_csv(tmp_path / "ept_interventions.csv", index=False)
    history.to_csv(tmp_path / "lot_history.csv", index=False)
    job = JobRuntime(PROJECT, tmp_path, environ={})
    result = job.sql(
        "sql/query_003_match_first_lots.sql",
        output="first_lot_after_ept_lots.csv",
        inputs=[
            ("ept_interventions.csv", "interventions"),
            ("lot_history.csv", "lot_history"),
        ],
    )
    assert list(zip(result["statehist_id"], result["lot"], strict=True)) == [
        ("10", "TIE_A"),
        ("10", "TIE_B"),
        ("4", "OTHER"),
    ]
    assert result.columns.tolist() == OUTPUT_COLUMNS


def test_dedup_groups_non_dgx_and_preserves_dgx_states_and_lot_occurrences(tmp_path):
    interventions = pd.DataFrame(
        [
            intervention(1, "20261103100000000000000"),
            intervention(2, "20261103100002000000000"),
            intervention(3, "20261103100000000000000"),
            intervention(4, "20261103100000000000000"),
            intervention(5, "20261103100000250000000"),
            intervention(6, "20261103100000500000000"),
            intervention(7, "20261103100000500000000"),
        ],
        columns=INTERVENTION_COLUMNS,
    )
    interventions.loc[2, "ept_entity"] = "DGX2"
    interventions.loc[3, "ept_state"] = "SETUP SDT"
    interventions.loc[4:, "ept_entity"] = ["ASF1", "JDC1", "JLP1"]
    history = pd.DataFrame(
        [
            history_row("LOT", "20261103100001000000000"),
            history_row("LOT", "20261103100003000000000"),
            history_row("LOT", "20261103100003000000000"),
        ],
        columns=HISTORY_COLUMNS,
    )
    history["first_lot_timestamp"] = [
        "2026-11-03 10:00:01",
        "2026-11-03 10:00:03",
        "2026-11-03 10:00:03",
    ]
    history.loc[2, "operation"] = "2000"
    interventions.to_csv(tmp_path / "ept_interventions.csv", index=False)
    history.to_csv(tmp_path / "lot_history.csv", index=False)
    job = JobRuntime(PROJECT, tmp_path, environ={})
    result = job.sql(
        "sql/query_003_match_first_lots.sql",
        output="first_lot_after_ept_lots.csv",
        inputs=[
            ("ept_interventions.csv", "interventions"),
            ("lot_history.csv", "lot_history"),
        ],
    )
    assert sorted(result["statehist_id"].tolist()) == ["1", "2", "2", "3", "4", "7"]
    assert result.loc[result["statehist_id"] == "7", "ept_entity"].tolist() == ["JLP1"]
    grouped = result.assign(
        entity_group=result["ept_entity"].where(
            result["ept_entity"].str.startswith("DGX")
        )
    )
    keys = [
        "facility",
        "parent_entity",
        "entity_group",
        "ept_state",
        "lot",
        "operation",
        "first_lot_timestamp",
    ]
    assert not grouped.duplicated(keys).any()


@pytest.mark.parametrize("empty_interventions", [True, False])
def test_first_lot_job_empty_results(monkeypatch, tmp_path, empty_interventions):
    interventions = pd.DataFrame(
        [] if empty_interventions else [intervention(1, "20261103100000500000000")],
        columns=INTERVENTION_COLUMNS,
    )
    history = pd.DataFrame(columns=HISTORY_COLUMNS)
    calls = []

    class Reader:
        def read(self, site, query):
            calls.append(query)
            return (interventions if len(calls) == 1 else history).copy()

    aed = _offline_job(monkeypatch, tmp_path, Reader())
    for filename in (
        "ept_interventions.csv",
        "lot_history.csv",
        "first_lot_after_ept_lots.csv",
    ):
        (tmp_path / filename).write_text("stale,data\n1,2\n", encoding="utf-8")

    runpy.run_path(str(PROJECT / "main.py"), run_name="__main__")
    assert len(calls) == 2
    aed.lot_status.assert_not_called()
    assert "<<<" not in calls[1]
    if empty_interventions:
        normalized = " ".join(calls[1].split())
        assert "history.facility IN ('__NO_VALUES__')" in normalized
        assert "history.entity IN ('__NO_VALUES__')" in normalized
        assert "TO_DATE('', 'YYYYMMDDHH24MISS')" in normalized
    for filename, columns in (
        ("ept_interventions.csv", INTERVENTION_COLUMNS),
        ("lot_history.csv", HISTORY_COLUMNS),
        ("first_lot_after_ept_lots.csv", OUTPUT_COLUMNS),
    ):
        output = pd.read_csv(tmp_path / filename)
        assert output.columns.tolist() == columns
        if filename != "ept_interventions.csv" or empty_interventions:
            assert output.empty
