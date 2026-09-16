import { useEffect, useState, type ChangeEvent, type DragEvent } from 'react'

import { formatBytes, type SourceIntakeController } from './useSourceIntake'
import './sourceIntake.css'

const CONTENT_ID = 'source-intake-content'

interface Props {
  intake: SourceIntakeController
  hasOpenDocument: boolean
}

export function SourceIntake({ intake, hasOpenDocument }: Props) {
  const [compactCollapsed, setCompactCollapsed] = useState(hasOpenDocument)
  const accept = intake.policy?.allowed_upload_suffixes.join(',')
  const hasActionableQueue = intake.queue.some((item) => item.status !== 'uploaded')

  useEffect(() => {
    if (hasActionableQueue) setCompactCollapsed(false)
    else setCompactCollapsed(hasOpenDocument)
  }, [hasActionableQueue, hasOpenDocument])

  function filesSelected(event: ChangeEvent<HTMLInputElement>) {
    intake.stageFiles(Array.from(event.currentTarget.files ?? []))
    event.currentTarget.value = ''
  }

  function dropped(event: DragEvent<HTMLElement>) {
    event.preventDefault()
    if (!intake.canStage) return
    intake.stageFiles(Array.from(event.dataTransfer.files))
  }

  const policyErrorMessage = intake.policyError?.message || 'Could not load workspace upload policy.'
  const compactMessage = intake.notice?.message ?? (intake.policyError ? policyErrorMessage : null)
  const compactError = intake.notice ? intake.notice.tone === 'error' : Boolean(intake.policyError)
  const compactSummary = intake.uploading
    ? 'Uploading…'
    : intake.translating
      ? 'Translating…'
      : intake.loadingFiles
        ? 'Loading workspace files…'
        : compactMessage ?? [
            intake.queuedCount ? `${intake.queuedCount} queued` : null,
            intake.selectedSources.length ? `${intake.selectedSources.length} selected` : null,
          ].filter(Boolean).join(' · ') || 'Upload or select sources'
  const shellClassName = [
    'source-intake-shell',
    compactCollapsed ? 'is-compact-collapsed' : '',
    compactError ? 'has-error' : '',
  ].filter(Boolean).join(' ')

  return <section className={shellClassName} aria-label="Workspace source intake">
    <input ref={intake.fileInputRef} className="sr-only" tabIndex={-1} type="file" multiple accept={accept} onChange={filesSelected} />
    <input
      ref={(node) => {
        intake.folderInputRef.current = node
        if (node) node.setAttribute('webkitdirectory', '')
      }}
      className="sr-only"
      tabIndex={-1}
      type="file"
      multiple
      accept={accept}
      onChange={filesSelected}
    />
    {intake.notice && <span className="sr-only" role={intake.notice.tone === 'error' ? 'alert' : 'status'}>{intake.notice.message}</span>}
    {intake.policyError && !intake.notice && <span className="sr-only" role="alert">{policyErrorMessage}</span>}

    <button
      className="source-intake__compact-toggle"
      type="button"
      aria-expanded={!compactCollapsed}
      aria-controls={CONTENT_ID}
      onClick={() => setCompactCollapsed((current) => !current)}
    >
      <span><strong>Sources & translation</strong><small>{compactSummary}</small></span>
      <span className="source-intake__compact-action">{compactCollapsed ? 'Show' : 'Hide'}</span>
    </button>

    <div id={CONTENT_ID} className="source-intake">
      <div className="source-intake__upload" onDragOver={(event) => event.preventDefault()} onDrop={dropped}>
        <div className="source-intake__heading">
          <div><span className="eyebrow">Source intake</span><strong>Upload source and data files</strong></div>
          <span className="source-intake__hint">Drop files here or browse</span>
        </div>
        <div className="source-intake__buttons" role="group" aria-label="Upload choices">
          <button type="button" onClick={intake.openFiles} disabled={!intake.canStage}>Upload files</button>
          <button type="button" onClick={intake.openFolder} disabled={!intake.canStage}>Upload folder</button>
          <button className="primary-button" type="button" onClick={() => void intake.uploadQueued()} disabled={!intake.canUploadQueued}>{intake.uploading ? 'Uploading…' : `Upload queued${intake.queuedCount ? ` (${intake.queuedCount})` : ''}`}</button>
        </div>
        {intake.policyError
          ? <p className="source-intake__policy source-intake__policy--error">{policyErrorMessage}</p>
          : !intake.policy && <p className="source-intake__policy">Loading upload policy…</p>}
      </div>

      <div className="source-intake__sources">
        <div className="source-intake__heading">
          <div><span className="eyebrow">Translation</span><strong>Select VG2 sources</strong></div>
          <span className="source-intake__hint">{intake.selectedSources.length} selected</span>
        </div>
        <div className="source-list" role="group" aria-label="Sources selected for translation" aria-busy={intake.loadingFiles}>
          {intake.loadingFiles
            ? <p className="empty-copy">Loading workspace files…</p>
            : intake.sources.length
              ? intake.sources.map((file) => <label className="source-option" key={file.path}>
                  <input
                    type="checkbox"
                    checked={intake.selectedSources.includes(file.path)}
                    onChange={(event) => intake.toggleSource(file.path, event.target.checked)}
                  />
                  <span title={file.path}>{file.path}</span>
                </label>)
              : <p className="empty-copy">Upload a source file to begin.</p>}
        </div>
        <button className="primary-button translate-selected" type="button" onClick={() => void intake.translateSelected()} disabled={!intake.canTranslate}>{intake.translating ? 'Translating…' : 'Translate selected'}</button>
      </div>

      {intake.notice && <p className={`source-intake__notice source-intake__notice--${intake.notice.tone}`}>{intake.notice.message}</p>}

      {intake.translationDiagnostics.length > 0 && <div className="source-intake__diagnostics" aria-label="Translation failures">
        {intake.translationDiagnostics.map((diagnostic, index) => <p key={`${diagnostic.code}-${index}`}>
          <strong>{diagnostic.code}</strong>
          <span>{diagnostic.message}</span>
        </p>)}
      </div>}

      {intake.queue.length > 0 && <div className="upload-queue" aria-label="Upload queue">
        <div className="upload-queue__header"><strong>Upload queue</strong>{intake.completedCount > 0 && <button className="text-button" type="button" onClick={intake.clearCompleted}>Clear completed</button>}</div>
        <div className="upload-queue__items">
          {intake.queue.map((item) => <article className={`upload-item upload-item--${item.status}`} key={item.id}>
            <div className="upload-item__copy"><strong title={item.path}>{item.path}</strong><small>{formatBytes(item.file.size)} · {statusLabel(item.status)}</small>{item.error && <span role="alert">{item.error}</span>}</div>
            <div className="upload-item__actions">
              {item.status === 'failed' && item.retryable && <button type="button" onClick={() => void intake.retry(item.id)} disabled={!intake.canStage}>Retry</button>}
              {(item.status === 'queued' || item.status === 'failed') && <button type="button" onClick={() => intake.remove(item.id)} disabled={intake.uploading}>Remove</button>}
            </div>
          </article>)}
        </div>
      </div>}
    </div>
  </section>
}

function statusLabel(status: SourceIntakeController['queue'][number]['status']): string {
  if (status === 'queued') return 'Queued'
  if (status === 'uploading') return 'Uploading'
  if (status === 'uploaded') return 'Uploaded'
  return 'Failed'
}
