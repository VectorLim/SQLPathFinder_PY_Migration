"""Execute real VG2 blocks through the original ScriptHost in isolated workers."""

from __future__ import annotations

import csv
import json
import os
import shutil
import sqlite3
import stat
import subprocess
import sys
import xml.etree.ElementTree as ET
import zipfile
from datetime import datetime, timedelta
from http.server import BaseHTTPRequestHandler, ThreadingHTTPServer
from pathlib import Path
from threading import Thread

import pytest

from scripthost_portable.worker import ScriptHostJob, run_job


def block(*options: str, body: str = "") -> str:
    return "<OPTIONS>\n" + "\n".join(options) + "\n</OPTIONS>\n" + body


def utility(name: str, *args: object) -> str:
    return block("/UTILITIES=" + name + " " + " ".join(f'"{arg}"' for arg in args))


def execute(root: Path, *blocks: str):
    result = run_job(
        ScriptHostJob(
            working_directory=str(root),
            script_text="\n<---- New Query ---->\n".join(blocks),
        ),
        timeout=30,
    )
    assert result.success, result.message + "\n" + result.stdout + result.stderr
    print(result.stdout)
    print(result.stderr)
    return result


def rows(path: Path):
    with path.open(encoding="utf-8-sig", newline="") as stream:
        return list(csv.reader(stream, delimiter="\t" if path.suffix == ".tab" else ","))


def test_write_rows_value_and_metadata(tmp_path: Path):
    source = tmp_path / "source.csv"
    source.write_text("ID,Value\n1,hello\n2,.\n", encoding="utf-8")
    os.utime(source, (1700000000, 1700000000))
    result = execute(
        tmp_path,
        utility("{ROWS-IN-FILE}", source, "AUDIT_ROWS", "N"),
        utility("{VALUE-IN-FILE}", source, "AUDIT_VALUE", "value"),
        utility("{AGE-OF-FILE}", source, "AUDIT_AGE", "Hours"),
        utility("{DATE-OF-FILE}", source, "AUDIT_DATE"),
        block(
            "/WRITE-FILE=Y", f"/CSV={tmp_path / 'result.txt'}", body="literal content<EOF>discard"
        ),
    )
    text = (tmp_path / "result.txt").read_text()
    assert text == "literal content"
    assert "Row Count is 2" in result.stdout
    assert "Value is hello" in result.stdout
    assert "2023" in result.stdout
    assert "AUDIT_ROWS" not in os.environ


@pytest.mark.parametrize("contents,expected", [("ID,Value\n", "EMPTY"), (None, "ERROR")])
def test_value_empty_and_missing(tmp_path: Path, contents, expected):
    source = tmp_path / "source.csv"
    if contents is not None:
        source.write_text(contents)
    result = execute(
        tmp_path,
        utility("{VALUE-IN-FILE}", source, "AUDIT_VALUE", "Value"),
        block("/WRITE-FILE=Y", f"/CSV={tmp_path / 'value.txt'}", body="%AUDIT_VALUE%"),
    )
    assert expected in result.stdout


def test_csv_html_xml(tmp_path: Path):
    source = tmp_path / "input.csv"
    source.write_text("First_Name,Value\nA&B,<hello>\nsecond,\n", encoding="utf-8")
    execute(
        tmp_path,
        utility(r"@EXEDIR@\CSVToHTML.va", source, tmp_path / "out.html", "Audit table"),
        utility(r"@EXEDIR@\CSVToXML.va", source, tmp_path / "out.xml", ""),
    )
    html = (tmp_path / "out.html").read_text(encoding="utf-8-sig")
    assert "Audit table" in html and "A&B" in html and "<hello>" in html
    xml = ET.parse(tmp_path / "out.xml").getroot()
    assert xml.tag == "Main"
    assert xml[0].findtext("First_Name") == "A&B"
    assert xml[1].findtext("Value") == "."


def test_zip_original(tmp_path: Path):
    source = tmp_path / "input.txt"
    source.write_text("archive content", encoding="utf-8")
    execute(tmp_path, utility(r"@EXEDIR@\SPFZIP.va", source, tmp_path / "out.zip", "N"))
    with zipfile.ZipFile(tmp_path / "out.zip") as archive:
        assert archive.namelist() == ["input.txt"]
        assert archive.read("input.txt") == b"archive content"
    assert source.exists()


def test_wait_existing_and_zero_interval(tmp_path: Path):
    source = tmp_path / "ready"
    source.touch()
    execute(
        tmp_path,
        utility(r"@EXEDIR@\WaitFile.va", source, "1"),
        utility(r"@EXEDIR@\WaitInterval.va", "0"),
    )


def test_get_files(tmp_path: Path):
    folder = tmp_path / "input"
    folder.mkdir()
    (folder / "a.txt").write_text("a")
    (folder / "sub").mkdir()
    (folder / "sub" / "b.txt").write_text("b")
    execute(tmp_path, utility("{GET-FILES}", folder, "Y", tmp_path / "files.tab", "N"))
    text = (tmp_path / "files.tab").read_text(encoding="utf-8-sig")
    assert "a.txt" in text and "b.txt" in text


def test_file_compare(tmp_path: Path):
    a, b = tmp_path / "a.txt", tmp_path / "b.txt"
    a.write_text("same\n")
    b.write_text("same\n")
    execute(tmp_path, utility("{FILE-COMPARE}", a, b, "N", "N", "10", "", "N"))


def test_stack_data(tmp_path: Path):
    a, b = tmp_path / "a.csv", tmp_path / "b.csv"
    a.write_text("ID,A\n2,x\n")
    b.write_text("ID,B\n1,y\n")
    execute(
        tmp_path,
        block("/STACK=Y", f"/TABLE={a},{b}", f"/CSV={tmp_path / 'stack.csv'}", "/SORT=ID ASC-1"),
    )
    result = rows(tmp_path / "stack.csv")
    assert [v.upper() for v in result[0]] == ["ID", "A", "B"]
    assert [r[0] for r in result[1:]] == ["1", "2"]


def test_retired_entrypoint_cannot_execute(tmp_path: Path):
    script = tmp_path / "job.spfsql"
    script.write_text(block("/WRITE-FILE=Y", f"/CSV={tmp_path / 'unwanted.txt'}", body="bad"))
    result = subprocess.run(
        [sys.executable, "-m", "vg2c_new", str(script)], capture_output=True, text=True
    )
    assert result.returncode == 2
    assert "scripthost_portable.worker.run_job" in result.stderr
    assert not (tmp_path / "unwanted.txt").exists()


def test_smart_append_v4(tmp_path: Path):
    old, new = tmp_path / "old.csv", tmp_path / "new.csv"
    old.write_text("ID,DATE,OLD\n1,2026-01-01 00:00:00,a\n2,2026-09-01 00:00:00,b\n")
    new.write_text("ID,DATE,NEW\n2,2026-09-02 00:00:00,x\n3,2026-09-03 00:00:00,y\n")
    execute(
        tmp_path,
        utility(
            r"@EXEDIR@\SmartAppend.va",
            old,
            new,
            "DATE",
            "2026-02-01 00:00:00",
            "ID",
            "",
            "Y",
            "",
            "VERSION4",
            "",
            "missing",
            "",
            "Y",
        ),
    )
    result = rows(old)
    assert result[0] == ["ID", "DATE", "NEW", "OLD"]
    assert [r[0] for r in result[1:]] == ["2", "3"]
    assert [r[3] for r in result[1:]] == ["", ""]  # Original V4 ignores MyDefNew here.


@pytest.mark.parametrize("pyscript", [False, True])
def test_python_original_launch(tmp_path: Path, pyscript: bool):
    script = tmp_path / "script with spaces.py"
    script.write_text(
        "import json,sys\nfrom pathlib import Path\n"
        "Path('argv.json').write_text(json.dumps(sys.argv[1:]))\n"
    )
    # Existing ScriptHost interpreter configuration; no replacement task needed.
    (tmp_path / "SQLPathFinder.ini").write_text(f"[SQLPATHFINDER]\nPYTHON3={sys.executable}\n")
    command = (
        utility("{PYSCRIPT}", script, "two words", "tail")
        if pyscript
        else utility(r"@EXEDIR@\Run_Python_Script.va", script, "one two", "N", "", "Python-v3")
    )
    execute(tmp_path, command)
    args = json.loads((tmp_path / "argv.json").read_text())
    assert args == (["two words", "tail", "/SPFLOGLEVEL=ERROR"] if pyscript else ["one", "two"])


def test_append_rename_delete(tmp_path: Path):
    dest, source = tmp_path / "dest.csv", tmp_path / "source.csv"
    dest.write_text("ID\n1\n")
    source.write_text("ID\n2\n")
    renamed = tmp_path / "renamed.csv"
    execute(
        tmp_path,
        utility(r"@EXEDIR@\AppendFile.va", dest, source, "Y"),
        utility(r"@EXEDIR@\SPFRename.va", dest, renamed),
    )
    assert rows(renamed) == [["ID"], ["1"], ["2"]]
    assert not dest.exists()
    execute(tmp_path, utility(r"@EXEDIR@\SPFDelete.bat", renamed, "N"))
    assert not renamed.exists()


@pytest.mark.skipif(os.name == "nt", reason="POSIX replacement for Windows unzip.exe")
@pytest.mark.parametrize("preserve", ["Y", "N"])
def test_unzip_original_task(tmp_path: Path, preserve):
    (tmp_path / "out").mkdir()  # Original task requires the destination to exist.
    archive = tmp_path / "input.zip"
    with zipfile.ZipFile(archive, "w") as writer:
        writer.writestr("nested/value.txt", "content")
    execute(tmp_path, utility(r"@EXEDIR@\SPFUNZIP.va", archive, tmp_path / "out", preserve))
    target = tmp_path / "out" / ("nested/value.txt" if preserve == "Y" else "value.txt")
    assert target.read_text() == "content"


@pytest.mark.parametrize(
    "member", ["../escape.txt", "/absolute.txt", r"C:\escape.txt", r"..\escape.txt"]
)
def test_unzip_rejects_escape_before_extracting(tmp_path: Path, member):
    from scripthost_portable.file_operations import unzip_file

    archive = tmp_path / "bad.zip"
    with zipfile.ZipFile(archive, "w") as writer:
        writer.writestr("good.txt", "good")
        writer.writestr(member, "bad")
    with pytest.raises(ValueError, match="Unsafe ZIP member"):
        unzip_file(str(archive), str(tmp_path / "out"), True)
    assert not (tmp_path / "out").exists()


@pytest.mark.skipif(os.name == "nt", reason="POSIX replacement for COPY")
def test_copy_original_distribution_tokens(tmp_path: Path):
    source, dest = tmp_path / "source", tmp_path / "dest"
    source.mkdir()
    dest.mkdir()
    (source / "old.txt").write_text("old")
    (source / "new.txt").write_text("new")
    os.utime(source / "old.txt", (1000000000, 1000000000))
    execute(
        tmp_path,
        utility(r"@EXEDIR@\SPFCopy.bat", str(source / "<file-datelastmodified>"), dest, "N"),
    )
    assert sorted(p.name for p in dest.iterdir()) == ["new.txt"]
    execute(tmp_path, utility(r"@EXEDIR@\SPFCopy.bat", source / "*.txt", dest, "N"))
    assert (dest / "old.txt").read_text() == "old"


@pytest.mark.skipif(os.name == "nt", reason="POSIX requests boundary, no Windows SSPI")
def test_web_v2_original_local_http(tmp_path: Path):
    class Handler(BaseHTTPRequestHandler):
        def do_GET(self):
            self.send_response(200 if self.path == "/ok" else 404)
            self.end_headers()
            self.wfile.write("hello café".encode())

        def log_message(self, *args):
            pass

    server = ThreadingHTTPServer(("127.0.0.1", 0), Handler)
    thread = Thread(target=server.serve_forever, daemon=True)
    thread.start()
    try:
        base = f"http://127.0.0.1:{server.server_port}"
        execute(
            tmp_path,
            utility(
                r"@EXEDIR@\Get_Web_Text.exe",
                base + "/ok",
                tmp_path / "web.txt",
                "N",
                "N",
                "Version 2",
                "2",
            ),
        )
        assert (tmp_path / "web.txt").read_bytes() == "hello café".encode()
        execute(
            tmp_path,
            utility(
                r"@EXEDIR@\Get_Web_Text.exe",
                base + "/missing",
                tmp_path / "missing.txt",
                "N",
                "Y",
                "Version 2",
                "2",
            ),
        )
        assert not (tmp_path / "missing.txt").exists()
    finally:
        server.shutdown()
        server.server_close()
        thread.join()


def test_wait_poll_count_original(tmp_path: Path, monkeypatch):
    from SPFLib.SPFUtilities import utils

    from scripthost_portable.runtime import _spf_manager_type

    sleeps = []
    monkeypatch.setattr(utils.time, "sleep", sleeps.append)
    manager = _spf_manager_type()()
    manager.WaitFile(str(tmp_path / "missing"), "3")
    assert sleeps == [10, 10, 10]


def test_xlsx_to_csv_original(tmp_path: Path):
    from openpyxl import Workbook

    book = Workbook()
    sheet = book.active
    sheet.title = "Data"
    sheet.append(["ID", "Value"])
    sheet.append(["001", "hello"])
    book.save(tmp_path / "input.xlsx")
    execute(
        tmp_path,
        utility(
            r"@EXEDIR@\XLSToCSV.va",
            tmp_path / "input.xlsx",
            tmp_path / "output.csv",
            "Data",
            "0",
            "0",
            "N",
            "N",
        ),
    )
    assert rows(tmp_path / "output.csv") == [["ID", "Value"], ["001", "hello"]]


@pytest.mark.skipif(os.name == "nt", reason="POSIX replacement for RoboCopy")
def test_robocopy_original_task(tmp_path: Path):
    source = tmp_path / "source"
    (source / "nested" / "empty").mkdir(parents=True)
    (source / "nested" / "data.csv").write_text("ID\n1\n")
    dest = tmp_path / "destination"
    execute(
        tmp_path,
        utility(r"@EXEDIR@\RoboCopy.va", "*.csv", source, dest, "0", "0", "N", "/E /MOV /NP", "N"),
    )
    assert (dest / "nested" / "data.csv").read_text() == "ID\n1\n"
    assert (dest / "nested" / "empty").is_dir()
    assert not (source / "nested" / "data.csv").exists()


@pytest.mark.skipif(os.name == "nt", reason="POSIX replacement for spfExcelUtility.exe")
def test_excel_load_import_original_tasks(tmp_path: Path):
    from openpyxl import load_workbook

    data = tmp_path / "input.csv"
    data.write_text("ID,Value\n001,hello\n002,\n")
    template = tmp_path / "template.xlsx"
    execute(tmp_path, utility(r"@EXEDIR@\LoadExcel.va", data, template, "Version 2", "N"))
    book = load_workbook(template)
    assert list(book.active.values) == [("ID", "Value"), ("001", "hello"), ("002", None)]
    book.close()
    result = tmp_path / "result.xlsx"
    execute(
        tmp_path,
        utility(
            r"@EXEDIR@\ImportExcel.va", template, result, data, "Imported", "", "", "Version 2", "N"
        ),
    )
    book = load_workbook(result)
    assert book.sheetnames == ["Sheet1", "Imported"]
    assert list(book["Imported"].values) == [("ID", "Value"), ("001", "hello"), ("002", None)]
    book.close()


@pytest.mark.skipif(os.name == "nt", reason="Linux R interpreter validation")
@pytest.mark.parametrize("inline", [False, True])
def test_r_original_local_interpreter(tmp_path: Path, inline):
    r = shutil.which("R")
    assert r, "Install R for the supported Linux validation suite"
    (tmp_path / "SQLPathFinder.ini").write_text(f"[SQLPATHFINDER]\nRTERM={r}\n")
    body = 'writeLines("original R", "r-output.txt")'
    script = tmp_path / "input.R"
    script.write_text(body)
    command = (
        block("/RSCRIPT=Y", body=body)
        if inline
        else utility(r"@EXEDIR@\Run_R_Script.va", script, "", "N", "")
    )
    execute(tmp_path, command)
    assert (tmp_path / "r-output.txt").read_text().strip() == "original R"


@pytest.mark.skipif(os.name == "nt", reason="POSIX replacement for Excel XML conversion")
def test_xml_to_csv_original_roundtrip(tmp_path: Path):
    source = tmp_path / "input.csv"
    source.write_text("ID,Value\n001,A&B\n002,\n", encoding="utf-8")
    execute(
        tmp_path,
        utility(r"@EXEDIR@\CSVToXML.va", source, tmp_path / "out.xml", ""),
        utility(r"@EXEDIR@\XMLToCSV.va", tmp_path / "out.xml", tmp_path / "back.tab"),
    )
    assert rows(tmp_path / "back.tab") == [["ID", "Value"], ["001", "A&B"], ["002", "."]]


def test_xml_converter_rejects_unproven_shape(tmp_path: Path):
    from scripthost_portable.file_operations import xml_to_csv

    source = tmp_path / "spreadsheet.xml"
    source.write_text("<Workbook><Worksheet /></Workbook>")
    with pytest.raises(ValueError, match="Main/Item"):
        xml_to_csv(str(source), str(tmp_path / "out.csv"), ",")
    assert not (tmp_path / "out.csv").exists()


def test_email_original_task_keeps_role_and_recipient_policy(tmp_path: Path, monkeypatch):
    from scripthost_portable import PortableScriptHostRuntime
    from scripthost_portable.runtime import _spf_manager_type

    _spf_manager_type()
    from SPFLib.SPFSQL3 import EmailTask

    calls = []
    monkeypatch.setattr(EmailTask, "SPFEmail", lambda self, *args: calls.append(args))
    text = utility(
        r"@EXEDIR@\SQLPathFinder_Email.va",
        "attachment.csv",
        "person@example.test",
        "subject",
        "body.txt",
        "cc@example.test",
        "bcc@example.test",
        "role",
        "Y",
        "N",
    )
    assert PortableScriptHostRuntime().run_text(text, tmp_path)
    assert len(calls) == 1
    assert calls[0][1:9] == (
        "attachment.csv",
        "person@example.test",
        "subject",
        "body.txt",
        "cc@example.test",
        "bcc@example.test",
        "role",
        True,
    )


@pytest.mark.skipif(os.name == "nt", reason="POSIX file mode replacement for Windows attributes")
def test_readonly_original_task(tmp_path: Path):
    path = tmp_path / "file.txt"
    path.write_text("read only")
    execute(tmp_path, utility(r"@EXEDIR@\SetFileRO.va", path, "READONLY"))
    assert not path.stat().st_mode & (stat.S_IWUSR | stat.S_IWGRP | stat.S_IWOTH)
    execute(tmp_path, utility(r"@EXEDIR@\SetFileRO.va", path, "READWRITE"))
    assert path.stat().st_mode & stat.S_IWUSR


def test_echo_original(tmp_path: Path):
    result = execute(tmp_path, utility("@Echo", "original echo"))
    assert "original echo" in result.stdout


def test_gmt_update_time_original(tmp_path: Path):
    output = tmp_path / "time.csv"
    execute(tmp_path, utility("{GET-SITE-TIME}", "GMT"), utility("{UPDATE-TIME}", output))
    data = rows(output)
    values = dict(zip(data[0], data[1], strict=True))
    actual = datetime.strptime(values["last_Date"], "%Y-%m-%d %H:%M:%S")
    assert datetime.strptime(values["Last_Date-15m"], "%Y-%m-%d %H:%M:%S") == actual - timedelta(
        minutes=15
    )


def test_sqlite_load_delete_original(tmp_path: Path):
    source = tmp_path / "input.csv"
    source.write_text("ID,Value\n1,hello\n")
    database = tmp_path / "loaded.sdb"
    execute(tmp_path, utility(r"@EXEDIR@\SQLite-Load.va", source, database, "", "N"))
    with sqlite3.connect(database) as connection:
        tables = [
            row[0]
            for row in connection.execute("SELECT name FROM sqlite_master WHERE type='table'")
        ]
        assert tables
        assert any(
            connection.execute('SELECT COUNT(*) FROM "' + name.replace('"', '""') + '"').fetchone()[
                0
            ]
            == 1
            for name in tables
        )
    connection.close()  # sqlite3 context managers commit; they do not close handles.
    execute(tmp_path, utility(r"@EXEDIR@\SQLiteDelete.va", database))
    assert not database.exists()


@pytest.mark.parametrize("continue_on_error", ["N", "Y"])
def test_file_compare_mismatch_original(tmp_path: Path, continue_on_error):
    a, b = tmp_path / "a.txt", tmp_path / "b.txt"
    a.write_text("one\n")
    b.write_text("two\n")
    os.utime(a, (1000000000, 1000000000))
    os.utime(b, (1000000010, 1000000010))
    result = run_job(
        ScriptHostJob(
            working_directory=str(tmp_path),
            script_text=utility("{FILE-COMPARE}", a, b, "N", continue_on_error, "10", "", "N"),
        ),
        timeout=30,
    )
    assert result.success is (continue_on_error == "Y"), result


def test_zip_folder_delete_original(tmp_path: Path):
    folder = tmp_path / "folder"
    (folder / "nested").mkdir(parents=True)
    (folder / "nested" / "file.txt").write_text("content")
    execute(tmp_path, utility(r"@EXEDIR@\SPFZIP.va", folder, tmp_path / "folder.zip", "Y"))
    assert not folder.exists()
    with zipfile.ZipFile(tmp_path / "folder.zip") as archive:
        assert archive.read("nested/file.txt") == b"content"


def test_time_file_persistence_original(tmp_path: Path):
    first, second = tmp_path / "first.csv", tmp_path / "second.csv"

    def task(name, arg):
        return utility(name, arg).replace("<OPTIONS>", "<OPTIONS>\n/INSTANCE=4242")

    execute(tmp_path, task("{GET-SITE-TIME-FILE}", "GMT"), task("{UPDATE-TIME}", first))
    assert list(tmp_path.glob("*.spf$data"))
    execute(tmp_path, task("{UPDATE-TIME-FILE}", second))
    assert rows(first) == rows(second)
