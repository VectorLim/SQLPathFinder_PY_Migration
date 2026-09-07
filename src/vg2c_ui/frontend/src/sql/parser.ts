import type {
  SqlEditableModel,
  SqlJoin,
  SqlLogicalConnector,
  SqlPredicate,
  SqlSelection,
  SqlSource,
  SqlSpan,
} from './model'

type TokenKind = 'word' | 'number' | 'string' | 'quoted' | 'operator' | 'symbol' | 'whitespace' | 'comment'

interface Token {
  kind: TokenKind
  text: string
  upper: string
  start: number
  end: number
  depth: number
}

interface LexResult {
  tokens: Token[]
  error: string | null
}

interface ClausePositions {
  select: Token
  from: Token | null
  where: Token | null
  group: Token | null
  having: Token | null
  order: Token | null
  limit: Token | null
  offset: Token | null
  fetch: Token | null
  qualify: Token | null
  window: Token | null
}

const SET_OPERATORS = new Set(['UNION', 'INTERSECT', 'EXCEPT'])
const CLAUSE_WORDS = new Set(['WHERE', 'GROUP', 'HAVING', 'ORDER', 'LIMIT', 'OFFSET', 'FETCH', 'QUALIFY', 'WINDOW'])
const SIMPLE_JOIN_TYPES = new Set(['INNER', 'LEFT', 'RIGHT', 'FULL', 'CROSS'])

export function parseSql(source: string): SqlEditableModel {
  const fullSpan = trimmedSpan(source, { start: 0, end: source.length })
  const initial = emptyModel(source, fullSpan)
  if (fullSpan.start >= fullSpan.end) {
    return { ...initial, readOnlyReason: 'SQL is empty.' }
  }

  const lexed = lexSql(source)
  if (lexed.error) return { ...initial, readOnlyReason: lexed.error }
  const statementChoice = chooseEditableStatement(source, lexed.tokens, fullSpan)
  if (!statementChoice.span) {
    return { ...initial, readOnlyReason: statementChoice.reason ?? 'Only SELECT statements are structurally editable.' }
  }
  const statementSpan = statementChoice.span
  const empty = emptyModel(source, statementSpan)
  const significant = lexed.tokens.filter((token) => !isTrivia(token) && overlaps(token, statementSpan))
  if (!significant.length) return { ...empty, readOnlyReason: 'SQL is empty.' }

  const first = significant[0]
  if (first.upper === 'WITH') {
    return { ...empty, readOnlyReason: 'CTEs are preserved as raw SQL until a CTE-aware structural editor is available.' }
  }
  if (first.upper !== 'SELECT') {
    return { ...empty, readOnlyReason: 'Only SELECT statements are structurally editable.' }
  }
  if (significant.some((token) => token.depth === 0 && SET_OPERATORS.has(token.upper))) {
    return { ...empty, readOnlyReason: 'UNION, INTERSECT, and EXCEPT queries are preserved as raw SQL.' }
  }

  const clauses = locateClauses(significant)
  const statementEnd = statementSpan.end
  const selectInfo = parseSelections(source, lexed.tokens, clauses, statementEnd)
  const fromInfo = parseFromAndJoins(source, lexed.tokens, clauses, statementEnd)
  const whereInfo = parseWhere(source, lexed.tokens, clauses, statementEnd)

  const reasons = [selectInfo.reason, fromInfo.reason, whereInfo.reason].filter(Boolean) as string[]
  return {
    source,
    statementSpan,
    selections: selectInfo.selections,
    filters: whereInfo.filters,
    joins: fromInfo.joins,
    sources: fromInfo.sources,
    capabilities: {
      selected: selectInfo.capable,
      filters: whereInfo.capable,
      joins: fromInfo.capable,
      rawSql: true,
    },
    readOnlyReason: reasons.length === 3 ? reasons.join(' ') : null,
    selectListSpan: selectInfo.span,
    whereClauseSpan: whereInfo.clauseSpan,
    whereBodySpan: whereInfo.bodySpan,
    fromClauseSpan: fromInfo.span,
  }
}

function chooseEditableStatement(
  source: string,
  tokens: Token[],
  fullSpan: SqlSpan,
): { span: SqlSpan | null; reason: string | null } {
  const semicolons = tokens.filter((token) => token.depth === 0 && token.text === ';' && overlaps(token, fullSpan))
  const spans: SqlSpan[] = []
  let start = fullSpan.start
  for (const semicolon of semicolons) {
    const span = trimmedSpanFromTokens(tokens, { start, end: semicolon.start })
    if (span.start < span.end) spans.push(span)
    start = semicolon.end
  }
  const tail = trimmedSpanFromTokens(tokens, { start, end: fullSpan.end })
  if (tail.start < tail.end) spans.push(tail)
  if (!spans.length) return { span: null, reason: 'SQL is empty.' }

  const firstTokens = spans.map((span) => tokens.find((token) => !isTrivia(token) && token.start >= span.start && token.end <= span.end) ?? null)
  const selectSpans = spans.filter((_, index) => firstTokens[index]?.upper === 'SELECT')
  if (selectSpans.length === 1) return { span: selectSpans[0], reason: null }
  if (selectSpans.length > 1) {
    return { span: null, reason: 'Multiple SELECT statements are preserved raw because the editable target is ambiguous.' }
  }
  if (spans.length === 1 && firstTokens[0]?.upper === 'WITH') return { span: spans[0], reason: null }
  return { span: null, reason: 'No unambiguous SELECT statement was found for structured editing.' }
}

function emptyModel(source: string, statementSpan: SqlSpan): SqlEditableModel {
  return {
    source,
    statementSpan,
    selections: [],
    filters: [],
    joins: [],
    sources: [],
    capabilities: { selected: false, filters: false, joins: false, rawSql: true },
    readOnlyReason: null,
    selectListSpan: null,
    whereClauseSpan: null,
    whereBodySpan: null,
    fromClauseSpan: null,
  }
}

function parseSelections(
  source: string,
  tokens: Token[],
  clauses: ClausePositions,
  statementEnd: number,
): { selections: SqlSelection[]; span: SqlSpan | null; capable: boolean; reason: string | null } {
  const top = tokens.filter((token) => !isTrivia(token) && token.depth === 0)
  const selectIndex = top.findIndex((token) => token === clauses.select)
  let startToken = top[selectIndex + 1]
  if (!startToken) return { selections: [], span: null, capable: false, reason: 'SELECT list is missing.' }

  if (startToken.upper === 'TOP') {
    return {
      selections: [],
      span: null,
      capable: false,
      reason: 'SELECT TOP syntax is preserved raw until its modifier can be isolated safely.',
    }
  }
  if (startToken.upper === 'DISTINCT' || startToken.upper === 'ALL') {
    const next = top[selectIndex + 2]
    if (!next || (startToken.upper === 'DISTINCT' && next.upper === 'ON')) {
      return {
        selections: [],
        span: null,
        capable: false,
        reason: 'This SELECT modifier is preserved raw.',
      }
    }
    startToken = next
  }

  const end = clauses.from?.start ?? firstClauseStartAfter(clauses.select.end, clauses, statementEnd)
  if (startToken.start >= end) return { selections: [], span: null, capable: false, reason: 'SELECT list is empty.' }
  const span = trimmedSpan(source, { start: startToken.start, end })
  const itemSpans = splitByTopLevelComma(tokens, span)
  const selections = itemSpans.map((itemSpan, index) => parseSelection(source, tokens, itemSpan, index))
  return {
    selections,
    span,
    capable: selections.length > 0,
    reason: selections.length ? null : 'No safely isolated SELECT items were found.',
  }
}

function parseSelection(source: string, tokens: Token[], span: SqlSpan, index: number): SqlSelection {
  const local = tokens.filter((token) => !isTrivia(token) && token.start >= span.start && token.end <= span.end)
  const comments = tokens.some((token) => token.kind === 'comment' && token.start >= span.start && token.end <= span.end)
  let expressionSpan = span
  let alias: string | null = null

  const asCandidates = local.filter((token) => token.depth === 0 && token.upper === 'AS')
  const asToken = asCandidates.at(-1)
  if (asToken) {
    const after = local.filter((token) => token.start >= asToken.end)
    if (after.length === 1 && isIdentifierToken(after[0])) {
      expressionSpan = trimmedSpan(source, { start: span.start, end: asToken.start })
      alias = source.slice(after[0].start, after[0].end)
    }
  }

  const expression = source.slice(expressionSpan.start, expressionSpan.end).trim()
  const raw = source.slice(span.start, span.end)
  const editable = Boolean(expression) && !comments
  return {
    id: `selection-${index}`,
    expression,
    alias,
    raw,
    editable,
    readOnlyReason: comments ? 'Selections containing comments are preserved raw.' : editable ? null : 'Selection is not safely editable.',
    span,
  }
}

function parseWhere(
  source: string,
  tokens: Token[],
  clauses: ClausePositions,
  statementEnd: number,
): {
  filters: SqlPredicate[]
  clauseSpan: SqlSpan | null
  bodySpan: SqlSpan | null
  capable: boolean
  reason: string | null
} {
  if (!clauses.where) return { filters: [], clauseSpan: null, bodySpan: null, capable: true, reason: null }
  const end = nextClauseStart(clauses.where.start, clauses, statementEnd)
  const bodySpan = trimmedSpan(source, { start: clauses.where.end, end })
  const clauseSpan = trimmedSpan(source, { start: clauses.where.start, end })
  if (bodySpan.start >= bodySpan.end) {
    return { filters: [], clauseSpan, bodySpan, capable: false, reason: 'WHERE clause is empty.' }
  }
  const filters = parsePredicateChain(source, tokens, bodySpan, 'filter')
  return { filters, clauseSpan, bodySpan, capable: true, reason: null }
}

function parseFromAndJoins(
  source: string,
  tokens: Token[],
  clauses: ClausePositions,
  statementEnd: number,
): { joins: SqlJoin[]; sources: SqlSource[]; span: SqlSpan | null; capable: boolean; reason: string | null } {
  if (!clauses.from) return { joins: [], sources: [], span: null, capable: true, reason: null }
  const end = nextClauseStart(clauses.from.start, clauses, statementEnd)
  const span = trimmedSpan(source, { start: clauses.from.end, end })
  if (span.start >= span.end) return { joins: [], sources: [], span, capable: false, reason: 'FROM clause is empty.' }

  const significant = tokens.filter(
    (token) => !isTrivia(token) && token.depth === 0 && token.start >= span.start && token.end <= span.end,
  )
  const joinTokens = significant.filter((token) => token.upper === 'JOIN')
  const joinStarts = joinTokens.map((joinToken) => joinTypeStart(significant, joinToken))
  const baseSourceSpan = trimmedSpan(source, { start: span.start, end: joinStarts[0] ?? span.end })
  const sources = parseSources(source, tokens, baseSourceSpan, 'from')
  if (!joinTokens.length) return { joins: [], sources, span, capable: true, reason: null }

  const joins: SqlJoin[] = []
  for (let index = 0; index < joinTokens.length; index += 1) {
    const joinToken = joinTokens[index]
    const start = joinStarts[index]
    const joinEnd = joinStarts[index + 1] ?? span.end
    const interior = significant.filter((token) => token.start >= joinToken.end && token.start < joinEnd)
    const conditionToken = interior.find((token) => token.upper === 'ON' || token.upper === 'USING') ?? null
    const sourceSpan = trimmedSpan(source, {
      start: joinToken.end,
      end: conditionToken?.start ?? joinEnd,
    })
    const sourceText = source.slice(sourceSpan.start, sourceSpan.end)
    const sourceTrimmed = sourceText.trim()
    const sourceContainsComment = tokens.some((token) => token.kind === 'comment' && overlaps(token, sourceSpan))
    const sourceSubquery = sourceTrimmed.startsWith('(')
    const typeText = source.slice(start, joinToken.end)
    const normalizedType = normalizeJoinType(typeText)
    const natural = /\bNATURAL\b/i.test(typeText)
    const using = conditionToken?.upper === 'USING'
    let predicates: SqlPredicate[] = []
    if (conditionToken?.upper === 'ON') {
      const predicateSpan = trimmedSpan(source, { start: conditionToken.end, end: joinEnd })
      predicates = parsePredicateChain(source, tokens, predicateSpan, `join-${index}-predicate`)
      if (predicates.some((predicate) => predicate.connector === 'OR')) {
        predicates = predicates.map((predicate) => ({
          ...predicate,
          editable: false,
          readOnlyReason: 'OR-based join conditions are preserved raw.',
        }))
      }
    }
    const joinId = `join-${index}`
    joins.push({
      id: joinId,
      joinType: normalizedType,
      source: sourceText,
      predicates,
      editableType: !natural,
      editableSource: Boolean(sourceTrimmed) && !natural && !sourceContainsComment && !sourceSubquery,
      readOnlyReason: natural
        ? 'NATURAL JOIN is preserved raw.'
        : using
          ? 'USING join keys are preserved raw; the join type remains editable.'
          : sourceSubquery
            ? 'Subquery join sources are preserved raw.'
            : sourceContainsComment
              ? 'Join sources containing comments are preserved raw.'
              : null,
      span: trimmedSpan(source, { start, end: joinEnd }),
      typeSpan: trimmedSpan(source, { start, end: joinToken.end }),
      sourceSpan,
    })
    sources.push({
      id: `source-join-${index}`,
      expression: sourceTrimmed,
      kind: 'join',
      editable: Boolean(sourceTrimmed) && !natural && !sourceContainsComment && !sourceSubquery,
      readOnlyReason: natural
        ? 'NATURAL JOIN source is preserved raw.'
        : sourceSubquery
          ? 'Subquery join sources are preserved raw.'
          : sourceContainsComment
            ? 'Join sources containing comments are preserved raw.'
            : null,
      span: sourceSpan,
      joinId,
    })
  }
  return { joins, sources, span, capable: true, reason: null }
}

function parseSources(
  source: string,
  tokens: Token[],
  span: SqlSpan,
  kind: 'from',
): SqlSource[] {
  if (span.start >= span.end) return []
  return splitByTopLevelComma(tokens, span).map((sourceSpan, index) => {
    const expression = source.slice(sourceSpan.start, sourceSpan.end).trim()
    const containsComment = tokens.some((token) => token.kind === 'comment' && overlaps(token, sourceSpan))
    const subquery = expression.startsWith('(')
    const editable = Boolean(expression) && !containsComment && !subquery
    return {
      id: `source-${kind}-${index}`,
      expression,
      kind,
      editable,
      readOnlyReason: containsComment
        ? 'Sources containing comments are preserved raw.'
        : subquery
          ? 'Subquery sources are preserved raw.'
          : editable ? null : 'Source is not safely editable.',
      span: sourceSpan,
      joinId: null,
    }
  })
}

function parsePredicateChain(source: string, tokens: Token[], span: SqlSpan, prefix: string): SqlPredicate[] {
  const significant = tokens.filter(
    (token) => !isTrivia(token) && token.start >= span.start && token.end <= span.end,
  )
  const pieces: Array<{ span: SqlSpan; connector: SqlLogicalConnector | null; connectorSpan: SqlSpan | null }> = []
  let segmentStart = span.start
  let connectorForSegment: SqlLogicalConnector | null = null
  let connectorSpanForSegment: SqlSpan | null = null
  let betweenPending = false

  for (const token of significant) {
    if (token.depth !== 0) continue
    if (token.upper === 'CASE') betweenPending = false
    if (token.upper === 'BETWEEN') {
      betweenPending = true
      continue
    }
    if (token.upper === 'AND' && betweenPending) {
      betweenPending = false
      continue
    }
    if (token.upper !== 'AND' && token.upper !== 'OR') continue
    const candidate = trimmedSpan(source, { start: segmentStart, end: token.start })
    if (candidate.start < candidate.end) {
      pieces.push({ span: candidate, connector: connectorForSegment, connectorSpan: connectorSpanForSegment })
    }
    connectorForSegment = token.upper as SqlLogicalConnector
    connectorSpanForSegment = { start: token.start, end: token.end }
    segmentStart = token.end
    betweenPending = false
  }
  const tail = trimmedSpan(source, { start: segmentStart, end: span.end })
  if (tail.start < tail.end) {
    pieces.push({ span: tail, connector: connectorForSegment, connectorSpan: connectorSpanForSegment })
  }

  return pieces.map((piece, index) => parsePredicate(source, tokens, piece, `${prefix}-${index}`))
}

function parsePredicate(
  source: string,
  tokens: Token[],
  piece: { span: SqlSpan; connector: SqlLogicalConnector | null; connectorSpan: SqlSpan | null },
  id: string,
): SqlPredicate {
  const local = tokens.filter(
    (token) => !isTrivia(token) && token.start >= piece.span.start && token.end <= piece.span.end,
  )
  const raw = source.slice(piece.span.start, piece.span.end)
  const hasComment = tokens.some(
    (token) => token.kind === 'comment' && token.start >= piece.span.start && token.end <= piece.span.end,
  )
  const top = local.filter((token) => token.depth === 0)
  const op = findPredicateOperator(top)
  if (!op || hasComment || top.some((token) => ['CASE', 'BETWEEN'].includes(token.upper))) {
    return {
      id,
      left: raw.trim(),
      operator: '',
      right: '',
      connector: piece.connector,
      raw,
      editable: false,
      readOnlyReason: hasComment
        ? 'Predicates containing comments are preserved raw.'
        : 'This predicate is too complex for safe row editing.',
      span: piece.span,
      connectorSpan: piece.connectorSpan,
    }
  }
  const leftSpan = trimmedSpan(source, { start: piece.span.start, end: op.start })
  const rightSpan = trimmedSpan(source, { start: op.end, end: piece.span.end })
  const left = source.slice(leftSpan.start, leftSpan.end)
  const right = source.slice(rightSpan.start, rightSpan.end)
  const editable = Boolean(left.trim() && right.trim())
  return {
    id,
    left,
    operator: op.text,
    right,
    connector: piece.connector,
    raw,
    editable,
    readOnlyReason: editable ? null : 'Predicate operands could not be isolated safely.',
    span: piece.span,
    connectorSpan: piece.connectorSpan,
  }
}

function findPredicateOperator(tokens: Token[]): { start: number; end: number; text: string } | null {
  const candidates: Array<{ start: number; end: number; text: string }> = []
  for (let index = 0; index < tokens.length; index += 1) {
    const token = tokens[index]
    if (token.kind === 'operator' && ['=', '!=', '<>', '<', '<=', '>', '>='].includes(token.text)) {
      candidates.push({ start: token.start, end: token.end, text: token.text.toUpperCase() })
      continue
    }
    if (['LIKE', 'ILIKE', 'IN', 'IS'].includes(token.upper)) {
      const previous = tokens[index - 1]
      const next = tokens[index + 1]
      if (token.upper === 'LIKE' || token.upper === 'ILIKE' || token.upper === 'IN') {
        if (previous?.upper === 'NOT') {
          candidates.push({ start: previous.start, end: token.end, text: `NOT ${token.upper}` })
        } else {
          candidates.push({ start: token.start, end: token.end, text: token.upper })
        }
      } else if (token.upper === 'IS') {
        if (next?.upper === 'NOT') {
          candidates.push({ start: token.start, end: next.end, text: 'IS NOT' })
        } else {
          candidates.push({ start: token.start, end: token.end, text: 'IS' })
        }
      }
    }
  }
  const unique = candidates.filter((candidate, index) => (
    index === 0 || candidate.start !== candidates[index - 1].start || candidate.end !== candidates[index - 1].end
  ))
  return unique.length === 1 ? unique[0] : null
}

function locateClauses(significant: Token[]): ClausePositions {
  const top = significant.filter((token) => token.depth === 0)
  const select = top.find((token) => token.upper === 'SELECT') as Token
  const afterSelect = top.filter((token) => token.start > select.start)
  const find = (name: string) => afterSelect.find((token) => token.upper === name) ?? null
  return {
    select,
    from: find('FROM'),
    where: find('WHERE'),
    group: find('GROUP'),
    having: find('HAVING'),
    order: find('ORDER'),
    limit: find('LIMIT'),
    offset: find('OFFSET'),
    fetch: find('FETCH'),
    qualify: find('QUALIFY'),
    window: find('WINDOW'),
  }
}

function firstClauseStartAfter(position: number, clauses: ClausePositions, fallback: number): number {
  return Object.values(clauses)
    .filter((token): token is Token => Boolean(token) && token.start > position && token.upper !== 'SELECT')
    .map((token) => token.start)
    .sort((left, right) => left - right)[0] ?? fallback
}

function nextClauseStart(position: number, clauses: ClausePositions, fallback: number): number {
  return Object.values(clauses)
    .filter((token): token is Token => Boolean(token) && token.start > position && CLAUSE_WORDS.has(token.upper))
    .map((token) => token.start)
    .sort((left, right) => left - right)[0] ?? fallback
}

function splitByTopLevelComma(tokens: Token[], span: SqlSpan): SqlSpan[] {
  const commas = tokens.filter(
    (token) => token.depth === 0 && token.text === ',' && token.start >= span.start && token.end <= span.end,
  )
  const spans: SqlSpan[] = []
  let start = span.start
  for (const comma of commas) {
    spans.push({ start, end: comma.start })
    start = comma.end
  }
  spans.push({ start, end: span.end })
  return spans.map((item) => trimmedSpanFromTokens(tokens, item)).filter((item) => item.start < item.end)
}

function trimmedSpanFromTokens(tokens: Token[], span: SqlSpan): SqlSpan {
  const significant = tokens.filter((token) => !isTrivia(token) && token.start >= span.start && token.end <= span.end)
  if (!significant.length) return { start: span.end, end: span.end }
  return { start: significant[0].start, end: significant.at(-1)!.end }
}

function joinTypeStart(tokens: Token[], joinToken: Token): number {
  const index = tokens.indexOf(joinToken)
  let cursor = index - 1
  let start = joinToken.start
  if (cursor >= 0 && tokens[cursor].upper === 'OUTER') {
    start = tokens[cursor].start
    cursor -= 1
  }
  if (cursor >= 0 && SIMPLE_JOIN_TYPES.has(tokens[cursor].upper)) {
    start = tokens[cursor].start
    cursor -= 1
  }
  if (cursor >= 0 && tokens[cursor].upper === 'NATURAL') {
    start = tokens[cursor].start
  }
  return start
}

function normalizeJoinType(raw: string): string {
  const text = raw.replace(/\bJOIN\s*$/i, '').replace(/\bOUTER\b/i, '').trim().toUpperCase()
  if (!text) return 'INNER'
  if (text === 'LEFT' || text === 'RIGHT' || text === 'FULL' || text === 'INNER' || text === 'CROSS') return text
  return text
}

function lexSql(source: string): LexResult {
  const tokens: Token[] = []
  let index = 0
  let depth = 0
  while (index < source.length) {
    const start = index
    const character = source[index]
    if (/\s/.test(character)) {
      index += 1
      while (index < source.length && /\s/.test(source[index])) index += 1
      tokens.push(token('whitespace', source, start, index, depth))
      continue
    }
    if (source.startsWith('--', index)) {
      index += 2
      while (index < source.length && source[index] !== '\n') index += 1
      tokens.push(token('comment', source, start, index, depth))
      continue
    }
    if (source.startsWith('/*', index)) {
      const close = source.indexOf('*/', index + 2)
      if (close < 0) return { tokens, error: 'SQL contains an unterminated block comment.' }
      index = close + 2
      tokens.push(token('comment', source, start, index, depth))
      continue
    }
    if (character === "'") {
      index += 1
      let closed = false
      while (index < source.length) {
        if (source[index] === "'") {
          if (source[index + 1] === "'") {
            index += 2
            continue
          }
          index += 1
          closed = true
          break
        }
        index += 1
      }
      if (!closed) return { tokens, error: 'SQL contains an unterminated string literal.' }
      tokens.push(token('string', source, start, index, depth))
      continue
    }
    if (character === '"' || character === '`' || character === '[') {
      const closeCharacter = character === '[' ? ']' : character
      index += 1
      let closed = false
      while (index < source.length) {
        if (source[index] === closeCharacter) {
          if (character !== '[' && source[index + 1] === closeCharacter) {
            index += 2
            continue
          }
          if (character === '[' && source[index + 1] === ']') {
            index += 2
            continue
          }
          index += 1
          closed = true
          break
        }
        index += 1
      }
      if (!closed) return { tokens, error: 'SQL contains an unterminated quoted identifier.' }
      tokens.push(token('quoted', source, start, index, depth))
      continue
    }
    if (/[A-Za-z_$#@]/.test(character)) {
      index += 1
      while (index < source.length && /[A-Za-z0-9_$#@]/.test(source[index])) index += 1
      tokens.push(token('word', source, start, index, depth))
      continue
    }
    if (/\d/.test(character)) {
      index += 1
      while (index < source.length && /[\d.eE+-]/.test(source[index])) index += 1
      tokens.push(token('number', source, start, index, depth))
      continue
    }
    const two = source.slice(index, index + 2)
    if (['<=', '>=', '<>', '!=', '||', '::', '->'].includes(two)) {
      index += 2
      tokens.push(token('operator', source, start, index, depth))
      continue
    }
    if (character === '(') {
      tokens.push(token('symbol', source, start, start + 1, depth))
      depth += 1
      index += 1
      continue
    }
    if (character === ')') {
      depth -= 1
      if (depth < 0) return { tokens, error: 'SQL contains an unmatched closing parenthesis.' }
      tokens.push(token('symbol', source, start, start + 1, depth))
      index += 1
      continue
    }
    const kind: TokenKind = '=<>+-*/%'.includes(character) ? 'operator' : 'symbol'
    index += 1
    tokens.push(token(kind, source, start, index, depth))
  }
  if (depth !== 0) return { tokens, error: 'SQL contains unmatched parentheses.' }
  return { tokens, error: null }
}

function token(kind: TokenKind, source: string, start: number, end: number, depth: number): Token {
  const text = source.slice(start, end)
  return { kind, text, upper: text.toUpperCase(), start, end, depth }
}

function trimmedSpan(source: string, span: SqlSpan): SqlSpan {
  let start = Math.max(0, span.start)
  let end = Math.min(source.length, span.end)
  while (start < end && /\s/.test(source[start])) start += 1
  while (end > start && /\s/.test(source[end - 1])) end -= 1
  return { start, end }
}

function isTrivia(token: Token): boolean {
  return token.kind === 'whitespace' || token.kind === 'comment'
}

function overlaps(token: Token, span: SqlSpan): boolean {
  return token.end > span.start && token.start < span.end
}

function isIdentifierToken(token: Token): boolean {
  return token.kind === 'word' || token.kind === 'quoted'
}
