from __future__ import annotations

from dataclasses import dataclass
from typing import Any

_UNSET = object()


@dataclass(frozen=True, slots=True)
class CodeExpr:
    """Explicit raw Python source with an optional value."""

    source: str
    value: Any = _UNSET

    @property
    def has_value(self) -> bool:
        return self.value is not _UNSET


@dataclass(frozen=True, slots=True)
class SourceRange:
    start_offset: int
    end_offset: int


@dataclass(frozen=True, slots=True)
class ParameterDefinition:
    name: str
    position: int | None
    artifact_role: bool = False


@dataclass(frozen=True, slots=True)
class UtilityOperationDefinition:
    utility_name: str
    method: str
    parameters: tuple[ParameterDefinition, ...]

    def parameter(self, name: str) -> ParameterDefinition | None:
        return next((item for item in self.parameters if item.name == name), None)


@dataclass(frozen=True, slots=True)
class RenderedArgument:
    name: str
    position: int | None
    source: str
    value: Any
    artifact_role: bool


class RenderedCall(str):
    """A generated call string carrying metadata known at render time."""

    definition: UtilityOperationDefinition
    arguments: tuple[RenderedArgument, ...]

    def __new__(
        cls,
        source: str,
        definition: UtilityOperationDefinition,
        arguments: tuple[RenderedArgument, ...],
    ) -> RenderedCall:
        value = str.__new__(cls, source)
        value.definition = definition
        value.arguments = arguments
        return value


@dataclass(frozen=True, slots=True)
class EmittedParameter:
    value: Any
    artifact_role: bool


@dataclass(frozen=True, slots=True)
class EmittedStep:
    block_index: int
    functional_kind: str
    source_range: SourceRange
    parameters: tuple[EmittedParameter, ...]


@dataclass(frozen=True, slots=True)
class EmittedScope:
    """Generated control structure, including its body and closing source block."""

    start_index: int
    end_index: int
    source_range: SourceRange


@dataclass(frozen=True, slots=True)
class EmittedScript:
    """Generated Python, assets, and source locations exposed by the compiler."""

    source: str
    steps: tuple[EmittedStep, ...] = ()
    assets: tuple[tuple[str, str], ...] = ()
    scopes: tuple[EmittedScope, ...] = ()

    def step_for_block(self, block_index: int) -> EmittedStep | None:
        return next(
            (step for step in self.steps if step.block_index == block_index), None
        )


@dataclass(frozen=True, slots=True)
class StepEmission:
    """Internal step source and companion-visible parameters before assembly."""

    function_name: str
    block_index: int
    functional_kind: str
    source: str
    parameters: tuple[EmittedParameter, ...]


@dataclass(frozen=True, slots=True)
class BlockEmission:
    lines: tuple[str, ...]
    imports: tuple[str, ...] = ()
    runtime_exports: tuple[str, ...] = ()
    assets: tuple[tuple[str, str], ...] = ()


__all__ = [
    "BlockEmission",
    "CodeExpr",
    "EmittedParameter",
    "EmittedScript",
    "EmittedScope",
    "EmittedStep",
    "ParameterDefinition",
    "RenderedArgument",
    "RenderedCall",
    "SourceRange",
    "StepEmission",
    "UtilityOperationDefinition",
]
