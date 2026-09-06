from __future__ import annotations

from pathlib import Path

import pytest

from vg2c.dataflow import analyze
from vg2c.dispatch import dispatch
from vg2c.frontend import classify, parse
from vg2c.kind import Kind
from vg2c.resolver import resolve

FIXTURE_NAMES = [
    "script_short.txt",
    "script_another.txt",
    "sql_script.txt",
    "actual_script.txt",
]

_SQL_BEARING = {Kind.SQL_QUERY, Kind.SQLITE_QUERY}


def _run_pipeline(fixtures: Path, file_name: str):
    text = (fixtures / file_name).read_text(encoding="utf-8", errors="replace")
    parsed = parse(text, source=fixtures / file_name)
    classified = classify(parsed)
    resolved = resolve(classified)
    analyzed = analyze(resolved)
    return dispatch(analyzed)


def _reader_names(program) -> set[str]:
    return {block.reader.name for block in program.dispatched}


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
    program = _run_pipeline(FIXTURES, fixture_name)
    sql_block_count = sum(
        1 for block in program.analyzed.resolved.blocks if block.kind in _SQL_BEARING
    )
    assert len(program.dispatched) == sql_block_count


def test_sql_script_oasys_schema_substituted(FIXTURES: Path) -> None:
    program = _run_pipeline(FIXTURES, "sql_script.txt")
    for block in program.dispatched:
        if block.reader.name == "OracleReader":
            assert "@OASYSSCHEMA@" not in block.rewritten_sql


def test_script_short_one_sqlite_block(FIXTURES: Path) -> None:
    program = _run_pipeline(FIXTURES, "script_short.txt")
    assert len(program.dispatched) == 1
    assert program.dispatched[0].reader.name == "SqliteReader"
    assert program.dispatched[0].reader.utility_name == "sqlite_reader"


def test_script_another_mars_calendar_record(FIXTURES: Path) -> None:
    program = _run_pipeline(FIXTURES, "script_another.txt")
    mars_blocks = [block for block in program.dispatched if block.reader.name == "MarsReader"]
    assert mars_blocks
    assert any(
        block.reader_target.record_name == "Calendar"
        and block.reader_target.record_version == "1.0.0.0"
        for block in mars_blocks
    )


def test_sql_script_has_mars_oasys_sqlite(FIXTURES: Path) -> None:
    program = _run_pipeline(FIXTURES, "sql_script.txt")
    assert {"MarsReader", "OracleReader", "SqliteReader"} <= _reader_names(program)


def test_sql_script_mars_record_metadata(FIXTURES: Path) -> None:
    program = _run_pipeline(FIXTURES, "sql_script.txt")
    mars = [block for block in program.dispatched if block.reader.name == "MarsReader"]
    assert mars
    assert any(block.reader_target.record_name == "WIP_Lot_History_v2" for block in mars)


def test_actual_script_has_mars_aries_sqlite(FIXTURES: Path) -> None:
    program = _run_pipeline(FIXTURES, "actual_script.txt")
    assert {"MarsReader", "AriesReader", "SqliteReader"} <= _reader_names(program)


def test_actual_script_record_names_present(FIXTURES: Path) -> None:
    program = _run_pipeline(FIXTURES, "actual_script.txt")
    record_names = {
        block.reader_target.record_name
        for block in program.dispatched
        if block.reader_target.record_name
    }
    assert "WIP_Lot_History_v2" in record_names or "AT_Metrology" in record_names


def test_script_another_site_is_km(FIXTURES: Path) -> None:
    program = _run_pipeline(FIXTURES, "script_another.txt")
    mars_blocks = [block for block in program.dispatched if block.reader.name == "MarsReader"]
    assert mars_blocks
    assert all(block.reader_target.site == "KM" for block in mars_blocks)


def test_sql_script_site_is_km_across_dialects(FIXTURES: Path) -> None:
    program = _run_pipeline(FIXTURES, "sql_script.txt")
    non_sqlite = [
        block for block in program.dispatched if block.reader.name != "SqliteReader"
    ]
    assert non_sqlite
    assert all(block.reader_target.site == "KM" for block in non_sqlite)


def test_actual_script_macro_placeholder_node_has_no_site(FIXTURES: Path) -> None:
    program = _run_pipeline(FIXTURES, "actual_script.txt")
    assert program.dispatched
    assert all(block.reader_target.site == "" for block in program.dispatched)
