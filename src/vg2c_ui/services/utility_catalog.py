from __future__ import annotations

import inspect
from dataclasses import dataclass
from types import UnionType
from typing import Any, Literal, Union, get_args, get_origin

from vg2c.emitter.models import EmittableMethod, emittable
from vg2c.kind import Kind
from vg2c.utilities import ensure_utility_checks_loaded
from vg2c.utilities._base import UtilitySpec
from vg2c_ui.domain.models import UtilityDescriptor


@dataclass(frozen=True, slots=True)
class MethodMetadata:
    utility: UtilityDescriptor
    parameters: tuple[inspect.Parameter, ...]


class UtilityCatalog:
    """Read-only UI metadata projected from the compiler utility registry."""

    def __init__(self) -> None:
        ensure_utility_checks_loaded()
        self._by_name = {
            utility.utility_name: utility for utility in UtilitySpec.registered()
        }

    def resolve(self, call_target: str | None, kind: Kind) -> MethodMetadata:
        utility_name, method_name = _call_parts(call_target)
        utility = self._by_name.get(utility_name or "")
        fallback = False
        if utility is None:
            utility = UtilitySpec.for_kind(kind)
            fallback = True
        if utility is None:
            return MethodMetadata(_unknown_descriptor(call_target), ())

        function = _emittable_function(utility, method_name)
        if function is None and method_name:
            fallback = True
        signature = inspect.signature(function) if function else None
        parameters = tuple(signature.parameters.values())[1:] if signature else ()
        class_doc = inspect.cleandoc(utility.__doc__) if utility.__doc__ else ""
        method_doc = inspect.getdoc(function) if function else None
        return MethodMetadata(
            UtilityDescriptor(
                name=utility.utility_name,
                class_name=utility.__name__,
                module=utility.__module__,
                title=_title(utility.__name__),
                description=class_doc or f"{_title(utility.__name__)} utility",
                method=method_name if function else None,
                method_description=method_doc,
                return_type=_annotation(signature.return_annotation) if signature else None,
                fallback=fallback,
            ),
            parameters,
        )


def enrich_parameter(
    metadata: MethodMetadata,
    name: str,
    position: int | None,
) -> dict[str, Any]:
    parameter = _signature_parameter(metadata.parameters, name, position)
    if parameter is None:
        return {}
    annotation = _annotation(parameter.annotation)
    constraints: dict[str, Any] = {}
    if get_origin(parameter.annotation) is Literal:
        constraints["choices"] = list(get_args(parameter.annotation))
    required = parameter.default is inspect.Parameter.empty
    default = None if required else _json_default(parameter.default)
    return {
        "name": parameter.name,
        "annotation": annotation,
        "required": required,
        "default": default,
        "constraints": constraints,
    }


def _signature_parameter(
    parameters: tuple[inspect.Parameter, ...], name: str, position: int | None
) -> inspect.Parameter | None:
    if position is not None:
        positional = [
            item
            for item in parameters
            if item.kind
            in {inspect.Parameter.POSITIONAL_ONLY, inspect.Parameter.POSITIONAL_OR_KEYWORD}
        ]
        return positional[position] if position < len(positional) else None
    return next((item for item in parameters if item.name == name), None)


def _emittable_function(utility: type[UtilitySpec], method_name: str | None) -> Any:
    if not method_name:
        return None
    raw = inspect.getattr_static(utility, method_name, None)
    if isinstance(raw, emittable):
        return raw.func
    value = getattr(utility, method_name, None)
    if isinstance(value, EmittableMethod):
        return value.func
    return None


def _call_parts(call_target: str | None) -> tuple[str | None, str | None]:
    if not call_target:
        return None, None
    parts = call_target.split(".")
    if len(parts) == 2 and parts[0] == "ctx":
        return "ctx", parts[1]
    if len(parts) == 3 and parts[0] == "ctx":
        return parts[1], parts[2]
    return None, parts[-1]


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
    json_types = (str, int, float, bool, list, dict)
    return value if value is None or isinstance(value, json_types) else repr(value)


def _unknown_descriptor(call_target: str | None) -> UtilityDescriptor:
    return UtilityDescriptor(
        name="utility",
        class_name="UnknownUtility",
        module="vg2c.utilities.generic",
        title="Unknown utility",
        description="This utility is not registered or does not expose editable metadata.",
        method=call_target.rsplit(".", 1)[-1] if call_target else None,
        fallback=True,
    )


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


__all__ = ["MethodMetadata", "UtilityCatalog", "enrich_parameter"]
