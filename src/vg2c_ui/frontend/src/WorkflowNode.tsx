import { Handle, Position, type NodeProps } from '@xyflow/react'

import { labelFor, type FlowNode } from './graph'

const icons: Record<string, string> = {
  step: '◆',
  if: '◇',
  branch: '⑂',
  loop: '↻',
  'csv-artifact': '▤',
}

export function WorkflowNode({ data, selected }: NodeProps<FlowNode>) {
  const item = data.item
  const summary = data.summary
  const unsupported = item.node_kind === 'step' && item.validation_state === 'unsupported'
  return (
    <article
      className={`workflow-node workflow-node--${summary ? 'group' : item.node_kind}${selected ? ' is-selected' : ''}`}
      aria-label={labelFor(item)}
    >
      <Handle type="target" position={Position.Top} aria-label="Input" />
      <header>
        <span className="node-icon" aria-hidden="true">
          {summary ? (summary.expanded ? '▾' : '▸') : icons[item.node_kind]}
        </span>
        <strong>{labelFor(item)}</strong>
      </header>
      {summary && (
        <>
          <small>{summary.stepCount} step{summary.stepCount === 1 ? '' : 's'} · click to {summary.expanded ? 'collapse' : 'expand'}</small>
          <FileBadges inputs={summary.csvInputs} outputs={summary.csvOutputs} limit={2} />
        </>
      )}
      {item.node_kind === 'step' && (
        <>
          <small>{item.functional_kind.replaceAll('_', ' ')}</small>
          <FileBadges inputs={item.csv_inputs} outputs={item.csv_outputs} />
          {unsupported && <div className="badges"><span className="warning">read-only</span></div>}
        </>
      )}
      {item.node_kind === 'csv-artifact' && (item.conditional || item.in_loop) && (
        <div className="badges">
          {item.conditional && <span className="warning">conditional</span>}
          {item.in_loop && <span>loop output</span>}
        </div>
      )}
      <Handle type="source" position={Position.Bottom} aria-label="Output" />
    </article>
  )
}

function FileBadges({
  inputs,
  outputs,
  limit,
}: {
  inputs: string[]
  outputs: string[]
  limit?: number
}) {
  if (!inputs.length && !outputs.length) return null
  return (
    <div className="badges">
      <FileBadgeList direction="in" paths={inputs} limit={limit} />
      <FileBadgeList direction="out" paths={outputs} limit={limit} />
    </div>
  )
}

function FileBadgeList({
  direction,
  paths,
  limit = paths.length,
}: {
  direction: 'in' | 'out'
  paths: string[]
  limit?: number
}) {
  const hiddenCount = Math.max(0, paths.length - limit)
  return (
    <>
      {paths.slice(0, limit).map((path) => (
        <span key={`${direction}-${path}`}>{direction}: {path}</span>
      ))}
      {hiddenCount > 0 && <span>+{hiddenCount} {direction === 'in' ? 'inputs' : 'outputs'}</span>}
    </>
  )
}
