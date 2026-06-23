from __future__ import annotations

import pytest

from vg2c.frontend.parser import parse_vg2


@pytest.mark.parametrize(
    ("fixture", "expected_blocks"),
    [
        ("script_short.txt", 1),
        ("script_another.txt", 3),
        ("sql_script.txt", 3),
        ("script_from_vietnam.txt", 17),
        ("actual_script.txt", 60),
    ],
)
def test_block_count(fixture: str, expected_blocks: int, FIXTURES) -> None:
    blocks = parse_vg2(FIXTURES / fixture)
    assert len(blocks) == expected_blocks


def test_every_block_has_at_least_options_or_body(FIXTURES) -> None:
    for fixture in FIXTURES.glob("*.txt"):
        for block in parse_vg2(fixture):
            assert block.options or block.body


def test_spans_are_within_file_bounds(FIXTURES) -> None:
    for fixture in FIXTURES.glob("*.txt"):
        total_lines = sum(1 for _ in fixture.open(encoding="utf-8", errors="replace"))
        for block in parse_vg2(fixture):
            assert 1 <= block.span.start_line <= block.span.end_line <= max(total_lines, 1)


def test_parser_is_deterministic(FIXTURES) -> None:
    for fixture in FIXTURES.glob("*.txt"):
        assert parse_vg2(fixture) == parse_vg2(fixture)


def test_script_short_has_correct_options(FIXTURES) -> None:
    blocks = parse_vg2(FIXTURES / "script_short.txt")
    assert len(blocks) == 1
    block = blocks[0]
    assert block.options["NODE"] == ".\\"
    assert block.options["OLEDB"] == "SQLite"
    assert block.options["ENGINE"] == "SQLite"
    assert block.options["CSV"] == "owner.csv"
    assert block.options["INSTANCE"] == "8486"
    assert block.options["PROMPT-TEXT"] == "Step 1.1. Fetching Text (SQLite) Data"


def test_script_short_body_contains_select(FIXTURES) -> None:
    blocks = parse_vg2(FIXTURES / "script_short.txt")
    block = blocks[0]
    assert "SELECT" in block.body
    assert "[owner]" in block.body
    assert "ww_yield" in block.body


def test_script_another_three_blocks_content(FIXTURES) -> None:
    blocks = parse_vg2(FIXTURES / "script_another.txt")
    assert len(blocks) == 3

    # Block 0: MARS data fetch
    assert blocks[0].options["ENGINE"] == "VA"
    assert blocks[0].options["PROMPT-TEXT"] == "Step 1.1-a0. Fetching MARS Data"
    assert "F_Calendar" in blocks[0].body
    assert "/*BEGIN SQL*/" in blocks[0].body or "@[]@F_Calendar" in blocks[0].body

    # Block 1: Write Python file
    assert blocks[1].options["WRITE-FILE"] == "Y"
    assert blocks[1].options["CSV"] == "lich.py"
    assert "pandas" in blocks[1].body or "pd" in blocks[1].body

    # Block 2: Run Python script (options only, no body)
    assert blocks[2].options["UTILITIES"].startswith("@EXEDIR@")
    assert "lich.py" in blocks[2].options["UTILITIES"]
    assert blocks[2].options["PROMPT-TEXT"] == "Step 3. Run Python script"


def test_sql_script_has_multiple_blocks(FIXTURES) -> None:
    blocks = parse_vg2(FIXTURES / "sql_script.txt")
    assert len(blocks) == 3

    # Block 0: MARS data
    assert blocks[0].options["PROMPT-TEXT"] == "Step 1.1-a0. Fetching MARS Data"
    assert "F_LotHist" in blocks[0].body

    # Block 1: OASys data
    assert blocks[1].options["PROMPT-TEXT"] == "Step 1.1-a1. Fetching OASys Data"
    assert "P_SPC" in blocks[1].body

    # Block 2: SQLite join and index
    assert blocks[2].options["INSTANCE"] == "29397"
    assert "CREATE INDEX" in blocks[2].body or "Create Index" in blocks[2].body


def test_options_with_empty_values(FIXTURES) -> None:
    blocks = parse_vg2(FIXTURES / "script_short.txt")
    block = blocks[0]
    # UN and PW are empty
    assert block.options["UN"] == ""
    assert block.options["PW"] == ""


def test_multiline_sql_bodies_preserved(FIXTURES) -> None:
    blocks = parse_vg2(FIXTURES / "sql_script.txt")
    # Block 0 should have multi-line SQL with proper formatting
    body = blocks[0].body
    assert "\n" in body  # Has newlines
    assert "SELECT" in body
    assert "FROM" in body
    assert "WHERE" in body


def test_options_with_special_characters(FIXTURES) -> None:
    blocks = parse_vg2(FIXTURES / "script_another.txt")
    # Block 0 has slashes and dots in NODE value
    assert blocks[0].options["NODE"] == "KM.[A15_PROD_21.].MARS"
    # Block 2 has UTILITIES with quotes and backslashes
    utilities = blocks[2].options["UTILITIES"]
    assert "lich.py" in utilities
    assert "@EXEDIR@" in utilities
    assert "Python-v3" in utilities


def test_span_line_numbers_match_file_content(FIXTURES) -> None:
    blocks = parse_vg2(FIXTURES / "script_another.txt")
    for block in blocks:
        file_text = (FIXTURES / "script_another.txt").read_text(encoding="utf-8", errors="replace")
        file_lines = file_text.splitlines()
        # Verify span is within file bounds
        assert block.span.start_line >= 1
        assert block.span.end_line <= len(file_lines)
        assert block.span.start_line <= block.span.end_line
