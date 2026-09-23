import { useEffect, useState } from 'react'
import { ChevronsDownUp, ChevronsUpDown, Command, GitBranch, ListTree, Settings2 } from 'lucide-react'

import { uploadWorkspaceFile } from './api/client'
import { ChangeToolbar } from './ChangeToolbar'
import { CommandPalette } from './CommandPalette'
import { buildCommands, executeCommand } from './commands'
import type { ChangePreviewView, DiagnosticView } from './api/contracts.generated'
import { DirtyCloseDialog } from './DirtyCloseDialog'
import { FileTabs, fileTabId } from './FileTabs'
import { SemanticScriptTree } from './SemanticScriptTree'
import { SemanticOperationEditor } from './SemanticOperationEditor'
import type { SemanticEditorSession } from './semanticEditorSession'
import { ContextPane } from './ContextPane'
import { SourceIntake } from './SourceIntake'
import { ThemeSelector } from './ThemeSelector'
import { useSourceIntake } from './useSourceIntake'
import { useTheme } from './theme'
import { useWorkspace } from './workspace/useWorkspace'
import { useWorkspaceFiles } from './workspace/useWorkspaceFiles'
import { hasUnsavedChanges } from './workspace/guards'
import { WorkbenchLayout } from './WorkbenchLayout'

export function App() {
  const workspace = useWorkspace()
  const fileInventory = useWorkspaceFiles()
  const theme = useTheme()
  const { state, active, dispatch } = workspace
  const [search, setSearch] = useState('')
  const [pane, setPane] = useState<'logic' | 'config' | 'context'>('logic')
  const [commandOpen, setCommandOpen] = useState(false)
  const [pendingCloseId, setPendingCloseId] = useState<string | null>(null)
  const intake = useSourceIntake({
    files: fileInventory.files,
    loadingFiles: fileInventory.loading,
    inventoryError: fileInventory.inventoryError,
    policy: fileInventory.policy,
    policyError: fileInventory.policyError,
    refreshFiles: fileInventory.refresh,
    translateSources: workspace.translate,
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

  const activeDocumentId = active?.document.id ?? null
  useEffect(() => {
    if (activeDocumentId) return
    setPane('logic')
    setSearch('')
  }, [activeDocumentId])

  function selectItem(id: string) {
    if (!active) return
    dispatch({ type: 'select', tabId: active.document.id, itemId: id })
  }

  function navigateOperation(documentId: string, operationId: string, focus: boolean) {
    dispatch({ type: 'navigate-operation', tabId: documentId, operationId, focus })
    setPane(focus ? 'logic' : 'config')
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

  function saveActive() {
    if (active) void workspace.save(active.document.id).catch(() => undefined)
  }

  function generateActive() {
    if (active) void workspace.generate(active.document.id).catch(() => undefined)
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
      openInspector: () => setPane('context'),
      undo: () => active && dispatch({ type: 'undo', tabId: active.document.id }),
      redo: () => active && dispatch({ type: 'redo', tabId: active.document.id }),
      validate: validateActive,
      save: saveActive,
      generate: generateActive,
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
        executeCommand(commands, 'editing.save')
      }
    }
    window.addEventListener('keydown', shortcut)
    return () => window.removeEventListener('keydown', shortcut)
  })

  const selectedOperation = active?.document.semantic_operations.find((operation) => operation.id === active.selectedId)
  const pendingCloseTab = state.tabs.find((tab) => tab.document.id === pendingCloseId) ?? null
  const hasTabs = state.tabs.length > 0
  const editorSession = active ? {
    values: active.edits.values,
    saving: active.status === 'saving',
    readOnly: Boolean(active.document.read_only_reason),
    resources: {
      symbols: active.document.symbols,
      conditionOperators: active.document.condition_operators,
    },
    drafts: {
      values: active.fieldDrafts,
      update: (key, draft) => dispatch({ type: 'field-draft', tabId: active.document.id, key, draft }),
    },
    actions: {
      uploadFile: async (file) => {
        const saved = await uploadWorkspaceFile(file)
        await fileInventory.refresh()
        await workspace.refreshFileChoices(active.document.id)
        return saved.path
      },
      edit: ({ binding, value, clearDraftPaths }) => workspace.edit(active.document.id, binding, value, clearDraftPaths),
      validateBinding: (bindingId, value) => workspace.validateCandidate(active.document.id, bindingId, value),
      previewHtml: (operationId) => workspace.previewHtmlOperation(active.document.id, operationId),
      inspectSql: (bindingId) => workspace.inspectStructuredSql(active.document.id, bindingId),
      runSqlCommand: (bindingId, command) => workspace.runSqlCommand(active.document.id, bindingId, command),
    },
  } satisfies SemanticEditorSession : null

  return <main className={`app-shell app-shell--with-intake${hasTabs ? '' : ' app-shell--no-tabs'}`}>
    <header className="topbar">
      <div className="brand"><span>SQL</span>PathFinder</div>
      <ThemeSelector preference={theme.preference} onChange={theme.setPreference} />
      <div className="workspace-downloads"><a href="/api/workspace/archive" download>Download workspace ZIP</a></div>
    </header>

    <SourceIntake intake={intake} hasOpenDocument={Boolean(active)} />

    {hasTabs && <FileTabs
      tabs={state.tabs}
      activeId={state.activeId}
      onActivate={(id) => dispatch({ type: 'activate', tabId: id })}
      onClose={requestCloseTab}
    />}

    {active ? <section className={`workspace workbench workbench--${pane}`} id="script-workspace" role="tabpanel" aria-labelledby={fileTabId(active.document.id)}>
      <div className="workbench-actions">
        <div className="editor-toolbar"><label className="search-field"><span className="sr-only">Search operations</span><input value={search} onChange={(event) => setSearch(event.target.value)} type="search" placeholder="Search operations" /></label><div className="toolbar-group tree-controls"><button className="icon-button" type="button" aria-label="Expand all scopes" title="Expand all scopes" onClick={() => dispatch({ type: 'set-all-scopes', tabId: active.document.id, expanded: true })} disabled={!active.document.semantic_operations.some((operation) => active.document.semantic_operations.some((child) => child.parent_operation_id === operation.id))}><ChevronsUpDown size={16} /></button><button className="icon-button" type="button" aria-label="Collapse all scopes" title="Collapse all scopes" onClick={() => dispatch({ type: 'set-all-scopes', tabId: active.document.id, expanded: false })} disabled={!active.document.semantic_operations.some((operation) => active.document.semantic_operations.some((child) => child.parent_operation_id === operation.id))}><ChevronsDownUp size={16} /></button></div><button className="icon-button" aria-label="Open commands" title="Open commands" type="button" onClick={() => setCommandOpen(true)} aria-keyshortcuts="Control+K Meta+K"><Command size={16} /></button></div>

        <ChangeToolbar
          tab={active}
          onUndo={() => executeCommand(commands, 'editing.undo')}
          onRedo={() => executeCommand(commands, 'editing.redo')}
          onValidate={() => executeCommand(commands, 'editing.preview')}
          onSave={() => executeCommand(commands, 'editing.save')}
          onGenerate={() => executeCommand(commands, 'editing.generate')}
          onReload={() => executeCommand(commands, 'editing.reload')}
        />

      </div>
      <WorkbenchLayout
        activePane={pane}
        onActivePaneChange={setPane}
        logic={<section id="pane-logic" className="workbench-pane logic-pane" aria-label="Script Logic"><header className="pane-heading"><ListTree size={17} aria-hidden="true" /><h2>Script Logic</h2><span>{active.document.semantic_operations.length}</span></header><div className="pane-scroll"><SemanticScriptTree document={active.document} search={search} expandedIds={active.expandedScopeIds} selectedId={active.selectedId} revealVersion={active.revealVersion} revealFocus={active.revealFocus} onSelect={selectItem} onToggle={(id, expanded) => dispatch({ type: 'toggle-scope', tabId: active.document.id, scopeId: id, expanded })} onReorder={(source, target) => { void workspace.reorder(active.document.id, source, target) }} reorderDisabled={Boolean(active.document.read_only_reason) || active.status === 'saving' || active.status === 'generating' || Object.keys(active.edits.values).length > 0 || Object.keys(active.fieldDrafts).length > 0} /><DocumentDiagnostics diagnostics={active.document.diagnostics} /></div></section>}
        configuration={<section id="pane-config" className="workbench-pane configuration-pane" aria-label="Configuration">
          <header className="pane-heading"><Settings2 size={17} aria-hidden="true" /><h2>Configuration</h2></header>
          <div className="pane-scroll">
            {selectedOperation && editorSession ? <SemanticOperationEditor
              key={`${active.instanceId}-${selectedOperation.id}`}
              operation={selectedOperation}
              session={editorSession}
            /> : <p className="pane-empty">Select an operation to configure it.</p>}
            {active.preview && <ChangePreview preview={active.preview} />}
          </div>
        </section>}
        context={<section id="pane-context" className="workbench-pane context-workbench-pane" aria-label="Context"><header className="pane-heading"><GitBranch size={17} aria-hidden="true" /><h2>Context</h2></header><div className="pane-scroll"><ContextPane
          document={active.document}
          values={active.edits.values}
          onNavigate={(operationId, focus) => navigateOperation(active.document.id, operationId, focus)}
          onEdit={(binding, value) => workspace.edit(active.document.id, binding, value)}
        /></div></section>}
      />
    </section> : <section className="empty-state" id="script-workspace" aria-label="Translated script editor"><strong>No translated file open</strong><span>Upload and translate a VG2 source file to begin.</span></section>}

    <CommandPalette open={commandOpen} commands={commands} onClose={() => setCommandOpen(false)} />
    <DirtyCloseDialog tab={pendingCloseTab} onCancel={() => setPendingCloseId(null)} onDiscard={discardAndCloseTab} />
  </main>
}

function DocumentDiagnostics({ diagnostics }: { diagnostics: DiagnosticView[] }) {
  return <details className="diagnostics" open={diagnostics.some((item) => item.level === 'error')}><summary>Diagnostics <span>{diagnostics.length}</span></summary><div>{diagnostics.length ? diagnostics.map((item, index) => <p key={`${item.code}-${index}`} className={`diagnostic diagnostic--${item.level}`}><strong>{item.code}</strong> {item.message} {item.location && <small>{item.location}</small>}</p>) : <p className="empty-copy">No diagnostics.</p>}</div></details>
}

function ChangePreview({ preview }: { preview: ChangePreviewView }) { return <details className={`change-preview${preview.valid ? ' is-valid' : ' is-invalid'}`} open={!preview.valid}><summary>{preview.valid ? 'Changes validated' : 'Changes need attention'}</summary><div>{preview.issues.map((issue) => <p className="validation-error" key={`${issue.code}-${issue.message}`}>{issue.message}</p>)}{preview.valid && <p className="empty-copy">The current draft is valid and ready to save.</p>}</div></details> }

