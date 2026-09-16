import type {
  BatchTranslationResponse,
  ChangeBatch,
  ChangePreviewView,
  ChangeResultView,
  CsvPreviewView,
  DocumentView,
  SqlActionRequest,
  SqlActionResponse,
  SqlModelRequest,
  SqlModelView,
  WorkspaceProjectionRequest,
  WorkspaceProjectionView,
  WorkspaceFileView,
  WorkspaceUploadPolicyView,
} from './contracts.generated'

export class ApiError extends Error {
  constructor(message: string, readonly status: number) {
    super(message)
  }
}

async function post<T>(path: string, body: unknown): Promise<T> {
  const response = await fetch(path, {
    method: 'POST',
    headers: { 'Content-Type': 'application/json' },
    body: JSON.stringify(body),
  })
  if (!response.ok) {
    const payload = await response.json().catch(() => ({ detail: response.statusText }))
    const detail = typeof payload.detail === 'string' ? payload.detail : JSON.stringify(payload.detail)
    throw new ApiError(detail || response.statusText, response.status)
  }
  return response.json() as Promise<T>
}

export function translateBatch(sourcePaths: string[]): Promise<BatchTranslationResponse> {
  return post('/api/translations/batch', { source_paths: sourcePaths, out_dir: null })
}

export function openDocument(sourcePath: string, outputPath?: string): Promise<DocumentView> {
  return post('/api/documents/open', { source_path: sourcePath, output_path: outputPath ?? null })
}

export function previewChanges(batch: ChangeBatch): Promise<ChangePreviewView> {
  return post('/api/changes/preview', batch)
}

export function applyChanges(batch: ChangeBatch): Promise<ChangeResultView> {
  return post('/api/changes/apply', batch)
}

export function projectWorkspace(request: WorkspaceProjectionRequest): Promise<WorkspaceProjectionView> {
  return post('/api/workspace/project', request)
}

export function inspectSql(request: SqlModelRequest): Promise<SqlModelView> {
  return post('/api/sql/inspect', request)
}

export function applySqlAction(request: SqlActionRequest): Promise<SqlActionResponse> {
  return post('/api/sql/apply-action', request)
}

export function previewCsv(sourcePath: string, csvPath: string): Promise<CsvPreviewView> {
  return post('/api/documents/preview-csv', { source_path: sourcePath, csv_path: csvPath })
}

export async function uploadWorkspaceFiles(files: File[]): Promise<WorkspaceFileView[]> {
  const body = new FormData()
  for (const file of files) {
    const relativePath = file.webkitRelativePath || file.name
    body.append('files', file)
    body.append('paths', relativePath)
  }
  const response = await fetch('/api/workspace/files', { method: 'POST', body })
  if (!response.ok) await throwApiError(response)
  return response.json() as Promise<WorkspaceFileView[]>
}

export async function uploadWorkspaceFile(file: File): Promise<WorkspaceFileView> {
  const saved = await uploadWorkspaceFiles([file])
  const result = saved[0]
  if (!result) throw new Error('Upload completed without a saved workspace file.')
  return result
}

export async function listWorkspaceFiles(): Promise<WorkspaceFileView[]> {
  const response = await fetch('/api/workspace/files')
  if (!response.ok) await throwApiError(response)
  return response.json() as Promise<WorkspaceFileView[]>
}

export async function getWorkspaceUploadPolicy(): Promise<WorkspaceUploadPolicyView> {
  const response = await fetch('/api/workspace/policy')
  if (!response.ok) await throwApiError(response)
  return response.json() as Promise<WorkspaceUploadPolicyView>
}

export function workspaceDownloadUrl(path: string): string {
  return `/api/workspace/download/${path.split('/').map(encodeURIComponent).join('/')}`
}

async function throwApiError(response: Response): Promise<never> {
  const payload = await response.json().catch(() => ({ detail: response.statusText }))
  const detail = typeof payload.detail === 'string' ? payload.detail : JSON.stringify(payload.detail)
  throw new ApiError(detail || response.statusText, response.status)
}
