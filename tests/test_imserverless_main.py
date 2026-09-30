from __future__ import annotations

import json
from pathlib import Path

import main


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
