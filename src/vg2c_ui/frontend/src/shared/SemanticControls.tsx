import { useId, useState, type KeyboardEvent, type ReactNode } from 'react'

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

  return <div className="reorderable-list">
    <span className="sr-only" aria-live="polite">{announcement}</span>
    {items.map((item, index) => <div
      key={getId(item)}
      className="reorderable-item"
      draggable={!disabled}
      tabIndex={disabled ? -1 : 0}
      role="listitem"
      aria-label={`Item ${index + 1} of ${items.length}. Alt plus arrow keys reorder.`}
      onKeyDown={(event) => onKeyDown(event, index)}
      onDragStart={() => setDragIndex(index)}
      onDragOver={(event) => { if (!disabled) event.preventDefault() }}
      onDrop={() => {
        if (dragIndex !== null) move(dragIndex, index)
        setDragIndex(null)
      }}
      onDragEnd={() => setDragIndex(null)}
    >{renderItem(item, index)}</div>)}
  </div>
}
