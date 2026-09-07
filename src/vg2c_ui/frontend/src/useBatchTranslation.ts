import { useCallback, useState } from 'react'

import { translateBatch } from './api'
import type { DiagnosticView, DocumentView } from './contracts.generated'
import { prepareSourceFiles } from './fileSelection'
import { useNotifications } from './NotificationProvider'

export type BatchFileStatus = 'translating' | 'success' | 'error'

export interface BatchProgressItem {
  name: string
  status: BatchFileStatus
  message?: string
}

export interface BatchProgress {
  items: BatchProgressItem[]
  complete: boolean
}

interface Options {
  onDocuments: (documents: DocumentView[]) => void
}

export function useBatchTranslation({ onDocuments }: Options) {
  const { notify } = useNotifications()
  const [busy, setBusy] = useState(false)
  const [progress, setProgress] = useState<BatchProgress | null>(null)

  const translateFiles = useCallback(async (files: File[]) => {
    if (!files.length || busy) return
    setBusy(true)

    const prepared = await prepareSourceFiles(files)
    const initialItems: BatchProgressItem[] = [
      ...prepared.accepted.map((file) => ({ name: file.name, status: 'translating' as const })),
      ...prepared.rejected.map((file) => ({ name: file.name, status: 'error' as const, message: file.message })),
    ]
    setProgress({ items: initialItems, complete: prepared.accepted.length === 0 })

    for (const rejected of prepared.rejected) {
      notify({
        type: 'error',
        title: 'File not translated',
        message: `${rejected.name}: ${rejected.message}`,
      })
    }

    if (!prepared.accepted.length) {
      setBusy(false)
      return
    }

    try {
      const response = await translateBatch(prepared.accepted)
      const documents: DocumentView[] = []
      const resultItems: BatchProgressItem[] = []

      for (const result of response.results) {
        if (result.status === 'success' && result.document) {
          documents.push(result.document)
          resultItems.push({ name: result.file_name, status: 'success' })
          continue
        }

        const message = diagnosticMessage(result.diagnostics)
        resultItems.push({ name: result.file_name, status: 'error', message })
        notify({
          type: 'error',
          title: 'Translation failed',
          message: `${result.file_name}: ${message}`,
        })
      }

      onDocuments(documents)
      setProgress({
        items: [...resultItems, ...prepared.rejected.map((file) => ({
          name: file.name,
          status: 'error' as const,
          message: file.message,
        }))],
        complete: true,
      })

      if (documents.length) {
        notify({
          type: 'success',
          title: documents.length === 1 ? 'Translation complete' : 'Translations complete',
          message: `${documents.length} of ${files.length} selected file${files.length === 1 ? '' : 's'} translated successfully.`,
        })
      }
    } catch (error) {
      const message = error instanceof Error ? error.message : 'The translation request failed.'
      setProgress({
        items: initialItems.map((item) => item.status === 'translating'
          ? { ...item, status: 'error' as const, message }
          : item),
        complete: true,
      })
      notify({ type: 'error', title: 'Translation request failed', message })
    } finally {
      setBusy(false)
    }
  }, [busy, notify, onDocuments])

  return {
    busy,
    progress,
    translateFiles,
    dismissProgress: () => setProgress(null),
  }
}

function diagnosticMessage(diagnostics: DiagnosticView[]): string {
  if (!diagnostics.length) return 'Translation failed without a diagnostic.'
  const primary = diagnostics.find((item) => item.level === 'error') ?? diagnostics[0]
  return primary.message
}
