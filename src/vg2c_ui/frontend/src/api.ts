import type {
  BatchTranslationResponse,
  CommandBatch,
  CommandPreview,
  CommandResult,
  CsvPreview,
  WorkflowDocument,
} from './types'

export class ApiError extends Error {
  constructor(message: string, readonly status: number) {
    super(message)
  }
}

async function json<T>(response: Response): Promise<T> {
  if (!response.ok) {
    const body = await response.json().catch(() => ({ detail: response.statusText }))
    const detail = typeof body.detail === 'string' ? body.detail : JSON.stringify(body.detail)
    throw new ApiError(detail || response.statusText, response.status)
  }
  return response.json() as Promise<T>
}

export async function translateBatch(sourcePaths: string[]): Promise<BatchTranslationResponse> {
  return json(await fetch('/api/translations/batch', {
    method: 'POST',
    headers: { 'Content-Type': 'application/json' },
    body: JSON.stringify({ source_paths: sourcePaths }),
  }))
}

export async function previewCommands(batch: CommandBatch): Promise<CommandPreview> {
  return json(await fetch('/api/commands/preview-diff', {
    method: 'POST',
    headers: { 'Content-Type': 'application/json' },
    body: JSON.stringify(batch),
  }))
}

export async function openWorkflow(
  sourcePath: string,
  outputPath?: string,
): Promise<WorkflowDocument> {
  return json(await fetch('/api/commands/get-workflow', {
    method: 'POST',
    headers: { 'Content-Type': 'application/json' },
    body: JSON.stringify({ source_path: sourcePath, output_path: outputPath ?? null }),
  }))
}

export async function applyCommands(batch: CommandBatch): Promise<CommandResult> {
  return json(await fetch('/api/commands/apply-changes', {
    method: 'POST',
    headers: { 'Content-Type': 'application/json' },
    body: JSON.stringify(batch),
  }))
}

export async function previewCsv(sourcePath: string, csvPath: string): Promise<CsvPreview> {
  return json(await fetch('/api/commands/preview-csv', {
    method: 'POST',
    headers: { 'Content-Type': 'application/json' },
    body: JSON.stringify({ source_path: sourcePath, csv_path: csvPath }),
  }))
}
