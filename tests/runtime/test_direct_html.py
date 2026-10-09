import csv

import pytest

from vg2c.runtime import csv_report, render_html


def shell(tmp_path, text):
    path = tmp_path / "shell.html"
    path.write_text(text)
    return path


@pytest.mark.parametrize("styles", [{"Column-Data": ["color:<<<COLOR>>>"]}, {"Column-Data": ["x:</style><script>x</script>"]}])
def test_style_updates_reject_dynamic_or_structural_css_before_output(tmp_path, styles):
    path = shell(tmp_path, '<html><head>${VG2C_CSS}</head><body></body></html>')
    with pytest.raises(ValueError, match="CSS is unsupported"):
        render_html(path, output="out.html", workdir=tmp_path, styles=styles)
    assert not (tmp_path / "out.html").exists()


def test_defer_layout_scope_edits_escaping_and_raw_markup(tmp_path):
    report = csv_report("<<<file>>>", columns=["NAME", "CE%", "EMPTY"])
    path = shell(tmp_path, '<html><head></head><body><b>Authored &amp; markup</b><p title="${TITLE}">${TITLE} $$</p><table><tbody>${R_ROWS}</tbody></table></body></html>')
    with (tmp_path / "data.csv").open("w", newline="") as stream:
        writer = csv.writer(stream)
        writer.writerow(["NAME", "CE%", "EMPTY"])
        writer.writerow(["<b>&nbsp;</b>", ".25", ""])
    out = render_html(path, output="out.html", workdir=tmp_path, reports={"R": report},
                      macros={"FILE": "data.csv"}, values={"TITLE": '"><b>&'})
    text = out.read_text()
    assert '<b>Authored &amp; markup</b>' in text
    assert '&lt;b&gt;&amp;nbsp;&lt;/b&gt;' in text
    assert '25.00%' in text
    assert '>&nbsp;</td>' in text
    assert '&quot;&gt;&lt;b&gt;&amp;' in text
    assert '${TITLE}' not in text
    assert ' $</p>' in text
    (tmp_path / "data.csv").write_text("NAME,CE%,EMPTY\nnew,.5,\n")
    path.write_text(path.read_text().replace("Authored", "Edited"))
    render_html(path, output="out.html", workdir=tmp_path, reports={"R": report},
                macros={"FILE": "data.csv"}, values={"TITLE": "safe"})
    assert 'Edited' in out.read_text() and 'new' in out.read_text() and '50.00%' in out.read_text()


@pytest.mark.parametrize("template", [
    '<p>${R_ROWS}</p>', '<table><tbody><tr>${R_ROWS}</tr></tbody></table>',
    '<script>${TEXT}</script>', '<style>${TEXT}</style>',
    '<a href="${TEXT}">link</a>', '<div style="${TEXT}"></div>',
    '<p title=${TEXT}>text</p>', '<p onclick="${TEXT}"></p>',
    '<${TEXT}>text</${TEXT}>', '<!--${TEXT}-->',
])
def test_unsupported_slot_contexts_fail_before_output(tmp_path, template):
    path = shell(tmp_path, template)
    with pytest.raises(ValueError):
        render_html(path, output="out.html", workdir=tmp_path, values={"TEXT": "value"},
                    reports={"R": csv_report("missing.csv", columns=["X"])})
    assert not (tmp_path / "out.html").exists()


def test_css_asset_edits_copy_each_render_and_embed(tmp_path):
    assets = tmp_path / "assets"
    assets.mkdir()
    work = tmp_path / "work"
    path = shell(assets, '<html><head>${VG2C_CSS}</head><body>content</body></html>')
    css = assets / "style.css"
    css.write_text("body { color:red; }")
    render_html(path, output="out.html", workdir=work, css_file="style.css")
    assert (work / "style.css").read_text() == css.read_text()
    assert 'href="style.css"' in (work / "out.html").read_text()
    css.write_text("body { color:blue; }")
    render_html(path, output="out.html", workdir=work, css_file="style.css")
    assert (work / "style.css").read_text() == "body { color:blue; }"
    render_html(path, output="embedded.html", workdir=work, css_file="style.css", embed_css=True)
    assert '<style' in (work / "embedded.html").read_text()
    assert 'href=' not in (work / "embedded.html").read_text()
    assert css.read_text() == "body { color:blue; }"


def test_missing_malformed_and_colliding_slots(tmp_path):
    path = shell(tmp_path, '<table><tbody>${R_ROWS}</tbody></table>')
    with pytest.raises(ValueError, match="Missing CSV"):
        render_html(path, output="out.html", workdir=tmp_path)
    with pytest.raises(ValueError, match="collide"):
        render_html(path, output="out.html", workdir=tmp_path, values={"R_ROWS": "raw"})
    path.write_text('<p>${MISSING}</p>')
    with pytest.raises(ValueError, match="Missing HTML"):
        render_html(path, output="out.html", workdir=tmp_path)
    path.write_text('<p>${BROKEN</p>')
    with pytest.raises(ValueError, match="Malformed"):
        render_html(path, output="out.html", workdir=tmp_path)
    assert not (tmp_path / "out.html").exists()


def test_raw_fallback_path_resolves_at_layout_time(tmp_path):
    report = csv_report("<<<INPUT>>>", columns=["X"], output_file="<<<OUTPUT>>>")
    path = shell(tmp_path, '<table><tbody>${R_ROWS}</tbody></table>')
    (tmp_path / "data.csv").write_text("X\nvalue\n")
    output = render_html(path, output="email:self", workdir=tmp_path, reports={"R": report},
                         macros={"INPUT": "data.csv", "OUTPUT": "results/out.html"})
    assert output == tmp_path / "results/out.html"
    assert 'value' in output.read_text()


@pytest.mark.parametrize("source", ['<p title="static" title=${VALUE}>x</p>', '<span${VALUE}>x</span>', '<!DOCTYPE ${VALUE}>'])
def test_dynamic_structural_edge_cases_rejected(tmp_path, source):
    path = shell(tmp_path, source)
    with pytest.raises(ValueError, match="unsupported|Unsupported|Dynamic|dynamic"):
        render_html(path, output="out.html", workdir=tmp_path, values={"VALUE": "x"})
    assert not (tmp_path / "out.html").exists()
