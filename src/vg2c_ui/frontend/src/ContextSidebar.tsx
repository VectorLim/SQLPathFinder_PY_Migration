import { useState, type ReactNode } from 'react'

import type {
  ArtifactView,
  CsvPreviewView,
  DocumentView,
  WorkspaceProjectionView,
} from './contracts.generated'
import { baseName } from './operationLabels'

interface Props {
  document: DocumentView | null
  documents: DocumentView[]
  projection: WorkspaceProjectionView | null
  csv: CsvPreviewView | null
  csvArtifactPath: string | null
  open: boolean
  onClose: () => void
  onPreviewCsv: (path: string) => void
  onActivateDocument: (id: string) => void
}

interface Section { id: string; title: string; defaultOpen?: boolean; content: ReactNode }

export function ContextSidebar({ document, documents, projection, csv, csvArtifactPath, open, onClose, onPreviewCsv, onActivateDocument }: Props) {
  const sections: Section[] = document ? [
    { id: 'data-flow', title: 'Data Flow', defaultOpen: true, content: <DataFlowSection document={document} documents={documents} projection={projection} csv={csv} csvArtifactPath={csvArtifactPath} onPreviewCsv={onPreviewCsv} onActivateDocument={onActivateDocument} /> },
    { id: 'file-details', title: 'File Details', content: <FileDetails document={document} /> },
  ] : []
  return <aside id="file-context" className={`context-sidebar${open ? ' is-open' : ''}`} aria-label="File context">
    <header className="context-sidebar__header"><div><span className="eyebrow">Current file</span><h2 title={document?.output_path}>{document ? baseName(document.output_path) : 'Context'}</h2></div><button className="icon-button context-close" type="button" onClick={onClose} aria-label="Close context sidebar">×</button></header>
    <div className="context-sidebar__body">{!document && <p className="empty-copy">Open a translated script to inspect its context.</p>}{sections.map((section) => <Panel key={section.id} section={section} />)}</div>
  </aside>
}

function Panel({ section }: { section: Section }) {
  const [expanded, setExpanded] = useState(Boolean(section.defaultOpen))
  return <details className="context-section" open={expanded} onToggle={(event) => setExpanded(event.currentTarget.open)}><summary><span>{section.title}</span></summary><div className="context-section__content">{section.content}</div></details>
}

function DataFlowSection({ document, documents, projection, csv, csvArtifactPath, onPreviewCsv, onActivateDocument }: Omit<Props, 'open' | 'onClose'> & { document: DocumentView }) {
  const artifacts = projection?.documents.find((item) => item.document_id === document.id)?.artifacts ?? document.artifacts
  const inputs = artifacts.filter((artifact) => artifact.is_external_input)
  const outputs = artifacts.filter((artifact) => artifact.is_output)
  const issues = projection?.issues.filter((item) => item.document_id === document.id) ?? []
  const upstream = dependencyRows(document.id, documents, projection, 'upstream')
  const downstream = dependencyRows(document.id, documents, projection, 'downstream')
  return <div className="data-flow">
    <p className="context-help">Dependencies reflect compiler projection for every open tab, including unsaved drafts.</p>
    {issues.length > 0 && <section className="flow-group dependency-issues"><h3>Dependency issues<span>{issues.length}</span></h3><div className="flow-list">{issues.map((item) => <article className="dependency-issue" key={`${item.code}-${item.step_id}-${item.artifact}`}><strong>{item.code.replaceAll('_', ' ')}</strong><span>{item.message}</span></article>)}</div></section>}
    <FlowGroup title="Required inputs" items={inputs} empty="No external file inputs detected.">{inputs.map((artifact) => <ArtifactRow key={artifact.id} artifact={artifact} direction="input" csv={csvArtifactPath === artifact.path ? csv : null} onPreviewCsv={onPreviewCsv} invalid={issues.some((item) => item.artifact === artifact.path)} />)}</FlowGroup>
    <DependencyGroup title="Upstream open files" rows={upstream} empty="No open translated file produces these inputs." onActivateDocument={onActivateDocument} />
    <div className="flow-divider" aria-hidden="true"><span>current script</span></div>
    <FlowGroup title="Produced files" items={outputs} empty="No file outputs detected.">{outputs.map((artifact) => <ArtifactRow key={artifact.id} artifact={artifact} direction="output" csv={csvArtifactPath === artifact.path ? csv : null} onPreviewCsv={onPreviewCsv} invalid={issues.some((item) => item.artifact === artifact.path)} />)}</FlowGroup>
    <DependencyGroup title="Downstream open files" rows={downstream} empty="No open translated file depends on these outputs." onActivateDocument={onActivateDocument} />
  </div>
}

function FlowGroup({ title, items, empty, children }: { title: string; items: ArtifactView[]; empty: string; children: ReactNode }) { return <section className="flow-group"><h3>{title}<span>{items.length}</span></h3>{items.length ? <div className="flow-list">{children}</div> : <p className="flow-empty">{empty}</p>}</section> }

function ArtifactRow({ artifact, direction, csv, onPreviewCsv, invalid }: { artifact: ArtifactView; direction: 'input' | 'output'; csv: CsvPreviewView | null; onPreviewCsv: (path: string) => void; invalid: boolean }) {
  return <article className={`artifact-row${invalid ? ' artifact-row--invalid' : ''}`}><div className="artifact-row__top"><span className={`flow-icon flow-icon--${direction}`} aria-hidden="true">{direction === 'input' ? '↓' : '↑'}</span><div className="artifact-name"><strong>{artifact.label}</strong><small>{artifact.path}</small></div><button className="text-button" type="button" onClick={() => onPreviewCsv(artifact.path)}>Preview</button></div><div className="artifact-meta">{artifact.conditional && <span>conditional</span>}{artifact.in_loop && <span>loop output</span>}{!artifact.order_valid && <span className="warning-chip">order warning</span>}{invalid && <span className="error-chip">dependency error</span>}</div>{csv && <CsvTable preview={csv} />}</article>
}

interface DependencyRow { documentId: string; fileName: string; sourcePath: string; artifactPaths: string[] }
function dependencyRows(activeId: string, documents: DocumentView[], projection: WorkspaceProjectionView | null, direction: 'upstream' | 'downstream'): DependencyRow[] {
  if (!projection) return []
  const grouped = new Map<string, string[]>()
  for (const edge of projection.dependencies) {
    const related = direction === 'upstream'
      ? edge.consumer_document_id === activeId ? edge.producer_document_id : null
      : edge.producer_document_id === activeId ? edge.consumer_document_id : null
    if (!related || related === activeId) continue
    grouped.set(related, [...(grouped.get(related) ?? []), edge.artifact])
  }
  return [...grouped].map(([documentId, paths]) => {
    const document = documents.find((item) => item.id === documentId)
    return { documentId, fileName: baseName(document?.output_path ?? documentId), sourcePath: document?.source_path ?? documentId, artifactPaths: [...new Set(paths)].sort() }
  })
}

function DependencyGroup({ title, rows, empty, onActivateDocument }: { title: string; rows: DependencyRow[]; empty: string; onActivateDocument: (id: string) => void }) { return <section className="flow-group dependency-group"><h3>{title}<span>{rows.length}</span></h3>{rows.length ? rows.map((row) => <button className="dependency-row" key={row.documentId} type="button" onClick={() => onActivateDocument(row.documentId)} title={row.sourcePath}><span aria-hidden="true">↗</span><span><strong>{row.fileName}</strong><small>{row.artifactPaths.map(baseName).join(', ')}</small></span></button>) : <p className="flow-empty">{empty}</p>}</section> }
function FileDetails({ document }: { document: DocumentView }) { return <dl className="file-details"><div><dt>Source</dt><dd title={document.source_path}>{document.source_path}</dd></div><div><dt>Generated</dt><dd title={document.output_path}>{document.output_path}</dd></div><div><dt>Revision</dt><dd>{document.revision}</dd></div><div><dt>Operations</dt><dd>{document.steps.length}</dd></div><div><dt>Diagnostics</dt><dd>{document.diagnostics.length}</dd></div></dl> }
function CsvTable({ preview }: { preview: CsvPreviewView }) { return <div className="csv-preview"><small>{preview.size_bytes.toLocaleString()} bytes{preview.truncated ? ' · truncated' : ''}</small><div className="table-scroll"><table><thead><tr>{preview.columns.map((column, index) => <th key={`${column}-${index}`}>{column}</th>)}</tr></thead><tbody>{preview.rows.map((row, rowIndex) => <tr key={rowIndex}>{row.map((cell, index) => <td key={index}>{cell}</td>)}</tr>)}</tbody></table></div></div> }
