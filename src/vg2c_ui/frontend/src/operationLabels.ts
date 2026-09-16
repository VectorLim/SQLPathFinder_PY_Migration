import type { ScopeView, StepView } from './contracts.generated'

export interface OperationLabel {
  primary: string
  secondary: string | null
}

export function formatOperationLabel(step: StepView): OperationLabel {
  const primary = step.display_label.trim() || step.utility.title || 'Operation'
  if (step.csv_outputs.length) return { primary, secondary: concisePaths(step.csv_outputs, 'output', 'outputs') }
  if (step.csv_inputs.length) return { primary, secondary: concisePaths(step.csv_inputs, 'input', 'inputs') }
  return { primary, secondary: null }
}

export function formatScopeLabel(scope: ScopeView): string {
  return scope.label
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
