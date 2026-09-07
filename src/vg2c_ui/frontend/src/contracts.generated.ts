// Generated from vg2c_ui.api.models. DO NOT EDIT.

export interface SourceSpanView {
  file: string | null
  start_line: number
  end_line: number
}

export interface ParameterView {
  id: string
  name: string
  position: number | null
  source: string
  value: unknown
  editor_type: 'string' | 'multiline' | 'integer' | 'boolean' | 'list' | 'dynamic'
  editable: boolean
  read_only_reason: string | null
  constraints: Record<string, unknown>
  annotation: string | null
  required: boolean
  default: unknown
  capabilities: Array<string>
}

export interface UtilityView {
  name: string
  class_name: string
  module: string
  title: string
  description: string
  method: string | null
  method_description: string | null
  return_type: string | null
  capabilities: Array<string>
  supported_mutations: Array<string>
}

export interface StepView {
  id: string
  node_kind: 'step'
  function_name: string
  block_index: number
  source_span: SourceSpanView
  functional_kind: string
  display_label: string
  description: string
  parameters: Array<ParameterView>
  csv_inputs: Array<string>
  csv_outputs: Array<string>
  parent_scope_id: string | null
  branch: 'true' | 'false' | null
  validation_state: 'valid' | 'warning' | 'unsupported'
  raw_code: string | null
  read_only: boolean
  utility: UtilityView
  capabilities: Array<string>
}

export interface ScopeView {
  id: string
  node_kind: 'if' | 'branch' | 'loop'
  scope_kind: string
  label: string
  start_index: number
  end_index: number
  parent_scope_id: string | null
}

export interface ArtifactView {
  id: string
  path: string
  label: string
  conditional: boolean
  in_loop: boolean
  producer_step_ids: Array<string>
  consumer_step_ids: Array<string>
  order_valid: boolean
  is_external_input: boolean
  is_output: boolean
}

export interface DiagnosticView {
  level: 'info' | 'warning' | 'error'
  code: string
  message: string
  location: string | null
  node_id: string | null
}

export interface DocumentView {
  schema_version: number
  id: string
  source_path: string
  output_path: string
  source_hash: string
  output_hash: string
  revision: number
  synchronized: boolean
  read_only_reason: string | null
  steps: Array<StepView>
  scopes: Array<ScopeView>
  artifacts: Array<ArtifactView>
  diagnostics: Array<DiagnosticView>
}

export interface ParameterChangeRequest {
  parameter_id: string
  value: unknown
}

export interface ChangeBatch {
  source_path: string
  output_path: string
  source_hash: string
  output_hash: string
  revision: number
  changes: Array<ParameterChangeRequest>
}

export interface ValidationIssueView {
  level: 'warning' | 'error'
  code: string
  message: string
  parameter_id: string | null
}

export interface ChangePreviewView {
  valid: boolean
  diff: string
  issues: Array<ValidationIssueView>
}

export interface ChangeResultView {
  document: DocumentView
}

export interface CsvPreviewView {
  path: string
  columns: Array<string>
  rows: Array<Array<string>>
  truncated: boolean
  size_bytes: number
}

export interface DocumentReference {
  source_path: string
  output_path: string | null
}

export interface CsvPreviewRequest {
  source_path: string
  csv_path: string
}

export interface BatchTranslationRequest {
  source_paths: Array<string>
  out_dir: string | null
}

export interface BatchTranslationResponse {
  documents: Array<DocumentView>
  diagnostics: Array<DiagnosticView>
}

export interface WorkspaceDocumentRequest {
  document_id: string
  source_path: string
  output_path: string
  changes: Array<ParameterChangeRequest>
}

export interface WorkspaceProjectionRequest {
  documents: Array<WorkspaceDocumentRequest>
}

export interface DependencyIssueView {
  code: 'BROKEN_DEPENDENCY' | 'DUPLICATE_OUTPUT'
  document_id: string
  step_id: string
  artifact: string
  message: string
  related_document_id: string | null
  related_step_id: string | null
}

export interface DependencyLinkView {
  artifact: string
  producer_document_id: string
  producer_step_id: string
  consumer_document_id: string
  consumer_step_id: string
}

export interface ProjectedDocumentView {
  document_id: string
  artifacts: Array<ArtifactView>
}

export interface WorkspaceProjectionView {
  documents: Array<ProjectedDocumentView>
  dependencies: Array<DependencyLinkView>
  issues: Array<DependencyIssueView>
}

export interface SqlSpanView {
  start: number
  end: number
}

export interface SqlSelectionView {
  id: string
  expression: string
  alias: string | null
  raw: string
  editable: boolean
  read_only_reason: string | null
  span: SqlSpanView
}

export interface SqlSourceView {
  id: string
  expression: string
  kind: 'from' | 'join'
  editable: boolean
  read_only_reason: string | null
  span: SqlSpanView
  join_id: string | null
}

export interface SqlPredicateView {
  id: string
  left: string
  operator: string
  right: string
  connector: 'AND' | 'OR' | null
  raw: string
  editable: boolean
  read_only_reason: string | null
  span: SqlSpanView
  connector_span: SqlSpanView | null
}

export interface SqlJoinView {
  id: string
  join_type: string
  source: string
  predicates: Array<SqlPredicateView>
  editable_type: boolean
  editable_source: boolean
  read_only_reason: string | null
  span: SqlSpanView
  type_span: SqlSpanView
  source_span: SqlSpanView
}

export interface SqlEditCapabilitiesView {
  selected: boolean
  filters: boolean
  joins: boolean
  raw_sql: boolean
}

export interface SqlModelView {
  source: string
  filter_operators: Array<string>
  join_types: Array<string>
  logical_connectors: Array<string>
  statement_span: SqlSpanView
  selections: Array<SqlSelectionView>
  filters: Array<SqlPredicateView>
  joins: Array<SqlJoinView>
  sources: Array<SqlSourceView>
  capabilities: SqlEditCapabilitiesView
  read_only_reason: string | null
  select_list_span: SqlSpanView | null
  where_clause_span: SqlSpanView | null
  where_body_span: SqlSpanView | null
  from_clause_span: SqlSpanView | null
}

export interface SqlModelRequest {
  source_path: string
  output_path: string
  parameter_id: string
  changes: Array<ParameterChangeRequest>
}

export interface SqlActionRequest {
  source_path: string
  output_path: string
  parameter_id: string
  changes: Array<ParameterChangeRequest>
  action: string
  arguments: Record<string, unknown>
}

export interface SqlActionResponse {
  change: ParameterChangeRequest
  model: SqlModelView
}

