import './operationPresentation.css'

import type { DependencyIssueView } from './contracts.generated'

interface Files {
  inputs: string[]
  outputs: string[]
}

export function FileReferences({ files }: { files: Files }) {
  if (!files.inputs.length && !files.outputs.length) return null
  return <div className="operation-io">
    {files.inputs.length > 0 && <FileChips label="Reads" paths={files.inputs} />}
    {files.outputs.length > 0 && <FileChips label="Produces" paths={files.outputs} />}
  </div>
}

export function OperationDiagnostics({ diagnostics }: { diagnostics: DependencyIssueView[] }) {
  if (!diagnostics.length) return null
  return <div className="operation-diagnostics" role="alert">
    {diagnostics.map((item) => <p key={`${item.code}-${item.artifact}`}>
      <strong>{item.code.replaceAll('_', ' ')}</strong>
      <span>{item.message}</span>
    </p>)}
  </div>
}

function FileChips({ label, paths }: { label: string; paths: string[] }) {
  return <div className="file-chip-row">
    <strong>{label}</strong>
    <div>{paths.map((path) => <code key={path} title={path}>{path}</code>)}</div>
  </div>
}
