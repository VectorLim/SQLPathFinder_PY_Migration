"""Projection of semantic operations into file effects and file resources."""

from __future__ import annotations

from collections.abc import Iterable
from typing import Literal

from vg2c.compilation import CompilationResult
from vg2c.dataflow.file_effects import FileEffect, FileEndpoint
from vg2c.semantic_models import EditableBinding, OperationReference, WorkflowOperation
from vg2c.utilities.html_report import HtmlReport
from vg2c.workflow_models import DocumentDiagnostic, FileResource

def _document_diagnostics(
    result: CompilationResult, effects: tuple[FileEffect, ...]
) -> tuple[DocumentDiagnostic, ...]:
    diagnostics = [
        DocumentDiagnostic(
            item.level,
            item.code,
            item.message,
            item.location,
        )
        for item in result.diagnostics
    ]
    for effect in effects:
        for endpoint in effect.inputs:
            if endpoint.status not in {"external", "missing"}:
                continue
            diagnostics.append(
                DocumentDiagnostic(
                    level="warning" if endpoint.status == "missing" else "info",
                    code=(
                        "file-state-missing"
                        if endpoint.status == "missing"
                        else "external-file-input"
                    ),
                    message=(
                        f"{endpoint.path}: unavailable after deletion/move."
                        if endpoint.status == "missing"
                        else f"{endpoint.path}: external input."
                    ),
                    node_id=effect.operation_id,
                )
            )
    return tuple(diagnostics)


def file_resources(effects: Iterable[FileEffect]) -> tuple[FileResource, ...]:
    grouped: dict[str, list[tuple[FileEffect, FileEndpoint]]] = {}
    for effect in effects:
        for endpoint in (*effect.inputs, *effect.outputs):
            if endpoint.file_resource_id is None:
                raise ValueError("File resource identity must be assigned by order_file_effects.")
            key = endpoint.file_resource_id
            grouped.setdefault(key, []).append((effect, endpoint))

    resources: list[FileResource] = []
    for resource_id, items in sorted(grouped.items()):
        known_path = next((endpoint.path for _, endpoint in items if endpoint.path), None)
        producers = tuple(
            dict.fromkeys(
                OperationReference(effect.operation_id, endpoint.binding_id)
                for effect, endpoint in items
                if endpoint.phase == "next"
            )
        )
        consumers = tuple(
            dict.fromkeys(
                OperationReference(effect.operation_id, endpoint.binding_id)
                for effect, endpoint in items
                if endpoint.phase != "next"
            )
        )
        lifecycle = tuple(
            dict.fromkeys(
                OperationReference(effect.operation_id, endpoint.binding_id)
                for effect, endpoint in items
                if effect.kind in {"copy", "move", "append", "delete"}
            )
        )
        statuses = {endpoint.status for _, endpoint in items}
        if known_path is None:
            status = "dynamic"
        elif "missing" in statuses:
            status = "missing"
        elif "possible" in statuses:
            status = "possible"
        elif producers:
            status = "generated"
        elif "external" in statuses:
            status = "external"
        else:
            status = "workspace"
        resources.append(
            FileResource(
                id=resource_id,
                path=known_path,
                status=status,
                producer_refs=producers,
                consumer_refs=consumers,
                lifecycle_refs=lifecycle,
            )
        )
    return tuple(resources)


def _macro_effects(
    operation: WorkflowOperation,
    step_id: str,
    scope_id: int,
    conditional: bool,
    in_loop: bool,
) -> tuple[FileEffect, ...]:
    binding = _binding(operation, "csv_path")
    if binding is None or not binding.value:
        return ()
    effect_id = f"{operation.id}:effect:macro-input"
    return (
        FileEffect(
            effect_id,
            operation.id,
            step_id,
            operation.block_index,
            scope_id,
            0,
            "read",
            (
                FileEndpoint(
                    f"{effect_id}:prior:csv_path:0",
                    binding.id,
                    binding.value if isinstance(binding.value, str) else None,
                    None if isinstance(binding.value, str) else repr(binding.value),
                    "runtime-search",
                    "prior",
                    status="known" if isinstance(binding.value, str) else "dynamic",
                ),
            ),
            (),
            conditional,
            in_loop,
            "Macro rows are loaded from this file.",
        ),
    )


def _chunk_effects(
    operation: WorkflowOperation,
    step_id: str,
    scope_id: int,
    conditional: bool,
    in_loop: bool,
) -> tuple[FileEffect, ...]:
    source = _binding(operation, "input_csv_path")
    target = _binding(operation, "chunk_csv_path")
    effect_id = f"{operation.id}:effect:chunks"
    inputs = _binding_endpoints(effect_id, source, "prior", "runtime-search")
    outputs = _binding_endpoints(effect_id, target, "next", "script-directory")
    return (
        FileEffect(
            effect_id,
            operation.id,
            step_id,
            operation.block_index,
            scope_id,
            0,
            "transform",
            inputs,
            outputs,
            conditional,
            True,
            "Each iteration reads the input and materializes the current chunk file.",
        ),
    )


def _row_count_effects(
    operation: WorkflowOperation,
    step_id: str,
    scope_id: int,
    conditional: bool,
    in_loop: bool,
) -> tuple[FileEffect, ...]:
    source = _binding(operation, "path")
    effect_id = f"{operation.id}:effect:row-count"
    return (
        FileEffect(
            effect_id,
            operation.id,
            step_id,
            operation.block_index,
            scope_id,
            0,
            "observe",
            _binding_endpoints(effect_id, source, "prior", "runtime-search"),
            (),
            conditional,
            in_loop,
            "Row count observes this input file and stores the count in a symbol.",
        ),
    )


def _html_effects(
    operation: WorkflowOperation,
    *,
    step_id: str,
    scope_id: int,
    conditional: bool,
    in_loop: bool,
) -> tuple[FileEffect, ...]:
    template = _binding(operation, "template")
    if template is None or not isinstance(template.value, str):
        return ()

    effects: list[FileEffect] = []
    if operation.kind == "html_report.defer":
        options = HtmlReport._parse_options(template.value)
        raw_inputs = options.get("INPUT-FILE", ())
        inputs = raw_inputs if isinstance(raw_inputs, list) else [raw_inputs]
        paths = [item for item in inputs if isinstance(item, str) and item]
        if paths:
            effect_id = f"{operation.id}:effect:html-inputs"
            effects.append(
                FileEffect(
                    effect_id,
                    operation.id,
                    step_id,
                    operation.block_index,
                    scope_id,
                    0,
                    "read",
                    tuple(
                        FileEndpoint(
                            f"{effect_id}:prior:input:{index}",
                            template.id,
                            path,
                            None,
                            "runtime-search",
                            "prior",
                        )
                        for index, path in enumerate(paths)
                    ),
                    (),
                    conditional,
                    in_loop,
                    "Deferred report rows are read from these input files.",
                )
            )

    if operation.kind == "html_report.layout":
        directives, _ = HtmlReport._split_layout(template.value)
        output = directives.get("FILE")
        effect_id = f"{operation.id}:effect:html-output"
        if output:
            outputs = (
                FileEndpoint(
                    f"{effect_id}:next:file:0",
                    template.id,
                    output,
                    None,
                    "script-directory",
                    "next",
                ),
            )
        else:
            outputs = (
                FileEndpoint(
                    f"{effect_id}:next:file:0",
                    template.id,
                    None,
                    "HTML output resolved from deferred report state",
                    "script-directory",
                    "next",
                    status="dynamic",
                ),
            )
        effects.append(
            FileEffect(
                effect_id,
                operation.id,
                step_id,
                operation.block_index,
                scope_id,
                len(effects),
                "write",
                (),
                outputs,
                conditional,
                in_loop,
                "HTML layout writes the resolved report output.",
            )
        )
    return tuple(effects)


def _binding_endpoints(
    effect_id: str,
    binding: EditableBinding | None,
    phase: Literal["prior", "next", "deleted"],
    base,
) -> tuple[FileEndpoint, ...]:
    if binding is None:
        return ()
    values = binding.value if isinstance(binding.value, list) else [binding.value]
    endpoints: list[FileEndpoint] = []
    for index, value in enumerate(values):
        known = isinstance(value, str) and bool(value.strip())
        endpoints.append(
            FileEndpoint(
                f"{effect_id}:{phase}:{binding.name}:{index}",
                binding.id,
                value if known else None,
                None if known else repr(value),
                base,
                phase,
                status="known" if known else "dynamic",
            )
        )
    return tuple(endpoints)


def _binding(operation: WorkflowOperation, name: str) -> EditableBinding | None:
    return next((item for item in operation.bindings if item.name == name), None)
