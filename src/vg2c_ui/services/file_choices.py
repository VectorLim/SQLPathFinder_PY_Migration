from __future__ import annotations

from collections.abc import Iterable
from pathlib import Path

from vg2c.dataflow.file_effects import (
    FileEffect,
    FileEndpoint,
    endpoint_keys,
    inventory_file_keys,
)


def _server_path(
    endpoint: FileEndpoint, output_path: Path, workspace_root: Path
) -> str | None:
    if endpoint.path is None:
        return None
    path = Path(endpoint.path.replace("\\", "/"))
    base = output_path.parent if endpoint.path_base == "script-directory" else workspace_root
    candidate = (path if path.is_absolute() else base / path).resolve()
    if candidate != workspace_root and workspace_root not in candidate.parents:
        return None
    return candidate.relative_to(workspace_root).as_posix()


def file_choices_by_operation(
    effects: Iterable[FileEffect],
    inventory_paths: Iterable[str],
    *,
    output_path: Path,
    workspace_root: Path,
) -> dict[str, list[str]]:
    """Combine execution history with physical files for each operation's inputs."""
    effects = tuple(effects)
    workspace_root = workspace_root.resolve()
    resource_keys: dict[str, set[str]] = {}
    resource_endpoints: dict[str, FileEndpoint] = {}
    produced: set[str] = set()
    for effect in effects:
        produced.update(
            endpoint.file_resource_id
            for endpoint in effect.outputs
            if endpoint.file_resource_id
        )
        for endpoint in (*effect.inputs, *effect.outputs):
            if endpoint.file_resource_id and endpoint.path:
                resource_endpoints.setdefault(endpoint.file_resource_id, endpoint)
                resource_keys.setdefault(endpoint.file_resource_id, set()).update(
                    endpoint_keys(endpoint, output_path)
                )
    for effect in effects:
        for endpoint in effect.outputs:
            if endpoint.file_resource_id and endpoint.path:
                resource_endpoints[endpoint.file_resource_id] = endpoint

    physical = {
        path: set(inventory_file_keys(path, workspace_root))
        for path in inventory_paths
    }
    choices: dict[str, list[str]] = {}
    for effect in effects:
        if effect.operation_id in choices:
            continue
        status = {
            item.file_resource_id: item.status for item in effect.available_before
        }
        known = [
            path
            for item in effect.available_before
            if item.status == "guaranteed"
            if (endpoint := resource_endpoints.get(item.file_resource_id)) is not None
            if (path := _server_path(endpoint, output_path, workspace_root)) is not None
        ]
        for path, keys in physical.items():
            matched = {
                resource_id
                for resource_id, candidate_keys in resource_keys.items()
                if keys & candidate_keys
            }
            if any(
                status.get(item) in {"missing", "possible"}
                or (item in produced and status.get(item) != "guaranteed")
                for item in matched
            ):
                continue
            known.append(path)
        choices[effect.operation_id] = sorted(set(known), key=str.casefold)
    return choices
