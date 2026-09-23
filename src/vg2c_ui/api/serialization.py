from __future__ import annotations

import hashlib
import json
from collections.abc import Iterable
from dataclasses import asdict
from pathlib import Path
from typing import get_args

from vg2c import CompilationResult
from vg2c.dataflow.file_effects import FileEffect
from vg2c.semantics import CONDITION_OPERATORS
from vg2c.sql_editor import SqlEditableModel, SqlLogicalConnector
from vg2c.sql_editor.operations import FILTER_OPERATORS, JOIN_TYPES
from vg2c.workflow import EffectiveDocument
from vg2c_ui.api.models import (
    ConditionOperatorView,
    DiagnosticView,
    DocumentView,
    FileEffectView,
    FileResourceView,
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
    SymbolView,
    ValueSchemaView,
)

MAX_DIAGNOSTICS = 200


def compiler_manifest_hash(result: CompilationResult) -> str:
    """Build the transport/session identity for the compiler manifest."""
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
    document: EffectiveDocument,
    *,
    output_path: Path,
    source_hash: str,
    output_hash: str,
    revision: str,
    file_choices: dict[str, list[str]] | None = None,
    read_only_reason: str | None = None,
    generation_state: str = "current",
    compiler_hash: str | None = None,
) -> DocumentView:
    """Map an authoritative effective document onto the transport contract."""
    file_choices = file_choices or {}
    diagnostics = _diagnostics(document.diagnostics)
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
        effects=effect_views(document.effects),
        semantic_operations=semantic_operation_views(
            document.operations,
            file_choices,
            read_only_reason=read_only_reason,
        ),
        files=file_resource_views(document.files),
        symbols=symbol_views(document.symbols),
        condition_operators=[
            ConditionOperatorView(code=code, symbol=symbol, operand_type=operand_type)
            for code, symbol, operand_type in CONDITION_OPERATORS
        ],
    )


def semantic_operation_views(
    operations,
    file_choices: dict[str, list[str]] | None = None,
    read_only_reason: str | None = None,
) -> list[SemanticOperationView]:
    file_choices = file_choices or {}
    return [
        SemanticOperationView(
            id=operation.id,
            kind=operation.kind,
            display_name=operation.display_name,
            summary=operation.summary,
            scope_id=operation.scope_id,
            reorder_targets=list(operation.reorder_targets),
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
    return [
        FileResourceView.model_validate(item, from_attributes=True)
        for item in files
    ]


def symbol_views(symbols) -> list[SymbolView]:
    return [
        SymbolView.model_validate(item, from_attributes=True)
        for item in symbols
    ]


def effect_views(effects: Iterable[FileEffect]) -> list[FileEffectView]:
    return [
        FileEffectView.model_validate(effect, from_attributes=True)
        for effect in effects
    ]


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


def _diagnostics(items) -> list[DiagnosticView]:
    diagnostics = [
        DiagnosticView(
            level=_diagnostic_level(item.level),
            code=item.code,
            message=item.message,
            location=item.location,
            node_id=item.node_id,
        )
        for item in items[:MAX_DIAGNOSTICS]
    ]
    if len(items) > MAX_DIAGNOSTICS:
        diagnostics.append(
            DiagnosticView(
                level="warning",
                code="diagnostics-truncated",
                message=f"Only the first {MAX_DIAGNOSTICS} diagnostics are shown.",
            )
        )
    return diagnostics


def _diagnostic_level(value: str) -> str:
    return value if value in {"info", "warning", "error"} else "error"


__all__ = [
    "MAX_DIAGNOSTICS",
    "compiler_manifest_hash",
    "document_view",
    "file_resource_views",
    "semantic_operation_views",
    "symbol_views",
    "sql_model_view",
]
