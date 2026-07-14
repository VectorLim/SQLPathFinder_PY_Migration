from __future__ import annotations

from pathlib import Path

import pytest

from vg2c.dataflow import analyze
from vg2c.frontend import classify, parse
from vg2c.kind import Kind
from vg2c.resolver import resolve

FIXTURE_NAMES = [
    "script_short.txt",
    "script_another.txt",
    "sql_script.txt",
    "actual_script.txt",
]


def _run_pipeline(fixtures: Path, file_name: str):
    text = (fixtures / file_name).read_text(encoding="utf-8", errors="replace")
    parsed = parse(text, source=fixtures / file_name)
    classified = classify(parsed)
    resolved = resolve(classified)
    return analyze(resolved)


@pytest.mark.parametrize("fixture_name", FIXTURE_NAMES)
def test_pipeline_stage1_to_stage3_runs(FIXTURES: Path, fixture_name: str) -> None:
    analyzed = _run_pipeline(FIXTURES, fixture_name)
    assert analyzed.resolved.scope_tree.kind == "program"
    assert len(analyzed.resolved.blocks) >= 1
    assert isinstance(analyzed.edges, tuple)


def test_edges_exist_on_cross_block_fixtures(FIXTURES: Path) -> None:
    for fixture_name in [
        "sql_script.txt",
        "actual_script.txt",
    ]:
        analyzed = _run_pipeline(FIXTURES, fixture_name)
        assert len(analyzed.edges) > 0


def test_sql_script_links_db_read_to_sql_macro_consumer(FIXTURES: Path) -> None:
    analyzed = _run_pipeline(FIXTURES, "sql_script.txt")
    target_edges = [
        e
        for e in analyzed.edges
        if e.csv_path.endswith("yeuchuan_a0_29397.tab")
        and e.consumer.consumer_kind == "sql-macro"
    ]
    assert target_edges
    assert all(edge.producer is not None for edge in target_edges)
    assert all(
        edge.producer.producer_kind is Kind.SQL_QUERY
        for edge in target_edges
        if edge.producer
    )


def test_actual_script_has_scope_and_external_signals(FIXTURES: Path) -> None:
    analyzed = _run_pipeline(FIXTURES, "actual_script.txt")
    sql_macro_edges = [
        e for e in analyzed.edges if e.consumer.consumer_kind == "sql-macro"
    ]
    assert sql_macro_edges
    assert all(e.producer is not None for e in sql_macro_edges)
