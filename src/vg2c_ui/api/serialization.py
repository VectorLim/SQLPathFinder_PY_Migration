from __future__ import annotations

import hashlib
import json
from collections.abc import Iterable
from dataclasses import asdict
from pathlib import Path
from typing import get_args

from vg2c import CompilationResult
from vg2c.dataflow.file_effects import FileEffect
from vg2c.editing import SemanticChange
from vg2c.reorder import legal_reorder_targets
from vg2c.semantics import CONDITION_OPERATORS
from vg2c.sql_editor import (
    FILTER_OPERATORS,
    JOIN_TYPES,
    SqlEditableModel,
    SqlLogicalConnector,
)
from vg2c.workflow import project_workflow
from vg2c_ui.api.models import (
    ArtifactView,
    ConditionOperatorView,
    DiagnosticView,
    DocumentView,
    FileEffectView,
    FileResourceView,
    OperationReferenceView,
    SemanticBindingView,
    SemanticOperationView,
    SourceSpanView,
    SqlColumnChoiceView,
    SqlEditCapabilitiesView,
    SqlFileListView,
    SqlJoinView,
    SqlModelView,
    SqlPredicateView,
    SqlSelectionView,
    SqlSourceView,
    SqlSpanView,
    SqlTableChoiceView,
    SymbolReferenceView,
    SymbolView,
    ValueSchemaView,
)
from vg2c_ui.services.file_choices import file_choices_by_operation

MAX_DIAGNOSTICS = 200


def compiler_manifest_hash(result: CompilationResult) -> str:
    manifest = [
        (invocation.id, asdict(invocation.operation))
        for step in result.emitted.steps
        for invocation in step.invocations
    ]
    digest = hashlib.sha256(result.emitted.source.encode("utf-8"))
    digest.update(json.dumps(manifest, sort_keys=True).encode("utf-8"))
    return digest.hexdigest()


def document_view(
    result: CompilationResult,
    *,
    output_path: Path,
    source_hash: str,
    output_hash: str,
    revision: str,
    saved_changes: Iterable[SemanticChange] = (),
    read_only_reason: str | None = None,
    generation_state: str = "current",
    workspace_root: Path | None = None,
    inventory_paths: Iterable[str] = (),
    compiler_hash: str | None = None,
) -> DocumentView:
    """Serialize compiler-owned semantics without re-discovering or re-inferring them."""
    saved_changes = tuple(saved_changes)
    workflow = project_workflow(result, saved_changes, output_path=output_path)
    reorder_targets = legal_reorder_targets(result, saved_changes)
    file_choices = file_choices_by_operation(
        workflow.effects,
        inventory_paths,
        output_path=output_path,
        workspace_root=workspace_root or output_path.parent,
    )
    diagnostics = _diagnostics(result, workflow.effects)
    return DocumentView(
        id=str(result.input_path.resolve()),
        source_path=str(result.input_path.resolve()),
        output_path=str(output_path.resolve()),
        source_hash=source_hash,
        compiler_hash=compiler_hash or compiler_manifest_hash(result),
        output_hash=output_hash,
        revision=revision,
        read_only_reason=read_only_reason,
        generation_state=generation_state,
        diagnostics=diagnostics,
        effects=effect_views(workflow.effects),
        semantic_operations=semantic_operation_views(
            workflow.operations, file_choices, result.resolved.scope_tree, reorder_targets,
            read_only_reason=read_only_reason,
        ),
        files=file_resource_views(workflow.files),
        symbols=symbol_views(workflow.symbols),
        condition_operators=[
            ConditionOperatorView(code=code, symbol=symbol, operand_type=operand_type)
            for code, symbol, operand_type in CONDITION_OPERATORS
        ],
    )


def semantic_operation_views(
    operations,
    file_choices: dict[str, list[str]] | None = None,
    scope_tree=None,
    reorder_targets: dict[int, tuple[int, ...]] | None = None,
    read_only_reason: str | None = None,
) -> list[SemanticOperationView]:
    file_choices = file_choices or {}
    scope_by_block = {}
    def collect(node):
        if node.kind == "leaf" and node.block_index is not None:
            scope_by_block[node.block_index] = node.scope_id
        for child in node.children:
            collect(child)
    if scope_tree is not None:
        collect(scope_tree)
    reorder_targets = reorder_targets or {}
    return [
        SemanticOperationView(
            id=operation.id,
            kind=operation.kind,
            display_name=operation.display_name,
            summary=operation.summary,
            scope_id=scope_by_block.get(operation.block_index),
            reorder_targets=list(reorder_targets.get(scope_by_block.get(operation.block_index), ())),
            parent_operation_id=operation.parent_operation_id,
            branch=operation.branch,
            source_span=SourceSpanView(
                file=str(operation.source_span.file) if operation.source_span.file else None,
                start_line=operation.source_span.start_line,
                end_line=operation.source_span.end_line,
            ),
            bindings=[
                SemanticBindingView(
                    id=binding.id,
                    owner_operation_id=binding.owner_operation_id,
                    name=binding.name,
                    display_label=binding.display_label,
                    value=None if binding.symbol_id else binding.value,
                    symbol_id=binding.symbol_id,
                    default_symbol_id=binding.default_symbol_id,
                    default=binding.default,
                    required=binding.required,
                    visibility=binding.visibility,
                    capabilities=list(binding.capabilities),
                    validation_state=binding.validation_state,
                    resettable=binding.resettable,
                    editable=binding.editable and read_only_reason is None,
                    read_only_reason=read_only_reason or binding.read_only_reason,
                    value_schema=_value_schema_view(binding.schema),
                    file_choices=(
                        file_choices.get(operation.id, [])
                        if "file-input" in binding.capabilities
                        else []
                    ),
                )
                for binding in operation.bindings
                if binding.visibility != "internal"
            ],
            capabilities=list(operation.capabilities),
            comments=list(operation.comments),
            validation_state=operation.validation_state,
            visibility=operation.visibility,
        )
        for operation in operations
        if operation.visibility != "internal"
    ]


def file_resource_views(files) -> list[FileResourceView]:
    def ref_view(ref):
        return OperationReferenceView(
            operation_id=ref.operation_id,
            binding_id=ref.binding_id,
        )

    return [
        FileResourceView(
            id=item.id,
            path=item.path,
            status=item.status,
            producer_refs=[ref_view(ref) for ref in item.producer_refs],
            consumer_refs=[ref_view(ref) for ref in item.consumer_refs],
            lifecycle_refs=[ref_view(ref) for ref in item.lifecycle_refs],
        )
        for item in files
    ]


def symbol_views(symbols) -> list[SymbolView]:
    return [
        SymbolView(
            id=item.id,
            display_name=item.display_name,
            kind=item.kind,
            value_state=item.value_state,
            value=item.value,
            value_binding_id=item.value_binding_id,
            introduction=(
                OperationReferenceView(
                    operation_id=item.introduction.operation_id,
                    binding_id=item.introduction.binding_id,
                )
                if item.introduction
                else None
            ),
            references=[
                SymbolReferenceView(
                    operation_id=ref.operation_id,
                    binding_id=ref.binding_id,
                    context=ref.context,
                )
                for ref in item.references
            ],
        )
        for item in symbols
    ]


def effect_views(effects: Iterable[FileEffect]) -> list[FileEffectView]:
    return [
        FileEffectView.model_validate(effect, from_attributes=True)
        for effect in effects
    ]


def artifact_views_for_effects(effects: Iterable[FileEffect]) -> list[ArtifactView]:
    artifacts: dict[str, ArtifactView] = {}
    for effect in effects:
        for endpoint in (*effect.inputs, *effect.outputs):
            if not endpoint.path:
                continue
            artifact = artifacts.setdefault(
                endpoint.path,
                ArtifactView(
                    id=endpoint.path,
                    path=endpoint.path,
                    label=Path(endpoint.path).name,
                ),
            )
            references = (
                artifact.producer_step_ids
                if endpoint.phase == "next"
                else artifact.consumer_step_ids
            )
            if effect.step_id not in references:
                references.append(effect.step_id)
            artifact.conditional |= effect.conditional
            artifact.in_loop |= effect.in_loop
            artifact.order_valid &= endpoint.status != "missing"
    for artifact in artifacts.values():
        artifact.is_output = bool(artifact.producer_step_ids)
        artifact.is_external_input = (
            bool(artifact.consumer_step_ids) and not artifact.is_output
        )
    return sorted(artifacts.values(), key=lambda item: item.path)


def sql_model_view(model: SqlEditableModel) -> SqlModelView:
    def span(value):
        return (
            SqlSpanView(start=value.start, end=value.end) if value is not None else None
        )

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
                display_label=item.display_label,
                raw=item.raw,
                editable=item.editable,
                read_only_reason=item.read_only_reason,
                span=span(item.span),
            )
            for item in model.selections
        ],
        column_choices=[
            SqlColumnChoiceView.model_validate(item, from_attributes=True)
            for item in model.column_choices
        ],
        table_choices=[
            SqlTableChoiceView.model_validate(item, from_attributes=True)
            for item in model.table_choices
        ],
        filters=predicates(model.filters),
        file_lists=[
            SqlFileListView(
                id=item.id, path=item.path, column_ref=item.column_ref,
                lead_in=item.lead_in, choices=list(item.choices),
            )
            for item in model.file_lists
        ],
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
        ),
        read_only_reason=model.read_only_reason,
        select_list_span=span(model.select_list_span),
        where_clause_span=span(model.where_clause_span),
        where_body_span=span(model.where_body_span),
        from_clause_span=span(model.from_clause_span),
    )


def _value_schema_view(schema) -> ValueSchemaView | None:
    if schema is None:
        return None
    return ValueSchemaView(
        kind=schema.kind,
        nullable=schema.nullable,
        choices=list(schema.choices),
        items=_value_schema_view(schema.items),
        properties={name: _value_schema_view(item) for name, item in schema.properties},
        required_keys=list(schema.required_keys),
        variants=[_value_schema_view(item) for item in schema.variants],
        path=schema.path,
        prefix_items=[_value_schema_view(item) for item in schema.prefix_items],
        tuple_value=schema.tuple_value,
    )


def _diagnostics(
    result: CompilationResult, effects: Iterable[FileEffect]
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
    for effect in effects:
        for endpoint in effect.inputs:
            if endpoint.status in {"external", "missing"}:
                diagnostics.append(
                    DiagnosticView(
                        level="warning" if endpoint.status == "missing" else "info",
                        code=(
                            "file-state-missing"
                            if endpoint.status == "missing"
                            else "external-file-input"
                        ),
                        message=f"{endpoint.path}: {'unavailable after deletion/move' if endpoint.status == 'missing' else 'external input' }.",
                        node_id=effect.operation_id,
                    )
                )
    return diagnostics


def _diagnostic_level(value: str) -> str:
    return value if value in {"info", "warning", "error"} else "error"


__all__ = [
    "MAX_DIAGNOSTICS",
    "document_view",
    "file_resource_views",
    "semantic_operation_views",
    "symbol_views",
    "sql_model_view",
]
