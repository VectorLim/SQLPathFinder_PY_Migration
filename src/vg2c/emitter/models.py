from __future__ import annotations

from dataclasses import dataclass
from typing import Any, Literal

from vg2c.utility_metadata import FileEffectDefinition, ValueSchema

_UNSET = object()


@dataclass(frozen=True, slots=True)
class CodeExpr:
    """Explicit raw Python source with an optional safe semantic value."""

    source: str
    value: Any = _UNSET
    global_names: tuple[str, ...] = ()
    symbol_names: tuple[str, ...] = ()

    @property
    def has_value(self) -> bool:
        return self.value is not _UNSET


EditorType = Literal[
    "string",
    "multiline",
    "integer",
    "number",
    "boolean",
    "list",
    "object",
    "union",
    "dynamic",
]
ArtifactDirection = Literal["input", "output"]
OperationVisibility = Literal["normal", "advanced", "internal"]


@dataclass(frozen=True, slots=True)
class SourceRange:
    start_offset: int
    end_offset: int


@dataclass(frozen=True, slots=True)
class ArtifactRole:
    direction: ArtifactDirection
    kind: str = "csv"
    many: bool = False


@dataclass(frozen=True, slots=True)
class ParameterDefinition:
    name: str
    position: int | None
    annotation: str | None
    display_label: str
    visibility: OperationVisibility
    required: bool
    default: Any = None
    schema: ValueSchema | None = None
    keyword: bool = True
    capabilities: tuple[str, ...] = ()
    artifact_role: ArtifactRole | None = None


@dataclass(frozen=True, slots=True)
class UtilityOperationDefinition:
    id: str
    utility_name: str
    class_name: str
    module: str
    method: str
    display_name: str
    parameters: tuple[ParameterDefinition, ...]
    summary_template: str | None = None
    capabilities: tuple[str, ...] = ()
    supported_mutations: tuple[str, ...] = ("set-parameter",)
    file_effects: tuple[FileEffectDefinition, ...] | None = None
    visibility: OperationVisibility = "normal"

    def parameter(self, name: str) -> ParameterDefinition | None:
        return next((item for item in self.parameters if item.name == name), None)

@dataclass(frozen=True, slots=True)
class RenderedArgument:
    name: str
    position: int | None
    source: str
    value: Any
    editor_type: EditorType
    editable: bool
    read_only_reason: str | None
    definition: ParameterDefinition | None
    source_range: SourceRange
    global_names: tuple[str, ...] = ()
    symbol_names: tuple[str, ...] = ()


class RenderedCall(str):
    """A generated call string carrying metadata known at render time."""

    definition: UtilityOperationDefinition
    arguments: tuple[RenderedArgument, ...]
    semantic_key: str | None

    def __new__(
        cls,
        source: str,
        definition: UtilityOperationDefinition,
        arguments: tuple[RenderedArgument, ...],
        semantic_key: str | None = None,
    ) -> RenderedCall:
        value = str.__new__(cls, source)
        value.definition = definition
        value.arguments = arguments
        value.semantic_key = semantic_key
        return value

    def with_key(self, semantic_key: str) -> RenderedCall:
        """Give this invocation an explicit identity when an operation repeats in one block."""
        key = semantic_key.strip()
        if not key:
            raise ValueError("emitted invocation semantic key must be non-empty")
        return RenderedCall(str(self), self.definition, self.arguments, key)


@dataclass(frozen=True, slots=True)
class EmittedParameter:
    id: str
    name: str
    position: int | None
    source: str
    value: Any
    editor_type: EditorType
    editable: bool
    read_only_reason: str | None
    definition: ParameterDefinition | None
    artifact_role: ArtifactRole | None
    source_range: SourceRange | None
    symbol_names: tuple[str, ...] = ()


@dataclass(frozen=True, slots=True)
class EmittedInvocation:
    id: str
    operation: UtilityOperationDefinition
    source_range: SourceRange
    parameters: tuple[EmittedParameter, ...]
    arguments: tuple[RenderedArgument, ...] = ()


@dataclass(frozen=True, slots=True)
class EmittedStep:
    function_name: str
    block_index: int
    functional_kind: str
    source: str
    source_range: SourceRange
    invocations: tuple[EmittedInvocation, ...]

    @property
    def parameters(self) -> tuple[EmittedParameter, ...]:
        return tuple(
            parameter
            for invocation in self.invocations
            for parameter in invocation.parameters
        )


@dataclass(frozen=True, slots=True)
class EmittedScope:
    """Generated control structure, including its body and closing source block."""

    start_index: int
    end_index: int
    source_range: SourceRange


@dataclass(frozen=True, slots=True)
class EmittedScript:
    """Stage 5 output plus the semantic/edit manifest produced during emission."""

    source: str
    imports: tuple[str, ...]
    steps: tuple[EmittedStep, ...] = ()
    assets: tuple[tuple[str, str], ...] = ()
    scopes: tuple[EmittedScope, ...] = ()

    def step_for_block(self, block_index: int) -> EmittedStep | None:
        return next(
            (step for step in self.steps if step.block_index == block_index), None
        )

@dataclass(frozen=True, slots=True)
class _RelativeInvocation:
    operation: UtilityOperationDefinition
    source_range: SourceRange
    arguments: tuple[RenderedArgument, ...]
    semantic_key: str | None = None


@dataclass(frozen=True, slots=True)
class StepEmission:
    """Internal step source plus relative metadata before final script assembly."""

    function_name: str
    block_index: int
    functional_kind: str
    source: str
    call_site: str
    invocations: tuple[_RelativeInvocation, ...]
    external_parameters: tuple[ExternalParameter, ...] = ()


@dataclass(frozen=True, slots=True)
class ExternalParameter:
    definition: ParameterDefinition
    value: Any
    editor_type: EditorType
    read_only_reason: str


@dataclass(frozen=True, slots=True)
class BlockEmission:
    lines: tuple[str, ...]
    imports: tuple[str, ...] = ()
    runtime_exports: tuple[str, ...] = ()
    assets: tuple[tuple[str, str], ...] = ()
    external_parameters: tuple[ExternalParameter, ...] = ()


__all__ = [
    "ArtifactRole",
    "BlockEmission",
    "CodeExpr",
    "EditorType",
    "OperationVisibility",
    "EmittedInvocation",
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
