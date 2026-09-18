from pathlib import Path

import pytest

from vg2c import compile_document
from vg2c.dataflow import analyze
from vg2c.workflow import project_workflow

FIXTURES = Path(__file__).parents[1] / "fixtures"


@pytest.mark.parametrize("fixture", ["script_short.txt", "TimeDelta.txt"])
def test_semantic_effect_projection_covers_legacy_analyzer_paths(fixture, tmp_path):
    source = tmp_path / fixture
    source.write_text((FIXTURES / fixture).read_text(encoding="utf-8"), encoding="utf-8")
    result = compile_document(source)

    legacy_paths = {artifact.path for artifact in analyze(result.resolved).artifacts}
    semantic_paths = {
        endpoint.path.lower().replace("\\", "/")
        for effect in project_workflow(result).effects
        for endpoint in (*effect.inputs, *effect.outputs)
        if endpoint.path
    }

    assert legacy_paths <= semantic_paths
