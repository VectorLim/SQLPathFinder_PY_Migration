import {
  analyzeDependencies,
  artifactKey,
  type DependencyDiagnostic,
  type DocumentDependencyState,
} from './dependencyValidation'
import { baseName } from './operationLabels'
import { effectiveStepFiles } from './sql/operation'
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
  diagnostics: DependencyDiagnostic[]
}

export function deriveFileFlow(
  active: WorkflowDocument,
  documents: WorkflowDocument[],
  values: Record<string, unknown> = {},
): FileFlow {
  const states = dependencyStates(active, documents, values)
  const analysis = analyzeDependencies(active, states)
  const inputs = analysis.artifacts.filter(isExternalInput)
  const outputs = analysis.artifacts.filter((artifact) => artifact.producer_step_ids.length > 0)
  const inputKeys = new Set(inputs.map((artifact) => artifactKey(artifact.path)))
  const outputKeys = new Set(outputs.map((artifact) => artifactKey(artifact.path)))

  const upstream = documents
    .filter((document) => document.id !== active.id)
    .flatMap((document) => {
      const paths = analyzeDependencies(document, states).artifacts
        .filter((artifact) => artifact.producer_step_ids.length > 0 && inputKeys.has(artifactKey(artifact.path)))
        .map((artifact) => artifact.path)
      return paths.length ? [dependency(document, paths)] : []
    })

  const downstream = documents
    .filter((document) => document.id !== active.id)
    .flatMap((document) => {
      const paths = analyzeDependencies(document, states).artifacts
        .filter((artifact) => isExternalInput(artifact) && outputKeys.has(artifactKey(artifact.path)))
        .map((artifact) => artifact.path)
      return paths.length ? [dependency(document, paths)] : []
    })

  return { inputs, outputs, upstream, downstream, diagnostics: analysis.diagnostics }
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
    [...effectiveStepFiles(step, editedValues).inputs, ...effectiveStepFiles(step, editedValues).outputs]
      .some((candidate) => artifactKey(candidate) === key)
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

export { artifactKey }

function dependencyStates(
  active: WorkflowDocument,
  documents: WorkflowDocument[],
  values: Record<string, unknown>,
): DocumentDependencyState[] {
  const included = documents.some((document) => document.id === active.id) ? documents : [...documents, active]
  return included.map((document) => ({ document, values: document.id === active.id ? values : {} }))
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
