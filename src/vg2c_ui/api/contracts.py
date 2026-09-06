from __future__ import annotations

import types
from typing import Any, Literal, Union, get_args, get_origin

from pydantic import BaseModel

from vg2c_ui.api.models import CONTRACT_MODELS

_HEADER = "// Generated from vg2c_ui.api.models. DO NOT EDIT.\n\n"


def render_typescript_contracts() -> str:
    chunks = [_HEADER]
    for model in CONTRACT_MODELS:
        chunks.append(f"export interface {model.__name__} {{\n")
        for name, field in model.model_fields.items():
            chunks.append(f"  {name}: {_ts_type(field.annotation)}\n")
        chunks.append("}\n\n")
    return "".join(chunks)


def _ts_type(annotation: Any) -> str:
    if annotation is Any:
        return "unknown"
    if annotation is None or annotation is type(None):
        return "null"
    if annotation is str:
        return "string"
    if annotation in {int, float}:
        return "number"
    if annotation is bool:
        return "boolean"
    if isinstance(annotation, type) and issubclass(annotation, BaseModel):
        return annotation.__name__

    origin = get_origin(annotation)
    args = get_args(annotation)
    if origin is Literal:
        return " | ".join(_ts_literal(item) for item in args)
    if origin in {Union, types.UnionType}:
        return " | ".join(dict.fromkeys(_ts_type(item) for item in args))
    if origin in {list, tuple, set, frozenset}:
        item = _ts_type(args[0]) if args else "unknown"
        return f"Array<{item}>"
    if origin is dict:
        key = _ts_type(args[0]) if args else "string"
        value = _ts_type(args[1]) if len(args) > 1 else "unknown"
        return f"Record<{key}, {value}>"
    return "unknown"


def _ts_literal(value: Any) -> str:
    if value is None:
        return "null"
    if isinstance(value, bool):
        return "true" if value else "false"
    if isinstance(value, str):
        escaped = value.replace("\\", "\\\\").replace("'", "\\'")
        return f"'{escaped}'"
    return repr(value)


__all__ = ["render_typescript_contracts"]
