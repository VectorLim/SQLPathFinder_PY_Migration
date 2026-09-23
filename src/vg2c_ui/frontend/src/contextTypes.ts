import type { DocumentView, SemanticBindingView } from './contracts.generated'

export interface ContextProps {
  document: DocumentView
  values: Record<string, unknown>
  onNavigate: (operationId: string, focus: boolean) => void
  onEdit: (binding: SemanticBindingView, value: unknown) => void
}
