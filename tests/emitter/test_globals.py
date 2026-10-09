import ast
import sqlite3
from types import SimpleNamespace

import pytest
from pathlib import Path

from vg2c import compile_document
from vg2c.editing import SemanticChange, project_changes
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
    return (
        eval(expression.source, {"SqliteEngine": SqliteEngine}, namespace),
        candidates,
    )


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
        db.executemany(
            "INSERT INTO t VALUES (?, ?)", [("O'Brien", 2), ("x", 3), ("z", 4)]
        )
        assert db.execute(sql).fetchall() == db.execute(rendered).fetchall()
    assert "-- lot = 'comment'" in rendered
    changed, _ = _render_sql(
        "SELECT * FROM t WHERE lot = '1' OR lot = '1'", LOT="O'Brien"
    )
    assert changed.count("'O''Brien'") == 2
    exact, _ = _render_sql("SELECT * FROM t WHERE x = 0.123456789012345678901")
    assert "0.123456789012345678901" in exact


def test_generated_literals_and_invocation_edits_are_independent(tmp_path):
    result = _compile(tmp_path, _sql_block("SELECT * FROM t WHERE lot = '1'") * 2 + _mail_block() * 2)
    assert len(result.emitted.assets) == 2
    assert len(result.emitted.steps) == 4
    parameters = [p for step in result.emitted.steps for p in step.parameters]
    assert not any(p.id.startswith("global:") for p in parameters)
    assert all(not p.editable and p.source_range is None for p in parameters if p.name == "sql")
    recipients = [p for p in parameters if p.name == "to"]
    assert len(recipients) == 2 and recipients[0].id != recipients[1].id
    projection = project_changes(result, [SemanticChange(recipients[0].id, "changed@example.test")])
    assert projection.valid
    assert projection.source.count("changed@example.test") == 1
    assert projection.source.count("person@example.com") == 1
    for parameter in parameters:
        if span := parameter.source_range:
            assert result.emitted.source[span.start_offset:span.end_offset] == parameter.source
    assert _compile(tmp_path, _sql_block("SELECT * FROM t WHERE lot = '1'") * 2 + _mail_block() * 2).emitted == result.emitted


def test_generated_queries_keep_independent_execution_boundaries(tmp_path):
    result = _compile(tmp_path, _sql_block("SELECT 1 AS value") * 2)
    project = tmp_path / "project"
    project.mkdir()
    for name, content in result.emitted.assets:
        path = project / name
        path.parent.mkdir(exist_ok=True)
        path.write_text(content)
    namespace = {"__file__": str(project / "main.py"), "__name__": "job"}
    exec(result.emitted.source, namespace)
    calls = []
    def capture(path, **kwargs):
        calls.append(Path(path).read_text())
    namespace["execute_sql"] = capture
    first = project / result.emitted.assets[0][0]
    first.write_text("SELECT 2 AS value")
    namespace["run"](workdir=tmp_path / "work")
    assert calls[0] == "SELECT 2 AS value"
    assert calls[1].strip() == "SELECT 1 AS value"


def test_email_long_form_and_dynamic_values(tmp_path):
    long_form = ('<OPTIONS>\n/UTILITIES="SQLPathFinder_Email.va" '
                 '"file.csv" "self" "Long report" "Body" "person@example.com"\n</OPTIONS>\n'
                 '<---- New Query ---->\n')
    result = _compile(tmp_path, long_form + _mail_block(subject="<<<SUBJECT>>>"))
    parameters = [p for step in result.emitted.steps for p in step.parameters]
    assert [p.value for p in parameters if p.name == "to"] == ["person@example.com"] * 2
    assert next(p for p in parameters if p.name == "subject").value == "Long report"
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
    assert SqliteEngine.extract_globals(SimpleNamespace(resolved_body=sql)) == {
        "OPERATION": "2303"
    }
    sql = "SELECT * FROM t JOIN u ON u.kind = 'one' LEFT JOIN v ON v.kind = 'two' WHERE t.id = 3"
    assert {v.key: v.value for v in extract_sql_globals(sql)} == {
        "KIND": "one",
        "KIND_2": "two",
        "ID": 3,
    }


def test_generated_sql_literals_remain_in_their_own_assets(tmp_path):
    result = _compile(tmp_path, "".join(_sql_block(f"SELECT * FROM t WHERE lot = '{value}'") for value in ("1", "2", "2")))
    assert [body.strip() for _, body in result.emitted.assets] == [f"SELECT * FROM t WHERE lot = '{value}'" for value in ("1", "2", "2")]
    assert len({name for name, _ in result.emitted.assets}) == 3


def test_production_emitter_does_not_assemble_embedded_runtime(tmp_path, monkeypatch):
    import vg2c.embedding
    def forbidden(**kwargs):
        pytest.fail("Native projects must not assemble an embedded runtime")
    monkeypatch.setattr(vg2c.embedding, "assemble_utilities", forbidden)
    result = _compile(tmp_path, _sql_block("SELECT * FROM t WHERE lot = '1'"))
    assert "from vg2c.runtime import" in result.emitted.source
    assert "LOT =" not in result.emitted.source
    assert "lot = '1'" in result.emitted.assets[0][1]
