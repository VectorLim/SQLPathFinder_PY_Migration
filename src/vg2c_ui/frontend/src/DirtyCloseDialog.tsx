import './dirtyCloseDialog.css'

import { useRef } from 'react'

import { baseName } from './pathDisplay'
import type { TabState } from './workspace/state'
import { useModalDialog } from './useModalDialog'

interface Props {
  tab: TabState | null
  onCancel: () => void
  onDiscard: (id: string) => void
}

export function DirtyCloseDialog({ tab, onCancel, onDiscard }: Props) {
  const keepEditingRef = useRef<HTMLButtonElement>(null)
  const modal = useModalDialog(Boolean(tab), onCancel, { onOpened: () => keepEditingRef.current?.focus() })
  if (!tab) return <dialog ref={modal.dialogRef} className="dirty-close-dialog" />

  const name = baseName(tab.document.source_path || tab.document.output_path)
  const changeCount = Object.keys(tab.edits.values).length

  function discard() {
    const id = tab!.document.id
    modal.requestClose()
    queueMicrotask(() => onDiscard(id))
  }

  return <dialog
    ref={modal.dialogRef}
    className="dirty-close-dialog"
    aria-labelledby="dirty-close-title"
    aria-describedby="dirty-close-description"
    onClose={modal.handleClose}
    onClick={modal.handleBackdropClick}
  >
    <div className="dirty-close-card">
      <span className="eyebrow">Unsaved changes</span>
      <h2 id="dirty-close-title">Close {name}?</h2>
      <p id="dirty-close-description">
        {changeCount} unapplied change{changeCount === 1 ? '' : 's'} will be discarded.
      </p>
      <div className="dirty-close-actions">
        <button ref={keepEditingRef} type="button" onClick={modal.requestClose}>Keep editing</button>
        <button className="danger-button" type="button" onClick={discard}>Discard changes</button>
      </div>
    </div>
  </dialog>
}
