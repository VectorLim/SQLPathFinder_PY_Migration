"""Generated macro projects use one readable store and native control flow."""

import ast
import shutil
from pathlib import Path
from types import SimpleNamespace

import pytest

from tests.fixtures.generated_job import capture_jobs
from vg2c import compile_document, translate
from vg2c.emitter.models import CodeExpr
from vg2c.runtime import MacroStore
from vg2c.runtime.job import JobRuntime
from vg2c.utilities._emit_helpers import native_argument
from vg2c.utilities.csv_io import CsvIO
from vg2c.utilities.macro_control import MacroControl


def block(options, body=""):
    return (
        "<OPTIONS>\n" + options + "\n</OPTIONS>\n" + body + "\n<---- New Query ---->\n"
    )


def run_job(main):
    namespace = {"__file__": str(main), "__name__": "generated_job"}
    exec(compile(main.read_text(), str(main), "exec"), namespace)
    capture_jobs(namespace)
    namespace["run"]()
    return namespace["_job"]


def test_prepost_functions_leave_intervening_normal_queries_inline(tmp_path, monkeypatch):
    source = tmp_path / "prepost.txt"
    source.write_text(
        block("/ENGINE=SQLite\n/CSV=first.csv\n/PROMPT-TEXT=Step 1-1. Fetch first / data", "SELECT 1")
        + block("/ENGINE=SQLite\n/CSV=second.csv\n/PROMPT-TEXT=Step 1-2.", "SELECT 2")
        + block("/ENGINE=SQLite\n/CSV=normal.csv\n/PROMPT-TEXT=Step 2.0.", "SELECT 3")
        + block("/ENGINE=SQLite\n/CSV=last.csv\n/PROMPT-TEXT=Step 3-1. Finish!", "SELECT 4")
        + block("/ENGINE=SQLite\n/CSV=unmarked.csv", "SELECT 5")
    )
    main = translate(source)
    tree = ast.parse(main.read_text())
    run = next(
        node for node in tree.body if isinstance(node, ast.FunctionDef) and node.name == "run"
    )
    helpers = [
        node for node in tree.body if isinstance(node, ast.FunctionDef) and node.name != "run"
    ]
    assert [node.name for node in helpers] == ["prepost_1_fetch_first_data", "prepost_3_finish"]
    assert not any(isinstance(node, ast.FunctionDef) for node in run.body)
    assert all([argument.arg for argument in node.args.args] == ["job"] for node in helpers)
    inline_sql = [
        node.value.keywords[0].value.value
        for node in run.body
        if isinstance(node, ast.Expr)
        and isinstance(node.value, ast.Call)
        and isinstance(node.value.func, ast.Attribute)
        and node.value.func.attr == "sql"
    ]
    assert inline_sql == ["normal.csv", "unmarked.csv"]
    calls = []
    monkeypatch.setattr(JobRuntime, "sql", lambda self, path, *, output: calls.append(output))
    run_job(main)
    assert calls == ["first.csv", "second.csv", "normal.csv", "last.csv", "unmarked.csv"]


def test_prepost_prompt_names_are_unique_and_deterministic(tmp_path):
    source = tmp_path / "prepost_names.txt"
    source.write_text(
        block("/WRITE-FILE=Y\n/CSV=first.txt\n/PROMPT-TEXT=Step 1-1. Load-data", "first")
        + block("/WRITE-FILE=Y\n/CSV=normal.txt\n/PROMPT-TEXT=Step 2.", "normal")
        + block("/WRITE-FILE=Y\n/CSV=second.txt\n/PROMPT-TEXT=Step 1-2. Load data", "second")
        + block("/WRITE-FILE=Y\n/CSV=normal.txt\n/PROMPT-TEXT=Step 2.", "normal")
        + block("/WRITE-FILE=Y\n/CSV=third.txt\n/PROMPT-TEXT=Step 1-3. Load data 2", "third")
    )
    emitted = compile_document(source).emitted
    names = [
        node.name
        for node in ast.parse(emitted.source).body
        if isinstance(node, ast.FunctionDef) and node.name != "run"
    ]
    assert names == ["prepost_1_load_data", "prepost_1_load_data_2", "prepost_1_load_data_2_2"]
    assert compile_document(source).emitted.source == emitted.source
    main = translate(source)
    job = run_job(main)
    assert [job.read_file(name).strip() for name in ("first.txt", "second.txt", "third.txt")] == [
        "first", "second", "third"
    ]


def test_prepost_functions_share_job_and_preserve_macro_scopes(tmp_path):
    source = tmp_path / "prepost_macros.txt"
    source.write_text(
        block("/WRITE-FILE=Y\n/CSV=seed.csv\n/PROMPT-TEXT=Step 1-1.", "VALUE\ninside")
        + block('/UTILITIES={START-MACRO} "seed.csv" "N"\n/PROMPT-TEXT=Step 1-2.')
        + block("/WRITE-FILE=Y\n/CSV=inside.txt\n/PROMPT-TEXT=Step 1-3.", "<<<VALUE>>>")
        + block("/UTILITIES={END-MACRO}")
        + block("/WRITE-FILE=Y\n/CSV=normal.txt\n/PROMPT-TEXT=Step 2.", "<<<ENV_MODE>>>")
        + block("/WRITE-FILE=Y\n/CSV=last.txt\n/PROMPT-TEXT=Step 3-1.", "<<<ENV_MODE>>>")
    )
    main = translate(source)
    job = run_job(main)
    assert job.read_file("inside.txt").strip() == "inside"
    assert job.read_file("normal.txt").strip() == job.env_mode
    assert job.read_file("last.txt").strip() == job.env_mode
    assert "prepost_1(job, macros)" in main.read_text()


def test_prepost_groups_inside_normal_macro_scopes(tmp_path):
    source = tmp_path / "nested_groups.txt"
    source.write_text(
        block("/WRITE-FILE=Y\n/CSV=seed.csv", "VALUE\ninside")
        + block('/UTILITIES={START-MACRO} "seed.csv" "N"\n/PROMPT-TEXT=Step 2.')
        + block("/WRITE-FILE=Y\n/CSV=first.txt\n/PROMPT-TEXT=Step 3-1. First group", "<<<VALUE>>>")
        + block("/WRITE-FILE=Y\n/CSV=normal.txt\n/PROMPT-TEXT=Step 4.", "<<<VALUE>>>")
        + block("/WRITE-FILE=Y\n/CSV=last.txt\n/PROMPT-TEXT=Step 5-1. Last group", "<<<VALUE>>>")
        + block("/UTILITIES={END-MACRO}")
    )
    main = translate(source)
    code = main.read_text()
    assert "def prepost_3_first_group(job, macros):" in code
    assert "def prepost_5_last_group(job, macros):" in code
    assert "            prepost_3_first_group(job, macros)" in code
    assert "            prepost_5_last_group(job, macros)" in code
    job = run_job(main)
    assert all(job.read_file(name).strip() == "inside" for name in ("first.txt", "normal.txt", "last.txt"))


def test_unmarked_blocks_between_same_group_prompts_stay_in_group(tmp_path):
    source = tmp_path / "group_gap.txt"
    source.write_text(
        block("/WRITE-FILE=Y\n/CSV=first.txt\n/PROMPT-TEXT=Step 1-1. Setup", "first")
        + block("/WRITE-FILE=Y\n/CSV=middle.txt", "middle")
        + block("/WRITE-FILE=Y\n/CSV=last.txt\n/PROMPT-TEXT=Step 1-2. Finish", "last")
    )
    emitted = compile_document(source).emitted
    functions = [node for node in ast.parse(emitted.source).body if isinstance(node, ast.FunctionDef)]
    assert [node.name for node in functions] == ["prepost_1_setup", "run"]
    assert all(step.source_range.start_offset < emitted.source.index("def run():") for step in emitted.steps)


def test_prepost_group_includes_unmarked_instance_consolidation(tmp_path):
    source = tmp_path / "query_group.txt"
    source.write_text(
        block(
            "/ENGINE=SQLite\n/CSV=a0.tab\n/INSTANCE=42\n/PROMPT-TEXT=Step 3-1.1-a0. Fetch data",
            "SELECT 1 AS value",
        )
        + block(
            "/ENGINE=SQLite\n/CSV=result.csv\n/INSTANCE=42\n/TABLE=a0.tab\n/DELETE=*Instance*",
            "SELECT * FROM a0",
        )
        + block("/ENGINE=SQLite\n/CSV=normal.csv\n/PROMPT-TEXT=Step 4.0.", "SELECT 2")
    )
    emitted = compile_document(source).emitted
    run_offset = emitted.source.index("def run():")
    assert all(step.source_range.start_offset < run_offset for step in emitted.steps[:2])
    assert emitted.steps[2].source_range.start_offset > run_offset


def test_csr_iam_prepost_layout():
    source = Path(__file__).resolve().parents[2] / "CSR_IAM_v2.txt"
    emitted = compile_document(source).emitted
    functions = [
        node for node in ast.parse(emitted.source).body if isinstance(node, ast.FunctionDef)
    ]
    helpers = [node for node in functions if node.name.startswith("prepost_")]
    assert [node.name.split("_")[1] for node in helpers] == ["1", "3", "6", "7", "8", "9", "10"]
    run = next(node for node in functions if node.name == "run")
    assert not any(isinstance(node, ast.FunctionDef) for node in ast.walk(run) if node is not run)
    assert not any(
        isinstance(node, ast.Call)
        and isinstance(node.func, ast.Attribute)
        and node.func.attr == "sql"
        for node in ast.walk(run)
    )
    query = next(node for node in helpers if node.name.startswith("prepost_3_"))
    assert len([
        node for node in ast.walk(query)
        if isinstance(node, ast.Call)
        and isinstance(node.func, ast.Attribute)
        and node.func.attr == "sql"
    ]) == 3


def test_prepost_inference_does_not_split_mixed_control_scopes(tmp_path):
    source = tmp_path / "mixed_scope.txt"
    source.write_text(
        block('/UTILITIES={START-MACRO} "seed.csv" "N"\n/PROMPT-TEXT=Step 1-1.')
        + block("/WRITE-FILE=Y\n/CSV=normal.txt\n/PROMPT-TEXT=Step 2.", "<<<VALUE>>>")
        + block("/UTILITIES={END-MACRO}")
    )
    emitted = compile_document(source).emitted
    assert "def prepost_" not in emitted.source
    assert "with macros.scope" in emitted.source


def test_write_file_macro_arguments_strip_only_leading_whitespace(
    tmp_path, monkeypatch
):
    source = tmp_path / "macro_command.txt"
    payload = "Sfolder,underDEV,useCSR,useMMS\nICMPCS_CWFNCO_CSR_IAM,N,Y,Y\n\n"
    source.write_text(
        block("/WRITE-FILE=Y\n/CSV=macrotmp.csv", "\n \t\n  " + payload)
        + block('/UTILITIES={START-MACRO} "macrotmp.csv" "N"')
        + block(
            "/UTILITIES=setsiteparam.exe KM <<<SFOLDER>>> <<<UNDERDEV>>> "
            "<<<USECSR>>> <<<USEMMS>>>"
        )
        + block("/UTILITIES={END-MACRO}")
    )
    calls = []

    def fake_run(argv, **kwargs):
        calls.append(argv)
        return SimpleNamespace(returncode=0)

    monkeypatch.setattr("vg2c.runtime.files.subprocess.run", fake_run)
    monkeypatch.setattr("vg2c.runtime.files.sys", SimpleNamespace(platform="win32"))
    main = translate(source)
    workdir = main.parent / "output"
    job = run_job(main)
    assert job.read_file("macrotmp.csv") == payload
    assert (main.parent / "files/macrotmp.csv").read_text() == payload
    assert not (workdir / "macrotmp.csv").exists()
    assert calls == [["setsiteparam.exe", "KM", "ICMPCS_CWFNCO_CSR_IAM", "N", "Y", "Y"]]


def test_scopes_and_independent_runs_and_empty_csv(tmp_path):
    source = tmp_path / "macros.txt"
    source.write_text(
        block('/UTILITIES={START-MACRO} "outer.csv" "N"')
        + block("/WRITE-FILE=Y\n/CSV=outer.txt", "<<<LABEL>>>")
        + block('/UTILITIES={START-MACRO} "inner.csv" "N"')
        + block("/WRITE-FILE=Y\n/CSV=inner.txt", "<<<LABEL>>>")
        + block("/UTILITIES={END-MACRO}")
        + block("/WRITE-FILE=Y\n/CSV=restored.txt", "<<<LABEL>>>")
        + block("/UTILITIES={END-MACRO}")
    )
    emitted = compile_document(source).emitted
    code = emitted.source
    tree = ast.parse(code)
    assert "macro_values_" not in code
    assert "macro_values =" not in code
    assert "job_entrypoint" not in code
    assert (
        "job = JobRuntime.from_script(__file__, test_mode_excluded=TEST_MODE_EXCLUDED)"
        in code
    )
    assert "macros = job.macros" in code
    assert "with macros.scope(" in code
    assert any(isinstance(node, ast.With) for node in ast.walk(tree))
    assert any(isinstance(node, ast.If) for node in ast.walk(tree))
    assert [step.block_index for step in emitted.steps] == [1, 3, 5]
    previous_end = 0
    for step, name in zip(emitted.steps, ("outer", "inner", "restored"), strict=True):
        span = step.source_range
        assert previous_end <= span.start_offset < span.end_offset
        assert code[span.start_offset : span.end_offset].strip() == (
            f"job.update_file(path='{name}.txt', asset='files/{name}.txt')"
        )
        previous_end = span.end_offset

    main = translate(source)
    for label in ["first", "second"]:
        project = tmp_path / label
        shutil.copytree(main.parent, project)
        folder = project / "output"
        folder.mkdir()
        (folder / "outer.csv").write_text("LABEL\n" + label + "\nignored\n")
        (folder / "inner.csv").write_text("LABEL\ninner\n")
        job = run_job(project / "main.py")
        assert job.read_file("outer.txt").strip() == label
        assert job.read_file("inner.txt").strip() == "inner"
        assert job.read_file("restored.txt").strip() == label

    empty = main.parent / "output"
    empty.mkdir()
    (empty / "outer.csv").write_text("LABEL\n")
    run_job(main)
    assert not (empty / "outer.txt").exists()
    assert not (empty / "inner.txt").exists()


def test_native_numeric_branch_and_editable_macro_assignment(tmp_path):
    source = tmp_path / "conditional.txt"
    source.write_text(
        block('/UTILITIES={ROWS-IN-FILE} "rows.csv" "COUNT" "N"')
        + block('/UTILITIES={IF-THEN} "COUNT" "GT" "0" "" "" "" ""')
        + block("/WRITE-FILE=Y\n/CSV=positive.txt", "<<<COUNT>>>")
        + block("/UTILITIES={ELSE}")
        + block("/WRITE-FILE=Y\n/CSV=empty.txt", "zero")
        + block("/UTILITIES={END-IF}")
    )
    emitted = compile_document(source).emitted
    code = emitted.source
    ast.parse(code)
    # <<<COUNT>>> is read again in the branch, so the macro stays assigned.
    assert "macros['COUNT'] = str(job.row_count('rows.csv'))" in code
    assert "if int(macros['COUNT']) > 0:" in code
    assert "macro_values_" not in code
    main = translate(source)
    folder = main.parent / "output"
    folder.mkdir()
    (folder / "rows.csv").write_text("ID\n1\n")
    job = run_job(main)
    assert job.read_file("positive.txt").strip() == "1"


@pytest.mark.parametrize(
    "value, expected",
    [
        ("EMAIL:<<<dEmail>>>", "EMAIL:recipient@example.com"),
        ("<<<first>>><<<SECOND>>>", "onetwo"),
        (["<<<FIRST>>>.csv", ("<<<SECOND>>>", "T0")], ["one.csv", ("two", "T0")]),
        ({"field": "<<<x > y>>>"}, {"field": "comparison"}),
        ("<<<line\nbreak>>>", "multiline"),
    ],
)
def test_macro_discovery_in_all_argument_values(value, expected):
    expression = native_argument(value, "macros")
    assert isinstance(expression, CodeExpr)
    assert not expression.has_value
    macros = MacroStore(
        initial={
            "dEmail": "recipient@example.com",
            "first": "one",
            "second": "two",
            "x > y": "comparison",
            "line\nbreak": "multiline",
        }
    )
    assert eval(expression.source, {"macros": macros}) == expected
    if isinstance(value, str):
        assert macros.substitute(value) == expected
    assert native_argument("ordinary value", "macros") == "ordinary value"


def test_native_macro_assignment_tracks_nested_row_count_arguments():
    row_count = CsvIO.row_count.render("<<<ROW_FILE>>>")
    assignment = MacroControl.render_named_assignment(
        MacroControl.set_named.render("COUNT", CodeExpr(f"str({row_count})")),
        "macros",
    )

    assert str(assignment) == "macros['COUNT'] = str(job.row_count(macros['ROW_FILE']))"


def test_generated_sql_and_html_options_discover_macros(tmp_path, monkeypatch):
    source = tmp_path / "options.txt"
    source.write_text(
        block('/UTILITIES={START-MACRO} "<<<CL_CONFIG>>>" "N"')
        + block(
            "/ENGINE=SQLite\n/CSV=<<<RESULT>>>\n/TABLE=<<<INPUT>>>:T0",
            "select * from T0",
        )
        + block(
            "/REPORT=HTML-LAYOUT",
            ":FILE:EMAIL:<<<dEmail>>>\n:CSS:<<<STYLE>>>\n<p><<<FIRST>>></p>",
        )
        + block("/UTILITIES={END-MACRO}")
    )
    emitted = compile_document(source).emitted
    code = emitted.source
    assert "job.read_macro_row(macros['CL_CONFIG'])" in code
    assert "output=macros['RESULT']" in code
    assert "inputs=[(macros['INPUT'], 'T0')]" in code
    assert "output='EMAIL:' + macros['DEMAIL']" in code
    assert "css_file=macros['STYLE']" in code
    calls = []
    monkeypatch.setattr(
        "vg2c.runtime.job.render_html", lambda path, **kwargs: calls.append(kwargs)
    )
    main = translate(source)
    work = main.parent / "output"
    work.mkdir()
    (work / "config.csv").write_text(
        "RESULT,INPUT,dEmail,STYLE,FIRST\nresult.csv,data.csv,recipient@example.com,style.css,one\n"
    )
    (work / "data.csv").write_text("VALUE\nvalue\n")
    # CL_CONFIG is a per-run value rather than an environment variable.
    namespace = {"__file__": str(main), "__name__": "generated_job"}
    exec(compile(main.read_text(), str(main), "exec"), namespace)

    class ConfiguredJob(JobRuntime):
        def __init__(self, **kwargs):
            super().__init__(**kwargs, values={"CL_CONFIG": "config.csv"})

    namespace["JobRuntime"] = ConfiguredJob
    namespace["run"]()
    assert (work / "result.csv").read_text() == "VALUE\nvalue\n"
    assert calls[0]["output"] == "EMAIL:recipient@example.com"
    assert calls[0]["values"]["VALUE_1"] == "one"
