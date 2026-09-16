import { useCallback, useEffect, useState } from 'react'

import { listWorkspaceFiles } from './api'
import type { WorkspaceFileView } from './contracts.generated'

export function useWorkspaceFiles() {
  const [files, setFiles] = useState<WorkspaceFileView[]>([])
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

  return { files, loading, error, refresh }
}
