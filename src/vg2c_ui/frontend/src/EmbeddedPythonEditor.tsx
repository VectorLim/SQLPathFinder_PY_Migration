import { useEffect, useRef, useState } from 'react'

import type { ChangePreviewView, SemanticBindingView } from './contracts.generated'
import { useModalDialog } from './useModalDialog'

interface Props {
  tabId: string
  binding: SemanticBindingView
  value: unknown
  disabled: boolean
  validateBinding: (tabId: string, bindingId: string, value: unknown) => Promise<ChangePreviewView>
  onCommit: (value: string) => void
}

export function EmbeddedPythonEditor({ tabId, binding, value, disabled, validateBinding, onCommit }: Props) {
  const [open, setOpen] = useState(false)
  const [draft, setDraft] = useState(String(value ?? ''))
  const [preview, setPreview] = useState<ChangePreviewView | null>(null)
  const [error, setError] = useState('')
  const [validating, setValidating] = useState(false)
  const textareaRef = useRef<HTMLTextAreaElement>(null)
  const modal = useModalDialog(open, () => setOpen(false), { onOpened: () => textareaRef.current?.focus() })

  useEffect(() => {
    if (!open) setDraft(String(value ?? ''))
  }, [open, value])

  async function validateDraft() {
    setValidating(true)
    try {
      const result = await validateBinding(tabId, binding.id, draft)
      setPreview(result)
      setError('')
      return result
    } catch (reason) {
      setError(reason instanceof Error ? reason.message : 'Could not validate embedded Python.')
      return null
    } finally {
      setValidating(false)
    }
  }

  async function save() {
    const result = await validateDraft()
    if (!result?.valid) return
    onCommit(draft)
    modal.requestClose()
  }

  return <div className="embedded-python-control">
    <button type="button" disabled={disabled} onClick={() => { setPreview(null); setError(''); setOpen(true) }}>Edit Python</button>
    <small>Opens a focused editor for this embedded Python block only.</small>
    <dialog
      ref={modal.dialogRef}
      className="embedded-python-dialog"
      aria-labelledby="embedded-python-title"
      onClose={modal.handleClose}
      onClick={modal.handleBackdropClick}
    >
      <div className="embedded-python-card">
        <header>
          <span className="eyebrow">Embedded Python</span>
          <h2 id="embedded-python-title">Edit Python block</h2>
        </header>
        <textarea
          ref={textareaRef}
          aria-label="Embedded Python source"
          rows={18}
          spellCheck={false}
          value={draft}
          onChange={(event) => { setDraft(event.target.value); setPreview(null); setError('') }}
        />
        {error && <p className="validation-error" role="alert">{error}</p>}
        {preview && !preview.valid && <div className="embedded-python-issues" role="alert">
          {preview.issues.map((issue) => <p key={`${issue.code}:${issue.message}`}>{issue.message}</p>)}
        </div>}
        {preview?.valid && <p className="validation-success" role="status">Python is valid.</p>}
        <footer>
          <button type="button" onClick={modal.requestClose}>Cancel</button>
          <button type="button" disabled={validating} onClick={() => void validateDraft()}>{validating ? 'Validating…' : 'Validate'}</button>
          <button className="primary-button" type="button" disabled={validating || disabled} onClick={() => void save()}>Update Python</button>
        </footer>
      </div>
    </dialog>
  </div>
}
