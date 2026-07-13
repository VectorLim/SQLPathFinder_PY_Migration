from __future__ import annotations

from pathlib import Path
from vg2c.frontend import parse


def _codes(diagnostics: list) -> list[str]:
    return [d.code for d in diagnostics]


def test_splits_two_blocks_on_separator() -> None:
    text = """<OPTIONS>\n/A=1\n</OPTIONS>\nbody1\n<---- New Query ---->\n<OPTIONS>\n/B=2\n</OPTIONS>\nbody2\n"""
    blocks, diagnostics = parse(text)

    assert len(blocks) == 2
    assert blocks[0].options.lookup["A"] == "1"
    assert blocks[1].options.lookup["B"] == "2"
    assert not diagnostics


def test_separator_allows_surrounding_whitespace() -> None:
    text = """<OPTIONS>\n/A=1\n</OPTIONS>\nleft\n   <----   New Query   ---->   \n<OPTIONS>\n/B=2\n</OPTIONS>\nright\n"""
    blocks, _ = parse(text)

    assert len(blocks) == 2
    assert blocks[0].body == "left"
    assert blocks[1].body == "right"


def test_parses_explicit_options_and_preserves_order() -> None:
    text = """<OPTIONS>\n/B=2\n/A=1\n</OPTIONS>\nbody\n"""
    blocks, diagnostics = parse(text)

    assert len(blocks) == 1
    assert blocks[0].options.pairs == (("B", "2"), ("A", "1"))
    assert not diagnostics


def test_parses_inline_options_when_markers_missing() -> None:
    text = "/A=1\n/B=2\nbody line\n"
    blocks, diagnostics = parse(text)

    assert len(blocks) == 1
    assert blocks[0].options.lookup["A"] == "1"
    assert blocks[0].options.lookup["B"] == "2"
    assert blocks[0].body == "body line"
    assert "inline-options" in _codes(diagnostics)


def test_duplicate_keys_preserved_with_last_lookup_and_diagnostic() -> None:
    text = """<OPTIONS>\n/TABLE=one.csv\n/TABLE=two.csv\n</OPTIONS>\n"""
    blocks, diagnostics = parse(text)

    assert blocks[0].options.pairs == (("TABLE", "one.csv"), ("TABLE", "two.csv"))
    assert blocks[0].options.lookup["TABLE"] == "two.csv"
    assert _codes(diagnostics).count("duplicate-option-key") == 1


def test_unclosed_options_emits_error_with_best_effort_block() -> None:
    text = "<OPTIONS>\n/A=1\n/B=2\n"
    blocks, diagnostics = parse(text)

    assert len(blocks) == 1
    assert blocks[0].options.lookup["A"] == "1"
    assert blocks[0].body == ""
    assert "unclosed-options" in _codes(diagnostics)


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
    blocks, _ = parse(text, source=Path("fixture.txt"))

    assert blocks[0].span.file == Path("fixture.txt")
    assert blocks[0].span.start_line == 1
    assert blocks[0].span.end_line == 5
    assert blocks[1].span.start_line == 6
    assert blocks[1].span.end_line == 10
    assert blocks[1].span.start_line > blocks[0].span.end_line


def test_fixture_driven_parser_behavior(FIXTURES: Path) -> None:
    # 1. script_short.txt: 1 block, key options, no errors
    short_text = (FIXTURES / "script_short.txt").read_text(encoding="utf-8", errors="replace")
    blocks, diags = parse(short_text, source=FIXTURES / "script_short.txt")
    assert len(blocks) == 1
    assert blocks[0].options.lookup.get("CSV") == "owner.csv"
    assert blocks[0].options.lookup.get("HEADERS") == "owner"
    assert not [d for d in diags if d.severity == "error"]

    # 2. script_another.txt: 3 blocks
    another_text = (FIXTURES / "script_another.txt").read_text(encoding="utf-8", errors="replace")
    blocks, _ = parse(another_text, source=FIXTURES / "script_another.txt")
    assert len(blocks) == 3

    # 3. sql_script.txt: 3 blocks
    sql_text = (FIXTURES / "sql_script.txt").read_text(encoding="utf-8", errors="replace")
    blocks, _ = parse(sql_text, source=FIXTURES / "sql_script.txt")
    assert len(blocks) == 3

    # 4. actual_script.txt: 60 blocks, leading-separator empty-block warning diagnostic
    actual_text = (FIXTURES / "actual_script.txt").read_text(encoding="utf-8", errors="replace")
    blocks, diags = parse(actual_text, source=FIXTURES / "actual_script.txt")
    assert len(blocks) == 60
    assert "empty-block" in _codes(diags)
    assert not [d for d in diags if d.severity == "error"]
