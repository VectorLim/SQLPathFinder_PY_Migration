"""PythonEmbed - directly embeds Python code blocks."""

from __future__ import annotations

from typing import Any

from vg2c.emitter.utilities._base import CheckedUtilitySpec
from vg2c.emitter.utilities._emit_helpers import (
    _emit_step_source,
    _step_name,
)
from vg2c.kind import Kind


class PythonEmbed(CheckedUtilitySpec):
    """Utility class for directly embedding Python script blocks."""

    utility_name = "py_embed"
    handles = (Kind.PYTHON_EMBED,)
    check_order = 20

    @staticmethod
    def check(options) -> tuple[Kind, str] | None:
        if options.lookup.get("WRITE-FILE", "").upper() != "Y":
            return None

        csv_value = options.lookup.get("CSV", "")
        if csv_value.lower().endswith(".py"):
            return Kind.PYTHON_EMBED, "/WRITE-FILE=Y targeting .py script"
        return None

    @staticmethod
    def emit_block(block: Any) -> tuple[str, str] | None:
        # Wrap the original python body directly in the step function definition
        return _emit_step_source(
            _step_name(block, "python_embed"),
            [block.resolved_body],
        )
