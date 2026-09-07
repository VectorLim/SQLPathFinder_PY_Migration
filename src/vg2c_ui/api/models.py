from __future__ import annotations

from typing import Any, Literal

from pydantic import BaseModel, Field

SCHEMA_VERSION = 2


class SourceSpanView(BaseModel):
    file: str | None = None
    start_line: int
    end_line: int


class ParameterView(BaseModel):
    id: str
    name: str
    position: int | None = None
    source: str
    value: Any = None
    editor_type: Literal["string", "multiline", "integer", "boolean", "list", "dynamic"]
    editable: bool
    read_only_reason: str | None = None
    constraints: dict[str, Any] = Field(default_factory=dict)
    annotation: str | None = None
    required: bool = True
    default: Any = None
    capabilities: list[str] = Field(default_factory=list)


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


class StepView(BaseModel):
    id: str
    node_kind: Literal["step"] = "step"
    function_name: str
    block_index: int
    source_span: SourceSpanView
    functional_kind: str
    display_label: str
    description: str
    parameters: list[ParameterView] = Field(default_factory=list)
    csv_inputs: list[str] = Field(default_factory=list)
    csv_outputs: list[str] = Field(default_factory=list)
    parent_scope_id: str | None = None
    branch: Literal["true", "false"] | None = None
    validation_state: Literal["valid", "warning", "unsupported"] = "valid"
    raw_code: str | None = None
    read_only: bool = True
    utility: UtilityView
    capabilities: list[str] = Field(default_factory=list)


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


class DocumentView(BaseModel):
    schema_version: int = SCHEMA_VERSION
    id: str
    source_path: str
    output_path: str
    source_hash: str
    output_hash: str
    revision: int = 1
    synchronized: bool = True
    read_only_reason: str | None = None
    steps: list[StepView]
    scopes: list[ScopeView]
    artifacts: list[ArtifactView]
    diagnostics: list[DiagnosticView]


class ParameterChangeRequest(BaseModel):
    parameter_id: str
    value: Any


class ChangeBatch(BaseModel):
    source_path: str
    output_path: str
    source_hash: str
    output_hash: str
    revision: int
    changes: list[ParameterChangeRequest] = Field(min_length=1)


class ValidationIssueView(BaseModel):
    level: Literal["warning", "error"] = "error"
    code: str
    message: str
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


class CsvPreviewRequest(BaseModel):
    source_path: str
    csv_path: str


class BatchTranslationRequest(BaseModel):
    source_paths: list[str] = Field(min_length=1, max_length=100)
    out_dir: str | None = None


class BatchTranslationResponse(BaseModel):
    documents: list[DocumentView]
    diagnostics: list[DiagnosticView]


class WorkspaceDocumentRequest(BaseModel):
    document_id: str
    source_path: str
    output_path: str
    changes: list[ParameterChangeRequest] = Field(default_factory=list)


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


class ProjectedDocumentView(BaseModel):
    document_id: str
    artifacts: list[ArtifactView]


class WorkspaceProjectionView(BaseModel):
    documents: list[ProjectedDocumentView]
    dependencies: list[DependencyLinkView]
    issues: list[DependencyIssueView]


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


class SqlModelRequest(BaseModel):
    source_path: str
    output_path: str
    parameter_id: str
    changes: list[ParameterChangeRequest] = Field(default_factory=list)


class SqlActionRequest(SqlModelRequest):
    action: str
    arguments: dict[str, Any] = Field(default_factory=dict)


class SqlActionResponse(BaseModel):
    change: ParameterChangeRequest
    model: SqlModelView


CONTRACT_MODELS = (
    SourceSpanView,
    ParameterView,
    UtilityView,
    StepView,
    ScopeView,
    ArtifactView,
    DiagnosticView,
    DocumentView,
    ParameterChangeRequest,
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


__all__ = [model.__name__ for model in CONTRACT_MODELS] + ["CONTRACT_MODELS", "SCHEMA_VERSION"]
