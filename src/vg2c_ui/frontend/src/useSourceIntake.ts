import { useEffect, useRef, useState } from 'react'

import { uploadWorkspaceFile } from './api'
import type {
  BatchTranslationResponse,
  DiagnosticView,
  WorkspaceFileView,
  WorkspaceUploadPolicyView,
} from './contracts.generated'

export type UploadQueueStatus = 'queued' | 'uploading' | 'uploaded' | 'failed'
export type IntakeNotice = { tone: 'info' | 'error'; message: string }

export interface UploadQueueItem {
  id: number
  file: File
  path: string
  status: UploadQueueStatus
  error: string | null
  retryable: boolean
  saved: WorkspaceFileView | null
}

interface Args {
  files: WorkspaceFileView[]
  loadingFiles: boolean
  inventoryError: Error | null
  policy: WorkspaceUploadPolicyView | null
  refreshFiles: () => Promise<WorkspaceFileView[]>
  translateSources: (paths: string[]) => Promise<BatchTranslationResponse>
}

export interface SourceIntakeController {
  sources: WorkspaceFileView[]
  selectedSources: string[]
  queue: UploadQueueItem[]
  queuedCount: number
  completedCount: number
  hasGeneratedFiles: boolean
  notice: IntakeNotice | null
  translationDiagnostics: DiagnosticView[]
  policy: WorkspaceUploadPolicyView | null
  loadingFiles: boolean
  uploading: boolean
  translating: boolean
  canStage: boolean
  canUploadQueued: boolean
  canTranslate: boolean
  fileInputRef: React.RefObject<HTMLInputElement | null>
  folderInputRef: React.RefObject<HTMLInputElement | null>
  openFiles: () => void
  openFolder: () => void
  stageFiles: (files: File[]) => void
  uploadQueued: () => Promise<void>
  retry: (id: number) => Promise<void>
  remove: (id: number) => void
  clearCompleted: () => void
  toggleSource: (path: string, selected: boolean) => void
  translateSelected: () => Promise<void>
}

export function useSourceIntake({ files, loadingFiles, inventoryError, policy, refreshFiles, translateSources }: Args): SourceIntakeController {
  const [queue, setQueue] = useState<UploadQueueItem[]>([])
  const [selectedSources, setSelectedSources] = useState<string[]>([])
  const [uploading, setUploading] = useState(false)
  const [translating, setTranslating] = useState(false)
  const [localNotice, setLocalNotice] = useState<IntakeNotice | null>(null)
  const [translationDiagnostics, setTranslationDiagnostics] = useState<DiagnosticView[]>([])
  const nextId = useRef(1)
  const sourceSelectionInitialized = useRef(false)
  const fileInputRef = useRef<HTMLInputElement | null>(null)
  const folderInputRef = useRef<HTMLInputElement | null>(null)
  const sources = files.filter((file) => file.translatable)
  const sourceSignature = sources.map((file) => file.path).join('|')
  const queuedCount = queue.filter((item) => item.status === 'queued').length
  const completedCount = queue.filter((item) => item.status === 'uploaded').length
  const hasGeneratedFiles = files.some((file) => file.role === 'generated')
  const inventoryReady = !loadingFiles && !inventoryError
  const busy = uploading || translating
  const canStage = Boolean(policy) && inventoryReady && !busy
  const canUploadQueued = queuedCount > 0 && inventoryReady && !busy
  const canTranslate = selectedSources.length > 0 && inventoryReady && !busy
  const notice = inventoryError
    ? { tone: 'error' as const, message: inventoryError.message || 'Could not load workspace files.' }
    : localNotice

  useEffect(() => {
    if (loadingFiles) return
    const available = new Set(sources.map((file) => file.path))
    setSelectedSources((current) => {
      if (!sourceSelectionInitialized.current) {
        sourceSelectionInitialized.current = true
        return [...available]
      }
      return current.filter((path) => available.has(path))
    })
  }, [loadingFiles, sourceSignature])

  function openFiles() {
    if (!canStage) return
    fileInputRef.current?.click()
  }

  function openFolder() {
    if (!canStage) return
    folderInputRef.current?.click()
  }

  function stageFiles(incoming: File[]) {
    if (!incoming.length || !canStage || !policy) return
    setLocalNotice(null)
    setQueue((current) => {
      const next = [...current]
      const reserved = next.filter((item) => item.status === 'queued' || item.status === 'uploading')
      let inputCount = files.filter((file) => file.role !== 'generated').length + reserved.length
      let workspaceBytes = files.reduce((total, file) => total + file.size_bytes, 0)
        + reserved.reduce((total, item) => total + item.file.size, 0)
      const knownPaths = new Set([
        ...files.map((file) => file.path),
        ...next.map((item) => targetPath(item.path)),
      ])

      for (const file of incoming) {
        const path = relativePath(file)
        const error = validateStagedFile(file, path, policy, knownPaths, inputCount, workspaceBytes)
        next.push({
          id: nextId.current++,
          file,
          path,
          status: error ? 'failed' : 'queued',
          error,
          retryable: false,
          saved: null,
        })
        if (!error) {
          knownPaths.add(targetPath(path))
          inputCount += 1
          workspaceBytes += file.size
        }
      }
      return next
    })
  }

  async function uploadItems(items: UploadQueueItem[]) {
    if (!items.length || !inventoryReady || busy) return
    setUploading(true)
    setLocalNotice(null)
    let succeeded = 0
    let failed = 0
    const newSources: string[] = []
    try {
      for (const item of items) {
        setQueue((current) => updateQueueItem(current, item.id, { status: 'uploading', error: null, retryable: false }))
        try {
          const saved = await uploadWorkspaceFile(item.file)
          succeeded += 1
          if (saved.translatable) newSources.push(saved.path)
          setQueue((current) => updateQueueItem(current, item.id, { status: 'uploaded', saved, error: null, retryable: false }))
        } catch (reason) {
          failed += 1
          setQueue((current) => updateQueueItem(current, item.id, {
            status: 'failed',
            error: errorMessage(reason, 'Upload failed'),
            retryable: true,
            saved: null,
          }))
        }
      }
      await refreshFiles().catch(() => undefined)
      if (newSources.length) setSelectedSources((current) => [...new Set([...current, ...newSources])])
      setLocalNotice(failed
        ? { tone: 'error', message: `${succeeded} uploaded; ${failed} failed. Review the upload queue.` }
        : { tone: 'info', message: `${succeeded} file${succeeded === 1 ? '' : 's'} uploaded.` })
    } finally {
      setUploading(false)
    }
  }

  async function uploadQueued() {
    if (!canUploadQueued) return
    await uploadItems(queue.filter((item) => item.status === 'queued'))
  }

  async function retry(id: number) {
    if (!policy || !inventoryReady || busy) return
    const item = queue.find((candidate) => candidate.id === id)
    if (!item || item.status !== 'failed' || !item.retryable) return
    const refreshed = await refreshFiles().catch(() => files)
    const knownPaths = new Set(refreshed.map((file) => file.path))
    const inputCount = refreshed.filter((file) => file.role !== 'generated').length
    const workspaceBytes = refreshed.reduce((total, file) => total + file.size_bytes, 0)
    const validationError = validateStagedFile(item.file, item.path, policy, knownPaths, inputCount, workspaceBytes)
    if (validationError) {
      setQueue((current) => updateQueueItem(current, id, { error: validationError, retryable: false }))
      setLocalNotice({ tone: 'error', message: validationError })
      return
    }
    await uploadItems([item])
  }

  function remove(id: number) {
    setQueue((current) => current.filter((item) => item.id !== id || item.status === 'uploading' || item.status === 'uploaded'))
  }

  function clearCompleted() {
    setQueue((current) => current.filter((item) => item.status !== 'uploaded'))
  }

  function toggleSource(path: string, selected: boolean) {
    setSelectedSources((current) => selected
      ? [...new Set([...current, path])]
      : current.filter((item) => item !== path))
  }

  async function translateSelected() {
    if (!canTranslate) return
    setTranslating(true)
    setLocalNotice(null)
    setTranslationDiagnostics([])
    try {
      const response = await translateSources(selectedSources)
      await refreshFiles().catch(() => undefined)
      setTranslationDiagnostics(response.diagnostics)
      if (response.diagnostics.length) {
        setLocalNotice({
          tone: 'error',
          message: `${response.documents.length} translated; ${response.diagnostics.length} source${response.diagnostics.length === 1 ? '' : 's'} failed.`,
        })
      } else {
        setLocalNotice({
          tone: 'info',
          message: response.documents.length
            ? `${response.documents.length} translated script${response.documents.length === 1 ? '' : 's'} ready.`
            : 'No scripts were translated.',
        })
      }
    } catch (reason) {
      setLocalNotice({ tone: 'error', message: errorMessage(reason, 'Translation failed') })
    } finally {
      setTranslating(false)
    }
  }

  return {
    sources,
    selectedSources,
    queue,
    queuedCount,
    completedCount,
    hasGeneratedFiles,
    notice,
    translationDiagnostics,
    policy,
    loadingFiles,
    uploading,
    translating,
    canStage,
    canUploadQueued,
    canTranslate,
    fileInputRef,
    folderInputRef,
    openFiles,
    openFolder,
    stageFiles,
    uploadQueued,
    retry,
    remove,
    clearCompleted,
    toggleSource,
    translateSelected,
  }
}

function validateStagedFile(
  file: File,
  path: string,
  policy: WorkspaceUploadPolicyView,
  knownPaths: Set<string>,
  inputCount: number,
  workspaceBytes: number,
): string | null {
  const suffix = fileSuffix(file.name)
  if (!policy.allowed_upload_suffixes.includes(suffix)) {
    return `Unsupported file type. Allowed: ${policy.allowed_upload_suffixes.join(', ')}`
  }
  if (file.size > policy.max_upload_bytes) return `File exceeds the ${formatBytes(policy.max_upload_bytes)} per-file limit.`
  if (knownPaths.has(targetPath(path))) return `A workspace file already exists at ${path}.`
  if (inputCount >= policy.max_file_count) return `Workspace upload file limit (${policy.max_file_count}) reached.`
  if (workspaceBytes + file.size > policy.max_workspace_bytes) return `Workspace storage limit (${formatBytes(policy.max_workspace_bytes)}) would be exceeded.`
  return null
}

function updateQueueItem(current: UploadQueueItem[], id: number, patch: Partial<UploadQueueItem>): UploadQueueItem[] {
  return current.map((item) => item.id === id ? { ...item, ...patch } : item)
}

function relativePath(file: File): string {
  return (file.webkitRelativePath || file.name).replaceAll('\\', '/')
}

function targetPath(path: string): string {
  return `inputs/${path}`
}

function fileSuffix(name: string): string {
  const index = name.lastIndexOf('.')
  return index >= 0 ? name.slice(index).toLowerCase() : ''
}

export function formatBytes(value: number): string {
  if (value < 1024) return `${value} B`
  if (value < 1024 * 1024) return `${Math.round(value / 1024)} KB`
  return `${Math.round(value / (1024 * 1024))} MB`
}

function errorMessage(error: unknown, fallback: string): string {
  return error instanceof Error ? error.message : fallback
}
