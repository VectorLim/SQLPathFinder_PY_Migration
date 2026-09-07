import type { BatchProgress } from './useBatchTranslation'

interface Props {
  progress: BatchProgress | null
  onDismiss: () => void
}

export function TranslationProgress({ progress, onDismiss }: Props) {
  if (!progress) return null
  const completed = progress.items.filter((item) => item.status !== 'translating').length

  return <section className="translation-progress" aria-live="polite" aria-atomic="false">
    <header>
      <div>
        <strong>{progress.complete ? 'Translation results' : 'Translating files'}</strong>
        <small>{completed} of {progress.items.length} finished</small>
      </div>
      {progress.complete && <button type="button" onClick={onDismiss} aria-label="Dismiss translation results">×</button>}
    </header>
    <ul>
      {progress.items.map((item, index) => <li key={`${item.name}-${index}`} className={`translation-progress__item is-${item.status}`}>
        <span aria-hidden="true">{item.status === 'success' ? '✓' : item.status === 'error' ? '!' : '…'}</span>
        <div>
          <strong>{item.name}</strong>
          {item.message && <small>{item.message}</small>}
        </div>
      </li>)}
    </ul>
  </section>
}
