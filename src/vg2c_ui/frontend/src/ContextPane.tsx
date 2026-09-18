import { useMemo, useState, type KeyboardEvent, type ReactNode } from 'react'
import { ExternalLink, FileClock, Globe2, Mail } from 'lucide-react'

import type {
  CsvPreviewView,
  DocumentView,
  FileEffectView,
  FileEndpointView,
  SemanticBindingView,
  SemanticOperationView,
  SymbolView,
} from './contracts.generated'
import { effectiveBindingValue } from './workspaceState'

type ContextTab = 'files' | 'email' | 'globals'

interface Props {
  document: DocumentView
  values: Record<string, unknown>
  csv: CsvPreviewView | null
  csvPath: string | null
  csvError: string | null
  csvLoading: boolean
  onNavigate: (operationId: string, focus: boolean) => void
  onEdit: (binding: SemanticBindingView, value: unknown) => void
  onPreview: (effectId: string, endpoint: FileEndpointView) => void
}

export function ContextPane(props: Props) {
  const [tab, setTab] = useState<ContextTab>('files')
  const emailCount = props.document.semantic_operations.filter(isEmail).length
  return <section className="context-pane">
    <nav className="context-tabs" aria-label="Context views" role="tablist">
      <ContextTabButton id="files" current={tab} onSelect={setTab} icon={<FileClock size={15} />}>File Flow</ContextTabButton>
      <ContextTabButton id="email" current={tab} onSelect={setTab} icon={<Mail size={15} />}>Email{emailCount ? ` (${emailCount})` : ''}</ContextTabButton>
      <ContextTabButton id="globals" current={tab} onSelect={setTab} icon={<Globe2 size={15} />}>Globals</ContextTabButton>
    </nav>
    <div className="context-content">
      {tab === 'files' && <FileContext {...props} />}
      {tab === 'email' && <EmailContext key={props.document.id} document={props.document} values={props.values} onNavigate={props.onNavigate} onEdit={props.onEdit} />}
      {tab === 'globals' && <GlobalsContext symbols={props.document.symbols} onNavigate={props.onNavigate} />}
    </div>
  </section>
}

const CONTEXT_TABS: ContextTab[] = ['files', 'email', 'globals']

function ContextTabButton({ id, current, onSelect, icon, children }: { id: ContextTab; current: ContextTab; onSelect: (id: ContextTab) => void; icon: ReactNode; children: ReactNode }) {
  const index = CONTEXT_TABS.indexOf(id)
  return <button
    type="button"
    role="tab"
    aria-selected={current === id}
    tabIndex={current === id ? 0 : -1}
    onClick={() => onSelect(id)}
    onKeyDown={(event) => handleContextTabKey(event, index, onSelect)}
  >{icon}<span>{children}</span></button>
}

function handleContextTabKey(event: KeyboardEvent<HTMLButtonElement>, index: number, onSelect: (id: ContextTab) => void) {
  if (!['ArrowLeft', 'ArrowRight', 'Home', 'End'].includes(event.key)) return
  event.preventDefault()
  let nextIndex = index
  if (event.key === 'ArrowLeft') nextIndex = (index - 1 + CONTEXT_TABS.length) % CONTEXT_TABS.length
  if (event.key === 'ArrowRight') nextIndex = (index + 1) % CONTEXT_TABS.length
  if (event.key === 'Home') nextIndex = 0
  if (event.key === 'End') nextIndex = CONTEXT_TABS.length - 1
  const next = CONTEXT_TABS[nextIndex]
  if (!next) return
  onSelect(next)
  event.currentTarget.closest('[role="tablist"]')?.querySelectorAll<HTMLButtonElement>('[role="tab"]')[nextIndex]?.focus()
}

function FileContext({ document, csv, csvPath, csvError, csvLoading, onNavigate, onPreview }: Props) {
  const lifecycleKinds = new Set<FileEffectView['kind']>(['write', 'copy', 'move', 'transform', 'append', 'delete'])
  const lifecycle = document.effects.filter((effect) => lifecycleKinds.has(effect.kind)).slice().sort((left, right) => left.order - right.order)
  const required = document.files.filter((file) =>
    file.producer_refs.length === 0
    && file.consumer_refs.length > 0
    && ['external', 'missing', 'dynamic', 'possible'].includes(file.status))
  return <div className="context-section file-context">
    <header><p>Ordered file operations, including fan-in when several inputs produce one output.</p></header>
    {lifecycle.length
      ? lifecycle.map((effect) => <FileEffectRow key={effect.id} document={document} effect={effect} onNavigate={onNavigate} onPreview={onPreview} />)
      : <p className="empty-copy">No file lifecycle changes.</p>}
    {required.length > 0 && <section className="required-inputs"><h3>Required Inputs</h3>{required.map((file) => <RequiredFileRow key={file.id} document={document} file={file} onNavigate={onNavigate} onPreview={onPreview} />)}</section>}
    {csvPath && <section className="on-disk-preview" aria-label="On-disk CSV preview">
      <h3>CSV Preview</h3><p>{csvPath}</p>
      {csvLoading && <p role="status">Loading file…</p>}
      {csvError && <p role="alert" className="validation-error">{csvError}</p>}
      {csv && <CsvTable preview={csv} />}
    </section>}
  </div>
}

function FileEffectRow({
  document,
  effect,
  onNavigate,
  onPreview,
}: {
  document: DocumentView
  effect: FileEffectView
  onNavigate: Props['onNavigate']
  onPreview: Props['onPreview']
}) {
  const operation = document.semantic_operations.find((item) => item.id === effect.operation_id)
  const inputs = effect.inputs
  const outputs = effect.outputs
  return <article className="file-effect-row">
    <header>
      <button type="button" className="file-effect-operation" onClick={() => onNavigate(effect.operation_id, true)}>
        <ExternalLink size={13} />{operation?.display_name ?? humanizeEffect(effect.kind)}
      </button>
      <span className="state-pill">{humanizeEffect(effect.kind)}</span>
    </header>
    <div className="file-effect-flow" aria-label={`${inputs.length} inputs to ${outputs.length} outputs`}>
      <div className="file-endpoints">
        {inputs.length ? inputs.map((endpoint) => <FileEndpointChip key={endpoint.id} endpoint={endpoint} />) : <span className="file-endpoint file-endpoint--generated">Generated content</span>}
      </div>
      <span className="file-flow-arrow" aria-hidden="true">→</span>
      <div className="file-endpoints">
        {outputs.length
          ? outputs.map((endpoint) => <FileEndpointChip key={endpoint.id} endpoint={endpoint} onPreview={() => onPreview(effect.id, endpoint)} />)
          : <span className="file-endpoint file-endpoint--deleted">Deleted</span>}
      </div>
    </div>
    <div className="file-effect-meta">
      {inputs.length > 1 && <small>{inputs.length} inputs feed this operation.</small>}
      {effect.conditional && <small>Conditional</small>}
      {effect.in_loop && <small>Runs in a loop</small>}
      {effect.reason && <small>{effect.reason}</small>}
    </div>
  </article>
}

function FileEndpointChip({
  endpoint,
  onPreview,
}: {
  endpoint: FileEndpointView
  onPreview?: () => void
}) {
  const label = endpoint.path ?? endpoint.expression ?? 'Dynamic path'
  const canPreview = Boolean(onPreview && endpoint.path?.toLowerCase().endsWith('.csv'))
  return <span className={`file-endpoint file-endpoint--${endpoint.status}`}>
    <span title={label}>{label}</span>
    {canPreview && <button type="button" onClick={onPreview}>Preview CSV</button>}
  </span>
}

function RequiredFileRow({
  document,
  file,
  onNavigate,
  onPreview,
}: {
  document: DocumentView
  file: DocumentView['files'][number]
  onNavigate: Props['onNavigate']
  onPreview: Props['onPreview']
}) {
  const refs = uniqueRefs(file.consumer_refs)
  const preview = file.path?.toLowerCase().endsWith('.csv') ? findCsvEndpoint(document, file.path) : null
  return <article className="file-resource-row file-resource-row--compact">
    <header><strong>{file.path ?? 'Dynamic file'}</strong><span className={`state-pill state-pill--${file.status}`}>{file.status}</span></header>
    <div className="operation-references" aria-label="Used by">
      {refs.map((ref) => {
        const operation = document.semantic_operations.find((item) => item.id === ref.operation_id)
        return <button key={`${ref.operation_id}:${ref.binding_id ?? ''}`} type="button" onClick={() => onNavigate(ref.operation_id, true)}><ExternalLink size={13} />{operation?.display_name ?? ref.operation_id}</button>
      })}
      {preview && <button type="button" onClick={() => onPreview(preview.effectId, preview.endpoint)}>Preview CSV</button>}
    </div>
  </article>
}

function humanizeEffect(kind: FileEffectView['kind']): string {
  if (kind === 'write') return 'Create'
  if (kind === 'transform') return 'Transform'
  return kind.charAt(0).toUpperCase() + kind.slice(1)
}

function findCsvEndpoint(document: DocumentView, path: string): { effectId: string; endpoint: FileEndpointView } | null {
  for (const effect of document.effects) {
    for (const endpoint of [...effect.outputs, ...effect.inputs]) {
      if (endpoint.path === path) return { effectId: effect.id, endpoint }
    }
  }
  return null
}

function EmailContext({
  document,
  values,
  onNavigate,
  onEdit,
}: {
  document: DocumentView
  values: Record<string, unknown>
  onNavigate: Props['onNavigate']
  onEdit: Props['onEdit']
}) {
  const operations = document.semantic_operations.filter(isEmail)
  const [selected, setSelected] = useState<Set<string>>(new Set())
  const editableEnabled = operations.flatMap((operation) => {
    const binding = operation.bindings.find((item) => item.name === 'enabled')
    return binding ? [{ operation, binding }] : []
  })

  function setMany(items: Array<{ operation: SemanticOperationView; binding: SemanticBindingView }>, enabled: boolean) {
    for (const { binding } of items) onEdit(binding, enabled)
  }

  if (!operations.length) return <div className="context-section"><p className="empty-copy context-empty-copy">This script does not send email.</p></div>
  return <div className="context-section email-context">
    <header><p>Review Send Email actions. Bulk editing is limited to enable/disable.</p></header>
    <div className="email-bulk-actions">
      <button type="button" disabled={!selected.size} onClick={() => setMany(editableEnabled.filter(({ operation }) => selected.has(operation.id)), true)}>Enable selected</button>
      <button type="button" disabled={!selected.size} onClick={() => setMany(editableEnabled.filter(({ operation }) => selected.has(operation.id)), false)}>Disable selected</button>
      <button type="button" onClick={() => setMany(editableEnabled, true)}>Enable all</button>
      <button type="button" onClick={() => setMany(editableEnabled, false)}>Disable all</button>
    </div>
    {operations.map((operation) => {
      const enabled = operation.bindings.find((binding) => binding.name === 'enabled')
      const subject = operation.bindings.find((binding) => binding.name === 'subject')
      const to = operation.bindings.find((binding) => binding.name === 'to')
      return <article key={operation.id} className="email-row">
        <label className="checkbox-field"><input type="checkbox" checked={selected.has(operation.id)} onChange={(event) => setSelected((current) => toggleSet(current, operation.id, event.target.checked))} /><span className="sr-only">Select {operation.display_name}</span></label>
        <label className="checkbox-field email-enabled"><input type="checkbox" checked={enabled ? Boolean(effectiveBindingValue(values, enabled)) : true} disabled={!enabled?.editable} onChange={(event) => enabled && onEdit(enabled, event.target.checked)} /><span>Enabled</span></label>
        <button type="button" className="email-summary" onClick={(event) => onNavigate(operation.id, event.detail === 0)}>
          <strong>{String(subject ? effectiveBindingValue(values, subject) : 'Send Email')}</strong>
          <small>To: {String(to ? effectiveBindingValue(values, to) : 'Not set')}</small>
        </button>
        {!enabled && <p className="validation-error">This document predates the Email enabled contract. Reload after recompilation.</p>}
      </article>
    })}
  </div>
}

function GlobalsContext({ symbols, onNavigate }: { symbols: SymbolView[]; onNavigate: Props['onNavigate'] }) {
  const [query, setQuery] = useState('')
  const filtered = useMemo(() => symbols.filter((symbol) => symbol.display_name.toLowerCase().includes(query.trim().toLowerCase())), [symbols, query])
  return <div className="context-section globals-context">
    <header><h3>Globals & Macros</h3><p>Known values and runtime symbols used by this script.</p></header>
    <label className="search-field"><span className="sr-only">Search globals</span><input type="search" value={query} onChange={(event) => setQuery(event.target.value)} placeholder="Search globals" /></label>
    {filtered.map((symbol) => <article key={symbol.id} className="symbol-row">
      <header><strong>{symbol.display_name}</strong><span>{symbol.kind}</span></header>
      <p>{symbol.value_state === 'known' ? String(symbol.value ?? '') : symbol.value_state === 'runtime' ? 'Resolved at runtime' : 'Value unknown'}</p>
      {symbol.introduction && <button type="button" onClick={(event) => onNavigate(symbol.introduction!.operation_id, event.detail === 0)}>Definition</button>}
      <div className="operation-references">{symbol.references.map((ref, index) => <button key={`${ref.operation_id}:${ref.binding_id}:${index}`} type="button" onClick={(event) => onNavigate(ref.operation_id, event.detail === 0)}>Reference {index + 1}</button>)}</div>
    </article>)}
    {!filtered.length && <p className="empty-copy">No matching globals or macros.</p>}
  </div>
}

function isEmail(operation: SemanticOperationView): boolean {
  return operation.capabilities.includes('email')
}

function uniqueRefs<T extends { operation_id: string; binding_id: string | null }>(refs: T[]): T[] {
  const seen = new Set<string>()
  return refs.filter((ref) => {
    const key = `${ref.operation_id}:${ref.binding_id ?? ''}`
    if (seen.has(key)) return false
    seen.add(key)
    return true
  })
}

function toggleSet(current: Set<string>, value: string, enabled: boolean): Set<string> {
  const next = new Set(current)
  if (enabled) next.add(value)
  else next.delete(value)
  return next
}

function CsvTable({ preview }: { preview: CsvPreviewView }) {
  return <div className="csv-preview"><small>{preview.size_bytes.toLocaleString()} bytes{preview.truncated ? ' / truncated' : ''}</small><div className="table-scroll"><table><thead><tr>{preview.columns.map((column, index) => <th key={`${column}-${index}`}>{column}</th>)}</tr></thead><tbody>{preview.rows.map((row, rowIndex) => <tr key={rowIndex}>{row.map((cell, index) => <td key={index}>{cell}</td>)}</tr>)}</tbody></table></div></div>
}
