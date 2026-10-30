"""The historical HTML compiler parser is not another runtime renderer."""

import pytest

from vg2c import compile_document
from vg2c.utilities.html_report import HtmlReport

D = r"<\\>"


def block(options: str, body: str = "") -> str:
    return "<OPTIONS>\n" + options + "\n</OPTIONS>\n" + body + "\n<---- New Query ---->\n"


def test_html_row_parser_keeps_script_host_format_options():
    value = "\n".join([
        D.join(["TYPE", "HTML"]),
        D.join(["INPUT-FILE", "output.csv"]),
        D.join(["COLUMN-DATA", "", "LOT", "VALUE"]),
    ])
    options = HtmlReport.parse_options(value)
    assert options["TYPE"] == "HTML"
    assert options["INPUT-FILE"] == "output.csv"
    assert options["COLUMN-DATA"] == ["LOT", "VALUE"]


def test_html_direct_emission_preserves_editable_shell_and_metadata(tmp_path):
    definition = "\n".join([
        D.join(["TYPE", "HTML"]),
        D.join(["INPUT-FILE", "output.csv"]),
        D.join(["COLUMN-DATA", "", "LOT"]),
        D.join(["COLUMN-HEADERS", "", "Lot"]),
    ])
    source = tmp_path / "html.txt"
    source.write_text(
        block("/REPORT=HTML-DEFER\n/ID=R", definition) +
        block("/REPORT=HTML-LAYOUT", ":FILE:out.html\n<h1>Report</h1>\nHTM:R"),
        encoding="utf-8",
    )
    emitted = compile_document(source).emitted
    assert "job.reports['R']" in emitted.source
    assert "job.html(" in emitted.source
    assert any(p.endswith(".html") and "data-field" in text for p, text in emitted.assets)
    assert not any(p.endswith(".report.json") for p, _ in emitted.assets)
    assert [step.block_index for step in emitted.steps] == [0, 1]
    previous_end = 0
    for step, statement in zip(emitted.steps, ("job.reports['R']", "job.html("), strict=True):
        span = step.source_range
        assert previous_end <= span.start_offset < span.end_offset
        assert statement in emitted.source[span.start_offset:span.end_offset]
        previous_end = span.end_offset


def test_deferred_report_state_is_isolated_between_translations(tmp_path):
    definition = "\n".join([
        D.join(["TYPE", "HTML"]),
        D.join(["INPUT-FILE", "output.csv"]),
        D.join(["COLUMN-DATA", "", "LOT"]),
    ])
    first = tmp_path / "first.txt"
    first.write_text(
        block("/REPORT=HTML-DEFER\n/ID=R", definition)
        + block("/REPORT=HTML-LAYOUT", ":FILE:first.html\nHTM:R"),
        encoding="utf-8",
    )
    second = tmp_path / "second.txt"
    second.write_text(
        block("/REPORT=HTML-LAYOUT", ":FILE:second.html\nHTM:R"),
        encoding="utf-8",
    )

    compile_document(first)
    with pytest.raises(ValueError, match="Unknown deferred report 'R'"):
        compile_document(second)
