from __future__ import annotations

from pathlib import Path

import pytest

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

_SQL_BEARING = {Kind.SQL_QUERY, Kind.SQLITE_QUERY, Kind.DUCKDB_QUERY}


def _run_pipeline(fixtures: Path, file_name: str):
    text = (fixtures / file_name).read_text(encoding="utf-8", errors="replace")
    parsed = parse(text, source=fixtures / file_name)
    classified = classify(parsed)
    resolved = resolve(classified)
    return dispatch(resolved)


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
        1 for block in program.resolved.blocks if block.kind in _SQL_BEARING
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


def test_sql_script_has_mars_oasys_sqlite(FIXTURES: Path) -> None:
    program = _run_pipeline(FIXTURES, "sql_script.txt")
    assert {"MarsReader", "OracleReader", "SqliteReader"} <= _reader_names(program)


def test_actual_script_has_mars_aries_sqlite(FIXTURES: Path) -> None:
    program = _run_pipeline(FIXTURES, "actual_script.txt")
    assert {"MarsReader", "AriesReader", "SqliteReader"} <= _reader_names(program)


