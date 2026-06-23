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
    for fixture in FIXTURES.iterdir():
        for block in parse_vg2(fixture):
            assert block.options or block.body


def test_spans_are_within_file_bounds(FIXTURES) -> None:
    for fixture in FIXTURES.iterdir():
        total_lines = sum(1 for _ in fixture.open(encoding="utf-8", errors="replace"))
        for block in parse_vg2(fixture):
            assert 1 <= block.span.start_line <= block.span.end_line <= max(total_lines, 1)


def test_parser_is_deterministic(FIXTURES) -> None:
    for fixture in FIXTURES.iterdir():
        assert parse_vg2(fixture) == parse_vg2(fixture)
