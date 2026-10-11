"""Session 04: reproduce HTML-first correctness regressions."""
import pytest
from vg2c import translate
from vg2c.runtime import JobRuntime, csv_report

D = r"<\\>"

def block(opts, body=""):
    return "<OPTIONS>\n" + opts + "\n</OPTIONS>\n" + body + "\n<---- New Query ---->\n"

def spec(field, label):
    return "\n".join(D.join(row) for row in [
        ["TYPE", "HTML"], ["INPUT-FILE", "data.csv"],
        ["COLUMN-DATA", "", field], ["COLUMN-HEADERS", "", label]])

def job(tmp_path, body, columns="A"):
    assets = tmp_path / "assets"
    assets.mkdir()
    work = tmp_path / "work"
    work.mkdir()
    (work / "data.csv").write_text(columns + "\n1\n")
    (assets / "page.html").write_text(body)
    runtime = JobRuntime(assets, work)
    runtime.reports["R"] = csv_report("data.csv")
    return runtime, assets / "page.html", work

def test_no_percent_is_prefix_and_schema_is_reloaded(tmp_path):
    html = ('<table data-report="R"><thead><tr>'
            '<th data-field="STARTS/ENDS WITH (%):" data-pattern="VOLTAGE" '
            'data-align="right">Match</th></tr></thead><tbody></tbody></table>')
    runtime, _, work = job(tmp_path, html)
    (work / "data.csv").write_text("VOLTAGE,VOLTAGE_A,Voltage_B,OTHER\n1,2,3,4\n")
    runtime.html("page.html", output="out.html")
    result = (work / "out.html").read_text()
    assert result.count("<th ") == result.count("<td ") == 3
    assert "VOLTAGE_A" in result and "Voltage b" in result
    (work / "data.csv").write_text("VOLTAGE_NEW,OTHER\n9,0\n")
    runtime.html("page.html", output="out.html")
    assert (work / "out.html").read_text().count("<td ") == 1

@pytest.mark.parametrize("ids", [
    ["R", "REPORT", "A", "AB", "ABC"],
    ["ABC", "AB", "A", "REPORT", "R"],
])
def test_full_line_report_directives_are_exact(tmp_path, ids):
    script = tmp_path / "source.txt"
    script.write_text(
        "".join(block(f"/REPORT=HTML-DEFER\n/ID={key}", spec("F" + key, "Head " + key)) for key in ids)
        + block("/REPORT=HTML-LAYOUT",
                ":FILE:out.html\n<p>Authored HTM:R text</p>\n" +
                "".join(f"HTM:{key}\n" for key in ids)))
    main = translate(script)
    work = tmp_path / "work"
    work.mkdir()
    (work / "data.csv").write_text(
        ",".join("F" + key for key in ids) + "\n" +
        ",".join(str(i) for i in range(len(ids))) + "\n")
    scope = {"__file__": str(main), "__name__": "test_generated"}
    exec(compile(main.read_text(), str(main), "exec"), scope)
    scope["run"](work)
    result = (work / "out.html").read_text()
    assert result.count('data-report=') == len(ids)
    assert [result.index("Head " + key + "</th>") for key in ids] == sorted(
        result.index("Head " + key + "</th>") for key in ids)
    assert "Authored HTM:R text" in result

def test_literal_currency_and_legacy_slots_after_html_only_edit(tmp_path):
    runtime, page, work = job(tmp_path, '<html><head>${VG2C_CSS}</head><body>'
        '<h1>Price $100</h1><h1>Price $USD</h1>'
        '<h1>Cost $5.99 per unit</h1><h1>Monthly budget: $1,000</h1>'
        '<p title="Cost $USD">$VALUE_1 ${VALUE_1}</p>'
        '<table><tbody>${R_ROWS}</tbody></table></body></html>')
    runtime.html("page.html", output="out.html", values={"VALUE_1": "safe & <value>"})
    html = (work / "out.html").read_text()
    for phrase in ("Price $100", "Price $USD", "Cost $5.99 per unit",
                   "Monthly budget: $1,000", 'title="Cost $USD"'):
        assert phrase in html
    assert "safe &amp; &lt;value&gt; safe &amp; &lt;value&gt;" in html
    page.write_text(page.read_text().replace("Price $100", "Edited $200"))
    runtime.html("page.html", output="out.html", values={"VALUE_1": "ok"})
    assert "Edited $200" in (work / "out.html").read_text()
    page.write_text(page.read_text() + "${NOT_A_SLOT}")
    with pytest.raises(ValueError, match="Missing HTML slot"):
        runtime.html("page.html", output="failure.html", values={"VALUE_1": "ok"})

def test_tbody_does_not_silently_delete_authored_rows(tmp_path):
    table = ('<table data-report="R"><thead><tr><th data-field="A">Alpha</th>'
             '</tr></thead><tbody>{body}</tbody></table>')
    runtime, page, work = job(tmp_path, table.format(body=' \n<!-- keep me -->\n${R_ROWS}\n'))
    runtime.html("page.html", output="out.html")
    assert "<!-- keep me -->" in (work / "out.html").read_text()
    assert (work / "out.html").read_text().count("<td ") == 1
    page.write_text(table.format(body="<tr><td>Manual</td></tr>"))
    with pytest.raises(ValueError, match="tbody"):
        runtime.html("page.html", output="bad.html")
    assert not (work / "bad.html").exists()
    page.write_text(table.format(body="") +
                    "<table><tbody><tr><td>Untouched</td></tr></tbody></table>")
    runtime.html("page.html", output="out.html")
    assert "<td>Untouched</td>" in (work / "out.html").read_text()


@pytest.mark.parametrize("mode,pattern,fields", [
    ("STARTS WITH:", "a.", ["a.one", "A.two"]),
    ("ENDS WITH:", "_x", ["a_x", "B_X", "A[1]_X"]),
    ("CONTAINS:", "(z)", ["A(z)B"]),
    ("STARTS/ENDS WITH (%):", "A%B%C", ["AXBYC", "A B C"]),
    ("STARTS/ENDS WITH (%):", "A[1]", ["A[1]", "A[1]_X"]),
    ("STARTS/ENDS WITH (%):", "nomatch", []),
])
def test_all_source_patterns_use_literal_fields_and_matching_cell_counts(tmp_path, mode, pattern, fields):
    root = tmp_path / "assets"
    root.mkdir()
    work = tmp_path / "work"
    work.mkdir()
    names = ["a.one", "A.two", "a_x", "B_X", "A(z)B",
             "AXBYC", "A B C", "A[1]", "A[1]_X", "OTHER"]
    (work / "data.csv").write_text(",".join(names) + "\n" +
                                   ",".join(str(i) for i in range(len(names))) + "\n")
    (root / "page.html").write_text(
        '<table data-report="R"><thead><tr><th data-field="' + mode +
        '" data-pattern="' + pattern + '" data-align="center">Any</th>'
        '</tr></thead><tbody></tbody></table>')
    runtime = JobRuntime(root, work)
    runtime.reports["R"] = csv_report("data.csv")
    runtime.html("page.html", output="out.html")
    result = (work / "out.html").read_text()
    assert result.count("<th ") == result.count("<td ") == len(fields)
    assert result.count("text-align:center") == len(fields)
    for field in fields:
        assert 'data-field="' + field + '"' in result


def test_html_only_csv_field_dollar_and_unicode_data_are_not_templates(tmp_path):
    root = tmp_path / "assets"
    root.mkdir()
    work = tmp_path / "work"
    work.mkdir()
    (root / "page.html").write_text(
        '<html><head></head><body><table data-report="R"><thead><tr>'
        '<th data-field="A$B">Price $USD &amp; ¥</th></tr></thead>'
        '<tbody></tbody></table></body></html>')
    (work / "data.csv").write_text('A$B\n"€ 3.25 & <span> ${NOT_A_SLOT}"\n')
    runtime = JobRuntime(root, work)
    runtime.reports["R"] = csv_report("data.csv")
    runtime.html("page.html", output="out.html")
    result = (work / "out.html").read_text()
    assert 'data-field="A$B"' in result
    assert "Price $USD &amp; ¥" in result
    assert "&lt;span&gt;" in result and "€ 3.25" in result
    assert "${NOT_A_SLOT}" in result


def test_unknown_and_interactive_layout_references_diagnosed(tmp_path):
    source = tmp_path / "source.txt"
    source.write_text(block("/REPORT=HTML-LAYOUT", ":FILE:x.html\nHTM:UNKNOWN"))
    with pytest.raises(ValueError, match="Unknown deferred report.*UNKNOWN"):
        translate(source)
    source.write_text(block("/REPORT=HTML-LAYOUT", ":FILE:x.html\nHTMI:REPO"))
    with pytest.raises(ValueError, match="HTMI/HTMIC"):
        translate(source)


def test_repeated_reference_and_report_prefixes(tmp_path):
    source = tmp_path / "source.txt"
    source.write_text(block("/REPORT=HTML-DEFER\n/ID=A", spec("F_A", "Header A"))
                      + block("/REPORT=HTML-DEFER\n/ID=AB", spec("F_AB", "Header AB"))
                      + block("/REPORT=HTML-LAYOUT",
                              ":FILE:out.html\nHTM:AB\nHTM:A\nHTM:A\n"))
    main = translate(source)
    work = tmp_path / "work"
    work.mkdir()
    (work / "data.csv").write_text("F_A,F_AB\none,two\n")
    scope = {"__file__": str(main), "__name__": "test_generated"}
    exec(compile(main.read_text(), str(main), "exec"), scope)
    scope["run"](work)
    html = (work / "out.html").read_text()
    assert html.count("Header A</th>") == 2
    assert html.count("Header AB</th>") == 1
    assert html.index("Header AB</th>") < html.index("Header A</th>")


def test_tbody_single_legacy_row_slot_and_renderer_slot_collision(tmp_path):
    runtime, page, work = job(tmp_path,
        '<table data-report="R"><thead><tr><th data-field="A">A</th>'
        '</tr></thead><tbody>$R_ROWS</tbody></table>')
    runtime.html("page.html", output="out.html")
    assert (work / "out.html").read_text().count("<td ") == 1
    page.write_text(page.read_text().replace("$R_ROWS", "${R_ROWS} ${R_ROWS}"))
    with pytest.raises(ValueError, match="tbody"):
        runtime.html("page.html", output="bad.html")
    page.write_text('<html><head>${VG2C_CSS}</head><body>ok</body></html>')
    with pytest.raises(ValueError, match="renderer-owned"):
        runtime.html("page.html", output="bad.html", values={"VG2C_CSS": "bad"})
    assert not (work / "bad.html").exists()
