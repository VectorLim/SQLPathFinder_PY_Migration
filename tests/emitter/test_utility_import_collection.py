"""The direct emitter owns runtime imports; legacy source embedding was removed."""

from pathlib import Path
import ast

from vg2c import compile_document
from vg2c.utilities import ensure_utility_checks_loaded
from vg2c.utilities._base import UtilitySpec


def test_generated_runtime_imports_are_deduplicated(tmp_path):
    source = tmp_path / "two_queries.txt"
    source.write_text(
        "<OPTIONS>\n/ENGINE=SQLite\n/CSV=one.csv\n</OPTIONS>\nSELECT 1 AS x\n<---- New Query ---->\n"
        "<OPTIONS>\n/ENGINE=SQLite\n/CSV=two.csv\n</OPTIONS>\nSELECT 2 AS y\n"
    )
    tree = ast.parse(compile_document(source).emitted.source)
    imported = [
        alias.name
        for node in tree.body if isinstance(node, ast.ImportFrom) and node.module == "vg2c.runtime"
        for alias in node.names
    ]
    assert imported.count("JobRuntime") == 1
    assert "job_entrypoint" not in imported
    assert imported.count("SqliteReader") == 1


def test_compiler_metadata_registry_retains_direct_sql_contract():
    ensure_utility_checks_loaded()
    definition = UtilitySpec.operation_definition("ctx", "run_query")
    assert definition is not None
    assert definition.parameter("inputs").artifact_role.direction == "input"
    assert definition.parameter("output").artifact_role.direction == "output"
    assert definition.parameter("reader").visibility == "internal"
