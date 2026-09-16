import assert from 'node:assert/strict'
import { getChangeActionState } from './workspaceGuards.ts'
import { initialWorkspaceState, workspaceProjectionRequest, workspaceReducer } from './workspaceState.ts'
import type { DocumentView } from './contracts.generated.ts'

function doc(id: string): DocumentView {
  return {
    schema_version: 2,
    id,
    source_path: `${id}.txt`,
    output_path: `${id}.py`,
    source_hash: `src-${id}`,
    output_hash: `out-${id}`,
    revision: 1,
    synchronized: true,
    read_only_reason: null,
    steps: [], scopes: [], artifacts: [], diagnostics: [],
  }
}

let state = workspaceReducer(initialWorkspaceState, { type: 'merge-documents', documents: [doc('a'), doc('b')], activateFirst: true })
state = workspaceReducer(state, { type: 'edit', tabId: 'a', parameterId: 'p1', value: 'draft' })
state = workspaceReducer(state, { type: 'activate', tabId: 'b' })
assert.equal(state.tabs.find((tab) => tab.document.id === 'a')?.edits.values.p1, 'draft', 'inactive dirty tab must retain its draft')
assert.equal(state.tabs.find((tab) => tab.document.id === 'a')?.status, 'dirty')

const projection = workspaceProjectionRequest(state)
assert.deepEqual(projection.documents.find((item) => item.document_id === 'a')?.changes, [{ parameter_id: 'p1', value: 'draft' }])
assert.deepEqual(projection.documents.find((item) => item.document_id === 'b')?.changes, [])

const beforeB = state.tabs.find((tab) => tab.document.id === 'b')!
const a = state.tabs.find((tab) => tab.document.id === 'a')!
state = workspaceReducer(state, {
  type: 'mutation-started', tabId: 'a', instanceId: a.instanceId,
  requestId: 'preview-1', baseVersion: 1, status: 'validating',
})
assert.equal(state.tabs.find((tab) => tab.document.id === 'b'), beforeB, 'targeted async state must not mutate active-but-unrelated tab')

state = workspaceReducer(state, { type: 'edit', tabId: 'a', parameterId: 'p2', value: 'newer', baseVersion: 1 })
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

let actionsState = workspaceReducer(initialWorkspaceState, { type: 'merge-documents', documents: [doc('actions')], activateFirst: true })
actionsState = workspaceReducer(actionsState, { type: 'edit', tabId: 'actions', parameterId: 'p1', value: 'draft' })
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

actionsState = workspaceReducer(actionsState, { type: 'edit', tabId: 'actions', parameterId: 'p2', value: 'newer' })
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

console.log('workspaceState tests passed')
