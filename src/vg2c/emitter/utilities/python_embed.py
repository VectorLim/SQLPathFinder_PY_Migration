"""PythonEmbed - directly embeds Python code blocks."""

from __future__ import annotations

from typing import Any

from vg2c.emitter.utilities._base import UtilitySpec
from vg2c.emitter.utilities._emit_helpers import (
    _emit_step_source,
    _step_name,
)
from vg2c.kind import Kind


class PythonEmbed(UtilitySpec):
    """Utility class for directly embedding Python script blocks."""

    utility_name = "py_embed"
    handles = (Kind.PYTHON_EMBED,)

    @staticmethod
    def emit_block(block: Any) -> tuple[str, str] | None:
        # Wrap the original python body directly in the step function definition
        return _emit_step_source(
            _step_name(block, "python_embed"),
            [block.resolved_body],
        )
