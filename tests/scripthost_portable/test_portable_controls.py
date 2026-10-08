"""Generic script/controls API against the original ScriptHost execution of the same program."""

from pathlib import Path

import pytest

from scripthost_portable.runtime import PortableScriptHostRuntime, _spf_manager_type
from scripthost_portable.script_api import controls, script

DELIMITER = "<---- New Query ---->"


def _legacy():
    _spf_manager_type()
    import SPFLib.SPFSQL3 as legacy

    return legacy


def block(options, command=""):
    return f"\n<OPTIONS>\n{options}\n</OPTIONS>\n{command}\n"


def util(value, options=""):
    return block(f"/UTILITIES={value}" + (f"\n{options}" if options else ""))


def write(target, text):
    return block(f"/WRITE-FILE=Y\n/CSV={target}", text)


def vg2(*blocks):
    return DELIMITER.join(["\n", *blocks])


def put(target, text):
    script.invoke(options={"WRITE-FILE": "Y", "CSV": target}, command=text)


@pytest.fixture
def trace(monkeypatch):
    """WRITE-FILE records (target, text) instead of writing; FAIL/SKIP texts raise; 'READ name' records a file."""
    legacy = _legacy()
    events = []

    def record(task):
        text = task.SPFTaskCommand.strip()
        if text.startswith("FAIL"):
            raise RuntimeError(text)
        if text.startswith("SKIP"):
            raise legacy.SPFNothingToProcessException(text)
        if text.startswith("READ "):
            text = Path(text[5:]).read_text()
        events.append((Path(task.OutExcel).name, text))

    monkeypatch.setattr(legacy.WriteFileTask, "executeTaskCommand", record)
    monkeypatch.setenv("FLAG", "1")
    return events


def _outcome(trace, action, workdir):
    trace.clear()
    try:
        action()
        error = None
    except Exception as failure:
        error = (type(failure).__name__, str(failure).replace(str(workdir), "<workdir>"))
    return list(trace), error


def assert_parity(tmp_path, trace, text, run, files=None):
    runtime = PortableScriptHostRuntime()
    outcomes = []
    for name, action in (("original", runtime.run_text), ("python", runtime.run_python)):
        workdir = tmp_path / name
        workdir.mkdir()
        for file_name, content in (files or {}).items():
            (workdir / file_name).write_text(content, encoding="utf-8")
        program = text if name == "original" else run
        outcomes.append(_outcome(trace, lambda: action(program, workdir), workdir))
    assert outcomes[1] == outcomes[0]
    return outcomes[0]


@pytest.mark.parametrize("value", ["1", "2"])
def test_if_then(tmp_path, trace, value):
    text = vg2(
        write("a", "before"), util(f'{{IF-THEN}} "FLAG" "EQ" "{value}"'), write("b", "then"),
        util("{END-IF}"), write("c", "after"),
    )

    def run():
        put("a", "before")
        with controls.if_then("FLAG", "EQ", value) as condition:
            if condition.matched:
                put("b", "then")
        put("c", "after")

    events, error = assert_parity(tmp_path, trace, text, run)
    assert error is None and len(events) == (3 if value == "1" else 2)


@pytest.mark.parametrize(
    "arguments",
    [
        ("FLAG", "EQ", "1"),
        ("FLAG", "EQ", "2"),
        ("FLAG", "EQ", "1", "AND", "VAR(a)", "EQS", "B"),
        ("FLAG", "GT", "5", "OR", "VAR(a)", "EQS", "A"),
    ],
)
def test_if_else(tmp_path, trace, arguments):
    quoted = " ".join(f'"{argument}"' for argument in arguments)
    text = vg2(
        util(f"{{IF-THEN}} {quoted}"), write("then", "then"), util("{ELSE}"), write("else", "else"),
        util("{END-IF}"), write("after", "after"),
    )

    def run():
        with controls.if_else(*arguments) as condition:
            if condition.matched:
                put("then", "then")
            else:
                put("else", "else")
        put("after", "after")

    events, error = assert_parity(tmp_path, trace, text, run)
    assert error is None and len(events) == 2


@pytest.mark.parametrize(
    ("files", "continue_on_error"),
    [
        ({"macro.csv": "NAME,DIR\nalpha,out\nbeta,x\n"}, "N"),
        ({"macro.csv": "NAME,DIR\n"}, "N"),
        ({}, "Y"),
        ({}, "N"),
    ],
)
def test_macro(tmp_path, trace, files, continue_on_error):
    text = vg2(
        util(f'{{START-MACRO}} "macro.csv" "{continue_on_error}"'), write("<<<DIR>>>.txt", "hello <<<NAME>>>"),
        util("{END-MACRO}"), write("after", "after <<<NAME>>>"),
    )

    def run():
        with controls.macro("macro.csv", continue_on_error) as macro:
            if macro.active:
                put("<<<DIR>>>.txt", "hello <<<NAME>>>")
        put("after", "after <<<NAME>>>")

    assert_parity(tmp_path, trace, text, run, files)


def test_nested_macro_precedence_and_barrier(tmp_path, trace):
    files = {"outer.csv": "X,A,INNER\nouter,1,inner.csv\n", "inner.csv": "X,B\ninner,2\n"}
    text = vg2(
        util('{START-MACRO} "outer.csv" "N"', "/PROMPT-TEXT=outer"),
        write("o1", "<<<X>>> <<<A>>>"),
        util('{START-MACRO} "<<<INNER>>>" "N"', "/PROMPT-TEXT=inner"),
        write("i1", "<<<X>>> <<<A>>> <<<B>>>"),
        util("{END-MACRO}"),
        write("o2", "<<<X>>>"),
        util("{END-MACRO}"),
    )

    def run():
        with controls.macro("outer.csv", "N", options={"PROMPT-TEXT": "outer"}) as outer:
            if outer.active:
                put("o1", "<<<X>>> <<<A>>>")
                with controls.macro("<<<INNER>>>", "N", options={"PROMPT-TEXT": "inner"}) as inner:
                    if inner.active:
                        put("i1", "<<<X>>> <<<A>>> <<<B>>>")
                put("o2", "<<<X>>>")

    events, error = assert_parity(tmp_path, trace, text, run, files)
    assert error is None
    assert events == [("o1", "outer 1"), ("i1", "inner 1 2"), ("o2", "outer")]


@pytest.mark.parametrize("arguments", [("1", "5", "2", "1", "N"), ("1", "5", "2", "1", "Y"), ("0", "3", "1.5", "1", "N")])
def test_for_loop(tmp_path, trace, arguments):
    quoted = " ".join(f'"{argument}"' for argument in arguments)
    body = "<<<spf-loop-ctr-1>>> <<<spf-step-1>>> <<<spf-start-1>>>"
    text = vg2(util(f"{{FOR-LOOP}} {quoted}"), write("f<<<spf-loop-ctr-1-int>>>", body), util("{END-LOOP}"))

    def run():
        with controls.for_loop(*arguments) as loop:
            for iteration in loop:
                with iteration:
                    put("f<<<spf-loop-ctr-1-int>>>", body)

    events, error = assert_parity(tmp_path, trace, text, run)
    assert error is None and len(events) >= 2


@pytest.mark.parametrize("fail", [False, True])
def test_site_loop_and_its_break_on_error(tmp_path, trace, fail):
    body = [write("<<<spf-site-for-file-name>>>", "<<<spf-site>>>")] + ([write("x", "FAIL")] if fail else [])
    text = vg2(util('{SITE-LOOP} "A.X,B.Y"'), *body, util("{END-LOOP}"), write("after", "after"))

    def run():
        with controls.site_loop("A.X,B.Y") as loop:
            for iteration in loop:
                with iteration:
                    put("<<<spf-site-for-file-name>>>", "<<<spf-site>>>")
                    if fail:
                        put("x", "FAIL")
        put("after", "after")

    events, error = assert_parity(tmp_path, trace, text, run)
    assert error is None and len(events) == (2 if fail else 3)


@pytest.mark.parametrize(("error_trap", "fail"), [("N", False), ("Y", True), ("N", True)])
def test_run_loop(tmp_path, trace, error_trap, fail):
    files = {"in.csv": "ID\n1\n2\n3\n4\n5\n"}
    body = [write("read", "READ chunk.csv")] + ([write("x", "FAIL")] if fail else [])
    text = vg2(util(f'{{RUN-LOOP}} "in.csv" "chunk.csv" "2" "{error_trap}"'), *body, util("{END-LOOP}"))

    def run():
        with controls.run_loop("in.csv", "chunk.csv", "2", error_trap) as loop:
            for iteration in loop:
                with iteration:
                    put("read", "READ chunk.csv")
                    if fail:
                        put("x", "FAIL")

    events, error = assert_parity(tmp_path, trace, text, run, files)
    assert len(events) == (1 if fail and error_trap == "N" else 3)
    assert sorted(path.name for path in (tmp_path / "python").iterdir()) == ["chunk.csv", "in.csv"]


def test_macro_inside_loop_and_loop_inside_macro(tmp_path, trace):
    files = {"m1.csv": "V\none\n", "m3.csv": "V\nthree\n", "n.csv": "N,LABEL\n3,lbl\n"}
    text = vg2(
        util('{FOR-LOOP} "1" "3" "2" "1" "N"'),
        util('{START-MACRO} "m<<<spf-loop-ctr-1-int>>>.csv" "N"'),
        write("x", "<<<V>>> <<<spf-loop-ctr-1-int>>>"),
        util("{END-MACRO}"),
        util("{END-LOOP}"),
        util('{START-MACRO} "n.csv" "N"'),
        util('{FOR-LOOP} "1" "<<<N>>>" "1" "2" "N"'),
        write("y", "<<<LABEL>>> <<<spf-loop-ctr-2-int>>>"),
        util("{END-LOOP}"),
        util("{END-MACRO}"),
    )

    def run():
        with controls.for_loop("1", "3", "2", "1", "N") as loop:
            for iteration in loop:
                with iteration:
                    with controls.macro("m<<<spf-loop-ctr-1-int>>>.csv", "N") as macro:
                        if macro.active:
                            put("x", "<<<V>>> <<<spf-loop-ctr-1-int>>>")
        with controls.macro("n.csv", "N") as macro:
            if macro.active:
                with controls.for_loop("1", "<<<N>>>", "1", "2", "N") as inner:
                    for iteration in inner:
                        with iteration:
                            put("y", "<<<LABEL>>> <<<spf-loop-ctr-2-int>>>")

    events, error = assert_parity(tmp_path, trace, text, run, files)
    assert error is None
    assert events == [("x", "one 1"), ("x", "three 3"), ("y", "lbl 1"), ("y", "lbl 2"), ("y", "lbl 3")]


@pytest.mark.parametrize("where", ["root", "if", "macro"])
@pytest.mark.parametrize("text_value", ["SKIP", "FAIL"])
def test_child_error_policy(tmp_path, trace, where, text_value):
    files = {"macro.csv": "A\n1\n"}
    inner = [write("a", text_value), write("b", "next")]
    opener, closer = {
        "root": ([], []),
        "if": ([util('{IF-THEN} "FLAG" "EQ" "1"')], [util("{END-IF}")]),
        "macro": ([util('{START-MACRO} "macro.csv" "N"')], [util("{END-MACRO}")]),
    }[where]
    text = vg2(*opener, *inner, *closer, write("c", "after"))

    def body():
        put("a", text_value)
        put("b", "next")

    def run():
        if where == "root":
            body()
        elif where == "if":
            with controls.if_then("FLAG", "EQ", "1") as condition:
                if condition.matched:
                    body()
        else:
            with controls.macro("macro.csv", "N") as macro:
                if macro.active:
                    body()
        put("c", "after")

    events, error = assert_parity(tmp_path, trace, text, run, files)
    assert (error is None) == (text_value == "SKIP")


def test_missing_macro_value_fails_only_when_reached(tmp_path, trace):
    (tmp_path / "macro.csv").write_text("A\n1\n", encoding="utf-8")

    def run(flag):
        def program():
            with controls.macro("macro.csv", "N") as macro:
                if macro.active:
                    with controls.if_then("FLAG", "EQ", flag) as condition:
                        if condition.matched:
                            put("x", "<<<MISSING>>>")
                    put("y", "<<<A>>>")

        return program

    runtime = PortableScriptHostRuntime()
    runtime.run_python(run("2"), tmp_path)
    assert trace == [("y", "1")]
    with pytest.raises(Exception, match="MISSING"):
        runtime.run_python(run("1"), tmp_path)


def test_leaving_a_run_loop_early_runs_its_cleanup(tmp_path, trace):
    (tmp_path / "in.csv").write_text("ID\n1\n2\n3\n", encoding="utf-8")

    def run():
        with controls.run_loop("in.csv", "chunk.csv", "1", "N") as loop:
            for iteration in loop:
                with iteration:
                    put("read", "READ chunk.csv")
                    break
        put("after", "after")

    PortableScriptHostRuntime().run_python(run, tmp_path)
    assert trace == [("read", "ID\n1\n"), ("after", "after")]
    assert sorted(path.name for path in tmp_path.iterdir()) == ["chunk.csv", "in.csv"]
    assert _legacy().SPFTaskBase().gAnyLoopCtr == 0


def test_leaf_forms_reach_the_original_routes(tmp_path, monkeypatch):
    legacy = _legacy()
    reached = []

    def spy(task):
        reached.append((type(task).__name__, task.MyUtilitiesValue, task.SPFTaskCommand))

    for cls in (legacy.DOSCmdTask, legacy.SPFDeleteTask, legacy.AEDTask, legacy.RowsInFileTask, legacy.PyScriptTask):
        monkeypatch.setattr(cls, "executeTaskCommand", spy)

    def run():
        script.command('dir /b | find "x"  >  out.txt', options={"WORKDIR": ".\\"})
        script.utility(r"@EXEDIR@\SPFDelete.bat", "a.csv,b.csv", "N", external=True)
        script.utility("AED", "AED_CANDIDATES.csv")
        script.utility("ROWS-IN-FILE", "data file.csv", "ROWS", "N", options=[("WORKDIR", ".\\")])
        script.utility("PYSCRIPT:CHECK", command="print('x')")

    PortableScriptHostRuntime().run_python(run, tmp_path)
    assert reached == [
        ("DOSCmdTask", 'dir /b | find "x"  >  out.txt', ""),
        ("SPFDeleteTask", r'@EXEDIR@\SPFDelete.bat "a.csv,b.csv" "N"', ""),
        ("AEDTask", '{AED} "AED_CANDIDATES.csv"', ""),
        ("RowsInFileTask", '{ROWS-IN-FILE} "data file.csv" "ROWS" "N"', ""),
        ("PyScriptTask", "{PYSCRIPT:CHECK}", "print('x')"),
    ]


def test_query_options_route_through_the_original_router(tmp_path, monkeypatch):
    legacy = _legacy()
    reached = []
    monkeypatch.setattr(legacy.SPFTaskBase, "execute", lambda task: reached.append(type(task).__name__))

    def run():
        for node, engine, oledb in (("local", "SQLite", "SQLite"), ("KM.MARS", "VA", "SQLPlus"), ("x@uber@y", "VA", "SQLPlus")):
            script.invoke(options={"NODE": node, "UN": "", "OLEDB": oledb, "ENGINE": engine, "CSV": "r.csv"},
                          command="SELECT 1")

    PortableScriptHostRuntime().run_python(run, tmp_path)
    assert reached == ["nqSQLiteTask", "nqOracleTask", "nqUberTask"]


def test_api_requires_a_running_job():
    with pytest.raises(RuntimeError, match="ScriptHost runtime"):
        script.invoke(options={"WRITE-FILE": "Y"}, command="x")
    with pytest.raises(RuntimeError, match="ScriptHost runtime"):
        with controls.if_then("FLAG", "EQ", "1"):
            pass
