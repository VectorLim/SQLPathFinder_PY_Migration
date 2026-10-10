"""Generated macro projects use one readable store and native control flow."""

import ast

from vg2c import compile_document, translate


def block(options, body=""):
    return "<OPTIONS>\n" + options + "\n</OPTIONS>\n" + body + "\n<---- New Query ---->\n"


def run_job(main, workdir):
    namespace = {"__file__": str(main), "__name__": "generated_job"}
    exec(compile(main.read_text(), str(main), "exec"), namespace)
    namespace["run"](workdir)


def test_scopes_and_independent_runs_and_empty_csv(tmp_path):
    source = tmp_path / "macros.txt"
    source.write_text(
        block('/UTILITIES={START-MACRO} "outer.csv" "N"')
        + block('/WRITE-FILE=Y\n/CSV=outer.txt', "<<<LABEL>>>")
        + block('/UTILITIES={START-MACRO} "inner.csv" "N"')
        + block('/WRITE-FILE=Y\n/CSV=inner.txt', "<<<LABEL>>>")
        + block('/UTILITIES={END-MACRO}')
        + block('/WRITE-FILE=Y\n/CSV=restored.txt', "<<<LABEL>>>")
        + block('/UTILITIES={END-MACRO}')
    )
    emitted = compile_document(source).emitted
    code = emitted.source
    tree = ast.parse(code)
    assert "macro_values_" not in code
    assert "macro_values =" not in code
    assert "MacroStore(" in code
    assert "with macros.scope(" in code
    assert any(isinstance(node, ast.With) for node in ast.walk(tree))
    assert any(isinstance(node, ast.If) for node in ast.walk(tree))
    for step in emitted.steps:
        assert code[step.source_range.start_offset:step.source_range.end_offset] == step.source
        for invocation in step.invocations:
            assert code[invocation.source_range.start_offset:invocation.source_range.end_offset] == invocation.source

    main = translate(source)
    for label, folder in [("first", tmp_path / "one"), ("second", tmp_path / "two")]:
        folder.mkdir()
        (folder / "outer.csv").write_text("LABEL\n" + label + "\nignored\n")
        (folder / "inner.csv").write_text("LABEL\ninner\n")
        run_job(main, folder)
        assert (folder / "outer.txt").read_text().strip() == label
        assert (folder / "inner.txt").read_text().strip() == "inner"
        assert (folder / "restored.txt").read_text().strip() == label

    empty = tmp_path / "empty"
    empty.mkdir()
    (empty / "outer.csv").write_text("LABEL\n")
    run_job(main, empty)
    assert not (empty / "outer.txt").exists()
    assert not (empty / "inner.txt").exists()


def test_native_numeric_branch_and_editable_macro_assignment(tmp_path):
    source = tmp_path / "conditional.txt"
    source.write_text(
        block('/UTILITIES={ROWS-IN-FILE} "rows.csv" "COUNT" "N"')
        + block('/UTILITIES={IF-THEN} "COUNT" "GT" "0" "" "" "" ""')
        + block('/WRITE-FILE=Y\n/CSV=positive.txt', "<<<COUNT>>>")
        + block('/UTILITIES={ELSE}')
        + block('/WRITE-FILE=Y\n/CSV=empty.txt', "zero")
        + block('/UTILITIES={END-IF}')
    )
    emitted = compile_document(source).emitted
    code = emitted.source
    ast.parse(code)
    assert "macros['COUNT'] = str(row_count(" in code
    assert "int(macros['COUNT']) > int('0')" in code
    assert "macro_values_" not in code
    main = translate(source)
    folder = tmp_path / "work"
    folder.mkdir()
    (folder / "rows.csv").write_text("ID\n1\n")
    run_job(main, folder)
    assert (folder / "positive.txt").read_text().strip() == "1"
