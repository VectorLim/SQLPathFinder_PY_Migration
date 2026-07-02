from __future__ import annotations

from abc import ABC, abstractmethod
from typing import Any, ClassVar

__all__ = ["UtilitySpec"]


class UtilitySpec(ABC):
    """Base contract for all embeddable utilities."""

    utility_name: ClassVar[str]
    utility_imports: ClassVar[tuple[str, ...]] = ()
    utility_dependencies: ClassVar[tuple[str, ...]] = ()
    utility_command_contains: ClassVar[tuple[tuple[str, tuple[str, ...]], ...]] = ()
    utility_command_suffixes: ClassVar[tuple[tuple[str, tuple[str, ...]], ...]] = ()
    utility_embed_exclude_methods: ClassVar[tuple[str, ...]] = ("emit",)

    @classmethod
    @abstractmethod
    def emit(
        cls,
        ctx: Any,
        block: Any,
        dispatched: Any | None,
    ) -> tuple[str, str]:
        """Emit one block function and call-site line."""
        raise NotImplementedError
