import './changeToolbar.css'
import { FileCheck2, Play, Redo2, RefreshCw, Save, Undo2 } from 'lucide-react'

import type { TabState, TabStatus } from './workspaceState'
import { getChangeActionState } from './workspaceGuards'

interface Props {
  tab: TabState
  onUndo: () => void
  onRedo: () => void
  onValidate: () => void
  onSave: () => void
  onGenerate: () => void
  onReload: () => void
}

export function ChangeToolbar({ tab, onUndo, onRedo, onValidate, onSave, onGenerate, onReload }: Props) {
  const actions = getChangeActionState(tab)
  return <div className="change-toolbar">
    <div className="change-status">
      <strong>{actions.editCount ? `${actions.editCount} unsaved change${actions.editCount === 1 ? '' : 's'}` : 'No pending changes'}</strong>
      <small>{statusCopy(tab.status)}</small>
      <span className="generation-state">{generationCopy(tab.document.generation_state)}</span>
      {Object.keys(tab.fieldDrafts).length > 0 && <span className="change-error" role="alert">{Object.keys(tab.fieldDrafts).length} invalid field draft(s)</span>}
      {tab.mutationError && <span className="change-error" role="alert">{tab.mutationError}</span>}
    </div>
    <div className="toolbar-group" role="group" aria-label="Change actions">
      <button className="icon-button" type="button" title="Undo" aria-label="Undo" onClick={onUndo} disabled={!actions.canUndo}><Undo2 size={16} /></button>
      <button className="icon-button" type="button" title="Redo" aria-label="Redo" onClick={onRedo} disabled={!actions.canRedo}><Redo2 size={16} /></button>
      <button type="button" onClick={onValidate} disabled={!actions.canPreview}><FileCheck2 size={16} aria-hidden="true" />Preview</button>
      <button className="primary-button" type="button" onClick={onSave} disabled={!actions.canSave}><Save size={16} aria-hidden="true" />Save</button>
      <button type="button" onClick={onGenerate} disabled={!actions.canGenerate}><Play size={16} aria-hidden="true" />Generate</button>
      {tab.status === 'conflict' && <button type="button" onClick={onReload} disabled={!actions.canReload}><RefreshCw size={16} aria-hidden="true" />Reload</button>}
    </div>
  </div>
}

function statusCopy(status: TabStatus): string {
  if (status === 'dirty') return 'Unvalidated changes.'
  if (status === 'validating') return 'Validating changes…'
  if (status === 'valid') return 'Validation passed.'
  if (status === 'invalid') return 'Validation found issues.'
  if (status === 'saving') return 'Saving changes…'
  if (status === 'generating') return 'Generating Python…'
  if (status === 'conflict') return 'File changed externally; reload required.'
  if (status === 'error') return 'The last update failed.'
  return 'No unsaved changes.'
}

function generationCopy(state: 'current' | 'stale' | 'missing'): string {
  if (state === 'stale') return 'Generate required'
  if (state === 'missing') return 'Generated file missing'
  return 'Generated'
}
