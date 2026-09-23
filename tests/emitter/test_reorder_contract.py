from pathlib import Path

import pytest

from vg2c import compile_document
from vg2c.reorder import (
    InvalidOrderChange,
    apply_order_changes,
    legal_reorder_targets,
    swap_adjacent,
)
from vg2c.workflow import project_workflow


def _source(tmp_path: Path, second_path: str = "second.txt"):
    source = tmp_path / "script.txt"
    source.write_text(
        "<OPTIONS>\n/WRITE-FILE=Y\n/CSV=first.txt\n</OPTIONS>\nfirst\n"
        "<---- New Query ---->\n"
        f"<OPTIONS>\n/WRITE-FILE=Y\n/CSV={second_path}\n</OPTIONS>\nsecond\n"
        "<---- New Query ---->\n",
        encoding="utf-8",
    )
    return compile_document(source)


def test_independent_writes_reorder_execution_semantics_and_effects(tmp_path):
    result = _source(tmp_path)
    first, second = result.resolved.scope_tree.children
    assert second.scope_id in legal_reorder_targets(result)[first.scope_id]
    order = swap_adjacent(result, first.scope_id, second.scope_id)
    reordered = apply_order_changes(result, [order])
    run_body = reordered.emitted.source.split("def run() -> None:", 1)[1]
    assert run_body.index("step_0001") < run_body.index("step_0000")
    workflow = project_workflow(reordered)
    assert [item.block_index for item in workflow.operations] == [1, 0]
    assert [item.block_index for item in workflow.effects] == [1, 0]
    assert [item.path for item in workflow.effects[1].available_before] == ["second.txt"]


def test_write_collision_cannot_reorder(tmp_path):
    result = _source(tmp_path, "first.txt")
    first, second = result.resolved.scope_tree.children
    assert legal_reorder_targets(result) == {}
    with pytest.raises(InvalidOrderChange):
        swap_adjacent(result, first.scope_id, second.scope_id)


def test_condition_is_a_fixed_reorder_barrier(tmp_path):
    source = tmp_path / "script.txt"
    source.write_text(
        "<OPTIONS>\n/WRITE-FILE=Y\n/CSV=first.txt\n</OPTIONS>\nfirst\n"
        "<---- New Query ---->\n"
        '<OPTIONS>\n/UTILITIES={IF-THEN} "a" "EQS" "a" "" "" "" ""\n</OPTIONS>\n'
        "<---- New Query ---->\n"
        "<OPTIONS>\n/WRITE-FILE=Y\n/CSV=inside.txt\n</OPTIONS>\ninside\n"
        "<---- New Query ---->\n"
        "<OPTIONS>\n/UTILITIES={END-IF}\n</OPTIONS>\n"
        "<---- New Query ---->\n"
        "<OPTIONS>\n/WRITE-FILE=Y\n/CSV=last.txt\n</OPTIONS>\nlast\n"
        "<---- New Query ---->\n",
        encoding="utf-8",
    )
    result = compile_document(source)
    first, control, last = result.resolved.scope_tree.children
    assert legal_reorder_targets(result) == {}
    with pytest.raises(InvalidOrderChange):
        swap_adjacent(result, first.scope_id, last.scope_id)
