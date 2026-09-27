"""Opt-in live 22844 validation; run in a fresh process with --output DIR.

Outputs may contain corporate query data. Keep the output directory local.
No credentials, SQL text, or row values are included in evidence.json.
"""

from __future__ import annotations

import argparse
import codecs
import hashlib
import inspect
import json
import os
import platform
from pathlib import Path
from unittest.mock import patch

from scripthost_portable import PortableOracleConnection, PortableScriptHostRuntime
from scripthost_portable.query_transport import DataSyncXReaderFactory, use_reader_factory
from scripthost_portable.runtime import _spf_manager_type


def main():
    parser = argparse.ArgumentParser(__doc__)
    parser.add_argument("--output", type=Path, required=True)
    args = parser.parse_args()
    output = args.output.resolve()
    output.mkdir(parents=True, exist_ok=False)
    import datasyncx

    evidence = {
        "pid": os.getpid(),
        "platform": platform.platform(),
        "python": platform.python_version(),
        "datasyncx": datasyncx.version,
        "calls": [],
        "steps": [],
    }
    _spf_manager_type()
    import SPFLib.SPFSQL3 as spf

    evidence["legacy_driver_available"] = spf.dbDriverCxOracle is not None
    fixture = Path(__file__).resolve().parents[2] / "scripthost-utilities-decompiled/22844.spfsql"
    segments = fixture.read_text(encoding="utf-8-sig").split("<---- New Query ---->")
    selected = [
        next(s for s in segments if marker in s).strip()
        for marker in ("/NODE=KM.[A15_PROD_21.].MARS", "/NODE=KM.ARIES", "/CSV=XRAY_results.csv")
    ]

    def save():
        (output / "evidence.json").write_text(json.dumps(evidence, indent=2), encoding="utf-8")

    class Factory(DataSyncXReaderFactory):
        def reader_for(self, backend, node):
            reader = super().reader_for(backend, node)

            class Observed:
                def read(self, *, site, query):
                    call = {
                        "backend": backend,
                        "node": node,
                        "site": site,
                        "reader": f"{type(reader).__module__}.{type(reader).__name__}",
                        "constructor": str(inspect.signature(type(reader))),
                        "read_signature": str(inspect.signature(reader.read)),
                        "sql_sha256": hashlib.sha256(query.encode()).hexdigest(),
                        "schema_expanded": "@[]@" not in query,
                        "csv_expanded": "SQL_Get_CSV_List" not in query,
                    }
                    evidence["calls"].append(call)
                    save()
                    try:
                        frame = reader.read(site=site, query=query)
                    except Exception as exc:
                        call["exception"] = f"{type(exc).__module__}.{type(exc).__name__}"
                        call["message"] = str(exc)
                        save()
                        raise
                    call.update(
                        return_type=f"{type(frame).__module__}.{type(frame).__name__}",
                        rows=len(frame),
                        columns=list(frame.columns),
                        nulls=int(frame.isna().sum().sum()),
                    )
                    save()
                    return frame

            return Observed()

    original_open = spf.nqOracleTask.OpenConnection

    def observed_open(self, *arguments):
        connection = original_open(self, *arguments)
        assert type(connection) is PortableOracleConnection
        evidence.setdefault("connections", []).append(type(connection).__name__)
        save()
        return connection

    runtime = PortableScriptHostRuntime()
    with (
        patch.object(spf, "dbDriverCxOracle", None),
        patch.object(spf.nqOracleTask, "OpenConnection", observed_open),
        use_reader_factory(Factory()),
    ):
        for name, text, filename in zip(
            ("mars", "aries", "sqlite"),
            selected,
            ("yeuchuan_a0_22844.tab", "yeuchuan_a1_22844.tab", "XRAY_results.csv"),
            strict=True,
        ):
            step = {
                "name": name,
                "fixture_headers": next(
                    line.split("=", 1)[1]
                    for line in text.splitlines()
                    if line.startswith("/HEADERS=")
                ),
            }
            evidence["steps"].append(step)
            try:
                step["success"] = runtime.run_text(text, output)
            except Exception as exc:
                step.update(
                    success=False,
                    exception=f"{type(exc).__module__}.{type(exc).__name__}",
                    message=str(exc),
                )
            path = output / filename
            if path.exists():
                raw = path.read_bytes()
                step.update(
                    bom=raw.startswith(codecs.BOM_UTF8),
                    bytes=len(raw),
                    generated_header=raw.decode("utf-8-sig").splitlines()[0],
                )
            save()
            if not step["success"]:
                break  # Never fabricate MARS data to make downstream queries appear live.
        empty_text = r"""<OPTIONS>
/NODE=KM.MARS
/UN=//
/PW=
/OLEDB=SQLPlus
/ENGINE=VA
/WORKDIR=.\
/T=
/CSV=empty_lifecycle.tab
/HEADERS=fallback_name
</OPTIONS>
/*BEGIN SQL*/ SELECT 1 AS actual_label FROM dual WHERE 1=0 /*END SQL*/"""
        evidence["empty_lifecycle"] = {"success": runtime.run_text(empty_text, output)}
        empty_path = output / "empty_lifecycle.tab"
        evidence["empty_lifecycle"]["text"] = empty_path.read_text(encoding="utf-8-sig")
        assert evidence["empty_lifecycle"]["text"] == "FALLBACK_NAME"
        save()
    # Synthetic read-only SQL probes use the real reader/transport, not fake frames.
    evidence["probes"] = []
    connection = PortableOracleConnection()
    connection.openConnection(None, None, "KM.MARS")
    probe_sql = "SELECT CAST(NULL AS VARCHAR2(10)) AS null_value, '.' AS dot_value, UNISTR('\\00E9\\4E2D') AS unicode_value FROM dual"
    for name, query in (
        ("null_unicode", probe_sql),
        ("append", probe_sql),
        ("empty", probe_sql + " WHERE 1=0"),
        ("error", "SELECT SQLPATHFINDER_MISSING_COLUMN FROM dual"),
    ):
        probe = {"name": name}
        evidence["probes"].append(probe)
        path = output / ("probe.tab" if name == "append" else f"{name}.tab")
        if name == "null_unicode":
            path = output / "probe.tab"
        try:
            probe["rows"] = connection.execute(
                query,
                OutExcel=str(path),
                FirstConnect=name != "append",
                MyHeaders="DIFFERENT,HEADERS,HERE",
            )
            probe["file_exists"] = path.exists()
            if path.exists():
                raw = path.read_bytes()
                probe.update(bom=raw.startswith(codecs.BOM_UTF8), text=raw.decode("utf-8"))
        except Exception as exc:
            probe.update(
                exception=f"{type(exc).__module__}.{type(exc).__name__}",
                cause=f"{type(exc.__cause__).__module__}.{type(exc.__cause__).__name__}",
                message=str(exc),
            )
        save()
    connection.close()
    probes = {p["name"]: p for p in evidence["probes"]}
    assert probes["null_unicode"]["rows"] == probes["append"]["rows"] == 1
    assert probes["null_unicode"]["text"].splitlines() == [
        "NULL_VALUE\tDOT_VALUE\tUNICODE_VALUE",
        "\t.\té中",
    ]
    assert probes["append"]["text"].splitlines() == [
        "NULL_VALUE\tDOT_VALUE\tUNICODE_VALUE",
        "\t.\té中",
        "\t.\té中",
    ]
    assert not probes["null_unicode"]["bom"]
    assert probes["empty"]["rows"] == 0 and not probes["empty"]["file_exists"]
    assert probes["error"]["cause"] == "oracledb.exceptions.DatabaseError"
    for step, call in zip(evidence["steps"][:2], evidence["calls"][:2], strict=True):
        step["historical_expected_headers_from_source"] = step["fixture_headers"].upper().split(",")
        assert call["site"] == "KM" and call["schema_expanded"] and call["csv_expanded"]
        assert call["columns"] == step["historical_expected_headers_from_source"]
        assert step["generated_header"].split("\t") == call["columns"]
    save()
    print(json.dumps(evidence, indent=2))
    return 0 if len(evidence["steps"]) == 3 and all(s["success"] for s in evidence["steps"]) else 1


if __name__ == "__main__":
    raise SystemExit(main())
