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
    run(work)
    assert "9" in (work / "final.csv").read_text()
    assert "Edited" in (work / "linked.html").read_text()
    assert "color:purple" in (work / "embedded.html").read_text()
    assert "color:purple" in (work / "report.css").read_text()


def test_long_report_schema_externalizes_and_rereads_user_edits(tmp_path):
    fields = [f"F{i}" for i in range(10)]
    labels = [f"Label {i}" for i in range(10)]
    spec = "\n".join([
        D.join(["TYPE", "HTML"]), D.join(["INPUT-FILE", "data.csv"]),
        D.join(["COLUMN-DATA", "", *fields]),
        D.join(["COLUMN-HEADERS", "", *labels]),
        D.join(["COLUMN-ALIGNMENT", "", *(["middle-right"] * len(fields))]),
    ])
    source = tmp_path / "long.txt"
    source.write_text(block("/REPORT=HTML-DEFER\n/ID=LONG", spec)
                      + block("/REPORT=HTML-LAYOUT", ":FILE:out.html\nHTM:LONG"))
    main = translate(source)
    code = main.read_text()
    assert "job.report_spec(" in code
    assert "Label 9" not in code
    definitions = list(main.parent.glob("html/*.report.json"))
    assert len(definitions) == 1
    import json
    config = json.loads(definitions[0].read_text())
    assert config["columns"] == fields
    config["headers"][0] = "Edited & Label"
    definitions[0].write_text(json.dumps(config))
    work = tmp_path / "work"
    work.mkdir()
    (work / "data.csv").write_text(",".join(fields) + "\n" + ",".join(str(i) for i in range(10)) + "\n")
    run = load_job(main)
    run(work)
    assert "Edited &amp; Label" in (work / "out.html").read_text()
    assert "9</td>" in (work / "out.html").read_text()
    config["headers"][0] = "Second Label"
    definitions[0].write_text(json.dumps(config))
    run(work)
    assert "Second Label" in (work / "out.html").read_text()
