import type { SqlAttributeOption } from './metadata'
import type { SqlEditableModel } from './model'

export interface SqlSourcePresentation {
  raw: string
  source: string
  alias: string | null
  label: string
}

export interface SqlExpressionPresentation {
  display: string
  sources: string[]
}

const SIMPLE_ALIAS = /^(.*?)(?:\s+AS\s+|\s+)([A-Za-z_][A-Za-z0-9_$#]*|\[[^\]]+\]|"(?:""|[^"])+"|`(?:``|[^`])+`)$/i
const SIMPLE_QUALIFIED = /^\s*([A-Za-z_][A-Za-z0-9_$#]*|\[[^\]]+\]|"(?:""|[^"])+"|`(?:``|[^`])+`)\s*\.\s*([A-Za-z_][A-Za-z0-9_$#]*|\[[^\]]+\]|"(?:""|[^"])+"|`(?:``|[^`])+`)\s*$/

export function sourcePresentation(expression: string): SqlSourcePresentation {
  const raw = expression.trim()
  const aliasMatch = raw.match(SIMPLE_ALIAS)
  const source = aliasMatch?.[1]?.trim() || raw
  const alias = aliasMatch?.[2]?.trim() || null
  return {
    raw,
    source,
    alias,
    label: sourceLabel(source),
  }
}

export function displaySource(expression: string): string {
  return sourcePresentation(expression).source
}

export function restoreSource(displayValue: string, originalExpression: string): string {
  const value = displayValue.trim()
  const original = sourcePresentation(originalExpression)
  if (!original.alias) return value
  return `${value} ${original.alias}`
}

export function presentExpression(expression: string, model: SqlEditableModel): SqlExpressionPresentation {
  const identities = sourceIdentities(model)
  const simple = expression.match(SIMPLE_QUALIFIED)
  if (simple) {
    const identity = findIdentity(identities, simple[1])
    if (identity) return { display: simple[2], sources: [identity.label] }
  }

  let display = expression
  const sources: string[] = []
  for (const identity of identities) {
    const qualifier = identity.alias ?? identity.source
    if (!qualifier) continue
    const replaced = replaceQualifier(display, qualifier, identity.source)
    if (replaced !== display) {
      display = replaced
      sources.push(identity.label)
    }
  }
  return { display, sources: unique(sources) }
}

export function restoreExpression(
  displayValue: string,
  originalExpression: string,
  model: SqlEditableModel,
): string {
  const value = displayValue.trim()
  const identities = sourceIdentities(model)
  const originalSimple = originalExpression.match(SIMPLE_QUALIFIED)
  if (originalSimple) {
    const originalIdentity = findIdentity(identities, originalSimple[1])
    const enteredSimple = value.match(SIMPLE_QUALIFIED)
    if (enteredSimple) {
      const enteredIdentity = findIdentity(identities, enteredSimple[1])
      if (enteredIdentity) return `${enteredIdentity.alias ?? enteredIdentity.source}.${enteredSimple[2]}`
    }
    if (originalIdentity && !value.includes('.')) {
      return `${originalIdentity.alias ?? originalIdentity.source}.${value}`
    }
  }

  let restored = value
  for (const identity of identities) {
    if (!identity.alias) continue
    restored = replaceQualifier(restored, identity.source, identity.alias)
    if (identity.label !== identity.source) restored = replaceQualifier(restored, identity.label, identity.alias)
  }
  return restored
}

export function presentMetadataExpression(expression: string, model: SqlEditableModel): string {
  return presentExpression(expression, model).display
}

export function attributeSourceLabels(option: SqlAttributeOption, model: SqlEditableModel): string[] {
  const declared = option.sources?.length
    ? option.sources
    : option.source
      ? [option.source]
      : []
  if (declared.length) return unique(declared.map((source) => sourcePresentation(source).label).filter(Boolean))
  return presentExpression(option.expression, model).sources
}

/**
 * Resolve source badges for an already selected expression. Exact metadata matches win;
 * otherwise a display-name match is used so an unqualified attribute can truthfully
 * advertise every table supplied by metadata without inventing a SQL qualifier.
 */
export function expressionSourceLabels(
  expression: string,
  model: SqlEditableModel,
  options: readonly SqlAttributeOption[] = [],
): string[] {
  const presentation = presentExpression(expression, model)
  const normalizedExpression = normalizeSqlExpression(expression)
  const exact = options.filter((option) => normalizeSqlExpression(option.expression) === normalizedExpression)
  const matching = exact.length ? exact : options.filter((option) => (
    normalizeSqlExpression(presentMetadataExpression(option.expression, model))
      === normalizeSqlExpression(presentation.display)
  ))
  return unique([
    ...presentation.sources,
    ...matching.flatMap((option) => attributeSourceLabels(option, model)),
  ])
}

function sourceIdentities(model: SqlEditableModel): SqlSourcePresentation[] {
  return model.sources.map((item) => sourcePresentation(item.expression))
}

function findIdentity(identities: SqlSourcePresentation[], qualifier: string): SqlSourcePresentation | null {
  const key = normalizeIdentifier(qualifier)
  return identities.find((identity) => (
    normalizeIdentifier(identity.alias ?? '') === key
    || normalizeIdentifier(identity.source) === key
    || normalizeIdentifier(identity.label) === key
  )) ?? null
}

function sourceLabel(source: string): string {
  const pieces = source.split('.')
  return stripIdentifier(pieces.at(-1)?.trim() || source)
}

function stripIdentifier(value: string): string {
  const trimmed = value.trim()
  if (trimmed.startsWith('[') && trimmed.endsWith(']')) return trimmed.slice(1, -1).replaceAll(']]', ']')
  if (trimmed.startsWith('"') && trimmed.endsWith('"')) return trimmed.slice(1, -1).replaceAll('""', '"')
  if (trimmed.startsWith('`') && trimmed.endsWith('`')) return trimmed.slice(1, -1).replaceAll('``', '`')
  return trimmed
}

function normalizeIdentifier(value: string): string {
  return stripIdentifier(value).toLowerCase()
}

function normalizeSqlExpression(value: string): string {
  return value.trim().replace(/\s+/g, ' ').toLowerCase()
}

function replaceQualifier(value: string, from: string, to: string): string {
  if (!from || from === to) return value
  const escaped = escapeRegExp(from)
  return value.replace(new RegExp(`(^|[^A-Za-z0-9_$#])${escaped}\\s*\\.`, 'g'), (_match, prefix: string) => `${prefix}${to}.`)
}

function escapeRegExp(value: string): string {
  return value.replace(/[.*+?^${}()|[\]\\]/g, '\\$&')
}

function unique(values: string[]): string[] {
  return [...new Set(values)]
}
