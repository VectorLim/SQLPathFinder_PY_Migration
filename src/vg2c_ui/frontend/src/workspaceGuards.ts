import type { TabState } from './workspaceState'

export interface ChangeActionState {
  editCount: number
  mutationPending: boolean
  canUndo: boolean
  canRedo: boolean
  canPreview: boolean
  canApply: boolean
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
      canApply: false,
      canReload: false,
    }
  }

  const editCount = Object.keys(tab.edits.values).length
  const mutationPending = tab.mutationRequestId !== null
  const validFields = Object.keys(tab.fieldDrafts).length === 0
  return {
    editCount,
    mutationPending,
    canUndo: tab.status !== 'saving' && (tab.edits.history.length > 0 || !validFields),
    canRedo: tab.status !== 'saving' && tab.edits.future.length > 0,
    canPreview: validFields && editCount > 0 && !mutationPending && tab.status !== 'conflict' && tab.document.synchronized,
    canApply: validFields && Boolean(tab.preview?.valid) && !mutationPending && tab.status === 'valid',
    canReload: !mutationPending && tab.status === 'conflict',
  }
}
