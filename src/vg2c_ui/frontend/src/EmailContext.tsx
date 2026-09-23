import { useState } from 'react'
import type { DocumentView, SemanticBindingView, SemanticOperationView } from './api/contracts.generated'
import type { ContextProps } from './contextTypes'
import { effectiveBindingValue } from './workspace/state'

export function EmailContext({
  document,
  values,
  onNavigate,
  onEdit,
}: {
  document: DocumentView
  values: Record<string, unknown>
  onNavigate: ContextProps['onNavigate']
  onEdit: ContextProps['onEdit']
}) {
  const operations = document.semantic_operations.filter(isEmail)
  const [selected, setSelected] = useState<Set<string>>(new Set())
  const editableEnabled = operations.flatMap((operation) => {
    const binding = operation.bindings.find((item) => item.name === 'enabled')
    return binding?.editable ? [{ operation, binding }] : []
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
      const isEnabled = enabled ? Boolean(effectiveBindingValue(values, enabled)) : true
      return <article key={operation.id} className="email-row">
        <label className="checkbox-field"><input type="checkbox" aria-label={`Select ${operation.display_name}`} checked={selected.has(operation.id)} onChange={(event) => setSelected((current) => toggleSet(current, operation.id, event.target.checked))} /></label>
        <label className="checkbox-field email-enabled"><input type="checkbox" aria-label={`Enable ${operation.display_name}`} checked={isEnabled} disabled={!enabled?.editable} onChange={(event) => enabled && onEdit(enabled, event.target.checked)} /><span>{isEnabled ? 'On' : 'Off'}</span></label>
        <button type="button" className="email-summary" onClick={(event) => onNavigate(operation.id, event.detail === 0)}>
          <strong>{String(subject ? effectiveBindingValue(values, subject) : 'Send Email')}</strong>
          <small>To: {String(to ? effectiveBindingValue(values, to) : 'Not set')}</small>
        </button>
      </article>
    })}
  </div>
}


export function isEmail(operation: SemanticOperationView): boolean {
  return operation.capabilities.includes('email')
}


function toggleSet(current: Set<string>, value: string, enabled: boolean): Set<string> {
  const next = new Set(current)
  if (enabled) next.add(value)
  else next.delete(value)
  return next
}
