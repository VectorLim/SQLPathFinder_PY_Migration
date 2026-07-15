from __future__ import annotations

import logging
import pytest

from vg2c.frontend.models import (
    BlockOptions,
    ClassifiedBlock,
    ParsedBlock,
    SourceSpan,
)
from vg2c.kind import Kind
from vg2c.resolver.scope_builder import build_scope_tree

from tests.resolver._fixture_flow import (
    all_scope_nodes,
    max_scope_depth,
    parse_classify_fixture,
)


def _block(
    index: int, kind: Kind, options: dict[str, str] | None = None
) -> ClassifiedBlock:
    opts = BlockOptions.from_pairs((options or {}).items())
    parsed = ParsedBlock(
        index=index,
        options=opts,
        body="",
        raw="",
        span=SourceSpan(file=None, start_line=index + 1, end_line=index + 1),
    )
    return ClassifiedBlock(parsed=parsed, kind=kind, reason="test")


def test_empty_program() -> None:
    root = build_scope_tree([])
    assert root.kind == "program"
    assert root.children == ()


def test_structural_happy_path_nested_scopes() -> None:
    blocks = [
        _block(0, Kind.MACRO_CONTROL, {"UTILITIES": '{START-MACRO} "outer.csv" "N"'}),
        _block(
            1,
            Kind.MACRO_CONTROL,
            {"UTILITIES": '{IF-THEN} "A" "EQS" "1" "" "" "" ""'},
        ),
        _block(
            2,
            Kind.MACRO_CONTROL,
            {"UTILITIES": '{RUN-LOOP} "in.csv" "chunk.csv" "25" "N"'},
        ),
        _block(3, Kind.EXTERNAL_RUN, {"UTILITIES": "inside_loop.bat"}),
        _block(4, Kind.MACRO_CONTROL, {"UTILITIES": "{END-LOOP}"}),
        _block(5, Kind.MACRO_CONTROL, {"UTILITIES": "{ELSE}"}),
        _block(6, Kind.WRITE_FILE, {"WRITE-FILE": "Y", "CSV": "branch.txt"}),
        _block(7, Kind.MACRO_CONTROL, {"UTILITIES": "{END-IF}"}),
        _block(8, Kind.MACRO_CONTROL, {"UTILITIES": "{END-MACRO}"}),
    ]
    root = build_scope_tree(blocks)

    assert len(root.children) == 1
    macro = root.children[0]
    assert macro.kind == "macro"
    assert len(macro.children) == 1
    if_node = macro.children[0]
    assert if_node.kind == "if"
    assert [branch.kind for branch in if_node.children] == ["if-branch", "else-branch"]

    loop = if_node.children[0].children[0]
    assert loop.kind == "loop"
    assert [child.block_index for child in loop.children] == [3]


@pytest.mark.parametrize(
    ("utilities", "expected_code"),
    [
        ("{END-MACRO}", "orphan-end-macro"),
        ("{END-LOOP}", "orphan-end-loop"),
        ("{END-IF}", "orphan-end-if"),
        ("{ELSE}", "orphan-else"),
    ],
)
def test_orphan_structural_tokens_emit_errors(
    utilities: str,
    expected_code: str,
    caplog,
) -> None:
    blocks = [_block(0, Kind.MACRO_CONTROL, {"UTILITIES": utilities})]
    with caplog.at_level(logging.ERROR):
        build_scope_tree(blocks)
        assert expected_code in caplog.text


@pytest.mark.parametrize(
    ("utilities", "expected_code"),
    [
        ('{START-MACRO} "a.csv" "N"', "unclosed-macro"),
        ('{RUN-LOOP} "in.csv" "chunk.csv" "2" "N"', "unclosed-loop"),
        ('{IF-THEN} "A" "EQS" "1" "" "" "" "', "unclosed-if"),
    ],
)
def test_unclosed_openers_emit_errors(
    utilities: str,
    expected_code: str,
    caplog,
) -> None:
    blocks = [
        _block(0, Kind.MACRO_CONTROL, {"UTILITIES": utilities}),
        _block(1, Kind.EXTERNAL_RUN, {"UTILITIES": "x.bat"}),
    ]
    with caplog.at_level(logging.ERROR):
        build_scope_tree(blocks)
        assert expected_code in caplog.text


def test_rows_in_file_stays_leaf() -> None:
    blocks = [
        _block(0, Kind.ROWS_IN_FILE, {"UTILITIES": '{ROWS-IN-FILE} "a.csv" "COUNT" "N"'}),
        _block(1, Kind.EXTERNAL_RUN, {"UTILITIES": "after.bat"}),
    ]
    root = build_scope_tree(blocks)
    assert [node.kind for node in root.children] == ["leaf", "leaf"]


def test_run_loop_parses_payload_and_wraps_inner_leaf() -> None:
    blocks = [
        _block(
            0,
            Kind.MACRO_CONTROL,
            {"UTILITIES": '{RUN-LOOP} "in.csv" "chunk.csv" "100" "N"'},
        ),
        _block(1, Kind.SQL_QUERY, {"NODE": "MARS", "ENGINE": "VA"}),
        _block(2, Kind.EXTERNAL_RUN, {"UTILITIES": "append.bat"}),
        _block(3, Kind.MACRO_CONTROL, {"UTILITIES": "{END-LOOP}"}),
    ]
    root = build_scope_tree(blocks)

    assert len(root.children) == 1
    loop = root.children[0]
    assert loop.kind == "loop"
    payload = loop.control_payload
    assert payload is not None
    assert payload.input_csv_path == "in.csv"
    assert payload.chunk_csv_path == "chunk.csv"
    assert payload.chunk_size == 100
    assert [c.block_index for c in loop.children] == [1, 2]


def test_actual_script_fixture_scope_invariants(FIXTURES) -> None:
    classified = parse_classify_fixture(FIXTURES, "actual_script.txt")
    root = build_scope_tree(classified)

    nodes = list(all_scope_nodes(root))
    assert any(node.kind == "macro" for node in nodes)
    assert any(node.kind == "if" for node in nodes)
    assert max_scope_depth(root) >= 4
