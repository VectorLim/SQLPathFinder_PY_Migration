export type NodeKind = 'step' | 'if' | 'branch' | 'loop' | 'csv-artifact'

export interface Position {
  x: number
  y: number
}

export interface Viewport extends Position {
  zoom: number
}

export interface ParameterDescriptor {
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
}

export interface UtilityDescriptor {
  name: string
  class_name: string
  module: string
  title: string
  description: string
  method: string | null
  method_description: string | null
  return_type: string | null
  fallback: boolean
}

export interface StepNode {
  id: string
  node_kind: 'step'
  function_name: string
  block_index: number
  functional_kind: string
  display_label: string
  icon_key: string
  description: string
  parameters: ParameterDescriptor[]
  csv_inputs: string[]
  csv_outputs: string[]
  parent_scope_id: string | null
  branch: 'true' | 'false' | null
  validation_state: 'valid' | 'warning' | 'unsupported'
  raw_code: string | null
  read_only: boolean
  utility: UtilityDescriptor
}

export interface ScopeNode {
  id: string
  node_kind: 'if' | 'branch' | 'loop'
  scope_kind: string
  label: string
  start_index: number
  end_index: number
  parent_scope_id: string | null
}

export interface CsvArtifactNode {
  id: string
  node_kind: 'csv-artifact'
  path: string
  label: string
  conditional: boolean
  in_loop: boolean
}

export type WorkflowNode = StepNode | ScopeNode | CsvArtifactNode

export interface WorkflowEdge {
  id: string
  source: string
  target: string
  kind: 'control' | 'data'
  label: string | null
  dashed: boolean
  valid: boolean
  scope_relation: string | null
}

export interface Diagnostic {
  level: 'info' | 'warning' | 'error'
  code: string
  message: string
  location: string | null
  node_id: string | null
}

export interface WorkflowDocument {
  schema_version: number
  id: string
  source_path: string
  output_path: string
  source_hash: string
  output_hash: string
  revision: number
  steps: StepNode[]
  scopes: ScopeNode[]
  artifacts: CsvArtifactNode[]
  control_edges: WorkflowEdge[]
  data_edges: WorkflowEdge[]
  diagnostics: Diagnostic[]
  layout: {
    positions: Record<string, Position>
    viewport: Viewport
  }
  overrides: Array<{ step_id: string; parameter_id: string; value: unknown }>
}

export interface BatchTranslationResponse {
  documents: WorkflowDocument[]
  diagnostics: Diagnostic[]
}

export interface SetParameterCommand {
  operation: 'set-parameter'
  step_id: string
  parameter_id: string
  value: unknown
}

export interface CommandBatch {
  source_path: string
  output_path: string
  source_hash: string
  output_hash: string
  revision: number
  commands: SetParameterCommand[]
}

export interface ValidationIssue {
  level: 'warning' | 'error'
  code: string
  message: string
  step_id: string | null
  parameter_id: string | null
}

export interface CommandPreview {
  valid: boolean
  diff: string
  issues: ValidationIssue[]
}

export interface CommandResult {
  document: WorkflowDocument
  diff: string
}

export interface CsvPreview {
  path: string
  columns: string[]
  rows: string[][]
  truncated: boolean
  size_bytes: number
}
