from dataclasses import replace

from vg2c import compile_document
from vg2c.emitter import emit
from vg2c.semantics import build_semantic_model
from vg2c.workflow import project_workflow


def test_scope_sibling_order_controls_generated_execution_and_semantic_order(tmp_path):
    source = tmp_path / "script.txt"
    source.write_text(
        "<OPTIONS>\n/WRITE-FILE=Y\n/CSV=first.txt\n</OPTIONS>\nfirst\n"
        "<---- New Query ---->\n"
        "<OPTIONS>\n/WRITE-FILE=Y\n/CSV=second.txt\n</OPTIONS>\nsecond\n"
        "<---- New Query ---->\n",
        encoding="utf-8",
    )
    original = compile_document(source)
    root = original.resolved.scope_tree
    assert len(root.children) == 2
    reordered = replace(root, children=tuple(reversed(root.children)))
    resolved = replace(original.resolved, scope_tree=reordered)
    dispatched = replace(original.dispatched, resolved=resolved)
    result = replace(original, resolved=resolved, dispatched=dispatched, emitted=emit(dispatched))

    run_body = result.emitted.source.split("def run() -> None:", 1)[1]
    assert run_body.index("step_0001") < run_body.index("step_0000")
    operations = build_semantic_model(result).operations
    assert [item.block_index for item in operations] == [1, 0]
    workflow = project_workflow(result)
    assert [item.block_index for item in workflow.operations] == [1, 0]
    assert [item.block_index for item in workflow.effects] == [1, 0]
