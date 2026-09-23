// Generated from vg2c_ui.api.models. DO NOT EDIT.

export const SCHEMA_VERSION = 6

export interface SourceSpanView {
  file: string | null
  start_line: number
  end_line: number
}

export interface ValueSchemaView {
  kind: 'string' | 'integer' | 'number' | 'boolean' | 'list' | 'object' | 'union' | 'dynamic'
  nullable: boolean
  choices: Array<unknown>
  items: ValueSchemaView | null
  properties: Record<string, ValueSchemaView>
  required_keys: Array<string>
  variants: Array<ValueSchemaView>
  path: boolean
  prefix_items: Array<ValueSchemaView>
  tuple_value: boolean
}

export interface SemanticBindingView {
  id: string
  owner_operation_id: string
  name: string
  display_label: string
  value: unknown
  symbol_id: string | null
  default_symbol_id: string | null
  default: unknown
  required: boolean
  visibility: 'normal' | 'advanced' | 'internal'
  capabilities: Array<string>
  validation_state: 'valid' | 'warning' | 'unresolved' | 'unsupported'
  resettable: boolean
  editable: boolean
  read_only_reason: string | null
  value_schema: ValueSchemaView | null
  file_choices: Array<string>
}

export interface SemanticOperationView {
  id: string
  kind: string
  display_name: string
  summary: string
  scope_id: number | null
  reorder_targets: Array<number>
  parent_operation_id: string | null
  branch: 'true' | 'false' | null
  source_span: SourceSpanView
  bindings: Array<SemanticBindingView>
  capabilities: Array<string>
  comments: Array<string>
  validation_state: 'valid' | 'warning' | 'unresolved' | 'unsupported'
  visibility: 'normal' | 'advanced' | 'internal'
}

export interface OperationReferenceView {
  operation_id: string
  binding_id: string | null
}

export interface SymbolReferenceView {
  operation_id: string
  binding_id: string
  context: 'condition' | 'parameter' | 'global-value'
}

export interface SymbolView {
  id: string
  display_name: string
  kind: 'global' | 'macro' | 'macro-row' | 'unresolved'
  value_state: 'known' | 'runtime' | 'unknown'
  value: unknown
  value_binding_id: string | null
  introduction: OperationReferenceView | null
  references: Array<SymbolReferenceView>
}

export interface FileResourceView {
  id: string
  path: string | null
  status: 'workspace' | 'generated' | 'external' | 'missing' | 'dynamic' | 'possible'
  producer_refs: Array<OperationReferenceView>
  consumer_refs: Array<OperationReferenceView>
  lifecycle_refs: Array<OperationReferenceView>
}

export interface ConditionOperatorView {
  code: string
  symbol: string
  operand_type: 'string' | 'numeric'
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

export interface FileEndpointView {
  id: string
  file_resource_id: string | null
  binding_id: string | null
  path: string | null
  expression: string | null
  path_base: 'working-directory' | 'script-directory' | 'runtime-search'
  phase: 'prior' | 'next' | 'deleted'
  state_ids: Array<string>
  status: 'known' | 'dynamic' | 'external' | 'missing' | 'possible'
}

export interface FileEffectView {
  id: string
  operation_id: string
  step_id: string
  block_index: number
  scope_id: number
  order: number
  kind: 'read' | 'observe' | 'write' | 'copy' | 'move' | 'transform' | 'append' | 'delete' | 'unknown'
  inputs: Array<FileEndpointView>
  outputs: Array<FileEndpointView>
  conditional: boolean
  in_loop: boolean
  reason: string | null
  dependency_ids: Array<string>
}

export interface DocumentView {
  schema_version: number
  id: string
  source_path: string
  output_path: string
  source_hash: string
  output_hash: string
  revision: string
  compiler_hash: string
  generation_state: 'current' | 'stale' | 'missing'
  read_only_reason: string | null
  diagnostics: Array<DiagnosticView>
  effects: Array<FileEffectView>
  semantic_operations: Array<SemanticOperationView>
  files: Array<FileResourceView>
  symbols: Array<SymbolView>
  condition_operators: Array<ConditionOperatorView>
}

export interface SemanticChangeRequest {
  binding_id: string
  value: unknown
  symbol_id: string | null
  reset: boolean
}

export interface DocumentSnapshot {
  schema_version: 6
  source_path: string
  output_path: string
  source_hash: string
  output_hash: string
  revision: string
  compiler_hash: string
}

export interface ChangeBatch {
  schema_version: 6
  source_path: string
  output_path: string
  source_hash: string
  output_hash: string
  revision: string
  compiler_hash: string
  changes: Array<SemanticChangeRequest>
}

export interface ReorderRequest {
  schema_version: 6
  source_path: string
  output_path: string
  source_hash: string
  output_hash: string
  revision: string
  compiler_hash: string
  source_scope_id: number
  target_scope_id: number
}

export interface ValidationIssueView {
  level: 'warning' | 'error'
  code: string
  message: string
  binding_id: string | null
}

export interface ChangePreviewView {
  valid: boolean
  diff: string
  issues: Array<ValidationIssueView>
}

export interface ChangeResultView {
  document: DocumentView
}

export interface DocumentReference {
  source_path: string
  output_path: string | null
}

export interface HtmlPreviewRequest {
  schema_version: 6
  source_path: string
  output_path: string
  source_hash: string
  output_hash: string
  revision: string
  compiler_hash: string
  operation_id: string
  changes: Array<SemanticChangeRequest>
}

export interface HtmlPreviewView {
  state: 'exact' | 'approximate' | 'error'
  html: string
  output_path: string | null
  message: string | null
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
  schema_version: 6
  source_path: string
  output_path: string
  source_hash: string
  output_hash: string
  revision: string
  compiler_hash: string
  document_id: string
  changes: Array<SemanticChangeRequest>
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
  producer_operation_id: string | null
  consumer_operation_id: string | null
}

export interface ProjectedDocumentView {
  document_id: string
  artifacts: Array<ArtifactView>
  effects: Array<FileEffectView>
  file_choices: Record<string, Array<string>>
}

export interface WorkspaceProjectionView {
  documents: Array<ProjectedDocumentView>
  dependencies: Array<DependencyLinkView>
  issues: Array<DependencyIssueView>
}

export interface WorkspaceFileView {
  path: string
  size_bytes: number
  modified_at: number
  role: 'source' | 'data' | 'generated'
  translatable: boolean
}

export interface WorkspaceUploadPolicyView {
  allowed_upload_suffixes: Array<string>
  max_upload_bytes: number
  max_file_count: number
  max_workspace_bytes: number
}

export interface SqlSpanView {
  start: number
  end: number
}

export interface SqlSelectionView {
  id: string
  expression: string
  alias: string | null
  display_label: string
  raw: string
  editable: boolean
  read_only_reason: string | null
  span: SqlSpanView
}

export interface SqlColumnChoiceView {
  id: string
  label: string
  source_id: string
}

export interface SqlTableChoiceView {
  id: string
  label: string
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

export interface SqlFileListView {
  id: string
  path: string
  column_ref: number | string
  lead_in: string
  choices: Array<string>
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
}

export interface SqlModelView {
  source: string
  filter_operators: Array<string>
  join_types: Array<string>
  logical_connectors: Array<string>
  statement_span: SqlSpanView
  selections: Array<SqlSelectionView>
  column_choices: Array<SqlColumnChoiceView>
  table_choices: Array<SqlTableChoiceView>
  filters: Array<SqlPredicateView>
  file_lists: Array<SqlFileListView>
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
  schema_version: 6
  source_path: string
  output_path: string
  source_hash: string
  output_hash: string
  revision: string
  compiler_hash: string
  binding_id: string
  changes: Array<SemanticChangeRequest>
}

export interface SqlActionRequest {
  schema_version: 6
  source_path: string
  output_path: string
  source_hash: string
  output_hash: string
  revision: string
  compiler_hash: string
  binding_id: string
  changes: Array<SemanticChangeRequest>
  action: string
  arguments: Record<string, unknown>
}

export interface SqlActionResponse {
  change: SemanticChangeRequest
  model: SqlModelView
}

