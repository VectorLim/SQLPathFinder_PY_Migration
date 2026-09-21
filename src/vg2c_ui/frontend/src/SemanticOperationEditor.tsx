import { useState } from 'react'

import type {
  HtmlPreviewView,
  SemanticOperationView,
} from './contracts.generated'
import { ConditionEditor } from './ConditionEditor'
import { SemanticBindingField } from './SemanticBindingField'
import type { SemanticEditorSession } from './semanticEditorSession'
import { ValidationMessage } from './shared/SemanticControls'

interface Props {
  operation: SemanticOperationView
  session: SemanticEditorSession
}

export function SemanticOperationEditor({ operation, session }: Props) {
  const { values, saving, resources, actions } = session
  const normal = operation.bindings.filter((binding) => binding.visibility === 'normal')
  const advanced = operation.bindings.filter((binding) => binding.visibility === 'advanced')
  const readOnly = operation.validation_state === 'unsupported'

  return <section className="operation-editor semantic-operation-editor" aria-label={`Configure ${operation.display_name}`}>
    <header className="operation-editor__header">
      <div>
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
          symbols={resources.symbols}
          operators={resources.conditionOperators}
          saving={saving}
          onEdit={actions.edit}
        />
      : <fieldset className="parameter-grid" disabled={saving} aria-busy={saving}>
          {normal.map((binding) => <SemanticBindingField key={binding.id} binding={binding} readOnly={readOnly} session={session} />)}
          {!normal.length && !advanced.length && <p className="empty-copy">
            {readOnly ? 'This operation is visible for context but has no safe editor.' : 'No configurable values.'}
          </p>}
        </fieldset>}

    {operation.capabilities.includes('html-preview')
      && <HtmlPreviewPanel operationId={operation.id} previewHtml={actions.previewHtml} />}

    {advanced.length > 0 && !operation.capabilities.includes('condition-editor') && <details className="advanced-settings">
      <summary>Advanced</summary>
      <fieldset className="parameter-grid" disabled={saving} aria-busy={saving}>
        {advanced.map((binding) => <SemanticBindingField key={binding.id} binding={binding} readOnly={readOnly} session={session} />)}
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
  operationId,
  previewHtml,
}: {
  operationId: string
  previewHtml: SemanticEditorSession['actions']['previewHtml']
}) {
  const [preview, setPreview] = useState<HtmlPreviewView | null>(null)
  const [loading, setLoading] = useState(false)
  const [error, setError] = useState('')

  async function load() {
    setLoading(true)
    try {
      setPreview(await previewHtml(operationId))
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
