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
