from __future__ import annotations

import json
from pathlib import Path
from typing import Any

from pydantic import BaseModel, Field, ValidationError

from vg2c_ui.services.atomic_io import atomic_write_text

SIDECAR_VERSION = 2


class SavedParameterChange(BaseModel):
    parameter_id: str
    value: Any


class EditorSidecar(BaseModel):
    """Persistence-only state for validated generated-Python edits."""

    schema_version: int = SIDECAR_VERSION
    source_hash: str
    output_hash: str
    changes: list[SavedParameterChange] = Field(default_factory=list)


class InvalidSidecar(ValueError):
    pass


def sidecar_path(output_path: Path) -> Path:
    return output_path.with_suffix(".vg2c-ui.json")


def read_sidecar(output_path: Path) -> EditorSidecar | None:
    path = sidecar_path(output_path)
    if not path.exists():
        return None
    try:
        sidecar = EditorSidecar.model_validate_json(path.read_text(encoding="utf-8"))
    except (OSError, ValidationError, json.JSONDecodeError) as exc:
        raise InvalidSidecar(f"invalid sidecar {path}: {exc}") from exc
    if sidecar.schema_version != SIDECAR_VERSION:
        raise InvalidSidecar(
            f"unsupported sidecar version {sidecar.schema_version}; expected {SIDECAR_VERSION}"
        )
    return sidecar


def write_sidecar(output_path: Path, sidecar: EditorSidecar) -> Path:
    path = sidecar_path(output_path)
    atomic_write_text(path, sidecar.model_dump_json(indent=2) + "\n")
    return path


__all__ = [
    "EditorSidecar",
    "InvalidSidecar",
    "SIDECAR_VERSION",
    "SavedParameterChange",
    "read_sidecar",
    "sidecar_path",
    "write_sidecar",
]
