import { useState, type ReactNode } from 'react'
import {
  artifactKey,
  deriveFileFlow,
  displayHeaderInfo,
  type HeaderInfo,
} from './dataFlow'
import { baseName } from './operationLabels'
import type { CsvArtifact, CsvPreview, WorkflowDocument } from './types'

interface ContextSidebarProps {
  document: WorkflowDocument | null
  documents: WorkflowDocument[]
  headerCache: Record<string, HeaderInfo>
  values: Record<string, unknown>
  csv: CsvPreview | null
  open: boolean
  onClose: () => void
  onPreviewCsv: (path: string) => void
  onActivateDocument: (id: string) => void
}

interface ContextSection {
  id: string
  title: string
  badge?: number
  defaultOpen?: boolean
  content: ReactNode
}

export function ContextSidebar({
  document,
  documents,
  headerCache,
  values,
  csv,
  open,
  onClose,
  onPreviewCsv,
  onActivateDocument,
}: ContextSidebarProps) {
  const sections: ContextSection[] = document ? [
    {
      id: 'data-flow',
      title: 'Data Flow',
      defaultOpen: true,
      content: (
        <DataFlowSection
          document={document}
          documents={documents}
          headerCache={headerCache}
          values={values}
          csv={csv}
          onPreviewCsv={onPreviewCsv}
          onActivateDocument={onActivateDocument}
        />
      ),
    },
    {
      id: 'file-details',
      title: 'File Details',
      content: <FileDetails document={document} />,
    },
  ] : []

  return (
    <aside id="file-context" className={`context-sidebar${open ? ' is-open' : ''}`} aria-label="File context">
      <header className="context-sidebar__header">
        <div>
          <span className="eyebrow">Current file</span>
          <h2 title={document?.output_path}>{document ? baseName(document.output_path) : 'Context'}</h2>
        </div>
        <button className="icon-button context-close" type="button" onClick={onClose} aria-label="Close context sidebar">×</button>
      </header>

      <div className="context-sidebar__body">
        {!document && <p className="empty-copy">Open a translated script to inspect its context.</p>}
        {sections.map((section) => <ContextSectionPanel key={section.id} section={section} />)}
      </div>
    </aside>
  )
}

function ContextSectionPanel({ section }: { section: ContextSection }) {
  const [expanded, setExpanded] = useState(Boolean(section.defaultOpen))
  return (
    <details
      className="context-section"
      open={expanded}
      onToggle={(event) => setExpanded(event.currentTarget.open)}
    >
      <summary>
        <span>{section.title}</span>
        {section.badge !== undefined && <small>{section.badge}</small>}
      </summary>
      <div className="context-section__content">{section.content}</div>
    </details>
  )
}

function DataFlowSection({
  document,
  documents,
  headerCache,
  values,
  csv,
  onPreviewCsv,
  onActivateDocument,
}: {
  document: WorkflowDocument
  documents: WorkflowDocument[]
  headerCache: Record<string, HeaderInfo>
  values: Record<string, unknown>
  csv: CsvPreview | null
  onPreviewCsv: (path: string) => void
  onActivateDocument: (id: string) => void
}) {
  const flow = deriveFileFlow(document, documents)
  return (
    <div className="data-flow">
      <p className="context-help">Dependencies are resolved from the translated files currently open in this editor.</p>

      <FlowGroup title="Required inputs" count={flow.inputs.length} empty="No external file inputs detected.">
        {flow.inputs.map((artifact) => (
          <ArtifactRow
            key={artifact.id}
            artifact={artifact}
            direction="input"
            header={displayHeaderInfo(document, artifact.path, headerCache, values)}
            csv={csv}
            onPreviewCsv={onPreviewCsv}
          />
        ))}
      </FlowGroup>

      <DependencyGroup
        title="Upstream open files"
        dependencies={flow.upstream}
        empty="No open translated file produces these inputs."
        onActivateDocument={onActivateDocument}
      />

      <div className="flow-divider" aria-hidden="true"><span>current script</span></div>

      <FlowGroup title="Produced files" count={flow.outputs.length} empty="No file outputs detected.">
        {flow.outputs.map((artifact) => (
          <ArtifactRow
            key={artifact.id}
            artifact={artifact}
            direction="output"
            header={displayHeaderInfo(document, artifact.path, headerCache, values)}
            csv={csv}
            onPreviewCsv={onPreviewCsv}
          />
        ))}
      </FlowGroup>

      <DependencyGroup
        title="Downstream open files"
        dependencies={flow.downstream}
        empty="No open translated file depends on these outputs."
        onActivateDocument={onActivateDocument}
      />
    </div>
  )
}

function FlowGroup({ title, count, empty, children }: {
  title: string
  count: number
  empty: string
  children: ReactNode
}) {
  return (
    <section className="flow-group">
      <h3>{title}<span>{count}</span></h3>
      {count ? <div className="flow-list">{children}</div> : <p className="flow-empty">{empty}</p>}
    </section>
  )
}

function ArtifactRow({ artifact, direction, header, csv, onPreviewCsv }: {
  artifact: CsvArtifact
  direction: 'input' | 'output'
  header: HeaderInfo
  csv: CsvPreview | null
  onPreviewCsv: (path: string) => void
}) {
  const selectedPreview = csv && artifactKey(csv.path) === artifactKey(artifact.path) ? csv : null
  return (
    <article className="artifact-row">
      <div className="artifact-row__top">
        <span className={`flow-icon flow-icon--${direction}`} aria-hidden="true">{direction === 'input' ? '↓' : '↑'}</span>
        <div className="artifact-name">
          <strong title={artifact.path}>{artifact.label}</strong>
          <small title={artifact.path}>{artifact.path}</small>
        </div>
        <button className="text-button" type="button" onClick={() => onPreviewCsv(artifact.path)}>Preview</button>
      </div>
      <div className="artifact-meta">
        {artifact.conditional && <span>conditional</span>}
        {artifact.in_loop && <span>loop output</span>}
        {!artifact.order_valid && <span className="warning-chip">order warning</span>}
      </div>
      <HeaderSummary info={header} />
      {selectedPreview && <CsvTable preview={selectedPreview} />}
    </article>
  )
}

function HeaderSummary({ info }: { info: HeaderInfo }) {
  const label = info.source === 'declared'
    ? 'Expected headers'
    : info.source === 'detected'
      ? 'Current headers'
      : info.source === 'loading'
        ? 'Headers'
        : 'Headers'
  return (
    <div className="header-summary">
      <small>{label}</small>
      {info.source === 'loading' ? (
        <span className="header-status">Checking…</span>
      ) : info.columns.length ? (
        <div className="header-chips">
          {info.columns.slice(0, 10).map((column) => <code key={column}>{column}</code>)}
          {info.columns.length > 10 && <span>+{info.columns.length - 10}</span>}
        </div>
      ) : (
        <span className="header-status">Unknown / unavailable</span>
      )}
    </div>
  )
}

function DependencyGroup({ title, dependencies, empty, onActivateDocument }: {
  title: string
  dependencies: ReturnType<typeof deriveFileFlow>['upstream']
  empty: string
  onActivateDocument: (id: string) => void
}) {
  return (
    <section className="flow-group dependency-group">
      <h3>{title}<span>{dependencies.length}</span></h3>
      {dependencies.length ? dependencies.map((dependency) => (
        <button
          className="dependency-row"
          key={dependency.documentId}
          type="button"
          onClick={() => onActivateDocument(dependency.documentId)}
          title={dependency.sourcePath}
        >
          <span aria-hidden="true">↗</span>
          <span>
            <strong>{dependency.fileName}</strong>
            <small>{dependency.artifactPaths.map(baseName).join(', ')}</small>
          </span>
        </button>
      )) : <p className="flow-empty">{empty}</p>}
    </section>
  )
}

function FileDetails({ document }: { document: WorkflowDocument }) {
  return (
    <dl className="file-details">
      <div><dt>Source</dt><dd title={document.source_path}>{document.source_path}</dd></div>
      <div><dt>Generated</dt><dd title={document.output_path}>{document.output_path}</dd></div>
      <div><dt>Revision</dt><dd>{document.revision}</dd></div>
      <div><dt>Operations</dt><dd>{document.steps.length}</dd></div>
      <div><dt>Diagnostics</dt><dd>{document.diagnostics.length}</dd></div>
    </dl>
  )
}

function CsvTable({ preview }: { preview: CsvPreview }) {
  return (
    <div className="csv-preview">
      <small>{preview.size_bytes.toLocaleString()} bytes{preview.truncated ? ' · truncated' : ''}</small>
      <div className="table-scroll">
        <table>
          <thead><tr>{preview.columns.map((column, index) => <th key={`${column}-${index}`}>{column}</th>)}</tr></thead>
          <tbody>{preview.rows.map((row, rowIndex) => (
            <tr key={rowIndex}>{row.map((cell, index) => <td key={index}>{cell}</td>)}</tr>
          ))}</tbody>
        </table>
      </div>
    </div>
  )
}
