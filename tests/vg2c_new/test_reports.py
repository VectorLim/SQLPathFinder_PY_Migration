from __future__ import annotations

from pathlib import Path
from unittest.mock import Mock

import pytest

from vg2c_new.model import Command, CommandKind, SourceSpan
from vg2c_new.parser import RESOLVER_MANIFEST, parse
from vg2c_new.runtime import Interpreter, RuntimeState
from vg2c_new.utilities.files import WriteFileUtility
from vg2c_new.utilities.report import (
    HtmlDeferUtility,
    HtmlDeleteUtility,
    HtmlGnuPlotUtility,
    HtmlJsUtility,
    HtmlLayoutUtility,
    HtmlPyPlotUtility,
    HtmlRPlotUtility,
    HtmlRunUtility,
    HtmlTabMenuLayoutUtility,
)

DLM = "<\\\\>"
QUERY_DLM = "<---- New Query ---->"


def command(
    target: str,
    *,
    body: str = "",
    options: tuple[tuple[str, str], ...] = (),
) -> Command:
    return Command(
        1,
        CommandKind.UTILITY,
        target,
        target,
        options,
        body,
        "",
        (),
        SourceSpan(None, 1, 1),
    )


def option_rows(*rows: tuple[str, ...]) -> str:
    return "\n".join(DLM.join(row) for row in rows)


def test_html_run_generates_css_and_standalone_report(tmp_path: Path) -> None:
    runtime = RuntimeState(tmp_path)
    css = option_rows(
        ("Type", "Key", "COL1"),
        ("TYPE", "CSS"),
        ("CSS", "style.css"),
        ("FORMAT", "Column-Headers", "font-size:12", "font-weight:bold"),
        ("FORMAT", "COLUMN-BORDER", "border-width:1px"),
    )
    HtmlRunUtility().apply(command("report.html_run", body=css), runtime)

    css_text = (tmp_path / "style.css").read_text(encoding="utf-8")
    assert "font-size:12px;" in css_text
    assert "font-weight:bold;" in css_text
    assert "table.tblin, td.tblin, th, td.alt" in css_text

    (tmp_path / "rows.csv").write_text("name,percent\nA,0.5\nB,\n", encoding="utf-8")
    report = option_rows(
        ("TYPE", "HTML"),
        ("INPUT-FILE", "rows.csv"),
        ("OUTPUT-FILE", "standalone.html"),
        ("CSS", "style.css"),
        ("AT-TOP-OF-REPORT", "", "Standalone"),
        ("COLUMN-DATA", "", "name", "percent"),
        ("COLUMN-HEADERS", "", "Name", "Percent"),
    )
    HtmlRunUtility().apply(command("report.html_run", body=report), runtime)

    html = (tmp_path / "standalone.html").read_text(encoding="utf-8")
    assert "<strong" not in html
    assert "Standalone" in html
    assert "<th>Name</th>" in html
    assert "50.00%" in html
    assert "&nbsp;" in html


def test_html_run_missing_and_empty_inputs_are_visible(tmp_path: Path) -> None:
    utility = HtmlRunUtility()
    runtime = RuntimeState(tmp_path)

    with pytest.raises(ValueError, match="template is empty"):
        utility.apply(command("report.html_run"), runtime)

    missing = option_rows(
        ("TYPE", "HTML"),
        ("INPUT-FILE", "missing.csv"),
        ("OUTPUT-FILE", "out.html"),
        ("COLUMN-DATA", "", "x"),
    )
    with pytest.raises(FileNotFoundError, match="input file not found"):
        utility.apply(command("report.html_run", body=missing), runtime)

    (tmp_path / "empty.csv").write_text("x,y\n", encoding="utf-8")
    empty = option_rows(
        ("TYPE", "HTML"),
        ("INPUT-FILE", "empty.csv"),
        ("OUTPUT-FILE", "empty.html"),
        ("COLUMN-DATA", "", "x", "y"),
    )
    utility.apply(command("report.html_run", body=empty), runtime)
    html = (tmp_path / "empty.html").read_text(encoding="utf-8")
    assert "<table" in html
    assert "<tbody>" in html


def test_defer_layout_order_linux_paths_and_delete(tmp_path: Path) -> None:
    runtime = RuntimeState(tmp_path)
    defer = HtmlDeferUtility()
    layout = HtmlLayoutUtility()

    (tmp_path / "one.csv").write_text("name\nONE\n", encoding="utf-8")
    (tmp_path / "two.csv").write_text("name\nTWO\n", encoding="utf-8")
    one = option_rows(
        ("TYPE", "HTML"),
        ("INPUT-FILE", "one.csv"),
        ("COLUMN-DATA", "", "name"),
    )
    two = option_rows(
        ("TYPE", "HTML"),
        ("INPUT-FILE", "two.csv"),
        ("COLUMN-DATA", "", "name"),
    )
    defer.apply(
        command(
            "report.html_defer",
            body=one,
            options=(("INSTANCE", "100"), ("ID", "R1")),
        ),
        runtime,
    )
    defer.apply(
        command(
            "report.html_defer",
            body=two,
            options=(("INSTANCE", "100"), ("ID", "R2")),
        ),
        runtime,
    )

    layout.apply(
        command(
            "report.html_layout",
            options=(("INSTANCE", "100"),),
            body=(
                ":FILE:reports\\ordered.html\n:TITLE:Ordered\nLAYOUT-BEFORE\nHTM:R2\nLAYOUT-MIDDLE\nHTM:R1\nLAYOUT-AFTER"
            ),
        ),
        runtime,
    )

    output = tmp_path / "reports" / "ordered.html"
    html = output.read_text(encoding="utf-8")
    assert html.index("LAYOUT-BEFORE") < html.index("TWO") < html.index("LAYOUT-MIDDLE")
    assert html.index("LAYOUT-MIDDLE") < html.index("ONE") < html.index("LAYOUT-AFTER")
    assert runtime.report.window_counter == 2
    assert runtime.report.chart_counter == 1

    deferred_paths = [tmp_path / "100_R1_tmp_.ini", tmp_path / "100_R2_tmp_.ini"]
    assert all(path.exists() for path in deferred_paths)
    HtmlDeleteUtility().apply(command("report.delete"), runtime)
    assert all(not path.exists() for path in deferred_paths)
    assert output.exists()
    assert runtime.report.deferred == {}
    assert runtime.report.cleanup_paths == []


def test_layout_email_target_materializes_local_body(tmp_path: Path) -> None:
    runtime = RuntimeState(tmp_path)
    HtmlLayoutUtility().apply(
        command(
            "report.html_layout",
            options=(("INSTANCE", "9988"),),
            body=":FILE:email:self\n:TITLE:Mail Body\n<p>hello</p>",
        ),
        runtime,
    )
    assert (tmp_path / "9988_sqlpathfinder.htm").exists()


def test_report_state_is_isolated_between_runtime_states(tmp_path: Path) -> None:
    defer = HtmlDeferUtility()
    layout = HtmlLayoutUtility()
    (tmp_path / "rows.csv").write_text("x\nfrom-state-one\n", encoding="utf-8")
    spec = option_rows(
        ("TYPE", "HTML"),
        ("INPUT-FILE", "rows.csv"),
        ("COLUMN-DATA", "", "x"),
    )

    first = RuntimeState(tmp_path / "first")
    first.working_directory.mkdir()
    (first.working_directory / "rows.csv").write_text("x\nfrom-state-one\n", encoding="utf-8")
    second = RuntimeState(tmp_path / "second")
    second.working_directory.mkdir()

    defer.apply(
        command(
            "report.html_defer",
            body=spec,
            options=(("INSTANCE", "1"), ("ID", "ONLYFIRST")),
        ),
        first,
    )
    layout.apply(
        command(
            "report.html_layout",
            body=":FILE:second.html\nHTM:ONLYFIRST",
        ),
        second,
    )

    second_html = (second.working_directory / "second.html").read_text(encoding="utf-8")
    assert "from-state-one" not in second_html
    assert first.report.lookup("ONLYFIRST") is not None
    assert second.report.lookup("ONLYFIRST") is None


def test_tab_and_menu_layouts_are_self_contained(tmp_path: Path) -> None:
    runtime = RuntimeState(tmp_path)
    utility = HtmlTabMenuLayoutUtility()

    (tmp_path / "tabs.csv").write_text("label,url\nAlpha,a.html\nBeta,b.html\n", encoding="utf-8")
    utility.apply(
        command(
            "report.html_tab_layout",
            body=(":FILE:tabs.html\n:IN:tabs.csv\n:WINTITLE:Tabs\n:HEIGHT:800px\n:WIDTH:1200px"),
        ),
        runtime,
    )
    tabs = (tmp_path / "tabs.html").read_text(encoding="utf-8")
    assert "Alpha" in tabs and "Beta" in tabs
    assert 'src="a.html"' in tabs and 'src="b.html"' in tabs
    assert "vg2Tab" in tabs

    (tmp_path / "menu.txt").write_text(
        "htm=content.html\nmenu=<ul><li>Menu A</li></ul>", encoding="utf-8"
    )
    utility.apply(
        command(
            "report.html_menu_layout",
            body=":FILE:menu.html\n:IN:menu.txt\n:TITLE:Menu",
        ),
        runtime,
    )
    menu = (tmp_path / "menu.html").read_text(encoding="utf-8")
    assert "Menu A" in menu
    assert 'src="content.html"' in menu


def test_js_show_and_defer_embed_portable_chart_in_order(tmp_path: Path) -> None:
    runtime = RuntimeState(tmp_path)
    utility = HtmlJsUtility()
    (tmp_path / "chart.csv").write_text("x,y\n1,2\n2,4\n3,3\n", encoding="utf-8")
    spec = (
        "<in-file>chart.csv</in-file>\n"
        "<x>x</x>\n"
        "<title>My Chart</title>\n"
        "<size-x>600</size-x>\n"
        "<size-y>300</size-y>\n"
        "#SQLVAR=x;y"
    )

    utility.apply(command("report.js_show", body=spec), runtime)
    standalone = (tmp_path / "sqlpathfinder.htm").read_text(encoding="utf-8")
    assert "<svg" in standalone
    assert "My Chart" in standalone
    assert runtime.report.chart_counter == 2

    utility.apply(
        command(
            "report.js_defer",
            body=spec,
            options=(("INSTANCE", "4"), ("ID", "CHART1")),
        ),
        runtime,
    )
    HtmlLayoutUtility().apply(
        command(
            "report.html_layout",
            body=":FILE:chart-layout.html\nfirst\nHTMIC:CHART1\nlast",
        ),
        runtime,
    )
    combined = (tmp_path / "chart-layout.html").read_text(encoding="utf-8")
    assert combined.index("first") < combined.index("My Chart") < combined.index("last")
    assert "<svg" in combined
    assert runtime.report.chart_counter == 1


@pytest.mark.parametrize("target", ["report.pyplot", "report.pyplot_show"])
def test_pyplot_executes_portably_and_counter_is_per_run(tmp_path: Path, target: str) -> None:
    utility = HtmlPyPlotUtility()
    runtime = RuntimeState(tmp_path)
    script = (
        "#SPF-REQUIRED-OUT: plot_<<<spf-cw#-ctr>>>.txt\n"
        "from pathlib import Path\n"
        "Path('plot_<<<spf-cw#-ctr>>>.txt').write_text('ok', encoding='utf-8')"
    )
    utility.apply(command(target, body=script), runtime)

    assert (tmp_path / "plot_1.txt").read_text(encoding="utf-8") == "ok"
    assert runtime.report.chart_counter == 2

    isolated = RuntimeState(tmp_path / target.replace(".", "_"))
    isolated.working_directory.mkdir()
    utility.apply(command(target, body=script), isolated)
    assert (isolated.working_directory / "plot_1.txt").exists()
    assert isolated.report.chart_counter == 2


def test_rplot_preserves_script_and_requires_only_rscript(
    tmp_path: Path, monkeypatch: pytest.MonkeyPatch
) -> None:
    runtime = RuntimeState(tmp_path)
    captured: list[str] = []

    monkeypatch.setattr(
        "vg2c_new.utilities.report.shutil.which",
        lambda executable: "/portable/Rscript" if executable == "Rscript" else None,
    )

    def fake_run(args: list[str], *, cwd: Path, check: bool) -> Mock:
        assert args[0] == "/portable/Rscript"
        assert check is True
        captured.append(Path(args[-1]).read_text(encoding="utf-8"))
        (Path(cwd) / "r_1.png").write_text("plot", encoding="utf-8")
        return Mock(returncode=0)

    monkeypatch.setattr("vg2c_new.utilities.report.subprocess.run", fake_run)
    HtmlRPlotUtility().apply(
        command(
            "report.rplot",
            body=("#SPF-REQUIRED-OUT: r_<<<spf-cw#-ctr>>>.png\nprint('<<<spf-cw#-ctr>>>')"),
        ),
        runtime,
    )
    assert "r_1.png" in captured[0]
    assert "print('1')" in captured[0]
    assert (tmp_path / "r_1.png").exists()

    monkeypatch.setattr("vg2c_new.utilities.report.shutil.which", lambda executable: None)
    with pytest.raises(RuntimeError, match="Rscript"):
        HtmlRPlotUtility().apply(command("report.rplot", body="print('x')"), runtime)


def test_gnuplot_preserves_markers_and_header_substitution(
    tmp_path: Path, monkeypatch: pytest.MonkeyPatch
) -> None:
    runtime = RuntimeState(tmp_path)
    (tmp_path / "gnu.csv").write_text("x,y\n1,2\n", encoding="utf-8")
    captured: list[str] = []

    monkeypatch.setattr(
        "vg2c_new.utilities.report.shutil.which",
        lambda executable: "/portable/gnuplot" if executable == "gnuplot" else None,
    )

    def fake_run(args: list[str], *, cwd: Path, check: bool) -> Mock:
        assert args[0] == "/portable/gnuplot"
        assert check is True
        script = Path(args[-1]).read_text(encoding="utf-8")
        captured.append(script)
        (Path(cwd) / "gnu.png").write_text("plot", encoding="utf-8")
        return Mock(returncode=0)

    monkeypatch.setattr("vg2c_new.utilities.report.subprocess.run", fake_run)
    HtmlGnuPlotUtility().apply(
        command(
            "report.gnuplot",
            body=(
                "#SPF-REQUIRED-CSV: gnu.csv\n"
                "#SPF-REQUIRED-OUT: gnu.png\n"
                "plot '$spf-in-file$' using {x}:{y}"
            ),
        ),
        runtime,
    )
    assert "using 1:2" in captured[0]
    assert "$spf-in-file$" not in captured[0]
    assert str(tmp_path / "gnu.csv") in captured[0]
    assert (tmp_path / "gnu.png").exists()

    monkeypatch.setattr("vg2c_new.utilities.report.shutil.which", lambda executable: None)
    with pytest.raises(RuntimeError, match="gnuplot"):
        HtmlGnuPlotUtility().apply(
            command(
                "report.gnuplot",
                body="#SPF-REQUIRED-CSV: gnu.csv\n#SPF-REQUIRED-OUT: gnu2.png",
            ),
            runtime,
        )


def test_report_manifest_entries_are_terminal_implemented() -> None:
    rows = [row for row in RESOLVER_MANIFEST if row[2].startswith("report.")]
    assert len(rows) == 14
    assert {row[6] for row in rows} == {"implemented"}

    modes = {row[2]: row[3] for row in rows}
    assert modes["report.html_run"] == "AMENDED_PORT"
    assert modes["report.html_defer"] == "AMENDED_PORT"
    assert modes["report.html_layout"] == "AMENDED_PORT"
    assert modes["report.delete"] == "AMENDED_PORT"
    assert modes["report.pyplot"] == "AMENDED_PORT"
    assert modes["report.gnuplot"] == "AMENDED_PORT"
    assert modes["report.rplot"] == "AMENDED_PORT"
    assert modes["report.html_tab_layout"] == "REWRITE"
    assert modes["report.html_menu_layout"] == "REWRITE"
    assert modes["report.js_show"] == "REWRITE"
    assert modes["report.js_defer"] == "REWRITE"


def test_parser_interpreter_report_flow_with_ordinary_vg2_command(tmp_path: Path) -> None:
    def block(*options: str, body: str = "") -> str:
        return "<OPTIONS>\n" + "\n".join(options) + "\n</OPTIONS>\n" + body

    source = f"\n{QUERY_DLM}\n".join(
        (
            block("/WRITE-FILE=Y", "/CSV=data.csv", body="name\nA"),
            block(
                "/REPORT=HTML-DEFER",
                "/INSTANCE=10",
                "/ID=R",
                body=option_rows(
                    ("TYPE", "HTML"),
                    ("INPUT-FILE", "data.csv"),
                    ("COLUMN-DATA", "", "name"),
                ),
            ),
            block("/REPORT=HTML-LAYOUT", "/INSTANCE=10", body=":FILE:out.html\nHTM:R"),
        )
    )
    commands = parse(source)
    interpreter = Interpreter(
        {
            "write_file": WriteFileUtility(),
            "report.html_defer": HtmlDeferUtility(),
            "report.html_layout": HtmlLayoutUtility(),
        }
    )
    interpreter.execute(commands, RuntimeState(tmp_path))

    assert "A" in (tmp_path / "out.html").read_text(encoding="utf-8")
