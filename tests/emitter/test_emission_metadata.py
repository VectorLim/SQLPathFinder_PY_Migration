from pathlib import Path

from vg2c import compile_document

FIXTURES = Path(__file__).parents[1] / "fixtures"


def test_generated_source_contains_no_compiler_decorators():
    source = compile_document(FIXTURES / "script_short.txt").emitted.source

    assert "@emittable" not in source
    compile(source, "<generated-workflow>", "exec")
