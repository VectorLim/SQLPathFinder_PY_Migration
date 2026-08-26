from __future__ import annotations

import json
from pathlib import Path

from pydantic import ValidationError

from vg2c_ui.domain.models import SCHEMA_VERSION, WorkflowSidecar
from vg2c_ui.services.atomic_io import atomic_write_text


class InvalidSidecar(ValueError):
    pass


def sidecar_path(output_path: Path) -> Path:
    return output_path.with_suffix(".vg2c-ui.json")


def read_sidecar(output_path: Path) -> WorkflowSidecar | None:
    path = sidecar_path(output_path)
    if not path.exists():
        return None
    try:
        sidecar = WorkflowSidecar.model_validate_json(path.read_text(encoding="utf-8"))
    except (OSError, ValidationError, json.JSONDecodeError) as exc:
        raise InvalidSidecar(f"invalid sidecar {path}: {exc}") from exc
    if sidecar.schema_version != SCHEMA_VERSION:
        raise InvalidSidecar(
            f"unsupported sidecar version {sidecar.schema_version}; expected {SCHEMA_VERSION}"
        )
    return sidecar


def write_sidecar(output_path: Path, sidecar: WorkflowSidecar) -> Path:
    path = sidecar_path(output_path)
    payload = sidecar.model_dump_json(indent=2) + "\n"
    atomic_write_text(path, payload)
    return path


__all__ = ["InvalidSidecar", "read_sidecar", "sidecar_path", "write_sidecar"]
