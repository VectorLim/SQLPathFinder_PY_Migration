from __future__ import annotations

from typing import Any

from vg2c.emitter.utilities._base import CheckedUtilitySpec
from vg2c.kind import Kind


class PythonEmbed(CheckedUtilitySpec):
    """Utility class for directly embedding Python script blocks."""

    utility_name = "python_embed"
    handles = (Kind.PYTHON_EMBED,)

    @staticmethod
    def check(options) -> tuple[Kind, str] | None:
        if options.lookup.get("WRITE-FILE", "").upper() != "Y":
            return None

        csv_value = options.lookup.get("CSV", "")
        if csv_value.lower().endswith(".py"):
            return Kind.PYTHON_EMBED, "/WRITE-FILE=Y targeting .py script"
        return None

    @classmethod
    def emit_block(cls, block: Any) -> list[str] | None:
        # Wrap the original python body directly in the step function definition
        return [block.resolved_body]
