from __future__ import annotations

from pathlib import Path

import pytest

from vg2c.dataflow import analyze
from vg2c.frontend import classify, parse
from vg2c.frontend.models import Kind
from vg2c.resolver import resolve

FIXTURE_NAMES = [
    "script_short.txt",
    "script_another.txt",
    "sql_script.txt",
    "actual_script.txt",
]


@pytest.mark.parametrize("fixture_name", FIXTURE_NAMES)
def test_pipeline_runs_end_to_end(FIXTURES: Path, fixture_name: str) -> None:
    program = _resolve_fixture(FIXTURES, fixture_name)
    assert program.scope_tree.kind == "program"
    assert len(program.blocks) >= 1


@pytest.mark.parametrize(
    "fixture_name",
    [
        "script_short.txt",
        "script_another.txt",
        "sql_script.txt",
    ],
)
def test_no_error_diagnostics_on_clean_fixtures(
    FIXTURES: Path, fixture_name: str
) -> None:
    program = _resolve_fixture(FIXTURES, fixture_name)
    assert not [d for d in program.diagnostics if d.severity == "error"]


def test_script_short_flat_scope_and_no_runtime_refs(FIXTURES: Path) -> None:
    program = _resolve_fixture(FIXTURES, "script_short.txt")
    assert not [n for n in _all_nodes(program.scope_tree) if n.kind in {"macro", "if"}]
    assert len(program.blocks) >= 1


def test_sql_script_detects_sql_get_csv_list_call(FIXTURES: Path) -> None:
    analyzed = _analyze_fixture(FIXTURES, "sql_script.txt")
    calls = [call for b in analyzed.resolved.blocks for call in b.sql_macro_calls]
    assert len(calls) == 1
    assert calls[0].csv_path.lower().endswith("yeuchuan_a0_29397.tab")
    assert calls[0].column_ref == "lot"


def test_actual_script_scope_and_macro_signals(FIXTURES: Path) -> None:
    program = _resolve_fixture(FIXTURES, "actual_script.txt")
    analyzed = _analyze_fixture(FIXTURES, "actual_script.txt")
    nodes = list(_all_nodes(program.scope_tree))
    macro_nodes = [n for n in nodes if n.kind == "macro"]
    if_nodes = [n for n in nodes if n.kind == "if"]

    assert len(macro_nodes) >= 2
    assert len(if_nodes) >= 5
    assert _max_depth(program.scope_tree) >= 3

    calls = [
        call for block in analyzed.resolved.blocks for call in block.sql_macro_calls
    ]
    assert len(calls) >= 4
    assert any(isinstance(call.column_ref, str) for call in calls)
    assert any(isinstance(call.column_ref, int) for call in calls)


def test_macro_control_blocks_have_scope_representation(FIXTURES: Path) -> None:
    text = (FIXTURES / "actual_script.txt").read_text(
        encoding="utf-8", errors="replace"
    )
    parsed, pdiag = parse(text, source=FIXTURES / "actual_script.txt")
    classified, cdiag = classify(parsed)
    program = resolve(classified, diagnostics=[*pdiag, *cdiag])

    macro_indices = [b.index for b in classified if b.kind is Kind.MACRO_CONTROL]
    for idx in macro_indices:
        assert _index_covered_by_scope(program.scope_tree, idx)


def _resolve_fixture(fixtures: Path, file_name: str):
    text = (fixtures / file_name).read_text(encoding="utf-8", errors="replace")
    parsed, pdiag = parse(text, source=fixtures / file_name)
    classified, cdiag = classify(parsed)
    return resolve(classified, diagnostics=[*pdiag, *cdiag])


def _analyze_fixture(fixtures: Path, file_name: str):
    resolved = _resolve_fixture(fixtures, file_name)
    return analyze(resolved)


def _all_nodes(node):
    yield node
    for child in node.children:
        yield from _all_nodes(child)


def _max_depth(node) -> int:
    if not node.children:
        return 1
    return 1 + max(_max_depth(child) for child in node.children)


def _index_covered_by_scope(node, idx: int) -> bool:
    if node.start_index <= idx <= node.end_index:
        return True
    return any(_index_covered_by_scope(child, idx) for child in node.children)
