from __future__ import annotations

from vg2c.frontend.models import (
    BlockOptions,
    ClassifiedBlock,
    Kind,
    ParsedBlock,
    SourceSpan,
)
from vg2c.resolver.macro_resolver import resolve_macros
from vg2c.resolver.models import IfThen, RowsInFile, RunLoop, StartMacro
from vg2c.resolver.scope_builder import build_scope_tree


def _block(
    index: int,
    kind: Kind,
    options: dict[str, str] | None = None,
    body: str = "",
) -> ClassifiedBlock:
    parsed = ParsedBlock(
        index=index,
        options=BlockOptions.from_pairs((options or {}).items()),
        body=body,
        raw="",
        span=SourceSpan(file=None, start_line=index + 1, end_line=index + 1),
    )
    return ClassifiedBlock(parsed=parsed, kind=kind, reason="test")


def test_start_macro_payload_is_parsed() -> None:
    blocks = [
        _block(0, Kind.MACRO_CONTROL, {"UTILITIES": '{START-MACRO} "config.csv" "N"'}),
        _block(1, Kind.UTILITY, {"UTILITIES": "echo run"}),
        _block(2, Kind.MACRO_CONTROL, {"UTILITIES": "{END-MACRO}"}),
    ]
    tree, _ = build_scope_tree(blocks)
    resolved, _ = resolve_macros(blocks, tree)

    payload = resolved[0].control_payload
    assert isinstance(payload, StartMacro)
    assert payload.csv_path == "config.csv"
    assert payload.prompt_off is False


def test_if_then_payload_is_parsed() -> None:
    blocks = [
        _block(
            0,
            Kind.MACRO_CONTROL,
            {"UTILITIES": '{IF-THEN} "A" "EQS" "B" "AND" "C" "NE" "D"'},
        )
    ]
    tree, _ = build_scope_tree(blocks)
    resolved, _ = resolve_macros(blocks, tree)

    payload = resolved[0].control_payload
    assert isinstance(payload, IfThen)
    assert payload.lhs == "A"
    assert payload.op == "EQS"
    assert payload.rhs == "B"
    assert payload.conj == "AND"
    assert payload.lhs2 == "C"
    assert payload.op2 == "NE"
    assert payload.rhs2 == "D"


def test_rows_in_file_payload_present() -> None:
    blocks = [
        _block(
            0, Kind.MACRO_CONTROL, {"UTILITIES": '{ROWS-IN-FILE} "f.csv" "COUNT" "N"'}
        )
    ]
    tree, _ = build_scope_tree(blocks)
    resolved, _ = resolve_macros(blocks, tree)
    assert isinstance(resolved[0].control_payload, RowsInFile)


def test_run_loop_payload_is_parsed() -> None:
    blocks = [
        _block(
            0,
            Kind.MACRO_CONTROL,
            {"UTILITIES": '{RUN-LOOP} "in.csv" "chunk.csv" "123" "N"'},
        )
    ]
    tree, _ = build_scope_tree(blocks)
    resolved, _ = resolve_macros(blocks, tree)
    payload = resolved[0].control_payload
    assert isinstance(payload, RunLoop)
    assert payload.input_csv_path == "in.csv"
    assert payload.chunk_csv_path == "chunk.csv"
    assert payload.chunk_size == 123
    assert payload.prompt_off is False


def test_scope_id_assigned_for_every_block() -> None:
    blocks = [
        _block(0, Kind.MACRO_CONTROL, {"UTILITIES": '{START-MACRO} "a.csv" "N"'}),
        _block(1, Kind.UTILITY, {"UTILITIES": "echo hello"}),
        _block(2, Kind.MACRO_CONTROL, {"UTILITIES": "{END-MACRO}"}),
    ]
    tree, _ = build_scope_tree(blocks)
    resolved, _ = resolve_macros(blocks, tree)
    assert all(block.scope_id >= 0 for block in resolved)
