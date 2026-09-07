export interface SqlMetadataContext {
  sql: string
  sources: string[]
}

export interface SqlAttributeOption {
  expression: string
  label: string
  source?: string
  sources?: string[]
  dataType?: string
  description?: string
}

export interface SqlSourceOption {
  source: string
  label: string
  description?: string
}

export interface SqlJoinCandidate {
  source: string
  label: string
  joinTypes?: string[]
}

export interface SqlJoinKeyOption {
  left: string
  right: string
  operator?: string
}

export interface SqlFilterValueOption {
  value: string
  label?: string
}

export interface SqlSchemaInfo {
  source: string
  attributes: SqlAttributeOption[]
}

export interface SqlMetadataCapabilities {
  attributes: boolean
  sources: boolean
  joinTargets: boolean
  joinKeys: boolean
  filterValues: boolean
  schema: boolean
}

export interface SqlMetadataProvider {
  capabilities(context: SqlMetadataContext): Promise<Partial<SqlMetadataCapabilities>>
  getAvailableAttributes?(context: SqlMetadataContext): Promise<SqlAttributeOption[]>
  getAvailableSources?(context: SqlMetadataContext): Promise<SqlSourceOption[]>
  getJoinCandidates?(context: SqlMetadataContext): Promise<SqlJoinCandidate[]>
  getJoinKeys?(context: SqlMetadataContext, leftSource: string, rightSource: string): Promise<SqlJoinKeyOption[]>
  getFilterValues?(context: SqlMetadataContext, expression: string): Promise<SqlFilterValueOption[]>
  getSchema?(context: SqlMetadataContext, source: string): Promise<SqlSchemaInfo | null>
}

const NONE: SqlMetadataCapabilities = {
  attributes: false,
  sources: false,
  joinTargets: false,
  joinKeys: false,
  filterValues: false,
  schema: false,
}

export const unavailableSqlMetadataProvider: SqlMetadataProvider = {
  async capabilities() {
    return NONE
  },
}

export async function loadSqlMetadataCapabilities(
  provider: SqlMetadataProvider,
  context: SqlMetadataContext,
): Promise<SqlMetadataCapabilities> {
  const capabilities = await provider.capabilities(context)
  return { ...NONE, ...capabilities }
}
