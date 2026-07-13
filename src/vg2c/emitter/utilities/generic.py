"""UnknownUtility - emit handler for unrecognised /UTILITIES commands."""

from __future__ import annotations

from vg2c.emitter.models import EmitContext
from vg2c.emitter.utilities._base import CheckedUtilitySpec
from vg2c.kind import Kind


class UnknownUtility(CheckedUtilitySpec):
    """Emit handler for unrecognised /UTILITIES commands."""

    utility_name = "utility"
    handles = (Kind.UTILITY,)

    @staticmethod
    def check(options) -> tuple[Kind, str] | None:
        return None

    @classmethod
    @EmitContext.step_emitter
    def emit_block(cls, block) -> list[str] | None:
        return ["pass  # TODO: utility command not classified"]
