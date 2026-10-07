"""Characterize clean Python calls against the original runtime, offline."""

from __future__ import annotations

import os
from pathlib import Path

import pandas as pd
import pytest

from scripthost_portable import PortableScriptHostRuntime, use_reader_factory
from scripthost_portable import aed_api, runtime, script_api
from scripthost_portable.script_api import aed, macros, query, reports, utilities
from scripthost_portable.worker import ScriptHostJob, run_job


@pytest.fixture(autouse=True)
def offline(monkeypatch):
    monkeypatch.setenv("SCRIPTHOST_FORCE_PORTABLE_QUERY_TRANSPORT", "1")
    for name in ("STAGE1_SIGNAL", "STAGE1_VALUE"):
        monkeypatch.setenv(name, "0")


def test_plain_run_shares_one_original_manager_and_query_path(tmp_path, monkeypatch):
    (tmp_path / "config.csv").write_text("SOURCE\nmeasurements.csv:measurements\nignored.csv\n")
    (tmp_path / "measurements.csv").write_text("lot,value\nA,1\nB,0\n")
    original = runtime._spf_manager_type()
    managers, tasks = [], []

    def create_manager():
        manager = original()
        managers.append(manager)
        get_query = manager.GetQuery

        def traced(*args):
            assert script_api._manager() is manager
            task = get_query(*args)
            tasks.append(task)
            return task

        manager.GetQuery = traced
        return manager

    monkeypatch.setattr(runtime, "_spf_manager_type", lambda: create_manager)
    before = Path.cwd()

    def run():
        assert Path.cwd() == tmp_path
        if macros.load_csv("config.csv"):
            query.run(
                sql="SELECT lot AS LOT FROM measurements WHERE value > 0",
                tables=macros["SOURCE"], output="candidates.csv", quote_csv=True,
            )
            assert utilities.rows_in_file("candidates.csv", "STAGE1_SIGNAL") == 1
            assert macros.compare("STAGE1_SIGNAL", "GT", "0")

    assert PortableScriptHostRuntime().run_python(run, tmp_path)
    assert len(managers) == 1
    assert [type(task).__name__ for task in tasks] == [
        "StartMacroTask", "nqSQLiteTask", "RowsInFileTask", "IfThenTask",
    ]
    assert tasks[1].ll_QuoteCSV
    assert (tmp_path / "candidates.csv").read_text(encoding="utf-8-sig").splitlines() == [
        'LOT', 'A',
    ]
    assert Path.cwd() == before
    assert not managers[0]._script_macro_tables
    with pytest.raises(RuntimeError, match="requires a job"):
        macros.get("SOURCE")


def test_macro_row_lookup_substitution_and_separate_env_namespace(tmp_path):
    (tmp_path / "config.csv").write_text("SITE,EMPTY\nKM,\nPG,second\n")

    def run():
        assert macros.load_csv("config.csv")
        manager = script_api._manager()
        assert macros["site"] == "KM"
        assert macros.get("EMPTY") == ""
        text = "site=<<<SITE>>> empty=<<<EMPTY>>>"
        assert macros.substitute(text) == manager.Substitute_Macro(
            text, manager._script_macro_tables, 1, 0
        )
        assert macros["spf-job-start-day"] == manager.gMySPFJobDay
        macros.set("STAGE1_VALUE", "7")
        assert macros["%STAGE1_VALUE%"] == "7"
        with pytest.raises(Exception, match="Macro translation"):
            macros["STAGE1_VALUE"]
        with pytest.raises(Exception, match="Macro translation"):
            macros["MISSING"]
        with pytest.raises(NotImplementedError, match="one CSV macro scope"):
            macros.load_csv("config.csv")

    assert PortableScriptHostRuntime().run_python(run, tmp_path)


def test_bracketed_csv_header_keeps_original_import_and_lookup_behavior(tmp_path):
    (tmp_path / "config.csv").write_text("[SITE]\nKM\n")

    def run():
        assert macros.load_csv("config.csv")
        manager = script_api._manager()
        # The original importer normalizes square brackets to parentheses.
        table = next(iter(manager._script_macro_tables.values()))
        assert table.GetColumnNamesForTable() == ["(SITE)"]
        with pytest.raises(Exception, match="Macro translation") as original:
            manager.Substitute_Macro("<<<SITE>>>", manager._script_macro_tables, 1, 0)
        with pytest.raises(type(original.value), match="Macro translation"):
            macros["SITE"]

    assert PortableScriptHostRuntime().run_python(run, tmp_path)


@pytest.mark.parametrize("contents", ["SITE\n", None])
def test_empty_and_missing_macro_scope(tmp_path, contents):
    if contents is not None:
        (tmp_path / "config.csv").write_text(contents)

    def run():
        assert macros.load_csv("config.csv", continue_on_error=True) is False
        if contents is None:
            with pytest.raises(FileNotFoundError):
                macros.load_csv("config.csv")

    assert PortableScriptHostRuntime().run_python(run, tmp_path)


def test_zero_byte_macro_preserves_original_failure(tmp_path):
    (tmp_path / "empty.csv").write_text("")
    text = '<OPTIONS>\n/UTILITIES={START-MACRO} "empty.csv" "N"\n</OPTIONS>'
    with pytest.raises(IndexError):
        PortableScriptHostRuntime().run_text(text, tmp_path)
    with pytest.raises(IndexError):
        PortableScriptHostRuntime().run_python(lambda: macros.load_csv("empty.csv"), tmp_path)
    assert script_api._current_manager is None


@pytest.mark.parametrize("operator,rhs", [("GT", "0"), ("EQ", "2"), ("LT", "1")])
def test_comparison_uses_original_resolution_and_comparevars(tmp_path, operator, rhs):
    def run():
        macros.set("STAGE1_VALUE", "2")
        manager = script_api._manager()
        assert macros.compare("STAGE1_VALUE", operator, rhs) == manager.CompareVars(
            os.environ["STAGE1_VALUE"], rhs, operator
        )
        assert macros.compare("VAR(2)", operator, rhs) == manager.CompareVars("2", rhs, operator)

    assert PortableScriptHostRuntime().run_python(run, tmp_path)


def test_rows_in_file_preserves_original_error_value(tmp_path):
    (tmp_path / "rows.csv").write_text("LOT\nA\nB\n")

    def run():
        manager = script_api._manager()
        assert utilities.rows_in_file("rows.csv", "STAGE1_SIGNAL") == (
            manager.getRowCountFromFile("rows.csv")
        )
        assert os.environ["STAGE1_SIGNAL"] == "2"
        assert utilities.rows_in_file("missing.csv", "STAGE1_SIGNAL") == -1
        assert not macros.compare("STAGE1_SIGNAL", "GT", "0")

    assert PortableScriptHostRuntime().run_python(run, tmp_path)


def test_oracle_facade_reaches_portability_override_and_transport(tmp_path):
    calls = []

    class Reader:
        def read(self, *, site, query):
            calls.append((site, query))
            return pd.DataFrame({"LOT": ["A"]})

    class Factory:
        def reader_for(self, backend, node):
            assert (backend, node) == ("mars", "KM.[A15_PROD_21.].MARS")
            return Reader()

    def run():
        query.run(
            sql="/*BEGIN SQL*/ SELECT 'A' AS LOT FROM dual /*END SQL*/", engine="VA",
            node="KM.[A15_PROD_21.].MARS", output="mars.csv", headers="LOT",
            record="Stage1", quote_csv=True,
        )

    with use_reader_factory(Factory()):
        assert PortableScriptHostRuntime().run_python(run, tmp_path)
    assert len(calls) == 1
    assert "SELECT 'A' AS LOT FROM dual" in calls[0][1]
    assert "A" in (tmp_path / "mars.csv").read_text(encoding="utf-8-sig")


@pytest.mark.parametrize("source", ["ICMPCS.txt", "output/aed-migration/CSR_IAM_v2.aed.txt"])
def test_all_current_job_query_options_reach_original_parser(tmp_path, monkeypatch, source):
    root = Path(__file__).resolve().parents[2]
    blocks = (root / source).read_text(encoding="utf-8-sig").split("<---- New Query ---->")
    # Explicit source-to-public spelling: any newly encountered option fails this
    # scoped characterization instead of being silently dropped.
    names = {
        "NODE": "node", "OLEDB": "oledb", "ENGINE": "engine", "UN": "username",
        "PW": "password", "WORKDIR": "workdir", "T": "show_result", "TS": "timestamp",
        "CSV": "output", "TABLE": "tables", "HEADERS": "headers", "RECORD": "record",
        "CTROW": "ct_rows", "CTVALUE": "ct_value", "CTHEADER": "ct_header",
        "CTARRAY": "ct_array", "RESET": "reset", "DELETE": "delete", "SQLITE_DT": "sqlite_types",
        "QUOTECSV": "quote_csv", "HEADERS_UNIQUE": "unique_headers", "INSTANCE": "instance",
        "PROMPT-TEXT": "prompt", "HADOOP_SERVER_DEFAULT": "hadoop_server",
    }
    parsed = []

    def run():
        manager = script_api._manager()
        original = manager.GetQuery

        def parse_only(*args):
            task = original(*args)
            task.parseTaskCommandDone = True

            def capture_options():
                parsed.append(task.taskOptionsDict)

            # Keep original execute()/parseTaskOptions() on the call stack;
            # native Windows node metadata validates its original invoker.
            # SQL execution is outside this option-preservation check.
            monkeypatch.setattr(task, "executeTaskCommand", capture_options)
            return task

        monkeypatch.setattr(manager, "GetQuery", parse_only)
        for block in blocks:
            options = block.partition("</OPTIONS>")[0]
            if "/ENGINE=" not in options:
                continue
            values = dict(line[1:].split("=", 1) for line in options.splitlines() if line.startswith("/"))
            expected = {"/" + name: value for name, value in values.items()}
            arguments = {names[name]: value for name, value in values.items()}
            # Prepared job macros are external inputs. Substitute only those
            # fixture placeholders so original option parsing can run offline.
            arguments["node"] = (
                ".\\" if values["ENGINE"] == "SQLite" else "KM.[A15_PROD_21.].MARS"
            )
            expected["/NODE"] = arguments["node"]
            query.run(sql="SELECT 1", **arguments)
            assert expected.items() <= parsed[-1].items()

    assert PortableScriptHostRuntime().run_python(run, tmp_path)
    assert len(parsed) == 5


def test_reports_keep_original_lifecycle_and_same_manager(tmp_path, monkeypatch):
    (tmp_path / "schema").mkdir()
    (tmp_path / "report.csv").write_text("total_pcg,total_flag,ce%\n10,2,80%\n")
    fixture = Path(__file__).resolve().parents[1] / "fixtures" / "html_test.txt"
    blocks = fixture.read_text(encoding="utf-8-sig").split("<---- New Query ---->")
    css = next(b for b in blocks if "/REPORT=HTML-RUN" in b).split("</OPTIONS>", 1)[1]
    css = css.replace("sqlpathfinder_style_1.css", "report.css")
    deferred = next(b for b in blocks if "/REPORT=HTML-DEFER" in b and "/ID=MYREPORT5" in b)
    deferred = deferred.split("</OPTIONS>", 1)[1]
    source = next(line for line in deferred.splitlines() if line.startswith("INPUT-FILE"))
    fields = source.split(r"<\\>")
    fields[1] = "report.csv"
    deferred = deferred.replace(source, r"<\\>".join(fields))
    deferred = deferred.replace("sqlpathfinder_style_1.css", "report.css")
    layout = "\n".join([
        '<table class="tblout"><tr class="tblout"><td class="tblout">',
        ":FILE:report.htm", ":CSS:report.css", ":CSSEMBED:N", ":RR:NO", ":B:Y",
        ":TITLE:Stage 1 report", '<table class="tblout">', '<tr class="tblout">',
        '<td class="tblout">', "HTM:MYREPORT5", '</td>', '</tr>', '</table>',
        '</td></tr></table>',
    ])
    seen = []
    original = script_api._manager

    def traced():
        manager = original()
        seen.append(manager)
        return manager

    monkeypatch.setattr(script_api, "_manager", traced)

    def run():
        reports.run(css, instance="STAGE1")
        reports.defer(deferred, report_id="MYREPORT5", instance="STAGE1")
        assert (tmp_path / "STAGE1_MYREPORT5_tmp_.ini").exists()
        reports.layout(layout, instance="STAGE1", outlook=False, json_only=False)
        assert script_api._manager().gHTMDelete
        reports.delete(instance="STAGE1")
        assert not script_api._manager().gHTMDelete

    assert PortableScriptHostRuntime().run_python(run, tmp_path)
    assert len({id(manager) for manager in seen}) == 1
    html = (tmp_path / "report.htm").read_text(encoding="utf-8-sig")
    assert "Stage 1 report" in html and "80%" in html
    assert "table.tblin" in (tmp_path / "report.css").read_text()
    assert not list(tmp_path.glob("*_MYREPORT5_tmp_.ini"))


def test_aed_calls_existing_implementation(tmp_path, monkeypatch):
    calls = []
    monkeypatch.setattr(aed_api, "process_candidates", calls.append)
    assert PortableScriptHostRuntime().run_python(lambda: aed.process("candidates.csv"), tmp_path)
    assert calls == ["candidates.csv"]


def test_failure_and_nested_execution_restore_binding_and_cwd(tmp_path):
    previous = Path.cwd()

    def run():
        manager = script_api._manager()
        with pytest.raises(RuntimeError, match="already running"):
            PortableScriptHostRuntime().run_python(lambda: None, tmp_path / "nested")
        assert script_api._manager() is manager
        raise ValueError("user failure")

    with pytest.raises(ValueError, match="user failure"):
        PortableScriptHostRuntime().run_python(run, tmp_path)
    assert Path.cwd() == previous
    assert script_api._current_manager is None


@pytest.mark.parametrize("call", [
    lambda: macros.load_csv("x.csv"), lambda: macros["X"], lambda: macros.set("X", "1"),
    lambda: macros.compare("X", "GT", "0"), lambda: utilities.rows_in_file("x.csv", "X"),
    lambda: query.run(sql="SELECT 1", output="x.csv"), lambda: reports.run("template"),
    lambda: reports.defer("template", report_id="X"), lambda: reports.layout("template"),
    lambda: reports.delete(), lambda: aed.process("x.csv"),
])
def test_facades_fail_outside_runtime(call):
    with pytest.raises(RuntimeError, match="requires a job"):
        call()


def test_unsupported_options_and_legacy_blocks_fail_clearly(tmp_path):
    def run():
        with pytest.raises(TypeError, match="unsupported_option"):
            query.run(sql="SELECT 1", output="x.csv", unsupported_option=True)
        with pytest.raises(ValueError, match="Unsupported Stage 1 query engine"):
            query.run(sql="SELECT 1", output="x.csv", engine="DuckDB")
        with pytest.raises(ValueError, match="Unsupported Stage 1 oledb"):
            query.run(sql="SELECT 1", output="x.csv", oledb="SQLPlus")
        with pytest.raises(ValueError, match="multiline"):
            query.run(sql="SELECT 1", output="x.csv\n/WRITE-FILE=Y")
        with pytest.raises(ValueError, match="without legacy task blocks"):
            reports.run("<OPTIONS>\n/REPORT=HTML-DELETE\n</OPTIONS>")

    assert PortableScriptHostRuntime().run_python(run, tmp_path)


def test_python_file_runs_in_fresh_worker_without_public_session(tmp_path):
    (tmp_path / "source.csv").write_text("LOT\nA\n")
    script = tmp_path / "job.py"
    script.write_text(
        "from scripthost_portable.script_api import macros, query, utilities\n"
        "def run():\n"
        "    query.run(sql='SELECT LOT FROM source', tables='source.csv', "
        "output='result.csv', quote_csv=True)\n"
        "    utilities.rows_in_file('result.csv', 'STAGE1_SIGNAL')\n"
        "    assert macros.compare('STAGE1_SIGNAL', 'GT', '0')\n"
    )
    result = run_job(ScriptHostJob(working_directory=str(tmp_path), python_path=str(script)), timeout=30)
    assert result.success, result.message + result.stderr
    assert result.child_pid != os.getpid()
    assert "result.csv" in result.generated_outputs
    assert os.environ["STAGE1_SIGNAL"] == "0"
    assert script_api.__all__ == ["aed", "macros", "query", "reports", "utilities"]
    assert not hasattr(script_api, "script_session")


def test_python_worker_requires_run_and_validates_one_input(tmp_path):
    script = tmp_path / "bad.py"
    script.write_text("value = 1\n")
    result = run_job(ScriptHostJob(working_directory=str(tmp_path), python_path=str(script)), timeout=30)
    assert not result.success
    assert "must define a callable run()" in result.message
    with pytest.raises(ValueError, match="Exactly one"):
        ScriptHostJob(working_directory=str(tmp_path), python_path=str(script), script_text="x").validate()
