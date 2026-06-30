from __future__ import annotations

from vg2c.frontend.models import (
    BlockOptions,
    ClassifiedBlock,
    Kind,
    ParsedBlock,
    SourceSpan,
)
from vg2c.resolver.scope_builder import build_scope_tree


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
    root, diagnostics = build_scope_tree([])
    assert root.kind == "program"
    assert root.children == ()
    assert diagnostics == []


def test_macro_pair_wraps_inner_leaves() -> None:
    blocks = [
        _block(0, Kind.MACRO_CONTROL, {"UTILITIES": '{START-MACRO} "a.csv" "N"'}),
        _block(1, Kind.UTILITY, {"UTILITIES": "run.bat"}),
        _block(2, Kind.WRITE_FILE, {"WRITE-FILE": "Y", "CSV": "out.csv"}),
        _block(3, Kind.SQLITE_QUERY, {"ENGINE": "SQLite"}),
        _block(4, Kind.MACRO_CONTROL, {"UTILITIES": "{END-MACRO}"}),
    ]
    root, diagnostics = build_scope_tree(blocks)

    assert diagnostics == []
    assert len(root.children) == 1
    macro = root.children[0]
    assert macro.kind == "macro"
    assert [c.block_index for c in macro.children] == [1, 2, 3]


def test_if_else_builds_if_node_with_branches() -> None:
    blocks = [
        _block(
            0, Kind.MACRO_CONTROL, {"UTILITIES": '{IF-THEN} "A" "EQS" "1" "" "" "" ""'}
        ),
        _block(1, Kind.UTILITY, {"UTILITIES": "left.bat"}),
        _block(2, Kind.MACRO_CONTROL, {"UTILITIES": "{ELSE}"}),
        _block(3, Kind.UTILITY, {"UTILITIES": "right.bat"}),
        _block(4, Kind.MACRO_CONTROL, {"UTILITIES": "{END-IF}"}),
    ]
    root, diagnostics = build_scope_tree(blocks)

    assert diagnostics == []
    assert len(root.children) == 1
    if_node = root.children[0]
    assert if_node.kind == "if"
    assert [branch.kind for branch in if_node.children] == ["if-branch", "else-branch"]


def test_unclosed_macro_emits_diagnostic() -> None:
    blocks = [
        _block(0, Kind.MACRO_CONTROL, {"UTILITIES": '{START-MACRO} "a.csv" "N"'}),
        _block(1, Kind.UTILITY, {"UTILITIES": "x.bat"}),
    ]
    _, diagnostics = build_scope_tree(blocks)
    assert any(d.code == "unclosed-macro" for d in diagnostics)


def test_orphan_end_macro_emits_diagnostic() -> None:
    blocks = [_block(0, Kind.MACRO_CONTROL, {"UTILITIES": "{END-MACRO}"})]
    _, diagnostics = build_scope_tree(blocks)
    assert any(d.code == "orphan-end-macro" for d in diagnostics)


def test_orphan_else_emits_diagnostic() -> None:
    blocks = [_block(0, Kind.MACRO_CONTROL, {"UTILITIES": "{ELSE}"})]
    _, diagnostics = build_scope_tree(blocks)
    assert any(d.code == "orphan-else" for d in diagnostics)


def test_rows_in_file_is_leaf_not_scope() -> None:
    blocks = [
        _block(
            0, Kind.MACRO_CONTROL, {"UTILITIES": '{ROWS-IN-FILE} "a.csv" "COUNT" "N"'}
        ),
        _block(1, Kind.UTILITY, {"UTILITIES": "after.bat"}),
    ]
    root, diagnostics = build_scope_tree(blocks)
    assert diagnostics == []
    assert [node.kind for node in root.children] == ["leaf", "leaf"]


def test_run_loop_pair_wraps_inner_leaves() -> None:
    blocks = [
        _block(
            0,
            Kind.MACRO_CONTROL,
            {"UTILITIES": '{RUN-LOOP} "in.csv" "chunk.csv" "100" "N"'},
        ),
        _block(1, Kind.SQL_QUERY, {"NODE": "MARS", "ENGINE": "VA"}),
        _block(2, Kind.UTILITY, {"UTILITIES": "append.bat"}),
        _block(3, Kind.MACRO_CONTROL, {"UTILITIES": "{END-LOOP}"}),
    ]
    root, diagnostics = build_scope_tree(blocks)

    assert diagnostics == []
    assert len(root.children) == 1
    loop = root.children[0]
    assert loop.kind == "loop"
    payload = loop.control_payload
    assert payload is not None
    assert payload.input_csv_path == "in.csv"
    assert payload.chunk_csv_path == "chunk.csv"
    assert payload.chunk_size == 100
    assert [c.block_index for c in loop.children] == [1, 2]


def test_unclosed_run_loop_emits_diagnostic() -> None:
    blocks = [
        _block(
            0,
            Kind.MACRO_CONTROL,
            {"UTILITIES": '{RUN-LOOP} "in.csv" "chunk.csv" "5" "N"'},
        ),
        _block(1, Kind.UTILITY, {"UTILITIES": "x.bat"}),
    ]
    _, diagnostics = build_scope_tree(blocks)
    assert any(d.code == "unclosed-loop" for d in diagnostics)


def test_orphan_end_loop_emits_diagnostic() -> None:
    blocks = [_block(0, Kind.MACRO_CONTROL, {"UTILITIES": "{END-LOOP}"})]
    _, diagnostics = build_scope_tree(blocks)
    assert any(d.code == "orphan-end-loop" for d in diagnostics)
