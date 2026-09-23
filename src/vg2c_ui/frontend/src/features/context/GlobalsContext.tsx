import { useMemo, useState } from 'react'
import type { DocumentView } from '../../api/contracts.generated'
import type { ContextProps } from './contextTypes'
import { effectiveBindingValue } from '../../workspace/state'

export function GlobalsContext({ document, values, onNavigate, onEdit }: {
  document: DocumentView
  values: Record<string, unknown>
  onNavigate: ContextProps['onNavigate']
  onEdit: ContextProps['onEdit']
}) {
  const [query, setQuery] = useState('')
  const filtered = useMemo(() => document.symbols.filter((symbol) => symbol.display_name.toLowerCase().includes(query.trim().toLowerCase())), [document.symbols, query])
  return <div className="context-section globals-context">
    <header><h3>Globals & Macros</h3><p>Known values and runtime symbols used by this script.</p></header>
    <label className="search-field"><span className="sr-only">Search globals</span><input type="search" value={query} onChange={(event) => setQuery(event.target.value)} placeholder="Search globals" /></label>
    {filtered.map((symbol) => {
      const binding = document.semantic_operations.flatMap((operation) => operation.bindings)
        .find((item) => item.id === symbol.value_binding_id && item.editable)
      return <article key={symbol.id} className="symbol-row">
      <header><strong>{symbol.display_name}</strong><span>{symbol.kind}</span></header>
      {binding && symbol.value_state === 'known'
        ? <label>Value<input type="text" aria-label={`${symbol.display_name} value`}
            value={String(effectiveBindingValue(values, binding) ?? '')}
            onChange={(event) => onEdit(binding, event.target.value)} /></label>
        : <p>{symbol.value_state === 'known' ? String(symbol.value ?? '') : symbol.value_state === 'runtime' ? 'Resolved at runtime' : 'Value unknown'}</p>}
      {symbol.introduction && <button type="button" onClick={(event) => onNavigate(symbol.introduction!.operation_id, event.detail === 0)}>Definition</button>}
      <div className="operation-references">{symbol.references.map((ref, index) => {
        const operation = document.semantic_operations.find((item) => item.id === ref.operation_id)
        return <button key={`${ref.operation_id}:${ref.binding_id}:${index}`} type="button" onClick={(event) => onNavigate(ref.operation_id, event.detail === 0)}>{operation?.display_name ?? 'Operation'} · {ref.context.replace('-', ' ')}</button>
      })}</div>
    </article>})}
    {!filtered.length && <p className="empty-copy">No matching globals or macros.</p>}
  </div>
}

