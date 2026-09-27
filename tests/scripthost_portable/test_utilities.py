"""Execute real VG2 blocks through the original ScriptHost in isolated workers."""

from __future__ import annotations

import csv
import os
import subprocess
import sys
import xml.etree.ElementTree as ET
import zipfile
from pathlib import Path

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
