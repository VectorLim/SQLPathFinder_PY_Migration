import { useEffect, useId, useState, type KeyboardEvent, type ReactNode } from 'react'

import type { SymbolView } from '../contracts.generated'

export function ValidationMessage({ message, id }: { message: string | null | undefined; id?: string }) {
  if (!message) return null
  return <p id={id} className="validation-error" role="alert">{message}</p>
}

export function FileSelector({
  label,
  value,
  files,
  disabled = false,
  onChange,
}: {
  label: string
  value: unknown
  files: string[]
  disabled?: boolean
  onChange: (value: string) => void
}) {
  const current = typeof value === 'string' ? value : ''
  const known = [...new Set(files.filter(Boolean))].sort()
  const matched = known.includes(current)
  const [manual, setManual] = useState(!matched)
  useEffect(() => setManual(!known.includes(current)), [current, known.join('\u0000')])
  const selectValue = manual ? '__manual__' : current

  return <div className="semantic-file-selector">
    <label>{label}
      <select
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
        <option value="__manual__">Manual / external path…</option>
        {known.map((path) => <option key={path} value={path}>{path}</option>)}
      </select>
    </label>
    {manual && <input
      aria-label={`${label} path`}
      type="text"
      value={current}
      disabled={disabled}
      placeholder="Enter workspace or external path"
      onChange={(event) => onChange(event.target.value)}
    />}
  </div>
}

export function FileListSelector({
  label,
  value,
  files,
  disabled = false,
  onChange,
  onUpload,
}: {
  label: string
  value: unknown
  files: string[]
  disabled?: boolean
  onChange: (value: string[]) => void
  onUpload?: (file: File) => Promise<string>
}) {
  const items = Array.isArray(value) ? value.map(String) : []
  const inputId = useId()
  return <div className="semantic-file-list">
    <span className="field-label">{label}</span>
    {items.map((item, index) => <div className="collection-row" key={`${index}:${item}`}>
      <FileSelector label={`${label} ${index + 1}`} value={item} files={files} disabled={disabled} onChange={(next) => onChange(items.map((current, position) => position === index ? next : current))} />
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
  disabled = false,
  onChange,
}: {
  label: string
  value: unknown
  symbols: SymbolView[]
  disabled?: boolean
  onChange: (value: string) => void
}) {
  const listId = useId()
  return <label>{label}
    <input
      type="text"
      list={listId}
      value={typeof value === 'string' ? value : ''}
      disabled={disabled}
      onChange={(event) => onChange(event.target.value)}
    />
    <datalist id={listId}>
      {symbols
        .filter((symbol) => symbol.condition_value)
        .map((symbol) => <option key={symbol.id} value={symbol.condition_value ?? symbol.display_name}>{symbol.display_name}</option>)}
    </datalist>
  </label>
}

export function OptionalValue({
  label,
  enabled,
  disabled = false,
  onEnabledChange,
  children,
}: {
  label: string
  enabled: boolean
  disabled?: boolean
  onEnabledChange: (enabled: boolean) => void
  children: ReactNode
}) {
  return <div className="optional-value">
    <label className="checkbox-field">
      <input type="checkbox" checked={enabled} disabled={disabled} onChange={(event) => onEnabledChange(event.target.checked)} />
      <span>{label}</span>
    </label>
    {enabled && children}
  </div>
}

export function ReorderableList<T>({
  items,
  getId,
  disabled = false,
  onReorder,
  renderItem,
}: {
  items: T[]
  getId: (item: T) => string
  disabled?: boolean
  onReorder: (fromIndex: number, toIndex: number) => void
  renderItem: (item: T, index: number) => ReactNode
}) {
  const [dragIndex, setDragIndex] = useState<number | null>(null)
  const [announcement, setAnnouncement] = useState('')

  function move(fromIndex: number, toIndex: number) {
    if (disabled || fromIndex === toIndex || toIndex < 0 || toIndex >= items.length) return
    onReorder(fromIndex, toIndex)
    setAnnouncement(`Moved item ${fromIndex + 1} to position ${toIndex + 1}.`)
  }

  function onKeyDown(event: KeyboardEvent<HTMLDivElement>, index: number) {
    if (!event.altKey || disabled) return
    if (event.key === 'ArrowUp') {
      event.preventDefault()
      move(index, index - 1)
    } else if (event.key === 'ArrowDown') {
      event.preventDefault()
      move(index, index + 1)
    }
  }

  return <div className="reorderable-list" role="list">
    <span className="sr-only" aria-live="polite">{announcement}</span>
    {items.map((item, index) => <div
      key={getId(item)}
      className="reorderable-item"
      tabIndex={disabled ? -1 : 0}
      role="listitem"
      aria-label={`Item ${index + 1} of ${items.length}. Alt plus arrow keys reorder.`}
      onKeyDown={(event) => onKeyDown(event, index)}
      onDragOver={(event) => { if (!disabled) event.preventDefault() }}
      onDrop={() => {
        if (dragIndex !== null) move(dragIndex, index)
        setDragIndex(null)
      }}
    >
      <span
        className="reorder-drag-handle"
        draggable={!disabled}
        aria-hidden="true"
        onDragStart={() => setDragIndex(index)}
        onDragEnd={() => setDragIndex(null)}
      >⋮⋮</span>
      {renderItem(item, index)}
    </div>)}
  </div>
}
