import type { ScopeNode, StepNode } from './types'

export interface OperationLabel {
  primary: string
  secondary: string | null
}

const KIND_LABELS: Record<string, string> = {
  SQL_QUERY: 'SQL Query',
  SQLITE_QUERY: 'SQLite Query',
  WRITE_FILE: 'Write File',
  PYTHON_EMBED: 'Python Code',
  FS_COPY: 'Copy File',
  FS_DELETE: 'Delete File',
  EXTERNAL_RUN: 'Run External Command',
  WAIT_FILE: 'Wait for File',
  HTML_REPORT: 'HTML Report',
  EMAIL: 'Send Email',
  MACRO_CONTROL: 'Macro',
  ROWS_IN_FILE: 'Read Rows',
  UNKNOWN: 'Unsupported Operation',
}

const SECONDARY_PARAMETER_NAMES = [
  'target_table',
  'table',
  'destination',
  'target',
  'recipient',
  'source',
  'path',
  'name',
]

export function formatOperationLabel(step: StepNode): OperationLabel {
  const primary = KIND_LABELS[step.functional_kind] ?? humanize(step.functional_kind)
  const output = concisePaths(step.csv_outputs, 'output', 'outputs')
  if (output) return { primary, secondary: output }

  const identifyingParameter = SECONDARY_PARAMETER_NAMES
    .map((name) => step.parameters.find((parameter) => parameter.name.toLocaleLowerCase() === name))
    .find((parameter) => parameter !== undefined)
  const value = identifyingParameter?.value
  if (identifyingParameter && typeof value === 'string' && value.trim()) {
    const label = humanize(identifyingParameter.name).toLocaleLowerCase()
    return { primary, secondary: `${label}: “${shorten(value.trim(), 54)}”` }
  }

  const input = concisePaths(step.csv_inputs, 'source', 'sources')
  if (input) return { primary, secondary: input }

  const fallback = step.display_label.trim()
  return {
    primary,
    secondary: fallback && fallback.toLocaleLowerCase() !== primary.toLocaleLowerCase()
      ? shorten(fallback, 64)
      : null,
  }
}

export function formatScopeLabel(scope: ScopeNode): string {
  if (scope.scope_kind === 'if') return 'Condition'
  if (scope.scope_kind === 'if-branch') return 'True branch'
  if (scope.scope_kind === 'else-branch') return 'Else branch'
  if (scope.scope_kind === 'macro') return 'For each macro row'
  if (scope.scope_kind === 'loop') return 'For each row'
  return scope.label || humanize(scope.scope_kind)
}

function concisePaths(paths: string[], singular: string, plural: string): string | null {
  if (!paths.length) return null
  const name = baseName(paths[0])
  if (paths.length === 1) return `${singular}: “${shorten(name, 48)}”`
  return `${plural}: “${shorten(name, 40)}” +${paths.length - 1}`
}

export function baseName(path: string): string {
  return path.split(/[\\/]/).filter(Boolean).at(-1) ?? path
}

function humanize(value: string): string {
  return value
    .replace(/[_-]+/g, ' ')
    .replace(/([a-z])([A-Z])/g, '$1 $2')
    .trim()
    .toLocaleLowerCase()
    .replace(/(^|\s)\S/g, (letter) => letter.toLocaleUpperCase())
}

function shorten(value: string, length: number): string {
  return value.length <= length ? value : `${value.slice(0, length - 1).trimEnd()}…`
}
