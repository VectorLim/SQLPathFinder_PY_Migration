import './sql/sqlEditor.css'

import { useEffect, useState, type FormEvent, type KeyboardEvent } from 'react'
import { RotateCcw } from 'lucide-react'

import type {
  SemanticBindingView,
  SqlJoinView,
  SqlModelView,
  SqlPredicateView,
  SqlSelectionView,
} from './api/contracts.generated'
import { ReorderableList } from './sql/ReorderableList'
import { sqlCommand, type SqlCommand, type SqlCommandArguments, type SqlCommandName } from './api/sqlActions'
import { effectiveBindingValue } from './workspaceState'

interface Props {
  binding: SemanticBindingView
  readOnly: boolean
  fileListReadOnly: boolean
  onReset: () => void
  values: Record<string, unknown>
  inspect: (bindingId: string) => Promise<SqlModelView>
  runCommand: (bindingId: string, command: SqlCommand) => Promise<SqlModelView>
}

type SqlTab = 'columns' | 'filters' | 'joins'
const SQL_TABS: Array<{ id: SqlTab; label: string }> = [
  { id: 'columns', label: 'Columns' },
  { id: 'filters', label: 'Filters' },
  { id: 'joins', label: 'Joins' },
]


export function StructuredSqlEditor({ binding, values, readOnly, fileListReadOnly, inspect, runCommand, onReset }: Props) {
  const effectiveSql = effectiveBindingValue(values, binding)
  const [model, setModel] = useState<SqlModelView | null>(null)
  const [error, setError] = useState('')
  const [busy, setBusy] = useState(false)
  const [tab, setTab] = useState<SqlTab>('columns')

  useEffect(() => {
    let cancelled = false
    setBusy(true)
    void inspect(binding.id)
      .then((next) => { if (!cancelled) { setModel(next); setError('') } })
      .catch((reason) => { if (!cancelled) setError(reason instanceof Error ? reason.message : 'Could not inspect SQL.') })
      .finally(() => { if (!cancelled) setBusy(false) })
    return () => { cancelled = true }
  }, [binding.id, values, typeof effectiveSql === 'string' ? effectiveSql : ''])

  async function act<T extends SqlCommandName>(action: T, args: SqlCommandArguments<T>): Promise<boolean> {
    setBusy(true)
    try {
      setModel(await runCommand(binding.id, sqlCommand(action, args)))
      setError('')
      return true
    } catch (reason) {
      setError(reason instanceof Error ? reason.message : 'SQL update failed.')
      return false
    } finally {
      setBusy(false)
    }
  }

  return <section className="sql-operation-editor" aria-label="Structured SQL">
    <header className="sql-parameter-heading">
      <strong>Query</strong>
      <button className="icon-button" type="button" aria-label="Reset SQL to generated value" title="Reset SQL to generated value" disabled={readOnly || !(binding.id in values)} onClick={onReset}><RotateCcw size={14} /></button>
    </header>
    {error && <p className="sql-edit-error" role="alert">{error}</p>}
    {busy && !model && <p className="empty-copy">Loading structured SQL…</p>}
    {model && <>
      <nav className="sql-tabs" aria-label="Query configuration" role="tablist">
        {SQL_TABS.map(({ id, label }, index) => {
          const count = id === 'columns' ? model.selections.length : id === 'filters' ? model.filters.length + model.file_lists.length : model.joins.length
          return <button
            id={`sql-tab-${id}`}
            key={id}
            type="button"
            role="tab"
            aria-selected={tab === id}
            aria-controls="sql-tab-panel"
            tabIndex={tab === id ? 0 : -1}
            onClick={() => setTab(id)}
            onKeyDown={(event) => handleSqlTabKey(event, index, setTab)}
          >{label}<span>{count}</span></button>
        })}
      </nav>
      <div id="sql-tab-panel" className="sql-tab-panel" role="tabpanel" aria-labelledby={`sql-tab-${tab}`}>
        {tab === 'columns' && <Selections model={model} onAction={act} disabled={busy || readOnly} />}
        {tab === 'filters' && <Filters model={model} onAction={act} disabled={busy || readOnly} fileDisabled={busy || fileListReadOnly} />}
        {tab === 'joins' && <Joins model={model} onAction={act} disabled={busy || readOnly} />}
      </div>
      {model.read_only_reason && <p className="read-only-note">{model.read_only_reason}</p>}
      <details className="sql-advanced">
        <summary>Advanced</summary>
        <details className="raw-sql-details">
          <summary>View raw SQL</summary>
          <pre className="raw-code raw-sql">{model.source ?? String(effectiveSql ?? binding.value ?? '')}</pre>
          <small>Raw SQL is reference-only. Safe structural changes use the backend SQL editor.</small>
        </details>
      </details>
    </>}
  </section>
}

function Selections({ model, onAction, disabled }: SectionProps) {
  const [adding, setAdding] = useState(false)
  const choices = model.column_choices.filter((choice) => model.sources.some((source) => source.id === choice.source_id))
  return <section className="sql-section">
    <header><h4>Columns</h4><button type="button" disabled={disabled || !model.capabilities.selected || !choices.length} onClick={() => setAdding(true)}>Add column</button></header>
    {adding && <SelectionAddForm choices={choices} disabled={disabled} onAction={onAction} onClose={() => setAdding(false)} />}
    <ReorderableList
      items={model.selections}
      getId={(item) => item.id}
      disabled={disabled || !model.capabilities.selected || model.selections.some((item) => !item.editable)}
      onReorder={(sourceId, targetId) => void onAction('reorder-selection', {
        selection_id: sourceId,
        target_index: model.selections.findIndex((item) => item.id === targetId),
      })}
      renderItem={(item) => <SelectionRow item={item} choices={choices} count={model.selections.length} disabled={disabled} onAction={onAction} />}
    />
    {!model.selections.length && !adding && <p className="empty-copy">No selected columns.</p>}
    {!choices.length && <p className="empty-copy">Column choices need an uploaded SQLite table with a readable header.</p>}
  </section>
}

function SelectionAddForm({ choices, disabled, onAction, onClose }: AddFormProps & { choices: SqlModelView['column_choices'] }) {
  const [choiceId, setChoiceId] = useState(choices[0]?.id ?? '')
  function submit(event: FormEvent<HTMLFormElement>) {
    event.preventDefault()
    if (!choiceId) return
    void onAction('add-selection', { column_choice_id: choiceId }).then((success) => { if (success) onClose() })
  }
  return <form className="sql-add-panel" onSubmit={submit}>
    <label>Column<select autoFocus value={choiceId} disabled={disabled} onChange={(event) => setChoiceId(event.target.value)}>{choices.map((choice) => <option key={choice.id} value={choice.id}>{choice.label}</option>)}</select></label>
    <div className="sql-add-panel__actions"><button type="button" onClick={onClose}>Cancel</button><button className="primary-button" type="submit" disabled={disabled || !choiceId}>Add column</button></div>
  </form>
}

function SelectionRow({ item, choices, count, disabled, onAction }: { item: SqlSelectionView; choices: SqlModelView['column_choices']; count: number; disabled: boolean; onAction: ActionFn }) {
  return <div className="sql-row sql-column-row">
    <strong className="sql-column-label" title={item.raw}>{item.display_label}</strong>
    {choices.length > 0 && <select aria-label={`Change ${item.display_label} column`} value="" disabled={disabled || !item.editable} onChange={(event) => void onAction('update-selection', { selection_id: item.id, column_choice_id: event.target.value })}>
      <option value="">Change column…</option>
      {choices.map((choice) => <option key={choice.id} value={choice.id}>{choice.label}</option>)}
    </select>}
    <CommitInput value={item.alias ?? ''} disabled={disabled || !item.editable} ariaLabel="Column alias" placeholder="alias" onCommit={(alias) => onAction('update-selection', { selection_id: item.id, alias: alias || null })} />
    <button type="button" disabled={disabled || count <= 1 || !item.editable} onClick={() => void onAction('remove-selection', { selection_id: item.id })}>Remove</button>
  </div>
}

function Filters({ model, onAction, disabled, fileDisabled }: SectionProps & { fileDisabled: boolean }) {
  const [adding, setAdding] = useState(false)
  const choices = model.column_choices.filter((choice) => model.sources.some((source) => source.id === choice.source_id))
  return <section className="sql-section">
    <header><h4>Filters</h4><button type="button" disabled={disabled || !model.capabilities.filters || !choices.length} onClick={() => setAdding(true)}>Add filter</button></header>
    {adding && <FilterAddForm model={model} disabled={disabled} onAction={onAction} onClose={() => setAdding(false)} />}
    {model.filters.map((item) => <PredicateRow key={item.id} item={item} choices={choices} operators={model.filter_operators} connectors={model.logical_connectors} disabled={disabled} onAction={onAction} />)}
    {model.file_lists.map((item) => <label className="sql-file-list" key={item.id}>
      <span>File list · {item.lead_in || String(item.column_ref)}</span>
      <select aria-label={`File list for ${item.lead_in || item.column_ref}`} value={item.path}
        disabled={fileDisabled || !item.choices.length}
        onChange={(event) => void onAction('update-file-list', { file_list_id: item.id, path: event.target.value })}>
        {!item.choices.includes(item.path) && <option value={item.path}>Current: {item.path}</option>}
        {item.choices.map((path) => <option key={path} value={path}>{path}</option>)}
      </select>
    </label>)}
    {!model.filters.length && !adding && <p className="empty-copy">No filters.</p>}
  </section>
}

function FilterAddForm({ model, disabled, onAction, onClose }: ModelAddFormProps) {
  const choices = model.column_choices.filter((choice) => model.sources.some((source) => source.id === choice.source_id))
  const [leftId, setLeftId] = useState(choices[0]?.id ?? '')
  const [operator, setOperator] = useState(model.filter_operators[0] ?? '')
  const [right, setRight] = useState('')
  const [connector, setConnector] = useState(model.logical_connectors[0] ?? 'AND')
  function submit(event: FormEvent<HTMLFormElement>) {
    event.preventDefault()
    if (!leftId || !right.trim() || !operator) return
    void onAction('add-filter', { left_choice_id: leftId, operator, right: right.trim(), connector }).then((success) => { if (success) onClose() })
  }
  return <form className="sql-add-panel sql-add-panel--join" onSubmit={submit}>
    <div className="sql-add-panel__row">
      <label>Column<select autoFocus value={leftId} disabled={disabled} onChange={(event) => setLeftId(event.target.value)}>{choices.map((choice) => <option key={choice.id} value={choice.id}>{choice.label}</option>)}</select></label>
      <label>Operator<select value={operator} disabled={disabled} onChange={(event) => setOperator(event.target.value)}>{model.filter_operators.map((item) => <option key={item}>{item}</option>)}</select></label>
      <label>Right<input value={right} disabled={disabled} onChange={(event) => setRight(event.target.value)} /></label>
      <label>Connector<select value={connector} disabled={disabled} onChange={(event) => setConnector(event.target.value)}>{model.logical_connectors.map((item) => <option key={item}>{item}</option>)}</select></label>
    </div>
    <div className="sql-add-panel__actions"><button type="button" onClick={onClose}>Cancel</button><button className="primary-button" type="submit" disabled={disabled || !leftId || !right.trim() || !operator}>Add filter</button></div>
  </form>
}

function PredicateRow({ item, choices, operators, connectors, disabled, onAction }: { item: SqlPredicateView; choices: SqlModelView['column_choices']; operators: string[]; connectors: string[]; disabled: boolean; onAction: ActionFn }) {
  return <div className="sql-row">
    {item.connector && <select aria-label="Filter connector" disabled={disabled || !item.editable} value={item.connector} onChange={(event) => void onAction('update-filter', { filter_id: item.id, connector: event.target.value })}>{connectors.map((connector) => <option key={connector}>{connector}</option>)}</select>}
    <span title={item.raw}>{item.left}</span>
    {choices.length > 0 && <select aria-label="Filter column" value="" disabled={disabled || !item.editable} onChange={(event) => void onAction('update-filter', { filter_id: item.id, left_choice_id: event.target.value })}>
      <option value="">Change column…</option>
      {choices.map((choice) => <option key={choice.id} value={choice.id}>{choice.label}</option>)}
    </select>}
    <select aria-label="Filter operator" disabled={disabled || !item.editable} value={item.operator} onChange={(event) => void onAction('update-filter', { filter_id: item.id, operator: event.target.value })}>{operators.map((operator) => <option key={operator}>{operator}</option>)}</select>
    <CommitInput value={item.right} disabled={disabled || !item.editable} ariaLabel="Filter right" onCommit={(right) => onAction('update-filter', { filter_id: item.id, right })} />
    <button type="button" disabled={disabled || !item.editable} onClick={() => void onAction('remove-filter', { filter_id: item.id })}>Remove</button>
  </div>
}

function Joins({ model, onAction, disabled }: SectionProps) {
  const [adding, setAdding] = useState(false)
  const canAdd = model.table_choices.length > 0 && model.column_choices.some((choice) => model.sources.some((source) => source.id === choice.source_id))
  return <section className="sql-section">
    <header><h4>Joins</h4><button type="button" disabled={disabled || !model.capabilities.joins || !canAdd} onClick={() => setAdding(true)}>Add join</button></header>
    {adding && <JoinAddForm model={model} disabled={disabled} onAction={onAction} onClose={() => setAdding(false)} />}
    {model.joins.map((item) => <JoinRow key={item.id} item={item} model={model} disabled={disabled} onAction={onAction} />)}
    {!model.joins.length && !adding && <p className="empty-copy">No joins.</p>}
  </section>
}

function JoinAddForm({ model, disabled, onAction, onClose }: ModelAddFormProps) {
  const [joinType, setJoinType] = useState(model.join_types[0] ?? '')
  const [tableId, setTableId] = useState(model.table_choices[0]?.id ?? '')
  const leftChoices = model.column_choices.filter((choice) => model.sources.some((source) => source.id === choice.source_id))
  const rightChoices = model.column_choices.filter((choice) => choice.source_id === tableId)
  const [leftId, setLeftId] = useState(leftChoices[0]?.id ?? '')
  const [operator, setOperator] = useState(model.filter_operators[0] ?? '')
  const [rightId, setRightId] = useState(rightChoices[0]?.id ?? '')
  const selectedRightId = rightChoices.some((choice) => choice.id === rightId) ? rightId : rightChoices[0]?.id ?? ''
  function submit(event: FormEvent<HTMLFormElement>) {
    event.preventDefault()
    if (!joinType || !tableId || !leftId || !operator || !selectedRightId) return
    void onAction('add-join', { join_type: joinType, table_choice_id: tableId, left_choice_id: leftId, right_choice_id: selectedRightId, operator }).then((success) => { if (success) onClose() })
  }
  return <form className="sql-add-panel sql-add-panel--join" onSubmit={submit}>
    <div className="sql-add-panel__row">
      <label>Join type<select value={joinType} disabled={disabled} onChange={(event) => setJoinType(event.target.value)}>{model.join_types.map((item) => <option key={item}>{item}</option>)}</select></label>
      <label>Table<select autoFocus value={tableId} disabled={disabled} onChange={(event) => setTableId(event.target.value)}>{model.table_choices.map((table) => <option key={table.id} value={table.id}>{table.label}</option>)}</select></label>
    </div>
    <div className="sql-add-panel__row">
      <label>Left key<select value={leftId} disabled={disabled} onChange={(event) => setLeftId(event.target.value)}>{leftChoices.map((choice) => <option key={choice.id} value={choice.id}>{choice.label}</option>)}</select></label>
      <label>Operator<select value={operator} disabled={disabled} onChange={(event) => setOperator(event.target.value)}>{model.filter_operators.map((item) => <option key={item}>{item}</option>)}</select></label>
      <label>Right key<select value={selectedRightId} disabled={disabled} onChange={(event) => setRightId(event.target.value)}>{rightChoices.map((choice) => <option key={choice.id} value={choice.id}>{choice.label}</option>)}</select></label>
    </div>
    <div className="sql-add-panel__actions"><button type="button" onClick={onClose}>Cancel</button><button className="primary-button" type="submit" disabled={disabled || !joinType || !tableId || !leftId || !operator || !selectedRightId}>Add join</button></div>
  </form>
}

function JoinRow({ item, model, disabled, onAction }: { item: SqlJoinView; model: SqlModelView; disabled: boolean; onAction: ActionFn }) {
  return <div className="sql-join-row">
    <div className="sql-row">
      <select aria-label="Join type" disabled={disabled || !item.editable_type} value={item.join_type} onChange={(event) => void onAction('update-join-type', { join_id: item.id, join_type: event.target.value })}>{model.join_types.map((joinType) => <option key={joinType}>{joinType}</option>)}</select>
      <strong title={item.source}>{item.source}</strong>
      <button type="button" disabled={disabled} onClick={() => void onAction('remove-join', { join_id: item.id })}>Remove join</button>
    </div>
    {item.predicates.map((predicate) => <div className="sql-row" key={predicate.id}>
      <span>{predicate.left}</span>
      {model.column_choices.length > 0 && <select aria-label="Join left key" value="" disabled={disabled || !predicate.editable} onChange={(event) => void onAction('update-join-predicate', { join_id: item.id, predicate_id: predicate.id, left_choice_id: event.target.value })}>
        <option value="">Change key…</option>
        {model.column_choices.map((choice) => <option key={choice.id} value={choice.id}>{choice.label}</option>)}
      </select>}
      <select aria-label="Join operator" disabled={disabled || !predicate.editable} value={predicate.operator} onChange={(event) => void onAction('update-join-predicate', { join_id: item.id, predicate_id: predicate.id, operator: event.target.value })}>{model.filter_operators.map((operator) => <option key={operator}>{operator}</option>)}</select>
      <span>{predicate.right}</span>
      {model.column_choices.length > 0 && <select aria-label="Join right key" value="" disabled={disabled || !predicate.editable} onChange={(event) => void onAction('update-join-predicate', { join_id: item.id, predicate_id: predicate.id, right_choice_id: event.target.value })}>
        <option value="">Change key…</option>
        {model.column_choices.map((choice) => <option key={choice.id} value={choice.id}>{choice.label}</option>)}
      </select>}
      <button type="button" disabled={disabled || item.predicates.length <= 1 || !predicate.editable} onClick={() => void onAction('remove-join-predicate', { join_id: item.id, predicate_id: predicate.id })}>Remove key</button>
    </div>)}
  </div>
}

type ActionFn = <T extends SqlCommandName>(action: T, args: SqlCommandArguments<T>) => Promise<boolean>
interface SectionProps { model: SqlModelView; onAction: ActionFn; disabled: boolean }
interface AddFormProps { disabled: boolean; onAction: ActionFn; onClose: () => void }
interface ModelAddFormProps extends AddFormProps { model: SqlModelView }

function CommitInput({ value, disabled, ariaLabel, placeholder, onCommit }: { value: string; disabled: boolean; ariaLabel: string; placeholder?: string; onCommit: (value: string) => Promise<unknown> }) {
  const [draft, setDraft] = useState(value)
  useEffect(() => setDraft(value), [value])
  return <input aria-label={ariaLabel} placeholder={placeholder} disabled={disabled} value={draft} onChange={(event) => setDraft(event.target.value)} onBlur={() => { const next = draft.trim(); if (next !== value.trim()) void onCommit(next) }} />
}

function handleSqlTabKey(
  event: KeyboardEvent<HTMLButtonElement>,
  index: number,
  setTab: (tab: SqlTab) => void,
) {
  if (!['ArrowLeft', 'ArrowRight', 'Home', 'End'].includes(event.key)) return
  event.preventDefault()
  let nextIndex = index
  if (event.key === 'ArrowLeft') nextIndex = (index - 1 + SQL_TABS.length) % SQL_TABS.length
  if (event.key === 'ArrowRight') nextIndex = (index + 1) % SQL_TABS.length
  if (event.key === 'Home') nextIndex = 0
  if (event.key === 'End') nextIndex = SQL_TABS.length - 1
  const next = SQL_TABS[nextIndex]
  if (!next) return
  setTab(next.id)
  event.currentTarget.closest('[role="tablist"]')?.querySelectorAll<HTMLButtonElement>('[role="tab"]')[nextIndex]?.focus()
}
