import assert from 'node:assert/strict'
import { getChangeActionState } from './guards.ts'
import { draftChanges, initialWorkspaceState, RESET_VALUE, workspaceProjectionRequest, workspaceReducer } from './state.ts'
import { solvePanes } from '../workbench/paneSolver.ts'
import { SCHEMA_VERSION, type DocumentView } from '../api/contracts.generated.ts'

function doc(id: string): DocumentView {
  return {
    schema_version: SCHEMA_VERSION,
    id,
    source_path: `${id}.txt`,
    output_path: `${id}.py`,
    source_hash: `src-${id}`,
    output_hash: `out-${id}`,
    revision: '5755395593540295586',
    compiler_hash: `compiler-${id}`,
    generation_state: 'current',
    read_only_reason: null,
    diagnostics: [], effects: [],
    semantic_operations: [], files: [], symbols: [], condition_operators: [],
  }
}

let state = workspaceReducer(initialWorkspaceState, { type: 'merge-documents', documents: [doc('a'), doc('b')], activateFirst: true })
state = workspaceReducer(state, { type: 'edit', tabId: 'a', bindingId: 'p1', value: 'draft' })
state = workspaceReducer(state, { type: 'activate', tabId: 'b' })
assert.equal(state.tabs.find((tab) => tab.document.id === 'a')?.edits.values.p1, 'draft', 'inactive dirty tab must retain its draft')
assert.equal(state.tabs.find((tab) => tab.document.id === 'a')?.status, 'dirty')

const refreshedA = { ...doc('a'), revision: 'translated-revision' }
const preserved = workspaceReducer(state, {
  type: 'merge-documents',
  documents: [refreshedA, doc('c')],
  activateFirst: true,
  preserveDirty: true,
})
assert.equal(
  preserved.tabs.find((tab) => tab.document.id === 'a'),
  state.tabs.find((tab) => tab.document.id === 'a'),
  'batch translation must not replace an existing tab with unsaved edits',
)
assert.equal(preserved.tabs.find((tab) => tab.document.id === 'a')?.document.revision, doc('a').revision)
assert.equal(preserved.activeId, 'c', 'a skipped dirty collision must not become the active translation result')
assert.ok(preserved.tabs.some((tab) => tab.document.id === 'c'))

const projection = workspaceProjectionRequest(state)
assert.deepEqual(projection.documents.find((item) => item.document_id === 'a')?.changes, [{ binding_id: 'p1', value: 'draft', symbol_id: null, reset: false }])
assert.deepEqual(projection.documents.find((item) => item.document_id === 'b')?.changes, [])

const fileDoc = doc('files')
fileDoc.semantic_operations = [{
  id: 'copy', kind: 'copy', display_name: 'Copy', summary: '',
  parent_operation_id: null, branch: null, source_span: { file: null, start_line: 1, end_line: 1 },
  capabilities: [], comments: [], validation_state: 'valid', visibility: 'normal',
  bindings: [{
    id: 'source', owner_operation_id: 'copy', name: 'src', display_label: 'Source',
    value: '', default: null, required: true, visibility: 'normal', capabilities: ['file-input'],
    validation_state: 'valid', resettable: true, editable: true, read_only_reason: null,
    value_schema: null, file_choices: [],
  }],
}]
let fileState = workspaceReducer(initialWorkspaceState, { type: 'merge-documents', documents: [fileDoc] })
fileState = workspaceReducer(fileState, { type: 'edit', tabId: 'files', bindingId: 'source', value: 'draft.csv' })
const refreshedFiles = structuredClone(fileDoc)
refreshedFiles.semantic_operations[0].bindings[0].file_choices = ['inputs/uploaded.csv']
fileState = workspaceReducer(fileState, {
  type: 'refresh-file-choices', tabId: 'files', instanceId: fileState.tabs[0].instanceId,
  document: refreshedFiles,
})
assert.deepEqual(fileState.tabs[0].document.semantic_operations[0].bindings[0].file_choices, ['inputs/uploaded.csv'])
assert.equal(fileState.tabs[0].edits.values.source, 'draft.csv', 'upload refresh must preserve unsaved edits')

const beforeB = state.tabs.find((tab) => tab.document.id === 'b')!
const navigationDoc = doc('navigation')
navigationDoc.semantic_operations = [
  { id: 'scope-a', kind: 'loop', display_name: 'Loop', parent_operation_id: null, branch: null, source_span: { file: null, start_line: 1, end_line: 2 }, bindings: [], capabilities: [], comments: [], validation_state: 'valid', visibility: 'normal' },
  ...['operation-a', 'operation-b'].map((id) => ({ id, kind: 'probe', display_name: 'Probe', parent_operation_id: 'scope-a', branch: null, source_span: { file: null, start_line: 1, end_line: 2 }, bindings: [], capabilities: [], comments: [], validation_state: 'valid' as const, visibility: 'normal' as const })),
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
assert.deepEqual(draftChanges(resetState.tabs[0]), [{ binding_id: 'p1', value: null, symbol_id: null, reset: true }])
assert.equal(workspaceReducer(resetState, { type: 'undo', tabId: 'a' }).tabs[0].edits.values.p1, 'draft')
let invalidFields = workspaceReducer(state, { type: 'field-draft', tabId: 'a', key: 'number', draft: { bindingId: 'numeric', text: '', error: 'Enter a number' } })
invalidFields = workspaceReducer(invalidFields, { type: 'edit', tabId: 'a', bindingId: 'other', value: 'changed' })
assert.equal(invalidFields.tabs[0].fieldDrafts.number.text, '')
assert.equal(getChangeActionState(invalidFields.tabs[0]).canPreview, false)
assert.equal(getChangeActionState(invalidFields.tabs[0]).canSave, false)
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

const narrowPanes = solvePanes(390, 360, 460, false, false, null)
assert.equal(narrowPanes.single, true)
const twoPanes = solvePanes(800, 360, 460, false, false, null)
assert.deepEqual([twoPanes.showConfig, twoPanes.showContext], [false, true])
assert.ok(twoPanes.logicWidth + 6 + 320 <= 800)
const focusedConfig = solvePanes(800, 360, 460, false, false, 'config')
assert.deepEqual([focusedConfig.showConfig, focusedConfig.showContext], [true, false])
const threePanes = solvePanes(1200, 360, 460, false, false, null)
assert.deepEqual([threePanes.showConfig, threePanes.showContext], [true, true])
const manuallyClosed = solvePanes(1200, 360, 460, true, false, 'config')
assert.deepEqual([manuallyClosed.showConfig, manuallyClosed.showContext], [false, true])

let actionsState = workspaceReducer(initialWorkspaceState, { type: 'merge-documents', documents: [doc('actions')], activateFirst: true })
actionsState = workspaceReducer(actionsState, { type: 'edit', tabId: 'actions', bindingId: 'p1', value: 'draft' })
let actionsTab = actionsState.tabs[0]
let availability = getChangeActionState(actionsTab)
assert.equal(availability.canPreview, true, 'dirty editable tab should be previewable')
assert.equal(availability.canSave, true, 'valid draft may be saved without preview')
assert.equal(availability.canGenerate, false, 'unsaved draft must block generation')
const savedStale = doc('saved-stale')
savedStale.generation_state = 'stale'
let generatedState = workspaceReducer(initialWorkspaceState, { type: 'merge-documents', documents: [savedStale] })
assert.equal(getChangeActionState(generatedState.tabs[0]).canGenerate, true, 'saved stale state can generate')
generatedState = workspaceReducer(generatedState, { type: 'edit', tabId: 'saved-stale', bindingId: 'p1', value: 'draft' })
assert.equal(getChangeActionState(generatedState.tabs[0]).canGenerate, false, 'draft blocks generation')

actionsState = workspaceReducer(actionsState, {
  type: 'mutation-started', tabId: 'actions', instanceId: actionsTab.instanceId,
  requestId: 'actions-preview', baseVersion: actionsTab.edits.version, status: 'validating',
})
actionsTab = actionsState.tabs[0]
availability = getChangeActionState(actionsTab)
assert.equal(availability.canPreview, false, 'pending validation must block a second preview mutation')
assert.equal(availability.canSave, false, 'pending validation must block save')

actionsState = workspaceReducer(actionsState, {
  type: 'preview-result', tabId: 'actions', instanceId: actionsTab.instanceId,
  requestId: 'actions-preview', baseVersion: actionsTab.edits.version,
  preview: { valid: true, diff: 'valid', issues: [] },
})
actionsTab = actionsState.tabs[0]
availability = getChangeActionState(actionsTab)
assert.equal(availability.canSave, true, 'validated draft remains saveable')

actionsState = workspaceReducer(actionsState, {
  type: 'mutation-started', tabId: 'actions', instanceId: actionsTab.instanceId,
  requestId: 'actions-conflict', baseVersion: actionsTab.edits.version, status: 'saving',
})
actionsTab = actionsState.tabs[0]
availability = getChangeActionState(actionsTab)
assert.equal(availability.canPreview, false, 'pending save must block preview')
assert.equal(availability.canSave, false, 'pending save must block re-entry')

actionsState = workspaceReducer(actionsState, {
  type: 'mutation-error', tabId: 'actions', instanceId: actionsTab.instanceId,
  requestId: 'actions-conflict', baseVersion: actionsTab.edits.version, conflict: true,
  message: 'File changed externally',
})
actionsTab = actionsState.tabs[0]
availability = getChangeActionState(actionsTab)
assert.equal(actionsTab.mutationError, 'File changed externally', 'mutation errors should stay on the affected tab')
assert.equal(availability.canSave, false, 'conflict must block save')
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
