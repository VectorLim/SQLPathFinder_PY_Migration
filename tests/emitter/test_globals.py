import ast
import sqlite3
from dataclasses import replace
from types import SimpleNamespace

import pytest

from vg2c import compile_document
from vg2c.editing import ParameterChange, project_changes
from vg2c.emitter.globals import resolve_globals
from vg2c.utilities._sql_globals import extract_sql_globals
from vg2c.utilities.sqlite_engine import SqliteEngine


def _sql_block(sql):
    return f"<OPTIONS>\n/OLEDB=SQLite\n/CSV=out.csv\n</OPTIONS>\n{sql}\n<---- New Query ---->\n"


def _mail_block(to="person@example.com", subject="Report"):
    return (
        f'<OPTIONS>\n/UTILITIES="SQLPathFinder_Email.va" "{to}" "{subject}" "Body"\n'
        "</OPTIONS>\n<---- New Query ---->\n"
    )


def _compile(tmp_path, text):
    path = tmp_path / "globals.txt"
    path.write_text(text, encoding="utf-8")
    result = compile_document(path)
    assert not [item for item in result.diagnostics if item.level == "error"]
    return result


def _render_sql(sql, **changes):
    block = SimpleNamespace(resolved_body=sql)
    candidates = SqliteEngine.extract_globals(block)
    refs = resolve_globals(candidates, {}, 0)
    expression = SqliteEngine._extract_sql_text(block, refs)
    namespace = {reference.source: reference.value for reference in refs.values()}
    namespace.update(changes)
    return eval(expression.source, {"SqliteEngine": SqliteEngine}, namespace), candidates


def test_collection_reuses_conflicts_and_normalizes_without_aliasing_types():
    collected = {}
    first = resolve_globals({"lot": "1", "a-b": [1], "9 units": 9}, collected, 0)
    second = resolve_globals({"lot": "2", "a b": [1]}, collected, 4)
    reused = resolve_globals({"lot": "2"}, collected, 8)
    typed = resolve_globals({"a-b": [True]}, collected, 9)
    assert first["lot"].source == "LOT"
    assert first["9 units"].source == "VALUE_9_UNITS"
    assert second["lot"].source == reused["lot"].source == "STEP_0004_LOT"
    assert second["a b"].source == "STEP_0004_A_B"
    assert typed["a-b"].source == "STEP_0009_A_B"
    reserved = resolve_globals({"lot": "1"}, {}, 3, {"LOT", "STEP_0003_LOT"})
    assert reserved["lot"].source == "STEP_0003_LOT_2"
    assert resolve_globals({"lot": "2"}, {}, 0)["lot"].source == "LOT"


@pytest.mark.parametrize(
    "predicate, expected",
    [
        ("t.[lot] = 'O''Brien'", {"LOT": "O'Brien"}),
        ("lot IN ('a', 'b')", {"LOT": ["a", "b"]}),
        ("lot NOT IN (1, 2, 3)", {"LOT": [1, 2, 3]}),
        ("x != 3 AND y <> 4", {"X": 3, "Y": 4}),
        ("x >= -2 AND x < 10", {"X_MIN": -2, "X_MAX": 10}),
        ("x BETWEEN -2 AND 10", {"X_MIN": -2, "X_MAX": 10}),
        ("x NOT BETWEEN 1 AND 2", {"X_MIN": 1, "X_MAX": 2}),
        ("x = 0.123456789012345678901", {"X": "0.123456789012345678901"}),
        ("x IN (1, 1.25, 2e-3)", {"X": [1, "1.25", "2e-3"]}),
        ("x = .5 OR y = -.25", {"X": ".5", "Y": "-.25"}),
        ("(lot = '1' OR lot = '2') AND lot = '1'", {"LOT": "1", "LOT_2": "2"}),
    ],
)
def test_sql_literal_selection(predicate, expected):
    rendered, candidates = _render_sql("SELECT * FROM t WHERE " + predicate)
    assert candidates == expected
    assert rendered.startswith("SELECT * FROM t WHERE ")


@pytest.mark.parametrize(
    "sql",
    [
        "SELECT lot = '1' FROM t",
        "SELECT * FROM t WHERE lower(lot) = '1'",
        "SELECT * FROM t WHERE lot = '1' || '2'",
        "SELECT * FROM t WHERE x = 1+2",
        "SELECT * FROM t WHERE lot IN (SELECT lot FROM u)",
        "SELECT * FROM t WHERE lot = '<<<LOT>>>'",
        "SELECT * FROM t WHERE lot In SQL_Get_CSV_List('data.csv', 'lot', '(')",
        "SELECT * FROM t WHERE lot IN ('a', /* keep */ 'b')",
        "SELECT * FROM t WHERE CASE WHEN x = 1 AND y = 2 THEN 1 ELSE 0 END = 1",
        "SELECT * FROM t WHERE lot = 'unterminated",
    ],
)
def test_sql_leaves_unsupported_operands_alone(sql):
    assert extract_sql_globals(sql) == []


def test_sql_preserves_behavior_and_changes_all_references():
    sql = (
        "SELECT a.lot FROM t a JOIN t b ON a.lot = b.lot AND b.n >= 2 "
        "WHERE a.lot IN ('O''Brien', 'x') AND a.n BETWEEN 1 AND 3 "
        "AND (a.lot = 'O''Brien' OR a.lot = 'x') -- lot = 'comment'\n"
    )
    rendered, _ = _render_sql(sql)
    with sqlite3.connect(":memory:") as db:
        db.execute("CREATE TABLE t (lot TEXT, n INTEGER)")
        db.executemany("INSERT INTO t VALUES (?, ?)", [("O'Brien", 2), ("x", 3), ("z", 4)])
        assert db.execute(sql).fetchall() == db.execute(rendered).fetchall()
    assert "-- lot = 'comment'" in rendered
    changed, _ = _render_sql("SELECT * FROM t WHERE lot = '1' OR lot = '1'", LOT="O'Brien")
    assert changed.count("'O''Brien'") == 2
    exact, _ = _render_sql("SELECT * FROM t WHERE x = 0.123456789012345678901")
    assert "0.123456789012345678901" in exact


def test_generated_globals_metadata_and_shared_edits(tmp_path):
    text = (
        _sql_block("SELECT * FROM t WHERE lot = '1'")
        + _sql_block("SELECT * FROM t WHERE lot = '1'")
        + _mail_block()
        + _mail_block()
    )
    result = _compile(tmp_path, text)
    source = result.emitted.source
    tree = ast.parse(source)
    last_import = max(
        i for i, node in enumerate(tree.body) if isinstance(node, (ast.Import, ast.ImportFrom))
    )
    assert ast.unparse(tree.body[last_import + 1]) == "LOT = '1'"
    assert source.count("\nLOT = '1'\n") == 1
    assert len(result.emitted.steps) == 4
    all_parameters = [p for step in result.emitted.steps for p in step.parameters]
    for parameter in all_parameters:
        span = parameter.source_range
        assert source[span.start_offset : span.end_offset] == parameter.source
    lots = [p for p in all_parameters if p.name == "LOT"]
    assert len(lots) == 2 and lots[0].id == lots[1].id
    assert all(not p.editable for p in all_parameters if p.name == "sql")
    assert len([p for p in all_parameters if p.id == "global:EMAIL_TO" and p.name == "to"]) == 2
    assert all(p.definition is not None for p in all_parameters if p.name == "to")
    assert not any(
        "vg2c" in ast.unparse(node)
        for node in tree.body
        if isinstance(node, (ast.Import, ast.ImportFrom))
    )
    change = ParameterChange(lots[0].id, "2")
    projected = project_changes(result, [change, change])
    assert projected.valid and projected.source.count("\nLOT = '2'\n") == 1
    assert (
        project_changes(result, [change, ParameterChange(lots[1].id, "3")]).issues[0].code
        == "conflicting-global-change"
    )
    assert _compile(tmp_path, text).emitted.source == source


def test_generated_steps_execute_shared_sql_without_external_services(tmp_path):
    result = _compile(tmp_path, _sql_block("SELECT * FROM t WHERE lot = '1'") * 2)
    tree = ast.parse(result.emitted.source)
    # Execute only assignments and step functions, with a capturing context.
    names = {step.function_name for step in result.emitted.steps}
    selected = [
        node
        for node in tree.body
        if isinstance(node, ast.FunctionDef)
        and node.name in names
        or isinstance(node, ast.Assign)
        and any(isinstance(t, ast.Name) and t.id == "LOT" for t in node.targets)
    ]
    namespace = {"SqliteEngine": SqliteEngine, "SqliteReader": lambda **kw: None}
    exec(compile(ast.Module(body=selected, type_ignores=[]), "<steps>", "exec"), namespace)
    calls = []
    context = SimpleNamespace(run_query=lambda **kwargs: calls.append(kwargs["sql"]))
    namespace["LOT"] = "2"
    for step in result.emitted.steps:
        namespace[step.function_name](context)
    assert len(calls) == 2 and all("lot = '2'" in sql for sql in calls)


def test_email_long_form_and_dynamic_values(tmp_path):
    long_form = (
        '<OPTIONS>\n/UTILITIES="SQLPathFinder_Email.va" '
        '"file.csv" "self" "Long report" "Body" "person@example.com"\n</OPTIONS>\n'
        "<---- New Query ---->\n"
    )
    result = _compile(tmp_path, long_form + _mail_block(subject="<<<SUBJECT>>>"))
    parameters = [p for step in result.emitted.steps for p in step.parameters]
    assert [p.value for p in parameters if p.id == "global:EMAIL_SUBJECT"] == ["Long report"]
    assert [p.value for p in parameters if p.id == "global:EMAIL_TO"] == ["person@example.com"] * 2
    assert any(p.name == "subject" and not p.editable for p in parameters)


def test_macro_calls_coexist_with_literal_globals():
    sql = "SELECT * FROM t WHERE lot In SQL_Get_CSV_List('data.csv', 'lot', '(') AND x = 2"
    block = SimpleNamespace(resolved_body=sql)
    refs = resolve_globals(SqliteEngine.extract_globals(block), {}, 0)
    expression = SqliteEngine._extract_sql_text(block, refs)
    assert "ctx.csv_io.sql_get_csv_list(" in expression.source
    assert "SqliteEngine.global_sql(X, True)" in expression.source
    assert expression.global_names == ("X",)


def test_legacy_macro_wrapper_and_clause_boundaries():
    sql = (
        "SELECT * FROM (SELECT * FROM t WHERE (lot In "
        "SQL_Get_CSV_List('data.csv', 'lot', 'lot In') AND operation = '2303')"
    )
    assert SqliteEngine.extract_globals(SimpleNamespace(resolved_body=sql)) == {"OPERATION": "2303"}
    sql = "SELECT * FROM t JOIN u ON u.kind = 'one' LEFT JOIN v ON v.kind = 'two' WHERE t.id = 3"
    assert {v.key: v.value for v in extract_sql_globals(sql)} == {
        "KIND": "one",
        "KIND_2": "two",
        "ID": 3,
    }


def test_generated_conflicts_reuse_the_disambiguated_global(tmp_path):
    result = _compile(
        tmp_path,
        "".join(_sql_block(f"SELECT * FROM t WHERE lot = '{value}'") for value in ("1", "2", "2")),
    )
    ids = [[p.id for p in s.parameters if p.id.startswith("global:")] for s in result.emitted.steps]
    assert ids == [["global:LOT"], ["global:STEP_0001_LOT"], ["global:STEP_0001_LOT"]]


def test_imported_or_embedded_names_are_reserved(tmp_path, monkeypatch):
    import vg2c.utilities

    assemble = vg2c.utilities.assemble_utilities

    def with_runtime_constant(**kwargs):
        embedded = assemble(**kwargs)
        return replace(embedded, sources=(*embedded.sources, "LOT = 'runtime constant'"))

    monkeypatch.setattr(vg2c.utilities, "assemble_utilities", with_runtime_constant)
    result = _compile(tmp_path, _sql_block("SELECT * FROM t WHERE lot = '1'"))
    assert "STEP_0000_LOT = '1'" in result.emitted.source
    parameter = next(p for p in result.emitted.steps[0].parameters if p.id.startswith("global:"))
    assert parameter.id == "global:STEP_0000_LOT"
    span = parameter.source_range
    assert result.emitted.source[span.start_offset : span.end_offset] == "'1'"
