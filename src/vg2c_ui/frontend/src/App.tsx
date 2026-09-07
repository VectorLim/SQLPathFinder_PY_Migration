import { useEffect, useState, type KeyboardEvent as ReactKeyboardEvent } from 'react'

import {
  ApiError,
  applyCommands,
  openWorkflow,
  previewCommands,
  previewCsv,
  translateBatch,
} from './api'
import { ContextSidebar } from './ContextSidebar'
import {
  declaredHeadersForPath,
  deriveFileFlow,
  headerCacheKey,
  type HeaderInfo,
} from './dataFlow'
import { editValue, emptyEdits, redo, undo, type EditState } from './editState'
import { baseName } from './operationLabels'
import { ancestorScopeIds, ScriptTree } from './ScriptTree'
import type {
  CommandBatch,
  CommandPreview,
  CsvPreview,
  Diagnostic,
  ParameterDescriptor,
  WorkflowDocument,
} from './types'

type TabStatus = 'ready' | 'dirty' | 'validating' | 'valid' | 'invalid' | 'saving' | 'conflict' | 'error'

interface TabState {
  document: WorkflowDocument
  selectedId: string | null
  status: TabStatus
  edits: EditState
  preview: CommandPreview | null
  csv: CsvPreview | null
  expandedScopeIds: Set<string>
}

function createTab(document: WorkflowDocument, previous?: TabState): TabState {
  const itemIds = new Set([...document.steps, ...document.scopes].map((item) => item.id))
  const scopeIds = new Set(document.scopes.map((scope) => scope.id))
  return {
    document,
    selectedId: previous?.selectedId && itemIds.has(previous.selectedId) ? previous.selectedId : null,
    status: 'ready',
    edits: emptyEdits(),
    preview: null,
    csv: null,
    expandedScopeIds: new Set(
      [...(previous?.expandedScopeIds ?? [])].filter((id) => scopeIds.has(id)),
    ),
  }
}

function withEdits(tab: TabState, edits: EditState): TabState {
  return {
    ...tab,
    edits,
    preview: null,
    status: Object.keys(edits.values).length ? 'dirty' : 'ready',
  }
}

export function App() {
  const [tabs, setTabs] = useState<TabState[]>([])
  const [activeId, setActiveId] = useState<string | null>(null)
  const [sourceText, setSourceText] = useState('')
  const [search, setSearch] = useState('')
  const [busy, setBusy] = useState(false)
  const [message, setMessage] = useState('Enter one VG2 source path per line.')
  const [batchDiagnostics, setBatchDiagnostics] = useState<Diagnostic[]>([])
  const [contextOpen, setContextOpen] = useState(false)
  const [headerCache, setHeaderCache] = useState<Record<string, HeaderInfo>>({})

  const active = tabs.find((tab) => tab.document.id === activeId) ?? null
  const documents = tabs.map((tab) => tab.document)

  function updateActive(update: (tab: TabState) => TabState) {
    setTabs((current) => current.map((tab) => tab.document.id === activeId ? update(tab) : tab))
  }

  function replaceDocument(document: WorkflowDocument) {
    setTabs((current) => current.map((tab) => (
      tab.document.id === document.id ? createTab(document, tab) : tab
    )))
  }

  async function translate(paths?: string[]) {
    const sourcePaths = paths ?? parseSourcePaths(sourceText)
    if (!sourcePaths.length) return
    setBusy(true)
    setMessage('Translating…')
    try {
      const response = await translateBatch(sourcePaths)
      setBatchDiagnostics(response.diagnostics)
      setTabs((current) => mergeDocuments(current, response.documents))
      if (response.documents.length) {
        setActiveId(response.documents[0].id)
        setMessage(`${response.documents.length} translated script${response.documents.length === 1 ? '' : 's'} ready.`)
      } else {
        setMessage('No scripts were translated.')
      }
    } catch (error) {
      setMessage(error instanceof Error ? error.message : 'Translation failed')
    } finally {
      setBusy(false)
    }
  }

  async function openExisting() {
    const sourcePaths = parseSourcePaths(sourceText)
    if (!sourcePaths.length) return
    setBusy(true)
    setMessage('Opening generated scripts…')
    try {
      const documentsToOpen = await Promise.all(sourcePaths.map((path) => openWorkflow(path)))
      setTabs((current) => mergeDocuments(current, documentsToOpen))
      setActiveId(documentsToOpen[0]?.id ?? null)
      setMessage(`${documentsToOpen.length} script${documentsToOpen.length === 1 ? '' : 's'} opened.`)
    } catch (error) {
      setMessage(error instanceof Error ? error.message : 'Open failed')
    } finally {
      setBusy(false)
    }
  }

  function editParameter(parameter: ParameterDescriptor, value: unknown) {
    updateActive((tab) => withEdits(tab, editValue(tab.edits, parameter.id, value)))
  }

  function commandBatch(tab: TabState): CommandBatch | null {
    const commands = tab.document.steps.flatMap((step) => step.parameters
      .filter((parameter) => Object.hasOwn(tab.edits.values, parameter.id))
      .map((parameter) => ({
        operation: 'set-parameter' as const,
        step_id: step.id,
        parameter_id: parameter.id,
        value: tab.edits.values[parameter.id],
      })))
    if (!commands.length) return null
    return {
      source_path: tab.document.source_path,
      output_path: tab.document.output_path,
      source_hash: tab.document.source_hash,
      output_hash: tab.document.output_hash,
      revision: tab.document.revision,
      commands,
    }
  }

  async function validateEdits() {
    if (!active) return
    const batch = commandBatch(active)
    if (!batch) return
    updateActive((tab) => ({ ...tab, status: 'validating' }))
    try {
      const preview = await previewCommands(batch)
      updateActive((tab) => ({ ...tab, preview, status: preview.valid ? 'valid' : 'invalid' }))
      setMessage(preview.valid ? 'Changes are valid. Review the diff, then apply.' : 'Validation found problems.')
    } catch (error) {
      handleMutationError(error)
    }
  }

  async function persistEdits() {
    if (!active?.preview?.valid) return
    const batch = commandBatch(active)
    if (!batch) return
    updateActive((tab) => ({ ...tab, status: 'saving' }))
    try {
      const result = await applyCommands(batch)
      replaceDocument(result.document)
      setMessage('Changes applied atomically to generated Python.')
    } catch (error) {
      handleMutationError(error)
    }
  }

  function handleMutationError(error: unknown) {
    const conflict = error instanceof ApiError && error.status === 409
    updateActive((tab) => ({ ...tab, status: conflict ? 'conflict' : 'error' }))
    setMessage(error instanceof Error ? error.message : 'Could not update the document')
  }

  async function loadCsv(path: string) {
    if (!active) return
    try {
      const csv = await previewCsv(active.document.source_path, path)
      updateActive((tab) => ({ ...tab, csv }))
      setHeaderCache((current) => ({
        ...current,
        [headerCacheKey(active.document, path)]: { columns: csv.columns, source: 'detected' },
      }))
    } catch (error) {
      updateActive((tab) => ({ ...tab, csv: null }))
      setMessage(error instanceof Error ? error.message : 'CSV preview failed')
    }
  }

  async function reloadActive() {
    if (!active) return
    try {
      const document = await openWorkflow(active.document.source_path, active.document.output_path)
      replaceDocument(document)
      setMessage('Reloaded the externally changed document; local edits were cleared.')
    } catch (error) {
      setMessage(error instanceof Error ? error.message : 'Reload failed')
    }
  }

  function selectItem(id: string) {
    updateActive((tab) => ({
      ...tab,
      selectedId: id,
      csv: null,
      expandedScopeIds: new Set([
        ...tab.expandedScopeIds,
        ...ancestorScopeIds(tab.document, id),
      ]),
    }))
  }

  function toggleScope(id: string, expanded?: boolean) {
    updateActive((tab) => {
      const next = new Set(tab.expandedScopeIds)
      const shouldExpand = expanded ?? !next.has(id)
      if (shouldExpand) next.add(id)
      else next.delete(id)
      return { ...tab, selectedId: id, csv: null, expandedScopeIds: next }
    })
  }

  function setAllScopes(expanded: boolean) {
    updateActive((tab) => ({
      ...tab,
      expandedScopeIds: expanded
        ? new Set(tab.document.scopes.map((scope) => scope.id))
        : new Set(),
    }))
  }

  function closeTab(id: string) {
    setTabs((current) => {
      const index = current.findIndex((tab) => tab.document.id === id)
      const remaining = current.filter((tab) => tab.document.id !== id)
      if (id === activeId) {
        setActiveId(remaining[Math.min(index, remaining.length - 1)]?.document.id ?? null)
      }
      return remaining
    })
  }

  useEffect(() => {
    function shortcut(event: KeyboardEvent) {
      if (event.key === 'Escape' && contextOpen) {
        setContextOpen(false)
        return
      }
      if (!(event.ctrlKey || event.metaKey)) return
      if ((event.target as HTMLElement | null)?.closest('.translate-box')) return
      if (event.key.toLocaleLowerCase() === 'z') {
        event.preventDefault()
        updateActive((tab) => withEdits(tab, event.shiftKey ? redo(tab.edits) : undo(tab.edits)))
      } else if (event.key.toLocaleLowerCase() === 'y') {
        event.preventDefault()
        updateActive((tab) => withEdits(tab, redo(tab.edits)))
      } else if (event.key.toLocaleLowerCase() === 's') {
        event.preventDefault()
        if (active?.preview?.valid) void persistEdits()
        else void validateEdits()
      }
    }
    window.addEventListener('keydown', shortcut)
    return () => window.removeEventListener('keydown', shortcut)
  })

  useEffect(() => {
    if (!active) return
    let cancelled = false
    const flow = deriveFileFlow(active.document, documents)
    const paths = [...flow.inputs, ...flow.outputs].map((artifact) => artifact.path)
    const missing = [...new Set(paths)].filter((path) => {
      if (declaredHeadersForPath(active.document, path, active.edits.values).length) return false
      return !headerCache[headerCacheKey(active.document, path)]
    })
    if (!missing.length) return

    setHeaderCache((current) => {
      const next = { ...current }
      for (const path of missing) {
        next[headerCacheKey(active.document, path)] = { columns: [], source: 'loading' }
      }
      return next
    })

    void Promise.all(missing.map(async (path) => {
      const key = headerCacheKey(active.document, path)
      try {
        const csv = await previewCsv(active.document.source_path, path)
        if (!cancelled) {
          setHeaderCache((current) => ({
            ...current,
            [key]: { columns: csv.columns, source: 'detected' },
          }))
        }
      } catch {
        if (!cancelled) {
          setHeaderCache((current) => ({
            ...current,
            [key]: { columns: [], source: 'unknown' },
          }))
        }
      }
    }))
    return () => { cancelled = true }
  }, [activeId, active?.document, active?.edits.values, documents.length])

  const diagnostics = active ? [...active.document.diagnostics, ...batchDiagnostics] : batchDiagnostics
  const editCount = active ? Object.keys(active.edits.values).length : 0

  return (
    <main className="app-shell">
      <header className="topbar">
        <div className="brand"><span>PYTHON</span>PathFinder</div>
        <div className="translate-box">
          <textarea
            aria-label="VG2 source paths"
            value={sourceText}
            onChange={(event) => setSourceText(event.target.value)}
            placeholder={'workflows/report.txt\nworkflows/export.txt'}
            rows={2}
          />
          <button type="button" onClick={() => void openExisting()} disabled={busy || !sourceText.trim()}>Open</button>
          <button className="primary-button" type="button" onClick={() => void translate()} disabled={busy || !sourceText.trim()}>
            {busy ? 'Working…' : 'Translate'}
          </button>
        </div>
        <output className="status-message" aria-live="polite">{message}</output>
      </header>

      <nav className="tabs" aria-label="Open translated files" role="tablist">
        {tabs.map((tab, index) => (
          <div className={`tab${tab.document.id === activeId ? ' is-active' : ''}`} key={tab.document.id}>
            <button
              type="button"
              role="tab"
              aria-selected={tab.document.id === activeId}
              aria-controls="script-workspace"
              tabIndex={tab.document.id === activeId ? 0 : -1}
              title={tab.document.output_path}
              onClick={() => setActiveId(tab.document.id)}
              onKeyDown={(event) => handleTabKeys(event, tabs, index, setActiveId)}
            >
              <span className="tab-name">{baseName(tab.document.output_path || tab.document.source_path)}</span>
              <span className={`tab-state tab-state--${tab.status}`} aria-hidden="true" />
              <span className="sr-only">{tab.status}</span>
            </button>
            <button
              className="tab-close"
              type="button"
              tabIndex={tab.document.id === activeId ? 0 : -1}
              onClick={() => closeTab(tab.document.id)}
              aria-label={`Close ${baseName(tab.document.output_path)}`}
            >×</button>
          </div>
        ))}
      </nav>

      <section className="workspace" id="script-workspace" role="tabpanel" aria-label="Translated script editor">
        <section className="editor-pane" aria-label="Script editor">
          <div className="editor-toolbar">
            <label className="search-field">
              <span className="sr-only">Search operations</span>
              <input
                value={search}
                onChange={(event) => setSearch(event.target.value)}
                type="search"
                placeholder="Search operations…"
                disabled={!active}
              />
            </label>
            <div className="toolbar-group tree-controls" aria-label="Tree controls">
              <button type="button" onClick={() => setAllScopes(true)} disabled={!active?.document.scopes.length}>Expand all</button>
              <button type="button" onClick={() => setAllScopes(false)} disabled={!active?.document.scopes.length}>Collapse all</button>
            </div>
            <button className="context-toggle" type="button" onClick={() => setContextOpen(true)} disabled={!active} aria-expanded={contextOpen} aria-controls="file-context">
              File context
            </button>
          </div>

          {active && (
            <div className="change-toolbar" aria-label="Edit actions">
              <div className="change-status">
                <strong>{editCount ? `${editCount} unsaved change${editCount === 1 ? '' : 's'}` : 'No pending changes'}</strong>
                <small>{statusCopy(active.status)}</small>
              </div>
              <div className="toolbar-group">
                <button type="button" onClick={() => updateActive((tab) => withEdits(tab, undo(tab.edits)))} disabled={!active.edits.history.length}>Undo</button>
                <button type="button" onClick={() => updateActive((tab) => withEdits(tab, redo(tab.edits)))} disabled={!active.edits.future.length}>Redo</button>
                <button type="button" onClick={() => void validateEdits()} disabled={!editCount || active.status === 'validating'}>Preview changes</button>
                <button className="primary-button" type="button" onClick={() => void persistEdits()} disabled={!active.preview?.valid || active.status === 'saving'}>Apply changes</button>
                {active.status === 'conflict' && <button type="button" onClick={() => void reloadActive()}>Reload</button>}
              </div>
            </div>
          )}

          <div className="editor-scroll">
            {active ? (
              <ScriptTree
                document={active.document}
                search={search}
                expandedScopes={active.expandedScopeIds}
                selectedId={active.selectedId}
                values={active.edits.values}
                onSelect={selectItem}
                onToggleScope={toggleScope}
                onEdit={editParameter}
              />
            ) : (
              <div className="empty-state">
                <strong>No translated file open</strong>
                <span>Open or translate a VG2 source file to begin.</span>
              </div>
            )}

            {active?.preview && <ChangePreview preview={active.preview} />}

            <details className="diagnostics" open={diagnostics.some((item) => item.level === 'error')}>
              <summary>Diagnostics <span>{diagnostics.length}</span></summary>
              <div>
                {diagnostics.length ? diagnostics.map((item, index) => (
                  <p key={`${item.code}-${index}`} className={`diagnostic diagnostic--${item.level}`}>
                    <strong>{item.code}</strong> {item.message} {item.location && <small>{item.location}</small>}
                  </p>
                )) : <p className="empty-copy">No diagnostics.</p>}
              </div>
            </details>
          </div>
        </section>

        <button
          className={`context-backdrop${contextOpen ? ' is-open' : ''}`}
          type="button"
          aria-label="Close file context"
          tabIndex={contextOpen ? 0 : -1}
          onClick={() => setContextOpen(false)}
        />
        <ContextSidebar
          document={active?.document ?? null}
          documents={documents}
          headerCache={headerCache}
          values={active?.edits.values ?? {}}
          csv={active?.csv ?? null}
          open={contextOpen}
          onClose={() => setContextOpen(false)}
          onPreviewCsv={(path) => void loadCsv(path)}
          onActivateDocument={(id) => {
            setActiveId(id)
            setContextOpen(false)
          }}
        />
      </section>
    </main>
  )
}

function ChangePreview({ preview }: { preview: CommandPreview }) {
  return (
    <details className={`change-preview${preview.valid ? ' is-valid' : ' is-invalid'}`} open={!preview.valid}>
      <summary>{preview.valid ? 'Validated Python diff' : 'Changes need attention'}</summary>
      <div>
        {preview.issues.map((issue) => (
          <p className="validation-error" key={`${issue.code}-${issue.message}`}>{issue.message}</p>
        ))}
        <pre>{preview.diff || 'No textual change.'}</pre>
      </div>
    </details>
  )
}

function mergeDocuments(current: TabState[], documents: WorkflowDocument[]): TabState[] {
  const byId = new Map(current.map((tab) => [tab.document.id, tab]))
  for (const document of documents) byId.set(document.id, createTab(document, byId.get(document.id)))
  return [...byId.values()]
}

function parseSourcePaths(sourceText: string): string[] {
  return sourceText.split(/\r?\n/).map((item) => item.trim()).filter(Boolean)
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
  setActiveId: (id: string) => void,
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
  setActiveId(next.document.id)
  const tabButtons = document.querySelectorAll<HTMLButtonElement>('.tab > button[role="tab"]')
  tabButtons[nextIndex]?.focus()
}
