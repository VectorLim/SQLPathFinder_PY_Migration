import { useState, type KeyboardEvent, type ReactNode } from 'react'
import { ArrowDown, ArrowUp, GripVertical } from 'lucide-react'

const MIME = 'application/x-sqlpathfinder-list-item-id'

export function ReorderableList<T>({
  items, getId, disabled = false, onReorder, renderItem,
}: {
  items: T[]
  getId: (item: T) => string
  disabled?: boolean
  onReorder: (sourceId: string, targetId: string) => void
  renderItem: (item: T, index: number) => ReactNode
}) {
  const [dragSourceId, setDragSourceId] = useState<string | null>(null)
  const [announcement, setAnnouncement] = useState('')

  function move(sourceId: string, targetId: string) {
    const sourceIndex = items.findIndex((item) => getId(item) === sourceId)
    const targetIndex = items.findIndex((item) => getId(item) === targetId)
    if (disabled || sourceIndex < 0 || targetIndex < 0 || sourceIndex === targetIndex) return
    onReorder(sourceId, targetId)
    setAnnouncement(`Requested move of item ${sourceIndex + 1} to position ${targetIndex + 1}.`)
  }

  function onKeyDown(event: KeyboardEvent<HTMLDivElement>, index: number) {
    if (!event.altKey || disabled) return
    const target = event.key === 'ArrowUp' ? items[index - 1] :
      event.key === 'ArrowDown' ? items[index + 1] : null
    if (!target) return
    event.preventDefault()
    move(getId(items[index]), getId(target))
  }

  return <div className="reorderable-list" role="list">
    <span className="sr-only" role="status" aria-live="polite">{announcement}</span>
    {items.map((item, index) => {
      const id = getId(item)
      return <div key={id} className={`reorderable-item${dragSourceId && dragSourceId !== id ? ' is-drop-target' : ''}`}
        tabIndex={disabled ? -1 : 0} role="listitem"
        aria-label={`Item ${index + 1} of ${items.length}. Alt plus arrow keys reorder.`}
        onKeyDown={(event) => onKeyDown(event, index)}
        onDragOver={(event) => { if (!disabled && dragSourceId !== id) event.preventDefault() }}
        onDrop={(event) => {
          event.preventDefault()
          move(event.dataTransfer.getData(MIME), id)
          setDragSourceId(null)
        }}>
        <button type="button" className="reorder-drag-handle" aria-label={`Drag item ${index + 1}`}
          disabled={disabled} draggable={!disabled}
          onDragStart={(event) => {
            event.dataTransfer.setData(MIME, id)
            event.dataTransfer.effectAllowed = 'move'
            setDragSourceId(id)
          }} onDragEnd={() => setDragSourceId(null)}><GripVertical size={15} /></button>
        {renderItem(item, index)}
        <span className="reorder-touch-controls">
          <button type="button" aria-label={`Move item ${index + 1} up`} disabled={disabled || index === 0}
            onClick={() => move(id, getId(items[index - 1]))}><ArrowUp size={14} /></button>
          <button type="button" aria-label={`Move item ${index + 1} down`} disabled={disabled || index === items.length - 1}
            onClick={() => move(id, getId(items[index + 1]))}><ArrowDown size={14} /></button>
        </span>
      </div>
    })}
  </div>
}
