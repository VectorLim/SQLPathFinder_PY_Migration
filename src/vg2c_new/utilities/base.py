from __future__ import annotations

from abc import ABC, abstractmethod
from typing import TYPE_CHECKING

if TYPE_CHECKING:
    from vg2c_new.model import Command
    from vg2c_new.runtime import RuntimeState


class Utility(ABC):
    @abstractmethod
    def apply(self, command: Command, state: RuntimeState) -> None:
        ...
