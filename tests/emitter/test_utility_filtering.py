from __future__ import annotations

import ast
import inspect
from pathlib import Path

import pytest

from vg2c.compilation import compile_document
from vg2c.emitter.models import emittable
from vg2c.utilities import assemble_utilities, ensure_utility_checks_loaded
from vg2c.utilities._base import UtilitySpec


def methods(source, name):
    cls = next(n for n in ast.parse(source).body if isinstance(n, ast.ClassDef) and n.name == name)
    return {n.name for n in cls.body if isinstance(n, (ast.FunctionDef, ast.AsyncFunctionDef))}


def test_sqlite_workflow_embeds_runtime_reader_and_excludes_compiler():
    source = compile_document(
        Path(__file__).parents[1] / "fixtures/script_short.txt"
    ).emitted.source
    assert "class SqliteReader" in source
    assert "class SqliteEngine" not in source
    assert "class UtilitySpec" not in source
    assert "class Kind" not in source
    assert "class HtmlReport" not in source
    assert methods(source, "PipelineContext") == {
        "__init__",
        "write_file",
        "run_query",
        "eval_condition",
        "_read_datasyncx",
    }
    assert "scan_sql_get_csv_list_calls" not in methods(source, "CsvIO")
    assert "globals().values" not in source


def test_full_runtime_filesystem_api_is_embedded():
    embedded = assemble_utilities(
        step_emissions=(),
        workflow_source="def run(ctx):\n    ctx.fs_ops.write_file('out', 'hello')",
        reader_names=set(),
    )
    source = "\n".join(embedded.sources)
    assert methods(source, "FileSystemOps") == {"copy", "rename", "delete", "write_file"}
    assert "class SqliteReader" not in source
    assert "'fs_ops': FileSystemOps()" in embedded.context_expression


def test_nested_expression_roots_are_discovered_without_invocation_metadata():
    embedded = assemble_utilities(
        step_emissions=(),
        workflow_source="def run(ctx):\n    ctx.write_file('out', ctx.macro.named('USER'))",
        reader_names=set(),
    )
    source = "\n".join(embedded.sources)
    assert "named" in methods(source, "MacroState")
    assert "write_file" in methods(source, "FileSystemOps")
    assert "emit_block" not in methods(source, "MacroState")


def test_repeated_roots_emit_one_class_and_one_context_instance():
    embedded = assemble_utilities(
        step_emissions=(),
        workflow_source=(
            "def run(ctx):\n    ctx.csv_io.row_count('a')\n    ctx.csv_io.row_count('b')"
        ),
        reader_names=set(),
    )
    assert "\n".join(embedded.sources).count("class CsvIO:") == 1
    assert embedded.context_expression.count("CsvIO()") == 1


def test_explicit_reader_root_uses_the_same_resolver():
    embedded = assemble_utilities(
        step_emissions=(), workflow_source="def run(ctx): pass", reader_names={"sqlite_reader"}
    )
    assert "class SqliteReader" in "\n".join(embedded.sources)


ensure_utility_checks_loaded()


@pytest.mark.parametrize(
    "utility",
    [
        utility
        for utility in UtilitySpec.registered()
        if utility.__module__.startswith("vg2c.")
        and any(isinstance(value, emittable) for value in vars(utility).values())
    ],
    ids=lambda utility: utility.utility_name,
)
def test_included_utility_keeps_every_emittable_method(utility):
    operations = {
        name
        for name in vars(utility)
        if isinstance(inspect.getattr_static(utility, name), emittable)
    }
    receiver = "ctx" if utility.utility_name == "ctx" else f"ctx.{utility.utility_name}"
    embedded = assemble_utilities(
        step_emissions=(),
        workflow_source=f"def run(ctx):\n    {receiver}.{sorted(operations)[0]}()",
        reader_names=set(),
    )
    source = "\n".join([*embedded.imports, *embedded.sources])
    assert operations <= methods(source, utility.__name__)
    for node in ast.walk(ast.parse(source)):
        if isinstance(node, (ast.FunctionDef, ast.AsyncFunctionDef)):
            assert node.name not in {"check", "emit_block"}
            assert not node.name.startswith("_emit_")
        if isinstance(node, ast.Name):
            assert node.id not in {
                "utility_name",
                "handles",
                "check_priority",
                "CodeExpr",
                "Kind",
                "emittable",
                "operation_spec",
                "_EMIT_DISPATCH",
                "_CALL_RE",
                "_CALL_SITE_WRAP_RE",
                "_MACRO_CONTROL_TOKEN_RE",
                "_TABLE_BINDING_RE",
            }
        if isinstance(node, ast.ImportFrom):
            assert not (node.module or "").startswith("vg2c")


@pytest.mark.parametrize(
    ("fixture", "context_keys", "reader"),
    [
        ("script_short.txt", "crosstab csv_io macro", True),
        ("script_another.txt", "crosstab csv_io external macro", False),
        ("html_test.txt", "csv_io fs_ops html_report macro", False),
        (
            "maxlidheight.txt",
            "crosstab csv_io email external fs_ops html_report macro smart_append",
            True,
        ),
    ],
)
def test_api_retention_preserves_workflow_utility_selection(fixture, context_keys, reader):
    emitted = compile_document(Path(__file__).parents[1] / "fixtures" / fixture).emitted
    tree = ast.parse(emitted.source)
    context = next(
        node
        for node in ast.walk(tree)
        if isinstance(node, ast.Call)
        and isinstance(node.func, ast.Name)
        and node.func.id == "PipelineContext"
    )
    assert {key.value for key in context.args[0].keys} == set(context_keys.split())
    expected_classes = {"Logger", "OracleClient", "PipelineContext"} | {
        UtilitySpec.for_name(name).__name__ for name in context_keys.split()
    }
    if reader:
        expected_classes.add("SqliteReader")
    assert {node.name for node in tree.body if isinstance(node, ast.ClassDef)} == expected_classes
