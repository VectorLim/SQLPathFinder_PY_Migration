from __future__ import annotations

from typing import Any, Literal

from pydantic import BaseModel, Field

from vg2c.utility_metadata import FileEffectKind, PathBase, ValueKind

SCHEMA_VERSION = 6


class SourceSpanView(BaseModel):
    file: str | None = None
    start_line: int
    end_line: int


class ValueSchemaView(BaseModel):
    kind: ValueKind
    nullable: bool = False
    choices: list[Any] = Field(default_factory=list)
    items: ValueSchemaView | None = None
    properties: dict[str, ValueSchemaView] = Field(default_factory=dict)
    required_keys: list[str] = Field(default_factory=list)
    variants: list[ValueSchemaView] = Field(default_factory=list)
    path: bool = False
    prefix_items: list[ValueSchemaView] = Field(default_factory=list)
    tuple_value: bool = False


class SemanticBindingView(BaseModel):
    id: str
    owner_operation_id: str
    name: str
    display_label: str
    value: Any = None
    symbol_id: str | None = None
    default_symbol_id: str | None = None
    default: Any = None
    required: bool = True
    visibility: Literal["normal", "advanced", "internal"] = "normal"
    capabilities: list[str] = Field(default_factory=list)
    validation_state: Literal["valid", "warning", "unresolved", "unsupported"] = "valid"
    resettable: bool = True
    editable: bool = True
    read_only_reason: str | None = None
    value_schema: ValueSchemaView | None = None
    file_choices: list[str] = Field(default_factory=list)


class SemanticOperationView(BaseModel):
    id: str
    kind: str
    display_name: str
    summary: str
    scope_id: int | None = None
    reorder_targets: list[int] = Field(default_factory=list)
    parent_operation_id: str | None = None
    branch: Literal["true", "false"] | None = None
    source_span: SourceSpanView
    bindings: list[SemanticBindingView] = Field(default_factory=list)
    capabilities: list[str] = Field(default_factory=list)
    comments: list[str] = Field(default_factory=list)
    validation_state: Literal["valid", "warning", "unresolved", "unsupported"] = "valid"
    visibility: Literal["normal", "advanced", "internal"] = "normal"


class OperationReferenceView(BaseModel):
    operation_id: str
    binding_id: str | None = None


class SymbolReferenceView(BaseModel):
    operation_id: str
    binding_id: str
    context: Literal["condition", "parameter", "global-value"] = "parameter"


class SymbolView(BaseModel):
    id: str
    display_name: str
    kind: Literal["global", "macro", "macro-row", "unresolved"]
    value_state: Literal["known", "runtime", "unknown"]
    value: Any = None
    value_binding_id: str | None = None
    introduction: OperationReferenceView | None = None
    references: list[SymbolReferenceView] = Field(default_factory=list)


class FileResourceView(BaseModel):
    id: str
    path: str | None = None
    status: Literal["workspace", "generated", "external", "missing", "dynamic", "possible"]
    producer_refs: list[OperationReferenceView] = Field(default_factory=list)
    consumer_refs: list[OperationReferenceView] = Field(default_factory=list)
    lifecycle_refs: list[OperationReferenceView] = Field(default_factory=list)


class ConditionOperatorView(BaseModel):
    code: str
    symbol: str
    operand_type: Literal["string", "numeric"]


class ArtifactView(BaseModel):
    id: str
    path: str
    label: str
    conditional: bool = False
    in_loop: bool = False
    producer_step_ids: list[str] = Field(default_factory=list)
    consumer_step_ids: list[str] = Field(default_factory=list)
    order_valid: bool = True
    is_external_input: bool = False
    is_output: bool = False


class DiagnosticView(BaseModel):
    level: Literal["info", "warning", "error"]
    code: str
    message: str
    location: str | None = None
    node_id: str | None = None


class FileEndpointView(BaseModel):
    id: str
    file_resource_id: str | None = None
    binding_id: str | None = None
    path: str | None
    expression: str | None
    path_base: PathBase
    phase: Literal["prior", "next", "deleted"]
    state_ids: list[str] = Field(default_factory=list)
    status: Literal["known", "dynamic", "external", "missing", "possible"]


class FileEffectView(BaseModel):
    id: str
    operation_id: str
    step_id: str
    block_index: int
    scope_id: int
    order: int
    kind: FileEffectKind
    inputs: list[FileEndpointView]
    outputs: list[FileEndpointView]
    conditional: bool
    in_loop: bool
    reason: str | None
    dependency_ids: list[str]


class DocumentView(BaseModel):
    schema_version: int = SCHEMA_VERSION
    id: str
    source_path: str
    output_path: str
    source_hash: str
    output_hash: str
    revision: str
    compiler_hash: str
    generation_state: Literal["current", "stale", "missing"] = "current"
    read_only_reason: str | None = None
    diagnostics: list[DiagnosticView]
    effects: list[FileEffectView] = Field(default_factory=list)
    semantic_operations: list[SemanticOperationView] = Field(default_factory=list)
    files: list[FileResourceView] = Field(default_factory=list)
    symbols: list[SymbolView] = Field(default_factory=list)
    condition_operators: list[ConditionOperatorView] = Field(default_factory=list)


class SemanticChangeRequest(BaseModel):
    binding_id: str
    value: Any = None
    symbol_id: str | None = None
    reset: bool = False


class DocumentSnapshot(BaseModel):
    schema_version: Literal[6]
    source_path: str
    output_path: str
    source_hash: str
    output_hash: str
    revision: str
    compiler_hash: str


class ChangeBatch(DocumentSnapshot):
    changes: list[SemanticChangeRequest] = Field(min_length=1)


class ReorderRequest(DocumentSnapshot):
    source_scope_id: int
    target_scope_id: int


class ValidationIssueView(BaseModel):
    level: Literal["warning", "error"] = "error"
    code: str
    message: str
    binding_id: str | None = None


class ChangePreviewView(BaseModel):
    valid: bool
    diff: str
    issues: list[ValidationIssueView] = Field(default_factory=list)


class ChangeResultView(BaseModel):
    document: DocumentView


class DocumentReference(BaseModel):
    source_path: str
    output_path: str | None = None


class HtmlPreviewRequest(DocumentSnapshot):
    operation_id: str
    changes: list[SemanticChangeRequest] = Field(default_factory=list)


class HtmlPreviewView(BaseModel):
    state: Literal["exact", "approximate", "error"]
    html: str
    output_path: str | None = None
    message: str | None = None


class BatchTranslationRequest(BaseModel):
    source_paths: list[str] = Field(min_length=1, max_length=100)
    out_dir: str | None = None


class BatchTranslationResponse(BaseModel):
    documents: list[DocumentView]
    diagnostics: list[DiagnosticView]


class WorkspaceDocumentRequest(DocumentSnapshot):
    document_id: str
    changes: list[SemanticChangeRequest] = Field(default_factory=list)


class WorkspaceProjectionRequest(BaseModel):
    documents: list[WorkspaceDocumentRequest] = Field(default_factory=list)


class DependencyIssueView(BaseModel):
    code: Literal["BROKEN_DEPENDENCY", "DUPLICATE_OUTPUT"]
    document_id: str
    step_id: str
    artifact: str
    message: str
    related_document_id: str | None = None
    related_step_id: str | None = None


class DependencyLinkView(BaseModel):
    artifact: str
    producer_document_id: str
    producer_step_id: str
    consumer_document_id: str
    consumer_step_id: str
    producer_operation_id: str | None = None
    consumer_operation_id: str | None = None


class ProjectedDocumentView(BaseModel):
    document_id: str
    artifacts: list[ArtifactView]
    effects: list[FileEffectView] = Field(default_factory=list)
    file_choices: dict[str, list[str]] = Field(default_factory=dict)


class WorkspaceProjectionView(BaseModel):
    documents: list[ProjectedDocumentView]
    dependencies: list[DependencyLinkView]
    issues: list[DependencyIssueView]


class WorkspaceFileView(BaseModel):
    path: str
    size_bytes: int
    modified_at: float
    role: Literal["source", "data", "generated"]
    translatable: bool


class WorkspaceUploadPolicyView(BaseModel):
    allowed_upload_suffixes: list[str]
    max_upload_bytes: int
    max_file_count: int
    max_workspace_bytes: int


class SqlSpanView(BaseModel):
    start: int
    end: int


class SqlSelectionView(BaseModel):
    id: str
    expression: str
    alias: str | None = None
    display_label: str
    raw: str
    editable: bool
    read_only_reason: str | None = None
    span: SqlSpanView


class SqlColumnChoiceView(BaseModel):
    id: str
    label: str
    source_id: str


class SqlTableChoiceView(BaseModel):
    id: str
    label: str


class SqlSourceView(BaseModel):
    id: str
    expression: str
    kind: Literal["from", "join"]
    editable: bool
    read_only_reason: str | None = None
    span: SqlSpanView
    join_id: str | None = None


class SqlPredicateView(BaseModel):
    id: str
    left: str
    operator: str
    right: str
    connector: Literal["AND", "OR"] | None = None
    raw: str
    editable: bool
    read_only_reason: str | None = None
    span: SqlSpanView
    connector_span: SqlSpanView | None = None


class SqlFileListView(BaseModel):
    id: str
    path: str
    column_ref: int | str
    lead_in: str
    choices: list[str]


class SqlJoinView(BaseModel):
    id: str
    join_type: str
    source: str
    predicates: list[SqlPredicateView]
    editable_type: bool
    editable_source: bool
    read_only_reason: str | None = None
    span: SqlSpanView
    type_span: SqlSpanView
    source_span: SqlSpanView


class SqlEditCapabilitiesView(BaseModel):
    selected: bool
    filters: bool
    joins: bool


class SqlModelView(BaseModel):
    source: str
    filter_operators: list[str]
    join_types: list[str]
    logical_connectors: list[str]
    statement_span: SqlSpanView
    selections: list[SqlSelectionView]
    column_choices: list[SqlColumnChoiceView] = Field(default_factory=list)
    table_choices: list[SqlTableChoiceView] = Field(default_factory=list)
    filters: list[SqlPredicateView]
    file_lists: list[SqlFileListView] = Field(default_factory=list)
    joins: list[SqlJoinView]
    sources: list[SqlSourceView]
    capabilities: SqlEditCapabilitiesView
    read_only_reason: str | None = None
    select_list_span: SqlSpanView | None = None
    where_clause_span: SqlSpanView | None = None
    where_body_span: SqlSpanView | None = None
    from_clause_span: SqlSpanView | None = None


class SqlModelRequest(DocumentSnapshot):
    binding_id: str
    changes: list[SemanticChangeRequest] = Field(default_factory=list)


class SqlActionRequest(SqlModelRequest):
    action: str
    arguments: dict[str, Any] = Field(default_factory=dict)


class SqlActionResponse(BaseModel):
    change: SemanticChangeRequest
    model: SqlModelView


CONTRACT_MODELS = (
    SourceSpanView,
    ValueSchemaView,
    SemanticBindingView,
    SemanticOperationView,
    OperationReferenceView,
    SymbolReferenceView,
    SymbolView,
    FileResourceView,
    ConditionOperatorView,
    ArtifactView,
    DiagnosticView,
    FileEndpointView,
    FileEffectView,
    DocumentView,
    SemanticChangeRequest,
    DocumentSnapshot,
    ChangeBatch,
    ReorderRequest,
    ValidationIssueView,
    ChangePreviewView,
    ChangeResultView,
    DocumentReference,
    HtmlPreviewRequest,
    HtmlPreviewView,
    BatchTranslationRequest,
    BatchTranslationResponse,
    WorkspaceDocumentRequest,
    WorkspaceProjectionRequest,
    DependencyIssueView,
    DependencyLinkView,
    ProjectedDocumentView,
    WorkspaceProjectionView,
    WorkspaceFileView,
    WorkspaceUploadPolicyView,
    SqlSpanView,
    SqlSelectionView,
    SqlColumnChoiceView,
    SqlTableChoiceView,
    SqlSourceView,
    SqlPredicateView,
    SqlFileListView,
    SqlJoinView,
    SqlEditCapabilitiesView,
    SqlModelView,
    SqlModelRequest,
    SqlActionRequest,
    SqlActionResponse,
)


__all__ = [model.__name__ for model in CONTRACT_MODELS] + [
    "CONTRACT_MODELS",
    "SCHEMA_VERSION",
]
