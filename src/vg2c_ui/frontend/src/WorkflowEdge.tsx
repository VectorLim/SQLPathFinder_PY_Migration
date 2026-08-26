import { BaseEdge, EdgeLabelRenderer, getBezierPath, type EdgeProps } from '@xyflow/react'

export function WorkflowEdge(props: EdgeProps) {
  const [path, labelX, labelY] = getBezierPath(props)
  const isData = props.data?.kind === 'data'
  return (
    <>
      <BaseEdge path={path} markerEnd={props.markerEnd} style={props.style} />
      {props.label && (
        <EdgeLabelRenderer>
          <span
            className={`edge-label${isData ? ' edge-label--data' : ''}`}
            style={{ transform: `translate(-50%, -50%) translate(${labelX}px, ${labelY}px)` }}
          >
            {String(props.label)}
          </span>
        </EdgeLabelRenderer>
      )}
    </>
  )
}
