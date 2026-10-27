"""Editable WRITE-FILE sources survive repeated, isolated invocations."""

import sys
from types import SimpleNamespace

import pytest

from vg2c import translate
from vg2c.runtime import DuckDbReader, JobRuntime, SqliteReader
from tests.fixtures.generated_job import capture_jobs


def block(options, body=""):
    return f"<OPTIONS>\n{options}\n</OPTIONS>\n{body}\n<---- New Query ---->\n"


def test_emitted_files_keep_names_templates_and_execution_order(tmp_path):
    source = tmp_path / "example.txt"
    source.write_text(
        block('/WRITE-FILE=Y\n/CSV=config.csv', "NAME,DEST\nfirst,nested/result.csv")
        + block('/UTILITIES={START-MACRO} "config.csv" "N"')
        + block('/WRITE-FILE=Y\n/CSV=<<<DEST>>>', "VALUE\n<<<NAME>>>")
        + block('/WRITE-FILE=Y\n/CSV=same.txt', "first")
        + block('/WRITE-FILE=Y\n/CSV=same.txt', "<<<NAME>>>")
        + block('/UTILITIES={END-MACRO}')
    )
    main = translate(source)
    assets = {str(p.relative_to(main.parent)): p.read_bytes()
              for p in (main.parent / "files").rglob("*") if p.is_file()}
    assert len(assets) == 4
    assert (main.parent / "files/config.csv").read_text().startswith("NAME,DEST")
    assert len(list((main.parent / "files").glob("step_*/same.txt"))) == 2
    assert "job.write_file(" not in main.read_text()
    assert "job.update_file(" in main.read_text()
    assert "<<<NAME>>>" not in main.read_text()
    namespace = {"__file__": str(main), "__name__": "test_job"}
    exec(main.read_text(), namespace)
    capture_jobs(namespace)
    for name in ("first", "second"):
        (main.parent / "files/config.csv").write_text(f"NAME,DEST\n{name},nested/result.csv")
        namespace["run"]()
        job = namespace["_job"]
        assert job.read_file("nested/result.csv").strip() == f"VALUE\n{name}"
        assert job.read_file("same.txt").strip() == name
        assert not job.workdir.exists()
        for path, content in assets.items():
            if path != str(main.parent.joinpath("files/config.csv").relative_to(main.parent)):
                assert (main.parent / path).read_bytes() == content


@pytest.mark.parametrize("reader", [SqliteReader, DuckDbReader])
def test_macro_file_is_visible_to_queries_and_csv_helpers(tmp_path, reader):
    (tmp_path / "template.csv").write_text("VALUE\n<<<VALUE>>>\n")
    (tmp_path / "query.sql").write_text(
        "select * from <DDBTable>T0</DDBTable>" if reader is DuckDbReader else "select * from T0"
    )
    job = JobRuntime(tmp_path, tmp_path / "work", initial_macros={"VALUE": "42"})
    job.update_file("data.csv", "template.csv")
    assert job.row_count("data.csv") == 1
    assert job.read_macro_row("data.csv") == {"VALUE": "42"}
    assert job.wait_file("data.csv", timeout=0)
    result = job.sql("query.sql", reader=reader(), output="result.csv", inputs=[("data.csv", "T0")])
    assert str(result.iloc[0, 0]) == "42"
    assert not (job.workdir / "data.csv").exists()
    assert (tmp_path / "template.csv").read_text() == "VALUE\n<<<VALUE>>>\n"


def test_copy_rename_delete_and_stale_output(tmp_path):
    work = tmp_path / "work"
    work.mkdir()
    (work / "a.txt").write_text("stale")
    template = tmp_path / "template.txt"
    template.write_text("<<<VALUE>>>")
    job = JobRuntime(tmp_path, work, initial_macros={"VALUE": "fresh"})
    job.update_file("a.txt", "template.txt")
    job.copy_file("a.txt", "export.txt")
    assert (work / "export.txt").read_text() == "fresh"
    job.rename_file("a.txt", "b.txt")
    assert job.read_file("b.txt") == "fresh"
    assert not job.wait_file("a.txt", timeout=0)
    job.delete_files(["b.txt"])
    assert not job.wait_file("b.txt", timeout=0)
    assert template.read_text() == "<<<VALUE>>>"


def test_memory_email_body_and_external_boundary(tmp_path, monkeypatch):
    calls = []
    monkeypatch.setitem(sys.modules, "datasyncx", SimpleNamespace(send_msg=lambda **kw: calls.append(kw)))
    (tmp_path / "body.htm").write_text("<p><<<VALUE>>></p>")
    job = JobRuntime(tmp_path, tmp_path / "work", environ={"ENV_MODE": "prod"},
                     initial_macros={"VALUE": "fresh"})
    job.update_file("body.htm", "body.htm")
    job.send_mail("user@example.test", "subject", "body.htm")
    assert calls[0]["mail_content"] == "<p>fresh</p>"
    assert calls[0]["format_"] == "html"
    with pytest.raises(ValueError, match="disk file"):
        job.send_mail("user@example.test", "subject", "body", attachments=["body.htm"])
    with pytest.raises(ValueError, match="disk file"):
        job.run_program(["tool", "body.htm"])
    assert not job.workdir.exists()


def test_macro_failure_does_not_publish_partial_content(tmp_path):
    (tmp_path / "template.txt").write_text("<<<MISSING>>>")
    job = JobRuntime(tmp_path, tmp_path / "work")
    with pytest.raises(ValueError, match="Unknown value"):
        job.update_file("out.txt", "template.txt")
    assert job.files == {}
    assert (tmp_path / "template.txt").read_text() == "<<<MISSING>>>"


def test_disk_operations_replace_aliases_and_wait_sees_external_files(tmp_path):
    (tmp_path / "template.csv").write_text("VALUE\nold\n")
    job = JobRuntime(tmp_path, tmp_path / "work")
    job.load_file("out.csv", "template.csv")
    job.write_file("<<<DEST>>>", "VALUE\nnew\n", vars={"DEST": "out.csv"})
    assert job.read_file("out.csv") == "VALUE\nnew\n"
    job.load_file("chunk.csv", "template.csv")
    for _ in job.csv_chunks("out.csv", "chunk.csv", 1):
        assert job.read_file("chunk.csv") == "VALUE\nnew\n"
    assert not job.wait_file("chunk.csv", timeout=0)
    job.delete_files(["out.csv"])
    (job.workdir / "out.csv").write_text("external")
    assert job.wait_file("out.csv", timeout=0)
    assert job.read_file("out.csv") == "external"


def test_memory_csv_and_stylesheet_are_visible_to_html(tmp_path):
    from vg2c.runtime import csv_report

    (tmp_path / "template.csv").write_text("VALUE\n<<<VALUE>>>\n")
    (tmp_path / "template.css").write_text("p {color: <<<COLOR>>>;}")
    (tmp_path / "page.html").write_text("<html><head>${VG2C_CSS}</head><body><table><tbody>${R_ROWS}</tbody></table></body></html>")
    job = JobRuntime(tmp_path, tmp_path / "work", initial_macros={"VALUE": "fresh", "COLOR": "blue"})
    job.update_file("data.csv", "template.csv")
    job.update_file("style.css", "template.css")
    job.reports["R"] = csv_report("data.csv", columns=["VALUE"])
    result = job.html("page.html", output="out.html", css_file="style.css", embed_css=True)
    assert "fresh" in result.read_text()
    assert "p {color: blue;}" in result.read_text()
    assert (tmp_path / "template.css").read_text() == "p {color: <<<COLOR>>>;}"


def test_static_alias_rename_preserves_logical_extension(tmp_path):
    (tmp_path / "template.csv").write_text("A|B\n1|2\n")
    job = JobRuntime(tmp_path, tmp_path / "work")
    job.load_file("data.csv", "template.csv")
    job.rename_file("data.csv", "data.asc")
    assert job.row_count("data.asc") == 1
    assert (tmp_path / "template.csv").read_text() == "A|B\n1|2\n"


def test_crosstab_output_replaces_old_header_alias(tmp_path):
    from vg2c.runtime.crosstab import _CrosstabUtility

    (tmp_path / "old.ini").write_text("stale")
    job = JobRuntime(tmp_path, tmp_path / "work")
    job.load_file("1_A.ini", "old.ini")
    _CrosstabUtility.write_header_list(job.workdir, "a,1", ["fresh"], files=job.files)
    assert job.read_file("1_A.ini") == "fresh"


def test_external_program_cannot_modify_static_template(tmp_path, monkeypatch):
    calls = []
    (tmp_path / "config.csv").write_text("VALUE\n42\n")
    job = JobRuntime(tmp_path, tmp_path / "work")
    job.load_file("config.csv", "config.csv")
    def external(argv, **kwargs):
        calls.append(argv)
        assert (job.workdir / "config.csv").read_text() == "VALUE\n42\n"
        (job.workdir / "config.csv").write_text("changed by external command")
        return SimpleNamespace(returncode=0)

    monkeypatch.setattr("vg2c.runtime.files.subprocess.run", external)
    job.run_program(["tool", "config.csv"])
    assert calls == [["tool", str(job.workdir / "config.csv")]]
    assert job.read_file("config.csv") == "changed by external command"
    assert (tmp_path / "config.csv").read_text() == "VALUE\n42\n"


def test_rendered_outputs_replace_file_aliases(tmp_path):
    (tmp_path / "old.txt").write_text("stale")
    (tmp_path / "page.html").write_text("<html><head>${VG2C_CSS}</head><body>fresh</body></html>")
    (tmp_path / "style.css").write_text("p {color: blue;}")
    job = JobRuntime(tmp_path, tmp_path / "work")
    job.load_file("out.html", "old.txt")
    job.load_file("style.css", "old.txt")
    job.define_css("style.css", "style.css")
    assert job.read_file("style.css") == "p {color: blue;}"
    job.html("page.html", output="out.html")
    assert "fresh" in job.read_file("out.html")
    assert (tmp_path / "old.txt").read_text() == "stale"
