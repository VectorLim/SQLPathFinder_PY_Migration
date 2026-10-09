"""VG2 -> Python compiler: generated code reproduces the original task sequence exactly."""

import ast
import subprocess
import sys
from pathlib import Path

import pytest

from scripthost_portable import PortableScriptHostRuntime, aed_api
from scripthost_portable.task_inputs import (
    TaskInput,
    option_pairs,
    task_delimiter,
    utility_input,
)
from vg2c import CompileError, compile_document, translate
from vg2c.emitter.literals import py_string, string_literal
from vg2c.frontend import read_source

ROOT = Path(__file__).resolve().parents[2]
FULL_JOBS = [
    "tests/fixtures/aed_migration/ICMPCS.txt",
    "tests/fixtures/aed_migration/CSR_IAM_v2.txt",
]
AED_JOBS = [
    "ICMPCS.txt",
    "output/aed-migration/CSR_IAM_v2.aed.txt",
]  # smaller AED-integrated variants
DELIMITER = "<---- New Query ---->"


def block(options, body=""):
    return "<OPTIONS>\n" + options + "\n</OPTIONS>\n" + body


def compile_blocks(tmp_path, *blocks):
    path = tmp_path / "job.txt"
    path.write_text(DELIMITER.join("\n" + item for item in blocks), encoding="utf-8")
    return compile_document(path)


def _value(node):
    return ast.literal_eval(node)


def _call_input(call: ast.Call) -> TaskInput:
    """TaskInput a script.*/remote.* call sends to ScriptHost."""
    method = call.func.attr
    keywords = {keyword.arg: _value(keyword.value) for keyword in call.keywords}
    options, command = keywords.get("options"), keywords.get("command", "")
    arguments = [_value(argument) for argument in call.args]
    if method == "invoke":
        return TaskInput(option_pairs(options), command)
    if method == "command":
        return TaskInput((("UTILITIES", arguments[0]), *option_pairs(options)))
    if method == "else_branch":
        return utility_input("{ELSE}", (), options)
    name, *rest = arguments
    return utility_input(
        name if keywords.get("external") else "{" + name + "}", rest, options, command
    )


_ROUTES = {
    "if_then": ("{IF-THEN}", "{END-IF}"),
    "if_else": ("{IF-THEN}", "{END-IF}"),
    "macro": ("{START-MACRO}", "{END-MACRO}"),
    "for_loop": ("{FOR-LOOP}", "{END-LOOP}"),
    "site_loop": ("{SITE-LOOP}", "{END-LOOP}"),
    "run_loop": ("{RUN-LOOP}", "{END-LOOP}"),
    "hpc": ("{BEGIN-HPC}", "{END-HPC}"),
}


def flatten(statements) -> list[TaskInput]:
    """Task inputs in the order a VG2 file would list them."""
    inputs = []
    for statement in statements:
        if isinstance(statement, ast.Expr):
            inputs.append(_call_input(statement.value))
        elif isinstance(statement, ast.With) and isinstance(
            statement.items[0].context_expr, ast.Name
        ):
            inputs += flatten(statement.body)  # 'with iteration:'
        elif isinstance(statement, ast.With):
            call = statement.items[0].context_expr
            keywords = {keyword.arg: _value(keyword.value) for keyword in call.keywords}
            header, closer = _ROUTES[call.func.attr]
            inputs.append(
                utility_input(
                    header, [_value(a) for a in call.args], keywords.get("options")
                )
            )
            if call.func.attr == "if_else":
                branch = statement.body[0]
                inputs += flatten(branch.body)
                inputs.append(utility_input("{ELSE}", (), keywords.get("else_options")))
                inputs += flatten(branch.orelse)
            else:
                inputs += flatten(statement.body)
            inputs.append(utility_input(closer, (), keywords.get("end_options")))
        elif isinstance(statement, (ast.If, ast.For)):
            inputs += flatten(statement.body)
        elif not isinstance(statement, ast.Pass):
            raise AssertionError(ast.dump(statement))
    return inputs


def utilities_first(task_input: TaskInput):
    utilities = tuple(pair for pair in task_input.options if pair[0] == "UTILITIES")
    return (
        utilities
        + tuple(pair for pair in task_input.options if pair[0] != "UTILITIES"),
        task_input.command,
    )


@pytest.mark.parametrize("target", FULL_JOBS + AED_JOBS)
def test_generated_python_lists_every_original_task_input(target):
    path = ROOT / target
    result = compile_document(path)
    source = result.emitted.source
    tree = ast.parse(source, feature_version=(3, 11))
    run = next(node for node in tree.body if isinstance(node, ast.FunctionDef))
    assert [node.name for node in tree.body if isinstance(node, ast.FunctionDef)] == [
        "run"
    ]
    imports = [
        node
        for node in ast.walk(tree)
        if isinstance(node, (ast.Import, ast.ImportFrom))
    ]
    assert [
        (node.module, {alias.name for alias in node.names}) for node in imports
    ] == [("scripthost_portable.script_api", {"controls", "script"})]
    for forbidden in (
        "<OPTIONS>",
        DELIMITER,
        "SPFLib",
        "vg2c",
        "Process_Query",
        "parentMacTables",
    ):
        assert forbidden not in source

    text, _ = read_source(path.read_bytes())
    original = [
        TaskInput.parse(item) for item in text.split(task_delimiter()) if item.strip()
    ]
    assert [utilities_first(task_input) for task_input in flatten(run.body)] == list(
        map(utilities_first, original)
    )

    indices = sorted(emitted.block_index for emitted in result.emitted.blocks)
    assert indices == [
        index for index, item in enumerate(text.split(task_delimiter())) if item.strip()
    ]
    for emitted in result.emitted.blocks:
        assert (
            source[emitted.source_range.start_offset : emitted.source_range.end_offset]
            == emitted.source
        )


def test_source_spans_follow_the_original_split():
    data = (
        "\ufeff\r\n".encode()
        + f"{DELIMITER}\r\n\r\n{block('/WRITE-FILE=Y', 'x')}\r\n{DELIMITER}{DELIMITER}\n".encode()
    )
    text, spans = read_source(data, Path("job.txt"))
    assert "\r" not in text and not text.startswith("\ufeff")
    assert len(spans) == len(text.split(DELIMITER)) == 4
    assert [(span.start_line, span.end_line) for span in spans] == [
        (2, 2),
        (4, 8),
        (8, 8),
        (9, 9),
    ]
    with pytest.raises(CompileError, match="UTF-8"):
        read_source(b"\xff\xfe")


def test_leaf_forms_and_options_keep_the_original_input(tmp_path):
    result = compile_blocks(
        tmp_path,
        block("/WRITE-FILE=Y\n/CSV=a.csv\n/CSV=b.csv", "line\n  two\n"),
        block('/WORKDIR=.\\\n/UTILITIES=dir /b | find "x"  >  out.txt'),
        block('/UTILITIES=@EXEDIR@\\SPFDelete.bat "a.csv" "N"'),
        block('/UTILITIES={ROWS-IN-FILE} "data.csv" "ROWS" "N" ""'),
        block("/UTILITIES={ROWS-IN-FILE} data.csv ROWS"),
        block("/UTILITIES={PYSCRIPT:CHECK}", "print('x')"),
    )
    source = result.emitted.source
    assert 'options=[("WRITE-FILE", "Y"), ("CSV", "a.csv"), ("CSV", "b.csv")]' in source
    assert (
        'script.command(\'dir /b | find "x"  >  out.txt\', options={"WORKDIR": ".\\\\"})'
        in source
    )
    assert (
        'script.utility(r"@EXEDIR@\\SPFDelete.bat", "a.csv", "N", external=True)'
        in source
    )
    assert 'script.utility("ROWS-IN-FILE", "data.csv", "ROWS", "N", "")' in source
    assert (
        'script.invoke(options={"UTILITIES": "{ROWS-IN-FILE} data.csv ROWS"})' in source
    )
    assert 'script.utility("PYSCRIPT:CHECK", command="print(\'x\')")' in source


def test_controls_use_native_python_scopes(tmp_path):
    result = compile_blocks(
        tmp_path,
        block('/UTILITIES={START-MACRO} "m.csv" "N"'),
        block('/UTILITIES={FOR-LOOP} "1" "3" "1" "1" "N"'),
        block('/UTILITIES={IF-THEN} "A" "EQ" "1"'),
        block("/WRITE-FILE=Y\n/CSV=x", "x"),
        block("/UTILITIES={ELSE}"),
        block('/UTILITIES={IF-THEN} "B" "EQ" "1"'),
        block("/UTILITIES={END-IF}"),
        block("/UTILITIES={END-IF}"),
        block("/UTILITIES={END-LOOP}"),
        block("/UTILITIES={END-MACRO}"),
    )
    assert result.emitted.source.split("\n", 3)[3] == (
        "def run():\n"
        '    with controls.macro("m.csv", "N") as macro:\n'
        "        if macro.active:\n"
        '            with controls.for_loop("1", "3", "1", "1", "N") as loop:\n'
        "                for iteration in loop:\n"
        "                    with iteration:\n"
        '                        with controls.if_else("A", "EQ", "1") as condition:\n'
        "                            if condition.matched:\n"
        '                                script.invoke(options={"WRITE-FILE": "Y", "CSV": "x"}, command="x")\n'
        "                            else:\n"
        '                                with controls.if_then("B", "EQ", "1") as condition_2:\n'
        "                                    if condition_2.matched:\n"
        "                                        pass\n"
        '\n\nif __name__ == "__main__":\n'
        '    raise SystemExit("Use python -m scripthost_portable.launcher <job.py> --workdir <directory>.")\n'
    )


@pytest.mark.parametrize(
    ("blocks", "code", "index"),
    [
        ([block('/UTILITIES={IF-THEN} "A" "EQ" "1"')], "missing-end", 0),
        (
            [
                block('/UTILITIES={START-MACRO} "m.csv" "N"'),
                block("/UTILITIES={END-LOOP}"),
            ],
            "missing-end",
            0,
        ),
        (
            [block("/UTILITIES={IF-THEN} A EQ 1"), block("/UTILITIES={END-IF}")],
            "control-arguments",
            0,
        ),
        (
            [
                block("/WRITE-FILE=Y", "x"),
                block("/UTILITIES={ELSE}"),
                block("/UTILITIES={END-IF}"),
            ],
            "unsupported-control",
            1,
        ),
        ([block('/UTILITIES={NO-SUCH} "x"')], "scripthost-inspection", 0),
    ],
)
def test_structures_without_a_faithful_python_form_are_rejected(
    tmp_path, blocks, code, index
):
    with pytest.raises(CompileError) as error:
        compile_blocks(tmp_path, *blocks)
    assert (error.value.code, error.value.block_index) == (code, index)


@pytest.mark.parametrize(
    "text",
    [
        'line 1\nC:\\folder\\x.csv\nline 3"',
        "line1\nline2''",
        'line1\n"""four quotes""""\nline3',
        "line1\n\"\"\"\n'''\nline3",
        "one\nnull:\0 and braces: {x} and literal \\n",
        "path\\\n",
    ],
)
def test_multiline_literals_preserve_content(text):
    assert ast.literal_eval(string_literal(text)) == text


@pytest.mark.parametrize(
    "text", ["", ".\\", r"@EXEDIR@\x.va", "it's", 'say "hi"', "tab\there", "é"]
)
def test_single_line_literals_preserve_content(text):
    assert ast.literal_eval(py_string(text)) == text


def test_generated_module_runs_through_the_original_runtime(tmp_path, monkeypatch):
    monkeypatch.setenv("SCRIPTHOST_FORCE_PORTABLE_QUERY_TRANSPORT", "1")
    (tmp_path / "config.csv").write_text(
        "SOURCE\nmeasurements.csv:measurements\nignored.csv\n"
    )
    (tmp_path / "measurements.csv").write_text("lot,value\nA,1\nB,0\n")
    result = compile_blocks(
        tmp_path,
        block('/UTILITIES={START-MACRO} "config.csv" "N"'),
        block(
            "/NODE=.\\\n/UN=\n/PW=\n/OLEDB=SQLite\n/ENGINE=SQLite\n/TABLE=<<<SOURCE>>>\n/CSV=candidates.csv",
            "SELECT lot AS LOT FROM measurements WHERE value > 0",
        ),
        block('/UTILITIES={ROWS-IN-FILE} "candidates.csv" "COMPILER_SIGNAL" "N"'),
        block('/UTILITIES={IF-THEN} "COMPILER_SIGNAL" "GT" "0"'),
        block('/UTILITIES={AED} "candidates.csv"'),
        block("/UTILITIES={END-IF}"),
        block("/UTILITIES={END-MACRO}"),
    )
    seen = []
    monkeypatch.setattr(
        aed_api, "process_candidates", lambda path, logger=None: seen.append(path)
    )
    monkeypatch.setenv("COMPILER_SIGNAL", "0")
    namespace = {"__name__": "generated_job"}
    exec(compile(result.emitted.source, "generated_job.py", "exec"), namespace)
    assert PortableScriptHostRuntime().run_python(namespace["run"], tmp_path)
    assert seen == ["candidates.csv"]
    assert (tmp_path / "candidates.csv").read_text(encoding="utf-8-sig").split() == [
        "LOT",
        "A",
    ]


def test_generated_imports_work_when_compiler_is_blocked(tmp_path):
    output = translate(ROOT / FULL_JOBS[0], tmp_path)
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
    completed = subprocess.run(
        [sys.executable, "-c", code], capture_output=True, text=True
    )
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
        *(str(ROOT / p) for p in FULL_JOBS),
        "--out-dir",
        str(tmp_path),
    ]
    completed = subprocess.run(command, capture_output=True, text=True)
    assert completed.returncode == 0, completed.stderr
    for target in FULL_JOBS:
        ast.parse(
            (tmp_path / Path(target).with_suffix(".py").name).read_text(
                encoding="utf-8"
            )
        )
    bad = tmp_path / "bad.txt"
    bad.write_text(block('/UTILITIES={IF-THEN} "A" "EQ" "1"'))
    completed = subprocess.run(
        [sys.executable, "-m", "vg2c", str(bad)], capture_output=True, text=True
    )
    assert completed.returncode == 1 and "missing-end" in completed.stderr
    assert not bad.with_suffix(".py").exists()
