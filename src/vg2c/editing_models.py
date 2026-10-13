"""Data contracts for semantic edit intent, validation and previews."""

from __future__ import annotations

from dataclasses import dataclass, field
from typing import Any, Literal

@dataclass(frozen=True, slots=True)
class SemanticChange:
    binding_id: str
    value: Any = None
    symbol_id: str | None = None
    reset: bool = False

@dataclass(frozen=True, slots=True)
class ValidationIssue:
    code: str
    message: str
    binding_id: str | None = None
    level: Literal["warning", "error"] = "error"



@dataclass(frozen=True, slots=True)
class ChangeProjection:
    source: str
    values: tuple[SemanticChange, ...]
    issues: tuple[ValidationIssue, ...]
    effective_values: dict[str, Any] = field(default_factory=dict)

    @property
    def valid(self) -> bool:
        return not any(issue.level == "error" for issue in self.issues)


@dataclass(frozen=True, slots=True)
class ChangePreview:
    projection: ChangeProjection
    diff: str

    @property
    def valid(self) -> bool:
        return self.projection.valid


class ChangeValidationError(ValueError):
    def __init__(self, issues: tuple[ValidationIssue, ...]) -> None:
        super().__init__("change validation failed")
        self.issues = issues
