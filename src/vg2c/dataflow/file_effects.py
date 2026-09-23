from __future__ import annotations

import hashlib
import re
from dataclasses import dataclass, replace
from pathlib import Path
from typing import Any, Literal

from vg2c.emitter.models import EmittedInvocation
from vg2c.utility_metadata import FileEffectDefinition, FileEffectKind, PathBase


@dataclass(frozen=True, slots=True)
class FileEndpoint:
    id: str
    binding_id: str | None
    path: str | None
    expression: str | None
    path_base: PathBase
    phase: Literal["prior", "next", "deleted"]
    state_ids: tuple[str, ...] = ()
    status: Literal["known", "dynamic", "external", "missing", "possible"] = "known"
    file_resource_id: str | None = None

@dataclass(frozen=True, slots=True)
class FileEffect:
    id: str
    operation_id: str
    step_id: str
    block_index: int
    scope_id: int
    order: int
    kind: FileEffectKind
    inputs: tuple[FileEndpoint, ...]
    outputs: tuple[FileEndpoint, ...]
    conditional: bool = False
    in_loop: bool = False
    reason: str | None = None
    dependency_ids: tuple[str, ...] = ()
    available_before: tuple[FileAvailability, ...] = ()


@dataclass(frozen=True, slots=True)
class FileAvailability:
    file_resource_id: str
    path: str
    status: Literal["guaranteed", "possible", "missing"]


@dataclass(frozen=True, slots=True)
class _State:
    id: str
    effect_id: str
    branches: tuple[tuple[int, int], ...]
    deleted: bool
    uncertain: bool
    file_resource_id: str | None
    path: str | None


def _portable_path_key(value: str) -> str:
    """Normalize VG2 paths using Windows separators even when tests run on POSIX."""
    text = value.replace("\\", "/")
    parts: list[str] = []
    for part in text.split("/"):
        if part in {"", "."}:
            if not parts and part == "":
                parts.append("")
            continue
        if part == ".." and parts and parts[-1] not in {"", ".."}:
            parts.pop()
        else:
            parts.append(part)
    normalized = "/".join(parts) or "."
    if text.startswith("//") and not normalized.startswith("//"):
        normalized = "/" + normalized
    return normalized.casefold()


def _is_absolute_artifact_path(value: str) -> bool:
    normalized = value.replace("\\", "/")
    return normalized.startswith("/") or bool(re.match(r"^[A-Za-z]:/", normalized))


def endpoint_keys(endpoint: FileEndpoint, output_path: Path) -> tuple[str, ...]:
    if endpoint.path is None:
        return ()
    raw = endpoint.path
    if _is_absolute_artifact_path(raw):
        return (_portable_path_key(raw),)

    relative = _portable_path_key(raw)
    working = f"working-directory:{relative}"
    script = _portable_path_key(str(output_path.parent / Path(raw.replace("\\", "/"))))
    if endpoint.path_base == "runtime-search":
        return working, script
    return (working,) if endpoint.path_base == "working-directory" else (script,)


def inventory_file_keys(path: str, workspace_root: Path) -> tuple[str, str]:
    """Match a physical workspace file to runtime and absolute endpoint keys."""
    relative = _portable_path_key(path)
    absolute = _portable_path_key(str(workspace_root / Path(path.replace("\\", "/"))))
    return f"working-directory:{relative}", absolute


def order_file_effects(
    effects: tuple[FileEffect, ...], scope_tree, output_path: Path
) -> tuple[FileEffect, ...]:
    effects = _attach_resource_ids(effects, output_path)
    branches_by_scope: dict[int, tuple[tuple[int, int], ...]] = {}

    def visit(node, branches=(), parent_id=0):
        if node.kind in {"if-branch", "else-branch"}:
            branches += ((parent_id, node.scope_id),)
        branches_by_scope[node.scope_id] = branches
        for child in node.children:
            visit(child, branches, node.scope_id)

    visit(scope_tree)
    states: dict[str, list[_State]] = {}
    ordered: list[FileEffect] = []

    for effect in effects:
        branches = branches_by_scope.get(effect.scope_id, ())
        current_branches = dict(branches)
        available_before = _available_before(states, current_branches)
        inputs: list[FileEndpoint] = []
        dependency_ids: set[str] = set()
        for endpoint in effect.inputs:
            candidates = [
                state
                for key in endpoint_keys(endpoint, output_path)
                for state in states.get(key, ())
                if all(
                    parent not in current_branches or current_branches[parent] == branch
                    for parent, branch in state.branches
                )
            ]
            if endpoint.status == "dynamic":
                status = "dynamic"
            elif not candidates:
                status = "external"
            elif all(state.deleted and not state.uncertain for state in candidates):
                status = "missing"
            elif (
                len(candidates) > 1
                or any(state.uncertain for state in candidates)
                or endpoint.path_base == "runtime-search"
            ):
                status = "possible"
            else:
                status = "known"
            inputs.append(
                replace(
                    endpoint,
                    state_ids=tuple(state.id for state in candidates),
                    status=status,
                )
            )
            dependency_ids.update(
                state.effect_id for state in candidates if not state.deleted
            )

        outputs = tuple(
            replace(endpoint, state_ids=(f"{endpoint.id}:state",))
            for endpoint in effect.outputs
        )
        for endpoint in (
            *(inputs if effect.kind in {"delete", "move"} else ()),
            *outputs,
        ):
            deleted = endpoint.phase != "next"
            state = _State(
                f"{endpoint.id}:{'deleted' if deleted else 'state'}",
                effect.id,
                branches,
                deleted,
                effect.conditional or effect.in_loop,
                endpoint.file_resource_id,
                endpoint.path,
            )
            for key in endpoint_keys(endpoint, output_path):
                previous = states.get(key, [])
                states[key] = (
                    [
                        item
                        for item in previous
                        if item.branches != branches or not item.uncertain
                    ]
                    if state.uncertain
                    else []
                ) + [state]
        ordered.append(
            replace(
                effect,
                inputs=tuple(inputs),
                outputs=outputs,
                dependency_ids=tuple(sorted(dependency_ids)),
                available_before=available_before,
            )
        )
    return tuple(ordered)


def _available_before(
    states: dict[str, list[_State]], current_branches: dict[int, int]
) -> tuple[FileAvailability, ...]:
    by_resource: dict[str, dict[str, _State]] = {}
    for candidates in states.values():
        for state in candidates:
            if state.file_resource_id is None or state.path is None:
                continue
            if not all(
                parent not in current_branches or current_branches[parent] == branch
                for parent, branch in state.branches
            ):
                continue
            by_resource.setdefault(state.file_resource_id, {})[state.id] = state
    available = []
    for resource_id, candidates in sorted(by_resource.items()):
        members = list(candidates.values())
        if all(member.deleted and not member.uncertain for member in members):
            status = "missing"
        elif len(members) == 1 and not members[0].deleted and not members[0].uncertain:
            status = "guaranteed"
        else:
            status = "possible"
        available.append(FileAvailability(resource_id, members[-1].path, status))
    return tuple(available)


def _attach_resource_ids(
    effects: tuple[FileEffect, ...], output_path: Path
) -> tuple[FileEffect, ...]:
    """Give equivalent endpoint spellings one resource identity using state keys."""
    parents: dict[str, str] = {}

    def root(key: str) -> str:
        parents.setdefault(key, key)
        while parents[key] != key:
            key = parents[key]
        return key

    for effect in effects:
        for endpoint in (*effect.inputs, *effect.outputs):
            keys = endpoint_keys(endpoint, output_path)
            if not keys:
                continue
            representative = min(root(key) for key in keys)
            for key in keys:
                parents[root(key)] = representative

    members: dict[str, list[str]] = {}
    for key in parents:
        members.setdefault(root(key), []).append(key)
    ids = {
        key: "file:" + hashlib.sha256(min(group).encode()).hexdigest()[:16]
        for group in members.values()
        for key in group
    }

    def with_id(endpoint: FileEndpoint) -> FileEndpoint:
        keys = endpoint_keys(endpoint, output_path)
        identity = ids[keys[0]] if keys else f"file:dynamic:{endpoint.id}"
        return replace(endpoint, file_resource_id=identity)

    return tuple(
        replace(
            effect,
            inputs=tuple(with_id(endpoint) for endpoint in effect.inputs),
            outputs=tuple(with_id(endpoint) for endpoint in effect.outputs),
        )
        for effect in effects
    )


def bind_file_effects(
    invocation: EmittedInvocation,
    values: dict[str, Any],
    *,
    step_id: str,
    block_index: int,
    scope_id: int,
    order: int,
    conditional: bool = False,
    in_loop: bool = False,
) -> tuple[FileEffect, ...]:
    definitions = invocation.operation.file_effects
    if definitions is None:
        definitions = (
            FileEffectDefinition(
                "unknown", "unknown", reason="File effects are not declared."
            ),
        )
    parameters = {parameter.name: parameter for parameter in invocation.parameters}
    effects: list[FileEffect] = []
    for definition in definitions:
        effect_id = f"{invocation.id}:effect:{definition.key}"

        def endpoints(names: tuple[str, ...], phase, base) -> tuple[FileEndpoint, ...]:
            bound: list[FileEndpoint] = []
            for name in names:
                parameter = parameters.get(name)
                if parameter is None:
                    bound.append(
                        FileEndpoint(
                            f"{effect_id}:{phase}:{name}",
                            None,
                            None,
                            name,
                            base,
                            phase,
                            status="dynamic",
                        )
                    )
                    continue
                value = values.get(parameter.id, parameter.value)
                if value is None and parameter.read_only_reason is None:
                    continue
                paths = value if isinstance(value, list) else [value]
                for index, path in enumerate(paths):
                    if (
                        definition.input_format == "table-binding"
                        and phase == "prior"
                        and isinstance(path, (tuple, list))
                        and len(path) == 2
                    ):
                        path = path[0]
                    known = isinstance(path, str) and bool(path.strip())
                    bound.append(
                        FileEndpoint(
                            f"{effect_id}:{phase}:{name}:{index}",
                            parameter.id,
                            path if known else None,
                            None if known else parameter.source,
                            base,
                            phase,
                            status="known" if known else "dynamic",
                        )
                    )
            return tuple(bound)

        inputs = endpoints(definition.inputs, "prior", definition.input_base)
        outputs = endpoints(definition.outputs, "next", definition.output_base)
        if definition.kind == "append":
            inputs += endpoints(definition.outputs, "prior", definition.output_base)
        kind = (
            "write"
            if definition.kind == "transform" and not inputs
            else definition.kind
        )
        effects.append(
            FileEffect(
                effect_id,
                invocation.id,
                step_id,
                block_index,
                scope_id,
                order + len(effects),
                kind,
                inputs,
                outputs,
                conditional,
                in_loop,
                definition.reason,
            )
        )
    return tuple(effects)
