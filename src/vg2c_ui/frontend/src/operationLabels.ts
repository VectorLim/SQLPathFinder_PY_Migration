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
  if (scope.scope_kind === 'if') return 'Condition'
  if (scope.scope_kind === 'if-branch') return 'True branch'
  if (scope.scope_kind === 'else-branch') return 'Else branch'
  if (scope.scope_kind === 'macro') return 'For each macro row'
  if (scope.scope_kind === 'loop') return 'For each row'
  return scope.label || humanize(scope.scope_kind)
}

export function baseName(path: string): string {
  return path.split(/[\\/]/).filter(Boolean).at(-1) ?? path
}

function concisePaths(paths: string[], singular: string, plural: string): string {
  const first = `“${shorten(baseName(paths[0]), 46)}”`
  return paths.length === 1 ? `${singular}: ${first}` : `${plural}: ${first} +${paths.length - 1}`
}

function humanize(value: string): string {
  return value.replace(/[_-]+/g, ' ').trim().replace(/(^|\s)\S/g, (value) => value.toUpperCase())
}

function shorten(value: string, length: number): string {
  return value.length <= length ? value : `${value.slice(0, length - 1).trimEnd()}…`
}
