import { analyzeDependencies, artifactKey } from '../../src/vg2c_ui/frontend/src/dependencyValidation'
import { parseSql } from '../../src/vg2c_ui/frontend/src/sql/parser'
import {
  addFilter,
  addJoin,
  addSelection,
  removeFilter,
  removeJoin,
  removeSelection,
  reorderSelection,
  updateFilter,
  updateJoinPredicate,
  updateJoinSource,
  updateJoinType,
  updateSelection,
  updateSource,
} from '../../src/vg2c_ui/frontend/src/sql/transform'
import type { ParameterDescriptor, StepNode, WorkflowDocument } from '../../src/vg2c_ui/frontend/src/types'

type TransformCase = {
  name: string
  sql: string
  operation: string
  args: unknown[]
}

const parserCases = [
  { name: 'selections-and-aliases', sql: 'SELECT a, b AS bee FROM input' },
  { name: 'filters-and-connectors', sql: "SELECT a FROM input WHERE x = 1 OR y NOT LIKE 'z%'" },
  { name: 'joins-and-sources', sql: 'SELECT a.id, b.name FROM foo a LEFT JOIN bar b ON a.id = b.id AND b.active = 1' },
  { name: 'commented-selection-read-only', sql: 'SELECT a, b /* preserve */ + c FROM foo' },
  { name: 'using-join-read-only-keys', sql: 'SELECT a.id FROM foo a LEFT JOIN bar b USING (id)' },
  { name: 'cte-read-only', sql: 'WITH cte AS (SELECT a FROM foo) SELECT a FROM cte' },
  { name: 'set-operation-read-only', sql: 'SELECT a FROM foo UNION SELECT a FROM bar' },
  { name: 'multiple-select-statements-read-only', sql: 'SELECT a FROM foo; SELECT b FROM bar;' },
]

const transformCases: TransformCase[] = [
  { name: 'add-selection', sql: 'SELECT a FROM foo', operation: 'addSelection', args: ['b AS bee'] },
  { name: 'update-selection', sql: 'SELECT a, b AS bee FROM foo', operation: 'updateSelection', args: ['selection-1', { expression: 'c', alias: 'cee' }] },
  { name: 'remove-selection', sql: 'SELECT a, b, c FROM foo', operation: 'removeSelection', args: ['selection-1'] },
  { name: 'reorder-selection', sql: 'SELECT a, b, c FROM foo', operation: 'reorderSelection', args: ['selection-2', 0] },
  { name: 'add-filter', sql: 'SELECT a FROM foo', operation: 'addFilter', args: [{ left: 'a', operator: '>=', right: '10' }] },
  { name: 'update-filter', sql: 'SELECT a FROM foo WHERE a = 1 AND b = 2', operation: 'updateFilter', args: ['filter-1', { operator: '<>', right: '3', connector: 'OR' }] },
  { name: 'remove-filter', sql: 'SELECT a FROM foo WHERE a = 1 AND b = 2', operation: 'removeFilter', args: ['filter-0'] },
  { name: 'add-join', sql: 'SELECT a.id FROM foo a', operation: 'addJoin', args: [{ joinType: 'LEFT', source: 'bar b', left: 'a.id', right: 'b.id' }] },
  { name: 'update-join-type', sql: 'SELECT a.id FROM foo a LEFT JOIN bar b ON a.id = b.id', operation: 'updateJoinType', args: ['join-0', 'INNER'] },
  { name: 'update-join-source', sql: 'SELECT a.id FROM foo a LEFT JOIN bar b ON a.id = b.id', operation: 'updateJoinSource', args: ['join-0', 'baz b'] },
  { name: 'update-join-predicate', sql: 'SELECT a.id FROM foo a LEFT JOIN bar b ON a.id = b.id', operation: 'updateJoinPredicate', args: ['join-0', 'join-0-predicate-0', { operator: '<>', right: 'b.parent_id' }] },
  { name: 'remove-join', sql: 'SELECT a.id FROM foo a LEFT JOIN bar b ON a.id = b.id', operation: 'removeJoin', args: ['join-0'] },
  { name: 'update-source', sql: 'SELECT a FROM foo f', operation: 'updateSource', args: ['source-from-0', 'bar b'] },
]

function runTransform(item: TransformCase) {
  const [a, b, c] = item.args as any[]
  switch (item.operation) {
    case 'addSelection': return addSelection(item.sql, a)
    case 'updateSelection': return updateSelection(item.sql, a, b)
    case 'removeSelection': return removeSelection(item.sql, a)
    case 'reorderSelection': return reorderSelection(item.sql, a, b)
    case 'addFilter': return addFilter(item.sql, a)
    case 'updateFilter': return updateFilter(item.sql, a, b)
    case 'removeFilter': return removeFilter(item.sql, a)
    case 'addJoin': return addJoin(item.sql, a)
    case 'updateJoinType': return updateJoinType(item.sql, a, b)
    case 'updateJoinSource': return updateJoinSource(item.sql, a, b)
    case 'updateJoinPredicate': return updateJoinPredicate(item.sql, a, b, c)
    case 'removeJoin': return removeJoin(item.sql, a)
    case 'updateSource': return updateSource(item.sql, a, b)
    default: throw new Error(`Unknown transform ${item.operation}`)
  }
}

function parameter(id: string, name: string, value: unknown): ParameterDescriptor {
  return {
    id,
    name,
    position: null,
    source: 'fixture',
    value,
    editor_type: Array.isArray(value) ? 'list' : 'string',
    editable: true,
    read_only_reason: null,
    constraints: {},
    annotation: null,
    required: true,
    default: null,
  }
}

function sqlStep(id: string, index: number, inputs: string[], outputs: string[]): StepNode {
  return {
    id,
    node_kind: 'step',
    function_name: id,
    block_index: index,
    source_span: { file: null, start_line: index + 1, end_line: index + 1 },
    functional_kind: 'SQL_QUERY',
    display_label: id,
    icon_key: 'sql',
    description: '',
    parameters: [
      parameter(`${id}-sql`, 'sql', 'SELECT 1'),
      parameter(`${id}-inputs`, 'inputs', inputs),
      parameter(`${id}-output`, 'output', outputs[0] ?? ''),
    ],
    csv_inputs: inputs,
    csv_outputs: outputs,
    parent_scope_id: null,
    branch: null,
    validation_state: 'valid',
    raw_code: null,
    read_only: false,
    utility: {
      name: 'fixture',
      class_name: 'Fixture',
      module: 'fixture',
      title: 'Fixture',
      description: 'Parity fixture',
      method: null,
      method_description: null,
      return_type: null,
      fallback: false,
    },
  }
}

function document(id: string, steps: StepNode[], artifacts: WorkflowDocument['artifacts'] = []): WorkflowDocument {
  return {
    schema_version: 1,
    id,
    source_path: `${id}.txt`,
    output_path: `${id}.py`,
    source_hash: `${id}-source`,
    output_hash: `${id}-output`,
    revision: 1,
    steps,
    scopes: [],
    artifacts,
    diagnostics: [],
    overrides: [],
  }
}

function dependencyFixtures() {
  const producer = document('producer', [sqlStep('produce', 0, [], ['data/out.csv'])], [
    { id: 'out', path: 'data/out.csv', label: 'out.csv', conditional: true, in_loop: true, producer_step_ids: ['produce'], consumer_step_ids: [], order_valid: true },
  ])
  const consumer = document('consumer', [sqlStep('consume', 0, ['data/out.csv'], [])], [
    { id: 'in', path: 'data/out.csv', label: 'out.csv', conditional: false, in_loop: false, producer_step_ids: [], consumer_step_ids: ['consume'], order_valid: true },
  ])

  const producerRename = { 'produce-output': 'data/new.csv' }
  const consumerRename = { 'consume-inputs': ['data/new.csv'] }
  const broken = analyzeDependencies(consumer, [
    { document: producer, values: producerRename },
    { document: consumer, values: {} },
  ])
  const repaired = analyzeDependencies(consumer, [
    { document: producer, values: producerRename },
    { document: consumer, values: consumerRename },
  ])

  const duplicate = document('duplicate', [
    sqlStep('one', 0, [], ['same.csv']),
    sqlStep('two', 1, [], ['./SAME.csv']),
  ])
  const duplicateResult = analyzeDependencies(duplicate)

  const external = document('external', [sqlStep('read-external', 0, ['external.csv'], [])])
  const externalResult = analyzeDependencies(external)

  const normalizedProducer = document('normalized-producer', [sqlStep('np', 0, [], ['./Folder\\Data.CSV'])])
  const normalizedConsumer = document('normalized-consumer', [sqlStep('nc', 0, ['folder/data.csv'], [])])
  const normalizedResult = analyzeDependencies(normalizedConsumer, [
    { document: normalizedProducer, values: {} },
    { document: normalizedConsumer, values: {} },
  ])

  const metadata = document('metadata', [sqlStep('meta', 0, [], ['loop.csv'])], [
    { id: 'loop', path: 'loop.csv', label: 'loop.csv', conditional: true, in_loop: true, producer_step_ids: ['meta'], consumer_step_ids: [], order_valid: true },
  ])
  const metadataResult = analyzeDependencies(metadata)

  return [
    { name: 'pending-output-rename-breaks-consumer', expected: broken },
    { name: 'matching-consumer-edit-repairs-dependency', expected: repaired },
    { name: 'duplicate-outputs-use-artifact-normalization', expected: duplicateResult },
    { name: 'external-input-without-known-producer', expected: externalResult },
    { name: 'path-normalization-matches-producer', expected: normalizedResult, normalized_key: artifactKey(' ./Folder\\Data.CSV ') },
    { name: 'conditional-loop-metadata-preserved', expected: metadataResult },
  ]
}

function transformResult(item: TransformCase) {
  const result = runTransform(item)
  return {
    sql: result.sql,
    model: {
      selections: result.model.selections.map(({ id, expression, alias }) => ({ id, expression, alias })),
      filters: result.model.filters.map(({ id, left, operator, right, connector }) => ({ id, left, operator, right, connector })),
      joins: result.model.joins.map(({ id, joinType, source, predicates }) => ({
        id,
        joinType,
        source,
        predicates: predicates.map(({ id: predicateId, left, operator, right, connector }) => ({
          id: predicateId, left, operator, right, connector,
        })),
      })),
      sources: result.model.sources.map(({ id, expression, kind }) => ({ id, expression, kind })),
      capabilities: result.model.capabilities,
      readOnlyReason: result.model.readOnlyReason,
    },
  }
}

const payload = {
  schema_version: 1,
  captured_from: {
    branch: 'agent/sqlpathfinder-script-editor',
    commit: '4e2961e85b9f19111895a733d223fe696034fd56',
  },
  parser_cases: parserCases.map((item) => ({ ...item, expected: parseSql(item.sql) })),
  transform_cases: transformCases.map((item) => ({ ...item, expected: transformResult(item) })),
  dependency_cases: dependencyFixtures(),
}

console.log(JSON.stringify(payload, null, 2))
