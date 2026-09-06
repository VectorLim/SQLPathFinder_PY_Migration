from __future__ import annotations

from collections.abc import Iterable
from pathlib import Path
from typing import get_args

from vg2c import CompilationResult
from vg2c.dataflow import AnalyzedProgram
from vg2c.editing import ParameterChange
from vg2c.kind import Kind
from vg2c.operands import ScopeNode as CompilerScopeNode
from vg2c.sql_editor import FILTER_OPERATORS, JOIN_TYPES, SqlEditableModel, SqlLogicalConnector
from vg2c.sql_editor.capability import parameter_capabilities
from vg2c_ui.api.models import (
    ArtifactView,
    DiagnosticView,
    DocumentView,
    ParameterView,
    ScopeView,
    SourceSpanView,
    SqlEditCapabilitiesView,
    SqlJoinView,
    SqlModelView,
    SqlPredicateView,
    SqlSelectionView,
    SqlSourceView,
    SqlSpanView,
    StepView,
    UtilityView,
)

MAX_DIAGNOSTICS = 200


def document_view(
    result: CompilationResult,
    *,
    output_path: Path,
    source_hash: str,
    output_hash: str,
    revision: int,
    saved_changes: Iterable[ParameterChange] = (),
    synchronized: bool = True,
    read_only_reason: str | None = None,
) -> DocumentView:
    """Serialize compiler-owned semantics without re-discovering or re-inferring them."""
    values = {change.parameter_id: change.value for change in saved_changes}
    block_by_index = {block.index: block for block in result.resolved.blocks}
    step_id_by_block = {step.block_index: step.function_name for step in result.emitted.steps}
    parent_by_scope, scope_by_id = _scope_indexes(result.resolved.scope_tree)
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

    inputs_by_block: dict[int, set[str]] = {}
    outputs_by_block: dict[int, set[str]] = {}
    for artifact in result.analyzed.artifacts:
        for producer in artifact.producers:
            outputs_by_block.setdefault(producer.block_index, set()).add(artifact.path)
        for consumer in artifact.consumers:
            inputs_by_block.setdefault(consumer.block_index, set()).add(artifact.path)

    steps: list[StepView] = []
    for emitted_step in result.emitted.steps:
        block = block_by_index.get(emitted_step.block_index)
        if block is None:
            continue
        primary = emitted_step.invocations[0] if emitted_step.invocations else None
        unsupported = block.kind in {Kind.PYTHON_EMBED, Kind.UNKNOWN} or primary is None
        step_read_only = unsupported or not synchronized
        params: list[ParameterView] = []
        step_capabilities: set[str] = set(primary.operation.capabilities if primary else ())
        for invocation in emitted_step.invocations:
            for parameter in invocation.parameters:
                capabilities = parameter_capabilities(invocation, parameter)
                step_capabilities.update(capabilities)
                effective_value = values.get(parameter.id, parameter.value)
                editable = parameter.editable and not step_read_only
                reason = parameter.read_only_reason
                if not synchronized:
                    reason = (
                        read_only_reason
                        or "Generated output is not synchronized with compiler metadata."
                    )
                elif unsupported:
                    reason = f"{block.kind.value} blocks are read-only"
                definition = parameter.definition
                params.append(
                    ParameterView(
                        id=parameter.id,
                        name=parameter.name,
                        position=parameter.position,
                        source=(
                            repr(effective_value) if parameter.id in values else parameter.source
                        ),
                        value=effective_value,
                        editor_type=parameter.editor_type,
                        editable=editable,
                        read_only_reason=reason,
                        constraints=(
                            {"choices": list(definition.choices)}
                            if definition and definition.choices
                            else {}
                        ),
                        annotation=definition.annotation if definition else None,
                        required=definition.required if definition else True,
                        default=definition.default if definition else None,
                        capabilities=list(capabilities),
                    )
                )

        utility = _utility_view(primary.operation if primary else None)
        display_label = block.resolved_options.lookup.get("PROMPT-TEXT") or (
            primary.operation.title if primary else block.kind.value.replace("_", " ").title()
        )
        description = (
            (primary.operation.method_description or primary.operation.description)
            if primary
            else f"{block.kind.value.replace('_', ' ').title()} block"
        )
        parent_scope = leaf_parent.get(block.index)
        steps.append(
            StepView(
                id=emitted_step.function_name,
                function_name=emitted_step.function_name,
                block_index=block.index,
                source_span=SourceSpanView(
                    file=str(block.span.file) if block.span.file else None,
                    start_line=block.span.start_line,
                    end_line=block.span.end_line,
                ),
                functional_kind=block.kind.value,
                display_label=display_label,
                description=description,
                parameters=params,
                csv_inputs=sorted(inputs_by_block.get(block.index, ())),
                csv_outputs=sorted(outputs_by_block.get(block.index, ())),
                parent_scope_id=_scope_view_id(parent_scope, scope_by_id),
                branch=branch_by_scope.get(parent_scope),
                validation_state="unsupported" if step_read_only else "valid",
                raw_code=emitted_step.source if step_read_only else None,
                read_only=step_read_only,
                utility=utility,
                capabilities=sorted(step_capabilities),
            )
        )

    diagnostics = _diagnostics(result, step_id_by_block)
    if not synchronized:
        diagnostics.append(
            DiagnosticView(
                level="warning",
                code="output-unsynchronized",
                message=read_only_reason
                or "Generated output cannot be reconciled with compiler metadata; "
                "retranslate before editing.",
            )
        )

    return DocumentView(
        id=str(result.input_path.resolve()),
        source_path=str(result.input_path.resolve()),
        output_path=str(output_path.resolve()),
        source_hash=source_hash,
        output_hash=output_hash,
        revision=revision,
        synchronized=synchronized,
        read_only_reason=read_only_reason,
        steps=sorted(steps, key=lambda item: item.block_index),
        scopes=_scope_views(result.resolved.scope_tree, parent_by_scope),
        artifacts=_artifact_views(result.analyzed, step_id_by_block),
        diagnostics=diagnostics,
    )


def artifact_views_for_analysis(
    analyzed: AnalyzedProgram, step_id_by_block: dict[int, str]
) -> list[ArtifactView]:
    return _artifact_views(analyzed, step_id_by_block)


def sql_model_view(model: SqlEditableModel) -> SqlModelView:
    def span(value):
        return SqlSpanView(start=value.start, end=value.end) if value is not None else None

    def predicates(values):
        return [
            SqlPredicateView(
                id=item.id,
                left=item.left,
                operator=item.operator,
                right=item.right,
                connector=item.connector,
                raw=item.raw,
                editable=item.editable,
                read_only_reason=item.read_only_reason,
                span=span(item.span),
                connector_span=span(item.connector_span),
            )
            for item in values
        ]

    return SqlModelView(
        source=model.source,
        filter_operators=list(FILTER_OPERATORS),
        join_types=list(JOIN_TYPES),
        logical_connectors=list(get_args(SqlLogicalConnector)),
        statement_span=span(model.statement_span),
        selections=[
            SqlSelectionView(
                id=item.id,
                expression=item.expression,
                alias=item.alias,
                raw=item.raw,
                editable=item.editable,
                read_only_reason=item.read_only_reason,
                span=span(item.span),
            )
            for item in model.selections
        ],
        filters=predicates(model.filters),
        joins=[
            SqlJoinView(
                id=item.id,
                join_type=item.join_type,
                source=item.source,
                predicates=predicates(item.predicates),
                editable_type=item.editable_type,
                editable_source=item.editable_source,
                read_only_reason=item.read_only_reason,
                span=span(item.span),
                type_span=span(item.type_span),
                source_span=span(item.source_span),
            )
            for item in model.joins
        ],
        sources=[
            SqlSourceView(
                id=item.id,
                expression=item.expression,
                kind=item.kind,
                editable=item.editable,
                read_only_reason=item.read_only_reason,
                span=span(item.span),
                join_id=item.join_id,
            )
            for item in model.sources
        ],
        capabilities=SqlEditCapabilitiesView(
            selected=model.capabilities.selected,
            filters=model.capabilities.filters,
            joins=model.capabilities.joins,
            raw_sql=model.capabilities.raw_sql,
        ),
        read_only_reason=model.read_only_reason,
        select_list_span=span(model.select_list_span),
        where_clause_span=span(model.where_clause_span),
        where_body_span=span(model.where_body_span),
        from_clause_span=span(model.from_clause_span),
    )


def _utility_view(operation) -> UtilityView:
    if operation is None:
        return UtilityView(
            name="unsupported",
            class_name="UnsupportedOperation",
            module="vg2c",
            title="Unsupported operation",
            description="No emitted utility invocation is available for this block.",
        )
    return UtilityView(
        name=operation.utility_name,
        class_name=operation.class_name,
        module=operation.module,
        title=operation.title,
        description=operation.description,
        method=operation.method,
        method_description=operation.method_description,
        return_type=operation.return_type,
        capabilities=list(operation.capabilities),
        supported_mutations=list(operation.supported_mutations),
    )


def _diagnostics(
    result: CompilationResult, step_id_by_block: dict[int, str]
) -> list[DiagnosticView]:
    diagnostics = [
        DiagnosticView(
            level=_diagnostic_level(item.level),
            code=item.code,
            message=item.message,
            location=item.location,
        )
        for item in result.diagnostics[:MAX_DIAGNOSTICS]
    ]
    if len(result.diagnostics) > MAX_DIAGNOSTICS:
        diagnostics.append(
            DiagnosticView(
                level="warning",
                code="diagnostics-truncated",
                message=f"Only the first {MAX_DIAGNOSTICS} compiler diagnostics are shown.",
            )
        )
    for edge in result.analyzed.edges:
        node_id = step_id_by_block.get(edge.consumer.block_index)
        if edge.producer is None:
            diagnostics.append(
                DiagnosticView(
                    level="warning",
                    code="csv-input-missing-producer",
                    message=f"{edge.csv_path} has no producer in this script.",
                    node_id=node_id,
                )
            )
        elif not edge.order_ok:
            diagnostics.append(
                DiagnosticView(
                    level="warning",
                    code="csv-producer-order",
                    message=(
                        f"{edge.csv_path} may be consumed before its producer "
                        f"({edge.scope_relation})."
                    ),
                    node_id=node_id,
                )
            )
    return diagnostics


def _artifact_views(
    analyzed: AnalyzedProgram, step_id_by_block: dict[int, str]
) -> list[ArtifactView]:
    views: list[ArtifactView] = []
    for artifact in analyzed.artifacts:
        views.append(
            ArtifactView(
                id=artifact.path,
                path=artifact.path,
                label=Path(artifact.path).name,
                conditional=artifact.conditional,
                in_loop=artifact.in_loop,
                producer_step_ids=[
                    step_id_by_block[item.block_index]
                    for item in artifact.producers
                    if item.block_index in step_id_by_block
                ],
                consumer_step_ids=[
                    step_id_by_block[item.block_index]
                    for item in artifact.consumers
                    if item.block_index in step_id_by_block
                ],
                order_valid=artifact.order_valid,
                is_external_input=artifact.is_external_input,
                is_output=artifact.is_output,
            )
        )
    return views


def _scope_indexes(
    root: CompilerScopeNode,
) -> tuple[dict[int, int | None], dict[int, CompilerScopeNode]]:
    parent_by_scope: dict[int, int | None] = {}
    scope_by_id: dict[int, CompilerScopeNode] = {}
    stack = [(root, None)]
    while stack:
        node, parent = stack.pop()
        parent_by_scope[node.scope_id] = parent
        scope_by_id[node.scope_id] = node
        for child in reversed(node.children):
            stack.append((child, node.scope_id))
    return parent_by_scope, scope_by_id


def _scope_view_id(scope_id: int | None, scope_by_id: dict[int, CompilerScopeNode]) -> str | None:
    if scope_id is None or scope_id not in scope_by_id:
        return None
    return None if scope_by_id[scope_id].kind == "program" else f"scope-{scope_id}"


def _scope_views(
    root: CompilerScopeNode, parent_by_scope: dict[int, int | None]
) -> list[ScopeView]:
    _, scope_by_id = _scope_indexes(root)
    scopes: list[ScopeView] = []
    for scope in scope_by_id.values():
        if scope.kind == "program" or scope.kind == "leaf":
            continue
        node_kind = (
            "branch"
            if scope.kind in {"if-branch", "else-branch"}
            else "if"
            if scope.kind == "if"
            else "loop"
        )
        scopes.append(
            ScopeView(
                id=f"scope-{scope.scope_id}",
                node_kind=node_kind,
                scope_kind=scope.kind,
                label=scope.kind.replace("-", " ").title(),
                start_index=scope.start_index,
                end_index=scope.end_index,
                parent_scope_id=_scope_view_id(parent_by_scope[scope.scope_id], scope_by_id),
            )
        )
    return sorted(scopes, key=lambda item: (item.start_index, item.id))


def _diagnostic_level(value: str) -> str:
    return value if value in {"info", "warning", "error"} else "error"


__all__ = [
    "MAX_DIAGNOSTICS",
    "artifact_views_for_analysis",
    "document_view",
    "sql_model_view",
]
