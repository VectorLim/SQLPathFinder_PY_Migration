from __future__ import annotations

import ast
import inspect
from pathlib import Path

import pytest

from vg2c.compilation import compile_document
from vg2c.embedding import assemble_utilities
from vg2c.utilities import ensure_utility_checks_loaded
from vg2c.emitter.models import EmittableOperation
from vg2c.utilities._base import UtilitySpec


def methods(source, name):
    cls = next(
        n
        for n in ast.parse(source).body
        if isinstance(n, ast.ClassDef) and n.name == name
    )
    return {
        n.name
        for n in cls.body
        if isinstance(n, (ast.FunctionDef, ast.AsyncFunctionDef))
    }


def test_sqlite_workflow_embeds_runtime_reader_and_excludes_compiler():
    source = compile_document(Path(__file__).parents[1] / "fixtures/script_short.txt").emitted.source
    assert "SqliteReader(" in source
    assert "execute_sql(" in source
    assert not any(isinstance(node, ast.ClassDef) for node in ast.parse(source).body)
    assert "def step_" not in source
    assert "ctx" not in source


def test_full_runtime_filesystem_api_is_embedded():
    embedded = assemble_utilities(
        step_emissions=(),
        workflow_source="def run(ctx):\n    ctx.fs_ops.write_file('out', 'hello')",
        reader_names=set(),
    )
    source = "\n".join(embedded.sources)
    assert methods(source, "FileSystemOps") == {
        "copy",
        "rename",
        "delete",
        "write_file",
    }
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
    classes = [node.name for node in ast.parse("\n".join(embedded.sources)).body
               if isinstance(node, ast.ClassDef)]
    assert classes.count("CsvIO") == 1
    assert classes.count("_CsvIO") == 1
    assert embedded.context_expression.count("CsvIO()") == 1


def test_explicit_reader_root_uses_the_same_resolver():
    embedded = assemble_utilities(
        step_emissions=(),
        workflow_source="def run(ctx): pass",
        reader_names={"sqlite_reader"},
    )
    assert "class SqliteReader" in "\n".join(embedded.sources)


ensure_utility_checks_loaded()


@pytest.mark.parametrize(
    "utility",
    [
        utility
        for utility in UtilitySpec.registered()
        if utility.__module__.startswith("vg2c.")
        and any(
            isinstance(value, EmittableOperation) for value in vars(utility).values()
        )
    ],
    ids=lambda utility: utility.utility_name,
)
def test_included_utility_keeps_every_emittable_method(utility):
    operations = {
        name
        for name in vars(utility)
        if isinstance(inspect.getattr_static(utility, name), EmittableOperation)
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
            assert node.name not in {"check", "emit_block", "extract_globals"}
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
    ("fixture", "context_keys", "reader", "sql_globals"),
    [
        ("script_short.txt", "crosstab csv_io macro", True, False),
        ("script_another.txt", "crosstab csv_io external macro", False, True),
        ("html_test.txt", "csv_io fs_ops html_report macro", False, False),
        (
            "maxlidheight.txt",
            "crosstab csv_io email external fs_ops html_report macro smart_append",
            True,
            True,
        ),
    ],
)
def test_api_retention_preserves_workflow_utility_selection(fixture, context_keys, reader, sql_globals):
    if fixture == "html_test.txt":
        with pytest.raises(ValueError, match="JMP/JSL"):
            compile_document(Path(__file__).parents[1] / "fixtures" / fixture)
        return
    emitted = compile_document(Path(__file__).parents[1] / "fixtures" / fixture).emitted
    tree = ast.parse(emitted.source)
    assert not any(isinstance(node, ast.ClassDef) for node in tree.body)
    imports = {name.name for node in tree.body if isinstance(node, ast.ImportFrom) and node.module == "vg2c.runtime" for name in node.names}
    assert ("SqliteReader" in imports) == reader
    assert "ctx" not in emitted.source
    if "html_report" in context_keys:
        assert "render_html" in imports
    if "email" in context_keys:
        assert "send_mail" in imports
