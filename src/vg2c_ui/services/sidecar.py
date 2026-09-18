from __future__ import annotations

import json
from pathlib import Path
from typing import Any

from pydantic import BaseModel, Field, ValidationError

from vg2c_ui.services.atomic_io import atomic_write_text

SIDECAR_VERSION = 3
LEGACY_SIDECAR_VERSION = 2


class SavedSemanticChange(BaseModel):
    binding_id: str
    value: Any

    @property
    def parameter_id(self) -> str:
        """Compatibility alias for pre-v5 callers."""
        return self.binding_id


# Import compatibility only; canonical v3 serialization uses SavedSemanticChange.
SavedParameterChange = SavedSemanticChange


class EditorSidecar(BaseModel):
    """Persistence-only state for validated semantic binding edits."""

    schema_version: int = SIDECAR_VERSION
    source_hash: str
    output_hash: str
    changes: list[SavedSemanticChange] = Field(default_factory=list)


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
        version = payload.get("schema_version")
        if version == LEGACY_SIDECAR_VERSION:
            payload = _upgrade_v2(payload)
        elif version != SIDECAR_VERSION:
            raise InvalidSidecar(
                f"unsupported sidecar version {version}; expected {SIDECAR_VERSION}"
            )
        sidecar = EditorSidecar.model_validate(payload)
    except InvalidSidecar:
        raise
    except (OSError, ValidationError, json.JSONDecodeError, TypeError, KeyError) as exc:
        raise InvalidSidecar(
            "Saved editor changes could not be read or validated. Original files are preserved."
        ) from exc
    return sidecar


def _upgrade_v2(payload: dict[str, Any]) -> dict[str, Any]:
    """Upgrade stable v2 parameter ids directly into generalized v3 binding ids."""
    upgraded = dict(payload)
    upgraded["schema_version"] = SIDECAR_VERSION
    upgraded["changes"] = [
        {
            "binding_id": item["parameter_id"],
            "value": item.get("value"),
        }
        for item in payload.get("changes", [])
    ]
    return upgraded


def write_sidecar(output_path: Path, sidecar: EditorSidecar) -> Path:
    path = sidecar_path(output_path)
    atomic_write_text(path, sidecar.model_dump_json(indent=2) + "\n")
    return path


__all__ = [
    "EditorSidecar",
    "InvalidSidecar",
    "LEGACY_SIDECAR_VERSION",
    "SIDECAR_VERSION",
    "SavedParameterChange",
    "SavedSemanticChange",
    "read_sidecar",
    "sidecar_path",
    "write_sidecar",
]
