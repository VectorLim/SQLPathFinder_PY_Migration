import { ArrowUpRight } from 'lucide-react'
import type { CsvPreviewView, DocumentView, FileEffectView, FileEndpointView, WorkspaceProjectionView } from '../contracts.generated'
import { FlowRow } from './FlowRow'

interface Props {
  document: DocumentView
  effects: FileEffectView[]
  projection: WorkspaceProjectionView | null
  selectedId: string | null
  status: string
  error: string | null
  csv: CsvPreviewView | null
  csvError: string | null
  csvLoading: boolean
  csvPath: string | null
  onActivate: (documentId: string, operationId: string, focus: boolean) => void
  onPreview: (effectId: string, endpoint: FileEndpointView) => void
}

export function FileFlowPane(props: Props) {
  const { document, effects, selectedId, onActivate, onPreview } = props
  const links = props.projection?.dependencies.filter((link) => link.consumer_document_id === document.id || link.producer_document_id === document.id) ?? []
  const issues = props.projection?.issues.filter((issue) => issue.document_id === document.id) ?? []
  return <div className="file-flow-pane" aria-busy={props.status === 'loading'}>
    {props.status === 'loading' && <p className="flow-notice" role="status">Updating. Displayed flow may be stale.</p>}
    {props.error && <p className="validation-error" role="alert">Stale flow: {props.error}</p>}
    {issues.map((issue) => <p className="validation-error" key={`${issue.code}-${issue.step_id}-${issue.artifact}`}>{issue.message}</p>)}
    {effects.length ? effects.map((effect) => {
      const step = document.steps.find((item) => item.id === effect.step_id)
      return <FlowRow key={effect.id} effect={effect} label={step?.display_label ?? effect.operation_id} selected={effect.operation_id === selectedId} onActivate={(operationId, focus) => onActivate(document.id, operationId, focus)} onPreview={onPreview} />
    }) : <p className="empty-copy">No declared file effects.</p>}
    {links.length > 0 && <section className="workspace-dependencies"><h3>Possible Workspace Dependencies</h3>{links.map((link, index) => {
      const upstream = link.consumer_document_id === document.id
      const targetDocument = upstream ? link.producer_document_id : link.consumer_document_id
      const targetOperation = upstream ? link.producer_operation_id : link.consumer_operation_id
      return <button key={`${targetDocument}-${targetOperation}-${index}`} type="button" disabled={!targetOperation} onClick={(event) => targetOperation && onActivate(targetDocument, targetOperation, event.detail === 0)}><ArrowUpRight size={15} aria-hidden="true" /><span>{upstream ? 'From' : 'To'} {targetDocument}<small>{link.artifact}</small></span></button>
    })}</section>}
    {props.csvPath && <section className="on-disk-preview" aria-label="On-disk CSV preview"><h3>On-Disk Preview</h3><p>{props.csvPath}</p>{props.csvLoading && <p role="status">Loading file...</p>}{props.csvError && <p role="alert" className="validation-error">{props.csvError}</p>}{props.csv && <CsvTable preview={props.csv} />}</section>}
    <details className="file-details"><summary>File Details</summary><dl><dt>Source</dt><dd>{document.source_path}</dd><dt>Generated</dt><dd>{document.output_path}</dd><dt>Revision</dt><dd>{document.revision}</dd></dl></details>
  </div>
}

function CsvTable({ preview }: { preview: CsvPreviewView }) {
  return <div className="csv-preview"><small>{preview.size_bytes.toLocaleString()} bytes{preview.truncated ? ' / truncated' : ''}</small><div className="table-scroll"><table><thead><tr>{preview.columns.map((column, index) => <th key={`${column}-${index}`}>{column}</th>)}</tr></thead><tbody>{preview.rows.map((row, rowIndex) => <tr key={rowIndex}>{row.map((cell, index) => <td key={index}>{cell}</td>)}</tr>)}</tbody></table></div></div>
}