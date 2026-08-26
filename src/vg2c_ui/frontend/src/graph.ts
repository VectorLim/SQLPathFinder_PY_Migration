import { MarkerType, type Edge, type Node } from '@xyflow/react'

import type { ScopeNode, StepNode, WorkflowDocument, WorkflowNode } from './types'

export interface ScopeSummary {
  scopeId: string
  expanded: boolean
  stepCount: number
  csvInputs: string[]
  csvOutputs: string[]
}

export interface WorkflowNodeData extends Record<string, unknown> {
  item: WorkflowNode
  summary?: ScopeSummary
}

export type FlowNode = Node<WorkflowNodeData, 'workflow'>

export function toGraph(document: WorkflowDocument): { nodes: FlowNode[]; edges: Edge[] } {
  const items: WorkflowNode[] = [...document.steps, ...document.scopes, ...document.artifacts]
  return {
    nodes: items.map((item, index) => ({
      id: item.id,
      type: 'workflow',
      position: document.layout.positions[item.id] ?? { x: 80, y: 60 + index * 150 },
      data: { item },
      ariaLabel: labelFor(item),
    })),
    edges: [...document.control_edges, ...document.data_edges].map((item) => ({
      id: item.id,
      source: item.source,
      target: item.target,
      type: 'workflow',
      label: item.label ?? undefined,
      data: { kind: item.kind, valid: item.valid },
      markerEnd: { type: MarkerType.ArrowClosed },
      style: item.dashed
        ? { strokeDasharray: '5 5', stroke: item.valid ? '#98a2b3' : '#b42318', strokeWidth: 1 }
        : { stroke: '#98a2b3', strokeWidth: 1 },
    })),
  }
}

export function projectGraph(
  document: WorkflowDocument,
  nodes: FlowNode[],
  edges: Edge[],
  expandedScopes: Set<string>,
  selectedId: string | null,
): { nodes: FlowNode[]; edges: Edge[] } {
  const scopeById = new Map(document.scopes.map((scope) => [scope.id, scope]))
  const nodeById = new Map(nodes.map((node) => [node.id, node]))
  const selected = selectedId ? nodeById.get(selectedId)?.data.item : undefined
  const visibleArtifacts = relatedArtifactIds(document, selected)

  function representative(id: string): string | null {
    const item = nodeById.get(id)?.data.item
    if (!item) return null
    if (item.node_kind === 'csv-artifact') {
      return visibleArtifacts.has(item.id) ? item.id : null
    }
    let parent = item.parent_scope_id
    while (parent) {
      if (!expandedScopes.has(parent)) return parent
      parent = scopeById.get(parent)?.parent_scope_id ?? null
    }
    return id
  }

  let visibleNodes = nodes.flatMap((node) => {
    const item = node.data.item
    if (representative(node.id) !== node.id) return []
    if (item.node_kind === 'csv-artifact' && !visibleArtifacts.has(item.id)) return []
    if (item.node_kind === 'if' || item.node_kind === 'branch' || item.node_kind === 'loop') {
      return [{
        ...node,
        data: {
          item,
          summary: summarizeScope(document, item, expandedScopes.has(item.id), scopeById),
        },
      }]
    }
    return [node]
  })
  if (expandedScopes.size < document.scopes.length) {
    visibleNodes = compactLayout(visibleNodes, scopeById)
  }

  const projectedEdges = new Map<string, Edge>()
  for (const edge of edges) {
    const kind = edge.data?.kind
    if (kind === 'data' && !visibleArtifacts.has(edge.source) && !visibleArtifacts.has(edge.target)) {
      continue
    }
    const source = representative(edge.source)
    const target = representative(edge.target)
    if (!source || !target || source === target) continue
    const label = kind === 'control' && (edge.label === 'True' || edge.label === 'False')
      ? edge.label
      : undefined
    const key = `${kind}:${source}:${target}:${label ?? ''}`
    if (!projectedEdges.has(key)) {
      projectedEdges.set(key, {
        ...edge,
        id: `projected-${projectedEdges.size}`,
        source,
        target,
        label,
        animated: false,
      })
    }
  }
  return { nodes: visibleNodes, edges: [...projectedEdges.values()] }
}

function compactLayout(
  nodes: FlowNode[],
  scopeById: Map<string, ScopeNode>,
): FlowNode[] {
  const ordered = [...nodes].sort((left, right) => {
    const leftItem = left.data.item
    const rightItem = right.data.item
    const leftIndex = leftItem.node_kind === 'step'
      ? leftItem.block_index
      : leftItem.node_kind === 'csv-artifact'
        ? Number.MAX_SAFE_INTEGER
        : leftItem.start_index
    const rightIndex = rightItem.node_kind === 'step'
      ? rightItem.block_index
      : rightItem.node_kind === 'csv-artifact'
        ? Number.MAX_SAFE_INTEGER
        : rightItem.start_index
    return leftIndex - rightIndex || depth(leftItem, scopeById) - depth(rightItem, scopeById)
  })
  return ordered.map((node, row) => ({
    ...node,
    draggable: false,
    position: node.data.item.node_kind === 'csv-artifact'
      ? { x: -170, y: 50 + row * 115 }
      : { x: 70 + depth(node.data.item, scopeById) * 230, y: 50 + row * 115 },
  }))
}

function depth(item: WorkflowNode, scopeById: Map<string, ScopeNode>): number {
  if (item.node_kind === 'csv-artifact') return 0
  let value = 0
  let parent = item.parent_scope_id
  while (parent) {
    value += 1
    parent = scopeById.get(parent)?.parent_scope_id ?? null
  }
  return value
}

export function labelFor(item: WorkflowNode): string {
  if (item.node_kind === 'step') return item.display_label
  if (item.node_kind === 'csv-artifact') return item.label
  return scopeLabel(item)
}

export function scopeLabel(scope: ScopeNode): string {
  if (scope.scope_kind === 'if') return 'if condition:'
  if (scope.scope_kind === 'if-branch') return 'if True:'
  if (scope.scope_kind === 'else-branch') return 'else:'
  if (scope.scope_kind === 'macro') return 'for each macro row:'
  return 'for each row:'
}

export function ancestorScopeIds(document: WorkflowDocument, nodeId: string): string[] {
  const scopeById = new Map(document.scopes.map((scope) => [scope.id, scope]))
  const item = [...document.steps, ...document.scopes].find((candidate) => candidate.id === nodeId)
  const result: string[] = []
  let parent = item?.parent_scope_id ?? null
  while (parent) {
    result.push(parent)
    parent = scopeById.get(parent)?.parent_scope_id ?? null
  }
  return result
}

export function highlightRelated(nodes: FlowNode[], edges: Edge[], selectedId: string | null) {
  if (!selectedId) return { nodes, edges }
  const related = new Set([selectedId])
  for (const edge of edges) {
    if (edge.source === selectedId || edge.target === selectedId) {
      related.add(edge.source)
      related.add(edge.target)
    }
  }
  return {
    nodes: nodes.map((node) => ({
      ...node,
      style: related.has(node.id) ? undefined : { opacity: 0.38 },
    })),
    edges: edges.map((edge) => ({
      ...edge,
      animated: edge.source === selectedId || edge.target === selectedId,
      style: {
        ...edge.style,
        opacity: related.has(edge.source) && related.has(edge.target) ? 1 : 0.16,
      },
    })),
  }
}

function summarizeScope(
  document: WorkflowDocument,
  scope: ScopeNode,
  expanded: boolean,
  scopeById: Map<string, ScopeNode>,
): ScopeSummary {
  const descendants = document.steps.filter((step) => isInside(step, scope.id, scopeById))
  const descendantIds = new Set(descendants.map((step) => step.id))
  const inputs = new Set(descendants.flatMap((step) => step.csv_inputs))
  const outputs = new Set(descendants.flatMap((step) => step.csv_outputs))
  const outsideInputs = new Set(
    document.steps
      .filter((step) => !descendantIds.has(step.id))
      .flatMap((step) => step.csv_inputs),
  )
  const allInputs = new Set(document.steps.flatMap((step) => step.csv_inputs))
  return {
    scopeId: scope.id,
    expanded,
    stepCount: descendants.length,
    csvInputs: [...inputs].filter((path) => !outputs.has(path)).sort(),
    csvOutputs: [...outputs]
      .filter((path) => outsideInputs.has(path) || !allInputs.has(path))
      .sort(),
  }
}

function isInside(
  step: StepNode,
  scopeId: string,
  scopeById: Map<string, ScopeNode>,
): boolean {
  let parent = step.parent_scope_id
  while (parent) {
    if (parent === scopeId) return true
    parent = scopeById.get(parent)?.parent_scope_id ?? null
  }
  return false
}

function relatedArtifactIds(
  document: WorkflowDocument,
  selected: WorkflowNode | undefined,
): Set<string> {
  if (!selected) return new Set()
  if (selected.node_kind === 'csv-artifact') return new Set([selected.id])
  if (selected.node_kind !== 'step') return new Set()
  const paths = new Set([...selected.csv_inputs, ...selected.csv_outputs])
  return new Set(
    document.artifacts.filter((artifact) => paths.has(artifact.path)).map((artifact) => artifact.id),
  )
}
