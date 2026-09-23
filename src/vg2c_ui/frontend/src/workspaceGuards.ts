import type { TabState } from './workspaceState'

export interface ChangeActionState {
  editCount: number
  mutationPending: boolean
  canUndo: boolean
  canRedo: boolean
  canPreview: boolean
  canSave: boolean
  canGenerate: boolean
  canReload: boolean
}

export function hasUnsavedChanges(tab: TabState): boolean {
  return Object.keys(tab.edits.values).length > 0 || Object.keys(tab.fieldDrafts).length > 0
}

export function getChangeActionState(tab: TabState | null): ChangeActionState {
  if (!tab) {
    return {
      editCount: 0,
      mutationPending: false,
      canUndo: false,
      canRedo: false,
      canPreview: false,
      canSave: false,
      canGenerate: false,
      canReload: false,
    }
  }

  const editCount = Object.keys(tab.edits.values).length
  const mutationPending = tab.mutationRequestId !== null
  const validFields = Object.keys(tab.fieldDrafts).length === 0
  const editable = !tab.document.read_only_reason && tab.status !== 'conflict'
  return {
    editCount,
    mutationPending,
    canUndo: !mutationPending && (tab.edits.history.length > 0 || !validFields),
    canRedo: !mutationPending && tab.edits.future.length > 0,
    canPreview: validFields && editCount > 0 && !mutationPending && editable,
    canSave: validFields && editCount > 0 && !mutationPending && editable,
    canGenerate: validFields && editCount === 0 && !mutationPending && editable && tab.document.generation_state !== 'current',
    canReload: !mutationPending && tab.status === 'conflict',
  }
}
