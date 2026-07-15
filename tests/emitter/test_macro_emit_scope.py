from __future__ import annotations

from vg2c.emitter.models import IndentWriter
from vg2c.operands.base import ScopeNode
from vg2c.operands.macro import StartMacro


def _leaf() -> ScopeNode:
    return ScopeNode(
        scope_id=1,
        kind="leaf",
        start_index=0,
        end_index=0,
        children=(),
        block_index=0,
        control_payload=None,
    )


def test_emit_scope_uses_scoped_rows_for_csv_macros():
    writer = IndentWriter()
    payload = StartMacro(csv_path="configsets.csv", prompt_off=False)

    payload.emit_scope(writer, lambda _node: writer.write("pass"), (_leaf(),))

    source = writer.source()
    assert "with ctx.macro.scope(ctx.csv_io.single_row('configsets.csv')):" in source
    assert "for _ in" not in source
    assert "with ctx.macro.scope(__row):" not in source


def test_emit_scope_uses_scope_context_for_static_macros():
    writer = IndentWriter()
    payload = StartMacro(csv_path="", prompt_off=False)

    payload.emit_scope(writer, lambda _node: writer.write("pass"), (_leaf(),))

    source = writer.source()
    assert "with ctx.macro.scope():" in source
    assert "scoped_rows(" not in source
