from __future__ import annotations

from abc import ABC
from typing import Any, ClassVar

from vg2c.frontend.models import Kind

__all__ = ["UtilitySpec"]


class UtilitySpec(ABC):
    """Base contract for all embeddable utilities."""

    utility_name: ClassVar[str]
    utility_imports: ClassVar[tuple[str, ...]] = ()
    utility_dependencies: ClassVar[tuple[str, ...]] = ()
    handles: ClassVar[tuple[Kind, ...]] = ()

    @classmethod
    def emit_block(
        cls, ctx: Any, block: Any, dispatched: Any
    ) -> tuple[str, str] | None:
        return None
