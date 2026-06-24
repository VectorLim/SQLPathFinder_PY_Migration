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


def test_preserves_sql_body_verbatim() -> None:
    body = "/*BEGIN SQL*/\nSELECT\n    a,\n    b\nFROM t\nORDER BY\n    1\n/*END SQL*/\n"
    text = f"<OPTIONS>\n/OLEDB=SQLite\n</OPTIONS>\n{body}"
    blocks, _ = parse(text)

    assert blocks[0].body == body.rstrip("\n")


def test_preserves_python_body_verbatim() -> None:
    body = "def run():\n    x = 1\n    if x:\n        print(x)\n"
    text = f"<OPTIONS>\n/WRITE-FILE=Y\n/CSV=script.py\n</OPTIONS>\n{body}"
    blocks, _ = parse(text)

    assert blocks[0].body == body.rstrip("\n")


def test_preserves_csv_and_html_bodies_verbatim() -> None:
    csv_body = "col1,col2\n1,2\n"
    html_body = "<html>\n  <body>ok</body>\n</html>\n"
    text = (
        "<OPTIONS>\n/WRITE-FILE=Y\n/CSV=data.csv\n</OPTIONS>\n"
        + csv_body
        + "<---- New Query ---->\n"
        + "<OPTIONS>\n/WRITE-FILE=Y\n/CSV=page.htm\n</OPTIONS>\n"
        + html_body
    )
    blocks, _ = parse(text)

    assert blocks[0].body == csv_body.rstrip("\n")
    assert blocks[1].body == html_body.rstrip("\n")


def test_quoted_utility_value_kept_verbatim() -> None:
    value = '@EXEDIR@\\Run_Python_Script.va "lich.py" "" "N" "atd_atm.hadoop" "Python-v3"'
    text = f"<OPTIONS>\n/UTILITIES={value}\n</OPTIONS>\n"
    blocks, _ = parse(text)

    assert blocks[0].options.lookup["UTILITIES"] == value


def test_duplicate_keys_preserved_with_last_lookup_and_diagnostic() -> None:
    text = """<OPTIONS>\n/TABLE=one.csv\n/TABLE=two.csv\n</OPTIONS>\n"""
    blocks, diagnostics = parse(text)

    assert blocks[0].options.pairs == (("TABLE", "one.csv"), ("TABLE", "two.csv"))
    assert blocks[0].options.lookup["TABLE"] == "two.csv"
    assert _codes(diagnostics).count("duplicate-option-key") == 1


def test_empty_block_between_separators_emits_warning_and_skips() -> None:
    text = (
        "<OPTIONS>\n/A=1\n</OPTIONS>\nfirst\n"
        "<---- New Query ---->\n"
        "   \n"
        "<---- New Query ---->\n"
        "<OPTIONS>\n/B=2\n</OPTIONS>\nsecond\n"
    )
    blocks, diagnostics = parse(text)

    assert len(blocks) == 2
    assert "empty-block" in _codes(diagnostics)


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


def test_macro_placeholders_are_not_interpreted() -> None:
    value = "setsiteparam.exe KM <<<SFOLDER>>> <<<UNDERDEV>>> <<>>"
    text = f"<OPTIONS>\n/UTILITIES={value}\n</OPTIONS>\n"
    blocks, _ = parse(text)

    assert blocks[0].options.lookup["UTILITIES"] == value


def test_unc_path_is_preserved_verbatim() -> None:
    value = r'@EXEDIR@\SPFCopy.bat "\\AZATSHFS.intel.com\AZATAnalysis$\MAOATM\Config\input.csv" ".\\" "N"'
    text = f"<OPTIONS>\n/UTILITIES={value}\n</OPTIONS>\n"
    blocks, _ = parse(text)

    assert blocks[0].options.lookup["UTILITIES"] == value
