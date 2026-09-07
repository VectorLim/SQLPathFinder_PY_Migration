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
  readOnlyReason: string | null
  span: SqlSpan
}

export interface SqlSource {
  id: string
  expression: string
  kind: 'from' | 'join'
  editable: boolean
  readOnlyReason: string | null
  span: SqlSpan
  joinId: string | null
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
  readOnlyReason: string | null
  span: SqlSpan
  connectorSpan: SqlSpan | null
}

export interface SqlJoin {
  id: string
  joinType: string
  source: string
  predicates: SqlPredicate[]
  editableType: boolean
  editableSource: boolean
  readOnlyReason: string | null
  span: SqlSpan
  typeSpan: SqlSpan
  sourceSpan: SqlSpan
}

export interface SqlEditCapabilities {
  selected: boolean
  filters: boolean
  joins: boolean
  rawSql: true
}

export interface SqlEditableModel {
  source: string
  statementSpan: SqlSpan
  selections: SqlSelection[]
  filters: SqlPredicate[]
  joins: SqlJoin[]
  sources: SqlSource[]
  capabilities: SqlEditCapabilities
  readOnlyReason: string | null
  selectListSpan: SqlSpan | null
  whereClauseSpan: SqlSpan | null
  whereBodySpan: SqlSpan | null
  fromClauseSpan: SqlSpan | null
}

export interface SqlTransformResult {
  sql: string
  model: SqlEditableModel
}

export class SqlEditError extends Error {
  constructor(message: string) {
    super(message)
    this.name = 'SqlEditError'
  }
}
