import { useCallback, useEffect, useReducer, useRef } from 'react'

import {
  ApiError,
  applySqlAction,
  generateOutput,
  reorderExecution,
  inspectSql,
  openDocument,
  previewChanges,
  previewHtml,
  projectWorkspace,
  saveChanges,
  translateBatch,
} from './api'
import type {
  SemanticBindingView,
  SqlModelView,
} from './contracts.generated'
import type { SqlCommand } from './sql/sqlCommands'
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

  const refreshFileChoices = useCallback(async (tabId: string) => {
    const tab = tabById(stateRef.current, tabId)
    if (!tab) return
    const document = await openDocument(tab.document.source_path, tab.document.output_path)
    dispatch({ type: 'refresh-file-choices', tabId, instanceId: tab.instanceId, document })
  }, [])

  const edit = useCallback((tabId: string, binding: SemanticBindingView, value: unknown, clearDraftPaths?: FieldPath[]) => {
    dispatch({ type: 'edit', tabId, bindingId: binding.id, value, clearDraftPaths })
  }, [])

  const validateCandidate = useCallback(async (tabId: string, bindingId: string, value: unknown) => {
    const tab = tabById(stateRef.current, tabId)
    if (!tab) throw new Error('Document is no longer open.')
    const changes = [
      ...draftChanges(tab).filter((change) => change.binding_id !== bindingId),
      { binding_id: bindingId, value, symbol_id: null, reset: false },
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

  const save = useCallback(async (tabId: string) => {
    const tab = tabById(stateRef.current, tabId)
    if (!tab) return null
    const batch = changeBatch(tab)
    if (!batch) return null
    const version = tab.edits.version
    const instanceId = tab.instanceId
    const requestId = mutationId(tabId, instanceId, ++mutationCounter.current)
    dispatch({ type: 'mutation-started', tabId, instanceId, requestId, baseVersion: version, status: 'saving' })
    try {
      const result = await saveChanges(batch)
      dispatch({ type: 'replace-document', tabId, instanceId, requestId, baseVersion: version, document: result.document })
      return result
    } catch (error) {
      dispatch({
        type: 'mutation-error', tabId, instanceId, requestId, baseVersion: version,
        conflict: error instanceof ApiError && error.status === 409,
        message: errorMessage(error, 'Could not save changes'),
      })
      throw error
    }
  }, [])

  const generate = useCallback(async (tabId: string) => {
    const tab = tabById(stateRef.current, tabId)
    if (!tab || draftChanges(tab).length || Object.keys(tab.fieldDrafts).length) return null
    const version = tab.edits.version
    const instanceId = tab.instanceId
    const requestId = mutationId(tabId, instanceId, ++mutationCounter.current)
    dispatch({ type: 'mutation-started', tabId, instanceId, requestId, baseVersion: version, status: 'generating' })
    try {
      const result = await generateOutput(documentSnapshot(tab.document))
      dispatch({ type: 'replace-document', tabId, instanceId, requestId, baseVersion: version, document: result.document })
      return result
    } catch (error) {
      dispatch({
        type: 'mutation-error', tabId, instanceId, requestId, baseVersion: version,
        conflict: error instanceof ApiError && error.status === 409,
        message: errorMessage(error, 'Could not generate output'),
      })
      throw error
    }
  }, [])

  const reorder = useCallback(async (tabId: string, sourceScopeId: number, targetScopeId: number) => {
    const tab = tabById(stateRef.current, tabId)
    if (!tab || draftChanges(tab).length || Object.keys(tab.fieldDrafts).length) return null
    const version = tab.edits.version
    const instanceId = tab.instanceId
    const requestId = mutationId(tabId, instanceId, ++mutationCounter.current)
    dispatch({ type: 'mutation-started', tabId, instanceId, requestId, baseVersion: version, status: 'saving' })
    try {
      const result = await reorderExecution({
        ...documentSnapshot(tab.document),
        source_scope_id: sourceScopeId,
        target_scope_id: targetScopeId,
      })
      dispatch({ type: 'replace-document', tabId, instanceId, requestId, baseVersion: version, document: result.document })
      return result
    } catch (error) {
      dispatch({
        type: 'mutation-error', tabId, instanceId, requestId, baseVersion: version,
        conflict: error instanceof ApiError && error.status === 409,
        message: errorMessage(error, 'Could not reorder operations'),
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

  const inspectStructuredSql = useCallback(async (tabId: string, bindingId: string): Promise<SqlModelView> => {
    const tab = tabById(stateRef.current, tabId)
    if (!tab) throw new Error('Document is no longer open.')
    const instanceId = tab.instanceId
    const model = await inspectSql({
      ...documentSnapshot(tab.document),
      binding_id: bindingId,
      changes: draftChanges(tab),
    })
    const current = tabById(stateRef.current, tabId)
    if (!current || current.instanceId !== instanceId || current.edits.values !== tab.edits.values || current.document.revision !== tab.document.revision) {
      throw new Error('SQL draft changed while the structured model was loading.')
    }
    return model
  }, [])

  const runSqlCommand = useCallback(async (
    tabId: string,
    bindingId: string,
    command: SqlCommand,
  ): Promise<SqlModelView> => {
    const tab = tabById(stateRef.current, tabId)
    if (!tab) throw new Error('Document is no longer open.')
    const version = tab.edits.version
    const instanceId = tab.instanceId
    const response = await applySqlAction({
      ...documentSnapshot(tab.document),
      binding_id: bindingId,
      changes: draftChanges(tab),
      action: command.action,
      arguments: { ...command.arguments },
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
    refreshFileChoices,
    edit,
    validateCandidate,
    validate,
    save,
    generate,
    reorder,
    previewHtmlOperation,
    inspectStructuredSql,
    runSqlCommand,
  }
}

function mutationId(tabId: string, instanceId: number, sequence: number): string {
  return `${tabId}:${instanceId}:mutation:${sequence}`
}

function errorMessage(error: unknown, fallback: string): string {
  return error instanceof Error && error.message ? error.message : fallback
}
