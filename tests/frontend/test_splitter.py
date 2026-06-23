from __future__ import annotations

from vg2c.frontend.splitter import split_blocks


def test_zero_delimiters_yields_one_block() -> None:
    text = "SELECT 1\n"
    blocks = split_blocks(text, file="f.txt")
    assert len(blocks) == 1
    assert blocks[0].body_text == "SELECT 1\n"


def test_trailing_delimiter_does_not_emit_empty_block() -> None:
    text = "SELECT 1\n<---- New Query ---->\n"
    blocks = split_blocks(text, file="f.txt")
    assert len(blocks) == 1


def test_inline_header_form_consumed_as_single_line() -> None:
    text = "/REPORT=HTML-RUN /INSTANCE=1\nSELECT 1\n"
    blocks = split_blocks(text, file="f.txt")
    assert len(blocks) == 1
    assert blocks[0].header_text == "/REPORT=HTML-RUN /INSTANCE=1\n"


def test_long_form_header_multiline() -> None:
    text = "<OPTIONS>\n/CSV=foo.csv\n</OPTIONS>\nSELECT 1"
    blocks = split_blocks(text, file="f.txt")
    assert len(blocks) == 1
    assert blocks[0].header_text == "<OPTIONS>\n/CSV=foo.csv\n</OPTIONS>\n"
    assert blocks[0].body_text in {"SELECT 1", "SELECT 1\n"}


def test_delimiter_substring_inside_body_is_not_split() -> None:
    text = '/WRITE-FILE=foo.sql\nSELECT "<---- New Query ---->" AS marker;\n'
    blocks = split_blocks(text, file="f.txt")
    assert len(blocks) == 1


def test_source_spans_are_contiguous_and_one_based() -> None:
    text = "A\n<---- New Query ---->\nB\n<---- New Query ---->\nC\n"
    blocks = split_blocks(text, file="f.txt")
    assert len(blocks) == 3
    prev_end = 0
    for block in blocks:
        assert block.span.start_line >= 1
        assert block.span.end_line >= block.span.start_line
        assert block.span.start_line > prev_end
        prev_end = block.span.end_line


def test_header_with_empty_body_is_kept() -> None:
    text = "<OPTIONS>\n/CSV=foo\n</OPTIONS>"
    blocks = split_blocks(text, file="f.txt")
    assert len(blocks) == 1
    assert blocks[0].header_text
    assert blocks[0].body_text == ""


def test_body_with_no_header_is_kept() -> None:
    text = "SELECT * FROM table_name\n"
    blocks = split_blocks(text, file="f.txt")
    assert len(blocks) == 1
    assert blocks[0].header_text == ""
    assert blocks[0].body_text == "SELECT * FROM table_name\n"
