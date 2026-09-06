import './sql/sqlEditor.css'

import { useEffect, useState } from 'react'

import type {
  DependencyIssueView,
  SqlActionRequest,
  SqlJoinView,
  SqlModelView,
  SqlPredicateView,
  SqlSelectionView,
  StepView,
} from './contracts.generated'
import { formatOperationLabel } from './operationLabels'

interface Props {
  tabId: string
  step: StepView
  values: Record<string, unknown>
  files: { inputs: string[]; outputs: string[] }
  diagnostics: DependencyIssueView[]
  inspect: (tabId: string, parameterId: string) => Promise<SqlModelView>
  runAction: (
    tabId: string,
    parameterId: string,
    action: SqlActionRequest['action'],
    args: Record<string, unknown>,
  ) => Promise<SqlModelView>
}

export function StructuredSqlEditor({ tabId, step, values, files, diagnostics, inspect, runAction }: Props) {
  const parameter = step.parameters.find((item) => item.capabilities.includes('structured-sql')) ?? null
  const effectiveSql = parameter && Object.hasOwn(values, parameter.id) ? values[parameter.id] : parameter?.value
  const [model, setModel] = useState<SqlModelView | null>(null)
  const [error, setError] = useState('')
  const [busy, setBusy] = useState(false)

  useEffect(() => {
    if (!parameter) return
    let cancelled = false
    setBusy(true)
    void inspect(tabId, parameter.id)
      .then((next) => { if (!cancelled) { setModel(next); setError('') } })
      .catch((reason) => { if (!cancelled) setError(reason instanceof Error ? reason.message : 'Could not inspect SQL.') })
      .finally(() => { if (!cancelled) setBusy(false) })
    return () => { cancelled = true }
  }, [tabId, parameter?.id, typeof effectiveSql === 'string' ? effectiveSql : ''])

  async function act(action: SqlActionRequest['action'], args: Record<string, unknown>) {
    if (!parameter) return
    setBusy(true)
    try {
      setModel(await runAction(tabId, parameter.id, action, args))
      setError('')
    } catch (reason) {
      setError(reason instanceof Error ? reason.message : 'SQL update failed.')
    } finally {
      setBusy(false)
    }
  }

  const label = formatOperationLabel(step)
  if (!parameter) return <p className="read-only-note">This operation does not expose a structured SQL parameter.</p>

  return (
    <section className="operation-editor sql-operation-editor" aria-label={`Edit ${label.primary}`}>
      <header className="operation-editor__header">
        <div><span className="eyebrow">Operation {step.block_index + 1}</span><h3>{label.primary}</h3>{label.secondary && <p className="operation-secondary">{label.secondary}</p>}</div>
        <span className={`state-pill${step.read_only ? ' state-pill--readonly' : ''}`}>{step.read_only ? 'Read only' : 'Structured SQL'}</span>
      </header>
      {step.description && <p className="operation-description">{step.description}</p>}
      <FileSummary files={files} />
      <Diagnostics diagnostics={diagnostics} />
      {error && <p className="sql-edit-error" role="alert">{error}</p>}
      {busy && !model && <p className="empty-copy">Loading structured SQL…</p>}
      {model && !step.read_only && (
        <div className="sql-structured-sections">
          <Selections model={model} onAction={act} disabled={busy} />
          <Filters model={model} onAction={act} disabled={busy} />
          <Joins model={model} onAction={act} disabled={busy} />
        </div>
      )}
      {model?.read_only_reason && <p className="read-only-note">{model.read_only_reason}</p>}
      <details className="raw-sql-details">
        <summary>Raw SQL</summary>
        <pre className="raw-code raw-sql">{model?.source ?? String(effectiveSql ?? parameter.source)}</pre>
        <small>Raw SQL is displayed only; structural changes are validated and transformed by vg2c.</small>
      </details>
    </section>
  )
}

function Selections({ model, onAction, disabled }: SectionProps) {
  return (
    <section className="sql-section"><header><h4>Selected attributes</h4><button type="button" disabled={disabled || !model.capabilities.selected} onClick={() => {
      const expression = prompt('Selected expression')?.trim(); if (expression) void onAction('add-selection', { expression })
    }}>Add</button></header>
      {model.selections.map((item, index) => <SelectionRow key={item.id} item={item} index={index} count={model.selections.length} disabled={disabled} onAction={onAction} />)}
    </section>
  )
}

function SelectionRow({ item, index, count, disabled, onAction }: { item: SqlSelectionView; index: number; count: number; disabled: boolean; onAction: ActionFn }) {
  return <div className="sql-row">
    <CommitInput value={item.expression} disabled={disabled || !item.editable} ariaLabel="Selected expression" onCommit={(expression) => onAction('update-selection', { selection_id: item.id, expression })} />
    <CommitInput value={item.alias ?? ''} disabled={disabled || !item.editable} ariaLabel="Alias" placeholder="alias" onCommit={(alias) => onAction('update-selection', { selection_id: item.id, alias: alias || null })} />
    <button type="button" disabled={disabled || index === 0 || !item.editable} onClick={() => void onAction('move-selection', { selection_id: item.id, direction: -1 })}>↑</button>
    <button type="button" disabled={disabled || index === count - 1 || !item.editable} onClick={() => void onAction('move-selection', { selection_id: item.id, direction: 1 })}>↓</button>
    <button type="button" disabled={disabled || count <= 1 || !item.editable} onClick={() => void onAction('remove-selection', { selection_id: item.id })}>Remove</button>
  </div>
}

function Filters({ model, onAction, disabled }: SectionProps) {
  return (
    <section className="sql-section"><header><h4>Filters</h4><button type="button" disabled={disabled || !model.capabilities.filters} onClick={() => {
      const left = prompt('Left expression')?.trim(); const right = prompt('Right expression')?.trim(); if (left && right) void onAction('add-filter', { left, operator: model.filter_operators[0], right, connector: model.logical_connectors[0] })
    }}>Add</button></header>
      {model.filters.map((item) => <PredicateRow key={item.id} item={item} operators={model.filter_operators} connectors={model.logical_connectors} disabled={disabled} onAction={onAction} />)}
      {!model.filters.length && <p className="empty-copy">No filters.</p>}
    </section>
  )
}

function PredicateRow({ item, operators, connectors, disabled, onAction }: { item: SqlPredicateView; operators: string[]; connectors: string[]; disabled: boolean; onAction: ActionFn }) {
  return <div className="sql-row">
    {item.connector && <select disabled={disabled || !item.editable} value={item.connector} onChange={(event) => void onAction('update-filter', { filter_id: item.id, connector: event.target.value })}>{connectors.map((connector) => <option key={connector}>{connector}</option>)}</select>}
    <CommitInput value={item.left} disabled={disabled || !item.editable} ariaLabel="Filter left" onCommit={(left) => onAction('update-filter', { filter_id: item.id, left })} />
    <select disabled={disabled || !item.editable} value={item.operator} onChange={(event) => void onAction('update-filter', { filter_id: item.id, operator: event.target.value })}>{operators.map((operator) => <option key={operator}>{operator}</option>)}</select>
    <CommitInput value={item.right} disabled={disabled || !item.editable} ariaLabel="Filter right" onCommit={(right) => onAction('update-filter', { filter_id: item.id, right })} />
    <button type="button" disabled={disabled || !item.editable} onClick={() => void onAction('remove-filter', { filter_id: item.id })}>Remove</button>
  </div>
}

function Joins({ model, onAction, disabled }: SectionProps) {
  return (
    <section className="sql-section"><header><h4>Joins</h4><button type="button" disabled={disabled || !model.capabilities.joins} onClick={() => {
      const source = prompt('Join source')?.trim(); const left = prompt('Left key')?.trim(); const right = prompt('Right key')?.trim(); if (source && left && right) void onAction('add-join', { join_type: model.join_types[0], source, left, right, operator: model.filter_operators[0] })
    }}>Add</button></header>
      {model.joins.map((item) => <JoinRow key={item.id} item={item} model={model} disabled={disabled} onAction={onAction} />)}
      {!model.joins.length && <p className="empty-copy">No joins.</p>}
    </section>
  )
}

function JoinRow({ item, model, disabled, onAction }: { item: SqlJoinView; model: SqlModelView; disabled: boolean; onAction: ActionFn }) {
  return <div className="sql-join-row">
    <div className="sql-row">
      <select disabled={disabled || !item.editable_type} value={item.join_type} onChange={(event) => void onAction('update-join-type', { join_id: item.id, join_type: event.target.value })}>{model.join_types.map((joinType) => <option key={joinType}>{joinType}</option>)}</select>
      <CommitInput value={item.source} disabled={disabled || !item.editable_source} ariaLabel="Join source" onCommit={(source) => onAction('update-join-source', { join_id: item.id, source })} />
      <button type="button" disabled={disabled} onClick={() => void onAction('remove-join', { join_id: item.id })}>Remove join</button>
    </div>
    {item.predicates.map((predicate) => <div className="sql-row" key={predicate.id}>
      <CommitInput value={predicate.left} disabled={disabled || !predicate.editable} ariaLabel="Join left key" onCommit={(left) => onAction('update-join-predicate', { join_id: item.id, predicate_id: predicate.id, left })} />
      <select disabled={disabled || !predicate.editable} value={predicate.operator} onChange={(event) => void onAction('update-join-predicate', { join_id: item.id, predicate_id: predicate.id, operator: event.target.value })}>{model.filter_operators.map((operator) => <option key={operator}>{operator}</option>)}</select>
      <CommitInput value={predicate.right} disabled={disabled || !predicate.editable} ariaLabel="Join right key" onCommit={(right) => onAction('update-join-predicate', { join_id: item.id, predicate_id: predicate.id, right })} />
      <button type="button" disabled={disabled || item.predicates.length <= 1 || !predicate.editable} onClick={() => void onAction('remove-join-predicate', { join_id: item.id, predicate_id: predicate.id })}>Remove key</button>
    </div>)}
  </div>
}

type ActionFn = (action: SqlActionRequest['action'], args: Record<string, unknown>) => Promise<void>
interface SectionProps { model: SqlModelView; onAction: ActionFn; disabled: boolean }

function CommitInput({ value, disabled, ariaLabel, placeholder, onCommit }: { value: string; disabled: boolean; ariaLabel: string; placeholder?: string; onCommit: (value: string) => Promise<void> }) {
  const [draft, setDraft] = useState(value)
  useEffect(() => setDraft(value), [value])
  return <input aria-label={ariaLabel} placeholder={placeholder} disabled={disabled} value={draft} onChange={(event) => setDraft(event.target.value)} onBlur={() => { const next = draft.trim(); if (next !== value.trim()) void onCommit(next) }} />
}

function FileSummary({ files }: { files: { inputs: string[]; outputs: string[] } }) {
  return <div className="operation-io">{files.inputs.length > 0 && <FileChips label="Reads" paths={files.inputs} />}{files.outputs.length > 0 && <FileChips label="Produces" paths={files.outputs} />}</div>
}
function FileChips({ label, paths }: { label: string; paths: string[] }) { return <div className="file-chip-row"><strong>{label}</strong><div>{paths.map((path) => <code key={path} title={path}>{path}</code>)}</div></div> }
function Diagnostics({ diagnostics }: { diagnostics: DependencyIssueView[] }) { return diagnostics.length ? <div className="operation-diagnostics" role="alert">{diagnostics.map((item) => <p key={`${item.code}-${item.artifact}`}><strong>{item.code.replaceAll('_', ' ')}</strong><span>{item.message}</span></p>)}</div> : null }
