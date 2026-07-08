from __future__ import annotations
from typing import Any
from vg2c.dispatch.models import DispatchedProgram, DispatchedBlock, SQLFilter, ReaderTarget
from vg2c.emitter import post_process_comments

class DummyClass:
    pass

def test_post_process_comments():
    target = ReaderTarget(None, None, "node", None)
    block1 = DispatchedBlock(
        block_index=1,
        reader_cls=DummyClass,
        reader_kwargs={},
        reader_target=target,
        rewritten_sql="SELECT * FROM t WHERE a = 1",
        step_name="step_0001_sql_query",
        sql_filters=(
            SQLFilter(
                step_name="step_0001_sql_query",
                attributes=("a",),
                sql_statement="a = 1"
            ),
        )
    )
    block2 = DispatchedBlock(
        block_index=2,
        reader_cls=DummyClass,
        reader_kwargs={},
        reader_target=target,
        rewritten_sql="SELECT * FROM t2",
        step_name="step_0002_sql_query",
        sql_filters=()
    )
    
    # Use cast or dummy structure since we only access .dispatched attribute
    dp: Any = DispatchedProgram(
        analyzed=None,  # type: ignore
        dispatched=(block1, block2),
        diagnostics=()
    )
    
    source = "def step_0001_sql_query(ctx):\n    pass\n\ndef step_0002_sql_query(ctx):\n    pass"
    step_lines = {
        "step_0001_sql_query": 1,
        "step_0002_sql_query": 4
    }
    
    processed = post_process_comments(source, dp, step_lines)
    
    # 1 filter step -> len(steps_with_filters) = 1
    # num_comment_lines = 1 + 2 = 3
    # original line 1 -> final line = 1 + 3 = 4
    lines = processed.split("\n")
    assert lines[0] == "# SQL statements containing filters:"
    assert lines[1] == "# - step_0001_sql_query (Line 4): filters on a"
    assert lines[2] == ""
    assert lines[3] == "def step_0001_sql_query(ctx):"
