import { useMemo, useState, type ReactNode } from 'react'
import { ExternalLink, FileClock, Globe2, Mail } from 'lucide-react'

import type {
  CsvPreviewView,
  DocumentView,
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
  return <section className="context-pane" aria-label="Context">
    <nav className="context-tabs" aria-label="Context views" role="tablist">
      <ContextTabButton id="files" current={tab} onSelect={setTab} icon={<FileClock size={15} />}>File Flow</ContextTabButton>
      <ContextTabButton id="email" current={tab} onSelect={setTab} icon={<Mail size={15} />}>Email{emailCount ? ` (${emailCount})` : ''}</ContextTabButton>
      <ContextTabButton id="globals" current={tab} onSelect={setTab} icon={<Globe2 size={15} />}>Globals</ContextTabButton>
    </nav>
    <div className="context-content">
      {tab === 'files' && <FileContext {...props} />}
      {tab === 'email' && <EmailContext document={props.document} values={props.values} onNavigate={props.onNavigate} onEdit={props.onEdit} />}
      {tab === 'globals' && <GlobalsContext symbols={props.document.symbols} onNavigate={props.onNavigate} />}
    </div>
  </section>
}

function ContextTabButton({ id, current, onSelect, icon, children }: { id: ContextTab; current: ContextTab; onSelect: (id: ContextTab) => void; icon: ReactNode; children: ReactNode }) {
  return <button type="button" role="tab" aria-selected={current === id} onClick={() => onSelect(id)}>{icon}<span>{children}</span></button>
}

function FileContext({ document, csv, csvPath, csvError, csvLoading, onNavigate, onPreview }: Props) {
  const lifecycle = document.files.filter((file) => file.lifecycle_refs.length > 0)
  const required = document.files.filter((file) =>
    file.producer_refs.length === 0
    && file.consumer_refs.length > 0
    && ['external', 'missing', 'dynamic', 'possible'].includes(file.status))
  return <div className="context-section file-context">
    <header><h3>File Flow</h3><p>Files that are created, copied, moved, appended, renamed, or deleted.</p></header>
    {lifecycle.length ? lifecycle.map((file) => <FileResourceRow key={file.id} document={document} file={file} onNavigate={onNavigate} onPreview={onPreview} />) : <p className="empty-copy">No file lifecycle changes.</p>}
    {required.length > 0 && <section className="required-inputs"><h3>Required Inputs</h3>{required.map((file) => <FileResourceRow key={file.id} document={document} file={file} onNavigate={onNavigate} onPreview={onPreview} compact />)}</section>}
    {csvPath && <section className="on-disk-preview" aria-label="On-disk CSV preview">
      <h3>CSV Preview</h3><p>{csvPath}</p>
      {csvLoading && <p role="status">Loading file…</p>}
      {csvError && <p role="alert" className="validation-error">{csvError}</p>}
      {csv && <CsvTable preview={csv} />}
    </section>}
  </div>
}

function FileResourceRow({
  document,
  file,
  onNavigate,
  onPreview,
  compact = false,
}: {
  document: DocumentView
  file: DocumentView['files'][number]
  onNavigate: Props['onNavigate']
  onPreview: Props['onPreview']
  compact?: boolean
}) {
  const refs = uniqueRefs([...file.lifecycle_refs, ...file.producer_refs, ...file.consumer_refs])
  const preview = file.path?.toLowerCase().endsWith('.csv') ? findCsvEndpoint(document, file.path) : null
  return <article className={`file-resource-row${compact ? ' file-resource-row--compact' : ''}`}>
    <header><strong>{file.path ?? 'Dynamic file'}</strong><span className={`state-pill state-pill--${file.status}`}>{file.status}</span></header>
    {!compact && <p>{lifecycleSummary(document, file.lifecycle_refs.map((ref) => ref.operation_id))}</p>}
    <div className="operation-references" aria-label="Used by">
      {refs.map((ref) => {
        const operation = document.semantic_operations.find((item) => item.id === ref.operation_id)
        return <button key={`${ref.operation_id}:${ref.binding_id ?? ''}`} type="button" onClick={(event) => onNavigate(ref.operation_id, event.detail === 0)}><ExternalLink size={13} />{operation?.display_name ?? ref.operation_id}</button>
      })}
      {preview && <button type="button" onClick={() => onPreview(preview.effectId, preview.endpoint)}>Preview CSV</button>}
    </div>
  </article>
}

function lifecycleSummary(document: DocumentView, operationIds: string[]): string {
  const names = [...new Set(operationIds.map((id) => document.semantic_operations.find((operation) => operation.id === id)?.display_name).filter(Boolean))]
  return names.length ? names.join(' → ') : 'File lifecycle change'
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

  if (!operations.length) return <div className="context-section"><h3>Email</h3><p className="empty-copy">This script does not send email.</p></div>
  return <div className="context-section email-context">
    <header><h3>Email</h3><p>Review Send Email actions. Bulk editing is limited to enable/disable.</p></header>
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
