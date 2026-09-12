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
    assert result.resolved.blocks
    assert result.resolved.scope_tree.kind == "program"
    assert result.emitted.steps
    assert all(step.function_name.startswith("step_") for step in result.emitted.steps)
    resolved_indices = {block.index for block in result.resolved.blocks}
    assert all(step.block_index in resolved_indices for step in result.emitted.steps)


def test_emitter_regions_are_ordered_and_translate_stays_compatible(tmp_path):
    source = tmp_path / "script.txt"
    source.write_text((FIXTURES / "script_short.txt").read_text(encoding="utf-8"))

    output = translate(source)
    generated = output.read_text(encoding="utf-8")

    offsets = [
        generated.index(marker)
        for marker in (
            DEPENDENCIES_END,
            STEPS_START,
            STEPS_END,
            WORKFLOW_START,
            WORKFLOW_END,
        )
    ]
    assert offsets == sorted(offsets)
    assert output == source.with_suffix(".py")


def test_generated_script_settings_follow_imports_and_precede_dependencies(tmp_path):
    source = tmp_path / "script.txt"
    source.write_text((FIXTURES / "script_short.txt").read_text(encoding="utf-8"))

    generated = translate(source).read_text(encoding="utf-8")

    settings = generated.index("# VG2C generated-script settings")
    chunk_size = generated.index("VG2C_SQL_GET_CSV_LIST_CHUNK_SIZE = 1000")
    dependencies = generated.index(DEPENDENCIES_END)
    assert settings < chunk_size < dependencies


def test_emitted_script_seeds_node_default_from_literal_site(tmp_path):
    source = tmp_path / "script.txt"
    source.write_text((FIXTURES / "reflow.txt").read_text(encoding="utf-8"))

    output = translate(source)
    generated = output.read_text(encoding="utf-8")

    ctx_line = generated.index("ctx = PipelineContext({")
    node_line = generated.index('ctx.macro.set_named("NODE", \'PG\')')
    assert node_line > ctx_line


def test_emitted_script_omits_node_default_when_no_literal_site(tmp_path):
    source = tmp_path / "script.txt"
    source.write_text((FIXTURES / "script_short.txt").read_text(encoding="utf-8"))

    output = translate(source)
    generated = output.read_text(encoding="utf-8")

    assert 'ctx.macro.set_named("NODE"' not in generated


def test_every_emitted_if_logs_its_source_prompt_and_result(tmp_path):
    source = tmp_path / "script.txt"
    source.write_text((FIXTURES / "actual_script.txt").read_text(encoding="utf-8"))

    generated = translate(source).read_text(encoding="utf-8")
    workflow = generated.split(WORKFLOW_START, 1)[1].split(WORKFLOW_END, 1)[0]

    assert "Logger.basicConfig(level=Logger.INFO)" in workflow
    assert "def condition(cls," in generated
    assert "/PROMPT-TEXT=Step 1-11. TRUE if config file not found" in workflow
    assert "_if_result" not in workflow
    assert 'Logger.getLogger("vg2c.workflow").info(' not in workflow
    assert workflow.count("if Logger.condition(") == 7
