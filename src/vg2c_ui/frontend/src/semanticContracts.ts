import type { WorkflowDocument } from './types'

export const SEMANTIC_SCHEMA_VERSION = 1 as const

export type WorkflowEntityKind = 'document' | 'step' | 'parameter' | 'artifact'
export type SqlEntityKind = 'selection' | 'filter' | 'join' | 'join_predicate' | 'source'

export interface WorkflowEntityRef {
  schema_version: typeof SEMANTIC_SCHEMA_VERSION
  document_id: string
  entity_kind: WorkflowEntityKind
  entity_id: string
  step_id: string | null
  document_revision: number
  output_hash: string
}

export interface SqlEntityRef {
  schema_version: typeof SEMANTIC_SCHEMA_VERSION
  document_id: string
  step_id: string
  sql_parameter_id: string
  entity_kind: SqlEntityKind
  parsed_id: string
  fingerprint: string
  ordinal_hint: number
  document_revision: number
  output_hash: string
}

export interface OpenDocumentState {
  document_id: string
  source_hash: string
  output_hash: string
  revision: number
}

export interface PendingEdit {
  document_id: string
  step_id: string
  parameter_id: string
  value: unknown
}

export interface ClientWorkingState {
  schema_version: typeof SEMANTIC_SCHEMA_VERSION
  active_document_id: string | null
  selected_item_id: string | null
  open_documents: OpenDocumentState[]
  pending_edits: PendingEdit[]
}

export interface EffectiveDocumentInput {
  base_document: WorkflowDocument
  pending_edits: PendingEdit[]
}

export interface SqlSpan {
  start: number
  end: number
}

export interface SqlSelection {
  id: string
  expression: string
  alias: string | null
  raw: string
  editable: boolean
  read_only_reason: string | null
  span: SqlSpan
}

export interface SqlSource {
  id: string
  expression: string
  kind: 'from' | 'join'
  editable: boolean
  read_only_reason: string | null
  span: SqlSpan
  join_id: string | null
}

export type SqlLogicalConnector = 'AND' | 'OR'

export interface SqlPredicate {
  id: string
  left: string
  operator: string
  right: string
  connector: SqlLogicalConnector | null
  raw: string
  editable: boolean
  read_only_reason: string | null
  span: SqlSpan
  connector_span: SqlSpan | null
}

export interface SqlJoin {
  id: string
  join_type: string
  source: string
  predicates: SqlPredicate[]
  editable_type: boolean
  editable_source: boolean
  read_only_reason: string | null
  span: SqlSpan
  type_span: SqlSpan
  source_span: SqlSpan
}

export interface SqlEditCapabilities {
  selected: boolean
  filters: boolean
  joins: boolean
  raw_sql: true
}

export interface SqlEditableModel {
  source: string
  statement_span: SqlSpan
  selections: SqlSelection[]
  filters: SqlPredicate[]
  joins: SqlJoin[]
  sources: SqlSource[]
  capabilities: SqlEditCapabilities
  read_only_reason: string | null
  select_list_span: SqlSpan | null
  where_clause_span: SqlSpan | null
  where_body_span: SqlSpan | null
  from_clause_span: SqlSpan | null
}

export interface SqlTransformResult {
  sql: string
  model: SqlEditableModel
}

// Omitted patch fields preserve their current value. Null is reserved for
// values that can be explicitly cleared by the backend semantic service.
export interface SelectionPatch {
  expression?: string
  alias?: string | null
}

export interface PredicatePatch {
  left?: string
  operator?: string
  right?: string
  connector?: SqlLogicalConnector | null
}

export interface JoinPatch {
  join_type?: string
  source?: string
}

export interface SqlEntityResolution {
  status: 'resolved' | 'not_found' | 'ambiguous' | 'stale'
  ref: SqlEntityRef | null
  reason: string | null
}
