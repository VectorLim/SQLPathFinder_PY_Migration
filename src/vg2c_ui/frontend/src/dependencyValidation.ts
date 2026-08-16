import { effectiveStepFiles } from './sql/operation'
import type { CsvArtifact, StepNode, WorkflowDocument } from './types'

export interface DocumentDependencyState {
  document: WorkflowDocument
  values: Record<string, unknown>
}

export type DependencyDiagnosticCode = 'BROKEN_DEPENDENCY' | 'MISSING_INPUT' | 'DUPLICATE_OUTPUT'

export interface DependencyDiagnostic {
  severity: 'warning' | 'error'
  code: DependencyDiagnosticCode
  message: string
  artifact: string
  operationId: string
  relatedOperationId: string | null
}

export interface DependencyAnalysis {
  artifacts: CsvArtifact[]
  diagnostics: DependencyDiagnostic[]
}

interface ProducerRef {
  documentId: string
  stepId: string
  step: StepNode
  path: string
}

export function analyzeDependencies(
  active: WorkflowDocument,
  states: DocumentDependencyState[] = [{ document: active, values: {} }],
): DependencyAnalysis {
  const normalizedStates = ensureActiveState(active, states)
  const activeState = normalizedStates.find((state) => state.document.id === active.id)!
  const currentProducers = producerIndex(normalizedStates, true)
  const baselineProducers = producerIndex(normalizedStates, false)
  const artifacts = buildEffectiveArtifacts(activeState)
  const diagnostics: DependencyDiagnostic[] = []

  for (const [key, producers] of currentProducers) {
    if (producers.length <= 1) continue
    const path = producers[0].path
    for (const producer of producers) {
      if (producer.documentId !== active.id) continue
      diagnostics.push({
        severity: 'error',
        code: 'DUPLICATE_OUTPUT',
        message: `${path} is produced by ${producers.length} operations. Output filenames must be unique across the open workflow set.`,
        artifact: path,
        operationId: producer.stepId,
        relatedOperationId: producers.find((item) => item.stepId !== producer.stepId)?.stepId ?? null,
      })
    }
  }

  for (const step of active.steps) {
    const effective = effectiveStepFiles(step, activeState.values)
    const baselineInputs = step.csv_inputs
    for (let index = 0; index < effective.inputs.length; index += 1) {
      const path = effective.inputs[index]
      const key = artifactKey(path)
      if (currentProducers.get(key)?.length) continue

      const baselinePath = baselineInputs[index] ?? path
      const previous = baselineProducers.get(artifactKey(baselinePath)) ?? []
      if (!previous.length) continue
      const previousForConsumer = previous.filter((producer) => (
        producer.documentId !== active.id || producer.step.block_index < step.block_index
      ))
      if (!previousForConsumer.length) continue

      const related = previousForConsumer[0]
      const relatedState = normalizedStates.find((state) => state.document.id === related.documentId)
      const relatedStep = relatedState?.document.steps.find((item) => item.id === related.stepId)
      const nowOutputs = relatedStep && relatedState
        ? effectiveStepFiles(relatedStep, relatedState.values).outputs
        : []
      const changedOutput = nowOutputs.length && !nowOutputs.some((candidate) => artifactKey(candidate) === key)
      diagnostics.push({
        severity: 'error',
        code: changedOutput ? 'BROKEN_DEPENDENCY' : 'MISSING_INPUT',
        message: changedOutput
          ? `Missing input: ${path}. The producer previously referenced by this operation now outputs ${nowOutputs.join(', ')}.`
          : `Missing input: ${path}. Its previously known producer is no longer available.`,
        artifact: path,
        operationId: step.id,
        relatedOperationId: related.stepId,
      })
    }
  }

  return { artifacts, diagnostics: dedupeDiagnostics(diagnostics) }
}

export function diagnosticsForOperation(
  analysis: DependencyAnalysis,
  operationId: string,
): DependencyDiagnostic[] {
  return analysis.diagnostics.filter((diagnostic) => diagnostic.operationId === operationId)
}

export function artifactKey(path: string): string {
  return path.trim().replaceAll('\\', '/').replace(/^\.\//, '').toLocaleLowerCase()
}

function ensureActiveState(active: WorkflowDocument, states: DocumentDependencyState[]): DocumentDependencyState[] {
  const existing = states.find((state) => state.document.id === active.id)
  return existing ? states : [...states, { document: active, values: {} }]
}

function producerIndex(states: DocumentDependencyState[], effective: boolean): Map<string, ProducerRef[]> {
  const result = new Map<string, ProducerRef[]>()
  for (const state of states) {
    for (const step of state.document.steps) {
      const outputs = effective ? effectiveStepFiles(step, state.values).outputs : step.csv_outputs
      for (const path of outputs) {
        if (!path.trim()) continue
        const key = artifactKey(path)
        result.set(key, [
          ...(result.get(key) ?? []),
          { documentId: state.document.id, stepId: step.id, step, path },
        ])
      }
    }
  }
  return result
}

function buildEffectiveArtifacts(state: DocumentDependencyState): CsvArtifact[] {
  const byKey = new Map<string, CsvArtifact>()
  const originalByKey = new Map(state.document.artifacts.map((artifact) => [artifactKey(artifact.path), artifact]))
  const stepsById = new Map(state.document.steps.map((step) => [step.id, step]))

  function ensure(path: string): CsvArtifact {
    const key = artifactKey(path)
    const existing = byKey.get(key)
    if (existing) return existing
    const original = originalByKey.get(key)
    const artifact: CsvArtifact = {
      id: original?.id ?? `effective-${encodeURIComponent(key)}`,
      path,
      label: baseName(path),
      conditional: original?.conditional ?? false,
      in_loop: original?.in_loop ?? false,
      producer_step_ids: [],
      consumer_step_ids: [],
      order_valid: true,
    }
    byKey.set(key, artifact)
    return artifact
  }

  for (const step of state.document.steps) {
    const files = effectiveStepFiles(step, state.values)
    for (const path of files.outputs) {
      const artifact = ensure(path)
      if (!artifact.producer_step_ids.includes(step.id)) artifact.producer_step_ids.push(step.id)
    }
    for (const path of files.inputs) {
      const artifact = ensure(path)
      if (!artifact.consumer_step_ids.includes(step.id)) artifact.consumer_step_ids.push(step.id)
    }
  }

  for (const artifact of byKey.values()) {
    const producerIndexes = artifact.producer_step_ids
      .map((id) => stepsById.get(id)?.block_index)
      .filter((value): value is number => value !== undefined)
    const consumerIndexes = artifact.consumer_step_ids
      .map((id) => stepsById.get(id)?.block_index)
      .filter((value): value is number => value !== undefined)
    artifact.order_valid = !producerIndexes.length || !consumerIndexes.length
      || Math.min(...producerIndexes) <= Math.min(...consumerIndexes)
  }

  return [...byKey.values()].sort((left, right) => left.path.localeCompare(right.path))
}

function dedupeDiagnostics(diagnostics: DependencyDiagnostic[]): DependencyDiagnostic[] {
  const seen = new Set<string>()
  return diagnostics.filter((diagnostic) => {
    const key = `${diagnostic.code}:${diagnostic.operationId}:${artifactKey(diagnostic.artifact)}`
    if (seen.has(key)) return false
    seen.add(key)
    return true
  })
}

function baseName(path: string): string {
  return path.split(/[\\/]/).filter(Boolean).at(-1) ?? path
}
