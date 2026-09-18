import { useState } from 'react'

import type {
  ChangePreviewView,
  ConditionOperatorView,
  HtmlPreviewView,
  SemanticBindingView,
  SemanticOperationView,
  SqlActionRequest,
  SqlModelView,
  SymbolView,
} from './contracts.generated'
import { ConditionEditor } from './ConditionEditor'
import type { FieldDraftProps } from './SchemaValueField'
import { SemanticBindingField } from './SemanticBindingField'
import { ValidationMessage } from './shared/SemanticControls'
import type { FieldPath } from './workspaceState'

interface Props extends FieldDraftProps {
  tabId: string
  operation: SemanticOperationView
  values: Record<string, unknown>
  saving: boolean
  knownFiles: string[]
  symbols: SymbolView[]
  conditionOperators: ConditionOperatorView[]
  onUploadFile: (file: File) => Promise<string>
  onEdit: (binding: SemanticBindingView, value: unknown, clearDraftPaths?: FieldPath[]) => void
  validateBinding: (tabId: string, bindingId: string, value: unknown) => Promise<ChangePreviewView>
  previewHtml: (tabId: string, operationId: string) => Promise<HtmlPreviewView>
  inspectSql: (tabId: string, bindingId: string) => Promise<SqlModelView>
  runSqlAction: (
    tabId: string,
    bindingId: string,
    action: SqlActionRequest['action'],
    args: Record<string, unknown>,
  ) => Promise<SqlModelView>
}

export function SemanticOperationEditor({
  tabId,
  operation,
  values,
  saving,
  knownFiles,
  symbols,
  conditionOperators,
  onUploadFile,
  onEdit,
  validateBinding,
  previewHtml,
  inspectSql,
  runSqlAction,
  drafts,
  onDraft,
}: Props) {
  const normal = operation.bindings.filter((binding) => binding.visibility === 'normal')
  const advanced = operation.bindings.filter((binding) => binding.visibility === 'advanced')
  const readOnly = operation.validation_state === 'unsupported'
  const bindingProps = {
    tabId,
    values,
    readOnly,
    knownFiles,
    symbols,
    onUploadFile,
    onEdit,
    validateBinding,
    inspectSql,
    runSqlAction,
    drafts,
    onDraft,
  }

  return <section className="operation-editor semantic-operation-editor" aria-label={`Configure ${operation.display_name}`}>
    <header className="operation-editor__header">
      <div>
        <span className="eyebrow">Configuration</span>
        <h3>{operation.display_name}</h3>
        {operation.description && <p className="operation-description">{operation.description}</p>}
      </div>
      <span className={`state-pill${readOnly ? ' state-pill--readonly' : ''}`}>
        {readOnly ? 'Read only' : operation.validation_state === 'unresolved' ? 'Needs attention' : 'Editable'}
      </span>
    </header>

    {operation.capabilities.includes('condition-editor')
      ? <ConditionEditor
          operation={operation}
          values={values}
          symbols={symbols}
          operators={conditionOperators}
          saving={saving}
          onEdit={onEdit}
        />
      : <fieldset className="parameter-grid" disabled={saving} aria-busy={saving}>
          {normal.map((binding) => <SemanticBindingField key={binding.id} binding={binding} {...bindingProps} />)}
          {!normal.length && !advanced.length && <p className="empty-copy">
            {readOnly ? 'This operation is visible for context but has no safe editor.' : 'No configurable values.'}
          </p>}
        </fieldset>}

    {operation.capabilities.includes('html-preview')
      && <HtmlPreviewPanel tabId={tabId} operationId={operation.id} previewHtml={previewHtml} />}

    {advanced.length > 0 && !operation.capabilities.includes('condition-editor') && <details className="advanced-settings">
      <summary>Advanced</summary>
      <fieldset className="parameter-grid" disabled={saving} aria-busy={saving}>
        {advanced.map((binding) => <SemanticBindingField key={binding.id} binding={binding} {...bindingProps} />)}
      </fieldset>
    </details>}

    {operation.validation_state === 'unresolved'
      && !operation.capabilities.includes('condition-editor')
      && <ValidationMessage message="One or more values reference an unresolved symbol." />}

    {operation.comments.length > 0 && <details className="operation-comments">
      <summary>Comments</summary>
      {operation.comments.map((comment, index) => <p key={index}>{comment}</p>)}
    </details>}
  </section>
}

function HtmlPreviewPanel({
  tabId,
  operationId,
  previewHtml,
}: {
  tabId: string
  operationId: string
  previewHtml: Props['previewHtml']
}) {
  const [preview, setPreview] = useState<HtmlPreviewView | null>(null)
  const [loading, setLoading] = useState(false)
  const [error, setError] = useState('')

  async function load() {
    setLoading(true)
    try {
      setPreview(await previewHtml(tabId, operationId))
      setError('')
    } catch (reason) {
      setPreview(null)
      setError(reason instanceof Error ? reason.message : 'HTML preview failed.')
    } finally {
      setLoading(false)
    }
  }

  return <section className="html-preview">
    <header>
      <div>
        <strong>Report Preview</strong>
        <small>Rendered safely from the current draft without running the workflow.</small>
      </div>
      <button type="button" disabled={loading} onClick={() => void load()}>{loading ? 'Rendering…' : 'Preview'}</button>
    </header>
    {error && <ValidationMessage message={error} />}
    {preview && <>
      <p className={`html-preview__state html-preview__state--${preview.state}`}>
        <strong>{preview.state === 'exact' ? 'Exact preview' : preview.state === 'approximate' ? 'Approximate preview' : 'Preview unavailable'}</strong>
        {preview.message ? ` — ${preview.message}` : ''}
      </p>
      {preview.output_path && <small>Output: {preview.output_path}</small>}
      {preview.html && <iframe title="HTML report preview" sandbox="" srcDoc={sandboxHtml(preview.html)} />}
    </>}
  </section>
}

function sandboxHtml(html: string): string {
  const csp = `<meta http-equiv="Content-Security-Policy" content="default-src 'none'; style-src 'unsafe-inline'; img-src data: blob:; font-src data:;">`
  return /<head(?:\s[^>]*)?>/i.test(html)
    ? html.replace(/<head(?:\s[^>]*)?>/i, (head) => `${head}${csp}`)
    : `${csp}${html}`
}
