import { useEffect, useMemo, useRef, type KeyboardEvent, type ReactNode } from 'react'
import { ChevronRight } from 'lucide-react'

import type { DocumentView, SemanticOperationView } from './contracts.generated'
import { ancestorScopeIds } from './workspaceState'

interface Props {
  document: DocumentView
  search: string
  expandedIds: Set<string>
  selectedId: string | null
  revealVersion: number
  revealFocus: boolean
  onSelect: (id: string) => void
  onToggle: (id: string, expanded?: boolean) => void
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
}: Props) {
  const treeRef = useRef<HTMLUListElement>(null)
  const operations = document.semantic_operations.filter((operation) => operation.visibility !== 'internal')
  const byId = useMemo(() => new Map(operations.map((operation) => [operation.id, operation])), [operations])
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
        operation.description,
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

  function render(parentId: string, depth: number): ReactNode {
    return (children.get(parentId) ?? []).map((operation) => {
      if (visibleIds && !visibleIds.has(operation.id)) return null
      const childOperations = children.get(operation.id) ?? []
      const hasChildren = childOperations.some((child) => !visibleIds || visibleIds.has(child.id))
      const expanded = hasChildren && effectiveExpanded.has(operation.id)
      const selected = operation.id === selectedId
      return <li key={operation.id} role="none" className={`tree-node${selected ? ' is-selected' : ''}`}>
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
          onKeyDown={(event) => handleTreeKey(event, operation, hasChildren, expanded, onToggle)}
        >
          <span className={`tree-toggle${hasChildren ? '' : ' tree-toggle--step'}`} aria-hidden="true">
            {hasChildren ? <ChevronRight className="chevron" size={15} /> : '•'}
          </span>
          <span className="tree-label">
            <strong>{operation.display_name}</strong>
            <small>{operationSummary(operation)}</small>
          </span>
          {operation.validation_state !== 'valid' && <span className="operation-warning" aria-label={operation.validation_state}>!</span>}
        </button>
        {hasChildren && <div className={`tree-branch${expanded ? ' is-open' : ''}`}><ul role="group">{render(operation.id, depth + 1)}</ul></div>}
      </li>
    })
  }

  if (!operations.length) return <p className="empty-copy">No semantic operations were produced for this script.</p>
  return <ul ref={treeRef} className="script-tree semantic-script-tree" role="tree" aria-label="Script Logic">{render('__root__', 1)}</ul>
}

function operationSummary(operation: SemanticOperationView): string {
  const value = (name: string) => operation.bindings.find((binding) => binding.name === name)?.value
  if (operation.kind === 'condition') {
    const lhs = value('lhs')
    const op = value('op')
    const rhs = value('rhs')
    const conj = value('conj')
    const lhs2 = value('lhs2')
    const op2 = value('op2')
    const rhs2 = value('rhs2')
    const first = [lhs, op, rhs].filter((item) => item !== null && item !== undefined && item !== '').join(' ')
    const second = [lhs2, op2, rhs2].filter((item) => item !== null && item !== undefined && item !== '').join(' ')
    return [first, conj && second ? conj : null, second].filter(Boolean).join(' ')
  }
  const fileBinding = operation.bindings.find((binding) => binding.capabilities.some((capability) => capability.startsWith('file-')))
  if (fileBinding?.value) return `${fileBinding.display_label}: ${String(fileBinding.value)}`
  return operation.description
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
