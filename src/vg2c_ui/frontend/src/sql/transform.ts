import { SqlEditError, type SqlLogicalConnector, type SqlTransformResult } from './model'
import { parseSql } from './parser'

export const FILTER_OPERATORS = [
  '=', '!=', '<>', '<', '<=', '>', '>=', 'LIKE', 'NOT LIKE', 'ILIKE', 'IN', 'NOT IN', 'IS', 'IS NOT',
] as const

export const JOIN_TYPES = ['INNER', 'LEFT', 'RIGHT', 'FULL', 'CROSS'] as const


export function addFilter(
  source: string,
  values: { left: string; operator: string; right: string; connector?: SqlLogicalConnector },
): SqlTransformResult {
  const model = parseSql(source)
  if (!model.capabilities.filters) throw new SqlEditError(model.readOnlyReason ?? 'Filters are not structurally editable.')
  const left = values.left.trim()
  const right = values.right.trim()
  const operator = normalizeOperator(values.operator)
  if (!left || !right) throw new SqlEditError('Filter operands cannot be empty.')
  const predicate = `${left} ${operator} ${right}`
  if (model.whereBodySpan) {
    const connector = values.connector ?? 'AND'
    return finish(replaceSpan(source, model.whereBodySpan.end, model.whereBodySpan.end, `\n${connector} ${predicate}`), 'filters')
  }
  if (!model.fromClauseSpan) throw new SqlEditError('A WHERE filter cannot be added because the FROM clause is unavailable.')
  return finish(replaceSpan(source, model.fromClauseSpan.end, model.fromClauseSpan.end, `\nWHERE ${predicate}`), 'filters')
}

export function addJoin(
  source: string,
  values: { joinType: string; source: string; left: string; operator?: string; right: string },
): SqlTransformResult {
  const model = parseSql(source)
  if (!model.capabilities.joins || !model.fromClauseSpan) {
    throw new SqlEditError(model.readOnlyReason ?? 'Joins are not structurally editable for this query.')
  }
  const joinType = values.joinType.trim().toUpperCase()
  if (!(JOIN_TYPES as readonly string[]).includes(joinType) || joinType === 'CROSS') {
    throw new SqlEditError('New keyed joins must use INNER, LEFT, RIGHT, or FULL.')
  }
  const joinSource = values.source.trim()
  const left = values.left.trim()
  const right = values.right.trim()
  const operator = normalizeOperator(values.operator ?? '=')
  if (!joinSource || !left || !right) throw new SqlEditError('Join source and key expressions cannot be empty.')
  const clause = `\n${joinType} JOIN ${joinSource} ON ${left} ${operator} ${right}`
  return finish(replaceSpan(source, model.fromClauseSpan.end, model.fromClauseSpan.end, clause), 'joins')
}

export function addSelection(source: string, expression: string): SqlTransformResult {
  const model = parseSql(source)
  if (!model.capabilities.selected || !model.selectListSpan) throw new SqlEditError('SELECT list is not structurally editable.')
  const value = expression.trim()
  if (!value) throw new SqlEditError('Selected expression cannot be empty.')
  const replacement = model.selections.length
    ? `${source.slice(model.selectListSpan.start, model.selectListSpan.end)}, ${value}`
    : value
  return finish(replaceSpan(source, model.selectListSpan.start, model.selectListSpan.end, replacement), 'selected')
}


export function updateSource(source: string, sourceId: string, value: string): SqlTransformResult {
  const model = parseSql(source)
  const sqlSource = model.sources.find((item) => item.id === sourceId)
  if (!sqlSource || !sqlSource.editable) throw new SqlEditError(sqlSource?.readOnlyReason ?? 'Source is not editable.')
  const expression = value.trim()
  if (!expression) throw new SqlEditError('Source cannot be empty.')
  const capability = sqlSource.kind === 'join' ? 'joins' : 'selected'
  return finish(replaceSpan(source, sqlSource.span.start, sqlSource.span.end, expression), capability)
}

export function updateSelection(
  source: string,
  selectionId: string,
  patch: { expression?: string; alias?: string | null },
): SqlTransformResult {
  const model = parseSql(source)
  const selection = model.selections.find((item) => item.id === selectionId)
  if (!selection || !selection.editable) throw new SqlEditError(selection?.readOnlyReason ?? 'Selection is not editable.')
  const expression = (patch.expression ?? selection.expression).trim()
  if (!expression) throw new SqlEditError('Selected expression cannot be empty.')
  const alias = patch.alias === undefined ? selection.alias : cleanAlias(patch.alias)
  const replacement = `${expression}${alias ? ` AS ${alias}` : ''}`
  return finish(replaceSpan(source, selection.span.start, selection.span.end, replacement), 'selected')
}

export function removeSelection(source: string, selectionId: string): SqlTransformResult {
  const model = parseSql(source)
  if (model.selections.length <= 1) throw new SqlEditError('A SELECT query must keep at least one selected expression.')
  const index = model.selections.findIndex((item) => item.id === selectionId)
  const selection = model.selections[index]
  if (!selection || !selection.editable) throw new SqlEditError(selection?.readOnlyReason ?? 'Selection is not removable.')
  let start = selection.span.start
  let end = selection.span.end
  if (index < model.selections.length - 1) end = model.selections[index + 1].span.start
  else start = model.selections[index - 1].span.end
  return finish(replaceSpan(source, start, end, ''), 'selected')
}

export function moveSelection(source: string, selectionId: string, direction: -1 | 1): SqlTransformResult {
  const model = parseSql(source)
  if (!model.selectListSpan) throw new SqlEditError('SELECT list is not structurally editable.')
  const index = model.selections.findIndex((item) => item.id === selectionId)
  const target = index + direction
  if (index < 0 || target < 0 || target >= model.selections.length) throw new SqlEditError('Selection cannot move further.')
  if (!model.selections[index].editable || !model.selections[target].editable) {
    throw new SqlEditError('Read-only selections cannot be reordered.')
  }
  const rows = model.selections.map((item) => source.slice(item.span.start, item.span.end))
  ;[rows[index], rows[target]] = [rows[target], rows[index]]
  const separator = model.selections.length > 1
    ? source.slice(model.selections[0].span.end, model.selections[1].span.start)
    : ', '
  const replacement = rows.join(separator.includes(',') ? separator : ', ')
  return finish(replaceSpan(source, model.selectListSpan.start, model.selectListSpan.end, replacement), 'selected')
}

export function reorderSelection(source: string, selectionId: string, targetIndex: number): SqlTransformResult {
  const model = parseSql(source)
  if (!model.selectListSpan) throw new SqlEditError('SELECT list is not structurally editable.')
  const index = model.selections.findIndex((item) => item.id === selectionId)
  if (index < 0 || targetIndex < 0 || targetIndex >= model.selections.length) {
    throw new SqlEditError('Selection reorder target is unavailable.')
  }
  if (index === targetIndex) return { sql: source, model }
  if (!model.selections[index].editable || !model.selections[targetIndex].editable) {
    throw new SqlEditError('Read-only selections cannot be reordered.')
  }
  const rows = model.selections.map((item) => source.slice(item.span.start, item.span.end))
  const [moved] = rows.splice(index, 1)
  rows.splice(targetIndex, 0, moved)
  const separator = model.selections.length > 1
    ? source.slice(model.selections[0].span.end, model.selections[1].span.start)
    : ', '
  const replacement = rows.join(separator.includes(',') ? separator : ', ')
  return finish(replaceSpan(source, model.selectListSpan.start, model.selectListSpan.end, replacement), 'selected')
}

export function updateFilter(
  source: string,
  filterId: string,
  patch: { left?: string; operator?: string; right?: string; connector?: SqlLogicalConnector },
): SqlTransformResult {
  const model = parseSql(source)
  const filter = model.filters.find((item) => item.id === filterId)
  if (!filter || !filter.editable) throw new SqlEditError(filter?.readOnlyReason ?? 'Filter is not editable.')
  const left = (patch.left ?? filter.left).trim()
  const right = (patch.right ?? filter.right).trim()
  const operator = normalizeOperator(patch.operator ?? filter.operator)
  if (!left || !right) throw new SqlEditError('Filter operands cannot be empty.')
  const replacements: Array<[number, number, string]> = [[filter.span.start, filter.span.end, `${left} ${operator} ${right}`]]
  if (patch.connector && filter.connectorSpan) {
    replacements.push([filter.connectorSpan.start, filter.connectorSpan.end, patch.connector])
  }
  return finish(applyReplacements(source, replacements), 'filters')
}

export function removeFilter(source: string, filterId: string): SqlTransformResult {
  const model = parseSql(source)
  const index = model.filters.findIndex((item) => item.id === filterId)
  const filter = model.filters[index]
  if (!filter || !filter.editable) throw new SqlEditError(filter?.readOnlyReason ?? 'Filter is not removable.')
  if (model.filters.length === 1) {
    if (!model.whereClauseSpan) throw new SqlEditError('WHERE clause span is unavailable.')
    return finish(replaceSpan(source, model.whereClauseSpan.start, model.whereClauseSpan.end, ''), 'filters')
  }
  if (index > 0 && filter.connectorSpan) {
    return finish(replaceSpan(source, filter.connectorSpan.start, filter.span.end, ''), 'filters')
  }
  const next = model.filters[index + 1]
  if (!next?.connectorSpan) throw new SqlEditError('Filter connector could not be isolated safely.')
  return finish(replaceSpan(source, filter.span.start, next.connectorSpan.end, ''), 'filters')
}

export function updateJoinType(source: string, joinId: string, joinType: string): SqlTransformResult {
  const model = parseSql(source)
  const join = model.joins.find((item) => item.id === joinId)
  if (!join || !join.editableType) throw new SqlEditError(join?.readOnlyReason ?? 'Join type is not editable.')
  const normalized = joinType.trim().toUpperCase()
  if (!(JOIN_TYPES as readonly string[]).includes(normalized)) throw new SqlEditError('Unsupported join type.')
  if (normalized === 'CROSS' && (join.predicates.length > 0 || join.readOnlyReason?.includes('USING'))) {
    throw new SqlEditError('CROSS JOIN cannot retain ON/USING join keys.')
  }
  return finish(replaceSpan(source, join.typeSpan.start, join.typeSpan.end, `${normalized} JOIN`), 'joins')
}

export function updateJoinSource(source: string, joinId: string, value: string): SqlTransformResult {
  const model = parseSql(source)
  const join = model.joins.find((item) => item.id === joinId)
  if (!join || !join.editableSource) throw new SqlEditError(join?.readOnlyReason ?? 'Join source is not editable.')
  const sourceValue = value.trim()
  if (!sourceValue) throw new SqlEditError('Join source cannot be empty.')
  return finish(replaceSpan(source, join.sourceSpan.start, join.sourceSpan.end, sourceValue), 'joins')
}

export function updateJoinPredicate(
  source: string,
  joinId: string,
  predicateId: string,
  patch: { left?: string; operator?: string; right?: string },
): SqlTransformResult {
  const model = parseSql(source)
  const join = model.joins.find((item) => item.id === joinId)
  const predicate = join?.predicates.find((item) => item.id === predicateId)
  if (!predicate || !predicate.editable) throw new SqlEditError(predicate?.readOnlyReason ?? 'Join predicate is not editable.')
  const left = (patch.left ?? predicate.left).trim()
  const right = (patch.right ?? predicate.right).trim()
  const operator = normalizeOperator(patch.operator ?? predicate.operator)
  if (!left || !right) throw new SqlEditError('Join key expressions cannot be empty.')
  return finish(replaceSpan(source, predicate.span.start, predicate.span.end, `${left} ${operator} ${right}`), 'joins')
}

export function removeJoinPredicate(source: string, joinId: string, predicateId: string): SqlTransformResult {
  const model = parseSql(source)
  const join = model.joins.find((item) => item.id === joinId)
  if (!join) throw new SqlEditError('Join no longer exists.')
  if (join.predicates.length <= 1) throw new SqlEditError('A join using ON must keep at least one predicate.')
  const index = join.predicates.findIndex((item) => item.id === predicateId)
  const predicate = join.predicates[index]
  if (!predicate || !predicate.editable) throw new SqlEditError(predicate?.readOnlyReason ?? 'Join predicate is not removable.')
  if (index > 0 && predicate.connectorSpan) {
    return finish(replaceSpan(source, predicate.connectorSpan.start, predicate.span.end, ''), 'joins')
  }
  const next = join.predicates[index + 1]
  if (!next?.connectorSpan) throw new SqlEditError('Join predicate connector could not be isolated safely.')
  return finish(replaceSpan(source, predicate.span.start, next.connectorSpan.end, ''), 'joins')
}

export function removeJoin(source: string, joinId: string): SqlTransformResult {
  const model = parseSql(source)
  const join = model.joins.find((item) => item.id === joinId)
  if (!join || join.readOnlyReason?.includes('NATURAL')) throw new SqlEditError(join?.readOnlyReason ?? 'Join is not removable.')
  return finish(replaceSpan(source, join.span.start, join.span.end, ''), 'joins')
}

function cleanAlias(value: string | null): string | null {
  if (value === null) return null
  const alias = value.trim()
  if (!alias) return null
  const plain = /^[A-Za-z_][A-Za-z0-9_$#]*$/
  const quoted = /^("(?:[^"]|"")+"|`[^`]+`|\[[^\]]+\])$/
  if (!plain.test(alias) && !quoted.test(alias)) {
    throw new SqlEditError('Alias must be an identifier; quote aliases that contain spaces or punctuation.')
  }
  return alias
}

function normalizeOperator(value: string): string {
  const normalized = value.trim().replace(/\s+/g, ' ').toUpperCase()
  if (!(FILTER_OPERATORS as readonly string[]).includes(normalized)) throw new SqlEditError('Unsupported predicate operator.')
  return normalized
}

function finish(sql: string, capability: 'selected' | 'filters' | 'joins'): SqlTransformResult {
  const model = parseSql(sql)
  if (!model.capabilities[capability]) {
    throw new SqlEditError(model.readOnlyReason ?? `Updated SQL can no longer be edited safely in ${capability}.`)
  }
  return { sql, model }
}

function replaceSpan(source: string, start: number, end: number, replacement: string): string {
  return `${source.slice(0, start)}${replacement}${source.slice(end)}`
}

function applyReplacements(source: string, replacements: Array<[number, number, string]>): string {
  let result = source
  for (const [start, end, replacement] of [...replacements].sort((left, right) => right[0] - left[0])) {
    result = replaceSpan(result, start, end, replacement)
  }
  return result
}
