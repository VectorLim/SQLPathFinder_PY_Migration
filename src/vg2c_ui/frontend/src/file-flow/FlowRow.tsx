import { ArrowRight, Eye, X } from 'lucide-react'
import type { FileEffectView, FileEndpointView } from '../contracts.generated'
import { flowRenderers } from './flowRenderers'

interface Props {
  effect: FileEffectView
  selected: boolean
  label: string
  onActivate: (operationId: string, focus: boolean) => void
  onPreview: (effectId: string, endpoint: FileEndpointView) => void
}

export function FlowRow({ effect, selected, label, onActivate, onPreview }: Props) {
  const recipe = flowRenderers[effect.kind]
  const Icon = recipe.icon
  const deleted = effect.kind === 'delete' || effect.kind === 'move'
  return <article className={`flow-row flow-row--${effect.kind}${selected ? ' is-selected' : ''}`} data-effect-id={effect.id}>
    <button className="flow-row__activate" type="button" aria-label={`${recipe.label}: ${label}`} aria-pressed={selected} onClick={(event) => onActivate(effect.operation_id, event.detail === 0)}>
      <span className="flow-row__heading"><Icon size={16} aria-hidden="true" /><strong>{recipe.label}</strong><small>{effect.order + 1}</small></span>
      <span className="flow-row__operation">{label}</span>
      <span className="flow-row__path">
        <span className="flow-endpoints">{effect.inputs.map((endpoint) => <Endpoint key={endpoint.id} endpoint={endpoint} deleted={deleted} />)}</span>
        {(effect.inputs.length > 0 || effect.outputs.length > 0) && <ArrowRight size={16} aria-hidden="true" />}
        <span className="flow-endpoints">{effect.outputs.map((endpoint) => <Endpoint key={endpoint.id} endpoint={endpoint} />)}{effect.kind === 'delete' && <span className="flow-tombstone"><X size={18} aria-hidden="true" />Deleted</span>}{effect.kind === 'observe' && <span>Observed</span>}{effect.kind === 'read' && <span>Read only</span>}</span>
      </span>
      <span className="flow-row__status">{effect.conditional && <span>Conditional</span>}{effect.in_loop && <span>Repeated</span>}{effect.kind === 'move' && <span>Source removed on success</span>}</span>
      {effect.reason && <small className="flow-reason">{effect.reason}</small>}
    </button>
    <div className="flow-previews">{[...effect.inputs, ...effect.outputs].filter((endpoint) => endpoint.path && /\.(csv|tab|asc)$/i.test(endpoint.path)).map((endpoint) => <button type="button" key={endpoint.id} className="flow-preview-button" onClick={() => onPreview(effect.id, endpoint)} aria-label={`Preview on-disk ${endpoint.path}`} title={`Preview on-disk ${endpoint.path}`}><Eye size={14} aria-hidden="true" /><span>{endpoint.path}</span></button>)}</div>
  </article>
}

function Endpoint({ endpoint, deleted = false }: { endpoint: FileEndpointView; deleted?: boolean }) {
  return <span className={`flow-endpoint${deleted ? ' flow-endpoint--deleted' : ''}`}>
    {deleted ? <s>{endpoint.path ?? endpoint.expression ?? 'Unresolved path'}</s> : <span>{endpoint.path ?? endpoint.expression ?? 'Unresolved path'}</span>}
    <small>{endpoint.phase === 'next' ? 'Next state' : 'Prior state'} / {endpoint.path_base.replaceAll('-', ' ')}</small>
    {endpoint.status !== 'known' && <small className={`endpoint-status endpoint-status--${endpoint.status}`}>{endpoint.status}</small>}
  </span>
}