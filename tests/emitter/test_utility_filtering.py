"""Direct-project compiler emits only runtime imports and native control flow."""

from pathlib import Path
import ast

import pytest

from vg2c.compilation import compile_document
from vg2c.utilities import ensure_utility_checks_loaded
from vg2c.utilities._base import UtilitySpec


def test_sqlite_workflow_uses_runtime_reader_not_embedded_classes():
    source = compile_document(
        Path(__file__).parents[1] / "fixtures/script_short.txt"
    ).emitted.source
    assert "job.sql(" in source and "reader=" not in source
    assert not any(isinstance(node, ast.ClassDef) for node in ast.parse(source).body)
    assert "def step_" not in source and "ctx" not in source


def test_operation_definitions_retain_rendering_inputs_and_artifact_markers():
    ensure_utility_checks_loaded()
    query = UtilitySpec.operation_definition("ctx", "run_query")
    html = UtilitySpec.operation_definition("html_report", "layout")
    macro = UtilitySpec.operation_definition("macro", "set_named")
    assert (
        query is not None
        and query.parameter("output").artifact_role is True
    )
    assert html is not None and html.parameter("template") is not None
    assert macro is not None and macro.parameter("name") is not None


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
def test_api_retention_preserves_workflow_utility_selection(
    fixture, context_keys, reader, sql_globals
):
    emitted = compile_document(Path(__file__).parents[1] / "fixtures" / fixture).emitted
    tree = ast.parse(emitted.source)
    assert not any(isinstance(node, ast.ClassDef) for node in tree.body)
    imports = {
        name.name
        for node in tree.body
        if isinstance(node, ast.ImportFrom) and node.module == "vg2c.runtime"
        for name in node.names
    }
    assert "SqliteReader" not in imports
    assert "ctx" not in emitted.source
    assert "job_entrypoint" not in imports
    assert "JobRuntime" in imports
    if "html_report" in context_keys:
        assert "job.html(" in emitted.source
    if "email" in context_keys:
        assert "job.send_mail(" in emitted.source
