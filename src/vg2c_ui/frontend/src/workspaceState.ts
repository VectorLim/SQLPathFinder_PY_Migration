import type {
  ChangeBatch,
  ChangePreviewView,
  CsvPreviewView,
  DocumentView,
  DocumentSnapshot,
  ParameterChangeRequest,
  ParameterView,
  SemanticBindingView,
  SemanticChangeRequest,
  WorkspaceProjectionRequest,
  WorkspaceProjectionView,
} from './contracts.generated'

export type TabStatus = 'ready' | 'dirty' | 'validating' | 'valid' | 'invalid' | 'saving' | 'conflict' | 'error'

export const RESET_VALUE = Symbol('reset-to-generated')
export type FieldPath = Array<string | number>

export function effectiveParameterValue(values: Record<string, unknown>, parameter: ParameterView): unknown {
  const value = Object.hasOwn(values, parameter.id) ? values[parameter.id] : parameter.value
  return value === RESET_VALUE ? parameter.generated_value : value
}

export function effectiveBindingValue(values: Record<string, unknown>, binding: SemanticBindingView): unknown {
  const value = Object.hasOwn(values, binding.id) ? values[binding.id] : binding.value
  return value === RESET_VALUE ? binding.default : value
}

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
  revealVersion: number
  revealFocus: boolean
  expandedScopeIds: Set<string>
  status: TabStatus
  edits: EditState
  fieldDrafts: Record<string, { parameterId: string; text: string; error: string }>
  preview: ChangePreviewView | null
  mutationRequestId: string | null
  mutationError: string | null
  csv: CsvPreviewView | null
  csvArtifactPath: string | null
  csvRequestId: string | null
  csvError: string | null
}

export interface WorkspaceState {
  tabs: TabState[]
  activeId: string | null
  projection: WorkspaceProjectionView | null
  projectionStatus: 'idle' | 'loading' | 'ready' | 'error'
  projectionError: string | null
  nextInstanceId: number
}

export type WorkspaceAction =
  | { type: 'merge-documents'; documents: DocumentView[]; activateFirst?: boolean }
  | { type: 'activate'; tabId: string | null }
  | { type: 'close'; tabId: string }
  | { type: 'select'; tabId: string; itemId: string | null }
  | { type: 'navigate-operation'; tabId: string; operationId: string; focus?: boolean }
  | { type: 'toggle-scope'; tabId: string; scopeId: string; expanded?: boolean }
  | { type: 'set-all-scopes'; tabId: string; expanded: boolean }
  | { type: 'edit'; tabId: string; parameterId: string; value: unknown; clearDraftPaths?: FieldPath[]; baseVersion?: number; instanceId?: number }
  | { type: 'field-draft'; tabId: string; key: string; draft: TabState['fieldDrafts'][string] | null }
  | { type: 'undo'; tabId: string }
  | { type: 'redo'; tabId: string }
  | { type: 'mutation-started'; tabId: string; instanceId: number; requestId: string; baseVersion: number; status: TabStatus }
  | { type: 'preview-result'; tabId: string; instanceId: number; requestId: string; baseVersion: number; preview: ChangePreviewView }
  | { type: 'replace-document'; tabId: string; instanceId: number; requestId: string; baseVersion: number; document: DocumentView }
  | { type: 'mutation-error'; tabId: string; instanceId: number; requestId: string; baseVersion: number; conflict: boolean; message: string }
  | { type: 'csv-loading'; tabId: string; instanceId: number; requestId: string; path: string }
  | { type: 'csv-result'; tabId: string; instanceId: number; requestId: string; csv: CsvPreviewView | null; error: string | null }
  | { type: 'projection'; projection: WorkspaceProjectionView | null }
  | { type: 'projection-loading' }
  | { type: 'projection-error'; message: string }

export const initialWorkspaceState: WorkspaceState = { tabs: [], activeId: null, projection: null, projectionStatus: 'idle', projectionError: null, nextInstanceId: 1 }

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
  if (action.type === 'navigate-operation') {
    const tab = tabById(state, action.tabId)
    if (!tab || !tab.document.semantic_operations.some((operation) => operation.id === action.operationId)) return state
    return updateTab({ ...state, activeId: action.tabId }, action.tabId, (current) => ({
      ...selectItem(current, action.operationId), revealVersion: current.revealVersion + 1, revealFocus: Boolean(action.focus),
    }))
  }
  if (action.type === 'projection') return { ...state, projection: action.projection, projectionStatus: action.projection ? 'ready' : 'idle', projectionError: null }
  if (action.type === 'projection-loading') return { ...state, projectionStatus: 'loading', projectionError: null }
  if (action.type === 'projection-error') return { ...state, projectionStatus: 'error', projectionError: action.message }
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

function reduceTab(tab: TabState, action: Exclude<WorkspaceAction, { type: 'merge-documents' | 'activate' | 'close' | 'projection' | 'projection-loading' | 'projection-error' | 'navigate-operation' }>): TabState {
  if (tab.status === 'saving' && ['edit', 'field-draft', 'undo', 'redo'].includes(action.type)) return tab
  if (action.type === 'field-draft') {
    if (!action.draft && !tab.fieldDrafts[action.key]) return tab
    const fieldDrafts = { ...tab.fieldDrafts }
    if (action.draft) fieldDrafts[action.key] = action.draft
    else delete fieldDrafts[action.key]
    return { ...tab, fieldDrafts, preview: null, mutationRequestId: null,
      csv: null, csvRequestId: null, csvArtifactPath: null, csvError: null,
      edits: { ...tab.edits, version: tab.edits.version + 1 } }
  }
  if (action.type === 'select') return selectItem(tab, action.itemId)
  if (action.type === 'toggle-scope') {
    const next = new Set(tab.expandedScopeIds)
    const expand = action.expanded ?? !next.has(action.scopeId)
    if (expand) next.add(action.scopeId)
    else next.delete(action.scopeId)
    return { ...tab, expandedScopeIds: next }
  }
  if (action.type === 'set-all-scopes') {
    return {
      ...tab,
      expandedScopeIds: action.expanded
        ? new Set(tab.document.semantic_operations.filter((operation) => tab.document.semantic_operations.some((child) => child.parent_operation_id === operation.id)).map((operation) => operation.id))
        : new Set(),
    }
  }
  if (action.type === 'edit') {
    if (action.instanceId !== undefined && action.instanceId !== tab.instanceId) return tab
    if (action.baseVersion !== undefined && action.baseVersion !== tab.edits.version) return tab
    const values = { ...tab.edits.values, [action.parameterId]: action.value }
    const fieldDrafts = Object.fromEntries(Object.entries(tab.fieldDrafts).filter(([key, draft]) => {
      if (draft.parameterId !== action.parameterId) return true
      if (action.value === RESET_VALUE) return false
      if (!action.clearDraftPaths?.length) return true
      const path: FieldPath = JSON.parse(key)
      return !action.clearDraftPaths.some((prefix) => [action.parameterId, ...prefix].every((part, index) => path[index] === part))
    }))
    return withEditState({ ...tab, fieldDrafts }, {
      values,
      history: [...tab.edits.history, tab.edits.values],
      future: [],
      version: tab.edits.version + 1,
    })
  }
  if (action.type === 'undo') {
    if (Object.keys(tab.fieldDrafts).length) return { ...tab, fieldDrafts: {}, edits: { ...tab.edits, version: tab.edits.version + 1 } }
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
    return { ...tab, status: action.status, mutationRequestId: action.requestId, mutationError: null }
  }
  if (action.type === 'preview-result') {
    if (!ownsMutation(tab, action)) return tab
    return { ...tab, preview: action.preview, mutationRequestId: null, mutationError: null, status: action.preview.valid ? 'valid' : 'invalid' }
  }
  if (action.type === 'replace-document') {
    if (!ownsMutation(tab, action)) return tab
    return createTab(action.document, tab, tab.instanceId)
  }
  if (action.type === 'mutation-error') {
    if (!ownsMutation(tab, action)) return tab
    return {
      ...tab,
      mutationRequestId: null,
      mutationError: action.message,
      status: action.conflict ? 'conflict' : 'error',
    }
  }
  if (action.type === 'csv-loading') {
    if (action.instanceId !== tab.instanceId) return tab
    return { ...tab, csvRequestId: action.requestId, csvArtifactPath: action.path, csv: null, csvError: null }
  }
  if (action.type === 'csv-result') {
    if (action.instanceId !== tab.instanceId || tab.csvRequestId !== action.requestId) return tab
    return { ...tab, csvRequestId: null, csv: action.csv, csvError: action.error }
  }
  return tab
}

export function ancestorScopeIds(document: DocumentView, itemId: string): string[] {
  const operations = new Map(document.semantic_operations.map((operation) => [operation.id, operation]))
  const ancestors: string[] = []
  let parent = operations.get(itemId)?.parent_operation_id ?? null
  while (parent && !ancestors.includes(parent)) {
    ancestors.push(parent)
    parent = operations.get(parent)?.parent_operation_id ?? null
  }
  return ancestors
}

function selectItem(tab: TabState, itemId: string | null): TabState {
  const selectedId = itemId && tab.document.semantic_operations.some((operation) => operation.id === itemId) ? itemId : null
  return { ...tab, selectedId, revealFocus: false,
    expandedScopeIds: new Set([...tab.expandedScopeIds, ...ancestorScopeIds(tab.document, selectedId ?? '')]) }
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
  const itemIds = new Set(document.semantic_operations.map((operation) => operation.id))
  const scopeIds = new Set(document.semantic_operations.filter((operation) => document.semantic_operations.some((child) => child.parent_operation_id === operation.id)).map((operation) => operation.id))
  return {
    document,
    instanceId,
    selectedId: previous?.selectedId && itemIds.has(previous.selectedId) ? previous.selectedId : null,
    revealVersion: previous?.revealVersion ?? 0,
    revealFocus: false,
    expandedScopeIds: new Set([...(previous?.expandedScopeIds ?? [])].filter((id) => scopeIds.has(id))),
    status: 'ready',
    edits: emptyEdits(),
    fieldDrafts: {},
    preview: null,
    mutationRequestId: null,
    mutationError: null,
    csv: null,
    csvArtifactPath: null,
    csvRequestId: null,
    csvError: null,
  }
}

function emptyEdits(): EditState {
  return { values: {}, history: [], future: [], version: 0 }
}

function withEditState(tab: TabState, edits: EditState): TabState {
  const conflict = tab.status === 'conflict'
  return {
    ...tab,
    edits,
    csv: null,
    csvRequestId: null,
    csvArtifactPath: null,
    csvError: null,
    preview: null,
    mutationRequestId: null,
    mutationError: conflict ? tab.mutationError : null,
    status: conflict ? 'conflict' : Object.keys(edits.values).length ? 'dirty' : 'ready',
  }
}

export function tabById(state: WorkspaceState, tabId: string | null): TabState | null {
  return state.tabs.find((tab) => tab.document.id === tabId) ?? null
}

export function activeTab(state: WorkspaceState): TabState | null {
  return tabById(state, state.activeId)
}

export function draftChanges(tab: TabState): SemanticChangeRequest[] {
  return Object.entries(tab.edits.values).map(([binding_id, value]) => ({
    binding_id, value: value === RESET_VALUE ? null : value, reset: value === RESET_VALUE,
  }))
}

export function changeBatch(tab: TabState): ChangeBatch | null {
  const changes = draftChanges(tab)
  if (!changes.length) return null
  return {
    ...documentSnapshot(tab.document),
    changes,
  }
}

export function documentSnapshot(document: DocumentView): DocumentSnapshot {
  return {
    schema_version: 5,
    source_path: document.source_path,
    output_path: document.output_path,
    source_hash: document.source_hash,
    output_hash: document.output_hash,
    revision: document.revision,
    compiler_hash: document.compiler_hash,
  }
}

export function workspaceProjectionRequest(state: WorkspaceState): WorkspaceProjectionRequest {
  return {
    documents: state.tabs.map((tab) => ({
      document_id: tab.document.id,
      ...documentSnapshot(tab.document),
      changes: draftChanges(tab),
    })),
  }
}
