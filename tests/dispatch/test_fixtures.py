from __future__ import annotations

from pathlib import Path

import pytest

from vg2c.dataflow import analyze
from vg2c.dispatch import dispatch
from vg2c.dispatch.models import DispatchConfig
from vg2c.frontend import classify, parse
from vg2c.resolver import resolve

FIXTURE_NAMES = [
    "script_short.txt",
    "script_another.txt",
    "sql_script.txt",
    "actual_script.txt",
]

# SQL-bearing Kind values for counting dispatched blocks
from vg2c.frontend.models import Kind

_SQL_BEARING = {Kind.SQL_QUERY, Kind.SQLITE_QUERY}


def _run_pipeline(fixtures: Path, file_name: str, config=None):
    text = (fixtures / file_name).read_text(encoding="utf-8", errors="replace")
    parsed, pdiag = parse(text, source=fixtures / file_name)
    classified, cdiag = classify(parsed)
    resolved = resolve(classified, diagnostics=[*pdiag, *cdiag])
    analyzed = analyze(resolved)
    return dispatch(analyzed, config=config)


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
    program = _run_pipeline(
        FIXTURES, fixture_name, config=DispatchConfig(oasys_schema="SCHEMA")
    )
    sql_block_count = sum(
        1 for b in program.analyzed.resolved.blocks if b.kind in _SQL_BEARING
    )
    assert len(program.dispatched) == sql_block_count


# --- No new error diagnostics on clean fixtures (with schema configured) ---


@pytest.mark.parametrize(
    "fixture_name",
    ["script_short.txt", "script_another.txt"],
)
def test_no_error_diagnostics_on_clean_fixtures(
    FIXTURES: Path, fixture_name: str
) -> None:
    program = _run_pipeline(
        FIXTURES, fixture_name, config=DispatchConfig(oasys_schema="SCHEMA")
    )
    stage4_errors = [d for d in program.diagnostics if d.severity == "error"]
    assert not stage4_errors


def test_sql_script_oasys_unset_emits_error(FIXTURES: Path) -> None:
    """sql_script has @OASYSSCHEMA@; without config this should error."""
    program = _run_pipeline(FIXTURES, "sql_script.txt", config=None)
    assert any(
        d.code == "dispatch-oasys-schema-unset" and d.severity == "error"
        for d in program.diagnostics
    )


def test_sql_script_oasys_schema_substituted(FIXTURES: Path) -> None:
    """With oasys_schema configured, @OASYSSCHEMA@ must not appear in any rewritten_sql."""
    program = _run_pipeline(
        FIXTURES, "sql_script.txt", config=DispatchConfig(oasys_schema="OASYS_OWN")
    )
    for db in program.dispatched:
        if db.dialect == "oracle_oasys":
            assert "@OASYSSCHEMA@" not in db.rewritten_sql
            assert "OASYS_OWN." in db.rewritten_sql


# --- Per-fixture spot checks ---


def test_script_short_one_sqlite_block(FIXTURES: Path) -> None:
    program = _run_pipeline(FIXTURES, "script_short.txt", config=DispatchConfig())
    assert len(program.dispatched) == 1
    assert program.dispatched[0].dialect == "sqlite"
    assert program.dispatched[0].reader_target.database_arg is None


def test_script_another_mars_calendar_record(FIXTURES: Path) -> None:
    program = _run_pipeline(FIXTURES, "script_another.txt", config=DispatchConfig())
    mars_blocks = [d for d in program.dispatched if d.dialect == "oracle_mars"]
    assert mars_blocks
    # At least one MARS block has Calendar record metadata
    assert any(
        d.reader_target.record_name == "Calendar"
        and d.reader_target.record_version == "1.0.0.0"
        for d in mars_blocks
    )


def test_sql_script_has_mars_oasys_sqlite(FIXTURES: Path) -> None:
    program = _run_pipeline(
        FIXTURES, "sql_script.txt", config=DispatchConfig(oasys_schema="OASYS_OWN")
    )
    dialects = {d.dialect for d in program.dispatched}
    assert "oracle_mars" in dialects
    assert "oracle_oasys" in dialects
    assert "sqlite" in dialects


def test_sql_script_mars_record_metadata(FIXTURES: Path) -> None:
    program = _run_pipeline(
        FIXTURES, "sql_script.txt", config=DispatchConfig(oasys_schema="OASYS_OWN")
    )
    mars = [d for d in program.dispatched if d.dialect == "oracle_mars"]
    assert mars
    assert any(d.reader_target.record_name == "WIP_Lot_History_v2" for d in mars)


def test_actual_script_has_mars_aries_sqlite(FIXTURES: Path) -> None:
    program = _run_pipeline(FIXTURES, "actual_script.txt", config=DispatchConfig())
    dialects = {d.dialect for d in program.dispatched}
    assert "oracle_mars" in dialects
    assert "oracle_aries" in dialects
    assert "sqlite" in dialects


def test_actual_script_aries_emits_untested_note(FIXTURES: Path) -> None:
    program = _run_pipeline(FIXTURES, "actual_script.txt", config=DispatchConfig())
    assert any(d.code == "dispatch-aries-rule-untested" for d in program.diagnostics)


def test_actual_script_record_names_present(FIXTURES: Path) -> None:
    program = _run_pipeline(FIXTURES, "actual_script.txt", config=DispatchConfig())
    record_names = {
        d.reader_target.record_name
        for d in program.dispatched
        if d.reader_target.record_name
    }
    assert "WIP_Lot_History_v2" in record_names or "AT_Metrology" in record_names
