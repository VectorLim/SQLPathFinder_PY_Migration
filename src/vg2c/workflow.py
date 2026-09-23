from __future__ import annotations

from collections.abc import Iterable
from dataclasses import dataclass, replace
from pathlib import Path
from typing import Literal

from vg2c.compilation import CompilationResult
from vg2c.dataflow.file_effects import (
    FileEffect,
    FileEndpoint,
    bind_file_effects,
    endpoint_keys,
    order_file_effects,
)
from vg2c.editing import (
    ChangeProjection,
    ChangeValidationError,
    SemanticChange,
    project_changes,
)
from vg2c.semantics import (
    EditableBinding,
    OperationReference,
    Symbol,
    WorkflowOperation,
    build_semantic_model,
)
from vg2c.utilities._emit_helpers import scan_sql_get_csv_list_calls
from vg2c.utilities.html_report import HtmlReport


@dataclass(frozen=True, slots=True)
class FileResource:
    id: str
    path: str | None
    status: Literal["workspace", "generated", "external", "missing", "dynamic", "possible"]
    producer_refs: tuple[OperationReference, ...] = ()
    consumer_refs: tuple[OperationReference, ...] = ()
    lifecycle_refs: tuple[OperationReference, ...] = ()


@dataclass(frozen=True, slots=True)
class WorkflowProjection:
    changes: ChangeProjection
    effects: tuple[FileEffect, ...]
    operations: tuple[WorkflowOperation, ...] = ()
    bindings: tuple[EditableBinding, ...] = ()
    symbols: tuple[Symbol, ...] = ()
    files: tuple[FileResource, ...] = ()


@dataclass(frozen=True, slots=True)
class WorkflowDocument:
    document_id: str
    output_path: Path
    workflow: WorkflowProjection


@dataclass(frozen=True, slots=True)
class WorkflowLink:
    artifact: str
    producer_document_id: str
    producer_operation_id: str
    producer_step_id: str
    consumer_document_id: str
    consumer_operation_id: str
    consumer_step_id: str
    consumer_endpoint_id: str


@dataclass(frozen=True, slots=True)
class WorkflowIssue:
    code: Literal["BROKEN_DEPENDENCY", "DUPLICATE_OUTPUT"]
    document_id: str
    step_id: str
    artifact: str
    message: str
    related_document_id: str | None = None
    related_step_id: str | None = None


def _live_producers(
    documents: Iterable[WorkflowDocument],
) -> dict[str, list[tuple[str, FileEffect]]]:
    producers: dict[str, list[tuple[str, FileEffect]]] = {}
    for document in documents:
        live: dict[str, list[FileEffect]] = {}
        for effect in document.workflow.effects:
            if (
                effect.kind in {"delete", "move"}
                and not effect.conditional
                and not effect.in_loop
            ):
                for endpoint in effect.inputs:
                    for key in endpoint_keys(endpoint, document.output_path):
                        live.pop(key, None)
            for endpoint in effect.outputs:
                for key in endpoint_keys(endpoint, document.output_path):
                    live[key] = (
                        live.get(key, [])
                        if effect.conditional or effect.in_loop
                        else []
                    ) + [effect]
        for key, effects in live.items():
            producers.setdefault(key, []).extend(
                (document.document_id, effect) for effect in effects
            )
    return producers


def workspace_links(documents: Iterable[WorkflowDocument]) -> tuple[WorkflowLink, ...]:
    documents = tuple(documents)
    producers = _live_producers(documents)
    links: list[WorkflowLink] = []
    for document in documents:
        for effect in document.workflow.effects:
            for endpoint in effect.inputs:
                if (
                    endpoint.status not in {"external", "possible"}
                    or endpoint.state_ids
                ):
                    continue
                for key in endpoint_keys(endpoint, document.output_path):
                    for producer_document, producer in producers.get(key, ()):
                        if producer_document == document.document_id:
                            continue
                        links.append(
                            WorkflowLink(
                                endpoint.path or "",
                                producer_document,
                                producer.operation_id,
                                producer.step_id,
                                document.document_id,
                                effect.operation_id,
                                effect.step_id,
                                endpoint.id,
                            )
                        )
    return tuple(dict.fromkeys(links))


def workspace_issues(
    documents: Iterable[WorkflowDocument], baseline: Iterable[WorkflowDocument]
) -> tuple[WorkflowIssue, ...]:
    documents = tuple(documents)
    issues: list[WorkflowIssue] = []
    for producers in _live_producers(documents).values():
        if len({document_id for document_id, _ in producers}) < 2:
            continue
        for document_id, effect in producers:
            related_document, related_effect = next(
                item for item in producers if item[0] != document_id
            )
            path = next(
                (endpoint.path for endpoint in effect.outputs if endpoint.path),
                "Unknown path",
            )
            issues.append(
                WorkflowIssue(
                    "DUPLICATE_OUTPUT",
                    document_id,
                    effect.step_id,
                    path,
                    f"{path} may be written by more than one open document.",
                    related_document,
                    related_effect.step_id,
                )
            )
    linked_consumers = {
        (link.consumer_document_id, link.consumer_endpoint_id)
        for link in workspace_links(documents)
    }
    current_inputs = {
        (document.document_id, endpoint.id)
        for document in documents
        for effect in document.workflow.effects
        for endpoint in effect.inputs
    }
    for link in workspace_links(baseline):
        identity = (link.consumer_document_id, link.consumer_endpoint_id)
        if identity not in current_inputs or identity in linked_consumers:
            continue
        issues.append(
            WorkflowIssue(
                "BROKEN_DEPENDENCY",
                link.consumer_document_id,
                link.consumer_step_id,
                link.artifact,
                f"Previously linked input {link.artifact} no longer has an available workspace producer.",
                link.producer_document_id,
                link.producer_step_id,
            )
        )
    return tuple(dict.fromkeys(issues))


def project_workflow(
    result: CompilationResult,
    changes: Iterable[SemanticChange] = (),
    *,
    output_path: Path | None = None,
) -> WorkflowProjection:
    """Project edits, semantic operations, symbols, and file flow from one authority."""
    projection = project_changes(result, changes)
    if not projection.valid:
        raise ChangeValidationError(projection.issues)

    values = projection.effective_values
    semantic = build_semantic_model(result, values)
    flags = _scope_flags(result)
    invocations = {
        invocation.id: (step, invocation)
        for step in result.emitted.steps
        for invocation in step.invocations
    }
    steps_by_block = {step.block_index: step for step in result.emitted.steps}
    effects: list[FileEffect] = []

    for operation in semantic.operations:
        conditional, in_loop = flags.get(operation.block_index, (False, False))
        step = steps_by_block.get(operation.block_index)
        step_id = step.function_name if step else operation.id
        bound: list[FileEffect] = []

        emitted = invocations.get(operation.id)
        if emitted is not None:
            emitted_step, invocation = emitted
            bound.extend(
                bind_file_effects(
                    invocation,
                    values,
                    step_id=emitted_step.function_name,
                    block_index=operation.block_index,
                    scope_id=_block_scope_id(result, operation.block_index),
                    order=0,
                    conditional=conditional,
                    in_loop=in_loop,
                )
            )
            bound.extend(
                _html_effects(
                    operation,
                    step_id=emitted_step.function_name,
                    scope_id=_block_scope_id(result, operation.block_index),
                    conditional=conditional,
                    in_loop=in_loop,
                )
            )
        elif operation.kind == "macro-loop":
            bound.extend(
                _macro_effects(operation, step_id, _block_scope_id(result, operation.block_index), conditional, in_loop)
            )
        elif operation.kind == "chunk-loop":
            bound.extend(
                _chunk_effects(operation, step_id, _block_scope_id(result, operation.block_index), conditional, in_loop)
            )
        elif operation.kind == "check-row-count":
            bound.extend(
                _row_count_effects(operation, step_id, _block_scope_id(result, operation.block_index), conditional, in_loop)
            )
        elif operation.kind in {"embedded-python", "unsupported"}:
            bound.append(
                FileEffect(
                    f"{operation.id}:effect:unknown",
                    operation.id,
                    step_id,
                    operation.block_index,
                    _block_scope_id(result, operation.block_index),
                    0,
                    "unknown",
                    (),
                    (),
                    conditional,
                    in_loop,
                    (
                        "Embedded Python may perform arbitrary file operations."
                        if operation.kind == "embedded-python"
                        else "No declared file effects are available for this operation."
                    ),
                )
            )

        # SQL_Get_CSV_List is source syntax embedded inside SQL, not a runtime utility
        # invocation. Reuse the compiler's existing scanner and represent the read explicitly.
        block = next(
            (item for item in result.resolved.blocks if item.index == operation.block_index),
            None,
        )
        if block is not None and operation.kind == "ctx.run_query":
            for index, call in enumerate(scan_sql_get_csv_list_calls(block.resolved_body)):
                effect_id = f"{operation.id}:effect:sql-get-csv-list:{index}"
                binding_id = f"{operation.id}:sql-file-list:{index}"
                file_binding = next(
                    (item for item in operation.bindings if item.id == binding_id), None
                )
                bound.append(
                    FileEffect(
                        effect_id,
                        operation.id,
                        step_id,
                        operation.block_index,
                        block.scope_id,
                        len(bound),
                        "read",
                        (
                            FileEndpoint(
                                f"{effect_id}:prior:path:0",
                                binding_id if file_binding else None,
                                file_binding.value if file_binding else call.source_path,
                                None,
                                "runtime-search",
                                "prior",
                            ),
                        ),
                        (),
                        conditional,
                        in_loop,
                        "SQL_Get_CSV_List reads this file while resolving SQL input.",
                    )
                )

        start_order = len(effects)
        effects.extend(
            replace(effect, order=start_order + index)
            for index, effect in enumerate(bound)
        )

    ordered = order_file_effects(
        tuple(effects),
        result.resolved.scope_tree,
        output_path or result.input_path.with_suffix(".py"),
    )
    return WorkflowProjection(
        changes=projection,
        operations=semantic.operations,
        bindings=semantic.bindings,
        symbols=semantic.symbols,
        effects=ordered,
        files=file_resources(ordered),
    )


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


def _scope_flags(result: CompilationResult) -> dict[int, tuple[bool, bool]]:
    flags: dict[int, tuple[bool, bool]] = {}

    def visit(node, conditional=False, in_loop=False):
        conditional = conditional or node.kind in {"if-branch", "else-branch"}
        in_loop = in_loop or node.kind in {"macro", "loop"}
        flags[node.scope_id] = conditional, in_loop
        for child in node.children:
            visit(child, conditional, in_loop)

    visit(result.resolved.scope_tree)
    return flags


def _block_scope_id(result: CompilationResult, block_index: int) -> int:
    block = next((item for item in result.resolved.blocks if item.index == block_index), None)
    return block.scope_id if block is not None else 0


__all__ = [
    "FileResource",
    "WorkflowDocument",
    "WorkflowIssue",
    "WorkflowLink",
    "WorkflowProjection",
    "file_resources",
    "project_workflow",
    "workspace_issues",
    "workspace_links",
]
