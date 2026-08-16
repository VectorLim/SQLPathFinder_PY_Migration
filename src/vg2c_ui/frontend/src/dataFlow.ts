import { baseName } from './operationLabels'
import type { CsvArtifact, StepNode, WorkflowDocument } from './types'

export type HeaderSource = 'declared' | 'detected' | 'unknown' | 'loading'

export interface HeaderInfo {
  columns: string[]
  source: HeaderSource
}

export interface FileDependency {
  documentId: string
  fileName: string
  sourcePath: string
  artifactPaths: string[]
}

export interface FileFlow {
  inputs: CsvArtifact[]
  outputs: CsvArtifact[]
  upstream: FileDependency[]
  downstream: FileDependency[]
}

export function deriveFileFlow(
  active: WorkflowDocument,
  documents: WorkflowDocument[],
): FileFlow {
  const inputs = active.artifacts.filter(isExternalInput)
  const outputs = active.artifacts.filter((artifact) => artifact.producer_step_ids.length > 0)
  const inputKeys = new Set(inputs.map((artifact) => artifactKey(artifact.path)))
  const outputKeys = new Set(outputs.map((artifact) => artifactKey(artifact.path)))

  const upstream = documents
    .filter((document) => document.id !== active.id)
    .flatMap((document) => {
      const paths = document.artifacts
        .filter((artifact) => artifact.producer_step_ids.length > 0 && inputKeys.has(artifactKey(artifact.path)))
        .map((artifact) => artifact.path)
      return paths.length ? [dependency(document, paths)] : []
    })

  const downstream = documents
    .filter((document) => document.id !== active.id)
    .flatMap((document) => {
      const paths = document.artifacts
        .filter((artifact) => isExternalInput(artifact) && outputKeys.has(artifactKey(artifact.path)))
        .map((artifact) => artifact.path)
      return paths.length ? [dependency(document, paths)] : []
    })

  return { inputs, outputs, upstream, downstream }
}

export function headerCacheKey(document: WorkflowDocument, path: string): string {
  return `${document.id}::${artifactKey(path)}`
}

export function declaredHeadersForPath(
  document: WorkflowDocument,
  path: string,
  editedValues: Record<string, unknown> = {},
): string[] {
  const key = artifactKey(path)
  const relatedSteps = document.steps.filter((step) => (
    [...step.csv_inputs, ...step.csv_outputs].some((candidate) => artifactKey(candidate) === key)
  ))
  for (const step of relatedSteps) {
    const header = headerFromStep(step, editedValues)
    if (header.length) return header
  }
  return []
}

export function displayHeaderInfo(
  document: WorkflowDocument,
  path: string,
  cache: Record<string, HeaderInfo>,
  editedValues: Record<string, unknown> = {},
): HeaderInfo {
  const declared = declaredHeadersForPath(document, path, editedValues)
  if (declared.length) return { columns: declared, source: 'declared' }
  return cache[headerCacheKey(document, path)] ?? { columns: [], source: 'unknown' }
}

export function artifactKey(path: string): string {
  return path.trim().replaceAll('\\', '/').replace(/^\.\//, '').toLocaleLowerCase()
}

function isExternalInput(artifact: CsvArtifact): boolean {
  return artifact.consumer_step_ids.length > 0 && artifact.producer_step_ids.length === 0
}

function dependency(document: WorkflowDocument, paths: string[]): FileDependency {
  return {
    documentId: document.id,
    fileName: baseName(document.output_path || document.source_path),
    sourcePath: document.source_path,
    artifactPaths: [...new Set(paths)].sort(),
  }
}

function headerFromStep(step: StepNode, editedValues: Record<string, unknown>): string[] {
  const candidates = step.parameters.filter((parameter) => (
    /(^|_)(header|headers|columns|fieldnames|fields)($|_)/i.test(parameter.name)
  ))
  for (const parameter of candidates) {
    const value = Object.hasOwn(editedValues, parameter.id)
      ? editedValues[parameter.id]
      : parameter.value
    const parsed = parseHeaderValue(value)
    if (parsed.length) return parsed
  }
  return []
}

function parseHeaderValue(value: unknown): string[] {
  if (Array.isArray(value)) {
    return value.map(String).map((item) => item.trim()).filter(Boolean)
  }
  if (typeof value !== 'string') return []
  const trimmed = value.trim()
  if (!trimmed) return []
  try {
    const parsed = JSON.parse(trimmed) as unknown
    if (Array.isArray(parsed)) return parsed.map(String).map((item) => item.trim()).filter(Boolean)
  } catch {
    // Some generated literals are plain comma-separated text rather than JSON.
  }
  if (!trimmed.includes(',')) return []
  return trimmed.split(',').map((item) => item.trim()).filter(Boolean)
}
