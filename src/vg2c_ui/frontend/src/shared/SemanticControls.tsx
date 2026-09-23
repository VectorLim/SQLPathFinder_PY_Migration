import { useEffect, useId, useState, type ReactNode } from 'react'

import type { SymbolView } from '../api/contracts.generated'
import { isSymbolSelection, type SymbolSelection } from '../workspace/state'

export function ValidationMessage({ message, id }: { message: string | null | undefined; id?: string }) {
  if (!message) return null
  return <p id={id} className="validation-error" role="alert">{message}</p>
}

export function FileSelector({
  label,
  value,
  files,
  disabled = false,
  showLabel = true,
  onChange,
}: {
  label: string
  value: unknown
  files: string[]
  disabled?: boolean
  showLabel?: boolean
  onChange: (value: string) => void
}) {
  const current = typeof value === 'string' ? value : ''
  const known = [...new Set(files.filter(Boolean))].sort()
  const matched = known.includes(current)
  const [manual, setManual] = useState(!matched)
  useEffect(() => setManual(!known.includes(current)), [current, known.join('\u0000')])
  const selectValue = manual ? '__manual__' : current

  const select = <select
        aria-label={label}
        value={selectValue}
        disabled={disabled}
        onChange={(event) => {
          if (event.target.value === '__manual__') {
            setManual(true)
            return
          }
          setManual(false)
          onChange(event.target.value)
        }}
      >
        <option value="__manual__">Enter server workspace path…</option>
        {known.map((path) => <option key={path} value={path}>{path}</option>)}
      </select>

  return <div className="semantic-file-selector">
    {showLabel ? <label>{label}{select}</label> : select}
    {manual && <input
      aria-label={`${label} path`}
      type="text"
      value={current}
      disabled={disabled}
      placeholder="Path in your server workspace"
      onChange={(event) => onChange(event.target.value)}
    />}
  </div>
}

export function FileListSelector({
  label,
  value,
  files,
  disabled = false,
  showLabel = true,
  onChange,
  onUpload,
}: {
  label: string
  value: unknown
  files: string[]
  disabled?: boolean
  showLabel?: boolean
  onChange: (value: unknown[]) => void
  onUpload?: (file: File) => Promise<string>
}) {
  const items: unknown[] = Array.isArray(value) ? value : []
  const pathOf = (item: unknown) => Array.isArray(item) && item.length === 2 ? String(item[0]) : String(item ?? '')
  const withPath = (item: unknown, path: string) => Array.isArray(item) && item.length === 2 ? [path, item[1]] : path
  const inputId = useId()
  return <div className="semantic-file-list">
    {showLabel && <span className="field-label">{label}</span>}
    {items.map((item, index) => <div className="collection-row" key={`${index}:${pathOf(item)}`}>
      <FileSelector label={`${label} ${index + 1}`} showLabel={false} value={pathOf(item)} files={files} disabled={disabled} onChange={(next) => onChange(items.map((current, position) => position === index ? withPath(current, next) : current))} />
      {Array.isArray(item) && item.length === 2 && <small>Table: {String(item[1])}</small>}
      <button type="button" className="icon-button" disabled={disabled} aria-label={`Remove ${label} ${index + 1}`} onClick={() => onChange(items.filter((_, position) => position !== index))}>×</button>
    </div>)}
    <div className="field-command-row">
      <button type="button" className="field-command" disabled={disabled} onClick={() => onChange([...items, files[0] ?? ''])}>Add file</button>
      {onUpload && <>
        <label className="field-command" htmlFor={inputId}>Upload file</label>
        <input id={inputId} className="sr-only" type="file" disabled={disabled} onChange={(event) => {
          const file = event.target.files?.[0]
          event.currentTarget.value = ''
          if (!file) return
          void onUpload(file).then((path) => onChange([...items, path]))
        }} />
      </>}
    </div>
  </div>
}

export function SymbolSelector({
  label,
  value,
  symbols,
  symbolId,
  disabled = false,
  showLabel = true,
  onChange,
}: {
  label: string
  value: unknown
  symbols: SymbolView[]
  symbolId?: string | null
  disabled?: boolean
  showLabel?: boolean
  onChange: (value: string | SymbolSelection) => void
}) {
  const selectedId = isSymbolSelection(value) ? value.symbol_id : typeof value === 'string' ? '' : symbolId ?? ''
  const eligible = symbols.filter((symbol) => symbol.kind === 'macro' || symbol.kind === 'macro-row' || symbol.id === selectedId)
  const input = <input
      aria-label={label}
      type="text"
      value={typeof value === 'string' ? value : ''}
      disabled={disabled}
      onChange={(event) => onChange(event.target.value)}
    />
  const selector = <div className="symbol-selector">
    <select aria-label={`${label} mode`} value={selectedId} disabled={disabled}
      onChange={(event) => onChange(event.target.value ? { symbol_id: event.target.value } : '')}>
      <option value="">Literal value</option>
      {eligible.map((symbol) => <option key={symbol.id} value={symbol.id}>{symbol.display_name}{symbol.kind === 'unresolved' ? ' (unresolved)' : ''}</option>)}
    </select>
    {!selectedId && input}
  </div>
  return showLabel ? <label>{label}{selector}</label> : selector
}

export function OptionalValue({
  label,
  enabled,
  disabled = false,
  onEnabledChange,
  action,
  children,
}: {
  label: string
  enabled: boolean
  disabled?: boolean
  onEnabledChange: (enabled: boolean) => void
  action?: ReactNode
  children: ReactNode
}) {
  return <div className="optional-value">
    <div className="optional-value__header">
      <label className="checkbox-field">
        <input type="checkbox" checked={enabled} disabled={disabled} onChange={(event) => onEnabledChange(event.target.checked)} />
        <span>{label}</span>
      </label>
      {action}
    </div>
    {enabled && children}
  </div>
}
