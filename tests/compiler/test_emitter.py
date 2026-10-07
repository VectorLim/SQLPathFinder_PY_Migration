"""Clean output shape, exact API mapping, source identity and one runtime smoke."""

import ast
import csv
import subprocess
import sys
from collections import Counter
from pathlib import Path
from types import SimpleNamespace

import pytest

from scripthost_portable import PortableScriptHostRuntime, aed_api, script_api
from vg2c import CompileError, compile_document, translate
from vg2c.emitter.globals import render_sql
from vg2c.emitter.literals import string_literal

ROOT = Path(__file__).resolve().parents[2]
TARGETS = ["ICMPCS.txt", "output/aed-migration/CSR_IAM_v2.aed.txt"]


def block(options, body=""):
    return "<OPTIONS>\n" + options + "\n</OPTIONS>\n" + body


def compile_blocks(tmp_path, *blocks):
    path = tmp_path / "job.txt"
    path.write_text("\n<---- New Query ---->\n".join(blocks), encoding="utf-8")
    return compile_document(path)


def capture_calls(monkeypatch):
    calls = []

    def capture(name):
        def invoke(*args, **kwargs):
            calls.append((name, args, kwargs))
            return True

        return invoke

    class Macros:
        load_csv = staticmethod(capture("macros.load_csv"))
        compare = staticmethod(capture("macros.compare"))

        def __getitem__(self, name):
            return f"NODE_{name}"

    monkeypatch.setattr(script_api, "macros", Macros())
    for receiver, methods in {
        "query": ["run"],
        "utilities": ["rows_in_file"],
        "aed": ["process"],
        "reports": ["run", "defer", "layout", "delete"],
    }.items():
        monkeypatch.setattr(
            script_api,
            receiver,
            SimpleNamespace(**{method: capture(f"{receiver}.{method}") for method in methods}),
        )
    return calls


@pytest.mark.parametrize(
    "target,condition_count,row_count,block_count,duration",
    [
        (TARGETS[0], 1, 1, 17, "TRUNC(SYSDATE) - 2"),
        (TARGETS[1], 2, 2, 20, "SYSDATE - 1"),
    ],
)
def test_real_jobs_clean_shape_and_exact_api_mapping(
    monkeypatch,
    target,
    condition_count,
    row_count,
    block_count,
    duration,
):
    result = compile_document(ROOT / target)
    source = result.emitted.source
    tree = ast.parse(source)
    assert len(result.resolved.blocks) == block_count
    assert [n.name for n in tree.body if isinstance(n, ast.FunctionDef)] == ["run"]
    imports = [n for n in ast.walk(tree) if isinstance(n, (ast.Import, ast.ImportFrom))]
    assert len(imports) == 1
    assert imports[0].module == "scripthost_portable.script_api"
    assert {n.name for n in imports[0].names} == {"aed", "macros", "query", "reports", "utilities"}
    assert not any(isinstance(n, (ast.With, ast.For, ast.Pass)) for n in ast.walk(tree))
    for forbidden in (
        "ctx",
        "script_session",
        "step_000",
        "SPFManager",
        "SPFTaskBase",
        "SPFGlobals",
        "MemTable",
        "parentMacTables",
        "Rowidx",
        "MyMode",
        "Substitute_Macro",
        "Process_Query",
        "<OPTIONS>",
        "<vg2c:block",
        "SQL filter metadata",
    ):
        assert forbidden not in source
    calls = capture_calls(monkeypatch)
    namespace = {"__name__": "generated_job"}
    exec(compile(source, target, "exec"), namespace)
    assert namespace["OPERATION"] == "2303"
    assert namespace["DURATION"] == duration
    namespace["run"]()
    counts = Counter(name for name, _, _ in calls)
    assert counts == {
        "macros.load_csv": 1,
        "macros.compare": condition_count,
        "utilities.rows_in_file": row_count,
        "query.run": 5,
        "aed.process": 1,
        "reports.run": 1,
        "reports.defer": 1,
        "reports.layout": 2,
        "reports.delete": 2,
    }
    names = [name for name, _, _ in calls]
    assert names[:4] == ["reports.run", "reports.layout", "reports.delete", "macros.load_csv"]
    assert names[-3:] == ["reports.defer", "reports.layout", "reports.delete"]
    queries = [kwargs for name, _, kwargs in calls if name == "query.run"]
    assert [q["engine"] for q in queries] == ["VA", "VA", "SQLite", "SQLite", "SQLite"]
    assert [q["output"] for q in queries[-3:]] == [
        "PARMI_IPM_RAW.csv",
        "IPM_Data.csv",
        "AED_CANDIDATES.csv",
    ]
    assert queries[0]["node"] == "NODE_MARS" and queries[1]["node"] == "NODE_ARIES"
    assert "SQL_Get_CSV_List" in queries[1]["sql"]
    assert "CrossTab->" in queries[2]["sql"]
    query_names = {"NODE": "node", "ENGINE": "engine", "TABLE": "tables", "CSV": "output"}
    query_names.update({legacy: public for public, legacy in script_api._QUERY_OPTIONS.items()})
    original_queries = [b for b in result.resolved.blocks if "ENGINE" in b.options.lookup]
    for original, emitted in zip(original_queries, queries):
        assert emitted["sql"] == original.body.strip()
        for key, value in original.options.pairs:
            if value.startswith("<<<") and value.endswith(">>>"):
                value = "NODE_" + value[3:-3]
            assert emitted[query_names[key]] == value
    original_reports = [b for b in result.resolved.blocks if "REPORT" in b.options.lookup]
    emitted_reports = [c for c in calls if c[0].startswith("reports.")]
    report_names = {legacy: public for public, legacy in script_api._REPORT_OPTIONS.items()}
    report_names["ID"] = "report_id"
    for original, (_, args, kwargs) in zip(original_reports, emitted_reports):
        if original.options.lookup["REPORT"] != "HTML-DELETE":
            assert args == (original.body.strip(),)
        for key, value in original.options.pairs:
            if key != "REPORT":
                assert kwargs[report_names[key]] == value
    run = next(n for n in tree.body if isinstance(n, ast.FunctionDef))
    macro = run.body[3]
    assert isinstance(macro, ast.If) and isinstance(macro.test, ast.Call)
    assert isinstance(macro.test.func, ast.Attribute) and macro.test.func.attr == "load_csv"
    conditions = [n for n in macro.body if isinstance(n, ast.If)]
    assert len(conditions) == condition_count  # CSR conditions are siblings.
    if condition_count == 2:
        assert any(
            isinstance(n, ast.Expr)
            and isinstance(n.value, ast.Call)
            and isinstance(n.value.func, ast.Attribute)
            and n.value.func.attr == "process"
            for n in conditions[0].body
        )
    else:
        assert not any(
            isinstance(n, ast.Call)
            and isinstance(n.func, ast.Attribute)
            and n.func.attr == "process"
            for n in ast.walk(conditions[0])
        )


def test_metadata_ranges_and_ids_differentiate_repeated_operations(tmp_path):
    inputs = [
        block('/UTILITIES={ROWS-IN-FILE} "é.csv" "COUNT"'),
        block('/UTILITIES={ROWS-IN-FILE} "second.csv" "COUNT"'),
    ]
    result = compile_blocks(tmp_path, *inputs)
    blocks = result.emitted.blocks
    assert len(blocks) == 2
    ids = [b.invocations[0].id for b in blocks]
    assert ids == ["block-0:utilities.rows_in_file", "block-1:utilities.rows_in_file"]
    for emitted in blocks:
        span = emitted.source_range
        assert result.emitted.source[span.start_offset : span.end_offset] == emitted.source
        assert emitted.input_span.file == tmp_path / "job.txt"
        for parameter in emitted.invocations[0].parameters:
            span = parameter.source_range
            assert result.emitted.source[span.start_offset : span.end_offset] == parameter.source
    original = (tmp_path / "job.txt").read_bytes()
    (tmp_path / "job.txt").write_bytes(original.replace(b"\n", b"\r\n"))
    assert [
        b.invocations[0].id for b in compile_document(tmp_path / "job.txt").emitted.blocks
    ] == ids


@pytest.mark.parametrize(
    "options,body",
    [
        ("/ENGINE=SQLite\n/CSV=x.csv\n/UNKNOWN=", "SELECT 1"),
        ("/ENGINE=VA\n/OLEDB=SQLite\n/CSV=x.csv", "SELECT 1"),
        ("/ENGINE=SQLite", "SELECT 1"),
        ('/UTILITIES={ROWS-IN-FILE} "x.csv" "COUNT" "Y"', ""),
        ('/UTILITIES={ROWS-IN-FILE} "x.zip" "COUNT" "N" "inner.csv"', ""),
        ('/UTILITIES={AED} "x.csv" "extra"', ""),
        ('/UTILITIES={AED} "x.csv"\n/WORKDIR=other', ""),
        ('/UTILITIES={AED} "x.csv"', "lost work"),
        ("/REPORT=HTML-DEFER", "template"),
        ("/REPORT=HTML-DELETE\n/OUTLOOK=Y", "N/A"),
        ("/REPORT=HTML-DELETE", "lost template"),
    ],
)
def test_unsupported_work_never_generates_successful_output(tmp_path, options, body):
    with pytest.raises(CompileError):
        compile_blocks(tmp_path, block(options, body))


@pytest.mark.parametrize(
    "text",
    [
        'line 1\nC:\\folder\\x.csv\nline 3"',
        "line1\nline2''",
        'line1\n"""four quotes""""\nline3',
        "line1\n\"\"\"\n'''\nline3",
        "one\nnull:\0 and braces: {x} and literal \\n",
    ],
)
def test_multiline_literals_preserve_content(text):
    assert ast.literal_eval(string_literal(text)) == text


def test_settings_change_only_selected_predicates_and_escape_strings():
    sql = "SELECT 'operation = ''9999''' AS text FROM dual\nWHERE operation = '2303'\nAND out_date >= SYSDATE - 1; -- operation = '42'\n"
    constants = {}
    expression = render_sql(sql, constants)
    assert constants == {"OPERATION": "2303", "DURATION": "SYSDATE - 1"}
    assert eval(expression, {}, constants) == sql
    constants.update(OPERATION="2'303", DURATION="TRUNC(SYSDATE) - 3")
    edited = eval(expression, {}, constants)
    assert "operation = '2''303'" in edited
    assert "out_date >= TRUNC(SYSDATE) - 3" in edited
    assert "-- operation = '42'" in edited
    assert "'operation = ''9999'''" in edited


def test_duration_prefix_of_longer_arithmetic_is_not_promoted():
    for value in ("SYSDATE - 1.5", "SYSDATE - 1 / 2", "SYSDATE - 12foo"):
        constants = {}
        sql = f"SELECT * FROM dual WHERE out_date >= {value};"
        assert ast.literal_eval(render_sql(sql, constants)) == sql
        assert not constants


def test_generated_module_invokes_original_stage1_api_offline(tmp_path, monkeypatch):
    monkeypatch.setenv("SCRIPTHOST_FORCE_PORTABLE_QUERY_TRANSPORT", "1")
    monkeypatch.setenv("STAGE2_SIGNAL", "0")
    (tmp_path / "config.csv").write_text("SOURCE\nmeasurements.csv:measurements\nignored.csv\n")
    (tmp_path / "measurements.csv").write_text("lot,value\nA,1\nB,0\n")
    result = compile_blocks(
        tmp_path,
        block('/UTILITIES={START-MACRO} "config.csv" "N"'),
        block(
            "/ENGINE=SQLite\n/TABLE=<<<SOURCE>>>\n/CSV=candidates.csv\n/QUOTECSV=Y",
            "SELECT lot AS LOT FROM measurements WHERE value > 0",
        ),
        block('/UTILITIES={ROWS-IN-FILE} "candidates.csv" "STAGE2_SIGNAL" "N"'),
        block('/UTILITIES={IF-THEN} "STAGE2_SIGNAL" "GT" "0"'),
        block('/UTILITIES={AED} "candidates.csv"'),
        block("/UTILITIES={END-IF}"),
        block("/UTILITIES={END-MACRO}"),
    )
    seen = []
    monkeypatch.setattr(aed_api, "process_candidates", lambda path: seen.append(path))
    namespace = {"__name__": "generated_job"}
    exec(compile(result.emitted.source, "generated_job.py", "exec"), namespace)
    assert PortableScriptHostRuntime().run_python(namespace["run"], tmp_path)
    assert seen == ["candidates.csv"]
    with (tmp_path / "candidates.csv").open(encoding="utf-8-sig") as stream:
        assert list(csv.reader(stream)) == [["LOT"], ["A"]]
    assert script_api._current_manager is None


def test_generated_imports_work_when_compiler_is_blocked(tmp_path):
    output = translate(ROOT / TARGETS[0], tmp_path)
    code = (
        "import runpy, sys\n"
        "class Guard:\n"
        "    def find_spec(self, fullname, path=None, target=None):\n"
        "        if fullname.split('.')[0] == 'vg2c':\n"
        "            raise AssertionError(fullname)\n"
        "sys.meta_path.insert(0, Guard())\n"
        f"namespace = runpy.run_path({str(output)!r})\n"
        "assert callable(namespace['run'])\n"
    )
    completed = subprocess.run([sys.executable, "-c", code], capture_output=True, text=True)
    assert completed.returncode == 0, completed.stderr


def test_failed_translation_preserves_existing_output(tmp_path):
    source = tmp_path / "bad.txt"
    source.write_text(block('/UTILITIES={RUN-LOOP} "x.csv"'))
    output = source.with_suffix(".py")
    output.write_text("keep this")
    with pytest.raises(CompileError):
        translate(source)
    assert output.read_text() == "keep this"
    with pytest.raises(ValueError, match="empty job"):
        compile_blocks(tmp_path, "")


def test_cli_compiles_targets_and_rejects_bad_input(tmp_path):
    command = [
        sys.executable,
        "-m",
        "vg2c",
        *(str(ROOT / p) for p in TARGETS),
        "--out-dir",
        str(tmp_path),
    ]
    completed = subprocess.run(command, capture_output=True, text=True)
    assert completed.returncode == 0, completed.stderr
    for target in TARGETS:
        ast.parse((tmp_path / Path(target).with_suffix(".py").name).read_text())
    bad = tmp_path / "bad.txt"
    bad.write_text(block("/REPORT=PDF", "unsupported"))
    completed = subprocess.run(
        [sys.executable, "-m", "vg2c", str(bad)], capture_output=True, text=True
    )
    assert completed.returncode == 1 and "unsupported-report" in completed.stderr
    assert not bad.with_suffix(".py").exists()
