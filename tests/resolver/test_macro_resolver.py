from __future__ import annotations

import logging
from pathlib import Path

import pytest

from tests.resolver._fixture_flow import (
    blocks_for_token,
    parse_classify_fixture,
)
from vg2c.frontend.models import (
    BlockOptions,
    ClassifiedBlock,
    ParsedBlock,
    SourceSpan,
)
from vg2c.kind import Kind
from vg2c.operands import (
    Else,
    EndIf,
    EndLoop,
    EndMacro,
    IfThen,
    RunLoop,
    StartMacro,
)
from vg2c.resolver.macro_resolver import resolve_macros
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


def _resolve_blocks(blocks: list[ClassifiedBlock]):
    tree = build_scope_tree(blocks)
    return resolve_macros(blocks, tree)


@pytest.mark.parametrize(
    ("utilities", "payload_type", "expected_attrs"),
    [
        (
            '{START-MACRO} "config.csv" "Y"',
            StartMacro,
            {"csv_path": "config.csv", "prompt_off": True},
        ),
        (
            '{IF-THEN} "A" "EQS" "B" "AND" "C" "NE" "D"',
            IfThen,
            {
                "lhs": "A",
                "op": "EQS",
                "rhs": "B",
                "conj": "AND",
                "lhs2": "C",
                "op2": "NE",
                "rhs2": "D",
            },
        ),
        (
            '{RUN-LOOP} "in.csv" "chunk.csv" "123" "N"',
            RunLoop,
            {
                "input_csv_path": "in.csv",
                "chunk_csv_path": "chunk.csv",
                "chunk_size": 123,
                "prompt_off": False,
            },
        ),
    ],
)
def test_control_payloads_are_parsed(
    utilities: str,
    payload_type: type,
    expected_attrs: dict[str, object],
) -> None:
    blocks = [_block(0, Kind.MACRO_CONTROL, {"UTILITIES": utilities})]
    resolved = _resolve_blocks(blocks)

    payload = resolved[0].control_payload
    assert isinstance(payload, payload_type)
    for attr, value in expected_attrs.items():
        assert getattr(payload, attr) == value


@pytest.mark.parametrize(
    ("utilities", "payload_type"),
    [
        ("{END-MACRO}", EndMacro),
        ("{END-IF}", EndIf),
        ("{END-LOOP}", EndLoop),
    ],
)
def test_orphan_closers_receive_payload_types(
    utilities: str,
    payload_type: type,
) -> None:
    blocks = [_block(0, Kind.MACRO_CONTROL, {"UTILITIES": utilities})]
    resolved = _resolve_blocks(blocks)
    assert isinstance(resolved[0].control_payload, payload_type)


def test_else_payload_present_when_else_branch_exists() -> None:
    blocks = [
        _block(
            0,
            Kind.MACRO_CONTROL,
            {"UTILITIES": '{IF-THEN} "A" "EQS" "B" "" "" "" ""'},
        ),
        _block(1, Kind.EXTERNAL_RUN, {"UTILITIES": "true_branch.bat"}),
        _block(2, Kind.MACRO_CONTROL, {"UTILITIES": "{ELSE}"}),
        _block(3, Kind.EXTERNAL_RUN, {"UTILITIES": "false_branch.bat"}),
        _block(4, Kind.MACRO_CONTROL, {"UTILITIES": "{END-IF}"}),
    ]
    resolved = _resolve_blocks(blocks)
    assert isinstance(resolved[2].control_payload, Else)


def test_unknown_macro_control_emits_warning_and_has_no_payload(caplog) -> None:
    blocks = [_block(0, Kind.MACRO_CONTROL, {"UTILITIES": '{DO-WHATEVER} "x"'})]
    with caplog.at_level(logging.WARNING):
        tree = build_scope_tree(blocks)
        resolved = resolve_macros(blocks, tree)
        assert "unknown-macro-control" in caplog.text
        assert resolved[0].control_payload is None


def test_invalid_run_loop_chunk_size_coerces_to_zero() -> None:
    blocks = [
        _block(
            0,
            Kind.MACRO_CONTROL,
            {"UTILITIES": '{RUN-LOOP} "in.csv" "chunk.csv" "bad" "N"'},
        )
    ]
    resolved = _resolve_blocks(blocks)

    payload = resolved[0].control_payload
    assert isinstance(payload, RunLoop)
    assert payload.chunk_size == 0


def test_missing_quoted_args_use_defaults() -> None:
    blocks = [
        _block(0, Kind.MACRO_CONTROL, {"UTILITIES": "{START-MACRO}"}),
        _block(1, Kind.MACRO_CONTROL, {"UTILITIES": '{START-MACRO} "x.csv" "Y"'}),
    ]
    resolved = _resolve_blocks(blocks)

    first = resolved[0].control_payload
    second = resolved[1].control_payload
    assert isinstance(first, StartMacro)
    assert first.csv_path == ""
    assert first.prompt_off is False
    assert isinstance(second, StartMacro)
    assert second.csv_path == "x.csv"
    assert second.prompt_off is True


def test_actual_script_fixture_first_occurrence_payload_values(FIXTURES: Path) -> None:
    import re

    classified = parse_classify_fixture(FIXTURES, "actual_script.txt")
    tree = build_scope_tree(classified)
    resolved = resolve_macros(classified, tree)

    start_macro = blocks_for_token(resolved, "START-MACRO")[0].control_payload
    rows_in_file_block = next(b for b in resolved if b.kind is Kind.ROWS_IN_FILE)
    if_then = blocks_for_token(resolved, "IF-THEN")[0].control_payload

    assert isinstance(start_macro, StartMacro)
    assert start_macro.csv_path == "macrotmp.csv"

    rif_args = re.findall(
        r'"([^"]*)"', rows_in_file_block.resolved_options.lookup.get("UTILITIES", "")
    )
    assert rif_args[0] == "ICMPCS_config.csv"
    assert rif_args[1] == "CONFIG"

    assert isinstance(if_then, IfThen)
    assert if_then.lhs == "CONFIG"
    assert if_then.op == "LE"
    assert if_then.rhs == "0"


def test_scope_id_assigned_and_control_blocks_map_to_containing_scope(
    FIXTURES: Path,
) -> None:
    classified = parse_classify_fixture(FIXTURES, "actual_script.txt")
    tree = build_scope_tree(classified)
    resolved = resolve_macros(classified, tree)

    assert all(block.scope_id >= 0 for block in resolved)

    macro_control_indices = {
        block.index for block in classified if block.kind is Kind.MACRO_CONTROL
    }
    by_index = {block.index: block for block in resolved}
    for idx in macro_control_indices:
        expected_scope = _deepest_scope_containing(tree, idx)
        assert by_index[idx].scope_id == expected_scope


def _deepest_scope_containing(node, idx: int, best: int = 0) -> int:
    if node.start_index <= idx <= node.end_index:
        best = node.scope_id
        for child in node.children:
            best = _deepest_scope_containing(child, idx, best)
    return best
