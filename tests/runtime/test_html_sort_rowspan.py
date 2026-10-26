"""Native SORT declarations retain hierarchy and adjacent merged report cells."""

from html import escape
import json
import re

import pytest

from vg2c import translate
from vg2c.runtime import JobRuntime, csv_report
from vg2c.utilities.html_report import HtmlReport


def test_generated_sort_and_hierarchical_rowspan(tmp_path):
    delimiter = r"<\\>"
    rows = [
        ["TYPE", "HTML"], ["INPUT-FILE", "data.csv"],
        ["SORT", "time", "Asc", "N"], ["SORT", "lot", "Asc", "Y"],
        ["SORT", "grouping", "Asc", "Y"], ["SORT", "theta", "Desc", "Y"],
        ["COLUMN-DATA", "", "time", "lot", "grouping", "theta", "vid"],
    ]
    source = tmp_path / "report.txt"
    source.write_text(
        "<OPTIONS>\n/REPORT=HTML-DEFER\n/ID=R\n</OPTIONS>\n"
        + "\n".join(delimiter.join(row) for row in rows)
        + "\n<---- New Query ---->\n<OPTIONS>\n/REPORT=HTML-LAYOUT\n</OPTIONS>\n"
        + ":FILE:out.html\nHTM:R\n"
    )
    main = translate(source)
    work = main.parent / "output"
    work.mkdir()
    (work / "data.csv").write_text(
        "time,lot,grouping,theta,vid\n"
        "T2,A,X,1,V5\nT1,B,X,1,V4\nT1,A,Y,1,V3\n"
        "T1,A,X,10,V2\nT1,A,X,2,<V1>\nT1,A,X,2,V0\n"
    )
    namespace = {"__file__": str(main), "__name__": "generated_test"}
    exec(compile(main.read_text(), str(main), "exec"), namespace)
    namespace["run"]()
    output = (work / "out.html").read_text()
    body = re.search(r"<tbody>(.*?)</tbody>", output, re.S).group(1)
    assert body.count("<tr>") == 6
    assert [body.index(value) for value in ("&lt;V1&gt;", "V0", "V2", "V3", "V4", "V5")] == sorted(
        body.index(value) for value in ("&lt;V1&gt;", "V0", "V2", "V3", "V4", "V5")
    )
    assert 'rowspan="4"' in body  # lot A within T1, not across T2
    assert 'rowspan="3"' in body  # grouping X within T1/A
    assert 'rowspan="2"' in body  # theta 2 within T1/A/X
    assert body.count(">T1</td>") == 5  # nonmerged parent still bounds child spans
    assert body.count(">A</td>") == 2
    assert body.count(">X</td>") == 3
    assert body.index(">2</td>") < body.index(">10</td>")  # native CSV text order


@pytest.mark.parametrize("sorting", ["invalid-json", {}, [["x", "SIDEWAYS", True]],
                                     [["x", "ASC", "Y"]], [["missing", "ASC", True]],
                                     [["x", "ASC", True], ["X", "DESC", False]]])
def test_invalid_editable_sort_fails_without_output(tmp_path, sorting):
    value = sorting if isinstance(sorting, str) else json.dumps(sorting)
    (tmp_path / "shell.html").write_text(
        '<table data-report="R" data-sort="' + escape(value, quote=True) + '">'
        '<thead><tr><th data-field="x">X</th></tr></thead><tbody></tbody></table>'
    )
    (tmp_path / "data.csv").write_text("x\n1\n")
    job = JobRuntime(tmp_path, tmp_path)
    job.reports["R"] = csv_report("data.csv")
    with pytest.raises(ValueError, match="data-sort"):
        job.html("shell.html", output="out.html")
    assert not (tmp_path / "out.html").exists()


def test_sort_rejects_nonempty_extra_fields():
    with pytest.raises(ValueError, match="SORT accepts only"):
        HtmlReport.parse_options(r"SORT<\\>lot<\\>Asc<\\>Y<\\>unexpected")
