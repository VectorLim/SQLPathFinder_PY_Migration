import type {
  ChangePreviewView,
  ConditionOperatorView,
  HtmlPreviewView,
  SemanticBindingView,
  SqlModelView,
  SymbolView,
} from './api/contracts.generated'
import type { SqlCommand } from './api/sqlActions'
import type { FieldPath, TabState } from './workspace/state'

export interface BindingEdit {
  binding: SemanticBindingView
  value: unknown
  clearDraftPaths?: FieldPath[]
}

export interface SemanticEditorSession {
  values: Record<string, unknown>
  saving: boolean
  readOnly: boolean
  resources: {
    symbols: SymbolView[]
    conditionOperators: ConditionOperatorView[]
  }
  drafts: {
    values: TabState['fieldDrafts']
    update: (key: string, draft: TabState['fieldDrafts'][string] | null) => void
  }
  actions: {
    uploadFile: (file: File) => Promise<string>
    edit: (request: BindingEdit) => void
    validateBinding: (bindingId: string, value: unknown) => Promise<ChangePreviewView>
    previewHtml: (operationId: string) => Promise<HtmlPreviewView>
    inspectSql: (bindingId: string) => Promise<SqlModelView>
    runSqlCommand: (bindingId: string, command: SqlCommand) => Promise<SqlModelView>
  }
}
