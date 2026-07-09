"""UnknownUtility - emit handler for unrecognised /UTILITIES commands."""

from __future__ import annotations

from vg2c.emitter.utilities._base import CheckedUtilitySpec
from vg2c.emitter.utilities._emit_helpers import _emit_step_source, _step_name
from vg2c.kind import Kind


class UnknownUtility(CheckedUtilitySpec):
    """Emit handler for unrecognised /UTILITIES commands."""

    utility_name = "unknown"
    handles = (Kind.UTILITY,)

    @staticmethod
    def check(options) -> tuple[Kind, str] | None:
        return None

    @staticmethod
    def emit_block(block) -> tuple[str, str] | None:
        return _emit_step_source(
            _step_name(block, "utility"),
            ["pass  # TODO: utility command not classified"],
        )
