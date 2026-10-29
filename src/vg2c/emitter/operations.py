from __future__ import annotations

import inspect
from collections.abc import Callable
from typing import (
    Any,
    Generic,
    ParamSpec,
    TypeVar,
    overload,
)

from vg2c.emitter.models import (
    CodeExpr,
    ParameterDefinition,
    RenderedArgument,
    RenderedCall,
    UtilityOperationDefinition,
)

P = ParamSpec("P")
R = TypeVar("R")


class EmittableOperation(Generic[P, R]):
    """Descriptor that renders utility calls and records their semantic arguments."""

    def __init__(
        self,
        func: Callable[P, R],
        *,
        artifact_roles: dict[str, bool] | None = None,
    ) -> None:
        self.func = func
        self.__name__ = func.__name__
        self.__doc__ = func.__doc__
        self.artifact_roles = dict(artifact_roles or {})

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
        positional = [p for p in definition.parameters if p.position is not None]
        arguments = [
            _rendered_argument(
                positional[index].name if index < len(positional) else f"arg_{index + 1}",
                index,
                value,
                positional[index] if index < len(positional) else None,
            )
            for index, value in enumerate(args)
        ]
        arguments.extend(
            _rendered_argument(name, None, value, definition.parameter(name))
            for name, value in (kwargs or {}).items()
        )
        parts = (
            argument.source if argument.position is not None
            else f"{argument.name}={argument.source}"
            for argument in arguments
        )
        source = f"{function or receiver + '.' + definition.method}({', '.join(parts)})"
        return RenderedCall(source, definition, tuple(arguments))


@overload
def emittable(func: Callable[P, R], /) -> EmittableOperation[P, R]: ...


@overload
def emittable(
    *,
    artifact_roles: dict[str, bool] | None = None,
) -> Callable[[Callable[P, R]], EmittableOperation[P, R]]: ...


def emittable(
    func: Callable[P, R] | None = None,
    /,
    *,
    artifact_roles: dict[str, bool] | None = None,
) -> EmittableOperation[P, R] | Callable[[Callable[P, R]], EmittableOperation[P, R]]:
    """Declare an emitted operation and any non-inferable semantic metadata."""

    def decorate(target: Callable[P, R]) -> EmittableOperation[P, R]:
        return EmittableOperation(
            target,
            artifact_roles=artifact_roles,
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


class BoundEmittableMethod(EmittableMethod[P, R]):
    def __init__(
        self, descriptor: EmittableOperation[P, R], instance: Any, owner: Any
    ) -> None:
        super().__init__(descriptor, owner)
        self.instance = instance

    def __call__(self, *args: P.args, **kwargs: P.kwargs) -> R:
        return self.func(self.instance, *args, **kwargs)


def _rendered_argument(
    name: str, position: int | None, value: Any, parameter: ParameterDefinition | None
) -> RenderedArgument:
    if isinstance(value, CodeExpr):
        source, rendered = value.source, value.value if value.has_value else None
    else:
        source, rendered = repr(value), value
    return RenderedArgument(
        name, position, source, rendered, bool(parameter and parameter.artifact_role)
    )


def _operation_definition(
    owner: Any, operation: EmittableOperation[P, R]
) -> UtilityOperationDefinition:
    func = operation.func
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
        parameters.append(
            ParameterDefinition(
                name=parameter.name,
                position=position,
                artifact_role=operation.artifact_roles.get(parameter.name, False),
            )
        )
    known_names = {parameter.name for parameter in parameters}
    unknown = set(operation.artifact_roles) - known_names
    if unknown:
        raise ValueError(f"Unknown utility parameters for {func.__name__}: {sorted(unknown)}")
    return UtilityOperationDefinition(
        utility_name=utility_name,
        method=func.__name__,
        parameters=tuple(parameters),
    )


__all__ = [
    "BoundEmittableMethod",
    "EmittableMethod",
    "EmittableOperation",
    "emittable",
]
