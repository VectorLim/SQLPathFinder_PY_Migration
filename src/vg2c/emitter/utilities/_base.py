from __future__ import annotations

from abc import ABC
from dataclasses import dataclass
from typing import Any, Callable, ClassVar

from vg2c.frontend.models import Kind


@dataclass(frozen=True, slots=True)
class UtilityShape:
    name: str
    contains: tuple[str, ...] = ()
    suffixes: tuple[str, ...] = ()
    emit: Callable[[Any, list[str]], str | None] | None = None


__all__ = ["UtilityShape", "UtilitySpec"]


class UtilitySpec(ABC):
    """Base contract for all embeddable utilities."""

    utility_name: ClassVar[str]
    utility_imports: ClassVar[tuple[str, ...]] = ()
    utility_dependencies: ClassVar[tuple[str, ...]] = ()
    utility_shapes: ClassVar[tuple[UtilityShape, ...]] = ()
    handles: ClassVar[tuple[Kind, ...]] = ()

    @classmethod
    def emit_block(
        cls, ctx: Any, block: Any, dispatched: Any
    ) -> tuple[str, str] | None:
        return None
