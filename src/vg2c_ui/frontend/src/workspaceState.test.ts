import assert from 'node:assert/strict'
import { getChangeActionState } from './workspaceGuards.ts'
import { draftChanges, initialWorkspaceState, RESET_VALUE, workspaceProjectionRequest, workspaceReducer } from './workspaceState.ts'
import type { DocumentView } from './contracts.generated.ts'

function doc(id: string): DocumentView {
  return {
    schema_version: 5,
    id,
    source_path: `${id}.txt`,
    output_path: `${id}.py`,
    source_hash: `src-${id}`,
    output_hash: `out-${id}`,
    revision: '5755395593540295586',
    compiler_hash: `compiler-${id}`,
    synchronized: true,
    read_only_reason: null,
    steps: [], scopes: [], artifacts: [], diagnostics: [], effects: [],
    semantic_operations: [], files: [], symbols: [], condition_operators: [],
  }
}

let state = workspaceReducer(initialWorkspaceState, { type: 'merge-documents', documents: [doc('a'), doc('b')], activateFirst: true })
state = workspaceReducer(state, { type: 'edit', tabId: 'a', bindingId: 'p1', value: 'draft' })
state = workspaceReducer(state, { type: 'activate', tabId: 'b' })
assert.equal(state.tabs.find((tab) => tab.document.id === 'a')?.edits.values.p1, 'draft', 'inactive dirty tab must retain its draft')
assert.equal(state.tabs.find((tab) => tab.document.id === 'a')?.status, 'dirty')

const projection = workspaceProjectionRequest(state)
assert.deepEqual(projection.documents.find((item) => item.document_id === 'a')?.changes, [{ binding_id: 'p1', value: 'draft', reset: false }])
assert.deepEqual(projection.documents.find((item) => item.document_id === 'b')?.changes, [])

const beforeB = state.tabs.find((tab) => tab.document.id === 'b')!
const navigationDoc = doc('navigation')
navigationDoc.semantic_operations = [
  { id: 'scope-a', kind: 'loop', display_name: 'Loop', description: '', parent_operation_id: null, branch: null, source_span: { file: null, start_line: 1, end_line: 2 }, bindings: [], capabilities: [], comments: [], validation_state: 'valid', visibility: 'normal' },
  ...['operation-a', 'operation-b'].map((id) => ({ id, kind: 'probe', display_name: 'Probe', description: '', parent_operation_id: 'scope-a', branch: null, source_span: { file: null, start_line: 1, end_line: 2 }, bindings: [], capabilities: [], comments: [], validation_state: 'valid' as const, visibility: 'normal' as const })),
]
let navigation = workspaceReducer(state, { type: 'merge-documents', documents: [navigationDoc] })
navigation = workspaceReducer(navigation, { type: 'navigate-operation', tabId: 'navigation', operationId: 'operation-b', focus: true })
const navigationTab = navigation.tabs.find((tab) => tab.document.id === 'navigation')!
assert.equal(navigation.activeId, 'navigation')
assert.equal(navigationTab.selectedId, 'operation-b')
assert.ok(navigationTab.expandedScopeIds.has('scope-a'))
assert.equal(navigationTab.revealFocus, true)
assert.equal(workspaceReducer(navigation, { type: 'toggle-scope', tabId: 'navigation', scopeId: 'scope-a', expanded: true }).tabs.at(-1)?.selectedId, 'operation-b')
assert.equal(workspaceReducer(navigation, { type: 'navigate-operation', tabId: 'navigation', operationId: 'operation-b' }).tabs.at(-1)?.revealVersion, 2)
const resetState = workspaceReducer(state, { type: 'edit', tabId: 'a', bindingId: 'p1', value: RESET_VALUE })
assert.deepEqual(draftChanges(resetState.tabs[0]), [{ binding_id: 'p1', value: null, reset: true }])
assert.equal(workspaceReducer(resetState, { type: 'undo', tabId: 'a' }).tabs[0].edits.values.p1, 'draft')
let invalidFields = workspaceReducer(state, { type: 'field-draft', tabId: 'a', key: 'number', draft: { bindingId: 'numeric', text: '', error: 'Enter a number' } })
invalidFields = workspaceReducer(invalidFields, { type: 'edit', tabId: 'a', bindingId: 'other', value: 'changed' })
assert.equal(invalidFields.tabs[0].fieldDrafts.number.text, '')
assert.equal(getChangeActionState(invalidFields.tabs[0]).canPreview, false)
assert.equal(getChangeActionState(invalidFields.tabs[0]).canApply, false)
assert.deepEqual(workspaceReducer(invalidFields, { type: 'undo', tabId: 'a' }).tabs[0].fieldDrafts, {})
const a = state.tabs.find((tab) => tab.document.id === 'a')!
state = workspaceReducer(state, {
  type: 'mutation-started', tabId: 'a', instanceId: a.instanceId,
  requestId: 'preview-1', baseVersion: 1, status: 'validating',
})
assert.equal(state.tabs.find((tab) => tab.document.id === 'b'), beforeB, 'targeted async state must not mutate active-but-unrelated tab')

state = workspaceReducer(state, { type: 'edit', tabId: 'a', bindingId: 'p2', value: 'newer', baseVersion: 1 })
state = workspaceReducer(state, {
  type: 'preview-result', tabId: 'a', instanceId: a.instanceId,
  requestId: 'preview-1', baseVersion: 1,
  preview: { valid: true, diff: 'stale', issues: [] },
})
assert.equal(state.tabs.find((tab) => tab.document.id === 'a')?.preview, null, 'stale async response must be ignored after a newer edit')

let sameVersion = workspaceReducer(initialWorkspaceState, { type: 'merge-documents', documents: [doc('same')], activateFirst: true })
const same = sameVersion.tabs[0]
sameVersion = workspaceReducer(sameVersion, {
  type: 'mutation-started', tabId: 'same', instanceId: same.instanceId,
  requestId: 'request-old', baseVersion: 0, status: 'validating',
})
sameVersion = workspaceReducer(sameVersion, {
  type: 'mutation-started', tabId: 'same', instanceId: same.instanceId,
  requestId: 'request-new', baseVersion: 0, status: 'validating',
})
sameVersion = workspaceReducer(sameVersion, {
  type: 'preview-result', tabId: 'same', instanceId: same.instanceId,
  requestId: 'request-old', baseVersion: 0,
  preview: { valid: true, diff: 'old', issues: [] },
})
assert.equal(sameVersion.tabs[0].preview, null, 'older same-version request must not win')
sameVersion = workspaceReducer(sameVersion, {
  type: 'preview-result', tabId: 'same', instanceId: same.instanceId,
  requestId: 'request-new', baseVersion: 0,
  preview: { valid: true, diff: 'new', issues: [] },
})
assert.equal(sameVersion.tabs[0].preview?.diff, 'new')

let reopened = workspaceReducer(initialWorkspaceState, { type: 'merge-documents', documents: [doc('reopen')], activateFirst: true })
const oldInstance = reopened.tabs[0].instanceId
reopened = workspaceReducer(reopened, {
  type: 'mutation-started', tabId: 'reopen', instanceId: oldInstance,
  requestId: 'old-request', baseVersion: 0, status: 'validating',
})
reopened = workspaceReducer(reopened, { type: 'close', tabId: 'reopen' })
reopened = workspaceReducer(reopened, { type: 'merge-documents', documents: [doc('reopen')], activateFirst: true })
assert.notEqual(reopened.tabs[0].instanceId, oldInstance, 'reopened document must be a new tab instance')
reopened = workspaceReducer(reopened, {
  type: 'preview-result', tabId: 'reopen', instanceId: oldInstance,
  requestId: 'old-request', baseVersion: 0,
  preview: { valid: true, diff: 'leaked', issues: [] },
})
assert.equal(reopened.tabs[0].preview, null, 'closed tab response must not leak into reopened document')

const csvInstance = reopened.tabs[0].instanceId
reopened = workspaceReducer(reopened, { type: 'csv-loading', tabId: 'reopen', instanceId: csvInstance, requestId: 'csv-new', path: 'a.csv' })
reopened = workspaceReducer(reopened, { type: 'csv-result', tabId: 'reopen', instanceId: oldInstance, requestId: 'csv-new', csv: null, error: 'stale csv error' })
assert.equal(reopened.tabs[0].csvRequestId, 'csv-new', 'CSV result from a prior tab instance must be ignored')
assert.equal(reopened.tabs[0].csvError, null, 'stale CSV errors must not leak into a reopened document')
reopened = workspaceReducer(reopened, { type: 'csv-result', tabId: 'reopen', instanceId: csvInstance, requestId: 'csv-new', csv: null, error: 'CSV preview failed' })
assert.equal(reopened.tabs[0].csvError, 'CSV preview failed', 'current CSV error should stay with the tab/artifact request')
reopened = workspaceReducer(reopened, { type: 'csv-loading', tabId: 'reopen', instanceId: csvInstance, requestId: 'csv-retry', path: 'a.csv' })
assert.equal(reopened.tabs[0].csvError, null, 'starting a new CSV request should clear the prior CSV error')
reopened = workspaceReducer(reopened, { type: 'edit', tabId: 'reopen', bindingId: 'output', value: 'changed.csv' })
reopened = workspaceReducer(reopened, { type: 'csv-result', tabId: 'reopen', instanceId: csvInstance, requestId: 'csv-retry', csv: null, error: 'stale after edit' })
assert.equal(reopened.tabs[0].csvError, null, 'CSV response cannot survive a newer draft')
assert.equal(reopened.tabs[0].csvArtifactPath, null)

let actionsState = workspaceReducer(initialWorkspaceState, { type: 'merge-documents', documents: [doc('actions')], activateFirst: true })
actionsState = workspaceReducer(actionsState, { type: 'edit', tabId: 'actions', bindingId: 'p1', value: 'draft' })
let actionsTab = actionsState.tabs[0]
let availability = getChangeActionState(actionsTab)
assert.equal(availability.canPreview, true, 'dirty synchronized tab should be previewable')
assert.equal(availability.canApply, false, 'unvalidated draft must not be applicable')

actionsState = workspaceReducer(actionsState, {
  type: 'mutation-started', tabId: 'actions', instanceId: actionsTab.instanceId,
  requestId: 'actions-preview', baseVersion: actionsTab.edits.version, status: 'validating',
})
actionsTab = actionsState.tabs[0]
availability = getChangeActionState(actionsTab)
assert.equal(availability.canPreview, false, 'pending validation must block a second preview mutation')
assert.equal(availability.canApply, false, 'pending validation must block apply')

actionsState = workspaceReducer(actionsState, {
  type: 'preview-result', tabId: 'actions', instanceId: actionsTab.instanceId,
  requestId: 'actions-preview', baseVersion: actionsTab.edits.version,
  preview: { valid: true, diff: 'valid', issues: [] },
})
actionsTab = actionsState.tabs[0]
availability = getChangeActionState(actionsTab)
assert.equal(availability.canApply, true, 'fresh valid preview should enable apply')

actionsState = workspaceReducer(actionsState, {
  type: 'mutation-started', tabId: 'actions', instanceId: actionsTab.instanceId,
  requestId: 'actions-conflict', baseVersion: actionsTab.edits.version, status: 'saving',
})
actionsTab = actionsState.tabs[0]
availability = getChangeActionState(actionsTab)
assert.equal(availability.canPreview, false, 'pending apply must block preview')
assert.equal(availability.canApply, false, 'pending apply must block re-entry')

actionsState = workspaceReducer(actionsState, {
  type: 'mutation-error', tabId: 'actions', instanceId: actionsTab.instanceId,
  requestId: 'actions-conflict', baseVersion: actionsTab.edits.version, conflict: true,
  message: 'File changed externally',
})
actionsTab = actionsState.tabs[0]
availability = getChangeActionState(actionsTab)
assert.equal(actionsTab.mutationError, 'File changed externally', 'mutation errors should stay on the affected tab')
assert.equal(availability.canApply, false, 'stale valid preview must not remain applicable after conflict')
assert.equal(availability.canPreview, false, 'conflict requires reload before another preview')
assert.equal(availability.canReload, true, 'conflicted tab should enable reload')

actionsState = workspaceReducer(actionsState, { type: 'edit', tabId: 'actions', bindingId: 'p2', value: 'newer' })
actionsTab = actionsState.tabs[0]
availability = getChangeActionState(actionsTab)
assert.equal(actionsTab.status, 'conflict', 'editing must not bypass an unresolved external conflict')
assert.equal(actionsTab.mutationError, 'File changed externally', 'conflict feedback should remain until reload starts')
assert.equal(availability.canPreview, false, 'editing during conflict must not reopen preview')
assert.equal(availability.canReload, true, 'reload must remain available after additional draft edits')

actionsState = workspaceReducer(actionsState, {
  type: 'mutation-started', tabId: 'actions', instanceId: actionsTab.instanceId,
  requestId: 'actions-reload', baseVersion: actionsTab.edits.version, status: actionsTab.status,
})
assert.equal(actionsState.tabs[0].mutationError, null, 'reload start should clear the prior conflict message')
availability = getChangeActionState(actionsState.tabs[0])
assert.equal(availability.canReload, false, 'pending reload must block reload re-entry')

actionsTab = actionsState.tabs[0]
actionsState = workspaceReducer(actionsState, {
  type: 'mutation-error', tabId: 'actions', instanceId: actionsTab.instanceId,
  requestId: 'actions-reload', baseVersion: actionsTab.edits.version, conflict: true,
  message: 'Reload failed',
})
assert.equal(actionsState.tabs[0].status, 'conflict', 'failed conflict reload must stay in conflict state')
assert.equal(actionsState.tabs[0].mutationError, 'Reload failed')
assert.equal(getChangeActionState(actionsState.tabs[0]).canReload, true, 'failed conflict reload must remain retryable')

const validProjection = { documents: [], dependencies: [], issues: [] }
let projectionState = workspaceReducer(initialWorkspaceState, { type: 'projection', projection: validProjection })
projectionState = workspaceReducer(projectionState, { type: 'projection-loading' })
assert.equal(projectionState.projectionStatus, 'loading')
projectionState = workspaceReducer(projectionState, { type: 'projection-error', message: 'Unavailable' })
assert.equal(projectionState.projection, validProjection, 'failed projection retains last valid snapshot')
assert.equal(projectionState.projectionError, 'Unavailable')
assert.equal(JSON.parse(JSON.stringify(doc('revision'))).revision, doc('revision').revision)

let nestedDrafts = workspaceReducer(initialWorkspaceState, { type: 'merge-documents', documents: [doc('nested')] })
for (const path of [['options', 'left', 1], ['options', 'left', 2], ['options', 'right'], ['other']]) {
  nestedDrafts = workspaceReducer(nestedDrafts, { type: 'field-draft', tabId: 'nested', key: JSON.stringify(path), draft: { bindingId: String(path[0]), text: '-', error: 'Invalid number' } })
}
const pruned = workspaceReducer(nestedDrafts, { type: 'edit', tabId: 'nested', bindingId: 'options', value: { left: null }, clearDraftPaths: [['left']] })
assert.deepEqual(Object.keys(pruned.tabs[0].fieldDrafts), ['["options","right"]', '["other"]'], 'structural replacement clears only its draft subtree')
const shifted = workspaceReducer(nestedDrafts, { type: 'edit', tabId: 'nested', bindingId: 'options', value: { left: [0] }, clearDraftPaths: [['left', 2]] })
assert.ok(shifted.tabs[0].fieldDrafts['["options","left",1]'], 'list edit preserves earlier item drafts')
assert.ok(!shifted.tabs[0].fieldDrafts['["options","left",2]'], 'removed or shifted list drafts cannot attach to another item')

let saving = workspaceReducer(initialWorkspaceState, { type: 'merge-documents', documents: [doc('saving')] })
saving = workspaceReducer(saving, { type: 'edit', tabId: 'saving', bindingId: 'value', value: 'saved' })
const saveTab = saving.tabs[0]
saving = workspaceReducer(saving, { type: 'mutation-started', tabId: 'saving', instanceId: saveTab.instanceId, requestId: 'save', baseVersion: saveTab.edits.version, status: 'saving' })
for (const action of [
  { type: 'edit', tabId: 'saving', bindingId: 'value', value: 'newer' },
  { type: 'field-draft', tabId: 'saving', key: '["value"]', draft: { bindingId: 'value', text: '-', error: 'Invalid' } },
  { type: 'undo', tabId: 'saving' },
  { type: 'redo', tabId: 'saving' },
] as const) assert.equal(workspaceReducer(saving, action).tabs[0], saving.tabs[0], 'save must freeze draft mutations')
assert.equal(getChangeActionState(saving.tabs[0]).canUndo, false)
const saved = { ...doc('saving'), revision: 'saved-revision' }
const saveResult = { type: 'replace-document', tabId: 'saving', instanceId: saveTab.instanceId, requestId: 'save', baseVersion: saveTab.edits.version, document: saved } as const
assert.equal(workspaceReducer(saving, saveResult).tabs[0].document.revision, 'saved-revision')
let reopenSave = workspaceReducer(saving, { type: 'close', tabId: 'saving' })
reopenSave = workspaceReducer(reopenSave, { type: 'merge-documents', documents: [doc('saving')] })
assert.equal(workspaceReducer(reopenSave, saveResult).tabs[0].document.revision, doc('saving').revision, 'old save response cannot replace a reopened tab')

console.log('workspaceState tests passed')
