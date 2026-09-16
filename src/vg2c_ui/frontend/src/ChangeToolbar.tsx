import type { TabState, TabStatus } from './workspaceState'

interface Props {
  tab: TabState
  onUndo: () => void
  onRedo: () => void
  onValidate: () => void
  onApply: () => void
  onReload: () => void
}

export function ChangeToolbar({ tab, onUndo, onRedo, onValidate, onApply, onReload }: Props) {
  const editCount = Object.keys(tab.edits.values).length
  return <div className="change-toolbar">
    <div className="change-status">
      <strong>{editCount ? `${editCount} unsaved change${editCount === 1 ? '' : 's'}` : 'No pending changes'}</strong>
      <small>{statusCopy(tab.status)}</small>
    </div>
    <div className="toolbar-group" role="group" aria-label="Change actions">
      <button type="button" onClick={onUndo} disabled={!tab.edits.history.length}>Undo</button>
      <button type="button" onClick={onRedo} disabled={!tab.edits.future.length}>Redo</button>
      <button type="button" onClick={onValidate} disabled={!editCount || tab.status === 'validating' || !tab.document.synchronized}>Preview changes</button>
      <button className="primary-button" type="button" onClick={onApply} disabled={!tab.preview?.valid || tab.status === 'saving'}>Apply changes</button>
      {tab.status === 'conflict' && <button type="button" onClick={onReload}>Reload</button>}
    </div>
  </div>
}

function statusCopy(status: TabStatus): string {
  if (status === 'dirty') return 'Preview before applying.'
  if (status === 'validating') return 'Validating generated Python…'
  if (status === 'valid') return 'Validation passed.'
  if (status === 'invalid') return 'Validation found issues.'
  if (status === 'saving') return 'Applying changes…'
  if (status === 'conflict') return 'File changed externally; reload required.'
  if (status === 'error') return 'The last update failed.'
  return 'Select an operation to inspect or edit it.'
}
