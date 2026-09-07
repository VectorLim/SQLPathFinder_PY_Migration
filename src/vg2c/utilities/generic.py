from __future__ import annotations

from vg2c.kind import Kind
from vg2c.utilities._base import EmitterUtility


class UnknownUtility(EmitterUtility):
    """Emit handler for unrecognised /UTILITIES commands."""

    utility_name = "utility"
    handles = (Kind.UNKNOWN,)

    @staticmethod
    def check(options) -> tuple[Kind, str] | None:
        return None

    @classmethod
    def emit_block(cls, block) -> list[str] | None:
        return ["pass  # TODO: utility command not classified"]
