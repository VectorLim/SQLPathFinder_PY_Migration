from __future__ import annotations

import json
from pathlib import Path
from typing import Any, Literal

from pydantic import BaseModel, ConfigDict, Field, PrivateAttr, ValidationError

from vg2c_ui.services.atomic_io import atomic_write_text

SIDECAR_VERSION = 4
LEGACY_SIDECAR_VERSIONS = (2, 3)


class SavedSemanticChange(BaseModel):
    model_config = ConfigDict(extra="forbid")

    binding_id: str
    value: Any
    symbol_id: str | None = None


class EditorSidecar(BaseModel):
    """Persistence-only state for validated semantic binding edits."""

    model_config = ConfigDict(extra="forbid")

    schema_version: Literal[4] = SIDECAR_VERSION
    source_hash: str
    last_generated_hash: str | None = None
    value_changes: list[SavedSemanticChange] = Field(default_factory=list)
    _legacy_version: int | None = PrivateAttr(default=None)

    @property
    def legacy_version(self) -> int | None:
        return self._legacy_version


class InvalidSidecar(ValueError):
    pass


def sidecar_path(output_path: Path) -> Path:
    return output_path.with_suffix(".vg2c-ui.json")


def read_sidecar(output_path: Path) -> EditorSidecar | None:
    path = sidecar_path(output_path)
    if not path.exists():
        return None
    try:
        payload = json.loads(path.read_text(encoding="utf-8"))
        if not isinstance(payload, dict):
            raise InvalidSidecar("Saved editor state must be a JSON object.")
        version = payload.get("schema_version")
        legacy_version = version if version in LEGACY_SIDECAR_VERSIONS else None
        if legacy_version is not None:
            payload = _upgrade_legacy(payload)
        elif version != SIDECAR_VERSION:
            raise InvalidSidecar(
                f"unsupported sidecar version {version}; expected {SIDECAR_VERSION}"
            )
        sidecar = EditorSidecar.model_validate(payload)
        sidecar._legacy_version = legacy_version
    except InvalidSidecar:
        raise
    except (OSError, ValidationError, json.JSONDecodeError, TypeError, KeyError) as exc:
        raise InvalidSidecar(
            "Saved editor changes could not be read or validated. Original files are preserved."
        ) from exc
    return sidecar


def _upgrade_legacy(payload: dict[str, Any]) -> dict[str, Any]:
    """Normalize v2/v3 for reads while leaving the original file untouched."""
    version = payload["schema_version"]
    return {
        "schema_version": SIDECAR_VERSION,
        "source_hash": payload["source_hash"],
        "last_generated_hash": payload["output_hash"],
        "value_changes": [
        {
            "binding_id": item["parameter_id"] if version == 2 else item["binding_id"],
            "value": item.get("value"),
            "symbol_id": None,
        }
        for item in payload.get("changes", [])
        ],
    }


def write_sidecar(output_path: Path, sidecar: EditorSidecar) -> Path:
    path = sidecar_path(output_path)
    atomic_write_text(path, sidecar.model_dump_json(indent=2) + "\n")
    return path


__all__ = [
    "EditorSidecar",
    "InvalidSidecar",
    "LEGACY_SIDECAR_VERSIONS",
    "SIDECAR_VERSION",
    "SavedSemanticChange",
    "read_sidecar",
    "sidecar_path",
    "write_sidecar",
]
