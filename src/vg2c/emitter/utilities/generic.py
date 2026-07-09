"""Generic utility fallback for unsupported /UTILITIES commands."""

from __future__ import annotations

from vg2c.emitter.utilities._base import CheckedUtilitySpec
from vg2c.emitter.utilities._emit_helpers import _emit_step_source, _step_name
from vg2c.kind import Kind


class GenericUtility(CheckedUtilitySpec):
    """Fallback owner for utility blocks without a more specific handler."""

    utility_name = "utility"
    handles = (Kind.UTILITY,)
    check_order = 60

    @staticmethod
    def check(options) -> tuple[Kind, str] | None:
        if "UTILITIES" in options.lookup:
            return Kind.UTILITY, "/UTILITIES present"
        return None

    @staticmethod
    def emit_block(block) -> tuple[str, str] | None:
        return _emit_step_source(
            _step_name(block, "utility"),
            ["pass  # TODO: utility command not classified"],
        )
