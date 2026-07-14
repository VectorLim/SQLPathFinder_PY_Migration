from __future__ import annotations

import logging
from pathlib import Path
from vg2c.frontend import parse


def test_splits_two_blocks_on_separator() -> None:
    text = """<OPTIONS>\n/A=1\n</OPTIONS>\nbody1\n<---- New Query ---->\n<OPTIONS>\n/B=2\n</OPTIONS>\nbody2\n"""
    blocks = parse(text)

    assert len(blocks) == 2
    assert blocks[0].options.lookup["A"] == "1"
    assert blocks[1].options.lookup["B"] == "2"


def test_separator_allows_surrounding_whitespace() -> None:
    text = """<OPTIONS>\n/A=1\n</OPTIONS>\nleft\n   <----   New Query   ---->   \n<OPTIONS>\n/B=2\n</OPTIONS>\nright\n"""
    blocks = parse(text)

    assert len(blocks) == 2
    assert blocks[0].body == "left"
    assert blocks[1].body == "right"


def test_parses_explicit_options_and_preserves_order() -> None:
    text = """<OPTIONS>\n/B=2\n/A=1\n</OPTIONS>\nbody\n"""
    blocks = parse(text)

    assert len(blocks) == 1
    assert blocks[0].options.pairs == (("B", "2"), ("A", "1"))


def test_parses_inline_options_when_markers_missing(caplog) -> None:
    text = "/A=1\n/B=2\nbody line\n"
    with caplog.at_level(logging.INFO):
        blocks = parse(text)

    assert len(blocks) == 1
    assert blocks[0].options.lookup["A"] == "1"
    assert blocks[0].options.lookup["B"] == "2"
    assert blocks[0].body == "body line"
    assert "inline-options" in caplog.text


def test_duplicate_keys_preserved_with_last_lookup_and_diagnostic(caplog) -> None:
    text = """<OPTIONS>\n/TABLE=one.csv\n/TABLE=two.csv\n</OPTIONS>\n"""
    with caplog.at_level(logging.INFO):
        blocks = parse(text)

    assert blocks[0].options.pairs == (("TABLE", "one.csv"), ("TABLE", "two.csv"))
    assert blocks[0].options.lookup["TABLE"] == "two.csv"
    assert "duplicate-option-key" in caplog.text


def test_unclosed_options_emits_error_with_best_effort_block(caplog) -> None:
    text = "<OPTIONS>\n/A=1\n/B=2\n"
    with caplog.at_level(logging.ERROR):
        blocks = parse(text)

    assert len(blocks) == 1
    assert blocks[0].options.lookup["A"] == "1"
    assert blocks[0].body == ""
    assert "unclosed-options" in caplog.text


def test_source_spans_track_absolute_line_numbers() -> None:
    text = (
        "<OPTIONS>\n"
        "/A=1\n"
        "</OPTIONS>\n"
        "first\n"
        "<---- New Query ---->\n"
        "<OPTIONS>\n"
        "/B=2\n"
        "</OPTIONS>\n"
        "second\n"
    )
    blocks = parse(text, source=Path("fixture.txt"))

    assert blocks[0].span.file == Path("fixture.txt")
    assert blocks[0].span.start_line == 1
    assert blocks[0].span.end_line == 5
    assert blocks[1].span.start_line == 6
    assert blocks[1].span.end_line == 10
    assert blocks[1].span.start_line > blocks[0].span.end_line


def test_fixture_driven_parser_behavior(FIXTURES: Path, caplog) -> None:
    # 1. script_short.txt: 1 block, key options, no errors
    short_text = (FIXTURES / "script_short.txt").read_text(encoding="utf-8", errors="replace")
    with caplog.at_level(logging.ERROR):
        blocks = parse(short_text, source=FIXTURES / "script_short.txt")
    assert len(blocks) == 1
    assert blocks[0].options.lookup.get("CSV") == "owner.csv"
    assert blocks[0].options.lookup.get("HEADERS") == "owner"
    assert not [r for r in caplog.records if r.levelno >= logging.ERROR]

    # 2. script_another.txt: 3 blocks
    another_text = (FIXTURES / "script_another.txt").read_text(encoding="utf-8", errors="replace")
    blocks = parse(another_text, source=FIXTURES / "script_another.txt")
    assert len(blocks) == 3

    # 3. sql_script.txt: 3 blocks
    sql_text = (FIXTURES / "sql_script.txt").read_text(encoding="utf-8", errors="replace")
    blocks = parse(sql_text, source=FIXTURES / "sql_script.txt")
    assert len(blocks) == 3

    # 4. actual_script.txt: 60 blocks, leading-separator empty-block warning diagnostic
    actual_text = (FIXTURES / "actual_script.txt").read_text(encoding="utf-8", errors="replace")
    caplog.clear()
    with caplog.at_level(logging.WARNING):
        blocks = parse(actual_text, source=FIXTURES / "actual_script.txt")
    assert len(blocks) == 60
    assert "empty-block" in caplog.text
    assert not [r for r in caplog.records if r.levelno >= logging.ERROR]
