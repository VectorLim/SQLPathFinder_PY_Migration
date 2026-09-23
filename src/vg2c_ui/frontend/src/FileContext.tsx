import { ExternalLink } from 'lucide-react'
import type { DocumentView, FileEffectView } from './contracts.generated'
import type { ContextProps } from './contextTypes'

export function FileContext({ document, onNavigate }: ContextProps) {
  const lifecycleKinds = new Set<FileEffectView['kind']>(['write', 'copy', 'move', 'transform', 'append', 'delete'])
  const lifecycle = document.effects.filter((effect) => lifecycleKinds.has(effect.kind)).slice().sort((left, right) => left.order - right.order)
  return <div className="context-section file-context">
    <header><p>Ordered file operations, including fan-in when several inputs produce one output.</p></header>
    {lifecycle.length
      ? lifecycle.map((effect) => <FileEffectRow key={effect.id} document={document} effect={effect} onNavigate={onNavigate} />)
      : <p className="empty-copy">No file lifecycle changes.</p>}
  </div>
}

function FileEffectRow({
  document,
  effect,
  onNavigate,
}: {
  document: DocumentView
  effect: FileEffectView
  onNavigate: ContextProps['onNavigate']
}) {
  const operation = document.semantic_operations.find((item) => item.id === effect.operation_id)
  const inputs = effect.inputs
  const outputs = effect.outputs
  return <article className="file-effect-row">
    <header>
      <button type="button" className="file-effect-operation" onClick={() => onNavigate(effect.operation_id, true)}>
        <ExternalLink size={13} />{operation?.display_name ?? humanizeEffect(effect.kind)}
      </button>
      <small>{humanizeEffect(effect.kind)}</small>
    </header>
    <div className="file-effect-flow" aria-label={`${inputs.length} inputs to ${outputs.length} outputs`}>
      <div className="file-endpoints">
        {inputs.length ? inputs.map((endpoint) => <FileEndpointChip key={endpoint.id} endpoint={endpoint} document={document} onNavigate={onNavigate} />) : <span className="file-endpoint file-endpoint--generated">Generated content</span>}
      </div>
      <span className="file-flow-arrow" aria-hidden="true">→</span>
      <div className="file-endpoints">
        {outputs.length
          ? outputs.map((endpoint) => <FileEndpointChip key={endpoint.id} endpoint={endpoint} document={document} onNavigate={onNavigate} />)
          : <span className="file-endpoint file-endpoint--deleted">Deleted</span>}
      </div>
    </div>
    <div className="file-effect-meta">
      {inputs.length > 1 && <small>{inputs.length} inputs feed this operation.</small>}
      {effect.conditional && <small>Conditional</small>}
      {effect.in_loop && <small>Runs in a loop</small>}
      {effect.reason && <small>{effect.reason}</small>}
    </div>
  </article>
}

function FileEndpointChip({
  endpoint,
  document,
  onNavigate,
}: {
  endpoint: FileEffectView['inputs'][number]
  document: DocumentView
  onNavigate: ContextProps['onNavigate']
}) {
  const label = endpoint.path ?? endpoint.expression ?? 'Dynamic path'
  const resource = document.files.find((item) => item.id === endpoint.file_resource_id)
  if (!resource) return <span className={`file-endpoint file-endpoint--${endpoint.status}`} title={label}>{label}</span>
  const references = [
    ...resource.producer_refs.map((ref) => ({ ref, role: 'Produced' })),
    ...resource.consumer_refs.map((ref) => ({ ref, role: 'Used' })),
    ...resource.lifecycle_refs.map((ref) => ({ ref, role: 'Changed' })),
  ]
  return <details className="file-resource-details">
    <summary className={`file-endpoint file-endpoint--${endpoint.status}`} title={label}>{label}</summary>
    <div className="file-resource-popover">
      <strong>{resource.path ?? label}</strong>
      {references.map(({ ref, role }, index) => {
        const operation = document.semantic_operations.find((item) => item.id === ref.operation_id)
        return <button type="button" key={`${role}:${ref.operation_id}:${ref.binding_id}:${index}`}
          onClick={() => onNavigate(ref.operation_id, true)}>{role}: {operation?.display_name ?? 'Operation'}</button>
      })}
      {!references.length && <small>No workflow references.</small>}
    </div>
  </details>
}

function humanizeEffect(kind: FileEffectView['kind']): string {
  if (kind === 'write') return 'Create'
  if (kind === 'transform') return 'Transform'
  return kind.charAt(0).toUpperCase() + kind.slice(1)
}


