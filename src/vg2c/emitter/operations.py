from __future__ import annotations

import inspect
from collections.abc import Callable
from string import Formatter
from types import UnionType
from typing import (
    Any,
    Generic,
    ParamSpec,
    TypeVar,
    Union,
    get_args,
    get_origin,
    get_type_hints,
    overload,
)

from vg2c.emitter.models import (
    ArtifactRole,
    CodeExpr,
    EditorType,
    OperationVisibility,
    ParameterDefinition,
    RenderedArgument,
    RenderedCall,
    SourceRange,
    UtilityOperationDefinition,
)
from vg2c.utility_metadata import (
    FileEffectDefinition,
    ValueSchema,
    safe_literal,
    value_schema,
)

P = ParamSpec("P")
R = TypeVar("R")


class EmittableOperation(Generic[P, R]):
    """Descriptor that renders utility calls and records their semantic arguments."""

    def __init__(
        self,
        func: Callable[P, R],
        *,
        capabilities: tuple[str, ...] = (),
        parameter_capabilities: dict[str, tuple[str, ...]] | None = None,
        artifact_roles: dict[str, ArtifactRole] | None = None,
        parameter_labels: dict[str, str] | None = None,
        parameter_visibility: dict[str, OperationVisibility] | None = None,
        parameter_schemas: dict[str, ValueSchema] | None = None,
        file_effects: tuple[FileEffectDefinition, ...] | None = None,
        supported_mutations: tuple[str, ...] = ("set-parameter",),
        display_name: str | None = None,
        summary: str | None = None,
        visibility: OperationVisibility | None = None,
    ) -> None:
        self.func = func
        self.__name__ = func.__name__
        self.__doc__ = func.__doc__
        self.capabilities = tuple(capabilities)
        self.parameter_capabilities = tuple(
            (name, tuple(values))
            for name, values in (parameter_capabilities or {}).items()
        )
        self.artifact_roles = tuple((artifact_roles or {}).items())
        self.parameter_labels = parameter_labels or {}
        self.parameter_visibility = parameter_visibility or {}
        self.parameter_schemas = parameter_schemas or {}
        self.file_effects = file_effects
        self.supported_mutations = tuple(supported_mutations)
        self.display_name = display_name
        self.summary = summary
        self.visibility = visibility

    @overload
    def __get__(self, instance: None, owner: Any) -> EmittableMethod[P, R]: ...

    @overload
    def __get__(self, instance: object, owner: Any) -> BoundEmittableMethod[P, R]: ...

    def __get__(self, instance: Any, owner: Any) -> Any:
        if instance is None:
            return EmittableMethod(self, owner)
        return BoundEmittableMethod(self, instance, owner)

    def definition(self, owner: Any) -> UtilityOperationDefinition:
        return _operation_definition(owner, self)

    @staticmethod
    def render_method_call(
        definition: UtilityOperationDefinition,
        *,
        args: tuple[Any, ...] = (),
        kwargs: dict[str, Any] | None = None,
        function: str | None = None,
    ) -> RenderedCall:
        receiver = (
            "ctx"
            if definition.utility_name == "ctx"
            else f"ctx.{definition.utility_name}"
        )
        prefix = f"{function or receiver + '.' + definition.method}("
        parts: list[str] = []
        rendered_arguments: list[RenderedArgument] = []
        cursor = len(prefix)

        positional_definitions = [
            parameter
            for parameter in definition.parameters
            if parameter.position is not None
        ]
        for position, value in enumerate(args):
            source, metadata = _render_argument(value)
            parameter_definition = (
                positional_definitions[position]
                if position < len(positional_definitions)
                else None
            )
            name = (
                parameter_definition.name
                if parameter_definition
                else f"arg_{position + 1}"
            )
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


@overload
def emittable(func: Callable[P, R], /) -> EmittableOperation[P, R]: ...


@overload
def emittable(
    *,
    capabilities: tuple[str, ...] = (),
    parameter_capabilities: dict[str, tuple[str, ...]] | None = None,
    artifact_roles: dict[str, ArtifactRole] | None = None,
    parameter_labels: dict[str, str] | None = None,
    parameter_visibility: dict[str, OperationVisibility] | None = None,
    parameter_schemas: dict[str, ValueSchema] | None = None,
    file_effects: tuple[FileEffectDefinition, ...] | None = None,
    supported_mutations: tuple[str, ...] = ("set-parameter",),
    display_name: str | None = None,
    summary: str | None = None,
    visibility: OperationVisibility | None = None,
) -> Callable[[Callable[P, R]], EmittableOperation[P, R]]: ...


def emittable(
    func: Callable[P, R] | None = None,
    /,
    *,
    capabilities: tuple[str, ...] = (),
    parameter_capabilities: dict[str, tuple[str, ...]] | None = None,
    artifact_roles: dict[str, ArtifactRole] | None = None,
    parameter_labels: dict[str, str] | None = None,
    parameter_visibility: dict[str, OperationVisibility] | None = None,
    parameter_schemas: dict[str, ValueSchema] | None = None,
    file_effects: tuple[FileEffectDefinition, ...] | None = None,
    supported_mutations: tuple[str, ...] = ("set-parameter",),
    display_name: str | None = None,
    summary: str | None = None,
    visibility: OperationVisibility | None = None,
) -> EmittableOperation[P, R] | Callable[[Callable[P, R]], EmittableOperation[P, R]]:
    """Declare an emitted operation and any non-inferable semantic metadata."""

    def decorate(target: Callable[P, R]) -> EmittableOperation[P, R]:
        return EmittableOperation(
            target,
            capabilities=capabilities,
            parameter_capabilities=parameter_capabilities,
            artifact_roles=artifact_roles,
            parameter_labels=parameter_labels,
            parameter_visibility=parameter_visibility,
            parameter_schemas=parameter_schemas,
            file_effects=file_effects,
            supported_mutations=supported_mutations,
            display_name=display_name,
            summary=summary,
            visibility=visibility,
        )

    return decorate(func) if func is not None else decorate


class EmittableMethod(Generic[P, R]):
    def __init__(self, descriptor: EmittableOperation[P, R], owner: Any) -> None:
        self.descriptor = descriptor
        self.func = descriptor.func
        self.owner = owner

    def __call__(self, instance: Any, *args: P.args, **kwargs: P.kwargs) -> R:
        return self.func(instance, *args, **kwargs)

    def render(self, *args: Any, **kwargs: Any) -> RenderedCall:
        return EmittableOperation.render_method_call(
            self.descriptor.definition(self.owner), args=args, kwargs=kwargs
        )


class BoundEmittableMethod(Generic[P, R]):
    def __init__(
        self, descriptor: EmittableOperation[P, R], instance: Any, owner: Any
    ) -> None:
        self.descriptor = descriptor
        self.func = descriptor.func
        self.instance = instance
        self.owner = owner

    def __call__(self, *args: P.args, **kwargs: P.kwargs) -> R:
        return self.func(self.instance, *args, **kwargs)

    def render(self, *args: Any, **kwargs: Any) -> RenderedCall:
        return EmittableOperation.render_method_call(
            self.descriptor.definition(self.owner), args=args, kwargs=kwargs
        )


def _render_argument(value: Any) -> tuple[str, dict[str, Any]]:
    if isinstance(value, CodeExpr):
        metadata = (
            _value_metadata(value.value) if value.has_value else _dynamic_metadata()
        )
        metadata["global_names"] = value.global_names
        metadata["symbol_names"] = value.symbol_names
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
    if not safe_literal(value):
        return {
            "value": None,
            "editor_type": "dynamic",
            "editable": False,
            "read_only_reason": "This literal type is not supported",
        }
    if isinstance(value, bool):
        editor_type = "boolean"
    elif isinstance(value, int):
        editor_type = "integer"
    elif isinstance(value, float):
        editor_type = "number"
    elif isinstance(value, dict):
        editor_type = "object"
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
        "value": value,
        "editor_type": editor_type,
        "editable": editable,
        "read_only_reason": None,
    }


def _operation_definition(
    owner: Any, operation: EmittableOperation[P, R]
) -> UtilityOperationDefinition:
    func = operation.func
    utility_name = getattr(owner, "utility_name", owner.__name__.lower())
    signature = inspect.signature(func)
    try:
        hints = get_type_hints(func)
    except (NameError, TypeError):
        hints = {}
    parameters: list[ParameterDefinition] = []
    parameter_capabilities = dict(operation.parameter_capabilities)
    artifact_roles = dict(operation.artifact_roles)
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
        hint = hints.get(parameter.name, parameter.annotation)
        schema = operation.parameter_schemas.get(parameter.name, value_schema(hint))
        required = parameter.default is inspect.Parameter.empty
        capabilities = parameter_capabilities.get(parameter.name, ())
        artifact_role = artifact_roles.get(parameter.name)
        parameters.append(
            ParameterDefinition(
                name=parameter.name,
                position=position,
                annotation=_annotation(parameter.annotation),
                display_label=operation.parameter_labels.get(
                    parameter.name, _parameter_label(parameter.name)
                ),
                visibility=operation.parameter_visibility.get(parameter.name, "normal"),
                required=required,
                default=None if required else _json_default(parameter.default),
                schema=schema,
                keyword=parameter.kind
                in {
                    inspect.Parameter.POSITIONAL_OR_KEYWORD,
                    inspect.Parameter.KEYWORD_ONLY,
                },
                capabilities=capabilities,
                artifact_role=artifact_role,
            )
        )
    known_names = {parameter.name for parameter in parameters}
    for metadata in (
        parameter_capabilities,
        artifact_roles,
        operation.parameter_labels,
        operation.parameter_visibility,
        operation.parameter_schemas,
    ):
        unknown = set(metadata) - known_names
        if unknown:
            raise ValueError(f"Unknown utility parameters for {func.__name__}: {sorted(unknown)}")
    for parameter in parameters:
        if not parameter.display_label.strip():
            raise ValueError(f"Missing label for {func.__name__}.{parameter.name}")
        if parameter.visibility not in {"normal", "advanced", "internal"}:
            raise ValueError(f"Invalid visibility for {func.__name__}.{parameter.name}")
    if operation.summary is not None:
        _validate_summary(operation.summary, parameters)
    return UtilityOperationDefinition(
        id=f"{utility_name}.{func.__name__}",
        utility_name=utility_name,
        class_name=owner.__name__,
        module=owner.__module__,
        method=func.__name__,
        display_name=operation.display_name or _title(func.__name__),
        parameters=tuple(parameters),
        summary_template=operation.summary,
        capabilities=operation.capabilities,
        supported_mutations=operation.supported_mutations,
        file_effects=operation.file_effects,
        visibility=operation.visibility or getattr(owner, "semantic_visibility", "normal"),
    )


def _parameter_label(name: str) -> str:
    labels = {
        "src": "Source",
        "dst": "Destination",
        "path": "File",
        "paths": "Files",
        "output": "Output file",
        "inputs": "Input files",
        "source": "Source file",
        "destination": "Destination file",
        "argv": "Command",
        "to": "To",
        "subject": "Subject",
        "body": "Body",
        "attachments": "Attachments",
        "from_addr": "From",
        "sql": "SQL",
    }
    return labels.get(name, name.replace("_", " ").title())


def _validate_summary(template: str, parameters: list[ParameterDefinition]) -> None:
    available = {parameter.name: parameter for parameter in parameters}
    for _, field, format_spec, conversion in Formatter().parse(template):
        if field is None:
            continue
        if format_spec or conversion or field not in available:
            raise ValueError(f"Invalid summary placeholder: {field!r}")
        if available[field].visibility == "internal":
            raise ValueError(f"Summary cannot expose internal parameter: {field}")


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
    "BoundEmittableMethod",
    "EmittableMethod",
    "EmittableOperation",
    "emittable",
]
