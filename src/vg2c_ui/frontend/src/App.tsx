import { useEffect, useState, type KeyboardEvent as ReactKeyboardEvent } from 'react'

import { AddFilesButton } from './AddFilesButton'
import { ContextSidebar } from './ContextSidebar'
import type { ChangePreviewView, ParameterView } from './contracts.generated'
import { useNotifications } from './NotificationProvider'
import { baseName } from './operationLabels'
import { ancestorScopeIds, ScriptTree } from './ScriptTree'
import { TranslationProgress } from './TranslationProgress'
import { useBatchTranslation } from './useBatchTranslation'
import { useWorkspace } from './useWorkspace'
import type { TabStatus, TabState } from './workspaceState'

export function App() {
  const workspace = useWorkspace()
  const { state, active, dispatch } = workspace
  const { notify } = useNotifications()
  const batch = useBatchTranslation({ onDocuments: workspace.mergeDocuments })
  const [search, setSearch] = useState('')
  const [contextOpen, setContextOpen] = useState(false)

  function selectItem(id: string) {
    if (!active) return
    dispatch({ type: 'select', tabId: active.document.id, itemId: id })
    for (const scopeId of ancestorScopeIds(active.document, id)) {
      dispatch({ type: 'toggle-scope', tabId: active.document.id, scopeId, expanded: true })
    }
  }

  async function validateActive() {
    if (!active) return
    try {
      const preview = await workspace.validate(active.document.id)
      if (!preview) return
      notify({
        type: preview.valid ? 'success' : 'warning',
        title: preview.valid ? 'Changes validated' : 'Changes need attention',
        message: preview.valid ? 'Review the generated Python diff, then apply.' : 'Validation found problems in the pending changes.',
      })
    } catch (error) {
      notifyError(notify, 'Could not validate changes', error)
    }
  }

  async function applyActive() {
    if (!active) return
    try {
      const result = await workspace.apply(active.document.id)
      if (result) notify({ type: 'success', title: 'Changes applied', message: 'Generated Python was updated atomically.' })
    } catch (error) {
      notifyError(notify, 'Could not apply changes', error)
    }
  }

  async function reloadActive() {
    if (!active) return
    try {
      await workspace.reload(active.document.id)
      notify({ type: 'info', title: 'Document reloaded', message: 'Unsaved drafts were cleared.' })
    } catch (error) {
      notifyError(notify, 'Reload failed', error)
    }
  }

  useEffect(() => {
    function shortcut(event: KeyboardEvent) {
      if (event.key === 'Escape' && contextOpen) {
        setContextOpen(false)
        return
      }
      if (!(event.ctrlKey || event.metaKey) || !active) return
      const key = event.key.toLowerCase()
      if (key === 'z') {
        event.preventDefault()
        dispatch({ type: event.shiftKey ? 'redo' : 'undo', tabId: active.document.id })
      } else if (key === 'y') {
        event.preventDefault()
        dispatch({ type: 'redo', tabId: active.document.id })
      } else if (key === 's') {
        event.preventDefault()
        if (active.preview?.valid) void applyActive()
        else void validateActive()
      }
    }
    window.addEventListener('keydown', shortcut)
    return () => window.removeEventListener('keydown', shortcut)
  })

  const diagnostics = active?.document.diagnostics ?? []
  const editCount = active ? Object.keys(active.edits.values).length : 0
  const documents = state.tabs.map((tab) => tab.document)

  return <main className="app-shell">
    <header className="topbar">
      <div className="brand"><span>PYTHON</span>PathFinder</div>
      <div className="topbar-meta" aria-live="polite">
        {batch.busy ? 'Translating selected files…' : active ? baseName(active.document.output_path || active.document.source_path) : 'VG2 translation workspace'}
      </div>
    </header>

    <nav className="tabs" aria-label="Open translated files" role="tablist">
      {state.tabs.map((tab, index) => <div className={`tab${tab.document.id === state.activeId ? ' is-active' : ''}`} key={tab.document.id}>
        <button
          type="button"
          role="tab"
          aria-selected={tab.document.id === state.activeId}
          aria-controls="script-workspace"
          tabIndex={tab.document.id === state.activeId ? 0 : -1}
          title={tab.document.output_path}
          onClick={() => dispatch({ type: 'activate', tabId: tab.document.id })}
          onKeyDown={(event) => handleTabKeys(event, state.tabs, index, (id) => dispatch({ type: 'activate', tabId: id }))}
        >
          <span className="tab-name">{baseName(tab.document.output_path || tab.document.source_path)}</span>
          <span className={`tab-state tab-state--${tab.status}`} aria-hidden="true" />
          <span className="sr-only">{tab.status}</span>
        </button>
        <button
          className="tab-close"
          type="button"
          tabIndex={tab.document.id === state.activeId ? 0 : -1}
          onClick={() => dispatch({ type: 'close', tabId: tab.document.id })}
          aria-label={`Close ${baseName(tab.document.output_path || tab.document.source_path)}`}
        >×</button>
      </div>)}
    </nav>

    <section className="workspace" id="script-workspace" role="tabpanel" aria-label="Translated script editor">
      <section className="editor-pane">
        <div className="editor-toolbar">
          <label className="search-field">
            <span className="sr-only">Search operations</span>
            <input value={search} onChange={(event) => setSearch(event.target.value)} type="search" placeholder="Search operations…" disabled={!active} />
          </label>
          <div className="toolbar-group tree-controls">
            <button type="button" onClick={() => active && dispatch({ type: 'set-all-scopes', tabId: active.document.id, expanded: true })} disabled={!active?.document.scopes.length}>Expand all</button>
            <button type="button" onClick={() => active && dispatch({ type: 'set-all-scopes', tabId: active.document.id, expanded: false })} disabled={!active?.document.scopes.length}>Collapse all</button>
          </div>
          <button className="context-toggle" type="button" onClick={() => setContextOpen(true)} disabled={!active} aria-expanded={contextOpen} aria-controls="file-context">File context</button>
        </div>

        {active && <div className="change-toolbar">
          <div className="change-status">
            <strong>{editCount ? `${editCount} unsaved change${editCount === 1 ? '' : 's'}` : 'No pending changes'}</strong>
            <small>{statusCopy(active.status)}</small>
          </div>
          <div className="toolbar-group">
            <button type="button" onClick={() => dispatch({ type: 'undo', tabId: active.document.id })} disabled={!active.edits.history.length}>Undo</button>
            <button type="button" onClick={() => dispatch({ type: 'redo', tabId: active.document.id })} disabled={!active.edits.future.length}>Redo</button>
            <button type="button" onClick={() => void validateActive()} disabled={!editCount || active.status === 'validating' || !active.document.synchronized}>Preview changes</button>
            <button className="primary-button" type="button" onClick={() => void applyActive()} disabled={!active.preview?.valid || active.status === 'saving'}>Apply changes</button>
            {active.status === 'conflict' && <button type="button" onClick={() => void reloadActive()}>Reload</button>}
          </div>
        </div>}

        <div className="editor-scroll">
          {active ? <ScriptTree
            tabId={active.document.id}
            document={active.document}
            projection={state.projection}
            search={search}
            expandedScopes={active.expandedScopeIds}
            selectedId={active.selectedId}
            values={active.edits.values}
            onSelect={selectItem}
            onToggleScope={(id, expanded) => dispatch({ type: 'toggle-scope', tabId: active.document.id, scopeId: id, expanded })}
            onEdit={(parameter: ParameterView, value) => workspace.edit(active.document.id, parameter, value)}
            inspectSql={workspace.inspectStructuredSql}
            runSqlAction={workspace.runSqlAction}
          /> : <div className="empty-state">
            <strong>No translated file open</strong>
            <span>Use the + button to select and translate one or more VG2 source files.</span>
          </div>}
          {active?.preview && <ChangePreview preview={active.preview} />}
          <details className="diagnostics" open={diagnostics.some((item) => item.level === 'error')}>
            <summary>Diagnostics <span>{diagnostics.length}</span></summary>
            <div>{diagnostics.length ? diagnostics.map((item, index) => <p key={`${item.code}-${index}`} className={`diagnostic diagnostic--${item.level}`}><strong>{item.code}</strong> {item.message} {item.location && <small>{item.location}</small>}</p>) : <p className="empty-copy">No diagnostics.</p>}</div>
          </details>
        </div>
      </section>

      <button className={`context-backdrop${contextOpen ? ' is-open' : ''}`} type="button" aria-label="Close file context" tabIndex={contextOpen ? 0 : -1} onClick={() => setContextOpen(false)} />
      <ContextSidebar
        document={active?.document ?? null}
        documents={documents}
        projection={state.projection}
        csv={active?.csv ?? null}
        csvArtifactPath={active?.csvArtifactPath ?? null}
        open={contextOpen}
        onClose={() => setContextOpen(false)}
        onPreviewCsv={(path) => active && void workspace.loadCsv(active.document.id, path).catch((error) => notifyError(notify, 'CSV preview failed', error))}
        onActivateDocument={(id) => {
          dispatch({ type: 'activate', tabId: id })
          setContextOpen(false)
        }}
      />
    </section>

    <TranslationProgress progress={batch.progress} onDismiss={batch.dismissProgress} />
    <AddFilesButton onFiles={(files) => void batch.translateFiles(files)} disabled={batch.busy} />
  </main>
}

function ChangePreview({ preview }: { preview: ChangePreviewView }) {
  return <details className={`change-preview${preview.valid ? ' is-valid' : ' is-invalid'}`} open={!preview.valid}>
    <summary>{preview.valid ? 'Validated Python diff' : 'Changes need attention'}</summary>
    <div>
      {preview.issues.map((issue) => <p className="validation-error" key={`${issue.code}-${issue.message}`}>{issue.message}</p>)}
      <pre>{preview.diff || 'No textual change.'}</pre>
    </div>
  </details>
}

function notifyError(
  notify: (input: { type: 'error'; title: string; message: string }) => string,
  title: string,
  error: unknown,
) {
  notify({ type: 'error', title, message: error instanceof Error ? error.message : title })
}

function statusCopy(status: TabStatus): string {
  if (status === 'dirty') return 'Preview before applying.'
  if (status === 'validating') return 'Validating generated Python…'
  if (status === 'valid') return 'Validation passed.'
  if (status === 'invalid') return 'Validation found issues.'
  if (status === 'saving') return 'Applying changes…'
  if (status === 'conflict') return 'File changed externally; reload required.'
  if (status === 'error') return 'The last update failed.'
  return 'Select an operation to inspect or edit it.'
}

function handleTabKeys(
  event: ReactKeyboardEvent<HTMLButtonElement>,
  tabs: TabState[],
  index: number,
  activate: (id: string) => void,
) {
  if (!['ArrowLeft', 'ArrowRight', 'Home', 'End'].includes(event.key)) return
  event.preventDefault()
  let nextIndex = index
  if (event.key === 'ArrowLeft') nextIndex = (index - 1 + tabs.length) % tabs.length
  if (event.key === 'ArrowRight') nextIndex = (index + 1) % tabs.length
  if (event.key === 'Home') nextIndex = 0
  if (event.key === 'End') nextIndex = tabs.length - 1
  const next = tabs[nextIndex]
  if (!next) return
  activate(next.document.id)
  document.querySelectorAll<HTMLButtonElement>('.tab > button[role="tab"]')[nextIndex]?.focus()
}
