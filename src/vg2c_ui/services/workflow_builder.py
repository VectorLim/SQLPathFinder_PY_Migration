from __future__ import annotations

import hashlib
from collections import defaultdict
from pathlib import Path

from vg2c import CompilationResult
from vg2c.kind import Kind
from vg2c.operands import ScopeNode as CompilerScopeNode
from vg2c_ui.domain.models import (
    CsvArtifactNode,
    Diagnostic,
    Position,
    ScopeNode,
    SourceSpan,
    StepNode,
    WorkflowDocument,
    WorkflowEdge,
    WorkflowLayout,
    WorkflowSidecar,
)
from vg2c_ui.services.python_document import parse_generated_python
from vg2c_ui.services.utility_catalog import UtilityCatalog, enrich_parameter

MAX_DIAGNOSTICS = 200


def build_workflow(
    result: CompilationResult,
    output_path: Path,
    generated_python: str,
    sidecar: WorkflowSidecar | None = None,
) -> WorkflowDocument:
    parsed = parse_generated_python(generated_python)
    catalog = UtilityCatalog()
    source_hash = _hash(result.input_path.read_bytes())
    output_hash = _hash(generated_python.encode("utf-8"))
    document_id = _stable_id("document", str(result.input_path.resolve()).casefold())
    block_to_step: dict[int, str] = {}
    steps: list[StepNode] = []

    parent_by_scope, scope_by_id, depth_by_scope = _scope_indexes(result.scope_tree)
    leaf_parent = {
        node.block_index: parent_by_scope.get(node.scope_id)
        for node in scope_by_id.values()
        if node.kind == "leaf" and node.block_index is not None
    }
    branch_by_scope = {
        scope_id: (
            "true"
            if scope.kind == "if-branch"
            else "false"
            if scope.kind == "else-branch"
            else None
        )
        for scope_id, scope in scope_by_id.items()
    }

    inputs_by_block: dict[int, set[str]] = defaultdict(set)
    outputs_by_block: dict[int, set[str]] = defaultdict(set)
    for producer in result.analyzed.producers:
        outputs_by_block[producer.block_index].add(producer.csv_path)
    for consumer in result.analyzed.consumers:
        inputs_by_block[consumer.block_index].add(consumer.csv_path)

    for function_name, block in result.function_to_block.items():
        function = parsed.steps.get(function_name)
        if function is None:
            continue
        step_id = _stable_id(
            "step",
            f"{result.input_path.as_posix().casefold()}:{block.index}:{block.kind.value}",
        )
        block_to_step[block.index] = step_id
        parent_scope = leaf_parent.get(block.index)
        primary = catalog.resolve(
            function.parameters[0].call_target if function.parameters else None,
            block.kind,
        )
        read_only = (
            block.kind in {Kind.PYTHON_EMBED, Kind.UNKNOWN}
            or primary.utility.fallback
        )
        parameters = []
        for parsed_parameter in function.parameters:
            method = catalog.resolve(parsed_parameter.call_target, block.kind)
            descriptor = parsed_parameter.descriptor.model_copy(deep=True)
            metadata = enrich_parameter(method, descriptor.name, descriptor.position)
            if metadata:
                descriptor = descriptor.model_copy(update=metadata)
            if method.utility.fallback:
                descriptor.editable = False
                descriptor.read_only_reason = "No registered utility method owns this value"
            parameters.append(descriptor)
        if read_only:
            for parameter in parameters:
                parameter.editable = False
                parameter.read_only_reason = f"{block.kind.value} blocks are read-only"
        steps.append(
            StepNode(
                id=step_id,
                function_name=function_name,
                block_index=block.index,
                source_span=SourceSpan(
                    file=str(block.span.file) if block.span.file else None,
                    start_line=block.span.start_line,
                    end_line=block.span.end_line,
                ),
                functional_kind=block.kind.value,
                display_label=block.resolved_options.lookup.get("PROMPT-TEXT")
                or block.kind.value.replace("_", " ").title(),
                icon_key=block.kind.value.lower().replace("_", "-"),
                description=(
                    function.description
                    if function.description != "No description provided"
                    else primary.utility.method_description
                    or primary.utility.description
                ),
                parameters=parameters,
                csv_inputs=sorted(inputs_by_block[block.index]),
                csv_outputs=sorted(outputs_by_block[block.index]),
                parent_scope_id=_scope_ui_id(parent_scope, scope_by_id),
                branch=branch_by_scope.get(parent_scope),
                validation_state="unsupported" if read_only else "valid",
                raw_code=function.raw_code if read_only else None,
                read_only=read_only,
                utility=primary.utility,
            )
        )

    scopes = _build_scopes(result.scope_tree, parent_by_scope)
    control_edges = _build_control_edges(result.scope_tree, block_to_step)
    data_edges, artifacts = _build_data_edges(result, block_to_step)
    diagnostics = [
        Diagnostic(
            level=_diagnostic_level(item.level),
            code=item.code,
            message=item.message,
            location=item.location,
        )
        for item in result.diagnostics[:MAX_DIAGNOSTICS]
    ]
    if len(result.diagnostics) > MAX_DIAGNOSTICS:
        diagnostics.append(
            Diagnostic(
                level="warning",
                code="diagnostics-truncated",
                message=f"Only the first {MAX_DIAGNOSTICS} compiler diagnostics are shown.",
            )
        )
    for edge in result.dataflow_edges:
        node_id = block_to_step.get(edge.consumer.block_index)
        if edge.producer is None:
            diagnostics.append(
                Diagnostic(
                    level="warning",
                    code="csv-input-missing-producer",
                    message=f"{edge.csv_path} has no producer in this workflow.",
                    node_id=node_id,
                )
            )
        elif not edge.order_ok:
            diagnostics.append(
                Diagnostic(
                    level="warning",
                    code="csv-producer-order",
                    message=(
                        f"{edge.csv_path} may be consumed before its producer "
                        f"({edge.scope_relation})."
                    ),
                    node_id=node_id,
                )
            )
    if sidecar and sidecar.output_hash != output_hash:
        diagnostics.append(
            Diagnostic(
                level="warning",
                code="output-changed",
                message=(
                    "Generated Python changed since the UI sidecar was saved; "
                    "layout was retained."
                ),
            )
        )
    if sidecar and sidecar.source_hash != source_hash:
        diagnostics.append(
            Diagnostic(
                level="warning",
                code="source-changed",
                message=(
                    "VG2 source changed since the UI sidecar was saved; "
                    "unresolved overrides were not applied."
                ),
            )
        )

    layout = sidecar.layout if sidecar else _initial_layout(
        steps, scopes, artifacts, depth_by_scope
    )
    return WorkflowDocument(
        id=document_id,
        source_path=str(result.input_path),
        output_path=str(output_path.resolve()),
        source_hash=source_hash,
        output_hash=output_hash,
        revision=sidecar.revision if sidecar else 1,
        steps=sorted(steps, key=lambda item: item.block_index),
        scopes=scopes,
        artifacts=artifacts,
        control_edges=control_edges,
        data_edges=data_edges,
        diagnostics=diagnostics,
        layout=layout,
        overrides=sidecar.overrides if sidecar else [],
    )


def _scope_indexes(
    root: CompilerScopeNode,
) -> tuple[dict[int, int | None], dict[int, CompilerScopeNode], dict[int, int]]:
    parent_by_scope: dict[int, int | None] = {}
    scope_by_id: dict[int, CompilerScopeNode] = {}
    depth_by_scope: dict[int, int] = {}
    stack = [(root, None, 0)]
    while stack:
        node, parent, depth = stack.pop()
        parent_by_scope[node.scope_id] = parent
        scope_by_id[node.scope_id] = node
        depth_by_scope[node.scope_id] = depth
        for child in reversed(node.children):
            stack.append((child, node.scope_id, depth + 1))
    return parent_by_scope, scope_by_id, depth_by_scope


def _scope_ui_id(
    scope_id: int | None, scope_by_id: dict[int, CompilerScopeNode]
) -> str | None:
    if scope_id is None or scope_id not in scope_by_id:
        return None
    return None if scope_by_id[scope_id].kind == "program" else f"scope-{scope_id}"


def _build_scopes(
    root: CompilerScopeNode, parent_by_scope: dict[int, int | None]
) -> list[ScopeNode]:
    mapped = {
        "if": ("if", "Condition"),
        "if-branch": ("branch", "True"),
        "else-branch": ("branch", "False"),
        "loop": ("loop", "Loop"),
        "macro": ("loop", "Macro rows"),
    }
    _, scope_by_id, _ = _scope_indexes(root)
    scopes: list[ScopeNode] = []
    for scope in scope_by_id.values():
        if scope.kind not in mapped:
            continue
        node_kind, label = mapped[scope.kind]
        scopes.append(
            ScopeNode(
                id=f"scope-{scope.scope_id}",
                node_kind=node_kind,
                scope_kind=scope.kind,
                label=label,
                start_index=scope.start_index,
                end_index=scope.end_index,
                parent_scope_id=_scope_ui_id(parent_by_scope[scope.scope_id], scope_by_id),
            )
        )
    return sorted(scopes, key=lambda item: (item.start_index, item.id))


def _build_control_edges(
    root: CompilerScopeNode, block_to_step: dict[int, str]
) -> list[WorkflowEdge]:
    edges: list[WorkflowEdge] = []

    def entry(node: CompilerScopeNode) -> str | None:
        if node.kind == "leaf":
            return block_to_step.get(node.block_index or -1)
        if node.kind == "program":
            return next((value for child in node.children if (value := entry(child))), None)
        return f"scope-{node.scope_id}"

    def exits(node: CompilerScopeNode) -> list[str]:
        if node.kind == "leaf":
            item = block_to_step.get(node.block_index or -1)
            return [item] if item else []
        if node.kind == "if":
            result: list[str] = []
            for child in node.children:
                result.extend(exits(child))
            return result or [f"scope-{node.scope_id}"]
        if node.kind in {"loop", "macro"}:
            return [f"scope-{node.scope_id}"]
        return exits(node.children[-1]) if node.children else [f"scope-{node.scope_id}"]

    def add(source: str, target: str, label: str | None = None) -> None:
        edge_id = f"control-{len(edges)}"
        edges.append(
            WorkflowEdge(id=edge_id, source=source, target=target, kind="control", label=label)
        )

    def visit(node: CompilerScopeNode) -> None:
        visible_parent = None if node.kind == "program" else f"scope-{node.scope_id}"
        if visible_parent:
            for child in node.children:
                target = entry(child)
                if target:
                    label = (
                        "True"
                        if child.kind == "if-branch"
                        else "False"
                        if child.kind == "else-branch"
                        else None
                    )
                    add(visible_parent, target, label)
        if node.kind != "if":
            for previous, current in zip(node.children, node.children[1:], strict=False):
                target = entry(current)
                if target:
                    for source in exits(previous):
                        add(source, target)
        for child in node.children:
            visit(child)
        if node.kind in {"loop", "macro"} and node.children:
            for source in exits(node.children[-1]):
                if source != visible_parent:
                    add(source, visible_parent or "", "repeat")

    visit(root)
    unique: dict[tuple[str, str, str | None], WorkflowEdge] = {}
    for edge in edges:
        if edge.source and edge.target and edge.source != edge.target:
            unique.setdefault((edge.source, edge.target, edge.label), edge)
    return [
        edge.model_copy(update={"id": f"control-{i}"})
        for i, edge in enumerate(unique.values())
    ]


def _build_data_edges(
    result: CompilationResult, block_to_step: dict[int, str]
) -> tuple[list[WorkflowEdge], list[CsvArtifactNode]]:
    artifacts: dict[str, CsvArtifactNode] = {}
    edges: list[WorkflowEdge] = []
    for index, edge in enumerate(result.dataflow_edges):
        artifact_id = _stable_id("csv", edge.csv_path)
        artifacts.setdefault(
            artifact_id,
            CsvArtifactNode(
                id=artifact_id,
                path=edge.csv_path,
                label=Path(edge.csv_path).name,
                conditional=edge.producer.is_conditional if edge.producer else False,
                in_loop=edge.producer.is_in_loop if edge.producer else False,
            ),
        )
        consumer_id = block_to_step.get(edge.consumer.block_index)
        if consumer_id is None:
            continue
        producer_id = (
            block_to_step.get(edge.producer.block_index) if edge.producer is not None else None
        )
        if producer_id:
            edges.append(
                WorkflowEdge(
                    id=f"data-{index}-producer",
                    source=producer_id,
                    target=artifact_id,
                    kind="data",
                    label=edge.csv_path,
                    dashed=True,
                    valid=edge.order_ok,
                    scope_relation=edge.scope_relation,
                )
            )
        edges.append(
            WorkflowEdge(
                id=f"data-{index}-consumer",
                source=artifact_id,
                target=consumer_id,
                kind="data",
                label=edge.csv_path,
                dashed=True,
                valid=edge.order_ok,
                scope_relation=edge.scope_relation,
            )
        )
    return edges, sorted(artifacts.values(), key=lambda item: item.path)


def _initial_layout(
    steps: list[StepNode],
    scopes: list[ScopeNode],
    artifacts: list[CsvArtifactNode],
    depth_by_scope: dict[int, int],
) -> WorkflowLayout:
    positions: dict[str, Position] = {}
    def order(item: StepNode | ScopeNode) -> tuple[int, str]:
        return (item.block_index if isinstance(item, StepNode) else item.start_index, item.id)

    ordered = sorted([*steps, *scopes], key=order)
    for row, item in enumerate(ordered):
        parent = item.parent_scope_id
        depth = depth_by_scope.get(int(parent.split("-")[-1]), 0) if parent else 0
        positions[item.id] = Position(x=80 + depth * 260, y=60 + row * 150)
    for row, artifact in enumerate(artifacts):
        positions[artifact.id] = Position(x=-260, y=60 + row * 120)
    return WorkflowLayout(positions=positions)


def _stable_id(prefix: str, value: str) -> str:
    return f"{prefix}-{hashlib.sha256(value.encode('utf-8')).hexdigest()[:16]}"


def _hash(value: bytes) -> str:
    return hashlib.sha256(value).hexdigest()


def _diagnostic_level(value: str) -> str:
    return value if value in {"info", "warning", "error"} else "error"


__all__ = ["MAX_DIAGNOSTICS", "build_workflow"]
