import { useCallback, useEffect, useReducer, useRef } from 'react'

import {
  ApiError,
  applyChanges,
  applySqlAction,
  inspectSql,
  openDocument,
  previewChanges,
  previewCsv,
  projectWorkspace,
  translateBatch,
} from './api'
import type {
  ParameterView,
  SqlActionRequest,
  SqlModelView,
} from './contracts.generated'
import {
  activeTab,
  changeBatch,
  draftChanges,
  initialWorkspaceState,
  tabById,
  workspaceProjectionRequest,
  workspaceReducer,
} from './workspaceState'

export function useWorkspace() {
  const [state, dispatch] = useReducer(workspaceReducer, initialWorkspaceState)
  const stateRef = useRef(state)
  const csvCounter = useRef(0)
  const mutationCounter = useRef(0)
  stateRef.current = state

  const translate = useCallback(async (sourcePaths: string[]) => {
    const response = await translateBatch(sourcePaths)
    dispatch({ type: 'merge-documents', documents: response.documents, activateFirst: true })
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
      dispatch({ type: 'mutation-error', tabId, instanceId, requestId, baseVersion: version, conflict: false })
      throw error
    }
  }, [])

  const edit = useCallback((tabId: string, parameter: ParameterView, value: unknown) => {
    dispatch({ type: 'edit', tabId, parameterId: parameter.id, value })
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
      dispatch({ type: 'mutation-error', tabId, instanceId, requestId, baseVersion: version, conflict: error instanceof ApiError && error.status === 409 })
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
      dispatch({ type: 'mutation-error', tabId, instanceId, requestId, baseVersion: version, conflict: error instanceof ApiError && error.status === 409 })
      throw error
    }
  }, [])

  const loadCsv = useCallback(async (tabId: string, path: string) => {
    const tab = tabById(stateRef.current, tabId)
    if (!tab) return null
    const instanceId = tab.instanceId
    const requestId = `${tabId}:${instanceId}:csv:${++csvCounter.current}`
    dispatch({ type: 'csv-loading', tabId, instanceId, requestId, path })
    try {
      const csv = await previewCsv(tab.document.source_path, path)
      dispatch({ type: 'csv-result', tabId, instanceId, requestId, csv })
      return csv
    } catch (error) {
      dispatch({ type: 'csv-result', tabId, instanceId, requestId, csv: null })
      throw error
    }
  }, [])

  const inspectStructuredSql = useCallback(async (tabId: string, parameterId: string): Promise<SqlModelView> => {
    const tab = tabById(stateRef.current, tabId)
    if (!tab) throw new Error('Document is no longer open.')
    const version = tab.edits.version
    const instanceId = tab.instanceId
    const model = await inspectSql({
      source_path: tab.document.source_path,
      output_path: tab.document.output_path,
      parameter_id: parameterId,
      changes: draftChanges(tab),
    })
    const current = tabById(stateRef.current, tabId)
    if (!current || current.instanceId !== instanceId || current.edits.version !== version) {
      throw new Error('SQL draft changed while the structured model was loading.')
    }
    return model
  }, [])

  const runSqlAction = useCallback(async (
    tabId: string,
    parameterId: string,
    action: SqlActionRequest['action'],
    args: Record<string, unknown>,
  ): Promise<SqlModelView> => {
    const tab = tabById(stateRef.current, tabId)
    if (!tab) throw new Error('Document is no longer open.')
    const version = tab.edits.version
    const instanceId = tab.instanceId
    const response = await applySqlAction({
      source_path: tab.document.source_path,
      output_path: tab.document.output_path,
      parameter_id: parameterId,
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
      parameterId: response.change.parameter_id,
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
    void projectWorkspace(request)
      .then((projection) => {
        if (!cancelled) dispatch({ type: 'projection', projection })
      })
      .catch(() => {
        if (!cancelled) dispatch({ type: 'projection', projection: null })
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
    validate,
    apply,
    loadCsv,
    inspectStructuredSql,
    runSqlAction,
  }
}

function mutationId(tabId: string, instanceId: number, sequence: number): string {
  return `${tabId}:${instanceId}:mutation:${sequence}`
}
