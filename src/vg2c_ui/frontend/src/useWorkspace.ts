import { useCallback, useEffect, useReducer, useRef } from 'react'

import {
  ApiError,
  applyChanges,
  applySqlAction,
  inspectSql,
  openDocument,
  previewChanges,
  previewCsv,
  previewHtml,
  projectWorkspace,
  translateBatch,
} from './api'
import type {
  FileEndpointView,
  SemanticBindingView,
  SqlActionRequest,
  SqlModelView,
} from './contracts.generated'
import {
  activeTab,
  changeBatch,
  draftChanges,
  documentSnapshot,
  initialWorkspaceState,
  tabById,
  workspaceProjectionRequest,
  workspaceReducer,
  type FieldPath,
} from './workspaceState'

export function useWorkspace() {
  const [state, dispatch] = useReducer(workspaceReducer, initialWorkspaceState)
  const stateRef = useRef(state)
  const csvCounter = useRef(0)
  const mutationCounter = useRef(0)
  stateRef.current = state

  const translate = useCallback(async (sourcePaths: string[]) => {
    const response = await translateBatch(sourcePaths)
    dispatch({ type: 'merge-documents', documents: response.documents, activateFirst: true, preserveDirty: true })
    return response
  }, [])

  const open = useCallback(async (sourcePaths: string[]) => {
    const documents = await Promise.all(sourcePaths.map((path) => openDocument(path)))
    dispatch({ type: 'merge-documents', documents, activateFirst: true })
    return documents
  }, [])

  const reload = useCallback(async (tabId: string) => {
    const tab = tabById(stateRef.current, tabId)
    if (!tab) return null
    const version = tab.edits.version
    const instanceId = tab.instanceId
    const requestId = mutationId(tabId, instanceId, ++mutationCounter.current)
    dispatch({ type: 'mutation-started', tabId, instanceId, requestId, baseVersion: version, status: tab.status })
    try {
      const document = await openDocument(tab.document.source_path, tab.document.output_path)
      dispatch({ type: 'replace-document', tabId, instanceId, requestId, baseVersion: version, document })
      return document
    } catch (error) {
      dispatch({
        type: 'mutation-error', tabId, instanceId, requestId, baseVersion: version,
        conflict: tab.status === 'conflict', message: errorMessage(error, 'Reload failed'),
      })
      throw error
    }
  }, [])

  const edit = useCallback((tabId: string, binding: SemanticBindingView, value: unknown, clearDraftPaths?: FieldPath[]) => {
    dispatch({ type: 'edit', tabId, bindingId: binding.id, value, clearDraftPaths })
  }, [])

  const validateCandidate = useCallback(async (tabId: string, bindingId: string, value: unknown) => {
    const tab = tabById(stateRef.current, tabId)
    if (!tab) throw new Error('Document is no longer open.')
    const changes = [
      ...draftChanges(tab).filter((change) => change.binding_id !== bindingId),
      { binding_id: bindingId, value, reset: false },
    ]
    return previewChanges({ ...documentSnapshot(tab.document), changes })
  }, [])

  const validate = useCallback(async (tabId: string) => {
    const tab = tabById(stateRef.current, tabId)
    if (!tab) return null
    const batch = changeBatch(tab)
    if (!batch) return null
    const version = tab.edits.version
    const instanceId = tab.instanceId
    const requestId = mutationId(tabId, instanceId, ++mutationCounter.current)
    dispatch({ type: 'mutation-started', tabId, instanceId, requestId, baseVersion: version, status: 'validating' })
    try {
      const preview = await previewChanges(batch)
      dispatch({ type: 'preview-result', tabId, instanceId, requestId, baseVersion: version, preview })
      return preview
    } catch (error) {
      dispatch({
        type: 'mutation-error', tabId, instanceId, requestId, baseVersion: version,
        conflict: error instanceof ApiError && error.status === 409,
        message: errorMessage(error, 'Could not validate changes'),
      })
      throw error
    }
  }, [])

  const apply = useCallback(async (tabId: string) => {
    const tab = tabById(stateRef.current, tabId)
    if (!tab) return null
    const batch = changeBatch(tab)
    if (!batch) return null
    const version = tab.edits.version
    const instanceId = tab.instanceId
    const requestId = mutationId(tabId, instanceId, ++mutationCounter.current)
    dispatch({ type: 'mutation-started', tabId, instanceId, requestId, baseVersion: version, status: 'saving' })
    try {
      const result = await applyChanges(batch)
      dispatch({ type: 'replace-document', tabId, instanceId, requestId, baseVersion: version, document: result.document })
      return result
    } catch (error) {
      dispatch({
        type: 'mutation-error', tabId, instanceId, requestId, baseVersion: version,
        conflict: error instanceof ApiError && error.status === 409,
        message: errorMessage(error, 'Could not apply changes'),
      })
      throw error
    }
  }, [])

  const previewHtmlOperation = useCallback(async (tabId: string, operationId: string) => {
    const tab = tabById(stateRef.current, tabId)
    if (!tab) throw new Error('Document is no longer open.')
    return previewHtml({
      ...documentSnapshot(tab.document),
      operation_id: operationId,
      changes: draftChanges(tab),
    })
  }, [])

  const loadCsv = useCallback(async (tabId: string, effectId: string, endpoint: FileEndpointView) => {
    const tab = tabById(stateRef.current, tabId)
    if (!tab || !endpoint.path) return null
    const instanceId = tab.instanceId
    const requestId = `${tabId}:${instanceId}:csv:${++csvCounter.current}`
    dispatch({ type: 'csv-loading', tabId, instanceId, requestId, path: endpoint.path })
    try {
      const csv = await previewCsv({ ...documentSnapshot(tab.document), effect_id: effectId,
        endpoint_id: endpoint.id, expected_path: endpoint.path, changes: draftChanges(tab) })
      dispatch({ type: 'csv-result', tabId, instanceId, requestId, csv, error: null })
      return csv
    } catch (error) {
      dispatch({
        type: 'csv-result', tabId, instanceId, requestId, csv: null,
        error: errorMessage(error, 'CSV preview failed'),
      })
      throw error
    }
  }, [])

  const inspectStructuredSql = useCallback(async (tabId: string, bindingId: string): Promise<SqlModelView> => {
    const tab = tabById(stateRef.current, tabId)
    if (!tab) throw new Error('Document is no longer open.')
    const instanceId = tab.instanceId
    const model = await inspectSql({
      ...documentSnapshot(tab.document),
      parameter_id: bindingId,
      changes: draftChanges(tab),
    })
    const current = tabById(stateRef.current, tabId)
    if (!current || current.instanceId !== instanceId || current.edits.values !== tab.edits.values || current.document.revision !== tab.document.revision) {
      throw new Error('SQL draft changed while the structured model was loading.')
    }
    return model
  }, [])

  const runSqlAction = useCallback(async (
    tabId: string,
    bindingId: string,
    action: SqlActionRequest['action'],
    args: Record<string, unknown>,
  ): Promise<SqlModelView> => {
    const tab = tabById(stateRef.current, tabId)
    if (!tab) throw new Error('Document is no longer open.')
    const version = tab.edits.version
    const instanceId = tab.instanceId
    const response = await applySqlAction({
      ...documentSnapshot(tab.document),
      parameter_id: bindingId,
      changes: draftChanges(tab),
      action,
      arguments: args,
    })
    const current = tabById(stateRef.current, tabId)
    if (!current || current.instanceId !== instanceId || current.edits.version !== version) {
      throw new Error('SQL draft changed before the structured action completed.')
    }
    dispatch({
      type: 'edit',
      tabId,
      instanceId,
      bindingId: response.change.binding_id,
      value: response.change.value,
      baseVersion: version,
    })
    return response.model
  }, [])

  useEffect(() => {
    if (!state.tabs.length) {
      dispatch({ type: 'projection', projection: null })
      return
    }
    let cancelled = false
    const request = workspaceProjectionRequest(state)
    dispatch({ type: 'projection-loading' })
    void projectWorkspace(request)
      .then((projection) => {
        if (!cancelled) dispatch({ type: 'projection', projection })
      })
      .catch((error) => {
        if (!cancelled) dispatch({ type: 'projection-error', message: errorMessage(error, 'Could not update file flow') })
      })
    return () => { cancelled = true }
  }, [state.tabs.map((tab) => `${tab.document.id}:${tab.instanceId}:${tab.document.revision}:${tab.edits.version}`).join('|')])

  return {
    state,
    active: activeTab(state),
    dispatch,
    translate,
    open,
    reload,
    edit,
    validateCandidate,
    validate,
    apply,
    loadCsv,
    previewHtmlOperation,
    inspectStructuredSql,
    runSqlAction,
  }
}

function mutationId(tabId: string, instanceId: number, sequence: number): string {
  return `${tabId}:${instanceId}:mutation:${sequence}`
}

function errorMessage(error: unknown, fallback: string): string {
  return error instanceof Error && error.message ? error.message : fallback
}
