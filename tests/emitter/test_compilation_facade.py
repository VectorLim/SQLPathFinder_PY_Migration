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
    assert result.resolved_blocks
    assert result.scope_tree.kind == "program"
    assert result.function_to_block
    assert all(name.startswith("step_") for name in result.function_to_block)


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


def test_emitted_script_seeds_node_default_from_literal_site(tmp_path):
    source = tmp_path / "script.txt"
    source.write_text((FIXTURES / "reflow.txt").read_text(encoding="utf-8"))

    output = translate(source)
    generated = output.read_text(encoding="utf-8")

    ctx_line = generated.index("ctx = PipelineContext()")
    node_line = generated.index('ctx.macro.set_named("NODE", \'PG\')')
    assert node_line > ctx_line


def test_emitted_script_omits_node_default_when_no_literal_site(tmp_path):
    source = tmp_path / "script.txt"
    source.write_text((FIXTURES / "script_short.txt").read_text(encoding="utf-8"))

    output = translate(source)
    generated = output.read_text(encoding="utf-8")

    assert 'ctx.macro.set_named("NODE"' not in generated
