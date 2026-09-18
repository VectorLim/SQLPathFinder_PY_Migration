import type { FileEffectView, OperationView, StepView } from './contracts.generated'

export interface OperationLabel {
  primary: string
  secondary: string | null
}

export function formatOperationLabel(step: StepView, operation: OperationView = step.operations[0], effects: FileEffectView[] = []): OperationLabel {
  const primary = (operation === step.operations[0] ? step.display_label.trim() : '') || operation?.utility.title || 'Operation'
  const owned = effects.filter((effect) => effect.operation_id === operation?.id)
  const outputs = [...new Set(owned.flatMap((effect) => effect.outputs.flatMap((endpoint) => endpoint.path ? [endpoint.path] : [])))]
  const inputs = [...new Set(owned.flatMap((effect) => effect.inputs.flatMap((endpoint) => endpoint.path ? [endpoint.path] : [])))]
  if (outputs.length) return { primary, secondary: concisePaths(outputs, 'output', 'outputs') }
  if (inputs.length) return { primary, secondary: concisePaths(inputs, 'input', 'inputs') }
  return { primary, secondary: null }
}

export function baseName(path: string): string {
  return path.split(/[\\/]/).filter(Boolean).at(-1) ?? path
}

function concisePaths(paths: string[], singular: string, plural: string): string {
  const first = `“${shorten(baseName(paths[0]), 46)}”`
  return paths.length === 1 ? `${singular}: ${first}` : `${plural}: ${first} +${paths.length - 1}`
}

function shorten(value: string, length: number): string {
  return value.length <= length ? value : `${value.slice(0, length - 1).trimEnd()}…`
}
