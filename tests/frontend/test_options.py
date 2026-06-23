from __future__ import annotations

from vg2c.frontend.options import parse_options


def test_long_form_one_per_line() -> None:
    header = "/REPORT=HTML-RUN\n/INSTANCE=15507\n/PROMPT-TEXT=Step 1-1"
    options = parse_options(header)
    assert options == {
        "REPORT": "HTML-RUN",
        "INSTANCE": "15507",
        "PROMPT-TEXT": "Step 1-1",
    }


def test_inline_form_space_separated() -> None:
    header = "/REPORT=HTML-RUN /INSTANCE=15507 /PROMPT-TEXT=Step 1-1"
    options = parse_options(header)
    assert options == {
        "REPORT": "HTML-RUN",
        "INSTANCE": "15507",
        "PROMPT-TEXT": "Step 1-1",
    }


def test_empty_value_after_equals() -> None:
    assert parse_options("/UN=") == {"UN": ""}


def test_value_with_quoted_positional_args_round_trips() -> None:
    header = '/UTILITIES=foo.va "lich.py" "" "N"'
    options = parse_options(header)
    assert options["UTILITIES"] == 'foo.va "lich.py" "" "N"'


def test_value_with_embedded_slash_in_unc_path() -> None:
    header = r"/CSV=\\server\share\file.csv"
    options = parse_options(header)
    assert options["CSV"] == r"\\server\share\file.csv"


def test_options_tags_are_stripped() -> None:
    wrapped = "<OPTIONS>\n/REPORT=HTML-RUN\n/INSTANCE=15507\n</OPTIONS>"
    plain = "/REPORT=HTML-RUN\n/INSTANCE=15507"
    assert parse_options(wrapped) == parse_options(plain)


def test_duplicate_keys_last_wins() -> None:
    header = "/REPORT=A /REPORT=B"
    options = parse_options(header)
    assert options["REPORT"] == "B"
