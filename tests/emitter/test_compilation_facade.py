from pathlib import Path

from vg2c import compile_document, translate
from vg2c.emitter import (
    DEPENDENCIES_END,
    STEPS_END,
    STEPS_START,
    WORKFLOW_END,
    WORKFLOW_START,
)

FIXTURES = Path(__file__).parents[1] / "fixtures"


def test_compile_document_exposes_metadata_without_writing(tmp_path):
    source = tmp_path / "script.txt"
    source.write_text((FIXTURES / "script_short.txt").read_text(encoding="utf-8"))

    result = compile_document(source)

    assert not source.with_suffix(".py").exists()
    assert result.resolved is result.dispatched.resolved
    assert result.resolved.blocks
    assert result.resolved.scope_tree.kind == "program"
    assert result.emitted.steps
    assert all(step.function_name.startswith("step_") for step in result.emitted.steps)
    resolved_indices = {block.index for block in result.resolved.blocks}
    assert all(step.block_index in resolved_indices for step in result.emitted.steps)


def test_emitter_regions_are_ordered_and_translate_stays_compatible(tmp_path):
    source = tmp_path / "script.txt"
    source.write_text((FIXTURES / "script_short.txt").read_text())
    output = translate(source)
    generated = output.read_text()
    assert output == tmp_path / "script/main.py"
    assert "def run(workdir=WORK_DIR):" in generated
    assert "def step_" not in generated
    assert "PipelineContext" not in generated
    assert list(output.parent.glob("sql/*.sql"))


def test_generated_script_settings_follow_imports_and_precede_dependencies(tmp_path):
    source = tmp_path / "script.txt"
    source.write_text((FIXTURES / "script_short.txt").read_text())
    generated = translate(source).read_text()
    assert generated.index("from vg2c.runtime import") < generated.index("BASE_DIR =") < generated.index("def run(")
    assert "VG2C_SQL_GET_CSV_LIST_CHUNK_SIZE" not in generated


def test_emitted_script_seeds_node_default_from_literal_site(tmp_path):
    source = tmp_path / "script.txt"
    source.write_text((FIXTURES / "reflow.txt").read_text())
    generated = translate(source).read_text()
    assert "node='PG.[A12_PROD_0.].MARS'" in generated
    assert "VG2C_DEFAULT_NODE" not in generated
    assert "ctx" not in generated


def test_emitted_script_omits_node_default_when_no_literal_site(tmp_path):
    source = tmp_path / "script.txt"
    source.write_text((FIXTURES / "script_short.txt").read_text(encoding="utf-8"))

    output = translate(source)
    generated = output.read_text(encoding="utf-8")

    assert 'ctx.macro.set_named("NODE"' not in generated


def test_every_emitted_if_logs_its_source_prompt_and_result(tmp_path):
    import ast
    source = tmp_path / "script.txt"
    source.write_text((FIXTURES / "actual_script.txt").read_text())
    generated = translate(source).read_text()
    run = next(node for node in ast.parse(generated).body if isinstance(node, ast.FunctionDef) and node.name == "run")
    assert sum(isinstance(node, ast.If) for node in ast.walk(run)) == 11
    assert "ctx" not in generated
    assert "Logger.condition" not in generated
