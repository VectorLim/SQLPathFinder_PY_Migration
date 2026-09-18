import './sql/sqlEditor.css'

import { useEffect, useState, type FormEvent } from 'react'
import { RotateCcw } from 'lucide-react'

import type {
  SemanticBindingView,
  SqlActionRequest,
  SqlJoinView,
  SqlModelView,
  SqlPredicateView,
  SqlSelectionView,
} from './contracts.generated'
import { effectiveBindingValue } from './workspaceState'

interface Props {
  tabId: string
  binding: SemanticBindingView
  readOnly: boolean
  onReset: () => void
  values: Record<string, unknown>
  inspect: (tabId: string, parameterId: string) => Promise<SqlModelView>
  runAction: (
    tabId: string,
    parameterId: string,
    action: SqlActionRequest['action'],
    args: Record<string, unknown>,
  ) => Promise<SqlModelView>
}

export function StructuredSqlEditor({ tabId, binding, values, readOnly, inspect, runAction, onReset }: Props) {
  const effectiveSql = effectiveBindingValue(values, binding)
  const [model, setModel] = useState<SqlModelView | null>(null)
  const [error, setError] = useState('')
  const [busy, setBusy] = useState(false)

  useEffect(() => {
    let cancelled = false
    setBusy(true)
    void inspect(tabId, binding.id)
      .then((next) => { if (!cancelled) { setModel(next); setError('') } })
      .catch((reason) => { if (!cancelled) setError(reason instanceof Error ? reason.message : 'Could not inspect SQL.') })
      .finally(() => { if (!cancelled) setBusy(false) })
    return () => { cancelled = true }
  }, [tabId, binding.id, values, typeof effectiveSql === 'string' ? effectiveSql : ''])

  async function act(action: SqlActionRequest['action'], args: Record<string, unknown>): Promise<boolean> {
    setBusy(true)
    try {
      setModel(await runAction(tabId, binding.id, action, args))
      setError('')
      return true
    } catch (reason) {
      setError(reason instanceof Error ? reason.message : 'SQL update failed.')
      return false
    } finally {
      setBusy(false)
    }
  }

  return (
    <section className="sql-operation-editor" aria-label="Structured SQL">
      <header className="sql-parameter-heading"><strong>SQL</strong><button className="icon-button" type="button" aria-label="Reset SQL to generated value" title="Reset SQL to generated value" disabled={readOnly || !(binding.id in values)} onClick={onReset}><RotateCcw size={14} /></button></header>
      {error && <p className="sql-edit-error" role="alert">{error}</p>}
      {busy && !model && <p className="empty-copy">Loading structured SQL…</p>}
      {model && !readOnly && (
        <div className="sql-structured-sections">
          <Selections model={model} onAction={act} disabled={busy} />
          <Filters model={model} onAction={act} disabled={busy} />
          <Joins model={model} onAction={act} disabled={busy} />
        </div>
      )}
      {model?.read_only_reason && <p className="read-only-note">{model.read_only_reason}</p>}
      <details className="raw-sql-details">
        <summary>Raw SQL</summary>
        <pre className="raw-code raw-sql">{model?.source ?? String(effectiveSql ?? binding.value ?? '')}</pre>
        <small>Raw SQL is displayed only; structural changes are validated and transformed by vg2c.</small>
      </details>
    </section>
  )
}

function Selections({ model, onAction, disabled }: SectionProps) {
  const [adding, setAdding] = useState(false)
  return (
    <section className="sql-section">
      <header><h4>Selected attributes</h4><button type="button" disabled={disabled || !model.capabilities.selected} onClick={() => setAdding(true)}>Add</button></header>
      {adding && <SelectionAddForm disabled={disabled} onAction={onAction} onClose={() => setAdding(false)} />}
      {model.selections.map((item, index) => <SelectionRow key={item.id} item={item} index={index} count={model.selections.length} disabled={disabled} onAction={onAction} />)}
    </section>
  )
}

function SelectionAddForm({ disabled, onAction, onClose }: AddFormProps) {
  const [expression, setExpression] = useState('')
  function submit(event: FormEvent<HTMLFormElement>) {
    event.preventDefault()
    const next = expression.trim()
    if (!next) return
    void onAction('add-selection', { expression: next }).then((success) => {
      if (success) onClose()
    })
  }
  return <form className="sql-add-panel" onSubmit={submit}>
    <label><span className="sr-only">Selected expression</span><input autoFocus value={expression} disabled={disabled} onChange={(event) => setExpression(event.target.value)} placeholder="Selected expression" /></label>
    <div className="sql-add-panel__actions"><button type="button" onClick={onClose}>Cancel</button><button className="primary-button" type="submit" disabled={disabled || !expression.trim()}>Add selection</button></div>
  </form>
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
  const [adding, setAdding] = useState(false)
  return (
    <section className="sql-section">
      <header><h4>Filters</h4><button type="button" disabled={disabled || !model.capabilities.filters} onClick={() => setAdding(true)}>Add</button></header>
      {adding && <FilterAddForm model={model} disabled={disabled} onAction={onAction} onClose={() => setAdding(false)} />}
      {model.filters.map((item) => <PredicateRow key={item.id} item={item} operators={model.filter_operators} connectors={model.logical_connectors} disabled={disabled} onAction={onAction} />)}
      {!model.filters.length && !adding && <p className="empty-copy">No filters.</p>}
    </section>
  )
}

function FilterAddForm({ model, disabled, onAction, onClose }: ModelAddFormProps) {
  const [left, setLeft] = useState('')
  const [operator, setOperator] = useState(model.filter_operators[0] ?? '')
  const [right, setRight] = useState('')
  const [connector, setConnector] = useState(model.logical_connectors[0] ?? 'AND')
  function submit(event: FormEvent<HTMLFormElement>) {
    event.preventDefault()
    if (!left.trim() || !right.trim() || !operator) return
    void onAction('add-filter', { left: left.trim(), operator, right: right.trim(), connector }).then((success) => {
      if (success) onClose()
    })
  }
  return <form className="sql-add-panel sql-add-panel--join" onSubmit={submit}>
    <div className="sql-add-panel__row">
      <label>Left<input autoFocus value={left} disabled={disabled} onChange={(event) => setLeft(event.target.value)} /></label>
      <label>Operator<select value={operator} disabled={disabled} onChange={(event) => setOperator(event.target.value)}>{model.filter_operators.map((item) => <option key={item}>{item}</option>)}</select></label>
      <label>Right<input value={right} disabled={disabled} onChange={(event) => setRight(event.target.value)} /></label>
      <label>Connector<select value={connector} disabled={disabled} onChange={(event) => setConnector(event.target.value)}>{model.logical_connectors.map((item) => <option key={item}>{item}</option>)}</select></label>
    </div>
    <div className="sql-add-panel__actions"><button type="button" onClick={onClose}>Cancel</button><button className="primary-button" type="submit" disabled={disabled || !left.trim() || !right.trim() || !operator}>Add filter</button></div>
  </form>
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
  const [adding, setAdding] = useState(false)
  return (
    <section className="sql-section">
      <header><h4>Joins</h4><button type="button" disabled={disabled || !model.capabilities.joins} onClick={() => setAdding(true)}>Add</button></header>
      {adding && <JoinAddForm model={model} disabled={disabled} onAction={onAction} onClose={() => setAdding(false)} />}
      {model.joins.map((item) => <JoinRow key={item.id} item={item} model={model} disabled={disabled} onAction={onAction} />)}
      {!model.joins.length && !adding && <p className="empty-copy">No joins.</p>}
    </section>
  )
}

function JoinAddForm({ model, disabled, onAction, onClose }: ModelAddFormProps) {
  const [joinType, setJoinType] = useState(model.join_types[0] ?? '')
  const [source, setSource] = useState('')
  const [left, setLeft] = useState('')
  const [operator, setOperator] = useState(model.filter_operators[0] ?? '')
  const [right, setRight] = useState('')
  function submit(event: FormEvent<HTMLFormElement>) {
    event.preventDefault()
    if (!joinType || !source.trim() || !left.trim() || !operator || !right.trim()) return
    void onAction('add-join', { join_type: joinType, source: source.trim(), left: left.trim(), right: right.trim(), operator }).then((success) => {
      if (success) onClose()
    })
  }
  return <form className="sql-add-panel sql-add-panel--join" onSubmit={submit}>
    <div className="sql-add-panel__row">
      <label>Join type<select value={joinType} disabled={disabled} onChange={(event) => setJoinType(event.target.value)}>{model.join_types.map((item) => <option key={item}>{item}</option>)}</select></label>
      <label>Source<input autoFocus value={source} disabled={disabled} onChange={(event) => setSource(event.target.value)} /></label>
    </div>
    <div className="sql-add-panel__row">
      <label>Left key<input value={left} disabled={disabled} onChange={(event) => setLeft(event.target.value)} /></label>
      <label>Operator<select value={operator} disabled={disabled} onChange={(event) => setOperator(event.target.value)}>{model.filter_operators.map((item) => <option key={item}>{item}</option>)}</select></label>
      <label>Right key<input value={right} disabled={disabled} onChange={(event) => setRight(event.target.value)} /></label>
    </div>
    <div className="sql-add-panel__actions"><button type="button" onClick={onClose}>Cancel</button><button className="primary-button" type="submit" disabled={disabled || !joinType || !source.trim() || !left.trim() || !operator || !right.trim()}>Add join</button></div>
  </form>
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

type ActionFn = (action: SqlActionRequest['action'], args: Record<string, unknown>) => Promise<boolean>
interface SectionProps { model: SqlModelView; onAction: ActionFn; disabled: boolean }
interface AddFormProps { disabled: boolean; onAction: ActionFn; onClose: () => void }
interface ModelAddFormProps extends AddFormProps { model: SqlModelView }

function CommitInput({ value, disabled, ariaLabel, placeholder, onCommit }: { value: string; disabled: boolean; ariaLabel: string; placeholder?: string; onCommit: (value: string) => Promise<unknown> }) {
  const [draft, setDraft] = useState(value)
  useEffect(() => setDraft(value), [value])
  return <input aria-label={ariaLabel} placeholder={placeholder} disabled={disabled} value={draft} onChange={(event) => setDraft(event.target.value)} onBlur={() => { const next = draft.trim(); if (next !== value.trim()) void onCommit(next) }} />
}
