import { useCallback, useEffect, useState } from 'react'

import { getWorkspaceUploadPolicy, listWorkspaceFiles } from './api'
import type { WorkspaceFileView, WorkspaceUploadPolicyView } from './contracts.generated'

export function useWorkspaceFiles() {
  const [files, setFiles] = useState<WorkspaceFileView[]>([])
  const [policy, setPolicy] = useState<WorkspaceUploadPolicyView | null>(null)
  const [loading, setLoading] = useState(true)
  const [error, setError] = useState<Error | null>(null)

  const refresh = useCallback(async () => {
    setLoading(true)
    try {
      const next = await listWorkspaceFiles()
      setFiles(next)
      setError(null)
      return next
    } catch (reason) {
      const nextError = reason instanceof Error ? reason : new Error('Could not load workspace files')
      setError(nextError)
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
      .then((next) => { if (!cancelled) setPolicy(next) })
      .catch((reason) => {
        if (cancelled) return
        setError(reason instanceof Error ? reason : new Error('Could not load workspace upload policy'))
      })
    return () => { cancelled = true }
  }, [])

  return { files, policy, loading, error, refresh }
}
