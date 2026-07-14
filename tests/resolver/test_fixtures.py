from __future__ import annotations

from pathlib import Path

from vg2c.kind import Kind

from tests.resolver._fixture_flow import (
    all_scope_nodes,
    blocks_for_token,
    diagnostics_by_code,
    max_scope_depth,
    resolve_fixture,
)


def test_script_short_exact_single_leaf_contract(FIXTURES: Path) -> None:
    program = resolve_fixture(FIXTURES, "script_short.txt")

    assert len(program.blocks) == 1
    only_block = program.blocks[0]
    assert only_block.scope_id >= 0
    assert only_block.control_payload is None

    root = program.scope_tree
    assert root.kind == "program"
    assert len(root.children) == 1
    assert root.children[0].kind == "leaf"

    resolver_error_codes = {
        "orphan-end-macro",
        "orphan-end-loop",
        "orphan-end-if",
        "orphan-else",
        "unclosed-macro",
        "unclosed-loop",
        "unclosed-if",
    }
    assert not [
        diag for diag in program.diagnostics if diag.code in resolver_error_codes
    ]


def test_script_another_no_macro_scope_and_no_control_payloads(FIXTURES: Path) -> None:
    program = resolve_fixture(FIXTURES, "script_another.txt")
    nodes = list(all_scope_nodes(program.scope_tree))

    assert not [node for node in nodes if node.kind in {"macro", "if", "loop"}]
    assert all(block.control_payload is None for block in program.blocks)
    assert not diagnostics_by_code(program.diagnostics, "unknown-macro-control")


def test_sql_script_preserves_sql_and_has_no_resolver_sql_expansion(
    FIXTURES: Path,
) -> None:
    program = resolve_fixture(FIXTURES, "sql_script.txt")

    sql_blocks = [
        block
        for block in program.blocks
        if block.kind in {Kind.SQL_QUERY, Kind.SQLITE_QUERY}
    ]
    assert sql_blocks
    assert any("SQL_Get_CSV_List" in block.resolved_body for block in sql_blocks)
    # ResolvedBlocks no longer contain SQL macro/CSV-generation call slots.
    assert all(block.control_payload is None for block in sql_blocks)


def test_actual_script_scope_and_macro_signals(FIXTURES: Path) -> None:
    program = resolve_fixture(FIXTURES, "actual_script.txt")
    nodes = list(all_scope_nodes(program.scope_tree))
    macro_nodes = [n for n in nodes if n.kind == "macro"]
    if_nodes = [n for n in nodes if n.kind == "if"]

    assert len(macro_nodes) >= 2
    assert len(if_nodes) >= 5
    assert max_scope_depth(program.scope_tree) >= 4

    start_macro_blocks = blocks_for_token(program.blocks, "START-MACRO")
    rows_in_file_blocks = blocks_for_token(program.blocks, "ROWS-IN-FILE")
    if_then_blocks = blocks_for_token(program.blocks, "IF-THEN")
    else_blocks = blocks_for_token(program.blocks, "ELSE")

    assert start_macro_blocks
    assert rows_in_file_blocks
    assert if_then_blocks
    assert else_blocks

    assert start_macro_blocks[0].control_payload is not None
    assert rows_in_file_blocks[0].control_payload is not None
    assert if_then_blocks[0].control_payload is not None
    assert else_blocks[0].control_payload is not None

    disallowed_codes = {
        "orphan-end-macro",
        "orphan-end-loop",
        "orphan-end-if",
        "orphan-else",
        "unclosed-macro",
        "unclosed-loop",
        "unclosed-if",
    }
    assert not [diag for diag in program.diagnostics if diag.code in disallowed_codes]
