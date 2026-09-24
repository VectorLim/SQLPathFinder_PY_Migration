from __future__ import annotations

from pathlib import Path

import pytest

from vg2c import compile_document
from vg2c.workflow import project_document

FIXTURE_NAMES = [
    "script_short.txt",
    "script_another.txt",
    "sql_script.txt",
    "actual_script.txt",
]


@pytest.mark.parametrize("fixture_name", FIXTURE_NAMES)
def test_compiler_and_semantic_workflow_run(FIXTURES: Path, fixture_name: str) -> None:
    result = compile_document(FIXTURES / fixture_name)
    workflow = project_document(result)

    assert result.resolved.scope_tree.kind == "program"
    assert len(result.resolved.blocks) >= 1
    assert isinstance(workflow.effects, tuple)
    assert isinstance(workflow.operations, tuple)


def test_cross_block_fixtures_have_file_dependencies(FIXTURES: Path) -> None:
    for fixture_name in ["sql_script.txt", "actual_script.txt"]:
        workflow = project_document(compile_document(FIXTURES / fixture_name))
        assert any(effect.dependency_ids for effect in workflow.effects)


def test_sql_script_links_sql_get_csv_list_to_prior_producer(FIXTURES: Path) -> None:
    workflow = project_document(compile_document(FIXTURES / "sql_script.txt"))
    target_inputs = [
        (effect, endpoint)
        for effect in workflow.effects
        for endpoint in effect.inputs
        if endpoint.path and endpoint.path.lower().endswith("yeuchuan_a0_29397.tab")
    ]
    assert target_inputs
    assert all(effect.dependency_ids for effect, _ in target_inputs)


def test_actual_script_preserves_control_and_external_file_signals(FIXTURES: Path) -> None:
    workflow = project_document(compile_document(FIXTURES / "actual_script.txt"))
    assert any(operation.kind == "macro-loop" for operation in workflow.operations)
    assert any(effect.dependency_ids for effect in workflow.effects)
