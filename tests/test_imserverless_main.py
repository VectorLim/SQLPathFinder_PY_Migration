from __future__ import annotations

import csv
import json
from pathlib import Path

import main
from vg2c import compile_document


def test_handle_runs_script_from_env(monkeypatch, tmp_path: Path, capsys) -> None:
    workdir = tmp_path / "work"
    script = tmp_path / "job.txt"
    script.write_text(
        "<OPTIONS>\n/WRITE-FILE=Y\n/CSV=result.txt\n</OPTIONS>\nhello", encoding="utf-8"
    )
    monkeypatch.setenv("SCRIPT_PATH", str(script))
    monkeypatch.setenv("WORKDIR", str(workdir))
    monkeypatch.setenv("JOB_TIMEOUT", "60")

    success, message, _ = main.handle()

    assert success, message
    assert (workdir / "result.txt").read_text(encoding="utf-8") == "hello"
    record = json.loads(capsys.readouterr().out.strip().splitlines()[-1])
    assert record["success"] is True
    assert "result.txt" in record["generated_outputs"]


def test_handle_fails_without_script_path(monkeypatch) -> None:
    monkeypatch.delenv("SCRIPT_PATH", raising=False)

    success, message, _ = main.handle(body="ignored")

    assert not success
    assert "SCRIPT_PATH" in message


def test_handle_runs_uppercase_python_script_through_isolated_worker(
    monkeypatch, tmp_path: Path, capsys
) -> None:
    job = tmp_path / "job.txt"
    job.write_text(
        '<OPTIONS>\n/ENGINE=SQLite\n/TABLE=measurements.csv\n/CSV=result.csv\n/QUOTECSV=Y\n'
        '</OPTIONS>\nSELECT value FROM measurements',
        encoding="utf-8",
    )
    generated = tmp_path / "job.PY"
    generated.write_text(compile_document(job).emitted.source, encoding="utf-8")
    workdir = tmp_path / "work"
    workdir.mkdir()
    (workdir / "measurements.csv").write_text("value\n42\n", encoding="utf-8")
    monkeypatch.setenv("SCRIPT_PATH", str(generated))
    monkeypatch.setenv("WORKDIR", str(workdir))
    monkeypatch.setenv("JOB_TIMEOUT", "60")

    success, message, _ = main.handle()

    assert success, message
    with (workdir / "result.csv").open(encoding="utf-8-sig", newline="") as stream:
        assert list(csv.reader(stream)) == [["value"], ["42"]]
    records = [json.loads(line) for line in capsys.readouterr().out.splitlines()]
    record = records[-1]
    assert record["success"] is True
    assert "result.csv" in record["generated_outputs"]
