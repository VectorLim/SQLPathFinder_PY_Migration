from __future__ import annotations

import csv
from pathlib import Path
import re

from vg2c.dataflow import analyze
from vg2c.dispatch import dispatch
from vg2c.emitter import emit
from vg2c.emitter.utilities.html_report import HtmlReport
from vg2c.frontend import classify, parse
from vg2c.resolver import resolve


class MockCtx:
    def __init__(self):
        self.macro = MockMacro()
        self.write_calls: list[tuple[str, str]] = []

    def write_file(self, path: str, content: str) -> None:
        self.write_calls.append((path, content))
        p = Path(path)
        p.parent.mkdir(parents=True, exist_ok=True)
        p.write_text(content, encoding="utf-8")


class MockMacro:
    def __init__(self) -> None:
        self.write_calls: list[tuple[str, str]] = []

    def substitute_sql(self, val: str) -> str:
        return val

    def write_file(self, path: str, content: str) -> None:
        self.write_calls.append((path, content))
        p = Path(path)
        p.parent.mkdir(parents=True, exist_ok=True)
        p.write_text(content, encoding="utf-8")


class SpyCsvIO:
    def __init__(self) -> None:
        self.calls: list[str] = []

    def iter(self, name: str):
        self.calls.append(name)
        with Path(name).open(newline="", encoding="utf-8", errors="replace") as fh:
            reader = csv.DictReader(fh)
            for row in reader:
                yield row


class CtxWithCsvIO(MockCtx):
    def __init__(self) -> None:
        super().__init__()
        self.csv_io = SpyCsvIO()


def test_html_report_css_synthesis():
    report = HtmlReport()
    template = (
        "Type<\\>Key<\\>COL1\n"
        "TYPE<\\>CSS\n"
        "CSS<\\>test_style.css\n"
        "FORMAT<\\>Column-Headers<\\>background-color:#dbd9c0<\\>font-size:12<\\>font-weight:bold\n"
        "FORMAT<\\>COLUMN-BORDER<\\>border-color:#cc9<\\>border-width:1px\n"
    )
    report.run(template=template)

    assert report.css_file == "test_style.css"
    css_content = report._build_css()

    assert "background-color:#dbd9c0;" in css_content
    assert "font-size:12px;" in css_content  # auto-appends px
    assert "font-weight:bold;" in css_content
    assert "border-color:#cc9;" in css_content
    assert "border-width:1px;" in css_content
    assert "th, #colhdr" in css_content
    assert "table.tblin, td.tblin, th, td.alt" in css_content


def test_html_report_defer_and_render(tmp_path, monkeypatch):
    monkeypatch.chdir(tmp_path)

    csv_file = tmp_path / "data.csv"
    with csv_file.open("w", newline="", encoding="utf-8") as f:
        writer = csv.writer(f)
        writer.writerow(["col1", "ce%", "other"])
        writer.writerow(["Val1", "0.852", "Val2"])
        writer.writerow(["Val3", "0.91", ""])

    report = HtmlReport()
    report.defer(
        id="REPORT1",
        template=(
            "TYPE<\\>HTML\n"
            f"INPUT-FILE<\\>{csv_file}\n"
            "COLUMN-DATA<\\><\\>col1<\\>ce%<\\>other\n"
            "COLUMN-HEADERS<\\><\\>Col 1<\\>CE Ratio<\\>Other Col\n"
            "COLUMN-ALIGNMENT<\\><\\>middle-left<\\>middle-left<\\>middle-left\n"
            "AT-TOP-OF-REPORT<\\><\\>Test Top Header\n"
        ),
    )

    ctx = MockCtx()
    rendered = report._render_report("REPORT1", ctx)

    assert "Test Top Header" in rendered
    assert '<table class="tblin">' in rendered
    assert "<th>Col 1</th>" in rendered
    assert "<th>CE Ratio</th>" in rendered
    assert "<th>Other Col</th>" in rendered
    assert "Val1" in rendered
    assert "85.20%" in rendered  # formats as percentage
    assert "91.00%" in rendered
    assert "&nbsp;" in rendered  # formats empty string

    # Check alternating row classes
    assert (
        '<td class="tblin" style="vertical-align:middle;text-align:left;">Val1</td>'
        in rendered
    )
    assert (
        '<td class="alt" style="vertical-align:middle;text-align:left;">Val3</td>'
        in rendered
    )


def test_html_report_layout_link(tmp_path, monkeypatch):
    monkeypatch.chdir(tmp_path)

    report = HtmlReport()
    report.css_file = "style.css"

    template = (
        ":FILE:out.html\n"
        ":CSS:style.css\n"
        ":CSSEMBED:N\n"
        ":TITLE:Layout Title\n"
        "<h1>Hello World</h1>\n"
        "HTM:REPORT1\n"
    )

    # Defer an empty report for replacement
    report.deferred_reports["REPORT1"] = {
        "template": "INPUT-FILE<\\>nonexistent.csv\nCOLUMN-DATA<\\><\\>col1\n"
    }

    ctx = MockCtx()
    report.layout(ctx, template)

    output_file = tmp_path / "out.html"
    assert output_file.exists()

    html_content = output_file.read_text(encoding="utf-8")
    assert "<title>Layout Title</title>" in html_content
    assert '<link rel="stylesheet" type="text/css" href="style.css" />' in html_content
    assert "<h1>Hello World</h1>" in html_content
    assert '<table class="tblin">' in html_content


def test_html_report_delete_clears_state():
    report = HtmlReport()
    report.styles["some-format"] = ["style1"]
    report.css_file = "test.css"
    report.deferred_reports["r1"] = {}

    report.delete()

    assert not report.styles
    assert report.css_file is None
    assert not report.deferred_reports


def test_html_report_layout_email_fallback(tmp_path, monkeypatch):
    monkeypatch.chdir(tmp_path)

    report = HtmlReport()
    report.instance = "9999"

    template = ":FILE:email:self\n" "HTM:REPORT1\n"

    report.deferred_reports["REPORT1"] = {
        "template": "OUTPUT-FILE<\\>my_output.htm\nINPUT-FILE<\\>nonexistent.csv\nCOLUMN-DATA<\\><\\>col1\n"
    }

    ctx = MockCtx()
    report.layout(ctx, template)

    output_file = tmp_path / "9999_my_output.htm"
    assert output_file.exists()


def test_html_report_fallback_output_uses_cached_parsed_payload(tmp_path, monkeypatch):
    monkeypatch.chdir(tmp_path)

    report = HtmlReport()
    report.instance = "1001"
    report.defer(
        id="R1",
        template="OUTPUT-FILE<\\>CaseSensitive.HTM\nINPUT-FILE<\\>missing.csv\nCOLUMN-DATA<\\><\\>col1\n",
    )

    # If layout re-parses template text directly, fallback would become report.html.
    report.deferred_reports["R1"]["template"] = ""

    ctx = MockCtx()
    report.layout(ctx, ":FILE:email:self\nHTM:R1\n")

    assert (tmp_path / "1001_casesensitive.htm").exists()


def test_html_report_render_uses_ctx_csv_io_iter(tmp_path, monkeypatch):
    monkeypatch.chdir(tmp_path)

    csv_file = tmp_path / "rows.csv"
    with csv_file.open("w", newline="", encoding="utf-8") as fh:
        writer = csv.writer(fh)
        writer.writerow(["col1", "col2"])
        writer.writerow(["A", "B"])

    report = HtmlReport()
    report.defer(
        id="RCSV",
        template=(
            f"INPUT-FILE<\\>{csv_file}\n"
            "COLUMN-DATA<\\><\\>col1<\\>col2\n"
            "COLUMN-HEADERS<\\><\\>C1<\\>C2\n"
        ),
    )

    ctx = CtxWithCsvIO()
    rendered = report._render_report("RCSV", ctx)

    assert ctx.csv_io.calls == [str(csv_file)]
    assert "<th>C1</th>" in rendered
    assert "<th>C2</th>" in rendered
    assert "A" in rendered
    assert "B" in rendered


def test_html_report_layout_prefers_ctx_write_file(tmp_path, monkeypatch):
    monkeypatch.chdir(tmp_path)

    report = HtmlReport()
    report.deferred_reports["REPORT1"] = {
        "template": "INPUT-FILE<\\>missing.csv\nCOLUMN-DATA<\\><\\>col1\n"
    }

    ctx = MockCtx()
    report.layout(ctx, ":FILE:out.html\nHTM:REPORT1\n")

    assert ctx.write_calls
    assert not ctx.macro.write_calls
    assert (tmp_path / "out.html").exists()


def test_html_report_layout_ignores_unknown_directives(tmp_path, monkeypatch):
    monkeypatch.chdir(tmp_path)

    report = HtmlReport()
    ctx = MockCtx()
    report.layout(
        ctx,
        ":FILE:unknown.html\n:TITLE:Hello\n:FUTURE-FLAG:keep\n<div>Body</div>\n",
    )

    html = (tmp_path / "unknown.html").read_text(encoding="utf-8")
    assert "<title>Hello</title>" in html
    assert "<div>Body</div>" in html


def test_html_report_fixture_flow_parity_order():
    fixture = Path(__file__).parent.parent / "fixtures" / "html_test.txt"
    text = fixture.read_text(encoding="utf-8", errors="replace")
    parsed, parse_diag = parse(text, source=fixture)
    classified, classify_diag = classify(parsed)
    resolved = resolve(classified, diagnostics=[*parse_diag, *classify_diag])
    analyzed = analyze(resolved)
    dispatched = dispatch(analyzed)
    source = emit(dispatched).source

    methods = re.findall(r"ctx\.html_report\.(defer|run|layout|delete)\(", source)
    assert methods == ["defer", "defer", "run", "layout", "delete"]
    assert "ctx.html_report.layout(ctx," in source
