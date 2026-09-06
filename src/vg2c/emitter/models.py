from __future__ import annotations

import inspect
from collections.abc import Callable
from dataclasses import dataclass
from types import UnionType
from typing import (
    Any,
    Generic,
    Literal,
    ParamSpec,
    TypeVar,
    Union,
    get_args,
    get_origin,
    overload,
)

P = ParamSpec("P")
R = TypeVar("R")
_UNSET = object()


@dataclass(frozen=True, slots=True)
class CodeExpr:
    """Explicit raw Python source with an optional safe semantic value."""

    source: str
    value: Any = _UNSET

    @property
    def has_value(self) -> bool:
        return self.value is not _UNSET


EditorType = Literal["string", "multiline", "integer", "boolean", "list", "dynamic"]
ArtifactDirection = Literal["input", "output"]


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
class OperationSpec:
    """Non-inferable semantic metadata attached directly to an emittable method."""

    capabilities: tuple[str, ...] = ()
    parameter_capabilities: tuple[tuple[str, tuple[str, ...]], ...] = ()
    artifact_roles: tuple[tuple[str, ArtifactRole], ...] = ()
    supported_mutations: tuple[str, ...] = ("set-parameter",)

    def capabilities_for_parameter(self, parameter_name: str) -> tuple[str, ...]:
        return next(
            (caps for name, caps in self.parameter_capabilities if name == parameter_name),
            (),
        )

    def artifact_role(self, parameter_name: str) -> ArtifactRole | None:
        return next((role for name, role in self.artifact_roles if name == parameter_name), None)


@dataclass(frozen=True, slots=True)
class ParameterDefinition:
    name: str
    position: int | None
    annotation: str | None
    required: bool
    default: Any = None
    choices: tuple[Any, ...] = ()


@dataclass(frozen=True, slots=True)
class UtilityOperationDefinition:
    id: str
    utility_name: str
    class_name: str
    module: str
    method: str
    title: str
    description: str
    method_description: str | None
    return_type: str | None
    parameters: tuple[ParameterDefinition, ...]
    capabilities: tuple[str, ...] = ()
    parameter_capabilities: tuple[tuple[str, tuple[str, ...]], ...] = ()
    artifact_roles: tuple[tuple[str, ArtifactRole], ...] = ()
    supported_mutations: tuple[str, ...] = ("set-parameter",)

    def parameter(self, name: str) -> ParameterDefinition | None:
        return next((item for item in self.parameters if item.name == name), None)

    def capabilities_for_parameter(self, parameter_name: str) -> tuple[str, ...]:
        return next(
            (caps for name, caps in self.parameter_capabilities if name == parameter_name),
            (),
        )

    def artifact_role(self, parameter_name: str) -> ArtifactRole | None:
        return next((role for name, role in self.artifact_roles if name == parameter_name), None)


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
    source_range: SourceRange


@dataclass(frozen=True, slots=True)
class EmittedInvocation:
    id: str
    operation: UtilityOperationDefinition
    source_range: SourceRange
    parameters: tuple[EmittedParameter, ...]


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
            parameter for invocation in self.invocations for parameter in invocation.parameters
        )


@dataclass(frozen=True, slots=True)
class EmittedScript:
    """Stage 5 output plus the semantic/edit manifest produced during emission."""

    source: str
    imports: tuple[str, ...]
    steps: tuple[EmittedStep, ...] = ()

    def step_for_block(self, block_index: int) -> EmittedStep | None:
        return next((step for step in self.steps if step.block_index == block_index), None)

    def parameter(self, parameter_id: str) -> EmittedParameter | None:
        for step in self.steps:
            for parameter in step.parameters:
                if parameter.id == parameter_id:
                    return parameter
        return None


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


def operation_spec(
    *,
    capabilities: tuple[str, ...] = (),
    parameter_capabilities: dict[str, tuple[str, ...]] | None = None,
    artifact_roles: dict[str, ArtifactRole] | None = None,
    supported_mutations: tuple[str, ...] = ("set-parameter",),
) -> Callable[[Callable[P, R]], Callable[P, R]]:
    """Attach only semantics that cannot be inferred from the Python signature."""
    spec = OperationSpec(
        capabilities=tuple(capabilities),
        parameter_capabilities=tuple(
            (name, tuple(values)) for name, values in (parameter_capabilities or {}).items()
        ),
        artifact_roles=tuple((artifact_roles or {}).items()),
        supported_mutations=tuple(supported_mutations),
    )

    def decorate(func: Callable[P, R]) -> Callable[P, R]:
        func.__vg2c_operation_spec__ = spec
        return func

    return decorate


class emittable(Generic[P, R]):
    """Descriptor that renders utility calls and records their semantic arguments."""

    def __init__(self, func: Callable[P, R]) -> None:
        self.func = func
        self.__name__ = func.__name__
        self.__doc__ = func.__doc__

    @overload
    def __get__(self, instance: None, owner: Any) -> EmittableMethod[P, R]: ...

    @overload
    def __get__(self, instance: object, owner: Any) -> BoundEmittableMethod[P, R]: ...

    def __get__(self, instance: Any, owner: Any) -> Any:
        if instance is None:
            return EmittableMethod(self, owner)
        return BoundEmittableMethod(self, instance, owner)

    def definition(self, owner: Any) -> UtilityOperationDefinition:
        return _operation_definition(owner, self.func)

    @staticmethod
    def render_method_call(
        definition: UtilityOperationDefinition,
        *,
        args: tuple[Any, ...] = (),
        kwargs: dict[str, Any] | None = None,
    ) -> RenderedCall:
        receiver = "ctx" if definition.utility_name == "ctx" else f"ctx.{definition.utility_name}"
        prefix = f"{receiver}.{definition.method}("
        parts: list[str] = []
        rendered_arguments: list[RenderedArgument] = []
        cursor = len(prefix)

        positional_definitions = [
            parameter for parameter in definition.parameters if parameter.position is not None
        ]
        for position, value in enumerate(args):
            source, metadata = _render_argument(value)
            parameter_definition = (
                positional_definitions[position] if position < len(positional_definitions) else None
            )
            name = parameter_definition.name if parameter_definition else f"arg_{position + 1}"
            rendered_arguments.append(
                RenderedArgument(
                    name=name,
                    position=position,
                    source=source,
                    definition=parameter_definition,
                    source_range=SourceRange(cursor, cursor + len(source)),
                    **metadata,
                )
            )
            parts.append(source)
            cursor += len(source) + 2

        for name, value in (kwargs or {}).items():
            source, metadata = _render_argument(value)
            part = f"{name}={source}"
            value_start = cursor + len(name) + 1
            rendered_arguments.append(
                RenderedArgument(
                    name=name,
                    position=None,
                    source=source,
                    definition=definition.parameter(name),
                    source_range=SourceRange(value_start, value_start + len(source)),
                    **metadata,
                )
            )
            parts.append(part)
            cursor += len(part) + 2

        source = f"{prefix}{', '.join(parts)})"
        return RenderedCall(source, definition, tuple(rendered_arguments))


class EmittableMethod(Generic[P, R]):
    def __init__(self, descriptor: emittable[P, R], owner: Any) -> None:
        self.descriptor = descriptor
        self.func = descriptor.func
        self.owner = owner

    def __call__(self, instance: Any, *args: P.args, **kwargs: P.kwargs) -> R:
        return self.func(instance, *args, **kwargs)

    def render(self, *args: Any, **kwargs: Any) -> RenderedCall:
        return emittable.render_method_call(
            self.descriptor.definition(self.owner), args=args, kwargs=kwargs
        )


class BoundEmittableMethod(Generic[P, R]):
    def __init__(self, descriptor: emittable[P, R], instance: Any, owner: Any) -> None:
        self.descriptor = descriptor
        self.func = descriptor.func
        self.instance = instance
        self.owner = owner

    def __call__(self, *args: P.args, **kwargs: P.kwargs) -> R:
        return self.func(self.instance, *args, **kwargs)

    def render(self, *args: Any, **kwargs: Any) -> RenderedCall:
        return emittable.render_method_call(
            self.descriptor.definition(self.owner), args=args, kwargs=kwargs
        )


def build_step_emission(
    *,
    function_name: str,
    block_index: int,
    functional_kind: str,
    body_lines: list[str],
) -> StepEmission:
    """Wrap emitted body lines while preserving top-level rendered-call metadata."""
    lines = [f"def {function_name}(ctx) -> None:"]
    invocations: list[_RelativeInvocation] = []

    if not body_lines:
        lines.append("    pass")
    else:
        for body_line in body_lines:
            rendered = body_line if isinstance(body_line, RenderedCall) else None
            call_start = _joined_length(lines) + 4
            source_text = str(body_line)
            for line in source_text.split("\n"):
                lines.append(f"    {line}" if line.strip() else "")
            if rendered is None:
                continue

            exact_call = _indent_call_source(str(rendered))[4:]
            call_end = call_start + len(exact_call)
            invocations.append(
                _RelativeInvocation(
                    operation=rendered.definition,
                    source_range=SourceRange(call_start, call_end),
                    arguments=tuple(
                        _adjust_argument_for_indentation(argument, rendered, exact_call, call_start)
                        for argument in rendered.arguments
                    ),
                    semantic_key=rendered.semantic_key,
                )
            )

    return StepEmission(
        function_name=function_name,
        block_index=block_index,
        functional_kind=functional_kind,
        source="\n".join(lines),
        call_site=f"{function_name}(ctx)",
        invocations=tuple(invocations),
    )


def finalize_steps(source: str, emissions: list[StepEmission]) -> tuple[EmittedStep, ...]:
    """Convert emitter-relative metadata into absolute ranges in the final source."""
    finalized: list[EmittedStep] = []
    search_from = 0
    for emission in emissions:
        step_start = source.find(emission.source, search_from)
        if step_start < 0:
            raise ValueError(f"emitted step not found in final source: {emission.function_name}")
        step_end = step_start + len(emission.source)
        search_from = step_end
        seen_invocation_ids: set[str] = set()
        invocations: list[EmittedInvocation] = []
        for relative in emission.invocations:
            semantic_key = relative.semantic_key or "default"
            invocation_id = f"block-{emission.block_index}:{relative.operation.id}:{semantic_key}"
            if invocation_id in seen_invocation_ids:
                raise ValueError(
                    "ambiguous emitted invocation identity for "
                    f"block {emission.block_index} and {relative.operation.id}; "
                    "use RenderedCall.with_key() for repeated operations"
                )
            seen_invocation_ids.add(invocation_id)
            parameters: list[EmittedParameter] = []
            for argument in relative.arguments:
                key = (
                    argument.definition.name
                    if argument.definition
                    else (
                        f"pos-{argument.position}"
                        if argument.position is not None
                        else argument.name
                    )
                )
                parameters.append(
                    EmittedParameter(
                        id=f"{invocation_id}:{key}",
                        name=argument.name,
                        position=argument.position,
                        source=argument.source,
                        value=argument.value,
                        editor_type=argument.editor_type,
                        editable=argument.editable,
                        read_only_reason=argument.read_only_reason,
                        definition=argument.definition,
                        artifact_role=relative.operation.artifact_role(argument.name),
                        source_range=SourceRange(
                            step_start + argument.source_range.start_offset,
                            step_start + argument.source_range.end_offset,
                        ),
                    )
                )
            invocations.append(
                EmittedInvocation(
                    id=invocation_id,
                    operation=relative.operation,
                    source_range=SourceRange(
                        step_start + relative.source_range.start_offset,
                        step_start + relative.source_range.end_offset,
                    ),
                    parameters=tuple(parameters),
                )
            )
        finalized.append(
            EmittedStep(
                function_name=emission.function_name,
                block_index=emission.block_index,
                functional_kind=emission.functional_kind,
                source=emission.source,
                source_range=SourceRange(step_start, step_end),
                invocations=tuple(invocations),
            )
        )
    return tuple(finalized)


def _adjust_argument_for_indentation(
    argument: RenderedArgument,
    rendered: RenderedCall,
    exact_call: str,
    call_start: int,
) -> RenderedArgument:
    start = _indent_adjusted_offset(str(rendered), argument.source_range.start_offset)
    end = _indent_adjusted_offset(str(rendered), argument.source_range.end_offset)
    return RenderedArgument(
        name=argument.name,
        position=argument.position,
        source=exact_call[start:end],
        value=argument.value,
        editor_type=argument.editor_type,
        editable=argument.editable,
        read_only_reason=argument.read_only_reason,
        definition=argument.definition,
        source_range=SourceRange(call_start + start, call_start + end),
    )


def _indent_call_source(source: str) -> str:
    return "\n".join(f"    {line}" if line.strip() else "" for line in source.split("\n"))


def _indent_adjusted_offset(source: str, offset: int) -> int:
    line_index = source[:offset].count("\n")
    lines = source.split("\n")
    extra = sum(4 for line in lines[1 : line_index + 1] if line.strip())
    return offset + extra


def _joined_length(lines: list[str]) -> int:
    return len("\n".join(lines)) + (1 if lines else 0)


def _render_argument(value: Any) -> tuple[str, dict[str, Any]]:
    if isinstance(value, CodeExpr):
        metadata = _value_metadata(value.value) if value.has_value else _dynamic_metadata()
        return value.source, metadata
    return repr(value), _value_metadata(value)


def _dynamic_metadata() -> dict[str, Any]:
    return {
        "value": None,
        "editor_type": "dynamic",
        "editable": False,
        "read_only_reason": "Dynamic Python expressions are read-only",
    }


def _value_metadata(value: Any) -> dict[str, Any]:
    editor_type: EditorType
    if isinstance(value, bool):
        editor_type = "boolean"
    elif isinstance(value, int):
        editor_type = "integer"
    elif isinstance(value, list) and all(_safe_list_item(item) for item in value):
        editor_type = "list"
    elif isinstance(value, str) and "\n" in value:
        editor_type = "multiline"
    elif isinstance(value, str):
        editor_type = "string"
    else:
        editor_type = "dynamic"
    editable = editor_type != "dynamic"
    return {
        "value": value if editable else None,
        "editor_type": editor_type,
        "editable": editable,
        "read_only_reason": None if editable else "This literal type is not supported",
    }


def _operation_definition(owner: Any, func: Callable[..., Any]) -> UtilityOperationDefinition:
    utility_name = getattr(owner, "utility_name", owner.__name__.lower())
    signature = inspect.signature(func)
    parameters: list[ParameterDefinition] = []
    positional_index = 0
    for index, parameter in enumerate(signature.parameters.values()):
        if index == 0 and parameter.name in {"self", "cls"}:
            continue
        is_positional = parameter.kind in {
            inspect.Parameter.POSITIONAL_ONLY,
            inspect.Parameter.POSITIONAL_OR_KEYWORD,
        }
        position = positional_index if is_positional else None
        if is_positional:
            positional_index += 1
        choices = (
            tuple(get_args(parameter.annotation))
            if get_origin(parameter.annotation) is Literal
            else ()
        )
        required = parameter.default is inspect.Parameter.empty
        parameters.append(
            ParameterDefinition(
                name=parameter.name,
                position=position,
                annotation=_annotation(parameter.annotation),
                required=required,
                default=None if required else _json_default(parameter.default),
                choices=choices,
            )
        )
    spec = getattr(func, "__vg2c_operation_spec__", OperationSpec())
    class_doc = inspect.cleandoc(owner.__doc__) if owner.__doc__ else ""
    return UtilityOperationDefinition(
        id=f"{utility_name}.{func.__name__}",
        utility_name=utility_name,
        class_name=owner.__name__,
        module=owner.__module__,
        method=func.__name__,
        title=_title(owner.__name__),
        description=class_doc or f"{_title(owner.__name__)} utility",
        method_description=inspect.getdoc(func),
        return_type=_annotation(signature.return_annotation),
        parameters=tuple(parameters),
        capabilities=spec.capabilities,
        parameter_capabilities=spec.parameter_capabilities,
        artifact_roles=spec.artifact_roles,
        supported_mutations=spec.supported_mutations,
    )


def _annotation(value: Any) -> str | None:
    if value is inspect.Signature.empty or value is inspect.Parameter.empty:
        return None
    if isinstance(value, str):
        return value
    origin = get_origin(value)
    if origin in {Union, UnionType}:
        return " | ".join(filter(None, (_annotation(item) for item in get_args(value))))
    return inspect.formatannotation(value).replace("typing.", "")


def _json_default(value: Any) -> Any:
    json_types = (str, int, float, bool, list, dict, tuple)
    return value if value is None or isinstance(value, json_types) else repr(value)


def _safe_list_item(value: object) -> bool:
    return value is None or isinstance(value, (str, int, float, bool))


def _title(value: str) -> str:
    words: list[str] = []
    current = ""
    for character in value:
        if current and character.isupper() and not current[-1].isupper():
            words.append(current)
            current = character
        else:
            current += character
    if current:
        words.append(current)
    return " ".join(words)


__all__ = [
    "ArtifactRole",
    "BoundEmittableMethod",
    "CodeExpr",
    "EditorType",
    "EmittableMethod",
    "EmittedInvocation",
    "EmittedParameter",
    "EmittedScript",
    "EmittedStep",
    "OperationSpec",
    "ParameterDefinition",
    "RenderedCall",
    "SourceRange",
    "StepEmission",
    "UtilityOperationDefinition",
    "build_step_emission",
    "emittable",
    "finalize_steps",
    "operation_spec",
]
