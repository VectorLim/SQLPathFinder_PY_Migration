import { useEffect, useMemo, useRef, useState, type KeyboardEvent, type ReactNode } from 'react'
import { ChevronRight, ArrowUp, ArrowDown, GripVertical } from 'lucide-react'

import type { DocumentView, SemanticOperationView } from './api/contracts.generated'
import { ancestorScopeIds } from './workspace/state'

interface Props {
  document: DocumentView
  search: string
  expandedIds: Set<string>
  selectedId: string | null
  revealVersion: number
  revealFocus: boolean
  onSelect: (id: string) => void
  onToggle: (id: string, expanded?: boolean) => void
  onReorder: (sourceScopeId: number, targetScopeId: number) => void
  reorderDisabled?: boolean
}

export function SemanticScriptTree({
  document,
  search,
  expandedIds,
  selectedId,
  revealVersion,
  revealFocus,
  onSelect,
  onToggle,
  onReorder,
  reorderDisabled = false,
}: Props) {
  const treeRef = useRef<HTMLUListElement>(null)
  const [dragSourceId, setDragSourceId] = useState<string | null>(null)
  const [announcement, setAnnouncement] = useState('')
  const operations = document.semantic_operations
  const children = useMemo(() => {
    const index = new Map<string, SemanticOperationView[]>()
    for (const operation of operations) {
      const parent = operation.parent_operation_id ?? '__root__'
      index.set(parent, [...(index.get(parent) ?? []), operation])
    }
    return index
  }, [operations])

  const query = search.trim().toLowerCase()
  const visibleIds = useMemo(() => {
    if (!query) return null
    const ids = new Set<string>()
    for (const operation of operations) {
      const haystack = [
        operation.display_name,
        operation.summary,
        ...operation.comments,
        ...operation.bindings.flatMap((binding) => [binding.display_label, String(binding.value ?? '')]),
      ].join(' ').toLowerCase()
      if (!haystack.includes(query)) continue
      ids.add(operation.id)
      for (const ancestor of ancestorScopeIds(document, operation.id)) ids.add(ancestor)
    }
    return ids
  }, [document, operations, query])

  const effectiveExpanded = new Set(expandedIds)
  if (visibleIds) {
    for (const id of visibleIds) {
      for (const ancestor of ancestorScopeIds(document, id)) effectiveExpanded.add(ancestor)
    }
  }

  useEffect(() => {
    const row = [...(treeRef.current?.querySelectorAll<HTMLButtonElement>('[data-semantic-tree-item]') ?? [])]
      .find((item) => item.dataset.semanticTreeItem === selectedId)
    if (!row) return
    row.scrollIntoView({ block: 'nearest' })
    if (revealFocus) row.focus({ preventScroll: true })
  }, [selectedId, revealVersion, revealFocus])

  const visibleOperations = operations.filter((operation) => !visibleIds || visibleIds.has(operation.id))
  const focusableId = selectedId && visibleOperations.some((operation) => operation.id === selectedId)
    ? selectedId
    : visibleOperations[0]?.id ?? null

  function move(source: SemanticOperationView, target: SemanticOperationView) {
    if (reorderDisabled || query || source.scope_id === null || target.scope_id === null
      || !source.reorder_targets.includes(target.scope_id)) return
    onReorder(source.scope_id, target.scope_id)
    setAnnouncement(`Requested move of ${source.display_name} ${operations.indexOf(source) < operations.indexOf(target) ? 'down' : 'up'}.`)
  }

  function render(parentId: string, depth: number): ReactNode {
    return (children.get(parentId) ?? []).map((operation) => {
      if (visibleIds && !visibleIds.has(operation.id)) return null
      const childOperations = children.get(operation.id) ?? []
      const hasChildren = childOperations.some((child) => !visibleIds || visibleIds.has(child.id))
      const expanded = hasChildren && effectiveExpanded.has(operation.id)
      const selected = operation.id === selectedId
      const siblings = children.get(parentId) ?? []
      const siblingIndex = siblings.findIndex((item) => item.id === operation.id)
      const previous = siblings[siblingIndex - 1]
      const next = siblings[siblingIndex + 1]
      const canMove = (target: SemanticOperationView | undefined) =>
        operation.scope_id !== null && target?.scope_id !== null &&
        target?.scope_id !== undefined && operation.reorder_targets.includes(target.scope_id)
      const source = operations.find((item) => item.id === dragSourceId)
      const validDrop = source && source.id !== operation.id && source.reorder_targets.includes(operation.scope_id ?? -1)
      return <li key={operation.id} role="none" className={`tree-node${selected ? ' is-selected' : ''}${validDrop ? ' is-drop-target' : ''}`}
        onDragOver={(event) => { if (validDrop && !reorderDisabled && !query) { event.preventDefault(); event.stopPropagation() } }}
        onDrop={(event) => {
          event.preventDefault()
          event.stopPropagation()
          const sourceId = event.dataTransfer.getData('application/x-sqlpathfinder-operation-id')
          const dragged = operations.find((item) => item.id === sourceId)
          if (dragged) move(dragged, operation)
          setDragSourceId(null)
        }}>
        <div className="semantic-tree-row-wrap">
        <button
          type="button"
          role="treeitem"
          className="tree-row semantic-tree-row"
          aria-level={depth}
          aria-selected={selected}
          aria-expanded={hasChildren ? expanded : undefined}
          aria-invalid={operation.validation_state === 'unresolved' || operation.validation_state === 'unsupported' || undefined}
          tabIndex={operation.id === focusableId ? 0 : -1}
          data-semantic-tree-item={operation.id}
          data-parent-id={operation.parent_operation_id ?? ''}
          onClick={() => onSelect(operation.id)}
          onKeyDown={(event) => {
            if (event.altKey && (event.key === 'ArrowUp' || event.key === 'ArrowDown')) {
              event.preventDefault()
              const target = event.key === 'ArrowUp' ? previous : next
              if (target) move(operation, target)
            } else handleTreeKey(event, operation, hasChildren, expanded, onToggle)
          }}
        >
          <span className={`tree-toggle${hasChildren ? '' : ' tree-toggle--step'}`} aria-hidden="true">
            {hasChildren ? <ChevronRight className="chevron" size={15} /> : '•'}
          </span>
          <span className="tree-label">
            <strong>{operation.display_name}</strong>
            {operation.summary && <small>{operation.summary}</small>}
          </span>
          {operation.validation_state !== 'valid' && <span className="operation-warning" aria-label={operation.validation_state}>!</span>}
        </button>
        {(canMove(previous) || canMove(next)) && <span className="tree-reorder-controls">
          <button type="button" className="tree-drag-handle" aria-label={`Drag ${operation.display_name} to reorder`}
            draggable={!reorderDisabled && !query}
            disabled={reorderDisabled || Boolean(query)}
            onDragStart={(event) => {
              event.dataTransfer.setData('application/x-sqlpathfinder-operation-id', operation.id)
              event.dataTransfer.effectAllowed = 'move'
              setDragSourceId(operation.id)
            }}
            onDragEnd={() => setDragSourceId(null)}><GripVertical size={14} /></button>
          {canMove(previous) && <button type="button" aria-label={`Move ${operation.display_name} up`}
            disabled={reorderDisabled || Boolean(query)}
            onClick={() => move(operation, previous)}><ArrowUp size={14} /></button>}
          {canMove(next) && <button type="button" aria-label={`Move ${operation.display_name} down`}
            disabled={reorderDisabled || Boolean(query)}
            onClick={() => move(operation, next)}><ArrowDown size={14} /></button>}
        </span>}
        </div>
        {hasChildren && <div className={`tree-branch${expanded ? ' is-open' : ''}`}><ul role="group">{render(operation.id, depth + 1)}</ul></div>}
      </li>
    })
  }

  if (!operations.length) return <p className="empty-copy">No semantic operations were produced for this script.</p>
  return <><ul ref={treeRef} className="script-tree semantic-script-tree" role="tree" aria-label="Script Logic">{render('__root__', 1)}</ul><span className="sr-only" role="status" aria-live="polite">{announcement}</span></>
}

function handleTreeKey(
  event: KeyboardEvent<HTMLButtonElement>,
  operation: SemanticOperationView,
  hasChildren: boolean,
  expanded: boolean,
  onToggle: (id: string, expanded?: boolean) => void,
) {
  const root = event.currentTarget.closest('[role="tree"]')
  const visible = [...(root?.querySelectorAll<HTMLButtonElement>('[data-semantic-tree-item]') ?? [])]
    .filter((element) => element.offsetParent !== null)
  const index = visible.indexOf(event.currentTarget)
  if (event.key === 'ArrowDown') {
    event.preventDefault()
    visible[Math.min(index + 1, visible.length - 1)]?.focus()
  } else if (event.key === 'ArrowUp') {
    event.preventDefault()
    visible[Math.max(index - 1, 0)]?.focus()
  } else if (event.key === 'Home') {
    event.preventDefault()
    visible[0]?.focus()
  } else if (event.key === 'End') {
    event.preventDefault()
    visible.at(-1)?.focus()
  } else if (event.key === 'ArrowRight' && hasChildren) {
    event.preventDefault()
    if (!expanded) onToggle(operation.id, true)
    else visible[index + 1]?.focus()
  } else if (event.key === 'ArrowLeft') {
    event.preventDefault()
    if (hasChildren && expanded) onToggle(operation.id, false)
    else if (operation.parent_operation_id) {
      visible.find((item) => item.dataset.semanticTreeItem === operation.parent_operation_id)?.focus()
    }
  }
}
