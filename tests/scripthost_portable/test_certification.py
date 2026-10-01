"""Representative source slices, always entering through the isolated worker."""

from __future__ import annotations

import csv
import json
import os
import re
import subprocess
import sys
from pathlib import Path

import pytest
from test_original_scripthost_runtime import block, outputs, script, vertical_slice

from scripthost_portable.worker import ScriptHostJob, run_job

ROOT = Path(__file__).resolve().parents[2]


def test_control_flow_through_worker(tmp_path):
    text = vertical_slice(tmp_path)
    text += "\n<---- New Query ---->\n" + script(
        block('/UTILITIES={SITE-LOOP} "A,B"'),
        block("/WRITE-FILE=Y", "/CSV=site_<<<spf-site-for-file-name>>>.txt", body="<<<spf-site>>>"),
        block("/UTILITIES={END-LOOP}"),
        block(
            f'/UTILITIES={{RUN-LOOP}} "{tmp_path / "seed.csv"}" "{tmp_path / "chunk.csv"}" "1" "N"'
        ),
        block("/UTILITIES={END-LOOP}"),
    )
    result = run_job(ScriptHostJob(working_directory=str(tmp_path), script_text=text), timeout=60)
    assert result.success, result
    actual = outputs(tmp_path)
    assert actual["verified.txt"].strip() == "verified"
    for i in range(3):
        assert actual[f"out_alpha_{i}.csv"].strip() == f"name,loop\nalpha,{i}"
    assert not (tmp_path / "bad.txt").exists()
    for site in ("A", "B"):
        assert (tmp_path / f"site_{site}.txt").read_text().strip() == site
    assert (tmp_path / "chunk.csv").read_text(encoding="utf-8-sig").strip() == "id,value\n2,b"


@pytest.mark.parametrize("fixture", ["html_test.txt", "tcb_yield.txt"])
def test_representative_original_reports_through_worker(tmp_path, fixture):
    if os.name == "nt" and fixture == "tcb_yield.txt":
        pytest.skip("Windows report preprocessing needs original CleanDelimsCRLF.exe")
    # Preserve actual report options, columns, sorting and layout. Substitute only
    # external CSV inputs, CSS/output paths, and email-only destination metadata.
    source = (ROOT / "tests/fixtures" / fixture).read_text(encoding="utf-8-sig")
    blocks = [s.strip() for s in source.split("<---- New Query ---->") if "/REPORT=" in s]
    (tmp_path / "schema").mkdir()
    separator = r"<\\>"
    markers = []
    for i, text in enumerate(blocks):
        if "/REPORT=HTML-DEFER" in text:
            fields = next(
                line for line in text.splitlines() if line.startswith("COLUMN-DATA")
            ).split(separator)
            columns = [v for v in fields[2:] if v]
            marker = f"CERTIFIED_REPORT_{i}"
            markers.append(marker)
            data = tmp_path / f"input_{i}.csv"
            with data.open("w", newline="", encoding="utf-8") as stream:
                writer = csv.writer(stream)
                writer.writerow(columns)
                writer.writerow([marker] + ["17"] * (len(columns) - 1))
            original = next(line for line in text.splitlines() if line.startswith("INPUT-FILE"))
            values = original.split(separator)
            values[1] = str(data)
            text = text.replace(original, separator.join(values))
        text = text.replace("sqlpathfinder_style_1.css", str(tmp_path / "report.css"))
        if "/REPORT=HTML-LAYOUT" in text:
            text = text.replace(":B:N", ":B:Y")
            text = re.sub(r"^:FILE:.*$", "", text, flags=re.M)
            text = re.sub(r"^:EM-[AS]:.*$", "", text, flags=re.M)
            text = text.replace("</OPTIONS>", "</OPTIONS>\n:FILE:" + str(tmp_path / "report.htm"))
        blocks[i] = text
    job = tmp_path / "report.spfsql"
    job.write_text(script(*blocks), encoding="utf-8")
    result = run_job(
        ScriptHostJob(working_directory=str(tmp_path), script_path=str(job)), timeout=60
    )
    assert result.success, result
    html = (tmp_path / "report.htm").read_text(encoding="utf-8-sig")
    assert all(marker in html for marker in markers), result.stdout + result.stderr
    assert "<table" in html.lower()
    assert "table.tblin" in (tmp_path / "report.css").read_text()
    assert not list(tmp_path.glob("*_tmp_.ini"))
    assert not list(tmp_path.glob("*_tmp_.htm"))


@pytest.mark.skipif(os.name == "nt", reason="Linux delivery boundary")
def test_original_email_role_verification_is_explicitly_unresolved(tmp_path):
    text = block(
        r'/UTILITIES=@EXEDIR@\SQLPathFinder_Email.va "" "self" "subject" "" "" "" "role" "Y" "N"'
    )
    result = run_job(ScriptHostJob(working_directory=str(tmp_path), script_text=text), timeout=30)
    assert not result.success
    assert "UNRESOLVED" in result.message + result.stdout + result.stderr


@pytest.mark.parametrize("exists", [True, False])
def test_thin_launcher_result_and_exit_code(tmp_path, exists):
    job = tmp_path / "job.spfsql"
    if exists:
        job.write_text(block("/WRITE-FILE=Y", "/CSV=result.txt", body="launcher"))
    result = subprocess.run(
        [
            sys.executable,
            "-m",
            "scripthost_portable.launcher",
            str(job),
            "--workdir",
            str(tmp_path),
            "--timeout",
            "30",
        ],
        capture_output=True,
        text=True,
        timeout=40,
    )
    payload = json.loads(result.stdout)
    assert payload["success"] is exists
    assert result.returncode == (0 if exists else 1)
    if exists:
        assert (tmp_path / "result.txt").read_text() == "launcher"


def test_real_22844_query_slice_through_worker(tmp_path, monkeypatch):
    from test_scripthost_query_transport import _aries_frame, _mars_frame, _real_22844_segments

    boundary = tmp_path / "transport"
    boundary.mkdir()
    _mars_frame().to_json(boundary / "mars.json", orient="table")
    _aries_frame().to_json(boundary / "aries.json", orient="table")
    (boundary / "datasyncx.py").write_text(
        "from pathlib import Path\nimport pandas as pd\n"
        "class MarsReader:\n"
        "    def read(self, *, site, query):\n"
        "        assert site == 'KM' and '@[]@' not in query\n"
        "        assert 'A15_PROD_21.F_LotHist' in query\n"
        "        return pd.read_json(Path(__file__).with_name('mars.json'), orient='table')\n"
        "class AriesReader:\n"
        "    def read(self, *, site, query):\n"
        "        assert site == 'KM' and 'SQL_Get_CSV_List' not in query\n"
        "        assert chr(39) + 'LOT_A' + chr(39) in query\n"
        "        return pd.read_json(Path(__file__).with_name('aries.json'), orient='table')\n"
    )
    monkeypatch.setenv("PYTHONPATH", str(boundary) + os.pathsep + os.environ.get("PYTHONPATH", ""))
    monkeypatch.setenv("SCRIPTHOST_FORCE_PORTABLE_QUERY_TRANSPORT", "1")
    job = tmp_path / "22844-query-slice.spfsql"
    job.write_text(script(*_real_22844_segments()), encoding="utf-8")
    result = run_job(
        ScriptHostJob(working_directory=str(tmp_path), script_path=str(job)), timeout=60
    )
    assert result.success, result
    with (tmp_path / "XRAY_results.csv").open(encoding="utf-8-sig", newline="") as stream:
        rows = [{k.upper(): v for k, v in row.items()} for row in csv.DictReader(stream)]
    diagnostics = result.stdout + result.stderr
    for path in tmp_path.glob("*.tab"):
        diagnostics += "\n" + path.name + "\n" + path.read_text(encoding="utf-8-sig")
    assert len(rows) == 1, diagnostics
    assert rows[0]["LOT"] == "LOT_A"
    assert rows[0]["VISUAL_ID"] == "VID_A"
    assert float(rows[0]["NUMERIC_VALUE_MAX"]) == 5.0
