"""Source-derived Session 04 report/CSS asset and lifecycle regressions."""
from pathlib import Path

import pytest

from vg2c import translate
from vg2c.runtime import JobRuntime, csv_report


D = r"<\\>"


def block(options, body=""):
    return "<OPTIONS>\n" + options + "\n</OPTIONS>\n" + body + "\n<---- New Query ---->\n"


def load_job(path):
    ns = {"__file__": str(path), "__name__": "test_generated"}
    exec(compile(path.read_text(), str(path), "exec"), ns)
    return ns["run"]


def test_immediate_css_external_asset_execution_and_edit(tmp_path, monkeypatch):
    css = D.join(["TYPE", "CSS"]) + "\n" + D.join(["CSS", "my.css"]) + "\n" + D.join(
        ["FORMAT", "COLUMN-HEADERS", "color:red", "font-size:12"])
    html = "\n".join([
        D.join(["TYPE", "HTML"]), D.join(["INPUT-FILE", "data.csv"]),
        D.join(["OUTPUT-FILE", "immediate.html"]),
        D.join(["COLUMN-DATA", "", "value"]),
        D.join(["COLUMN-HEADERS", "", "Readable & Label"]),
    ])
    source = tmp_path / "report.txt"
    source.write_text(block("/REPORT=HTML-RUN", css) + block("/REPORT=HTML-RUN", html))
    main = translate(source)
    emitted = main.read_text()
    assert "job.define_css(" in emitted
    assert "job.styles[" not in emitted
    css_asset = next(main.parent.glob("styles/*.css"))
    assert "color:red" in css_asset.read_text()
    assert "font-size:12px" in css_asset.read_text()
    work = tmp_path / "work"
    work.mkdir()
    (work / "data.csv").write_text("value\n<b>&</b>\n")
    monkeypatch.chdir(tmp_path)
    run = load_job(main)
    run(work)
    assert "color:red" in (work / "my.css").read_text()
    output = (work / "immediate.html").read_text()
    assert "Readable &amp; Label" in output
    assert "&lt;b&gt;&amp;&lt;/b&gt;" in output
    assert 'href="my.css"' in output
    css_asset.write_text("th { color:green; }\n")
    run(work)
    assert (work / "my.css").read_text() == "th { color:green; }\n"
    assert css_asset.read_text() == "th { color:green; }\n"


def test_html_delete_keeps_css_and_unreferenced_deferred_report(tmp_path):
    assets = tmp_path / "assets"
    assets.mkdir()
    work = tmp_path / "work"
    work.mkdir()
    (assets / "style.css").write_text("th {color:blue;}\n")
    (assets / "one.html").write_text("<html><head>${VG2C_CSS}</head><body><table><tbody>${A_ROWS}</tbody></table></body></html>")
    (assets / "two.html").write_text("<html><head>${VG2C_CSS}</head><body><table><tbody>${B_ROWS}</tbody></table></body></html>")
    (work / "rows.csv").write_text("value\n1\n")
    job = JobRuntime(assets, work)
    job.define_css("layout.css", "style.css")
    job.reports["A"] = csv_report("rows.csv", columns=["value"])
    job.reports["B"] = csv_report("rows.csv", columns=["value"])
    job.html("one.html", output="one.html")
    job.delete_html()
    assert "A" not in job.reports and "B" in job.reports
    assert job.css_file is not None
    job.html("two.html", output="two.html", embed_css=True)
    assert "color:blue" in (work / "two.html").read_text()
    assert "1" in (work / "two.html").read_text()


def test_explicit_missing_css_and_csv_raise_without_output(tmp_path):
    assets = tmp_path / "assets"
    assets.mkdir()
    (assets / "shell.html").write_text("<html><head>${VG2C_CSS}</head><body>x</body></html>")
    job = JobRuntime(assets, tmp_path / "work")
    with pytest.raises(FileNotFoundError, match="Required HTML stylesheet"):
        job.html("shell.html", output="out.html", css_file="gone.css")
    assert not (tmp_path / "work/out.html").exists()
    (assets / "shell.html").write_text("<html><body><table><tbody>${R_ROWS}</tbody></table></body></html>")
    job.reports["R"] = csv_report("not-here.csv", columns=["col"])
    with pytest.raises(FileNotFoundError, match="Report input CSV"):
        job.html("shell.html", output="out.html")
    assert not (tmp_path / "work/out.html").exists()


def test_connected_sql_pivot_downstream_sql_html_css_assets(tmp_path, monkeypatch):
    """Source-derived integration: editable SQL -> CTARRAY -> HTML, no retranslating."""
    first = (
        "SELECT 'L1' AS lot, 'A' AS metric, '1' AS reading "
        "UNION ALL SELECT 'L1','B','2'"
    )
    pivot = "SELECT lot, metric, reading FROM [long]"
    downstream = "SELECT a0.LOT, CrossTab->[[a0,42;:A]] FROM [wide] a0"
    css_spec = "\n".join([
        D.join(["TYPE", "CSS"]), D.join(["CSS", "report.css"]),
        D.join(["FORMAT", "Column-Headers", "color:blue"]),
    ])
    report_spec = "\n".join([
        D.join(["TYPE", "HTML"]), D.join(["INPUT-FILE", "final.csv"]),
        D.join(["COLUMN-DATA", "", "LOT", "A", "B"]),
        D.join(["COLUMN-HEADERS", "", "Lot", "Alpha", "Beta"]),
        D.join(["COLUMN-ALIGNMENT", "", "middle-left", "middle-center", "middle-right"]),
    ])
    source = tmp_path / "pipeline.txt"
    source.write_text(
        block("/ENGINE=SQLite\n/CSV=long.csv", first)
        + block("/ENGINE=SQLite\n/TABLE=long.csv\n/CSV=wide.csv\n/CTROW=lot\n/CTHEADER=metric\n/CTVAL=reading\n/CTARRAY=a0,42", pivot)
        + block("/ENGINE=SQLite\n/TABLE=wide.csv\n/CSV=final.csv", downstream)
        + block("/REPORT=HTML-RUN", css_spec)
        + block("/REPORT=HTML-DEFER\n/ID=R", report_spec)
        + block("/REPORT=HTML-LAYOUT", ":FILE:linked.html\n:CSS:report.css\n:CSSEMBED:N\n<h1>Original</h1>\nHTM:R")
        + block("/REPORT=HTML-LAYOUT", ":FILE:embedded.html\n:CSS:report.css\n:CSSEMBED:Y\n<h1>Original</h1>\nHTM:R")
    )
    main = translate(source)
    sql_files = sorted(main.parent.glob("sql/*.sql"))
    assert len(sql_files) == 3
    assert len(list(main.parent.glob("html/*.html"))) == 2
    assert len(list(main.parent.glob("styles/*.css"))) == 1
    monkeypatch.chdir(tmp_path)
    run = load_job(main)
    work = tmp_path / "work"
    run(work)
    assert (work / "wide.csv").read_text().splitlines()[0] == "LOT,A,B"
    assert (work / "42_A0.ini").read_text() == "A\tB"
    assert (work / "final.csv").read_text().splitlines()[0] == "LOT,A,B"
    assert "Alpha" in (work / "linked.html").read_text()
    assert 'href="report.css"' in (work / "linked.html").read_text()
    assert "color:blue" in (work / "embedded.html").read_text()
    assert 'href="report.css"' not in (work / "embedded.html").read_text()
    # Reread independently edited generated source assets.
    long_query = next(p for p in sql_files if "'L1'" in p.read_text())
    long_query.write_text(long_query.read_text().replace("'1' AS reading", "'9' AS reading"))
    shell = sorted(main.parent.glob("html/*.html"))[0]
    shell.write_text(shell.read_text().replace("Original", "Edited"))
    css_asset = next(main.parent.glob("styles/*.css"))
    css_asset.write_text("th {color:purple;}\n")
    fragment = next(main.parent.glob("html/reports/*.table.html"))
    fragment.write_text(fragment.read_text().replace(">Beta</th>", ">Updated Beta</th>"))
    run(work)
    assert "Updated Beta" in (work / "linked.html").read_text()
    assert "Updated Beta" in (work / "embedded.html").read_text()
    assert "9" in (work / "final.csv").read_text()
    assert "Edited" in (work / "linked.html").read_text()
    assert "color:purple" in (work / "embedded.html").read_text()
    assert "color:purple" in (work / "report.css").read_text()


def test_long_html_report_columns_edit_without_python_or_json(tmp_path):
    fields = [f"F{i}" for i in range(10)]
    spec = "\n".join([
        D.join(["TYPE", "HTML"]), D.join(["INPUT-FILE", "data.csv"]),
        D.join(["COLUMN-DATA", "", *fields]),
        D.join(["COLUMN-HEADERS", "", *(f"Label {i}" for i in range(10))]),
        D.join(["COLUMN-ALIGNMENT", "", *(["middle-right"] * 10)]),
    ])
    source = tmp_path / "large.txt"
    source.write_text(block("/REPORT=HTML-DEFER\n/ID=R", spec)
        + block("/REPORT=HTML-LAYOUT", ":FILE:out.html\nHTM:R"))
    main = translate(source)
    python_before = main.read_text()
    assert "job.report_spec" not in python_before and "Label 9" not in python_before
    assert not list(main.parent.rglob("*.report.json"))
    shell = next(main.parent.glob("html/*.html"))
    content = shell.read_text()
    assert 'data-field="F0"' in content
    work = tmp_path / "work"
    work.mkdir()
    (work / "data.csv").write_text(",".join(fields + ["NEW"]) + "\n" +
        ",".join([str(i) for i in range(10)] + ["extra&<script>"]) + "\n")
    run = load_job(main)
    run(work)
    updated = content.replace(
        '<th data-field="F0" data-align="middle-right">Label 0</th>', "")
    updated = updated.replace(
        '<th data-field="F1" data-align="middle-right">Label 1</th>'
        '<th data-field="F2" data-align="middle-right">Label 2</th>',
        '<th data-field="F2" data-align="center">Maximum Voltage</th>'
        '<th data-field="F1" data-align="left">First</th>')
    updated = updated.replace("</tr></thead>",
        '<th data-field="NEW" data-align="right">New Measurement</th></tr></thead>')
    shell.write_text(updated.replace("</table>", "</table><p>Authored <b>markup</b></p>"))
    run(work)
    html = (work / "out.html").read_text()
    assert html.index("Maximum Voltage") < html.index("First")
    assert "Label 0" not in html and "New Measurement" in html
    assert "extra&amp;&lt;script&gt;" in html
    assert "text-align:center" in html and "text-align:left" in html
    assert "Authored <b>markup</b>" in html
    assert main.read_text() == python_before
    assert not list(main.parent.rglob("*.report.json"))


def test_relocated_project_relative_css_and_changed_cwd(tmp_path, monkeypatch):
    """CSS paths beginning at the project root remain editable after relocation."""
    import shutil
    assets = tmp_path / "assets"
    (assets / "html").mkdir(parents=True)
    (assets / "styles").mkdir()
    (assets / "html/report.html").write_text(
        '<html><head>${VG2C_CSS}</head><body><i>Unchanged</i></body></html>')
    (assets / "styles/custom.css").write_text("i {color:green;}\n")
    moved = tmp_path / "moved"
    shutil.move(assets, moved)
    work = tmp_path / "work"
    unrelated = tmp_path / "unrelated"
    unrelated.mkdir()
    monkeypatch.chdir(unrelated)
    job = JobRuntime(moved, work)
    job.html("html/report.html", output="linked.html",
             css_file="styles/custom.css", embed_css=False)
    assert 'href="custom.css"' in (work / "linked.html").read_text()
    assert (work / "custom.css").read_text() == "i {color:green;}\n"
    (moved / "styles/custom.css").write_text("i {color:purple;}\n")
    job.html("html/report.html", output="embedded.html",
             css_file="styles/custom.css", embed_css=True)
    assert "i {color:purple;}" in (work / "embedded.html").read_text()
    assert '<link ' not in (work / "embedded.html").read_text()
    (moved / "styles/custom.css").unlink()
    with pytest.raises(FileNotFoundError, match="Required HTML stylesheet"):
        job.html("html/report.html", output="failure.html",
                 css_file="styles/custom.css")
    assert not (work / "failure.html").exists()


def test_native_conditional_and_loop_css_state_is_per_job(tmp_path):
    assets = tmp_path / "assets"
    assets.mkdir()
    (assets / "a.css").write_text("p {color:red;}")
    (assets / "b.css").write_text("p {color:blue;}")
    (assets / "shell.html").write_text(
        "<html><head>${VG2C_CSS}</head><body><p>Body</p></body></html>"
    )
    root = tmp_path / "output"
    root.mkdir()
    for selected in [False, True]:
        job = JobRuntime(assets, root)
        if selected:
            job.define_css("active.css", "b.css")
        else:
            job.define_css("active.css", "a.css")
        for iteration in range(2):
            job.html("shell.html", output=f"{selected}_{iteration}.html",
                     embed_css=True)
        required = "color:blue" if selected else "color:red"
        for iteration in range(2):
            assert required in (root / f"{selected}_{iteration}.html").read_text()
    assert (assets / "a.css").read_text() == "p {color:red;}"
    assert (assets / "b.css").read_text() == "p {color:blue;}"


def test_type_key_header_precedes_actual_css_report_type(tmp_path):
    """The ScriptHost design-table header must never override TYPE=CSS."""
    spec = "\n".join([
        D.join(["Type", "Key", "COL1", "COL2"]),
        D.join(["TYPE", "CSS"]),
        D.join(["CSS", "report.css"]),
        D.join(["FORMAT", "Column-Headers", "background-color:#eeeeee", "font-size:12"]),
    ])
    source = tmp_path / "source.txt"
    source.write_text(block("/REPORT=HTML-RUN", spec))
    main = translate(source)
    assert "job.define_css(" in main.read_text()
    css_source = next(main.parent.glob("styles/*.css"))
    assert "background-color:#eeeeee" in css_source.read_text()
    assert "font-size:12px" in css_source.read_text()
    work = tmp_path / "work"
    load_job(main)(work)
    assert (work / "report.css").read_text() == css_source.read_text()


def test_reused_report_fragment_is_one_editable_html_location(tmp_path):
    spec = "\n".join([
        D.join(["TYPE", "HTML"]), D.join(["INPUT-FILE", "data.csv"]),
        D.join(["COLUMN-DATA", "", "A", "B"]),
        D.join(["COLUMN-HEADERS", "", "First", "Second"]),
    ])
    source = tmp_path / "shared.txt"
    source.write_text(block("/REPORT=HTML-DEFER\n/ID=R", spec)
        + block("/REPORT=HTML-LAYOUT", ":FILE:first.html\nHTM:R")
        + block("/REPORT=HTML-LAYOUT", ":FILE:second.html\nHTM:R"))
    main = translate(source)
    assert "job.report(" in main.read_text()
    fragments = list(main.parent.glob("html/reports/*.table.html"))
    assert len(fragments) == 1
    assert not list(main.parent.rglob("*.report.json"))
    work = tmp_path / "work"
    work.mkdir()
    (work / "data.csv").write_text("A,B,C\n1,2,3\n")
    run = load_job(main)
    run(work)
    for file in ("first.html", "second.html"):
        assert "Second" in (work / file).read_text()
    fragment = fragments[0]
    fragment.write_text(fragment.read_text().replace(
        '<th data-field="B" data-align="middle-left">Second</th>',
        '<th data-field="C" data-align="right">Third</th>'))
    run(work)
    for file in ("first.html", "second.html"):
        result = (work / file).read_text()
        assert "Third" in result and "3</td>" in result and "Second" not in result


def test_pattern_column_and_live_csv_headers(tmp_path):
    spec = "\n".join([
        D.join(["TYPE", "HTML"]), D.join(["INPUT-FILE", "data.csv"]),
        D.join(["COLUMN-DATA", "", "ID", "STARTS WITH:"]),
        D.join(["COLUMN-HEADERS", "", "Identifier", "M_"]),
        D.join(["COLUMN-ALIGNMENT", "", "left", "right"]),
    ])
    source = tmp_path / "pattern.txt"
    source.write_text(block("/REPORT=HTML-DEFER\n/ID=P", spec)
        + block("/REPORT=HTML-LAYOUT", ":FILE:result.html\nHTM:P"))
    main = translate(source)
    assert 'data-pattern="M_"' in next(main.parent.glob("html/*.html")).read_text()
    work = tmp_path / "work"
    work.mkdir()
    (work / "data.csv").write_text("ID,M_ONE,OTHER\nx,10,no\n")
    run = load_job(main)
    run(work)
    assert "M one" in (work / "result.html").read_text()
    (work / "data.csv").write_text("ID,M_ONE,M_TWO,OTHER\nx,10,20,no\n")
    run(work)
    html = (work / "result.html").read_text()
    assert "M one" in html and "M two" in html and "20</td>" in html
    assert html.count("<th ") == 3 and html.count("<td ") == 3


def test_html_report_missing_field_empty_csv_and_unsafe_text(tmp_path):
    root = tmp_path / "assets"
    root.mkdir()
    template = root / "report.html"
    template.write_text(
        '<html><body><table data-report="R"><thead><tr><th data-field="A">Shown</th>'
        '</tr></thead><tbody></tbody></table></body></html>')
    work = tmp_path / "work"
    work.mkdir()
    job = JobRuntime(root, work)
    job.reports["R"] = csv_report("data.csv")
    (work / "data.csv").write_text("A\n")
    job.html("report.html", output="out.html")
    assert "<td " not in (work / "out.html").read_text()
    slot_literal = chr(36) + "{NOT_A_SLOT}"
    (work / "data.csv").write_text('A\n"<script>& ' + slot_literal + '"\n')
    job.html("report.html", output="out.html")
    html = (work / "out.html").read_text()
    assert '&lt;script&gt;&amp;' in html and slot_literal in html
    template.write_text(template.read_text().replace('data-field="A"', 'data-field="ABSENT"'))
    with pytest.raises(ValueError, match="missing CSV field"):
        job.html("report.html", output="failure.html")
    (work / "data.csv").write_text("")
    with pytest.raises(ValueError, match="no header row"):
        job.html("report.html", output="failure.html")


def test_redefined_report_id_resolves_current_source_html_fragment(tmp_path):
    first = "\n".join([
        D.join(["TYPE", "HTML"]), D.join(["INPUT-FILE", "rows.csv"]),
        D.join(["COLUMN-DATA", "", "A"]), D.join(["COLUMN-HEADERS", "", "Alpha"]),
    ])
    second = "\n".join([
        D.join(["TYPE", "HTML"]), D.join(["INPUT-FILE", "rows.csv"]),
        D.join(["COLUMN-DATA", "", "B"]), D.join(["COLUMN-HEADERS", "", "Beta"]),
    ])
    source = tmp_path / "redefined.txt"
    source.write_text(block("/REPORT=HTML-DEFER\n/ID=R", first)
        + block("/REPORT=HTML-LAYOUT", ":FILE:first.html\nHTM:R")
        + block("/REPORT=HTML-DEFER\n/ID=R", second)
        + block("/REPORT=HTML-LAYOUT", ":FILE:second.html\nHTM:R"))
    main = translate(source)
    fragments = sorted(main.parent.glob("html/reports/*.table.html"))
    assert len(fragments) == 2
    assert "job.report(" in main.read_text()
    work = tmp_path / "work"
    work.mkdir()
    (work / "rows.csv").write_text("A,B\n1,2\n")
    run = load_job(main)
    run(work)
    a = (work / "first.html").read_text()
    b = (work / "second.html").read_text()
    assert "Alpha" in a and "1</td>" in a and "Beta" not in a
    assert "Beta" in b and "2</td>" in b and "Alpha" not in b
    fragments[0].write_text(fragments[0].read_text().replace(">Alpha</th>", ">Edited</th>"))
    run(work)
    assert "Edited" in (work / "first.html").read_text()
    assert "Edited" not in (work / "second.html").read_text()


@pytest.mark.parametrize("mode,pattern,expected", [
    ("ENDS WITH:", "_MAX", "SITE_MAX"),
    ("CONTAINS:", "OLT", "VOLTAGE"),
    ("STARTS/ENDS WITH (%):", "V%GE", "VOLTAGE"),
])
def test_script_host_pattern_families_from_live_csv(tmp_path, mode, pattern, expected):
    root = tmp_path / "assets"
    root.mkdir()
    (root / "report.html").write_text(
        '<table data-report="R"><thead><tr><th data-field="' + mode +
        '" data-pattern="' + pattern + '" data-align="right">Matching</th>'
        '</tr></thead><tbody></tbody></table>')
    work = tmp_path / "work"
    work.mkdir()
    (work / "data.csv").write_text("SITE_MAX,VOLTAGE,OTHER\n1,2,3\n")
    job = JobRuntime(root, work)
    job.reports["R"] = csv_report("data.csv")
    job.html("report.html", output="result.html")
    html = (work / "result.html").read_text()
    assert 'data-field="' + expected + '"' in html
    assert html.count("<th ") == html.count("<td ") == 1


def test_malformed_report_declaration_and_duplicate_csv_headers(tmp_path):
    root = tmp_path / "assets"
    root.mkdir()
    (root / "report.html").write_text(
        '<table data-report="R"><thead><tr><th>No data-field</th>'
        '</tr></thead><tbody></tbody></table>')
    work = tmp_path / "work"
    work.mkdir()
    (work / "data.csv").write_text("A\nx\n")
    job = JobRuntime(root, work)
    job.reports["R"] = csv_report("data.csv")
    with pytest.raises(ValueError, match="data-field"):
        job.html("report.html", output="out.html")
    (root / "report.html").write_text(
        '<table data-report="R"><thead><tr><th data-field="A">A</th>'
        '</tr></thead><tbody></tbody></table>')
    (work / "data.csv").write_text("A,a\nx,y\n")
    with pytest.raises(ValueError, match="duplicate case-insensitive"):
        job.html("report.html", output="out.html")
    assert not (work / "out.html").exists()


def test_special_csv_field_with_dollar_does_not_become_template_slot(tmp_path):
    source = tmp_path / "special.txt"
    spec = "\n".join([
        D.join(["TYPE", "HTML"]), D.join(["INPUT-FILE", "rows.csv"]),
        D.join(["COLUMN-DATA", "", "A$B"]),
        D.join(["COLUMN-HEADERS", "", "Price $ label"]),
    ])
    source.write_text(block("/REPORT=HTML-DEFER\n/ID=R", spec)
        + block("/REPORT=HTML-LAYOUT", ":FILE:out.html\nHTM:R"))
    main = translate(source)
    assert not list(main.parent.rglob("*.report.json"))
    shell = next(main.parent.glob("html/*.html"))
    assert "A&#36;B" in shell.read_text()
    work = tmp_path / "work"
    work.mkdir()
    (work / "rows.csv").write_text("A$B\n52\n")
    load_job(main)(work)
    html = (work / "out.html").read_text()
    assert "Price $ label" in html and "52</td>" in html
