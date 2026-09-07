import { useEffect, useState } from 'react'

import type { SqlEditableModel } from './model'
import {
  loadSqlMetadataCapabilities,
  type SqlAttributeOption,
  type SqlFilterValueOption,
  type SqlJoinCandidate,
  type SqlJoinKeyOption,
  type SqlMetadataCapabilities,
  type SqlMetadataContext,
  type SqlMetadataProvider,
  type SqlSourceOption,
} from './metadata'

const NONE: SqlMetadataCapabilities = {
  attributes: false,
  sources: false,
  joinTargets: false,
  joinKeys: false,
  filterValues: false,
  schema: false,
}

export interface SqlMetadataSnapshot {
  capabilities: SqlMetadataCapabilities
  attributes: SqlAttributeOption[]
  sources: SqlSourceOption[]
  joinCandidates: SqlJoinCandidate[]
  joinKeys: Record<string, SqlJoinKeyOption[]>
  joinCandidateKeys: Record<string, SqlJoinKeyOption[]>
  filterValues: Record<string, SqlFilterValueOption[]>
}

const EMPTY: SqlMetadataSnapshot = {
  capabilities: NONE,
  attributes: [],
  sources: [],
  joinCandidates: [],
  joinKeys: {},
  joinCandidateKeys: {},
  filterValues: {},
}

export function useSqlMetadata(
  provider: SqlMetadataProvider,
  model: SqlEditableModel,
): SqlMetadataSnapshot {
  const [snapshot, setSnapshot] = useState<SqlMetadataSnapshot>(EMPTY)

  useEffect(() => {
    let cancelled = false
    const context: SqlMetadataContext = {
      sql: model.source,
      sources: model.sources.map((item) => item.expression),
    }

    void (async () => {
      try {
        const capabilities = await loadSqlMetadataCapabilities(provider, context)
        const attributes = capabilities.attributes && provider.getAvailableAttributes
          ? await provider.getAvailableAttributes(context)
          : []
        const sources = capabilities.sources && provider.getAvailableSources
          ? await provider.getAvailableSources(context)
          : []
        const joinCandidates = capabilities.joinTargets && provider.getJoinCandidates
          ? await provider.getJoinCandidates(context)
          : []
        const baseSource = model.sources.find((item) => item.kind === 'from')?.expression ?? ''
        const joinKeys: Record<string, SqlJoinKeyOption[]> = {}
        const joinCandidateKeys: Record<string, SqlJoinKeyOption[]> = {}
        if (capabilities.joinKeys && provider.getJoinKeys) {
          for (const join of model.joins) {
            joinKeys[join.id] = await provider.getJoinKeys(context, baseSource, join.source)
          }
          for (const candidate of joinCandidates) {
            joinCandidateKeys[candidate.source] = await provider.getJoinKeys(context, baseSource, candidate.source)
          }
        }
        const filterValues: Record<string, SqlFilterValueOption[]> = {}
        if (capabilities.filterValues && provider.getFilterValues) {
          for (const filter of model.filters) {
            if (filter.editable) filterValues[filter.id] = await provider.getFilterValues(context, filter.left)
          }
        }
        if (!cancelled) setSnapshot({ capabilities, attributes, sources, joinCandidates, joinKeys, joinCandidateKeys, filterValues })
      } catch {
        // Metadata is an enhancement. Failure deliberately degrades to the local structured editor.
        if (!cancelled) setSnapshot(EMPTY)
      }
    })()
    return () => { cancelled = true }
  }, [provider, model.source])

  return snapshot
}
