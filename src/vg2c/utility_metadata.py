from __future__ import annotations

import math
from dataclasses import dataclass, replace
from pathlib import Path
from types import UnionType
from typing import (
    Any,
    Literal,
    NotRequired,
    Required,
    Union,
    get_args,
    get_origin,
    get_type_hints,
    is_typeddict,
)

ValueKind = Literal[
    "string", "integer", "number", "boolean", "list", "object", "union", "dynamic"
]
FileEffectKind = Literal[
    "read",
    "observe",
    "write",
    "copy",
    "move",
    "transform",
    "append",
    "delete",
    "unknown",
]
PathBase = Literal["working-directory", "script-directory", "runtime-search"]


@dataclass(frozen=True, slots=True)
class FileEffectDefinition:
    key: str
    kind: FileEffectKind
    inputs: tuple[str, ...] = ()
    outputs: tuple[str, ...] = ()
    input_base: PathBase = "runtime-search"
    output_base: PathBase = "script-directory"
    reason: str | None = None
    input_format: Literal["path", "table-binding"] = "path"


@dataclass(frozen=True, slots=True)
class ValueSchema:
    kind: ValueKind
    nullable: bool = False
    choices: tuple[Any, ...] = ()
    items: ValueSchema | None = None
    properties: tuple[tuple[str, ValueSchema], ...] = ()
    required_keys: tuple[str, ...] = ()
    variants: tuple[ValueSchema, ...] = ()
    path: bool = False
    prefix_items: tuple[ValueSchema, ...] = ()
    tuple_value: bool = False

    def accepts(self, value: Any) -> bool:
        if value is None:
            return self.nullable
        if self.choices and not any(
            type(value) is type(choice) and value == choice for choice in self.choices
        ):
            return False
        if self.kind == "string":
            return isinstance(value, str)
        if self.kind == "integer":
            return type(value) is int
        if self.kind == "number":
            return type(value) is int or type(value) is float and math.isfinite(value)
        if self.kind == "boolean":
            return type(value) is bool
        if self.kind == "list":
            if self.tuple_value:
                return (
                    isinstance(value, (list, tuple))
                    and len(value) == len(self.prefix_items)
                    and all(
                        schema.accepts(item)
                        for schema, item in zip(self.prefix_items, value)
                    )
                )
            return (
                isinstance(value, list)
                and self.items is not None
                and all(self.items.accepts(item) for item in value)
            )
        if self.kind == "object":
            if not isinstance(value, dict) or not all(
                isinstance(key, str) for key in value
            ):
                return False
            properties = dict(self.properties)
            return set(self.required_keys) <= value.keys() and all(
                (
                    properties[key].accepts(item)
                    if key in properties
                    else self.items is not None and self.items.accepts(item)
                )
                for key, item in value.items()
            )
        if self.kind == "union":
            return any(variant.accepts(value) for variant in self.variants)
        return False

    def python_value(self, value: Any) -> Any:
        if value is None:
            return None
        if self.kind == "union":
            schema = next((item for item in self.variants if item.accepts(value)), None)
            return schema.python_value(value) if schema else value
        if self.tuple_value:
            return tuple(
                schema.python_value(item)
                for schema, item in zip(self.prefix_items, value)
            )
        if self.kind == "list" and self.items:
            return [self.items.python_value(item) for item in value]
        if self.kind == "object":
            properties = dict(self.properties)
            return {
                key: (
                    schema.python_value(item)
                    if (schema := properties.get(key, self.items))
                    else item
                )
                for key, item in value.items()
            }
        return value


def value_schema(annotation: Any) -> ValueSchema:
    origin = get_origin(annotation)
    arguments = get_args(annotation)
    if origin in {Required, NotRequired}:
        return value_schema(arguments[0])
    if origin in {Union, UnionType}:
        nullable = type(None) in arguments
        variants = tuple(
            dict.fromkeys(
                value_schema(item) for item in arguments if item is not type(None)
            )
        )
        if len(variants) == 1:
            return replace(variants[0], nullable=nullable or variants[0].nullable)
        if set(arguments) <= {str, Path, type(None)}:
            return ValueSchema("string", nullable=nullable, path=True)
        return ValueSchema("union", nullable=nullable, variants=variants)
    if origin is Literal:
        values = tuple(item for item in arguments if item is not None)
        variants = tuple(dict.fromkeys(value_schema(type(item)) for item in values))
        kind = variants[0].kind if len(variants) == 1 else "union"
        return ValueSchema(
            kind,
            nullable=None in arguments,
            choices=values,
            variants=variants if len(variants) != 1 else (),
        )
    if annotation is Path:
        return ValueSchema("dynamic", path=True)
    if annotation in (str, int, float, bool):
        return ValueSchema(
            {str: "string", int: "integer", float: "number", bool: "boolean"}[
                annotation
            ]
        )
    if origin is list and arguments:
        return ValueSchema("list", items=value_schema(arguments[0]))
    if origin is tuple and arguments and Ellipsis not in arguments:
        return ValueSchema(
            "list",
            prefix_items=tuple(value_schema(item) for item in arguments),
            tuple_value=True,
        )
    if origin is dict and len(arguments) == 2 and arguments[0] is str:
        return ValueSchema("object", items=value_schema(arguments[1]))
    if is_typeddict(annotation):
        hints = get_type_hints(annotation, include_extras=True)
        required = {
            name
            for name, hint in hints.items()
            if get_origin(hint) is Required
            or get_origin(hint) is not NotRequired
            and name in annotation.__required_keys__
        }
        return ValueSchema(
            "object",
            properties=tuple(
                (name, value_schema(hint)) for name, hint in hints.items()
            ),
            required_keys=tuple(sorted(required)),
        )
    return ValueSchema("dynamic")


def safe_literal(value: Any) -> bool:
    if value is None or type(value) in {str, int, bool}:
        return True
    if type(value) is float:
        return math.isfinite(value)
    if isinstance(value, (list, tuple)):
        return all(safe_literal(item) for item in value)
    if isinstance(value, dict):
        return all(
            isinstance(key, str) and safe_literal(item) for key, item in value.items()
        )
    return False
