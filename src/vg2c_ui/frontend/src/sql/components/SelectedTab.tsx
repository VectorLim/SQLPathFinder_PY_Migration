import { useEffect, useRef, useState } from 'react'

import type { SqlMetadataSnapshot } from '../useSqlMetadata'
import type { SqlEditableModel } from '../model'
import { expressionSourceLabels } from '../presentation'
import { addSelection, removeSelection, reorderSelection, updateSelection } from '../transform'
import { SqlAttributePicker } from './SqlAttributePicker'
import { SqlExpressionField } from './SqlExpressionField'
import { SqlField } from './SqlField'

interface SelectedTabProps {
  model: SqlEditableModel
  metadata: SqlMetadataSnapshot
  onSqlChange: (sql: string) => void
  onError: (message: string) => void
}

interface TouchDragState {
  timer: number | null
  sourceId: string | null
  targetId: string | null
  startX: number
  startY: number
  active: boolean
}

export function SelectedTab({ model, metadata, onSqlChange, onError }: SelectedTabProps) {
  const available = metadata.attributes.filter((option) => (
    !model.selections.some((selection) => selection.expression === option.expression)
  ))
  const [adding, setAdding] = useState(false)
  const [pendingAttribute, setPendingAttribute] = useState('')
  const [draggingId, setDraggingId] = useState<string | null>(null)
  const [dragTargetId, setDragTargetId] = useState<string | null>(null)
  const touchDrag = useRef<TouchDragState>({
    timer: null,
    sourceId: null,
    targetId: null,
    startX: 0,
    startY: 0,
    active: false,
  })

  useEffect(() => () => clearTouchTimer(), [])

  function apply(action: () => { sql: string }): boolean {
    try {
      onSqlChange(action().sql)
      onError('')
      return true
    } catch (error) {
      onError(error instanceof Error ? error.message : 'Could not update selected expression.')
      return false
    }
  }

  function reorder(sourceId: string, targetId: string | null) {
    if (!targetId || sourceId === targetId) return
    const targetIndex = model.selections.findIndex((selection) => selection.id === targetId)
    if (targetIndex >= 0) apply(() => reorderSelection(model.source, sourceId, targetIndex))
  }

  function beginTouchDrag(event: any, sourceId: string) {
    if (event.pointerType === 'mouse') return
    clearTouchTimer()
    touchDrag.current = {
      timer: window.setTimeout(() => {
        touchDrag.current.active = true
        setDraggingId(sourceId)
        setDragTargetId(sourceId)
      }, 260),
      sourceId,
      targetId: sourceId,
      startX: event.clientX,
      startY: event.clientY,
      active: false,
    }
  }

  function moveTouchDrag(event: any) {
    const state = touchDrag.current
    if (!state.sourceId) return
    if (!state.active && Math.hypot(event.clientX - state.startX, event.clientY - state.startY) > 8) {
      clearTouchTimer()
      state.sourceId = null
      return
    }
    if (!state.active) return
    event.preventDefault()
    const row = document.elementFromPoint(event.clientX, event.clientY)?.closest('[data-selection-id]') as HTMLElement | null
    const targetId = row?.dataset.selectionId ?? null
    if (targetId) {
      state.targetId = targetId
      setDragTargetId(targetId)
    }
  }

  function endTouchDrag(event: any) {
    const state = touchDrag.current
    if (state.active) {
      event.preventDefault()
      reorder(state.sourceId!, state.targetId)
    }
    resetTouchDrag()
  }

  function clearTouchTimer() {
    if (touchDrag.current.timer !== null) window.clearTimeout(touchDrag.current.timer)
    touchDrag.current.timer = null
  }

  function resetTouchDrag() {
    clearTouchTimer()
    touchDrag.current.sourceId = null
    touchDrag.current.targetId = null
    touchDrag.current.active = false
    setDraggingId(null)
    setDragTargetId(null)
  }

  return (
    <div className="sql-section" role="tabpanel" id="sql-panel-selected" aria-labelledby="sql-tab-selected">
      {model.selections.length ? model.selections.map((selection, index) => (
        <div
          className={`sql-row sql-selection${selection.editable ? '' : ' is-readonly'}${draggingId === selection.id ? ' is-dragging' : ''}${dragTargetId === selection.id && draggingId !== selection.id ? ' is-drop-target' : ''}`}
          key={selection.id}
          data-selection-id={selection.id}
          onDragOver={(event) => {
            if (!draggingId) return
            event.preventDefault()
            setDragTargetId(selection.id)
          }}
          onDrop={(event) => {
            event.preventDefault()
            const sourceId = event.dataTransfer.getData('text/sql-selection') || draggingId
            if (sourceId) reorder(sourceId, selection.id)
            setDraggingId(null)
            setDragTargetId(null)
          }}
        >
          {selection.editable ? (
            <>
              <button
                type="button"
                className="sql-drag-handle"
                draggable
                aria-label={`Drag ${selection.expression} to reorder`}
                title="Drag to reorder"
                onDragStart={(event) => {
                  event.dataTransfer.effectAllowed = 'move'
                  event.dataTransfer.setData('text/sql-selection', selection.id)
                  setDraggingId(selection.id)
                }}
                onDragEnd={() => {
                  setDraggingId(null)
                  setDragTargetId(null)
                }}
                onPointerDown={(event) => beginTouchDrag(event, selection.id)}
                onPointerMove={moveTouchDrag}
                onPointerUp={endTouchDrag}
                onPointerCancel={resetTouchDrag}
              >
                <span aria-hidden="true">⠿</span>
              </button>
              <SqlExpressionField
                model={model}
                className="sql-field sql-field--expression"
                value={selection.expression}
                ariaLabel={`Selected expression ${index + 1}`}
                sourceLabels={expressionSourceLabels(selection.expression, model, metadata.attributes)}
                onCommit={(value) => apply(() => updateSelection(model.source, selection.id, { expression: value }))}
              />
              <span className="sql-as" aria-hidden="true">AS</span>
              <SqlField
                className="sql-field sql-field--alias"
                value={selection.alias ?? ''}
                ariaLabel={`Output name for selected expression ${index + 1}`}
                placeholder="output name"
                onCommit={(value) => apply(() => updateSelection(model.source, selection.id, { alias: value || null }))}
              />
              <button
                type="button"
                className="icon-button icon-button--danger sql-remove"
                aria-label={`Remove ${selection.expression}`}
                disabled={model.selections.length === 1}
                onClick={() => apply(() => removeSelection(model.source, selection.id))}
              >×</button>
            </>
          ) : (
            <>
              <span className="sql-drag-handle sql-drag-handle--disabled" aria-hidden="true">⠿</span>
              <div className="sql-raw-row">
                <code>{selection.raw}</code>
                {selection.readOnlyReason && <small>{selection.readOnlyReason}</small>}
              </div>
            </>
          )}
        </div>
      )) : <p className="empty-copy">No selected expressions could be isolated safely.</p>}

      {available.length > 0 && !adding && (
        <div className="sql-centered-action">
          <button type="button" className="sql-add-button" onClick={() => setAdding(true)}>+ Add attribute</button>
        </div>
      )}

      {available.length > 0 && adding && (
        <div className="sql-add-panel" aria-label="Add selected attribute">
          <SqlAttributePicker
            model={model}
            options={available}
            value={pendingAttribute}
            ariaLabel="Available attribute"
            onChange={setPendingAttribute}
          />
          <div className="sql-add-panel__actions">
            <button type="button" className="secondary-button" onClick={() => { setAdding(false); setPendingAttribute('') }}>Cancel</button>
            <button type="button" disabled={!pendingAttribute} onClick={() => {
              if (apply(() => addSelection(model.source, pendingAttribute))) {
                setPendingAttribute('')
                setAdding(false)
              }
            }}>Add</button>
          </div>
        </div>
      )}

    </div>
  )
}
