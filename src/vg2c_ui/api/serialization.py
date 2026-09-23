from __future__ import annotations

import hashlib
import json
from collections.abc import Iterable
from dataclasses import asdict
from pathlib import Path
from typing import get_args

from vg2c import CompilationResult
from vg2c.dataflow.file_effects import FileEffect
from vg2c.editing import ParameterChange
from vg2c.kind import Kind
from vg2c.operands import ScopeNode as CompilerScopeNode
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
    OperationView,
    ParameterView,
    ScopeView,
    SemanticBindingView,
    SemanticOperationView,
    SourceSpanView,
    SqlEditCapabilitiesView,
    SqlJoinView,
    SqlModelView,
    SqlPredicateView,
    SqlSelectionView,
    SqlSourceView,
    SqlSpanView,
    StepView,
    SymbolReferenceView,
    SymbolView,
    UtilityView,
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
    saved_changes: Iterable[ParameterChange] = (),
    synchronized: bool = True,
    read_only_reason: str | None = None,
    workspace_root: Path | None = None,
    inventory_paths: Iterable[str] = (),
) -> DocumentView:
    """Serialize compiler-owned semantics without re-discovering or re-inferring them."""
    saved_changes = tuple(saved_changes)
    values = {change.parameter_id: change.value for change in saved_changes}
    workflow = project_workflow(result, saved_changes, output_path=output_path)
    file_choices = file_choices_by_operation(
        workflow.effects,
        inventory_paths,
        output_path=output_path,
        workspace_root=workspace_root or output_path.parent,
    )
    artifacts = artifact_views_for_effects(workflow.effects)
    block_by_index = {block.index: block for block in result.resolved.blocks}
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
            else "false" if scope.kind == "else-branch" else None
        )
        for scope_id, scope in scope_by_id.items()
    }

    steps: list[StepView] = []
    for emitted_step in result.emitted.steps:
        block = block_by_index.get(emitted_step.block_index)
        if block is None:
            continue
        primary = emitted_step.invocations[0] if emitted_step.invocations else None
        unsupported = block.kind in {Kind.PYTHON_EMBED, Kind.UNKNOWN} or primary is None
        step_read_only = unsupported or not synchronized
        operations: list[OperationView] = []
        for invocation in emitted_step.invocations:
            invocation_parameters: list[ParameterView] = []
            for parameter in invocation.parameters:
                capabilities = parameter.definition.capabilities if parameter.definition else ()
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
                invocation_parameters.append(
                    ParameterView(
                        id=parameter.id,
                        name=parameter.name,
                        position=parameter.position,
                        source=(
                            repr(effective_value)
                            if parameter.id in values
                            else parameter.source
                        ),
                        value=effective_value,
                        editor_type=parameter.editor_type,
                        editable=editable,
                        read_only_reason=reason,
                        constraints=(
                            {"choices": list(definition.schema.choices)}
                            if definition and definition.schema and definition.schema.choices
                            else {}
                        ),
                        annotation=definition.annotation if definition else None,
                        required=definition.required if definition else True,
                        default=definition.default if definition else None,
                        capabilities=list(capabilities),
                        value_schema=(
                            _value_schema_view(definition.schema)
                            if definition
                            else None
                        ),
                        internal=definition.visibility == "internal" if definition else False,
                        omitted=parameter.source_range is None,
                        overridden=parameter.id in values,
                        generated_value=parameter.value,
                    )
                )
            operations.append(
                OperationView(
                    id=invocation.id,
                    utility=_utility_view(invocation.operation),
                    parameters=invocation_parameters,
                )
            )

        utility = _utility_view(primary.operation if primary else None)
        if not operations:
            operations.append(
                OperationView(id=f"block-{block.index}:source", utility=utility)
            )
        display_label = block.resolved_options.lookup.get("PROMPT-TEXT") or (
            primary.operation.title
            if primary
            else block.kind.value.replace("_", " ").title()
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
                parent_scope_id=_scope_view_id(parent_scope, scope_by_id),
                branch=branch_by_scope.get(parent_scope),
                validation_state="unsupported" if step_read_only else "valid",
                raw_code=None,
                read_only=step_read_only,
                operations=operations,
            )
        )

    known_steps = {step.id for step in steps}
    for effect in workflow.effects:
        if effect.step_id in known_steps:
            continue
        block = block_by_index[effect.block_index]
        utility = _utility_view(None)
        label = block.kind.value.replace("_", " ").title()
        steps.append(
            StepView(
                id=effect.step_id,
                function_name="",
                block_index=block.index,
                source_span=SourceSpanView(
                    file=str(block.span.file) if block.span.file else None,
                    start_line=block.span.start_line,
                    end_line=block.span.end_line,
                ),
                functional_kind=block.kind.value,
                display_label=label,
                description="Compiler source/control operation",
                read_only=True,
                validation_state="unsupported",
                raw_code=None,
                parent_scope_id=_scope_view_id(block.scope_id, scope_by_id),
                operations=[OperationView(id=effect.operation_id, utility=utility)],
            )
        )
        known_steps.add(effect.step_id)

    diagnostics = _diagnostics(result, workflow.effects)
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
        compiler_hash=compiler_manifest_hash(result),
        output_hash=output_hash,
        revision=revision,
        synchronized=synchronized,
        read_only_reason=read_only_reason,
        steps=sorted(steps, key=lambda item: item.block_index),
        scopes=_scope_views(result.resolved.scope_tree, parent_by_scope),
        artifacts=artifacts,
        diagnostics=diagnostics,
        effects=effect_views(workflow.effects),
        semantic_operations=semantic_operation_views(workflow.operations, file_choices),
        files=file_resource_views(workflow.files),
        symbols=symbol_views(workflow.symbols),
        condition_operators=[
            ConditionOperatorView(code=code, symbol=symbol, operand_type=operand_type)
            for code, symbol, operand_type in CONDITION_OPERATORS
        ],
    )


def semantic_operation_views(
    operations, file_choices: dict[str, list[str]] | None = None
) -> list[SemanticOperationView]:
    file_choices = file_choices or {}
    return [
        SemanticOperationView(
            id=operation.id,
            kind=operation.kind,
            display_name=operation.display_name,
            summary=operation.summary,
            description=operation.description,
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
                    value=binding.value,
                    default=binding.default,
                    required=binding.required,
                    visibility=binding.visibility,
                    capabilities=list(binding.capabilities),
                    validation_state=binding.validation_state,
                    resettable=binding.resettable,
                    editable=binding.editable,
                    read_only_reason=binding.read_only_reason,
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
            condition_value=item.condition_value,
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
        before_statement=model.source[: model.statement_span.start],
        after_statement=model.source[model.statement_span.end :],
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


def _scope_view_id(
    scope_id: int | None, scope_by_id: dict[int, CompilerScopeNode]
) -> str | None:
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
            else "if" if scope.kind == "if" else "loop"
        )
        scopes.append(
            ScopeView(
                id=f"scope-{scope.scope_id}",
                node_kind=node_kind,
                scope_kind=scope.kind,
                label=_scope_label(scope.kind),
                start_index=scope.start_index,
                end_index=scope.end_index,
                parent_scope_id=_scope_view_id(
                    parent_by_scope[scope.scope_id], scope_by_id
                ),
            )
        )
    return sorted(scopes, key=lambda item: (item.start_index, item.id))


def _scope_label(kind: str) -> str:
    labels = {
        "if": "Condition",
        "if-branch": "True branch",
        "else-branch": "Else branch",
        "macro": "For each macro row",
        "loop": "For each row",
    }
    return labels.get(kind, kind.replace("-", " ").title())


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
