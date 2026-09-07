import assert from 'node:assert/strict'
import type { DocumentView } from './contracts.generated.ts'
import { initialWorkspaceState, workspaceReducer } from './workspaceState.ts'

function doc(id: string, revision = 1): DocumentView {
  return {
    schema_version: 2,
    id,
    source_path: `${id}.txt`,
    output_path: `${id}.py`,
    source_hash: `src-${id}-${revision}`,
    output_hash: `out-${id}-${revision}`,
    revision,
    synchronized: true,
    read_only_reason: null,
    steps: [], scopes: [], artifacts: [], diagnostics: [],
  }
}

let state = workspaceReducer(initialWorkspaceState, {
  type: 'merge-documents',
  documents: [doc('one'), doc('two'), doc('three')],
  activateFirst: true,
  preserveDirty: true,
})
assert.deepEqual(state.tabs.map((tab) => tab.document.id), ['one', 'two', 'three'])
assert.equal(state.activeId, 'one', 'successful translated documents become independent tabs')

state = workspaceReducer(state, { type: 'edit', tabId: 'two', parameterId: 'p', value: 'dirty draft' })
const dirtyBefore = state.tabs.find((tab) => tab.document.id === 'two')!
state = workspaceReducer(state, {
  type: 'merge-documents',
  documents: [doc('two', 2), doc('four')],
  activateFirst: true,
  preserveDirty: true,
})
const dirtyAfter = state.tabs.find((tab) => tab.document.id === 'two')!
assert.equal(dirtyAfter, dirtyBefore, 'a translation merge must not replace a dirty existing tab')
assert.equal(dirtyAfter.document.revision, 1)
assert.equal(dirtyAfter.edits.values.p, 'dirty draft')
assert.equal(state.activeId, 'four', 'a skipped dirty collision must not become the active translation result')
assert.ok(state.tabs.some((tab) => tab.document.id === 'four'))

console.log('workspace merge tests passed')
