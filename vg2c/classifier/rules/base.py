from __future__ import annotations

from dataclasses import dataclass
from typing import Protocol

from vg2c.classifier.model import Kind, Role, Spec
from vg2c.model import ParsedBlock


@dataclass(frozen=True)
class Match:
    """A successful rule match result."""

    kind: Kind
    role: Role
    spec: Spec
    reason: str


class Rule(Protocol):
    """Protocol for classification rules."""

    name: str

    def match(self, b: ParsedBlock) -> Match | None:
        """Attempt to match this rule against a block."""
        ...
