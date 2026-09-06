import type {
  ChangeBatch,
  ChangePreviewView,
  CsvPreviewView,
  DocumentView,
  ParameterChangeRequest,
  WorkspaceProjectionRequest,
  WorkspaceProjectionView,
} from './contracts.generated'

export type TabStatus = 'ready' | 'dirty' | 'validating' | 'valid' | 'invalid' | 'saving' | 'conflict' | 'error'

export interface EditState {
  values: Record<string, unknown>
  history: Array<Record<string, unknown>>
  future: Array<Record<string, unknown>>
  version: number
}

export interface TabState {
  document: DocumentView
  instanceId: number
  selectedId: string | null
  expandedScopeIds: Set<string>
  status: TabStatus
  edits: EditState
  preview: ChangePreviewView | null
  mutationRequestId: string | null
  csv: CsvPreviewView | null
  csvArtifactPath: string | null
  csvRequestId: string | null
}

export interface WorkspaceState {
  tabs: TabState[]
  activeId: string | null
  projection: WorkspaceProjectionView | null
  nextInstanceId: number
}

export type WorkspaceAction =
  | { type: 'merge-documents'; documents: DocumentView[]; activateFirst?: boolean }
  | { type: 'activate'; tabId: string | null }
  | { type: 'close'; tabId: string }
  | { type: 'select'; tabId: string; itemId: string | null }
  | { type: 'toggle-scope'; tabId: string; scopeId: string; expanded?: boolean }
  | { type: 'set-all-scopes'; tabId: string; expanded: boolean }
  | { type: 'edit'; tabId: string; parameterId: string; value: unknown; baseVersion?: number; instanceId?: number }
  | { type: 'undo'; tabId: string }
  | { type: 'redo'; tabId: string }
  | { type: 'mutation-started'; tabId: string; instanceId: number; requestId: string; baseVersion: number; status: TabStatus }
  | { type: 'preview-result'; tabId: string; instanceId: number; requestId: string; baseVersion: number; preview: ChangePreviewView }
  | { type: 'replace-document'; tabId: string; instanceId: number; requestId: string; baseVersion: number; document: DocumentView }
  | { type: 'mutation-error'; tabId: string; instanceId: number; requestId: string; baseVersion: number; conflict: boolean }
  | { type: 'csv-loading'; tabId: string; instanceId: number; requestId: string; path: string }
  | { type: 'csv-result'; tabId: string; instanceId: number; requestId: string; csv: CsvPreviewView | null }
  | { type: 'projection'; projection: WorkspaceProjectionView | null }

export const initialWorkspaceState: WorkspaceState = { tabs: [], activeId: null, projection: null, nextInstanceId: 1 }

export function workspaceReducer(state: WorkspaceState, action: WorkspaceAction): WorkspaceState {
  if (action.type === 'merge-documents') {
    const byId = new Map(state.tabs.map((tab) => [tab.document.id, tab]))
    let nextInstanceId = state.nextInstanceId
    for (const document of action.documents) {
      const previous = byId.get(document.id)
      const instanceId = previous?.instanceId ?? nextInstanceId++
      byId.set(document.id, createTab(document, previous, instanceId))
    }
    const tabs = [...byId.values()]
    return {
      ...state,
      tabs,
      nextInstanceId,
      activeId: action.activateFirst && action.documents.length
        ? action.documents[0].id
        : state.activeId && tabs.some((tab) => tab.document.id === state.activeId)
          ? state.activeId
          : tabs[0]?.document.id ?? null,
    }
  }
  if (action.type === 'activate') return { ...state, activeId: action.tabId }
  if (action.type === 'projection') return { ...state, projection: action.projection }
  if (action.type === 'close') {
    const index = state.tabs.findIndex((tab) => tab.document.id === action.tabId)
    const tabs = state.tabs.filter((tab) => tab.document.id !== action.tabId)
    const activeId = state.activeId === action.tabId
      ? tabs[Math.min(Math.max(index, 0), tabs.length - 1)]?.document.id ?? null
      : state.activeId
    return { ...state, tabs, activeId }
  }
  return updateTab(state, action.tabId, (tab) => reduceTab(tab, action))
}

function reduceTab(tab: TabState, action: Exclude<WorkspaceAction, { type: 'merge-documents' | 'activate' | 'close' | 'projection' }>): TabState {
  if (action.type === 'select') return { ...tab, selectedId: action.itemId, csv: null }
  if (action.type === 'toggle-scope') {
    const next = new Set(tab.expandedScopeIds)
    const expand = action.expanded ?? !next.has(action.scopeId)
    if (expand) next.add(action.scopeId)
    else next.delete(action.scopeId)
    return { ...tab, selectedId: action.scopeId, expandedScopeIds: next, csv: null }
  }
  if (action.type === 'set-all-scopes') {
    return {
      ...tab,
      expandedScopeIds: action.expanded
        ? new Set(tab.document.scopes.map((scope) => scope.id))
        : new Set(),
    }
  }
  if (action.type === 'edit') {
    if (action.instanceId !== undefined && action.instanceId !== tab.instanceId) return tab
    if (action.baseVersion !== undefined && action.baseVersion !== tab.edits.version) return tab
    const values = { ...tab.edits.values, [action.parameterId]: action.value }
    return withEditState(tab, {
      values,
      history: [...tab.edits.history, tab.edits.values],
      future: [],
      version: tab.edits.version + 1,
    })
  }
  if (action.type === 'undo') {
    const previous = tab.edits.history.at(-1)
    if (!previous) return tab
    return withEditState(tab, {
      values: previous,
      history: tab.edits.history.slice(0, -1),
      future: [tab.edits.values, ...tab.edits.future],
      version: tab.edits.version + 1,
    })
  }
  if (action.type === 'redo') {
    const next = tab.edits.future[0]
    if (!next) return tab
    return withEditState(tab, {
      values: next,
      history: [...tab.edits.history, tab.edits.values],
      future: tab.edits.future.slice(1),
      version: tab.edits.version + 1,
    })
  }
  if (action.type === 'mutation-started') {
    if (action.instanceId !== tab.instanceId || action.baseVersion !== tab.edits.version) return tab
    return { ...tab, status: action.status, mutationRequestId: action.requestId }
  }
  if (action.type === 'preview-result') {
    if (!ownsMutation(tab, action)) return tab
    return { ...tab, preview: action.preview, mutationRequestId: null, status: action.preview.valid ? 'valid' : 'invalid' }
  }
  if (action.type === 'replace-document') {
    if (!ownsMutation(tab, action)) return tab
    return createTab(action.document, tab, tab.instanceId)
  }
  if (action.type === 'mutation-error') {
    if (!ownsMutation(tab, action)) return tab
    return { ...tab, mutationRequestId: null, status: action.conflict ? 'conflict' : 'error' }
  }
  if (action.type === 'csv-loading') {
    if (action.instanceId !== tab.instanceId) return tab
    return { ...tab, csvRequestId: action.requestId, csvArtifactPath: action.path, csv: null }
  }
  if (action.type === 'csv-result') {
    if (action.instanceId !== tab.instanceId || tab.csvRequestId !== action.requestId) return tab
    return { ...tab, csvRequestId: null, csv: action.csv }
  }
  return tab
}

function ownsMutation(tab: TabState, action: { instanceId: number; requestId: string; baseVersion: number }): boolean {
  return action.instanceId === tab.instanceId
    && action.baseVersion === tab.edits.version
    && action.requestId === tab.mutationRequestId
}

function updateTab(state: WorkspaceState, tabId: string, update: (tab: TabState) => TabState): WorkspaceState {
  return {
    ...state,
    tabs: state.tabs.map((tab) => tab.document.id === tabId ? update(tab) : tab),
  }
}

function createTab(document: DocumentView, previous: TabState | undefined, instanceId: number): TabState {
  const itemIds = new Set([...document.steps, ...document.scopes].map((item) => item.id))
  const scopeIds = new Set(document.scopes.map((scope) => scope.id))
  return {
    document,
    instanceId,
    selectedId: previous?.selectedId && itemIds.has(previous.selectedId) ? previous.selectedId : null,
    expandedScopeIds: new Set([...(previous?.expandedScopeIds ?? [])].filter((id) => scopeIds.has(id))),
    status: 'ready',
    edits: emptyEdits(),
    preview: null,
    mutationRequestId: null,
    csv: null,
    csvArtifactPath: null,
    csvRequestId: null,
  }
}

function emptyEdits(): EditState {
  return { values: {}, history: [], future: [], version: 0 }
}

function withEditState(tab: TabState, edits: EditState): TabState {
  return {
    ...tab,
    edits,
    preview: null,
    mutationRequestId: null,
    status: Object.keys(edits.values).length ? 'dirty' : 'ready',
  }
}

export function tabById(state: WorkspaceState, tabId: string | null): TabState | null {
  return state.tabs.find((tab) => tab.document.id === tabId) ?? null
}

export function activeTab(state: WorkspaceState): TabState | null {
  return tabById(state, state.activeId)
}

export function draftChanges(tab: TabState): ParameterChangeRequest[] {
  return Object.entries(tab.edits.values).map(([parameter_id, value]) => ({ parameter_id, value }))
}

export function changeBatch(tab: TabState): ChangeBatch | null {
  const changes = draftChanges(tab)
  if (!changes.length) return null
  return {
    source_path: tab.document.source_path,
    output_path: tab.document.output_path,
    source_hash: tab.document.source_hash,
    output_hash: tab.document.output_hash,
    revision: tab.document.revision,
    changes,
  }
}

export function workspaceProjectionRequest(state: WorkspaceState): WorkspaceProjectionRequest {
  return {
    documents: state.tabs.map((tab) => ({
      document_id: tab.document.id,
      source_path: tab.document.source_path,
      output_path: tab.document.output_path,
      changes: draftChanges(tab),
    })),
  }
}
