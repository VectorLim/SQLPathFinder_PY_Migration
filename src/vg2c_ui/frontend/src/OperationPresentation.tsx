import './operationPresentation.css'

import type { DependencyIssueView } from './contracts.generated'

export function OperationDiagnostics({ diagnostics }: { diagnostics: DependencyIssueView[] }) {
  if (!diagnostics.length) return null
  return <div className="operation-diagnostics" role="alert">
    {diagnostics.map((item) => <p key={`${item.code}-${item.artifact}`}>
      <strong>{item.code.replaceAll('_', ' ')}</strong>
      <span>{item.message}</span>
    </p>)}
  </div>
}
