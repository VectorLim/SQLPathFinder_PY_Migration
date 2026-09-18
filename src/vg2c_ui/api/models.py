from __future__ import annotations

from typing import Any, Literal

from pydantic import BaseModel, Field

from vg2c.emitter.models import EditorType
from vg2c.utility_metadata import FileEffectKind, PathBase, ValueKind

SCHEMA_VERSION = 5


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
    default: Any = None
    required: bool = True
    visibility: Literal["normal", "advanced", "internal"] = "normal"
    capabilities: list[str] = Field(default_factory=list)
    validation_state: Literal["valid", "warning", "unresolved", "unsupported"] = "valid"
    resettable: bool = True
    editable: bool = True
    read_only_reason: str | None = None
    value_schema: ValueSchemaView | None = None


class SemanticOperationView(BaseModel):
    id: str
    kind: str
    display_name: str
    description: str
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


class SymbolView(BaseModel):
    id: str
    display_name: str
    kind: Literal["global", "macro", "macro-row", "unresolved"]
    value_state: Literal["known", "runtime", "unknown"]
    value: Any = None
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


class ParameterView(BaseModel):
    id: str
    name: str
    position: int | None = None
    source: str
    value: Any = None
    editor_type: EditorType
    editable: bool
    read_only_reason: str | None = None
    constraints: dict[str, Any] = Field(default_factory=dict)
    annotation: str | None = None
    required: bool = True
    default: Any = None
    capabilities: list[str] = Field(default_factory=list)
    value_schema: ValueSchemaView | None = None
    internal: bool = False
    omitted: bool = False
    overridden: bool = False
    generated_value: Any = None


class UtilityView(BaseModel):
    name: str
    class_name: str
    module: str
    title: str
    description: str
    method: str | None = None
    method_description: str | None = None
    return_type: str | None = None
    capabilities: list[str] = Field(default_factory=list)
    supported_mutations: list[str] = Field(default_factory=list)


class OperationView(BaseModel):
    id: str
    utility: UtilityView
    parameters: list[ParameterView] = Field(default_factory=list)


class StepView(BaseModel):
    id: str
    node_kind: Literal["step"] = "step"
    function_name: str
    block_index: int
    source_span: SourceSpanView
    functional_kind: str
    display_label: str
    description: str
    parent_scope_id: str | None = None
    branch: Literal["true", "false"] | None = None
    validation_state: Literal["valid", "warning", "unsupported"] = "valid"
    raw_code: str | None = None
    read_only: bool = True
    operations: list[OperationView] = Field(default_factory=list)


class ScopeView(BaseModel):
    id: str
    node_kind: Literal["if", "branch", "loop"]
    scope_kind: str
    label: str
    start_index: int
    end_index: int
    parent_scope_id: str | None = None


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
    binding_id: str | None = None
    parameter_id: str | None = None
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
    synchronized: bool = True
    read_only_reason: str | None = None
    steps: list[StepView]
    scopes: list[ScopeView]
    artifacts: list[ArtifactView]
    diagnostics: list[DiagnosticView]
    effects: list[FileEffectView] = Field(default_factory=list)
    semantic_operations: list[SemanticOperationView] = Field(default_factory=list)
    files: list[FileResourceView] = Field(default_factory=list)
    symbols: list[SymbolView] = Field(default_factory=list)
    condition_operators: list[ConditionOperatorView] = Field(default_factory=list)


class SemanticChangeRequest(BaseModel):
    binding_id: str | None = None
    parameter_id: str = ""
    value: Any = None
    reset: bool = False


class ParameterChangeRequest(SemanticChangeRequest):
    """Deprecated v4 transport name retained until the frontend consumes v5 bindings."""



class DocumentSnapshot(BaseModel):
    schema_version: Literal[5]
    source_path: str
    output_path: str
    source_hash: str
    output_hash: str
    revision: str
    compiler_hash: str


class ChangeBatch(DocumentSnapshot):
    changes: list[SemanticChangeRequest] = Field(min_length=1)


class ValidationIssueView(BaseModel):
    level: Literal["warning", "error"] = "error"
    code: str
    message: str
    binding_id: str | None = None
    parameter_id: str | None = None


class ChangePreviewView(BaseModel):
    valid: bool
    diff: str
    issues: list[ValidationIssueView] = Field(default_factory=list)


class ChangeResultView(BaseModel):
    document: DocumentView


class CsvPreviewView(BaseModel):
    path: str
    columns: list[str]
    rows: list[list[str]]
    truncated: bool
    size_bytes: int


class DocumentReference(BaseModel):
    source_path: str
    output_path: str | None = None


class CsvPreviewRequest(DocumentSnapshot):
    effect_id: str
    endpoint_id: str
    expected_path: str
    changes: list[SemanticChangeRequest] = Field(default_factory=list)


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
    raw: str
    editable: bool
    read_only_reason: str | None = None
    span: SqlSpanView


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
    raw_sql: bool = True


class SqlModelView(BaseModel):
    source: str
    filter_operators: list[str]
    join_types: list[str]
    logical_connectors: list[str]
    statement_span: SqlSpanView
    selections: list[SqlSelectionView]
    filters: list[SqlPredicateView]
    joins: list[SqlJoinView]
    sources: list[SqlSourceView]
    capabilities: SqlEditCapabilitiesView
    read_only_reason: str | None = None
    select_list_span: SqlSpanView | None = None
    where_clause_span: SqlSpanView | None = None
    where_body_span: SqlSpanView | None = None
    from_clause_span: SqlSpanView | None = None


class SqlModelRequest(DocumentSnapshot):
    parameter_id: str
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
    ParameterView,
    UtilityView,
    OperationView,
    StepView,
    ScopeView,
    ArtifactView,
    DiagnosticView,
    FileEndpointView,
    FileEffectView,
    DocumentView,
    SemanticChangeRequest,
    ParameterChangeRequest,
    DocumentSnapshot,
    ChangeBatch,
    ValidationIssueView,
    ChangePreviewView,
    ChangeResultView,
    CsvPreviewView,
    DocumentReference,
    CsvPreviewRequest,
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
    SqlSourceView,
    SqlPredicateView,
    SqlJoinView,
    SqlEditCapabilitiesView,
    SqlModelView,
    SqlModelRequest,
    SqlActionRequest,
    SqlActionResponse,
)


__all__ = [model.__name__ for model in CONTRACT_MODELS] + [
    "CONTRACT_MODELS",
    "ParameterChangeRequest",
    "SCHEMA_VERSION",
]
