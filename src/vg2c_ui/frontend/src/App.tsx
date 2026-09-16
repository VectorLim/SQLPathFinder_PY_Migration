import { useEffect, useState } from 'react'

import { workspaceDownloadUrl } from './api'
import { ChangeToolbar } from './ChangeToolbar'
import { CommandPalette } from './CommandPalette'
import { buildCommands, executeCommand } from './commands'
import { ContextSidebar } from './ContextSidebar'
import type { ChangePreviewView, DiagnosticView, ParameterView } from './contracts.generated'
import { DirtyCloseDialog } from './DirtyCloseDialog'
import { FileTabs, fileTabId } from './FileTabs'
import { baseName } from './operationLabels'
import { ancestorScopeIds, ScriptTree } from './ScriptTree'
import { SourceIntake } from './SourceIntake'
import { ThemeSelector } from './ThemeSelector'
import { useSourceIntake } from './useSourceIntake'
import { useTheme } from './theme'
import { useWorkspace } from './useWorkspace'
import { useWorkspaceFiles } from './useWorkspaceFiles'
import { hasUnsavedChanges } from './workspaceGuards'

export function App() {
  const workspace = useWorkspace()
  const fileInventory = useWorkspaceFiles()
  const theme = useTheme()
  const { state, active, dispatch } = workspace
  const [search, setSearch] = useState('')
  const [batchDiagnostics, setBatchDiagnostics] = useState<DiagnosticView[]>([])
  const [contextOpen, setContextOpen] = useState(false)
  const [commandOpen, setCommandOpen] = useState(false)
  const [pendingCloseId, setPendingCloseId] = useState<string | null>(null)
  const intake = useSourceIntake({
    files: fileInventory.files,
    loadingFiles: fileInventory.loading,
    inventoryError: fileInventory.error,
    policy: fileInventory.policy,
    refreshFiles: fileInventory.refresh,
    translateSources: workspace.translate,
    onDiagnostics: setBatchDiagnostics,
  })

  const hasUnsavedWorkspaceChanges = state.tabs.some(hasUnsavedChanges)
  useEffect(() => {
    if (!hasUnsavedWorkspaceChanges) return
    function beforeUnload(event: BeforeUnloadEvent) {
      event.preventDefault()
      event.returnValue = ''
    }
    window.addEventListener('beforeunload', beforeUnload)
    return () => window.removeEventListener('beforeunload', beforeUnload)
  }, [hasUnsavedWorkspaceChanges])

  function selectItem(id: string) {
    if (!active) return
    dispatch({ type: 'select', tabId: active.document.id, itemId: id })
    for (const scopeId of ancestorScopeIds(active.document, id)) dispatch({ type: 'toggle-scope', tabId: active.document.id, scopeId, expanded: true })
  }

  function requestCloseTab(id: string) {
    const tab = state.tabs.find((candidate) => candidate.document.id === id)
    if (!tab) return
    if (hasUnsavedChanges(tab)) {
      setPendingCloseId(id)
      return
    }
    dispatch({ type: 'close', tabId: id })
  }

  function discardAndCloseTab(id: string) {
    dispatch({ type: 'close', tabId: id })
    setPendingCloseId(null)
    requestAnimationFrame(() => document.querySelector<HTMLButtonElement>('.tabs [role="tab"][tabindex="0"]')?.focus())
  }

  function validateActive() {
    if (active) void workspace.validate(active.document.id).catch(() => undefined)
  }

  function applyActive() {
    if (active) void workspace.apply(active.document.id).catch(() => undefined)
  }

  function reloadActive() {
    if (active) void workspace.reload(active.document.id).catch(() => undefined)
  }

  const commands = buildCommands({
    tabs: state.tabs,
    active,
    workspace: {
      canStage: intake.canStage,
      queuedCount: intake.canUploadQueued ? intake.queuedCount : 0,
      canTranslate: intake.canTranslate,
      openFiles: intake.openFiles,
      openFolder: intake.openFolder,
      uploadQueued: () => { void intake.uploadQueued() },
      translateSelected: () => { void intake.translateSelected() },
    },
    actions: {
      activateDocument: (id) => dispatch({ type: 'activate', tabId: id }),
      selectItem,
      openInspector: () => setContextOpen(true),
      undo: () => active && dispatch({ type: 'undo', tabId: active.document.id }),
      redo: () => active && dispatch({ type: 'redo', tabId: active.document.id }),
      validate: validateActive,
      apply: applyActive,
      reload: reloadActive,
      expandAll: () => active && dispatch({ type: 'set-all-scopes', tabId: active.document.id, expanded: true }),
      collapseAll: () => active && dispatch({ type: 'set-all-scopes', tabId: active.document.id, expanded: false }),
    },
  })

  useEffect(() => {
    function shortcut(event: KeyboardEvent) {
      if ((event.target as HTMLElement | null)?.closest('dialog[open]')) return
      if ((event.ctrlKey || event.metaKey) && event.key.toLowerCase() === 'k') {
        event.preventDefault()
        setCommandOpen(true)
        return
      }
      if (!(event.ctrlKey || event.metaKey) || !active) return
      const key = event.key.toLowerCase()
      if (key === 'z') {
        event.preventDefault()
        executeCommand(commands, event.shiftKey ? 'editing.redo' : 'editing.undo')
      } else if (key === 'y') {
        event.preventDefault()
        executeCommand(commands, 'editing.redo')
      } else if (key === 's') {
        event.preventDefault()
        if (!executeCommand(commands, 'editing.apply')) executeCommand(commands, 'editing.preview')
      }
    }
    window.addEventListener('keydown', shortcut)
    return () => window.removeEventListener('keydown', shortcut)
  })

  const diagnostics = active ? [...active.document.diagnostics, ...batchDiagnostics] : batchDiagnostics
  const documents = state.tabs.map((tab) => tab.document)
  const generatedFiles = fileInventory.files.filter((file) => file.role === 'generated')
  const pendingCloseTab = state.tabs.find((tab) => tab.document.id === pendingCloseId) ?? null

  return <main className="app-shell app-shell--with-intake">
    <header className="topbar">
      <div className="brand"><span>PYTHON</span>PathFinder</div>
      <ThemeSelector preference={theme.preference} onChange={theme.setPreference} />
      <div className="workspace-downloads">{generatedFiles.map((file) => <a key={file.path} href={workspaceDownloadUrl(file.path)} download>{baseName(file.path)}</a>)}<a href="/api/workspace/archive" download>Download workspace ZIP</a></div>
    </header>

    <SourceIntake intake={intake} />

    <FileTabs
      tabs={state.tabs}
      activeId={state.activeId}
      onActivate={(id) => dispatch({ type: 'activate', tabId: id })}
      onClose={requestCloseTab}
    />

    <section className="workspace" id="script-workspace" role="tabpanel" aria-label={active ? undefined : 'Translated script editor'} aria-labelledby={active ? fileTabId(active.document.id) : undefined}><section className="editor-pane"><div className="editor-toolbar"><label className="search-field"><span className="sr-only">Search operations</span><input value={search} onChange={(event) => setSearch(event.target.value)} type="search" placeholder="Search operations…" disabled={!active} /></label><div className="toolbar-group tree-controls"><button type="button" onClick={() => active && dispatch({ type: 'set-all-scopes', tabId: active.document.id, expanded: true })} disabled={!active?.document.scopes.length}>Expand all</button><button type="button" onClick={() => active && dispatch({ type: 'set-all-scopes', tabId: active.document.id, expanded: false })} disabled={!active?.document.scopes.length}>Collapse all</button></div><button className="command-trigger" type="button" onClick={() => setCommandOpen(true)} aria-keyshortcuts="Control+K Meta+K">Commands <kbd>Ctrl/⌘ K</kbd></button><button className="context-toggle" type="button" onClick={() => setContextOpen(true)} disabled={!active} aria-expanded={contextOpen} aria-controls="file-context"><span className="context-toggle__icon" aria-hidden="true">☷</span><span className="context-toggle__label">File context</span></button></div>

      {active && <ChangeToolbar
        tab={active}
        onUndo={() => executeCommand(commands, 'editing.undo')}
        onRedo={() => executeCommand(commands, 'editing.redo')}
        onValidate={() => executeCommand(commands, 'editing.preview')}
        onApply={() => executeCommand(commands, 'editing.apply')}
        onReload={() => executeCommand(commands, 'editing.reload')}
      />}

      <div className="editor-scroll">{active ? <ScriptTree tabId={active.document.id} document={active.document} projection={state.projection} search={search} expandedScopes={active.expandedScopeIds} selectedId={active.selectedId} values={active.edits.values} onSelect={selectItem} onToggleScope={(id, expanded) => dispatch({ type: 'toggle-scope', tabId: active.document.id, scopeId: id, expanded })} onEdit={(parameter: ParameterView, value) => workspace.edit(active.document.id, parameter, value)} inspectSql={workspace.inspectStructuredSql} runSqlAction={workspace.runSqlAction} /> : <div className="empty-state"><strong>No translated file open</strong><span>Upload and translate a VG2 source file to begin.</span></div>}
        {active?.preview && <ChangePreview preview={active.preview} />}
        <details className="diagnostics" open={diagnostics.some((item) => item.level === 'error')}><summary>Diagnostics <span>{diagnostics.length}</span></summary><div>{diagnostics.length ? diagnostics.map((item, index) => <p key={`${item.code}-${index}`} className={`diagnostic diagnostic--${item.level}`}><strong>{item.code}</strong> {item.message} {item.location && <small>{item.location}</small>}</p>) : <p className="empty-copy">No diagnostics.</p>}</div></details>
      </div>
    </section>

    <ContextSidebar document={active?.document ?? null} documents={documents} projection={state.projection} csv={active?.csv ?? null} csvArtifactPath={active?.csvArtifactPath ?? null} csvError={active?.csvError ?? null} open={contextOpen} onClose={() => setContextOpen(false)} onPreviewCsv={(path) => active && void workspace.loadCsv(active.document.id, path).catch(() => undefined)} onActivateDocument={(id) => { dispatch({ type: 'activate', tabId: id }); setContextOpen(false) }} />
    </section>

    <CommandPalette open={commandOpen} commands={commands} onClose={() => setCommandOpen(false)} />
    <DirtyCloseDialog tab={pendingCloseTab} onCancel={() => setPendingCloseId(null)} onDiscard={discardAndCloseTab} />
  </main>
}

function ChangePreview({ preview }: { preview: ChangePreviewView }) { return <details className={`change-preview${preview.valid ? ' is-valid' : ' is-invalid'}`} open={!preview.valid}><summary>{preview.valid ? 'Validated Python diff' : 'Changes need attention'}</summary><div>{preview.issues.map((issue) => <p className="validation-error" key={`${issue.code}-${issue.message}`}>{issue.message}</p>)}<pre>{preview.diff || 'No textual change.'}</pre></div></details> }
