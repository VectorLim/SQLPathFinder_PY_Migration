from __future__ import annotations

from pathlib import Path

import pytest

from vg2c.dataflow import analyze
from vg2c.dispatch import dispatch
from datasyncx import AriesReader, MarsReader, OracleReader
from vg2c.dispatch.dialects.sqlite import SqliteReader
from vg2c.frontend import classify, parse
from vg2c.resolver import resolve

FIXTURE_NAMES = [
    "script_short.txt",
    "script_another.txt",
    "sql_script.txt",
    "actual_script.txt",
]

# SQL-bearing Kind values for counting dispatched blocks
from vg2c.kind import Kind

_SQL_BEARING = {Kind.SQL_QUERY, Kind.SQLITE_QUERY}


def _run_pipeline(fixtures: Path, file_name: str):
    text = (fixtures / file_name).read_text(encoding="utf-8", errors="replace")
    parsed = parse(text, source=fixtures / file_name)
    classified = classify(parsed)
    resolved = resolve(classified)
    analyzed = analyze(resolved)
    return dispatch(analyzed)


# --- Smoke tests ---


@pytest.mark.parametrize("fixture_name", FIXTURE_NAMES)
def test_full_pipeline_runs_without_exception(
    FIXTURES: Path, fixture_name: str
) -> None:
    program = _run_pipeline(FIXTURES, fixture_name)
    assert isinstance(program.dispatched, tuple)


@pytest.mark.parametrize("fixture_name", FIXTURE_NAMES)
def test_dispatched_count_equals_sql_bearing_blocks(
    FIXTURES: Path, fixture_name: str
) -> None:
    """dispatched tuple has exactly one entry per SQL-bearing block."""
    program = _run_pipeline(FIXTURES, fixture_name)
    sql_block_count = sum(
        1 for b in program.analyzed.resolved.blocks if b.kind in _SQL_BEARING
    )
    assert len(program.dispatched) == sql_block_count


def test_sql_script_oasys_schema_substituted(FIXTURES: Path) -> None:
    """Verify @OASYSSCHEMA@ is replaced with an empty string in rewritten_sql."""
    program = _run_pipeline(FIXTURES, "sql_script.txt")
    for db in program.dispatched:
        if db.reader_cls is OracleReader:
            assert "@OASYSSCHEMA@" not in db.rewritten_sql


# --- Per-fixture spot checks ---


def test_script_short_one_sqlite_block(FIXTURES: Path) -> None:
    program = _run_pipeline(FIXTURES, "script_short.txt")
    assert len(program.dispatched) == 1
    assert program.dispatched[0].reader_cls is SqliteReader


def test_script_another_mars_calendar_record(FIXTURES: Path) -> None:
    program = _run_pipeline(FIXTURES, "script_another.txt")
    mars_blocks = [d for d in program.dispatched if d.reader_cls is MarsReader]
    assert mars_blocks
    # At least one MARS block has Calendar record metadata
    assert any(
        d.reader_target.record_name == "Calendar"
        and d.reader_target.record_version == "1.0.0.0"
        for d in mars_blocks
    )


def test_sql_script_has_mars_oasys_sqlite(FIXTURES: Path) -> None:
    program = _run_pipeline(FIXTURES, "sql_script.txt")
    reader_classes = {d.reader_cls for d in program.dispatched}
    assert MarsReader in reader_classes
    assert OracleReader in reader_classes
    assert SqliteReader in reader_classes


def test_sql_script_mars_record_metadata(FIXTURES: Path) -> None:
    program = _run_pipeline(FIXTURES, "sql_script.txt")
    mars = [d for d in program.dispatched if d.reader_cls is MarsReader]
    assert mars
    assert any(d.reader_target.record_name == "WIP_Lot_History_v2" for d in mars)


def test_actual_script_has_mars_aries_sqlite(FIXTURES: Path) -> None:
    program = _run_pipeline(FIXTURES, "actual_script.txt")
    reader_classes = {d.reader_cls for d in program.dispatched}
    assert MarsReader in reader_classes
    assert AriesReader in reader_classes
    assert SqliteReader in reader_classes


def test_actual_script_record_names_present(FIXTURES: Path) -> None:
    program = _run_pipeline(FIXTURES, "actual_script.txt")
    record_names = {
        d.reader_target.record_name
        for d in program.dispatched
        if d.reader_target.record_name
    }
    assert "WIP_Lot_History_v2" in record_names or "AT_Metrology" in record_names


# --- /NODE site extraction (ReaderTarget.site) ---


def test_script_another_site_is_km(FIXTURES: Path) -> None:
    """/NODE=KM.[A15_PROD_21.].MARS -> site "KM"."""
    program = _run_pipeline(FIXTURES, "script_another.txt")
    mars_blocks = [d for d in program.dispatched if d.reader_cls is MarsReader]
    assert mars_blocks
    assert all(d.reader_target.site == "KM" for d in mars_blocks)


def test_sql_script_site_is_km_across_dialects(FIXTURES: Path) -> None:
    """/NODE=KM.[...].MARS and /NODE=KM.OASYS both resolve to site "KM"."""
    program = _run_pipeline(FIXTURES, "sql_script.txt")
    non_sqlite = [d for d in program.dispatched if d.reader_cls is not SqliteReader]
    assert non_sqlite
    assert all(d.reader_target.site == "KM" for d in non_sqlite)


def test_actual_script_macro_placeholder_node_has_no_site(FIXTURES: Path) -> None:
    """/NODE=<<<MARS>>> / <<<ARIES>>> carry no literal site at compile time."""
    program = _run_pipeline(FIXTURES, "actual_script.txt")
    assert program.dispatched
    assert all(d.reader_target.site == "" for d in program.dispatched)
