from __future__ import annotations

from abc import ABC
from typing import ClassVar

__all__ = ["UtilitySpec"]


class UtilitySpec(ABC):
    """Base contract for all embeddable utilities."""

    utility_name: ClassVar[str]
    utility_imports: ClassVar[tuple[str, ...]] = ()
    utility_dependencies: ClassVar[tuple[str, ...]] = ()
    utility_command_contains: ClassVar[tuple[tuple[str, tuple[str, ...]], ...]] = ()
    utility_command_suffixes: ClassVar[tuple[tuple[str, tuple[str, ...]], ...]] = ()
