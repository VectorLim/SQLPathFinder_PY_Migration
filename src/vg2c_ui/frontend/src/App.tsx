import { useEffect, useMemo, useState } from 'react'
import {
  Background,
  BackgroundVariant,
  Controls,
  MiniMap,
  ReactFlow,
  ReactFlowProvider,
  applyNodeChanges,
  type Edge,
  type NodeChange,
  type ReactFlowInstance,
  type Viewport,
} from '@xyflow/react'

import {
  ApiError,
  applyCommands,
  openWorkflow,
  previewCommands,
  previewCsv,
  saveLayout,
  translateBatch,
} from './api'
import { editValue, emptyEdits, redo, undo, type EditState } from './editState'
import {
  ancestorScopeIds,
  highlightRelated,
  labelFor,
  projectGraph,
  toGraph,
  type FlowNode,
  type ScopeSummary,
} from './graph'
import { Inspector } from './Inspector'
import type {
  CommandBatch,
  CommandPreview,
  CsvPreview,
  Diagnostic,
  ParameterDescriptor,
  WorkflowDocument,
  WorkflowNode as WorkflowNodeModel,
} from './types'
import { WorkflowEdge } from './WorkflowEdge'
import { WorkflowNavigator } from './WorkflowNavigator'
import { WorkflowNode } from './WorkflowNode'

type TabStatus = 'ready' | 'dirty' | 'validating' | 'valid' | 'invalid' | 'saving' | 'saved' | 'conflict' | 'error'

interface TabState {
  document: WorkflowDocument
  nodes: FlowNode[]
  edges: Edge[]
  viewport: Viewport
  selectedId: string | null
  status: TabStatus
  edits: EditState
  preview: CommandPreview | null
  csv: CsvPreview | null
  expandedScopeIds: Set<string>
}

const nodeTypes = { workflow: WorkflowNode }
const edgeTypes = { workflow: WorkflowEdge }

function createTab(document: WorkflowDocument, previous?: TabState): TabState {
  const graph = toGraph(document)
  const positions = new Map(previous?.nodes.map((node) => [node.id, node.position]))
  return {
    document,
    nodes: graph.nodes.map((node) => ({ ...node, position: positions.get(node.id) ?? node.position })),
    edges: graph.edges,
    viewport: previous?.viewport ?? document.layout.viewport,
    selectedId: previous?.selectedId ?? null,
    status: 'ready',
    edits: emptyEdits(),
    preview: null,
    csv: null,
    expandedScopeIds: previous?.expandedScopeIds ?? new Set(),
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

function Editor() {
  const [tabs, setTabs] = useState<TabState[]>([])
  const [activeId, setActiveId] = useState<string | null>(null)
  const [sourceText, setSourceText] = useState('')
  const [search, setSearch] = useState('')
  const [busy, setBusy] = useState(false)
  const [message, setMessage] = useState('Enter one VG2 source path per line.')
  const [batchDiagnostics, setBatchDiagnostics] = useState<Diagnostic[]>([])
  const [flow, setFlow] = useState<ReactFlowInstance<FlowNode, Edge> | null>(null)

  const active = tabs.find((tab) => tab.document.id === activeId) ?? null
  const graph = useMemo(() => {
    if (!active) return { nodes: [], edges: [] }
    const projected = projectGraph(
      active.document,
      active.nodes,
      active.edges,
      active.expandedScopeIds,
      active.selectedId,
    )
    const highlighted = highlightRelated(projected.nodes, projected.edges, active.selectedId)
    const query = search.trim().toLocaleLowerCase()
    return {
      nodes: highlighted.nodes.map((node) => ({
        ...node,
        hidden: Boolean(query) && !labelFor(node.data.item).toLocaleLowerCase().includes(query),
      })),
      edges: highlighted.edges,
    }
  }, [active, search])
  const selectedNode = graph.nodes.find((node) => node.id === active?.selectedId)
  const selected = selectedNode?.data.item ?? (active
    ? [...active.document.steps, ...active.document.scopes, ...active.document.artifacts]
        .find((item) => item.id === active.selectedId) ?? null
    : null)
  const selectedSummary = selectedNode?.data.summary as ScopeSummary | undefined

  function updateActive(update: (tab: TabState) => TabState) {
    setTabs((current) => current.map((tab) => tab.document.id === activeId ? update(tab) : tab))
  }

  function replaceDocument(document: WorkflowDocument) {
    setTabs((current) => current.map((tab) => (
      tab.document.id === document.id ? createTab(document, tab) : tab
    )))
  }

  async function translate(paths?: string[]) {
    const sourcePaths = paths ?? sourceText.split(/\r?\n/).map((item) => item.trim()).filter(Boolean)
    if (!sourcePaths.length) return
    setBusy(true)
    setMessage('Translating…')
    try {
      const response = await translateBatch(sourcePaths)
      setBatchDiagnostics(response.diagnostics)
      setTabs((current) => {
        const byId = new Map(current.map((tab) => [tab.document.id, tab]))
        for (const document of response.documents) byId.set(document.id, createTab(document, byId.get(document.id)))
        return [...byId.values()]
      })
      if (response.documents.length) {
        setActiveId(response.documents[0].id)
        setMessage(`${response.documents.length} workflow${response.documents.length === 1 ? '' : 's'} ready`)
      } else setMessage('No workflows were translated.')
    } catch (error) {
      setMessage(error instanceof Error ? error.message : 'Translation failed')
    } finally {
      setBusy(false)
    }
  }

  async function openExisting() {
    const sourcePaths = sourceText.split(/\r?\n/).map((item) => item.trim()).filter(Boolean)
    if (!sourcePaths.length) return
    setBusy(true)
    setMessage('Opening generated workflows…')
    try {
      const documents = await Promise.all(sourcePaths.map((path) => openWorkflow(path)))
      setTabs((current) => {
        const byId = new Map(current.map((tab) => [tab.document.id, tab]))
        for (const document of documents) {
          byId.set(document.id, createTab(document, byId.get(document.id)))
        }
        return [...byId.values()]
      })
      setActiveId(documents[0]?.id ?? null)
      setMessage(`${documents.length} saved workflow${documents.length === 1 ? '' : 's'} opened`)
    } catch (error) {
      setMessage(error instanceof Error ? error.message : 'Open failed')
    } finally {
      setBusy(false)
    }
  }

  function editParameter(parameter: ParameterDescriptor, value: unknown) {
    updateActive((tab) => ({
      ...tab,
      edits: editValue(tab.edits, parameter.id, value),
      preview: null,
      status: 'dirty',
    }))
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
    } catch (error) {
      setMessage(error instanceof Error ? error.message : 'CSV preview failed')
    }
  }

  async function reloadActive() {
    if (!active) return
    try {
      const document = await openWorkflow(
        active.document.source_path,
        active.document.output_path,
      )
      replaceDocument(document)
      setMessage('Reloaded the externally changed document; local edits were cleared.')
    } catch (error) {
      setMessage(error instanceof Error ? error.message : 'Reload failed')
    }
  }

  async function persistLayout() {
    if (!active) return
    updateActive((tab) => ({ ...tab, status: 'saving' }))
    const positions = Object.fromEntries(active.nodes.map((node) => [node.id, node.position]))
    try {
      await saveLayout(active.document, positions, active.viewport)
      updateActive((tab) => ({
        ...tab,
        status: Object.keys(tab.edits.values).length ? 'dirty' : 'saved',
      }))
    } catch (error) {
      handleMutationError(error)
    }
  }

  useEffect(() => {
    function shortcut(event: KeyboardEvent) {
      if (!(event.ctrlKey || event.metaKey)) return
      if (event.key.toLowerCase() === 'z') {
        event.preventDefault()
        updateActive((tab) => withEdits(tab, event.shiftKey ? redo(tab.edits) : undo(tab.edits)))
      } else if (event.key.toLowerCase() === 'y') {
        event.preventDefault()
        updateActive((tab) => withEdits(tab, redo(tab.edits)))
      } else if (event.key.toLowerCase() === 's') {
        event.preventDefault()
        if (active?.preview?.valid) void persistEdits()
        else void validateEdits()
      }
    }
    window.addEventListener('keydown', shortcut)
    return () => window.removeEventListener('keydown', shortcut)
  })

  function selectNode(id: string) {
    updateActive((tab) => ({
      ...tab,
      selectedId: id,
      csv: null,
      expandedScopeIds: new Set([
        ...tab.expandedScopeIds,
        ...ancestorScopeIds(tab.document, id),
      ]),
    }))
    window.setTimeout(
      () => flow?.fitView({ nodes: [{ id }], duration: 220, padding: 0.8 }),
      0,
    )
  }

  function toggleScope(id: string, expanded?: boolean) {
    updateActive((tab) => {
      const next = new Set(tab.expandedScopeIds)
      const shouldExpand = expanded ?? !next.has(id)
      if (shouldExpand) next.add(id)
      else next.delete(id)
      return { ...tab, selectedId: id, csv: null, expandedScopeIds: next }
    })
    window.setTimeout(() => flow?.fitView({ duration: 220, padding: 0.22 }), 0)
  }

  function closeTab(id: string) {
    setTabs((current) => {
      const remaining = current.filter((tab) => tab.document.id !== id)
      if (id === activeId) setActiveId(remaining[0]?.document.id ?? null)
      return remaining
    })
  }

  const diagnostics = active ? [...active.document.diagnostics, ...batchDiagnostics] : batchDiagnostics

  return (
    <main className="app-shell">
      <header className="topbar">
        <div className="brand"><span>VG2</span> Visual Editor</div>
        <div className="translate-box">
          <textarea aria-label="VG2 source paths" value={sourceText} onChange={(event) => setSourceText(event.target.value)} placeholder={'workflows/report.txt\nworkflows/export.txt'} rows={2} />
          <button type="button" onClick={() => void openExisting()} disabled={busy || !sourceText.trim()}>Open</button>
          <button type="button" onClick={() => void translate()} disabled={busy || !sourceText.trim()}>{busy ? 'Working…' : 'Translate'}</button>
        </div>
        <output className="status-message" aria-live="polite">{message}</output>
      </header>

      <nav className="tabs" aria-label="Open workflows" role="tablist">
        {tabs.map((tab) => (
          <div className={`tab${tab.document.id === activeId ? ' is-active' : ''}`} key={tab.document.id}>
            <button type="button" role="tab" aria-selected={tab.document.id === activeId} onClick={() => setActiveId(tab.document.id)}>
              {tab.document.source_path.split(/[\\/]/).at(-1)}
              <span className={`tab-state tab-state--${tab.status}`} aria-hidden="true" />
              <span className="sr-only">{tab.status}</span>
            </button>
            <button className="tab-close" type="button" onClick={() => closeTab(tab.document.id)} aria-label="Close tab">×</button>
          </div>
        ))}
      </nav>

      <section className="workspace">
        <aside className="navigator" aria-label="Workflow navigator">
          <label>Search workflow<input value={search} onChange={(event) => setSearch(event.target.value)} type="search" /></label>
          {active ? <WorkflowNavigator
            document={active.document}
            search={search}
            expandedScopes={active.expandedScopeIds}
            onSelect={selectNode}
            onToggleScope={toggleScope}
          /> : <p className="empty-copy">Translated workflows appear here.</p>}
        </aside>

        <section className="canvas" aria-label="Workflow canvas">
          {active ? <ReactFlow<FlowNode, Edge>
            key={active.document.id}
            nodes={graph.nodes}
            edges={graph.edges}
            nodeTypes={nodeTypes}
            edgeTypes={edgeTypes}
            defaultViewport={active.viewport}
            onInit={setFlow}
            onNodesChange={(changes: NodeChange<FlowNode>[]) => updateActive((tab) => ({ ...tab, nodes: applyNodeChanges(changes, tab.nodes) }))}
            onNodeClick={(_, node) => {
              if (node.data.summary) toggleScope(node.id)
              else selectNode(node.id)
            }}
            onMoveEnd={(_, viewport) => updateActive((tab) => ({ ...tab, viewport }))}
            minZoom={0.2}
            maxZoom={2}
            fitView
          >
            <Background variant={BackgroundVariant.Dots} gap={20} size={1} />
            <Controls position="bottom-left" />
            <MiniMap pannable zoomable aria-label="Workflow minimap" />
          </ReactFlow> : <div className="empty-state"><strong>No workflow open</strong><span>Translate a VG2 file to begin.</span></div>}
          {active && <div className="canvas-actions">
            <button type="button" onClick={() => updateActive((tab) => withEdits(tab, undo(tab.edits)))} disabled={!active.edits.history.length}>Undo</button>
            <button type="button" onClick={() => updateActive((tab) => withEdits(tab, redo(tab.edits)))} disabled={!active.edits.future.length}>Redo</button>
            <button type="button" onClick={() => void validateEdits()} disabled={!Object.keys(active.edits.values).length || active.status === 'validating'}>Preview changes</button>
            <button type="button" onClick={() => void persistEdits()} disabled={!active.preview?.valid}>Apply changes</button>
            <button type="button" onClick={() => void persistLayout()}>Save layout</button>
            {active.status === 'conflict' && <button type="button" onClick={() => void reloadActive()}>Reload</button>}
          </div>}
        </section>

        <Inspector item={selected as WorkflowNodeModel | null} summary={selectedSummary} values={active?.edits.values ?? {}} preview={active?.preview ?? null} csv={active?.csv ?? null} onEdit={editParameter} onPreviewCsv={(path) => void loadCsv(path)} />
      </section>

      <details className="diagnostics" open={diagnostics.some((item) => item.level === 'error')}>
        <summary>Diagnostics <span>{diagnostics.length}</span></summary>
        {diagnostics.length ? diagnostics.map((item, index) => <p key={`${item.code}-${index}`} className={`diagnostic diagnostic--${item.level}`}><strong>{item.code}</strong> {item.message} {item.location && <small>{item.location}</small>}</p>) : <p>No diagnostics.</p>}
      </details>
    </main>
  )
}

export function App() {
  return <ReactFlowProvider><Editor /></ReactFlowProvider>
}
