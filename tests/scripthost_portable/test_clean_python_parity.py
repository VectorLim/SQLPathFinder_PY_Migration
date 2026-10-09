"""Offline subprocess parity for the two clean Python migration targets."""

from __future__ import annotations

import ast
import csv
import json
import os
import subprocess
import sys
from pathlib import Path

_CHILD_MARKER = "__CLEAN_PYTHON_PARITY__="
_ROOT = Path(__file__).resolve().parents[2]
_TARGETS = {
    "icm": _ROOT / "ICMPCS.txt",
    "csr": _ROOT / "output" / "aed-migration" / "CSR_IAM_v2.aed.txt",
}


def _child(argv: list[str]) -> int:
    """Stdlib-first child entrypoint; safe to run from the production image."""
    import hashlib
    import random

    import pandas as pd

    from scripthost_portable import PortableScriptHostRuntime, use_reader_factory
    from scripthost_portable.runtime import _spf_manager_type

    # Load the legacy package tree before importing or patching any SPF module.
    runtime = PortableScriptHostRuntime()
    manager_type = _spf_manager_type()
    from SPFLib.SPFUtilities.utils import Utilities
    from scripthost_portable import aed_api

    source, workdir_arg, mode, scenario = argv
    workdir = Path(workdir_arg).resolve()
    workdir.mkdir(parents=True, exist_ok=True)
    os.chdir(workdir)
    random.seed(20261007)
    os.environ["SCRIPTHOST_FORCE_PORTABLE_QUERY_TRANSPORT"] = "1"
    for key in ("SIGNAL", "RowsInFile", "STAGE2_SIGNAL"):
        os.environ.pop(key, None)

    (workdir / "configsets.csv").write_text(
        "MARS,ARIES,AED_FACILITY,dEmail,AED_ENABLED,ATTR_LIST,SKIP_OPERATION\n"
        "KM.MARS,KM.ARIES,ATC,quality@example.test,Y,NCO,1001\n"
        "ignored,ignored,ignored,ignored,N,ignored,ignored\n",
        encoding="utf-8",
    )
    if scenario == "csr-seed":
        (workdir / "AED_CANDIDATES.csv").write_text(
            "FACILITY,LOT\nATC,LOT-SEED\n", encoding="utf-8"
        )
        fields = [
            "lot_1",
            "newqty1",
            "facility",
            "operation",
            "tool_entity",
            "primary_entity",
            "processing_end_date",
            "lot",
            "prodgroup3",
            "product",
            "visual_id",
            "ws_loss_code",
            "media_in_x",
            "media_in_y",
            "height",
            *[f"patch_lift_roi{i}" for i in range(1, 9)],
            "patch_lift_roi_max",
            "patch_sli",
            "interposer_sli",
            "NCO_Risk",
            "VIDCount",
            "FlagLot",
        ]
        row = [
            "LOT-SEED",
            10,
            "ATC",
            "2303",
            "TOOL",
            "PRIMARY",
            "2026-10-01 01:00:00",
            "LOT-SEED",
            "CWF",
            "PRODUCT",
            "VIS-SEED",
            "",
            1,
            1,
            10,
            *([0] * 8),
            0,
            "PSLI",
            "ISLI",
            "0",
            2,
            "0",
        ]
        with (workdir / "IPM_Data.csv").open(
            "w", encoding="utf-8", newline=""
        ) as stream:
            writer = csv.writer(stream)
            writer.writerow(fields)
            writer.writerow(row)
    if scenario == "local-malformed":
        (workdir / "items.csv").write_text('LOT\n"unterminated\n', encoding="utf-8")

    mars_columns = [
        "lot_1",
        "operation_1",
        "out_date",
        "oldqty1",
        "newqty1",
        "Interposer_SLI",
        "Patch_SLI",
        "prodgroup3_1",
        "entity",
        "transaction",
    ]
    aries_columns = [
        "facility",
        "operation",
        "module_name",
        "tool_entity",
        "primary_entity",
        "processing_start_date",
        "processing_end_date",
        "lot",
        "product",
        "prodgroup3",
        "product_desc",
        "owner",
        "visual_id",
        "ws_loss_code",
        "media_in_x",
        "media_in_y",
        "parameter",
        "numeric_value",
    ]
    mars: list[list[object]] = []
    aries: list[list[object]] = []
    if scenario in {"positive", "no-candidates", "zero-input"}:
        if scenario == "positive":
            lots = [("LOT-POS", "VIS-1", 2100)]
        elif scenario == "no-candidates":
            lots = [("LOT-LOW", "VIS-1", 100), ("LOT-LOW", "VIS-2", 100)]
        else:
            lots = [("LOT-SEED", "VIS-1", 100)]
        mars_lots: set[str] = set()
        for lot, visual, roi in lots:
            if scenario != "zero-input" and lot not in mars_lots:
                mars.append(
                    [
                        lot,
                        "2303",
                        "2026-10-01 00:00:00",
                        10,
                        10,
                        "ISLI",
                        "PSLI",
                        "CWF",
                        "IAM-1",
                        "MVOU",
                    ]
                )
                mars_lots.add(lot)
            values = {
                "height": 10,
                **{
                    f"patch_lift_roi{index}": roi if index == 4 else 0
                    for index in range(1, 9)
                },
                "patch_lift_roi_max": roi,
            }
            for parameter, value in values.items():
                aries.append(
                    [
                        "ATC",
                        "2303",
                        "MODULE",
                        "TOOL",
                        "PRIMARY",
                        "2026-10-01 00:00:00",
                        "2026-10-01 01:00:00",
                        lot,
                        "PRODUCT",
                        "CWF",
                        "test product",
                        "owner",
                        visual,
                        "",
                        1,
                        1,
                        parameter,
                        value,
                    ]
                )

    events: list[dict[str, object]] = []

    class Reader:
        def __init__(self, backend: str, node: str):
            self.backend, self.node = backend, node

        def read(self, *, site: str, query: str):
            events.append(
                {
                    "kind": "query",
                    "backend": self.backend,
                    "node": self.node,
                    "site": site,
                    "sql": query,
                }
            )
            if scenario == "transport-failure":
                raise RuntimeError("injected offline reader failure")
            rows, columns = (
                (mars, mars_columns)
                if self.backend == "mars"
                else (aries, aries_columns)
            )
            return pd.DataFrame(rows, columns=columns)

    class Factory:
        def reader_for(self, backend: str, node: str):
            events.append({"kind": "reader", "backend": backend, "node": node})
            return Reader(backend, node)

    def capture_aed(path, logger=None):
        candidate = Path(path)
        events.append(
            {
                "kind": "aed",
                "path": str(path),
                "bytes": candidate.read_bytes().decode("utf-8-sig"),
            }
        )

    def capture_email(
        self,
        MyLocal,
        MyCSVFile,
        MailToIn,
        Subject,
        BodyF,
        MailCC,
        MailBCC,
        MyRole,
        OnlyIntel,
        ll_Outlook,
        EmailUtility="SA",
    ):
        body = Path(BodyF)
        events.append(
            {
                "kind": "email",
                "to": str(MailToIn),
                "subject": str(Subject),
                "body": (
                    body.read_text(encoding="utf-8-sig")
                    if body.is_file()
                    else str(BodyF)
                ),
                "attachments": str(MyCSVFile),
            }
        )

    aed_api.process_candidates = capture_aed
    Utilities.SPFEmail = capture_email

    query_classes = {"nqOracleTask", "nqSQLiteTask"}
    report_classes = {
        "HTMLRunTask",
        "HTMLDeferTask",
        "HTMLLayoutTask",
        "HTMLDeleteTask",
    }
    control_classes = {"StartMacroTask", "RowsInFileTask", "IfThenTask"}
    instrumented_classes = query_classes | report_classes | control_classes
    manager_ids: dict[int, str] = {}
    original_get_query = manager_type.GetQuery
    from SPFLib.SPFSQL3 import SPFTaskBase

    base_steps = SPFTaskBase.executeTaskCommandSteps

    def report_files():
        return {
            path.name: path.read_text(encoding="utf-8-sig", errors="replace").replace(
                str(workdir), "<WORKDIR>"
            )
            for path in workdir.iterdir()
            if path.is_file()
            and path.suffix.lower() in {".htm", ".html", ".css", ".ini"}
        }

    def instrument_get_query(manager, *args, **kwargs):
        task = original_get_query(manager, *args, **kwargs)
        if type(task).__name__ not in instrumented_classes:
            return task
        manager_ids.setdefault(id(manager), f"manager-{len(manager_ids)}")
        original_execute = task.executeTaskCommand

        def before():
            name = type(task).__name__
            files_before = {path.name for path in workdir.iterdir() if path.is_file()}
            event = {
                "kind": "task",
                "class": name,
                "manager": manager_ids[id(manager)],
            }
            if name in query_classes | report_classes:
                event["raw_options"] = dict(task.taskOptionsDict)
                event["effective_options"] = {
                    field: getattr(task, field)
                    for field in (
                        "SQLEngine",
                        "OLEDBopt",
                        "Sitei",
                        "MyTables",
                        "OutExcel",
                        "OutFile",
                        "MyHeaders",
                        "MyReset",
                        "ll_QuoteCSV",
                        "MyRecd",
                        "MyPromptTxt",
                        "CTRow",
                        "CTVal",
                        "CTHeader",
                        "MyInstance",
                        "g_ID",
                        "MyCTArray",
                        "MyTblDelete",
                        "MySQLite_DT",
                        "WorkDir",
                        "OutTT",
                        "MyTS",
                        "ll_UniqueHdr",
                        "ll_hadoopSvr",
                        "ll_Outlook",
                        "ll_AppSvr",
                        "genJSDataOnly",
                        "jsDataFileLabel",
                        "gHTMLReport",
                    )
                }
                if name in query_classes:
                    event["query_before"] = task.SPFTaskCommand
                elif name != "HTMLDeleteTask":
                    event["report_template"] = task.SPFTaskCommand
            elif name in control_classes:
                event["utilities"] = list(task.MyUtilities or [])[
                    : 4 if name == "IfThenTask" else 3
                ]
            if name in report_classes:
                event["report_before"] = report_files()
            events.append(event)
            return name, event, files_before

        def after(name, event, files_before):
            files_after = {path.name for path in workdir.iterdir() if path.is_file()}
            event["created"] = sorted(files_after - files_before)
            event["deleted"] = sorted(files_before - files_after)
            if name in query_classes:
                event["query_after"] = task.SPFTaskCommand
            if name in control_classes:
                event["execute_child_tasks"] = getattr(
                    task, "shouldExecuteChildTasks", None
                )
                event["environment"] = {
                    key: os.environ.get(key) for key in ("RowsInFile", "SIGNAL")
                }
            if name in report_classes:
                event["report_after"] = report_files()

        def instrument_execute():
            state = before()
            try:
                return original_execute()
            finally:
                after(*state)

        def instrument_steps():
            state = before()
            try:
                yield from original_steps()
            finally:
                after(*state)

        # Controllers with a generator seam run their command through executeTaskCommandSteps,
        # both from the original executeTaskCommand and from generated controls.
        if type(task).executeTaskCommandSteps is base_steps:
            task.executeTaskCommand = instrument_execute
        else:
            original_steps = task.executeTaskCommandSteps
            task.executeTaskCommandSteps = instrument_steps
        return task

    manager_type.GetQuery = instrument_get_query
    before = {path.name for path in workdir.iterdir() if path.is_file()}
    try:
        with use_reader_factory(Factory()):
            if mode == "original":
                success = runtime.run_text(
                    Path(source).read_text(encoding="utf-8-sig"), workdir
                )
            else:
                success = runtime.run_python_file(Path(source), workdir)
        error = None
    except Exception as exc:
        success = False
        error = {"type": type(exc).__name__, "message": str(exc)}
    events.append(
        {
            "kind": "environment",
            "values": {
                key: os.environ.get(key)
                for key in ("SIGNAL", "RowsInFile", "STAGE2_SIGNAL")
            },
        }
    )
    paths = sorted(path for path in workdir.iterdir() if path.is_file())
    snapshots = {}
    for path in paths:
        data = path.read_bytes()
        normalized = data.replace(str(workdir).encode(), b"<WORKDIR>")
        try:
            snapshots[path.name] = {
                "sha256": hashlib.sha256(normalized).hexdigest(),
                "text": normalized.decode("utf-8-sig"),
            }
        except UnicodeDecodeError:
            snapshots[path.name] = {"sha256": hashlib.sha256(normalized).hexdigest()}

    def normalize(value):
        if isinstance(value, str):
            return value.replace(str(workdir), "<WORKDIR>")
        if isinstance(value, list):
            return [normalize(item) for item in value]
        if isinstance(value, dict):
            return {key: normalize(item) for key, item in value.items()}
        return value

    print(
        _CHILD_MARKER
        + json.dumps(
            normalize(
                {
                    "success": success,
                    "error": error,
                    "events": events,
                    "files_before": sorted(before),
                    "files_after": [path.name for path in paths],
                    "snapshots": snapshots,
                }
            ),
            sort_keys=True,
        )
    )
    return 0


if __name__ == "__main__" and "--child" in sys.argv:
    raise SystemExit(_child(sys.argv[sys.argv.index("--child") + 1 :]))


import pytest  # noqa: E402 - child CLI dispatch must precede pytest import

from vg2c import compile_document  # noqa: E402


def _child_run(source: Path, workdir: Path, mode: str, scenario: str) -> dict:
    workdir.mkdir(parents=True, exist_ok=True)
    env = dict(os.environ)
    env["PYTHONPATH"] = os.pathsep.join(
        [str(_ROOT / "src"), str(_ROOT), env.get("PYTHONPATH", "")]
    )
    result = subprocess.run(
        [
            sys.executable,
            str(Path(__file__).resolve()),
            "--child",
            str(source),
            str(workdir),
            mode,
            scenario,
        ],
        cwd=_ROOT,
        env=env,
        text=True,
        capture_output=True,
        timeout=180,
        check=False,
    )
    line = next(
        (
            line
            for line in reversed(result.stdout.splitlines())
            if line.startswith(_CHILD_MARKER)
        ),
        None,
    )
    assert result.returncode == 0 and line, result.stdout + result.stderr
    return json.loads(line[len(_CHILD_MARKER) :])


def _run_pair(
    source: Path, generated: Path, root: Path, scenario: str
) -> tuple[dict, dict]:
    original = _child_run(source, root / "original", "original", scenario)
    python = _child_run(generated, root / "python", "python", scenario)
    assert original["success"] == python["success"]
    assert (original["error"] or {}).get("type") == (python["error"] or {}).get("type")
    assert original["events"] == python["events"]
    assert original["files_after"] == python["files_after"]
    assert original["snapshots"] == python["snapshots"]
    return original, python


@pytest.mark.parametrize("job", ["icm", "csr"])
@pytest.mark.parametrize("scenario", ["positive", "no-candidates", "zero-input"])
def test_original_and_generated_jobs_match_in_clean_processes(
    tmp_path: Path, job: str, scenario: str
) -> None:
    source = _TARGETS[job]
    generated = tmp_path / f"{job}.py"
    generated.write_text(compile_document(source).emitted.source, encoding="utf-8")
    _, result = _run_pair(source, generated, tmp_path / f"{job}-{scenario}", scenario)
    assert result["success"]

    tasks = [event for event in result["events"] if event["kind"] == "task"]
    queries = [
        event for event in tasks if event["class"] in {"nqOracleTask", "nqSQLiteTask"}
    ]
    transported = [event for event in result["events"] if event["kind"] == "query"]
    assert len(queries) == (3 if job == "csr" and scenario == "zero-input" else 5)
    assert [event["manager"] for event in tasks] == ["manager-0"] * len(tasks)
    assert [(event["backend"], event["site"]) for event in transported] == [
        ("mars", "KM"),
        ("aries", "KM"),
    ]
    aries_sql = transported[1]["sql"]
    assert "SQL_Get_CSV_List" not in aries_sql
    assert "<<<" not in aries_sql and "@[]@" not in aries_sql
    selected_lot = {"positive": "LOT-POS", "no-candidates": "LOT-LOW"}.get(scenario)
    if selected_lot:
        assert selected_lot in aries_sql

    aed = [event for event in result["events"] if event["kind"] == "aed"]
    expected_aed_calls = 0 if job == "csr" and scenario == "zero-input" else 1
    assert len(aed) == expected_aed_calls
    rows = list(csv.reader(aed[0]["bytes"].splitlines())) if aed else []
    if scenario == "positive":
        assert result["success"]
        assert rows == [["FACILITY", "LOT"], ["ATC", "LOT-POS"]]
    elif job == "csr" and scenario == "zero-input":
        assert not aed
    else:
        assert rows == [["FACILITY", "LOT"]]

    reports = [
        event["class"]
        for event in tasks
        if event["class"]
        in {"HTMLRunTask", "HTMLDeferTask", "HTMLLayoutTask", "HTMLDeleteTask"}
    ]
    initial = ["HTMLRunTask", "HTMLLayoutTask", "HTMLDeleteTask"]
    if scenario == "positive":
        assert reports == initial + [
            "HTMLDeferTask",
            "HTMLLayoutTask",
            "HTMLDeleteTask",
        ]
    else:
        assert reports == initial


def test_csr_seed_skips_transformation_but_runs_later_signal_report(
    tmp_path: Path,
) -> None:
    source = _TARGETS["csr"]
    generated = tmp_path / "csr.py"
    generated.write_text(compile_document(source).emitted.source, encoding="utf-8")
    _, result = _run_pair(source, generated, tmp_path / "csr-seed", "csr-seed")
    assert result["success"]
    tasks = [event for event in result["events"] if event["kind"] == "task"]
    rows = [event for event in tasks if event["class"] == "RowsInFileTask"]
    branches = [event for event in tasks if event["class"] == "IfThenTask"]
    queries = [
        event for event in tasks if event["class"] in {"nqOracleTask", "nqSQLiteTask"}
    ]
    assert len(queries) == 3
    assert [event["manager"] for event in tasks] == ["manager-0"] * len(tasks)
    assert rows[0]["environment"]["RowsInFile"] == "0"
    assert rows[-1]["environment"]["SIGNAL"] == "1"
    assert branches[0]["execute_child_tasks"] is False
    assert branches[-1]["execute_child_tasks"] is True
    assert not [event for event in result["events"] if event["kind"] == "aed"]
    reports = [event["class"] for event in tasks if event["class"].startswith("HTML")]
    assert reports == [
        "HTMLRunTask",
        "HTMLLayoutTask",
        "HTMLDeleteTask",
        "HTMLDeferTask",
        "HTMLLayoutTask",
        "HTMLDeleteTask",
    ]


@pytest.mark.parametrize(
    "scenario", ["transport-failure", "local-missing", "local-malformed"]
)
def test_stage1_failure_boundaries_match_for_synthetic_query(
    tmp_path: Path, scenario: str
) -> None:
    source = tmp_path / "query.txt"
    source.write_text(
        "<OPTIONS>\n/NODE=KM.MARS\n/UN=\n/PW=\n/OLEDB=SQLPlus\n/ENGINE=VA\n"
        "/WORKDIR=.\\\n/CSV=result.tab\n/HEADERS=LOT\n</OPTIONS>\n"
        'SELECT LOT FROM source WHERE LOT IN SQL_Get_CSV_List("items.csv", LOT, "LOT IN")',
        encoding="utf-8",
    )
    generated = tmp_path / "query.py"
    generated.write_text(compile_document(source).emitted.source, encoding="utf-8")
    original, python = _run_pair(source, generated, tmp_path / scenario, scenario)
    assert (original["error"] or {}).get("type") == (python["error"] or {}).get("type")
    if scenario == "transport-failure":
        assert original["success"] is False
        assert original["error"]["type"] == "QueryExecutionError"


def test_checked_in_generation_matches_both_sources_and_python311_grammar(
    tmp_path: Path,
) -> None:
    checked_in = {
        "icm": _ROOT / "output" / "clean-python" / "ICMPCS.py",
        "csr": _ROOT / "output" / "clean-python" / "CSR_IAM_v2.aed.py",
    }
    for name, source in _TARGETS.items():
        emitted = compile_document(source).emitted.source
        assert checked_in[name].read_text(encoding="utf-8") == emitted
        ast.parse(emitted, feature_version=(3, 11))
        direct = subprocess.run(
            [sys.executable, str(checked_in[name])],
            cwd=_ROOT,
            env={
                **os.environ,
                "PYTHONPATH": os.pathsep.join(
                    [str(_ROOT / "src"), str(_ROOT), os.environ.get("PYTHONPATH", "")]
                ),
            },
            capture_output=True,
            text=True,
            timeout=30,
            check=False,
        )
        assert direct.returncode != 0
        assert "python -m scripthost_portable.launcher" in direct.stderr
        assert "Traceback" not in direct.stderr
