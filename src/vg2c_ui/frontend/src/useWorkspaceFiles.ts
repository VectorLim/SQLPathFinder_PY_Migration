import { useCallback, useEffect, useState } from 'react'

import { getWorkspaceUploadPolicy, listWorkspaceFiles } from './api'
import type { WorkspaceFileView, WorkspaceUploadPolicyView } from './contracts.generated'

export function useWorkspaceFiles() {
  const [files, setFiles] = useState<WorkspaceFileView[]>([])
  const [policy, setPolicy] = useState<WorkspaceUploadPolicyView | null>(null)
  const [loading, setLoading] = useState(true)
  const [inventoryError, setInventoryError] = useState<Error | null>(null)
  const [policyError, setPolicyError] = useState<Error | null>(null)

  const refresh = useCallback(async () => {
    setLoading(true)
    try {
      const next = await listWorkspaceFiles()
      setFiles(next)
      setInventoryError(null)
      return next
    } catch (reason) {
      const nextError = reason instanceof Error ? reason : new Error('Could not load workspace files')
      setInventoryError(nextError)
      throw nextError
    } finally {
      setLoading(false)
    }
  }, [])

  useEffect(() => {
    void refresh().catch(() => undefined)
  }, [refresh])

  useEffect(() => {
    let cancelled = false
    void getWorkspaceUploadPolicy()
      .then((next) => {
        if (cancelled) return
        setPolicy(next)
        setPolicyError(null)
      })
      .catch((reason) => {
        if (cancelled) return
        setPolicyError(reason instanceof Error ? reason : new Error('Could not load workspace upload policy'))
      })
    return () => { cancelled = true }
  }, [])

  return { files, policy, loading, inventoryError, policyError, refresh }
}
