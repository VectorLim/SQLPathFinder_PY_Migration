"""Opt-in live worker certification. Outputs stay local; evidence contains no row values."""

from __future__ import annotations

import argparse
import hashlib
import inspect
import json
import os
import platform
from importlib.metadata import version
from pathlib import Path

import pandas as pd

from scripthost_portable.worker import ScriptHostJob, run_job


def main() -> int:
    parser = argparse.ArgumentParser(__doc__)
    parser.add_argument("--output", type=Path, required=True)
    parser.add_argument("--timeout", type=float, default=240)
    args = parser.parse_args()
    output = args.output.resolve()
    output.mkdir(parents=True, exist_ok=False)
    from datasyncx import AriesReader, MarsReader, OracleReader

    evidence = {
        "platform": platform.platform(),
        "python": platform.python_version(),
        "datasyncx": version("datasyncx"),
        "entrypoint": "scripthost_portable.worker.run_job",
        "contract": {
            cls.__name__: {
                "constructor": str(inspect.signature(cls)),
                "read": str(inspect.signature(cls.read)),
            }
            for cls in (MarsReader, AriesReader, OracleReader)
        },
        "jobs": [],
        "outputs": [],
    }
    fixture = Path(__file__).resolve().parents[2] / "tests/fixtures/22844.spfsql"
    segments = fixture.read_text(encoding="utf-8-sig").split("<---- New Query ---->")
    selected = [
        next(s for s in segments if marker in s).strip()
        for marker in ("/NODE=KM.[A15_PROD_21.].MARS", "/NODE=KM.ARIES", "/CSV=XRAY_results.csv")
    ]
    os.environ["SCRIPTHOST_FORCE_PORTABLE_QUERY_TRANSPORT"] = "1"

    def execute(name: str, text: str) -> bool:
        result = run_job(
            ScriptHostJob(working_directory=str(output), script_text=text), timeout=args.timeout
        )
        evidence["jobs"].append(
            {
                "name": name,
                "success": result.success,
                "error_category": result.error_category,
                "child_pid": result.child_pid,
                "script_sha256": hashlib.sha256(text.encode()).hexdigest(),
            }
        )
        (output / f"{name}.log").write_text(
            result.message + "\n" + result.stdout + result.stderr, encoding="utf-8"
        )
        return result.success

    success = execute("22844-query-slice", "\n<---- New Query ---->\n".join(selected))
    if success:
        frames = []
        for name in ("yeuchuan_a0_22844.tab", "yeuchuan_a1_22844.tab", "XRAY_results.csv"):
            path = output / name
            frame = pd.read_csv(path, sep="\t" if path.suffix == ".tab" else ",", dtype=str)
            frame.columns = frame.columns.str.upper()
            frames.append(frame)
            evidence["outputs"].append(
                {
                    "file": name,
                    "rows": len(frame),
                    "columns": list(frame.columns),
                    "sha256": hashlib.sha256(path.read_bytes()).hexdigest(),
                }
            )
        mars, aries, sqlite = frames
        assert not mars.empty and not aries.empty and not sqlite.empty
        assert set(aries["LOT"].dropna()) <= set(mars["LOT_"].dropna())
        assert set(sqlite["LOT"].dropna()) <= set(aries["LOT"].dropna())
        assert set(sqlite["VISUAL_ID"].dropna()) <= set(aries["VISUAL_ID"].dropna())
        evidence["query_chain_relationships"] = True

        def query_block(filename: str, sql: str) -> str:
            return (
                "<OPTIONS>\n/NODE=KM.MARS\n/UN=//\n/PW=\n/OLEDB=SQLPlus\n"
                "/ENGINE=VA\n/WORKDIR=.\\\n/T=\n/CSV="
                + filename
                + "\n/HEADERS=fallback_name\n</OPTIONS>\n/*BEGIN SQL*/ "
                + sql
                + " /*END SQL*/"
            )

        success = execute(
            "empty", query_block("empty.tab", "SELECT 1 AS actual_label FROM dual WHERE 1=0")
        )
        if success:
            assert (output / "empty.tab").read_text(encoding="utf-8-sig").strip() == "FALLBACK_NAME"
            evidence["empty_header_fallback"] = True
        success = (
            execute(
                "null-unicode",
                query_block(
                    "probe.tab",
                    "SELECT CAST(NULL AS VARCHAR2(10)) AS null_value, '.' AS dot_value, UNISTR('\\00E9\\4E2D') AS unicode_value FROM dual",
                ),
            )
            and success
        )
        if success:
            lines = (output / "probe.tab").read_text(encoding="utf-8-sig").splitlines()
            assert lines == ["NULL_VALUE\tDOT_VALUE\tUNICODE_VALUE", "\t.\té中"]
            evidence["null_dot_unicode"] = True
    evidence["success"] = success
    (output / "evidence.json").write_text(json.dumps(evidence, indent=2), encoding="utf-8")
    print(json.dumps(evidence, indent=2))
    return 0 if success else 1


if __name__ == "__main__":
    raise SystemExit(main())
