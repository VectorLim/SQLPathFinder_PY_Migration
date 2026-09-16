import type { TabState } from './workspaceState'

export function hasUnsavedChanges(tab: TabState): boolean {
  return Object.keys(tab.edits.values).length > 0
}
