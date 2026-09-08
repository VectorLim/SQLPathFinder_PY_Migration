from typing import Any

from vg2c.dispatch.models import (
    DispatchedBlock,
    DispatchedProgram,
    ReaderSpec,
    ReaderTarget,
    SQLFilter,
)
from vg2c.emitter import _sql_filter_comment_lines
from vg2c.frontend.models import BlockOptions, ClassifiedBlock, ParsedBlock, SourceSpan
from vg2c.kind import Kind
from vg2c.resolver.models import ResolvedBlock


def _dummy_resolved_block(index: int) -> ResolvedBlock:
    parsed = ParsedBlock(
        index=index,
        options=BlockOptions.from_pairs(()),
        body="",
        raw="",
        span=SourceSpan(None, 1, 1),
    )
    classified = ClassifiedBlock(parsed, Kind.SQL_QUERY, "")
    return ResolvedBlock(classified, BlockOptions.from_pairs(()), "", None, 0)


def test_sql_filter_comments_use_final_step_lines() -> None:
    target = ReaderTarget(None, None, "node", None)
    reader = ReaderSpec(module="example", name="DummyReader")
    block1 = DispatchedBlock(
        resolved=_dummy_resolved_block(1),
        reader=reader,
        reader_kwargs={},
        reader_target=target,
        rewritten_sql="SELECT * FROM t WHERE a = 1",
        step_name="step_0001_sql_query",
        sql_filters=(
            SQLFilter(
                step_name="step_0001_sql_query",
                attributes=("a",),
                sql_statement="a = 1",
            ),
        ),
    )
    block2 = DispatchedBlock(
        resolved=_dummy_resolved_block(2),
        reader=reader,
        reader_kwargs={},
        reader_target=target,
        rewritten_sql="SELECT * FROM t2",
        step_name="step_0002_sql_query",
        sql_filters=(),
    )

    dp: Any = DispatchedProgram(
        analyzed=None,  # type: ignore[arg-type]
        dispatched=(block1, block2),
    )
    step_lines = {
        "step_0001_sql_query": 1,
        "step_0002_sql_query": 4,
    }

    comments = _sql_filter_comment_lines(dp, step_lines)

    assert comments == (
        "# SQL statements containing filters:",
        "# - step_0001_sql_query (Line 4): filters on a",
    )
